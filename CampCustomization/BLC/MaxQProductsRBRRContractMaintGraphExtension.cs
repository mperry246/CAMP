using System;
using System.Collections;
using System.Collections.Generic;
using PX.SM;
using PX.Data;
using System.Linq;
using CAMPCustomization;
using PX.Objects.IN;
using PX.Objects.AR;



namespace MaxQ.Products.RBRR
{
    public class MaxQProductsRBRRContractMaintGraphExtension : PXGraphExtension<ContractMaint>
    {
        public override void Initialize()
        {
            Base.report.AddMenuAction(ProFormaInvoice);
        }

        [PXCopyPasteHiddenView]
        public PXFilter<CreditInvoiceSettings> CreditSettings;

        [PXCopyPasteHiddenView]
        public PXFilter<CancelContractSettings> CancellationSettings;




        #region Actions

        public PXAction<XRBContrHdr> GenerateOrder;
        [PXUIField(DisplayName = "Generate Bill", MapEnableRights = PXCacheRights.Update,
                     MapViewRights = PXCacheRights.Update, Enabled = true)]
        [PXButton()]
        public IEnumerable generateOrder(PXAdapter adapter)
        {
            /*PXGraph.InstanceCreated.AddHandler<ContractMaint>((graph) =>
            {
                graph.RowPersisting.AddHandler<XRBContrHdr>((sender, e) =>
                {
                    XRBContrHdr contract = PXResult<XRBContrHdr>.Current;
                    XRBContrDet contractdet = PXResult<XRBContrDet>.Current;
                    XRBContrHdrExt contractEX = PXCache<XRBContrHdr>.GetExtension<XRBContrHdrExt>(contract);
                    CAMPContractType contracttype = (CAMPContractType)PXSelect<CAMPContractType,
                       Where<CAMPContractType.contractTypeID,
                        Equal<Required<XRBContrHdrExt.usrContractTypeID>>>>.Select(Base, contractEX.UsrContractTypeID);
                    if ((bool)contractdet.ManuallyBooked && contracttype != null && (bool)contracttype.UpdateContractType)
                    {
                        contractEX.UsrContractTypeID = contracttype.NextContractTypeID;
                    }
                });
            }); */

            PXGraph.InstanceCreated.AddHandler<PX.Objects.AR.ARInvoiceEntry>((graph) =>
            {
                graph.RowInserting.AddHandler<PX.Objects.AR.ARInvoice>((sender, e) =>
                {
                    XRBContrHdr contract = PXResult<XRBContrHdr>.Current;
                    XRBContrDet contractdet = PXResult<XRBContrDet>.Current;
                    XRBContrHdrExt contractEX = PXCache<XRBContrHdr>.GetExtension<XRBContrHdrExt>(contract);
                    PX.Objects.AR.ARInvoice invoice = (PX.Objects.AR.ARInvoice)e.Row;
                    ARRegisterExt ext = PXCache<PX.Objects.AR.ARRegister>.GetExtension<ARRegisterExt>(invoice);
                    CAMPLineofBusiness lob = (CAMPLineofBusiness)PXSelect<CAMPLineofBusiness,
                        Where<CAMPLineofBusiness.lineofBusinessID,
                            Equal<Current<XRBContrHdrExt.usrLineofBusiness>>>>.Select(Base);
                    if (contract != null)
                    {
                        ext.UsrContractID = contract.ContractID;
                        ext.UsrContractCD = contract.ContractCD;
                        ext.UsrRevisionNbr = contract.RevisionNbr;
                        ext.UsrLineofBusiness = contractEX.UsrLineofBusiness;
                        ext.UsrContractTypeID = contractEX.UsrContractTypeID;
                        ext.UsrTotPeriods = contract.TotPeriods;

                    }
                    if (contractdet != null)
                    {
                        ext.UsrGenerateDate = contractdet.GenDate;
                        switch (contract.TotPeriods)
                        {
                            case ContractMaintContractInterval_List.Every18Months_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(18);
                                break;
                            case ContractMaintContractInterval_List.FiveYears_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(60);
                                break;
                            case ContractMaintContractInterval_List.SixYears_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(72);
                                break;
                            case ContractMaintContractInterval_List.SevenYears_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(84);
                                break;
                            case ContractMaintContractInterval_List.ThreeYears_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(36);
                                break;
                            case ContractMaintContractInterval_List.Biennially_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(24);
                                break;
                            case ContractMaintContractInterval_List.Years_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(12);
                                break;
                            case ContractMaintContractInterval_List.SemiAnnually_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(6);
                                break;
                            case ContractMaintContractInterval_List.Quarters_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(3);
                                break;
                            case ContractMaintContractInterval_List.BiMonthly_key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(2);
                                break;
                            case ContractMaintContractInterval_List.Months_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(1);
                                break;
                            case ContractMaintContractInterval_List.UserDefined_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(60);
                                break;
                            case ContractMaintContractInterval_List.FourMonths_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(4);
                                break;
                            case ContractMaintContractInterval_List.BiWeekly_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddDays(14);
                                break;
                            case ContractMaintContractInterval_List.Days_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddDays(1);
                                break;
                            case ContractMaintContractInterval_List.Weeks_Key:
                                ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddDays(7);
                                break;
                        }
                        ext.UsrContractEndDate = ((DateTime)ext.UsrContractEndDate).AddDays(-1);
                    }
                });
                graph.RowInserting.AddHandler<PX.Objects.AR.ARTran>((sender, e) =>
                {
                    XRBGLDist contractdist = PXResult<XRBGLDist>.Current;
                    XRBGLDistExt contractdistEX = PXCache<XRBGLDist>.GetExtension<XRBGLDistExt>(contractdist);
                    if (contractdist != null)
                    {
                        ARTranExt ext = PXCache<PX.Objects.AR.ARTran>.GetExtension<ARTranExt>((PX.Objects.AR.ARTran)e.Row);
                        ext.UsrProfileNumber = contractdistEX.UsrProfileNumber;
                        ext.UsrDiscountReasonID = contractdistEX.UsrDiscountReasonID;
                        ext.UsrRegNbr = contractdistEX.UsrRegNbr;
                    }
                });
            });

            return Base.generateOrder.Press(adapter);

        }

