using System;
using System.Collections;
using System.Collections.Generic;
using PX.SM;
using PX.Data;


namespace NV.Lockbox
{
    public class NVLockboxSetupMaint : PXGraph<NVLockboxSetupMaint>
    {
        public PXSave<NVLockboxSetup> Save;
        public PXCancel<NVLockboxSetup> Cancel;

        public PXSelect<NVLockboxSetup> Records;

    }
}