using System;
using PX.Data;

namespace CAMPCustomization1
{
    [Serializable]
    public class CampLogoLogic : IBqlTable
    {
        #region BranchID
        [PXDBInt()]
        [PXUIField(DisplayName = "Branch ID")]
        public virtual int? BranchID { get; set; }
        public abstract class branchID : IBqlField { }
        #endregion

        #region CustomerID
        [PXDBInt()]
        [PXUIField(DisplayName = "Customer ID")]
        public virtual int? CustomerID { get; set; }
        public abstract class customerID : IBqlField { }
        #endregion

        #region StatementDate
        [PXDBDate()]
        [PXUIField(DisplayName = "Statement Date")]
        public virtual DateTime? StatementDate { get; set; }
        public abstract class statementDate : IBqlField { }
        #endregion

        #region Curyid
        [PXDBString(5, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Curyid")]
        public virtual string Curyid { get; set; }
        public abstract class curyid : IBqlField { }
        #endregion

        #region StatementCycleId
        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Statement Cycle Id")]
        public virtual string StatementCycleId { get; set; }
        public abstract class statementCycleId : IBqlField { }
        #endregion

        #region Numlob
        [PXDBInt()]
        [PXUIField(DisplayName = "Numlob")]
        public virtual int? Numlob { get; set; }
        public abstract class numlob : IBqlField { }
        #endregion

        #region LineofBusinessCD
        [PXDBInt()]
        [PXUIField(DisplayName = "Lineof Business CD")]
        public virtual int? LineofBusinessCD { get; set; }
        public abstract class lineofBusinessCD : IBqlField { }
        #endregion
    }
}
