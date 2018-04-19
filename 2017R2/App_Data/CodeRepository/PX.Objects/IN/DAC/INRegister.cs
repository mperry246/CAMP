using PX.Data.ReferentialIntegrity.Attributes;

namespace PX.Objects.IN
{
	using System;
	using PX.Data;
	using PX.Objects.GL;
	using PX.Objects.CS;
	using PX.Objects.CM;
	using PX.Objects.SO;
	using PX.Objects.PO;

	[System.SerializableAttribute()]	

	[PXPrimaryGraph(new Type[] {
					typeof(INReceiptEntry),
					typeof(INIssueEntry),
					typeof(INTransferEntry),
					typeof(INAdjustmentEntry),
					typeof(KitAssemblyEntry),
					typeof(KitAssemblyEntry)},
					new Type[] {
					typeof(Where<INRegister.docType, Equal<INDocType.receipt>>),
					typeof(Where<INRegister.docType, Equal<INDocType.issue>>),
					typeof(Where<INRegister.docType, Equal<INDocType.transfer>>),
					typeof(Where<INRegister.docType, Equal<INDocType.adjustment>>),
					typeof(Select<INKitRegister, Where<INKitRegister.docType, Equal<INDocType.production>, And<INKitRegister.refNbr, Equal<Current<INRegister.refNbr>>>>>),
					typeof(Select<INKitRegister, Where<INKitRegister.docType, Equal<INDocType.disassembly>, And<INKitRegister.refNbr, Equal<Current<INRegister.refNbr>>>>>),
					})]
	[INRegisterCacheName(Messages.Register)]
	public partial class INRegister : PX.Data.IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		protected bool? _Selected = false;
		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected
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
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch()]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region DocType
		public abstract class docType : PX.Data.IBqlField
		{
			public const string DisplayName = "Document Type";
		}
		protected String _DocType;
		[PXDBString(1, IsKey = true, IsFixed = true)]
		[PXDefault()]
		[INDocType.List()]
		[PXUIField(DisplayName = docType.DisplayName, Enabled = false, Visibility=PXUIVisibility.SelectorVisible)]
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
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
			public const string DisplayName = "Reference Nbr.";
		}
		protected String _RefNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault()]
		[PXUIField(DisplayName= refNbr.DisplayName, Visibility=PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<INRegister.refNbr, Where<INRegister.docType, Equal<Optional<INRegister.docType>>>, OrderBy<Desc<INRegister.refNbr>>>), Filterable = true)]
		[INDocType.Numbering()]
		[PX.Data.EP.PXFieldDescription]
		public virtual String RefNbr
		{
			get
			{
				return this._RefNbr;
			}
			set
			{
				this._RefNbr = value;
			}
		}
		#endregion
		#region OrigModule
		public abstract class origModule : PX.Data.IBqlField
		{
            public const string PI = "PI";

			public class List : PXStringListAttribute
			{
				public List() : base(
					new[]
					{
						Pair(BatchModule.SO, GL.Messages.ModuleSO),
						Pair(BatchModule.PO, GL.Messages.ModulePO),
						Pair(BatchModule.IN, GL.Messages.ModuleIN),
						Pair(PI, Messages.ModulePI),
						Pair(BatchModule.AP, GL.Messages.ModuleAP),
					}) {}
			}
		}
		protected String _OrigModule;
		[PXDBString(2, IsFixed = true)]
		[PXDefault(GL.BatchModule.IN)]
		[PXUIField(DisplayName = "Source", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[origModule.List()]
		public virtual String OrigModule
		{
			get
			{
				return this._OrigModule;
			}
			set
			{
				this._OrigModule = value;
			}
		}
		#endregion
        #region OrigRefNbr
        public abstract class origRefNbr : PX.Data.IBqlField
        {
        }
        protected String _OrigRefNbr;
        [PXDBString(15, IsUnicode = true)]
        public virtual String OrigRefNbr
        {
            get
            {
                return this._OrigRefNbr;
            }
            set
            {
                this._OrigRefNbr = value;
            }
        }
        #endregion
		/// <summary>
		/// The following two unbound fields are used for consolidation by Shipment Number of IN Issues created for the same Invoice
		/// </summary>
		#region OrigShipmentType
		public abstract class origShipmentType : IBqlField { }
		[PXString(1)]
		public virtual string OrigShipmentType { get; set; }
		#endregion
		#region OrigShipmentNbr
		public abstract class origShipmentNbr : IBqlField { }
		[PXString(15, IsUnicode = true)]
		public virtual string OrigShipmentNbr { get; set; }
		#endregion
		#region SiteID
		public abstract class siteID : PX.Data.IBqlField
		{
		}
		protected Int32? _SiteID;
		[IN.Site(DisplayName = "Warehouse ID", DescriptionField=typeof(INSite.descr))]
		[PXForeignReference(typeof(Field<siteID>.IsRelatedTo<INSite.siteID>))]
		public virtual Int32? SiteID
		{
			get
			{
				return this._SiteID;
			}
			set
			{
				this._SiteID = value;
			}
		}
		#endregion
		#region ToSiteID
		public abstract class toSiteID : PX.Data.IBqlField
		{
		}
		protected Int32? _ToSiteID;
		[IN.ToSite(DisplayName = "To Warehouse ID",DescriptionField = typeof(INSite.descr))]
		[PXForeignReference(typeof(Field<toSiteID>.IsRelatedTo<INSite.siteID>))]
		public virtual Int32? ToSiteID
		{
			get
			{
				return this._ToSiteID;
			}
			set
			{
				this._ToSiteID = value;
			}
		}
		#endregion
		#region TransferType
		public abstract class transferType : PX.Data.IBqlField
		{
		}
		protected String _TransferType;
		[PXDBString(1, IsFixed = true)]
		[PXDefault(INTransferType.OneStep)]
		[INTransferType.List()]
		[PXUIField(DisplayName = "Transfer Type")]
		public virtual String TransferType
		{
			get
			{
				return this._TransferType;
			}
			set
			{
				this._TransferType = value;
			}
		}
		#endregion
        /// <summary>
        /// Field used in INReceiptEntry screen. 
        /// Unbound, calculated field. Filled up only on correspondent screen.
        /// </summary>
		#region TransferNbr
		public abstract class transferNbr : PX.Data.IBqlField
		{
		}
		protected String _TransferNbr;
		[PXString(15, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Transfer Nbr.")]
		public virtual String TransferNbr
		{
			get
			{
				return this._TransferNbr;
			}
			set
			{
				this._TransferNbr = value;
			}
		}
		#endregion
		#region TranDesc
		public abstract class tranDesc : PX.Data.IBqlField
		{
		}
		protected String _TranDesc;
		[PXDBString(256, IsUnicode = true)]
		[PXUIField(DisplayName="Description", Visibility=PXUIVisibility.SelectorVisible)]
		public virtual String TranDesc
		{
			get
			{
				return this._TranDesc;
			}
			set
			{
				this._TranDesc = value;
			}
		}
		#endregion
		#region Released
		public abstract class released : PX.Data.IBqlField
		{
		}
		protected Boolean? _Released;
		[PXDBBool()]
		[PXDefault(false)]
		public virtual Boolean? Released
		{
			get
			{
				return this._Released;
			}
			set
			{
				this._Released = value;
				this.SetStatus();
			}
		}
		#endregion
		#region Hold
		public abstract class hold : PX.Data.IBqlField
		{
		}
		protected Boolean? _Hold;
		[PXDBBool()]
		[PXDefault(typeof(INSetup.holdEntry))]
		[PXUIField(DisplayName="Hold")]
		public virtual Boolean? Hold
		{
			get
			{
				return this._Hold;
			}
			set
			{
				this._Hold = value;
				this.SetStatus();
			}
		}
		#endregion
		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected String _Status;
		[PXDBString(1, IsFixed = true)]
		[PXDefault()]
		[PXUIField(DisplayName="Status", Visibility=PXUIVisibility.SelectorVisible, Enabled=false)]
		[INDocStatus.List()]
		public virtual String Status
		{
			[PXDependsOnFields(typeof(released), typeof(hold))]
			get
			{
				return this._Status;
			}
			set
			{
				//this._Status = value;
			}
		}
		#endregion
		#region TranDate
		public abstract class tranDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _TranDate;
		[PXDBDate()]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? TranDate
		{
			get
			{
				return this._TranDate;
			}
			set
			{
				this._TranDate = value;
			}
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		protected String _FinPeriodID;
		[INOpenPeriod(typeof(INRegister.tranDate))]
		[PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String FinPeriodID
		{
			get
			{
				return this._FinPeriodID;
			}
			set
			{
				this._FinPeriodID = value;
			}
		}
		#endregion
		#region TranPeriodID
		public abstract class tranPeriodID : PX.Data.IBqlField
		{
		}
		protected String _TranPeriodID;
		[TranPeriodID(typeof(INRegister.tranDate))]
		public virtual String TranPeriodID
		{
			get
			{
				return this._TranPeriodID;
			}
			set
			{
				this._TranPeriodID = value;
			}
		}
		#endregion
		#region LineCntr
		public abstract class lineCntr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineCntr;
		[PXDBInt()]
		[PXDefault(0)]
		public virtual Int32? LineCntr
		{
			get
			{
				return this._LineCntr;
			}
			set
			{
				this._LineCntr = value;
			}
		}
		#endregion
		#region TotalQty
		public abstract class totalQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _TotalQty;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total Qty.", Visibility=PXUIVisibility.SelectorVisible, Enabled=false)]
		public virtual Decimal? TotalQty
		{
			get
			{
				return this._TotalQty;
			}
			set
			{
				this._TotalQty = value;
			}
		}
		#endregion
		#region TotalAmount
		public abstract class totalAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _TotalAmount;
		[PXDBBaseCury()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total Amount", Visibility=PXUIVisibility.SelectorVisible, Enabled=false)]
		public virtual Decimal? TotalAmount
		{
			get
			{
				return this._TotalAmount;
			}
			set
			{
				this._TotalAmount = value;
			}
		}
		#endregion
		#region TotalCost
		public abstract class totalCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _TotalCost;
		[PXDBBaseCury()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total Cost", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual Decimal? TotalCost
		{
			get
			{
				return this._TotalCost;
			}
			set
			{
				this._TotalCost = value;
			}
		}
		#endregion
		#region ControlQty
		public abstract class controlQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _ControlQty;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Control Qty.")]
		public virtual Decimal? ControlQty
		{
			get
			{
				return this._ControlQty;
			}
			set
			{
				this._ControlQty = value;
			}
		}
		#endregion
		#region ControlAmount
		public abstract class controlAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _ControlAmount;
		[PXDBBaseCury()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Control Amount")]
		public virtual Decimal? ControlAmount
		{
			get
			{
				return this._ControlAmount;
			}
			set
			{
				this._ControlAmount = value;
			}
		}
		#endregion
		#region ControlCost
		public abstract class controlCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _ControlCost;
		[PXDBBaseCury()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Control Cost")]
		public virtual Decimal? ControlCost
		{
			get
			{
				return this._ControlCost;
			}
			set
			{
				this._ControlCost = value;
			}
		}
		#endregion
		#region BatchNbr
		public abstract class batchNbr : PX.Data.IBqlField
		{
		}
		protected String _BatchNbr;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[PXSelector(typeof(Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleIN>>>))]
		public virtual String BatchNbr
		{
			get
			{
				return this._BatchNbr;
			}
			set
			{
				this._BatchNbr = value;
			}
		}
		#endregion
		#region ExtRefNbr
		public abstract class extRefNbr : PX.Data.IBqlField
		{
		}
		protected String _ExtRefNbr;
		[PXDBString(40, IsUnicode = true)]
		[PXUIField(DisplayName = "External Ref.", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String ExtRefNbr
		{
			get
			{
				return this._ExtRefNbr;
			}
			set
			{
				this._ExtRefNbr = value;
			}
		}
		#endregion			
		#region KitInventoryID
		public abstract class kitInventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _KitInventoryID;
		[PXDBInt]
		[PXUIField(DisplayName = "Inventory ID", Visibility = PXUIVisibility.Visible)]
		[PXDimensionSelector(InventoryAttribute.DimensionName, typeof(Search<InventoryItem.inventoryID>), typeof(InventoryItem.inventoryCD), DescriptionField = typeof(InventoryItem.descr))]
		public virtual Int32? KitInventoryID
		{
			get
			{
				return this._KitInventoryID;
			}
			set
			{
				this._KitInventoryID = value;
			}
		}
		#endregion
		#region KitRevisionID
		public abstract class kitRevisionID : PX.Data.IBqlField
		{
		}
		protected String _KitRevisionID;
		[PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
		[PXUIField(DisplayName = "Revision")]
		public virtual String KitRevisionID
		{
			get
			{
				return this._KitRevisionID;
			}
			set
			{
				this._KitRevisionID = value;
			}
		}
		#endregion
		#region KitLineNbr
		public abstract class kitLineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _KitLineNbr;
		[PXDBInt()]
		[PXDefault(0)]
		public virtual Int32? KitLineNbr
		{
			get
			{
				return this._KitLineNbr;
			}
			set
			{
				this._KitLineNbr = value;
			}
		}
		#endregion
		#region SOOrderType
		public abstract class sOOrderType : PX.Data.IBqlField
		{
		}
		protected String _SOOrderType;
		[PXDBString(2, IsFixed = true)]
		[PXUIField(DisplayName = "SO Order Type", Visible = false)]
		[PXSelector(typeof(Search<SOOrderType.orderType>))]
		public virtual String SOOrderType
		{
			get
			{
				return this._SOOrderType;
			}
			set
			{
				this._SOOrderType = value;
			}
		}
		#endregion
		#region SOOrderNbr
		public abstract class sOOrderNbr : PX.Data.IBqlField
		{
		}
		protected String _SOOrderNbr;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "SO Order Nbr.", Visible = false, Enabled = false)]
		[PXSelector(typeof(Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<INRegister.sOOrderType>>>>))]
		public virtual String SOOrderNbr
		{
			get
			{
				return this._SOOrderNbr;
			}
			set
			{
				this._SOOrderNbr = value;
			}
		}
		#endregion
		#region SOShipmentType
		public abstract class sOShipmentType : PX.Data.IBqlField
		{
		}
		protected String _SOShipmentType;
		[PXDBString(1, IsFixed = true)]
		public virtual String SOShipmentType
		{
			get
			{
				return this._SOShipmentType;
			}
			set
			{
				this._SOShipmentType = value;
			}
		}
		#endregion
		#region SOShipmentNbr
		public abstract class sOShipmentNbr : PX.Data.IBqlField
		{
		}
		protected String _SOShipmentNbr;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "SO Shipment Nbr.", Visible = false, Enabled = false)]
		[PXSelector(typeof(Search<SOShipment.shipmentNbr>))]
		public virtual String SOShipmentNbr
		{
			get
			{
				return this._SOShipmentNbr;
			}
			set
			{
				this._SOShipmentNbr = value;
			}
		}
		#endregion
		#region POReceiptType
		public abstract class pOReceiptType : PX.Data.IBqlField
		{
		}
		protected String _POReceiptType;
		[PXDBString(2, IsFixed = true)]
		[PXUIField(DisplayName = "PO Receipt Type", Visible = false, Enabled = false)]
		public virtual String POReceiptType
		{
			get
			{
				return this._POReceiptType;
			}
			set
			{
				this._POReceiptType = value;
			}
		}
		#endregion
		#region POReceiptNbr
		public abstract class pOReceiptNbr : PX.Data.IBqlField
		{
		}
		protected String _POReceiptNbr;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "PO Receipt Nbr.", Visible = false, Enabled = false)]
		[PXSelector(typeof(Search<POReceipt.receiptNbr>))]
		public virtual String POReceiptNbr
		{
			get
			{
				return this._POReceiptNbr;
			}
			set
			{
				this._POReceiptNbr = value;
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
		[PXSearchable(SM.SearchCategory.IN, "{0}: {1}", new Type[] { typeof(INRegister.docType), typeof(INRegister.refNbr) },
			new Type[] { typeof(INRegister.tranDesc), typeof(INRegister.extRefNbr), typeof(INRegister.transferNbr) },
			NumberFields = new Type[] { typeof(INRegister.refNbr) },
			Line1Format = "{0}{1:d}{2}{3}{4}", Line1Fields = new Type[] { typeof(INRegister.extRefNbr), typeof(INRegister.tranDate), typeof(INRegister.transferType), typeof(INRegister.transferNbr), typeof(INRegister.status) },
			Line2Format = "{0}", Line2Fields = new Type[] { typeof(INRegister.tranDesc) },
			WhereConstraint = typeof(Where<INRegister.docType, NotEqual<INDocType.production>, And<INRegister.docType, NotEqual<INDocType.disassembly>>>)
		)]
		[PXNote(DescriptionField = typeof(INRegister.refNbr), 
			Selector = typeof(INRegister.refNbr))]
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
		#region IsPPVTran
		public abstract class isPPVTran : IBqlField
		{
		}
		protected bool? _IsPPVTran = false;
		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? IsPPVTran
		{
			get
			{
				return _IsPPVTran;
			}
			set
			{
				_IsPPVTran = value;
			}
		}
		#endregion
		#region Methods
		protected virtual void SetStatus()
		{
			if (this._Hold != null && (bool)this._Hold)
			{
				this._Status = INDocStatus.Hold;
			}
			else if (this._Released != null && this._Released == false)
			{
				this._Status = INDocStatus.Balanced;
			}
			else 
			{
				this._Status = INDocStatus.Released;
			}
		}
		#endregion
	}

    [PXProjection(typeof(Select4<INTransitLineStatus, Where<INTransitLineStatus.qtyOnHand, Greater<Zero>>, Aggregate<GroupBy<INTransitLineStatus.transferNbr>>>))]
    [Serializable]
    [PXHidden]
	public partial class INTransferInTransit : IBqlTable
	{
        #region TransferNbr
        public abstract class transferNbr : PX.Data.IBqlField
        {
        }
        protected String _TransferNbr;
        [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof(INTransitLineStatus.transferNbr))]
        public virtual String TransferNbr
        {
            get
            {
                return this._TransferNbr;
            }
            set
            {
                this._TransferNbr = value;
            }
        }
        #endregion

        #region RefNoteID
        public abstract class refNoteID : PX.Data.IBqlField
        {
        }
        protected Guid? _RefNoteID;
        [PXNote(BqlField = typeof(INTransitLineStatus.refNoteID))]
        public virtual Guid? RefNoteID
        {
            get
            {
                return this._RefNoteID;
            }
            set
            {
                this._RefNoteID = value;
            }
        }
        #endregion
    }

    [PXProjection(typeof(Select<INTransitLineStatus, Where<INTransitLineStatus.qtyOnHand, Greater<Zero>>>))]
    [Serializable]
    [PXHidden]
    public partial class INTranInTransit : IBqlTable
    {
        #region RefNbr
        public abstract class refNbr : PX.Data.IBqlField
        {
        }
        protected String _RefNbr;
        [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof(INTransitLineStatus.transferNbr))]
        public virtual String RefNbr
        {
            get
            {
                return this._RefNbr;
            }
            set
            {
                this._RefNbr = value;
            }
        }
        #endregion
        #region LineNbr
        public abstract class lineNbr : PX.Data.IBqlField
        {
        }
        protected Int32? _LineNbr;
        [PXDBInt(IsKey = true, BqlField = typeof(INTransitLineStatus.transferLineNbr))]
        public virtual Int32? LineNbr
        {
            get
            {
                return this._LineNbr;
            }
            set
            {
                this._LineNbr = value;
            }
        }
        #endregion
        #region OrigModule
        public abstract class origModule : PX.Data.IBqlField
        {
        }
        protected String _OrigModule;
        [PXDBString(2, IsFixed = true, BqlField = typeof(INTransitLineStatus.origModule))]
        public virtual String OrigModule
        {
            get
            {
                return this._OrigModule;
            }
            set
            {
                this._OrigModule = value;
            }
        }
        #endregion
    }


    public class INDocType
	{
		public class NumberingAttribute : AutoNumberAttribute
		{
			public NumberingAttribute()
				: base(typeof(INRegister.docType), typeof(INRegister.tranDate),
					new string[] { Issue, Receipt, Transfer, Adjustment, Production, Change, Disassembly },
					new Type[] { typeof(INSetup.issueNumberingID), typeof(INSetup.receiptNumberingID), typeof(INSetup.receiptNumberingID), typeof(INSetup.adjustmentNumberingID), typeof(INSetup.kitAssemblyNumberingID), typeof(INSetup.kitAssemblyNumberingID), typeof(INSetup.kitAssemblyNumberingID) }) { ; }
		}

	    public class ListAttribute : PXStringListAttribute
	    {
		    public ListAttribute() : base(
			    new[]
				{
					Pair(Issue, Messages.Issue),
					Pair(Receipt, Messages.Receipt),
					Pair(Transfer, Messages.Transfer),
					Pair(Adjustment, Messages.Adjustment),
					Pair(Production, Messages.Production),
					Pair(Disassembly, Messages.Disassembly),
				}) {}
	    }

	    public class KitListAttribute : PXStringListAttribute
	    {
		    public KitListAttribute() : base(
			    new[]
				{
					Pair(Production, Messages.Production),
					Pair(Disassembly, Messages.Disassembly),
				}) {}
	    }

	    public class SOListAttribute : PXStringListAttribute
	    {
		    public SOListAttribute() : base(
			    new[]
				{
					Pair(Issue, Messages.Issue),
					Pair(Receipt, Messages.Receipt),
					Pair(Transfer, Messages.Transfer),
					Pair(Adjustment, Messages.Adjustment),
					Pair(Production, Messages.Production),
					Pair(Disassembly, Messages.Disassembly),
					Pair(DropShip, Messages.DropShip),
				}) {}
	    }

	    public const string Undefined = "0";
		public const string Issue = "I";
		public const string Receipt = "R";
		public const string Transfer = "T";
		public const string Adjustment = "A";
		public const string Production = "P";
		public const string Change = "C";
		public const string Disassembly = "D";
		public const string DropShip = "H";

		public class undefined : Constant<string>
		{
			public undefined() : base(Undefined) { ;}
		}
		
		public class issue : Constant<string>
		{
			public issue() : base(Issue) { ;}
		}

		public class receipt : Constant<string>
		{
			public receipt() : base(Receipt) { ;}
		}

		public class transfer : Constant<string>
		{
			public transfer() : base(Transfer) { ;}
		}

		public class adjustment : Constant<string>
		{
			public adjustment() : base(Adjustment) { ;}
		}

		public class production : Constant<string>
		{
			public production() : base(Production) { ;}
		}
		public class change : Constant<string>
		{
			public change() : base(Change) { ;}
		}
		public class disassembly : Constant<string>
		{
			public disassembly() : base(Disassembly) { ;}
		}
		public class dropShip : Constant<string>
		{
			public dropShip() : base(DropShip) { ;}
		}
	}

	public class INDocStatus
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new[]
				{
					Pair(Hold, Messages.Hold),
					Pair(Balanced, Messages.Balanced),
					Pair(Released, Messages.Released),
				}) {}
		}

		public const string Hold = "H";
		public const string Balanced = "B";
		public const string Released = "R";

		public class hold : Constant<string>
		{
			public hold() : base(Hold) { ;}
		}

		public class balanced : Constant<string>
		{
			public balanced() : base(Balanced) { ;}
		}

		public class released : Constant<string>
		{
			public released() : base(Released) { ;}
		}

	}

	public class INTransferType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new[]
				{
					Pair(OneStep, Messages.OneStep),
					Pair(TwoStep, Messages.TwoStep),
				}) {}
		}

		public const string OneStep = "1";
		public const string TwoStep = "2";


		public class oneStep : Constant<string>
		{
			public oneStep() : base(OneStep) { ;}
		}

		public class twoStep : Constant<string>
		{
			public twoStep() : base(TwoStep) { ;}
		}
	}
}