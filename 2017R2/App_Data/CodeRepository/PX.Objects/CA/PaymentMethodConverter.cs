using System;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;

namespace PX.Objects.CA
{
	public static class ProcessingCenterTypeNameConstants
	{
		public const string AuthnetAPIPluginFullName = "PX.CCProcessing.V2.AuthnetProcessingPlugin";
		public const string AuthnetCIMPluginFullName = "PX.CCProcessing.AuthorizeNetTokenizedProcessing";
		public const string AuthnetAIMPluginFullName = "PX.CCProcessing.AuthorizeNetProcessing";
		public class authnetAPIPluginFullName : Constant<string>
		{
			public authnetAPIPluginFullName() : base(AuthnetAPIPluginFullName) {}
		}
		public class authnetCIMPluginFullName : Constant<string>
		{
			public authnetCIMPluginFullName() : base(AuthnetCIMPluginFullName) {}
		}
		public class authnetAIMPluginFullName : Constant<string>
		{
			public authnetAIMPluginFullName() : base(AuthnetAIMPluginFullName) {}
		}
	}
	public class PaymentMethodConverter : PXGraph<PaymentMethodConverter>
	{
		public PXFilter<Filter> filter;
		public PXFilteredProcessing<CustomerPaymentMethod, Filter, Where<CustomerPaymentMethod.paymentMethodID, Equal<Current<Filter.oldPaymentMethodID>>, 
			And<CustomerPaymentMethod.cCProcessingCenterID, Equal<Current<Filter.oldCCProcessingCenterID>>>>> CustomerPaymentMethodList;
		public PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Optional<Filter.newCCProcessingCenterID>>>> ProcessingCenters;
		public PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Optional<CustomerPaymentMethod.pMInstanceID>>>> NewCustomerPM;
		public PXSelectJoin<CustomerPaymentMethodDetail, LeftJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>,
					And<PaymentMethodDetail.detailID, Equal<CustomerPaymentMethodDetail.detailID>,
					And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, 
					Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Optional<CustomerPaymentMethod.pMInstanceID>>>> CustomerPMDetails;
		[Serializable]
		public partial class Filter : IBqlTable
		{
			#region OldPaymentMethodID
			public abstract class oldPaymentMethodID : PX.Data.IBqlField
			{
			}
			protected String _OldPaymentMethodID;
			[PXDBString(10, IsUnicode = true)]
			[PXDefault()]
			[PXUIField(DisplayName = "Old Payment Method ID", Visibility = PXUIVisibility.SelectorVisible)]
			[PXSelector(typeof (Search<PaymentMethod.paymentMethodID, Where<PaymentMethod.paymentType, Equal<PaymentMethodType.creditCard>>>))]
			public virtual String OldPaymentMethodID
			{
				get { return this._OldPaymentMethodID; }
				set { this._OldPaymentMethodID = value; }
			}
			#endregion
			#region OldCCProcessingCenterID
			public abstract class oldCCProcessingCenterID : PX.Data.IBqlField
			{
			}
			[PXDBString(10, IsUnicode = true)]
			[PXDefault(typeof(Search2<CCProcessingCenterPmntMethod.processingCenterID,
				InnerJoin<PaymentMethod, On<CCProcessingCenterPmntMethod.paymentMethodID, Equal<PaymentMethod.paymentMethodID>>,
				InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>>,
				Where<PaymentMethod.paymentMethodID, Equal<Current<Filter.oldPaymentMethodID>>,
					And<CCProcessingCenterPmntMethod.isDefault, Equal<True>,
					And<CCProcessingCenter.processingTypeName, In3<ProcessingCenterTypeNameConstants.authnetAIMPluginFullName, ProcessingCenterTypeNameConstants.authnetCIMPluginFullName>>>>>))]
			[PXSelector(typeof(Search2<CCProcessingCenterPmntMethod.processingCenterID,
				InnerJoin<PaymentMethod, On<CCProcessingCenterPmntMethod.paymentMethodID, Equal<PaymentMethod.paymentMethodID>>,
				InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>>, 
				Where<PaymentMethod.paymentMethodID, Equal<Current<Filter.oldPaymentMethodID>>,
					And<CCProcessingCenter.processingTypeName, In3<ProcessingCenterTypeNameConstants.authnetAIMPluginFullName, ProcessingCenterTypeNameConstants.authnetCIMPluginFullName>>>>))]
			[PXUIField(DisplayName = "Old Proc. Center ID", Visibility = PXUIVisibility.SelectorVisible)]
			public virtual string OldCCProcessingCenterID { get; set; }
			#endregion
			#region NewCCProcessingCenterID
			public abstract class newCCProcessingCenterID : PX.Data.IBqlField
			{
			}
			[PXDBString(10, IsUnicode = true)]
			[PXDefault()]
			[PXSelector(typeof(Search<CCProcessingCenter.processingCenterID, Where<CCProcessingCenter.processingTypeName, Equal<ProcessingCenterTypeNameConstants.authnetAPIPluginFullName>>>))]
			[PXUIField(DisplayName = "New Proc. Center ID", Visibility = PXUIVisibility.SelectorVisible)]
			public virtual string NewCCProcessingCenterID { get; set; }
			#endregion
		}

