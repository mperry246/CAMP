using PX.CCProcessingBase;
using PX.Common;
using PX.Data;
using PX.Objects.CA;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Helpers;

namespace PX.Objects.AR
{
	public class CustomerPaymentMethodMaint : PXGraph<CustomerPaymentMethodMaint, CustomerPaymentMethod>
	{
		#region Buttons

		public PXAction<CustomerPaymentMethod> viewBillAddressOnMap;

		[PXUIField(DisplayName = CR.Messages.ViewOnMap, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = true, FieldClass = CS.Messages.ValidateAddress)]
		[PXButton()]
		public virtual IEnumerable ViewBillAddressOnMap(PXAdapter adapter)
		{

			BAccountUtility.ViewOnMap(this.BillAddress.Current);
			return adapter.Get();
		}

		public PXAction<CustomerPaymentMethod> validateAddresses;
		[PXUIField(DisplayName = CS.Messages.ValidateAddresses, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton]
		public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
		{
			CustomerPaymentMethod doc = this.CustomerPaymentMethod.Current;
			if (doc != null && doc.BillAddressID.HasValue)
			{
				bool needSave = false;
				Save.Press();
				Address address = this.BillAddress.Current;
				if (address != null && address.IsValidated == false)
				{
					if (CS.PXAddressValidator.Validate<Address>(this, address, true))
						needSave = true;
				}
				if (needSave == true)
					this.Save.Press();

			}
			return adapter.Get();
		}

		public PXAction<CustomerPaymentMethod> createCCPaymentMethodHF;
		[PXUIField(DisplayName = "Create New", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton]
		public virtual IEnumerable CreateCCPaymentMethodHF(PXAdapter adapter)
		{
			PXTrace.WriteInformation($"CustomerPaymentMethodMaint.CreateCCPaymentMethodHF. CustomerPaymentMethod.Current.CCProcessingCenterID:{CustomerPaymentMethod.Current.CCProcessingCenterID}");
			var graph = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
			graph.GetCreatePaymentProfileForm(this, CustomerPaymentMethod, CustomerPaymentMethod.Current);
			return adapter.Get();
		}

		public PXAction<CustomerPaymentMethod> syncCCPaymentMethods;

	    [PXUIField(DisplayName = "Sync with Server", MapEnableRights = PXCacheRights.Select,
	        MapViewRights = PXCacheRights.Select, Visible = false)]
		[PXButton]
		public virtual IEnumerable SyncCCPaymentMethods(PXAdapter adapter)
		{
			PXTrace.WriteInformation($"CustomerPaymentMethodMaint.SyncCCPaymentMethods. CustomerPaymentMethod.Current.CCProcessingCenterID:{CustomerPaymentMethod.Current.CCProcessingCenterID}");
			List<CustomerPaymentMethod> cpmList = new List<CustomerPaymentMethod>();
			foreach (CustomerPaymentMethod cpm in adapter.Get<CustomerPaymentMethod>())
			{
				cpmList.Add(cpm);
			}
			var graph = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
			bool isIDFilled = CCProcessingHelper.IsCCPIDFilled(this, CustomerPaymentMethod.Current.PMInstanceID);
			if (!isIDFilled)
			{
				graph.GetNewPaymentProfiles(this, CustomerPaymentMethod, DetailsAll, CustomerPaymentMethod.Current);
			}
			else
			{
				graph.GetPaymentProfile(this, CustomerPaymentMethod, DetailsAll);
			}
			this.Save.Press();

			return cpmList;
		}

		public PXAction<CustomerPaymentMethod> manageCCPaymentMethodHF;
		[PXUIField(DisplayName = "Edit", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton]
		public virtual IEnumerable ManageCCPaymentMethodHF(PXAdapter adapter)
		{
			PXTrace.WriteInformation($"CustomerPaymentMethodMaint.ManageCCPaymentMethodHF. CustomerPaymentMethod.Current.CCProcessingCenterID:{CustomerPaymentMethod.Current.CCProcessingCenterID}");
			var graph = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
			graph.GetManagePaymentProfileForm(this, CustomerPaymentMethod.Current);
			return adapter.Get();
		}

		#endregion
		#region Public Selects
		public CustomerPaymentMethodMaint()
		{
			ARSetup setup = ARSetup.Current;
			this.Details.Cache.AllowInsert = false;
			this.Details.Cache.AllowDelete = false;
			PXUIFieldAttribute.SetEnabled<CustomerPaymentMethodDetail.detailID>(this.Details.Cache, null, false);
			FieldDefaulting.AddHandler<BAccountR.type>((sender, e) => { if (e.Row != null) e.NewValue = BAccountType.CustomerType; });
		}
		public PXSelect<CustomerPaymentMethod,
				Where<CustomerPaymentMethod.bAccountID, Equal<Optional<CustomerPaymentMethod.bAccountID>>>> CustomerPaymentMethod;
		public PXSelect<CustomerPaymentMethod,
				Where<CustomerPaymentMethod.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>,
						And<CustomerPaymentMethod.pMInstanceID, Equal<Current<CustomerPaymentMethod.pMInstanceID>>>>> CurrentCPM;

		public PXFilter<CustomerPaymentMethodInputMode> InputModeFilter;

		public PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>>>,
			Where<True, Equal<True>>,
			OrderBy<Asc<PaymentMethodDetail.orderIndex>>> Details;

		public PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>,
					And<PaymentMethodDetail.detailID, Equal<CustomerPaymentMethodDetail.detailID>,
					And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>,
				Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Current<CustomerPaymentMethod.pMInstanceID>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> DetailsAll;
		public PXSelect<PaymentMethodDetail,
				Where<PaymentMethodDetail.paymentMethodID, Equal<Optional<CustomerPaymentMethod.paymentMethodID>>,
				And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>> PMDetails;
		public PXSelect<PaymentMethod,
				Where<PaymentMethod.paymentMethodID, Equal<Optional<CustomerPaymentMethod.paymentMethodID>>>> PaymentMethodDef;

		public PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail,
			On<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>,
				And<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>,
					And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>,
			Where<PaymentMethodDetail.isCCProcessingID, Equal<True>, And<CustomerPaymentMethodDetail.pMInstanceID, Equal<Current<CustomerPaymentMethod.pMInstanceID>>>>> ccpIdDet;

		public PXSelect<Address, Where<Address.addressID, Equal<Optional<CustomerPaymentMethod.billAddressID>>>> BillAddress;
		public PXSelect<Contact, Where<Contact.contactID, Equal<Optional<CustomerPaymentMethod.billContactID>>>> BillContact;
		public PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<CustomerPaymentMethod.bAccountID>>>> Customer;

		public PXSelect<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>,
			And<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Current<CustomerPaymentMethod.cCProcessingCenterID>>,
			And<CustomerProcessingCenterID.customerCCPID, Equal<Optional<CustomerPaymentMethod.customerCCPID>>>>>> CustomerProcessingID;

		public PXSetup<ARSetup> ARSetup;
		
		#endregion

		#region Cache Attached
		[PXDBString(30, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Customer ID", Visibility = PXUIVisibility.Visible)]
		protected virtual void Customer_AcctCD_CacheAttached(PXCache sender)
		{
		}
        #endregion

        #region Select Delagates
        public IEnumerable billAddress()
		{
			CustomerPaymentMethod row = this.CustomerPaymentMethod.Current;
			if (row != null && row.BAccountID != null)
			{
				if (row.BillAddressID != null)
				{
					return PXSelect<Address, Where<Address.addressID, Equal<Required<Address.addressID>>>>.Select(this, row.BillAddressID);
				}
				else
				{
					return PXSelectJoin<Address, InnerJoin<Customer, On<Customer.defBillAddressID, Equal<Address.addressID>>>,
										Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Select(this, row.BAccountID);
				}
			}
			return null;
		}

		public IEnumerable billContact()
		{
			CustomerPaymentMethod row = this.CustomerPaymentMethod.Current;
			if (row != null && row.BAccountID != null)
			{
				if (row.BillContactID != null)
				{
					return PXSelect<Contact, Where<Contact.contactID, Equal<Required<Contact.contactID>>>>.Select(this, row.BillContactID);
				}
				else
				{
					return PXSelectJoin<Contact, InnerJoin<Customer, On<Customer.defBillContactID, Equal<Contact.contactID>>>,
										Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Select(this, row.BAccountID);
				}
			}
			return null;
		}

		public IEnumerable details()
		{
			bool isTokenized = CCProcessingHelper.IsTokenizedPaymentMethod(this, CustomerPaymentMethod.Current.PMInstanceID);
			bool isHF = CCProcessingHelper.IsHFPaymentMethod(this, CustomerPaymentMethod.Current.PMInstanceID);
			bool showToken = InputModeFilter.Current != null && InputModeFilter.Current.InputMode == InputModeType.Token && isTokenized;
			bool showDetails = InputModeFilter.Current == null || InputModeFilter.Current.InputMode == InputModeType.Details && !isHF;
			return CCProcessingHelper.GetPMdetails(this, CustomerPaymentMethod.Current.PMInstanceID, showToken, showDetails);
		}

		#endregion

		#region Main Record Events

		protected virtual void CustomerPaymentMethod_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
		{
			CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;
			if (row != null)
			{
				cache.SetDefaultExt<CustomerPaymentMethod.pMInstanceID>(row);
			}
		}

		protected virtual void CustomerPaymentMethodInputMode_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			CustomerPaymentMethodInputMode inputMode = e.Row as CustomerPaymentMethodInputMode;
			if (inputMode == null) return;
			bool? isVisible = null;
			foreach (PXEventSubscriberAttribute attribute in cache.GetAttributesReadonly<CustomerPaymentMethodInputMode.inputMode>(inputMode))
			{
				if (attribute is PXUIFieldAttribute)
				{
					isVisible = ((PXUIFieldAttribute) attribute).Visible;
				}
			}
			if (isVisible == false)
			{
				bool isTokenized = CCProcessingHelper.IsTokenizedPaymentMethod(this, CustomerPaymentMethod.Current?.PMInstanceID);
				bool isHFPM = CCProcessingHelper.IsHFPaymentMethod(this, CustomerPaymentMethod.Current?.PMInstanceID);
				inputMode.InputMode = isHFPM ? InputModeType.Token : InputModeType.Details;
			}
		}

		protected virtual void CustomerPaymentMethod_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;
			if (row != null)
			{
				if (row.BAccountID != null)
				{
					this.bAccountID = row.BAccountID;
				}

				PXUIFieldAttribute.SetEnabled(this.Details.Cache, null, true);
				PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.descr>(cache, row, false);
				PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.customerCCPID>(cache, row, string.IsNullOrEmpty(row.Descr));
				bool isTokenized = CCProcessingHelper.IsTokenizedPaymentMethod(this, CustomerPaymentMethod.Current.PMInstanceID);
				PXUIFieldAttribute.SetVisible<CustomerPaymentMethod.expirationDate>(cache, row, isTokenized);
				Details.Cache.AllowUpdate = (cache.GetStatus(row) == PXEntryStatus.Inserted) || !isTokenized;
				bool isHFPM = CCProcessingHelper.IsHFPaymentMethod(this, CustomerPaymentMethod.Current.PMInstanceID);
				this.createCCPaymentMethodHF.SetVisible(isHFPM);
				this.syncCCPaymentMethods.SetVisible(isHFPM);
				this.manageCCPaymentMethodHF.SetVisible(isHFPM);
				bool isIDFilled = CCProcessingHelper.IsCCPIDFilled(this, CustomerPaymentMethod.Current.PMInstanceID);
				this.createCCPaymentMethodHF.SetEnabled(isHFPM && !isIDFilled);
				this.manageCCPaymentMethodHF.SetEnabled(isHFPM && !string.IsNullOrEmpty(row.Descr));
				validateAddresses.SetEnabled(row.HasBillingInfo == true);

				if (!String.IsNullOrEmpty(row.PaymentMethodID))
				{
					PaymentMethod pmDef = (PaymentMethod)this.PaymentMethodDef.Select(row.PaymentMethodID);

					bool singleInstance = pmDef.ARIsOnePerCustomer ?? false;
					bool isIDMaskExists = false;
					if (!singleInstance)
					{
						foreach (PaymentMethodDetail iDef in this.PMDetails.Select(row.PaymentMethodID))
						{
							if ((iDef.IsIdentifier ?? false) && (!string.IsNullOrEmpty(iDef.DisplayMask)))
							{
								isIDMaskExists = true;
								break;
							}
						}
					}
					if (!(isIDMaskExists || singleInstance))
					{
						PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.descr>(cache, row, true);
					}
					PXDefaultAttribute.SetPersistingCheck<CustomerPaymentMethod.descr>(cache, row, (isIDMaskExists ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank));
					row.HasBillingInfo = pmDef.ARHasBillingInfo;
					bool integratedProcessing = pmDef.ARIsProcessingRequired == true;
					PXUIFieldAttribute.SetVisible<CustomerPaymentMethod.cCProcessingCenterID>(cache, row, integratedProcessing);
					if (!string.IsNullOrEmpty(row.CCProcessingCenterID))
					{
						PXUIFieldAttribute.SetVisible<CustomerPaymentMethod.customerCCPID>(cache, row, isTokenized);
						PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.cCProcessingCenterID>(cache, row, integratedProcessing && (!isTokenized || string.IsNullOrEmpty(row.Descr)));
                        bool needInputMode = isTokenized && !isHFPM;
						PXUIFieldAttribute.SetVisible<CustomerPaymentMethodInputMode.inputMode>(InputModeFilter.Cache, InputModeFilter.Current, needInputMode);
                        if (!needInputMode && InputModeFilter.Current!= null) 
                        {                       
                            InputModeFilter.Current.InputMode = isHFPM ? InputModeType.Token : InputModeType.Details;
                        }
					}

				}
				bool isInserted = (cache.GetStatus(e.Row) == PXEntryStatus.Inserted);
				PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.paymentMethodID>(cache, row, (isInserted || String.IsNullOrEmpty(row.PaymentMethodID)));
				if (!isInserted && (!String.IsNullOrEmpty(row.PaymentMethodID)))
				{
					this.MergeDetailsWithDefinition(row);
					CCProcTran ccTran = PXSelect<CCProcTran, Where<CCProcTran.pMInstanceID, Equal<Required<CCProcTran.pMInstanceID>>,
												And<CCProcTran.procStatus, Equal<CCProcStatus.finalized>,
												And<CCProcTran.tranStatus, Equal<CCTranStatusCode.approved>>>>>.Select(this, row.PMInstanceID);
					bool hasTransactions = (ccTran != null);
					this.Details.Cache.AllowDelete = !hasTransactions;
					PXUIFieldAttribute.SetEnabled(this.Details.Cache, null, !hasTransactions);
					if (!hasTransactions)
					{
						CCProcTran ccTranAny = PXSelect<CCProcTran, Where<CCProcTran.pMInstanceID, Equal<Required<CCProcTran.pMInstanceID>>>>.Select(this, row.PMInstanceID);
						hasTransactions = (ccTranAny != null);
					}
				}
				if (row.BAccountID != null)
				{
					Customer customer = (Customer)this.Customer.Select(row.BAccountID);
					row.IsBillContactSameAsMain = (row.BillContactID == null) || (customer.DefBillContactID == row.BillContactID);
					row.IsBillAddressSameAsMain = (row.BillAddressID == null) || (customer.DefBillAddressID == row.BillAddressID);
				}
				if (row.CashAccountID.HasValue)
				{
					PaymentMethodAccount pmAcct = PXSelect<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>,
														And<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>,
														And<PaymentMethodAccount.useForAR, Equal<True>>>>>.Select(this, row.CashAccountID, row.PaymentMethodID);
					PXUIFieldAttribute.SetWarning<CustomerPaymentMethod.cashAccountID>(cache, e.Row, pmAcct == null ? PXMessages.LocalizeFormatNoPrefixNLA(Messages.CashAccountIsNotConfiguredForPaymentMethodInAR, row.PaymentMethodID) : null);
				}
			}

			if (row.IsActive == false)
			{
				ARInvoice doc = PXSelectJoin<ARInvoice, InnerJoin<GL.Schedule, On<GL.Schedule.scheduleID, Equal<ARInvoice.scheduleID>, And<GL.Schedule.active, Equal<True>>>>,
									Where<ARInvoice.scheduled, Equal<True>, And<ARInvoice.pMInstanceID, Equal<Required<ARInvoice.pMInstanceID>>>>>.Select(this, row.PMInstanceID);
				if (doc != null)
				{
					cache.RaiseExceptionHandling<CustomerPaymentMethod.isActive>(row, row.IsActive, new PXSetPropertyException(Messages.InactiveCustomerPaymentMethodIsUsedInTheScheduledInvoices, PXErrorLevel.Warning));
				}
				else
				{
					cache.RaiseExceptionHandling<CustomerPaymentMethod.isActive>(row, null, null);
				}
			}
			else
			{
				cache.RaiseExceptionHandling<CustomerPaymentMethod.isActive>(row, null, null);
			}
			foreach (CustomerPaymentMethodDetail CPMDetail in Details.Select())
			{
				Details.Cache.RaiseRowSelected(CPMDetail);
			}
		}

		protected virtual void CustomerPaymentMethod_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
		{
			if ((e.Operation & PXDBOperation.Command) != PXDBOperation.Delete)
			{
				CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;
				PaymentMethod def = this.PaymentMethodDef.Select(row.PaymentMethodID);
				if (def != null && (def.ARIsOnePerCustomer ?? false))
				{
					CustomerPaymentMethod existing = PXSelect<CustomerPaymentMethod,
					Where<CustomerPaymentMethod.bAccountID, Equal<Required<CustomerPaymentMethod.bAccountID>>,
					And<CustomerPaymentMethod.paymentMethodID, Equal<Required<CustomerPaymentMethod.paymentMethodID>>,
					And<CustomerPaymentMethod.pMInstanceID, NotEqual<Required<CustomerPaymentMethod.pMInstanceID>>>>>>.Select(this, row.BAccountID, row.PaymentMethodID, row.PMInstanceID);
					if (existing != null)
					{
						throw new PXException(Messages.PaymentMethodIsAlreadyDefined);
					}
				}

				if (row != null)
				{
					Customer customer = Customer.Select(row.BAccountID);
					if (customer != null && customer.DefPaymentMethodID == row.PaymentMethodID)
					{
						PaymentMethod pm = PaymentMethodDef.Select();
						if (pm != null && pm.ARIsOnePerCustomer == true)
						{
							customer.DefPMInstanceID = row.PMInstanceID;
							Customer.Update(customer);
						}
					}

					if (!string.IsNullOrEmpty(row.CustomerCCPID))
					{
						CustomerProcessingCenterID test = CustomerProcessingID.Select(row.CustomerCCPID);
						if (test == null)
						{
							CustomerProcessingCenterID cPCID = new CustomerProcessingCenterID();
							cPCID.BAccountID = row.BAccountID;
							cPCID.CCProcessingCenterID = row.CCProcessingCenterID;
							cPCID.CustomerCCPID = row.CustomerCCPID;
							CustomerProcessingID.Insert(cPCID);
						}
					}
				}

			}
			else
			{
				CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;
				if (row != null)
				{
					Customer customer = Customer.Select(row.BAccountID);
					if (customer != null && customer.DefPaymentMethodID == row.PaymentMethodID)
					{
						PaymentMethod pm = PaymentMethodDef.Select(row.PaymentMethodID);
						if (pm != null)
						{
							if (pm.ARIsOnePerCustomer == true)
							{
								customer.DefPMInstanceID = pm.PMInstanceID;
								Customer.Update(customer);
							}
							else
							{
								PXResultset<CustomerPaymentMethod> otherMethods = PXSelect<CustomerPaymentMethod,
									Where<CustomerPaymentMethod.paymentMethodID, Equal<Required<CustomerPaymentMethod.paymentMethodID>>>>.Select(this, row.PaymentMethodID);
								if (otherMethods.Count == 0)
								{
									customer.DefPMInstanceID = null;
									customer.DefPaymentMethodID = null;
									Customer.Update(customer);
								}
							}
						}
					}
				}
			}
		}

		protected virtual void CustomerPaymentMethod_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
		{
			CustomerPaymentMethod row = e.Row as CustomerPaymentMethod;

			if (row.IsActive != true
				&& !cache.ObjectsEqual<CustomerPaymentMethod.isActive>(e.OldRow, e.Row))
			{
				Customer currCustomer = Customer.Select(row.BAccountID);

				if (currCustomer != null
					&& currCustomer.DefPMInstanceID == row.PMInstanceID)
				{
					currCustomer.DefPaymentMethodID = null;
					currCustomer.DefPMInstanceID = null;
					Customer.Update(currCustomer);
				}
			}
		}

		protected virtual void CustomerPaymentMethod_BAccountID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;
			if (this.bAccountID != null)
			{
				e.NewValue = this.bAccountID;
				e.Cancel = true;
			}
		}
		protected virtual void CustomerPaymentMethod_PaymentMethodID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;
			this.ClearDetails();
			this.AddDetails(row.PaymentMethodID);
			row.CashAccountID = null;
			cache.SetDefaultExt<CustomerPaymentMethod.cashAccountID>(e.Row);
			cache.SetDefaultExt<CustomerPaymentMethod.cCProcessingCenterID>(e.Row);
			PaymentMethod pmDef = this.PaymentMethodDef.Select(row.PaymentMethodID);
			if (pmDef.ARIsOnePerCustomer ?? false)
			{
				cache.SetValueExt<CustomerPaymentMethod.descr>(row, pmDef.Descr);
			}
			this.Details.View.RequestRefresh();
		}

		protected virtual void CustomerPaymentMethod_CCProcessingCenterID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			CustomerPaymentMethod row = e.Row as CustomerPaymentMethod;
			if (row == null) return;
			cache.SetDefaultExt<CustomerPaymentMethod.customerCCPID>(e.Row);
		}

