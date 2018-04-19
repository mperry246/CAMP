using System;
using PX.Data;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Data.ReferentialIntegrity.Attributes;

namespace PX.Objects.CA
{
	[PXCacheName(Messages.CashAccount)]
	[Serializable]
	[PXPrimaryGraph(
		new Type[] { typeof(CashAccountMaint) },
		new Type[] { typeof(Select<CashAccount,
			Where<CashAccount.cashAccountID, Equal<Current<CashAccount.cashAccountID>>>>)
		})]
	public partial class CashAccount : IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		protected bool? _Selected = false;
		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Selected")]
		public bool? Selected
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
		#region Active
		public abstract class active : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Active", Visibility = PXUIVisibility.Visible)]
		public virtual bool? Active
		{
			get;
			set;
		}
		#endregion

		#region CashAccountID
		public abstract class cashAccountID : IBqlField
		{
		}

		[PXDBIdentity]
		[PXUIField(Enabled = false)]
		[PXReferentialIntegrityCheck]
		public virtual int? CashAccountID
		{
			get;
			set;
		}
		#endregion
		#region CashAccountCD
		public abstract class cashAccountCD : IBqlField
		{
		}

		[CashAccountRaw(IsKey = true, Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault]
		public virtual string CashAccountCD
		{
			get;
			set;
		}
		#endregion
		#region AccountID
		public abstract class accountID : IBqlField
		{
		}

		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
		[Account(Required = true, Visibility = PXUIVisibility.SelectorVisible)]
		public virtual int? AccountID
		{
			get;
			set;
		}
		#endregion
		#region BranchID
		public abstract class branchID : IBqlField
		{
		}

		[Branch(Visibility = PXUIVisibility.SelectorVisible)]
		public virtual int? BranchID
		{
			get;
			set;
		}
		#endregion
		#region SubID
		public abstract class subID : IBqlField
		{
		}

		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
		[SubAccount(typeof(CashAccount.accountID), DisplayName = "Subaccount", DescriptionField = typeof(Sub.description),
			Required = true, Visibility = PXUIVisibility.SelectorVisible)]
		public virtual int? SubID
		{
			get;
			set;
		}
		#endregion
		#region Descr
		public abstract class descr : IBqlField
		{
		}

		[PXDBLocalizableString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual string Descr
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
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, Required = true)]
		[PXSelector(typeof(CM.Currency.curyID))]
		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
		public virtual string CuryID
		{
			get;
			set;
		}
		#endregion
		#region CuryRateTypeID
		public abstract class curyRateTypeID : IBqlField
		{
		}

		[PXDBString(6, IsUnicode = true)]
		[PXSelector(typeof(CM.CurrencyRateType.curyRateTypeID))]
		[PXUIField(DisplayName = "Curr. Rate Type ")]
		public virtual string CuryRateTypeID
		{
			get;
			set;
		}
		#endregion
		#region ExtRefNbr
		public abstract class extRefNbr : IBqlField
		{
		}

		[PXDBString(40, IsUnicode = true)]
		[PXUIField(DisplayName = "External Ref. Number", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string ExtRefNbr
		{
			get;
			set;
		}
		#endregion
		#region Reconcile
		public abstract class reconcile : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Requires Reconciliation")]
		public virtual bool? Reconcile
		{
			get;
			set;
		}
		#endregion
		#region ReferenceID
		public abstract class referenceID : IBqlField
		{
		}

		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[Vendor(DescriptionField = typeof(Vendor.acctName), DisplayName = "Bank ID")]
		[PXUIField(DisplayName = "Bank ID")]
		public virtual int? ReferenceID
		{
			get;
			set;
		}
		#endregion
		#region ReconNumberingID
		public abstract class reconNumberingID : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true)]
		[PXSelector(typeof(Numbering.numberingID),
					 DescriptionField = typeof(Numbering.descr))]
		[PXUIField(DisplayName = "Reconciliation Numbering Sequence", Required = false)]
		public virtual string ReconNumberingID
		{
			get;
			set;
		}
		#endregion
		#region ClearingAccount
		public abstract class clearingAccount : IBqlField
		{
		}
		
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Clearing Account")]
		public virtual bool? ClearingAccount
		{
			get;
			set;
		}
		#endregion
		#region Signature
		public abstract class signature : IBqlField
		{
		}
		[PXDBString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Signature")]
		public virtual string Signature
		{
			get;
			set;
		}
		#endregion
		#region SignatureDescr
		public abstract class signatureDescr : IBqlField
		{
		}
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Name")]
		public virtual string SignatureDescr
		{
			get;
			set;
		}
		#endregion
		#region StatementImportTypeName
		public abstract class statementImportTypeName : IBqlField
		{
		}

		[PXDBString(255)]
		[PXUIField(DisplayName = "Statement Import Service")]
		[PXProviderTypeSelector(typeof(IStatementReader))]
		public virtual string StatementImportTypeName
		{
			get;
			set;
		}
		#endregion
		#region RestrictVisibilityWithBranch
		public abstract class restrictVisibilityWithBranch : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Restrict Visibility with Branch")]
		public virtual bool? RestrictVisibilityWithBranch
		{
			get;
			set;
		}
		#endregion
		#region PTInstanceAllowed
		public abstract class pTInstancesAllowed : IBqlField
		{
		}

		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Cards Allowed", Visible = false, Enabled = false)]
		public virtual bool? PTInstancesAllowed
		{
			get;
			set;
		}
		#endregion
		#region AcctSettingsAllowed
		public abstract class acctSettingsAllowed : IBqlField
		{
		}

		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Account Settings Allowed", Visible = false, Enabled = false)]
		public virtual bool? AcctSettingsAllowed
		{
			get;
			set;
		}
		#endregion
		#region MatchToBatch
		public abstract class matchToBatch : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Match Bank Transactions to Batch Payments")]
		public virtual bool? MatchToBatch
		{
			get;
			set;
		}
		#endregion

		#region NoteID
		public abstract class noteID : IBqlField { }
		[PXNote(DescriptionField = typeof(CashAccount.cashAccountCD))]
		public virtual Guid? NoteID
		{
			get;
			set;
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : IBqlField
		{ }
		[PXDBTimestamp]
		public virtual byte[] tstamp
		{
			get;
			set;
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : IBqlField
		{ }
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField
		{ }
		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField
		{ }
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
		{ }
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField
		{ }
		[PXDBLastModifiedByScreenID]
		public virtual string LastModifiedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField
		{ }
		[PXDBLastModifiedDateTime]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
	}
}
