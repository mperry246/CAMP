using System;
using PX.Data;

namespace CAMPCustomization
{
    public class CAMPLineofBusinessMaint : PXGraph<CAMPLineofBusinessMaint, CAMPLineofBusiness>
    {

        //public PXCancel<CAMPLineofBusiness> Cancel;
        //public PXSave<CAMPLineofBusiness> Save;

        [PXImport(typeof(CAMPLineofBusiness))]
        [PXFilterable]
        public PXSelect<CAMPLineofBusiness> LineofBusinessRecords;

        public PXSelect<CAMPLineofBusiness, Where<CAMPLineofBusiness.lineofBusinessID
                                , Equal<Current<CAMPLineofBusiness.lineofBusinessID>>>> CurrentLOB;
    }
}