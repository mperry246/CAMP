using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.CS;

namespace PX.Objects.DR
{
	[SerializableAttribute]
	[PXPrimaryGraph(typeof(DRSetupMaint))]
	[PXCacheName(Messages.DRSetup)]
	public class DRSetup: IBqlTable
	{
		#region ScheduleNumberingID
		public abstract class scheduleNumberingID : PX.Data.IBqlField {}

		/// <summary>
		/// The identifier of the <see cref="Numbering">Numbering Sequence</see> used for the Deferred Revenue.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="Numbering.NumberingID"/> field.
		/// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("DRSCHEDULE")]
		[PXUIField(DisplayName = "Deferral Schedule Numbering Sequence")]
		[PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
		public virtual String ScheduleNumberingID { get; set; }
		#endregion
	}
}
