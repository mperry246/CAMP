using System;
using System.Collections.Generic;
using System.Text;
using PX.Common;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.Common;

namespace PX.Objects.AR.CCPaymentProcessing.Helpers
{
	public static class CCProcTranHelper
	{
		public class CCProcTranOrderComparer : IComparer<CCProcTran>
		{
			private bool _descending;

			public CCProcTranOrderComparer(bool aDescending)
			{
				this._descending = aDescending;
			}
			public CCProcTranOrderComparer()
			{
				this._descending = false;
			}

			#region IComparer<CCProcTran> Members

			public virtual int Compare(CCProcTran x, CCProcTran y)
			{
				int order = x.TranNbr.Value.CompareTo(y.TranNbr.Value);
				return (this._descending ? -order : order);
			}

			#endregion
		}

		public static string FormatCCPaymentState(CCPaymentState aState)
		{
			Dictionary<CCPaymentState, string> stateDict = new Dictionary<CCPaymentState, string>();

			stateDict[CCPaymentState.None] = PXMessages.LocalizeNoPrefix(Messages.CCNone);
			stateDict[CCPaymentState.PreAuthorized] = PXMessages.LocalizeNoPrefix(Messages.CCPreAuthorized);
			stateDict[CCPaymentState.PreAuthorizationFailed] = PXMessages.LocalizeNoPrefix(Messages.CCPreAuthorizationFailed);
			stateDict[CCPaymentState.PreAuthorizationExpired] = PXMessages.LocalizeNoPrefix(Messages.CCPreAuthorizationExpired);
			stateDict[CCPaymentState.Captured] = PXMessages.LocalizeNoPrefix(Messages.CCCaptured);
			stateDict[CCPaymentState.CaptureFailed] = PXMessages.LocalizeNoPrefix(Messages.CCCaptureFailed);
			stateDict[CCPaymentState.Voided] = PXMessages.LocalizeNoPrefix(Messages.CCVoided);
			stateDict[CCPaymentState.VoidFailed] = PXMessages.LocalizeNoPrefix(Messages.CCVoidFailed);
			stateDict[CCPaymentState.Refunded] = PXMessages.LocalizeNoPrefix(Messages.CCRefunded);
			stateDict[CCPaymentState.RefundFailed] = PXMessages.LocalizeNoPrefix(Messages.CCRefundFailed);
			stateDict[CCPaymentState.VoidFailed] = PXMessages.LocalizeNoPrefix(Messages.CCRefundFailed);
			StringBuilder result = new StringBuilder();
			foreach (KeyValuePair<CCPaymentState, string> it in stateDict)
			{
				if ((aState & it.Key) != 0)
				{
					if (result.Length > 0)
						result.Append(",");
					result.Append(it.Value);
				}
			}
			return result.ToString();
		}

		public static CCPaymentState ResolveCCPaymentState(IEnumerable<PXResult<CCProcTran>> ccProcTrans)
		{
			CCProcTran lastTran;
			return ResolveCCPaymentState(ccProcTrans, out lastTran);
		}

