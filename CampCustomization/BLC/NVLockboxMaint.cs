using System;
using System.Collections;
using System.Collections.Generic;
using PX.SM;
using PX.Data;


namespace NV.Lockbox
{
    public class NVLockboxMaint : PXGraph<NVLockboxMaint>
    {
        public NVLockboxMaint()
        {
            NVLockboxSetup setup = Setup.Current;
        }

        public PXSetup<NVLockboxSetup> Setup;

        public PXCancel<NVLockbox> Cancel;
        public PXSave<NVLockbox> Save;

        [PXImport(typeof(NVLockbox))]
        public PXSelect<NVLockbox> NVLockboxRecords;

    }
}