using PX.Data;
using System.Collections.Generic;
using PX.Objects.AR.CCPaymentProcessing.Common;
using V1 = PX.CCProcessingBase;
using V2 = PX.CCProcessingBase.Interfaces.V2;
using System.Web;

namespace PX.Objects.AR.CCPaymentProcessing.Wrappers
{
	public class BaseProfileProcessingWrapper
	{
		public static IBaseProfileProcessingWrapper GetBaseProfileProcessingWrapper(object pluginObject, CCProcessingContext context)
		{
			if (HttpContext.Current?.Request?.Url.Scheme != null && HttpContext.Current.Request.Url.Scheme != System.Uri.UriSchemeHttps)
			{
				throw new PXException(CCProcessingBase.Messages.MustUseHttps);
			}
			if (pluginObject is V1.ICCPaymentProcessing)
			{
				return new V1BaseProfileProcessor((V1.ICCPaymentProcessing) pluginObject,
					Repositories.CardProcessingReadersProvider.GetCardProcessingProvider(context));
			}
			if (pluginObject is V2.ICCProcessingPlugin)
			{
				return new V2BaseProfileProcessor((V2.ICCProcessingPlugin)pluginObject,
					Repositories.CardProcessingReadersProvider.GetCardProcessingProvider(context));
			}
			throw new PXException(V1.Messages.UnknownPluginType, pluginObject.GetType().Name);
		}

		private class V1BaseProfileProcessor : V1ProcessorBase, IBaseProfileProcessingWrapper
		{
			private V1.ICCTokenizedPaymentProcessing _processor;
			public V1BaseProfileProcessor(V1.ICCPaymentProcessing v1Plugin, Repositories.ICardProcessingReadersProvider provider) : base(v1Plugin, provider)
			{
				_processor = GetProcessor();
			}
			
			private V1.ICCTokenizedPaymentProcessing GetProcessor()
			{
				_plugin.Initialize(
					_provider.GetProcessingCenterSettingsStorage(),
					_provider.GetCardDataReader(),
					_provider.GetCustomerDataReader());
				V1.ICCTokenizedPaymentProcessing profileProcessor = _plugin as V1.ICCTokenizedPaymentProcessing;
				if (profileProcessor == null)
				{
					string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA(
						Messages.FeatureNotSupportedByProcessing,
						CCProcessingFeature.ProfileManagement);
					throw new PXException(errorMessage);
				}
				return profileProcessor;
			}

			public string CreateCustomerProfile()
			{
				V1.APIResponse response = new V1.APIResponse();
				string customerId;
				_processor.CreateCustomer(response, out customerId);
				ProcessAPIResponse(response);
				return customerId;
			}

			public string CreatePaymentProfile()
			{ 
				V1.APIResponse response = new V1.APIResponse();
				string profileId;
				_processor.CreatePMI(response, out profileId);
				ProcessAPIResponse(response);
				return profileId;
			}

			public void DeleteCustomerProfile()
			{
				V1.APIResponse response = new V1.APIResponse();
				_processor.DeleteCustomer(response);
				ProcessAPIResponse(response);
			}

			public void DeletePaymentProfile()
			{
				V1.APIResponse response = new V1.APIResponse();
				_processor.DeletePMI(response);
				ProcessAPIResponse(response);
			}

			public V2.CreditCardData GetPaymentProfile()
			{
				V1.APIResponse response = new V1.APIResponse();
				V1.SyncPMResponse syncResponse = new V1.SyncPMResponse();
				_processor.GetPMI(response, syncResponse);
				ProcessAPIResponse(response);
				List<V2.CreditCardData> cardList = GetCardData(syncResponse);
				if (cardList.Count != 1)
				{
					throw new PXException(V1.Messages.UnexpectedResult, _plugin.GetType().Name);
				}
				return cardList[0];
			}
		}

		private class V2BaseProfileProcessor : IBaseProfileProcessingWrapper
		{
			private V2.ICCProcessingPlugin _plugin;
			private Repositories.ICardProcessingReadersProvider _provider;
			private V2.ICCProfileProcessor _processor;

			public V2BaseProfileProcessor(V2.ICCProcessingPlugin v2Plugin, Repositories.ICardProcessingReadersProvider provider)
			{
				_plugin = v2Plugin;
				_provider = provider;
				_processor = GetProcessor();
			}

			private V2.ICCProfileProcessor GetProcessor()
			{
				V2SettingsGenerator settingsGen = new V2SettingsGenerator(_provider);
				V2.ICCProfileProcessor processor = _plugin.CreateProcessor<V2.ICCProfileProcessor>(settingsGen.GetSettings());
				if (processor == null)
				{
					string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA(
						Messages.FeatureNotSupportedByProcessing,
						CCProcessingFeature.ProfileManagement);
					throw new PXException(errorMessage);
				}
				return processor;
			}
			
			public string CreateCustomerProfile()
			{
				V2.CustomerData customerData = V2ProcessingInputGenerator.GetCustomerData(_provider.GetCustomerDataReader());
				string result = V2PluginErrorHandler.ExecuteAndHandleError(() => _processor.CreateCustomerProfile(customerData));
				return result;
			}

			public string CreatePaymentProfile()
			{
				string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(_provider.GetCustomerDataReader()).CustomerProfileID;
				V2.CreditCardData cardData = V2ProcessingInputGenerator.GetCardData(_provider.GetCardDataReader(), _provider.GetExpirationDateConverter());
				V2.AddressData addressData = V2ProcessingInputGenerator.GetAddressData(_provider.GetCustomerDataReader());
				cardData.AddressData = addressData;
				string result = V2PluginErrorHandler.ExecuteAndHandleError(() => _processor.CreatePaymentProfile(customerProfileId, cardData));
				return result;
			}

			public void DeleteCustomerProfile()
			{
				string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(_provider.GetCustomerDataReader()).CustomerProfileID;
				V2PluginErrorHandler.ExecuteAndHandleError(() => _processor.DeleteCustomerProfile(customerProfileId));
			}

			public void DeletePaymentProfile()
			{
				string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(_provider.GetCustomerDataReader()).CustomerProfileID;
				string paymentProfileId = V2ProcessingInputGenerator.GetCardData(_provider.GetCardDataReader()).PaymentProfileID;
				V2PluginErrorHandler.ExecuteAndHandleError(() => _processor.DeletePaymentProfile(customerProfileId, paymentProfileId));
			}

			public V2.CreditCardData GetPaymentProfile()
			{
				string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(_provider.GetCustomerDataReader()).CustomerProfileID;
				string paymentProfileId = V2ProcessingInputGenerator.GetCardData(_provider.GetCardDataReader()).PaymentProfileID;
				V2.CreditCardData result = V2PluginErrorHandler.ExecuteAndHandleError(() => _processor.GetPaymentProfile(customerProfileId, paymentProfileId));
				return result;
			}
		}
	}
}
