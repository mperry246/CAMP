using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Objects.GL;

namespace PX.Objects.Common.Extensions
{
	public static class BranchClassExtensions
	{
		public static bool IsParentOrSeparate(this Branch branch) => branch.ParentBranchID == branch.BranchID;
	}
}
