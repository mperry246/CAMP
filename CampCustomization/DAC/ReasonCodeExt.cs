using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects;
using System.Collections.Generic;
using System;

namespace PX.Objects.CS
{
    public class ReasonCodeExt : PXCacheExtension<PX.Objects.CS.ReasonCode>
    {
        #region UsrActive
        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Active", Visibility = PXUIVisibility.Visible)]
        public virtual bool? UsrActive { get; set; }
        public abstract class usrActive : IBqlField { }
        #endregion
    }
}
