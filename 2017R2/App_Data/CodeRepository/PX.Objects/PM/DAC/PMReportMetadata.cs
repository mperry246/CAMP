using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.IN;
using System.Collections;
using PX.Objects.GL;
using PX.Web.UI;
using PX.Objects.EP;
using PX.Objects.CM;

namespace PX.Objects.PM
{
	/// <summary>
	/// This is a virtual DAC. The fields/selector of this dac is used in reports.
	/// </summary>
	[Serializable]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMReportMetadata : IBqlTable
	{
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
		//Note: This field is used by Reports (Selector in parameters).
		[PXDefault]
		[Project(typeof(Where<PMProject.isTemplate, NotEqual<True>, And<PMProject.nonProject, NotEqual<True>>>), WarnIfCompleted = false)]
		public virtual Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
	}
}