		public static CCPaymentState ResolveCCPaymentState(IEnumerable<PXResult<CCProcTran>> ccProcTrans, out CCProcTran aLastTran)
		{
			CCPaymentState result = CCPaymentState.None;
			CCProcTran lastTran = null;
			CCProcTran lastSSTran = null; //Last successful tran
			CCProcTran preLastSSTran = null;
			CCProcTranOrderComparer ascComparer = new CCProcTranOrderComparer();
			aLastTran = null;
			foreach (CCProcTran iTran in ccProcTrans)
			{
				if (iTran.ProcStatus != CCProcStatus.Finalized && iTran.ProcStatus != CCProcStatus.Error)
					continue;
				if (lastTran == null)
				{
					lastTran = iTran;
				}
				else
				{
					if (ascComparer.Compare(iTran, lastTran) > 0)
					{
						lastTran = iTran;
					}
				}

				if (iTran.TranStatus == CCTranStatusCode.Approved)
				{
					if (lastSSTran == null)
					{
						lastSSTran = iTran;
					}
					else if (ascComparer.Compare(iTran, lastSSTran) > 0)
					{
						lastSSTran = iTran;
					}

					if (lastSSTran != null && (ascComparer.Compare(iTran, lastSSTran) < 0))
					{
						if (preLastSSTran == null)
						{
							preLastSSTran = iTran;
						}
						else if (ascComparer.Compare(iTran, preLastSSTran) > 0)
						{
							preLastSSTran = iTran;
						}
					}
				}
			}

			if (lastTran != null)
			{
				if (lastSSTran != null)
				{
					switch (lastSSTran.TranType)
					{
						case CCTranTypeCode.Authorize:
							if (!IsExpired(lastSSTran))
								result = CCPaymentState.PreAuthorized;
							else
								result = CCPaymentState.PreAuthorizationExpired;
							break;
						case CCTranTypeCode.AuthorizeAndCapture:
						case CCTranTypeCode.PriorAuthorizedCapture:
						case CCTranTypeCode.CaptureOnly:
							result = CCPaymentState.Captured;
							break;
						case CCTranTypeCode.VoidTran:
							if (preLastSSTran != null)
							{
								result = (preLastSSTran.TranType == CCTranTypeCode.Authorize) ?
											result = CCPaymentState.None : result = CCPaymentState.Voided; //Voidin of credit currenly is not allowed								
							}
							break;
						case CCTranTypeCode.Credit:
							result = CCPaymentState.Refunded;
							break;
					}
				}

				if (lastSSTran == null || lastSSTran.TranNbr != lastTran.TranNbr) //this means that lastOp failed
				{
					switch (lastTran.TranType)
					{
						case CCTranTypeCode.Authorize:
							result |= CCPaymentState.PreAuthorizationFailed;
							break;
						case CCTranTypeCode.AuthorizeAndCapture:
						case CCTranTypeCode.PriorAuthorizedCapture:
						case CCTranTypeCode.CaptureOnly:
							result |= CCPaymentState.CaptureFailed;
							break;
						case CCTranTypeCode.VoidTran:
							result |= CCPaymentState.VoidFailed;
							break;
						case CCTranTypeCode.Credit:
							result |= CCPaymentState.RefundFailed;
							break;
					}
				}
			}
			aLastTran = lastTran;
			return result;
		}

		public static bool HasSuccessfulCCTrans(PXSelectBase<CCProcTran> ccProcTran)
		{
			CCProcTran lastSTran = FindCCLastSuccessfulTran(ccProcTran);
			if (lastSTran != null && !IsExpired(lastSTran))
			{
				return true;
			}
			return false;
		}

		public static bool HasUnfinishedCCTrans(PXGraph aGraph, CustomerPaymentMethod aCPM)
		{
			if (aCPM.PMInstanceID < 0)
			{
				return false;
			}

			Dictionary<string, List<PXResult<CCProcTran>>> TranDictionary = new Dictionary<string, List<PXResult<CCProcTran>>>();
			PXResultset<CCProcTran> ccTrans = PXSelect<CCProcTran, Where<CCProcTran.pMInstanceID, Equal<Required<CCProcTran.pMInstanceID>>,
					And<CCProcTran.pCTranNumber, IsNotNull>>, OrderBy<Asc<CCProcTran.pCTranNumber>>>.Select(aGraph, aCPM.PMInstanceID);

			foreach (var row in ccTrans)
			{
				CCProcTran tran = (CCProcTran)row;
				if (tran.PCTranNumber != "0")
				{
					if (!TranDictionary.ContainsKey(tran.PCTranNumber))
					{
						TranDictionary[tran.PCTranNumber] = new List<PXResult<CCProcTran>>();
					}
					TranDictionary[tran.PCTranNumber].Add(row);
				}
			}

			bool hasUnfinishedTrans = false;
			foreach (var kvp in TranDictionary)
			{
				List<PXResult<CCProcTran>> tranList = kvp.Value;
				CCProcTran lastTran;
				CCPaymentState ccPaymentState = ResolveCCPaymentState(tranList, out lastTran);
				bool isCCPreAuthorized = (ccPaymentState & CCPaymentState.PreAuthorized) != 0;
				if (isCCPreAuthorized && lastTran != null && (lastTran.ExpirationDate == null || lastTran.ExpirationDate > DateTime.Now))
				{
					hasUnfinishedTrans = true;
					break;
				}
			}
			return hasUnfinishedTrans;
		}