        public PXAction<XRBContrHdr> VoidOrder;
        [PXUIField(DisplayName = "Void Bill", MapEnableRights = PXCacheRights.Update,
                     MapViewRights = PXCacheRights.Update, Enabled = true)]
        [PXButton()]
        public IEnumerable voidOrder(PXAdapter adapter)
        {
            if (CreditSettings.AskExt() == WebDialogResult.OK)
            {
                if (CreditSettings.Current.CreditDate != null && CreditSettings.Current.ReasonCodeID != null)
                {
                    XRBContrDet contractdet = Base.ContractDetails.Current;

                    PXGraph.InstanceCreated.AddHandler<PX.Objects.AR.ARInvoiceEntry>((graph) =>
                    {
                        graph.RowInserting.AddHandler<PX.Objects.AR.ARInvoice>((sender, e) =>
                        {
                            ARRegisterExt invoiceExt = PXCache<PX.Objects.AR.ARRegister>.GetExtension<ARRegisterExt>((PX.Objects.AR.ARInvoice)e.Row);
                            invoiceExt.UsrGenerateDate = CreditSettings.Current.CreditDate;
                            invoiceExt.UsrReasonCodeID = CreditSettings.Current.ReasonCodeID;
                            invoiceExt.UsrOrginalRefNbr = (contractdet != null) ? contractdet.ARRefNbr : null;

                        });
                    });
                }
                else
                {
                    throw new PXSetPropertyException<CancelContractSettings.reasonCD>(
                                       "Enter a reason code.", PXErrorLevel.Error);
                }
            }
            return Base.voidOrder.Press(adapter);

        }


