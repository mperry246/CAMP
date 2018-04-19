using System;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.PO;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.AP;

namespace PX.Objects.FA
{
	/// <summary>
	/// Contains the additional properties of <see cref="FixedAsset"/>.
	/// </summary>
	[Serializable]
	[PXProjection(typeof(Select2<FADetails, 
		LeftJoin<FABookHistoryRecon, On<FABookHistoryRecon.assetID, Equal<FADetails.assetID>, And<FABookHistoryRecon.updateGL, Equal<True>>>>>), new Type[]{typeof(FADetails)})]
	[PXCacheName(Messages.FADetails)]
	public partial class FADetails : PX.Data.IBqlTable
	{
		#region AssetID
		public abstract class assetID : PX.Data.IBqlField
		{
		}
		protected Int32? _AssetID;
		/// <summary>
		/// A reference to <see cref="FixedAsset"/>.
		/// </summary>
		/// <value>
		/// An integer identifier of the fixed asset. 
		/// It is a required value. 
		/// By default, the value is set to the current fixed asset identifier.
		/// </value>
		[PXDBInt(IsKey = true)]
		[PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible)]
		[PXParent(typeof(Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FADetails.assetID>>>>))]
		[PXDBLiteDefault(typeof(FixedAsset.assetID))]
		public virtual Int32? AssetID
		{
			get
			{
				return this._AssetID;
			}
			set
			{
				this._AssetID = value;
			}
		}
		#endregion
		#region PropertyType
		public abstract class propertyType : PX.Data.IBqlField
		{
			#region List
			/// <summary>
			/// The type of the fixed asset property.
			/// </summary>
			/// <value>
			/// The class exposes the following values:
			/// <list type="bullet">
			/// <item> <term><c>CP</c></term> <description>Property</description> </item>
			/// <item> <term><c>GP</c></term> <description>Grant Property</description> </item>
			/// <item> <term><c>CL</c></term> <description>Leased</description> </item>
			/// <item> <term><c>LO</c></term> <description>Leased to Others</description> </item>
			/// <item> <term><c>CR</c></term> <description>Rented</description> </item>
			/// <item> <term><c>RO</c></term> <description>Rented to Others</description> </item>
			/// <item> <term><c>CC</c></term> <description>To the Credit of</description> </item>
			/// </list>
			/// </value>
			public class ListAttribute : PXStringListAttribute
			{
				public ListAttribute()
					: base(
					new string[] { Property, GrantProperty, Leased, LeasedtoOthers, Rented, RentedtoOthers, Credit },
					new string[] { Messages.Property, Messages.GrantProperty, Messages.Leased, Messages.LeasedtoOthers, Messages.Rented, Messages.RentedtoOthers, Messages.Credit }) { ; }
			}

			public const string Property = "CP";
			public const string GrantProperty = "GP";
			public const string Leased = "CL";
			public const string LeasedtoOthers = "LO";
			public const string Rented = "CR";
			public const string RentedtoOthers = "RO";
			public const string Credit = "CC";

			public class property : Constant<string>
			{
				public property() : base(Property) { ;}
			}
			public class grantProperty : Constant<string>
			{
				public grantProperty() : base(GrantProperty) { ;}
			}
			public class leased : Constant<string>
			{
				public leased() : base(Leased) { ;}
			}
			public class leasedtoOthers : Constant<string>
			{
				public leasedtoOthers() : base(LeasedtoOthers) { ;}
			}
			public class rented : Constant<string>
			{
				public rented() : base(Rented) { ;}
			}
			public class rentedtoOthers : Constant<string>
			{
				public rentedtoOthers() : base(RentedtoOthers) { ;}
			}
			public class credit : Constant<string>
			{
				public credit() : base(Credit) { ;}
			}
			#endregion
		}
		protected String _PropertyType;
		/// <summary>
		/// The type of the fixed asset property.
		/// </summary>
		/// <value>
		/// The field can have one of the values described in <see cref="propertyType.ListAttribute"/>.
		/// </value>
		[PXDBString(2, IsFixed = true)]
		[propertyType.List]
		[PXDefault(propertyType.Property)]
		[PXUIField(DisplayName = "Property Type")]
		public virtual String PropertyType
		{
			get
			{
				return this._PropertyType;
			}
			set
			{
				this._PropertyType = value;
			}
		}
		#endregion
		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected String _Status;
		/// <summary>
		/// The status of the fixed asset.
		/// </summary>
		/// <value>
		/// The field can have one of the values described in <see cref="FixedAssetStatus.ListAttribute"/>.
		/// </value>
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[FixedAssetStatus.List()]
		[PXDefault(FixedAssetStatus.Active)]
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
		
