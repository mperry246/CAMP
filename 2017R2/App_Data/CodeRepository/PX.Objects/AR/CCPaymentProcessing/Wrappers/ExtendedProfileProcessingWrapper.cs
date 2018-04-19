using PX.Data;
using System;
using System.Collections.Generic;
using PX.Objects.AR.CCPaymentProcessing.Common;
using V1 = PX.CCProcessingBase;
using V2 = PX.CCProcessingBase.Interfaces.V2;
using System.Web;

namespace PX.Objects.AR.CCPaymentProcessing.Wrappers
{
	public class ExtendedProfileProcessingWrapper
	{
		public static IExtendedProfileProcessingWrapper GetExtendedProfileProcessingWrapper(object pluginObject, CCProcessingContext context)
		{
			if (HttpContext.Current?.Request?.Url.Scheme != null &&  HttpContext.Current.Request.Url.Scheme != System.Uri.UriSchemeHttps)
			{
				throw new PXException(CCProcessingBase.Messages.MustUseHttps);
			}
			if (pluginObject is V1.ICCPaymentProcessing)
			{
				string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA(
					Messages.FeatureNotSupportedByProcessing,
					CCProcessingFeature.ExtendedProfileManagement);
				throw new PXException(errorMessage);
			}
			if (pluginObject is V2.ICCProcessingPlugin)
			{
				return new V2ExtendedProfileProcessor((V2.ICCProcessingPlugin)pluginObject,
					Repositories.CardProcessingReadersProvider.GetCardProcessingProvider(context));
			}
			throw new PXException(V1.Messages.UnknownPluginType, pluginObject.GetType().Name);
		}

		private class V2ExtendedProfileProcessor : IExtendedProfileProcessingWrapper
		{
			private V2.ICCProcessingPlugin _plugin;
			private Repositories.ICardProcessingReadersProvider _provider;

			public V2ExtendedProfileProcessor(V2.ICCProcessingPlugin v2Plugin, Repositories.ICardProcessingReadersProvider provider)
			{
				_plugin = v2Plugin;
				_provider = provider;
			}

			private V2.ICCProfileProcessor GetProcessor()
			{
				V2SettingsGenerator seetingsGen = new V2SettingsGenerator(_provider);
				V2.ICCProfileProcessor processor = _plugin.CreateProcessor<V2.ICCProfileProcessor>(seetingsGen.GetSettings());
				if (processor == null)
				{
					string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA(
						Messages.FeatureNotSupportedByProcessing,
						CCProcessingFeature.ExtendedProfileManagement);
					throw new PXException(errorMessage);
				}
				return processor;
			}
			public IEnumerable<V2.CustomerData> GetAllCustomerProfiles()
			{
				throw new NotImplementedException();
			}

			public IEnumerable<V2.CreditCardData> GetAllPaymentProfiles()
			{
				V2.ICCProfileProcessor processor = GetProcessor();
				string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(_provider.GetCustomerDataReader()).CustomerProfileID;
				IEnumerable<V2.CreditCardData> result = V2PluginErrorHandler.ExecuteAndHandleError(() => processor.GetAllPaymentProfiles(customerProfileId));
				return result;
			}

			public V2.CustomerData GetCustomerProfile()
			{
				throw new NotImplementedException();
			}

			public void UpdateCustomerProfile()
			{
				throw new NotImplementedException();
			}

			public void UpdatePaymentProfile()
			{
				throw new NotImplementedException();
			}
		}
	}
}
