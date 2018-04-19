using System;
using PX.Data;

namespace CAMPCustomization1
{
    [Serializable]
    public class CampPositivePay : IBqlTable
    {
        #region CashAccount
        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Cash Account")]
        public virtual string CashAccount { get; set; }
        public abstract class cashAccount : IBqlField { }
        #endregion

        #region TranType
        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Tran Type")]
        public virtual string TranType { get; set; }
        public abstract class tranType : IBqlField { }
        #endregion

        #region ChkDate
        [PXDBDate()]
        [PXUIField(DisplayName = "Chk Date")]
        public virtual DateTime? ChkDate { get; set; }
        public abstract class chkDate : IBqlField { }
        #endregion

        #region ChkNbr
        [PXDBString(40, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Chk Nbr")]
        public virtual string ChkNbr { get; set; }
        public abstract class chkNbr : IBqlField { }
        #endregion

        #region AcctNbr
        [PXDBString(40, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Acct Nbr")]
        public virtual string AcctNbr { get; set; }
        public abstract class acctNbr : IBqlField { }
        #endregion

        #region ChkAmt
        [PXDBDecimal()]
        [PXUIField(DisplayName = "Chk Amt")]
        public virtual Decimal? ChkAmt { get; set; }
        public abstract class chkAmt : IBqlField { }
        #endregion

        #region Payee
        [PXDBString(255, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Payee")]
        public virtual string Payee { get; set; }
        public abstract class payee : IBqlField { }
        #endregion

        #region Ccichkdate
        [PXDBString(4000, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Ccichkdate")]
        public virtual string Ccichkdate { get; set; }
        public abstract class ccichkdate : IBqlField { }
        #endregion

        #region Ccichknbr
        [PXDBString(40, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Ccichknbr")]
        public virtual string Ccichknbr { get; set; }
        public abstract class ccichknbr : IBqlField { }
        #endregion

        #region Cciacctnbr
        [PXDBString(40, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Cciacctnbr")]
        public virtual string Cciacctnbr { get; set; }
        public abstract class cciacctnbr : IBqlField { }
        #endregion

        #region Ccichkamt
        [PXDBString(8000, InputMask = "")]
        [PXUIField(DisplayName = "Ccichkamt")]
        public virtual string Ccichkamt { get; set; }
        public abstract class ccichkamt : IBqlField { }
        #endregion

        #region Ccipayee
        [PXDBString(40, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Ccipayee")]
        public virtual string Ccipayee { get; set; }
        public abstract class ccipayee : IBqlField { }
        #endregion
    }
}