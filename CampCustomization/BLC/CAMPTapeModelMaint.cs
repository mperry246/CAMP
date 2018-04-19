using System;
using PX.Data;

namespace CAMPCustomization
{
    public class CAMPTapeModelMaint : PXGraph<CAMPTapeModelMaint>
    {
        #region Event Handlers
        public PXCancel<CAMPTapeModel> Cancel;
        public PXSave<CAMPTapeModel> Save;

        [PXImport(typeof(CAMPTapeModel))]
        [PXFilterable]
        public PXSelect<CAMPTapeModel> TapeModelRecords;
        #endregion
    }
}
