using System;
using System.Collections.Generic;
using System.Text;

using PX.Data;

namespace PX.Objects.AR.BQL
{
	/// <summary>
	/// A predicate which returns <c>true</c> whenever the value of its operand field
	/// does not correspond to a self-applying document, which has no balance, e.g. a 
	/// <see cref="ARDocType.cashSale">Cash Sale</see> or a 
	/// <see cref="ARDocType.cashReturn">Cash Return</see>.
	/// </summary>
	public class IsNotSelfApplying<TDocTypeField> : IBqlUnary
		where TDocTypeField : IBqlOperand
	{
		private IBqlCreator _where = new Where<
			TDocTypeField, NotEqual<ARDocType.cashSale>,
			And<TDocTypeField, NotEqual<ARDocType.cashReturn>>>();

		public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, BqlCommand.Selection selection)
			=> _where.Parse(graph, pars, tables, fields, sortColumns, text, selection);

		public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
			=> _where.Verify(cache, item, pars, ref result, ref value);
	}
}
