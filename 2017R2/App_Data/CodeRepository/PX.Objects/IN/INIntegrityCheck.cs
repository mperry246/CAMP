using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Objects.IN.Overrides.INDocumentRelease;
using PX.Objects.CS;
using Company = PX.Objects.GL.Company;
using SOOrderType = PX.Objects.SO.SOOrderType;
using SOShipLineSplit = PX.Objects.SO.Table.SOShipLineSplit;
using POReceiptLineSplit = PX.Objects.PO.POReceiptLineSplit;
using PX.Objects.GL;
using PX.Objects.SO;

namespace PX.Objects.IN
{
    [Serializable]
    [PXHidden]
	public partial class ReadOnlySiteStatus : INSiteStatus
	{
		#region InventoryID
		public new abstract class inventoryID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region SubItemID
		public new abstract class subItemID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? SubItemID
		{
			get
			{
				return this._SubItemID;
			}
			set
			{
				this._SubItemID = value;
			}
		}
		#endregion
		#region SiteID
		public new abstract class siteID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? SiteID
		{
			get
			{
				return this._SiteID;
			}
			set
			{
				this._SiteID = value;
			}
		}
		#endregion
	}

    [Serializable]
    [PXHidden]
	public partial class ReadOnlyLocationStatus : INLocationStatus
	{
		#region InventoryID
		public new abstract class inventoryID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region SubItemID
		public new abstract class subItemID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? SubItemID
		{
			get
			{
				return this._SubItemID;
			}
			set
			{
				this._SubItemID = value;
			}
		}
		#endregion
		#region SiteID
		public new abstract class siteID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? SiteID
		{
			get
			{
				return this._SiteID;
			}
			set
			{
				this._SiteID = value;
			}
		}
		#endregion
		#region LocationID
		public new abstract class locationID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? LocationID
		{
			get
			{
				return this._LocationID;
			}
			set
			{
				this._LocationID = value;
			}
		}
		#endregion
		#region QtyOnHand
		public new abstract class qtyOnHand : PX.Data.IBqlField
		{
		}
		#endregion
	}

    [Serializable]
	[PXHidden]
	public partial class ReadOnlyLotSerialStatus : INLotSerialStatus
	{
		#region InventoryID
		public new abstract class inventoryID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region SubItemID
		public new abstract class subItemID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? SubItemID
		{
			get
			{
				return this._SubItemID;
			}
			set
			{
				this._SubItemID = value;
			}
		}
		#endregion
		#region SiteID
		public new abstract class siteID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? SiteID
		{
			get
			{
				return this._SiteID;
			}
			set
			{
				this._SiteID = value;
			}
		}
		#endregion
		#region LocationID
		public new abstract class locationID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public override Int32? LocationID
		{
			get
			{
				return this._LocationID;
			}
			set
			{
				this._LocationID = value;
			}
		}
		#endregion
		#region LotSerialNbr
		public new abstract class lotSerialNbr : PX.Data.IBqlField
		{
		}
		[PXDBString(IsKey = true)]
		[PXDefault()]
		public override string LotSerialNbr
		{
			get
			{
				return this._LotSerialNbr;
			}
			set
			{
				this._LotSerialNbr = value;
			}
		}
		#endregion
		#region QtyOnHand
		public new abstract class qtyOnHand : PX.Data.IBqlField
		{
		}
		#endregion
	}

	[Serializable]
	[PXHidden]
    public partial class INItemSiteSummary : INItemSite 
    {
        public new abstract class inventoryID : IBqlField { }
        public new abstract class siteID : IBqlField { }
    }

	[PX.Objects.GL.TableAndChartDashboardType]
	public class INIntegrityCheck : PXGraph<INIntegrityCheck>
	{
		public PXCancel<INSiteFilter> Cancel;
		public PXFilter<INSiteFilter> Filter;
		[PXFilterable]
        public PXFilteredProcessingJoin<InventoryItem,
			INSiteFilter,
            LeftJoin<INSiteStatusSummary, On<INSiteStatusSummary.inventoryID, Equal<InventoryItem.inventoryID>,
				And<INSiteStatusSummary.siteID, Equal<Current<INSiteFilter.siteID>>>>>,
			Where<True, Equal<True>>,
            OrderBy<Asc<InventoryItem.inventoryCD>>>
			INItemList;
		public PXSetup<INSetup> insetup;
		public PXSelect<INItemSite> itemsite;
		public PXSelect<INSiteStatus> sitestatus_s;
		public PXSelect<SiteStatus> sitestatus;
		public PXSelect<LocationStatus> locationstatus;
		public PXSelect<LotSerialStatus> lotserialstatus;
        public PXSelect<ItemLotSerial> itemlotserial;
		public PXSelect<SiteLotSerial> sitelotserial;
		public PXSelect<INItemPlan> initemplan;

		public PXSelect<ItemSiteHist> itemsitehist;
        public PXSelect<ItemSiteHistD> itemsitehistd;
        public PXSelect<ItemCostHist> itemcosthist;
		public PXSelect<ItemSalesHistD> itemsalehistd;
		public PXSelect<ItemCustSalesStats> itemcustsalesstats;

