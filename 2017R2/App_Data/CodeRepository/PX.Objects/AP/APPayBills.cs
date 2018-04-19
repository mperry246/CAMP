using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

using PX.Data;

using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.CM;
using PX.Objects.CA;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.AP.MigrationMode;

namespace PX.Objects.AP
{
	[TableAndChartDashboardType]
	public class APPayBills : PXGraph<APPayBills>
	{
		public PXFilter<PayBillsFilter> Filter;
		public PXCancel<PayBillsFilter> Cancel;
		public PXAction<PayBillsFilter> ViewDocument;

		[PXFilterable]
		public PXFilteredProcessingJoin<APAdjust, PayBillsFilter, 
			InnerJoin<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>>> APDocumentList;

		public PXSelectJoin<APAdjust, 
			InnerJoin<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>>> APExceptionsList;

		public ToggleCurrency<PayBillsFilter> CurrencyView;

		public PXSelect<CurrencyInfo> currencyinfo;
		public PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
		
		#region Setups
		public PXSetup<GL.Company> Company;

		public PXSetup<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<PayBillsFilter.payAccountID>>>> cashaccount;

		public PXSetup<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Current<PayBillsFilter.payAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<PrintChecksFilter.payTypeID>>>>> cashaccountdetail;

		public PXSetup<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<PayBillsFilter.payTypeID>>>> paymenttype;

		public APSetupNoMigrationMode APSetup;

		#endregion

