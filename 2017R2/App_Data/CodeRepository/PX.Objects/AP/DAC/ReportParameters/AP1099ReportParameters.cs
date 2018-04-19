using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.GL;

namespace PX.Objects.AP.DAC.ReportParameters
{
	public class AP1099ReportParameters: IBqlTable
	{
		#region MasterBranchID

		public abstract class masterBranchID : IBqlField{}

		[MasterBranch1099(DisplayName = "Branch")]
		public virtual int? MasterBranchID { get; set; }

		#endregion

		#region FinYear

		public abstract class finYear : PX.Data.IBqlField{}

		[PXDBString(4, IsFixed = true)]
		[PXUIField(DisplayName = "1099 Year")]
		[PXSelector(typeof(Search4<AP1099Year.finYear,
			Where<AP1099Year.branchID, Equal<Optional2<AP1099ReportParameters.masterBranchID>>,
				Or<Optional2<AP1099ReportParameters.masterBranchID>, IsNull>>,
			Aggregate<GroupBy<AP1099Year.finYear>>>))]
		public virtual String FinYear { get; set; }

		#endregion
	}
}
