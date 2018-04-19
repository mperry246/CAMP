using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PX.Data;

namespace PX.Objects.AP
{/*
	public class IsPayToVendor<VendorID> : IBqlUnary
		where VendorID : IBqlOperand
	{
		private readonly IBqlUnary exists;

		[Serializable]
		private class SuppliedByVendorAlias : Vendor {}

		public IsPayToVendor()
		{
			exists = new Exists<Select<SuppliedByVendorAlias, Where<SuppliedByVendorAlias.payToVendorID, Equal<VendorID>>>>();
		}

		public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			exists.Verify(cache, item, pars, ref result, ref value);
		}

		public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, BqlCommand.Selection selection)
		{
			exists.Parse(graph, pars, tables, fields, sortColumns, text, selection);
		}

		public static bool Ensure(PXCache cache, Vendor vendor)
		{
			return PXSelect<Vendor, Where<IsPayToVendor<Vendor.bAccountID>>>.Selec
		}
	}*/
}
