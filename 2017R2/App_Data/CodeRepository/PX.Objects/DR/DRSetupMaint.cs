using PX.Data;

namespace PX.Objects.DR
{
	public class DRSetupMaint: PXGraph<DRSetupMaint>
	{
		public PXSelect<DRSetup> DRSetupRecord;
		public PXSave<DRSetup> Save;
		public PXCancel<DRSetup> Cancel;
	}
}
