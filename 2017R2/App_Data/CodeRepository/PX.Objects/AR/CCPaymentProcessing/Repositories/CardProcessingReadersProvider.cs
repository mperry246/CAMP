using System;
using System.Collections.Generic;
using PX.CCProcessingBase;
using PX.Objects.AR.CCPaymentProcessing.Common;

namespace PX.Objects.AR.CCPaymentProcessing.Repositories
{
	class CardProcessingReadersProvider : ICardProcessingReadersProvider
	{
		private CCProcessingContext _context;
		private String2DateConverterFunc _string2DateConverter;

		protected CardProcessingReadersProvider(CCProcessingContext context)
		{
			_context = context;
			_string2DateConverter = _context.expirationDateConverter;
		}

		public static ICardProcessingReadersProvider GetCardProcessingProvider(CCProcessingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}
			return new CardProcessingReadersProvider(context);
		}

		public ICreditCardDataReader GetCardDataReader()
		{
			return new CreditCardDataReader(_context.callerGraph, _context.aPMInstanceID);
		}

		public IEnumerable<ICreditCardDataReader> GetCustomerCardsDataReaders()
		{
			return new CustomerCardsDataReader(
				_context.callerGraph,
				_context.aCustomerID,
				_context.processingCenter.ProcessingCenterID,
				(graph, insID) => { return new CreditCardDataReader(graph, insID); }).GetCardReaders();
		}

		public ICustomerDataReader GetCustomerDataReader()
		{
			return new CustomerDataReader(_context);
		}

		public IDocDetailsDataReader GetDocDetailsDataReader()
		{
			return new DocDetailsDataReader(_context.callerGraph, _context.aDocType, _context.aRefNbr);
		}

		public IProcessingCenterSettingsStorage GetProcessingCenterSettingsStorage()
		{
			return new ProcessingCenterSettingsStorage(_context.callerGraph, _context.processingCenter.ProcessingCenterID);
		}

		public String2DateConverterFunc GetExpirationDateConverter()
		{
			return _string2DateConverter;
		}
	}
}