        /* Override InventoryID */
        [PXDBInt()]
        [PXUIField(DisplayName = "Inventory ID")]
        //[CAMPXRBGLDistInventorySelector]
        [PXSelector(typeof(Search<PX.Objects.IN.InventoryItem.inventoryID,
                            Where<PX.Objects.IN.InventoryItem.itemStatus,
                                Equal<PX.Objects.IN.InventoryItemStatus.active>>,
                            OrderBy<Asc<PX.Objects.IN.InventoryItem.inventoryCD>>>),
                   new Type[]{
                            typeof(PX.Objects.IN.InventoryItem.inventoryCD),
                            typeof(PX.Objects.IN.InventoryItem.descr),
                   typeof(PX.Objects.IN.InventoryItem.itemClassID),
                   typeof(PX.Objects.IN.InventoryItem.itemStatus),
                   typeof(PX.Objects.IN.InventoryItem.basePrice)},
                   DescriptionField = typeof(PX.Objects.IN.InventoryItem.inventoryCD))]
        [PXRestrictor(typeof(Where2<Where<InventoryItemExt.usrLineofBusinessID,
                                           Equal<Current<XRBContrHdrExt.usrLineofBusiness>>,
                                           And<InventoryItemExt.usrTapeModelID,
                                                Equal<Current<XRBGLDistExt.usrTapeModelID>>,
                                    Or<InventoryItemExt.usrIsAircraft, Equal<False>,
                                            And<InventoryItemExt.usrLineofBusinessID,
                                                  Equal<Current<XRBContrHdrExt.usrLineofBusiness>>>>>>,

                              Or<Where<InventoryItemExt.usrLineofBusinessID,
                                           Equal<Current<XRBContrHdrExt.usrLineofBusiness>>,
                                           And<Current<XRBGLDistExt.usrTapeModelID>, IsNull,
                                     Or<InventoryItemExt.usrIsAircraft, Equal<False>,
                                            And<InventoryItemExt.usrLineofBusinessID,
                                                  Equal<Current<XRBContrHdrExt.usrLineofBusiness>>>>>>>>),
                            "Item '{0}' is not in the line of business.", typeof(PX.Objects.IN.InventoryItem.inventoryCD))]
        public virtual void XRBGLDist_InventoryID_CacheAttached(PXCache sender) { }

        [PXDBInt()]
        [PXUIField(DisplayName = "Inventory ID")]
        //[CAMPXRBGLDistInventorySelector]
        [PXSelector(typeof(Search<PX.Objects.IN.InventoryItem.inventoryID,
                            Where<PX.Objects.IN.InventoryItem.itemStatus,
                                Equal<PX.Objects.IN.InventoryItemStatus.active>>,
                            OrderBy<Asc<PX.Objects.IN.InventoryItem.inventoryCD>>>),
                   new Type[]{
                            typeof(PX.Objects.IN.InventoryItem.inventoryCD),
                            typeof(PX.Objects.IN.InventoryItem.descr),
                            typeof(PX.Objects.IN.InventoryItem.itemClassID),
                   typeof(PX.Objects.IN.InventoryItem.itemStatus),
                   typeof(PX.Objects.IN.InventoryItem.basePrice)},
                   DescriptionField = typeof(PX.Objects.IN.InventoryItem.inventoryCD))]

        [PXRestrictor(typeof(Where2<Where<InventoryItemExt.usrLineofBusinessID,
                                           Equal<Current<XRBContrHdrExt.usrLineofBusiness>>,
                                           And<InventoryItemExt.usrTapeModelID,
                                                Equal<Current<XRBGLDistExt.usrTapeModelID>>,
                                    Or<InventoryItemExt.usrIsAircraft, Equal<False>,
                                            And<InventoryItemExt.usrLineofBusinessID,
                                                  Equal<Current<XRBContrHdrExt.usrLineofBusiness>>>>>>,

                              Or<Where<InventoryItemExt.usrLineofBusinessID,
                                           Equal<Current<XRBContrHdrExt.usrLineofBusiness>>,
                                           And<Current<XRBGLDistExt.usrTapeModelID>, IsNull,
                                     Or<InventoryItemExt.usrIsAircraft, Equal<False>,
                                            And<InventoryItemExt.usrLineofBusinessID,
                                                  Equal<Current<XRBContrHdrExt.usrLineofBusiness>>>>>>>>),
                            "Item '{0}' is not in the line of business.", typeof(PX.Objects.IN.InventoryItem.inventoryCD))]
        public virtual void AutoGen_InventoryID_CacheAttached(PXCache sender) { }






