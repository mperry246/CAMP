using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PX.Objects.Common
{
	public class ReportFunctions
	{
		#region IN

		public object GetFullItemClassDescription(object itemClassCD)
		{
			string cd = itemClassCD as string;
			if (cd == null) return null;
			return IN.ItemClassTree.Instance.GetFullItemClassDescription(cd);
		}

		#endregion
	}
}
