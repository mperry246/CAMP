using System;
using PX.Data;
using PX.Objects.CM;

namespace PX.Objects.Extensions.Discount
{
    /// <summary>A mapped cache extension that represents a document that supports discounts.</summary>
    public class Document : PXMappedCacheExtension
    {
        #region BranchID
        /// <exclude />
        public abstract class branchID : IBqlField
        {
        }
        /// <exclude />
        protected Int32? _BranchID;

        /// <summary>The identifier of the branch associated with the document.</summary>
        public virtual Int32? BranchID
        {
            get
            {
                return _BranchID;
            }
            set
            {
                _BranchID = value;
            }
        }
        #endregion
        #region CustomerID
        /// <exclude />
        public abstract class customerID : IBqlField
        {
        }
        /// <exclude />
        protected Int32? _CustomerID;
        /// <summary>The identifier of a customer account to whom this document belongs.</summary>
        public virtual Int32? CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                _CustomerID = value;
            }
        }
        #endregion
        #region CuryID
        /// <exclude />
        public abstract class curyID : IBqlField
        {
        }
        /// <exclude />
        protected String _CuryID;

        /// <summary>The identifier of the currency of the document.</summary>
        public virtual String CuryID
        {
            get
            {
                return _CuryID;
            }
            set
            {
                _CuryID = value;
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

        /// <summary>The identifier of the <see cref="CurrencyInfo">CurrencyInfo</see> object associated with the document.</summary>
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
        #region CuryOrigDiscAmt
        /// <exclude />
        public abstract class curyOrigDiscAmt : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _CuryOrigDiscAmt;

        /// <summary>The cash discount allowed for the document in the currency of the document (<see cref="CuryID" />).</summary>
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXDBCurrency(typeof(curyInfoID), typeof(origDiscAmt))]
        [PXUIField(DisplayName = "Cash Discount", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? CuryOrigDiscAmt
        {
            get
            {
                return _CuryOrigDiscAmt;
            }
            set
            {
                _CuryOrigDiscAmt = value;
            }
        }
        #endregion
        #region OrigDiscAmt
        /// <exclude />
        public abstract class origDiscAmt : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _OrigDiscAmt;

        /// <summary>The cash discount allowed for the document in the currency of the company.</summary>
        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? OrigDiscAmt
        {
            get
            {
                return _OrigDiscAmt;
            }
            set
            {
                _OrigDiscAmt = value;
            }
        }
        #endregion
        #region CuryDiscTaken
        /// <exclude />
        public abstract class curyDiscTaken : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _CuryDiscTaken;

        /// <summary>The cash discount applied to the document, in the currency of the document (<see cref="CuryID" />).</summary>
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXDBCurrency(typeof(curyInfoID), typeof(discTaken))]
        public virtual Decimal? CuryDiscTaken
        {
            get
            {
                return _CuryDiscTaken;
            }
            set
            {
                _CuryDiscTaken = value;
            }
        }
        #endregion
        #region DiscTaken
        /// <exclude />
        public abstract class discTaken : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _DiscTaken;

        /// <summary>The cash discount actually applied to the document, in the base currency of the company.</summary>
        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? DiscTaken
        {
            get
            {
                return _DiscTaken;
            }
            set
            {
                _DiscTaken = value;
            }
        }
        #endregion
        #region CuryDiscBal
        /// <exclude />
        public abstract class curyDiscBal : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _CuryDiscBal;

        /// <summary>The cash discount balance of the document, in the currency of the document (<see cref="CuryID" />).</summary>
        [PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        [PXDBCurrency(typeof(curyInfoID), typeof(discBal), BaseCalc = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryDiscBal
        {
            get
            {
                return _CuryDiscBal;
            }
            set
            {
                _CuryDiscBal = value;
            }
        }
        #endregion
        #region DiscBal
        /// <exclude />
        public abstract class discBal : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _DiscBal;

        /// <summary>The cash discount balance of the document, in the base currency of the company.</summary>
        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? DiscBal
        {
            get
            {
                return _DiscBal;
            }
            set
            {
                _DiscBal = value;
            }
        }
        #endregion
        #region DiscTot
        /// <exclude />
        public abstract class discTot : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _DiscTot;

        /// <summary>The total group and document discount for the document, in the base currency of the company.</summary>
        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? DiscTot
        {
            get
            {
                return _DiscTot;
            }
            set
            {
                _DiscTot = value;
            }
        }
        #endregion
        #region CuryDiscTot
        /// <exclude />
        public abstract class curyDiscTot : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _CuryDiscTot;

        /// <summary>The total group and document discount for the document. The discount is in the currency of the document (<see cref="CuryID" />).</summary>
        public virtual Decimal? CuryDiscTot
        {
            get
            {
                return _CuryDiscTot;
            }
            set
            {
                _CuryDiscTot = value;
            }
        }
        #endregion
        #region DocDisc
        /// <exclude />
        public abstract class docDisc : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _DocDisc;

        /// <summary>The document discount amount (without group discounts) for the document. The amount is in the base currency of the company.</summary>
        [PXBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual Decimal? DocDisc
        {
            get
            {
                return _DocDisc;
            }
            set
            {
                _DocDisc = value;
            }
        }
        #endregion
        #region CuryDocDisc
        /// <exclude />
        public abstract class curyDocDisc : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _CuryDocDisc;

        /// <summary>The document discount amount (without group discounts) for the document. The discount is in the currency of the document (<see cref="CuryID" />).</summary>
        [PXCurrency(typeof(curyInfoID), typeof(docDisc))]
        public virtual Decimal? CuryDocDisc
        {
            get
            {
                return _CuryDocDisc;
            }
            set
            {
                _CuryDocDisc = value;
            }
        }
        #endregion
        #region CuryDiscountedDocTotal
        /// <exclude />
        public abstract class curyDiscountedDocTotal : IBqlField
        {
        }
        /// <exclude />
        protected decimal? _CuryDiscountedDocTotal;

        /// <summary>The discounted amount of the document, in the currency of the document (<see cref="CuryID" />).</summary>
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXCurrency(typeof(curyInfoID), typeof(discountedDocTotal))]
        [PXUIField(DisplayName = "Discounted Doc. Total", Visibility = PXUIVisibility.Visible)]
        public virtual decimal? CuryDiscountedDocTotal
        {
            get
            {
                return _CuryDiscountedDocTotal;
            }
            set
            {
                _CuryDiscountedDocTotal = value;
            }
        }
        #endregion
        #region DiscountedDocTotal
        /// <exclude />
        public abstract class discountedDocTotal : IBqlField
        {
        }
        /// <exclude />
        protected decimal? _DiscountedDocTotal;

        /// <summary>The discounted amount of the document, in the base currency of the company.</summary>
        [PXBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual decimal? DiscountedDocTotal
        {
            get
            {
                return _DiscountedDocTotal;
            }
            set
            {
                _DiscountedDocTotal = value;
            }
        }
        #endregion
        #region CuryDiscountedTaxableTotal
        /// <exclude />
        public abstract class curyDiscountedTaxableTotal : IBqlField
        {
        }
        /// <exclude />
        protected decimal? _CuryDiscountedTaxableTotal;

        /// <summary>The total taxable amount reduced on early payment according to cash discount. The amount is in the currency of the document (<see cref="CuryID" />).</summary>
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXCurrency(typeof(curyInfoID), typeof(discountedTaxableTotal))]
        [PXUIField(DisplayName = "Discounted Taxable Total", Visibility = PXUIVisibility.Visible)]
        public virtual decimal? CuryDiscountedTaxableTotal
        {
            get
            {
                return _CuryDiscountedTaxableTotal;
            }
            set
            {
                _CuryDiscountedTaxableTotal = value;
            }
        }
        #endregion
        #region DiscountedTaxableTotal
        /// <exclude />
        public abstract class discountedTaxableTotal : IBqlField
        {
        }
        /// <exclude />
        protected decimal? _DiscountedTaxableTotal;

        /// <summary>The total taxable amount reduced on early payment according to cash discount. The amount is in the base currency of the company.</summary>
        [PXBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual decimal? DiscountedTaxableTotal
        {
            get
            {
                return _DiscountedTaxableTotal;
            }
            set
            {
                _DiscountedTaxableTotal = value;
            }
        }
        #endregion
        #region CuryDiscountedPrice
        /// <exclude />
        public abstract class curyDiscountedPrice : IBqlField
        {
        }
        /// <exclude />
        protected decimal? _CuryDiscountedPrice;

        /// <summary>The total tax amount reduced on early payment according to cash discount. The amount is in the currency of the document (<see cref="CuryID" />).</summary>
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXCurrency(typeof(curyInfoID), typeof(discountedPrice))]
        [PXUIField(DisplayName = "Tax on Discounted Price", Visibility = PXUIVisibility.Visible)]
        public virtual decimal? CuryDiscountedPrice
        {
            get
            {
                return _CuryDiscountedPrice;
            }
            set
            {
                _CuryDiscountedPrice = value;
            }
        }
        #endregion
        #region DiscountedPrice
        /// <exclude />
        public abstract class discountedPrice : IBqlField
        {
        }
        /// <exclude />
        protected decimal? _DiscountedPrice;

        /// <summary>The total tax amount reduced on early payment according to cash discount. The amount is in the base currency of the company.</summary>
        [PXBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual decimal? DiscountedPrice
        {
            get
            {
                return _DiscountedPrice;
            }
            set
            {
                _DiscountedPrice = value;
            }
        }
        #endregion
        #region LocationID
        /// <exclude />
        public abstract class locationID : IBqlField
        {
        }
        /// <exclude />
        protected Int32? _LocationID;

        /// <summary>The identifier of the location of the customer.</summary>
        public virtual Int32? LocationID
        {
            get
            {
                return _LocationID;
            }
            set
            {
                _LocationID = value;
            }
        }
        #endregion
        #region DocumentDate
        /// <exclude />
        public abstract class documentDate : IBqlField
        {
        }
        /// <exclude />
        protected DateTime? _DocumentDate;
        /// <summary>The date of the document.</summary>
        public virtual DateTime? DocumentDate
        {
            get
            {
                return _DocumentDate;
            }
            set
            {
                _DocumentDate = value;
            }
        }
        #endregion
        #region CuryLineTotal
        /// <exclude />
        public abstract class curyLineTotal : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _CuryLineTotal;

        /// <summary>The total amount of the lines of the document, in the currency of the document (<see cref="CuryID" />).</summary>
        public virtual Decimal? CuryLineTotal
        {
            get
            {
                return _CuryLineTotal;
            }
            set
            {
                _CuryLineTotal = value;
            }
        }
        #endregion
        #region LineTotal
        /// <exclude />
        public abstract class lineTotal : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _LineTotal;

        /// <summary>The total amount of the lines of the document, in the base currency of the company.</summary>
        public virtual Decimal? LineTotal
        {
            get
            {
                return _LineTotal;
            }
            set
            {
                _LineTotal = value;
            }
        }
        #endregion
        #region CuryMiscTot
        /// <exclude />
        public abstract class curyMiscTot : IBqlField
        {
        }
        /// <exclude />
        protected Decimal? _CuryMiscTot;
        /// <summary>The miscellaneous total amount, in the currency of the document (<see cref="CuryID" />).</summary>
        public virtual Decimal? CuryMiscTot
        {
            get
            {
                return _CuryMiscTot;
            }
            set
            {
                _CuryMiscTot = value;
            }
        }
        #endregion

    }
}