        public PXAction<XRBContrHdr> ProFormaInvoice;
        [PXUIField(DisplayName = "Pro-Forma Invoice", MapEnableRights = PXCacheRights.Select,
            MapViewRights = PXCacheRights.Select, Visible = false)]
        [PXButton(ImageKey = PX.Web.UI.Sprite.Main.Report)]
        public virtual IEnumerable proFormaInvoice(PXAdapter adapter)
        {
            if (Base.XRBContrHdrRecords.Current != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                parameters["FinPeriodID"] = Base.ContractDetails.Current.PerPost;
                parameters["ContractID"] = Convert.ToString(Base.ContractDetails.Current.ContractID);
                //parameters["LineNbr"] = Convert.ToString(Base.ContractDetails.Current.LineNbr);

                throw new PXReportRequiredException(parameters, "XQ900006",
                                                    PXBaseRedirectException.WindowMode.New, "Pro-Forma Invoice");
            }
            return adapter.Get();
        }

        #endregion



        /* Events */

        protected void XRBContrHdr_RowSelected(PXCache sender, PXRowSelectedEventArgs e, PXRowSelected del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
            {
                XRBContrHdr doc = e.Row as XRBContrHdr;
                if (doc != null)
                {


                    XRBContrDet current = PXSelect<XRBContrDet,
                        Where<XRBContrDet.manuallyBooked, Equal<False>,
                            And<XRBContrDet.contractID,
                            Equal<Required<XRBContrHdr.contractID>>>>>.Select(Base, doc.ContractID);
                    if (current != null)
                    {
                        var count = PXSelect<XRBGLDist,
                                        Where<XRBGLDist.contractID, Equal<Required<XRBContrDet.contractID>>,
                                           And<XRBGLDist.contrDetLineNbr, Equal<Required<XRBContrDet.lineNbr>>>>>.Select(Base, current.ContractID, current.LineNbr).Count;
                        if (count > 0)
                        {
                            PXUIFieldAttribute.SetEnabled<XRBContrHdrExt.usrLineofBusiness>(sender, doc, false);
                        }
                        else
                        {
                            PXUIFieldAttribute.SetEnabled<XRBContrHdrExt.usrLineofBusiness>(sender, doc, true);
                        }
                    }
                    PXDefaultAttribute.SetPersistingCheck<XRBContrHdr.classID>(sender, doc, PXPersistingCheck.NullOrBlank);
                }
            }
        }

        protected void XRBContrHdr_RowPersisting(PXCache sender, PXRowPersistingEventArgs e, PXRowPersisting del)
        {
            XRBContrHdr row = (XRBContrHdr)e.Row;
            if (row != null)
            {
                if (String.IsNullOrEmpty(row.ClassID))
                {
                    throw new PXSetPropertyException<XRBContrHdr.classID>(
                                "Enter a Contract Class.", PXErrorLevel.Error);
                }
                XRBContrHdrExt contractEX = PXCache<XRBContrHdr>.GetExtension<XRBContrHdrExt>(row);
                if (contractEX != null)
                {
                    if (contractEX.UsrContractTypeID == null)
                        throw new PXSetPropertyException<XRBContrHdrExt.usrContractTypeID>(
                                    "Enter a Contract Type.", PXErrorLevel.Error);
                    if (contractEX.UsrLineofBusiness == null)
                        throw new PXSetPropertyException<XRBContrHdrExt.usrLineofBusiness>(
                                    "Enter a Line of Business.", PXErrorLevel.Error);
                }


            }
            del(sender, e);
        }



        protected virtual void XRBContrHdr_ContractType_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e, PXFieldDefaulting del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(cache, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
            {
                e.NewValue = ContractTypeList.PerpetualKey;
            }
        }



