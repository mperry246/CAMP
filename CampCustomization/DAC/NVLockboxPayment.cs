using CAMPCustomization;

namespace NV.Lockbox
{
    using System;
    using PX.Data;

    [System.SerializableAttribute()]
    public class NVLockboxPayment : PX.Data.IBqlTable
    {
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }
        protected int? _BranchID;
        [PX.Objects.GL.Branch(typeof(Search<PX.Objects.GL.Branch.branchID,
            Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>), IsDetail = false)]
        [PXUIField(DisplayName = "Branch", Visibility = PXUIVisibility.Visible)]

        public virtual int? BranchID
        {
            get
            {
                return this._BranchID;
            }
            set
            {
                this._BranchID = value;
            }
        }
        #endregion
        #region LockboxTranCD
        public abstract class lockboxTranCD : PX.Data.IBqlField
        {
        }
        protected string _LockboxTranCD;
        [PXDBString(10, IsKey = true, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "LockboxCD")]
        [PX.Objects.CS.AutoNumber(typeof(NVLockboxSetup.lastRefNbr), typeof(AccessInfo.businessDate))]
        [PXSelector(typeof(NVLockboxPayment.lockboxTranCD), new Type[]{
            typeof(NVLockboxPayment.lockboxTranCD),
        typeof(NVLockboxPayment.status),
        typeof(NVLockboxPayment.transactionType),
        typeof(NVLockboxPayment.transactionID)})]
        public virtual string LockboxTranCD
        {
            get
            {
                return this._LockboxTranCD;
            }
            set
            {
                this._LockboxTranCD = value;
            }
        }
        #endregion
        #region TransactionType
        public abstract class transactionType : PX.Data.IBqlField
        {
        }
        protected string _TransactionType;
        [PXDBString(2, IsFixed = true)]
        [PXDefault(NVLockboxTransactionType.ECheck)]
        [PXUIField(DisplayName = "Type", IsReadOnly = true)]
        [NVLockboxTransactionType.List()]
        public virtual string TransactionType
        {
            get
            {
                return this._TransactionType;
            }
            set
            {
                this._TransactionType = value;
            }
        }
        #endregion
        #region Status
        public abstract class status : PX.Data.IBqlField
        {
        }
        [PXDBString(1, IsFixed = true)]
        [PXDefault(NVLockboxStatus.Pending)]
        [PXUIField(DisplayName = "Status", IsReadOnly = true)]
        [NVLockboxStatus.List()]
        public virtual string Status { get; set; }
        #endregion

        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }
        protected int? _CustomerID;
        [PXDBInt()]
        [PXUIField(DisplayName = "Customer", IsReadOnly = true)]
        [PXSelector(typeof(PX.Objects.AR.Customer.bAccountID), new Type[]{
            typeof(PX.Objects.AR.Customer.acctCD),typeof(PX.Objects.AR.Customer.acctName)},
            SubstituteKey = typeof(PX.Objects.AR.Customer.acctCD),
            DescriptionField = typeof(PX.Objects.AR.Customer.acctName))]
        public virtual int? CustomerID
        {
            get
            {
                return this._CustomerID;
            }
            set
            {
                this._CustomerID = value;
            }
        }
        #endregion

        #region GatewayID
        public abstract class gatewayID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "GatewayID", IsReadOnly = true)]
        public virtual int? GatewayID { get; set; }
        #endregion

        #region PaymentMethodID
        public abstract class paymentMethodID : PX.Data.IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Payment Method")]
        public virtual string PaymentMethodID { get; set; }

        #endregion

        #region TransactionID
        public abstract class transactionID : PX.Data.IBqlField
        {
        }
        protected string _TransactionID;
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "TransactionID", IsReadOnly = true)]
        public virtual string TransactionID
        {
            get
            {
                return this._TransactionID;
            }
            set
            {
                this._TransactionID = value;
            }
        }
        #endregion
        #region GatewayMessage
        public abstract class gatewayMessage : PX.Data.IBqlField
        {
        }
        protected string _GatewayMessage;
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Message", IsReadOnly = true)]
        public virtual string GatewayMessage
        {
            get
            {
                return this._GatewayMessage;
            }
            set
            {
                this._GatewayMessage = value;
            }
        }
        #endregion
        #region CheckDate
        public abstract class checkDate : PX.Data.IBqlField
        {
        }
        protected DateTime? _CheckDate;
        [PXDBDateAndTime()]
        [PXUIField(DisplayName = "Date", IsReadOnly = true)]
        public virtual DateTime? CheckDate
        {
            get
            {
                return this._CheckDate;
            }
            set
            {
                this._CheckDate = value;
            }
        }
        #endregion
        #region CheckNumber
        public abstract class checkNumber : PX.Data.IBqlField
        {
        }
        protected string _CheckNumber;
        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Check Number", IsReadOnly = true)]
        public virtual string CheckNumber
        {
            get
            {
                return this._CheckNumber;
            }
            set
            {
                this._CheckNumber = value;
            }
        }
        #endregion
        #region CheckAmt
        public abstract class checkAmt : PX.Data.IBqlField
        {
        }
        protected decimal? _CheckAmt;
        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Amount", IsReadOnly = true)]
        public virtual decimal? CheckAmt
        {
            get
            {
                return this._CheckAmt;
            }
            set
            {
                this._CheckAmt = value;
            }
        }
        #endregion



        #region InvoiceNbr
        public abstract class invoiceNbr : PX.Data.IBqlField
        {
        }
        protected string _InvoiceNbr;
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "InvoiceNbr")]
        public virtual string InvoiceNbr
        {
            get
            {
                return this._InvoiceNbr;
            }
            set
            {
                this._InvoiceNbr = value;
            }
        }
        #endregion
        #region InvoiceAmt
        public abstract class invoiceAmt : PX.Data.IBqlField
        {
        }
        protected decimal? _InvoiceAmt;
        [PXDBDecimal(5)]
        [PXUIField(DisplayName = "InvoiceAmt")]
        public virtual decimal? InvoiceAmt
        {
            get
            {
                return this._InvoiceAmt;
            }
            set
            {
                this._InvoiceAmt = value;
            }
        }
        #endregion
        #region ApplyAmt
        public abstract class applyAmt : PX.Data.IBqlField
        {
        }
        protected decimal? _ApplyAmt;
        [PXDBDecimal(5)]
        [PXUIField(DisplayName = "ApplyAmt")]
        public virtual decimal? ApplyAmt
        {
            get
            {
                return this._ApplyAmt;
            }
            set
            {
                this._ApplyAmt = value;
            }
        }
        #endregion
        #region IsSplit
        public abstract class isSplit : PX.Data.IBqlField
        {
        }
        protected bool? _IsSplit;
        [PXDBBool()]
        [PXUIField(DisplayName = "IsSplit")]
        public virtual bool? IsSplit
        {
            get
            {
                return this._IsSplit;
            }
            set
            {
                this._IsSplit = value;
            }
        }
        #endregion
        #region DataSourceID
        public abstract class dataSourceID : PX.Data.IBqlField
        {
        }
        protected int? _DataSourceID;
        [PXDBInt()]
        [PXUIField(DisplayName = "DataSourceID")]
        public virtual int? DataSourceID
        {
            get
            {
                return this._DataSourceID;
            }
            set
            {
                this._DataSourceID = value;
            }
        }
        #endregion
        #region SourceFileName
        public abstract class sourceFileName : PX.Data.IBqlField
        {
        }
        protected string _SourceFileName;
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "SourceFileName")]
        public virtual string SourceFileName
        {
            get
            {
                return this._SourceFileName;
            }
            set
            {
                this._SourceFileName = value;
            }
        }
        #endregion
        #region FileID
        public abstract class fileID : PX.Data.IBqlField
        {
        }
        protected string _FileID;
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "FileID")]
        public virtual string FileID
        {
            get
            {
                return this._FileID;
            }
            set
            {
                this._FileID = value;
            }
        }
        #endregion
        #region FileRevisionID
        public abstract class fileRevisionID : PX.Data.IBqlField
        {
        }
        protected int? _FileRevisionID;
        [PXDBInt()]
        [PXUIField(DisplayName = "FileRevisionID")]
        public virtual int? FileRevisionID
        {
            get
            {
                return this._FileRevisionID;
            }
            set
            {
                this._FileRevisionID = value;
            }
        }
        #endregion
        #region IsProcessed
        public abstract class isProcessed : PX.Data.IBqlField
        {
        }
        protected bool? _IsProcessed;
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "IsProcessed")]
        public virtual bool? IsProcessed
        {
            get
            {
                return this._IsProcessed;
            }
            set
            {
                this._IsProcessed = value;
            }
        }
        #endregion
        #region Hold
        public abstract class hold : PX.Data.IBqlField
        {
        }
        [PXDBBool()]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Hold")]
        public virtual bool? Hold { get; set; }
        #endregion
        #region IsMapped
        public abstract class isMapped : PX.Data.IBqlField
        {
        }
        protected bool? _IsMapped;
        [PXDBBool()]
        [PXUIField(DisplayName = "IsMapped")]
        public virtual bool? IsMapped
        {
            get
            {
                return this._IsMapped;
            }
            set
            {
                this._IsMapped = value;
            }
        }
        #endregion
        #region AcumaticaCashAccount
        public abstract class acumaticaCashAccount : PX.Data.IBqlField
        {
        }
        protected int? _AcumaticaCashAccount;
        [PXDBInt()]
        [PXUIField(DisplayName = "AcumaticaCashAccount")]
        public virtual int? AcumaticaCashAccount
        {
            get
            {
                return this._AcumaticaCashAccount;
            }
            set
            {
                this._AcumaticaCashAccount = value;
            }
        }
        #endregion
        #region AcumaticaInvoiceRefNbr
        public abstract class acumaticaInvoiceRefNbr : PX.Data.IBqlField
        {
        }
        protected string _AcumaticaInvoiceRefNbr;
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "AcumaticaInvoiceRefNbr")]
        public virtual string AcumaticaInvoiceRefNbr
        {
            get
            {
                return this._AcumaticaInvoiceRefNbr;
            }
            set
            {
                this._AcumaticaInvoiceRefNbr = value;
            }
        }
        #endregion
        #region AcumaticaPaymentRefNbr
        public abstract class acumaticaPaymentRefNbr : PX.Data.IBqlField
        {
        }
        protected string _AcumaticaPaymentRefNbr;
        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Acumatica Payment RefNbr")]
        public virtual string AcumaticaPaymentRefNbr
        {
            get
            {
                return this._AcumaticaPaymentRefNbr;
            }
            set
            {
                this._AcumaticaPaymentRefNbr = value;
            }
        }
        #endregion
        #region LastLineNbr
        public abstract class lastLineNbr : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXDefault(0)]
        [PXUIField(Visible = false)]
        public virtual int? LastLineNbr { get; set; }
        #endregion


        #region Selected
        public abstract class selected : IBqlField
        {
        }
        [PXBool()]
        [PXUIField(DisplayName = "Selected")]
        public virtual bool? Selected { get; set; }
        #endregion
        #region UnApply
        public abstract class unApply : IBqlField
        {
        }
        [PXBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "UnApply")]
        public virtual bool? UnApply { get; set; }
        #endregion


        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }
        protected byte[] _tstamp;
        [PXDBTimestamp()]
        public virtual byte[] tstamp
        {
            get
            {
                return this._tstamp;
            }
            set
            {
                this._tstamp = value;
            }
        }
        #endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }
        protected Guid? _CreatedByID;
        [PXDBCreatedByID()]
        [PXUIField(DisplayName = "Created By")]
        public virtual Guid? CreatedByID
        {
            get
            {
                return this._CreatedByID;
            }
            set
            {
                this._CreatedByID = value;
            }
        }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }
        protected string _CreatedByScreenID;
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID
        {
            get
            {
                return this._CreatedByScreenID;
            }
            set
            {
                this._CreatedByScreenID = value;
            }
        }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }
        protected DateTime? _CreatedDateTime;
        [PXDBCreatedDateTime()]
        [PXUIField(DisplayName = "Date Time")]
        public virtual DateTime? CreatedDateTime
        {
            get
            {
                return this._CreatedDateTime;
            }
            set
            {
                this._CreatedDateTime = value;
            }
        }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }
        protected Guid? _LastModifiedByID;
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID
        {
            get
            {
                return this._LastModifiedByID;
            }
            set
            {
                this._LastModifiedByID = value;
            }
        }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }
        protected string _LastModifiedByScreenID;
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID
        {
            get
            {
                return this._LastModifiedByScreenID;
            }
            set
            {
                this._LastModifiedByScreenID = value;
            }
        }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }
        protected DateTime? _LastModifiedDateTime;
        [PXDBLastModifiedDateTime()]
        public virtual DateTime? LastModifiedDateTime
        {
            get
            {
                return this._LastModifiedDateTime;
            }
            set
            {
                this._LastModifiedDateTime = value;
            }
        }
        #endregion

    }
}