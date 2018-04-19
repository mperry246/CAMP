namespace PX.Objects.AR.CCPaymentProcessing
{
	using PX.Data;
	using CR;

	public class CCCustomerInformationManagerGraph : PXGraph<CCCustomerInformationManagerGraph>
	{
		public virtual void GetOrCreatePaymentProfile<TPaymentMethodType, TDetialsType>(PXGraph graph
				, PXSelectBase<TPaymentMethodType> customerPaymentMethodView
				, PXSelectBase<TDetialsType> detailsView)
			where TPaymentMethodType : CustomerPaymentMethod, new()
			where TDetialsType : CustomerPaymentMethodDetail, new()
		{
			CCCustomerInformationManager.GetOrCreatePaymentProfile(graph, customerPaymentMethodView, detailsView);
		}

		public virtual void GetCreatePaymentProfileForm<TPaymentMethodType>(PXGraph graph,
			PXSelectBase<TPaymentMethodType> customerPaymentMethodView,
			TPaymentMethodType currentCustomerPaymentMethod)
			where TPaymentMethodType : CustomerPaymentMethod, new()
		{
			CCCustomerInformationManager.GetCreatePaymentProfileForm(graph, customerPaymentMethodView, currentCustomerPaymentMethod);
		}

		public virtual PXResultset<CustomerPaymentMethodDetail> GetAllCustomersCardsInProcCenter(PXGraph graph, int? BAccountID, string CCProcessingCenterID)
		{
			return CCCustomerInformationManager.GetAllCustomersCardsInProcCenter(graph, BAccountID, CCProcessingCenterID);
		}

		public virtual void GetManagePaymentProfileForm<TPaymentMethodType>(PXGraph graph, TPaymentMethodType currentCutomerPaymenMethod)
			where TPaymentMethodType : CustomerPaymentMethod, new()
		{
			CCCustomerInformationManager.GetManagePaymentProfileForm(graph, currentCutomerPaymenMethod);
		}

		public virtual void DeletePaymentProfile(PXGraph graph, PXSelectBase<CustomerPaymentMethod> customerPaymentMethodView, PXSelectBase<CustomerPaymentMethodDetail> detailsView)
		{
			CCCustomerInformationManager.DeletePaymentProfile(graph, customerPaymentMethodView, detailsView);
		}

		public virtual void GetNewPaymentProfiles<TPaymentMethodType, TDetialsType>(PXGraph graph,
			PXSelectBase<TPaymentMethodType> customerPaymentMethodView,
			PXSelectBase<TDetialsType> detailsView,
			TPaymentMethodType currentCustomerPaymentMethod)
			where TPaymentMethodType : CustomerPaymentMethod, new()
			where TDetialsType : CustomerPaymentMethodDetail, new()
		{
			CCCustomerInformationManager.GetNewPaymentProfiles(graph, customerPaymentMethodView, detailsView, currentCustomerPaymentMethod);
		}

		public virtual void GetPaymentProfile(PXGraph graph, PXSelectBase<CustomerPaymentMethod> customerPaymentMethodView, PXSelectBase<CustomerPaymentMethodDetail> detailsView)
		{
			CCCustomerInformationManager.GetPaymentProfile(graph, customerPaymentMethodView, detailsView);
		}
	}
}