using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using PX.Data;

using PX.Objects.Common;
using PX.Objects.AR.Standalone;

namespace PX.Objects.AR.BQL
{
	/// <summary>
	/// A predicate that returns <c>true</c> if and only if the payment defined
	/// by its key fields (document type and reference number) has an unreleased 
	/// void payment. This may be needed to exclude such payments from processing
	/// to prevent creating unnecessary applications, see e.g. the
	/// <see cref="ARAutoApplyPayments"/> graph.
	/// </summary>
	public class HasUnreleasedVoidPayment<TDocTypeField, TRefNbrField> : IBqlUnary
		where TDocTypeField : IBqlField
		where TRefNbrField : IBqlField
	{
		private readonly IBqlCreator exists = new Exists<Select<
			ARRegisterAlias2,
				Where<
					ARRegisterAlias2.docType, Equal<ARDocType.voidPayment>,
					And<ARRegisterAlias2.docType, NotEqual<TDocTypeField>,
					And<ARRegisterAlias2.refNbr, Equal<TRefNbrField>,
					And<ARRegisterAlias2.released, NotEqual<True>>>>>>>();

		public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, BqlCommand.Selection selection)
			=> exists.Parse(graph, pars, tables, fields, sortColumns, text, selection);

		public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			string docType = cache.GetValue<TDocTypeField>(item) as string;
			string refNbr = cache.GetValue<TRefNbrField>(item) as string;

			value = result = Select(cache.Graph, docType, refNbr) != null;
		}

		public static bool Verify(PXGraph graph, ARRegister payment)
		{
			bool? result = null;
			object value = null;

			new HasUnreleasedVoidPayment<ARRegister.docType, ARRegister.refNbr>().Verify(
				graph.Caches[payment.GetType()],
				payment,
				new List<object>(0),
				ref result,
				ref value);

			return result == true;
		}

		public static ARRegister Select(PXGraph graph, ARRegister payment)
			=> Select(graph, payment.DocType, payment.RefNbr);

		public static ARRegister Select(PXGraph graph, string docType, string refNbr)
			=> PXSelect<
				ARRegisterAlias2,
				Where<
					ARRegisterAlias2.docType, Equal<ARDocType.voidPayment>,
					And<ARRegisterAlias2.docType, NotEqual<Required<ARRegister.docType>>,
					And<ARRegisterAlias2.refNbr, Equal<Required<ARRegister.refNbr>>,
					And<ARRegisterAlias2.released, NotEqual<True>>>>>>
				.SelectWindowed(graph, 0, 1, docType, refNbr)
				.RowCast<ARRegisterAlias2>()
				.FirstOrDefault();
	}
}
