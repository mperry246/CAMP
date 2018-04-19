using System.Collections.Generic;

namespace PX.Objects.AR
{
	public static class ARRegisterClassExtensions
	{
		/// <summary>
		/// Returns an enumerable string array, which is included all 
		/// possible original document types for voiding document.
		/// </summary>
		public static IEnumerable<string> PossibleOriginalDocumentTypes(this ARRegister voidpayment)
		{
			switch (voidpayment.DocType)
			{
				case ARDocType.CashReturn:
					return new[] { ARDocType.CashSale };

				case ARDocType.VoidPayment:
					return new[] { ARDocType.Payment, ARDocType.Prepayment };

				default:
					return new[] { voidpayment.DocType };
			}
		}
	}
}
