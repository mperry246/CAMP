using CAMPCustomization;

using PX.Data;
using PX.Objects.IN;
using MaxQ.Products.RBRR;



namespace PX.Objects.AR

{
    public sealed class opend : Constant<string>
    {
        public opend() : base("N") { }
    }
    public class open : PX.Data.Constant<string>
    {
        public open() : base("O") { }
    }
    public class hold : PX.Data.Constant<string>
    {
        public hold() : base("H") { }
    }
    public class elg : PX.Data.Constant<string>
    {
        public elg() : base("elg") { }
    }

    public class CustomerMaint_Extension : PXGraphExtension<CustomerMaint>
    {




        #region CustomerDocuments DAC




        [PXFilterable()]
        public PXSelectJoin<ARRegister,
                   LeftJoin<ARInvoice, On<ARInvoice.refNbr, Equal<ARRegister.refNbr>,
                                       And<ARInvoice.docType, Equal<ARRegister.docType>>>,
                   LeftJoin<CR.BAccount, On<CR.BAccount.bAccountID, Equal<ARRegister.customerID>>,
                   LeftJoin<Customer, On<Customer.bAccountID, Equal<CR.BAccount.bAccountID>>,
                   LeftJoin<CAMPSingleProfileDocuments, On<CAMPSingleProfileDocuments.refNbr, Equal<ARRegister.refNbr>>>>>>,
                   Where<ARRegister.openDoc, Equal<True>,
                                   And<ARRegister.released, Equal<True>,
                                   And<ARRegister.customerID, Equal<Current<Customer.bAccountID>>>>>> CustomerDocuments;







        #endregion


        #region CustomerCurrentProducts DAC


        public PXSelectJoin<Customer,
         InnerJoin<XRBContrHdr,
             On<Customer.bAccountID, Equal<XRBContrHdr.customerID>>,
         LeftJoin<XRBContrDet,
             On<XRBContrHdr.contractID, Equal<XRBContrDet.contractID>>,
         InnerJoin<XRBGLDist,
             On<XRBContrDet.contractID, Equal<XRBGLDist.contractID>,
                 And<XRBContrDet.lineNbr, Equal<XRBGLDist.contrDetLineNbr>>>,
         LeftJoin<CAMPProfileNumber,
             On<XRBGLDistExt.usrProfileNumber, Equal<CAMPProfileNumber.profileNumberID>>,
         InnerJoin<InventoryItem,
             On<XRBGLDist.inventoryID, Equal<InventoryItem.inventoryID>>,
         InnerJoin<INItemClass,
             On<InventoryItem.itemClassID, Equal<INItemClass.itemClassID>>>>>>>>,
         Where<INItemClass.itemClassCD, NotEqual<elg>,
             And<Customer.bAccountID, Equal<Current<Customer.bAccountID>>,
             And<XRBContrHdr.status, In3<open, hold>,
             And<XRBContrDet.manuallyBooked, Equal<False>>>>>> CustomerCurrentProducts;




        #endregion

    }
}