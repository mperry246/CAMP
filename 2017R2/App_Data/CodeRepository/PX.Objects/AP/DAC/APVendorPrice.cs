using System;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.IN;

namespace PX.Objects.AP
{
	[Serializable]
	[PXCacheName(Messages.APVendorPrice)]
	public partial class APVendorPrice : IBqlTable
	{
		#region RecordID
		public abstract class recordID : PX.Data.IBqlField
		{
		}
		protected Int32? _RecordID;
		[PXDBIdentity(IsKey = true)]
		public virtual Int32? RecordID
		{
			get
			{
				return this._RecordID;
			}
			set
			{
				this._RecordID = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : IBqlField { }
		[Vendor]
		[PXDefault]
		[PXParent(typeof(Select<Vendor, Where<Vendor.bAccountID, Equal<Current<APVendorPrice.vendorID>>>>))]
		public virtual int? VendorID
		{
			get;
			set;
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[APCrossItem(BAccountField = typeof(vendorID), WarningOnNonUniqueSubstitution = true)]
		[PXParent(typeof(Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<APVendorPrice.inventoryID>>>>))] 
		public virtual Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region AlternateID
		public abstract class alternateID : PX.Data.IBqlField
		{
		}
		protected String _AlternateID;
		[PXUIField(DisplayName = "Alternate ID", Visible = false, Visibility = PXUIVisibility.Dynamic)]
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		public virtual String AlternateID
		{
			get
			{
				return this._AlternateID;
			}
			set
			{
				this._AlternateID = value;
			}
		}
		#endregion
		#region CuryID
		public abstract class curyID : PX.Data.IBqlField
		{
		}
		protected string _CuryID;
		[PXDBString(5)]
        [PXDefault(typeof(Search<GL.Company.baseCuryID>))]
		[PXSelector(typeof(Currency.curyID), CacheGlobal = true)]
		[PXUIField(DisplayName = "Currency", Required = false)]
		public virtual string CuryID
		{
			get
			{
				return this._CuryID;
			}
			set
			{
				this._CuryID = value;
			}
		}
		#endregion
		#region UOM
		public abstract class uOM : PX.Data.IBqlField
		{
		}
		protected String _UOM;
		[PXDefault(typeof(Search<InventoryItem.purchaseUnit, Where<InventoryItem.inventoryID, Equal<Current<APVendorPrice.inventoryID>>>>))]
		[INUnit(typeof(APVendorPrice.inventoryID))]
		[PXFormula(typeof(Selector<APVendorPrice.inventoryID, InventoryItem.purchaseUnit>))]
		public virtual String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		#region IsPromotionalPrice
		public abstract class isPromotionalPrice : IBqlField
		{
		}
		protected bool? _IsPromotionalPrice;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Promotional")]
		public virtual bool? IsPromotionalPrice
		{
			get
			{
				return _IsPromotionalPrice;
			}
			set
			{
				_IsPromotionalPrice = value;
			}
		}
		#endregion
		#region EffectiveDate
		public abstract class effectiveDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _EffectiveDate;
		[PXDefault(typeof(AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXDBDate()]
		[PXUIField(DisplayName = "Effective Date", Visibility = PXUIVisibility.Visible)]
		public virtual DateTime? EffectiveDate
		{
			get
			{
				return this._EffectiveDate;
			}
			set
			{
				this._EffectiveDate = value;
			}
		}
		#endregion
		#region SalesPrice
		public abstract class salesPrice : IBqlField { }
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBPriceCost]
		[PXUIField(DisplayName = "Price", Visibility = PXUIVisibility.Visible)]
		public virtual decimal? SalesPrice
		{
			get;
			set;
		}
		#endregion
		#region BreakQty
		public abstract class breakQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BreakQty;
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBQuantity(MinValue=0)]
		[PXUIField(DisplayName = "Break Qty", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? BreakQty
		{
			get
			{
				return this._BreakQty;
			}
			set
			{
				this._BreakQty = value;
			}
		}
		#endregion
		#region SiteID
		public abstract class siteID : PX.Data.IBqlField { }
		protected Int32? _SiteID;
		[NullableSite]
		public virtual Int32? SiteID
		{
			get { return this._SiteID; }
			set { this._SiteID = value; }
		}
		#endregion

		#region ExpirationDate
		public abstract class expirationDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _ExpirationDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Expiration Date", Visibility = PXUIVisibility.Visible)]
		public virtual DateTime? ExpirationDate
		{
			get
			{
				return this._ExpirationDate;
			}
			set
			{
				this._ExpirationDate = value;
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
