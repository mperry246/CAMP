using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.CR
{
	[Serializable]
	[PXCacheName(Messages.MarketingListMember)]
	public partial class CRMarketingListMember : IBqlTable
	{
		#region ContactID

		public abstract class contactID : IBqlField { }

		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Name")]
		[PXSelector(typeof(Search2<Contact.contactID,
		        LeftJoin<GL.Branch,
		            On<GL.Branch.bAccountID, Equal<Contact.bAccountID>,
		            And<Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>,
            Where<Branch.bAccountID, IsNull>>),
			typeof(Contact.fullName),
			typeof(Contact.displayName),
			typeof(Contact.eMail),
			typeof(Contact.phone1),
			typeof(Contact.bAccountID),
			typeof(Contact.salutation),
			typeof(Contact.contactType),
			typeof(Contact.isActive),
		    typeof(Contact.memberName),
            DescriptionField = typeof(Contact.memberName), 
			Filterable = true, 
			DirtyRead = true)]
		[PXParent(typeof(Select<Contact, Where<Contact.contactID, Equal<Current<CRMarketingListMember.contactID>>>>))]
		public virtual Int32? ContactID { get; set; }

		#endregion

		#region MarketingListID

		public abstract class marketingListID : IBqlField { }

		[PXDBInt(IsKey = true)]
		[PXDBLiteDefault(typeof(CRMarketingList.marketingListID))]
		[PXUIField(DisplayName = "Marketing List ID")]
		[PXSelector(typeof(Search<CRMarketingList.marketingListID,
			Where<CRMarketingList.isDynamic, Equal<False>>>),
		    DescriptionField = typeof(CRMarketingList.mailListCode))]
		public virtual Int32? MarketingListID { get; set; }

		#endregion

		#region IsSubscribed

		public abstract class isSubscribed : IBqlField { }

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Subscribed")]
		public virtual Boolean? IsSubscribed { get; set; }

		#endregion

		#region Format

		public abstract class format : IBqlField { }
		[PXDBString]
		[PXDefault(NotificationFormat.Html)]
		[PXUIField(DisplayName = "Format")]		
		[NotificationFormat.TemplateList]
		public virtual string Format { get; set; }

		#endregion

		#region Selected
		public abstract class selected : IBqlField
		{
		}
		protected bool? _Selected = false;
		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Selected", Visibility = PXUIVisibility.Service)]
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

		#region CreatedByScreenID

		public abstract class createdByScreenID : IBqlField { }

		[PXDBCreatedByScreenID]
		public virtual String CreatedByScreenID { get; set; }

		#endregion

		#region CreatedByID

		public abstract class createdByID : IBqlField { }

		[PXDBCreatedByID]
		public virtual Guid? CreatedByID { get; set; }

		#endregion

		#region CreatedDateTime

		public abstract class createdDateTime : IBqlField { }

		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime { get; set; }

		#endregion

		#region LastModifiedByID

		public abstract class lastModifiedByID : IBqlField { }

		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID { get; set; }

		#endregion

		#region LastModifiedByScreenID

		public abstract class lastModifiedByScreenID : IBqlField { }

		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID { get; set; }

		#endregion

		#region LastModifiedDateTime

		public abstract class lastModifiedDateTime : IBqlField { }

		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime { get; set; }

		#endregion

		#region tstamp

		public abstract class Tstamp : IBqlField { }

		[PXDBTimestamp]
		public virtual Byte[] tstamp { get; set; }

		#endregion
	}
}
