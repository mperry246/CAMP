using CAMPCustomization;
using MaxQ.Products.RBRR;
using PX.Data;
using PX.Objects.CS;
using System;

namespace PX.Objects.AR
{
    public class ARRegisterExt : PXCacheExtension<PX.Objects.AR.ARRegister>
    {
        #region UsrContractCD
        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "ContractCD", IsReadOnly = true)]
        public virtual string UsrContractCD { get; set; }
        public abstract class usrContractCD : IBqlField { }
        #endregion

        #region UsrRevisionNbr
        [PXDBString(5, IsUnicode = true)]
        [PXUIField(DisplayName = "Revision Nbr", IsReadOnly = true)]
        public virtual string UsrRevisionNbr { get; set; }
        public abstract class usrRevisionNbr : IBqlField { }
        #endregion

        #region UsrContractID
        [PXDBInt]
        [PXUIField(DisplayName = "Contract/Revision Nbr")]
        [PXSelector(typeof(Search<XRBContrHdr.contractID>),
          new Type[]{
           typeof(XRBContrHdr.contractCD),
           typeof(XRBContrHdr.revisionNbr)},
          DescriptionField = typeof(XRBContrHdr.contractCDRevNbr))]
        public virtual int? UsrContractID { get; set; }
        public abstract class usrContractID : IBqlField { }
        #endregion

        #region UsrLineofBusiness


        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Line of Business")]
        [PXSelector(typeof(Search<CAMPLineofBusiness.lineofBusinessID>),
                   new Type[]{
                typeof(CAMPLineofBusiness.lineofBusinessCD),
                typeof(CAMPLineofBusiness.description)},
                   SubstituteKey = typeof(CAMPLineofBusiness.lineofBusinessCD),
                   DescriptionField = typeof(CAMPLineofBusiness.description))]
        public virtual int? UsrLineofBusiness { get; set; }
        public abstract class usrLineofBusiness : IBqlField { }
        #endregion

        #region UsrContractTypeID
        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Contract Type")]
        [PXSelector(typeof(Search<CAMPContractType.contractTypeID>),
          new Type[]{
           typeof(CAMPContractType.contractTypeCD),
           typeof(CAMPContractType.description)},
          SubstituteKey = typeof(CAMPContractType.contractTypeCD),
          DescriptionField = typeof(CAMPContractType.description))]
        public virtual int? UsrContractTypeID { get; set; }
        public abstract class usrContractTypeID : IBqlField { }
        #endregion

        #region UsrReasonCodeID
        [PXDBString(20, InputMask = ">aaaaaaaaaaaaaaaaaaaa")]
        [PXUIField(DisplayName = "Reason Code", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<PX.Objects.CS.ReasonCode.reasonCodeID>))]
        [PXRestrictor(typeof(Where<ReasonCodeExt.usrActive, Equal<True>>),
          "Reason '{0}' is inactive", typeof(PX.Objects.CS.ReasonCode.reasonCodeID))]
        public virtual string UsrReasonCodeID { get; set; }
        public abstract class usrReasonCodeID : IBqlField { }
        #endregion

        #region UsrGenerateDate
        [PXDBDate]
        [PXUIField(DisplayName = "Term Start")]

        public virtual DateTime? UsrGenerateDate { get; set; }
        public abstract class usrGenerateDate : IBqlField { }
        #endregion

        #region UsrContractEndDate
        [PXDBDate]
        [PXUIField(DisplayName = "Term End")]

        public virtual DateTime? UsrContractEndDate { get; set; }
        public abstract class usrContractEndDate : IBqlField { }
        #endregion

        #region UsrCollectionStatus

        [PXDBString(1, IsUnicode = true)]
        [PXUIField(DisplayName = "Collection Status")]
        [CollectionStatus.List()]
        public virtual string UsrCollectionStatus { get; set; }
        public abstract class usrCollectionStatus : IBqlField { }
        #endregion

        #region UsrOrginalRefNbr
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Orginal RefNbr", IsReadOnly = true)]
        public virtual string UsrOrginalRefNbr { get; set; }
        public abstract class usrOrginalRefNbr : IBqlField { }
        #endregion

        #region UsrApplyAmt
        [PXDecimal(2)]
        [PXUIField(DisplayName = "Apply Amt")]
        public virtual Decimal? UsrApplyAmt { get; set; }
        public abstract class usrApplyAmt : IBqlField { }
        #endregion

        #region UsrPaymentStatus
        [PXString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Payment Status", IsReadOnly = true)]
        public virtual string UsrPaymentStatus { get; set; }
        public abstract class usrPaymentStatus : IBqlField { }
        #endregion

        #region UsrDaysPastDue
        [PXDouble(0)]
        [PXUIField(DisplayName = "Days Past Due")]
        public virtual int? UsrDaysPastDue { get; set; }
        public abstract class usrDaysPastDue : IBqlField { }
        #endregion

        #region UsrTotPeriods
        public abstract class usrTotPeriods : PX.Data.IBqlField
        {
        }
        [PXDBShort()]
        [PXUIField(DisplayName = "Billing Frequency")]
        //[PXDefault((Int16)MaxQ.Products.RBRR.ContractMaintContractInterval_List.Years_Key)]
        [PXIntList(
            new int[]
             {
                ContractMaintContractInterval_List.Years_Key,
                ContractMaintContractInterval_List.BiMonthly_key,
                ContractMaintContractInterval_List.Every18Months_Key,
                ContractMaintContractInterval_List.Biennially_Key,
                ContractMaintContractInterval_List.ThreeYears_Key,
                ContractMaintContractInterval_List.FiveYears_Key,
                ContractMaintContractInterval_List.SixYears_Key,
                ContractMaintContractInterval_List.SevenYears_Key,
                ContractMaintContractInterval_List.Months_Key,
                ContractMaintContractInterval_List.Quarters_Key,
                ContractMaintContractInterval_List.SemiAnnually_Key,
                ContractMaintContractInterval_List.UserDefined_Key,
                ContractMaintContractInterval_List.FourMonths_Key,
                ContractMaintContractInterval_List.BiWeekly_Key,
                ContractMaintContractInterval_List.Days_Key,
                ContractMaintContractInterval_List.Weeks_Key
             },
            new string[]
             {
                "Annually",
                "Bimonthly",
                "Every One and Half Years",
                "Every Two Years",
                "Every Three Years",
                "Every Five Years",
                "Every Six Years",
                "Every Seven Years",
                "Monthly",
                "Quarterly",
                "Semi-Annually",
                "UserDefined",
                "Every Four Months",
                "Biweekly",
                "Daily",
                "Weekly"
             })]
        public virtual short? UsrTotPeriods { get; set; }
        #endregion
    }
}