		public static bool UpdateCapturedState<T>(T doc, IEnumerable<PXResult<CCProcTran>> ccProcTrans)
			where T : class, IBqlTable, ICCCapturePayment
		{
			CCProcTran lastTran;
			CCPaymentState ccPaymentState = ResolveCCPaymentState(ccProcTrans, out lastTran);
			bool isCCVoided = (ccPaymentState & CCPaymentState.Voided) != 0;
			bool isCCCaptured = (ccPaymentState & CCPaymentState.Captured) != 0;
			bool isCCPreAuthorized = (ccPaymentState & CCPaymentState.PreAuthorized) != 0;
			bool isCCRefunded = (ccPaymentState & CCPaymentState.Refunded) != 0;
			bool isCCVoidingAttempted = (ccPaymentState & CCPaymentState.VoidFailed) != 0;
			bool needUpdate = false;

			if (doc.IsCCCaptured != isCCCaptured)
			{
				doc.IsCCCaptured = isCCCaptured;
				needUpdate = true;
			}

			if (lastTran != null
				&& (lastTran.TranType == CCTranTypeCode.PriorAuthorizedCapture
					|| lastTran.TranType == CCTranTypeCode.AuthorizeAndCapture
					|| lastTran.TranType == CCTranTypeCode.CaptureOnly))
			{
				if (isCCCaptured)
				{
					doc.CuryCCCapturedAmt = lastTran.Amount;
					doc.IsCCCaptureFailed = false;
					needUpdate = true;
				}
				else
				{
					doc.IsCCCaptureFailed = true;
					needUpdate = true;
				}
			}

			if (doc.IsCCCaptured == false && (doc.CuryCCCapturedAmt != Decimal.Zero))
			{
				doc.CuryCCCapturedAmt = Decimal.Zero;
				needUpdate = true;
			}

			return needUpdate;
		}

		public struct CCTransState
		{
			public bool NeedUpdate;
			public CCPaymentState CCState;
			public CCProcTran LastTran;
		}

		public static CCTransState UpdateCCPaymentState<T>(T doc, IEnumerable<PXResult<CCProcTran>> ccProcTrans)
			where T : class, ICCAuthorizePayment, ICCCapturePayment
		{
			CCProcTran lastTran;
			CCPaymentState ccPaymentState = ResolveCCPaymentState(ccProcTrans, out lastTran);
			bool isCCVoided = (ccPaymentState & CCPaymentState.Voided) != 0;
			bool isCCCaptured = (ccPaymentState & CCPaymentState.Captured) != 0;
			bool isCCPreAuthorized = (ccPaymentState & CCPaymentState.PreAuthorized) != 0;
			bool isCCRefunded = (ccPaymentState & CCPaymentState.Refunded) != 0;
			bool isCCVoidingAttempted = (ccPaymentState & CCPaymentState.VoidFailed) != 0 || (ccPaymentState & CCPaymentState.RefundFailed) != 0;
			bool needUpdate = false;

			if (doc.IsCCAuthorized != isCCPreAuthorized || doc.IsCCCaptured != isCCCaptured)
			{
				if (!isCCVoidingAttempted)
				{
					doc.IsCCAuthorized = isCCPreAuthorized;
					doc.IsCCCaptured = isCCCaptured;
					needUpdate = true;
				}
				else
				{
					doc.IsCCAuthorized = false;
					doc.IsCCCaptured = false;
					needUpdate = false;
				}
			}


			if (lastTran != null && isCCPreAuthorized && lastTran.TranType == CCTranTypeCode.Authorize)
			{
				doc.CCAuthExpirationDate = lastTran.ExpirationDate;
				doc.CuryCCPreAuthAmount = lastTran.Amount;
				needUpdate = true;
			}

			if (doc.IsCCAuthorized == false && (doc.CCAuthExpirationDate != null || doc.CuryCCPreAuthAmount > Decimal.Zero))
			{
				doc.CCAuthExpirationDate = null;
				doc.CuryCCPreAuthAmount = Decimal.Zero;

				needUpdate = true;
			}

			if (lastTran != null
				&& (lastTran.TranType == CCTranTypeCode.PriorAuthorizedCapture
					|| lastTran.TranType == CCTranTypeCode.AuthorizeAndCapture
					|| lastTran.TranType == CCTranTypeCode.CaptureOnly))
			{
				if (isCCCaptured)
				{
					doc.CuryCCCapturedAmt = lastTran.Amount;
					doc.IsCCCaptureFailed = false;
					needUpdate = true;
				}
				else
				{
					doc.IsCCCaptureFailed = true;
					needUpdate = true;
				}
			}

			if (doc.IsCCCaptured == false && (doc.CuryCCCapturedAmt != Decimal.Zero))
			{
				doc.CuryCCCapturedAmt = Decimal.Zero;
				needUpdate = true;
			}

			return new CCTransState { NeedUpdate = needUpdate, CCState = ccPaymentState, LastTran = lastTran };
		}
		
		public static bool IsExpired(CCProcTran aTran)
		{
			return (aTran.ExpirationDate.HasValue && (aTran.ExpirationDate.Value < PXTimeZoneInfo.Now));
		}

