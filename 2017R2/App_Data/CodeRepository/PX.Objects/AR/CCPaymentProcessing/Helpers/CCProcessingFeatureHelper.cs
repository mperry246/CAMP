using System;
using System.Web.Compilation;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.CA;
using V1 = PX.CCProcessingBase;
using V2 = PX.CCProcessingBase.Interfaces.V2;
using PX.Data;

namespace PX.Objects.AR.CCPaymentProcessing.Helpers
{
	public class CCProcessingFeatureHelper
	{
		public static bool IsFeatureSupported(Type pluginType, CCProcessingFeature feature)
		{
			bool result = false;
			if (typeof(V1.ICCPaymentProcessing).IsAssignableFrom(pluginType))
			{
				switch (feature)
				{
					case CCProcessingFeature.ProfileManagement:
						result = typeof(V1.ICCTokenizedPaymentProcessing).IsAssignableFrom(pluginType);
						break;
					case CCProcessingFeature.HostedForm:
						result = typeof(V1.ICCProcessingHostedForm).IsAssignableFrom(pluginType);
						break;
					case CCProcessingFeature.ExtendedProfileManagement:
						result = false;
						break;
					default:
						result = false;
						break;
				}
			}
			else if (typeof(V2.ICCProcessingPlugin).IsAssignableFrom(pluginType))
			{
				V2.ICCProcessingPlugin plugin = (V2.ICCProcessingPlugin)Activator.CreateInstance(pluginType);
				object processorInstnace = null;
				try
				{
					switch (feature)
					{
						case CCProcessingFeature.ProfileManagement:
							processorInstnace = plugin.CreateProcessor<V2.ICCProfileProcessor>(null);
							break;
						case CCProcessingFeature.HostedForm:
							processorInstnace = plugin.CreateProcessor<V2.ICCHostedFormProcessor>(null);
							break;
						case CCProcessingFeature.ExtendedProfileManagement:
							processorInstnace = plugin.CreateProcessor<V2.ICCProfileProcessor>(null);
							break;
						default:
							break;
					}
					result = processorInstnace != null;
				}
				catch
				{
					//ignoring any initialization errors!
					result = true;
				}
			}
			return result;
		}

		public static bool IsFeatureSupported(CCProcessingCenter ccProcessingCenter, CCProcessingFeature feature)
		{
			return ccProcessingCenter != null && !string.IsNullOrEmpty(ccProcessingCenter.ProcessingTypeName) && IsFeatureSupported(PXBuildManager.GetType(ccProcessingCenter.ProcessingTypeName, true), feature);
		}
		public static void CheckProcessing(CCProcessingCenter processingCenter, CCProcessingFeature feature, CCProcessingContext newContext)
		{
			CheckProcessingCenter(processingCenter);
			newContext.processingCenter = processingCenter;
			if (feature != CCProcessingFeature.Base && !CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, feature))
			{
				throw new PXException(Messages.FeatureNotSupportedByProcessing, feature.ToString());
			}
		}

		private static void CheckProcessingCenter(CCProcessingCenter processingCenter)
		{
			if (processingCenter == null)
			{
				throw new PXException(Messages.ERR_CCProcessingCenterNotFound);
			}
			if (processingCenter.IsActive != true)
			{
				throw new PXException(Messages.ERR_CCProcessingCenterIsNotActive);
			}
			if (string.IsNullOrEmpty(processingCenter.ProcessingTypeName))
			{
				throw new PXException(Messages.ERR_ProcessingCenterForCardNotConfigured);
			}
		}

	}
}