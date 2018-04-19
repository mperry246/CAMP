using System;
using PX.Data;

namespace CAMPCustomization
{
    public class CAMPDiscountReasonCodeMaint : PXGraph<CAMPDiscountReasonCodeMaint>
    {
        #region Event Handlers
        public PXCancel<CAMPDiscountReasonCode> Cancel;
        public PXSave<CAMPDiscountReasonCode> Save;

        [PXImport(typeof(CAMPDiscountReasonCode))]
        [PXFilterable]
        public PXSelect<CAMPDiscountReasonCode> DiscountReasonCodeRecords;
        #endregion
    }
}