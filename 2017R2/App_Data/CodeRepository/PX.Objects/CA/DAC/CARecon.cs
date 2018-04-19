using System;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.CA
{
	public class CADocStatus
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Balanced, Closed, Hold, Voided },
				new string[] { AP.Messages.Balanced, AP.Messages.Closed, AP.Messages.Hold, AP.Messages.Voided })
			{ }
		}

		public const string Balanced = "B";
		public const string Closed = "C";
		public const string Hold = "H";
		public const string Voided = "V";

		public class balanced : Constant<string>
		{
			public balanced() : base(Balanced) { }
		}

		public class closed : Constant<string>
		{
			public closed() : base(Closed) { }
		}

		public class hold : Constant<string>
		{
			public hold() : base(Hold) { }
		}

		public class voided : Constant<string>
		{
			public voided() : base(Voided) { }
		}
	}

	/// <summary>
	/// Represents the header of the reconciliation statement.
	/// Reconciliation statements are edited on the Reconciliation Statements (CA302000) form 
	/// (which corresponds to the <see cref="CAReconEntry"/> graph).
	/// </summary>
	[PXCacheName(Messages.CARecon)]
	[Serializable]
	[PXPrimaryGraph(typeof(CAReconEntry))]
	public partial class CARecon : IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		[PXBool]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected
		{
			get;
			set;
		}
		#endregion

		#region CashAccountID
		public abstract class cashAccountID : IBqlField
		{
		}
		/// <summary>
		/// The <see cref="CashAccount">cash account</see> under reconciliation.
		/// </summary>
		[PXDefault]
		[CashAccount(null, typeof(Search<CashAccount.cashAccountID, Where<CashAccount.reconcile, Equal<boolTrue>, And<Match<Current<AccessInfo.userName>>>>>), IsKey = true, Visibility = PXUIVisibility.SelectorVisible, Enabled = true, Required = true)]
		public virtual int? CashAccountID
		{
			get;
			set;
		}
		#endregion
		#region ReconNbr
		public abstract class reconNbr : IBqlField
		{
		}
		/// <summary>
		/// The identification number of the reconciliation statement, 
		/// which the system assigns when the user saves the statement.
		/// </summary>
		[PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault]
		[AutoNumber(typeof(Search<CashAccount.reconNumberingID, Where<CashAccount.cashAccountID, Equal<Current<CARecon.cashAccountID>>>>), typeof(CARecon.reconDate))]
		[PXUIField(DisplayName = "Ref. Number", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
		[PXSelector(typeof(Search<CARecon.reconNbr, Where<CARecon.cashAccountID, Equal<Optional<CARecon.cashAccountID>>>>))] 
		public virtual string ReconNbr
		{
			get;
			set;
		}
		#endregion
		#region ReconDate
		public abstract class reconDate : IBqlField
		{
		}
		/// <summary>
		/// The date when the reconciliation statement was released and closed. A user can change the date up to the release.
		/// </summary>
		[PXDBDate]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Reconciliation Date", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
		public virtual DateTime? ReconDate
		{
			get;
			set;
		}
		#endregion
		#region LastReconDate
		public abstract class lastReconDate : IBqlField
		{
		}
		/// <summary>
		/// The date of the most recent <see cref="CARecon"/> for this cash account, if one exists.
		/// </summary>
		[PXDBDate]
		[PXUIField(DisplayName = "Last Reconciliation Date", Enabled = false)]
		public virtual DateTime? LastReconDate
		{
			get;
			set;
		}
		#endregion
		#region LoadDocumentsTill
		public abstract class loadDocumentsTill : IBqlField
		{
		}
		/// <summary>
		/// The latest date for <see cref="CAReconEntry.CATranExt">documents</see> to be loaded to the list. 
		/// </summary>
		[PXDate]
		[PXDefault(typeof(AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Load Documents Up To", Visibility = PXUIVisibility.SelectorVisible, Required = false)]
		public virtual DateTime? LoadDocumentsTill
		{
			get;
			set;
		}
		#endregion
		#region Reconciled
		public abstract class reconciled : IBqlField
		{
		}

		protected bool? _Reconciled;
		/// <summary>
		/// Indicates (if set to <c>true</c>) that the reconciliation statement was completed and cannot be edited anymore.
		/// </summary>
		[PXDBBool]
		[PXUIField(DisplayName = "Reconciled", Enabled = false)]
		[PXDefault(false)]
		public virtual bool? Reconciled
		{
			get
			{
				return this._Reconciled;
			}

			set
			{
				this._Reconciled = value;
				this.SetStatus();
			}
		}
		#endregion
		#region Voided
		public abstract class voided : IBqlField
		{
		}

		protected bool? _Voided;
		/// <summary>
		/// Indicates (if set to <c>true</c>) that the reconciliation statement was voided.
		/// </summary>
		[PXDBBool]
		[PXUIField(DisplayName = "Voided", Enabled = false)]
		[PXDefault(false)]
		public virtual bool? Voided
		{
			get
			{
				return this._Voided;
			}

			set
			{
				this._Voided = value;
				this.SetStatus();
			}
		}
		#endregion
		#region Hold
		public abstract class hold : IBqlField
		{
		}

		protected bool? _Hold;
		/// <summary>
		/// Indicates (if set to <c>true</c>) that the reconciliation statement is on hold and can be saved unbalanced.
		/// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Hold")]
		public virtual bool? Hold
		{
			get
			{
				return this._Hold;
			}

			set
			{
				this._Hold = value;
				this.SetStatus();
			}
		}
		#endregion
		#region CuryBegBalance
		public abstract class curyBegBalance : IBqlField
		{
		}
		/// <summary>
		/// The balance of the previous <see cref="CARecon">reconciliation statement</see>
		/// in the <see cref="CARecon.CuryID">selected currency</see>.
		/// </summary>
		/// <value>
		/// The value of the field defaults to the <see cref="CARecon.CuryReconciledBalance">reconciled balance</see>
		/// of the previous <see cref="CARecon">statement</see>.
		/// </value>
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBCury(typeof(CARecon.curyID))]
		[PXUIField(DisplayName = "Beginning Balance", Enabled = false)]
		public virtual decimal? CuryBegBalance
		{
			get;
			set;
		}
		#endregion
		#region CuryBalance
		public abstract class curyBalance : IBqlField
		{
		}
		/// <summary>
		/// The balance of the current bank statement in the selected currency, 
		/// which user should enter manually for the current reconciliation statement.
		/// </summary>
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBCury(typeof(CARecon.curyID))]
		[PXUIField(DisplayName = "Statement Balance")]
		public virtual decimal? CuryBalance
		{
			get;
			set;
		}
		#endregion
		#region CuryReconciledDebits
		public abstract class curyReconciledDebits : IBqlField
		{
		}
		/// <summary>
		/// The total amount of reconciled <see cref="CAReconEntry.CATranExt.CuryReconciledDebit">receipts</see> 
		/// on the statement in the selected currency.
		/// </summary>
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBCurrency(typeof(CARecon.curyInfoID), typeof(CARecon.reconciledDebits))]
		[PXUIField(DisplayName = "Reconciled Receipts", Enabled = false)]
		public virtual decimal? CuryReconciledDebits
		{
			get;
			set;
		}
		#endregion
		#region ReconciledDebits
		public abstract class reconciledDebits : IBqlField
		{
		}
		/// <summary>
		/// The total amount of reconciled <see cref="CAReconEntry.CATranExt.ReconciledDebit">receipts</see> 
		/// on the statement in the base currency.
		/// </summary>
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBDecimal(4)]
		[PXUIField(DisplayName = "Reconciled Receipts", Enabled = false, Required = false)]
		public virtual decimal? ReconciledDebits
		{
			get;
			set;
		}
		#endregion
		#region CuryReconciledCredits
		public abstract class curyReconciledCredits : IBqlField
		{
		}
		/// <summary>
		/// The total amount of reconciled <see cref="CAReconEntry.CATranExt.CuryReconciledCredit">disbursements</see> 
		/// on the statement in the selected currency.
		/// </summary>
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBCurrency(typeof(CARecon.curyInfoID), typeof(CARecon.reconciledCredits))]
		[PXUIField(DisplayName = "Reconciled Disb.", Enabled = false)]
		public virtual decimal? CuryReconciledCredits
		{
			get;
			set;
		}
		#endregion
		#region ReconciledCredits
		public abstract class reconciledCredits : IBqlField
		{
		}
		/// <summary>
		/// The total amount of reconciled <see cref="CAReconEntry.CATranExt.ReconciledCredit">disbursements</see> 
		/// on the statement in the base currency.
		/// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Reconciled Disb.", Enabled = false, Required = false)]
		public virtual decimal? ReconciledCredits
		{
			get;
			set;
		}
		#endregion
		#region CuryReconciledBalance
		public abstract class curyReconciledBalance : IBqlField
		{
		}
		/// <summary>
		/// The beginning balance of the statement plus the cleared receipts minus the cleared disbursements.
		/// </summary>
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Reconciled Balance", Enabled = false)]
		[PXDBCury(typeof(CARecon.curyID))]
		[PXFormula(typeof(Add<CARecon.curyBegBalance, CARecon.curyReconciledTurnover>))]
		public virtual decimal? CuryReconciledBalance
		{
			get;
			set;
		}
		#endregion
		#region CuryReconciledTurnover
		public abstract class curyReconciledTurnover : IBqlField
		{
		}
		/// <summary>
		/// Turnover of the reconciliation statement in the selected currency.
		/// </summary>
		/// <value>
		/// Equals the following value: <see cref="CARecon.CuryReconciledDebits"/> minus <see cref="CARecon.CuryReconciledCredits"/>.
		/// </value>
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Reconciled Turnover", Enabled = false)]
		[PXFormula(typeof(Sub<CARecon.curyReconciledDebits, CARecon.curyReconciledCredits>))]
		public virtual decimal? CuryReconciledTurnover
		{
			get;
			set;
		}
		#endregion
		#region ReconciledTurnover
		public abstract class reconciledTurnover : IBqlField
		{
		}
		/// <summary>
		/// Turnover of the reconciliation statement in the base currency.
		/// </summary>
		/// <value>
		/// Equals the following value: <see cref="CARecon.ReconciledDebits"/> minus <see cref="CARecon.ReconciledCredits"/>.
		/// </value>
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Reconciled Turnover", Enabled = false)]
		[PXFormula(typeof(Sub<CARecon.reconciledDebits, CARecon.reconciledCredits>))]
		public virtual decimal? ReconciledTurnover
		{
			get;
			set;
		}
		#endregion
		#region CuryDiffBalance
		public abstract class curyDiffBalance : IBqlField
		{
		}
		/// <summary>
		/// The difference between the <see cref="CARecon.CuryReconciledBalance">reconciled balance</see>
		/// and the <see cref="CARecon.CuryBalance">statement balance</see>.
		/// </summary>
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBCury(typeof(CARecon.curyID))]
		[PXUIField(DisplayName = "Difference", Enabled = false)]
		[PXFormula(typeof(Sub<CARecon.curyBalance, CARecon.curyReconciledBalance>))]
		public virtual decimal? CuryDiffBalance
		{
			get;
			set;
		}
		#endregion
		#region CuryID
		public abstract class curyID : IBqlField
		{
		}
		/// <summary>
		/// The currency of the <see cref="CashAccount">cash account</see>.
		/// </summary>
		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
		[PXUIField(DisplayName = "Currency", Enabled = false)]
		[PXSelector(typeof(Currency.curyID))]
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
		/// <summary>
		/// Identifier of the <see cref="PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the reconciliation statement.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="PX.Objects.CM.CurrencyInfo.CurrencyInfoID"/> field. The value is generated automatically.
		/// </value>
		[PXDBLong]
		[CurrencyInfo]
		public virtual long? CuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region Status
		public abstract class status : IBqlField
		{
		}

		protected string _Status;
		/// <summary>
		/// The current status of the statement.
		/// </summary>
		/// <value>
		/// This field can have one of the values defined 
		/// by <see cref="CADocStatus.ListAttribute"/>.
		/// </value>
		[PXString(1, IsFixed = true)]
		[CADocStatus.List]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual string Status
		{
			[PXDependsOnFields(typeof(reconciled), typeof(voided), typeof(hold))]
			get
			{
				return this._Status;
			}

			set
			{
			}
		}
		#endregion
		#region LineCntr
		[Obsolete("This field is obsolete and will be removed in Acumatica ERP 2018R1.")]
		public abstract class lineCntr : IBqlField
		{
		}
		[Obsolete("This field is obsolete and will be removed in Acumatica ERP 2018R1.")]
		[PXDBInt]
		[PXDefault(0)]
		public virtual int? LineCntr
		{
			get;
			set;
		}
		#endregion
		#region CountDebit
		public abstract class countDebit : IBqlField
		{
		}
		/// <summary>
		/// The number of <see cref="CAReconEntry.CATranExt.CountDebit">reconciled receipts</see>.
		/// </summary>
		[PXDBInt]
		[PXDefault(0)]
		[PXUIField(DisplayName = "Receipt Count", Enabled = false)]
		public virtual int? CountDebit
		{
			get;
			set;
		}
		#endregion
		#region CountCredit
		public abstract class countCredit : IBqlField
		{
		}
		/// <summary>
		/// The number of <see cref="CAReconEntry.CATranExt.CountCredit">reconciled disbursements</see>.
		/// </summary>
		[PXDBInt]
		[PXDefault(0)]
		[PXUIField(DisplayName = "Disb. Count", Enabled = false)]
		public virtual int? CountCredit
		{
			get;
			set;
		}
		#endregion
		#region SkipVoided
		public abstract class skipVoided : IBqlField
		{
		}
		/// <summary>
		/// Specifies (if set to <c>true</c>) that the direct <see cref="CATran">transaction</see>
		/// and the reversing transaction are considered as a single transaction.
		/// </summary>
		/// <value>
		/// Defaults to the value of <see cref="CASetup.SkipVoided"/>.
		/// </value>
		/// <remarks>
		/// The <see cref="CAReconEntry.Skip(CATran, CATran, bool)"/> method is used to determine
		/// whether both transactions (<see cref="CATran"/>) are shown on the form or only the aggregated one is shown.
		/// </remarks>
		[PXDBBool]
		[PXDefault(typeof(CASetup.skipVoided))]
		[PXUIField(DisplayName = "Voided Transactions Are Skipped", Enabled = false, Visible = false)]
		public virtual bool? SkipVoided
		{
			get;
			set;
		}
		#endregion
		#region ShowBatchPayments
		public abstract class showBatchPayments : IBqlField
		{
		}
		/// <summary>
		/// Specifies (if set to <c>true</c>) that all <see cref="CATran">transactions</see> 
		/// included in the <see cref="CABatch">batch</see> are considered as a single transaction.
		/// </summary>
		/// <value>
		/// Defaults to the value of <see cref="CashAccount.MatchToBatch"/>.
		/// </value>
		[PXDBBool]
		[PXDefault(typeof(Search<CashAccount.matchToBatch, Where<CashAccount.cashAccountID, Equal<Current<CARecon.cashAccountID>>>>))]
		[PXUIField(DisplayName = "Bank Transactions Are Matched to Batch Payments", Enabled = false, Visible = false)]
		public virtual bool? ShowBatchPayments
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
		#region NoteID
		public abstract class noteID : IBqlField
		{
		}
		[PXNote(DescriptionField = typeof(CARecon.reconNbr))]
		public virtual Guid? NoteID
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
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
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
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
		#region Methods
		private void SetStatus()
		{
			if (this._Reconciled == true)
			{
				this._Status = CADocStatus.Closed;
			}
			else if (this._Voided == true)
			{
				this._Status = CADocStatus.Voided;
			}
			else if (this._Hold == true)
			{
				this._Status = CADocStatus.Hold;
			}
			else
			{
				this._Status = CADocStatus.Balanced;
			}
		}
		#endregion
	}
   
}
