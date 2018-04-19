using System;
using PX.Data;

namespace PX.Objects.Extensions.SalesTax
{
    /// <summary>A mapped cache extension that represents a detail line of the document.</summary>
    public class Detail : PXMappedCacheExtension
    {
        #region TaxCategoryID
        /// <exclude />
        public abstract class taxCategoryID : IBqlField
        {
        }
        /// <summary>Identifier of the tax category associated with the line.</summary>

        public virtual string TaxCategoryID { get; set; }
        #endregion
        #region TaxID
        /// <exclude />
        public abstract class taxID : IBqlField
        {
        }

        /// <summary>The identifier of the tax applied to the detail line.</summary>
        public virtual string TaxID { get; set; }
        #endregion
        #region CuryInfoID
        /// <exclude />
        public abstract class curyInfoID : IBqlField
        {
        }
        /// <exclude />
        protected Int64? _CuryInfoID;

        /// <summary>
        /// Identifier of the CurrencyInfo object associated with the document.
        /// </summary>

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
        #region CuryTranAmt
        /// <exclude />
        public abstract class curyTranAmt : IBqlField { }

        /// <summary>The total amount for the line item, in the currency of the document (<see cref="Document.CuryID" />).</summary>
        public decimal? CuryTranAmt { get; set; }
        #endregion
        #region GroupDiscountRate
        /// <exclude />
        public abstract class groupDiscountRate : IBqlField
        {
        }

        /// <summary>The Group-level discount rate.</summary>
        public virtual decimal? GroupDiscountRate { get; set; }
        #endregion
        #region DocumentDiscountRate
        /// <exclude />
        public abstract class documentDiscountRate : IBqlField
        {
        }

        /// <summary>The Document-level discount rate.</summary>
        public virtual decimal? DocumentDiscountRate { get; set; }
        #endregion
    }
}
