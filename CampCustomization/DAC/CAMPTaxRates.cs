using System;
using PX.Data;

namespace CAMPCustomization1
{
    [Serializable]
    public class CAMPTaxRates : IBqlTable
    {
        #region TaxZoneID
        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Tax Zone ID")]
        public virtual string TaxZoneID { get; set; }
        public abstract class taxZoneID : IBqlField { }
        #endregion

        #region TaxType
        [PXDBString(1, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Tax Type")]
        public virtual string TaxType { get; set; }
        public abstract class taxType : IBqlField { }
        #endregion

        #region TaxRate
        [PXDBDecimal()]
        [PXUIField(DisplayName = "Tax Rate")]
        public virtual Decimal? TaxRate { get; set; }
        public abstract class taxRate : IBqlField { }
        #endregion
    }
}
