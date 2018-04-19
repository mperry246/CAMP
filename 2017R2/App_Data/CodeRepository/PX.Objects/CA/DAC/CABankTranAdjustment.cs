using System;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CA.BankStatementProtoHelpers;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;

namespace PX.Objects.CA
{
	[Serializable]
    [PXCacheName(Messages.BankTranAdjustment)]
	public partial class CABankTranAdjustment : IBqlTable, ICADocAdjust, IAdjustment
	{
		#region TranID
		public abstract class tranID : IBqlField
		{
		}
		
		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(CABankTran.tranID))]
		public virtual int? TranID
		{
			get;
			set;
		}
		#endregion
		#region Selected
		public abstract class selected : IBqlField
		{
		}

		protected bool? _Selected = false;
		[PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Selected")]
        public virtual bool? Selected
        {
            get
            {
                return _Selected;
            }

            set
            {
                _Selected = value;
            }
        }
		#endregion
		#region AdjdModule
		public abstract class adjdModule : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true)]
		[PXDefault]
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
		[PXDBString(3, IsFixed = true, InputMask = "")]
		[PXDefault(APDocType.Invoice)]
		[PXUIField(DisplayName = "Document Type", Visibility = PXUIVisibility.Visible)]
		[APInvoiceType.AdjdList]
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
		[PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible)]
		[PXInvoiceSelector(typeof(CABankTran.origModule))]
		public virtual string AdjdRefNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjdBranchID
		public abstract class adjdBranchID : IBqlField
		{
		}
		[Branch(null, Enabled = false, Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
        public virtual int? AdjdBranchID
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
		[PXUIField(DisplayName = "Adjustment Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
		[PXLineNbr(typeof(CABankTran.lineCntr))]
		[PXParent(typeof(Select<CABankTran, Where<CABankTran.tranID, Equal<Current<CABankTranAdjustment.tranID>>>>))]
		[PXDefault(TypeCode.Int32, "0")]
		public virtual int? AdjNbr
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjdAmt
		public abstract class curyAdjdAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryAdjdAmt
		{
			get;
			set;
		}
        #endregion

		#region SeparateCheck
		public abstract class separateCheck : IBqlField
		{
		}
		[PXBool]
		[PXUIField(DisplayName = "Pay Separately", Visibility = PXUIVisibility.Visible)]
		public virtual bool? SeparateCheck
		{
			get;
			set;
		}
		#endregion
		#region AdjdCuryInfoID
		public abstract class adjdCuryInfoID : IBqlField
		{
		}

		[PXDBLong]
		[PXDefault]
		[CurrencyInfo(ModuleCode = BatchModule.AP, CuryIDField = "AdjdCuryID", Enabled = false)]
		public virtual long? AdjdCuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region PrintAdjdDocType
		public abstract class printAdjdDocType : IBqlField
		{
		}
		[PXString(3, IsFixed = true)]
		[APDocType.PrintList]
		[PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.Visible, Enabled = true)]
		public virtual string PrintAdjdDocType
		{
			get
			{
				return this.AdjdDocType;
			}

			set
			{
			}
		}
		#endregion
		#region StubNbr
		public abstract class stubNbr : IBqlField
		{
		}

		[PXDBString(40, IsUnicode = true)]
		public virtual string StubNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjBatchNbr
		public abstract class adjBatchNbr : IBqlField
		{
		}

		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.Visible, Visible = true, Enabled = false)]
		public virtual string AdjBatchNbr
		{
			get;
			set;
		}
		#endregion
		#region VoidAdjNbr
		public abstract class voidAdjNbr : IBqlField
		{
		}

		[PXDBInt]
		public virtual int? VoidAdjNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjdOrigCuryInfoID
		public abstract class adjdOrigCuryInfoID : IBqlField
		{
		}

		[PXDBLong]
		[PXDefault]
		[CurrencyInfo(ModuleCode = BatchModule.AP, CuryIDField = "AdjdOrigCuryID")]
		public virtual long? AdjdOrigCuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region AdjgCuryInfoID
		public abstract class adjgCuryInfoID : IBqlField
		{
		}

		[PXDBLong]
		[CurrencyInfo(CuryIDField = "AdjgCuryID")]
		public virtual long? AdjgCuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region AdjgDocDate
		public abstract class adjgDocDate : IBqlField
		{
		}

		[PXDBDate]
		[PXDBDefault(typeof(CABankTran.tranDate))]
		public virtual DateTime? AdjgDocDate
		{
			get;
			set;
		}
		#endregion
		#region AdjgFinPeriodID
		public abstract class adjgFinPeriodID : IBqlField
		{
		}
		[FinPeriodID]
		[PXUIField(DisplayName = "Application Period", Enabled = false)]
		public virtual string AdjgFinPeriodID
		{
			get;
			set;
		}
		#endregion
		#region AdjgTranPeriodID
		public abstract class adjgTranPeriodID : IBqlField
		{
		}

		[FinPeriodID]
		public virtual string AdjgTranPeriodID
		{
			get;
			set;
		}
		#endregion
		#region AdjdDocDate
		public abstract class adjdDocDate : IBqlField
		{
		}

		[PXDBDate]
		[PXDefault]
		[PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual DateTime? AdjdDocDate
		{
			get;
			set;
		}
		#endregion
		#region AdjdFinPeriodID
		public abstract class adjdFinPeriodID : IBqlField
		{
		}

		[FinPeriodID(typeof(CABankTranAdjustment.adjdDocDate))]
		[PXUIField(DisplayName = "Post Period", Enabled = false, Visible = false)]
		public virtual string AdjdFinPeriodID
		{
			get;
			set;
		}
		#endregion
		#region AdjdClosedFinPeriodID
		public abstract class adjdClosedFinPeriodID : IBqlField
		{
		}

		[PXDBScalar(typeof(Search<APRegister.closedFinPeriodID, Where<APRegister.docType, Equal<CABankTranAdjustment.adjdDocType>, And<APRegister.refNbr, Equal<CABankTranAdjustment.adjdRefNbr>>>>))]
		[PXString]
		public virtual string AdjdClosedFinPeriodID
		{
			get;
			set;
		}
		#endregion
		#region AdjdTranPeriodID
		public abstract class adjdTranPeriodID : IBqlField
		{
		}

		[TranPeriodID(typeof(CABankTranAdjustment.adjdDocDate))]
		public virtual string AdjdTranPeriodID
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjgDiscAmt
		public abstract class curyAdjgDiscAmt : IBqlField
		{
		}

		[PXDBCurrency(typeof(CABankTranAdjustment.adjgCuryInfoID), typeof(CABankTranAdjustment.adjDiscAmt))]
		[PXUIField(DisplayName = "Cash Discount Taken", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual decimal? CuryAdjgDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjgWhTaxAmt
		public abstract class curyAdjgWhTaxAmt : IBqlField
		{
		}

		[PXDBCurrency(typeof(CABankTranAdjustment.adjgCuryInfoID), typeof(CABankTranAdjustment.adjWhTaxAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "With. Tax", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual decimal? CuryAdjgWhTaxAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjgAmt
		public abstract class curyAdjgAmt : IBqlField
		{
		}

		[PXDBCurrency(typeof(CABankTranAdjustment.adjgCuryInfoID), typeof(CABankTranAdjustment.adjAmt), BaseCalc = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount Paid", Visibility = PXUIVisibility.Visible)]
		[PXUnboundFormula(typeof(Mult<CABankTranAdjustment.adjgBalSign, CABankTranAdjustment.curyAdjgAmt>), typeof(SumCalc<CABankTran.curyApplAmt>))]
		public virtual decimal? CuryAdjgAmt
		{
			get;
			set;
		}

		public virtual decimal? CuryAdjgAmount
		{
			get
			{
				return CuryAdjgAmt;
			}

			set
			{
				CuryAdjgAmt = value;
			}
		}
		#endregion
		#region AdjDiscAmt
		public abstract class adjDiscAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? AdjDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjdDiscAmt
		public abstract class curyAdjdDiscAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryAdjdDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region AdjWhTaxAmt
		public abstract class adjWhTaxAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? AdjWhTaxAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjdWhTaxAmt
		public abstract class curyAdjdWhTaxAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryAdjdWhTaxAmt
		{
			get;
			set;
		}
		#endregion
		#region AdjAmt
		public abstract class adjAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? AdjAmt
		{
			get;
			set;
		}
		#endregion
		#region RGOLAmt
		public abstract class rGOLAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? RGOLAmt
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
		[PXDefault(false)]
		public virtual bool? Released
		{
			get;
			set;
		}
		#endregion
		#region Hold
		public abstract class hold : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? Hold
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
		[PXDefault(false)]
		public virtual bool? Voided
		{
			get;
			set;
		}
		#endregion
		#region AdjdAPAcct
		public abstract class adjdAPAcct : IBqlField
		{
		}

		[Account(SuppressCurrencyValidation = true)]
		public virtual int? AdjdAPAcct
		{
			get;
			set;
		}
		#endregion
		#region AdjdAPSub
		public abstract class adjdAPSub : IBqlField
		{
		}

		[SubAccount]
		public virtual int? AdjdAPSub
		{
			get;
			set;
		}
		#endregion
		#region AdjdARAcct
		public abstract class adjdARAcct : IBqlField
		{
		}
		[Account]
		public virtual int? AdjdARAcct
		{
			get;
			set;
		}
		#endregion
		#region AdjdARSub
		public abstract class adjdARSub : IBqlField
		{
		}

		[SubAccount]
		public virtual int? AdjdARSub
		{
			get;
			set;
		}
		#endregion
		#region AdjdWhTaxAcctID
		public abstract class adjdWhTaxAcctID : IBqlField
		{
		}

		[Account]
		[PXDefault(typeof(Search2<APTaxTran.accountID, InnerJoin<Tax, On<Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<CABankTranAdjustment.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<CABankTranAdjustment.adjdRefNbr>>, And<Tax.taxType, Equal<CSTaxType.withholding>>>>, OrderBy<Asc<APTaxTran.taxID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? AdjdWhTaxAcctID
		{
			get;
			set;
		}
		#endregion
		#region AdjdWhTaxSubID
		public abstract class adjdWhTaxSubID : IBqlField
		{
		}

		[SubAccount]
		[PXDefault(typeof(Search2<APTaxTran.subID, InnerJoin<Tax, On<Tax.taxID, Equal<APTaxTran.taxID>>>, 
			Where<APTaxTran.tranType, Equal<Current<CABankTranAdjustment.adjdDocType>>, 
				And<APTaxTran.refNbr, Equal<Current<CABankTranAdjustment.adjdRefNbr>>, 
				And<Tax.taxType, Equal<CSTaxType.withholding>>>>, OrderBy<Asc<APTaxTran.taxID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? AdjdWhTaxSubID
		{
			get;
			set;
		}
		#endregion
		
		#region AdjdCuryRate
		public abstract class adjdCuryRate : IBqlField
		{
		}
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXDBDecimal(8)]
		[PXUIField(DisplayName = "Cross Rate", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual decimal? AdjdCuryRate
		{
			get;
			set;
		}
		#endregion
		#region CuryDocBal
		public abstract class curyDocBal : IBqlField
		{
		}

		[PXUnboundDefault(TypeCode.Decimal, "0.0")]
		[PXCury(typeof(CABankTran.curyID))]
		[PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
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

		[PXDecimal(4)]
		[PXUnboundDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? DocBal
		{
			get;
			set;
		}
		#endregion
		#region CuryDiscBal
		public abstract class curyDiscBal : IBqlField
		{
		}

		[PXCury(typeof(CABankTran.curyID))]
		[PXUnboundDefault]
		[PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.Visible, Enabled = false, Visible = false)]
		public virtual decimal? CuryDiscBal
		{
			get;
			set;
		}
		#endregion
		#region DiscBal
		public abstract class discBal : IBqlField
		{
		}

		[PXDecimal(4)]
		[PXUnboundDefault]
		public virtual decimal? DiscBal
		{
			get;
			set;
		}
		#endregion
		#region CuryWhTaxBal
		public abstract class curyWhTaxBal : IBqlField
		{
		}

		[PXCury(typeof(CABankTran.curyID))]
		[PXUnboundDefault]
		[PXUIField(DisplayName = "With. Tax Balance", Visibility = PXUIVisibility.Visible, Enabled = false, Visible = false)]
		public virtual decimal? CuryWhTaxBal
		{
			get;
			set;
		}
		#endregion
		#region WhTaxBal
		public abstract class whTaxBal : IBqlField
		{
		}
		[PXDecimal(4)]
		[PXUnboundDefault]
		public virtual decimal? WhTaxBal
		{
			get;
			set;
		}
		#endregion
		#region WriteOffReasonCode
		public abstract class writeOffReasonCode : IBqlField
		{
		}

		[PXFormula(typeof(Switch<Case<Where<CABankTranAdjustment.adjdDocType, NotEqual<AR.ARDocType.creditMemo>>, Current<AR.ARSetup.balanceWriteOff>>>))]
		[PXDBString(ReasonCode.reasonCodeID.Length, IsUnicode = true)]
		[PXSelector(typeof(Search<ReasonCode.reasonCodeID, Where2<Where<ReasonCode.usage, Equal<ReasonCodeUsages.creditWriteOff>, And<Current<CABankTranAdjustment.adjdDocType>, Equal<AR.ARDocType.creditMemo>>>,
			Or<Where<ReasonCode.usage, Equal<ReasonCodeUsages.balanceWriteOff>, And<Current<CABankTranAdjustment.adjdDocType>, NotEqual<AR.ARDocType.creditMemo>>>>>>))]
		[PXUIField(DisplayName = "Write-Off Reason Code", Visibility = PXUIVisibility.Visible)]
		public virtual string WriteOffReasonCode
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjgWOAmt
		public abstract class curyAdjgWOAmt : IBqlField
		{
		}
		[PXDecimal]
		[PXUIField(DisplayName = "Balance Write-Off", Visibility = PXUIVisibility.Visible)]
		[PXFormula(null, typeof(SumCalc<CABankTran.curyWOAmt>))]
		public virtual decimal? CuryAdjgWOAmt
		{
			get
			{
				return CuryAdjgWhTaxAmt;
			}

			set
			{
				CuryAdjgWhTaxAmt = value;
			}
		}
		#endregion
		#region AdjgBalSign
		public abstract class adjgBalSign : IBqlField
		{
		}

        protected decimal? _AdjgBalSign;

        [PXDecimal(4)]
		public virtual decimal? AdjgBalSign
		{
			get
			{
				return _AdjgBalSign ?? decimal.One;
			}

			set
			{
				_AdjgBalSign = value;
			}
		}
		#endregion
		#region ReverseGainLoss
		public bool? ReverseGainLoss
		{
			get { return false; }
			set { }
		}
		#endregion

		#region NoteID
		public abstract class noteID : IBqlField
		{
		}

		[PXNote]
		public virtual Guid? NoteID
		{
			get;
			set;
		}
		#endregion

		#region tstamp
		public abstract class Tstamp : IBqlField
		{
		}
		[PXDBTimestamp]
		public virtual byte[] tstamp
		{
			get;
			set;
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : IBqlField
		{
		}
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField
		{
		}
		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField
		{
		}
		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : IBqlField
		{
		}
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField
		{
		}
		[PXDBLastModifiedByScreenID]
		public virtual string LastModifiedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField
		{
		}
		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
	}
}
