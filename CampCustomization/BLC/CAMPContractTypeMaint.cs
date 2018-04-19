using System;
using PX.Data;

namespace CAMPCustomization
{
    public class CAMPContractTypeMaint : PXGraph<CAMPContractTypeMaint>
    {
        #region Event Handlers
        public PXCancel<CAMPContractType> Cancel;
        public PXSave<CAMPContractType> Save;

        [PXImport(typeof(CAMPContractType))]
        [PXFilterable]
        public PXSelect<CAMPContractType> ContractTypeRecords;


        protected void CAMPContractType_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
        {
            var row = (CAMPContractType)e.Row;
            if (row != null)
            {
                if ((bool)row.UpdateContractType && row.NextContractTypeID == null)
                {
                    throw new PXSetPropertyException<CAMPContractType.nextContractTypeID>(
                                "Enter the next Contract Type.", PXErrorLevel.Error);
                }
            }
        }
        #endregion
    }
}