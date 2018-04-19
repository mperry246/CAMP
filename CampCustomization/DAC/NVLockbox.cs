using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PX.SM;
using PX.Data;
using CAMPCustomization;

namespace NV.Lockbox
{
    [System.SerializableAttribute()]
    public class NVLockbox : PX.Data.IBqlTable
    {
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }
        [PX.Objects.GL.Branch(typeof(Search<PX.Objects.GL.Branch.branchID,
            Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>), IsDetail = false)]
        [PXUIField(DisplayName = "Branch", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Branch.branchID), SubstituteKey = typeof(Branch.branchCD), DescriptionField = typeof(Branch.branchCD))]
        public virtual int? BranchID { get; set; }

        #endregion
        #region LockboxID
        public abstract class lockboxID : PX.Data.IBqlField
        {
        }
        [PXDBIdentity()]
        [PXUIField(DisplayName = "LockboxID", Visibility = PXUIVisibility.Invisible, Visible = false)]
        public virtual int? LockboxID { get; set; }
        #endregion

        #region LockboxCD
        public abstract class lockboxCD : PX.Data.IBqlField
        {
        }
        [PXDBString(20, IsKey = true, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Lockbox")]
        public virtual string LockboxCD { get; set; }
        #endregion
        #region Description
        public abstract class description : PX.Data.IBqlField
        {
        }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual string Description { get; set; }

        #endregion

        #region CashAccountID
        public abstract class cashAccountID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "Cash Account")]
        [PXSelector(typeof(Search2<PX.Objects.CA.CashAccount.cashAccountID,
            InnerJoin<PX.Objects.CA.PaymentMethodAccount, 
                On<PX.Objects.CA.PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>,
            InnerJoin<NVLockboxSetup, 
                On<NVLockboxSetup.paymentMethodID, Equal<PX.Objects.CA.PaymentMethodAccount.paymentMethodID>>>>,
            Where<PX.Objects.CA.PaymentMethodAccount.useForAR,Equal<True>>>),
                new Type[]{
                typeof(PX.Objects.CA.CashAccount.cashAccountCD),
                typeof(PX.Objects.CA.CashAccount.descr)},
           DescriptionField = typeof(PX.Objects.CA.CashAccount.cashAccountCD))]
        public virtual int? CashAccountID { get; set; }

        #endregion

        #region GatewayAPILogin
        public abstract class gatewayAPILogin : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Gateway API Login")]
        public virtual string GatewayAPILogin { get; set; }
        #endregion

        #region GatewayTransactionKey
        public abstract class gatewayTransactionKey : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Gateway Transaction Key")]
        public virtual string GatewayTransactionKey { get; set; }
        #endregion

        #region InTestMode
        public abstract class inTestMode : PX.Data.IBqlField
        {
        }
        [PXDBBool()]
        [PXDefault(true)]
        [PXUIField(DisplayName = "In Test Mode")]
        public virtual bool? InTestMode { get; set; }
        #endregion

        #region PaymentMethodID
        public abstract class paymentMethodID : PX.Data.IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Payment Method")]
        [PXSelector(typeof(Search<PX.Objects.CA.PaymentMethod.paymentMethodID,
            Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>,
                And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>>>>),
                new Type[]{
                typeof(PX.Objects.CA.PaymentMethod.paymentMethodID),
                typeof(PX.Objects.CA.PaymentMethod.descr)},
           DescriptionField = typeof(PX.Objects.CA.PaymentMethod.paymentMethodID))]
        public virtual string PaymentMethodID { get; set; }

        #endregion


        #region DefaultGateway
        public abstract class defaultGateway : PX.Data.IBqlField
        {
        }
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Default Gateway")]
        public virtual bool? DefaultGateway { get; set; }
        #endregion

        #region LineofBusiness
        public abstract class lineofBusiness : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "Line of Business")]
        [PXSelector(typeof(CAMPLineofBusiness.lineofBusinessID),
            new Type[]{
                typeof(CAMPLineofBusiness.lineofBusinessCD),
                typeof(CAMPLineofBusiness.description),
                typeof(CAMPLineofBusiness.defaultSubaccount)},
            SubstituteKey = typeof(CAMPLineofBusiness.lineofBusinessCD),
            DescriptionField = typeof(CAMPLineofBusiness.description))]
        public virtual int? LineofBusiness { get; set; }
        #endregion

        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }
        [PXDBTimestamp()]
        public virtual byte[] tstamp { get; set; }

        #endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }
        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID { get; set; }

        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID { get; set; }

        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }
        [PXDBCreatedDateTime()]
        public virtual DateTime? CreatedDateTime { get; set; }

        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID { get; set; }

        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID { get; set; }

        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }
        [PXDBLastModifiedDateTime()]
        public virtual DateTime? LastModifiedDateTime { get; set; }

        #endregion
    }
}
