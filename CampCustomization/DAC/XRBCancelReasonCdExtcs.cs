using PX.Data;

namespace MaxQ.Products.RBRR
{
    public class XRBCancelReasonCdExt : PXCacheExtension<XRBCancelReasonCd>
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