		public INIntegrityCheck()
		{
			INSetup record = insetup.Current;

			INItemList.SetProcessCaption(Messages.Process);
			INItemList.SetProcessAllCaption(Messages.ProcessAll);

			PXDBDefaultAttribute.SetDefaultForUpdate<INTranSplit.refNbr>(this.Caches[typeof(INTranSplit)], null, false);
			PXDBDefaultAttribute.SetDefaultForUpdate<INTranSplit.tranDate>(this.Caches[typeof(INTranSplit)], null, false);
		}

        protected IEnumerable initemlist()
        {
            if (Filter.Current.SiteID != null)
            {
                return null;
            }
            return new List<object>();
        }

		protected virtual void INSiteFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
            if (e.Row == null) return;

            INSiteFilter filter = (INSiteFilter)e.Row;

            INItemList.SetProcessDelegate<INIntegrityCheck>(delegate(INIntegrityCheck graph, InventoryItem item)
			{
				graph.Clear(PXClearOption.PreserveTimeStamp);
                graph.IntegrityCheckProc(new INItemSiteSummary { InventoryID = item.InventoryID, SiteID = filter != null ? filter.SiteID : null }, filter != null && filter.RebuildHistory == true ? filter.FinPeriodID : null, filter.ReplanBackorders == true);
			});
			PXUIFieldAttribute.SetEnabled<INSiteFilter.finPeriodID>(sender, null, filter.RebuildHistory == true);
		}

        protected virtual void SiteStatus_NegAvailQty_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
        {
            e.NewValue = true;
            e.Cancel = true;
        }

        private INPlanType _plan44;
        INPlanType Plan44 { get
            { if (_plan44 == null) _plan44 = PXSelect<INPlanType, Where<INPlanType.planType, Equal<INPlanConstants.plan44>>>.Select(this); return _plan44; } }
        private INPlanType _plan60;
        INPlanType Plan60
        {
            get
            { if (_plan60 == null) _plan60 = PXSelect<INPlanType, Where<INPlanType.planType, Equal<INPlanConstants.plan60>>>.Select(this); return _plan60; }
        }
        private INPlanType _plan61;
        INPlanType Plan61
        {
            get
            { if (_plan61 == null) _plan61 = PXSelect<INPlanType, Where<INPlanType.planType, Equal<INPlanConstants.plan61>>>.Select(this); return _plan61; }
        }
        private INPlanType _plan70;
        INPlanType Plan70
        {
            get
            { if (_plan70 == null) _plan70 = PXSelect<INPlanType, Where<INPlanType.planType, Equal<INPlanConstants.plan70>>>.Select(this); return _plan70; }
        }
        private INPlanType _plan74;
        INPlanType Plan74
        {
            get
            { if (_plan74 == null) _plan74 = PXSelect<INPlanType, Where<INPlanType.planType, Equal<INPlanConstants.plan74>>>.Select(this); return _plan74; }
        }
        private INPlanType _plan76;
        INPlanType Plan76
        {
            get
            { if (_plan76 == null) _plan76 = PXSelect<INPlanType, Where<INPlanType.planType, Equal<INPlanConstants.plan76>>>.Select(this); return _plan76; }
        }
        private INPlanType _plan42;
        INPlanType Plan42
        {
            get
            { if (_plan42 == null) _plan42 = PXSelect<INPlanType, Where<INPlanType.planType, Equal<INPlanConstants.plan42>>>.Select(this); return _plan42; }
        }
        

