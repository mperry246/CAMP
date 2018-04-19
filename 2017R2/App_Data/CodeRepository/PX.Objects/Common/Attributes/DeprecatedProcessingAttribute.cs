using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA;

namespace PX.Objects.Common.Attributes
{
	public class DeprecatedProcessingAttribute : PXEventSubscriberAttribute, IPXFieldVerifyingSubscriber
	{
		public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.Row == null)
			{
				return;
			}
			int? pmInstanceID = e.NewValue as int?;
			if (IsProcessingCenterDeprecated(sender.Graph, pmInstanceID))
			{
				sender.RaiseExceptionHandling(FieldName, e.Row, e.NewValue, new PXSetPropertyException(PX.CCProcessingBase.Messages.DeprecatedProcessingCenter, PXErrorLevel.Warning));
			}
		}

		private static bool IsProcessingCenterDeprecated(PXGraph graph, int? pmInstanceID)
		{
			const string DeprecatedProcessingCenterTypeName = "PX.CCProcessing.AuthorizeNetTokenizedProcessing";
			if (pmInstanceID == null)
			{
				return false;
			}
			CCProcessingCenter processingCenter = (CCProcessingCenter)PXSelectJoin<CCProcessingCenter, 
					InnerJoin<CustomerPaymentMethod, On<CCProcessingCenter.processingCenterID, Equal<CustomerPaymentMethod.cCProcessingCenterID>>>,
					Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>
				.Select(graph, pmInstanceID);
			return string.Equals(processingCenter?.ProcessingTypeName, DeprecatedProcessingCenterTypeName);
		}
	}
}
