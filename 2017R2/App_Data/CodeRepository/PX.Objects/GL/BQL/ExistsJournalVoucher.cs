using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PX.Data;

using PX.Objects.CS;

namespace PX.Objects.GL.BQL
{
	/// <summary>
	/// A BQL predicate returning <c>true</c> if and only if there exists a 
	/// <see cref="GLVoucher">journal voucher</see> referencing the entity's
	/// note ID field by its <see cref="GLVoucher.RefNoteID"/> field.
	/// </summary>
	public class ExistsJournalVoucher<TNoteIDField> : IBqlUnary
		where TNoteIDField : IBqlField
	{
		private readonly IBqlCreator exists = new Exists<Select<
			GLVoucher,
			Where2<
				FeatureInstalled<FeaturesSet.gLWorkBooks>,
				And<TNoteIDField, Equal<GLVoucher.refNoteID>>>>>();

		public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, BqlCommand.Selection selection)
			=> exists.Parse(graph, pars, tables, fields, sortColumns, text, selection);
	
		public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			Guid? noteID = cache.GetValue<TNoteIDField>(item) as Guid?;

			value = result = PXSelect<
				GLVoucher,
				Where2<
					FeatureInstalled<FeaturesSet.gLWorkBooks>,
					And<GLVoucher.refNoteID, Equal<Required<GLVoucher.refNoteID>>>>>
				.SelectWindowed(cache.Graph, 0, 1, noteID)
				.RowCast<GLVoucher>()
				.Any();
		}
	}
}
