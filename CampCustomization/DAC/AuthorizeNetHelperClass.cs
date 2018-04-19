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
    public partial class ECheckSettings : IBqlTable
    {
        #region BankABACode
        public abstract class bankABACode : PX.Data.IBqlField
        {
        }
        [PXString(9, IsUnicode = true)]
        [PXUIField(DisplayName = "ABA Code", Required = true)]
        public virtual string BankABACode { get; set; }
        #endregion
        #region BankAccountNumber
        public abstract class bankAccountNumber : PX.Data.IBqlField
        {
        }
        [PXString(20, IsUnicode = true)]
        [PXUIField(DisplayName = "Account Number", Required=true)]
        public virtual string BankAccountNumber { get; set; }
        #endregion
        #region BankAccountType
        public abstract class bankAccountType : PX.Data.IBqlField
        {
        }
        [PXString(1, IsUnicode = true)]
        //[PXDefault(LockboxBankAccountType.Checking)]
        [PXUIField(DisplayName = "Account Type", Required=true)]
        [LockboxBankAccountType.List()]
        public virtual string BankAccountType { get; set; }
        #endregion

        #region BankName
        public abstract class bankName : PX.Data.IBqlField
        {
        }
        [PXString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Bank Name", Required = true)]
        public virtual string BankName { get; set; }
        #endregion
        #region AccountName
        public abstract class accountName : PX.Data.IBqlField
        {
        }
        [PXString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Account Name", Required = true)]
        public virtual string AccountName { get; set; }
        #endregion
        #region BankCheckNumber
        public abstract class bankCheckNumber : PX.Data.IBqlField
        {
        }
        [PXString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Check Number", Required = true)]
        public virtual string BankCheckNumber { get; set; }
        #endregion

        #region TransactionID
        public abstract class transactionID : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "TransactionID")]
        public virtual string TransactionID { get; set; }
        #endregion
        #region GatewayMessage
        public abstract class gatewayMessage : PX.Data.IBqlField
        {
        }
        [PXString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Gateway Message", IsReadOnly = true)]
        public virtual string GatewayMessage { get; set; }
        #endregion
        
        #region InvoiceNbr
        public abstract class invoiceNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "InvoiceNbr", IsReadOnly=true)]
        public virtual string InvoiceNbr { get; set; }
        #endregion
        #region CheckAmt
        public abstract class checkAmt : PX.Data.IBqlField
        {
        }
        [PXDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Check Amount", IsReadOnly=true)]
        public virtual decimal? CheckAmt { get; set; }
        #endregion
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }
        [PXInt()]
        [PXUIField(DisplayName = "Customer", IsReadOnly=true)]
        [PXSelector(typeof(PX.Objects.AR.Customer.bAccountID),
                new Type[]{
                typeof(PX.Objects.AR.Customer.acctCD),
                typeof(PX.Objects.AR.Customer.acctName)},
          SubstituteKey = typeof(PX.Objects.AR.Customer.acctCD),
          DescriptionField = typeof(PX.Objects.AR.Customer.acctName))]
        public virtual int? CustomerID { get; set; }
        #endregion
        #region EMail
        public abstract class eMail : PX.Data.IBqlField
        {
        }
        [PXDBEmail]
        [PXUIField(DisplayName = "Email", Visibility = PXUIVisibility.SelectorVisible)]
        [PX.Objects.CR.MassProcess.PXMassMergableField]
        public virtual string EMail { get; set; }
        #endregion
    
        /*
        #region CheckDate
        public abstract class checkDate : PX.Data.IBqlField
        {
        }
        [PXDate()]
        [PXUIField(DisplayName = "CheckDate", IsReadOnly = true)]
        public virtual DateTime? CheckDate { get; set; }
        #endregion
       
 
        #region TransactionType
        public abstract class transactionType : PX.Data.IBqlField
        {
        }
        [PXString(1, IsUnicode = true)]
        [PXUIField(DisplayName = "TransactionType", IsReadOnly = true)]
        public virtual string TransactionType { get; set; }
        #endregion
         */ 

    }

    [System.SerializableAttribute()]
    public partial class CreditCardSettings : IBqlTable
    {
        #region CardNumber
        public abstract class cardNumber : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Card Number", Required = true)]
        public virtual string CardNumber { get; set; }
        #endregion

        #region ExpirationMonthandYear
        public abstract class expirationMonthandYear : PX.Data.IBqlField
        {
        }
        [PXString(4, IsUnicode = true)]
        [PXUIField(DisplayName = "Expiration Month and Year", Required = true)]
        public virtual string ExpirationMonthandYear { get; set; }
        #endregion

        #region Description
        public abstract class description : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Description", Required = true)]
        public virtual string Description { get; set; }
        #endregion

        #region CardCode
        public abstract class cardCode : PX.Data.IBqlField
        {
        }
        [PXString(4, IsUnicode = true)]
        [PXUIField(DisplayName = "Card Code")]
        public virtual string CardCode { get; set; }
        #endregion

        #region BankAccountType
        public abstract class bankAccountType : PX.Data.IBqlField
        {
        }
        [PXString(1, IsUnicode = true)]
        //[PXDefault(LockboxBankAccountType.Checking)]
        [PXUIField(DisplayName = "Account Type", Required = true)]
        [LockboxBankAccountType.List()]
        public virtual string BankAccountType { get; set; }
        #endregion
        #region TransactionID
        public abstract class transactionID : PX.Data.IBqlField
        {
        }
        [PXString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "TransactionID")]
        public virtual string TransactionID { get; set; }
        #endregion
        #region GatewayMessage
        public abstract class gatewayMessage : PX.Data.IBqlField
        {
        }
        [PXString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Gateway Message", IsReadOnly = true)]
        public virtual string GatewayMessage { get; set; }
        #endregion

        #region InvoiceNbr
        public abstract class invoiceNbr : PX.Data.IBqlField
        {
        }
        [PXString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "InvoiceNbr", IsReadOnly = true)]
        public virtual string InvoiceNbr { get; set; }
        #endregion

        #region Amount
        public abstract class amount : PX.Data.IBqlField
        {
        }
        [PXDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Amount", IsReadOnly = true)]
        public virtual decimal? Amount { get; set; }
        #endregion
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }
        [PXInt()]
        [PXUIField(DisplayName = "Customer", IsReadOnly = true)]
        [PXSelector(typeof(PX.Objects.AR.Customer.bAccountID),
                new Type[]{
                typeof(PX.Objects.AR.Customer.acctCD),
                typeof(PX.Objects.AR.Customer.acctName)},
          SubstituteKey = typeof(PX.Objects.AR.Customer.acctCD),
          DescriptionField = typeof(PX.Objects.AR.Customer.acctName))]
        public virtual int? CustomerID { get; set; }
        #endregion
        #region EMail
        public abstract class eMail : PX.Data.IBqlField
        {
        }
        [PXDBEmail]
        [PXUIField(DisplayName = "Email", Visibility = PXUIVisibility.SelectorVisible)]
        [PX.Objects.CR.MassProcess.PXMassMergableField]
        public virtual string EMail { get; set; }
        #endregion
    }

    public abstract class LockboxBankAccountType : PX.Data.IBqlField
    {
        //Constant declaration
        public const string Checking = "C"; 
        public const string Savings = "S";
        public const string BusinessChecking = "B";
        
        //List Attribute
        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute()
                : base(
                new string[] { Checking, Savings, BusinessChecking },
                new string[] { "Checking", "Savings", "Business Checking" }) { ;}
        }
        //BQL constant declaration
        public class checking : Constant<string>
        {
            public checking() : base(Checking) { ;}
        }

        public class savings : Constant<string>
        {
            public savings() : base(Savings) { ;}
        }

        public class businessChecking : Constant<string>
        {
            public businessChecking() : base(BusinessChecking) { ;}
        }
    }

}
