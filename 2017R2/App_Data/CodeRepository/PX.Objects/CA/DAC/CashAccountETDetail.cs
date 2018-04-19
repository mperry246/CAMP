using System;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.GL;
using PX.Objects.TX;

namespace PX.Objects.CA
{
	[Serializable]
	[PXCacheName(Messages.CashAccountETDetail)]
	public partial class CashAccountETDetail : IBqlTable
	{
		#region AccountID
		public abstract class accountID : IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(CashAccount.cashAccountID))]
		[PXUIField(DisplayName = "AccountID", Visible = false)]
        [PXParent(typeof(Select<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<CashAccountETDetail.accountID>>>>))]
		public virtual int? AccountID
			{
			get;
			set;
		}
		#endregion
		#region EntryTypeID
		public abstract class entryTypeID : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Entry Type ID")]
		[PXSelector(typeof(CAEntryType.entryTypeId))]
		public virtual string EntryTypeID
			{
			get;
			set;
		}
		#endregion
		#region OffsetAccountID
		public abstract class offsetAccountID : IBqlField
		{
		}

		[Account(DescriptionField = typeof(Account.description), DisplayName = "Offset Account Override")]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? OffsetAccountID
			{
			get;
			set;
		}
		#endregion
		#region OffsetSubID
		public abstract class offsetSubID : IBqlField
		{
		}

		[SubAccount(typeof(CashAccountETDetail.offsetAccountID), DisplayName = "Offset Subaccount Override")]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? OffsetSubID
		{
			get;
			set;
		}
		#endregion
        #region OffsetBranchID
		public abstract class offsetBranchID : IBqlField
        {
        }

		[Branch(DisplayName = "Offset Account Branch Override", PersistingCheck = PXPersistingCheck.Nothing, Enabled = false)]
		public virtual int? OffsetBranchID
        {
			get;
			set;
        }
        #endregion
        #region OffsetCashAccountID
		public abstract class offsetCashAccountID : IBqlField
        {
        }

        [PXRestrictor(typeof(Where<CashAccount.cashAccountID, NotEqual<Current<CashAccount.cashAccountID>>>), Messages.SetOffsetAccountInSameCurrency)]
        [PXRestrictor(typeof(Where<CashAccount.curyID, Equal<Current<CashAccount.curyID>>>), Messages.SetOffsetAccountInSameCurrency)]
        [CashAccountScalar(DisplayName = "Reclassification Account Override", Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(CashAccount.descr))]
		[PXDBScalar(typeof(Search<CashAccount.cashAccountID,
				Where<CashAccount.accountID, Equal<CashAccountETDetail.offsetAccountID>,
				And<CashAccount.subID, Equal<CashAccountETDetail.offsetSubID>,
				And<CashAccount.branchID, Equal<CashAccountETDetail.offsetBranchID>>>>>))]
		public virtual int? OffsetCashAccountID
            {
			get;
			set;
        }
        #endregion
		#region TaxZoneID
		public abstract class taxZoneID : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Tax Zone")]
		[PXSelector(typeof(TaxZone.taxZoneID), DescriptionField = typeof(TaxZone.descr), Filterable = true)]
		public virtual string TaxZoneID
			{
			get;
			set;
		}
		#endregion
		#region TaxCalcMode
		public abstract class taxCalcMode : IBqlField { }
		[PXDBString(1, IsFixed = true)]
		[PXDefault(typeof(TaxCalculationMode.taxSetting))]
		[TaxCalculationMode.List]
		[PXUIField(DisplayName = "Tax Calculation Mode")]
		public virtual string TaxCalcMode
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