		protected virtual void CustomerPaymentMethod_Descr_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;
			PaymentMethod def = this.PaymentMethodDef.Select(row.PaymentMethodID);
			if (!(def.ARIsOnePerCustomer ?? false))
			{
				CustomerPaymentMethod existing = PXSelect<CustomerPaymentMethod,
				Where<CustomerPaymentMethod.bAccountID, Equal<Required<CustomerPaymentMethod.bAccountID>>,
				And<CustomerPaymentMethod.paymentMethodID, Equal<Required<CustomerPaymentMethod.paymentMethodID>>,
				And<CustomerPaymentMethod.pMInstanceID, NotEqual<Required<CustomerPaymentMethod.pMInstanceID>>,
				And<CustomerPaymentMethod.descr, Equal<Required<CustomerPaymentMethod.descr>>,
				And<CustomerPaymentMethod.isActive, Equal<True>>>>>>>.Select(this, row.BAccountID, row.PaymentMethodID, row.PMInstanceID, row.Descr);
				if (existing != null)
				{
					cache.RaiseExceptionHandling<CustomerPaymentMethod.descr>(row, row.Descr, new PXSetPropertyException(Messages.CustomerPMInstanceHasDuplicatedDescription, PXErrorLevel.Warning));
				}
			}
		}
		protected virtual void CustomerPaymentMethodInputMode_InputMode_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			if (e.Row == null) return;

		}


		protected virtual void CustomerPaymentMethod_IsBillAddressSameAsMain_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;
			if (row != null)
			{
				if (row.IsBillAddressSameAsMain == true)
				{
					Customer def = this.Customer.Select(row.BAccountID);
					if (row.BillAddressID.HasValue)
					{
						Address addr = this.BillAddress.Select(row.BillAddressID);
						if (addr != null && addr.AddressID != def.DefBillAddressID && addr.AddressID != def.DefAddressID)
						{
							this.BillAddress.Delete(addr);
						}
					}
					//row.BillAddressID = def.DefBillAddressID;
					row.BillAddressID = null;
				}
				else
				{
					int? id = row.BillAddressID;
					if (!id.HasValue)
					{
						Customer def = this.Customer.Select(row.BAccountID);
						id = def.DefBillAddressID;
						id = null;
					}
					Address addr = this.BillAddress.Select(id);
					if (addr != null)
					{
						Address copy = (Address)this.BillAddress.Cache.CreateCopy(addr);
						copy.AddressID = null;
						addr = this.BillAddress.Insert(copy);
						row.BillAddressID = addr.AddressID;
					}
				}
				//this.BillAddress.View.RequestRefresh();
			}
		}
		protected virtual void CustomerPaymentMethod_IsBillContactSameAsMain_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;
			if (row != null)
			{
				if (row.IsBillContactSameAsMain == true)
				{
					Customer def = this.Customer.Select(row.BAccountID);
					if (row.BillContactID.HasValue)
					{
						Contact addr = this.BillContact.Select(row.BillContactID);
						if (addr != null && addr.ContactID != def.DefBillContactID && addr.ContactID != def.DefContactID)
						{
							this.BillContact.Delete(addr);
						}
					}
					row.BillContactID = null;
				}
				else
				{
					int? id = row.BillContactID;
					if (!id.HasValue)
					{
						Customer def = this.Customer.Select(row.BAccountID);
						id = def.DefBillContactID;
					}
					Contact addr = this.BillContact.Select(id);
					if (addr != null)
					{
						Contact copy = (Contact)this.BillContact.Cache.CreateCopy(addr);
						copy.ContactID = null;
						addr = this.BillContact.Insert(copy);
						row.BillContactID = addr.ContactID;
					}
				}
				//this.BillContact.View.RequestRefresh();
			}
		}

		protected virtual void CustomerPaymentMethod_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
		{
			CustomerPaymentMethod row = (CustomerPaymentMethod)e.Row;

			if (row != null)
			{
				Customer def = this.Customer.Select(row.BAccountID);
				PXEntryStatus status = this.CustomerPaymentMethod.Cache.GetStatus(e.Row);
				if (CCProcTranHelper.HasUnfinishedCCTrans(this, row))
				{
					throw new PXSetPropertyException(Messages.CreditCardHasUncapturedTransactions);
				}

				if (row.BillContactID.HasValue)
				{
					Contact addr = this.BillContact.Select(row.BillContactID);
					if (addr != null && addr.ContactID != def.DefBillContactID
						&& addr.ContactID != def.DefContactID)
					{
						this.BillContact.Delete(addr);
					}
				}
				if (row.BillAddressID.HasValue)
				{
					Address addr = this.BillAddress.Select(row.BillAddressID);
					if (addr != null && addr.AddressID != def.DefBillAddressID
							&& addr.AddressID != def.DefAddressID)
					{
						this.BillAddress.Delete(addr);
					}
				}
			}
		}
		#endregion

		#region PM Details Events
		protected virtual void CustomerPaymentMethodDetail_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
		{
			CustomerPaymentMethodDetail row = (CustomerPaymentMethodDetail)e.Row;
			PaymentMethodDetail def = this.FindTemplate(row);
			if (def != null && def.IsIdentifier == true)
			{
				this.CustomerPaymentMethod.Current.Descr = null;
			}
		}
		protected virtual void CustomerPaymentMethodDetail_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			CustomerPaymentMethodDetail row = (CustomerPaymentMethodDetail)e.Row;
			if (row != null && !String.IsNullOrEmpty(row.PaymentMethodID))
			{
				PaymentMethodDetail iTempl = this.FindTemplate(row);
				if (iTempl != null)
				{
					bool isTokenized = CCProcessingHelper.IsTokenizedPaymentMethod(this, CustomerPaymentMethod.Current.PMInstanceID);
					bool isRequired = (iTempl.IsRequired ?? false);
					PXDefaultAttribute.SetPersistingCheck<CustomerPaymentMethodDetail.value>(cache, row, (isRequired) ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
					bool showDecripted = !(iTempl.IsEncrypted ?? false);
					PXRSACryptStringAttribute.SetDecrypted<CustomerPaymentMethodDetail.value>(cache, row, showDecripted);
				}
				else
				{
					PXDefaultAttribute.SetPersistingCheck<CustomerPaymentMethodDetail.value>(cache, row, PXPersistingCheck.Nothing);
				}
			}
		}

		protected virtual void CustomerPaymentMethodDetail_Value_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
		{
			CustomerPaymentMethodDetail row = e.Row as CustomerPaymentMethodDetail;
			PaymentMethodDetail def = this.FindTemplate(row);
			if (def != null)
			{
				if (def.IsCCProcessingID == true)
				{
					var graph = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
					PXResultset<CustomerPaymentMethodDetail> otherCards = graph.GetAllCustomersCardsInProcCenter(this, CustomerPaymentMethod.Current.BAccountID, 
						CustomerPaymentMethod.Current.CCProcessingCenterID);

					PXResult<CustomerPaymentMethodDetail> duplicate = otherCards
						.FirstOrDefault(result => ((CustomerPaymentMethodDetail)result).Value == (string)e.NewValue);

					if (duplicate != null)
					{
						CustomerPaymentMethodDetail duplicateDet = duplicate;
						CustomerPaymentMethod dublicateCPM = duplicate.GetItem<CustomerPaymentMethod>();

						throw new PXSetPropertyException(Messages.DuplicateCCProcessingID, row.Value, dublicateCPM.Descr);
					}
				}
			}
		}

		protected virtual void CustomerPaymentMethodDetail_Value_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			CustomerPaymentMethodDetail row = e.Row as CustomerPaymentMethodDetail;
			PaymentMethodDetail def = this.FindTemplate(row);
			if (def != null)
			{
				if (def.IsIdentifier == true)
				{
					bool isTokenized = CCProcessingHelper.IsTokenizedPaymentMethod(this, CustomerPaymentMethod.Current.PMInstanceID);
					string id;
					if (isTokenized)
					{
						id = IDObfuscator.AdjustedMaskID(row.Value, def.DisplayMask);
					}
					else
					{
						id = IDObfuscator.MaskID(row.Value, def.DisplayMask);
					}
					
					if (this.CustomerPaymentMethod.Current.Descr != id)
					{
						CustomerPaymentMethod parent = this.CustomerPaymentMethod.Current;
						this.CustomerPaymentMethod.Cache.SetValueExt<CustomerPaymentMethod.descr>(parent, FormatDescription(parent.PaymentMethodID, id));
						this.CustomerPaymentMethod.Update(parent);
					}
				}

				if ((def.IsExpirationDate ?? false) && !String.IsNullOrEmpty(row.Value))
				{
					CustomerPaymentMethod parent = this.CustomerPaymentMethod.Current;
					this.CustomerPaymentMethod.Cache.SetValueExt<CustomerPaymentMethod.expirationDate>(parent, CustomerPaymentMethodMaint.ParseExpiryDate(this, parent, row.Value));
					this.CustomerPaymentMethod.Update(parent);
				}				
			}
		}


		#endregion

		#region Address & Contact Events
		protected virtual void Address_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			Address row = (Address)e.Row;
			bool enabled = false;
			if (row != null)
			{
				CustomerPaymentMethod parent = this.CustomerPaymentMethod.Current;
				if (parent != null && parent.BillAddressID == row.AddressID && parent.IsBillAddressSameAsMain == false)
				{
					enabled = true;
				}
			}
			PXUIFieldAttribute.SetEnabled(cache, row, null, enabled);
		}

		protected virtual void Contact_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			Contact row = (Contact)e.Row;
			bool enabled = false;
			if (row != null)
			{
				CustomerPaymentMethod parent = this.CustomerPaymentMethod.Current;
				if (parent != null && parent.BillContactID == row.ContactID
					&& parent.IsBillContactSameAsMain == false)
				{
					enabled = true;
				}
			}
			PXUIFieldAttribute.SetEnabled(cache, row, null, enabled);
		}
		#endregion

		#region Internal Functions
		protected virtual PaymentMethodDetail FindTemplate(CustomerPaymentMethodDetail aDet)
		{
			PaymentMethodDetail res = PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>,
				And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>,
				And<PaymentMethodDetail.detailID, Equal<Required<PaymentMethodDetail.detailID>>>>>>.Select(this, aDet.PaymentMethodID, aDet.DetailID);
			return res;
		}
		protected virtual void ClearDetails()
		{
			foreach (CustomerPaymentMethodDetail iDet in this.DetailsAll.Select())
			{
				this.DetailsAll.Delete(iDet);
			}
		}
		protected virtual void AddDetails(string aPaymentMethodID)
		{
			if (!String.IsNullOrEmpty(aPaymentMethodID))
			{
				foreach (PaymentMethodDetail it in this.PMDetails.Select(aPaymentMethodID))
				{
					CustomerPaymentMethodDetail det = new CustomerPaymentMethodDetail();
					det.DetailID = it.DetailID;
					det = this.Details.Insert(det);
				}
			}
		}

		protected virtual void MergeDetailsWithDefinition(CustomerPaymentMethod row)
		{
			string aPaymentMethod = row.PaymentMethodID;
			if (aPaymentMethod != this.mergedPaymentMethod)
			{
				List<PaymentMethodDetail> toAdd = new List<PaymentMethodDetail>();
				foreach (PaymentMethodDetail it in this.PMDetails.Select(aPaymentMethod))
				{
					CustomerPaymentMethodDetail detail = null;
					foreach (CustomerPaymentMethodDetail iPDet in this.Details.Select())
					{
						if (iPDet.DetailID == it.DetailID)
						{
							detail = iPDet;
							break;
						}
					}
					if (detail == null && !(it.DetailID == CreditCardAttributes.CVV && row.CVVVerifyTran != null))
					{
						toAdd.Add(it);
					}
				}
				using (ReadOnlyScope rs = new ReadOnlyScope(this.Details.Cache))
				{
					foreach (PaymentMethodDetail it in toAdd)
					{
						CustomerPaymentMethodDetail detail = new CustomerPaymentMethodDetail();
						detail.DetailID = it.DetailID;
						detail = this.Details.Insert(detail);
					}
					if (toAdd.Count > 0)
					{
						this.Details.View.RequestRefresh();
					}
				}
				this.mergedPaymentMethod = aPaymentMethod;
			}
		}
		#endregion


		#region Utilities
		public static string FormatDescription(string aPaymentMethodID, string aMaskedID)
		{
			return String.Format("{0}:{1}", aPaymentMethodID, aMaskedID);
		}
		public static DateTime? ParseExpiryDate(PXGraph graph, CustomerPaymentMethod cpm, string aValue)
		{
			DateTime datetime;
			string dateStringFormat = null;
			if (graph != null && cpm != null && cpm.PMInstanceID != null)
			{
				dateStringFormat = CCProcessingHelper.GetExpirationDateFormat(graph, cpm.CCProcessingCenterID);
			}

			if (!(!String.IsNullOrEmpty(dateStringFormat) 
					&& DateTime.TryParseExact(aValue, dateStringFormat, null, System.Globalization.DateTimeStyles.None, out datetime)
				|| String.IsNullOrEmpty(dateStringFormat)
					&& (DateTime.TryParseExact(aValue, "Myyyy", null, System.Globalization.DateTimeStyles.None, out datetime)
					|| DateTime.TryParseExact(aValue, "Myy", null, System.Globalization.DateTimeStyles.None, out datetime))))
			{
				return null;
			}
			return datetime;
		}

		public static class IDObfuscator
		{
			private const char CS_UNDERSCORE = '_';
			private const char CS_DASH = '-';
			private const char CS_DOT = '.';
			private const char CS_MASKER = '*';
			private const char CS_NUMBER_MASK_0 = '#';
			private const char CS_NUMBER_MASK_1 = '0';
			private const char CS_NUMBER_MASK_2 = '9';
			private const char CS_ANY_CHAR_0 = '&';
			private const char CS_ANY_CHAR_1 = 'C';
			private const char CS_ALPHANUMBER_MASK_0 = 'a';
			private const char CS_ALPHANUMBER_MASK_1 = 'A';
			private const char CS_ALPHA_MASK_0 = 'L';
			private const char CS_ALPHA_MASK_1 = '?';

			public static string MaskID(string aID, string aEditMask, string aDisplayMask)
			{
				if (string.IsNullOrEmpty(aID) || string.IsNullOrWhiteSpace(aDisplayMask)) return aID;
				if (!string.IsNullOrEmpty(aEditMask))
				{
					int mskLength = aEditMask.Length;
					int displayMskLength = aDisplayMask.Length;
					int valueLength = aID.Length;
					char[] entryMask = aEditMask.ToCharArray();
					char[] displayMask = aDisplayMask.ToCharArray();
					char[] value = aID.ToCharArray();
					int valueIndex = 0;
					int displayMaskIndex = 0;
					StringBuilder res = new StringBuilder(mskLength);
					for (int i = 0; i < mskLength; i++)
					{
						if (valueIndex >= valueLength) break;
						if (displayMaskIndex >= displayMskLength)
						{
							res.Append(CS_MASKER);
						}
						else
						{
							if (IsSymbol(entryMask[i]))
							{
								if (IsSymbol(displayMask[displayMaskIndex]))
									res.Append(value[valueIndex]);
								else
									res.Append(CS_MASKER);
								valueIndex++;
								displayMaskIndex++;
							}
							else
							{
								if (IsSeparator(entryMask[i]) && IsSeparator(displayMask[displayMaskIndex]))
								{
									res.Append(displayMask[displayMaskIndex]);
									displayMaskIndex++;
								}
								//Any other characters are omited
							}
						}
					}
					return res.ToString();
				}
				return MaskID(aID, aDisplayMask);
			}

			public static string MaskID(string aID, string aDisplayMask)
			{
				if (string.IsNullOrEmpty(aID) || string.IsNullOrEmpty(aDisplayMask) || string.IsNullOrEmpty(aDisplayMask.Trim())) return aID;
				int mskLength = aDisplayMask.Length;
				int valueLength = aID.Length;
				char[] displayMask = aDisplayMask.ToCharArray();
				char[] value = aID.ToCharArray();
				int valueIndex = 0;
				StringBuilder res = new StringBuilder(mskLength);
				for (int i = 0; i < mskLength; i++)
				{
					if (valueIndex >= valueLength) break;
					if (IsSymbol(displayMask[i]))
					{
						res.Append(value[valueIndex]);
						valueIndex++;
					}
					else
					{
						//Any other characters are treated as separator and are omited
						if (IsSeparator(displayMask[i]))
						{
							res.Append(displayMask[i]);
						}
						else
						{
							res.Append(CS_MASKER);
							valueIndex++;
						}
					}
				}
				return res.ToString();
			}

			public static string AdjustedMaskID(string aID, string aDisplayMask)
			{
				string adjustedID = aID;
				int maskSymbolsLength = aDisplayMask.ToArray().Where(symbol => !IsSeparator(symbol)).Count();
				if (aID.Length > maskSymbolsLength)
				{
					adjustedID = aID.Substring(aID.Length - maskSymbolsLength);
				}
				else if (aID.Length < maskSymbolsLength)
				{
					adjustedID = aID.PadLeft(maskSymbolsLength, '0');
				}

				return MaskID(adjustedID, aDisplayMask);
			}

		    public static string GetMaskByID(PXGraph graph, string aID, string aDisplayMask, int? PMInstanceID)
		    {
                if (CCProcessingHelper.IsTokenizedPaymentMethod(graph, PMInstanceID))
                {
                    return CustomerPaymentMethodMaint.IDObfuscator.AdjustedMaskID(aID, aDisplayMask);
                }

                return CustomerPaymentMethodMaint.IDObfuscator.MaskID(aID, aDisplayMask);
            }

			//This Function is intended to restore value to masked if Displayed Mask was Used as Entry Mask. 
			//Mask Separator characters  may  be removed (optionally).

			public static string RestoreToMasked(string aValue, string aDisplayMask, string aMissedValuePlaceholder, bool aRemoveSeparators)
			{
				return RestoreToMasked(aValue, aDisplayMask, aMissedValuePlaceholder, aRemoveSeparators, false, CS_MASKER.ToString(), false);
			}

			public static string RestoreToMasked(string aValue, string aDisplayMask, string aMissedValuePlaceholder, bool aSkipSeparators, bool aReplaceMaskChars, string aNewMasker, bool aMergeNonValue)
			{
				char[] displayMask = aDisplayMask != null ? aDisplayMask.ToCharArray() : string.Empty.ToCharArray();
				char[] value = aValue != null ? aValue.ToCharArray() : string.Empty.ToCharArray();
				int valueIndex = 0;
				int mskLength = aDisplayMask != null ? aDisplayMask.Length : 0;
				int valueLength = aValue != null ? aValue.Length : 0;
				StringBuilder res = new StringBuilder(mskLength);
				bool lastAddedIsValue = false;
				for (int i = 0; i < mskLength; i++)
				{
					//if (valueIndex >= valueLength) break;
					if (IsSymbol(displayMask[i]))
					{
						if (valueIndex < valueLength && !Char.IsWhiteSpace(value[valueIndex]))
						{
							res.Append(value[valueIndex]);
						}
						else
						{
							res.Append(aMissedValuePlaceholder);
						}
						valueIndex++;
						lastAddedIsValue = true;
					}
					else
					{
						if (!aSkipSeparators || IsMasked(displayMask[i]))
						{
							if (aReplaceMaskChars)
							{
								if (!aMergeNonValue || lastAddedIsValue)
									res.Append(aNewMasker);
							}
							else
							{
								res.Append(displayMask[i]);
							}
							lastAddedIsValue = false;
						}
					}
				}
				return res.ToString();
			}

			private static bool IsSeparator(char aCh)
			{
				return aCh == CS_UNDERSCORE || aCh == CS_DASH || aCh == CS_DOT;
			}
			private static bool IsMasked(char aCh)
			{
				return aCh == CS_MASKER;
			}
			private static bool IsSymbol(char aCh)
			{
				switch (aCh)
				{
					case CS_NUMBER_MASK_0:
					case CS_NUMBER_MASK_1:
					case CS_NUMBER_MASK_2:
					case CS_ALPHANUMBER_MASK_0:
					case CS_ALPHANUMBER_MASK_1:
					case CS_ANY_CHAR_0:
					case CS_ANY_CHAR_1:
					case CS_ALPHA_MASK_0:
					case CS_ALPHA_MASK_1:
						return true;
				}
				return false;
			}


		}
		#endregion

		#region Private Functions
		private int? bAccountID;
		private string mergedPaymentMethod;
		#endregion

		public override void Persist()
		{
			bool isCPMDeleting = CustomerPaymentMethod.Cache.Deleted.Count() != 0;
			bool isDetailsInserting = DetailsAll.Cache.Inserted.Count() != 0;

			var graph = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
			if (!isCPMDeleting && isDetailsInserting && !string.IsNullOrEmpty(CustomerPaymentMethod.Current.CCProcessingCenterID)
				&& CCProcessingHelper.IsTokenizedPaymentMethod(this, CustomerPaymentMethod.Current.PMInstanceID))
			{
				graph.GetOrCreatePaymentProfile(this, CustomerPaymentMethod, DetailsAll);
			}
			//assuming only one record can be deleted from promary view of this graph at a time
			else if (isCPMDeleting && CCProcessingHelper.IsTokenizedPaymentMethod(this, null))
			{
				graph.DeletePaymentProfile(this, CustomerPaymentMethod, Details);
			}
			base.Persist();
		}

	}
}