		public APPayBills()
		{
			APSetup setup = APSetup.Current;

			APDocumentList.SetSelected<APAdjust.selected>();
			APDocumentList.SetProcessCaption(Messages.Process);
			APDocumentList.SetProcessAllCaption(Messages.ProcessAll);

			APDocumentList.Cache.AllowInsert = true;
			PXUIFieldAttribute.SetEnabled<APAdjust.adjdDocType>(APDocumentList.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<APAdjust.adjdRefNbr>(APDocumentList.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgAmt>(APDocumentList.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgDiscAmt>(APDocumentList.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<APAdjust.separateCheck>(APDocumentList.Cache, null, true);

			PXUIFieldAttribute.SetVisible<PayBillsFilter.curyID>(Filter.Cache, null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
			this.APExceptionsList.AllowInsert = false;
			this.APExceptionsList.AllowUpdate = false;
			this.APExceptionsList.AllowDelete = false;
		}

		public override bool IsDirty
		{
			get
			{
				return false;
			}
		}

		public override void Clear()
		{
			Filter.Current.CuryInfoID = null;
			Filter.Current.CurySelTotal = 0m;
			Filter.Current.SelTotal = 0m;
			Filter.Current.SelCount = 0;
			base.Clear();
		}
		public PXAction<PayBillsFilter> viewInvoice;
		[PXUIField(DisplayName = Messages.ViewInvoice, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton]
		public virtual IEnumerable ViewInvoice(PXAdapter adapter)
		{
			if (APExceptionsList.Current != null)
			{
				PXResultset<APInvoice> select = PXSelectJoin <APInvoice,
						InnerJoin<APAdjust,
							On<Current<APAdjust.adjdDocType>, Equal<APInvoice.docType>,
								And<Current<APAdjust.adjdRefNbr>, Equal<APInvoice.refNbr>>>>>.Select(this);
				
				APInvoice invoice = (APInvoice)select ;
				APInvoiceEntry graph = PXGraph.CreateInstance<APInvoiceEntry>();
				PXRedirectHelper.TryRedirect(graph, invoice, PXRedirectHelper.WindowMode.NewWindow);
			}
			return adapter.Get();
		}

        protected virtual void PayBillsFilter_Days_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
        {
            e.ReturnValue = PXLocalizer.Localize(e.ReturnValue as string);
        }

        protected virtual void PayBillsFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			if (e.Row != null && cashaccount.Current != null && object.Equals(cashaccount.Current.CashAccountID, ((PayBillsFilter)e.Row).PayAccountID) == false)
			{
				cashaccount.Current = null;
			}

			if (e.Row != null && cashaccountdetail.Current != null && (object.Equals(cashaccountdetail.Current.CashAccountID, ((PayBillsFilter)e.Row).PayAccountID) == false || object.Equals(cashaccountdetail.Current.PaymentMethodID, ((PayBillsFilter)e.Row).PayTypeID) == false))
			{
				cashaccountdetail.Current = null;
			}

			if (e.Row != null && paymenttype.Current != null && (object.Equals(paymenttype.Current.PaymentMethodID, ((PayBillsFilter)e.Row).PayTypeID) == false))
			{
				paymenttype.Current = null;
			}

			PayBillsFilter filter = e.Row as PayBillsFilter;
			if (filter != null)
			{
				CurrencyInfo info = CurrencyInfo_CuryInfoID.Select(filter.CuryInfoID);
				PaymentMethod paytype = paymenttype.Current;
				APDocumentList.SetProcessDelegate(
					delegate(List<APAdjust> list)
					{
						var graph = PXGraph.CreateInstance<APPayBills>();
						graph.CreatePayments(list, filter, info, paytype);
					}
				);
			}

			PXUIFieldAttribute.SetDisplayName<APAdjust.vendorID>(APDocumentList.Cache, Messages.VendorID);
			PXUIFieldAttribute.SetVisible<APAdjust.vendorID>(APDocumentList.Cache,null,true);

			if (HttpContext.Current != null && Filter.Current.BranchID != PXAccess.GetBranchID())
			{
				Filter.Current.BranchID = PXAccess.GetBranchID();
			}
		}
		protected virtual void APAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
			if (string.IsNullOrEmpty(((APAdjust)e.Row).AdjdRefNbr))
			{
				e.Cancel = true;
			}			
		}

		private bool CheckIfRowNotApprovedForPayment(APAdjust row) 
		{
			if (APSetup.Current.RequireApprovePayments != true)
				return false;

			var invoice = (APInvoice)PXSelectorAttribute.Select<APAdjust.adjdRefNbr>(APDocumentList.Cache, row);
			return invoice != null && !(invoice.DocType == APDocType.DebitAdj || invoice.PaySel == true);
		}
		
		protected virtual void APAdjust_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			if (e.Row == null || PXLongOperation.Exists(UID)) return;
			PXUIFieldAttribute.SetEnabled<APAdjust.adjdDocType>(sender, e.Row, false);
			PXUIFieldAttribute.SetEnabled<APAdjust.adjdRefNbr>(sender, e.Row, false);
			if (CheckIfRowNotApprovedForPayment((APAdjust)e.Row))
				APDocumentList.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>((APAdjust)e.Row, 0m, new PXSetPropertyException(Messages.DocumentNotApprovedNotProceed, PXErrorLevel.RowWarning));

			PXException ex = null;
			if (APSetup.Current.EarlyChecks == false && ((APAdjust)e.Row).AdjdDocDate > Filter.Current.PayDate)
			{
				ex = new PXSetPropertyException(Messages.ApplDate_Less_DocDate, PXErrorLevel.RowWarning, PXUIFieldAttribute.GetDisplayName<PayBillsFilter.payDate>(Filter.Cache));
			}

			if (APSetup.Current.EarlyChecks == false && !string.IsNullOrEmpty(Filter.Current.PayFinPeriodID) && string.Compare(((APAdjust)e.Row).AdjdFinPeriodID, Filter.Current.PayFinPeriodID) > 0)
			{
				ex = new PXSetPropertyException(Messages.ApplPeriod_Less_DocPeriod, PXErrorLevel.RowWarning, PXUIFieldAttribute.GetDisplayName<PayBillsFilter.payFinPeriodID>(Filter.Cache));
			}

			sender.RaiseExceptionHandling<APAdjust.selected>(e.Row, false, ex);
			PXUIFieldAttribute.SetEnabled<APAdjust.selected>(sender, e.Row, ex == null && ((APAdjust)e.Row).CuryDocBal != null && !string.IsNullOrEmpty(Filter.Current.PayFinPeriodID));

		}

		Dictionary<object, object> _copies = new Dictionary<object, object>();

		protected virtual IEnumerable aPExceptionsList()
		{
			PayBillsFilter filter = Filter.Current;

			if (filter?.PayDate == null || filter.PayTypeID == null || filter.PayAccountID == null)
				yield break;

			DateTime PayInLessThan = ((DateTime)filter.PayDate).AddDays(filter.PayInLessThan.GetValueOrDefault());
			DateTime DueInLessThan = ((DateTime)filter.PayDate).AddDays(filter.DueInLessThan.GetValueOrDefault());
			DateTime DiscountExpiresInLessThan = ((DateTime)filter.PayDate).AddDays(filter.DiscountExpiresInLessThan.GetValueOrDefault());

			PXSelectBase<APInvoice> cmd = new PXSelectJoin<
				APInvoice,
					InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>,
					InnerJoin<Vendor,
						On<Vendor.bAccountID, Equal<APInvoice.vendorID>,
						And<Where<
							Vendor.status, Equal<BAccount.status.active>,
							Or<Vendor.status, Equal<BAccount.status.oneTime>>>>>,
					InnerJoin<APAdjust,
						On<APAdjust.adjdDocType, Equal<APInvoice.docType>,
						And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>,
						And<APAdjust.released, Equal<False>>>>,
					
					InnerJoin<APPayment,
						On<APPayment.docType, Equal<APAdjust.adjgDocType>,
						And<APPayment.refNbr, Equal<APAdjust.adjgRefNbr>>>,

					CrossJoin<APSetup>>>>>,
				Where<
					APInvoice.openDoc, Equal<True>,
					And2<Where<
						APInvoice.released, Equal<True>,
						Or<APInvoice.prebooked, Equal<True>>>,
					And2<Where<
						APInvoice.paySel, Equal<True>,
						Or2<Where<APSetup.requireApprovePayments, Equal<False>>,
						Or<APInvoice.docType, Equal<APDocType.debitAdj>>>>,
					And2<Where<
						APInvoice.vendorID, Equal<Current<PayBillsFilter.vendorID>>,
						Or<Current<PayBillsFilter.vendorID>, IsNull>>,
					And<APInvoice.payAccountID, Equal<Current<PayBillsFilter.payAccountID>>,
					And<APInvoice.payTypeID, Equal<Current<PayBillsFilter.payTypeID>>,
					And2<Where<APPayment.status, Equal<APDocStatus.hold>,
								Or2<Where<APPayment.status, Equal<APDocStatus.pendingApproval>>,
								Or<APPayment.status, Equal<APDocStatus.rejected>>>>,
					And2<Where2<Where2<
						Where<
							Current<PayBillsFilter.showPayInLessThan>, Equal<True>,
							And<APInvoice.payDate, LessEqual<Required<APInvoice.payDate>>>>,
						Or2<Where<
							Current<PayBillsFilter.showDueInLessThan>, Equal<True>,
							And<Where<
								APInvoice.dueDate, LessEqual<Required<APInvoice.dueDate>>,
								Or<APInvoice.dueDate, IsNull>>>>,
						Or<Where<
							Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<True>,
							And<Where<
								APInvoice.discDate, LessEqual<Required<APInvoice.discDate>>,
								Or<APInvoice.discDate, IsNull>>>>>>>,
						Or<Where<
							Current<PayBillsFilter.showPayInLessThan>, Equal<False>,
							And<Current<PayBillsFilter.showDueInLessThan>, Equal<False>,
							And<Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<False>>>>>>,
					And<Match<Vendor, Current<AccessInfo.userName>>>>>>>>>>>>
					(this);

			int startRow = PXView.StartRow;
			int totalRows = 0;

			List<object> invoices = cmd.View.Select(new[] {filter},
				new[] {PayInLessThan, DueInLessThan, (object) DiscountExpiresInLessThan},
				PXView.Searches,
				Caches[typeof(APInvoice)].Keys.ToArray(),
				PXView.Descendings,
				APExceptionsList.View.GetExternalFilters(),
				ref startRow,
				PXView.MaximumRows,
				ref totalRows);
			foreach (PXResult<APInvoice, CurrencyInfo> res in invoices)
			{
				APInvoice doc = res;
				APAdjust adj = new APAdjust();
				adj.VendorID = doc.VendorID;
				adj.AdjdDocType = doc.DocType;
				adj.AdjdRefNbr = doc.RefNbr;
				adj.AdjgDocType = APDocType.Check;
				adj.SeparateCheck = doc.SeparateCheck;

				APAdjust locatedAdjust = APExceptionsList.Locate(adj);
				if (locatedAdjust == null)
				{
					PXSelectJoin<APInvoice,
							InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>>,
							Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>
						.StoreCached(this, new PXCommandKey(new object[] {adj.AdjdDocType, adj.AdjdRefNbr}), new List<object> {res});
					PXSelectorAttribute.StoreCached<APAdjust.adjdRefNbr>(APExceptionsList.Cache, adj, doc);
					yield return new PXResult<APAdjust, APInvoice>(APExceptionsList.Insert(adj), doc);
				}
				else
				{
					yield return new PXResult<APAdjust, APInvoice>(locatedAdjust, doc);
				}
			}

			APExceptionsList.Cache.IsDirty = false;
			APExceptionsList.View.RequestRefresh();
		}
		protected virtual IEnumerable apdocumentlist()
		{
			PayBillsFilter filter = Filter.Current;

			if (filter?.PayDate == null || filter.PayTypeID == null || filter.PayAccountID == null)
				yield break;

			foreach (APAdjust adj in APDocumentList.Cache.Inserted)
			{
				APInvoice doc = PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Select(this, adj.AdjdDocType, adj.AdjdRefNbr);
				adj.SeparateCheck = (adj.SeparateCheck ?? doc.SeparateCheck);
				yield return new PXResult<APAdjust, APInvoice>(adj, doc);
				if (_copies.ContainsKey(adj))
				{
					_copies.Remove(adj);
				}
				_copies.Add(adj, PXCache<APAdjust>.CreateCopy(adj));
			}

			PXSelectBase<APInvoice> cmd = ComposeBQLCommandForAPDocumentListSelect();

			int startRow = PXView.StartRow;
			int totalRows = 0;

			APDocumentListViewExecuteParamsBuilder.ViewExecutingParams viewExecutingParams =
				new APDocumentListViewExecuteParamsBuilder()
					.BuildViewExecutingParams(APDocumentList.View);

			foreach (PXResult<APInvoice, CurrencyInfo> res in
				cmd.View.Select(new[] { filter },
								ComposeParametersForAPDocumentListSelect(),
								PXView.Searches,
								viewExecutingParams.Sorts.ToArray(),
								viewExecutingParams.Descendings.ToArray(),
								viewExecutingParams.FilterRows.ToArray(),
								ref startRow,
								PXView.MaximumRows,
								ref totalRows))
			{
				APInvoice doc = res;
				APAdjust adj = new APAdjust();
				adj.VendorID = doc.VendorID;
				adj.AdjdDocType = doc.DocType;
				adj.AdjdRefNbr = doc.RefNbr;
				adj.AdjgDocType = APDocType.Check;
				adj.AdjgRefNbr = " <NEW>";
				adj.SeparateCheck = doc.SeparateCheck;

				if (APDocumentList.Locate(adj) == null)
				{
					PXSelectJoin<APInvoice, InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>>, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.StoreCached(this, new PXCommandKey(new object[] { adj.AdjdDocType, adj.AdjdRefNbr }), new List<object> { res });
					PXSelectorAttribute.StoreCached<APAdjust.adjdRefNbr>(APDocumentList.Cache, adj, doc);
					yield return new PXResult<APAdjust, APInvoice>(APDocumentList.Insert(adj), doc);

					_copies.Add(adj, PXCache<APAdjust>.CreateCopy(adj));
				}
			}

			APDocumentList.Cache.IsDirty = false;
			APDocumentList.View.RequestRefresh();
		}

		/// <summary>
		/// Composes parameters for the BQL command created by 
		/// <see cref="ComposeBQLCommandForAPDocumentListSelect"/> method.
		/// This method can be overridden along with <see cref="ComposeBQLCommandForAPDocumentListSelect"/>
		/// to modify filtering conditions.
		/// </summary>
		/// <returns>The method returns an array of objects that contain required parameters 
		/// for BQL select.</returns>
		public virtual object[] ComposeParametersForAPDocumentListSelect()
		{
			PayBillsFilter filter = Filter.Current;
			DateTime payInLessThan = ((DateTime)filter.PayDate).AddDays(filter.PayInLessThan.GetValueOrDefault());
			DateTime dueInLessThan = ((DateTime)filter.PayDate).AddDays(filter.DueInLessThan.GetValueOrDefault());
			DateTime discountExpiresInLessThan = ((DateTime)filter.PayDate).AddDays(filter.DiscountExpiresInLessThan.GetValueOrDefault());
			return new[] { (object)payInLessThan, (object)dueInLessThan, (object)discountExpiresInLessThan };
		}

		/// <summary>
		/// Composes a BQL query for the <see cref="apdocumentlist"/> 
		/// select delegate. 
		/// The <see cref="ComposeParametersForAPDocumentListSelect"/>
		/// method provides parameters for the query.
		/// </summary>
		public virtual PXSelectBase<APInvoice> ComposeBQLCommandForAPDocumentListSelect()
		{
			return new PXSelectJoin<
				APInvoice,
					InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>,
					InnerJoin<Vendor, 
						On<Vendor.bAccountID, Equal<APInvoice.vendorID>,
						And<Where<
							Vendor.status, Equal<BAccount.status.active>,
							Or<Vendor.status, Equal<BAccount.status.oneTime>>>>>,
					LeftJoin<APAdjust, 
						On<APAdjust.adjdDocType, Equal<APInvoice.docType>,
						And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>,
						And<APAdjust.released, Equal<False>>>>,
					LeftJoin<APAdjust2, 
						On<APAdjust2.adjgDocType, Equal<APInvoice.docType>,
						And<APAdjust2.adjgRefNbr, Equal<APInvoice.refNbr>,
						And<APAdjust2.released, Equal<False>>>>,
					LeftJoin<APPayment, 
						On<APPayment.docType, Equal<APInvoice.docType>,
						And<APPayment.refNbr, Equal<APInvoice.refNbr>,
						And<APPayment.docType, Equal<APDocType.prepayment>>>>,
					CrossJoin<APSetup>>>>>>,
				Where<
					APInvoice.openDoc, Equal<True>,
					And2<Where<
						APInvoice.released, Equal<True>,
						Or<APInvoice.prebooked, Equal<True>>>,
					And2<Where<
						APInvoice.paySel, Equal<True>, 
						Or2<Where<APSetup.requireApprovePayments, Equal<False>>,
						Or<APInvoice.docType, Equal<APDocType.debitAdj>>>>,
					And2<Where<
						APInvoice.vendorID, Equal<Current<PayBillsFilter.vendorID>>,
						Or<Current<PayBillsFilter.vendorID>, IsNull>>,
					And<APInvoice.payAccountID, Equal<Current<PayBillsFilter.payAccountID>>,
					And<APInvoice.payTypeID, Equal<Current<PayBillsFilter.payTypeID>>,
					And<APAdjust.adjgRefNbr, IsNull,
					And<APAdjust2.adjdRefNbr, IsNull,
					And<APPayment.refNbr, IsNull,
					And2<Where2<Where2<
						Where<
							Current<PayBillsFilter.showPayInLessThan>, Equal<True>,
							And<APInvoice.payDate, LessEqual<Required<APInvoice.payDate>>>>,
						Or2<Where<
							Current<PayBillsFilter.showDueInLessThan>, Equal<True>,
							And<Where<
								APInvoice.dueDate, LessEqual<Required<APInvoice.dueDate>>, 
								Or<APInvoice.dueDate, IsNull>>>>,
						Or<Where<
							Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<True>,
							And<Where<
								APInvoice.discDate, LessEqual<Required<APInvoice.discDate>>, 
								Or<APInvoice.discDate, IsNull>>>>>>>,
						Or<Where<
							Current<PayBillsFilter.showPayInLessThan>, Equal<False>,
							And<Current<PayBillsFilter.showDueInLessThan>, Equal<False>,
							And<Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<False>>>>>>,
					And<Match<Vendor, Current<AccessInfo.userName>>>>>>>>>>>>>>(this);
		}

		public virtual void CreatePayments(List<APAdjust> list, PayBillsFilter filter, CurrencyInfo info, PaymentMethod paymenttype)
		{
			if (RunningFlagScope<APPayBills>.IsRunning)
				throw new PXSetPropertyException(Messages.AnotherPayBillsRunning, PXErrorLevel.Warning);
			
			using (new RunningFlagScope<APPayBills>())
			{
				_createPayments(list, filter, info, paymenttype);
			}
		}

		protected virtual void _createPayments(List<APAdjust> list, PayBillsFilter filter, CurrencyInfo info, PaymentMethod paymenttype)
		{			
			foreach (APAdjust adj in list)
			{
				adj.AdjgDocDate = filter.PayDate;
				adj.AdjgFinPeriodID = filter.PayFinPeriodID;				
			}

			bool failed = false;

			//check amount is always sum of its applications
			PXRowSelecting del = delegate(PXCache cache, PXRowSelectingEventArgs e)
			{
				APPayment doc = e.Row as APPayment;
				if (doc != null)
				{
					doc.CuryApplAmt = doc.CuryOrigDocAmt;
					doc.CuryUnappliedBal = 0m;
				}
			};

			APPaymentEntry pe = CreateInstance<APPaymentEntry>();
			pe.RowSelecting.RemoveHandler<APPayment>(pe.APPayment_RowSelecting);
			pe.RowSelecting.AddHandler<APPayment>(del);
			
			List<PXResult<APPayment, Vendor>> createdPayments = new List<PXResult<APPayment, Vendor>>();

			list.Sort((a, b) =>
			{
				int aSortOrder = 0;
				int bSortOrder = 0;

				aSortOrder += (1 + ((IComparable)a.VendorID).CompareTo(b.VendorID)) / 2 * 100;
				bSortOrder += (1 - ((IComparable)a.VendorID).CompareTo(b.VendorID)) / 2 * 100;

				aSortOrder += (a.AdjdDocType == APDocType.DebitAdj ? 10 : 0);
				bSortOrder += (b.AdjdDocType == APDocType.DebitAdj ? 10 : 0);

				aSortOrder += (1 + ((IComparable)a.AdjdRefNbr).CompareTo(b.AdjdRefNbr)) / 2;
				bSortOrder += (1 - ((IComparable)a.AdjdRefNbr).CompareTo(b.AdjdRefNbr)) / 2;

				return aSortOrder.CompareTo(bSortOrder);
			});

			foreach (APAdjust adj in list)
			{
				PXProcessing<APAdjust>.SetCurrentItem(adj);
				try
				{
					if (CheckIfRowNotApprovedForPayment(adj))
					{
						throw new PXSetPropertyException(Messages.DocumentNotApprovedNotProceed, PXErrorLevel.RowError);
					}

					APInvoice apdoc = pe.APInvoice_VendorID_DocType_RefNbr.Select(adj.VendorID, adj.AdjdDocType, adj.AdjdRefNbr);
					apdoc.PayAccountID = filter.PayAccountID;
					apdoc.PayTypeID = filter.PayTypeID;
					pe.APInvoice_VendorID_DocType_RefNbr.StoreCached(
						new PXCommandKey(new object[] { adj.VendorID, adj.AdjdDocType, adj.AdjdRefNbr }),
						new List<object> { apdoc });

					pe.TakeDiscAlways = filter.TakeDiscAlways == true;
					pe.CreatePayment(adj, info, true);

					Vendor vendor = pe.vendor.Current;

					//always update to reflect changes in amount
					byte[] timestamp = pe.TimeStamp;
					pe.Clear();
					pe.Document.Search<APPayment.refNbr>(adj.AdjgRefNbr, adj.AdjgDocType);
					APPayment seldoc = PXCache<APPayment>.CreateCopy(pe.Document.Current);

					seldoc.tstamp = timestamp;

					createdPayments.Add(new PXResult<APPayment, Vendor>(seldoc, vendor));
					PXProcessing<APAdjust>.SetProcessed();
				}
				catch (PXException exc)
				{
					PXSetPropertyException exception = exc as PXSetPropertyException;
					if (exception != null && exception.ErrorLevel == PXErrorLevel.Warning)
					{
						PXProcessing<APAdjust>.SetWarning(exception);
					}
					else
					{
						PXProcessing<APAdjust>.SetError(exc);
						failed = true;

						pe.Clear();
					}
				}
			}

			if (failed)
			{
				throw new PXOperationCompletedWithErrorException(GL.Messages.DocumentsNotReleased);
			}
			else if (pe.created.Count > 0)
			{
				RedirectToResult(createdPayments, paymenttype, filter);
			}
		}

		protected virtual void RedirectToResult(List<PXResult<APPayment, Vendor>> payments, PaymentMethod paymentMethod, PayBillsFilter filter)
		{
			bool dontApprove = false;
			foreach (PXResult<APPayment, Vendor> result in payments)
			{
				APPayment payment = result;
				if (payment.Status != APDocStatus.PendingApproval)
				{
					dontApprove = true;
					break;
				}
			}

			if (!dontApprove)
				return;

			PXGraph targetGraph = null;
            PXSelectBase<APPayment> paymentView = null;
			if (paymentMethod?.PrintOrExport == true)
			{
				APPrintChecks printChecksGraph = CreateInstance<APPrintChecks>();

				PrintChecksFilter filterCopy = PXCache<PrintChecksFilter>.CreateCopy(printChecksGraph.Filter.Current);
				filterCopy.BranchID = filter.BranchID;
				filterCopy.PayTypeID = filter.PayTypeID;
				filterCopy = (PrintChecksFilter)printChecksGraph.Filter.Cache.Update(filterCopy);
				filterCopy.PayAccountID = filter.PayAccountID;
				filterCopy.CuryID = filter.CuryID;
				printChecksGraph.Filter.Cache.Update(filterCopy);

				targetGraph = printChecksGraph;
                paymentView = printChecksGraph.APPaymentList;

            }
			else
			{
				APReleaseChecks releaseChecksGraph = CreateInstance<APReleaseChecks>();

				ReleaseChecksFilter filterCopy = PXCache<ReleaseChecksFilter>.CreateCopy(releaseChecksGraph.Filter.Current);
				filterCopy.PayTypeID = filter.PayTypeID;
				filterCopy.PayAccountID = filter.PayAccountID;
				filterCopy.CuryID = filter.CuryID;
				releaseChecksGraph.Filter.Cache.Update(filterCopy);

				targetGraph = releaseChecksGraph;
                paymentView = releaseChecksGraph.APPaymentList;
            }

			if (targetGraph != null && paymentView != null)
            {
                PXCache paymentCache = paymentView.Cache;
                foreach (PXResult<APPayment, Vendor> result in payments)
				{
					APPayment payment = result;
					Vendor vendor = result;
                    payment = paymentView.Search<APPayment.docType, APPayment.refNbr>(payment.DocType, payment.RefNbr);
                    if (payment != null)
                    {
                        targetGraph.Caches[typeof(Vendor)].Current = vendor;

                        payment.Selected = true;
                        payment.Passed = true;

                        paymentCache.Update(payment);
                    }
				}

				paymentCache.IsDirty = false;
				targetGraph.TimeStamp = TimeStamp;

				throw new PXRedirectRequiredException(targetGraph, "NextProcessing");
			}
		}

		protected virtual void PayBillsFilter_PayAccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			foreach (CurrencyInfo info in PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<PayBillsFilter.curyInfoID>>>>.Select(this, null))
			{
				currencyinfo.Cache.SetDefaultExt<CurrencyInfo.curyRateTypeID>(info);
			}
			sender.SetDefaultExt<PayBillsFilter.payDate>(e.Row);
			
		}

		protected virtual void PayBillsFilter_PayTypeID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<PayBillsFilter.payDate>(e.Row);
			sender.SetDefaultExt<PayBillsFilter.payAccountID>(e.Row);
			sender.SetDefaultExt<PayBillsFilter.branchID>(e.Row);
		}

		protected virtual void PayBillsFilter_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<PayBillsFilter.payDate>(e.Row);
		}

