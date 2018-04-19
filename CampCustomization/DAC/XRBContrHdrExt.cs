using CAMPCustomization;
using PX.Data;
using System;

namespace MaxQ.Products.RBRR
{
    public class XRBContrHdrExt : PXCacheExtension<XRBContrHdr>
    {
        #region UsrLineofBusiness
        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Line of Business")]
        [PXSelector(typeof(CAMPLineofBusiness.lineofBusinessID),
          new Type[]{
          typeof(CAMPLineofBusiness.lineofBusinessCD),
          typeof(CAMPLineofBusiness.description),
          typeof(CAMPLineofBusiness.defaultSubaccount)},
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
    }
}