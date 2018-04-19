using PX.CCProcessingBase;
using PX.Common;
using PX.Data;
using PX.Objects.Common;
using System;
using System.Collections.Generic;
using System.Text;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;


namespace PX.Objects.AR.CCPaymentProcessing
{
	public static class CCPaymentEntry
	{
		public static void AuthorizeCCPayment<TNode>(TNode doc, PXSelectBase<CCProcTran> ccProcTran)
			where TNode : class, IBqlTable, ICCPayment, new()
		{
			AuthorizeCCPayment<TNode>(doc, ccProcTran, null);
		}

		public static void AuthorizeCCPayment<TNode>(TNode doc, PXSelectBase<CCProcTran> ccProcTran, 
			UpdateDocStateDelegate aDocStateUpdater,
			ICCTransactionsProcessor processor = null)
			where TNode : class, IBqlTable, ICCPayment, new()
		{
			if (processor == null)
			{
				processor = CCTransactionsProcessor.GetCCTransactionsProcessor();
			}
			if (doc != null && doc.PMInstanceID != null && doc.CuryDocBal != null)
			{
				if (CCProcTranHelper.HasOpenCCTran(ccProcTran))
					throw new PXException(Messages.ERR_CCTransactionCurrentlyInProgress);

				CCPaymentState paymentState = CCProcTranHelper.ResolveCCPaymentState(ccProcTran.Select());
				if ((paymentState & (CCPaymentState.Captured | CCPaymentState.PreAuthorized)) > 0)
				{
					throw new PXException(Messages.ERR_CCPaymentAlreadyAuthorized);
				}
				if (doc.Released == false)
				{
				ccProcTran.View.Graph.Actions.PressSave();
				}
				TNode toProc = PXCache<TNode>.CreateCopy(doc);
				PXLongOperation.StartOperation(ccProcTran.View.Graph, delegate ()
				{
					try
					{
						processor.ProcessCCTransaction(toProc, null, CCTranType.AuthorizeOnly);
					}
					finally
					{
						if (aDocStateUpdater != null)
						{
							aDocStateUpdater(doc, CCTranType.AuthorizeOnly);
						}
					}
				});
			}
		}

		public static void CaptureCCPayment<TNode>(TNode doc, PXSelectBase<CCProcTran> ccProcTran, bool doRelease, ReleaseDelegate CustomPreReleaseDelegate)
			where TNode : ARRegister, IBqlTable, ICCPayment, new()
		{
			ReleaseDelegate combinedReleaseDelegate = doRelease ? CustomPreReleaseDelegate + ReleaseARDocument : CustomPreReleaseDelegate;
			CaptureCCPayment<TNode>(doc, ccProcTran, combinedReleaseDelegate, null);
		}
		
		public static void CaptureOnlyCCPayment<TNode>(TNode doc, string aAuthorizationNbr, PXSelectBase<CCProcTran> ccProcTran, bool doRelease)
			where TNode : ARRegister, IBqlTable, ICCPayment, new()
		{
			CaptureOnlyCCPayment<TNode>(doc, aAuthorizationNbr, ccProcTran, doRelease ? ReleaseARDocument : (ReleaseDelegate)null, null);
		}

		public static void CaptureCCPayment<TNode>(TNode doc, PXSelectBase<CCProcTran> ccProcTran, 
			CCPaymentEntry.ReleaseDelegate aReleaseDelegate, 
			UpdateDocStateDelegate aDocStateUpdater,
			ICCTransactionsProcessor processor = null)
			where TNode : class, IBqlTable, ICCPayment, new()
		{
			if (processor == null)
			{
				processor = CCTransactionsProcessor.GetCCTransactionsProcessor();
			}
			if (doc != null && doc.PMInstanceID != null && doc.CuryDocBal != null)
			{
				if (CCProcTranHelper.HasOpenCCTran(ccProcTran))
					throw new PXException(Messages.ERR_CCTransactionCurrentlyInProgress);

				CCPaymentState paymentState = CCProcTranHelper.ResolveCCPaymentState(ccProcTran.Select());
				if ((paymentState & (CCPaymentState.Captured)) > 0)
				{
					throw new PXException(Messages.ERR_CCAuthorizedPaymentAlreadyCaptured);
				}
				if (doc.Released == false)
				{
				ccProcTran.View.Graph.Actions.PressSave();
				}
				CCProcTran authTran = CCProcTranHelper.FindCCPreAthorizing(ccProcTran.Select());
				TNode toProc = PXCache<TNode>.CreateCopy(doc);
				CCProcTran authTranCopy = null;
				if (authTran != null && !CCProcTranHelper.IsExpired(authTran))
					authTranCopy = PXCache<CCProcTran>.CreateCopy(authTran);
				CCTranType operation = (authTranCopy) != null ? CCTranType.PriorAuthorizedCapture : CCTranType.AuthorizeAndCapture;
				PXLongOperation.StartOperation(ccProcTran.View.Graph, delegate ()
				{
					try
					{
						processor.ProcessCCTransaction(toProc, authTranCopy, operation);
					}
					finally
					{
						//Update doc state in any case
						if (aDocStateUpdater != null)
							aDocStateUpdater(doc, operation);
					}
					if (aReleaseDelegate != null)
						aReleaseDelegate(toProc);       //On Success Only			

				});
			}
		}

