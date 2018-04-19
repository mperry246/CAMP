using System;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;


namespace PX.Objects.CA
{
    /// <summary>
    /// Contains the main properties of cash managment preferences and their classes.
    /// Cash managment preferences are edited on the Cash Managment Preferences (CA101000) form (which corresponds to the <see cref="CASetupMaint"/> graph).
    /// </summary>
    [Serializable]
    [PXPrimaryGraph(typeof(CASetupMaint))]
    [PXCacheName(Messages.CASetup)]
    public partial class CASetup : IBqlTable
	{
		#region BatchNumberingID
		public abstract class batchNumberingID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The numbering sequence to be used for the identifiers of batches originating in the Cash Management module.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="Numbering.NumberingID"/> field.
        /// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("BATCH")]
		[PXSelector(typeof(Numbering.numberingID),
					 DescriptionField = typeof(Numbering.descr))]
		[PXUIField(DisplayName = "Batch Numbering Sequence")]
		public virtual string BatchNumberingID
		{
			get;
			set;
		}
		#endregion
		#region RegisterNumberingID
		public abstract class registerNumberingID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The numbering sequence to be used for the identifiers of cash adjustments or direct cash transactions.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="Numbering.NumberingID"/> field.
        /// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("CATRAN")]
		[PXSelector(typeof(Numbering.numberingID),
					 DescriptionField = typeof(Numbering.descr))]
		[PXUIField(DisplayName = "Transaction Numbering Sequence")]
		public virtual string RegisterNumberingID
			{
			get;
			set;
		}
		#endregion
		#region TransferNumberingID
		public abstract class transferNumberingID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The numbering sequence to be used for the identifiers of transfers.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="Numbering.NumberingID"/> field.
        /// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("CATRANSFER")]
		[PXSelector(typeof(Numbering.numberingID),
					 DescriptionField = typeof(Numbering.descr))]
		[PXUIField(DisplayName = "Transfer Numbering Sequence")]
		public virtual string TransferNumberingID
			{
			get;
			set;
		}
		#endregion
		#region CABatchNumberingID
		public abstract class cABatchNumberingID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The numbering sequence to be used for the identifiers of batches of payments.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="Numbering.NumberingID"/> field.
        /// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("CABATCH")]
		[PXSelector(typeof(Numbering.numberingID),
					 DescriptionField = typeof(Numbering.descr))]
		[PXUIField(DisplayName = "Payment Batch Numbering Sequence")]
		public virtual string CABatchNumberingID
		{
			get;
			set;
		}
		#endregion
		#region CAStatementNumberingID
		public abstract class cAStatementNumberingID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The numbering sequence to be used for the identifiers of bank statements.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="Numbering.NumberingID"/> field.
        /// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("CABANKSTMT")]
		[PXSelector(typeof(Numbering.numberingID),
					 DescriptionField = typeof(Numbering.descr))]
		[PXUIField(DisplayName = "Bank Statement Numbering Sequence")]
		public virtual string CAStatementNumberingID
			{
			get;
			set;
		}
		#endregion
		#region CAImportPaymentsNumberingID
		public abstract class cAImportPaymentsNumberingID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The numbering sequence to be used for the identifiers of imported payments.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="Numbering.NumberingID"/> field.
        /// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXSelector(typeof(Numbering.numberingID),
					 DescriptionField = typeof(Numbering.descr))]
		[PXUIField(DisplayName = "Payments Import Numbering Sequence")]
		public virtual string CAImportPaymentsNumberingID
			{
			get;
			set;
		}
		#endregion
		#region TransitAcctId
		public abstract class transitAcctId : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The special multi-currency asset account used (when necessary) as an intermediate account for currency conversions performed during funds transfers.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="Account.accountID"/> field.
        /// </value>
		[PXDefault]
		[PXNonCashAccount(DisplayName = "Cash-In-Transit Account",
		   DescriptionField = typeof(Account.description))]
		public virtual int? TransitAcctId
			{
			get;
			set;
		}
		#endregion
		#region TransitSubID
		public abstract class transitSubID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The special multi-currency asset subaccount used (when necessary) as an intermediate account for currency conversions performed during funds transfers.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[SubAccount(typeof(CASetup.transitAcctId), DisplayName = "Cash-In-Transit Subaccount", DescriptionField = typeof(Sub.description))]
		public virtual int? TransitSubID
		{
			get;
			set;
		}
		#endregion
		#region RequireControlTotal
		public abstract class requireControlTotal : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the Control Total box is added to the Summary area of the Transactions (CA304000) form, 
        /// so a user can enter the transaction amount manually. This amount will be validated when users enter transactions.
        /// </summary>
        [PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Validate Control Totals on Entry")]
		public virtual bool? RequireControlTotal
			{
			get;
			set;
		}
		#endregion
		#region RequireControlTaxTotal

	    public abstract class requireControlTaxTotal : IBqlField
	    {
	    }

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the Tax Amount box is added to the Summary area of the Transactions (CA304000) form,
        /// so a user can enter the tax amount manually in the transaction.
        /// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Validate Tax Totals on Entry")]
	    public virtual bool? RequireControlTaxTotal
	    {
	        get;
            set;
	    }
		#endregion
		#region HoldEntry
		public abstract class holdEntry : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that newly entered transactions will by default get the On Hold status on entry.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Hold Transactions on Entry")]
		public virtual bool? HoldEntry
			{
			get;
			set;
		}
        #endregion
        #region CuryRateTypeID
        [Obsolete("This field is obsolete and will be removed in Acumatica 8.0.")]
        public abstract class curyRateTypeID : PX.Data.IBqlField
		{
		}

        [Obsolete("This field is obsolete and will be removed in Acumatica 8.0.")]
        [PXDBString(6, IsUnicode = true)]
		[PXSelector(typeof(CurrencyRateType.curyRateTypeID), DescriptionField = typeof(CurrencyRateType.descr))]
		[PXUIField(DisplayName = "Default Rate Type")]
		public virtual string CuryRateTypeID
			{
			get;
			set;
		}
		#endregion
		#region ReleaseAP
		public abstract class releaseAP : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that Accounts Payable documents can be released from the Cash Management module. 
        /// If the value of the field is <c>false</c>, Accounts Payable documents can be released only from the Accounts Payable module.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Release AP Documents from CA Module")]
		public virtual bool? ReleaseAP
		{
			get;
			set;
		}
		#endregion
		#region ReleaseAR
		public abstract class releaseAR : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that Accounts Receivable documents can be released from the Cash Management module. 
        /// If the value of the field is <c>false</c>, Accounts Receivable documents can be released only from the Accounts Receivable module.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Release AR Documents from CA Module")]
		public virtual bool? ReleaseAR
			{
			get;
			set;
		}
		#endregion
		#region CalcBalDebitUnclearedUnreleased
		public abstract class calcBalDebitUnclearedUnreleased : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that unreleased and uncleared receipts are included in calculation of available balances of cash accounts.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Unreleased Uncleared")]
		public virtual bool? CalcBalDebitUnclearedUnreleased
			{
			get;
			set;
		}
		#endregion
		#region CalcBalDebitClearedUnreleased
		public abstract class calcBalDebitClearedUnreleased : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that unreleased and cleared receipts are included in calculation of available balances of cash accounts.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Unreleased Cleared")]
		public virtual bool? CalcBalDebitClearedUnreleased
			{
			get;
			set;
		}
		#endregion
		#region CalcBalDebitUnclearedReleased
		public abstract class calcBalDebitUnclearedReleased : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that released and uncleared receipts are included in calculation of available balances of cash accounts.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Released Uncleared")]
		public virtual bool? CalcBalDebitUnclearedReleased
			{
			get;
			set;
		}
		#endregion
		#region CalcBalCreditUnclearedUnreleased
		public abstract class calcBalCreditUnclearedUnreleased : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that unreleased and uncleared disbursements are included in calculation of available balances of cash accounts.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Unreleased Uncleared")]
		public virtual bool? CalcBalCreditUnclearedUnreleased
			{
			get;
			set;
		}
		#endregion
		#region CalcBalCreditClearedUnreleased
		public abstract class calcBalCreditClearedUnreleased : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that unreleased and cleared disbursements are included in calculation of available balances of cash accounts.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Unreleased Cleared")]
		public virtual bool? CalcBalCreditClearedUnreleased
			{
			get;
			set;
		}
		#endregion
		#region CalcBalCreditUnclearedReleased
		public abstract class calcBalCreditUnclearedReleased : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that released and uncleared disbursements are included in calculation of available balances of cash accounts.
        /// </summary>
        [PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Released Uncleared")]
		public virtual bool? CalcBalCreditUnclearedReleased
			{
			get;
			set;
		}
		#endregion
		#region AutoPostOption
		public abstract class autoPostOption : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that cash transactions are automatically posted to General Ledger on release.
        /// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Automatically Post to GL on Release", Visibility = PXUIVisibility.Visible)]
		public virtual bool? AutoPostOption
			{
			get;
			set;
		}
		#endregion
		#region DateRangeDefault
		public abstract class dateRangeDefault : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The range (starting with the current business date) to be used by default on Cash Management reports: Day, Week, Month, or Financial Period.
        /// </summary>
		[PXDBString(1, IsFixed = true)]
		[PXDefault(CADateRange.Week)]
		[PXUIField(DisplayName = "Default Date Range")]
		[CADateRange.List]
		public virtual string DateRangeDefault
			{
			get;
			set;
		}
		#endregion		
		#region RequestApproval
		public abstract class requestApproval : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that cash transactions should be approved before they can be released.
        /// </summary>
		[EPRequireApproval]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Require Approval")]
		public virtual bool? RequestApproval
			{
			get;
			set;
		}
		#endregion

		#region ReceiptTranDaysBefore
		public abstract class receiptTranDaysBefore : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// For the system to categorize a cash transaction as matching, the maximum number of days
        /// between the date of a cash transaction (disbursement) and the selected transaction on the bank statement.
        /// </summary>
		[PXDBInt(MinValue = 0, MaxValue = 365)]
		[PXDefault(5, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Days before bank transaction date")]
		public virtual int? ReceiptTranDaysBefore
			{
			get;
			set;
		}
		#endregion
		#region ReceiptTranDaysAfter
		public abstract class receiptTranDaysAfter : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// For the system to categorize a cash transaction as matching, the maximum number of days
        /// between the date of the selected transaction on the bank statement and the date of a receipt.
        /// </summary>
		[PXDBInt(MinValue = 0, MaxValue = 365)]
		[PXDefault(5, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Days after bank transaction date")]
		public virtual int? ReceiptTranDaysAfter
		{
			get;
			set;
		}
		#endregion
		#region DisbursementTranDaysBefore
		public abstract class disbursementTranDaysBefore : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// For the system to categorize a cash transaction as matching, the maximum number of days 
        /// between the date of a cash transaction (receipt) and the selected transaction on the bank statement.
        /// </summary>
		[PXDBInt(MinValue = 0, MaxValue = 365)]
		[PXDefault(5, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Days before bank transaction date")]
		public virtual int? DisbursementTranDaysBefore
			{
			get;
			set;
		}
		#endregion
		#region DisbursementTranDaysAfter
		public abstract class disbursementTranDaysAfter : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// For the system to categorize a cash transaction as matching, the maximum number of days 
        /// between the date of the selected transaction on the bank statement and the date of a receipt.
        /// </summary>
		[PXDBInt(MinValue = 0, MaxValue = 365)]
		[PXDefault(5, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Days after bank transaction date")]
		public virtual int? DisbursementTranDaysAfter
			{
			get;
			set;
		}
		#endregion
		#region AllowMatchingCreditMemo
		public abstract class allowMatchingCreditMemo : PX.Data.IBqlField
		{
		}

		/// <summary>
		/// Specifies (if set to <c>true</c>) that credit memo are available for matching to bank transactions on the Process Bank Transactions (CA306000) form. 
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Allow Matching to Credit Memo")]
		public virtual bool? AllowMatchingCreditMemo
		{
			get;
			set;
		}
		#endregion
		#region RefNbrCompareWeight
		public abstract class refNbrCompareWeight : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The relative weight of the evaluated difference between the reference numbers of the bank transaction and the cash transaction.
        /// </summary>
		[PXDBDecimal(MinValue = 0, MaxValue = 100.0)]
		[PXDefault(TypeCode.Decimal, "70.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Ref. Nbr. Weight")]
		public virtual decimal? RefNbrCompareWeight
		{
			get;
			set;
		}
		#endregion
		#region DateCompareWeight
		public abstract class dateCompareWeight : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The relative weight of the evaluated difference between the dates of the bank transaction and the cash transaction.
        /// </summary>
		[PXDBDecimal(MinValue = 0, MaxValue = 100)]
		[PXDefault(TypeCode.Decimal, "20.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Doc. Date Weight")]
		public virtual decimal? DateCompareWeight
			{
			get;
			set;
		}
		#endregion
		#region PayeeCompareWeight
		public abstract class payeeCompareWeight : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The relative weight of the evaluated difference between the names of the customer (or vendor) on the bank transaction and the cash transaction.
        /// </summary>
		[PXDBDecimal(MinValue = 0, MaxValue = 100)]
		[PXDefault(TypeCode.Decimal, "10.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Doc. Payee Weight")]
		public virtual decimal? PayeeCompareWeight
			{
			get;
			set;
		}
		#endregion
		#region DateMeanOffset
		public abstract class dateMeanOffset : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The average number of days for a payment to be cleared with the bank.
        /// </summary>
		[PXDBDecimal(MinValue = -365, MaxValue = 365)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Payment Clearing Average Delay")]
		public virtual decimal? DateMeanOffset
			{
			get;
			set;
		}
		#endregion
		#region DateSigma
		public abstract class dateSigma : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The number of days before and after the average delay date during which 50% of payments are generally cleared.
        /// </summary>
		[PXDBDecimal(MinValue = 0, MaxValue = 365)]
		[PXDefault(TypeCode.Decimal, "5.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Estimated Deviation (days)")]
		public virtual decimal? DateSigma
			{
			get;
			set;
		}
        #endregion

        /// <summary>
        /// The total of relative weights used in relevance calculation for auto-matching of transactions and for validation of manual matching.
        /// The field is virtual and has no representation in the database.
        /// The field is invisible.
        /// </summary>
        protected decimal TotalWeight
		{
			get
			{
				decimal total = (this.DateCompareWeight ?? decimal.Zero)
								+ (this.RefNbrCompareWeight ?? decimal.Zero)
								+ (this.PayeeCompareWeight ?? decimal.Zero);
				return total;
			}
		}
		#region RefNbrComparePercent
		public abstract class refNbrComparePercent : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The relative weight of the evaluated difference between the reference numbers of the bank transaction and the cash transaction
        /// expressed in percentage of the <see cref="TotalWeight">total weight</see>/>.
        /// This field is virtual and has no representation in the database.
        /// </summary>
		[PXDecimal]
		[PXUIField(DisplayName = "%", Enabled = false)]
		public virtual decimal? RefNbrComparePercent
		{
			[PXDependsOnFields(typeof(refNbrCompareWeight), typeof(dateCompareWeight), typeof(payeeCompareWeight))] 
			get
			{
				return (this.TotalWeight != decimal.Zero ? (this.RefNbrCompareWeight / this.TotalWeight) : decimal.Zero) * 100.0m;
			}

			set
			{
			}
		}
		#endregion
		#region EmptyRefNbrMatching
		public abstract class emptyRefNbrMatching : IBqlField
		{
		}
		/// <summary>
		/// Specifies (if set to <c>true</c>) that batch payments with empty ref number are available for matching to bank transactions on the Process Bank Transactions (CA306000) form. 
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Consider Empty Ref. Nbr. as matching", Visibility = PXUIVisibility.Visible)]
		public virtual bool? EmptyRefNbrMatching { get; set; }
		#endregion EmptyRefNbrMatching
		#region DateComparePercent
		public abstract class dateComparePercent : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The relative weight of the evaluated difference between the dates of the bank transaction and the cash transaction
        /// expressed in percentage of the <see cref="TotalWeight">total weight</see>/>.
        /// This field is virtual and has no representation in the database.
        /// </summary>
		[PXDecimal]
		[PXUIField(DisplayName = "%", Enabled = false)]
		public virtual decimal? DateComparePercent
		{
			[PXDependsOnFields(typeof(refNbrCompareWeight), typeof(dateCompareWeight), typeof(payeeCompareWeight))] 
			get
			{
				return (this.TotalWeight != decimal.Zero ? (this.DateCompareWeight / this.TotalWeight) : decimal.Zero) * 100.0m;
			}

			set
			{
			}
		}
		#endregion
		#region PayeeComparePercent
		public abstract class payeeComparePercent : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The relative weight of the evaluated difference between the names of the customer (or vendor) on the bank transaction and the cash transaction
        /// expressed in percentage of the <see cref="TotalWeight">total weight</see>.
        /// This field is virtual and has no representation in the database.
        /// </summary>
		[PXDecimal]
		[PXUIField(DisplayName = "%", Enabled = false)]
		public virtual decimal? PayeeComparePercent
		{
			[PXDependsOnFields(typeof(refNbrCompareWeight), typeof(dateCompareWeight), typeof(payeeCompareWeight))] 
			get
			{
				return (this.TotalWeight != decimal.Zero ? (this.PayeeCompareWeight / this.TotalWeight) : decimal.Zero) * 100.0m;
			}

			set
			{
			}
		}
		#endregion
		#region MatchInSelection
        [Obsolete("This field is obsolete and will be removed in Acumatica 8.0.")]
		public abstract class matchInSelection : PX.Data.IBqlField
		{
		}

        [Obsolete("This field is obsolete and will be removed in Acumatica 8.0.")]
        [PXDBBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Match in the selection only", Visible = false)]
		public virtual bool? MatchInSelection
			{
			get;
			set;
		}
		#endregion
		#region IgnoreCuryCheckOnImport
		public abstract class ignoreCuryCheckOnImport : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the system ignores the currency check when you import bank statements. 
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Ignore Currency Check on Bank Statement Import")]
		public virtual bool? IgnoreCuryCheckOnImport
			{
			get;
			set;
		}
		#endregion
		#region ImportToSingleAccount
		public abstract class importToSingleAccount : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that importing a bank statement from a file should be performed to only a specific cash account.
        /// The user can import the data only after the user has selected the cash account on the Import Bank Transactions (CA306500) form.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Import Bank Statement to Single Cash Account")]
		public virtual bool? ImportToSingleAccount
			{
			get;
			set;
		}
		#endregion
		#region AllowEmptyFITID
		public abstract class allowEmptyFITID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the system ignores the empty FITID value  (the value, which is uploaded to the Ext. Tran. ID column 
        /// on the Import Bank Transactions (CA306500) form) when you import the bank statement file.
        /// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Allow Empty FITID")]
		public virtual bool? AllowEmptyFITID
			{
			get;
			set;
		}
		#endregion
		#region AllowMatchingToUnreleasedBatch
		public abstract class allowMatchingToUnreleasedBatch : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that unreleased batch payments are available for matching to bank transactions on the Process Bank Transactions (CA306000) form. 
        /// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Allow Matching to Unreleased Batch Payments")]
		public virtual bool? AllowMatchingToUnreleasedBatch
			{
			get;
			set;
		}
		#endregion
		#region UnknownPaymentEntryTypeID
		public abstract class unknownPaymentEntryTypeID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The entry type (configured for processing unrecognized payments and for payment reclassification) to be used by default on the Reclassify Payments (CA506500) form.
        /// </summary>
		[PXDBString(10, IsUnicode = true)]
		[PXSelector(typeof(Search<CAEntryType.entryTypeId,
									Where<CAEntryType.module, Equal<BatchModule.moduleCA>,
									And<CAEntryType.useToReclassifyPayments, Equal<True>>>>), DescriptionField = typeof(CAEntryType.descr))]
		[PXUIField(DisplayName = "Unrecognized Receipts Type", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string UnknownPaymentEntryTypeID
			{
			get;
			set;
		}
		#endregion
		#region StatementImportTypeName
		public abstract class statementImportTypeName : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The service to be used for importing bank statements if the Import Bank Statements to Single Cash Account check box is cleared. 
        /// </summary>
		[PXDBString(255)]
		[PXUIField(DisplayName = "Statement Import Service")]
		[CA.PXProviderTypeSelector(typeof(IStatementReader))]
		public virtual string StatementImportTypeName
			{
			get;
			set;
		}
		#endregion
        #region SkipVoided
        public abstract class skipVoided : PX.Data.IBqlField
        {
        }

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the system skip voided transactions during the reconciliation process if both the original 
        /// and the voided transactions are registered in the same financial period.
        /// </summary>
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Skip Voided Transactions")]
        public virtual bool? SkipVoided
            {
            get;
            set;
        }
        #endregion
        #region RequireExtRefNbr
        public abstract class requireExtRefNbr : PX.Data.IBqlField
        {
        }

        /// <summary>
        /// Specifies (if set to <c>true</c>) that users must fill in the Document Ref. box for new cash transactions and deposits.
        /// </summary>
        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Require Document Ref. Nbr. on Entry")]
        public virtual bool? RequireExtRefNbr
            {
            get;
            set;
        }
		#endregion
		#region ValidateDataConsistencyOnRelease
		public abstract class validateDataConsistencyOnRelease : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the system validates data consistency on the release of cash transactions and deposits.
        /// The validation is described in the <see cref="CAReleaseProcess.CheckMultipleGLPosting"/> method.
        /// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Validate data consistency on Release", Visible = false)]
	    public virtual bool? ValidateDataConsistencyOnRelease
	    {
	        get;
            set;
	    }
		#endregion

		#region ReceiptTranDaysBeforeIncPayments
		public abstract class receiptTranDaysBeforeIncPayments : IBqlField
		{
		}

		/// <summary>
		/// For the system to categorize a cash transaction as matching, the maximum number of days
		/// between the date of a cash transaction (disbursement) and the selected transaction on the incoming payments.
		/// </summary>
		[PXDBInt(MinValue = 0, MaxValue = 365)]
		[PXDefault(5, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Days before bank transaction date")]
		public virtual int? ReceiptTranDaysBeforeIncPayments { get; set; }
		#endregion
		#region ReceiptTranDaysAfterIncPayments
		public abstract class receiptTranDaysAfterIncPayments : IBqlField
		{
		}

		/// <summary>
		/// For the system to categorize a cash transaction as matching, the maximum number of days
		/// between the date of the selected transaction on the incoming payments and the date of a receipt.
		/// </summary>
		[PXDBInt(MinValue = 0, MaxValue = 365)]
		[PXDefault(5, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Days after bank transaction date")]
		public virtual int? ReceiptTranDaysAfterIncPayments { get; set; }
		#endregion
		#region DisbursementTranDaysBeforeIncPayments
		public abstract class disbursementTranDaysBeforeIncPayments : IBqlField
		{
		}

		/// <summary>
		/// For the system to categorize a cash transaction as matching, the maximum number of days 
		/// between the date of a cash transaction (receipt) and the selected transaction on the incoming payments.
		/// </summary>
		[PXDBInt(MinValue = 0, MaxValue = 365)]
		[PXDefault(5, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Days before bank transaction date")]
		public virtual int? DisbursementTranDaysBeforeIncPayments { get; set; }
		#endregion
		#region DisbursementTranDaysAfterIncPayments
		public abstract class disbursementTranDaysAfterIncPayments : IBqlField
		{
		}

		/// <summary>
		/// For the system to categorize a cash transaction as matching, the maximum number of days 
		/// between the date of the selected transaction on the incoming payments and the date of a receipt.
		/// </summary>
		[PXDBInt(MinValue = 0, MaxValue = 365)]
		[PXDefault(5, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Days after bank transaction date")]
		public virtual int? DisbursementTranDaysAfterIncPayments { get; set; }
		#endregion
		#region AllowMatchingCreditMemoIncPayments
		public abstract class allowMatchingCreditMemoIncPayments : IBqlField
		{
		}

		/// <summary>
		/// Specifies (if set to <c>true</c>) that credit memo are available for matching to incoming payments on the Process Bank Transactions (CA306000) form. 
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Allow Matching to Credit Memo")]
		public virtual bool? AllowMatchingCreditMemoIncPayments { get; set; }
		#endregion
		#region RefNbrCompareWeightIncPayments
		public abstract class refNbrCompareWeightIncPayments : IBqlField
		{
		}

		/// <summary>
		/// The relative weight of the evaluated difference between the reference numbers of the incoming payments and the cash transaction.
		/// </summary>
		[PXDBDecimal(MinValue = 0, MaxValue = 100.0)]
		[PXDefault(TypeCode.Decimal, "70.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Ref. Nbr. Weight")]
		public virtual decimal? RefNbrCompareWeightIncPayments { get; set; }
		#endregion
		#region DateCompareWeightIncPayments
		public abstract class dateCompareWeightIncPayments : IBqlField
		{
		}

		/// <summary>
		/// The relative weight of the evaluated difference between the dates of the incoming payments and the cash transaction.
		/// </summary>
		[PXDBDecimal(MinValue = 0, MaxValue = 100)]
		[PXDefault(TypeCode.Decimal, "20.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Doc. Date Weight")]
		public virtual decimal? DateCompareWeightIncPayments { get; set; }
		#endregion
		#region PayeeCompareWeightIncPayments
		public abstract class payeeCompareWeightIncPayments : IBqlField
		{
		}

		/// <summary>
		/// The relative weight of the evaluated difference between the names of the customer (or vendor) on the incoming payments and the cash transaction.
		/// </summary>
		[PXDBDecimal(MinValue = 0, MaxValue = 100)]
		[PXDefault(TypeCode.Decimal, "10.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Doc. Payee Weight")]
		public virtual decimal? PayeeCompareWeightIncPayments { get; set; }
		#endregion
		#region DateMeanOffsetIncPayments
		public abstract class dateMeanOffsetIncPayments : IBqlField
		{
		}

		/// <summary>
		/// The average number of days for a payment to be cleared with the bank.
		/// </summary>
		[PXDBDecimal(MinValue = -365, MaxValue = 365)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Payment Clearing Average Delay")]
		public virtual decimal? DateMeanOffsetIncPayments { get; set; }
		#endregion
		#region DateSigmaIncPayments
		public abstract class dateSigmaIncPayments : IBqlField
		{
		}

		/// <summary>
		/// The number of days before and after the average delay date during which 50% of payments are generally cleared.
		/// </summary>
		[PXDBDecimal(MinValue = 0, MaxValue = 365)]
		[PXDefault(TypeCode.Decimal, "5.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Estimated Deviation (days)")]
		public virtual decimal? DateSigmaIncPayments { get; set; }
		#endregion

		/// <summary>
		/// The total of relative weights used in relevance calculation for auto-matching of transactions and for validation of manual matching.
		/// The field is virtual and has no representation in the database.
		/// The field is invisible.
		/// </summary>
		protected decimal TotalWeightIncPayments
		{
			get
			{
				decimal total = (this.DateCompareWeightIncPayments ?? decimal.Zero)
								+ (this.RefNbrCompareWeightIncPayments ?? decimal.Zero)
								+ (this.PayeeCompareWeightIncPayments ?? decimal.Zero);
				return total;
			}
		}
		#region RefNbrComparePercentIncPayments
		public abstract class refNbrComparePercentIncPayments : IBqlField
		{
		}

		/// <summary>
		/// The relative weight of the evaluated difference between the reference numbers of the incoming payments and the cash transaction
		/// expressed in percentage of the <see cref="TotalWeight">total weight</see>/>.
		/// This field is virtual and has no representation in the database.
		/// </summary>
		[PXDecimal]
		[PXUIField(DisplayName = "%", Enabled = false)]
		public virtual decimal? RefNbrComparePercentIncPayments
		{
			[PXDependsOnFields(typeof(refNbrCompareWeightIncPayments), typeof(dateCompareWeightIncPayments), typeof(payeeCompareWeightIncPayments))]
			get
			{
				return (this.TotalWeightIncPayments != decimal.Zero ? (this.RefNbrCompareWeightIncPayments / this.TotalWeightIncPayments) : decimal.Zero) * 100.0m;
			}

			set
			{
			}
		}
		#endregion
		#region EmptyRefNbrMatchingIncPayments
		public abstract class emptyRefNbrMatchingIncPayments : IBqlField
		{
		}
		/// <summary>
		/// Specifies (if set to <c>true</c>) that the cash transaction with empty ref number are available for matching to the incoming payments 
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Consider Empty Ref. Nbr. as matching", Visibility = PXUIVisibility.Visible)]
		public virtual bool? EmptyRefNbrMatchingIncPayments { get; set; }
		#endregion EmptyRefNbrMatching
		#region DateComparePercentIncPayments
		public abstract class dateComparePercentIncPayments : IBqlField
		{
		}

		/// <summary>
		/// The relative weight of the evaluated difference between the dates of the incoming payments and the cash transaction
		/// expressed in percentage of the <see cref="TotalWeight">total weight</see>/>.
		/// This field is virtual and has no representation in the database.
		/// </summary>
		[PXDecimal]
		[PXUIField(DisplayName = "%", Enabled = false)]
		public virtual decimal? DateComparePercentIncPayments
		{
			[PXDependsOnFields(typeof(refNbrCompareWeightIncPayments), typeof(dateCompareWeightIncPayments), typeof(payeeCompareWeightIncPayments))]
			get
			{
				return (this.TotalWeightIncPayments != decimal.Zero ? (this.DateCompareWeightIncPayments / this.TotalWeightIncPayments) : decimal.Zero) * 100.0m;
			}

			set
			{
			}
		}
		#endregion
		#region PayeeComparePercentIncPayments
		public abstract class payeeComparePercentIncPayments : IBqlField
		{
		}

		/// <summary>
		/// The relative weight of the evaluated difference between the names of the customer (or vendor) on the incoming payments and the cash transaction
		/// expressed in percentage of the <see cref="TotalWeight">total weight</see>.
		/// This field is virtual and has no representation in the database.
		/// </summary>
		[PXDecimal]
		[PXUIField(DisplayName = "%", Enabled = false)]
		public virtual decimal? PayeeComparePercentIncPayments
		{
			[PXDependsOnFields(typeof(refNbrCompareWeightIncPayments), typeof(dateCompareWeightIncPayments), typeof(payeeCompareWeightIncPayments))]
			get
			{
				return (this.TotalWeightIncPayments != decimal.Zero ? (this.PayeeCompareWeightIncPayments / this.TotalWeightIncPayments) : decimal.Zero) * 100.0m;
			}

			set
			{
			}
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
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
		public abstract class createdByID : PX.Data.IBqlField
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
		public abstract class createdByScreenID : PX.Data.IBqlField
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
		public abstract class createdDateTime : PX.Data.IBqlField
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
		public abstract class lastModifiedByID : PX.Data.IBqlField
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
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
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
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
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

	public class CADateRange
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Day, Week, Month, Period},
				new string[] { Messages.Day, Messages.Week, Messages.Month, Messages.Period }) { }
		}

		public const string Day = "D";
		public const string Week = "W";
		public const string Month = "M";
		public const string Period = "P";
	}
}
