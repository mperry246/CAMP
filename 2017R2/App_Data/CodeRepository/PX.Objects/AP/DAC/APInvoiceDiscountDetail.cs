﻿namespace PX.Objects.AP
{
    using System;
    using System.Collections;
    using PX.Data;
    using PX.Data.ReferentialIntegrity.Attributes;
    using PX.Objects.IN;
    using PX.Objects.CM;
    using PX.Objects.GL;
    using PX.Objects.AR;

	/// <summary>
	/// Represents a document or group level discount that has been applied 
	/// to an Accounts Payable bill or adjustment. The records of this type 
	/// can be edited on the Bills and Adjustments (AP301000) form, which corresponds
	/// to the <see cref="APInvoiceEntry"/> graph.
	/// </summary>
	/// <remarks>
	/// Line-level discounts are specified in the document line itself,
	/// see <see cref="APTran.DiscPct"/>.
	/// </remarks>
	[System.SerializableAttribute()]
    [PXCacheName(Messages.APInvoiceDiscountDetail)]
    public partial class APInvoiceDiscountDetail : PX.Data.IBqlTable, IDiscountDetail
    {
        #region LineNbr
        public abstract class lineNbr : PX.Data.IBqlField
        {
        }
        protected Int32? _LineNbr;
        [PXDBIdentity(IsKey = true)]
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
        #region SkipDiscount
        public abstract class skipDiscount : PX.Data.IBqlField
        {
        }
        protected Boolean? _SkipDiscount;
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
        public virtual Boolean? SkipDiscount
        {
            get
            {
                return this._SkipDiscount;
            }
            set
            {
                this._SkipDiscount = value;
            }
        }
        #endregion
        #region DocType
        public abstract class docType : PX.Data.IBqlField
        {
        }
        protected String _DocType;
        [PXDBString(3, IsKey = true, IsFixed = true)]
        [PXDBDefault(typeof(APRegister.docType))]
        [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, TabOrder = 0)]
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
        }
        protected String _RefNbr;
        [PXDBString(15, IsUnicode = true, IsKey = true)]
        [PXDBDefault(typeof(APRegister.refNbr))]
        [PXParent(typeof(Select<APRegister, Where<APRegister.docType, Equal<Current<APInvoiceDiscountDetail.docType>>, And<APRegister.refNbr, Equal<Current<APInvoiceDiscountDetail.refNbr>>>>>))]
        [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
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
        #region DiscountID
        public abstract class discountID : PX.Data.IBqlField
        {
        }
        protected String _DiscountID;
        [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault()]
        [PXUIField(DisplayName = "Discount Code")]
        [PXSelector(typeof(Search<APDiscount.discountID, Where<APDiscount.type, NotEqual<DiscountType.LineDiscount>>>))]
        public virtual String DiscountID
        {
            get
            {
                return this._DiscountID;
            }
            set
            {
                this._DiscountID = value;
            }
        }
        #endregion
        #region DiscountSequenceID
        public abstract class discountSequenceID : PX.Data.IBqlField
        {
        }
        protected String _DiscountSequenceID;
        [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault()]
        [PXUIField(DisplayName = "Sequence ID")]
        [PXSelector(typeof(Search<VendorDiscountSequence.discountSequenceID, Where<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.discountID, Equal<Current<APInvoiceDiscountDetail.discountID>>>>>))]
        public virtual String DiscountSequenceID
        {
            get
            {
                return this._DiscountSequenceID;
            }
            set
            {
                this._DiscountSequenceID = value;
            }
        }
        #endregion
        #region Type
        public abstract class type : PX.Data.IBqlField
        {
        }
        protected String _Type;
        [PXDBString(1, IsFixed = true)]
        [PXDefault()]
        [DiscountType.List()]
        [PXUIField(DisplayName = "Type", Enabled = false)]
        public virtual String Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
        #endregion
        #region CuryInfoID
        public abstract class curyInfoID : PX.Data.IBqlField
        {
        }
        protected Int64? _CuryInfoID;
        [PXDBLong()]
        [CurrencyInfo(typeof(APInvoice.curyInfoID))]
        public virtual Int64? CuryInfoID
        {
            get
            {
                return this._CuryInfoID;
            }
            set
            {
                this._CuryInfoID = value;
            }
        }
        #endregion
        #region DiscountableAmt
        public abstract class discountableAmt : PX.Data.IBqlField
        {
        }
        protected Decimal? _DiscountableAmt;
        [PXDBDecimal(4)]
        public virtual Decimal? DiscountableAmt
        {
            get
            {
                return this._DiscountableAmt;
            }
            set
            {
                this._DiscountableAmt = value;
            }
        }
        #endregion
        #region CuryDiscountableAmt
        public abstract class curyDiscountableAmt : PX.Data.IBqlField
        {
        }
        protected Decimal? _CuryDiscountableAmt;
        [PXDBCurrency(typeof(APInvoiceDiscountDetail.curyInfoID), typeof(APInvoiceDiscountDetail.discountableAmt))]
        [PXUIField(DisplayName = "Discountable Amt.", Enabled = false)]
        public virtual Decimal? CuryDiscountableAmt
        {
            get
            {
                return this._CuryDiscountableAmt;
            }
            set
            {
                this._CuryDiscountableAmt = value;
            }
        }
        #endregion
        #region DiscountableQty
        public abstract class discountableQty : PX.Data.IBqlField
        {
        }
        protected Decimal? _DiscountableQty;
        [PXDBQuantity(MinValue = 0)]
        [PXUIField(DisplayName = "Discountable Qty.", Enabled = false)]
        public virtual Decimal? DiscountableQty
        {
            get
            {
                return this._DiscountableQty;
            }
            set
            {
                this._DiscountableQty = value;
            }
        }
        #endregion
        #region DiscountAmt
        public abstract class discountAmt : PX.Data.IBqlField
        {
        }
        protected Decimal? _DiscountAmt;
        [PXDBDecimal(4)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? DiscountAmt
        {
            get
            {
                return this._DiscountAmt;
            }
            set
            {
                this._DiscountAmt = value;
            }
        }
        #endregion
        #region CuryDiscountAmt
        public abstract class curyDiscountAmt : PX.Data.IBqlField
        {
        }
        protected Decimal? _CuryDiscountAmt;
        [PXDBCurrency(typeof(APInvoiceDiscountDetail.curyInfoID), typeof(APInvoiceDiscountDetail.discountAmt))]
        [PXUIField(DisplayName = "Discount Amt.", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryDiscountAmt
        {
            get
            {
                return this._CuryDiscountAmt;
            }
            set
            {
                this._CuryDiscountAmt = value;
            }
        }
        #endregion
        #region DiscountPct
        public abstract class discountPct : PX.Data.IBqlField
        {
        }
        protected Decimal? _DiscountPct;
        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Discount Percent", Enabled = false)]
        public virtual Decimal? DiscountPct
		{
            get
            {
                return this._DiscountPct;
            }
            set
            {
                this._DiscountPct = value;
            }
        }
        #endregion
        #region FreeItemID
        public abstract class freeItemID : PX.Data.IBqlField
        {
        }
        protected Int32? _FreeItemID;
        [Inventory(DisplayName = "Free Item", Enabled = false)]
        [PXForeignReference(typeof(Field<freeItemID>.IsRelatedTo<InventoryItem.inventoryID>))]
        public virtual Int32? FreeItemID
        {
            get
            {
                return this._FreeItemID;
            }
            set
            {
                this._FreeItemID = value;
            }
        }
        #endregion
        #region FreeItemQty
        public abstract class freeItemQty : PX.Data.IBqlField
        {
        }
        protected Decimal? _FreeItemQty;
        [PXDBQuantity(MinValue = 0)]
        [PXUIField(DisplayName = "Free Item Qty.", Enabled = false)]
        public virtual Decimal? FreeItemQty
        {
            get
            {
                return this._FreeItemQty;
            }
            set
            {
                this._FreeItemQty = value;
            }
        }
        #endregion
        #region IsManual
        public abstract class isManual : PX.Data.IBqlField
        {
        }
        protected Boolean? _IsManual;
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Manual Discount", Enabled = false)]
        public virtual Boolean? IsManual
        {
            get
            {
                return this._IsManual;
            }
            set
            {
                this._IsManual = value;
            }
        }
		#endregion
		#region OrderType
		public abstract class orderType : PX.Data.IBqlField
		{
		}
		protected String _OrderType;
		[PXDBString(2, IsFixed = true)]
		[PXUIField(DisplayName = "Order Type", Enabled = false)]
		public virtual String OrderType
		{
			get
			{
				return this._OrderType;
			}
			set
			{
				this._OrderType = value;
			}
		}
		#endregion
		#region OrderNbr
		public abstract class orderNbr : PX.Data.IBqlField
		{
		}
		protected String _OrderNbr;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "PO Order Nbr.", Enabled = false)]
		[PXSelector(typeof(Search<PO.POOrder.orderNbr>))]
		public virtual String OrderNbr
		{
			get
			{
				return this._OrderNbr;
			}
			set
			{
				this._OrderNbr = value;
			}
		}
		#endregion
		#region ReceiptType
		public abstract class receiptType : PX.Data.IBqlField
        {
        }
        protected String _ReceiptType;
        [PXDBString(2, IsFixed = true)]
        [PXUIField(DisplayName = "Receipt Type", Enabled = false)]
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
        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "PO Receipt Nbr.", Enabled = false)]
        [PXSelector(typeof(Search<PO.POReceipt.receiptNbr>))]
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