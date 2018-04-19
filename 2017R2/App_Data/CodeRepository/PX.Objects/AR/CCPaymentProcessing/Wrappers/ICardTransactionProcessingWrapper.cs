using PX.Objects.AR.CCPaymentProcessing;
using System.Collections.Generic;

namespace PX.Objects.AR.CCPaymentProcessing.Wrappers
{
	public interface ICardTransactionProcessingWrapper
	{
		CCProcessingBase.ProcessingResult DoTransaction(CCProcessingBase.CCTranType aTranType, CCProcessingBase.ProcessingInput inputData);
		CCProcessingBase.CCErrors ValidateSettings(CCProcessingBase.ISettingsDetail setting);
		void TestCredentials(CCProcessingBase.APIResponse apiResponse);
		void ExportSettings(IList<CCProcessingBase.ISettingsDetail> aSettings);
	}
}
