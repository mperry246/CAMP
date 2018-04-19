using CAMPCustomization;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.Objects;
using PX.TM;
using System.Collections.Generic;
using System;

namespace PX.Objects.IN
{
    public class InventoryItemExt : PXCacheExtension<PX.Objects.IN.InventoryItem>
    {
        #region UsrTapeModelID
        [PXDBInt]
        [PXSelector(typeof(CAMPTapeModel.tapeModelID),
         SubstituteKey = typeof(CAMPTapeModel.tapeModelCD),
         DescriptionField = typeof(CAMPTapeModel.description))]
        [PXUIField(DisplayName = "Model")]
        public virtual int? UsrTapeModelID { get; set; }
        public abstract class usrTapeModelID : IBqlField { }
        #endregion

        #region UsrManufacturerID
        [PXDBInt]
        [PXSelector(typeof(CAMPManufacturer.manufacturerID),
          SubstituteKey = typeof(CAMPManufacturer.manufacturerCD),
          DescriptionField = typeof(CAMPManufacturer.description))]
        [PXUIField(DisplayName = "Manufacturer", IsReadOnly = true)]
        public virtual int? UsrManufacturerID { get; set; }
        public abstract class usrManufacturerID : IBqlField { }
        #endregion

        #region UsrLineofBusinessID

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXSelector(typeof(CAMPLineofBusiness.lineofBusinessID),
              SubstituteKey = typeof(CAMPLineofBusiness.lineofBusinessCD),
              DescriptionField = typeof(CAMPLineofBusiness.description))]
        [PXUIField(DisplayName = "Line of Business", Required = true)]
        public virtual int? UsrLineofBusinessID { get; set; }
        public abstract class usrLineofBusinessID : IBqlField { }
        #endregion

        #region UsrIsAircraft
        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "IsAircraft")]
        public virtual bool? UsrIsAircraft { get; set; }
        public abstract class usrIsAircraft : IBqlField { }
        #endregion
    }
}