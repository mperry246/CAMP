using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.GL;

namespace PX.Objects.PM
{
	public class PMTaxAttribute : TaxAttribute
	{
		public PMTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
			: base(ParentType, TaxType, TaxSumType)
		{
			this.CuryLineTotal = typeof(PMProformaLine.curyLineTotal);
			this.CuryTranAmt = typeof(PMProformaLine.curyLineTotal);
			this.CuryDocBal = typeof(PMProforma.curyDocTotal);
			this.CuryTaxTotal = typeof(PMProforma.curyTaxTotal);
			this.DocDate = typeof(PMProforma.invoiceDate);
		}

		protected override List<object> SelectTaxes<Where>(PXGraph graph, object row, PXTaxCheck taxchk, params object[] parameters)
		{
			List<object> ret = new List<object>();
			switch (taxchk)
			{
				case PXTaxCheck.Line:
					int? linenbr = int.MinValue;

					if (row != null && row.GetType() == typeof(PMProformaProgressLine))
					{
						linenbr = (int?)graph.Caches[typeof(PMProformaProgressLine)].GetValue<PMProformaProgressLine.lineNbr>(row);
					}

					if (row != null && row.GetType() == typeof(PMProformaTransactLine))
					{
						linenbr = (int?)graph.Caches[typeof(PMProformaTransactLine)].GetValue<PMProformaTransactLine.lineNbr>(row);
					}

					foreach (PMTax record in PXSelect<PMTax, Where<PMTax.refNbr, Equal<Current<PMProforma.refNbr>>>>.SelectMultiBound(graph, new object[] { row }))
					{
						if (record.LineNbr == linenbr)
						{
							AppendTail<PMTax, Where>(graph, ret, record, row, parameters);
						}
					}
					return ret;
				case PXTaxCheck.RecalcLine:
					foreach (PMTax record in PXSelect<PMTax, Where<PMTax.refNbr, Equal<Current<PMProforma.refNbr>>>>.SelectMultiBound(graph, new object[] { row }))
					{
						AppendTail<PMTax, Where>(graph, ret, record, row, parameters);
					}
					return ret;
				case PXTaxCheck.RecalcTotals:
					foreach (PMTaxTran record in PXSelect<PMTaxTran,
						Where<PMTaxTran.refNbr, Equal<Current<PMProforma.refNbr>>>>.SelectMultiBound(graph, new object[] { row }))
					{
						AppendTail<PMTaxTran, Where>(graph, ret, record, row, parameters);
					}
					return ret;
				default:
					return ret;
			}
		}

		protected virtual void AppendTail<T, W>(PXGraph graph, List<object> ret, T record, object row, params object[] parameters) where T : class, ITaxDetail, PX.Data.IBqlTable, new()
			where W : IBqlWhere, new()
		{
			PXSelectBase<Tax> select = new PXSelectReadonly2<Tax,
				LeftJoin<TaxRev, On<TaxRev.taxID, Equal<Tax.taxID>,
				And<TaxRev.outdated, Equal<False>,
				And<TaxRev.taxType, Equal<TaxType.sales>,
				And<Tax.taxType, NotEqual<CSTaxType.withholding>,
				And<Tax.taxType, NotEqual<CSTaxType.use>,
				And<Tax.reverseTax, Equal<False>,
				And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>,
				Where2<Where<Tax.taxID, Equal<Required<Tax.taxID>>>, And<W>>>(graph);

			List<object> newParams = new List<object>();
			newParams.Add(ParentCache(graph).GetValue(ParentRow(graph), _DocDate));
			newParams.Add(record.TaxID);

			if (parameters != null)
			{
				newParams.AddRange(parameters);
			}

			foreach (PXResult<Tax, TaxRev> line in select.View.SelectMultiBound(new object[] { row }, newParams.ToArray()))
			{
				int idx;
				for (idx = ret.Count;
					(idx > 0) &&
					String.Compare(((Tax)(PXResult<T, Tax, TaxRev>)ret[idx - 1]).TaxCalcLevel, ((Tax)line).TaxCalcLevel) > 0;
					idx--) ;
				ret.Insert(idx, new PXResult<T, Tax, TaxRev>(record, (Tax)line, (TaxRev)line));
			}
		}

		protected override decimal CalcLineTotal(PXCache sender, object row)
		{
			decimal CuryLineTotal = 0m;

			object document = PXParentAttribute.SelectParent(sender, row, _ParentType);
			object[] progressiveDetails = PXParentAttribute.SelectChildren(sender.Graph.Caches[typeof(PMProformaProgressLine)], document, _ParentType);
			object[] transactionDetails = PXParentAttribute.SelectChildren(sender.Graph.Caches[typeof(PMProformaTransactLine)], document, _ParentType);

			if (progressiveDetails != null)
			{
				foreach (object detrow in progressiveDetails)
				{
					CuryLineTotal += GetCuryTranAmt(sender.Graph.Caches[typeof(PMProformaProgressLine)], sender.Graph.Caches[typeof(PMProformaProgressLine)].ObjectsEqual(detrow, row) ? row : detrow) ?? 0m;
				}
			}
			if (transactionDetails != null)
			{
				foreach (object detrow in transactionDetails)
				{
					CuryLineTotal += GetCuryTranAmt(sender.Graph.Caches[typeof(PMProformaTransactLine)], sender.Graph.Caches[typeof(PMProformaTransactLine)].ObjectsEqual(detrow, row) ? row : detrow) ?? 0m;
				}
			}
			return CuryLineTotal;
		}
	}
}