		public static void CaptureOnlyCCPayment<TNode>(TNode doc, 
			string aAuthorizationNbr, 
			PXSelectBase<CCProcTran> ccProcTran,
			CCPaymentEntry.ReleaseDelegate aReleaseDelegate, 
			UpdateDocStateDelegate aDocStateUpdater,
			ICCTransactionsProcessor processor = null)
			where TNode : class, IBqlTable, ICCPayment, new()
		{
			if (processor == null)
			{
				processor = CCTransactionsProcessor.GetCCTransactionsProcessor();
			}
			if (doc != null && doc.PMInstanceID != null && doc.CuryDocBal != null)
			{
				if (CCProcTranHelper.HasOpenCCTran(ccProcTran))
					throw new PXException(Messages.ERR_CCTransactionCurrentlyInProgress);

				if (string.IsNullOrEmpty(aAuthorizationNbr))
					throw new PXException(Messages.ERR_CCExternalAuthorizationNumberIsRequiredForCaptureOnlyTrans);
				if (doc.Released == false)
				{
				ccProcTran.View.Graph.Actions.PressSave();
				}
				TNode toProc = PXCache<TNode>.CreateCopy(doc);
				PXLongOperation.StartOperation(ccProcTran.View.Graph, delegate ()
				{
					try
					{
						CCProcTran refTran = new CCProcTran();
						refTran.AuthNumber = aAuthorizationNbr;
						processor.ProcessCCTransaction(toProc, refTran, CCTranType.CaptureOnly);
					}
					finally
					{
						if (aDocStateUpdater != null)
							aDocStateUpdater(doc, CCTranType.VoidOrCredit);
					}
					if (aReleaseDelegate != null)
						aReleaseDelegate(toProc);
				});
			}
		}

		public static void VoidCCPayment<TNode>(TNode doc, PXSelectBase<CCProcTran> ccProcTran, bool doRelease)
			where TNode : ARRegister, ICCPayment, new()
		{
			VoidCCPayment<TNode>(doc, ccProcTran, doRelease ? ReleaseARDocument : (ReleaseDelegate)null, null);
		}

		public static void VoidCCPayment<TNode>(TNode doc, PXSelectBase<CCProcTran> ccProcTran, bool doRelease, ReleaseDelegate CustomPreReleaseDelegate)
			where TNode : ARRegister, ICCPayment, new()
		{
			ReleaseDelegate combinedReleaseDelegate = doRelease ? CustomPreReleaseDelegate + ReleaseARDocument : CustomPreReleaseDelegate;
			VoidCCPayment<TNode>(doc, ccProcTran, combinedReleaseDelegate, null);
		}
		