		public PXCancel<Filter> Cancel;

		protected virtual void Filter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			if (e.Row == null) return;
			Filter row = (Filter)e.Row;
			bool allDataIsFilled = !string.IsNullOrEmpty(row.OldPaymentMethodID) 
				&& !string.IsNullOrEmpty(row.OldCCProcessingCenterID) 
				&& !string.IsNullOrEmpty(row.NewCCProcessingCenterID);
			CustomerPaymentMethodList.SetProcessEnabled(allDataIsFilled);
			CustomerPaymentMethodList.SetProcessAllEnabled(allDataIsFilled);
			if (allDataIsFilled)
			{
				CCProcessingCenter newCCPC = ProcessingCenters.Select();
				CustomerPaymentMethodList.SetProcessDelegate(cpm => ConvertCustomerPaymentMethod(cpm, newCCPC));
			}
		}

		private static void ConvertCustomerPaymentMethod(CustomerPaymentMethod cpm, CCProcessingCenter newCCPC)
		{
			if (newCCPC == null)
			{
				throw new PXException(Messages.NotSetProcessingCenter);
			}

			PaymentMethodUpdater updaterGraph = PXGraph.CreateInstance<PaymentMethodUpdater>();
			updaterGraph.ConvertCustomerPaymentMethod(cpm, newCCPC);
		}

