namespace PX.Objects.AR
{
	using System;
	using PX.Data;
	
	/// <summary>
	/// Represents a customer price class. Customer price classes are
	/// used to group customers by the price level offered to them. 
	/// Entities of this type are edited on the Customer Price Classes 
	/// (AR208000) form, which corresponds to the <see cref="ARPriceClassMaint"/> graph.
	/// </summary>
	[System.SerializableAttribute()]
	[PXPrimaryGraph(typeof(ARPriceClassMaint))]
	[PXCacheName(Messages.ARPriceClass)]
	public partial class ARPriceClass : PX.Data.IBqlTable
	{
		/// <summary>
		/// The identifier of the default customer price class.
		/// All customers will be assigned to this price class 
		/// when no other price classes are defined in the system.
		/// </summary>
		public const string EmptyPriceClass = "BASE";
		[Obsolete("This value is not used anymore and will be removed in Acumatica 8.0")]
        public const short  EmptyPriceClassSortOrder = 0;

		public class emptyPriceClass : Constant<string>
		{
 			public emptyPriceClass()
				:base(EmptyPriceClass)
			{
			}
		}

		#region PriceClassID
		public abstract class priceClassID : PX.Data.IBqlField
		{
		}
		protected String _PriceClassID;
		/// <summary>
		/// The unique identifier of the customer price class.
		/// This field is the key field.
		/// </summary>
		[PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
		[PXDefault()]
		[PXUIField(DisplayName = "Price Class ID", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String PriceClassID
		{
			get
			{
				return this._PriceClassID;
			}
			set
			{
				this._PriceClassID = value;
			}
		}
		#endregion
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected String _Description;
		/// <summary>
		/// The description of the customer price class.
		/// </summary>
		[PXDBLocalizableString(250, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}
		#endregion
		#region SortOrder
		[Obsolete("This field is not used anymore and will be removed in Acumatica 8.0")]
		public abstract class sortOrder : PX.Data.IBqlField
        {
        }
        protected Int16? _SortOrder;
		[Obsolete("This field is not used anymore and will be removed in Acumatica 8.0")]
		/// <summary>
		/// An unused obsolete field.
		/// </summary>
        [PXDefault((short)1)]
        [PXDBShort(MinValue=1)]
        [PXUIField(DisplayName = "Sort Order")]
        public virtual Int16? SortOrder
        {
            get
            {
                return this._SortOrder;
            }
            set
            {
                this._SortOrder = value;
            }
        }
		#endregion

		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Guid? _NoteID;
		[PXNote]
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

		#region System Columns
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
		#endregion
	}
}
