using System;
using PX.Data;

namespace PX.Objects.Extensions.SalesTax
{
    /// <summary>A mapped cache extension that represents a tax detail line.</summary>
    public class TaxDetail : PXMappedCacheExtension
    {
        #region RefTaxID
        /// <exclude />
        public abstract class refTaxID : IBqlField { }

        /// <summary>The ID of the tax calculated on the document.</summary>
        public virtual string RefTaxID { get; set; }
        #endregion
        #region TaxRate
        /// <exclude />
        public abstract class taxRate : IBqlField { }

        /// <summary>The tax rate used for tax calculation.</summary>
        public virtual decimal? TaxRate { get; set; }
        #endregion
        #region CuryInfoID
        /// <exclude />
        public abstract class curyInfoID : IBqlField { }

        /// <summary>Identifier of the <see cref="CurrencyInfo">CurrencyInfo</see> object associated with the document.</summary>
        public virtual Int64? CuryInfoID { get; set; }
        #endregion
        #region NonDeductibleTaxRate
        /// <exclude />
        public abstract class nonDeductibleTaxRate : IBqlField { }

        /// <summary>The percent of deduction that applies to the tax amount paid to the vendor for specific purchases.</summary>
        public virtual Decimal? NonDeductibleTaxRate { get; set; }
        #endregion
        #region CuryOrigTaxableAmt
        /// <exclude />
        public abstract class curyOrigTaxableAmt : IBqlField
        {
        }


        public virtual decimal? CuryOrigTaxableAmt { get; set; }
        #endregion
        #region CuryTaxableAmt
        /// <exclude />
        public abstract class curyTaxableAmt : IBqlField
        {
        }

        /// <summary>The taxable amount (tax base), in the currency of the document (<see cref="CuryID" />).</summary>
        public virtual decimal? CuryTaxableAmt { get; set; }
        #endregion
        #region CuryTaxAmt
        /// <exclude />
        public abstract class curyTaxAmt : IBqlField
        {
        }

        /// <summary>The tax amount, in the currency of the document (<see cref="CuryID" />).</summary>
        public virtual decimal? CuryTaxAmt { get; set; }
        #endregion
        #region CuryExpenseAmt
        /// <exclude />
        public abstract class curyExpenseAmt : IBqlField { }

        /// <summary>The percentage that is deducted from the tax amount paid to the vendor for specific purchases, in the currency of the document (<see cref="CuryID" />).</summary>
        public virtual Decimal? CuryExpenseAmt { get; set; }
        #endregion
        #region ExpenseAmt
        /// <exclude />
        public abstract class expenseAmt : IBqlField { }

        /// <summary>The percentage that is deducted from the tax amount paid to the vendor for specific purchases, in the base currency of the company.</summary>
        public virtual Decimal? ExpenseAmt { get; set; }
        #endregion

        /// <summary>Converts TaxDetail to <see cref="TaxItem" />.</summary>
        public static explicit operator TaxItem(TaxDetail i)
        {
            return new TaxItem { TaxID = i.RefTaxID };
        }

    }
}