		public static void VoidCCPayment<TNode>(TNode doc, 
			PXSelectBase<CCProcTran> ccProcTran,
			CCPaymentEntry.ReleaseDelegate aReleaseDelegate,
			UpdateDocStateDelegate aDocStateUpdater,
			ICCTransactionsProcessor processor = null)
			where TNode : class, IBqlTable, ICCPayment, new()
		{
			if (processor == null)
			{
				processor = CCTransactionsProcessor.GetCCTransactionsProcessor();
			}
			if (doc != null && doc.PMInstanceID != null && doc.CuryDocBal != null)
			{
				if (CCProcTranHelper.HasOpenCCTran(ccProcTran))
					throw new PXException(Messages.ERR_CCTransactionCurrentlyInProgress);

				CCProcTran toVoid = CCProcTranHelper.FindCCLastSuccessfulTran(ccProcTran);
				if (toVoid == null)
				{
					throw new PXException(Messages.ERR_CCNoTransactionToVoid);
				}
				else if (toVoid.TranType == CCTranTypeCode.VoidTran || toVoid.TranType == CCTranTypeCode.Credit)
				{
					throw new PXException(Messages.ERR_CCTransactionOfThisTypeInvalidToVoid);
				}

				if (CCProcTranHelper.IsExpired(toVoid))
				{
					throw new PXException(Messages.TransactionHasExpired);
				}
				if (doc.Released == false)
				{
				ccProcTran.View.Graph.Actions.PressSave();
				}
				TNode toProc = PXCache<TNode>.CreateCopy(doc);
				PXLongOperation.StartOperation(ccProcTran.View.Graph, delegate ()
				{
					try
					{
						processor.ProcessCCTransaction(toProc, toVoid, CCTranType.VoidOrCredit);
					}
					finally
					{
						if (aDocStateUpdater != null)
							aDocStateUpdater(doc, CCTranType.VoidOrCredit);
					}
					if (aReleaseDelegate != null)
						aReleaseDelegate(toProc);

				});
			}
		}

		public static void CreditCCPayment<TNode>(TNode doc, 
			string aPCRefTranNbr, 
			PXSelectBase<CCProcTran> ccProcTran, 
			CCPaymentEntry.ReleaseDelegate aReleaseDelegate, 
			UpdateDocStateDelegate aDocStateUpdater,
			ICCTransactionsProcessor processor = null)
			where TNode : class, IBqlTable, ICCPayment, new()
		{
			if (processor == null)
			{
				processor = CCTransactionsProcessor.GetCCTransactionsProcessor();
			}
			if (doc != null && doc.PMInstanceID != null && doc.CuryDocBal != null)
			{
				if (CCProcTranHelper.HasOpenCCTran(ccProcTran))
					throw new PXException(Messages.ERR_CCTransactionCurrentlyInProgress);

				if (doc.Released == false)
				{
				ccProcTran.View.Graph.Actions.PressSave();
				}
				TNode toProc = PXCache<TNode>.CreateCopy(doc);
				PXLongOperation.StartOperation(ccProcTran.View.Graph, delegate ()
				{
					try
					{
						CCProcTran refTran = new CCProcTran();
						refTran.TranNbr = null;
						refTran.PCTranNumber = aPCRefTranNbr;
						processor.ProcessCCTransaction(toProc, refTran, CCTranType.Credit);
					}
					finally
					{
						if (aDocStateUpdater != null)
							aDocStateUpdater(doc, CCTranType.VoidOrCredit);
					}
					if (aReleaseDelegate != null)
						aReleaseDelegate(toProc);

				});
			}
		}

		public static void CreditCCPayment<TNode>(TNode doc, string aPCRefTranNbr, PXSelectBase<CCProcTran> ccProcTran, bool doRelease, ReleaseDelegate CustomPreReleaseDelegate)
			where TNode : ARRegister, IBqlTable, ICCPayment, new()
		{
			ReleaseDelegate combinedReleaseDelegate = doRelease ? CustomPreReleaseDelegate + ReleaseARDocument : CustomPreReleaseDelegate;
			CreditCCPayment<TNode>(doc, aPCRefTranNbr, ccProcTran, combinedReleaseDelegate, null);
		}

		public static void RecordCCPayment<TNode>(TNode doc, string aExtPCTranNbr, string aPCAuthNbr, PXSelectBase<CCProcTran> ccProcTran, CCPaymentEntry.ReleaseDelegate aReleaseDelegate, UpdateDocStateDelegate aDocStateUpdater)
			where TNode : class, IBqlTable, ICCPayment, new()
		{
			if (doc != null && doc.PMInstanceID != null && doc.CuryDocBal != null)
			{
				if (CCProcTranHelper.HasOpenCCTran(ccProcTran))
					throw new PXException(Messages.ERR_CCTransactionCurrentlyInProgress);

				if (string.IsNullOrEmpty(aExtPCTranNbr))
					throw new PXException(Messages.ERR_PCTransactionNumberOfTheOriginalPaymentIsRequired);


				CCPaymentState paymentState = CCProcTranHelper.ResolveCCPaymentState(ccProcTran.Select());
				if ((paymentState & (CCPaymentState.Captured)) > 0)
				{
					throw new PXException(Messages.ERR_CCAuthorizedPaymentAlreadyCaptured);
				}

				if (doc.Released == false)
				{
				ccProcTran.View.Graph.Actions.PressSave();
				}

				TNode toProc = PXCache<TNode>.CreateCopy(doc);
				CCProcTran authTran = CCProcTranHelper.FindCCPreAthorizing(ccProcTran.Select());
				CCTranType operation = CCTranType.AuthorizeAndCapture;
				PXLongOperation.StartOperation(ccProcTran.View.Graph, delegate ()
				{
					try
					{
						var graph = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
						int? tranID = 0;
						string extTranID = aExtPCTranNbr;
						graph.RecordCapture(doc, extTranID, aPCAuthNbr, null, ref tranID);
					}
					finally
					{
						//Update doc state in any case
						if (aDocStateUpdater != null)
							aDocStateUpdater(doc, operation);
					}
					if (aReleaseDelegate != null)
						aReleaseDelegate(toProc);       //On Success Only			

				});
			}
		}

