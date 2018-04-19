using System;
using System.Collections;
using System.Collections.Generic;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.SO;

namespace PX.Objects.IN
{
	[PX.Objects.GL.TableAndChartDashboardType]
	public class INClosing : Closing
	{
		protected string _ReportID = "IN656000";

		[PXHidden]
		public PXSelectReadonly2<SOOrderShipment,
			LeftJoin<ARRegister, On<ARRegister.docType, Equal<SOOrderShipment.invoiceType>, And<ARRegister.refNbr, Equal<SOOrderShipment.invoiceNbr>>>>,
			Where<SOOrderShipment.confirmed, Equal<boolTrue>, And<SOOrderShipment.createINDoc, Equal<boolTrue>, And<SOOrderShipment.invtRefNbr, IsNull,
				And<Where<SOOrderShipment.invoiceNbr, IsNotNull, And<ARRegister.finPeriodID, Equal<Optional<FinPeriod.finPeriodID>>,
					Or<SOOrderShipment.invoiceNbr, IsNull, And<SOOrderShipment.shipmentType, NotEqual<INDocType.dropShip>,
						And<SOOrderShipment.shipDate, GreaterEqual<Optional<FinPeriod.startDate>>, And<SOOrderShipment.shipDate, Less<Optional<FinPeriod.endDate>>>>>>>>>>>>>
			UnpostedDocuments;

		public PXAction<FinPeriod> ShowUnpostedDocuments;

		[PXUIField(DisplayName = "Unposted to IN Documents", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXButton]
		public virtual IEnumerable showUnpostedDocuments(PXAdapter adapter)
		{
			_ReportID = "IN656500";
			return showDocuments(adapter);
		}

		protected override IEnumerable periodList()
		{
			string fiscalYear = null;
			INSetup.Current = INSetup.Select();
			if (INSetup.Current == null)
				yield break;
			foreach (FinPeriod per in PXSelect<FinPeriod, Where<FinPeriod.iNClosed, Equal<boolFalse>>>.Select(this))
			{
				if (fiscalYear == null)
				{
					fiscalYear = per.FinYear;
				}
				if (per.FinYear == fiscalYear)
				{
					yield return per;
				}
			}
		}

		protected override void StartClosePeriod(List<FinPeriod> list)
		{
			PXLongOperation.StartOperation(this, delegate() { ClosePeriod(list); });
		}

		public static new void ClosePeriod(List<FinPeriod> list)
		{
            INClosing pg = CreateInstance<INClosing>();
			for (int i = 0; i < list.Count; i++)
			{
				pg.Clear();
				FinPeriod per = list[i];
				pg.ClosePeriodProc(per);
			}
		}
		protected override void RunShowReport(Dictionary<string, string> d )
		{
			if (d.Count > 0)
			{
				throw new PXReportRequiredException(d, _ReportID, PXBaseRedirectException.WindowMode.New, "Open IN Documents");
			}
		}
		protected override void ClosePeriodProc(FinPeriod p)
		{
			PXSelectBase select = new PXSelect<INRegister,
										  Where<INRegister.finPeriodID, Equal<Required<INRegister.finPeriodID>>,
											And<INRegister.released, Equal<boolFalse>>>>(this);
			INRegister doc = (INRegister)select.View.SelectSingle(p.FinPeriodID);
			if (doc != null)
			{
				throw new PXException(AP.Messages.PeriodHasUnreleasedDocs);
			}

			//MS Landed cost will not be able to create these transactions if the period is closed
			LandedCostTran lcTranFromAP = PXSelectJoin<LandedCostTran,
										InnerJoin<APRegister, On<LandedCostTran.aPDocType, Equal<APRegister.docType>,
											And<LandedCostTran.aPRefNbr, Equal<APRegister.refNbr>>>>,
											Where<LandedCostTran.invoiceDate, GreaterEqual<Required<LandedCostTran.invoiceDate>>,
												And<LandedCostTran.invoiceDate, Less<Required<LandedCostTran.invoiceDate>>,
												And<LandedCostTran.source, Equal<LandedCostTranSource.fromAP>,
												And<APRegister.released, Equal<True>,
												And<LandedCostTran.processed, Equal<False>>>>>>>.Select(this, p.StartDate,p.EndDate);
			if (lcTranFromAP != null && lcTranFromAP.LCTranID.HasValue) 
			{
				throw new PXException(Messages.PeriodHasINDocsFromAP_LCToBeCreated);
			}

			PO.LandedCostTran lcTranFromPO = PXSelectJoin<LandedCostTran,
											InnerJoin<POReceipt, On<LandedCostTran.pOReceiptNbr, Equal<POReceipt.receiptNbr>>>,
											Where<LandedCostTran.invoiceDate, GreaterEqual<Required<LandedCostTran.invoiceDate>>,
												And<LandedCostTran.invoiceDate, Less<Required<LandedCostTran.invoiceDate>>,
											And<LandedCostTran.source, Equal<LandedCostTranSource.fromPO>,
											And<POReceipt.released, Equal<True>,											
											And<LandedCostTran.processed, Equal<False>>>>>>>.Select(this, p.StartDate, p.EndDate);
			if (lcTranFromPO != null && lcTranFromPO.LCTranID.HasValue)
			{
				throw new PXException(Messages.PeriodHasINDocsFromPO_LCToBeCreated);
			}

			p.INClosed = true;
			Caches[typeof(FinPeriod)].Update(p);

			Actions.PressSave();
		}

		protected override void FinPeriod_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			base.FinPeriod_RowSelected(sender, e);

			bool anyPeriodSelected = ShowDocuments.GetEnabled();
			ShowUnpostedDocuments.SetEnabled(anyPeriodSelected);

			var row = (FinPeriod)e.Row;
			if (row == null) return;

			bool warnDocsUnposted = (row.Selected == true && UnpostedDocuments.View.SelectSingleBound(new[] { row }) != null);
			Exception warnDocsUnpostedExc = warnDocsUnposted ? new PXSetPropertyException(Messages.UnpostedDocsExist, PXErrorLevel.RowWarning) : null;
			PeriodList.Cache.RaiseExceptionHandling<FinPeriod.selected>(row, null, warnDocsUnpostedExc);
		}
	}
}
