using PX.Data.EP;
using System;
using PX.Data;

namespace PX.Objects.CA
{
	[Obsolete("Will be removed in Acumatica 8.0")]
	public class namespaceAP : Constant<string>
	{
		public namespaceAP() : base("PX.Objects.AP.Reports") { ;}
	}

	public class urlReports : Constant<string>
	{
		public urlReports() : base("~/Frames/ReportLauncher%") { ;}
	}
}
