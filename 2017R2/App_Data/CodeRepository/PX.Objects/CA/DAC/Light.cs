using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PX.Common;
using PX.Data;
using PX.Objects;

namespace PX.Objects.CA.Light
{
	[Serializable]
	[PXTable]
	public partial class ARPayment : AR.ARRegister
	{
		#region ExtRefNbr
		public abstract class extRefNbr : IBqlField
		{
		}
		[PXDBString(40, IsUnicode = true)]
		public virtual string ExtRefNbr
		{
			get;
			set;
		}
		#endregion
		#region DocType
		public new abstract class docType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public override string DocType
		{
			get
			{
				return this._DocType;
			}

			set
			{
				this._DocType = value;
			}
		}
		#endregion
		#region RefNbr
		public new abstract class refNbr : IBqlField
		{
		}
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		public override string RefNbr
		{
			get
			{
				return this._RefNbr;
			}

			set
			{
				this._RefNbr = value;
			}
		}
		#endregion

		#region PaymentMethodID
		public abstract class paymentMethodID : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true)]
		public virtual string PaymentMethodID
		{
			get;
			set;
		}
		#endregion
		#region PMInstanceID
		public abstract class pMInstanceID : IBqlField
		{
		}
		[PXDBInt]
		public virtual int? PMInstanceID
		{
			get;
			set;
		}
		#endregion

		#region CashAccountID
		public abstract class cashAccountID : IBqlField
		{
		}
		[PXDBInt]
		public virtual int? CashAccountID
		{
			get;
			set;
		}
		#endregion

		#region BatchNbr
		public new abstract class batchNbr : IBqlField
		{
		}
		#endregion
		#region Voided
		public new abstract class voided : IBqlField
		{
		}
		[PXDBBool]
		public override bool? Voided
		{
			get
			{
				return this._Voided;
			}

			set
			{
				this._Voided = value;
			}
		}
		#endregion

		#region Released
		public new abstract class released : IBqlField
		{
		}
		#endregion
		#region CATranID
		public abstract class cATranID : IBqlField
		{
		}
		[PXDBLong]
		public virtual long? CATranID
		{
			get;
			set;
		}
		#endregion

		#region ARDepositAsBatch
		public abstract class depositAsBatch : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? DepositAsBatch
		{
			get;
			set;
		}
		#endregion
		#region DepositAfter
		public abstract class depositAfter : IBqlField
		{
		}
		[PXDBDate]
		public virtual DateTime? DepositAfter
		{
			get;
			set;
		}
		#endregion
		#region DepositDate
		public abstract class depositDate : IBqlField
		{
		}
		[PXDBDate]
		public virtual DateTime? DepositDate
		{
			get;
			set;
		}
		#endregion
		#region Deposited
		public abstract class deposited : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Deposited
		{
			get;
			set;
		}
		#endregion
		#region DepositType
		public abstract class depositType : IBqlField
		{
		}
		[PXDBString(3, IsFixed = true)]

		public virtual string DepositType
		{
			get;
			set;
		}
		#endregion
		#region DepositNbr
		public abstract class depositNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true)]
		public virtual string DepositNbr
		{
			get;
			set;
		}
		#endregion
	}
	[Serializable]
	[PXTable]
	public partial class APPayment : AP.APRegister
	{
		#region ExtRefNbr
		public abstract class extRefNbr : IBqlField
		{
		}
		[PXDBString(40, IsUnicode = true)]
		public virtual string ExtRefNbr
		{
			get;
			set;
		}
		#endregion
		#region DocType
		public new abstract class docType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public override string DocType
		{
			get
			{
				return this._DocType;
			}

			set
			{
				this._DocType = value;
			}
		}
		#endregion
		#region RefNbr
		public new abstract class refNbr : IBqlField
		{
		}
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		public override string RefNbr
		{
			get
			{
				return this._RefNbr;
			}

			set
			{
				this._RefNbr = value;
			}
		}
		#endregion

		#region BranchID
		public new abstract class branchID : IBqlField
		{
		}
		[PXDBInt]
		public override int? BranchID
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
		#region PaymentMethodID
		public abstract class paymentMethodID : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true)]
		public virtual string PaymentMethodID
		{
			get;
			set;
		}
		#endregion
		#region CashAccountID
		public abstract class cashAccountID : IBqlField
		{
		}
		[PXDBInt]
		public virtual int? CashAccountID
		{
			get;
			set;
		}
		#endregion

		#region BatchNbr
		public new abstract class batchNbr : IBqlField
		{
		}
		#endregion
		#region Released
		public new abstract class released : IBqlField
		{
		}
		#endregion
		#region OpenDoc
		public new abstract class openDoc : IBqlField
		{
		}
		#endregion
		#region Hold
		public new abstract class hold : IBqlField
		{
		}
		#endregion
		#region Voided
		public new abstract class voided : IBqlField
		{
		}
		[PXDBBool]
		public override bool? Voided
		{
			get
			{
				return this._Voided;
			}

			set
			{
				this._Voided = value;
			}
		}
		#endregion

		#region CATranID
		public abstract class cATranID : IBqlField
		{
		}

		[PXDBLong]
		public virtual long? CATranID
		{
			get;
			set;
		}
		#endregion

		#region DepositAsBatch
		public abstract class depositAsBatch : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? DepositAsBatch
		{
			get;
			set;
		}
		#endregion
		#region DepositAfter
		public abstract class depositAfter : IBqlField
		{
		}
		[PXDBDate]
		public virtual DateTime? DepositAfter
		{
			get;
			set;
		}
		#endregion
		#region Deposited
		public abstract class deposited : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Deposited
		{
			get;
			set;
		}
		#endregion
		#region DepositDate
		public abstract class depositDate : IBqlField
		{
		}
		[PXDBDate]
		public virtual DateTime? DepositDate
		{
			get;
			set;
		}
		#endregion
		#region DepositType
		public abstract class depositType : IBqlField
		{
		}
		[PXDBString(3, IsFixed = true)]
		public virtual string DepositType
		{
			get;
			set;
		}
		#endregion
		#region DepositNbr
		public abstract class depositNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true)]
		public virtual string DepositNbr
		{
			get;
			set;
		}
		#endregion
	}

	[PXTable]
	[Serializable]
	public partial class ARInvoice : ARRegister
	{
        #region DocType
        public new abstract class docType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public override string DocType
		{
			get;
			set;
		}
		#endregion
		#region RefNbr
		public new abstract class refNbr : IBqlField
		{
		}
		[PXDBString(15, IsKey = true, IsUnicode = true)]
		public override string RefNbr
		{
			get;
			set;
		}
		#endregion
		#region CustomerID
		public new abstract class customerID : IBqlField
		{
		}
		[PXDBInt]
		public override int? CustomerID
		{
			get;
			set;
		}
		#endregion
		#region CuryID
		public new abstract class curyID : IBqlField
		{
		}
		#endregion
		#region InvoiceNbr
		public abstract class invoiceNbr : IBqlField
		{
		}
		[PXDBString(40, IsUnicode = true)]
		public virtual string InvoiceNbr
		{
			get;
			set;
		}
		#endregion
		#region DocDate
		public new abstract class docDate : IBqlField
		{
		}
		#endregion
		#region CuryInfoID
		public new abstract class curyInfoID : IBqlField
		{
		}
		#endregion
		#region CuryDocBal
		public new abstract class curyDocBal : IBqlField
		{
		}
		#endregion
		#region DocBal
		public new abstract class docBal : IBqlField
		{
		}
		#endregion
		#region Released
		public new abstract class released : IBqlField
		{
		}
		#endregion
		#region OpenDoc
		public new abstract class openDoc : IBqlField
		{
		}
		#endregion
		#region Voided
		public new abstract class voided : IBqlField
		{
		}
		#endregion
		#region DocDesc
		public new abstract class docDesc : IBqlField
		{
		}
		#endregion
	}
	[Serializable]
	public partial class ARRegister : IBqlTable
	{
        #region BranchID
        public abstract class branchID : IBqlField
        {
        }
        [PXDBInt]
        public virtual int? BranchID
        {
            get;
            set;
        }
        #endregion
        #region DocType
        public abstract class docType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public virtual string DocType
		{
			get;
			set;
		}
		#endregion
		#region RefNbr
		public abstract class refNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		public virtual string RefNbr
		{
			get;
			set;
		}
		#endregion
		#region DocDate
		public abstract class docDate : IBqlField
		{
		}
		[PXDBDate]
		public virtual DateTime? DocDate
		{
			get;
			set;
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : IBqlField
		{
		}
		[PXDBString]
		public virtual string FinPeriodID
		{
			get;
			set;
		}
		#endregion
		#region CustomerID
		public abstract class customerID : IBqlField
		{
		}
		[PXDBInt]
		public virtual int? CustomerID
		{
			get;
			set;
		}
		#endregion
		#region CuryID
		public abstract class curyID : IBqlField
		{
		}
		[PXDBString(5, IsUnicode = true)]
		public virtual string CuryID
		{
			get;
			set;
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : IBqlField
		{
		}
		[PXDBLong]
		public virtual long? CuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region CuryDocBal
		public abstract class curyDocBal : IBqlField
		{
		}
		[PXDBDecimal]
		public virtual decimal? CuryDocBal
		{
			get;
			set;
		}
		#endregion
		#region DocBal
		public abstract class docBal : IBqlField
		{
		}
		[PXDBDecimal]
		public virtual decimal? DocBal
		{
			get;
			set;
		}
		#endregion
		#region DocDesc
		public abstract class docDesc : IBqlField
		{
		}
		[PXDBString(150, IsUnicode = true)]
		public virtual string DocDesc
		{
			get;
			set;
		}
		#endregion
		#region Released
		public abstract class released : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Released
		{
			get;
			set;
		}
		#endregion
		#region OpenDoc
		public abstract class openDoc : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? OpenDoc
		{
			get;
			set;
		}
		#endregion
		#region Voided
		public abstract class voided : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Voided
		{
			get;
			set;
		}
		#endregion
	}
	[Serializable]
	public partial class BAccount : IBqlTable
	{
		#region BAccountID
		public abstract class bAccountID : IBqlField
		{
		}
		[PXDBIdentity]
		public virtual int? BAccountID
		{
			get;
			set;
		}
		#endregion
		#region AcctName
		public abstract class acctName : IBqlField
		{
		}

		[PXDBString(60, IsUnicode = true)]
		public virtual string AcctName
		{
			get;
			set;
		}
        #endregion
        #region ConsolidatingBAccountID
        public abstract class consolidatingBAccountID : IBqlField { }

        [PXDBInt]
		public virtual int? ConsolidatingBAccountID
		{
			get;
			set;
		}
        #endregion
        #region AcctCD
        public abstract class acctCD : IBqlField
        {
        }

        [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
        public virtual string AcctCD
        {
            get;
            set;
        }
        #endregion
        #region Status
        public abstract class status : IBqlField
        {
        }
        [PXDBString(1, IsFixed = true)]
        [CR.BAccount.status.List]
        public virtual string Status
        {
            get;
            set;
        }
        #endregion
        #region NoteID
        public abstract class noteID : IBqlField
        {
        }
        [PXDBGuid]
        public virtual Guid? NoteID
        {
            get;
            set;
        }
        #endregion
    }
    [Serializable]
	public partial class CABankTranAdjustment : IBqlTable
	{
		#region TranID
		public abstract class tranID : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		public virtual int? TranID
		{
			get;
			set;
		}
		#endregion
		#region AdjdModule
		public abstract class adjdModule : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true)]
		public virtual string AdjdModule
		{
			get;
			set;
		}
		#endregion
		#region AdjdDocType
		public abstract class adjdDocType : IBqlField
		{
		}
		[PXDBString(3, IsFixed = true)]
		public virtual string AdjdDocType
		{
			get;
			set;
		}
		#endregion
		#region AdjdRefNbr
		public abstract class adjdRefNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true)]
		public virtual string AdjdRefNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjNbr
		public abstract class adjNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		public virtual int? AdjNbr
		{
			get;
			set;
		}
		#endregion
		#region Released
		public abstract class released : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Released
		{
			get;
			set;
		}
		#endregion
		#region Voided
		public abstract class voided : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Voided
		{
			get;
			set;
		}
		#endregion
	}

	[Serializable]
	public partial class ARAdjust : IBqlTable
	{
		#region AdjgDocType
		public abstract class adjgDocType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public virtual string AdjgDocType
		{
			get;
			set;
		}
		#endregion
		#region AdjgRefNbr
		public abstract class adjgRefNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		public virtual string AdjgRefNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjdDocType
		public abstract class adjdDocType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public virtual string AdjdDocType
		{
			get;
			set;
		}
		#endregion
		#region AdjdRefNbr
		public abstract class adjdRefNbr : IBqlField
		{
		}
		[PXDBString(15, IsKey = true, IsUnicode = true)]
		public virtual string AdjdRefNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjNbr
		public abstract class adjNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		public virtual int? AdjNbr
		{
			get;
			set;
		}
		#endregion
		#region Released
		public abstract class released : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Released
		{
			get;
			set;
		}
		#endregion
		#region Voided
		public abstract class voided : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Voided
		{
			get;
			set;
		}
		#endregion
	}
	[PXTable]
	[Serializable]
	public partial class APInvoice : APRegister
	{
		#region DocType
		public new abstract class docType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public override string DocType
		{
			get;
			set;
		}
		#endregion
		#region RefNbr
		public new abstract class refNbr : IBqlField
		{
		}
		[PXDBString(15, IsKey = true, IsUnicode = true)]
		public override string RefNbr
		{
			get;
			set;
		}
		#endregion
		#region VendorID
		public new abstract class vendorID : IBqlField
		{
		}
		[PXDBInt]
		public override int? VendorID
		{
			get;
			set;
		}
		#endregion
		#region CuryID
		public new abstract class curyID : IBqlField
		{
		}
		#endregion
		#region InvoiceNbr
		public abstract class invoiceNbr : IBqlField
		{
		}
		[PXDBString(40, IsUnicode = true)]
		public virtual string InvoiceNbr
		{
			get;
			set;
		}
		#endregion
		#region DocDate
		public new abstract class docDate : IBqlField
		{
		}
		#endregion
		#region CuryInfoID
		public new abstract class curyInfoID : IBqlField
		{
		}
		#endregion
		#region CuryDocBal
		public new abstract class curyDocBal : IBqlField
		{
		}
		#endregion
		#region DocBal
		public new abstract class docBal : IBqlField
		{
		}
		#endregion
		#region Released
		public new abstract class released : IBqlField
		{
		}
		#endregion
		#region OpenDoc
		public new abstract class openDoc : IBqlField
		{
		}
		#endregion
		#region Voided
		public new abstract class voided : IBqlField
		{
		}
		#endregion
		#region DocDesc
		public new abstract class docDesc : IBqlField
		{
		}
		#endregion
	}
	[Serializable]
	public partial class APRegister : IBqlTable
	{
        #region BranchID
        public abstract class branchID : IBqlField
        {
        }
        [PXDBInt]
        public virtual int? BranchID
        {
            get;
            set;
        }
        #endregion
        #region DocType
        public abstract class docType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public virtual string DocType
		{
			get;
			set;
		}
		#endregion
		#region RefNbr
		public abstract class refNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		public virtual string RefNbr
		{
			get;
			set;
		}
		#endregion
		#region DocDate
		public abstract class docDate : IBqlField
		{
		}
		[PXDBDate]
		public virtual DateTime? DocDate
		{
			get;
			set;
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : IBqlField
		{
		}
		[PXDBString]
		public virtual string FinPeriodID
		{
			get;
			set;
		}
		#endregion
		#region VendorID
		public abstract class vendorID : IBqlField
		{
		}
		[PXDBInt]
		public virtual int? VendorID
		{
			get;
			set;
		}
		#endregion
		#region CuryID
		public abstract class curyID : IBqlField
		{
		}
		[PXDBString(5, IsUnicode = true)]
		public virtual string CuryID
		{
			get;
			set;
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : IBqlField
		{
		}
		[PXDBLong]
		public virtual long? CuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region CuryDocBal
		public abstract class curyDocBal : IBqlField
		{
		}
		[PXDBDecimal]
		public virtual decimal? CuryDocBal
		{
			get;
			set;
		}
		#endregion
		#region DocBal
		public abstract class docBal : IBqlField
		{
		}
		[PXDBDecimal]
		public virtual decimal? DocBal
		{
			get;
			set;
		}
		#endregion
		#region DocDesc
		public abstract class docDesc : IBqlField
		{
		}
		[PXDBString(150, IsUnicode = true)]
		public virtual string DocDesc
		{
			get;
			set;
		}
		#endregion
		#region Released
		public abstract class released : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Released
		{
			get;
			set;
		}
		#endregion
		#region OpenDoc
		public abstract class openDoc : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? OpenDoc
		{
			get;
			set;
		}
		#endregion
		#region Voided
		public abstract class voided : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Voided
		{
			get;
			set;
		}
		#endregion
	}
	[Serializable]
	public partial class APAdjust : IBqlTable
	{
		#region AdjgDocType
		public abstract class adjgDocType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public virtual string AdjgDocType
		{
			get;
			set;
		}
		#endregion
		#region AdjgRefNbr
		public abstract class adjgRefNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		public virtual string AdjgRefNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjdDocType
		public abstract class adjdDocType : IBqlField
		{
		}
		[PXDBString(3, IsKey = true, IsFixed = true)]
		public virtual string AdjdDocType
		{
			get;
			set;
		}
		#endregion
		#region AdjdRefNbr
		public abstract class adjdRefNbr : IBqlField
		{
		}
		[PXDBString(15, IsKey = true, IsUnicode = true)]
		public virtual string AdjdRefNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjNbr
		public abstract class adjNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		public virtual int? AdjNbr
		{
			get;
			set;
		}
		#endregion
		#region Released
		public abstract class released : IBqlField
		{
		}
		[PXDBBool]
		public virtual bool? Released
		{
			get;
			set;
		}
		#endregion
		#region Voided
		public abstract class voided : IBqlField
		{
		}

		[PXDBBool]
		public virtual bool? Voided
		{
			get;
			set;
		}
		#endregion
	}
    [Serializable]
    [PXTable(typeof(CA.Light.BAccount.bAccountID))]
    [PXCacheName(AR.Messages.Customer)]
    public class Customer : CA.Light.BAccount
    {
        #region BAccountID
        public new abstract class bAccountID : IBqlField
        {
        }
        #endregion
        #region CuryID
        public abstract class curyID : IBqlField
        {
        }
        [PXDBString(5, IsUnicode = true)]
        public virtual string CuryID
        {
            get;
            set;
        }
        #endregion
        #region CustomerClassID
        public abstract class customerClassID : IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true)]
        public virtual string CustomerClassID
        {
            get;
            set;
        }
        #endregion
        #region StatementCycleId
        public abstract class statementCycleId : IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true)]
        public virtual string StatementCycleId
        {
            get;
            set;
        }
        #endregion
        #region ConsolidatingBAccountID
        public new abstract class consolidatingBAccountID : IBqlField { }
        #endregion
    }

    [Serializable]
    public class CustomerMaster : Customer
    {
        #region BAccountID
        public new abstract class bAccountID : IBqlField
        {
        }
        [AR.Customer(IsKey = true, DisplayName = "Customer ID")]
        public override int? BAccountID
        {
            get;
            set;
        }
        #endregion
        #region AcctCD
        public new abstract class acctCD : IBqlField
        {
        }
        [PXDBString(30, IsUnicode = true)]
        public override string AcctCD
        {
            get;
            set;
        }
        #endregion
        #region StatementCycleID
        public new abstract class statementCycleId : IBqlField { }
        #endregion
    }
}