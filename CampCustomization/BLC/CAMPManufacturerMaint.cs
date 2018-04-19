using System;
using PX.Data;

namespace CAMPCustomization
{
    public class CAMPManufacturerMaint : PXGraph<CAMPManufacturerMaint>
    {
        #region Event Handlers
        public PXCancel<CAMPManufacturer> Cancel;
        public PXSave<CAMPManufacturer> Save;

        [PXImport(typeof(CAMPManufacturer))]
        [PXFilterable]
        public PXSelect<CAMPManufacturer> ManufacturerRecords;
        #endregion
    }
}