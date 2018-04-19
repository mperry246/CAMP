using System;

using PX.Data;

namespace PX.Objects.CM.Standalone
{
	/// <summary>
	/// An alias DAC for <see cref="CurrencyInfo"/> that can e.g. be used
	/// to join <see cref="CurrencyInfo"/> twice in BQL queries.
	/// </summary>
	[Serializable]
	[PXHidden]
	public class CurrencyInfoAlias : CurrencyInfo
	{
		public new abstract class curyInfoID : IBqlField { }
		public new abstract class baseCalc : IBqlField { }
		public new abstract class baseCuryID : IBqlField { }
		public new abstract class curyID : IBqlField { }
		public new abstract class displayCuryID : IBqlField { }
		public new abstract class curyRateTypeID : IBqlField { }
		public new abstract class curyEffDate : IBqlField { }
		public new abstract class curyMultDiv : IBqlField { }
		public new abstract class curyRate : IBqlField { }
		public new abstract class recipRate : IBqlField { }
		public new abstract class sampleCuryRate : IBqlField { }
		public new abstract class sampleRecipRate : IBqlField { }
		public new abstract class curyPrecision : IBqlField { }
		public new abstract class basePrecision : IBqlField { }
		public new abstract class Tstamp : IBqlField { }
	}
}
