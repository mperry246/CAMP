using MaxQ.Products.RBRR;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;

namespace CAMPCustomization
{





    [System.SerializableAttribute()]
    public abstract class CollectionStatus : PX.Data.IBqlField
    {
        public const string WaitingExternal = "E";
        public const string CollectionAgency = "C";
        public const string PromiseToPay = "P";
        public const string CreditHold = "H";
        public const string PendingCreditHold = "A";
        public const string WaitingInternal = "W";
        public const string IncorrectContact = "I";
        public const string Reactivated = "R";

        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute()
                : base(new String[] { WaitingExternal, CollectionAgency, PromiseToPay, CreditHold, PendingCreditHold, WaitingInternal, IncorrectContact, Reactivated },
                        new String[] { "Waiting External", "Collection Agency", "Promise To Pay", "Credit Hold", "Pending Credit Hold", "Waiting Internal", "Incorrect Contact", "Reactivated" }
                      )
            {; }
        }
        //BQL constant declaration
        public class collectionAgency : Constant<string>
        {
            public collectionAgency() : base(CollectionAgency) {; }
        }
        public class promiseToPay : Constant<string>
        {
            public promiseToPay() : base(PromiseToPay) {; }
        }
        public class creditHold : Constant<string>
        {
            public creditHold() : base(CreditHold) {; }
        }
        public class pendingCreditHold : Constant<string>
        {
            public pendingCreditHold() : base(PendingCreditHold) {; }
        }
        public class waitingInternal : Constant<string>
        {
            public waitingInternal() : base(WaitingInternal) {; }
        }
        public class incorrectContact : Constant<string>
        {
            public incorrectContact() : base(IncorrectContact) {; }
        }
        public class reactivated : Constant<string>
        {
            public reactivated() : base(Reactivated) {; }
        }
    }
    [System.SerializableAttribute()]
    public partial class CreditInvoiceSettings : IBqlTable
    {
        #region CreditDate

        public abstract class creditDate : PX.Data.IBqlField
        {
        }
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXDate()]
        [PXUIField(DisplayName = "Credit Date", Required = true)]
        public virtual DateTime? CreditDate { get; set; }

        #endregion

        #region ReasonCodeID

        public abstract class reasonCodeID : PX.Data.IBqlField
        {
            public const int Length = 20;
        }
        [PXDefault()]
        [PXString(reasonCodeID.Length, InputMask = ">aaaaaaaaaaaaaaaaaaaa")]
        [PXUIField(DisplayName = "Reason Code", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
        [PXSelector(typeof(Search<PX.Objects.CS.ReasonCode.reasonCodeID>))]
        [PXRestrictor(typeof(Where<ReasonCodeExt.usrActive, Equal<True>>),
              "Reason '{0}' is inactive", typeof(PX.Objects.CS.ReasonCode.reasonCodeID))]
        public virtual String ReasonCodeID { get; set; }

        #endregion
    }

    [System.SerializableAttribute()]
    public partial class CancelContractSettings : IBqlTable
    {
        #region CancellationDate

        public abstract class cancellationDate : PX.Data.IBqlField
        {
        }
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXDate()]
        [PXUIField(DisplayName = "Cancellation Date", Required = true)]
        public virtual DateTime? CancellationDate { get; set; }

        #endregion

        #region ReasonCD
        public abstract class reasonCD : PX.Data.IBqlField { }
        [PXString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Reason", Required = true)]
        [PXSelector(typeof(XRBCancelReasonCd.code),
            DescriptionField = typeof(XRBCancelReasonCd.descr))]
        [PXRestrictor(typeof(Where<XRBCancelReasonCdExt.usrActive, Equal<True>>),
           "Reason '{0}' is inactive", typeof(XRBCancelReasonCd.code))]
        public virtual string ReasonCD { get; set; }
        #endregion
    }

    public class CAMPXRBGLDistInventorySelector : PX.Data.PXCustomSelectorAttribute
    {
        public CAMPXRBGLDistInventorySelector()
            : base(typeof(PX.Objects.IN.InventoryItem.inventoryID))
        {
            DescriptionField = typeof(PX.Objects.IN.InventoryItem.descr);
            SubstituteKey = typeof(PX.Objects.IN.InventoryItem.inventoryCD);
        }
        public IEnumerable GetRecords()
        {
            XRBContrHdr contract = (XRBContrHdr)PXSelect<XRBContrHdr,
                                                    Where<XRBContrHdr.contractID,
                                    Equal<Current<XRBContrHdr.contractID>>>>.Select(_Graph);

            MaxQ.Products.RBRR.XRBGLDist dist = (MaxQ.Products.RBRR.XRBGLDist)PXSelect<MaxQ.Products.RBRR.XRBGLDist,
                                                    Where<MaxQ.Products.RBRR.XRBGLDist.contractID,
                                    Equal<Current<XRBContrHdr.contractID>>>>.Select(_Graph);

            if (contract != null)
            {
                var contracthdrEx = PXCache<MaxQ.Products.RBRR.XRBGLDist>.GetExtension<XRBGLDistExt>(dist);
                if (contracthdrEx.UsrProfileNumber != null)
                {
                    return PXSelect<PX.Objects.IN.InventoryItem,
                                        Where<InventoryItemExt.usrLineofBusinessID,
                                           Equal<Current<XRBContrHdrExt.usrLineofBusiness>>,
                                           And<InventoryItemExt.usrTapeModelID,
                                                Equal<Current<XRBGLDistExt.usrTapeModelID>>,
                                                Or<InventoryItemExt.usrIsAircraft, Equal<False>,
                                                    And<InventoryItemExt.usrLineofBusinessID,
                                                        Equal<Current<XRBContrHdrExt.usrLineofBusiness>>>>>>>.Select(_Graph);
                }
                else
                {
                    return PXSelect<PX.Objects.IN.InventoryItem,
                                        Where<InventoryItemExt.usrLineofBusinessID,
                                           Equal<Current<XRBContrHdrExt.usrLineofBusiness>>>>.Select(_Graph);
                }
            }
            else
            {
                return PXSelect<PX.Objects.IN.InventoryItem>.Select(_Graph);
            }
        }

    }

    [PXDBInt()]
    [PXUIField(DisplayName = "Inventory ID", Visibility = PXUIVisibility.Visible)]
    public class CampNonStockItemAttribute : PX.Objects.IN.InventoryAttribute
    {
        public CampNonStockItemAttribute()
            : base(typeof(Where<InventoryItem.stkItem, Equal<boolFalse>,
                            And<InventoryItemExt.usrLineofBusinessID,
                                           Equal<Current<ARRegisterExt.usrLineofBusiness>>,
                                           And<InventoryItemExt.usrTapeModelID,
                                                Equal<Current<ARTranExt.usrTapeModelID>>,
                                                Or<InventoryItemExt.usrIsAircraft, Equal<False>>>>>))
        {

        }

       


    }


    public abstract class NVLockboxTransactionType : PX.Data.IBqlField
    {
        public const string ECheck = "EC";
        public const string CreditCard = "CC";
        public const string Lockbox = "LB";

        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute()
                : base(new String[] { ECheck, CreditCard, Lockbox },
                        new String[] { "eCheck", "Credit Card", "Lockbox" }
                      )
            {; }
        }
        //BQL constant declaration
        public class eCheck : Constant<string>
        {
            public eCheck() : base(ECheck) {; }
        }
        public class creditCard : Constant<string>
        {
            public creditCard() : base(CreditCard) {; }
        }
        public class lockbox : Constant<string>
        {
            public lockbox() : base(Lockbox) {; }
        }

    }

    public abstract class NVLockboxStatus : PX.Data.IBqlField
    {
        public const string Pending = "P";
        public const string Authorized = "A";
        public const string Completed = "C";

        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute()
                : base(new String[] { Pending, Authorized, Completed },
                        new String[] { "Pending", "Authorized", "Completed" }
                      )
            {; }
        }
        //BQL constant declaration
        public class authorized : Constant<string>
        {
            public authorized() : base(Authorized) {; }
        }
        public class completed : Constant<string>
        {
            public completed() : base(Completed) {; }
        }
        public class pending : Constant<string>
        {
            public pending() : base(Pending) {; }
        }

    }



}