		public static bool HasOpenCCTran(PXSelectBase<CCProcTran> ccProcTran)
		{
			foreach (CCProcTran iTr in ccProcTran.Select())
			{
				if (iTr.ProcStatus == CCProcStatus.Opened && !IsExpired(iTr))
					return true;
			}
			return false;
		}

		public static bool HasCCTransactions(PXSelectBase<CCProcTran> ccProcTran)
		{
			return (ccProcTran.Any());
		}

		public static CCProcTran FindCCPreAthorizing(IEnumerable<PXResult<CCProcTran>> ccProcTran)
		{
			List<CCProcTran> authTrans = new List<CCProcTran>(1);
			List<CCProcTran> result = new List<CCProcTran>(1);
			foreach (CCProcTran iTran in ccProcTran)
			{
				if (iTran.ProcStatus != CCProcStatus.Finalized)
					continue;
				if (iTran.TranType == CCTranTypeCode.Authorize && iTran.TranStatus == CCTranStatusCode.Approved)
				{
					authTrans.Add(iTran);
				}
			}

			foreach (CCProcTran it in authTrans)
			{
				bool cancelled = false;
				foreach (CCProcTran iTran in ccProcTran)
				{
					if (iTran.ProcStatus != CCProcStatus.Finalized)
						continue;
					if (iTran.RefTranNbr == it.TranNbr && iTran.TranStatus == CCTranStatusCode.Approved)
					{
						if (iTran.TranType.Trim() == CCTranTypeCode.PriorAuthorizedCapture
								|| iTran.TranType.Trim() == CCTranTypeCode.VoidTran)
						{
							cancelled = true;
							break;
						}
					}
				}
				if (!cancelled)
				{
					result.Add(it);
				}
			}

			if (result.Count > 0)
			{
				result.Sort(new CCProcTranOrderComparer(true)
						/*new Comparison<CCProcTran>(delegate(CCProcTran a, CCProcTran b)
						{
							return a.EndTime.Value.CompareTo(b.EndTime.Value);
						}) */
						);
				return result[0];
			}
			return null;
		}

		public static CCProcTran FindCapturing(PXSelectBase<CCProcTran> ccProcTran)
		{
			List<CCProcTran> authTrans = new List<CCProcTran>(1);
			List<CCProcTran> result = new List<CCProcTran>(1);
			foreach (CCProcTran iTran in ccProcTran.Select())
			{
				if (iTran.ProcStatus != CCProcStatus.Finalized)
					continue;
				if ((
						iTran.TranType == CCTranTypeCode.PriorAuthorizedCapture ||
						iTran.TranType == CCTranTypeCode.AuthorizeAndCapture ||
						iTran.TranType == CCTranTypeCode.CaptureOnly
					)
					&& iTran.TranStatus == CCTranStatusCode.Approved)
				{
					authTrans.Add(iTran);
				}
			}
			foreach (CCProcTran it in authTrans)
			{
				bool cancelled = false;
				foreach (CCProcTran iTran in ccProcTran.Select())
				{
					if (iTran.ProcStatus != CCProcStatus.Finalized)
						continue;
					if (iTran.RefTranNbr == it.TranNbr && iTran.TranStatus == CCTranStatusCode.Approved)
					{
						if (iTran.TranType == CCTranTypeCode.Credit
								|| iTran.TranType == CCTranTypeCode.VoidTran)
						{
							cancelled = true;
							break;
						}
					}
				}
				if (!cancelled)
				{
					result.Add(it);
				}
			}
			if (result.Count > 0)
			{
				result.Sort(new CCProcTranOrderComparer(true) /*Comparison<CCProcTran>(delegate(CCProcTran a, CCProcTran b)
						{
							return a.EndTime.Value.CompareTo(b.EndTime.Value);
						}
						)*/
					);
				return result[0];
			}
			return null;
		}

		public static CCProcTran FindCCLastSuccessfulTran(PXSelectBase<CCProcTran> ccProcTran)
		{
			CCProcTran lastTran = null;
			CCProcTranOrderComparer ascComparer = new CCProcTranOrderComparer();
			foreach (CCProcTran iTran in ccProcTran.Select())
			{
				if (iTran.ProcStatus != CCProcStatus.Finalized)
					continue;
				if (iTran.TranStatus == CCTranStatusCode.Approved)
				{
					if (lastTran == null || (ascComparer.Compare(iTran, lastTran) > 0))
					{
						lastTran = iTran;
					}
				}
			}
			return lastTran;
		}
	}
}
