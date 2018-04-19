using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using AuthorizeNet;

namespace NV.Lockbox
{
    [System.SerializableAttribute()]
    public partial class AuthorizeBatch: IBqlTable
    {
        #region BatchId
        public abstract class batchId : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "BatchId")]
        public virtual string BatchId { get; set; }
        #endregion

        #region SettlementState
        public abstract class settlementState : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "SettlementState")]
        public virtual string SettlementState { get; set; }
        #endregion

        #region MarketType
        public abstract class marketType : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "MarketType")]
        public virtual string MarketType { get; set; }
        #endregion

        #region Product
        public abstract class product : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Product")]
        public virtual string Product { get; set; }
        #endregion

        #region SettlementTimeUTC
        public abstract class settlementTimeUTC : PX.Data.IBqlField
        {
        }
        [PXDateAndTime()]
        [PXUIField(DisplayName = "SettlementTimeUTC")]
        public virtual DateTime? SettlementTimeUTC { get; set; }
        #endregion

        #region SettlementTimeLocal
        public abstract class settlementTimeLocal : PX.Data.IBqlField
        {
        }
        [PXDateAndTime()]
        [PXUIField(DisplayName = "SettlementTimeLocal")]
        public virtual DateTime? SettlementTimeLocal { get; set; }
        #endregion
        
    }

    [System.SerializableAttribute()]
    public partial class AuthorizeTransaction : IBqlTable
    {
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "CustomerID")]
        public virtual string CustomerID { get; set; }
        #endregion 

        #region AuthorizationAmt
        public abstract class authorizationAmt : PX.Data.IBqlField
        {
        }
        [PXDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Authorization Amount")]
        public virtual decimal? AuthorizationAmt { get; set; }
        #endregion

        #region BatchSettledOn
        public abstract class batchSettledOn : PX.Data.IBqlField
        {
        }
        [PXDateAndTime()]
        [PXUIField(DisplayName = "BatchSettledOn")]
        public virtual DateTime? BatchSettledOn { get; set; }
        #endregion

        #region CardNumber
        public abstract class cardNumber : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Card Number")]
        public virtual string CardNumber { get; set; }
        #endregion 

        #region CardType
        public abstract class cardType : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Card Type")]
        public virtual string CardType { get; set; }
        #endregion

        #region DateSubmitted
        public abstract class dateSubmitted : PX.Data.IBqlField
        {
        }
        [PXDateAndTime()]
        [PXUIField(DisplayName = "Date Submitted")]
        public virtual DateTime? DateSubmitted { get; set; }
        #endregion

        #region InvoiceNumber
        public abstract class invoiceNumber : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Invoice Number")]
        public virtual string InvoiceNumber { get; set; }
        #endregion

        #region MarketType
        public abstract class marketType : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Market Type")]
        public virtual string MarketType { get; set; }
        #endregion

        #region SettledAmt
        public abstract class settledAmt : PX.Data.IBqlField
        {
        }
        [PXDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Settled Amount")]
        public virtual decimal? SettledAmt { get; set; }
        #endregion

        #region Status
        public abstract class status : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Status")]
        public virtual string Status { get; set; }
        #endregion

        #region TransactionID
        public abstract class transactionID : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "TransactionID")]
        public virtual string TransactionID { get; set; }
        #endregion
    }
}
