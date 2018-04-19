using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;
using System.Collections;

namespace PX.Objects.PM
{
	[GL.TableDashboardType]
	[Serializable]
	public class CostCodeMaint : PXGraph<CostCodeMaint>, PXImportAttribute.IPXPrepareItems
	{
		[PXImport(typeof(PMCostCode))]
		[PXViewName(Messages.CostCode)]
		[PXFilterable]
		public PXSelect<PMCostCode> Items;
		public PXSavePerRow<PMCostCode> Save;
		public PXCancel<PMCostCode> Cancel;

		public PXSetup<PMSetup> Setup;

		protected virtual void _(Events.RowDeleting<PMCostCode> e)
		{
			if ( e.Row.IsDefault == true )
			{
				throw new PXException("This is a system record and cannot be deleted.");
			}
		}

		#region PMImport Implementation
		public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
		{
			return true;
		}

		public bool RowImporting(string viewName, object row)
		{
			return row == null;
		}

		public bool RowImported(string viewName, object row, object oldRow)
		{
			return oldRow == null;
		}

		public void PrepareItems(string viewName, IEnumerable items) { }
		#endregion
	}
}
