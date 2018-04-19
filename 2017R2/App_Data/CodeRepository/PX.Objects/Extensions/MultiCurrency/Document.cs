using System;
using PX.Data;

namespace PX.Objects.Extensions.MultiCurrency
{
    /// <summary>A mapped cache extension that represents a document that supports multiple currencies.</summary>
    public class Document : PXMappedCacheExtension
    {
        #region BAccountID
        /// <exclude />
        public abstract class bAccountID : IBqlField
        {
        }
        protected Int32? _BAccountID;

        /// <summary>The identifier of the business account of the document.</summary>
        /// <value>
        /// Corresponds to the <see cref="BAccount.BAccountID" /> field.
        /// </value>
        public virtual Int32? BAccountID
        {
            get
            {
                return _BAccountID;
            }
            set
            {
                _BAccountID = value;
            }
        }
        #endregion
        #region CuryID
        /// <exclude />
        public abstract class curyID : IBqlField
        {
        }
        protected String _CuryID;

        /// <summary>
        /// The code of the <see cref="Currency"/> of the document.
        /// </summary>
        /// <value>
        /// Defaults to the <see cref="Company.BaseCuryID">base currency of the company</see>.
        /// Corresponds to the <see cref="Currency.CuryID"/> field.
        /// </value>
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
        protected Int64? _CuryInfoID;

        /// <summary>The identifier of the <see cref="CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.</summary>
        /// <value>
        /// Corresponds to the <see cref="CurrencyInfoID" /> field.
        /// </value>
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
        #region DocumentDate
        /// <exclude />
        public abstract class documentDate : IBqlField
        {
        }
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
    }
}
