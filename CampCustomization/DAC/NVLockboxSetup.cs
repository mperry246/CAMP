using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PX.SM;
using PX.Data;

namespace NV.Lockbox
{
    [System.SerializableAttribute()]
    [PXPrimaryGraph(typeof(NVLockboxSetupMaint))]
    public class NVLockboxSetup : PX.Data.IBqlTable
    {
        #region DefaultCustomerID
        public abstract class defaultCustomerID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "Default Customer")]
        [PXSelector(typeof(PX.Objects.AR.Customer.bAccountID),
                new Type[]{
                typeof(PX.Objects.AR.Customer.acctCD),
                typeof(PX.Objects.AR.Customer.acctName)},
          SubstituteKey = typeof(PX.Objects.AR.Customer.acctCD),
          DescriptionField = typeof(PX.Objects.AR.Customer.acctName))]
        public virtual int? DefaultCustomerID { get; set; }

        #endregion

        #region PaymentMethodID
        public abstract class paymentMethodID : PX.Data.IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Payment Method")]
        [PXSelector(typeof(Search<PX.Objects.CA.PaymentMethod.paymentMethodID,
            Where<PX.Objects.CA.PaymentMethod.isActive,Equal<True>,
                And<PX.Objects.CA.PaymentMethod.useForAR,Equal<True>>>>),
                new Type[]{
                typeof(PX.Objects.CA.PaymentMethod.paymentMethodID),
                typeof(PX.Objects.CA.PaymentMethod.descr)},
           DescriptionField = typeof(PX.Objects.CA.PaymentMethod.paymentMethodID))]
        public virtual string PaymentMethodID { get; set; }

        #endregion

        #region LastRefNbr
        public abstract class lastRefNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Lockbox RefNbr")]
        [PXSelector(typeof(PX.Objects.CS.Numbering.numberingID))]
        public virtual string LastRefNbr { get; set; }
        #endregion

        #region CollectionRefNbr
        public abstract class collectionRefNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Collection RefNbr")]
        [PXSelector(typeof(PX.Objects.CS.Numbering.numberingID))]
        public virtual string CollectionRefNbr { get; set; }
        #endregion

        #region CreditRequestRefNbr
        public abstract class creditRequestRefNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "CreditRequest RefNbr")]
        [PXSelector(typeof(PX.Objects.CS.Numbering.numberingID))]
        public virtual string CreditRequestRefNbr { get; set; }
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

        #region Version
        public abstract class version : PX.Data.IBqlField
        {
        }
        [PXString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Version", IsReadOnly = true)]
        public virtual string Version
        {
            get
            {
                return (String)System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        #endregion

    }
}