		#region Condition
		public abstract class condition : PX.Data.IBqlField
		{
			#region List
			/// <summary>
			/// The condition of the fixed asset.
			/// </summary>
			/// <value>
			/// The class exposes the following values:
			/// <list type="bullet">
			/// <item> <term><c>G</c></term> <description>Good</description> </item>
			/// <item> <term><c>A</c></term> <description>Average</description> </item>
			/// <item> <term><c>P</c></term> <description>Poor</description> </item>
			/// </list>
			/// </value>
			public class ListAttribute : PXStringListAttribute
			{
				public ListAttribute()
					: base(
					new string[] { Good, Avg, Poor },
					new string[] { Messages.Good, Messages.Avg, Messages.Poor }) { ; }
			}

			public const string Good = "G";
			public const string Avg = "A";
			public const string Poor = "P";

			public class good : Constant<string>
			{
				public good() : base(Good) { ;}
			}
			public class avg : Constant<string>
			{
				public avg() : base(Avg) { ;}
			}
			public class poor : Constant<string>
			{
				public poor() : base(Poor) { ;}
			}
			#endregion
		}
		protected String _Condition;
		/// <summary>
		/// The condition of the fixed asset.
		/// </summary>
		/// <value>
		/// The field can have one of the values described in <see cref="condition.ListAttribute"/>.
		/// </value>
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Condition", Visibility = PXUIVisibility.SelectorVisible)]
		[condition.List()]
		[PXDefault(condition.Good)]
		public virtual String Condition
		{
			get
			{
				return this._Condition;
			}
			set
			{
				this._Condition = value;
			}
		}
		#endregion
		#region ReceiptDate
		public abstract class receiptDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _ReceiptDate;
		/// <summary>
		/// The acquisition date of the fixed asset.
		/// </summary>
		[PXDBDate()]
		[PXDefault(typeof(POReceipt.receiptDate))]
		[PXUIField(DisplayName = "Receipt Date")]
		public virtual DateTime? ReceiptDate
		{
			get
			{
				return this._ReceiptDate;
			}
			set
			{
				this._ReceiptDate = value;
			}
		}
		#endregion
		#region ReceiptType
		public abstract class receiptType : PX.Data.IBqlField
		{
		}
		protected String _ReceiptType;
		/// <summary>
		/// The type of the purchase receipt.
		/// This field is a part of the compound reference to the purchasing document (<see cref="POReceipt"/>).
		/// The full reference contains the <see cref="receiptType"/> and <see cref="receiptNbr"/> fields.
		/// </summary>
		/// <value>
		/// The field can have one of the values described in <see cref="POReceiptType.ListAttribute"/>.
		/// </value>
		[PXDBString(2, IsFixed = true, InputMask = "")]
		[POReceiptType.List()]
		[PXUIField(DisplayName = "Receipt Type")]
		public virtual String ReceiptType
		{
			get
			{
				return this._ReceiptType;
			}
			set
			{
				this._ReceiptType = value;
			}
		}
		#endregion
		#region ReceiptNbr
		public abstract class receiptNbr : PX.Data.IBqlField
		{
		}
		protected String _ReceiptNbr;
		/// <summary>
		/// The number of the purchase receipt.
		/// This field is a part of the compound reference to the purchasing document (<see cref="POReceipt")/>.
		/// The full reference contains the <see cref="receiptType"/> and <see cref="receiptNbr"/> fields. 
		/// </summary>
		[PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[POReceiptType.RefNbr(typeof(Search2<POReceipt.receiptNbr,
			LeftJoin<POReceiptLine, On<POReceiptLine.receiptType, Equal<POReceipt.receiptType>,
								   And<POReceiptLine.receiptNbr, Equal<POReceipt.receiptNbr>,
								   And<POReceiptLine.inventoryID, Equal<Optional<FADetails.inventoryID>>,
								   And<POReceiptLine.subItemID, Equal<Optional<FADetails.subItemID>>>>>>>,
			Where<POReceipt.receiptType, Equal<Optional<POReceipt.receiptType>>>>), Filterable = true)]
		[PXUIField(DisplayName = "Receipt Nbr.")]

		public virtual String ReceiptNbr
		{
			get
			{
				return this._ReceiptNbr;
			}
			set
			{
				this._ReceiptNbr = value;
			}
		}
		#endregion
		#region PONumber
		public abstract class pONumber : PX.Data.IBqlField
		{
		}
		protected String _PONumber;
		/// <summary>
		/// The number of the purchase order related to the purchase document.
		/// </summary>
		/// <value>
		/// The information field.
		/// </value>
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "PO Number")]
		[PXDefault(typeof(Search2<POOrder.orderNbr,
							InnerJoin<POReceiptLine, On<POReceiptLine.pOType, Equal<POOrder.orderType>,
													And<POReceiptLine.pONbr, Equal<POOrder.orderNbr>>>,
							InnerJoin<POReceipt, On<POReceipt.receiptType, Equal<POReceiptLine.receiptType>,
												And<POReceipt.receiptNbr, Equal<POReceiptLine.receiptNbr>>>>>,
							Where<POReceipt.receiptType, Equal<Current<FADetails.receiptType>>,
							  And<POReceipt.receiptNbr, Equal<Current<FADetails.receiptNbr>>,
							  And<POReceiptLine.inventoryID, Equal<Current<FADetails.inventoryID>>,
							  And<POReceiptLine.subItemID, Equal<Current<FADetails.subItemID>>>>>>>),
			PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String PONumber
		{
			get
			{
				return this._PONumber;
			}
			set
			{
				this._PONumber = value;
			}
		}
		#endregion
		#region BillNumber
		public abstract class billNumber : PX.Data.IBqlField
		{
		}
		protected String _BillNumber;
		/// <summary>
		/// The number of the bill.
		/// </summary>
		/// <value>
		/// The information field, which value is entered manually.
		/// </value>
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Bill Number")]
		public virtual String BillNumber
		{
			get
			{
				return this._BillNumber;
			}
			set
			{
				this._BillNumber = value;
			}
		}
		#endregion
		#region Manufacturer
		public abstract class manufacturer : PX.Data.IBqlField
		{
		}
		protected String _Manufacturer;
		/// <summary>
		/// The name of the fixed asset manufacturer.
		/// </summary>
		/// <value>
		/// The information field, which value is entered manually.
		/// </value>
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Manufacturer")]
		public virtual String Manufacturer
		{
			get
			{
				return this._Manufacturer;
			}
			set
			{
				this._Manufacturer = value;
			}
		}
		#endregion
		#region Model
		public abstract class model : PX.Data.IBqlField
		{
		}
		protected String _Model;
		/// <summary>
		/// The name of the fixed asset model.
		/// </summary>
		/// <value>
		/// The information field, which value is entered manually.
		/// </value>
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Model")]
		public virtual String Model
		{
			get
			{
				return this._Model;
			}
			set
			{
				this._Model = value;
			}
		}
		#endregion
		#region SerialNumber
		public abstract class serialNumber : PX.Data.IBqlField
		{
		}
		protected String _SerialNumber;
		/// <summary>
		/// The serial number of the fixed asset.
		/// </summary>
		/// <value>
		/// The information field, which value is entered manually.
		/// </value>
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Serial Number")]
		public virtual String SerialNumber
		{
			get
			{
				return this._SerialNumber;
			}
			set
			{
				this._SerialNumber = value;
			}
		}
		#endregion
		#region LocationRevID
		public abstract class locationRevID:IBqlField
		{
		}
		protected Int32? _LocationRevID;
		/// <summary>
		/// The number of the actual revision of the asset location.
		/// This field is a part of the compound reference to <see cref="FALocationHistory"/>.
		/// The full reference contains the <see cref="assetID"/> and <see cref="locationRevID"/> fields.
		/// </summary>
		[PXDBInt]
		public virtual Int32? LocationRevID
		{
			get
			{
				return _LocationRevID;
			}
			set
			{
				_LocationRevID = value;
			}
		}
		#endregion
		#region CurrentCost
		public abstract class currentCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _CurrentCost;
		/// <summary>
		/// The cost of the fixed asset in the current depreciation period.
		/// </summary>
		/// <value>
		/// The value is read-only and is selected from the appropriate <see cref="FABookHistoryRecon.ytdAcquired"/> field.
		/// </value>
		[PXDBBaseCury(BqlField = typeof(FABookHistoryRecon.ytdAcquired))]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Basis", Enabled = false)]
		public virtual Decimal? CurrentCost
		{
			get
			{
				return this._CurrentCost;
			}
			set
			{
				this._CurrentCost = value;
			}
		}
		#endregion
		#region AccrualBalance
		public abstract class accrualBalance : PX.Data.IBqlField
		{
		}
		protected Decimal? _AccrualBalance;
		/// <summary>
		/// The reconciled part of the current cost of the fixed asset.
		/// </summary>
		/// <value>
		/// The value is read-only and is selected from the appropriate <see cref="FABookHistoryRecon.ytdReconciled"/> field.
		/// </value>
		[PXDBBaseCury(BqlField = typeof(FABookHistoryRecon.ytdReconciled))]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? AccrualBalance
		{
			get
			{
				return this._AccrualBalance;
			}
			set
			{
				this._AccrualBalance = value;
			}
		}
		#endregion
		#region IsReconciled
		public abstract class isReconciled : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsReconciled;
		/// <summary>
		/// A flag that indicates (if set to <c>true</c>) that a fixed asset is fully reconciled with the General Ledger module.
		/// </summary>
		[PXBool()]
		[PXDBCalced(typeof(Switch<Case<Where<IsNull<FABookHistoryRecon.ytdAcquired, decimal0>, Equal<IsNull<FABookHistoryRecon.ytdReconciled, decimal0>>>, True>, False>), typeof(bool))]
		public virtual Boolean? IsReconciled
		{
			get
			{
				return this._IsReconciled;
			}
			set
			{
				this._IsReconciled = value;
			}
		}
		#endregion
		#region TransferPeriod
		[Obsolete("This class is not used anymore and will be removed in Acumatica 2018R2")]
		public abstract class transferPeriod : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The identifier of the transfer period.
		/// This is an unbound service field that is used to pass the parameter to transfer processing.
		/// </summary>
		[PXString(6, IsFixed = true)]
		[PXUIField(DisplayName = "Transfer Period", Enabled = false)]
		[FinPeriodIDFormatting]
		[Obsolete("This property is not used anymore and will be removed in Acumatica 2018R2")]
		public virtual string TransferPeriod
		{
			get;
			set;
		}
		#endregion
		#region Barcode
		public abstract class barcode : PX.Data.IBqlField
		{
		}
		protected String _Barcode;
		/// <summary>
		/// The barcode of the fixed asset.
		/// </summary>
		/// <value>
		/// The information field, which value is entered manually.
		/// </value>
		[PXDBString(20, IsUnicode = true)]
		[PXUIField(DisplayName = "Barcode")]
		public virtual String Barcode
		{
			get
			{
				return this._Barcode;
			}
			set
			{
				this._Barcode = value;
			}
		}
		#endregion
		#region TagNbr
		public abstract class tagNbr : IBqlField
		{
			#region Numbering
			public class NumberingAttribute : AutoNumberAttribute
			{
				public NumberingAttribute()
					: base(typeof(FASetup.tagNumberingID), typeof(createdDateTime))
				{
					NullMode = NullNumberingMode.UserNumbering;
				}
			}
			#endregion
		}
		protected String _TagNbr;
		/// <summary>
		/// The tag of the fixed asset.
		/// </summary>
		/// <value>
		/// The value can be entered manually or can be auto-numbered.
		/// </value>
		[PXDBString(20, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
		[PXUIField(DisplayName = "Tag Number", Enabled = false)]
		[tagNbr.Numbering]
		public virtual String TagNbr
		{
			get
			{
				return _TagNbr;
			}
			set
			{
				_TagNbr = value;
			}
		}
		#endregion

		#region Unused Fields
		#region LastCountDate
		public abstract class lastCountDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastCountDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Last Count Date")]
		public virtual DateTime? LastCountDate
		{
			get
			{
				return this._LastCountDate;
			}
			set
			{
				this._LastCountDate = value;
			}
		}
		#endregion
		#endregion

		#region DepreciateFromDate
		public abstract class depreciateFromDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _DepreciateFromDate;
		/// <summary>
		/// The date when depreciation of the fixed asset starts.
		/// </summary>
		/// <value>
		/// The date can not be greater than <see cref="receiptDate"/>.
		/// </value>
		[PXDBDate]
		[PXFormula(typeof(FADetails.receiptDate))]
		[PXDefault]
		[PXUIField(DisplayName = Messages.PlacedInServiceDate)]
		public virtual DateTime? DepreciateFromDate
		{
			get
			{
				return this._DepreciateFromDate;
			}
			set
			{
				this._DepreciateFromDate = value;
			}
		}
		#endregion

		#region AcquisitionCost
		public abstract class acquisitionCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _AcquisitionCost;
		/// <summary>
		/// The cost of the fixed asset at the time of acquisition.
		/// </summary>
		/// <value>
		/// The value can be changed during the life of the asset.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Orig. Acquisition Cost")]
		public virtual Decimal? AcquisitionCost
		{
			get
			{
				return this._AcquisitionCost;
			}
			set
			{
				this._AcquisitionCost = value;
			}
		}
		#endregion
		#region SalvageAmount
		public abstract class salvageAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _SalvageAmount;
		/// <summary>
		/// The salvage amount of the fixed asset.
		/// </summary>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Salvage Amount")]
		public virtual Decimal? SalvageAmount
		{
			get
			{
				return this._SalvageAmount;
			}
			set
			{
				this._SalvageAmount = value;
			}
		}
		#endregion
		#region ReplacementCost
		public abstract class replacementCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _ReplacementCost;
		/// <summary>
		/// The replacement cost of the fixed asset.
		/// </summary>
		[PXDBBaseCury]
		[PXUIField(DisplayName = "Replacement Cost")]
		public virtual Decimal? ReplacementCost
		{
			get
			{
				return this._ReplacementCost;
			}
			set
			{
				this._ReplacementCost = value;
			}
		}
		#endregion
		#region DisposalDate
		public abstract class disposalDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _DisposalDate;
		/// <summary>
		/// The date of fixed asset disposal.
		/// </summary>
		/// <value>
		/// The field is filled in only for disposed fixed assets.
		/// </value>
		[PXDBDate()]
		[PXUIField(DisplayName = "Disposal Date", Enabled = false)]
		public virtual DateTime? DisposalDate
		{
			get
			{
				return this._DisposalDate;
			}
			set
			{
				this._DisposalDate = value;
			}
		}
		#endregion
		#region DisposalPeriodID
		public abstract class disposalPeriodID : IBqlField
		{
		}
		protected string _DisposalPeriodID;
		/// <summary>
		/// The period of fixed asset disposal.
		/// </summary>
		/// <value>
		/// The field is filled in only for disposed fixed assets.
		/// </value>
		[PXDBString(6, IsFixed = true)]
		public virtual string DisposalPeriodID
		{
			get
			{
				return _DisposalPeriodID;
			}
			set
			{
				_DisposalPeriodID = value;
			}
		}
		#endregion
		#region DisposalMethodID
		public abstract class disposalMethodID : IBqlField
		{
		}
		protected Int32? _DisposalMethodID;
		/// <summary>
		/// A reference to <see cref="FADisposalMethod"/>.
		/// </summary>
		/// <value>
		/// An integer identifier of the disposal method.
		/// The field is filled in only for disposed fixed assets.
		/// </value>
		[PXDBInt]
		[PXSelector(typeof(FADisposalMethod.disposalMethodID), 
			SubstituteKey = typeof(FADisposalMethod.disposalMethodCD), 
			DescriptionField = typeof(FADisposalMethod.description))]
		[PXUIField(DisplayName = "Disposal Method", Enabled = false)]
		public virtual Int32? DisposalMethodID
		{
			get
			{
				return _DisposalMethodID;
			}
			set
			{
				_DisposalMethodID = value;
			}
		}
		#endregion
		#region SaleAmount
		public abstract class saleAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _SaleAmount;
		/// <summary>
		/// The amount of fixed asset disposal.
		/// </summary>
		/// <value>
		/// The field is filled in only for disposed fixed assets. The value of the field can be zero.
		/// </value>
		[PXDBBaseCury]
		[PXUIField(DisplayName = "Disposal Amount", Enabled = false)]

		public virtual Decimal? SaleAmount
		{
			get
			{
				return this._SaleAmount;
			}
			set
			{
				this._SaleAmount = value;
			}
		}
		#endregion
		#region Warrantor
		public abstract class warrantor : PX.Data.IBqlField
		{
		}
		protected String _Warrantor;
		/// <summary>
		/// The name of the fixed asset warrantor.
		/// </summary>
		/// <value>
		/// The information field, which value is entered manually.
		/// </value>
		[PXDBString(40, IsUnicode = true)]
		[PXUIField(DisplayName = "Warrantor")]
		public virtual String Warrantor
		{
			get
			{
				return this._Warrantor;
			}
			set
			{
				this._Warrantor = value;
			}
		}
		#endregion
		#region WarrantyExpirationDate
		public abstract class warrantyExpirationDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _WarrantyExpirationDate;
		/// <summary>
		/// The expiration date of the fixed asset warranty.
		/// </summary>
		/// <value>
		/// The information field, which value is entered manually.
		/// </value>
		[PXDBDate()]
		[PXUIField(DisplayName = "Warranty Expires On")]
		public virtual DateTime? WarrantyExpirationDate
		{
			get
			{
				return this._WarrantyExpirationDate;
			}
			set
			{
				this._WarrantyExpirationDate = value;
			}
		}
		#endregion
		#region WarrantyCertificateNumber
		public abstract class warrantyCertificateNumber : PX.Data.IBqlField
		{
		}
		protected String _WarrantyCertificateNumber;
		/// <summary>
		/// The certificate number of the fixed asset warranty.
		/// </summary>
		/// <value>
		/// The information field, which value is entered manually.
		/// </value>
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "Warranty Certificate Number")]
		public virtual String WarrantyCertificateNumber
		{
			get
			{
				return this._WarrantyCertificateNumber;
			}
			set
			{
				this._WarrantyCertificateNumber = value;
			}
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		/// <summary>
		/// A reference to <see cref="InventoryItem"/>.
		/// </summary>
		/// <value>
		/// An integer identifier of the inventory item appropriated with the fixed asset. 
		/// </value>
		[StockItem()]
		[PXForeignReference(typeof(Field<inventoryID>.IsRelatedTo<InventoryItem.inventoryID>))]
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
		#region SubItemID
		public abstract class subItemID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubItemID;
		/// <summary>
		/// A reference to <see cref="INSubItem"/>.
		/// </summary>
		/// <value>
		/// An integer identifier of the inventory subitem. 
		/// </value>
		[IN.SubItem(typeof(FADetails.inventoryID),
			typeof(LeftJoin<INSiteStatus,
				On<INSiteStatus.subItemID, Equal<INSubItem.subItemID>,
				And<INSiteStatus.inventoryID, Equal<Optional<FADetails.inventoryID>>,
				And<INSiteStatus.siteID, Equal<Optional<FADetails.siteID>>>>>>))]
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
		#region SiteID
		public abstract class siteID : PX.Data.IBqlField
		{
		}
		protected Int32? _SiteID;
		/// <summary>
		/// A reference to <see cref="INSite"/>.
		/// </summary>
		/// <value>
		/// An integer identifier of the inventory site. 
		/// </value>
		[IN.SiteAvail(typeof(FADetails.inventoryID), typeof(FADetails.subItemID))]
		[PXDefault(typeof(POReceiptLine.siteID), PersistingCheck = PXPersistingCheck.Nothing)]
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
		#region InventoryLocationID
		public abstract class inventoryLocationID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryLocationID;
		/// <summary>
		/// A reference to <see cref="INLocation"/>.
		/// </summary>
		/// <value>
		/// An integer identifier of the inventory location. 
		/// </value>
		[IN.LocationAvail(typeof(FADetails.inventoryID), typeof(FADetails.subItemID), typeof(FADetails.siteID), null, null, null)]
		[PXUIField(DisplayName = "Inventory Location")]
		public virtual Int32? InventoryLocationID
		{
			get
			{
				return this._InventoryLocationID;
			}
			set
			{
				this._InventoryLocationID = value;
			}
		}
		#endregion

		#region Unused Fields
		#region NextServiceDate
		public abstract class nextServiceDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _NextServiceDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Next Service Date")]
		public virtual DateTime? NextServiceDate
		{
			get
			{
				return this._NextServiceDate;
			}
			set
			{
				this._NextServiceDate = value;
			}
		}
		#endregion
		#region NextServiceValue
		public abstract class nextServiceValue : PX.Data.IBqlField
		{
		}
		protected Decimal? _NextServiceValue;
		[PXDBDecimal(4)]
		[PXUIField(DisplayName = "Next Service Value")]
		public virtual Decimal? NextServiceValue
		{
			get
			{
				return this._NextServiceValue;
			}
			set
			{
				this._NextServiceValue = value;
			}
		}
		#endregion
		#region NextMeasurementUsageDate
		public abstract class nextMeasurementUsageDate : IBqlField
		{
		}
		protected DateTime? _NextMeasurementUsageDate;
		[PXDBDate]
		[PXUIField(DisplayName = "Next Measurement Date")]
		[PXFormula(typeof(CalcNextMeasurementDate<lastMeasurementUsageDate, depreciateFromDate, assetID>))]
		public virtual DateTime? NextMeasurementUsageDate
		{
			get
			{
				return _NextMeasurementUsageDate;
			}
			set
			{
				_NextMeasurementUsageDate = value;
			}
		}
		#endregion
		#region LastServiceDate
		public abstract class lastServiceDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastServiceDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Last Service Date")]
		public virtual DateTime? LastServiceDate
		{
			get
			{
				return this._LastServiceDate;
			}
			set
			{
				this._LastServiceDate = value;
			}
		}
		#endregion
		#region LastServiceValue
		public abstract class lastServiceValue : PX.Data.IBqlField
		{
		}
		protected Decimal? _LastServiceValue;
		[PXDBDecimal(4)]
		[PXUIField(DisplayName = "Last Service Value")]
		public virtual Decimal? LastServiceValue
		{
			get
			{
				return this._LastServiceValue;
			}
			set
			{
				this._LastServiceValue = value;
			}
		}
		#endregion
		#region LastMeasurementUsageDate
		public abstract class lastMeasurementUsageDate : IBqlField
		{
		}
		protected DateTime? _LastMeasurementUsageDate;
		[PXDBDate]
		[PXUIField(DisplayName = "Last Measurement Date", Enabled = false)]
		public virtual DateTime? LastMeasurementUsageDate
		{
			get
			{
				return _LastMeasurementUsageDate;
			}
			set
			{
				_LastMeasurementUsageDate = value;
			}
		}
		#endregion
		#region TotalExpectedUsage
		public abstract class totalExpectedUsage : IBqlField
		{
		}
		protected Decimal? _TotalExpectedUsage;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total Expected Usage")]
		public virtual Decimal? TotalExpectedUsage
		{
			get
			{
				return _TotalExpectedUsage;
			}
			set
			{
				_TotalExpectedUsage = value;
			}
		}
		#endregion
		
		#region FairMarketValue
		public abstract class fairMarketValue : PX.Data.IBqlField
		{
		}
		protected Decimal? _FairMarketValue;
		[PXDBDecimal(4)]
		[PXUIField(DisplayName = "Fair Market Value")]
		public virtual Decimal? FairMarketValue
		{
			get
			{
				return this._FairMarketValue;
			}
			set
			{
				this._FairMarketValue = value;
			}
		}
		#endregion
		#region LessorID
		public abstract class lessorID : PX.Data.IBqlField
		{
		}
		protected Int32? _LessorID;
		[VendorNonEmployeeActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(Vendor.acctName), CacheGlobal = true, Filterable = true)]
		[PXUIField(DisplayName = "Lessor")]
		public virtual Int32? LessorID
		{
			get
			{
				return this._LessorID;
			}
			set
			{
				this._LessorID = value;
			}
		}
		#endregion
		#region LeaseRentTerm
		public abstract class leaseRentTerm : PX.Data.IBqlField
		{
		}
		protected Int32? _LeaseRentTerm;
		[PXDBInt()]
		[PXUIField(DisplayName = "Lease/Rent Term, months")]
		public virtual Int32? LeaseRentTerm
		{
			get
			{
				return this._LeaseRentTerm;
			}
			set
			{
				this._LeaseRentTerm = value;
			}
		}
		#endregion
		#region LeaseNumber
		public abstract class leaseNumber : PX.Data.IBqlField
		{
		}
		protected String _LeaseNumber;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Lease Number")]
		public virtual String LeaseNumber
		{
			get
			{
				return this._LeaseNumber;
			}
			set
			{
				this._LeaseNumber = value;
			}
		}
		#endregion
		#region RentAmount
		public abstract class rentAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _RentAmount;
		[PXDBBaseCury]
		[PXUIField(DisplayName = "Rent Amount")]
		public virtual Decimal? RentAmount
		{
			get
			{
				return this._RentAmount;
			}
			set
			{
				this._RentAmount = value;
			}
		}
		#endregion
		#region RetailCost
		public abstract class retailCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _RetailCost;
		[PXDBBaseCury]
		[PXUIField(DisplayName = "Retail Cost")]
		public virtual Decimal? RetailCost
		{
			get
			{
				return this._RetailCost;
			}
			set
			{
				this._RetailCost = value;
			}
		}
		#endregion
		#region ManufacturingYear
		public abstract class manufacturingYear : PX.Data.IBqlField
		{
		}
		protected String _ManufacturingYear;
		[PXDBString(4, IsFixed = true)]
		[PXUIField(DisplayName = "Manufacturing Year")]
		public virtual String ManufacturingYear
		{
			get
			{
				return this._ManufacturingYear;
			}
			set
			{
				this._ManufacturingYear = value;
			}
		}
		#endregion
		#region ReportingLineNbr
		public abstract class reportingLineNbr : PX.Data.IBqlField
		{
			#region List
			public class ListAttribute : PXStringListAttribute
			{
				public ListAttribute()
					: base(
					new string[] { NotAplicable, Line10, Line11, Line12, Line13, Line14, Line15, Line16, Line16a, Line17, Line18, Line19, Line20, Line21, Line22, Line23, Others },
					new string[] { Messages.NotAplicable, Messages.Line10, Messages.Line11, Messages.Line12, Messages.Line13, Messages.Line14, Messages.Line15, Messages.Line16, Messages.Line16a, Messages.Line17, Messages.Line18, Messages.Line19, Messages.Line20, Messages.Line21, Messages.Line22, Messages.Line23, Messages.Others }) { ; }
			}

			public const string NotAplicable = "NAP";
			public const string Line10 = "L10";
			public const string Line11 = "L11";
			public const string Line12 = "L12";
			public const string Line13 = "L13";
			public const string Line14 = "L14";
			public const string Line15 = "L15";
			public const string Line16 = "L16";
			public const string Line16a = "A16";
			public const string Line17 = "L17";
			public const string Line18 = "L18";
			public const string Line19 = "L19";
			public const string Line20 = "L20";
			public const string Line21 = "L21";
			public const string Line22 = "L22";
			public const string Line23 = "L23";
			public const string Others = "0TH";

			public class notAplicable : Constant<string>
			{
				public notAplicable() : base(NotAplicable) { ;}
			}
			public class line10 : Constant<string>
			{
				public line10() : base(Line10) { ;}
			}
			public class line11 : Constant<string>
			{
				public line11() : base(Line11) { ;}
			}
			public class line12 : Constant<string>
			{
				public line12() : base(Line12) { ;}
			}
			public class line13 : Constant<string>
			{
				public line13() : base(Line13) { ;}
			}
			public class line14 : Constant<string>
			{
				public line14() : base(Line14) { ;}
			}
			public class line15 : Constant<string>
			{
				public line15() : base(Line15) { ;}
			}
			public class line16 : Constant<string>
			{
				public line16() : base(Line16) { ;}
			}
			public class line16a : Constant<string>
			{
				public line16a() : base(Line16a) { ;}
			}
			public class line17 : Constant<string>
			{
				public line17() : base(Line17) { ;}
			}
			public class line18 : Constant<string>
			{
				public line18() : base(Line18) { ;}
			}
			public class line19 : Constant<string>
			{
				public line19() : base(Line19) { ;}
			}
			public class line20 : Constant<string>
			{
				public line20() : base(Line20) { ;}
			}
			public class line21 : Constant<string>
			{
				public line21() : base(Line21) { ;}
			}
			public class line22 : Constant<string>
			{
				public line22() : base(Line22) { ;}
			}
			public class line23 : Constant<string>
			{
				public line23() : base(Line23) { ;}
			}
			public class others : Constant<string>
			{
				public others() : base(Others) { ;}
			}
			#endregion
		}
		protected String _ReportingLineNbr;
		[PXDBString(3, IsFixed = true, InputMask = "")]
		[reportingLineNbr.List]
		[PXDefault(reportingLineNbr.NotAplicable)]
		[PXUIField(DisplayName = "Personal Property Type")]
		public virtual String ReportingLineNbr
		{
			get
			{
				return this._ReportingLineNbr;
			}
			set
			{
				this._ReportingLineNbr = value;
			}
		}
		#endregion
		#region IsTemplate
		public abstract class isTemplate : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsTemplate;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Is Template")]
		public virtual Boolean? IsTemplate
		{
			get
			{
				return this._IsTemplate;
			}
			set
			{
				this._IsTemplate = value;
			}
		}
		#endregion
		#region TemplateID
		public abstract class templateID : PX.Data.IBqlField
		{
		}
		protected Int32? _TemplateID;
		[PXDBInt()]
		[PXSelector(typeof(Search2<FixedAsset.assetID, 
								InnerJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>>, 
								Where<FADetails.isTemplate, Equal<True>>>), 
					typeof(FixedAsset.assetCD), typeof(FixedAsset.assetTypeID), typeof(FixedAsset.description), typeof(FixedAsset.usefulLife),
					SubstituteKey = typeof(FixedAsset.assetCD),
					DescriptionField = typeof(FixedAsset.description))]
		[PXUIField(DisplayName = "Template", Enabled = false)]
		public virtual Int32? TemplateID
		{
			get
			{
				return this._TemplateID;
			}
			set
			{
				this._TemplateID = value;
			}
		}
		#endregion
		#endregion

		#region Hold
		public abstract class hold : IBqlField
		{
		}
		protected Boolean? _Hold;
		/// <summary>
		/// A flag that indicates (if set to <c>true</c>) that the fixed asset is on hold and thus cannot be depreciated.
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Hold")]
		public virtual Boolean? Hold
		{
			get
			{
				return _Hold;
			}
			set
			{
				_Hold = value;
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
	}
}
