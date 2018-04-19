using System;
using System.Collections.Generic;
using PX.CCProcessingBase;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;

namespace PX.Objects.AR.CCPaymentProcessing
{
	public class CCPaymentProcessingGraph : PXGraph<CCPaymentProcessingGraph>, ICCPaymentProcessor
	{
		private CCPaymentProcessing _processing = new CCPaymentProcessing();
		#region Public Auxiliary Functions
		public virtual bool TestCredentials(PXGraph callerGraph, string processingCenterID)
		{
			return new CCPaymentProcessing(callerGraph).TestCredentials(callerGraph, processingCenterID);
		}	
		
		public virtual void ValidateSettings(PXGraph callerGraph, string processingCenterID, ISettingsDetail settingDetail)
		{
			new CCPaymentProcessing(callerGraph).ValidateSettings(callerGraph, processingCenterID, settingDetail);
		}

		public virtual IList<ISettingsDetail> ExportSettings(PXGraph callerGraph, string processingCenterID)
		{
			return new CCPaymentProcessing(callerGraph).ExportSettings(callerGraph, processingCenterID);
		}
		
		#endregion
		#region Public Processing Functions

		public virtual bool Authorize(ICCPayment aPmtInfo, bool aCapture, ref int aTranNbr)
		{
			return _processing.Authorize(aPmtInfo, aCapture, ref aTranNbr);
		}

		public virtual bool Capture(int? aPMInstanceID, int? aAuthTranNbr, string aCuryID, decimal? aAmount, ref int aTranNbr)
		{
			return _processing.Capture(aPMInstanceID, aAuthTranNbr, aCuryID, aAmount, ref aTranNbr);
		}

		public virtual bool CaptureOnly(ICCPayment aPmtInfo, string aAuthorizationNbr, ref int aTranNbr)
		{
			return _processing.CaptureOnly(aPmtInfo, aAuthorizationNbr, ref aTranNbr);
		}

		public virtual bool Void(int? aPMInstanceID, int? aRefTranNbr, ref int aTranNbr)
		{
			return _processing.Void(aPMInstanceID, aRefTranNbr, ref aTranNbr);
		}

		public virtual bool VoidOrCredit(int? aPMInstanceID, int? aRefTranNbr, ref int aTranNbr)
		{
			return _processing.VoidOrCredit(aPMInstanceID, aRefTranNbr, ref aTranNbr);
		}

		public virtual bool Credit(ICCPayment aPmtInfo, string aExtRefTranNbr, ref int aTranNbr)
		{
			return _processing.Credit(aPmtInfo, aExtRefTranNbr, ref aTranNbr);
		}

		public virtual bool Credit(ICCPayment aPmtInfo, int aRefTranNbr, ref int aTranNbr)
		{
			return _processing.Credit(aPmtInfo, aRefTranNbr, ref aTranNbr);
		}

		public virtual bool Credit(int? aPMInstanceID, int? aRefTranNbr, string aCuryID, decimal? aAmount, ref int aTranNbr)
		{
			return _processing.Credit(aPMInstanceID, aRefTranNbr, aCuryID, aAmount, ref aTranNbr);
		}

		public virtual bool RecordAuthorization(ICCPayment aPmtInfo, string aExtTranRef, Decimal? aAuthAmount, DateTime? aExpirationDate, ref int? aTranNbr)
		{
			return _processing.RecordAuthorization(aPmtInfo, aExtTranRef, aAuthAmount, aExpirationDate, ref aTranNbr);
		}

		public virtual void RecordCapture(ICCPayment aPmtInfo, string aExtTranRef, string aExtAuthNbr, Decimal? aAmount, ref int? aTranNbr)
		{
			_processing.RecordCapture(aPmtInfo, aExtTranRef, aExtAuthNbr, aAmount, ref aTranNbr);
		}

		public virtual void RecordVoidingTran(CCProcTran aOrigTran, string aExtTranRef, out int aTranNbr)
		{
			_processing.RecordVoidingTran(aOrigTran, aExtTranRef, out aTranNbr);
		}

		public virtual bool RecordCredit(ICCPayment aPmtInfo, string aRefPCTranNbr, string aExtTranRef, string aExtAuthNbr, out int? aTranNbr)
		{
			return _processing.RecordCredit(aPmtInfo, aRefPCTranNbr, aExtTranRef, aExtAuthNbr, out aTranNbr);
		}
		#endregion
	}
}