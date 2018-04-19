﻿using System;
using System.Text.RegularExpressions;

using PX.Data;
using PX.Objects.GL;

namespace PX.Objects.CA
{
    /// <summary>
    /// Represents the rules used to automatically generate documents
    /// from the <see cref="CABankTran">Bank Transactions</see> during
    /// the Auto-Matching process.
    /// </summary>
    [Serializable]
    [PXPrimaryGraph(typeof(CABankTranRuleMaint))]
    [PXCacheName(Messages.CABankTransactionsRuleName)]
    public partial class CABankTranRule : IBqlTable
    {
        #region RuleID
        public abstract class ruleID : IBqlField { }

        /// <summary>
        /// Identifier of the rule. Database autogenerated identity key.
        /// </summary>
        [PXDBIdentity]
        public virtual int? RuleID
        {
            get;
            set;
        }
        #endregion

        #region RuleCD
        public abstract class ruleCD : IBqlField { }

        /// <summary>
        /// Natural identifier of the rule, visible to user.
        /// </summary>
        [PXSelector(typeof(CABankTranRule.ruleCD),
            typeof(CABankTranRule.ruleCD),
            typeof(CABankTranRule.description),
            SubstituteKey = typeof(CABankTranRule.ruleCD),
            DescriptionField = typeof(CABankTranRule.description))]
        [PXDBString(30, IsUnicode = true, IsKey = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Rule", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string RuleCD
        {
            get;
            set;
        }
        #endregion

        #region Description
        public abstract class description : IBqlField { }

        /// <summary>
        /// Description of the rule. Does not affect any functionality.
        /// </summary>
        [PXDBString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual string Description
        {
            get;
            set;
        }
        #endregion

        #region IsActive

        public abstract class isActive : IBqlField { }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Active")]
        public virtual bool? IsActive
        {
            get;
            set;
        }
        #endregion

        #region BankTranCashAccountID
        public abstract class bankTranCashAccountID : IBqlField { }

        /// <summary>
        /// The <see cref="CashAccount">Cash Account</see> for the rule. When specified, the rule will be applied
        /// only to the transactions on this Cash Account.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CashAccount.CashAccountID"/> field.
        /// </value>
        [CashAccount]
        public virtual int? BankTranCashAccountID
        {
            get;
            set;
        }
        #endregion

        #region BankDrCr
        public abstract class bankDrCr : IBqlField { }

        /// <summary>
        /// Indicates whether this rule should be applied to receipt or disbursement bank transactions.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CABankTran.DrCr"/> field. Mandatory.
        /// </value>
        [PXDBString(1, IsFixed = true)]
        [PXDefault(CADrCr.CACredit)]
        [CADrCr.List]
        [PXUIField(DisplayName = "Debit/Credit")]
        public virtual string BankDrCr
        {
            get;
            set;
        }
        #endregion

        #region TranCuryID
        public abstract class tranCuryID : IBqlField { }

        /// <summary>
        /// Code of the <see cref="PX.Objects.CM.Currency">Currency</see>.
        /// If specified, the rule will be applied only to the transactions in the respective currency.
        /// </summary>
        [PXDBString(5, IsUnicode = true)]
        [PXSelector(typeof(PX.Objects.CM.Currency.curyID))]
        [PXUIField(DisplayName = "Currency")]
        public virtual string TranCuryID
        {
            get;
            set;
        }
        #endregion

        #region AmountMatchingMode
        public abstract class amountMatchingMode : IBqlField { }

        /// <summary>
        /// The mode of the transaction amount matching.
        /// </summary>
        /// <value>
        /// The following options are available:
        /// <c>"N"</c> (None): The amount criterion is not used;
        /// <c>"E"</c> (Equal): The transaction amount must be equal to the number specified in the Amount box;
        /// <c>"B"</c> (Between): The transaction amount must be in the range of the numbers specified in the Amount and the Max. Amount boxes.
        /// </value>
        [PXDBString(1, IsFixed = true)]
        [PXDefault(MatchingMode.None)]
        [MatchingMode.Amount]
        [PXUIField(DisplayName = "Amount Matching Mode")]
        public virtual string AmountMatchingMode
        {
            get;
            set;
        }
        #endregion

        #region CuryTranAmt
        public abstract class curyTranAmt : IBqlField { }

        /// <summary>
        /// Transaction amount in the currency of the transaction.
        /// When specified together with appropriate value in the <see cref="AmountMatchingMode"/> field,
        /// the rule will be applied only to the transactions with matching amount.
        /// In case the <see cref="AmountMatchingMode"/> is set to "Between",
        /// the value in this field is used as a lower bound for transaction amount.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CABankTran.CuryTranAmt"/> field.
        /// </value>
        [PXDBDecimal(4)]
        [PXDefault]
        [PXUIField(DisplayName = "Amount")]
        public virtual decimal? CuryTranAmt
        {
            get;
            set;
        }
        #endregion

        #region MaxCuryTranAmt
        public abstract class maxCuryTranAmt : IBqlField { }

        /// <summary>
        /// Maximum transaction amount in the currency of the transaction, which the rule should match.
        /// Used only when the <see cref="AmountMatchingMode"/> is set to "Between"
        /// </summary>
        /// <value>
        /// Matching is done on the <see cref="CABankTran.CuryTranAmt"/> field.
        /// </value>
        [PXDBDecimal(4)]
        [PXDefault]
        [PXUIField(DisplayName = "Max. Amount")]
        public virtual decimal? MaxCuryTranAmt
        {
            get;
            set;
        }
        #endregion

        #region TranCode
        public abstract class tranCode : PX.Data.IBqlField
        {
        }

        /// <summary>
        /// The transaction code that the rule should match.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CABankTran.TranCode"/> field.
        /// </value>
        [PXDBString(35, IsUnicode = true)]
        [PXUIField(DisplayName = "Tran. Code")]
        public virtual string TranCode
        {
            get;
            set;
        }
        #endregion

        #region BankTranDescription
        public abstract class bankTranDescription : IBqlField { }

        /// <summary>
        /// Represents the text used to identify transactions, to which the rule should be applied.
        /// Transaction must contain the text in the <see cref="CABankTran.TranDesc">description</see>.
        /// </summary>
        [PXDBString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual string BankTranDescription
        {
            get;
            set;
        }
        #endregion

        #region MatchDescriptionCase
        public abstract class matchDescriptionCase : IBqlField { }

        /// <summary>
        /// Indicates whether the case should be matched when the system attempts to apply the rule.
        /// </summary>
        /// <value>
        /// When set to <c>true</c> the <see cref="CABankTran.TranDesc">description</see> of the transactions
        /// will be scanned for presence of the <see cref="BankTranDescription">specified text</see>
        /// with respect to case. Otherwise, the case will be ignored during string comparison.
        /// Defaults to <c>false</c>.
        /// </value>
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Match Case")]
        public virtual bool? MatchDescriptionCase
        {
            get;
            set;
        }
        #endregion

		#region UseDescriptionWildcards
		public abstract class useDescriptionWildcards : IBqlField { }

		/// <summary>
		/// When set to <c>true</c>, indicates that the system must treat '*' and '?' characters in the <see cref="BankTranDescription" />
		/// as wildcards ('*' matches 0 or more arbitrary symbols, '?' matches one arbitrary symbol).
		/// </summary>
		/// <value>
		/// Defaults to <c>false</c>.
		/// </value>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Use Wildcards")]
		public virtual bool? UseDescriptionWildcards
		{
			get;
			set;
		}

		#endregion

        #region Pattern

        private string Pattern
        {
            get
            {
				var escaped = Regex.Escape(BankTranDescription ?? "");

				return UseDescriptionWildcards == true ?
					"^" + escaped.Replace("\\*", ".*").Replace("\\?", ".") + "$"
					: escaped;
            }
        }

        public Regex Regex => new Regex(Pattern,
            MatchDescriptionCase == true ? new RegexOptions() : RegexOptions.IgnoreCase);

        #endregion

        #region action
        public abstract class action : IBqlField { }

        [PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Action")]
        [PXDefault(RuleAction.CreateDocument)]
        [RuleAction.List]
        public virtual string Action
        {
            get;
            set;
        }
        #endregion

        #region DocumentModule
        public abstract class documentModule : IBqlField { }

        /// <summary>
        /// The module of the document generated by the rule.
        /// </summary>
        /// <value>
        /// Default is "CA". Now this is the only supported value. Mandatory.
        /// </value>
        [PXDBString(2, IsFixed = true)]
        [PXDefault(BatchModule.CA)]
        [PXUIField(DisplayName = "Resulting Document Module", Visible = false, Enabled = false)]
        public virtual string DocumentModule
        {
            get;
            set;
        }
        #endregion

        #region DocumentEntryTypeID
        public abstract class documentEntryTypeID : IBqlField { }

        /// <summary>
        /// Identifier of the <see cref="CAEntryType"/> of the document generated by the rule.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CAEntryType.EntryTypeID"/> field. Mandatory.
        /// </value>
        [PXDBString(10, IsUnicode = true)]
        [PXSelector(typeof(
			Search2<CAEntryType.entryTypeId,
			LeftJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>,
			And<CashAccountETDetail.accountID, Equal<Current<CABankTranRule.bankTranCashAccountID>>>>>,
				Where<CAEntryType.module, Equal<Current<CABankTranRule.documentModule>>,
					And<CAEntryType.drCr, Equal<Current<CABankTranRule.bankDrCr>>,
					And<Where<CashAccountETDetail.accountID, Equal<Current<CABankTranRule.bankTranCashAccountID>>, 
					Or<Current<CABankTranRule.bankTranCashAccountID>, IsNull>>>>>>),
            DescriptionField = typeof(CAEntryType.descr))]
        [PXUIField(DisplayName = "Resulting Entry Type")]
        [PXDefault]
        public virtual string DocumentEntryTypeID
        {
            get;
            set;
        }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }

        [PXNote]
        public virtual Guid? NoteID
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
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
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
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? LastModifiedDateTime
        {
            get;
            set;
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
    }
	[Serializable]
	[PXPrimaryGraph(typeof(CABankTranRuleMaintPopup))]
	public partial class CABankTranRulePopup : CABankTranRule { }

    public class MatchingMode
    {
        public class AmountAttribute : PXStringListAttribute
        {
            public AmountAttribute() : base(
                    new[] { None, Equal, Between },
					new[] { Messages.MatchModeNone, Messages.MatchModeEqual, Messages.MatchModeBetween }) { }
        }

        public const string None = "N";
        public const string Equal = "E";
        public const string Between = "B";
    }

    public class RuleAction
    {
        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute() : base(
                    new[] { CreateDocument, HideTransaction },
                    new[] { Messages.CreateDocument, Messages.HideTran })
            {
            }
        }

        public const string CreateDocument = "C";
        public const string HideTransaction = "H";
    }
}