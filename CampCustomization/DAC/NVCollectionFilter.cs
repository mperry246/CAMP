using System;
using PX.Data;
using PX.Objects.AR;

namespace CAMPCustomization
{
    [System.SerializableAttribute()]
    public partial class NVCollectionFilter : IBqlTable
    {
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField { }
        [PXInt()]
        [PXUIField(DisplayName = "Customer")]
        [PXSelector(typeof(PX.Objects.AR.Customer.bAccountID), new Type[]{
            typeof(PX.Objects.AR.Customer.acctCD),typeof(PX.Objects.AR.Customer.acctName)},
            SubstituteKey = typeof(PX.Objects.AR.Customer.acctCD),
            DescriptionField = typeof(PX.Objects.AR.Customer.acctName))]
        public virtual int? CustomerID { get; set; }
        #endregion

        #region ContactID
        public abstract class contactID : PX.Data.IBqlField { }
        [PXInt()]
        [PXUIField(DisplayName = "ContactID")]
        public virtual int? ContactID { get; set; }
        #endregion

        #region AddressID
        public abstract class addressID : PX.Data.IBqlField { }
        [PXInt()]
        [PXUIField(DisplayName = "AddressID")]
        public virtual int? AddressID { get; set; }
        #endregion  

        #region BalanceAmt
        public abstract class balanceAmt : PX.Data.IBqlField { }
        [PXDecimal(2)]
        [PXUIField(DisplayName = "Amount Due", IsReadOnly = true)]
        public virtual decimal? BalanceAmt { get; set; }
        #endregion

    }
}