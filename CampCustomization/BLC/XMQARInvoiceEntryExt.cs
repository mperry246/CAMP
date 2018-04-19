using PX.Data;
using PX.Objects.AR;

namespace CAMPCustomization
{
    public class XMQARInvoiceEntryExtonExt : PXGraphExtension<ARInvoiceEntry>
    {

        //Override InventoryID       
        [CampNonStockItemAttribute]
        public virtual void ARTran_InventoryID_CacheAttached(PXCache sender) { }

        protected void ARInvoice_RowSelected(PXCache sender, PXRowSelectedEventArgs e, PXRowSelected del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
            {
                ARInvoice doc = e.Row as ARInvoice;
                if (doc != null)
                {
                    var count = Base.Transactions.Select().Count;
                    if (count > 0)
                    {
                        PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrLineofBusiness>(sender, doc, false);
                    }
                    else
                    {
                        PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrLineofBusiness>(sender, doc, true); 
                    }
                    PXUIFieldAttribute.SetVisible<ARRegisterExt.usrReasonCodeID>(sender, doc, (doc.DocType != ARDocType.Invoice));

                    if ((bool)doc.Voided || (bool)doc.Released)
                    {
                        Base.salesPerTrans.Cache.AllowInsert = false;
                        Base.salesPerTrans.Cache.AllowUpdate = false;
                        Base.salesPerTrans.Cache.AllowDelete = false;
                    }
                    else
                    {
                        Base.salesPerTrans.Cache.AllowInsert = true;
                        Base.salesPerTrans.Cache.AllowUpdate = true;
                        Base.salesPerTrans.Cache.AllowDelete = true;
                    }


                }
            }
        }

        protected void ARInvoice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e, PXRowPersisting del)
        {
            ARInvoice invoice = (ARInvoice)e.Row;
            if (invoice != null)
            {
                ARRegisterExt ext = PXCache<PX.Objects.AR.ARRegister>.GetExtension<ARRegisterExt>((PX.Objects.AR.ARInvoice)e.Row);
                //Validate fields
                if ((e.Operation & PXDBOperation.Command) != PXDBOperation.Delete && ext != null)
                {
                    if (ext.UsrContractTypeID == null)
                        throw new PXSetPropertyException<ARRegisterExt.usrContractTypeID>(
                                    "Enter a Contract Type.", PXErrorLevel.Error);
                    if (ext.UsrLineofBusiness == null)
                        throw new PXSetPropertyException<ARRegisterExt.usrLineofBusiness>(
                                    "Enter a Line of Business.", PXErrorLevel.Error);
                    if (invoice.DocType == ARDocType.DebitMemo ||
                        invoice.DocType == ARDocType.CreditMemo ||
                        invoice.DocType == ARDocType.SmallCreditWO)
                    {
                        if (ext.UsrReasonCodeID == null)
                            throw new PXSetPropertyException<ARRegisterExt.usrReasonCodeID>(
                                        "Enter a Reason Code.", PXErrorLevel.Error);
                    }
                }
                /* Document level discount */
                // Initializing the data view
                PXSelectBase<ARTran> select = new PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>,
                                                        And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>,
                                                        And<ARTran.deferredCode, IsNotNull>>>,
                                                        OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>>(Base);
                // Executing the data view
                PX.Objects.AR.ARTran trandeferredCode = select.SelectWindowed(0, 1, null);
                // Set the defferral code to first line item
                if (trandeferredCode != null)
                {
                    foreach (ARTran tran in Base.Discount_Row.Select())
                    {
                        Base.Discount_Row.Current = tran;
                        Base.Discount_Row.Current.DeferredCode = trandeferredCode.DeferredCode;
                        Base.Discount_Row.Update(Base.Discount_Row.Current);
                    }
                }


            }
            del(sender, e);
        }