		protected virtual void Filter_OldPaymentMethodID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			Filter filter = e.Row as Filter;
			if (filter == null) return;
			cache.SetDefaultExt<Filter.oldCCProcessingCenterID>(filter);
		}

		protected virtual PaymentMethodDetail FindSameTypeTemplate(PaymentMethod targetPM, PaymentMethodDetail baseDetail)
		{
			PaymentMethodDetail res = PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>,
				And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>,
				And<PaymentMethodDetail.isIdentifier, Equal<Required<PaymentMethodDetail.isIdentifier>>,
				And<PaymentMethodDetail.isExpirationDate, Equal<Required<PaymentMethodDetail.isExpirationDate>>,
				And<PaymentMethodDetail.isOwnerName, Equal<Required<PaymentMethodDetail.isOwnerName>>,
				And<PaymentMethodDetail.isCCProcessingID, Equal<Required<PaymentMethodDetail.isCCProcessingID>>>>>>>>>.
					Select(this, targetPM.PaymentMethodID, baseDetail.IsIdentifier, baseDetail.IsExpirationDate, baseDetail.IsOwnerName, baseDetail.IsCCProcessingID);
			return res;
		}

		protected virtual PaymentMethodDetail FindCCPID(PaymentMethod pm)
		{
			PaymentMethodDetail res = PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>,
				And<PaymentMethodDetail.isCCProcessingID, Equal<True>>>>.Select(this, pm.PaymentMethodID);
			return res;
		}
	}

	public class PaymentMethodUpdater : PXGraph<PaymentMethodUpdater, CustomerPaymentMethod>
	{
		public PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Optional<CustomerPaymentMethod.pMInstanceID>>>> CustomerPM;
		public PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>,
					And<PaymentMethodDetail.detailID, Equal<CustomerPaymentMethodDetail.detailID>,
					And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>,
					Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Optional<CustomerPaymentMethod.pMInstanceID>>>> CustomerPMDetails;

		public PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Optional<CustomerPaymentMethod.paymentMethodID>>>> PM;
		public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Optional<CustomerPaymentMethod.paymentMethodID>>>> PMDetails;
		public PXSelect<CCProcessingCenterPmntMethod> ProcessingCenterPM;

		[PXDBString(10, IsUnicode = true)]
		[PXSelector(typeof(Search<CCProcessingCenterPmntMethod.processingCenterID, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<CustomerPaymentMethod.paymentMethodID>>>>), DirtyRead = true)]
		protected virtual void CustomerPaymentMethod_CCProcessingCenterID_CacheAttached(PXCache sender)
		{
		}

		public void ConvertCustomerPaymentMethod(CustomerPaymentMethod cpm, CCProcessingCenter newCCPC)
		{
			CCProcessingCenterPmntMethod newProcessingCenterPM = PXSelect<CCProcessingCenterPmntMethod, 
				Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Required<CCProcessingCenterPmntMethod.paymentMethodID>>, 
				And<CCProcessingCenterPmntMethod.processingCenterID, Equal<Required<CCProcessingCenterPmntMethod.processingCenterID>>>>>.Select(this, cpm.PaymentMethodID, newCCPC.ProcessingCenterID);
			if (newProcessingCenterPM == null)
			{
				newProcessingCenterPM = (CCProcessingCenterPmntMethod)ProcessingCenterPM.Cache.CreateInstance();
				newProcessingCenterPM.PaymentMethodID = cpm.PaymentMethodID;
				newProcessingCenterPM.ProcessingCenterID = newCCPC.ProcessingCenterID;
				ProcessingCenterPM.Insert(newProcessingCenterPM);
			}

			CustomerPaymentMethod currCPM = (CustomerPaymentMethod)CustomerPM.Cache.CreateCopy(cpm);
			var oldCCProcessingCenterID = currCPM.CCProcessingCenterID;
			currCPM.CCProcessingCenterID = newCCPC.ProcessingCenterID;
			CustomerPM.Cache.SetDefaultExt<CustomerPaymentMethod.customerCCPID>(currCPM);
			
			currCPM.Selected = true;
			currCPM = CustomerPM.Update(currCPM);
			CustomerPM.Current = currCPM;
			
			PXResultset<PaymentMethodDetail> oldDetails = PMDetails.Select(currCPM.PaymentMethodID);
			foreach (PaymentMethodDetail oldDetail in oldDetails)
			{
				PaymentMethodDetail newDetail = (PaymentMethodDetail)PMDetails.Cache.CreateCopy(oldDetail);
				newDetail.ValidRegexp = null;
				PMDetails.Update(newDetail);
			}

			PaymentMethod CurrPM = PM.Select();
			PaymentMethodDetail CCPID = FindCCPID(CurrPM);

			if (CCPID == null)
			{
				using (PXTransactionScope ts = new PXTransactionScope())
				{
					PaymentMethodDetail res;
					CCPID = (PaymentMethodDetail) PMDetails.Cache.CreateInstance();
					CCPID.PaymentMethodID = currCPM.PaymentMethodID;
					CCPID.UseFor = PaymentMethodDetailUsage.UseForARCards;
					CCPID.DetailID = "CCPID";
					CCPID.Descr = Messages.PaymentProfileID;
					CCPID.IsCCProcessingID = true;
					CCPID.IsRequired = true;
					res = PMDetails.Insert(CCPID);
					if (res == null)
					{
						throw new PXException(Messages.CouldNotInsertPMDetail);
					}
					else
					{
						PMDetails.Cache.Persist(PXDBOperation.Insert);
					}
					ts.Complete();
				}
			}

			CCProcessingCenter procCenter = PXSelect<CCProcessingCenter,
				Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>
					.Select(this, oldCCProcessingCenterID);
			bool doesOldProcCenterSupportTokenizing = CCProcessingFeatureHelper.IsFeatureSupported(procCenter, CCProcessingFeature.ProfileManagement);
			bool doesNewProcCenterSupportTokenizing = CCProcessingFeatureHelper.IsFeatureSupported(newCCPC, CCProcessingFeature.ProfileManagement);

			if (!doesOldProcCenterSupportTokenizing && doesNewProcCenterSupportTokenizing)
			{
				CustomerPaymentMethodDetail newCCPIDPM = PXSelect<CustomerPaymentMethodDetail, 
					Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Required<CustomerPaymentMethodDetail.pMInstanceID>>,
						And<CustomerPaymentMethodDetail.paymentMethodID, Equal<Required<CustomerPaymentMethodDetail.paymentMethodID>>,
						And<CustomerPaymentMethodDetail.detailID, Equal<Required<CustomerPaymentMethodDetail.detailID>>>>>>
							.Select(this, currCPM.PMInstanceID, currCPM.PaymentMethodID, CCPID.DetailID);
				if (newCCPIDPM != null)
				{
					newCCPIDPM.Value = null;
					CustomerPMDetails.Update(newCCPIDPM);
				}
				else
				{
					newCCPIDPM = new CustomerPaymentMethodDetail
					{
						PMInstanceID = currCPM.PMInstanceID,
						PaymentMethodID = currCPM.PaymentMethodID,
						DetailID = CCPID.DetailID
					};
					CustomerPMDetails.Insert(newCCPIDPM);
				}
				var graph = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
				graph.GetOrCreatePaymentProfile(this, CustomerPM, CustomerPMDetails);
			}

			if ( doesNewProcCenterSupportTokenizing)
			{
				if (currCPM.CustomerCCPID == null)
				{
					currCPM.CustomerCCPID = cpm.CustomerCCPID;
				}
				CustomerProcessingCenterID newCustomerProcessingCenterID = new CustomerProcessingCenterID
				{
					CCProcessingCenterID = newCCPC.ProcessingCenterID,
					BAccountID = cpm.BAccountID,
					CustomerCCPID = currCPM.CustomerCCPID
				};
				PXCache cache = Caches[typeof(CustomerProcessingCenterID)];
				newCustomerProcessingCenterID = cache.Insert(newCustomerProcessingCenterID) as CustomerProcessingCenterID;
				cache.PersistInserted(newCustomerProcessingCenterID);
			}

			currCPM = CustomerPM.Update(currCPM);
			this.Save.Press();
		}

		protected virtual PaymentMethodDetail FindSameTypeTemplate(PaymentMethod targetPM, PaymentMethodDetail baseDetail)
		{
			PaymentMethodDetail res = PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>,
				And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>,
				And<PaymentMethodDetail.isIdentifier, Equal<Required<PaymentMethodDetail.isIdentifier>>,
				And<PaymentMethodDetail.isExpirationDate, Equal<Required<PaymentMethodDetail.isExpirationDate>>,
				And<PaymentMethodDetail.isOwnerName, Equal<Required<PaymentMethodDetail.isOwnerName>>,
				And<PaymentMethodDetail.isCCProcessingID, Equal<Required<PaymentMethodDetail.isCCProcessingID>>>>>>>>>.
					Select(this, targetPM.PaymentMethodID, baseDetail.IsIdentifier, baseDetail.IsExpirationDate, baseDetail.IsOwnerName, baseDetail.IsCCProcessingID);
			return res;
		}

		protected virtual PaymentMethodDetail FindCCPID(PaymentMethod pm)
		{
			PaymentMethodDetail res = PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>,
				And<PaymentMethodDetail.isCCProcessingID, Equal<True>>>>.Select(this, pm.PaymentMethodID);
			return res;
		}
	}
}