        protected virtual void XRBContrHdr_TotPeriods_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e, PXFieldUpdated del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (!e.ExternalCall) return;
            if (e.Row != null)
            {
                foreach (XRBContrDet item in Base.ContractDetails.Select())
                {
                    if (!(bool)item.ManuallyBooked)
                    {
                        Base.ContractDetails.Current = item;
                        foreach (XRBGLDist lineitem in PXSelect<XRBGLDist, Where<XRBGLDist.contractID,
                            Equal<Required<XRBContrDet.contractID>>,
                            And<XRBGLDist.contrDetLineNbr,
                            Equal<Required<XRBContrDet.lineNbr>>>>>.Select(Base, item.ContractID, item.LineNbr))
                        {
                            Base.Distributions.Current = lineitem;
                            Base.Distributions.Cache.Update(Base.Distributions.Current);
                        }

                    }
                }

            }
        }



        protected void XRBGLDist_RowSelected(PXCache sender, PXRowSelectedEventArgs e, PXRowSelected del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
            {
                XRBGLDist row = (XRBGLDist)e.Row;
                PXUIFieldAttribute.SetEnabled<XRBGLDistExt.usrDiscountReasonID>(sender, e.Row, (bool)row.ManualDisc);
                PXUIFieldAttribute.SetEnabled<XRBGLDist.discountID>(sender, e.Row, false);
            }
        }




        protected virtual void XRBGLDist_ManualDisc_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            XRBGLDist row = (XRBGLDist)e.Row;
            if (row != null)
            {
                if (!(bool)row.ManualDisc)
                {
                    XRBGLDistExt rowext = PXCache<XRBGLDist>.GetExtension<XRBGLDistExt>(row);
                    if (rowext != null)
                    {
                        rowext.UsrDiscountReasonID = null;
                    }
                }
            }
        }

        protected virtual void XRBGLDist_UsrDiscountReasonID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            XRBGLDist row = (XRBGLDist)e.Row;
            if (row != null)
            {
                if (!(row.DiscPct > (decimal?)0.0))
                {
                    PXCache hCache = Base.Distributions.Cache;
                    XRBGLDist hdr = Base.Distributions.Current;
                    CAMPDiscountReasonCode rc = (CAMPDiscountReasonCode)PXSelectorAttribute.Select<XRBGLDistExt.usrDiscountReasonID>(hCache, hdr);
                    if (rc != null)
                    {
                        row.DiscPct = rc.DefaultPct;
                    }
                }
            }
        }

        protected virtual void XRBGLDist_DiscPct_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e, PXFieldVerifying del)
        {
            XRBGLDist row = (XRBGLDist)e.Row;
            // Event handler body before the base BLC event handler collection 
            if (row != null)
            {
                XRBGLDistExt distEx = PXCache<XRBGLDist>.GetExtension<XRBGLDistExt>(row);
                if (distEx != null && distEx.UsrDiscountReasonID > 0)
                {
                    PXCache hCache = Base.Distributions.Cache;
                    XRBGLDist hdr = Base.Distributions.Current;
                    CAMPDiscountReasonCode rc = (CAMPDiscountReasonCode)PXSelectorAttribute.Select<XRBGLDistExt.usrDiscountReasonID>(hCache, hdr);
                    if (rc != null)
                    {
                        if ((bool)rc.AllowChanges)
                        {
                            if ((bool)rc.AllowChanges && !(bool)rc.AllowOverMax)
                            {
                                if ((decimal)e.NewValue > rc.MaxPct)
                                {
                                    throw new PXSetPropertyException<XRBGLDist.discPct>(
                                  "discount % greater then allowed Max % for discount reason code.", PXErrorLevel.Error);
                                }
                            }
                        }
                        else
                        {
                            throw new PXSetPropertyException<XRBGLDist.discPct>(
                                "Cannot make changes to discount %", PXErrorLevel.Error);
                        }
                    }
                }
            }
            // Event handler body after the base BLC event handler collection
            if (del != null)
                del(sender, e);
        }




    }
}