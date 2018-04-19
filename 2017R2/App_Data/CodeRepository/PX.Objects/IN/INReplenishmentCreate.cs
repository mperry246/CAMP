using System;
using System.Collections.Generic;
using PX.Data;
using PX.TM;
using PX.Objects.GL;
using PX.Objects.AP;
using PX.Objects.RQ;
using PX.Objects.EP.Standalone;
using PX.Objects.CS;
using PX.Objects.CR;
using System.Collections;
using PX.Objects.PO;
using System.Text;
using PX.Common;

namespace PX.Objects.IN
{
	[System.SerializableAttribute]
	public partial class INReplenishmentFilter : IBqlTable
	{
		#region CurrentOwnerID
		public abstract class currentOwnerID : PX.Data.IBqlField
		{
		}

		[PXDBGuid]
		[CRCurrentOwnerID]
		public virtual Guid? CurrentOwnerID { get; set; }
		#endregion						
		#region MyOwner
		public abstract class myOwner : PX.Data.IBqlField
		{
		}
		protected Boolean? _MyOwner;
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Me")]
		public virtual Boolean? MyOwner
		{
			get
			{
				return _MyOwner;
			}
			set
			{
				_MyOwner = value;
			}
		}
		#endregion
		#region OwnerID
		public abstract class ownerID : PX.Data.IBqlField
		{
		}
		protected Guid? _OwnerID;
		[PXDBGuid]
		[PXUIField(DisplayName = "Product Manager")]
		[PX.TM.PXSubordinateOwnerSelector]
		public virtual Guid? OwnerID
		{
			get
			{
				return (_MyOwner == true) ? CurrentOwnerID : _OwnerID;
			}
			set
			{
				_OwnerID = value;
			}
		}
		#endregion
		#region WorkGroupID
		public abstract class workGroupID : PX.Data.IBqlField
		{
		}
		protected Int32? _WorkGroupID;
		[PXDBInt]
		[PXUIField(DisplayName = "Product  Workgroup")]
		[PXSelector(typeof(Search<EPCompanyTree.workGroupID,
			Where<EPCompanyTree.workGroupID, Owned<Current<AccessInfo.userID>>>>),
		 SubstituteKey = typeof(EPCompanyTree.description))]
		public virtual Int32? WorkGroupID
		{
			get
			{
				return (_MyWorkGroup == true) ? null : _WorkGroupID;
			}
			set
			{
				_WorkGroupID = value;
			}
		}
		#endregion
		#region MyWorkGroup
		public abstract class myWorkGroup : PX.Data.IBqlField
		{
		}
		protected Boolean? _MyWorkGroup;
		[PXDefault(false)]
		[PXDBBool]
		[PXUIField(DisplayName = "My", Visibility = PXUIVisibility.Visible)]
		public virtual Boolean? MyWorkGroup
		{
			get
			{
				return _MyWorkGroup;
			}
			set
			{
				_MyWorkGroup = value;
			}
		}
		#endregion
		#region FilterSet
		public abstract class filterSet : PX.Data.IBqlField
		{
		}
		[PXDefault(false)]
		[PXDBBool]
        public virtual Boolean? FilterSet
		{
			get
			{
				return
					this.OwnerID != null ||
					this.WorkGroupID != null ||
					this.MyWorkGroup == true;
			}
		}
		#endregion
		#region ReplenishmentSiteID
		public abstract class replenishmentSiteID : PX.Data.IBqlField
		{
		}
		protected Int32? _ReplenishmentSiteID;
		[IN.Site(DisplayName = "Warehouse")]
		[PXDefault]
		public virtual Int32? ReplenishmentSiteID
		{
			get
			{
				return this._ReplenishmentSiteID;
			}
			set
			{
				this._ReplenishmentSiteID = value;
			}
		}
		#endregion
		#region PurchaseDate
		public abstract class purchaseDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _PurchaseDate;
		[PXDBDate]
		[PXUIField(DisplayName = "Purchase Date")]
		[PXDefault(typeof(AccessInfo.businessDate))]		
		public virtual DateTime? PurchaseDate
		{
			get
			{
				return this._PurchaseDate;
			}
			set
			{
				this._PurchaseDate = value;
			}
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[StockItem(Filterable = true)]
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
		#region SubItemCD
		public abstract class subItemCD : PX.Data.IBqlField
		{
		}
		protected String _SubItemCD;
		[SubItemRawExt(typeof(INReplenishmentFilter.inventoryID), DisplayName = "Subitem")]
		//[SubItemRaw(DisplayName = "Subitem")]
		public virtual String SubItemCD
		{
			get
			{
				return this._SubItemCD;
			}
			set
			{
				this._SubItemCD = value;
			}
		}
		#endregion
		#region SubItemCD Wildcard
		public abstract class subItemCDWildcard : PX.Data.IBqlField { };
		[PXDBString(30, IsUnicode = true)]
		public virtual String SubItemCDWildcard
		{
			get
			{
				//return SubItemCDUtils.CreateSubItemCDWildcard(this._SubItemCD);
				return SubCDUtils.CreateSubCDWildcard(this._SubItemCD, SubItemAttribute.DimensionName);
			}
		}
		#endregion
		#region PreferredVendorID
		public abstract class preferredVendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _PreferredVendorID;
		[VendorNonEmployeeActive(Visibility = PXUIVisibility.SelectorVisible,
			DescriptionField = typeof(Vendor.acctName), CacheGlobal = true, Filterable = true, DisplayName = "Vendor")]
		public virtual Int32? PreferredVendorID
		{
			get
			{
				return this._PreferredVendorID;
			}
			set
			{
				this._PreferredVendorID = value;
			}
		}
		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}
		protected String _Descr;
		[PXDBString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Descr
		{
			get
			{
				return this._Descr;
			}
			set
			{
				this._Descr = value;
			}
		}
		#endregion
		#region DescrWildcard
		public abstract class descrWildcard : PX.Data.IBqlField
		{
		}
		[PXDBString(255, IsUnicode = true)]
		public virtual String DescrWildcard
		{
			get
			{
				return this._Descr == null ? null : "%" + this._Descr + "%";
			}
		}
		#endregion
		#region OnlySuggested
		public abstract class onlySuggested : PX.Data.IBqlField
		{
		}
		protected Boolean? _OnlySuggested;
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Only Suggested Items")]
		public virtual Boolean? OnlySuggested
		{
			get
			{
				return _OnlySuggested;
			}
			set
			{
				_OnlySuggested = value;
			}
		}
		#endregion
		#region ItemClassCD
		public abstract class itemClassCD : PX.Data.IBqlField { }
		protected string _ItemClassCD;

		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "Item Class ID", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDimensionSelector(INItemClass.Dimension, typeof(INItemClass.itemClassCD), DescriptionField = typeof(INItemClass.descr), ValidComboRequired = true)]
		public virtual string ItemClassCD
		{
			get { return this._ItemClassCD; }
			set { this._ItemClassCD = value; }
		}
		#endregion
		#region ItemClassCDWildcard
		public abstract class itemClassCDWildcard : PX.Data.IBqlField { }
		[PXString(IsUnicode = true)]
		[PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible)]
		[PXDimension(INItemClass.Dimension, ParentSelect = typeof(Select<INItemClass>), ParentValueField = typeof(INItemClass.itemClassCD))]
		public virtual string ItemClassCDWildcard
		{
			get { return ItemClassTree.MakeWildcard(ItemClassCD); }
			set { }
		}
		#endregion
	}

	public class INReplenishmentProjection : TM.OwnedFilter.ProjectionAttribute
	{
		public INReplenishmentProjection()
			: base(typeof(INReplenishmentFilter), typeof(Select<S.INItemSite>))
		{
		}

		protected override Type GetSelect(PXCache sender)
		{
			bool subItems = PXAccess.FeatureInstalled<FeaturesSet.subItem>();

			var commands = new List<Type>();
			commands.AddRange(new Type[] {
				typeof(Select2<,,>),
				typeof(IN.S.INItemSite),
				typeof(InnerJoin<,,>),
				typeof(InventoryItem),
				typeof(On<InventoryItem.inventoryID, Equal<IN.S.INItemSite.inventoryID>,
							And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>,
							And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>>),
				typeof(LeftJoin<,,>),
				typeof(Vendor),
				typeof(On<Vendor.bAccountID, Equal<IN.S.INItemSite.preferredVendorID>>) });

			if (subItems)
			{
				commands.AddRange(new Type[] {
				typeof(LeftJoin<,,>),
				typeof(INSiteStatus),
				typeof(On<INSiteStatus.inventoryID, Equal<IN.S.INItemSite.inventoryID>,
							And<INSiteStatus.siteID, Equal<IN.S.INItemSite.siteID>>>),
				typeof(LeftJoin<,,>),
				typeof(POVendorInventoryRepo),
				typeof(On<POVendorInventoryRepo.inventoryID, Equal<IN.S.INItemSite.inventoryID>,
				   And<POVendorInventoryRepo.vendorID, Equal<IN.S.INItemSite.preferredVendorID>,
				   And<IsNull<POVendorInventoryRepo.vendorLocationID, decimal_1>, Equal<IsNull<IN.S.INItemSite.preferredVendorLocationID, decimal_1>>,
				   And<IsNull<POVendorInventoryRepo.subItemID, decimal_1>, Equal<IsNull<INSiteStatus.subItemID, decimal_1>>>>>>),

				typeof(LeftJoin<,,>),
				typeof(INSubItem),
				typeof(On<INSubItem.subItemID, Equal<INSiteStatus.subItemID>>),
				typeof(LeftJoin<,,>),
				typeof(INItemSiteReplenishment),
				typeof(On<INItemSiteReplenishment.inventoryID, Equal<INItemSite.inventoryID>,
							And<INItemSiteReplenishment.siteID, Equal<INItemSite.siteID>,
							And<INItemSiteReplenishment.subItemID, Equal<INSiteStatus.subItemID>,
							And<INItemSiteReplenishment.itemStatus, NotEqual<INItemStatus.inactive>>>>>)});
			}
			else
			{
				commands.AddRange(new Type[] {
				typeof(LeftJoin<,,>),
				typeof(INSubItem),
				typeof(On<INSubItem.subItemCD, Equal<string0>>),
				typeof(LeftJoin<,,>),
				typeof(INSiteStatus),
				typeof(On<INSiteStatus.inventoryID, Equal<IN.S.INItemSite.inventoryID>,
							And<INSiteStatus.siteID, Equal<IN.S.INItemSite.siteID>,
							And<INSiteStatus.subItemID, Equal<INSubItem.subItemID>>>>),
                typeof(LeftJoin<,,>),
                typeof(POVendorInventoryRepo),
                typeof(On<POVendorInventoryRepo.inventoryID, Equal<IN.S.INItemSite.inventoryID>,
                   And<POVendorInventoryRepo.vendorID, Equal<IN.S.INItemSite.preferredVendorID>,
				   And<IsNull<POVendorInventoryRepo.vendorLocationID, decimal_1>, Equal<IsNull<IN.S.INItemSite.preferredVendorLocationID, decimal_1>>,
				   And<IsNull<POVendorInventoryRepo.subItemID, decimal_1>, Equal<IsNull<INSubItem.subItemID, decimal_1>>>>>>),
                typeof(LeftJoin<,,>),
				typeof(INItemSiteReplenishment),
				typeof(On<int1, Equal<int0>>)}); 
			}
			commands.AddRange(new Type[] {
				typeof(InnerJoin<,,>),
				typeof(INItemClass),
				typeof(On<INItemClass.itemClassID, Equal<InventoryItem.itemClassID>>),
				typeof(LeftJoin<,>),
				typeof(INAvailabilityScheme),
				typeof(On<INAvailabilityScheme.availabilitySchemeID, Equal<INItemClass.availabilitySchemeID>>),
				TM.OwnedFilter.ProjectionAttribute.ComposeWhere(
				typeof(INReplenishmentFilter),
				typeof(InventoryItem.productWorkgroupID),
				typeof(InventoryItem.productManagerID))});

			return BqlCommand.Compose(commands.ToArray());
		}
	}

	[Serializable]
	[INReplenishmentProjection]
	public partial class INReplenishmentItem : IN.S.INItemSite
	{
        #region SubItemID
        public abstract class subItemID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubItemID;
		[SubItem(IsKey = true, BqlField=typeof(INSubItem.subItemID))]
		[PXDefault]
		public virtual Int32? SubItemID
		{
			get
			{
				return this._SubItemID;
			}
			set
			{
				this._SubItemID = value;
			}
		}
		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}
		protected String _Descr;
		[PXDBLocalizableString(255, IsUnicode = true, BqlField = typeof(InventoryItem.descr), IsProjection = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Descr
		{
			get
			{
				return this._Descr;
			}
			set
			{
				this._Descr = value;
			}
		}
		#endregion				
		#region ReplenishmentSourceSiteID
		public new abstract class replenishmentSourceSiteID : PX.Data.IBqlField
		{
		}
		[IN.Site(DisplayName = "Source Warehouse", DescriptionField = typeof(INSite.descr))]
		public override Int32? ReplenishmentSourceSiteID
		{
			get
			{
				return this._ReplenishmentSourceSiteID;
			}
			set
			{
				this._ReplenishmentSourceSiteID = value;
			}
		}
		#endregion
		#region PreferredVendorID
		public new abstract class preferredVendorID : PX.Data.IBqlField
		{
		}		
		[AP.VendorActive(DisplayName = "Preferred Vendor ID", Required = false, DescriptionField = typeof(Vendor.acctName), BqlField=typeof(INItemSite.preferredVendorID))]
		public override Int32? PreferredVendorID
		{
			get
			{
				return this._PreferredVendorID;
			}
			set
			{
				this._PreferredVendorID = value;
			}
		}
		#endregion
		#region PreferredVendorLocationID
		public new abstract class preferredVendorLocationID : PX.Data.IBqlField
		{
		}		
		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>),
			DescriptionField = typeof(Location.descr), Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Preferred Location", BqlField=typeof(INItemSite.preferredVendorLocationID))]		
		[PXDefault(typeof(Search<Vendor.defLocationID, Where<Vendor.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<INReplenishmentItem.preferredVendorID>))]
		public override Int32? PreferredVendorLocationID
		{
			get
			{
				return this._PreferredVendorLocationID;
			}
			set
			{
				this._PreferredVendorLocationID = value;
			}
		}
		#endregion
		#region PreferredVendorName
		public abstract class preferredVendorName : PX.Data.IBqlField
		{
		}
		protected string _PreferredVendorName;
		[PXDBString(60, IsUnicode = true, BqlField = typeof(Vendor.acctName))]
		[PXDefault(typeof(Search<BAccountR.acctName, Where<BAccountR.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Preferred Vendor Name", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFormula(typeof(Default<INReplenishmentItem.preferredVendorID>))]
		public virtual String PreferredVendorName
		{
			get
			{
				return this._PreferredVendorName;
			}
			set
			{
				this._PreferredVendorName = value;
			}
		}
		#endregion
		#region DefaultVendorLocationID
		public abstract class defaultVendorLocationID : PX.Data.IBqlField
		{
		}
		protected int? _DefaultVendorLocationID;
		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>),
			DescriptionField = typeof(Location.descr), Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Default Location", BqlField = typeof(Vendor.defLocationID))]
		[PXDefault(typeof(Search<Vendor.defLocationID, Where<Vendor.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<INReplenishmentItem.preferredVendorID>))]
		public virtual Int32? DefaultVendorLocationID
		{
			get
			{
				return this._DefaultVendorLocationID;
			}
			set
			{
				this._DefaultVendorLocationID = value;
			}
		}
		#endregion
		#region ItemClassID
		public abstract class itemClassID : PX.Data.IBqlField
		{
		}
		protected int? _ItemClassID;
		[PXDBInt(BqlField = typeof(InventoryItem.itemClassID))]
		[PXUIField(DisplayName = "Item Class", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDimensionSelector(INItemClass.Dimension, typeof(Search<INItemClass.itemClassID, Where<INItemClass.stkItem, Equal<boolTrue>>>), typeof(INItemClass.itemClassCD), DescriptionField = typeof(INItemClass.descr), ValidComboRequired = true)]
		[PXDefault(typeof(Search<INSetup.dfltStkItemClassID>))]
		public virtual int? ItemClassID
		{
			get
			{
				return this._ItemClassID;
			}
			set
			{
				this._ItemClassID = value;
			}
		}
		#endregion
		#region DemandCalculation
		public abstract class demandCalculation : PX.Data.IBqlField
		{
		}
		protected String _DemandCalculation;
		[PXString(1, IsFixed = true, BqlTable = (typeof(INItemClass)))]
        [PXDBCalced(typeof(IsNull<INItemClass.demandCalculation, StringEmpty>), typeof(string))]
        [PXUIField(DisplayName = "Demand Calculation")]
		[PXDefault(INDemandCalculation.ItemClassSettings)]
		[INDemandCalculation.List]
		public virtual String DemandCalculation
		{
			get
			{
				return this._DemandCalculation;
			}
			set
			{
				this._DemandCalculation = value;
			}
		}
		#endregion	
		#region VendorClassID
		public abstract class vendorClassID : PX.Data.IBqlField
		{
		}
		protected String _VendorClassID;
		[PXDBString(10, IsUnicode = true, BqlField = typeof(Vendor.vendorClassID))]
		[PXDefault(typeof(Search<Vendor.vendorClassID, Where<Vendor.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXSelector(typeof(Search2<VendorClass.vendorClassID, LeftJoin<EPEmployeeClass, On<EPEmployeeClass.vendorClassID, Equal<VendorClass.vendorClassID>>>, Where<EPEmployeeClass.vendorClassID, IsNull>>), DescriptionField = typeof(VendorClass.descr), CacheGlobal = true)]
		[PXFormula(typeof(Default<INReplenishmentItem.preferredVendorID>))]
		[PXUIField(DisplayName = "Vendor Class")]
		public virtual String VendorClassID
		{
			get
			{
				return this._VendorClassID;
			}
			set
			{
				this._VendorClassID = value;
			}
		}
		#endregion
		#region SafetyStock
		public new abstract class safetyStock : PX.Data.IBqlField
		{
		}		
		//[PXDBQuantity(BqlField=typeof(INItemSite.safetyStock))]
		[PXDecimal]
		[PXDBCalced(typeof(IsNull<INItemSiteReplenishment.safetyStock, INItemSite.safetyStock>), typeof(decimal))]
		[PXUIField(DisplayName = "Safety Stock")]
		public override Decimal? SafetyStock
		{
			get
			{
				return this._SafetyStock;
			}
			set
			{
				this._SafetyStock = value;
			}
		}
		#endregion
		#region MinQty
		public new abstract class minQty : PX.Data.IBqlField
		{
		}
		[PXDecimal]
		[PXDBCalced(typeof(IsNull<INItemSiteReplenishment.minQty, INItemSite.minQty>), typeof(decimal))]
		[PXUIField(DisplayName = "Reorder Point")]
		public override Decimal? MinQty
		{
			get
			{
				return this._MinQty;
			}
			set
			{
				this._MinQty = value;
			}
		}
		#endregion
		#region MaxQty
		public new abstract class maxQty : PX.Data.IBqlField
		{
		}
		[PXDecimal]
		[PXDBCalced(typeof(IsNull<INItemSiteReplenishment.maxQty, INItemSite.maxQty>), typeof(decimal))]
		[PXUIField(DisplayName = "Max. Qty.")]
		public override Decimal? MaxQty
		{
			get
			{
				return this._MaxQty;
			}
			set
			{
				this._MaxQty = value;
			}
		}
        #endregion
        #region PurchaseERQ
        public abstract class purchaseERQ : PX.Data.IBqlField
        {
        }
        protected Decimal? _PurchaseERQ;
        [PXDecimal]
		[PXDBCalced(typeof(IsNull<POVendorInventoryRepo.eRQ, POVendorInventoryRepo.nullERQ>), typeof(decimal))]
		[PXUIField(DisplayName = "Purchase ERQ")]
        public virtual Decimal? PurchaseERQ
        {
            get
            {
                return this._PurchaseERQ;
            }
            set
            {
                this._PurchaseERQ = value;
            }
        }
        #endregion
        #region TransferERQ
        public new abstract class transferERQ : PX.Data.IBqlField
		{
		}
		[PXDecimal]
		[PXDBCalced(typeof(IsNull<INItemSiteReplenishment.transferERQ, INItemSite.transferERQ>), typeof(decimal))]
		[PXUIField(DisplayName = "Transfer ERQ")]
		public override Decimal? TransferERQ
		{
			get
			{
				return this._TransferERQ;
			}
			set
			{
				this._TransferERQ = value;
			}
		}
		#endregion
		
		#region LaunchDate
		public new abstract class launchDate : PX.Data.IBqlField
		{
		}
		
		[PXDBDate(BqlField=typeof(INItemSite.launchDate))]
		[PXUIField(DisplayName = "Launch Date")]
		public override DateTime? LaunchDate
		{
			get
			{
				return this._LaunchDate;
			}
			set
			{
				this._LaunchDate = value;
			}
		}
		#endregion		
		#region TerminationDate
		public new abstract class terminationDate : PX.Data.IBqlField
		{
		}
		[PXDBDate(BqlField=typeof(INItemSite.terminationDate))]
		[PXUIField(DisplayName = "Termination Date")]
		public override DateTime? TerminationDate
		{
			get
			{
				return this._TerminationDate;
			}
			set
			{
				this._TerminationDate = value;
			}
		}
		#endregion
		#region ReplenishmentPolicyID
		public new abstract class replenishmentPolicyID : PX.Data.IBqlField
		{
		}		
		[PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlField=typeof(INItemSite.replenishmentPolicyID))]
		[PXUIField(DisplayName = "Replenishment Policy ID")]
		[PXSelector(typeof(Search<INReplenishmentPolicy.replenishmentPolicyID>), DescriptionField = typeof(INReplenishmentPolicy.descr))]
		public override String ReplenishmentPolicyID
		{
			get
			{
				return this._ReplenishmentPolicyID;
			}
			set
			{
				this._ReplenishmentPolicyID = value;
			}
		}
		#endregion
		#region ReplenishmentSource
		public new abstract class replenishmentSource : PX.Data.IBqlField
		{
		}
		
		[PXString(1, IsFixed = true)]
        [PXDBCalced(typeof(IsNull<INItemSite.replenishmentSource, StringEmpty>),typeof(string))]
		[PXUIField(DisplayName = "Replenishment Source")]		
		[INReplenishmentSource.INPlanList]
		[PXDefault(INReplenishmentSource.Purchased, PersistingCheck = PXPersistingCheck.Nothing)]
		public override string ReplenishmentSource
		{
			get
			{
				return this._ReplenishmentSource;
			}
			set
			{
				this._ReplenishmentSource = value;
			}
		}
		#endregion
		#region InclQtySOBackOrdered
		public abstract class inclQtySOBackOrdered : PX.Data.IBqlField
		{
		}
		protected Boolean? _InclQtySOBackOrdered;
		[PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtySOBackOrdered))]
		[PXUIField(DisplayName = "Deduct Qty. On Back Orders")]
		public virtual Boolean? InclQtySOBackOrdered
		{
			get
			{
				return this._InclQtySOBackOrdered;
			}
			set
			{
				this._InclQtySOBackOrdered = value;
			}
		}
		#endregion
		#region InclQtySOPrepared
		public abstract class inclQtySOPrepared : PX.Data.IBqlField
		{
		}
		protected Boolean? _InclQtySOPrepared;
		[PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtySOPrepared))]
		[PXUIField(DisplayName = "Deduct Qty. on Sales Prepared")]
		public virtual Boolean? InclQtySOPrepared
		{
			get
			{
				return this._InclQtySOPrepared;
			}
			set
			{
				this._InclQtySOPrepared = value;
			}
		}
		#endregion
		#region InclQtySOBooked
		public abstract class inclQtySOBooked : PX.Data.IBqlField
		{
		}
		protected Boolean? _InclQtySOBooked;
		[PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtySOBooked))]
		[PXUIField(DisplayName = "Deduct Qty. on Customer Orders")]
		public virtual Boolean? InclQtySOBooked
		{
			get
			{
				return this._InclQtySOBooked;
			}
			set
			{
				this._InclQtySOBooked = value;
			}
		}
		#endregion
		#region InclQtySOShipped
		public abstract class inclQtySOShipped : PX.Data.IBqlField
		{
		}
		protected Boolean? _InclQtySOShipped;
		[PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtySOShipped))]
		[PXUIField(DisplayName = "Deduct Qty. Shipped")]
		public virtual Boolean? InclQtySOShipped
		{
			get
			{
				return this._InclQtySOShipped;
			}
			set
			{
				this._InclQtySOShipped = value;
			}
		}
		#endregion
		#region InclQtySOShipping
		public abstract class inclQtySOShipping : PX.Data.IBqlField
		{
		}
		protected Boolean? _InclQtySOShipping;
		[PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtySOShipping))]
		[PXUIField(DisplayName = "Deduct Qty. Shipping")]
		public virtual Boolean? InclQtySOShipping
		{
			get
			{
				return this._InclQtySOShipping;
			}
			set
			{
				this._InclQtySOShipping = value;
			}
		}
		#endregion
		#region InclQtyINIssues
		public abstract class inclQtyINIssues : PX.Data.IBqlField
		{
		}
		protected Boolean? _InclQtyINIssues;
		[PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtyINIssues))]
		[PXUIField(DisplayName = "Deduct Qty. On Issues")]
		public virtual Boolean? InclQtyINIssues
		{
			get
			{
				return this._InclQtyINIssues;
			}
			set
			{
				this._InclQtyINIssues = value;
			}
		}
		#endregion		
		#region InclQtyINAssemblyDemand
		public abstract class inclQtyINAssemblyDemand : PX.Data.IBqlField
		{
		}
		protected Boolean? _InclQtyINAssemblyDemand;
		[PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtyINAssemblyDemand))]
		[PXUIField(DisplayName = "Deduct Qty. of Kit Assembly Demand")]
		public virtual Boolean? InclQtyINAssemblyDemand
		{
			get
			{
				return this._InclQtyINAssemblyDemand;
			}
			set
			{
				this._InclQtyINAssemblyDemand = value;
			}
		}
        #endregion
        #region InclQtyProductionSupplyPrepared
        public abstract class inclQtyProductionSupplyPrepared : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionSupplyPrepared;
        [PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtyProductionSupplyPrepared))]
        [PXUIField(DisplayName = "Include Qty. of Production Supply Prepared")]
        public virtual Boolean? InclQtyProductionSupplyPrepared
        {
            get
            {
                return this._InclQtyProductionSupplyPrepared;
            }
            set
            {
                this._InclQtyProductionSupplyPrepared = value;
            }
        }
        #endregion
        #region InclQtyProductionSupply
        public abstract class inclQtyProductionSupply : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionSupply;
        [PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtyProductionSupply))]
        [PXUIField(DisplayName = "Include Qty. of Production Supply")]
        public virtual Boolean? InclQtyProductionSupply
        {
            get
            {
                return this._InclQtyProductionSupply;
            }
            set
            {
                this._InclQtyProductionSupply = value;
            }
        }
        #endregion
        #region InclQtyProductionDemandPrepared
        public abstract class inclQtyProductionDemandPrepared : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionDemandPrepared;
        [PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtyProductionDemandPrepared))]
        [PXUIField(DisplayName = "Deduct Qty. on Production Demand Prepared")]
        public virtual Boolean? InclQtyProductionDemandPrepared
        {
            get
            {
                return this._InclQtyProductionDemandPrepared;
            }
            set
            {
                this._InclQtyProductionDemandPrepared = value;
            }
        }
        #endregion
        #region InclQtyProductionDemand
        public abstract class inclQtyProductionDemand : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionDemand;
        [PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtyProductionDemand))]
        [PXUIField(DisplayName = "Deduct Qty. on Production Demand")]
        public virtual Boolean? InclQtyProductionDemand
        {
            get
            {
                return this._InclQtyProductionDemand;
            }
            set
            {
                this._InclQtyProductionDemand = value;
            }
        }
        #endregion
        #region InclQtyProductionAllocated
        public abstract class inclQtyProductionAllocated : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionAllocated;
        [PXDBBool(BqlField = typeof(INAvailabilityScheme.inclQtyProductionAllocated))]
        [PXUIField(DisplayName = "Deduct Qty. on Production Allocated")]
        public virtual Boolean? InclQtyProductionAllocated
        {
            get
            {
                return this._InclQtyProductionAllocated;
            }
            set
            {
                this._InclQtyProductionAllocated = value;
            }
        }
        #endregion
        #region QtyProcess
        public abstract class qtyProcess : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyProcess;
		[PXQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. To Process")]
		public virtual Decimal? QtyProcess
		{
			get
			{
				return this._QtyProcess;
			}
			set
			{
				this._QtyProcess = value;
			}
		}
		#endregion
		#region QtyOnHand
		public abstract class qtyOnHand : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyOnHand;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyOnHand, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]		
		[PXUIField(DisplayName="Qty. On Hand")]
		public virtual Decimal? QtyOnHand
		{
			get
			{
				return this._QtyOnHand;
			}
			set
			{
				this._QtyOnHand = value;
			}
		}
		#endregion
		#region QtyPOPrepared
		public abstract class qtyPOPrepared : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyPOPrepared;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyPOPrepared, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. PO Prepared", Visibility = PXUIVisibility.Visible, Visible =  false)]
		public virtual Decimal? QtyPOPrepared
		{
			get
			{
				return this._QtyPOPrepared;
			}
			set
			{
				this._QtyPOPrepared = value;
			}
		}
		#endregion
		#region QtyPOOrders
		public abstract class qtyPOOrders : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyPOOrders;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyPOOrders, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. PO Orders", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtyPOOrders
		{
			get
			{
				return this._QtyPOOrders;
			}
			set
			{
				this._QtyPOOrders = value;
			}
		}
		#endregion
		#region QtyPOReceipts
		public abstract class qtyPOReceipts : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyPOReceipts;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyPOReceipts, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. PO Receipts", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtyPOReceipts
		{
			get
			{
				return this._QtyPOReceipts;
			}
			set
			{
				this._QtyPOReceipts = value;
			}
		}
		#endregion
		#region QtyInTransit
		public abstract class qtyInTransit : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyInTransit;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyInTransit, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. IN Transit", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtyInTransit
		{
			get
			{
				return this._QtyInTransit;
			}
			set
			{
				this._QtyInTransit = value;
			}
		}
		#endregion
		#region QtyINReceipts
		public abstract class qtyINReceipts : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyINReceipts;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyINReceipts, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]		
		[PXUIField(DisplayName = "Qty. IN Receipts", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtyINReceipts
		{
			get
			{
				return this._QtyINReceipts;
			}
			set
			{
				this._QtyINReceipts = value;
			}
		}
		#endregion		
		#region QtyINAssemblySupply
		public abstract class qtyINAssemblySupply : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyINAssemblySupply;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyINAssemblySupply, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]		
		[PXUIField(DisplayName = "Qty. IN Assembly Supply", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtyINAssemblySupply
		{
			get
			{
				return this._QtyINAssemblySupply;
			}
			set
			{
				this._QtyINAssemblySupply = value;
			}
		}
        #endregion
        #region QtyInTransitToProduction
        public abstract class qtyInTransitToProduction : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyInTransitToProduction;
        [PXQuantity]
        [PXDBCalced(typeof(IsNull<INSiteStatus.qtyInTransitToProduction, decimal0>), typeof(decimal))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty In Transit to Production")]
        public virtual Decimal? QtyInTransitToProduction
        {
            get
            {
                return this._QtyInTransitToProduction;
            }
            set
            {
                this._QtyInTransitToProduction = value;
            }
        }
        #endregion
        #region QtyProductionSupplyPrepared
        public abstract class qtyProductionSupplyPrepared : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyProductionSupplyPrepared;
        [PXQuantity]
        [PXDBCalced(typeof(IsNull<INSiteStatus.qtyProductionSupplyPrepared, decimal0>), typeof(decimal))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty Production Supply Prepared")]
        public virtual Decimal? QtyProductionSupplyPrepared
        {
            get
            {
                return this._QtyProductionSupplyPrepared;
            }
            set
            {
                this._QtyProductionSupplyPrepared = value;
            }
        }
        #endregion
        #region QtyProductionSupply
        public abstract class qtyProductionSupply : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyProductionSupply;
        [PXQuantity]
        [PXDBCalced(typeof(IsNull<INSiteStatus.qtyProductionSupply, decimal0>), typeof(decimal))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty On Production Supply")]
        public virtual Decimal? QtyProductionSupply
        {
            get
            {
                return this._QtyProductionSupply;
            }
            set
            {
                this._QtyProductionSupply = value;
            }
        }
        #endregion
        #region QtyPOFixedProductionPrepared
        public abstract class qtyPOFixedProductionPrepared : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyPOFixedProductionPrepared;
        [PXQuantity]
        [PXDBCalced(typeof(IsNull<INSiteStatus.qtyPOFixedProductionPrepared, decimal0>), typeof(decimal))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty On Purchase for Prod. Prepared")]
        public virtual Decimal? QtyPOFixedProductionPrepared
        {
            get
            {
                return this._QtyPOFixedProductionPrepared;
            }
            set
            {
                this._QtyPOFixedProductionPrepared = value;
            }
        }
        #endregion
        #region QtyPOFixedProductionOrders
        public abstract class qtyPOFixedProductionOrders : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyPOFixedProductionOrders;
        [PXQuantity]
        [PXDBCalced(typeof(IsNull<INSiteStatus.qtyPOFixedProductionOrders, decimal0>), typeof(decimal))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty On Purchase for Production")]
        public virtual Decimal? QtyPOFixedProductionOrders
        {
            get
            {
                return this._QtyPOFixedProductionOrders;
            }
            set
            {
                this._QtyPOFixedProductionOrders = value;
            }
        }
        #endregion
        #region QtySOBackOrdered
        public abstract class qtySOBackOrdered : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtySOBackOrdered;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtySOBackOrdered, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]		
		[PXUIField(DisplayName = "Qty. SO Back Ordered", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtySOBackOrdered
		{
			get
			{
				return this._QtySOBackOrdered;
			}
			set
			{
				this._QtySOBackOrdered = value;
			}
		}
		#endregion
		#region QtySOPrepared
		public abstract class qtySOPrepared : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtySOPrepared;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtySOPrepared, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. SO Prepared", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtySOPrepared
		{
			get
			{
				return this._QtySOPrepared;
			}
			set
			{
				this._QtySOPrepared = value;
			}
		}
		#endregion
		#region QtySOBooked
		public abstract class qtySOBooked : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtySOBooked;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtySOBooked, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]		
		[PXUIField(DisplayName = "Qty. SO Booked", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtySOBooked
		{
			get
			{
				return this._QtySOBooked;
			}
			set
			{
				this._QtySOBooked = value;
			}
		}
		#endregion
		#region QtySOShipped
		public abstract class qtySOShipped : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtySOShipped;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtySOShipped, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]		
		[PXUIField(DisplayName = "Qty. SO Shipped", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtySOShipped
		{
			get
			{
				return this._QtySOShipped;
			}
			set
			{
				this._QtySOShipped = value;
			}
		}
		#endregion
		#region QtySOShipping
		public abstract class qtySOShipping : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtySOShipping;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtySOShipping, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]	
		[PXUIField(DisplayName = "Qty. SO Allocated", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtySOShipping
		{
			get
			{
				return this._QtySOShipping;
			}
			set
			{
				this._QtySOShipping = value;
			}
		}
		#endregion
		#region QtyINIssues
		public abstract class qtyINIssues : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyINIssues;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyINIssues, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]	
		[PXUIField(DisplayName = "Qty. IN Issues", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtyINIssues
		{
			get
			{
				return this._QtyINIssues;
			}
			set
			{
				this._QtyINIssues = value;
			}
		}
		#endregion
		#region QtyINAssemblyDemand
		public abstract class qtyINAssemblyDemand : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyINAssemblyDemand;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyINAssemblyDemand, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]	
		[PXUIField(DisplayName = "Qty. IN Assembly Demand", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Decimal? QtyINAssemblyDemand
		{
			get
			{
				return this._QtyINAssemblyDemand;
			}
			set
			{
				this._QtyINAssemblyDemand = value;
			}
		}
        #endregion
        #region QtyProductionDemandPrepared
        public abstract class qtyProductionDemandPrepared : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyProductionDemandPrepared;
        [PXQuantity]
        [PXDBCalced(typeof(IsNull<INSiteStatus.qtyProductionDemandPrepared, decimal0>), typeof(decimal))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty On Production Demand Prepared")]
        public virtual Decimal? QtyProductionDemandPrepared
        {
            get
            {
                return this._QtyProductionDemandPrepared;
            }
            set
            {
                this._QtyProductionDemandPrepared = value;
            }
        }
        #endregion
        #region QtyProductionDemand
        public abstract class qtyProductionDemand : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyProductionDemand;
        [PXQuantity]
        [PXDBCalced(typeof(IsNull<INSiteStatus.qtyProductionDemand, decimal0>), typeof(decimal))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty On Production Demand")]
        public virtual Decimal? QtyProductionDemand
        {
            get
            {
                return this._QtyProductionDemand;
            }
            set
            {
                this._QtyProductionDemand = value;
            }
        }
        #endregion
        #region QtyProductionAllocated
        public abstract class qtyProductionAllocated : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyProductionAllocated;
        [PXQuantity]
        [PXDBCalced(typeof(IsNull<INSiteStatus.qtyProductionAllocated, decimal0>), typeof(decimal))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty On Production Allocated")]
        public virtual Decimal? QtyProductionAllocated
        {
            get
            {
                return this._QtyProductionAllocated;
            }
            set
            {
                this._QtyProductionAllocated = value;
            }
        }
        #endregion
        #region QtySOFixedProduction
        public abstract class qtySOFixedProduction : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtySOFixedProduction;
        [PXQuantity]
        [PXDBCalced(typeof(IsNull<INSiteStatus.qtySOFixedProduction, decimal0>), typeof(decimal))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty On SO to Production")]
        public virtual Decimal? QtySOFixedProduction
        {
            get
            {
                return this._QtySOFixedProduction;
            }
            set
            {
                this._QtySOFixedProduction = value;
            }
        }
        #endregion
        #region QtyINReplaned
        public abstract class qtyINReplaned : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyINReplaned;
		[PXQuantity]
		[PXDBCalced(typeof(IsNull<INSiteStatus.qtyINReplaned, decimal0>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. Replaned")]
		public virtual Decimal? QtyINReplaned
		{
			get
			{
				return this._QtyINReplaned;
			}
			set
			{
				this._QtyINReplaned = value;
			}
		}
		#endregion		
		#region QtyReplenishment
		public abstract class qtyReplenishment : PX.Data.IBqlField
		{
		}
		protected decimal? _QtyReplenishment;
		[PXDecimal]
		[PXDBCalced(typeof(Add<INReplenishmentItem.qtyPOPrepared,
					Add<INReplenishmentItem.qtyPOOrders,
					Add<INReplenishmentItem.qtyPOReceipts,
					Add<INReplenishmentItem.qtyInTransit,
					Add<INReplenishmentItem.qtyINReceipts, INReplenishmentItem.qtyINAssemblySupply>>>>>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. on Supply")]
		public virtual Decimal? QtyReplenishment
		{
			get
			{
				return this._QtyReplenishment;
			}
			set
			{
				this._QtyReplenishment = value;
			}
		}
		#endregion	
		#region QtyHardDemand
		public abstract class qtyHardDemand : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyHardDemand;
		[PXDecimal]
		[PXDBCalced(typeof(Add<INReplenishmentItem.qtySOShipped, Add<INReplenishmentItem.qtySOShipping, INReplenishmentItem.qtySOBackOrdered>>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. on Hard Demand")]
		public virtual Decimal? QtyHardDemand
		{
			get
			{
				return this._QtyHardDemand;
			}
			set
			{
				this._QtyHardDemand = value;
			}
		}
		#endregion	
		#region QtyDemand
		public abstract class qtyDemand : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyDemand;
		[PXDecimal]
		[PXDBCalced(typeof(Add<IIf<Where<INAvailabilityScheme.inclQtySOPrepared, Equal<True>>, INReplenishmentItem.qtySOPrepared, decimal0>,
				Add<IIf<Where<INAvailabilityScheme.inclQtySOBooked, Equal<True>>, INReplenishmentItem.qtySOBooked, decimal0>,
				Add<IIf<Where<INAvailabilityScheme.inclQtySOShipping, Equal<True>>, INReplenishmentItem.qtySOShipping, decimal0>,
				Add<IIf<Where<INAvailabilityScheme.inclQtySOShipped, Equal<True>>, INReplenishmentItem.qtySOShipped, decimal0>,
				Add<IIf<Where<INAvailabilityScheme.inclQtySOBackOrdered, Equal<True>>, INReplenishmentItem.qtySOBackOrdered, decimal0>,
				Add<IIf<Where<INAvailabilityScheme.inclQtyINIssues, Equal<True>>, INReplenishmentItem.qtyINIssues, decimal0>,
				Add<IIf<Where<INAvailabilityScheme.inclQtyINAssemblyDemand, Equal<True>>, INReplenishmentItem.qtyINAssemblyDemand, decimal0>,
				Add<IIf<Where<INAvailabilityScheme.inclQtyProductionDemandPrepared, Equal<True>>, INReplenishmentItem.qtyProductionDemandPrepared, decimal0>,
				Add<IIf<Where<INAvailabilityScheme.inclQtyProductionDemand, Equal<True>>, INReplenishmentItem.qtyProductionDemand, decimal0>,
				IIf<Where<INAvailabilityScheme.inclQtyProductionAllocated, Equal<True>>, INReplenishmentItem.qtyProductionAllocated, decimal0>>>>>>>>>>), typeof(decimal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. on Demand")]
		public virtual Decimal? QtyDemand
		{
			get
			{
				return this._QtyDemand;
			}
			set
			{
				this._QtyDemand = value;
			}
		}
        #endregion

        public class effectiveOnHand : IBqlCreator, IBqlOperand
        {
            IBqlCreator _formula = new Add<IsNull<INReplenishmentItem.qtyOnHand, decimal0>,
            Add<IsNull<INReplenishmentItem.qtyReplenishment, decimal0>,
            Sub<IsNull<INReplenishmentItem.qtyINReplaned, decimal0>,
            Switch<Case<Where<INReplenishmentItem.demandCalculation, Equal<INDemandCalculation.hardDemand>>,
            IsNull<INReplenishmentItem.qtyHardDemand, decimal0>>,
            IsNull<INReplenishmentItem.qtyDemand, decimal0>>>>>();

            public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, BqlCommand.Selection selection)
            {
                _formula.Parse(graph, pars, tables, fields, sortColumns, text, selection);
            }

            public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
            {
                _formula.Verify(cache, item, pars, ref result, ref value);
            }
        }

        public class QtyProcessIntFormula : IBqlCreator, IBqlOperand
        {
            IBqlCreator _formula = new
                Switch<
                    Case<Where<effectiveOnHand, LessEqual<IsNull<INReplenishmentItem.minQty, decimal0>>,
                        And<Where<
                            INReplenishmentItem.replenishmentMethod, IsNotNull,
                            And<INReplenishmentItem.replenishmentMethod,NotEqual<INReplenishmentMethod.none>>>>>,
                        Switch<
                            Case<
                                Where<INReplenishmentItem.replenishmentMethod, Equal<INReplenishmentMethod.minMax>>,
                                Sub<IsNull<INReplenishmentItem.maxQty, decimal0>, effectiveOnHand>,
                            Case<
                                Where<INReplenishmentItem.replenishmentMethod, Equal<INReplenishmentMethod.fixedReorder>>,
                                Switch<
                                    Case<Where<INReplenishmentItem.replenishmentSource, Equal<INReplenishmentSource.transfer>>, 
                                        IsNull<INReplenishmentItem.transferERQ, decimal0>,
                                    Case<Where<INReplenishmentItem.replenishmentSource, Equal<INReplenishmentSource.purchased>>,
                                        IsNull<INReplenishmentItem.purchaseERQ, decimal0>>>, 
                                    decimal0>>>,
                            decimal0>>,
                        decimal0>();

            public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, BqlCommand.Selection selection)
            {
                _formula.Parse(graph, pars, tables, fields, sortColumns, text, selection);
            }

            public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
            {
                _formula.Verify(cache, item, pars, ref result, ref value);
            }
        }

        #region QtyProcessInt
        public abstract class qtyProcessInt : PX.Data.IBqlField
        {
        }
        protected decimal? _QtyProcessInt;
        [PXDecimal]
        [PXDBCalced(typeof(QtyProcessIntFormula),typeof(decimal))]
        public virtual decimal? QtyProcessInt
        {
            get
            {
                return this._QtyProcessInt;
            }
            set
            {
                this._QtyProcessInt = value;
            }
        }
        #endregion
        #region DefaultSubItemID
        public abstract class defaultSubItemID : PX.Data.IBqlField
		{
		}
		protected Int32? _DefaultSubItemID;
		[SubItem(BqlField = typeof(InventoryItem.defaultSubItemID))]
		public virtual Int32? DefaultSubItemID
		{
			get
			{
				return this._DefaultSubItemID;
			}
			set
			{
				this._DefaultSubItemID = value;
			}
		}
        #endregion
    }

    [PX.Objects.GL.TableAndChartDashboardType]
	public class INReplenishmentCreate: PXGraph<INReplenishmentCreate>
	{
		//Cache initialize
		public PXFilter<Vendor> _vendor;

		public PXFilter<INReplenishmentFilter> Filter;
		public PXCancel<INReplenishmentFilter> Cancel;
		public PXAction<INReplenishmentFilter> ViewVendorCatalogue;
		[PXFilterable]
		public Processing<INReplenishmentItem> Records;

		public PXAction<INReplenishmentFilter> ViewInventoryItem;
		[PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton]
		public virtual IEnumerable viewInventoryItem(PXAdapter adapter)
		{
			if (Records.Current != null && Records.Current.InventoryID.HasValue)
			{
				InventoryItemMaint graph = PXGraph.CreateInstance<InventoryItemMaint>();
				InventoryItem invItem = graph.Item.Search<InventoryItem.inventoryID>(Records.Current.InventoryID);
				if (invItem != null)
				{
					graph.Item.Current = invItem;
					throw new PXRedirectRequiredException(graph, true, "View Inventory Item") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}
			}
			return adapter.Get();
		}

		
		[PXUIField(DisplayName = "View Vendor Inventory", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton]
		public virtual IEnumerable viewVendorCatalogue(PXAdapter adapter)
		{
			if (Records.Current != null && Records.Current.InventoryID.HasValue)
			{
				POVendorCatalogueMaint graph = PXGraph.CreateInstance<POVendorCatalogueMaint>();
				VendorLocation baccount = PXSelect<VendorLocation,
					Where<VendorLocation.bAccountID, Equal<Required<VendorLocation.bAccountID>>,
						And<VendorLocation.locationID, Equal<Required<VendorLocation.locationID>>>>>
						.Select(graph, Records.Current.PreferredVendorID, Records.Current.PreferredVendorLocationID);

				if (baccount != null)
				{
					graph.BAccount.Current = baccount;
					throw new PXRedirectRequiredException(graph, true, "View Vendor Catalogue") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}
			}
			return adapter.Get();
		}
		public class Processing<Type> :
			PXFilteredProcessingJoin<INReplenishmentItem, INReplenishmentFilter,
			LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<INReplenishmentItem.itemClassID>>>,
		Where<INReplenishmentItem.siteID, Equal<Current<INReplenishmentFilter.replenishmentSiteID>>,						
			And2<Where<
				Current<INReplenishmentFilter.itemClassCDWildcard>, IsNull,
				Or<INItemClass.itemClassCD, Like<Current<INReplenishmentFilter.itemClassCDWildcard>>>>,
			And2<Where<
				INReplenishmentItem.launchDate, IsNull,
						Or<INReplenishmentItem.launchDate, LessEqual<Current<INReplenishmentFilter.purchaseDate>>>>,
			And<Where<
				INReplenishmentItem.terminationDate, IsNull,
						Or<INReplenishmentItem.terminationDate, GreaterEqual<Current<INReplenishmentFilter.purchaseDate>>>>>
			>>>>
		where Type : INReplenishmentItem
		{
			
			public Processing(PXGraph graph)
				: base(graph)
			{
				this._OuterView = new PXView(graph, false, BqlCommand.CreateInstance(
                    typeof(
                        Select2<INReplenishmentItem,
						LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<INReplenishmentItem.itemClassID>>>,
                        Where<INReplenishmentItem.siteID, Equal<Current<INReplenishmentFilter.replenishmentSiteID>>,
						And2<Where<
							Current<INReplenishmentFilter.itemClassCDWildcard>, IsNull,
							Or<INItemClass.itemClassCD, Like<Current<INReplenishmentFilter.itemClassCDWildcard>>>>,
						And2<Where<
							INReplenishmentItem.launchDate, IsNull,
                        Or<INReplenishmentItem.launchDate, LessEqual<Current<INReplenishmentFilter.purchaseDate>>>>,
                        And<Where<
							INReplenishmentItem.terminationDate, IsNull,
                        Or<INReplenishmentItem.terminationDate, GreaterEqual<Current<INReplenishmentFilter.purchaseDate>>>>>
                        >>>>)));

                this._OuterView.WhereAndCurrent<INReplenishmentFilter>(nameof(INReplenishmentFilter.itemClassCDWildcard), nameof(INReplenishmentFilter.itemClassCD));
                this._OuterView.WhereAnd<Where<Current<INReplenishmentFilter.onlySuggested>, Equal<False>, Or<INReplenishmentItem.qtyProcessInt,Greater<decimal0>>>>();
			}
		}

		public INReplenishmentCreate()
		{
			INReplenishmentFilter filter = Filter.Current;
			Records.SetProcessDelegate(delegate(List<INReplenishmentItem> list)
			{
				ReplenishmentCreateProc(list, filter);
			});
		}

		protected virtual void INReplenishmentFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			PXUIFieldAttribute.SetEnabled<INReplenishmentFilter.replenishmentSiteID>(sender, null, PXAccess.FeatureInstalled<FeaturesSet.warehouse>());

			PXUIFieldAttribute.SetEnabled(this.Records.Cache, null, false);
			PXUIFieldAttribute.SetEnabled<INReplenishmentItem.selected>(this.Records.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<INReplenishmentItem.qtyProcess>(this.Records.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<INReplenishmentItem.replenishmentSource>(this.Records.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<INReplenishmentItem.replenishmentSourceSiteID>(this.Records.Cache, null, PXAccess.FeatureInstalled<FeaturesSet.warehouse>());
			PXUIFieldAttribute.SetEnabled<INReplenishmentItem.preferredVendorID>(this.Records.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<INReplenishmentItem.preferredVendorLocationID>(this.Records.Cache, null, true);
		}		
        protected virtual void INReplenishmentItem_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			INReplenishmentItem rec = (INReplenishmentItem)e.Row;
			if (rec == null) return;
            if(rec.QtyProcessInt == null)
                PXDBCalcedAttribute.Calculate<INReplenishmentItem.qtyProcessInt>(this.Caches[typeof(INReplenishmentItem)], rec);
            if(rec.QtyProcess == null)
                rec.QtyProcess = RecalcQty(rec);
            PXUIFieldAttribute.SetEnabled<INReplenishmentItem.replenishmentSourceSiteID>(sender, rec, PXAccess.FeatureInstalled<FeaturesSet.warehouse>() && INReplenishmentSource.IsTransfer(rec.ReplenishmentSource));
			PXUIFieldAttribute.SetEnabled<INReplenishmentItem.preferredVendorID>(sender, rec, rec.ReplenishmentSource == INReplenishmentSource.Purchased);
			PXUIFieldAttribute.SetEnabled<INReplenishmentItem.preferredVendorLocationID>(sender, rec, rec.ReplenishmentSource == INReplenishmentSource.Purchased);
			if (rec.QtyINReplaned > 0 && rec.Selected == true && rec.QtyProcess == 0)
				sender.RaiseExceptionHandling<INReplenishmentItem.qtyProcess>(rec, rec.QtyProcess,
					new PXSetPropertyException(Messages.ReplenihmentPlanDeleted, PXErrorLevel.Warning));
		}
		protected virtual void INReplenishmentItem_QtyProcess_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			INReplenishmentItem rec = (INReplenishmentItem)e.Row;
			if (rec != null && rec.QtyProcess.HasValue)			
				rec.Selected = rec.QtyProcess > 0;			
		}
		protected virtual void INReplenishmentItem_ReplenishmentSource_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			INReplenishmentItem rec = (INReplenishmentItem)e.Row;
			if (rec != null)
			{
				rec.QtyProcessInt = null;
                rec.QtyProcess = null;
			}
		}
		protected virtual void INReplenishmentItem_PreferredVendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			INReplenishmentItem rec = (INReplenishmentItem)e.Row;
			if(rec != null)
			{				
			}
		}
		protected virtual void INReplenishmentItem_PreferredVendorLocationID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			INReplenishmentItem rec = (INReplenishmentItem)e.Row;
			if (rec != null)
		    {
                rec.QtyProcessInt = null;
                rec.QtyProcess = null;
            }
		}

		protected decimal? RecalcQty(INReplenishmentItem rec)
		{
            Decimal qty = rec.QtyProcessInt ?? 0m;

			if (qty > 0 && rec.ReplenishmentSource != INReplenishmentSource.Transfer)			
				qty = OnRoundQtyByVendor(rec, qty);
			
			return qty > 0? qty : 0m;
		}

		protected virtual decimal OnRoundQtyByVendor(INReplenishmentItem rec, decimal qty)
		{
			POVendorInventory vendorSettings =
					FetchVendorSettings(this, rec) ?? new POVendorInventory();

			if (rec.ReplenishmentMethod == INReplenishmentMethod.FixedReorder)
						qty = vendorSettings.ERQ.GetValueOrDefault();
					else
					{
						if (vendorSettings.LotSize > 0)
						{
							Decimal size = vendorSettings.LotSize.GetValueOrDefault();
							qty = decimal.Ceiling(qty / size) * size;
						}
						if (qty < vendorSettings.MinOrdQty.GetValueOrDefault())
							qty = vendorSettings.MinOrdQty.GetValueOrDefault();

						Decimal maxOrderQty = vendorSettings.MaxOrdQty ?? Decimal.Zero; //By default, in DB this value is 0, rather then null. So 0 is considered as "not set" for this value
						if (maxOrderQty > 0 && qty > maxOrderQty)
							qty = maxOrderQty;		
						
					}					
			return qty;
		}
		

		protected static void ReplenishmentCreateProc(List<INReplenishmentItem> list, INReplenishmentFilter filter)
		{
			INReplenishmentMaint graph = PXGraph.CreateInstance<INReplenishmentMaint>();						
			int index = 0;
			bool updated = false;
			foreach (INReplenishmentItem rec in list)
			{
				index +=1;
				if (rec.Selected == true || rec.QtyProcess > 0)
					foreach (INItemPlan e in
					PXSelect<INItemPlan,
					Where<INItemPlan.planType, Equal<Required<INItemPlan.planType>>,
					And<INItemPlan.inventoryID, Equal<Required<INItemPlan.inventoryID>>,
					And<INItemPlan.subItemID, Equal<Required<INItemPlan.subItemID>>,
					And<INItemPlan.siteID, Equal<Required<INItemPlan.siteID>>,
						And<INItemPlan.supplyPlanID, IsNull>>>>>>.Select(graph, INPlanConstants.Plan90, rec.InventoryID, rec.SubItemID, rec.SiteID))
						graph.Plans.Delete(e);

				if (rec.QtyProcess > 0)
				{

					INItemPlan plan = new INItemPlan();
					plan.InventoryID = rec.InventoryID;
					plan.SubItemID = rec.SubItemID;
					plan.SiteID = rec.SiteID;
					int? replenishmentSourceSiteID = PXAccess.FeatureInstalled<FeaturesSet.warehouse>() ? rec.ReplenishmentSourceSiteID : null;
					plan.SourceSiteID = replenishmentSourceSiteID;
					plan.VendorID = rec.ReplenishmentSource == INReplenishmentSource.Purchased ? rec.PreferredVendorID : null;
					plan.VendorLocationID = rec.ReplenishmentSource == INReplenishmentSource.Purchased
					                        	? rec.PreferredVendorLocationID
					                        	: null;
					plan.PlanQty = rec.QtyProcess;
					plan.PlanDate = filter.PurchaseDate;

                    plan.FixedSource = replenishmentSourceSiteID != null 
                        ? rec.ReplenishmentSource == INReplenishmentSource.Purchased 
                            ? INReplenishmentSource.TransferToPurchase 
                            : INReplenishmentSource.Transfer 
                        : INReplenishmentSource.Purchased;

					plan.PlanType = INPlanConstants.Plan90;

					plan.Hold = false;
					graph.Plans.Update(plan);
					updated = true;
				}
			}
			if (updated)
			{				
				INReplenishmentOrder order = new INReplenishmentOrder();
				order.OrderDate = filter.PurchaseDate;
				order.SiteID = filter.ReplenishmentSiteID;
				order.VendorID = filter.PreferredVendorID;								
				order = graph.Document.Update(order);
			}
				graph.Save.Press();
		}

		private static POVendorInventory FetchVendorSettings(PXGraph graph, INReplenishmentItem r)
		{
			var view = 
			new	PXSelect<POVendorInventory,
			Where<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>,
			And<POVendorInventory.subItemID, Equal<Required<POVendorInventory.subItemID>>,
			And<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>,
			And<Where2<Where<Required<POVendorInventory.vendorLocationID>, IsNull, And<POVendorInventory.vendorLocationID, IsNull>>,
					    Or<POVendorInventory.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>>>>>>>>(graph);

			return
				(POVendorInventory)view.SelectWindowed(0, 1, r.InventoryID, r.SubItemID, r.PreferredVendorID, r.PreferredVendorLocationID, r.PreferredVendorLocationID) ??
				(POVendorInventory)view.SelectWindowed(0, 1, r.InventoryID, r.DefaultSubItemID, r.PreferredVendorID, r.PreferredVendorLocationID, r.PreferredVendorLocationID) ??
				(POVendorInventory)view.SelectWindowed(0, 1, r.InventoryID, r.SubItemID, r.PreferredVendorID, null, null) ??
				(POVendorInventory)view.SelectWindowed(0, 1, r.InventoryID, r.DefaultSubItemID, r.PreferredVendorID, null, null);
		}
	}
}