		protected virtual void PayBillsFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			APDocumentList.Cache.Clear();
			APExceptionsList.Cache.Clear();
			Filter.Current.CurySelTotal = 0m;
			Filter.Current.SelTotal = 0m;
			Filter.Current.SelCount = 0;
			if (!sender.ObjectsEqual<PayBillsFilter.payAccountID>(e.OldRow, e.Row))
				Filter.Cache.SetDefaultExt<PayBillsFilter.curyID>(e.Row);
		}

		protected virtual void PayBillsFilter_PayDate_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			sender.RaiseExceptionHandling<PayBillsFilter.payDate>(e.Row, e.NewValue, null);
		}

		protected virtual void PayBillsFilter_PayDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			foreach (CurrencyInfo info in PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<PayBillsFilter.curyInfoID>>>>.Select(this, null))
			{
				currencyinfo.Cache.SetDefaultExt<CurrencyInfo.curyEffDate>(info);
			}
			APDocumentList.Cache.Clear();
		}

		protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			foreach (PayBillsFilter filter in Filter.Cache.Inserted)
			{
				e.NewValue = filter.PayDate;
			}
		}

		protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
			{
				foreach (PayBillsFilter filter in Filter.Cache.Inserted)
				{
					if (cashaccount.Current != null && !string.IsNullOrEmpty(cashaccount.Current.CuryRateTypeID))
					{
						e.NewValue = cashaccount.Current.CuryRateTypeID;
						e.Cancel = true;
					}
				}
			}
		}

		#region CacheAttached
		[PXDBLong()]
		[CurrencyInfo(typeof(PayBillsFilter.curyInfoID), CuryIDField = "AdjgCuryID")]
		protected virtual void APAdjust_AdjgCuryInfoID_CacheAttached(PXCache sender)
		{
		}

		[PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
		[PXDefault(APDocType.Check)]
		protected virtual void APAdjust_AdjgDocType_CacheAttached(PXCache sender)
		{
		}

		[PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault(" <NEW>")]
		protected virtual void APAdjust_AdjgRefNbr_CacheAttached(PXCache sender)
		{
		}

		[PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
		[PXDefault(APDocType.Invoice)]
		[PXUIField(DisplayName = "Document Type", Visibility = PXUIVisibility.Visible)]
		[APInvoiceType.List()]
		protected virtual void APAdjust_AdjdDocType_CacheAttached(PXCache sender)
		{
		}

		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible)]
		[APInvoiceType.AdjdRefNbr(typeof(Search2<
			APInvoice.refNbr,
			InnerJoin<BAccount,
				On<BAccount.bAccountID, Equal<APInvoice.vendorID>,
				And<Where<
					BAccount.status, Equal<BAccount.status.active>,
					Or<BAccount.status, Equal<BAccount.status.oneTime>>>>>,
			LeftJoin<APAdjust,
				On<APAdjust.adjdDocType, Equal<APInvoice.docType>,
				And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>,
				And<APAdjust.released, Equal<False>,
				And<Where<
					APAdjust.adjgDocType, NotEqual<Current<APPayment.docType>>,
					Or<APAdjust.adjgRefNbr, NotEqual<Current<APPayment.refNbr>>>>>>>>,
			LeftJoin<APPayment,
				On<APPayment.docType, Equal<APInvoice.docType>,
				And<APPayment.refNbr, Equal<APInvoice.refNbr>,
				And<APPayment.docType, Equal<APDocType.prepayment>>>>>>>,
			Where<
				APInvoice.docType, Equal<Optional<APAdjust.adjdDocType>>,
				And2<Where<
					APInvoice.released, Equal<True>,
					Or<APInvoice.prebooked, Equal<True>>>,
				And<APInvoice.openDoc, Equal<True>,
				And<APAdjust.adjgRefNbr, IsNull,
				And<APPayment.refNbr, IsNull>>>>>>),
			Filterable = true)]
		protected virtual void APAdjust_AdjdRefNbr_CacheAttached(PXCache sender)
		{
		}

		[PXInt()]
		protected virtual void APAdjust_AdjdWhTaxAcctID_CacheAttached(PXCache sender)
		{
		}

		[PXInt()]
		protected virtual void APAdjust_AdjdWhTaxSubID_CacheAttached(PXCache sender)
		{
		}
		#endregion

		protected virtual void APAdjust_VendorID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void APAdjust_AdjdRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;

			foreach (PXResult<APInvoice, CurrencyInfo> res in PXSelectJoin<APInvoice, InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>>, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Select(this, adj.AdjdDocType, adj.AdjdRefNbr))
			{
				CurrencyInfo info = res;
				CurrencyInfo info_copy = null;
				APInvoice invoice = res;


					//place select tail in cache to merge correctly
					CurrencyInfo_CuryInfoID.Cache.SetStatus(info, PXEntryStatus.Notchanged);

					try
					{
						info_copy = PXCache<CurrencyInfo>.CreateCopy((CurrencyInfo)res);
						info_copy.CuryInfoID = null;
						info_copy = (CurrencyInfo)currencyinfo.Cache.Insert(info_copy);

						info_copy.SetCuryEffDate(currencyinfo.Cache, Filter.Current.PayDate);
					}
					catch (PXRateIsNotDefinedForThisDateException ex)
					{
						APDocumentList.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>(adj, 0m, new PXSetPropertyException(ex.Message, PXErrorLevel.RowError));
					}

				adj.VendorID = invoice.VendorID;
				adj.AdjgDocDate = Filter.Current.PayDate;
				adj.AdjgFinPeriodID = Filter.Current.PayFinPeriodID;
				adj.AdjgCuryInfoID = Filter.Current.CuryInfoID;
				adj.AdjdCuryInfoID = info_copy.CuryInfoID;
				adj.AdjdOrigCuryInfoID = info.CuryInfoID;
				adj.AdjdBranchID = invoice.BranchID;
				adj.AdjdAPAcct = invoice.APAccountID;
				adj.AdjdAPSub = invoice.APSubID;
				adj.AdjdDocDate = invoice.DocDate;
				adj.AdjdFinPeriodID = invoice.FinPeriodID;
				adj.Released = false;

				CurrencyInfo_CuryInfoID.StoreCached(new PXCommandKey(new object[] { adj.AdjdOrigCuryInfoID }), new List<object> { info });
				CurrencyInfo_CuryInfoID.StoreCached(new PXCommandKey(new object[] { adj.AdjdCuryInfoID }), new List<object> { info_copy });

				CalcBalances(adj, invoice, false);

				if (adj.CuryWhTaxBal >= 0m && adj.CuryDiscBal >= 0m && adj.CuryDocBal - adj.CuryWhTaxBal - adj.CuryDiscBal <= 0m)
				{
					//no amount suggestion is possible
					return;
				}

				adj.CuryAdjgAmt = adj.CuryDocBal - adj.CuryWhTaxBal - adj.CuryDiscBal;
				adj.CuryAdjgDiscAmt = adj.CuryDiscBal;
				adj.CuryAdjgWhTaxAmt = adj.CuryWhTaxBal;

				CalcBalances(adj, invoice, true);

				return;
			}
		}

		protected virtual void APAdjust_CuryDocBal_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			if (e.Row != null && ((APAdjust)e.Row).AdjdCuryInfoID != null && ((APAdjust)e.Row).CuryDocBal == null)
			{
				CalcBalances((APAdjust)e.Row, false);
			}
			if (e.Row != null)
			{
				e.NewValue = ((APAdjust)e.Row).CuryDocBal;
			}
			e.Cancel = true;
		}

		protected virtual void APAdjust_CuryDiscBal_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			if (e.Row != null && ((APAdjust)e.Row).AdjdCuryInfoID != null && ((APAdjust)e.Row).CuryDiscBal == null)
			{
				CalcBalances((APAdjust)e.Row, false);
			}
			if (e.Row != null)
			{
				e.NewValue = ((APAdjust)e.Row).CuryDiscBal;
			}
			e.Cancel = true;
		}

		protected virtual void APAdjust_CuryAdjgAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;

			if (adj.CuryDocBal == null)
			{
				CalcBalances(adj, false);
			}

			if (adj.CuryAdjgAmt == null)
			{
				adj.CuryAdjgAmt = 0m;
			}

			if (adj.CuryAdjgDiscAmt == null)
			{
				adj.CuryAdjgDiscAmt = 0m;
			}

			if (adj.CuryDocBal + (decimal)adj.CuryAdjgAmt - (decimal?)e.NewValue < 0)
			{
				throw new PXSetPropertyException(Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgAmt).ToString());
			}
		}

		protected virtual void APAdjust_CuryAdjgAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			if (e.OldValue != null && ((APAdjust)e.Row).CuryDocBal == 0m && ((APAdjust)e.Row).CuryAdjgAmt < (decimal)e.OldValue)
			{
				((APAdjust)e.Row).CuryAdjgDiscAmt = 0m;
			}
			CalcBalances((APAdjust)e.Row, true);
		}

		protected virtual void APAdjust_CuryAdjgDiscAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;

			if (adj.CuryDiscBal == null)
			{
				CalcBalances(adj, false);
			}

			if (adj.CuryAdjgAmt == null)
			{
				adj.CuryAdjgAmt = 0m;
			}

			if (adj.CuryAdjgDiscAmt == null)
			{
				adj.CuryAdjgDiscAmt = 0m;
			}

			if (adj.CuryDiscBal + adj.CuryAdjgDiscAmt == 0&&(decimal?)e.NewValue !=0)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_EQ, 0.ToString());
			}
			else
				if (adj.CuryDiscBal + adj.CuryAdjgDiscAmt - (decimal?)e.NewValue < 0)
				{
					throw new PXSetPropertyException(Messages.Entry_LE, ((decimal)adj.CuryDiscBal + (decimal)adj.CuryAdjgDiscAmt).ToString());
				}

			if (adj.CuryAdjgAmt != null && (sender.GetValuePending<APAdjust.curyAdjgAmt>(e.Row) == PXCache.NotSetValue || (Decimal?)sender.GetValuePending<APAdjust.curyAdjgAmt>(e.Row) == adj.CuryAdjgAmt))
			{
				if ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgDiscAmt - (decimal)e.NewValue < 0)
				{
					throw new PXSetPropertyException(Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgDiscAmt).ToString());
				}
			}
		}

		protected virtual void APAdjust_CuryAdjgDiscAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CalcBalances((APAdjust)e.Row, true);
		}

		private void CalcBalances(APAdjust row, bool isCalcRGOL)
		{
			APAdjust adj = (APAdjust)row;
			foreach (APInvoice voucher in PXSelect<APInvoice, Where<APInvoice.vendorID, Equal<Required<APInvoice.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>>.Select(this, adj.VendorID, adj.AdjdDocType, adj.AdjdRefNbr))
			{
				CalcBalances(adj, voucher, isCalcRGOL);
				return;
			}
		}

		protected virtual void CalcBalances(APAdjust adj, APInvoice voucher, bool isCalcRGOL)
		{
			try
			{
				APPaymentEntry.CalcBalances<APInvoice>(CurrencyInfo_CuryInfoID, adj, voucher, isCalcRGOL, Filter.Current.TakeDiscAlways == false);
			}
			catch (PXRateIsNotDefinedForThisDateException ex)
			{
				APDocumentList.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>(adj, 0m, new PXSetPropertyException(ex.Message, PXErrorLevel.RowError));
			}
		}

		protected virtual void APAdjust_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			PayBillsFilter filter = Filter.Current;
			if (filter != null)
			{
				{
					APAdjust new_row = e.Row as APAdjust;
					filter.CurySelTotal += new_row.Selected == true ? new_row.AdjgBalSign * new_row.CuryAdjgAmt : 0m;
					filter.SelTotal += new_row.Selected == true ? new_row.AdjgBalSign * new_row.AdjAmt : 0m;
					filter.SelCount += new_row.Selected == true ? 1 : 0;
				}
			}

			if (CheckIfRowNotApprovedForPayment((APAdjust)e.Row))
				APDocumentList.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>((APAdjust)e.Row, 0m, new PXSetPropertyException(Messages.DocumentNotApprovedNotProceed, PXErrorLevel.RowWarning));

		}

		protected virtual void APAdjust_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			PayBillsFilter filter = Filter.Current;
			if (filter != null)
			{
				object OldRow = e.OldRow;
				if (object.ReferenceEquals(e.Row, e.OldRow) && !_copies.TryGetValue(e.Row, out OldRow))
				{
					decimal? curyval = 0m;
					decimal? val = 0m;
					int? count = 0;

					foreach (APAdjust res in APDocumentList.Select((object)null))
					{
						if (res.Selected == true)
						{
							curyval += res.AdjgBalSign * res.CuryAdjgAmt ?? 0m;
							val += res.AdjgBalSign * res.AdjAmt ?? 0m;
							count++;
						}
					}

					filter.CurySelTotal = curyval;
					filter.SelTotal = val;
					filter.SelCount = count;
				}
				else
				{
					APAdjust old_row = OldRow as APAdjust;
					APAdjust new_row = e.Row as APAdjust;
					filter.CurySelTotal -= old_row.Selected == true ? old_row.AdjgBalSign * old_row.CuryAdjgAmt : 0m;
					filter.CurySelTotal += new_row.Selected == true ? new_row.AdjgBalSign * new_row.CuryAdjgAmt : 0m;

					filter.SelTotal -= old_row.Selected == true ? old_row.AdjgBalSign * old_row.AdjAmt : 0m;
					filter.SelTotal += new_row.Selected == true ? new_row.AdjgBalSign * new_row.AdjAmt : 0m;

					filter.SelCount -= old_row.Selected == true ? 1 : 0;
					filter.SelCount += new_row.Selected == true ? 1 : 0;
				}
			}
		}

		protected virtual void CurrencyInfo_SampleCuryRate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			if (Filter.Current != null && ((CurrencyInfo)e.Row).CuryInfoID == Filter.Current.CuryInfoID && ((CurrencyInfo)CurrencyInfo_CuryInfoID.Select(Filter.Current.CuryInfoID)) != null)
			{
				foreach (PXResult<APAdjust, APInvoice> row in APDocumentList.Select())
				{
					CalcBalances(row, row, true);
				}
				APDocumentList.View.RequestRefresh();
			}
		}

		protected virtual void CurrencyInfo_SampleRecipRate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			if (Filter.Current != null && ((CurrencyInfo)e.Row).CuryInfoID == Filter.Current.CuryInfoID && ((CurrencyInfo)CurrencyInfo_CuryInfoID.Select(Filter.Current.CuryInfoID)) != null)
			{
				foreach (PXResult<APAdjust, APInvoice> row in APDocumentList.Select())
				{
					CalcBalances(row, row, true);
				}
				APDocumentList.View.RequestRefresh();
			}
		}

		public override int ExecuteUpdate(string viewName, IDictionary keys, IDictionary values, params object[] parameters)
		{
			if (viewName == "Filter" &&
				Filter.Current != null &&
				Filter.Current.SelCount > 0 &&
				Filter.View.Ask(viewName, Messages.AskUpdatePayBillsFilter, MessageButtons.YesNo) == WebDialogResult.No) return 0;

			return base.ExecuteUpdate(viewName, keys, values, parameters);
		}

		protected class APDocumentListViewExecuteParamsBuilder
		{
			protected readonly string APInvoiceViewPrefix = $"{nameof(APInvoice)}__";

			protected Dictionary<string, string> AdjFieldsToSubstituteByName;

			protected string DocTypeFieldName;
			protected string RefNbrFieldName;

			protected string AdjdDocTypeFieldName;
			protected string AdjdRefNbrFieldName;

			public APDocumentListViewExecuteParamsBuilder()
			{
				DocTypeFieldName = typeof(APInvoice.docType).Name.Capitalize();
				RefNbrFieldName = typeof(APInvoice.refNbr).Name.Capitalize();

				AdjdDocTypeFieldName = typeof(APAdjust.adjdDocType).Name.Capitalize();
				AdjdRefNbrFieldName = typeof(APAdjust.adjdRefNbr).Name.Capitalize();

				AdjFieldsToSubstituteByName = new Dictionary<string, string>()
				{
					{AdjdDocTypeFieldName, DocTypeFieldName},
					{AdjdRefNbrFieldName, RefNbrFieldName}
				};
			}

			public virtual ViewExecutingParams BuildViewExecutingParams(PXView view)
			{
				ViewExecutingParams viewExecutingParams = BuildSortingAndDescendings(view);

				viewExecutingParams.FilterRows = BuildFilters(view);

				return viewExecutingParams;
			}

			protected virtual ViewExecutingParams BuildSortingAndDescendings(PXView view)
			{
				ViewExecutingParams viewExecutingParams = new ViewExecutingParams()
				{
					Sorts = new List<string>(),
					Descendings = new List<bool>(),
					FilterRows = new List<PXFilterRow>()
				};

				string[] externalSorts = view.GetExternalSorts();
				bool[] externalDescendings = view.GetExternalDescendings();

				if (externalSorts != null)
				{
					for (int i = 0; i < externalSorts.Length; i++)
					{
						string sortField = externalSorts[i];

						if (AdjFieldsToSubstituteByName.ContainsKey(sortField))
						{
							viewExecutingParams.Sorts.Add(AdjFieldsToSubstituteByName[sortField]);
						}
						else if (sortField.Contains(APInvoiceViewPrefix))
						{
							viewExecutingParams.Sorts.Add(ConvertToFieldNameWithoutPrefix(sortField, APInvoiceViewPrefix));
						}
						else
						{
							viewExecutingParams.Sorts.Add(sortField);
						}

						viewExecutingParams.Descendings.Add(externalDescendings[i]);
					}
				}

				string[] keySortingFields = new string[] { DocTypeFieldName, RefNbrFieldName };

				foreach (var keyField in keySortingFields)
				{
					if (!viewExecutingParams.Sorts.Contains(keyField))
					{
						viewExecutingParams.Sorts.Add(keyField);
						viewExecutingParams.Descendings.Add(false);
					}
				}

				return viewExecutingParams;
			}

			protected virtual List<PXFilterRow> BuildFilters(PXView view)
			{
				List<PXFilterRow> filters = view.GetExternalFilters()?.ToList() ?? new List<PXFilterRow>();

				foreach (var externalFilter in filters)
				{
					if (AdjFieldsToSubstituteByName.ContainsKey(externalFilter.DataField))
					{
						externalFilter.DataField = AdjFieldsToSubstituteByName[externalFilter.DataField];
					}
					else if (externalFilter.DataField.Contains(APInvoiceViewPrefix))
					{
						externalFilter.DataField = ConvertToFieldNameWithoutPrefix(externalFilter.DataField, APInvoiceViewPrefix);
					}
				}

				return filters;
			}

			private string ConvertToFieldNameWithoutPrefix(string fieldNameWithPrefix, string prefix)
			{
				char firstLetter = char.ToUpper(fieldNameWithPrefix[prefix.Length]);

				return string.Concat(firstLetter,
					fieldNameWithPrefix.Substring(prefix.Length + 1, fieldNameWithPrefix.Length - prefix.Length - 1));
			}

			public class ViewExecutingParams
			{
				public List<string> Sorts;

				public List<bool> Descendings;

				public List<PXFilterRow> FilterRows;
			}
		}
	}
	
	[Serializable]
	public partial class PayBillsFilter : IBqlTable
	{
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[PXDefault(typeof(AccessInfo.branchID))]
		[Branch(Visible = true, Enabled = true)]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region PayTypeID
		public abstract class payTypeID : PX.Data.IBqlField
		{
		}
		protected String _PayTypeID;
		[PXDefault()]
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<PaymentMethod.paymentMethodID,
						  Where<PaymentMethod.useForAP, Equal<True>,
							And<PaymentMethod.isActive, Equal<True>>>>))]
		
		public virtual String PayTypeID
		{
			get
			{
				return this._PayTypeID;
			}
			set
			{
				this._PayTypeID = value;
			}
		}
		#endregion
		#region PayAccountID
		public abstract class payAccountID : PX.Data.IBqlField
		{
		}
		protected Int32? _PayAccountID;

		[CashAccount(typeof(PayBillsFilter.branchID), typeof(Search2<CashAccount.cashAccountID,
							InnerJoin<PaymentMethodAccount, 
								On<PaymentMethodAccount.cashAccountID,Equal<CashAccount.cashAccountID>>>,
							Where2<Match<Current<AccessInfo.userName>>, 
							And<CashAccount.clearingAccount, Equal<False>,
							And<PaymentMethodAccount.paymentMethodID,Equal<Current<PayBillsFilter.payTypeID>>,
							And<PaymentMethodAccount.useForAP,Equal<True>>>>>>), Visibility = PXUIVisibility.Visible)]
		[PXDefault(typeof(Search2<PaymentMethodAccount.cashAccountID, 
							InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>,
										Where<PaymentMethodAccount.paymentMethodID, Equal<Current<PayBillsFilter.payTypeID>>,
											And<PaymentMethodAccount.useForAP, Equal<True>,
							And<PaymentMethodAccount.aPIsDefault, Equal<True>,
							And<CashAccount.branchID, Equal<Current<AccessInfo.branchID>>>>>>>))]
		public virtual Int32? PayAccountID
		{
			get
			{
				return this._PayAccountID;
			}
			set
			{
				this._PayAccountID = value;
			}
		}
		#endregion
		#region PayDate
		public abstract class payDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _PayDate;
		[PXDBDate()]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Payment Date", Visibility = PXUIVisibility.Visible)]
		public virtual DateTime? PayDate
		{
			get
			{
				return this._PayDate;
			}
			set
			{
				this._PayDate = value;
			}
		}
		#endregion
		#region PayFinPeriodID
		public abstract class payFinPeriodID : PX.Data.IBqlField
		{
		}
		protected string _PayFinPeriodID;
		[APOpenPeriod(typeof(PayBillsFilter.payDate))]
		[PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.Visible)]
		public virtual String PayFinPeriodID
		{
			get
			{
				return this._PayFinPeriodID;
			}
			set
			{
				this._PayFinPeriodID = value;
			}
		}
		#endregion
		#region PayInLessThan
		public abstract class payInLessThan : PX.Data.IBqlField
		{
		}
		protected Int16? _PayInLessThan;
		[PXDBShort()]
		[PXUIField(Visibility = PXUIVisibility.Visible)]
		[PXDefault(typeof(APSetup.paymentLeadTime), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int16? PayInLessThan
		{
			get
			{
				return this._PayInLessThan;
			}
			set
			{
				this._PayInLessThan = value;
			}
		}
		#endregion
		#region ShowPayInLessThan
		public abstract class showPayInLessThan : PX.Data.IBqlField
		{
		}
		protected Boolean? _ShowPayInLessThan;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Pay Date Within", Visibility = PXUIVisibility.Visible)]
		public virtual Boolean? ShowPayInLessThan
		{
			get
			{
				return this._ShowPayInLessThan;
			}
			set
			{
				this._ShowPayInLessThan = value;
			}
		}
		#endregion
		#region DueInLessThan
		public abstract class dueInLessThan : PX.Data.IBqlField
		{
		}
		protected Int16? _DueInLessThan;
		[PXDBShort()]
		[PXUIField(Visibility = PXUIVisibility.Visible)]
		[PXDefault(typeof(APSetup.paymentLeadTime), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int16? DueInLessThan
		{
			get
			{
				return this._DueInLessThan;
			}
			set
			{
				this._DueInLessThan = value;
			}
		}
		#endregion
		#region ShowDueInLessThan
		public abstract class showDueInLessThan : PX.Data.IBqlField
		{
		}
		protected Boolean? _ShowDueInLessThan;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Due Date Within", Visibility = PXUIVisibility.Visible)]
		public virtual Boolean? ShowDueInLessThan
		{
			get
			{
				return this._ShowDueInLessThan;
			}
			set
			{
				this._ShowDueInLessThan = value;
			}
		}
		#endregion
		#region DiscountExpiredInLessThan
		public abstract class discountExpiresInLessThan : PX.Data.IBqlField
		{
		}
		protected Int16? _DiscountExpiresInLessThan;
		[PXDBShort()]
		[PXUIField(Visibility = PXUIVisibility.Visible)]
		[PXDefault(typeof(APSetup.paymentLeadTime), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int16? DiscountExpiresInLessThan
		{
			get
			{
				return this._DiscountExpiresInLessThan;
			}
			set
			{
				this._DiscountExpiresInLessThan = value;
			}
		}
		#endregion
		#region ShowDiscountExpiresInLessThan
		public abstract class showDiscountExpiresInLessThan : PX.Data.IBqlField
		{
		}
		protected Boolean? _ShowDiscountExpiresInLessThan;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Cash Discount Expires Within", Visibility = PXUIVisibility.Visible)]
		public virtual Boolean? ShowDiscountExpiresInLessThan
		{
			get
			{
				return this._ShowDiscountExpiresInLessThan;
			}
			set
			{
				this._ShowDiscountExpiresInLessThan = value;
			}
		}
		#endregion
		#region Balance
		public abstract class balance : PX.Data.IBqlField
		{
		}
		protected Decimal? _Balance;
		[PXDefault(TypeCode.Decimal, "0.0")]
		//[PXDBDecimal(4)]
		[PXDBCury(typeof(PayBillsFilter.curyID))]
		[PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual Decimal? Balance
		{
			get
			{
				return this._Balance;
			}
			set
			{
				this._Balance = value;
			}
		}
		#endregion
		#region CuryID
		public abstract class curyID : PX.Data.IBqlField
		{
		}
		protected String _CuryID;
		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[PXDefault(typeof(Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<PayBillsFilter.payAccountID>>>>))]
		[PXSelector(typeof(Currency.curyID))]
		public virtual String CuryID
		{
			get
			{
				return this._CuryID;
			}
			set
			{
				this._CuryID = value;
			}
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField
		{
		}
		protected Int64? _CuryInfoID;
		[PXDBLong()]
		[CurrencyInfo(ModuleCode = "AP")]
		public virtual Int64? CuryInfoID
		{
			get
			{
				return this._CuryInfoID;
			}
			set
			{
				this._CuryInfoID = value;
			}
		}
		#endregion
		#region Days
		public abstract class days : PX.Data.IBqlField
		{
		}
		protected String _Days;
		[PXDBString(IsUnicode = true)]
		[PXUIField]
		[PXDefault(Messages.Days, PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual string Days
		{
			get
			{
				return this._Days;
			}
			set
			{
				this._Days = value;
			}
		}
		#endregion

		#region CurySelTotal
		public abstract class curySelTotal : PX.Data.IBqlField
		{
		}
		protected Decimal? _CurySelTotal;
		[PXDefault(TypeCode.Decimal, "0.0")]
		//[PXDBCury(typeof(PayBillsFilter.curyID))]
		[PXDBCurrency(typeof(PayBillsFilter.curyInfoID), typeof(PayBillsFilter.selTotal), BaseCalc = false)]
		[PXUIField(DisplayName = "Selection Total", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual Decimal? CurySelTotal
		{
			get
			{
				return this._CurySelTotal;
			}
			set
			{
				this._CurySelTotal = value;
			}
		}
		#endregion
		#region SelTotal
		public abstract class selTotal : PX.Data.IBqlField
		{
		}
		protected Decimal? _SelTotal;
		[PXDBDecimal(4)]
		public virtual Decimal? SelTotal
		{
			get
			{
				return this._SelTotal;
			}
			set
			{
				this._SelTotal = value;
			}
		}
		#endregion
		#region SelCount
		public abstract class selCount : IBqlField {}
		[PXDBInt]
		[PXDefault(0)]
		[PXUIField(DisplayName = "Number of Documents to Pay", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual int? SelCount { get; set; }
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		[VendorActive(
			Visibility = PXUIVisibility.SelectorVisible, 
			Required = false, 
			DescriptionField = typeof(Vendor.acctName))]
		[PXDefault]
		public virtual int? VendorID
		{
			get;
			set;
		}
		#endregion
		#region TakeDiscAlways
		public abstract class takeDiscAlways : PX.Data.IBqlField
		{
		}
		protected Boolean? _TakeDiscAlways;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Always Take Cash Discount", Visibility = PXUIVisibility.Visible)]
		public virtual Boolean? TakeDiscAlways
		{
			get
			{
				return this._TakeDiscAlways;
			}
			set
			{
				this._TakeDiscAlways = value;
			}
		}
		#endregion
		#region CashBalance
		public abstract class cashBalance : PX.Data.IBqlField
		{
		}
		protected Decimal? _CashBalance;
		[PXDefault(TypeCode.Decimal, "0.0")]
		//[PXDBDecimal()]
		[PXDBCury(typeof(PayBillsFilter.curyID))]
		[PXUIField(DisplayName = "Available Balance", Enabled = false)]
		[CashBalance(typeof(PayBillsFilter.payAccountID))]
		public virtual Decimal? CashBalance
		{
			get
			{
				return this._CashBalance;
			}
			set
			{
				this._CashBalance = value;
			}
		}
		#endregion
		#region GLBalance
		public abstract class gLBalance : PX.Data.IBqlField
		{
		}
		protected Decimal? _GLBalance;
		[PXDefault(TypeCode.Decimal, "0.0")]
		//[PXDBDecimal()]
		[PXDBCury(typeof(PayBillsFilter.curyID))]
		[PXUIField(DisplayName = "GL Balance", Enabled = false)]
		[GLBalance(typeof(PayBillsFilter.payAccountID), typeof(PayBillsFilter.payFinPeriodID))]
		public virtual Decimal? GLBalance
		{
			get
			{
				return this._GLBalance;
			}
			set
			{
				this._GLBalance = value;
			}
		}
		#endregion
	}
}
