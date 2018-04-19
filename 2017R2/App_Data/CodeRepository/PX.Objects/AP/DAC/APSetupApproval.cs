using System;

using PX.Data;
using PX.Data.EP;
using PX.Objects.EP;

namespace PX.Objects.AP
{
	[PXCacheName(Messages.APSetupApproval)]
	public class APSetupApproval : IBqlTable, IAssignedMap
	{
		#region ApprovalID
		public abstract class approvalID : PX.Data.IBqlField
		{
		}

		[PXDBIdentity(IsKey = true)]
		public virtual int? ApprovalID
		{
			get;
			set;
		}
		#endregion
		#region AssignmentMapID
		public abstract class assignmentMapID : PX.Data.IBqlField
		{
		}

		[PXDefault]
		[PXDBInt]
		[PXSelector(typeof(Search<EPAssignmentMap.assignmentMapID,
			Where2<Where<Current<APSetupApproval.docType>, Equal<APDocType.invoice>,
					And<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeAPInvoice>>>,
				Or2<Where<Current<APSetupApproval.docType>, Equal<APDocType.creditAdj>,
					And<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeAPInvoice>>>,
				Or2<Where<Current<APSetupApproval.docType>, Equal<APDocType.debitAdj>,
					And<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeAPInvoice>>>,
				Or2<Where<Current<APSetupApproval.docType>, Equal<APDocType.prepaymentRequest>,
					And<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeAPInvoice>>>,
				Or2<Where<Current<APSetupApproval.docType>, Equal<APDocType.quickCheck>,
					And<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeAPQuickCheck>>>,
				Or2<Where<Current<APSetupApproval.docType>, Equal<APDocType.check>,
					And<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeAPPayment>>>,
				Or<Where<Current<APSetupApproval.docType>, Equal<APDocType.prepayment>,
					And<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeAPPayment>>>>>>>>>>>),
				DescriptionField = typeof(EPAssignmentMap.name),
				SubstituteKey = typeof(EPAssignmentMap.name))]
		[PXUIField(DisplayName = "Approval Map")]
		public virtual int? AssignmentMapID
		{
			get;
			set;
		}
		#endregion
		#region AssignmentNotificationID
		public abstract class assignmentNotificationID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXSelector(typeof(PX.SM.Notification.notificationID), SubstituteKey = typeof(PX.SM.Notification.name))]
		[PXUIField(DisplayName = "Pending Approval Notification")]
		public virtual int? AssignmentNotificationID { get; set; }
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
		#region IsActive
		public abstract class isActive : PX.Data.IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Active")]
		public virtual bool? IsActive
		{
			get;
			set;
		}
		#endregion
		#region DocType
		public abstract class docType : PX.Data.IBqlField
		{
		}
		protected String _DocType;
		[PXDBString(3, IsFixed = true)]
		[PXDefault(APDocType.Invoice)]
		[APDocType.APApprovalDocTypeList]
		[PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
		[PXFieldDescription]
		public virtual String DocType
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
	}
}
