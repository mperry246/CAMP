namespace PX.Objects.CR
{
	using System;
	using PX.Data;
	using PX.Objects.CM;
	using PX.Objects.CS;
	using PX.Data.EP;
	using AR;
	using AP;
	using GL;
	using TM;
    using SO;

    [PXCacheName(Messages.Campaign)]
	[System.SerializableAttribute()]
	[PXPrimaryGraph(typeof(CampaignMaint))]
	public partial class CRCampaign : PX.Data.IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		protected bool? _Selected = false;
		[PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
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
		#region CampaignID
		public abstract class campaignID : PX.Data.IBqlField
		{
		}
		protected String _CampaignID;
		[PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault()]
		[PXUIField(DisplayName = Messages.CampaignID, Visibility = PXUIVisibility.SelectorVisible)]
		[AutoNumber(typeof(CRSetup.campaignNumberingID), typeof(AccessInfo.businessDate))]
		[PXSelector(typeof(CRCampaign.campaignID), DescriptionField = typeof(CRCampaign.campaignName))]
        [PXFieldDescription]
		public virtual String CampaignID
		{
			get
			{
				return this._CampaignID;
			}
			set
			{
				this._CampaignID = value;
			}
		}
		#endregion
		#region CampaignName
		public abstract class campaignName : PX.Data.IBqlField
		{
		}
		protected String _CampaignName;
		[PXDBString(60, IsUnicode = true)]
		[PXDefault("", PersistingCheck = PXPersistingCheck.NullOrBlank)]
		[PXUIField(DisplayName = "Campaign Name", Visibility = PXUIVisibility.SelectorVisible)]
		[PXNavigateSelector(typeof(CRCampaign.campaignName))]
        [PXFieldDescription]
		public virtual String CampaignName
		{
			get
			{
				return this._CampaignName;
			}
			set
			{
				this._CampaignName = value;
			}
		}
        #endregion
        #region Description
        public abstract class description : IBqlField { }

        protected String _Description;
        [PXDBText(IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual String Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
                _plainText = null;
            }
        }
        #endregion

        #region DescriptionAsPlainText
        public abstract class descriptionAsPlainText : IBqlField { }

        private string _plainText;
        [PXString(IsUnicode = true)]
        [PXUIField(Visible = false)]
        public virtual String DescriptionAsPlainText
        {
            get
            {
                return _plainText ?? (_plainText = PX.Data.Search.SearchService.Html2PlainText(this.Description));
            }
        }
        #endregion

        #region CampaignType
        public abstract class campaignType : PX.Data.IBqlField
		{
		}
		protected String _CampaignType;
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("")]
		[PXUIField(DisplayName = "Campaign Class", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(CRCampaignType.typeID), DescriptionField = typeof(CRCampaignType.description))]
		public virtual String CampaignType
		{
			get
			{
				return this._CampaignType;
			}
			set
			{
				this._CampaignType = value;
			}
		}
		#endregion

		#region Attributes
		public abstract class attributes : IBqlField { }

		[CRAttributesField(typeof(CRCampaign.campaignType))]
		public virtual string[] Attributes { get; set; }
		#endregion

		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected String _Status;
		[PXDBString(1, IsFixed = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Stage", Visibility = PXUIVisibility.SelectorVisible)]
        [PXStringListAttribute(new string[0], new string[0])]
        public virtual String Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}
		#endregion
		#region IsActive
		public abstract class isActive : PX.Data.IBqlField
		{
		}
		protected bool? _IsActive;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Active")]
		public virtual bool? IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				this._IsActive = value;
			}
		}
		#endregion
		#region StartDate
		public abstract class startDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _StartDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Start Date")]
		public virtual DateTime? StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				this._StartDate = value;
			}
		}
		#endregion
		#region EndDate
		public abstract class endDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _EndDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "End Date")]
		public virtual DateTime? EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				this._EndDate = value;
			}
		}
        #endregion

        #region ExpectedRevenue
        public abstract class expectedRevenue : PX.Data.IBqlField
        {
        }
        protected Decimal? _ExpectedRevenue;
        [PXDBBaseCury()]
        [PXUIField(DisplayName = "Expected Return")]
        public virtual Decimal? ExpectedRevenue
        {
            get
            {
                return this._ExpectedRevenue;
            }
            set
            {
                this._ExpectedRevenue = value;
            }
        }
        #endregion        

        #region PlannedBudget
        public abstract class plannedBudget : PX.Data.IBqlField
		{
		}
		protected Decimal? _PlannedBudget;
		[PXDBBaseCury()]
		[PXUIField(DisplayName = "Planned Budget")]
		public virtual Decimal? PlannedBudget
		{
			get
			{
				return this._PlannedBudget;
			}
			set
			{
				this._PlannedBudget = value;
			}
		}
		#endregion		

		#region ExpectedResponse
		public abstract class expectedResponse : PX.Data.IBqlField
		{
		}
		protected Int32? _ExpectedResponse;
		[PXDBInt()]
		[PXUIField(DisplayName = "Expected Response")]
		public virtual Int32? ExpectedResponse
		{
			get
			{
				return this._ExpectedResponse;
			}
			set
			{
				this._ExpectedResponse = value;
			}
		}
		#endregion
		#region MailsSent
		public abstract class mailsSent : PX.Data.IBqlField
		{
		}
		protected Int32? _MailsSent;
		[PXDBInt()]
		[PXUIField(DisplayName = "Mails Sent")]
		public virtual Int32? MailsSent
		{
			get
			{
				return this._MailsSent;
			}
			set
			{
				this._MailsSent = value;
			}
		}
		#endregion
		
		public abstract class ownerID : IBqlField { }

		[PXDBGuid()]
		[PXOwnerSelector(typeof(CRCampaign.workgroupID))]
		[PXUIField(DisplayName = "Owner")]
		public Guid? OwnerID { get; set; }

		public abstract class workgroupID : IBqlField { }

		[PXDBInt]
		[PXUIField(DisplayName = "Workgroup")]
		[PXCompanyTreeSelector]
		public Int32? WorkgroupID { get; set; }		

        #region PromoCodeID
        public abstract class promoCodeID : PX.Data.IBqlField
		{
		}
		protected String _PromoCodeID;
		[PXDBString(30, IsUnicode = true)]
		[PXDefault("")]
		[PXUIField(DisplayName = "Promo Code", Required = false)]
		public virtual String PromoCodeID
		{
			get
			{
				return this._PromoCodeID;
			}
			set
			{
				this._PromoCodeID = value;
			}
        }
        #endregion

        #region SendFilter
        public abstract class sendFilter : PX.Data.IBqlField
		{
		}
		protected String _SendFilter = SendFilterSourcesAttribute._NEVERSENT;
		[PXString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Sent Emails Filter")]
		[PXDefault(SendFilterSourcesAttribute._NEVERSENT)]        
        [SendFilterSources]
        public virtual String SendFilter
		{
			get
			{
				return this._SendFilter;
			}
			set
			{
				this._SendFilter = value;
			}
		}
		#endregion

		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		protected Byte[] _tstamp;
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get
			{
				return this._tstamp;
			}
			set
			{
				this._tstamp = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? CreatedDateTime
		{
			get
			{
				return this._CreatedDateTime;
			}
			set
			{
				this._CreatedDateTime = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected String _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected String _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Guid? _NoteID;
        [PXNote(
        DescriptionField = typeof(CRCampaign.campaignID),
        Selector = typeof(CRCampaign.campaignID),
        ShowInReferenceSelector = true)]
        public virtual Guid? NoteID
		{
			get
			{
				return this._NoteID;
			}
			set
			{
				this._NoteID = value;
			}
		}
		#endregion		
	}
	public class CRSystemCampaignMemberStatusCodes
	{
		public class Sent : Constant<string>
		{
			public Sent()
				: base("S")
			{
			}
		}
		public class Responsed : Constant<string>
		{
			public Responsed()
				: base("P")
			{
			}
		}
	}
	public class CRSystemLeadStatusCodes
	{
		public class Converted : Constant<string>
		{
			public Converted()
				: base("C")
			{
			}
		}
	}
}
