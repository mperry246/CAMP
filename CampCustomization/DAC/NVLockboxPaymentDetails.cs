using System;
using System.Collections;
using System.Collections.Generic;
using PX.SM;
using PX.Data;


namespace NV.Lockbox
{
    [System.SerializableAttribute()]
    public class NVLockboxPaymentDetail : PX.Data.IBqlTable
    {
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }
        [PX.Objects.GL.Branch(typeof(Search<PX.Objects.GL.Branch.branchID,
            Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>), IsDetail = false)]
        [PXUIField(DisplayName = "Branch", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Branch.branchID), SubstituteKey = typeof(PX.SM.Branch.branchCD),
                    DescriptionField = typeof(Branch.branchCD))]
        public virtual int? BranchID { get; set; }
        #endregion

        #region LockboxTranCD
        public abstract class lockboxTranCD : PX.Data.IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true, IsKey = true)]
        [PXDBLiteDefault(typeof(NVLockboxPayment.lockboxTranCD))]
        [PXUIField(DisplayName = "LockboxTranCD", IsReadOnly = true)]
        [PXParent(typeof(Select<NVLockboxPayment, Where
            <NVLockboxPayment.lockboxTranCD, Equal<Current<NVLockboxPaymentDetail.lockboxTranCD>>>>))]
        public virtual string LockboxTranCD { get; set; }
        #endregion
        #region LockboxTranDetailID
        public abstract class lockboxTranDetailID : PX.Data.IBqlField
        {
        }
        [PXDBInt(IsKey = true)]
        [PXLineNbr(typeof(NVLockboxPayment.lastLineNbr))]
        public virtual int? LockboxTranDetailID { get; set; }
        #endregion

        #region InvoiceNbr
        public abstract class invoiceNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Invoice Nbr", IsReadOnly=true)]
        public virtual string InvoiceNbr { get; set; }
        #endregion	
        #region InvoiceAmt
        public abstract class invoiceAmt : PX.Data.IBqlField
        {
        }
        [PXDBDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Invoice Balance", IsReadOnly = true)]
        public virtual decimal? InvoiceAmt { get; set; }
        #endregion
        #region ApplyAmt
        public abstract class applyAmt : PX.Data.IBqlField
        {
        }
        [PXDBDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Apply Amt")]
        [PXFormula(null, typeof(SumCalc<NVLockboxPayment.checkAmt>))]
        public virtual decimal? ApplyAmt { get; set; }
        #endregion

        #region Selected
        public abstract class selected : IBqlField
        {
        }
        [PXBool()]
        [PXUIField(DisplayName = "Selected", Enabled = true)]
        public virtual bool? Selected { get; set; }
        #endregion

        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }
        [PXDBTimestamp()]
        public virtual byte[] tstamp { get; set; }
        #endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }
        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID { get; set; }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID { get; set; }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }
        [PXDBCreatedDateTime()]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID { get; set; }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }
        [PXDBLastModifiedDateTime()]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion       
      			


    }
}