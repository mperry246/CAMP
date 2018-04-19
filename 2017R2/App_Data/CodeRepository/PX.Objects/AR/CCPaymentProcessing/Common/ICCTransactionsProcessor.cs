using PX.CCProcessingBase;

namespace PX.Objects.AR.CCPaymentProcessing.Common
{
	public interface ICCTransactionsProcessor
	{
		void ProcessCCTransaction(ICCPayment aDoc, CCProcTran refTran, CCTranType aTranType);
	}
}