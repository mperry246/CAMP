using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects;
using System.Collections.Generic;
using System;

namespace PX.Objects.AR
{
			[PXNonInstantiatedExtension]
	public class AR_ARSalesPerTran_ExistingColumn : PXCacheExtension<PX.Objects.AR.ARSalesPerTran>
	{
			#region CuryCommnAmt	
			[PXDBCurrency(typeof(ARSalesPerTran.curyInfoID), typeof(ARSalesPerTran.commnAmt))]
    [PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Commission Amt.",Enabled = true)]
			public Decimal? CuryCommnAmt { get; set; }
			#endregion

			#region CuryCommnblAmt	
			[PXDBCurrency(typeof(ARSalesPerTran.curyInfoID), typeof(ARSalesPerTran.commnblAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Commissionable Amount", Enabled = true)]
			public Decimal? CuryCommnblAmt { get; set; }
			#endregion
	}
}