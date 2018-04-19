using System;
using PX.Data;

namespace PX.Objects.CR
{
	[Serializable]
	[PXCacheName(Messages.Probability)]
	public partial class CROpportunityProbability : PX.Data.IBqlTable
	{
		#region StageCode
		public abstract class stageCode : IBqlField { }

		[PXDBString(2, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Stage ID")]
		public virtual String StageCode { get; set; }
		#endregion

		#region Probability
		public abstract class probability : IBqlField { }

		[PXDBInt(MinValue = 0, MaxValue = 100)]
		[PXDefault(0)]
		[PXUIField(DisplayName = "Probability")]
		public virtual Int32? Probability { get; set; }
        #endregion

        #region Name
        public abstract class name : PX.Data.IBqlField { }

        [PXDBLocalizableString(50, IsUnicode = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Name")]
        public virtual String Name { get; set; }
        #endregion

        #region SortOrder
        public abstract class sortOrder : IBqlField { }

        [PXDBInt]
        [PXDefault(0)]
        [PXUIField(DisplayName = "Sort Order")]
        public virtual Int32? SortOrder { get; set; }
        #endregion

        #region IsActive
        public abstract class isActive : IBqlField { }

        [PXBool]
        [PXUIField(DisplayName = "Active")]
        [PXDefault(true)]
        public virtual bool? IsActive { get; set; }
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
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXDBCreatedDateTime()]
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
		[PXDBLastModifiedDateTime()]
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
        public abstract class noteID : IBqlField { }

        [PXNote(new Type[0])]
        public virtual Guid? NoteID { get; set; }
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
	}
}