        protected virtual void ARInvoice_UsrContractID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e, PXFieldUpdated del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
            {
                ARRegister row = (ARRegister)e.Row;
                ARRegisterExt invoiceEX = PXCache<ARRegister>.GetExtension<ARRegisterExt>(row);
                if (invoiceEX.UsrContractID != null)
                {
                    MaxQ.Products.RBRR.XRBContrHdr contract = PXSelect<MaxQ.Products.RBRR.XRBContrHdr,
                                                                                Where<MaxQ.Products.RBRR.XRBContrHdr.contractID,
                                                                                    Equal<Required<MaxQ.Products.RBRR.XRBContrHdr.contractID>>>>.Select(Base, invoiceEX.UsrContractID);

                    sender.SetValue<ARRegisterExt.usrContractCD>(e.Row, contract.ContractCD);
                    sender.SetValue<ARRegisterExt.usrRevisionNbr>(e.Row, contract.RevisionNbr);
                    sender.SetValue<ARInvoice.invoiceNbr>(e.Row, contract.ContractCDRevNbr);
                }
            }
        }

        protected void ARTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e, PXRowSelected del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
            {
                ARTran row = (ARTran)e.Row;
                PXUIFieldAttribute.SetEnabled<ARTranExt.usrDiscountReasonID>(sender, e.Row, (bool)row.ManualDisc);
                PXUIFieldAttribute.SetEnabled<ARTran.discountID>(sender, e.Row, false);
            }
        }

        protected virtual void ARInvoice_DontPrint_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e, PXFieldDefaulting del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
            {
                if (Base.CurrentDocument.Current != null &&
                    (Base.CurrentDocument.Current.DocType == ARDocType.DebitMemo ||
                     Base.CurrentDocument.Current.DocType == ARDocType.CreditMemo))
                {
                    e.NewValue = true;
                }
            }
        }
        protected virtual void ARInvoice_DontEmail_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e, PXFieldDefaulting del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
                if (e.Row != null)
                {
                    if (Base.CurrentDocument.Current != null &&
                        (Base.CurrentDocument.Current.DocType == ARDocType.DebitMemo ||
                         Base.CurrentDocument.Current.DocType == ARDocType.CreditMemo))
                    {
                        e.NewValue = true;
                    }
                }
        }


        protected virtual void ARTran_ManualDisc_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e, PXFieldUpdated del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
            {
                ARTran row = (ARTran)e.Row;
                if (row != null)
                {
                    if (!(bool)row.ManualDisc)
                    {
                        ARTranExt rowext = PXCache<ARTran>.GetExtension<ARTranExt>(row);
                        if (rowext != null)
                        {
                            rowext.UsrDiscountReasonID = null;
                        }
                    }
                }
            }
        }

        protected virtual void ARTran_UsrDiscountReasonID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            ARTran row = (ARTran)e.Row;
            if (row != null)
            {
                PXCache hCache = Base.Transactions.Cache;
                ARTran hdr = Base.Transactions.Current;
                CAMPDiscountReasonCode rc = (CAMPDiscountReasonCode)PXSelectorAttribute.Select<ARTranExt.usrDiscountReasonID>(hCache, hdr);
                if (rc != null)
                {
                    row.DiscPct = rc.DefaultPct;
                }
            }
        }

       

        protected virtual void ARTran_DiscPct_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e, PXFieldVerifying del)
        {
            ARTran row = (ARTran)e.Row;
            // Event handler body before the base BLC event handler collection 
            if (row != null)
            {
                ARTranExt distEx = PXCache<ARTran>.GetExtension<ARTranExt>(row);
                if (distEx != null && distEx.UsrDiscountReasonID > 0)
                {
                    PXCache hCache = Base.Transactions.Cache;
                    ARTran hdr = Base.Transactions.Current;
                    CAMPDiscountReasonCode rc = (CAMPDiscountReasonCode)PXSelectorAttribute.Select<ARTranExt.usrDiscountReasonID>(hCache, hdr);
                    if (rc != null)
                    {
                        if ((bool)rc.AllowChanges)
                        {
                            if ((bool)rc.AllowChanges && !(bool)rc.AllowOverMax)
                            {
                                if ((decimal)e.NewValue > rc.MaxPct)
                                {
                                    throw new PXSetPropertyException<ARTran.discPct>(
                                  "discount % greater then allowed Max % for discount reason code.", PXErrorLevel.Error);
                                }
                            }
                        }
                        else
                        {
                            throw new PXSetPropertyException<ARTran.discPct>(
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