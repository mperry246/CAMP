using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.GL;
using PX.Objects.TX.Data;
using PX.Common;

namespace PX.Objects.TX
{
	public class TaxCalendar
	{
		public class CreationParams
		{
			public int BranchID { get; set; }

			public int TaxAgencyID { get; set; }

			public string TaxPeriodType { get; set; }

			public DateTime StartDateTime { get; set; }

			public int? TaxYearNumber { get; set; }

			public int? PeriodCount { get; set; }

			public static CreationParams FromTaxYear(TaxYear taxYear)
			{
				return new CreationParams()
				{
					BranchID = taxYear.BranchID.Value,
					TaxAgencyID = taxYear.VendorID.Value,
					TaxPeriodType = taxYear.TaxPeriodType,
					StartDateTime = taxYear.StartDate.Value,
					TaxYearNumber = Convert.ToInt32(taxYear.Year),
					PeriodCount = taxYear.PeriodsCount
				};
			}
		}
	}

	public class TaxCalendar<TTaxYear, TTaxPeriod>: TaxCalendar
		where TTaxYear: TaxYear, new()
		where TTaxPeriod : TaxPeriod, new()
	{
		protected readonly PXGraph Graph;

		public TaxCalendar(PXGraph graph)
		{
			Graph = graph;
		}

		public virtual TaxPeriod GetOrCreateCurrentTaxPeriod(PXCache taxYearCache, PXCache taxPeriodCache, int? branchID, int? taxAgencyID)
		{
			TaxPeriod taxper = null;

			Vendor vendor = VendorMaint.GetByID(Graph, taxAgencyID);

			taxper = TaxYearMaint.FindPreparedPeriod(Graph, branchID, taxAgencyID);
			if (taxper != null)
			{
				return taxper;
			}

			TaxTran first_tran;

			using (new PXReadBranchRestrictedScope(branchID))
			{
				first_tran = ReportTax.GetEarliestNotReportedTaxTran(Graph, vendor);
			}

			DateTime? first_date = first_tran != null && first_tran.TranDate != null
				? first_tran.TranDate
				: Graph.Accessinfo.BusinessDate;

			if (first_tran != null && vendor.TaxReportFinPeriod == true)
			{
				first_date = first_tran.FinDate;
			}

			TaxPeriod lastTaxPeriod = TaxYearMaint.FindLastTaxPeriod(Graph, branchID, taxAgencyID);

			if (lastTaxPeriod?.Status == TaxPeriodStatus.Closed)
			{
				CreateAndAddToCache(taxYearCache, taxPeriodCache, branchID, vendor);
			}

			taxper = TaxYearMaint.FinTaxPeriodByDate(Graph, branchID, taxAgencyID, first_date);

			if (taxper != null && (vendor.UpdClosedTaxPeriods == true ||
								   taxper.Status == TaxPeriodStatus.Dummy ||
								   taxper.Status == TaxPeriodStatus.Open))
			{
				return taxper;
			}

			taxper = TaxYearMaint.FindFirstOpenTaxPeriod(Graph, branchID, taxAgencyID);
			if (taxper != null)
			{
				return taxper;
			}

			if (first_date != null)
			{
				CreateAndAddToCache(taxYearCache, taxPeriodCache, branchID, vendor, first_date.Value.Year);

				taxper = TaxYearMaint.FinTaxPeriodByDate(Graph, branchID, taxAgencyID, first_date);

				if (taxper != null)
				{
					taxper.Status = first_tran == null || first_tran.TranDate == null
										? TaxPeriodStatus.Dummy
										: taxper.Status;
				}
				else
				{
					taxper = TaxYearMaint.FinTaxPeriodByDate(Graph, branchID, taxAgencyID, Graph.Accessinfo.BusinessDate);

					if (taxper == null)
					{
						taxper = taxPeriodCache.Cached.Cast<TTaxPeriod>()
												.Where(period => period.Status == TaxPeriodStatus.Open)
												.OrderBy(period => period.TaxPeriodID)
												.FirstOrDefault();
					}
				}
			}

			return taxper;
		}

		public virtual TaxYearWithPeriods<TTaxYear, TTaxPeriod> CreateByFinancialPeriod(int? branchID, int? taxAgencyID, DateTime? date, int? periodCount)
		{
			var taxYearWithPeriods = new TaxYearWithPeriods<TTaxYear, TTaxPeriod>();

			foreach (PXResult<FinYear, FinPeriod> res in
												PXSelectJoin<FinYear,
															InnerJoin<FinPeriod,
																On<FinPeriod.finYear, Equal<FinYear.year>>>,
															Where<FinYear.startDate, LessEqual<Required<FinYear.startDate>>>,
															OrderBy<Desc<FinYear.year>>>.Select(Graph, (object)date))
			{
				FinYear finYear = res;

				if (taxYearWithPeriods.TaxYear == null)
				{
					taxYearWithPeriods.TaxYear = new TTaxYear
					{
						Year = finYear.Year,
						StartDate = finYear.StartDate,
						VendorID = taxAgencyID,
						BranchID = branchID,
						TaxPeriodType = VendorTaxPeriodType.FiscalPeriod,
						PlanPeriodsCount = 0,
						Filed = false
					};
				}
				else if (object.Equals(finYear.Year, taxYearWithPeriods.TaxYear.Year) == false)
				{
					break;
				}

				taxYearWithPeriods.TaxYear.PlanPeriodsCount++;

				if (periodCount == null
				    || taxYearWithPeriods.TaxPeriods.Count <= periodCount)

				{
					FinPeriod finPeriod = res;

					taxYearWithPeriods.TaxPeriods.Add(new TTaxPeriod
					{
						TaxYear = finPeriod.FinYear,
						TaxPeriodID = finPeriod.FinPeriodID,
						StartDate = finPeriod.StartDate,
						EndDate = finPeriod.EndDate,
						VendorID = taxAgencyID
					});
				}
			}

			taxYearWithPeriods.TaxYear.PeriodsCount = periodCount ?? taxYearWithPeriods.TaxYear.PlanPeriodsCount;

			return taxYearWithPeriods;
		}

		/// <summary>
		/// Creates <see cref="TaxPeriod"/> and <see cref="TaxYear"/> records based on specified tax calendar periods configuration (not on financial periods).
		/// </summary>
		/// <param name="branchID">The key of a master <see cref="Branch"/> record.</param>
		/// <param name="taxAgencyID">The key of a tax agency (<see cref="Vendor"/>).</param>
		/// <param name="taxPeriodType">Value of <see cref="VendorTaxPeriodType"/>.</param>
		/// <param name="startDate">Defines starting a day and a month of the target year and a year if the previous year does not exist.</param>
		/// <param name="taxYear">It is defined when previous <see cref="TaxYear"/> record exists and it is needed to create subsequent records.</param>
		/// <param name="periodCount">It is defined if <see cref="TaxYear"/> record has custom period count.</param>
		public virtual TaxYearWithPeriods<TTaxYear, TTaxPeriod> CreateByIndependentTaxCalendar(CreationParams creationParams)
		{
			short planPeriodsCount = 0;

			switch (creationParams.TaxPeriodType)
			{
				case VendorTaxPeriodType.Monthly:
					planPeriodsCount = 12;
					break;
				case VendorTaxPeriodType.SemiMonthly:
					planPeriodsCount = 24;
					break;
				case VendorTaxPeriodType.Quarterly:
					planPeriodsCount = 4;
					break;
				case VendorTaxPeriodType.Yearly:
					planPeriodsCount = 1;
					break;
				case VendorTaxPeriodType.BiMonthly:
					planPeriodsCount = 6;
					break;
				case VendorTaxPeriodType.SemiAnnually:
					planPeriodsCount = 2;
					break;
			}

			int calendarYear = creationParams.StartDateTime.Year;
			int month = creationParams.StartDateTime.Month;

			var taxYearWithPeriods = new TaxYearWithPeriods<TTaxYear, TTaxPeriod>
			{
				TaxYear = new TTaxYear
				{
					BranchID = creationParams.BranchID,
					VendorID = creationParams.TaxAgencyID,
					Year = (creationParams.TaxYearNumber ?? calendarYear).ToString(),
					StartDate = creationParams.StartDateTime,
					TaxPeriodType = creationParams.TaxPeriodType,
					PlanPeriodsCount = planPeriodsCount,
					Filed = false
				}
			};

			taxYearWithPeriods.TaxYear.PeriodsCount = creationParams.PeriodCount ?? planPeriodsCount;

			for (int i = 1; i <= planPeriodsCount && i <= taxYearWithPeriods.TaxYear.PeriodsCount; i++)
			{
				TTaxPeriod newTaxPeriod = new TTaxPeriod
				{
					BranchID = taxYearWithPeriods.TaxYear.BranchID,
					VendorID = taxYearWithPeriods.TaxYear.VendorID,
					TaxYear = taxYearWithPeriods.TaxYear.Year,
					TaxPeriodID = taxYearWithPeriods.TaxYear.Year + (i < 10 ? "0" : "") + i,
					Status = TaxPeriod.status.DefaultStatus,
					Filed = false
				};

				if (planPeriodsCount <= 12)
				{
					newTaxPeriod.StartDate = new DateTime(calendarYear, month, 1);
					month += 12 / (int)planPeriodsCount;
					if (month > 12)
					{
						month -= 12;
						calendarYear++;
					}
					newTaxPeriod.EndDate = new DateTime(calendarYear, month, 1);
				}
				else if (planPeriodsCount == 24)
				{
					newTaxPeriod.StartDate = (i % 2 == 1) ? new DateTime(calendarYear, month, 1) : new DateTime(calendarYear, month, 16);
					if (i % 2 == 0)
					{
						month++;
						if (month > 12)
						{
							month -= 12;
							calendarYear++;
						}
					}
					newTaxPeriod.EndDate = (i % 2 == 1) ? new DateTime(calendarYear, month, 16) : new DateTime(calendarYear, month, 1);
				}

				taxYearWithPeriods.TaxPeriods.Add(newTaxPeriod);
			}

			return taxYearWithPeriods;
		}

		public virtual TaxYearWithPeriods<TTaxYear, TTaxPeriod> CreateWithCorrespondingTaxPeriodType(CreationParams creationParams)
		{
			return creationParams.TaxPeriodType == VendorTaxPeriodType.FiscalPeriod
				? CreateByFinancialPeriod(creationParams.BranchID,
											creationParams.TaxAgencyID,
											creationParams.StartDateTime,
											creationParams.PeriodCount)
				: CreateByIndependentTaxCalendar(creationParams);
		}

		public virtual void CreateAndAddToCache(PXCache taxYearCache, PXCache taxPeriodCache, int? branchID, Vendor vendor, int? calendarYear = null)
		{
			TaxYearWithPeriods<TTaxYear, TTaxPeriod> taxYearWithPeriods = Create(branchID, vendor, calendarYear);

			taxYearCache.Insert(taxYearWithPeriods.TaxYear);

			foreach (TTaxPeriod taxPeriod in taxYearWithPeriods.TaxPeriods)
			{
				taxPeriodCache.Insert(taxPeriod);
			}
		}

		public virtual TaxYearWithPeriods<TTaxYear, TTaxPeriod> Create(int? branchID, Vendor vendor, int? calendarYear = null)
		{
			Graph.Caches[typeof(TaxYear)].Clear();
			Graph.Caches[typeof(TaxYear)].ClearQueryCache();
			Graph.Caches[typeof(TaxPeriod)].Clear();
			Graph.Caches[typeof(TaxPeriod)].ClearQueryCache();

			TTaxYear lastYear = null;

			TaxYear taxYear = TaxYearMaint.FindLastTaxYear(Graph, branchID, vendor.BAccountID);

			if (taxYear != null)
			{
				lastYear = new TTaxYear();
				Graph.Caches[typeof (TaxYear)].RestoreCopy(lastYear, taxYear);
			}

			CreationParams creationParams = new CreationParams()
			{
				BranchID = branchID.Value,
				TaxAgencyID = vendor.BAccountID.Value
			};

			if (lastYear != null)
			{
				creationParams.TaxPeriodType = lastYear.TaxPeriodType;

				TaxPeriod lastPeriod = TaxYearMaint.FindLastTaxPeriod(Graph, branchID, vendor.BAccountID);

				if (lastPeriod != null)
				{
					int yearNumber = Convert.ToInt32(lastYear.Year); 

					if (lastPeriod.TaxYear != lastYear.Year)
					{
						creationParams.PeriodCount = lastYear.PeriodsCount;
					}
					else
					{
						yearNumber++;
					}

					creationParams.StartDateTime = lastPeriod.EndDate.Value;
					creationParams.TaxYearNumber = yearNumber;

					return CreateWithCorrespondingTaxPeriodType(creationParams);
				}
				else
				{
					//no existing periods - only header
					creationParams.StartDateTime = lastYear.StartDate.Value;
					creationParams.TaxYearNumber = Convert.ToInt32(lastYear.Year);
					creationParams.PeriodCount = lastYear.PeriodsCount;

					return CreateWithCorrespondingTaxPeriodType(creationParams);
				}
			}
			else
			{
				FillCalendarDataWhenLastBranchTaxYearDoesNotExist(calendarYear.Value, vendor, creationParams);

				return CreateWithCorrespondingTaxPeriodType(creationParams);
			}
		}

		public void FillCalendarDataWhenLastBranchTaxYearDoesNotExist(int calendarYear, Vendor vendor, CreationParams creationParams)
		{
			TTaxYear otherBranchLastYear = null;

			TaxYear taxYear = PXSelect<TaxYear,
										Where<TaxYear.vendorID, Equal<Required<TaxYear.vendorID>>>,
										OrderBy<Desc<TaxYear.year>>>
										.SelectWindowed(Graph, 0, 1, vendor.BAccountID);

			if (taxYear != null)
			{
				otherBranchLastYear = new TTaxYear();
				Graph.Caches[typeof(TaxYear)].RestoreCopy(otherBranchLastYear, taxYear);
			}

			if (otherBranchLastYear != null)
			{
				int yearShift = Convert.ToInt32(otherBranchLastYear.Year) - otherBranchLastYear.StartDate.Value.Year;

				creationParams.StartDateTime = new DateTime(calendarYear - yearShift,
															otherBranchLastYear.StartDate.Value.Month,
															otherBranchLastYear.StartDate.Value.Day);

				creationParams.TaxPeriodType = otherBranchLastYear.TaxPeriodType;
			}
			else
			{
				creationParams.StartDateTime = new DateTime(calendarYear, 1, 1);
				creationParams.TaxPeriodType = vendor.TaxPeriodType;
			}

			creationParams.TaxYearNumber = calendarYear;
		}

		/// <summary>
		/// Returns TaxPeriod form the given date.
		/// </summary>
		[Obsolete("The method is obsolete and will be removed in Acumatica 8.0.")]
		public static string GetPeriod(int vendorID, DateTime? fromdate)
		{
			using (PXDataRecord record = PXDatabase.SelectSingle<TaxPeriod>(
				new PXDataField(typeof(TaxPeriod.taxPeriodID).Name),
				new PXDataFieldValue(typeof(TaxPeriod.vendorID).Name, PXDbType.Int, vendorID),
				new PXDataFieldValue(typeof(TaxPeriod.startDate).Name, PXDbType.SmallDateTime, 4, fromdate, PXComp.LE),
				new PXDataFieldValue(typeof(TaxPeriod.endDate).Name, PXDbType.SmallDateTime, 4, fromdate, PXComp.GT)))
			{
				if (record != null)
				{
					return record.GetString(0);
				}
			}
			return null;
		}
	}
}
