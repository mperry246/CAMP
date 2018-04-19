using PX.Data;
using System;
using V1 = PX.CCProcessingBase;
using V2 = PX.CCProcessingBase.Interfaces.V2;
using System.Collections.Generic;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Web.UI;
using PX.Objects.AR.CCPaymentProcessing.Repositories;

namespace PX.Objects.AR.CCPaymentProcessing.Wrappers
{
	public class CardTransactionProcessingWrapper
	{
		public static ICardTransactionProcessingWrapper GetTransactionProcessingWrapper(object pluginObject, CCProcessingContext context)
		{
			if (pluginObject is V1.ICCPaymentProcessing)
			{
				return new V1CardTransactionProcessor(
					(V1.ICCPaymentProcessing)pluginObject, 
					Repositories.CardProcessingReadersProvider.GetCardProcessingProvider(context));
			}
			if (pluginObject is V2.ICCProcessingPlugin)
			{
				return new V2CardTransactionProcessor(
					(V2.ICCProcessingPlugin)pluginObject,
					Repositories.CardProcessingReadersProvider.GetCardProcessingProvider(context));
			}
			throw new PXException(V1.Messages.UnknownPluginType, pluginObject.GetType().Name);
		}

		protected class V1CardTransactionProcessor : ICardTransactionProcessingWrapper
		{
			private readonly V1.ICCPaymentProcessing _plugin;
			private readonly ICardProcessingReadersProvider _provider;

			public V1CardTransactionProcessor(V1.ICCPaymentProcessing v1Plugin, Repositories.ICardProcessingReadersProvider provider)
			{
				_plugin = v1Plugin;
				_provider = provider;
			}

			public V1.ProcessingResult DoTransaction(V1.CCTranType aTranType, V1.ProcessingInput inputData)
			{
				_plugin.Initialize(
					_provider.GetProcessingCenterSettingsStorage(),
					_provider.GetCardDataReader(),
					_provider.GetCustomerDataReader(),
					_provider.GetDocDetailsDataReader());
				V1.ProcessingResult result = new V1.ProcessingResult();
				_plugin.DoTransaction(aTranType, inputData, result);
				return result;
			}

			public void ExportSettings(IList<V1.ISettingsDetail> aSettings)
			{
				_plugin.Initialize(
					_provider.GetProcessingCenterSettingsStorage(), null, null);
				_plugin.ExportSettings(aSettings);
			}

			public void TestCredentials(V1.APIResponse apiResponse)
			{
				_plugin.Initialize(_provider.GetProcessingCenterSettingsStorage(), null, null);
				_plugin.TestCredentials(apiResponse);
			}

			public V1.CCErrors ValidateSettings(V1.ISettingsDetail setting)
			{
				_plugin.Initialize(
					_provider.GetProcessingCenterSettingsStorage(), null, null);
				return _plugin.ValidateSettings(setting);
			}
		}

		protected class V2CardTransactionProcessor : ICardTransactionProcessingWrapper
		{
			private readonly V2.ICCProcessingPlugin _plugin;
			private readonly ICardProcessingReadersProvider _provider;

			public V2CardTransactionProcessor(V2.ICCProcessingPlugin v2Plugin, Repositories.ICardProcessingReadersProvider provider)
			{
				_plugin = v2Plugin;
				_provider = provider;
			}
			
			public V1.ProcessingResult DoTransaction(V1.CCTranType aTranType, V1.ProcessingInput inputData)
			{
				V2SettingsGenerator seetingsGen = new V2SettingsGenerator(_provider);
				V2.ICCTransactionProcessor processor = _plugin.CreateProcessor<V2.ICCTransactionProcessor>(seetingsGen.GetSettings());
				V1.ProcessingResult result = null;
				if (processor == null)
				{
					string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA(
						Messages.FeatureNotSupportedByProcessing,
						CCProcessingFeature.Base);
					result = V1ProcessingDTOGenerator.GetProcessingResult(errorMessage);
					return result;
				}

				var inputGenerator = new V2ProcessingInputGenerator(_provider);
				var processingInput = inputGenerator.GetProcessingInput(aTranType, inputData);
				V2.ProcessingResult v2Result = processor.DoTransaction(processingInput);
				result = V1ProcessingDTOGenerator.GetProcessingResult(v2Result);

				return result;
			}

			public void ExportSettings(IList<V1.ISettingsDetail> aSettings)
			{
				var v2Settings = _plugin.ExportSettings();
				V1ProcessingDTOGenerator.FillV1Settings(aSettings, v2Settings);
			}

			public void TestCredentials(V1.APIResponse apiResponse)
			{
				V2SettingsGenerator seetingsGen = new V2SettingsGenerator(_provider);
				try
				{
					_plugin.TestCredentials(seetingsGen.GetSettings());
					V1ProcessingDTOGenerator.ApiResponseSetSuccess(apiResponse);
				}
				catch (V2.CCProcessingException e)
				{
					V1ProcessingDTOGenerator.ApiResponseSetError(apiResponse, e);
				}
			}

			public V1.CCErrors ValidateSettings(V1.ISettingsDetail setting)
			{
				string result = _plugin.ValidateSettings(V1ProcessingDTOGenerator.ToV2(setting));
				return V1ProcessingDTOGenerator.GetCCErrors(result);
			}
		}
	}
}
