using System.Collections.Generic;

namespace PX.Objects.AP
{
	public static class APRegisterClassExtensions
	{
		/// <summary>
		/// Returns an enumerable string array, which is included all 
		/// possible original document types for voiding document.
		/// </summary>
		public static IEnumerable<string> PossibleOriginalDocumentTypes(this APRegister voidcheck)
		{
			switch (voidcheck.DocType)
			{
				case APDocType.VoidQuickCheck:
					return new[] { APDocType.QuickCheck };

				case APDocType.VoidCheck:
					return new[] { APDocType.Check, APDocType.Prepayment };

				default:
					return new[] { voidcheck.DocType };
			}
		}
	}
}
