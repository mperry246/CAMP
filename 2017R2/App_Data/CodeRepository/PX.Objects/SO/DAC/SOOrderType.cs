using PX.Common;

namespace PX.Objects.SO
{
	using System;
	using PX.Data;
	using PX.Objects.AR;
	using PX.Objects.IN;
	using PX.Objects.CS;
	using PX.Objects.GL;
	using PX.SM;
	/*
	 1. Non-inventory orders cannot require location and cannot create shipments
	 2. Non-ar orders cannot have inventory doc type IN,DM,CM
	 */

	[System.SerializableAttribute]
	[PXPrimaryGraph(typeof(SOOrderTypeMaint))]
	[PXCacheName(Messages.OrderType)]
	public partial class SOOrderType : IBqlTable, PXNoteAttribute.IPXCopySettings
	{
		#region OrderType
		public abstract class orderType : IBqlField { }
		protected String _OrderType;
		[PXDBString(2, IsKey = true, IsFixed = true, InputMask=">aa")]
		[PXUIField(DisplayName = "Order Type", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault]
		[PXSelector(typeof(Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On<SOOrderTypeOperation.orderType, Equal<SOOrderType.orderType>, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>>>>,
			Where<SOOrderType.requireShipping, Equal<boolFalse>, Or<FeatureInstalled<FeaturesSet.inventory>>>>))]
		[PXRestrictor(typeof(Where<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>, Or<FeatureInstalled<FeaturesSet.warehouse>>>), null)]
		[PXRestrictor(typeof(Where<SOOrderType.requireAllocation, NotEqual<True>, Or<AllocationAllowed>>), null)]
		public virtual String OrderType
		{
			get { return this._OrderType; }
			set { this._OrderType = value; }
		}
		#endregion
		#region Active
		public abstract class active : IBqlField { }
		protected Boolean? _Active;
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Active")]
		public virtual Boolean? Active
		{
			get { return this._Active; }
			set { this._Active = value; }
		}
		#endregion
		#region DaysToKeep
		public abstract class daysToKeep : IBqlField { }
		protected Int16? _DaysToKeep;
		[PXDBShort]
		[PXDefault((short)30)]
		[PXUIField(DisplayName = "Days To Keep")]
		public virtual Int16? DaysToKeep
		{
			get { return this._DaysToKeep; }
			set { this._DaysToKeep = value; }
		}
		#endregion
		#region Descr
		public abstract class descr : IBqlField { }
		protected String _Descr;
		[PXDBLocalizableString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Descr
		{
			get { return this._Descr; }
			set { this._Descr = value; }
		}
		#endregion		
		#region Template
		public abstract class template : IBqlField { }
		protected String _Template;
		[PXDBString(2, IsFixed = true, InputMask = ">aa")]
		[PXUIField(DisplayName = "Order Template", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault]
		[PXSelector(typeof(Search<SOOrderTypeT.orderType, Where2<Where<SOOrderTypeT.requireAllocation, NotEqual<True>
				, Or2<FeatureInstalled<FeaturesSet.warehouseLocation>
					, Or2<FeatureInstalled<FeaturesSet.lotSerialTracking>
						, Or2<FeatureInstalled<FeaturesSet.subItem>
							, Or2<FeatureInstalled<FeaturesSet.replenishment>
								, Or<FeatureInstalled<FeaturesSet.sOToPOLink>>
							>
						>
					>
				>>, And<Where<SOOrderTypeT.requireShipping, Equal<boolFalse>, Or<FeatureInstalled<FeaturesSet.inventory>>>>
			>
		>
			), DirtyRead = true, DescriptionField = typeof(SOOrderTypeT.descr))]
		public virtual String Template
		{
			get { return this._Template; }
			set { this._Template = value; }
		}
		#endregion
		#region IsSystem
		public abstract class isSystem : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsSystem;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Is System Template", Enabled = false)]
		public virtual Boolean? IsSystem
		{
			get
			{
				return this._IsSystem;
			}
			set
			{
				this._IsSystem = value;
			}
		}
		#endregion
		#region Behavior
		public abstract class behavior : IBqlField { }
		protected String _Behavior;
		[PXDBString(2, IsFixed = true, InputMask = ">aa")]
		[PXUIField(DisplayName = "Automation Behavior", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault]
		[SOBehavior.List]
		public virtual String Behavior
		{
			get { return this._Behavior; }
			set { this._Behavior = value; }
		}
		#endregion
		#region DefaultOperation
		public abstract class defaultOperation : IBqlField
		{
			public const int Length = 1;
		}
		protected String _DefaultOperation;
		[PXDBString(defaultOperation.Length, IsFixed = true, InputMask = ">a")]
		[PXUIField(DisplayName = "Default Operation")]
		[PXDefault(typeof(Search<SOOrderType.defaultOperation,
			Where<SOOrderType.orderType, Equal<Current<SOOrderType.behavior>>>>))]
		[SOOperation.List]
		public virtual String DefaultOperation
		{
			get { return this._DefaultOperation; }
			set { this._DefaultOperation = value; }
		}
		#endregion		
		#region INDocType
		public abstract class iNDocType : IBqlField { }
		protected String _INDocType;
		[PXDBString(3, IsFixed = true)]
		[PXDefault]
		[INTranType.SOList]
		[PXUIField(DisplayName = "Inventory Transaction Type")]
		public virtual String INDocType
		{
			get { return this._INDocType; }
			set { this._INDocType = value; }
		}
		#endregion
		#region ARDocType
		public abstract class aRDocType : IBqlField { }
		protected String _ARDocType;
		[PXDBString(3, IsFixed = true)]
		[PXDefault]
		[ARDocType.SOList]
		[PXUIField(DisplayName = "AR Document Type")]
		public virtual String ARDocType
		{
			get { return this._ARDocType; }
			set { this._ARDocType = value; }
		}
		#endregion
		#region OrderPlanType
		public abstract class orderPlanType : IBqlField { }
		protected String _OrderPlanType;
		[PXDBString(2, IsFixed = true, InputMask = ">aa")]
		[PXUIField(DisplayName = "Order Plan Type")]
		[PXSelector(typeof(Search<INPlanType.planType>), DescriptionField = typeof(INPlanType.descr))]
		public virtual String OrderPlanType
		{
			get { return this._OrderPlanType; }
			set { this._OrderPlanType = value; }
		}
		#endregion
		#region ShipmentPlanType
		public abstract class shipmentPlanType : IBqlField { }
		protected String _ShipmentPlanType;
		[PXDBString(2, IsFixed = true, InputMask = ">aa")]
		[PXUIField(DisplayName = "Shipment Plan Type")]
		[PXSelector(typeof(Search<INPlanType.planType>), DescriptionField=typeof(INPlanType.descr))]
		public virtual String ShipmentPlanType
		{
			get { return this._ShipmentPlanType; }
			set { this._ShipmentPlanType = value; }
		}
		#endregion
		#region OrderNumberingID
		public abstract class orderNumberingID : IBqlField { }
		protected String _OrderNumberingID;
		[PXDBString(10, IsUnicode = true)]
		[PXDefault]
		[PXSelector(typeof(Search<Numbering.numberingID>))]
		[PXUIField(DisplayName = "Order Numbering Sequence")]
		public virtual String OrderNumberingID
		{
			get { return this._OrderNumberingID; }
			set { this._OrderNumberingID = value; }
		}
		#endregion
		#region InvoiceNumberingID
		public abstract class invoiceNumberingID : IBqlField { }
		protected String _InvoiceNumberingID;
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("ARINVOICE")]
		[PXSelector(typeof(Search<Numbering.numberingID>))]
		[PXUIField(DisplayName = "Invoice Numbering Sequence")]
		public virtual String InvoiceNumberingID
		{
			get { return this._InvoiceNumberingID; }
			set { this._InvoiceNumberingID = value; }
		}
		#endregion
		#region UserInvoiceNumbering
		public abstract class userInvoiceNumbering : IBqlField { }
		protected Boolean? _UserInvoiceNumbering;
		[PXBool]
		[PXFormula(typeof(Selector<SOOrderType.invoiceNumberingID, Numbering.userNumbering>))]
		[PXUIField(DisplayName = "Manual Invoice Numbering")]
		public virtual Boolean? UserInvoiceNumbering
		{
			get { return this._UserInvoiceNumbering; }
			set { this._UserInvoiceNumbering = value; }
		}
		#endregion
		#region MarkInvoicePrinted
		public abstract class markInvoicePrinted : IBqlField { }
		protected Boolean? _MarkInvoicePrinted;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Mark as Printed")]
		public virtual Boolean? MarkInvoicePrinted
		{
			get { return this._MarkInvoicePrinted; }
			set { this._MarkInvoicePrinted = value; }
		}
		#endregion
		#region MarkInvoiceEmailed
		public abstract class markInvoiceEmailed : IBqlField { }
		protected Boolean? _MarkInvoiceEmailed;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Mark as Emailed")]
		public virtual Boolean? MarkInvoiceEmailed
		{
			get { return this._MarkInvoiceEmailed; }
			set { this._MarkInvoiceEmailed = value; }
		}
		#endregion
		#region HoldEntry
		public abstract class holdEntry : IBqlField { }
		protected Boolean? _HoldEntry;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName="Hold Orders on Entry")]
		public virtual Boolean? HoldEntry
		{
			get { return this._HoldEntry; }
			set { this._HoldEntry = value; }
		}
		#endregion
		#region InvoiceHoldEntry
		public abstract class invoiceHoldEntry : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Hold Invoices on Entry")]
		public virtual Boolean? InvoiceHoldEntry { get; set; }
		#endregion
		#region CreditHoldEntry
		public abstract class creditHoldEntry : IBqlField { }
		protected Boolean? _CreditHoldEntry;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Hold Document on Failed Credit Check")]
		public virtual Boolean? CreditHoldEntry
		{
			get { return this._CreditHoldEntry; }
			set { this._CreditHoldEntry = value; }
		}
		#endregion

		#region RequireAllocation
		public abstract class requireAllocation : IBqlField { }
		protected Boolean? _RequireAllocation;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Require Stock Allocation")]
		public virtual Boolean? RequireAllocation
		{
			get { return this._RequireAllocation; }
			set { this._RequireAllocation = value; }
		}
		#endregion		
		#region RequireLocation
		public abstract class requireLocation : IBqlField { }
		protected Boolean? _RequireLocation;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Require Location", Enabled = false)]
		public virtual Boolean? RequireLocation
		{
			get { return this._RequireLocation; }
			set { this._RequireLocation = value; }
		}
		#endregion
		#region RequireLotSerial
		public abstract class requireLotSerial : IBqlField { }
		protected Boolean? _RequireLotSerial;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Require Lot/Serial Entry")]
		public virtual Boolean? RequireLotSerial
		{
			get { return this._RequireLotSerial; }
			set { this._RequireLotSerial = value; }
		}
		#endregion

		#region RequireControlTotal
		public abstract class requireControlTotal : IBqlField { }
		protected Boolean? _RequireControlTotal;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Require Control Total")]
		public virtual Boolean? RequireControlTotal
		{
			get { return this._RequireControlTotal; }
			set { this._RequireControlTotal = value; }
		}
		#endregion
		#region RequireShipping
		public abstract class requireShipping : IBqlField { }
		protected Boolean? _RequireShipping;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Process Shipments")]
		public virtual Boolean? RequireShipping
		{
			get { return this._RequireShipping; }
			set { this._RequireShipping = value; }
		}
		#endregion
		#region SupportsApproval
		public abstract class supportsApproval : IBqlField { }
		protected bool? _SupportsApproval;
		[PXBool]        
		[PXUIField(DisplayName = "Supports Approval", Enabled = false)]
		public virtual bool? SupportsApproval
		{
			get { return _SupportsApproval; }
			set { _SupportsApproval = value; }
		}
		#endregion
		#region CopyLotSerialFromShipment
		public abstract class copyLotSerialFromShipment : IBqlField { }
		protected Boolean? _CopyLotSerialFromShipment;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Copy Lot/Serial numbers from Shipment back to Sales Order")]
		public virtual Boolean? CopyLotSerialFromShipment
		{
			get { return this._CopyLotSerialFromShipment; }
			set { this._CopyLotSerialFromShipment = value; }
		}
		#endregion

		#region BillSeparately
		public abstract class billSeparately : IBqlField { }
		protected Boolean? _BillSeparately;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Bill Separately")]
		public virtual Boolean? BillSeparately
		{
			get { return this._BillSeparately; }
			set { this._BillSeparately = value; }
		}
		#endregion
		#region ShipSeparately
		public abstract class shipSeparately : IBqlField { }
		protected Boolean? _ShipSeparately;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Ship Separately")]
		public virtual Boolean? ShipSeparately
		{
			get { return this._ShipSeparately; }
			set { this._ShipSeparately = value; }
		}
		#endregion
		#region SalesAcctDefault
		public abstract class salesAcctDefault : IBqlField { }
		protected String _SalesAcctDefault;
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Use Sales Account from")]
		[SOSalesAcctSubDefault.AcctList]
		[PXDefault(SOSalesAcctSubDefault.MaskItem)]
		public virtual String SalesAcctDefault
		{
			get { return this._SalesAcctDefault; }
			set { this._SalesAcctDefault = value; }
		}
		#endregion
		#region MiscAcctDefault
		public abstract class miscAcctDefault : IBqlField { }
		protected String _MiscAcctDefault;
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Use Misc. Account from")]
		[SOMiscAcctSubDefault.AcctList]
		[PXDefault(SOMiscAcctSubDefault.MaskItem)]
		public virtual String MiscAcctDefault
		{
			get { return this._MiscAcctDefault; }
			set { this._MiscAcctDefault = value; }
		}
		#endregion
		#region FreightAcctDefault
		public abstract class freightAcctDefault : IBqlField { }
		protected String _FreightAcctDefault;
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Use Freight Account from")]
		[SOFreightAcctSubDefault.AcctList]
		[PXDefault(SOFreightAcctSubDefault.MaskShipVia)]
		public virtual String FreightAcctDefault
		{
			get { return this._FreightAcctDefault; }
			set { this._FreightAcctDefault = value; }
		}
		#endregion
		#region DiscAcctDefault
		public abstract class discAcctDefault : IBqlField { }
		protected String _DiscAcctDefault;
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Use Discount Account from")]
		[SODiscAcctSubDefault.AcctList]
		[PXDefault(SODiscAcctSubDefault.MaskLocation)]
		public virtual String DiscAcctDefault
		{
			get { return this._DiscAcctDefault; }
			set { this._DiscAcctDefault = value; }
		}
		#endregion
		#region COGSAcctDefault
		public abstract class cOGSAcctDefault : IBqlField { }
		protected String _COGSAcctDefault;
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Use COGS Account from", Visible = false, Enabled = false)]
		[SOCOGSAcctSubDefault.AcctList]
		//[PXDefault(SOCOGSAcctSubDefault.MaskItem)]
		public virtual String COGSAcctDefault
		{
			get { return this._COGSAcctDefault; }
			set { this._COGSAcctDefault = value; }
		}
		#endregion
		#region SalesSubMask
		public abstract class salesSubMask : IBqlField { }
		protected String _SalesSubMask;
		[PXDefault]
		[PXUIRequired(typeof(Where<active, Equal<True>>))]
		[SOSalesSubAccountMask(DisplayName = "Combine Sales Sub. From")]
		public virtual String SalesSubMask
		{
			get { return this._SalesSubMask; }
			set { this._SalesSubMask = value; }
		}
		#endregion
		#region MiscSubMask
		public abstract class miscSubMask : IBqlField { }
		protected String _MiscSubMask;
		[PXDefault]
		[PXUIRequired(typeof(Where<active, Equal<True>>))]
		[SOMiscSubAccountMask(DisplayName = "Combine Misc. Sub. from")]
		public virtual String MiscSubMask
		{
			get { return this._MiscSubMask; }
			set { this._MiscSubMask = value; }
		}
		#endregion
		#region FreightSubMask
		public abstract class freightSubMask : IBqlField { }
		protected String _FreightSubMask;
		[PXDefault]
		[PXUIRequired(typeof(Where<active, Equal<True>>))]
		[SOFreightSubAccountMask(DisplayName = "Combine Freight Sub. from")]
		public virtual String FreightSubMask
		{
			get { return this._FreightSubMask; }
			set { this._FreightSubMask = value; }
		}
		#endregion
		#region DiscSubMask
		public abstract class discSubMask : IBqlField { }
		protected String _DiscSubMask;
		[PXDefault]
		[PXUIRequired(typeof(Where<active, Equal<True>, And<FeatureInstalled<FeaturesSet.vendorDiscounts>>>))]
		[SODiscSubAccountMask(DisplayName = "Combine Discount Sub. from")]
		public virtual String DiscSubMask
		{
			get { return this._DiscSubMask; }
			set { this._DiscSubMask = value; }
		}
		#endregion
		#region FreightAcctID
		public abstract class freightAcctID : IBqlField { }
		protected Int32? _FreightAcctID;
		[PXDefault]
		[PXUIRequired(typeof(Where<active, Equal<True>>))]
		[Account(DisplayName = "Freight Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Account.description))]
		public virtual Int32? FreightAcctID
		{
			get { return this._FreightAcctID; }
			set { this._FreightAcctID = value; }
		}
		#endregion
		#region FreightSubID
		public abstract class freightSubID : IBqlField { }
		protected Int32? _FreightSubID;
		[PXDefault]
		[PXUIRequired(typeof(Where<active, Equal<True>>))]
		[SubAccount(typeof(SOOrderType.freightAcctID), DisplayName = "Freight Sub.", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Sub.description))]
		public virtual Int32? FreightSubID
		{
			get { return this._FreightSubID; }
			set { this._FreightSubID = value; }
		}
		#endregion
		#region RecalculateDiscOnPartialShipment
		public abstract class recalculateDiscOnPartialShipment : IBqlField { }
		protected bool? _RecalculateDiscOnPartialShipment;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Recalculate Discount On Partial Shipment")]
		public virtual bool? RecalculateDiscOnPartialShipment
		{
			get { return _RecalculateDiscOnPartialShipment; }
			set { _RecalculateDiscOnPartialShipment = value; }
		}
		#endregion
		#region CalculateFreight
		public abstract class calculateFreight : IBqlField { }
		protected bool? _CalculateFreight;
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Calculate Freight")]
		public virtual bool? CalculateFreight
		{
			get { return _CalculateFreight; }
			set { _CalculateFreight = value; }
		}
		#endregion
		

		#region COGSSubMask
		public abstract class cOGSSubMask : IBqlField { }
		protected String _COGSSubMask;
		//[PXDefault()]
		[SOCOGSSubAccountMask(DisplayName = "Combine COGS Sub. From", Visible = false, Enabled = false)]
		public virtual String COGSSubMask
		{
			get { return this._COGSSubMask; }
			set { this._COGSSubMask = value; }
		}
		#endregion
		#region DiscountAcctID
		public abstract class discountAcctID : IBqlField { }
		protected Int32? _DiscountAcctID;
		[Account(DisplayName = "Discount Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Account.description))]
		[PXDefault]
		[PXUIRequired(typeof(Where<active, Equal<True>, And<FeatureInstalled<FeaturesSet.vendorDiscounts>>>))]
		public virtual Int32? DiscountAcctID
		{
			get { return this._DiscountAcctID; }
			set { this._DiscountAcctID = value; }
		}
		#endregion
		#region DiscountSubID
		public abstract class discountSubID : IBqlField { }
		protected Int32? _DiscountSubID;
		[SubAccount(typeof(SOOrderType.discountAcctID), DisplayName = "Discount Sub.", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Sub.description))]
		[PXDefault]
		[PXUIRequired(typeof(Where<active, Equal<True>, And2<FeatureInstalled<FeaturesSet.vendorDiscounts>, And<FeatureInstalled<FeaturesSet.subAccount>>>>))]
		public virtual Int32? DiscountSubID
		{
			get { return this._DiscountSubID; }
			set { this._DiscountSubID = value; }
		}
		#endregion
		#region OrderPriority
		public abstract class orderPriority : IBqlField { }
		protected Int16? _OrderPriority;
		[PXDBShort]
		public virtual Int16? OrderPriority
		{
			get { return this._OrderPriority; }
			set { this._OrderPriority = value; }
		}
		#endregion
		#region CopyNotes
		public abstract class copyNotes : IBqlField { }
		protected bool? _CopyNotes;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Copy Notes")]
		public virtual bool? CopyNotes
		{
			get { return _CopyNotes; }
			set { _CopyNotes = value; }
		}
		#endregion
		#region CopyFiles
		public abstract class copyFiles : IBqlField { }
		protected bool? _CopyFiles;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Copy Attachments")]
		public virtual bool? CopyFiles
		{
			get { return _CopyFiles; }
			set { _CopyFiles = value; }
		}
		#endregion
		#region CopyLineNotesToShipment
		public abstract class copyLineNotesToShipment : IBqlField { }
		protected bool? _CopyLineNotesToShipment;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Copy Line Notes To Shipment")]
		public virtual bool? CopyLineNotesToShipment
		{
			get { return _CopyLineNotesToShipment; }
			set { _CopyLineNotesToShipment = value; }
		}
		#endregion
		#region CopyLineFilesToShipment
		public abstract class copyLineFilesToShipment : IBqlField { }
		protected bool? _CopyLineFilesToShipment;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Copy Line Attachments To Shipment")]
		public virtual bool? CopyLineFilesToShipment
		{
			get { return _CopyLineFilesToShipment; }
			set { _CopyLineFilesToShipment = value; }
		}
		#endregion
		#region CopyLineNotesToInvoice
		public abstract class copyLineNotesToInvoice : IBqlField { }
		protected bool? _CopyLineNotesToInvoice;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Copy Line Notes To Invoice")]
		public virtual bool? CopyLineNotesToInvoice
		{
			get { return _CopyLineNotesToInvoice; }
			set { _CopyLineNotesToInvoice = value; }
		}
		#endregion
		#region CopyLineNotesToInvoiceOnlyNS
		public abstract class copyLineNotesToInvoiceOnlyNS : IBqlField { }
		protected bool? _CopyLineNotesToInvoiceOnlyNS;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Only Non-Stock")]
		public virtual bool? CopyLineNotesToInvoiceOnlyNS
		{
			get { return _CopyLineNotesToInvoiceOnlyNS; }
			set { _CopyLineNotesToInvoiceOnlyNS = value; }
		}
		#endregion
		#region CopyLineFilesToInvoice
		public abstract class copyLineFilesToInvoice : IBqlField { }
		protected bool? _CopyLineFilesToInvoice;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Copy Line Attachments To Invoice")]
		public virtual bool? CopyLineFilesToInvoice
		{
			get { return _CopyLineFilesToInvoice; }
			set { _CopyLineFilesToInvoice = value; }
		}
		#endregion
		#region CopyLineFilesToInvoiceOnlyNS
		public abstract class copyLineFilesToInvoiceOnlyNS : IBqlField { }
		protected bool? _CopyLineFilesToInvoiceOnlyNS;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Only Non-Stock")]
		public virtual bool? CopyLineFilesToInvoiceOnlyNS
		{
			get { return _CopyLineFilesToInvoiceOnlyNS; }
			set { _CopyLineFilesToInvoiceOnlyNS = value; }
		}
		#endregion

		#region PostLineDiscSeparately
		public abstract class postLineDiscSeparately : IBqlField { }
		protected bool? _PostLineDiscSeparately;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Post Line Discounts Separately")]
		public virtual bool? PostLineDiscSeparately
		{
			get { return _PostLineDiscSeparately; }
			set { _PostLineDiscSeparately = value; }
		}
		#endregion
		#region UseDiscountSubFromSalesSub
		public abstract class useDiscountSubFromSalesSub : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Use Discount Sub. from Sales Sub.", FieldClass = SubAccountAttribute.DimensionName)]
		public virtual bool? UseDiscountSubFromSalesSub { get; set; }
		#endregion
		#region CommitmentTracking
		public abstract class commitmentTracking : IBqlField { }
		protected Boolean? _CommitmentTracking;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Commitment Tracking")]
		public virtual Boolean? CommitmentTracking
		{
			get { return this._CommitmentTracking; }
			set { this._CommitmentTracking = value; }
		}
		#endregion
		#region NoteID
		public abstract class noteID : IBqlField { }

		[PXNote(DescriptionField = typeof(SOOrderType.orderType),
			Selector = typeof(SOOrderType.orderType), 
			FieldList = new [] { typeof(SOOrderType.orderType), typeof(SOOrderType.descr) })]
		public virtual Guid? NoteID { get; set; }
		#endregion

		#region CreatedByID
		public abstract class createdByID : IBqlField { }
		protected Guid? _CreatedByID;
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get { return this._CreatedByID; }
			set { this._CreatedByID = value; }
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField { }
		protected String _CreatedByScreenID;
		[PXDBCreatedByScreenID]
		public virtual String CreatedByScreenID
		{
			get { return this._CreatedByScreenID; }
			set { this._CreatedByScreenID = value; }
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField { }
		protected DateTime? _CreatedDateTime;
		[PXDBCreatedDateTime]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? CreatedDateTime
		{
			get { return this._CreatedDateTime; }
			set { this._CreatedDateTime = value; }
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : IBqlField { }
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get { return this._LastModifiedByID; }
			set { this._LastModifiedByID = value; }
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField { }
		protected String _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID]
		public virtual String LastModifiedByScreenID
		{
			get { return this._LastModifiedByScreenID; }
			set { this._LastModifiedByScreenID = value; }
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField { }
		protected DateTime? _LastModifiedDateTime;
		[PXDBLastModifiedDateTime]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? LastModifiedDateTime
		{
			get { return this._LastModifiedDateTime; }
			set { this._LastModifiedDateTime = value; }
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : IBqlField { }
		protected Byte[] _tstamp;
		[PXDBTimestamp]
		public virtual Byte[] tstamp
		{
			get { return this._tstamp; }
			set { this._tstamp = value; }
		}
		#endregion

		public bool CanHaveApplications => ARDocType.IsIn(AR.ARDocType.Invoice, AR.ARDocType.DebitMemo);
	}

	[PXProjection(typeof(Select<SOOrderType, 
		Where<SOOrderType.orderType, Equal<SOOrderType.template>,
			And<SOOrderType.orderType, IsNotNull>>>))]
	[Serializable]
	[PXHidden]
	public partial class SOOrderTypeT : IBqlTable
	{
		#region OrderType
		public abstract class orderType : IBqlField { }
		protected String _OrderType;
		[PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">aa", BqlField = typeof(SOOrderType.orderType))]
		[PXUIField(DisplayName = "Order Type", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault]
		[PXSelector(typeof(Search<SOOrderType.orderType>))]
		public virtual String OrderType
		{
			get { return this._OrderType; }
			set { this._OrderType = value; }
		}
		#endregion
		#region Descr
		public abstract class descr : IBqlField { }
		protected String _Descr;
		[PXDBLocalizableString(60, IsUnicode = true,  BqlField = typeof(SOOrderType.descr), IsProjection = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Descr
		{
			get { return this._Descr; }
			set { this._Descr = value; }
		}
		#endregion
		#region Behavior
		public abstract class behavior : IBqlField { }
		protected String _Behavior;
		[PXDBString(2, IsFixed = true, InputMask = ">aa", BqlField = typeof(SOOrderType.behavior))]
		[PXUIField(DisplayName = "Automation Behavior", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault]
		[SOBehavior.List]
		public virtual String Behavior
		{
			get { return this._Behavior; }
			set { this._Behavior = value; }
		}
		#endregion
		#region RequireAllocation
		public abstract class requireAllocation : IBqlField { }
		protected Boolean? _RequireAllocation;
		[PXDBBool(BqlField = typeof(SOOrderType.requireAllocation))]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Require Stock Allocation")]
		public virtual Boolean? RequireAllocation
		{
			get { return this._RequireAllocation; }
			set { this._RequireAllocation = value; }
		}
		#endregion
		#region RequireShipping
		public abstract class requireShipping : IBqlField { }
		protected Boolean? _RequireShipping;
		[PXDBBool(BqlField = typeof(SOOrderType.requireShipping))]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Process Shipments")]
		public virtual Boolean? RequireShipping
		{
			get { return this._RequireShipping; }
			set { this._RequireShipping = value; }
		}
		#endregion
	}

	public class SOAutomation
	{
		public const string Behavior = "Behavior";
		public const string GraphType = "PX.Objects.SO.SOOrderEntry";

		public class behavior : Constant<string>
		{
			public behavior() : base(Behavior) { }
		}
	}

	public class SOBehavior
	{
		public const string SO = SOOrderTypeConstants.SalesOrder;
		public const string IN = SOOrderTypeConstants.Invoice;
		public const string QT = SOOrderTypeConstants.QuoteOrder;
		public const string RM = SOOrderTypeConstants.RMAOrder;
		public const string CM = SOOrderTypeConstants.CreditMemo;

		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new[]
			{				
					Pair(SO, Messages.SOName),
					Pair(IN, Messages.INName),
					Pair(QT, Messages.QTName),
					Pair(CM, Messages.CMName),
					Pair(RM, Messages.RMName),
			}
				) {}
		}

		public static string DefaultOperation(string behavior, string ardoctype)
		{
			switch (behavior)
			{
				case SO:
				case IN:
					switch (ardoctype)
					{
						case ARDocType.Invoice:
						case ARDocType.DebitMemo:
						case ARDocType.CashSale:
						case ARDocType.NoUpdate:
							return SOOperation.Issue;
						case ARDocType.CreditMemo:
						case ARDocType.CashReturn:
							return SOOperation.Receipt;
						default:
							return null;
					}
				case QT:
					return SOOperation.Issue;
				case CM:
				case RM:
					return SOOperation.Receipt;
			}
			return null;
		}
	}

	public class AllocationAllowed : WhereBase<FeatureInstalled<FeaturesSet.warehouseLocation>,
			Or2<FeatureInstalled<FeaturesSet.lotSerialTracking>,
				Or2<FeatureInstalled<FeaturesSet.subItem>,
					Or2<FeatureInstalled<FeaturesSet.replenishment>,
						Or<FeatureInstalled<FeaturesSet.sOToPOLink>>>>>>
	{
		public override bool UseParenthesis() { return true; }
	}

}
