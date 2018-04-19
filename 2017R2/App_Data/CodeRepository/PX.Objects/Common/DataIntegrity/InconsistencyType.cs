using PX.Data;

using System.Collections.Generic;

namespace PX.Objects.Common.DataIntegrity
{
	public class InconsistencyCode : ILabelProvider
	{
		public const string UnreleasedDocumentHasGlTransactions = "UNRELDOCHASGL";
		public const string ReleasedDocumentHasNoGlTransactions = "RELDOCHASNOGL";
		public const string BatchTotalNotEqualToTransactionTotal = "BATCHTRANTOTALMISMATCH";
		public const string ReleasedDocumentHasUnreleasedApplications = "RELDOCUNRELADJUST";
		public const string UnreleasedDocumentHasReleasedApplications = "UNRELDOCRELADJUST";
		public const string DocumentTotalsWrongPrecision = "DOCTOTALSPRECISION";
		public const string DocumentNegativeBalance = "DOCNEGATIVEBALANCE";

		public IEnumerable<ValueLabelPair> ValueLabelPairs => new ValueLabelList
		{
			{ UnreleasedDocumentHasGlTransactions, Messages.DataIntegrityGLBatchExistsForUnreleasedDocument },
			{ ReleasedDocumentHasNoGlTransactions, Messages.DataIntegrityGLBatchNotExistsForReleasedDocument },
			{ BatchTotalNotEqualToTransactionTotal, Messages.DataIntegrityGLBatchSumsNotEqualGLTransSums },
			{ ReleasedDocumentHasUnreleasedApplications, Messages.DataIntegrityReleasedDocumentWithUnreleasedApplications },
			{ UnreleasedDocumentHasReleasedApplications, Messages.DataIntegrityUnreleasedDocumentWithReleasedApplications },
			{ DocumentTotalsWrongPrecision, Messages.DataIntegrityDocumentTotalsHaveLargerPrecisionThanCurrency },
			{ DocumentNegativeBalance, Messages.DataIntegrityDocumentHasNegativeBalance },
		};

		public class unreleasedDocumentHasGlTransactions : Constant<string>
		{
			public unreleasedDocumentHasGlTransactions() 
				: base(UnreleasedDocumentHasGlTransactions)
			{ }
		}

		public class releasedDocumentHasNoGlTransactions : Constant<string>
		{
			public releasedDocumentHasNoGlTransactions()
				: base(ReleasedDocumentHasNoGlTransactions)
			{ }
		}

		public class batchTotalNotEqualToTransactionTotal : Constant<string>
		{
			public batchTotalNotEqualToTransactionTotal()
				: base(BatchTotalNotEqualToTransactionTotal)
			{ }
		}

		public class releasedDocumentHasUnreleasedApplications : Constant<string>
		{
			public releasedDocumentHasUnreleasedApplications()
				: base(ReleasedDocumentHasUnreleasedApplications)
			{ }
		}

		public class unreleasedDocumentHasReleasedApplications : Constant<string>
		{
			public unreleasedDocumentHasReleasedApplications()
				: base(UnreleasedDocumentHasReleasedApplications)
			{ }
		}

		public class documentTotalsWrongPrecision : Constant<string>
		{
			public documentTotalsWrongPrecision()
				: base(DocumentTotalsWrongPrecision)
			{ }
		}

		public class documentNegativeBalance : Constant<string>
		{
			public documentNegativeBalance()
				: base(DocumentNegativeBalance)
			{ }
		}
	}
}
