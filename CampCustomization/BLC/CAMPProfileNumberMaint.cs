using System;
using PX.Data;

namespace CAMPCustomization
{
    public class CAMPProfileNumberMaint : PXGraph<CAMPProfileNumberMaint>
    {
        #region Event Handlers
        public PXCancel<CAMPProfileNumber> Cancel;
        public PXSave<CAMPProfileNumber> Save;

        [PXImport(typeof(CAMPProfileNumber))]
        [PXFilterable]
        public PXSelect<CAMPProfileNumber> ProfileNumberRecords;

        protected virtual void CAMPProfileNumber_TapeModelID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            var row = (CAMPProfileNumber)e.Row;

            if (row != null)
            {
                PXCache hCache = ProfileNumberRecords.Cache;
                CAMPProfileNumber hdr = row;
                CAMPTapeModel model = (CAMPTapeModel)PXSelectorAttribute.Select<CAMPProfileNumber.tapeModelID>(hCache, hdr);
                if (model != null)
                {
                    row.ManufacturerID = model.ManufacturerID;
                }
            }
        }
        #endregion
    }
}