        public virtual bool PlanTypeCalc(INItemPlan plan, SOShipLineSplit sosplit, POReceiptLineSplit posplit, ref INPlanType plantype, out INPlanType locplantype)
        {
            switch (plan.PlanType)
            {
                case INPlanConstants.Plan61:
                case INPlanConstants.Plan63:
                    locplantype = plantype;

                    if (sosplit.ShipmentNbr != null)
                    {
                        SOOrderType ordetype = PXSelect<SOOrderType, Where<SOOrderType.orderType, Equal<Current<SOShipLineSplit.origOrderType>>>>.SelectSingleBound(this, new object[] { sosplit });

                        if (plan.OrigPlanType == null)
                        {
                            plan.OrigPlanType = ordetype.OrderPlanType;
                        }

                        if (plan.OrigPlanType == INPlanConstants.Plan60 && sosplit.IsComponentItem != true)
                        {
                            plantype = plantype - Plan60;
                        }

                        if ((plan.OrigPlanType == INPlanConstants.Plan61 || plan.OrigPlanType == INPlanConstants.Plan63) && sosplit.IsComponentItem != true)
                        {
                            plantype = plantype - Plan61;
                        }
                    }

                    break;
                case INPlanConstants.Plan71:
                case INPlanConstants.Plan72:
                    locplantype = plantype;
                    if (posplit.ReceiptNbr == null)
                    {
                        PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict("PlanID", PXDbType.BigInt, 8, plan.PlanID, PXComp.EQ));
                        return false;
                    }
                    if (posplit.PONbr != null)
                    {
                        plantype = plantype - Plan70;
                    }
                    break;
                case INPlanConstants.Plan77:
                    locplantype = plantype;
                    if (posplit.ReceiptNbr != null && posplit.PONbr != null)
                    {
                        plantype = plantype - Plan76;
                    }

                    break;
                case INPlanConstants.Plan75:
                    locplantype = plantype;
                    if (posplit.ReceiptNbr != null && posplit.PONbr != null)
                    {
                        plantype = plantype - Plan74;
                    }
                    break;
                case INPlanConstants.Plan43:
                case INPlanConstants.Plan45:
                    if (plan.OrigPlanType == INPlanConstants.Plan44)
                    {
                        plantype = plantype - Plan44;
                    }

                    if (plan.OrigPlanType == INPlanConstants.Plan42)
                    {
                        plantype = plantype - Plan42;
                    }
                    locplantype = plantype;
                    break;

                default:
                    locplantype = plantype;
                    break;
            }
            return true;
        }


        public virtual void IntegrityCheckProc(INItemSiteSummary itemsite, string minPeriod, bool replanBackorders)
        {
            using (PXConnectionScope cs = new PXConnectionScope())
            {
                using (PXTransactionScope ts = new PXTransactionScope())
                {
                    foreach (INItemPlan p in PXSelectReadonly2<INItemPlan, LeftJoin<Note, On<Note.noteID, Equal<INItemPlan.refNoteID>>>, Where<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<Note.noteID, IsNull>>>>.SelectMultiBound(this, new object[] { itemsite }))
                    {
                        PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict("PlanID", PXDbType.BigInt, 8, p.PlanID, PXComp.EQ));
                    }

                    foreach (INItemPlan p in PXSelectReadonly2<INItemPlan,
                        InnerJoin<INRegister, On<INRegister.noteID, Equal<INItemPlan.refNoteID>, And<INRegister.siteID, Equal<INItemPlan.siteID>>>>,
                        Where<INRegister.docType, Equal<INDocType.transfer>,
                            And<INRegister.released, Equal<boolTrue>,
                            And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>,
                            And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>>.SelectMultiBound(this, new object[] { itemsite }))
                    {
                        PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict("PlanID", PXDbType.BigInt, 8, p.PlanID, PXComp.EQ));
                    }

                    //d22.
                    foreach (INItemPlan p in PXSelectReadonly2<INItemPlan,
                    InnerJoin<PO.POReceipt, On<PO.POReceipt.noteID, Equal<INItemPlan.refNoteID>>,
                    LeftJoin<POReceiptLineSplit, On<POReceiptLineSplit.receiptNbr, Equal<PO.POReceipt.receiptNbr>
                        , And<POReceiptLineSplit.receiptType, Equal<PO.POReceipt.receiptType>
                        , And<POReceiptLineSplit.planID, Equal<INItemPlan.planID>>>>>>,
                    Where<POReceiptLineSplit.receiptNbr, IsNull,
                    And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>,
                    And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>.SelectMultiBound(this, new object[] { itemsite }))
                    {
                        PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict("PlanID", PXDbType.BigInt, 8, p.PlanID, PXComp.EQ));
                    }

                    //d32.
                    foreach (INItemPlan p in PXSelectReadonly2<INItemPlan,
                    InnerJoin<SOOrder, On<SOOrder.noteID, Equal<INItemPlan.refNoteID>>,
                    LeftJoin<SOLineSplit, On<SOLineSplit.orderType, Equal<SOOrder.orderType>
                        , And<SOLineSplit.orderNbr, Equal<SOOrder.orderNbr>
                        , And<SOLineSplit.planID, Equal<INItemPlan.planID>>>>>>,
                    Where<SOLineSplit.orderNbr, IsNull,
                    And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>,
                    And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>.SelectMultiBound(this, new object[] { itemsite }))
                    {
                        PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict("PlanID", PXDbType.BigInt, 8, p.PlanID, PXComp.EQ));
                    }

                    //d33.
                    foreach (INItemPlan p in PXSelectReadonly2<INItemPlan,
                    InnerJoin<SOShipment, On<SOShipment.noteID, Equal<INItemPlan.refNoteID>>,
                    LeftJoin<SOShipLineSplit, On<SOShipLineSplit.shipmentNbr, Equal<SOShipment.shipmentNbr>
                        , And<SOShipLineSplit.planID, Equal<INItemPlan.planID>>>>>,
                    Where<SOShipLineSplit.shipmentNbr, IsNull,
                    And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>,
                    And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>.SelectMultiBound(this, new object[] { itemsite }))
                    {
                        PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict("PlanID", PXDbType.BigInt, 8, p.PlanID, PXComp.EQ));
                    }


                    var transferGraph = CreateInstance<INTransferEntry>();
                    foreach (PXResult<INLocationStatus2, INTransitLine> res in PXSelectJoin<INLocationStatus2,
                            InnerJoin<INTransitLine, On<INTransitLine.costSiteID, Equal<INLocationStatus2.locationID>>,
                            LeftJoin<INItemPlan, On<INItemPlan.refNoteID, Equal<INTransitLine.noteID>>>>,
                            Where<INLocationStatus2.qtyOnHand, Greater<decimal0>, 
                                And<INLocationStatus2.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>,
                                And<INTransitLine.toSiteID, Equal<Current<INItemSiteSummary.siteID>>,
                                And<INItemPlan.planID, IsNull>>>>>.SelectMultiBound(transferGraph, new object[] { itemsite }))
                    {
                        INItemPlan plan;
                        var locst = (INLocationStatus2)res;
                        var tl = (INTransitLine)res;
                        

                        foreach (TransitLotSerialStatus tlss in
                            PXSelect<TransitLotSerialStatus, 
                            Where <TransitLotSerialStatus.locationID, Equal<Current<INLocationStatus2.locationID>>,
                            And<TransitLotSerialStatus.inventoryID, Equal<Current<INLocationStatus2.inventoryID >>,
                            And<TransitLotSerialStatus.subItemID, Equal<Current<INLocationStatus2.subItemID>>,
                            And<TransitLotSerialStatus.qtyOnHand, Greater<decimal0>>>>>>.SelectMultiBound(transferGraph, new object[] { locst }))
                            {
                            plan = (INItemPlan)transferGraph.Caches[typeof(INItemPlan)].CreateInstance();
                            plan.PlanType = tl.SOShipmentNbr == null ? INPlanConstants.Plan42 : INPlanConstants.Plan44;
                            plan.InventoryID = tlss.InventoryID;
                            plan.SubItemID = tlss.SubItemID ?? locst.SubItemID;
                            plan.LotSerialNbr = tlss.LotSerialNbr;
                            plan.SiteID = tl.ToSiteID;
                            plan.LocationID = tl.ToLocationID;
                            plan.FixedSource = INReplenishmentSource.Purchased;
                            plan.PlanDate = tl.CreatedDateTime;
                            plan.Reverse = false;
                            plan.Hold = false;
                            plan.PlanQty = tlss.QtyOnHand;
                            locst.QtyOnHand -= tlss.QtyOnHand;
                            plan.RefNoteID = tl.NoteID;
                            plan = (INItemPlan)transferGraph.Caches[typeof(INItemPlan)].Insert(plan);
                        }

                        if (locst.QtyOnHand <= 0m)
                            continue;
                        plan = (INItemPlan)transferGraph.Caches[typeof(INItemPlan)].CreateInstance();
                        plan.PlanType = tl.SOShipmentNbr == null ? INPlanConstants.Plan42 : INPlanConstants.Plan44;
                        plan.InventoryID = locst.InventoryID;
                        plan.SubItemID = locst.SubItemID;
                        plan.SiteID = tl.ToSiteID;
                        plan.LocationID = tl.ToLocationID;
                        plan.FixedSource = INReplenishmentSource.Purchased;
                        plan.PlanDate = tl.CreatedDateTime;
                        plan.Reverse = false;
                        plan.Hold = false;
                        plan.PlanQty = locst.QtyOnHand;
                        plan.RefNoteID = tl.NoteID;
                        plan = (INItemPlan)transferGraph.Caches[typeof(INItemPlan)].Insert(plan);
                    }
                    transferGraph.Save.Press();

					//Deleting records from INLotSerialStatus, INItemLotSerial, INSiteLotSerial if item is not Lot/Serial tracked any more
					InventoryItem notTrackedItem = PXSelectJoin<InventoryItem, 
						InnerJoin<INLotSerClass, On<INLotSerClass.lotSerClassID, Equal<InventoryItem.lotSerClassID>, And<INLotSerClass.lotSerTrack, Equal<INLotSerTrack.notNumbered>>>,
						InnerJoin<INLotSerialStatus, On<INLotSerialStatus.inventoryID, Equal<InventoryItem.inventoryID>>>>,
						Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.SelectWindowed(this, 0, 1, itemsite.InventoryID);
					if (notTrackedItem != null && !InventoryItemMaint.IsQtyStillPresent(this, itemsite.InventoryID))
					{
						PXDatabase.Delete<INLotSerialStatus>(
							new PXDataFieldRestrict<INLotSerialStatus.inventoryID>(PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
							new PXDataFieldRestrict<INLotSerialStatus.qtyOnHand>(PXDbType.Decimal, 4, 0m, PXComp.EQ)
						);

						PXDatabase.Delete<INItemLotSerial>(
							new PXDataFieldRestrict<INItemLotSerial.inventoryID>(PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
							new PXDataFieldRestrict<INItemLotSerial.qtyOnHand>(PXDbType.Decimal, 4, 0m, PXComp.EQ)
						);

						PXDatabase.Delete<INSiteLotSerial>(
							new PXDataFieldRestrict<INSiteLotSerial.inventoryID>(PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
							new PXDataFieldRestrict<INSiteLotSerial.qtyOnHand>(PXDbType.Decimal, 4, 0m, PXComp.EQ)
						);
					}

                    PXDatabase.Update<INSiteStatus>(
							new PXDataFieldRestrict<INSiteStatus.inventoryID>(PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
							new PXDataFieldRestrict<INSiteStatus.siteID>(PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
							new PXDataFieldAssign<INSiteStatus.qtyAvail>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyHardAvail>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyActual>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyNotAvail>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyINIssues>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyINReceipts>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyInTransit>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyINAssemblySupply>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyINAssemblyDemand>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyInTransitToProduction>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyProductionSupplyPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyProductionSupply>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyPOFixedProductionPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyPOFixedProductionOrders>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyProductionDemandPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyProductionDemand>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyProductionAllocated>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtySOFixedProduction>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyINReplaned>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyPOPrepared>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyPOOrders>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyPOReceipts>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtySOPrepared>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtySOBooked>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtySOShipped>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtySOShipping>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtySOBackOrdered>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtySOFixed>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyPOFixedOrders>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyPOFixedPrepared>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyPOFixedReceipts>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtySODropShip>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyPODropShipOrders>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyPODropShipPrepared>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INSiteStatus.qtyPODropShipReceipts>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INSiteStatus.qtyInTransitToSO>(PXDbType.Decimal, 0m)
                        );

                    PXDatabase.Update<INLocationStatus>(
                            new PXDataFieldRestrict<INLocationStatus.inventoryID>(PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
                            new PXDataFieldRestrict<INLocationStatus.siteID>(PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
                            new PXDataFieldAssign<INLocationStatus.qtyAvail>(PXDbType.DirectExpression, "QtyOnHand"),
                            new PXDataFieldAssign<INLocationStatus.qtyHardAvail>(PXDbType.DirectExpression, "QtyOnHand"),
							new PXDataFieldAssign<INLocationStatus.qtyActual>(PXDbType.DirectExpression, "QtyOnHand"),
                            new PXDataFieldAssign<INLocationStatus.qtyINIssues>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyINReceipts>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyInTransit>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyINAssemblySupply>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyINAssemblyDemand>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyInTransitToProduction>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyProductionSupplyPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyProductionSupply>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPOFixedProductionPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPOFixedProductionOrders>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyProductionDemandPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyProductionDemand>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyProductionAllocated>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtySOFixedProduction>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPOPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPOOrders>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPOReceipts>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INLocationStatus.qtySOPrepared>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INLocationStatus.qtySOBooked>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtySOShipped>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtySOShipping>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtySOBackOrdered>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtySOFixed>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPOFixedOrders>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPOFixedPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPOFixedReceipts>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtySODropShip>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPODropShipOrders>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPODropShipPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyPODropShipReceipts>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLocationStatus.qtyInTransitToSO>(PXDbType.Decimal, 0m)
                        );

                    PXDatabase.Update<INLotSerialStatus>(
                            new PXDataFieldRestrict<INLotSerialStatus.inventoryID>(PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
							new PXDataFieldRestrict<INLotSerialStatus.siteID>(PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
							new PXDataFieldAssign<INLotSerialStatus.qtyAvail>(PXDbType.DirectExpression, "QtyOnHand"),
							new PXDataFieldAssign<INLotSerialStatus.qtyHardAvail>(PXDbType.DirectExpression, "QtyOnHand"),
							new PXDataFieldAssign<INLotSerialStatus.qtyActual>(PXDbType.DirectExpression, "QtyOnHand"),
                            new PXDataFieldAssign<INLotSerialStatus.qtyINIssues>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyINReceipts>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyInTransit>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyINAssemblySupply>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyINAssemblyDemand>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyInTransitToProduction>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyProductionSupplyPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyProductionSupply>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPOFixedProductionPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPOFixedProductionOrders>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyProductionDemandPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyProductionDemand>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyProductionAllocated>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtySOFixedProduction>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPOPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPOOrders>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPOReceipts>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INLotSerialStatus.qtySOPrepared>(PXDbType.Decimal, 0m),
							new PXDataFieldAssign<INLotSerialStatus.qtySOBooked>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtySOShipped>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtySOShipping>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtySOBackOrdered>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtySOFixed>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPOFixedOrders>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPOFixedPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPOFixedReceipts>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtySODropShip>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPODropShipOrders>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPODropShipPrepared>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyPODropShipReceipts>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign<INLotSerialStatus.qtyInTransitToSO>(PXDbType.Decimal, 0m)

                        );

                    PXDatabase.Update<INItemLotSerial>(
                            new PXDataFieldRestrict("InventoryID", PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
                            new PXDataFieldAssign("QtyAvail", PXDbType.DirectExpression, "QtyOnHand"),
                            new PXDataFieldAssign("QtyHardAvail", PXDbType.DirectExpression, "QtyOnHand"),
							new PXDataFieldAssign("QtyActual", PXDbType.DirectExpression, "QtyOnHand"),
							new PXDataFieldAssign<INItemLotSerial.qtyOnReceipt>(PXDbType.Decimal, 0m),
                            new PXDataFieldAssign("QtyINTransit", PXDbType.Decimal, 0m)
                        );

					PXDatabase.Update<INSiteLotSerial>(
							new PXDataFieldRestrict<INSiteLotSerial.inventoryID>(PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
							new PXDataFieldRestrict<INSiteLotSerial.siteID>(PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
							new PXDataFieldAssign<INSiteLotSerial.qtyAvail>(PXDbType.DirectExpression, "QtyOnHand"),
							new PXDataFieldAssign<INSiteLotSerial.qtyHardAvail>(PXDbType.DirectExpression, "QtyOnHand"),
							new PXDataFieldAssign<INSiteLotSerial.qtyActual>(PXDbType.DirectExpression, "QtyOnHand"),
							new PXDataFieldAssign<INSiteLotSerial.qtyInTransit>(PXDbType.Decimal, 0m)
						);


                    foreach (PXResult<ReadOnlyLocationStatus, INLocation> res in PXSelectJoinGroupBy<ReadOnlyLocationStatus, InnerJoin<INLocation, On<INLocation.locationID, Equal<ReadOnlyLocationStatus.locationID>>>, Where<ReadOnlyLocationStatus.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<ReadOnlyLocationStatus.siteID, Equal<Current<INItemSiteSummary.siteID>>>>, Aggregate<GroupBy<ReadOnlyLocationStatus.inventoryID, GroupBy<ReadOnlyLocationStatus.siteID, GroupBy<ReadOnlyLocationStatus.subItemID, GroupBy<INLocation.inclQtyAvail, Sum<ReadOnlyLocationStatus.qtyOnHand>>>>>>>.SelectMultiBound(this, new object[] { itemsite }))
                    {
                        SiteStatus status = new SiteStatus();
                        status.InventoryID = ((ReadOnlyLocationStatus)res).InventoryID;
                        status.SubItemID = ((ReadOnlyLocationStatus)res).SubItemID;
                        status.SiteID = ((ReadOnlyLocationStatus)res).SiteID;
                        status = (SiteStatus)sitestatus.Cache.Insert(status);

                        if (((INLocation)res).InclQtyAvail == true)
                        {
                            status.QtyAvail += ((ReadOnlyLocationStatus)res).QtyOnHand;
                            status.QtyHardAvail += ((ReadOnlyLocationStatus)res).QtyOnHand;
							status.QtyActual += ((ReadOnlyLocationStatus)res).QtyOnHand;
                        }
                        else
                        {
                            status.QtyNotAvail += ((ReadOnlyLocationStatus)res).QtyOnHand;
                        }
                    }

					foreach (PXResult<INItemPlan, INPlanType, InventoryItem, SOShipLineSplit, POReceiptLineSplit> res in PXSelectJoin<INItemPlan,
                        InnerJoin<INPlanType, On<INPlanType.planType, Equal<INItemPlan.planType>>,
						InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INItemPlan.inventoryID>>,
                        LeftJoin<SOShipLineSplit, On<SOShipLineSplit.planID, Equal<INItemPlan.planID>>,
						LeftJoin<POReceiptLineSplit, On<POReceiptLineSplit.planID, Equal<INItemPlan.planID>>>>>>,
						Where<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>,
							And<InventoryItem.stkItem, Equal<boolTrue>>>>>
						.SelectMultiBound(this, new object[] { itemsite }))
                    {
                        INItemPlan plan = (INItemPlan)res;
                        INPlanType plantype = (INPlanType)res;
                        INPlanType locplantype;
                        SOShipLineSplit sosplit = (SOShipLineSplit)res;
                        POReceiptLineSplit posplit = (POReceiptLineSplit)res;

                        if (plan.InventoryID != null &&
                                plan.SubItemID != null &&
                                plan.SiteID != null)
                        {
                            if (!PlanTypeCalc(plan, sosplit, posplit, ref plantype, out locplantype))
                                continue;

                            if (plan.LocationID != null)
                            {
                                LocationStatus item = INItemPlanIDAttribute.UpdateAllocatedQuantitiesBase<LocationStatus>(this, plan, locplantype, true);
                                INItemPlanIDAttribute.UpdateAllocatedQuantitiesBase<SiteStatus>(this, plan, plantype, (bool)item.InclQtyAvail);
                                if (!string.IsNullOrEmpty(plan.LotSerialNbr))
                                {
                                    INItemPlanIDAttribute.UpdateAllocatedQuantitiesBase<LotSerialStatus>(this, plan, locplantype, true);
									INItemPlanIDAttribute.UpdateAllocatedQuantitiesBase<SiteLotSerial>(this, plan, locplantype, true);
                                }
                            }
                            else
                            {
                                INItemPlanIDAttribute.UpdateAllocatedQuantitiesBase<SiteStatus>(this, plan, plantype, true);
								if (!string.IsNullOrEmpty(plan.LotSerialNbr))
								{
									//TODO: check if LotSerialNbr was allocated on OrigPlanType
									INItemPlanIDAttribute.UpdateAllocatedQuantitiesBase<SiteLotSerial>(this, plan, plantype, true);
								}
                            }
                        }
                    }

                    //Updating cross-site ItemLotSerial
                    foreach (PXResult<INItemPlan, INPlanType, SOShipLineSplit, POReceiptLineSplit> res in PXSelectJoin<INItemPlan,
                                    InnerJoin<INPlanType, On<INPlanType.planType, Equal<INItemPlan.planType>>,
                                    LeftJoin<SOShipLineSplit, On<SOShipLineSplit.planID, Equal<INItemPlan.planID>>,
                                    LeftJoin<POReceiptLineSplit, On<POReceiptLineSplit.planID, Equal<INItemPlan.planID>>>>>,
                            Where<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, 
                                And<INItemPlan.lotSerialNbr, NotEqual<StringEmpty>, 
                                And<INItemPlan.lotSerialNbr, IsNotNull>>>>
                            .SelectMultiBound(this, new object[] { itemsite }))
                    {
                        INItemPlan plan = (INItemPlan)res;
                        INPlanType plantype = (INPlanType)res;
                        INPlanType locplantype;
                        SOShipLineSplit sosplit = (SOShipLineSplit)res;
                        POReceiptLineSplit posplit = (POReceiptLineSplit)res;

                        if (plan.InventoryID != null &&
                         plan.SubItemID != null &&
                         plan.SiteID != null)
                        {
                            if (!PlanTypeCalc(plan, sosplit, posplit, ref plantype, out locplantype))
                                continue;

                            if (plan.LocationID != null)
                            {
                                INItemPlanIDAttribute.UpdateAllocatedQuantitiesBase<ItemLotSerial>(this, plan, locplantype, true);
                            }
                            else
                            {
                                INItemPlanIDAttribute.UpdateAllocatedQuantitiesBase<ItemLotSerial>(this, plan, plantype, true);
                            }
                        }
                    }

                    if (replanBackorders)
	                {
		                INReleaseProcess.ReplanBackOrders(this);
						initemplan.Cache.Persist(PXDBOperation.Insert);
						initemplan.Cache.Persist(PXDBOperation.Update);
	                }

                    Caches[typeof(INTranSplit)].Persist(PXDBOperation.Update);

                    sitestatus.Cache.Persist(PXDBOperation.Insert);
                    sitestatus.Cache.Persist(PXDBOperation.Update);

                    locationstatus.Cache.Persist(PXDBOperation.Insert);
                    locationstatus.Cache.Persist(PXDBOperation.Update);

                    lotserialstatus.Cache.Persist(PXDBOperation.Insert);
                    lotserialstatus.Cache.Persist(PXDBOperation.Update);

                    itemlotserial.Cache.Persist(PXDBOperation.Insert);
                    itemlotserial.Cache.Persist(PXDBOperation.Update);

					sitelotserial.Cache.Persist(PXDBOperation.Insert);
					sitelotserial.Cache.Persist(PXDBOperation.Update);

                    if (minPeriod != null)
                    {
                        FinPeriod period =
                            PXSelect<FinPeriod,
                                Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>>>
                                .SelectWindowed(this, 0, 1, minPeriod);
                        if (period == null) return;
                        DateTime startDate = (DateTime)period.StartDate;

                        PXDatabase.Delete<INItemCostHist>(
                            new PXDataFieldRestrict("InventoryID", PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
                            new PXDataFieldRestrict("CostSiteID", PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
                            new PXDataFieldRestrict("FinPeriodID", PXDbType.Char, 6, minPeriod, PXComp.GE)
                            );

                        PXDatabase.Delete<INItemSalesHistD>(
                            new PXDataFieldRestrict("InventoryID", PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
                            new PXDataFieldRestrict("SiteID", PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
                            new PXDataFieldRestrict("QtyPlanSales", PXDbType.Decimal, 0m),
                            new PXDataFieldRestrict("SDate", PXDbType.DateTime, 8, startDate, PXComp.GE)

                            );
                        PXDatabase.Delete<INItemCustSalesStats>(
                            new PXDataFieldRestrict("InventoryID", PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
                            new PXDataFieldRestrict("SiteID", PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
                            new PXDataFieldRestrict("LastDate", PXDbType.DateTime, 8, startDate, PXComp.GE));

                        PXDatabase.Update<INItemSalesHistD>(
                            new PXDataFieldAssign("QtyIssues", PXDbType.Decimal, 0m),
                            new PXDataFieldAssign("QtyExcluded", PXDbType.Decimal, 0m),
                            new PXDataFieldRestrict("InventoryID", PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
                            new PXDataFieldRestrict("SiteID", PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
                            new PXDataFieldRestrict("SDate", PXDbType.DateTime, 8, startDate, PXComp.GE)
                            );

                        foreach (INLocation loc in PXSelectReadonly2<INLocation, InnerJoin<INItemCostHist, On<INItemCostHist.costSiteID, Equal<INLocation.locationID>>>, Where<INLocation.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<INItemCostHist.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>>>>.SelectMultiBound(this, new object[] { itemsite }))
                        {
                            PXDatabase.Delete<INItemCostHist>(
                                new PXDataFieldRestrict("InventoryID", PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
                                new PXDataFieldRestrict("CostSiteID", PXDbType.Int, 4, loc.LocationID, PXComp.EQ),
                                new PXDataFieldRestrict("FinPeriodID", PXDbType.Char, 6, minPeriod, PXComp.GE)
                                );
                        }

                        PXDatabase.Delete<INItemSiteHist>(
                            new PXDataFieldRestrict("InventoryID", PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
                            new PXDataFieldRestrict("SiteID", PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
                            new PXDataFieldRestrict("FinPeriodID", PXDbType.Char, 6, minPeriod, PXComp.GE)
                            );

                        PXDatabase.Delete<INItemSiteHistD>(
                            new PXDataFieldRestrict("InventoryID", PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
                            new PXDataFieldRestrict("SiteID", PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
                            new PXDataFieldRestrict("SDate", PXDbType.DateTime, 8, startDate, PXComp.GE)
                            );

                        INTran prev_tran = null;
                        foreach (PXResult<INTran, INTranSplit> res in PXSelectReadonly2<INTran, InnerJoin<INTranSplit, On<INTranSplit.tranType, Equal<INTran.tranType>, And<INTranSplit.refNbr, Equal<INTran.refNbr>, And<INTranSplit.lineNbr, Equal<INTran.lineNbr>>>>>, Where<INTran.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INTran.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<INTran.finPeriodID, GreaterEqual<Required<INTran.finPeriodID>>, And<INTran.released, Equal<boolTrue>>>>>, OrderBy<Asc<INTran.tranType, Asc<INTran.refNbr, Asc<INTran.lineNbr>>>>>.SelectMultiBound(this, new object[] { itemsite }, minPeriod))
                        {
                            INTran tran = res;
                            INTranSplit split = res;

                            if (!Caches[typeof(INTran)].ObjectsEqual(prev_tran, tran))
                            {
                                INReleaseProcess.UpdateSalesHistD(this, tran);
                                INReleaseProcess.UpdateCustSalesStats(this, tran);

                                prev_tran = tran;
                            }

                            if (split.BaseQty != 0m)
                            {
                                INReleaseProcess.UpdateSiteHist(this, res, split);
                                INReleaseProcess.UpdateSiteHistD(this, split);
                            }
                        }

                        foreach (PXResult<INTran, INTranCost> res in PXSelectReadonly2<INTran, InnerJoin<INTranCost, On<INTranCost.tranType, Equal<INTran.tranType>, And<INTranCost.refNbr, Equal<INTran.refNbr>, And<INTranCost.lineNbr, Equal<INTran.lineNbr>>>>>, Where<INTran.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INTran.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<INTranCost.finPeriodID, GreaterEqual<Required<INTran.finPeriodID>>, And<INTran.released, Equal<boolTrue>>>>>>.SelectMultiBound(this, new object[] { itemsite }, minPeriod))
                        {
                            INReleaseProcess.UpdateCostHist(this, (INTranCost)res, (INTran)res);
                        }

                        itemcosthist.Cache.Persist(PXDBOperation.Insert);
                        itemcosthist.Cache.Persist(PXDBOperation.Update);

                        itemsitehist.Cache.Persist(PXDBOperation.Insert);
                        itemsitehist.Cache.Persist(PXDBOperation.Update);

                        itemsitehistd.Cache.Persist(PXDBOperation.Insert);
                        itemsitehistd.Cache.Persist(PXDBOperation.Update);

                        itemsalehistd.Cache.Persist(PXDBOperation.Insert);
                        itemsalehistd.Cache.Persist(PXDBOperation.Update);

                        itemcustsalesstats.Cache.Persist(PXDBOperation.Insert);
                        itemcustsalesstats.Cache.Persist(PXDBOperation.Update);
                    }

					DeleteZeroStatusRecords(itemsite);

                    ts.Complete();
                }

                sitestatus.Cache.Persisted(false);
                locationstatus.Cache.Persisted(false);
                lotserialstatus.Cache.Persisted(false);

                itemcosthist.Cache.Persisted(false);
                itemsitehist.Cache.Persisted(false);
                itemsitehistd.Cache.Persisted(false);
            }
        }

		public virtual void DeleteZeroStatusRecords(INItemSiteSummary itemsite)
		{
			var restrictions = new List<PXDataFieldRestrict>
			{
				new PXDataFieldRestrict(nameof(INLocationStatus.InventoryID), PXDbType.Int, 4, itemsite.InventoryID, PXComp.EQ),
				new PXDataFieldRestrict(nameof(INLocationStatus.SiteID), PXDbType.Int, 4, itemsite.SiteID, PXComp.EQ),
				// just for reliability as it may cause very sensitive data loss
				new PXDataFieldRestrict(nameof(INLocationStatus.QtyOnHand), PXDbType.Decimal, decimal.Zero),
				new PXDataFieldRestrict(nameof(INLocationStatus.QtyAvail), PXDbType.Decimal, decimal.Zero),
				new PXDataFieldRestrict(nameof(INLocationStatus.QtyHardAvail), PXDbType.Decimal, decimal.Zero),
			};
			restrictions.AddRange(
				locationstatus.Cache.Fields
				.Where(f => locationstatus.Cache.GetAttributesReadonly(f).OfType<PXDBQuantityAttribute>().Any())
				.Select(f => new PXDataFieldRestrict(f, PXDbType.Decimal, decimal.Zero)));
			PXDatabase.Delete<INLocationStatus>(restrictions.ToArray());
		}
    }
}
