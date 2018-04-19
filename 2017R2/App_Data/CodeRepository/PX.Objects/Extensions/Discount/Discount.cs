using System;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.IN;

namespace PX.Objects.Extensions.Discount
{
    /// <summary>A mapped cache extension that provides information about the discount.</summary>
    public class Discount : PXMappedCacheExtension, IDiscountDetail
    {
        #region LineNbr
        /// <exclude />
        public abstract class lineNbr : IBqlField
        {
        }
        /// <exclude />
        protected Int32? _LineNbr;
        //[PXDBIdentity(IsKey = true)]
        /// <summary>The number of the detail line to which this discount applies.</summary>
        public virtual Int32? LineNbr
        {
            get
            {
                return _LineNbr;
            }
            set
            {
                _LineNbr = value;
            }
        }
        #endregion
        #region SkipDiscount
        /// <exclude />
        public abstract class skipDiscount : IBqlField
        {
        }
        /// <exclude />
        protected Boolean? _SkipDiscount;
        /// <summary>If set to true, cancels the Group- and Document-level discounts for the document.</summary>
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
        public virtual Boolean? SkipDiscount
        {
            get
            {
                return _SkipDiscount;
            }
            set
            {
                _SkipDiscount = value;
            }
        }
        #endregion
        #region DiscountID
        /// <exclude />
        public abstract class discountID : IBqlField
        {
        }
        /// <exclude />
        protected String _DiscountID;
        /// <summary>The identifier (code) of the discount applied to the document.</summary>
        [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault]
        [PXUIField(DisplayName = "Discount Code")]
        public virtual String DiscountID
        {
            get
            {
                return _DiscountID;
            }
            set
            {
                _DiscountID = value;
            }
        }
        #endregion
        #region DiscountSequenceID
        /// <exclude />
        public abstract class discountSequenceID : IBqlField
        {
        }
        /// <exclude />
        protected String _DiscountSequenceID;
        /// <summary>The ID of the sequence defined for the discount.</summary>
        [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault]
        [PXUIField(DisplayName = "Sequence ID")]
        [PXSelector(typeof(Search<DiscountSequence.discountSequenceID, Where<DiscountSequence.isActive, Equal<True>, And<DiscountSequence.discountID, Equal<Current<discountID>>>>>))]
        public virtual String DiscountSequenceID
        {
            get
            {
                return _DiscountSequenceID;
            }
            set
            {
                _DiscountSequenceID = value;
            }
        }
        #endregion
        #region Type
        /// <exclude />
        public abstract class type : IBqlField
        {
        }
        /// <exclude />
        protected String _Type;
        /// <summary>The type of discount whose sequence was applied to the document (<i>Group</i>, or <i>Document</i>).</summary>
        [PXDBString(1, IsFixed = true)]
        [PXDefault]
        [DiscountType.ListAttribute]
        [PXUIField(DisplayName = "Type", Enabled = false)]
        public virtual String Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        #endregion
        #region CuryInfoID
        /// <exclude />
        public abstract class curyInfoID : IBqlField
        {
        }
        /// <exclude />
        protected Int64? _CuryInfoID;
        /// <summary>The identifier of the <see cref="CurrencyInfo" /> object associated with the discount.</summary>
        [PXDBLong]
        public virtual Int64? CuryInfoID
        {
            get
            {
                return _CuryInfoID;
            }
            set
            {
                _CuryInfoID = value;
            }
        }
        #endregion
        #region DiscountableAmt
        /// <exclude />
        public abstract class discountableAmt : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _DiscountableAmt;
        /// <summary>The amount used as a base for discount calculation if the discount is based on the amount. The amount is in the base currency of the company,</summary>
        [PXDBDecimal(4)]
        public virtual Decimal? DiscountableAmt
        {
            get
            {
                return _DiscountableAmt;
            }
            set
            {
                _DiscountableAmt = value;
            }
        }
        #endregion
        #region CuryDiscountableAmt
        /// <exclude />
        public abstract class curyDiscountableAmt : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _CuryDiscountableAmt;
        /// <summary>The amount used as a base for discount calculation if the discount is based on the amount. The amount is in the currency of the document (<see cref="Document.CuryID" />).</summary>
        [PXDBCurrency(typeof(curyInfoID), typeof(discountableAmt))]
        [PXUIField(DisplayName = "Discountable Amt.", Enabled = false)]
        public virtual Decimal? CuryDiscountableAmt
        {
            get
            {
                return _CuryDiscountableAmt;
            }
            set
            {
                _CuryDiscountableAmt = value;
            }
        }
        #endregion
        #region DiscountableQty
        /// <exclude />
        public abstract class discountableQty : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _DiscountableQty;
        /// <summary>The quantity used as a base for discount calculation if the discount is based on the item quantity.</summary>
        [PXDBQuantity(MinValue = 0)]
        [PXUIField(DisplayName = "Discountable Qty.", Enabled = false)]
        public virtual Decimal? DiscountableQty
        {
            get
            {
                return _DiscountableQty;
            }
            set
            {
                _DiscountableQty = value;
            }
        }
        #endregion
        #region DiscountAmt
        /// <exclude />
        public abstract class discountAmt : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _DiscountAmt;
        /// <summary>The amount of the discount, in the base currency of the company.</summary>
        [PXDBDecimal(4)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? DiscountAmt
        {
            get
            {
                return _DiscountAmt;
            }
            set
            {
                _DiscountAmt = value;
            }
        }
        #endregion
        #region CuryDiscountAmt
        /// <exclude />
        public abstract class curyDiscountAmt : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _CuryDiscountAmt;
        /// <summary>The amount of the discount, in the currency of the document (<see cref="Document.CuryID" />).</summary>
        [PXDBCurrency(typeof(curyInfoID), typeof(discountAmt))]
        [PXUIField(DisplayName = "Discount Amt.", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryDiscountAmt
        {
            get
            {
                return _CuryDiscountAmt;
            }
            set
            {
                _CuryDiscountAmt = value;
            }
        }
        #endregion
        #region DiscountPct
        /// <exclude />
        public abstract class discountPct : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _DiscountPct;
        /// <summary>The discount percent if by definition the discount is calculated as a percentage.</summary>
        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Discount Percent", Enabled = false)]
        public virtual Decimal? DiscountPct
        {
            get
            {
                return _DiscountPct;
            }
            set
            {
                _DiscountPct = value;
            }
        }
        #endregion
        #region FreeItemID
        /// <exclude />
        public abstract class freeItemID : IBqlField
        {
        }
        /// <exclude />
        protected Int32? _FreeItemID;
        /// <summary>The identifier of the free item, if one is specified by the discount applied to the document.</summary>
        [Inventory(DisplayName = "Free Item", Enabled = false)]
        public virtual Int32? FreeItemID
        {
            get
            {
                return _FreeItemID;
            }
            set
            {
                _FreeItemID = value;
            }
        }
        #endregion
        #region FreeItemQty
        /// <exclude />
        public abstract class freeItemQty : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _FreeItemQty;
        /// <summary>The quantity of the free item to be added as a discount.</summary>
        [PXDBQuantity(MinValue = 0)]
        [PXUIField(DisplayName = "Free Item Qty.", Enabled = false)]
        public virtual Decimal? FreeItemQty
        {
            get
            {
                return _FreeItemQty;
            }
            set
            {
                _FreeItemQty = value;
            }
        }
        #endregion
        #region IsManual
        /// <exclude />
        public abstract class isManual : IBqlField
        {
        }
        /// <exclude />
        protected Boolean? _IsManual;
        /// <summary>Indicates (if set to <tt>true</tt>) that the discount applicable to this detail row was changed manually.</summary>
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Manual Discount", Enabled = false)]
        public virtual Boolean? IsManual
        {
            get
            {
                return _IsManual;
            }
            set
            {
                _IsManual = value;
            }
        }
        #endregion

        #region System Columns
        #region tstamp
        /// <exclude />
        public abstract class Tstamp : IBqlField
        {
        }
        /// <exclude />
        protected Byte[] _tstamp;
        /// <exclude />
        [PXDBTimestamp]
        public virtual Byte[] tstamp
        {
            get
            {
                return _tstamp;
            }
            set
            {
                _tstamp = value;
            }
        }
        #endregion
        #region CreatedByID
        /// <exclude />
        public abstract class createdByID : IBqlField
        {
        }
        /// <exclude />
        protected Guid? _CreatedByID;
        /// <exclude />
        [PXDBCreatedByID]
        public virtual Guid? CreatedByID
        {
            get
            {
                return _CreatedByID;
            }
            set
            {
                _CreatedByID = value;
            }
        }
        #endregion
        #region CreatedByScreenID
        /// <exclude />
        public abstract class createdByScreenID : IBqlField
        {
        }
        /// <exclude />
        protected String _CreatedByScreenID;
        /// <exclude />
        [PXDBCreatedByScreenID]
        public virtual String CreatedByScreenID
        {
            get
            {
                return _CreatedByScreenID;
            }
            set
            {
                _CreatedByScreenID = value;
            }
        }
        #endregion
        #region CreatedDateTime
        /// <exclude />
        public abstract class createdDateTime : IBqlField
        {
        }
        /// <exclude />
        protected DateTime? _CreatedDateTime;
        /// <exclude />
        [PXDBCreatedDateTime]
        public virtual DateTime? CreatedDateTime
        {
            get
            {
                return _CreatedDateTime;
            }
            set
            {
                _CreatedDateTime = value;
            }
        }
        #endregion
        #region LastModifiedByID
        /// <exclude />
        public abstract class lastModifiedByID : IBqlField
        {
        }
        /// <exclude />
        protected Guid? _LastModifiedByID;
        /// <exclude />
        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID
        {
            get
            {
                return _LastModifiedByID;
            }
            set
            {
                _LastModifiedByID = value;
            }
        }
        #endregion
        #region LastModifiedByScreenID
        /// <exclude />
        public abstract class lastModifiedByScreenID : IBqlField
        {
        }
        /// <exclude />
        protected String _LastModifiedByScreenID;
        /// <exclude />
        [PXDBLastModifiedByScreenID]
        public virtual String LastModifiedByScreenID
        {
            get
            {
                return _LastModifiedByScreenID;
            }
            set
            {
                _LastModifiedByScreenID = value;
            }
        }
        #endregion
        #region LastModifiedDateTime
        /// <exclude />
        public abstract class lastModifiedDateTime : IBqlField
        {
        }
        /// <exclude />
        protected DateTime? _LastModifiedDateTime;
        /// <exclude />
        [PXDBLastModifiedDateTime]
        public virtual DateTime? LastModifiedDateTime
        {
            get
            {
                return _LastModifiedDateTime;
            }
            set
            {
                _LastModifiedDateTime = value;
            }
        }
        #endregion
        #endregion
    }
}
