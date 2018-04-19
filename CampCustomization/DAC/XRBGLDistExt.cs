using CAMPCustomization;
using PX.Data;
using System;

namespace MaxQ.Products.RBRR
{
    public class XRBGLDistExt : PXCacheExtension<XRBGLDist>
    {
        #region UsrProfileNumber
        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "ProfileSerial Nbr")]
        [PXSelector(typeof(Search<CAMPProfileNumber.profileNumberID>),
         new Type[]{
         typeof(CAMPProfileNumber.profileNumberCD),
         typeof(CAMPProfileNumber.description),
         typeof(CAMPProfileNumber.serialNbr),
         typeof(CAMPProfileNumber.registrationNbr)},
                SubstituteKey = typeof(CAMPProfileNumber.profileNumberCD),
                DescriptionField = typeof(CAMPProfileNumber.serialNbr))]
        public virtual int? UsrProfileNumber { get; set; }
        public abstract class usrProfileNumber : IBqlField { }
        #endregion

        #region UsrDiscountReasonID

        [PXDBInt]
        [PXUIField(DisplayName = "Discount Reason", Enabled = false)]
        [PXSelector(typeof(CAMPDiscountReasonCode.discountReasonID),
             SubstituteKey = typeof(CAMPDiscountReasonCode.discountReasonCD),
             DescriptionField = typeof(CAMPDiscountReasonCode.description))]
        public virtual int? UsrDiscountReasonID { get; set; }
        public abstract class usrDiscountReasonID : IBqlField { }
        #endregion

        #region UsrRegNbr
        [PXDBString(100)]
        [PXUIField(DisplayName = "EHMRegNbr")]
        public virtual string UsrRegNbr { get; set; }
        public abstract class usrRegNbr : IBqlField { }
        #endregion

        #region UsrManufacturerID
        [PXDBInt()]
        [PXUIField(DisplayName = "Manufacturer", IsReadOnly = true)]
        [PXDefault(typeof(Search<CAMPProfileNumber.manufacturerID, Where<CAMPProfileNumber.profileNumberID, Equal<Current<XRBGLDistExt.usrProfileNumber>>>>),
         PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<XRBGLDistExt.usrProfileNumber>))]
        [PXSelector(typeof(Search<CAMPManufacturer.manufacturerID>),
                new Type[]{
                typeof(CAMPManufacturer.manufacturerCD),
                typeof(CAMPManufacturer.description)},
                SubstituteKey = typeof(CAMPManufacturer.manufacturerCD),
                DescriptionField = typeof(CAMPManufacturer.description))]
        public virtual int? UsrManufacturerID { get; set; }
        public abstract class usrManufacturerID : IBqlField { }
        #endregion

        #region UsrPRegNbr
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Reg Nbr", IsReadOnly = true)]
        [PXDefault(typeof(Search<CAMPProfileNumber.registrationNbr, Where<CAMPProfileNumber.profileNumberID, Equal<Current<XRBGLDistExt.usrProfileNumber>>>>),
               PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<XRBGLDistExt.usrProfileNumber>))]
        public virtual string UsrPRegNbr { get; set; }
        public abstract class usrPRegNbr : IBqlField { }
        #endregion

        #region UsrSerialNbr
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Serial Nbr", IsReadOnly = true)]
        [PXDefault(typeof(Search<CAMPProfileNumber.serialNbr, Where<CAMPProfileNumber.profileNumberID, Equal<Current<XRBGLDistExt.usrProfileNumber>>>>),
               PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<XRBGLDistExt.usrProfileNumber>))]
        public virtual string UsrSerialNbr { get; set; }
        public abstract class usrSerialNbr : IBqlField { }
        #endregion

        #region UsrTapeModelID
        [PXDBInt]
        [PXUIField(DisplayName = "Model", IsReadOnly = true)]
        [PXDefault(typeof(Search<CAMPProfileNumber.tapeModelID, Where<CAMPProfileNumber.profileNumberID, Equal<Current<XRBGLDistExt.usrProfileNumber>>>>),
               PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<XRBGLDistExt.usrProfileNumber>))]
        [PXSelector(typeof(Search<CAMPTapeModel.tapeModelID>),
                new Type[]{
                typeof(CAMPTapeModel.tapeModelCD),
                typeof(CAMPTapeModel.description)},
                SubstituteKey = typeof(CAMPTapeModel.tapeModelCD),
                DescriptionField = typeof(CAMPTapeModel.description))]
        public virtual int? UsrTapeModelID { get; set; }
        public abstract class usrTapeModelID : IBqlField { }
        #endregion
    }
}