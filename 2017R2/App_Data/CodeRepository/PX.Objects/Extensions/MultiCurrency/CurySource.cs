using System;
using PX.Data;

namespace PX.Objects.Extensions.MultiCurrency
{
    /// <summary>A mapped cache extension that contains information on the currency source.</summary>
    public class CurySource : PXMappedCacheExtension
    {
        #region CuryID
        /// <exclude />
        public abstract class curyID : IBqlField
        {
        }
        protected String _CuryID;
        /// <summary>The identifier of the currency in the system.</summary>
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
        #region CuryRateTypeID
        /// <exclude />
        public abstract class curyRateTypeID : IBqlField
        {
        }
        protected String _CuryRateTypeID;
        /// <summary>The identifier of the currency rate type in the system.</summary>
        public virtual String CuryRateTypeID
        {
            get
            {
                return _CuryRateTypeID;
            }
            set
            {
                _CuryRateTypeID = value;
            }
        }
        #endregion
        #region AllowOverrideCury
        /// <exclude />
        public abstract class allowOverrideCury : IBqlField
        {
        }
        protected Boolean? _AllowOverrideCury;
        /// <summary>A property that indicates (if set to <tt>true</tt>) that the currency of the customer documents (<see cref="CuryID" />) can be overridden by a user during document entry.</summary>
        public virtual Boolean? AllowOverrideCury
        {
            get
            {
                return _AllowOverrideCury;
            }
            set
            {
                _AllowOverrideCury = value;
            }
        }
        #endregion
        #region AllowOverrideRate
        /// <exclude />
        public abstract class allowOverrideRate : IBqlField
        {
        }
        protected Boolean? _AllowOverrideRate;
        /// <summary>A property that indicates (if set to <tt>true</tt>) that the currency rate for the customer documents (which is calculated by the system based on the currency rate history) can be overridden
        /// by a user during document entry.</summary>
        public virtual Boolean? AllowOverrideRate
        {
            get
            {
                return _AllowOverrideRate;
            }
            set
            {
                _AllowOverrideRate = value;
            }
        }
        #endregion
    }
}
