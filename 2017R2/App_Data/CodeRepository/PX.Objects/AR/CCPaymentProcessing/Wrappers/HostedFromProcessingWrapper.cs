using PX.Data;
using System;
using System.Collections.Generic;
using System.Text;
using V1 = PX.CCProcessingBase;
using V2 = PX.CCProcessingBase.Interfaces.V2;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using System.Linq;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System.Web;

namespace PX.Objects.AR.CCPaymentProcessing.Wrappers
{
	class HostedFromProcessingWrapper
	{
		public static IHostedFromProcessingWrapper GetBaseProfileProcessingWrapper(object pluginObject, CCProcessingContext context)
		{
			if (pluginObject is V1.ICCPaymentProcessing)
			{
				return new V1HostedFormProcessor((V1.ICCPaymentProcessing)pluginObject,
					Repositories.CardProcessingReadersProvider.GetCardProcessingProvider(context));
			}
			if (pluginObject is V2.ICCProcessingPlugin)
			{
				return new V2HostedFormProcessor((V2.ICCProcessingPlugin)pluginObject,
					Repositories.CardProcessingReadersProvider.GetCardProcessingProvider(context));
			}
			throw new PXException(V1.Messages.UnknownPluginType, pluginObject.GetType().Name);
		}

		private static IEnumerable<V2.CreditCardData> GetExistingProfiles(ICardProcessingReadersProvider _provider)
		{
			return _provider
				.GetCustomerCardsDataReaders()
				.Select(reader => V2ProcessingInputGenerator.GetCardData(reader))
				//we can get a card that is just being created
				.Where(card => card.PaymentProfileID != null);
		}

		private class V1HostedFormProcessor : V1ProcessorBase, IHostedFromProcessingWrapper
		{
			public V1HostedFormProcessor(V1.ICCPaymentProcessing v1Plugin, ICardProcessingReadersProvider provider) : base(v1Plugin, provider)
			{
			}

			private V1.ICCProcessingHostedForm GetProcessor()
			{
				if (HttpContext.Current.Request.UrlReferrer != null && HttpContext.Current.Request.UrlReferrer.Scheme != System.Uri.UriSchemeHttps)
				{
					throw new PXException(CCProcessingBase.Messages.MustUseHttps);
				}
				_plugin.Initialize(
					_provider.GetProcessingCenterSettingsStorage(),
					_provider.GetCardDataReader(),
					_provider.GetCustomerDataReader());
				V1.ICCProcessingHostedForm profileProcessor = _plugin as V1.ICCProcessingHostedForm;
				if (profileProcessor == null)
				{
					string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA(
						Messages.FeatureNotSupportedByProcessing,
						CCProcessingFeature.HostedForm);
					throw new PXException(errorMessage);
				}
				return profileProcessor;
			}

			


			public void GetCreateForm()
			{
				V1.ICCProcessingHostedForm processor = GetProcessor();
				V1.APIResponse response = new V1.APIResponse();
				processor.CreatePaymentMethodHostedForm(response, processor.GetCallbackURL());
			}

			public void GetManageForm()
			{
				V1.ICCProcessingHostedForm processor = GetProcessor();
				V1.APIResponse response = new V1.APIResponse();
				processor.ManagePaymentMethodHostedForm(response, processor.GetCallbackURL());
			}

			public IEnumerable<V2.CreditCardData> GetMissingPaymentProfiles()
			{
				V1.ICCProcessingHostedForm processor = GetProcessor();
				V1.APIResponse response = new V1.APIResponse();
				V1.SyncPMResponse syncResponse = new V1.SyncPMResponse();
				processor.SynchronizePaymentMethods(response, syncResponse);
				List<V2.CreditCardData> cardList = GetCardData(syncResponse);
				IEnumerable<V2.CreditCardData> missingProfiles = cardList.Except(GetExistingProfiles(_provider), new V2.InterfaceExtensions.CreditCardDataEqualityComparer());
				return missingProfiles;
			}
		}

		private class V2HostedFormProcessor : IHostedFromProcessingWrapper
		{
			private readonly V2.ICCProcessingPlugin _plugin;
			private readonly ICardProcessingReadersProvider _provider;

			public V2HostedFormProcessor(V2.ICCProcessingPlugin v2Plugin, ICardProcessingReadersProvider provider)
			{
				_plugin = v2Plugin;
				_provider = provider;
			}

			private V2.ICCHostedFormProcessor GetProcessor()
			{
				if (HttpContext.Current.Request.UrlReferrer != null && HttpContext.Current.Request.UrlReferrer.Scheme != System.Uri.UriSchemeHttps)
				{
					throw new PXException(CCProcessingBase.Messages.MustUseHttps);
				}
				V2SettingsGenerator seetingsGen = new V2SettingsGenerator(_provider);
				V2.ICCHostedFormProcessor processor = _plugin.CreateProcessor<V2.ICCHostedFormProcessor>(seetingsGen.GetSettings());
				if (processor == null)
				{
					string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA(
						Messages.FeatureNotSupportedByProcessing,
						CCProcessingFeature.HostedForm);
					throw new PXException(errorMessage);
				}
				return processor;
			}

			private V2.ICCProfileProcessor GetProfileProcessor()
			{
				V2SettingsGenerator seetingsGen = new V2SettingsGenerator(_provider);
				V2.ICCProfileProcessor processor = _plugin.CreateProcessor<V2.ICCProfileProcessor>(seetingsGen.GetSettings());
				if (processor == null)
				{
					string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA(
						Messages.FeatureNotSupportedByProcessing,
						CCProcessingFeature.ProfileManagement);
					throw new PXException(errorMessage);
				}
				return processor;
			}

			public void GetCreateForm()
			{
				var processor = GetProcessor();
				V2.CustomerData customerData = V2ProcessingInputGenerator.GetCustomerData(_provider.GetCustomerDataReader());
				var result = V2PluginErrorHandler.ExecuteAndHandleError(() => processor.GetDataForCreateForm(customerData));
				throw new PXPaymentRedirectException(result.Caption, result.Url, result.Token, result.Parameters);
			}

			public void GetManageForm()
			{
				var processor = GetProcessor();
				V2.CustomerData customerData = V2ProcessingInputGenerator.GetCustomerData(_provider.GetCustomerDataReader());
				V2.CreditCardData cardData = V2ProcessingInputGenerator.GetCardData(_provider.GetCardDataReader());
				var result = V2PluginErrorHandler.ExecuteAndHandleError(() => processor.GetDataForManageForm(customerData, cardData));
				throw new PXPaymentRedirectException(result.Caption, result.Url, result.Token, result.Parameters);
			}

			public IEnumerable<V2.CreditCardData> GetMissingPaymentProfiles()
			{
				var processor = GetProfileProcessor();
				string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(_provider.GetCustomerDataReader()).CustomerProfileID;
				var result = V2PluginErrorHandler
								.ExecuteAndHandleError(
									() => V2.InterfaceExtensions
											.GetMissingPaymentProfiles(processor, customerProfileId, GetExistingProfiles(_provider)));
				return result;
			}
		}
	}
}
