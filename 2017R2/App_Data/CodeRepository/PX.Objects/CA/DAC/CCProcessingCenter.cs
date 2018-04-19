using V2 = PX.CCProcessingBase.Interfaces.V2;
using V1 = PX.CCProcessingBase;
using PX.Data;
using PX.Data.EP;
using System;
using PX.Objects.GL;

namespace PX.Objects.CA
{
	[PXCacheName(Messages.CCProcessingCenter)]
	[Serializable]
	public partial class CCProcessingCenter : IBqlTable
	{
		#region ProcessingCenterID
		public abstract class processingCenterID : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXSelector(typeof(Search<CCProcessingCenter.processingCenterID>))]
		[PXUIField(DisplayName = "Proc. Center ID", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string ProcessingCenterID
		{
			get;
			set;
		}
		#endregion
		#region Name
		public abstract class name : IBqlField
		{
		}

		[PXDBString(255, IsUnicode = true)]
		[PXDefault("")]
		[PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual string Name
		{
			get;
			set;
		}
		#endregion
		#region IsActive
		public abstract class isActive : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Active")]
		public virtual bool? IsActive
		{
			get;
			set;
		}
		#endregion
		#region CashAccountID
		public abstract class cashAccountID : IBqlField
		{
		}

		[CashAccount(DisplayName = "Cash Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(CashAccount.descr))]
		[PXDefault]
		public virtual int? CashAccountID
		{
			get;
			set;
		}
		#endregion
		#region ProcessingTypeName
		public abstract class processingTypeName : IBqlField
		{
		}

		[PXDBString(255)]
		[PXDefault(PersistingCheck=PXPersistingCheck.Nothing)]
		[PXProviderTypeSelector(typeof(V1.ICCPaymentProcessing), typeof(V2.ICCProcessingPlugin))]
		[PXUIField(DisplayName = "Payment Plug-In (Type)")]
		public virtual string ProcessingTypeName
		{
			get;
			set;
		}
		#endregion
		#region ProcessingAssemblyName
		public abstract class processingAssemblyName : IBqlField
		{
		}

		[PXDBString(255)]
		[PXUIField(DisplayName = "Assembly Name")]
		public virtual string ProcessingAssemblyName
		{
			get;
			set;
		}
		#endregion
		#region OpenTranTimeout
		public abstract class openTranTimeout : IBqlField
		{
		}

		[PXDBInt(MinValue = 0, MaxValue = 60)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Transaction Timeout (s)", Visibility = PXUIVisibility.Visible)]
		public virtual int? OpenTranTimeout
		{
			get;
			set;
		}
		#endregion
		#region AllowDirectInput
		public abstract class allowDirectInput : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Allow Direct Input", Visible = false)]
		public virtual bool? AllowDirectInput
		{
			get;
			set;
		}
		#endregion
		#region NeedsExpDateUpdate
		[PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual bool? NeedsExpDateUpdate
		{
			get;
			set;
		}
		#endregion
		#region SyncronizeDeletion
		public abstract class syncronizeDeletion : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Synchronize Deletion", Visible = false)]
		public virtual bool? SyncronizeDeletion
		{
			get;
			set;
		}
		#endregion
		#region SyncRetryAttemptsNo
		public abstract class syncRetryAttemptsNo : IBqlField
		{
		}

		[PXDBInt(MinValue = 0, MaxValue = 10)]
		[PXDefault(TypeCode.Int32, "3", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Number of Additional Synchronization Attempts", Visibility = PXUIVisibility.Visible)]
		public virtual int? SyncRetryAttemptsNo
		{
			get;
			set;
		}
		#endregion
		#region SyncRetryDelayMs
		public abstract class syncRetryDelayMs : IBqlField
		{
		}

		[PXDBInt(MinValue = 0, MaxValue = 1000)]
		[PXDefault(TypeCode.Int32, "500", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Delay Between Synchronization Attempts (ms)", Visibility = PXUIVisibility.Visible)]
		public virtual int? SyncRetryDelayMs
		{
			get;
			set;
		}
		#endregion

		#region CreditCardLimit
		public abstract class creditCardLimit : PX.Data.IBqlField
		{
		}

		[PXDBInt(MinValue = 1)]
		[PXDefault(10, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Maximum Credit Cards per Profile", Visible = true)]
		public virtual int? CreditCardLimit
		{
			get;
			set;
		}
		#endregion

		#region CreateAdditionalCustomerProfile
		public abstract class createAdditionalCustomerProfiles : PX.Data.IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Create Additional Customer Profiles", Visible = true)]
		public virtual Boolean? CreateAdditionalCustomerProfiles
		{
			get;
			set;
		}
		#endregion

		#region NoteID

		public abstract class noteID : IBqlField { }

		[PXNote(DescriptionField = typeof(CCProcessingCenter.processingCenterID))]
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
	}
}