		public static void RecordCCPayment<TNode>(TNode doc, string aExtPCTranNbr, string aPCAuthNbr, PXSelectBase<CCProcTran> ccProcTran, bool doRelease)
			where TNode : ARRegister, ICCPayment, new()
		{
			RecordCCPayment<TNode>(doc, aExtPCTranNbr, aPCAuthNbr, ccProcTran, doRelease ? ReleaseARDocument : (ReleaseDelegate)null, null);
		}

		public static void RecordCCCredit<TNode>(TNode doc, string aRefPCTranNbr, string aExtPCTranNbr, string aPCAuthNbr, PXSelectBase<CCProcTran> ccProcTran, CCPaymentEntry.ReleaseDelegate aReleaseDelegate, UpdateDocStateDelegate aDocStateUpdater)
		where TNode : class, IBqlTable, ICCPayment, new()
		{
			if (doc != null && doc.PMInstanceID != null && doc.CuryDocBal != null)
			{
				if (CCProcTranHelper.HasOpenCCTran(ccProcTran))
					throw new PXException(Messages.ERR_CCTransactionCurrentlyInProgress);

				if (string.IsNullOrEmpty(aExtPCTranNbr))
					throw new PXException(Messages.ERR_PCTransactionNumberOfTheOriginalPaymentIsRequired);


				CCPaymentState paymentState = CCProcTranHelper.ResolveCCPaymentState(ccProcTran.Select());
				if ((paymentState & (CCPaymentState.Refunded)) > 0)
				{
					throw new PXException(Messages.ERR_CCPaymentIsAlreadyRefunded);
				}

				if (doc.Released == false)
				{
				ccProcTran.View.Graph.Actions.PressSave();
				}

				TNode toProc = PXCache<TNode>.CreateCopy(doc);
				CCTranType operation = CCTranType.Credit;
				PXLongOperation.StartOperation(ccProcTran.View.Graph, delegate ()
				{
					try
					{
						var graph = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
						int? tranID;
						graph.RecordCredit(doc, aRefPCTranNbr, aExtPCTranNbr, aPCAuthNbr, out tranID);
					}
					finally
					{
						//Update doc state in any case
						if (aDocStateUpdater != null)
							aDocStateUpdater(doc, operation);
					}
					if (aReleaseDelegate != null)
						aReleaseDelegate(toProc);       //On Success Only			

				});
			}
		}
		
		public static void RecordCCCredit<TNode>(TNode doc, string aRefPCTranNbr, string aExtPCTranNbr, string aPCAuthNbr, PXSelectBase<CCProcTran> ccProcTran, bool doRelease)
			where TNode : ARRegister, ICCPayment, new()
		{
			RecordCCCredit<TNode>(doc, aRefPCTranNbr, aExtPCTranNbr, aPCAuthNbr, ccProcTran, doRelease ? ReleaseARDocument : (ReleaseDelegate)null, null);
		}

		public delegate void ReleaseDelegate(IBqlTable aTable);

		public delegate void UpdateDocStateDelegate(IBqlTable aDoc, CCTranType aLastOperation);

		public static void ReleaseARDocument(IBqlTable aTable)
		{
			ARRegister toProc = (ARRegister)aTable;
			if (!(toProc.Released ?? false))
			{
				List<ARRegister> list = new List<ARRegister>(1);
				list.Add(toProc);
				ARDocumentRelease.ReleaseDoc(list, false);
			}
		}
	}
}
