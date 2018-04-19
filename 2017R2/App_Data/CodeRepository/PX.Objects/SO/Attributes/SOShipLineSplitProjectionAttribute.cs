using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Objects.CS;

namespace PX.Objects.SO
{
	/// <summary>
	/// Special projection for SOShipLineSplit records.
	/// It returns both assigned and unassigned records in the scope of reports or generic inquiries,
	/// but only one type of records depending on the passed parameter in the scope of other graphs.
	/// </summary>
	public class SOShipLineSplitProjectionAttribute : PXProjectionAttribute
	{
		protected bool _isUnassignedValue;

		public SOShipLineSplitProjectionAttribute(bool isUnassignedValue)
			: base(typeof(Select<Table.SOShipLineSplit>))
		{
			_isUnassignedValue = isUnassignedValue;

			Persistent = true;
		}

		protected override Type GetSelect(PXCache sender)
		{
			if (sender.Graph.GetType() == typeof(PXGraph)	//report mode
				|| sender.Graph.GetType() == typeof(PXGenericInqGrph))
			{
				return base.GetSelect(sender);
			}
			// TODO: rewrite with BqlTemplate
			else if (_isUnassignedValue)
			{
				return typeof(Select<Table.SOShipLineSplit, Where<Table.SOShipLineSplit.isUnassigned, Equal<boolTrue>>>);
			}
			else
			{
				return typeof(Select<Table.SOShipLineSplit, Where<Table.SOShipLineSplit.isUnassigned, Equal<boolFalse>>>);
			}
		}
	}
}
