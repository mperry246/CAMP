using System;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.CA
{
    public class CashAccountScalarAttribute : CashAccountAttribute
    {
		public CashAccountScalarAttribute() : base() { }

		public CashAccountScalarAttribute(Type branchID, Type searchType) : base(branchID, searchType) { }

        public override void GetSubscriber<ISubscriber>(System.Collections.Generic.List<ISubscriber> subscribers)
        {
            if (typeof(ISubscriber) != typeof(IPXCommandPreparingSubscriber) && typeof(ISubscriber) != typeof(IPXRowSelectingSubscriber))
            {
                base.GetSubscriber<ISubscriber>(subscribers);
            }
        }
    }
	[Serializable]
	[PXPrimaryGraph(typeof(EntryTypeMaint))]
	[PXCacheName(Messages.CAEntryType)]
	public partial class CAEntryType : IBqlTable
	{
        
		#region EntryTypeId
		public abstract class entryTypeId : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
		[PXUIField(DisplayName = "Entry Type ID", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string EntryTypeId
			{
			get;
			set;
		}
		#endregion
		#region OrigModule
		public abstract class module : IBqlField
		{
		}

		[PXDBString(2, IsFixed = true)]
		[PXDefault(BatchModule.CA)]
		[BatchModule.CashManagerList]
		[PXUIField(DisplayName = "Module", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string Module
			{
			get;
			set;
		}
		#endregion
        #region CashAccountID
		public abstract class cashAccountID : IBqlField
        {
        }
        
        [CashAccountScalar(DisplayName = "Reclassification Account", Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(CashAccount.descr), Enabled = false)]
        [PXDBScalar(typeof(Search<CashAccount.cashAccountID, Where<CashAccount.accountID, Equal<CAEntryType.accountID>,
                                   And<CashAccount.subID, Equal<CAEntryType.subID>, And<CashAccount.branchID, Equal<CAEntryType.branchID>>>>>))]
		public virtual int? CashAccountID
            {
			get;
			set;
        }
        #endregion
		#region AccountID
		public abstract class accountID : IBqlField
		{
		}

		[Account(DescriptionField = typeof(Account.description), DisplayName = "Default Offset Account", Enabled = false)]
		public virtual int? AccountID
			{
			get;
			set;
		}
		#endregion
		#region SubID
		public abstract class subID : IBqlField
		{
		}

		[SubAccount(typeof(CAEntryType.accountID), DisplayName = "Default Offset Subaccount", Enabled = false)]
		public virtual int? SubID
		{
			get;
			set;
		}
		#endregion
        #region BranchID
		public abstract class branchID : IBqlField
        {
        }

        [Branch(DisplayName = "Default Offset Account Branch", PersistingCheck = PXPersistingCheck.Nothing, Enabled = false)]
		public virtual int? BranchID
            {
			get;
			set;
        }
        #endregion
		#region ReferenceID
		public abstract class referenceID : IBqlField
			{
			}

		[PXDBInt]
			[PXUIField(DisplayName = "Business Account", Enabled = false)]
			[PXVendorCustomerSelector(typeof(CAEntryType.module))]
		public virtual int? ReferenceID
				{
			get;
			set;
			}
		#endregion
		#region DrCr
		public abstract class drCr : IBqlField
			{
			}

			[PXDefault(GL.DrCr.Debit)]
			[PXDBString(1, IsFixed = true)]
		[CADrCr.List]
			[PXUIField(DisplayName = "Disb./Receipt", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string DrCr
				{
			get;
			set;
			}
		#endregion
		#region Descr
		public abstract class descr : IBqlField
		{
		}

		[PXDBString(60, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Entry Type Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string Descr
			{
			get;
			set;
		}
		#endregion
		#region UseToReclassifyPayments
		public abstract class useToReclassifyPayments : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Use for Payments Reclassification", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual bool? UseToReclassifyPayments
			{
			get;
			set;
		}
		#endregion		
        #region Consolidate
		public abstract class consolidate : IBqlField
        {
        }

		[PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Deduct from Payment")]
		public bool? Consolidate
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
