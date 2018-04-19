using System;
using PX.Data;

namespace CAMPCustomization
{
    [Serializable]
    public class CampBTvNBT : IBqlTable
    {
        #region DocType
        [PXDBString(3, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Doc Type")]
        public virtual string DocType { get; set; }
        public abstract class docType : IBqlField { }
        #endregion

        #region RefNbr
        [PXDBString(15, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Ref Nbr")]
        public virtual string RefNbr { get; set; }
        public abstract class refNbr : IBqlField { }
        #endregion

        #region DocDate
        [PXDBDate()]
        [PXUIField(DisplayName = "Doc Date")]
        public virtual DateTime? DocDate { get; set; }
        public abstract class docDate : IBqlField { }
        #endregion

        #region FinPeriodID
        [PXDBString(6, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Fin Period ID")]
        public virtual string FinPeriodID { get; set; }
        public abstract class finPeriodID : IBqlField { }
        #endregion

        #region CustomerID
        [PXDBInt()]
        [PXUIField(DisplayName = "Customer ID")]
        public virtual int? CustomerID { get; set; }
        public abstract class customerID : IBqlField { }
        #endregion

        #region Maxbt
        [PXDBString(3, InputMask = "")]
        [PXUIField(DisplayName = "Maxbt")]
        public virtual string Maxbt { get; set; }
        public abstract class maxbt : IBqlField { }
        #endregion
    }
}