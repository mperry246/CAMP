using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using PX.Common;
using PX.Data;

using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.CR;
using PX.Objects.AP.MigrationMode;
using PX.Objects.EP;


namespace PX.Objects.AP
{
	[Serializable]
	public class AdjustedNotFoundException : PXException
	{
		public AdjustedNotFoundException(): base(ErrorMessages.ElementDoesntExist, Messages.APInvoice) {}
		public AdjustedNotFoundException(SerializationInfo info, StreamingContext context): base(info, context)
		{
			PXReflectionSerializer.RestoreObjectProps(this, info);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			PXReflectionSerializer.GetObjectData(this, info);
			base.GetObjectData(info, context);
		}

	}

	public class APPaymentEntry : APDataEntryGraph<APPaymentEntry, APPayment>
	{
		#region Cache Attached Events
		#region PTInstTran
		#region PTInstanceID

		[PXDBInt()]
		[PXDBDefault(typeof(APPayment.pTInstanceID))]
		protected virtual void PTInstTran_PTInstanceID_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region OrigModule
		[PXDBString(2, IsFixed = true)]
		[PXDefault(BatchModule.AP)]
		[PXUIField(DisplayName = "Module")]
		protected virtual void PTInstTran_OrigModule_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region OrigTranType
		[PXDBString(3)]
		[PXDBDefault(typeof(APPayment.docType))]
		[PXUIField(DisplayName = "Tran. Type")]
		protected virtual void PTInstTran_OrigTranType_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region OrigRefNbr

		[PXDBString(15, IsUnicode = true)]
		[PXDBDefault(typeof(APPayment.refNbr))]
		[PXUIField(DisplayName = "Ref. Nbr.")]
		[PXParent(typeof(Select<APPayment,
							Where<APPayment.docType, Equal<Current<PTInstTran.origTranType>>,
							And<APPayment.refNbr, Equal<Current<PTInstTran.origRefNbr>>>>>))]
		protected virtual void PTInstTran_OrigRefNbr_CacheAttached(PXCache sender)
		{
		}

		#endregion
		#region ExtRefNbr
		[PXDBString(15, IsUnicode = true)]
		[PXDBDefault(typeof(APPayment.extRefNbr), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Ext. Ref. Nbr.")]
		protected virtual void PTInstTran_ExtRefNbr_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region CashAccountID

		[PXDBInt()]
		[PXDBDefault(typeof(APPayment.cashAccountID))]
		protected virtual void PTInstTran_CashAccountID_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region TranDate

		[PXDBDate()]
		[PXDBDefault(typeof(APPayment.docDate))]
		[PXUIField(DisplayName = "Tran. Date")]
		protected virtual void PTInstTran_TranDate_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region TranDesc
		[PXDBString(60, IsUnicode = true)]
		[PXDBDefault(typeof(APPayment.docDesc), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Descr")]
		protected virtual void PTInstTran_TranDesc_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region DrCr

		[PXDBString(1, IsFixed = true)]
		[PXDBDefault(typeof(APPayment.drCr))]
		protected virtual void PTInstTran_DrCr_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region CuryID

		[PXDBString(5, IsUnicode = true)]
		[PXDBDefault(typeof(APPayment.curyID))]
		[PXSelector(typeof(Currency.curyID), CacheGlobal = true)]
		[PXUIField(DisplayName = "Currency ID")]
		protected virtual void PTInstTran_CuryID_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region CuryInfoID

		[PXDBLong]
		[PXDBDefault(typeof(APPayment.curyInfoID))]
		protected virtual void PTInstTran_CuryInfoID_CacheAttached(PXCache sender)
		{
		}

		#endregion
		#region CuryTranAmt
		[PXDBDecimal(4)]
		[PXDBDefault(typeof(APPayment.curyOrigDocAmt))]
		protected virtual void PTInstTran_CuryTranAmt_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region TranAmt
		[PXDBDecimal(4)]
		[PXDBDefault(typeof(APPayment.origDocAmt), DefaultForUpdate = true)]
		protected virtual void PTInstTran_TranAmt_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region Hold

		[PXDBBool()]
		[PXDBDefault(typeof(APPayment.hold))]
		[PXUIField(DisplayName = "Hold")]
		protected virtual void PTInstTran_Hold_CacheAttached(PXCache sender)
		{
		}

		#endregion
		#region VendorID

		[PXDBInt()]
		[PXDBDefault(typeof(APPayment.vendorID))]
		[PXUIField(DisplayName = "Vendor ID")]
		protected virtual void PTInstTran_VendorID_CacheAttached(PXCache sender)
		{
		}
		#endregion
		#region TranSource

		[PXDBString(2, IsFixed = true)]
		[PXDefault("AP")]
		[PXUIField(DisplayName = "Source")]
		protected virtual void PTInstTran_TranSource_CacheAttached(PXCache sender)
		{
		}
		#endregion

		#endregion
		#region APAdjust
		[PXMergeAttributes(Method = MergeMethod.Append)]
		[BatchNbr(typeof(Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>),
			IsMigratedRecordField = typeof(APAdjust.isMigratedRecord))]
		protected virtual void APAdjust_AdjBatchNbr_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Switch<
			Case<Where<
				APAdjust.adjgDocType, Equal<Current<APPayment.docType>>,
				And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>>>,
				ARAdjust.adjType.adjusted>,
			ARAdjust.adjType.adjusting>))]
		protected virtual void APAdjust_AdjType_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Switch<
			Case<Where<APAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>, APAdjust.adjdDocType,
			Case<Where<APAdjust.adjType, Equal<ARAdjust.adjType.adjusting>>, APAdjust.adjgDocType>>>))]
		protected virtual void APAdjust_DisplayDocType_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Switch<
			Case<Where<APAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>, APAdjust.adjdRefNbr,
			Case<Where<APAdjust.adjType, Equal<ARAdjust.adjType.adjusting>>, APAdjust.adjgRefNbr>>>))]
		protected virtual void APAdjust_DisplayRefNbr_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Selector<APAdjust.displayRefNbr, Standalone.APRegister.docDate>))]
		protected virtual void APAdjust_DisplayDocDate_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Selector<APAdjust.displayRefNbr, Standalone.APRegister.docDesc>))]
		protected virtual void APAdjust_DisplayDocDesc_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Selector<APAdjust.displayRefNbr, Standalone.APRegister.curyID>))]
		protected virtual void APAdjust_DisplayCuryID_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[CurrencyInfo]
		[PXFormula(typeof(IIf<
			Where<APAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>,
			APAdjust.adjgCuryInfoID,
			APAdjust.adjdCuryInfoID>))]
		protected virtual void APAdjust_DisplayCuryInfoID_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(IIf<
			Where<APAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>,
			APAdjust.curyAdjgAmt,
			APAdjust.curyAdjdAmt>))]
		protected virtual void APAdjust_DisplayCuryAmt_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(IIf<
			Where<APAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>,
			APAdjust.curyAdjgDiscAmt,
			APAdjust.curyAdjdDiscAmt>))]
		protected virtual void APAdjust_DisplayCuryDiscAmt_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(IIf<
			Where<APAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>,
			APAdjust.curyAdjgWhTaxAmt,
			APAdjust.curyAdjdWhTaxAmt>))]
		protected virtual void APAdjust_DisplayCuryWhTaxAmt_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(IIf<
			Where<APAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>,
			APAdjust.curyAdjgPPDAmt,
			APAdjust.curyAdjdPPDAmt>))]
		protected virtual void APAdjust_DisplayCuryPPDAmt_CacheAttached(PXCache sender) { }

		#endregion
		#region EP Approval
		[PXDBDate]
		[PXDefault(typeof(APPayment.docDate), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
		{
		}

		[PXDBInt]
		[PXDefault(typeof(APPayment.vendorID), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
		{
		}

		[PXDBString(60, IsUnicode = true)]
		[PXDefault(typeof(APPayment.docDesc), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
		{
		}

		[PXDBLong]
		[CurrencyInfo(typeof(APPayment.curyInfoID))]
		protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
		{
		}

		[PXDBDecimal]
		[PXDefault(typeof(APPayment.curyOrigDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
		{
		}

		[PXDBDecimal]
		[PXDefault(typeof(APPayment.origDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
		{
		}

		protected virtual void EPApproval_SourceItemType_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Document.Current != null)
			{
				e.NewValue = new APDocType.ListAttribute()
					.ValueLabelDic[Document.Current.DocType];

				e.Cancel = true;
			}
		}
		protected virtual void EPApproval_Details_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Document.Current != null)
			{
				e.NewValue = EPApprovalHelper.BuildEPApprovalDetailsString(sender, Document.Current);
			}
		}

		#endregion
		#endregion

		/// <summary>
		/// Necessary for proper cache resolution inside selector
		/// on <see cref="APAdjust.DisplayRefNbr"/>.
		/// </summary>
		public PXSelect<Standalone.APRegister> dummy_register;

		public ToggleCurrency<APPayment> CurrencyView;

		[PXViewName(Messages.APPayment)]
		[PXCopyPasteHiddenFields(typeof(APPayment.extRefNbr))]
		public PXSelectJoin<APPayment,
			LeftJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>>,
			Where<APPayment.docType, Equal<Optional<APPayment.docType>>,
			And<Where<Vendor.bAccountID, IsNull,
			Or<Match<Vendor, Current<AccessInfo.userName>>>>>>> Document;
		public PXSelect<APPayment, Where<APPayment.docType, Equal<Current<APPayment.docType>>, And<APPayment.refNbr, Equal<Current<APPayment.refNbr>>>>> CurrentDocument;
		[PXViewName(Messages.APAdjust)]
		[PXCopyPasteHiddenView]
		public PXSelectJoin<APAdjust, LeftJoin<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<APAdjust.adjNbr, Equal<Current<APPayment.lineCntr>>>>>> Adjustments;
		public PXSelect<APAdjust, Where<APAdjust.adjgDocType, Equal<Optional<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Optional<APPayment.refNbr>>, And<APAdjust.adjNbr, Equal<Optional<APPayment.lineCntr>>>>>> Adjustments_Raw;

		public PXSelectJoin<
			APAdjust,
				InnerJoinSingleTable<APInvoice,
					On<APInvoice.docType, Equal<APAdjust.adjdDocType>,
					And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>,
				InnerJoin<Standalone.APRegisterAlias,
					On<Standalone.APRegisterAlias.docType, Equal<APAdjust.adjdDocType>,
					And<Standalone.APRegisterAlias.refNbr, Equal<APAdjust.adjdRefNbr>>>>>,
			Where<
				APAdjust.adjgDocType, Equal<Current<APPayment.docType>>,
				And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>,
				And<APAdjust.adjNbr, Equal<Current<APPayment.lineCntr>>,
				And<APAdjust.isInitialApplication, NotEqual<True>>>>>> 
			Adjustments_Invoices;

		public PXSelectJoin<
			APAdjust,
				InnerJoinSingleTable<APPayment,
					On<APPayment.docType, Equal<APAdjust.adjdDocType>,
					And<APPayment.refNbr, Equal<APAdjust.adjdRefNbr>>>,
				LeftJoinSingleTable<APInvoice,
					On<APInvoice.docType, Equal<APAdjust.adjdDocType>,
					And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>,
				InnerJoin<Standalone.APRegisterAlias,
					On<Standalone.APRegisterAlias.docType, Equal<APAdjust.adjdDocType>,
					And<Standalone.APRegisterAlias.refNbr, Equal<APAdjust.adjdRefNbr>>>>>>,
			Where<
				APAdjust.adjgDocType, Equal<Current<APPayment.docType>>,
				And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>,
				And<APAdjust.adjNbr, Equal<Current<APPayment.lineCntr>>,
				And<APInvoice.refNbr, IsNull>>>>>
			Adjustments_Payments;

		/// <summary>
		/// The released applications of the current document.
		/// </summary>
		[PXCopyPasteHiddenView]
		public PXSelectJoin<
			APAdjust,
			LeftJoin<APInvoice,
				On<APInvoice.docType, Equal<APAdjust.adjdDocType>,
				And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>>,
			Where<
				APAdjust.adjgDocType, Equal<Current<APPayment.docType>>,
				And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>,
				And<APAdjust.adjNbr, Less<Current<APPayment.lineCntr>>>>>>
			Adjustments_History;

		[PXViewName(Messages.APAdjustHistory)]
		public PXSelect<APAdjust> Adjustments_print;

		public PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<APPayment.curyInfoID>>>> currencyinfo;
		[PXReadOnlyView]
		public PXSelect<APInvoice> dummy_APInvoice;
		[PXReadOnlyView]
		public PXSelect<CATran> dummy_CATran;

		[PXViewName(Messages.APAddress)]
		public PXSelect<APAddress, Where<APAddress.addressID, Equal<Current<APPayment.remitAddressID>>>> Remittance_Address;
		[PXViewName(Messages.APContact)]
		public PXSelect<APContact, Where<APContact.contactID, Equal<Current<APPayment.remitContactID>>>> Remittance_Contact;

		public PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
		public PXSelectReadonly<APInvoice, Where<APInvoice.vendorID, Equal<Required<APInvoice.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>> APInvoice_VendorID_DocType_RefNbr;
		public PXSelect<APPayment, Where<APPayment.vendorID, Equal<Required<APPayment.vendorID>>, And<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>> APPayment_VendorID_DocType_RefNbr;

		public APPaymentChargeSelect<APPayment, APPayment.paymentMethodID, APPayment.cashAccountID, APPayment.docDate, APPayment.finPeriodID,
			Where<APPaymentChargeTran.docType, Equal<Current<APPayment.docType>>,
				And<APPaymentChargeTran.refNbr, Equal<Current<APPayment.refNbr>>>>> PaymentCharges;

		[PXViewName(Messages.Vendor)]
		public PXSetup<Vendor, Where<Vendor.bAccountID, Equal<Optional<APPayment.vendorID>>>> vendor;
		public PXSetup<Location, Where<Location.bAccountID, Equal<Current<APPayment.vendorID>>, And<Location.locationID, Equal<Optional<APPayment.vendorLocationID>>>>> location;
		public PXSetup<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>> vendorclass;
		public PXSelect<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Current<APPayment.cashAccountID>>>, OrderBy<Asc<PaymentMethodAccount.aPIsDefault>>> CashAcctDetail_AccountID;
		public PXSetup<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Optional<APPayment.paymentMethodID>>>> paymenttype;
		public PXSetup<CashAccount, Where<CashAccount.cashAccountID, Equal<Optional<APPayment.cashAccountID>>>> cashaccount;
		public PXSetup<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Optional<APPayment.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<APPayment.paymentMethodID>>>>> cashaccountdetail;
		public PXSelectReadonly<FinPeriod, Where<FinPeriod.finPeriodID, Equal<Current<APPayment.adjFinPeriodID>>>> finperiod;
		public PXSelect<PTInstTran, Where<PTInstTran.origTranType, Equal<Current<APPayment.docType>>,
											And<PTInstTran.origRefNbr, Equal<Current<APPayment.refNbr>>>>> ptInstanceTrans;
		public PXSetup<GLSetup> glsetup;
		public PXSelect<CashAccountCheck> CACheck;

		public PXSelect<GLVoucher, Where<True, Equal<False>>> Voucher;

		[PXViewName(EP.Messages.Employee)]
		public PXSetup<EPEmployee, Where<EPEmployee.userID, Equal<Current<APPayment.employeeID>>>> employee;
		public PXSelect<APSetupApproval,
			Where<APSetupApproval.docType, Equal<Current<APPayment.docType>>,
				And<Where<Current<APPayment.docType>, Equal<APDocType.check>,
					Or<Current<APPayment.docType>, Equal<APDocType.prepayment>>>>>> SetupApproval;
		[PXViewName(EP.Messages.Approval)]
		public EPApprovalAutomationWithReservedDoc<APPayment, APPayment.approved, APPayment.rejected, APPayment.hold, APSetupApproval> Approval;
		protected bool _AutoPaymentApp;
		public bool AutoPaymentApp
		{
			get
			{
				return _AutoPaymentApp;
			}
			set
			{
				_AutoPaymentApp = value;
			}
		}

		public FinPeriod FINPERIOD => finperiod.Select();

		#region Setups
		public PXSetup<APSetup> APSetup;
		#endregion

		#region Buttons
		public PXAction<APPayment> cancel;
		[PXCancelButton]
		[PXUIField(MapEnableRights = PXCacheRights.Select)]
		protected new virtual IEnumerable Cancel(PXAdapter a)
		{
			string lastDocType = null;
			string lastRefNbr = null;
			if (this.Document.Current != null)
			{
				lastDocType = this.Document.Current.DocType;
				lastRefNbr = this.Document.Current.RefNbr;
			}
			PXResult<APPayment, Vendor> r = null;
			foreach (PXResult<APPayment, Vendor> e in (new PXCancel<APPayment>(this, "Cancel")).Press(a))
			{
				r = e;
			}
			if (Document.Cache.GetStatus((APPayment)r) == PXEntryStatus.Inserted)
			{
				if (lastRefNbr != ((APPayment)r).RefNbr)
				{
					if (((APPayment)r).DocType == APPaymentType.Check || ((APPayment)r).DocType == APPaymentType.Prepayment)
					{
						string docType = ((APPayment)r).DocType;
						string refNbr = ((APPayment)r).RefNbr;
						string searchDocType = docType == APPaymentType.Check ? APPaymentType.Prepayment : APPaymentType.Check;
						APPayment duplicatePayment = PXSelect<APPayment,
							Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>
							.Select(this, searchDocType, refNbr);
						APInvoice inv = null;
						if (searchDocType == APPaymentType.Prepayment)
						{
							inv = PXSelect<APInvoice, Where<APInvoice.docType, Equal<APInvoiceType.prepayment>, And<APInvoice.refNbr, Equal<Required<APPayment.refNbr>>>>>.Select(this, refNbr);
						}
						if (duplicatePayment != null && inv == null)
						{
							Document.Cache.RaiseExceptionHandling<APPayment.refNbr>((APPayment)r, refNbr,
								new PXSetPropertyException<APPayment.refNbr>(Messages.SameRefNbr, searchDocType == APPaymentType.Check ? Messages.Check : Messages.Prepayment, refNbr));
						}
					}

				}
			}

			yield return r;
		}

		public PXAction<APPayment> printCheck;
		[PXUIField(DisplayName = "Print Check", MapEnableRights = PXCacheRights.Select)]
		[PXButton]
		[APMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable PrintCheck(PXAdapter adapter)
		{
			APPayment doc = Document.Current;
			APPrintChecks pp = PXGraph.CreateInstance<APPrintChecks>();
			PrintChecksFilter filter_copy = PXCache<PrintChecksFilter>.CreateCopy(pp.Filter.Current);
			filter_copy.PayAccountID = doc.CashAccountID;
			filter_copy.PayTypeID = doc.PaymentMethodID;
			pp.Filter.Cache.Update(filter_copy);
			doc.Selected = true;
			doc.Passed = true;
			pp.APPaymentList.Cache.Update(doc);
			pp.APPaymentList.Cache.SetStatus(doc, PXEntryStatus.Updated);
			pp.APPaymentList.Cache.IsDirty = false;
			throw new PXRedirectRequiredException(pp, "Preview");
		}

		public PXAction<APPayment> newVendor;
		[PXUIField(DisplayName = "New Vendor", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable NewVendor(PXAdapter adapter)
		{
			VendorMaint graph = PXGraph.CreateInstance<VendorMaint>();
			throw new PXRedirectRequiredException(graph, "New Vendor");
		}

		public PXAction<APPayment> editVendor;
		[PXUIField(DisplayName = "Edit Vendor", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable EditVendor(PXAdapter adapter)
		{
			if (vendor.Current != null)
			{
				VendorMaint graph = PXGraph.CreateInstance<VendorMaint>();
				graph.BAccount.Current = (VendorR)vendor.Current;
				throw new PXRedirectRequiredException(graph, "Edit Vendor");
			}
			return adapter.Get();
		}

		public PXAction<APPayment> vendorDocuments;
		[PXUIField(DisplayName = "Vendor Details", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable VendorDocuments(PXAdapter adapter)
		{
			if (vendor.Current != null)
			{
				APDocumentEnq graph = PXGraph.CreateInstance<APDocumentEnq>();
				graph.Filter.Current.VendorID = vendor.Current.BAccountID;
				graph.Filter.Select();
				throw new PXRedirectRequiredException(graph, "Vendor Details");
			}
			return adapter.Get();
		}

		[PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[APMigrationModeDependentActionRestriction(
			restrictInMigrationMode: false,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public override IEnumerable Release(PXAdapter adapter)
		{
			PXCache cache = Document.Cache;
			List<APRegister> list = new List<APRegister>();
			foreach (APPayment apdoc in adapter.Get<APPayment>())
			{
				if (apdoc.Status != APDocStatus.Balanced && apdoc.Status != APDocStatus.Printed && apdoc.Status != APDocStatus.Open)
				{
					throw new PXException(Messages.Document_Status_Invalid);
				}
				if ((apdoc.DocType == APDocType.Check
						|| apdoc.DocType == APDocType.Refund
						|| apdoc.DocType == APDocType.VoidCheck)
					&& this.PaymentRefMustBeUnique && string.IsNullOrEmpty(apdoc.ExtRefNbr))
				{
					cache.RaiseExceptionHandling<APPayment.extRefNbr>(apdoc, apdoc.ExtRefNbr,
						new PXRowPersistingException(typeof(APPayment.extRefNbr).Name, null, ErrorMessages.FieldIsEmpty, typeof(APPayment.extRefNbr).Name));
				}
				cache.Update(apdoc);
				list.Add(apdoc);
			}

			Save.Press();
			PXLongOperation.StartOperation(this, delegate () { APDocumentRelease.ReleaseDoc(list, false); });
			return list;
		}

		[PXUIField(DisplayName = "Void", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[APMigrationModeDependentActionRestriction(
			restrictInMigrationMode: false,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public override IEnumerable VoidCheck(PXAdapter adapter)
		{
			List<APPayment> rs = new List<APPayment>();

			if (Document.Current != null && 
				Document.Current.Released == true && 
				Document.Current.Voided == false && 
				(Document.Current.DocType == APDocType.Check || Document.Current.DocType == APDocType.Prepayment))
			{
				APAdjust checkApplication = PXSelect<APAdjust, 
					Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, 
						And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, 
						And<APAdjust.adjgDocType, Equal<APDocType.check>>>>>
					.SelectWindowed(this, 0, 1, Document.Current.DocType, Document.Current.RefNbr);

				if (checkApplication != null && checkApplication.IsSelfAdjustment() != true)
				{
					throw new PXException(Messages.PaymentIsPayedByCheck, checkApplication.AdjgRefNbr);
				}

				APAdjust refundApplication = PXSelect<
					APAdjust, 
					Where<
						APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, 
						And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, 
						And<APAdjust.adjgDocType, Equal<APDocType.refund>,
						And<APAdjust.voided, NotEqual<True>>>>>>
					.SelectWindowed(this, 0, 1, Document.Current.DocType, Document.Current.RefNbr);

				if (refundApplication != null && refundApplication.IsSelfAdjustment() != true)
				{
					throw new PXException(
						Common.Messages.DocumentHasBeenRefunded,
						GetLabel.For<APDocType>(Document.Current.DocType),
						Document.Current.RefNbr,
						GetLabel.For<APDocType>(refundApplication.AdjgDocType),
						refundApplication.AdjgRefNbr);
				}

				if (APSetup.Current.MigrationMode != true &&
					Document.Current.IsMigratedRecord == true &&
					Document.Current.CuryInitDocBal != Document.Current.CuryOrigDocAmt)
				{
					throw new PXException(Messages.MigrationModeIsDeactivatedForMigratedDocument);
				}

				if (APSetup.Current.MigrationMode == true &&
					Document.Current.IsMigratedRecord == true && 
					Adjustments_History
						.Select()
						.RowCast<APAdjust>()
						.Any(application =>
							application.Voided != true && 
							application.VoidAppl != true && 
							application.IsMigratedRecord != true &&
							application.IsInitialApplication != true))
				{
					throw new PXException(Common.Messages.CannotVoidPaymentRegularUnreversedApplications);
				}

				APPayment voidcheck = Document.Search<APPayment.refNbr>(Document.Current.RefNbr, APDocType.VoidCheck);

				if (voidcheck != null)
				{
					rs.Add(Document.Current);
					return rs;
				}

				//delete unreleased applications
				foreach (APAdjust adj in Adjustments_Raw.Select())
				{
					Adjustments.Cache.Delete(adj);
				}
				this.Save.Press();

				APPayment doc = PXCache<APPayment>.CreateCopy(Document.Current);
				FiscalPeriodUtils.VerifyFinPeriod<APPayment.finPeriodID, FinPeriod.aPClosed>(this, Document.Cache, doc, finperiod);

				try
				{
					_IsVoidCheckInProgress = true;
					this.VoidCheckProc(doc);
				}
				catch (PXSetPropertyException)
				{
					this.Clear();
					Document.Current = doc;
					throw;
				}
				finally
				{
					_IsVoidCheckInProgress = false;
				}

				Document.Cache.RaiseExceptionHandling<APPayment.finPeriodID>(Document.Current, Document.Current.FinPeriodID, null);

				rs.Add(Document.Current);
				return rs;
			}
			return Document.Select();
		}

		public PXAction<APPayment> viewPPDDebitAdj;

		[PXUIField(MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		public virtual IEnumerable ViewPPDDebitAdj(PXAdapter adapter)
		{
			var adj = Adjustments_History.Current;
			if (adj != null)
			{
				APInvoice invoice = PXSelect<APInvoice, Where<APInvoice.refNbr, Equal<Current<APAdjust.pPDDebitAdjRefNbr>>,
					And<APInvoice.docType, Equal<APDocType.debitAdj>>>>
						.Select(this)?.First();
				if (invoice != null)
				{
					PXGraph graph = PXGraph.CreateInstance<APInvoiceEntry>();
					PXRedirectHelper.TryRedirect(graph, invoice, PXRedirectHelper.WindowMode.NewWindow);
				}
			}
			return adapter.Get();
		}

		private class PXLoadInvoiceException : Exception
		{
			public PXLoadInvoiceException() { }

			public PXLoadInvoiceException(SerializationInfo info, StreamingContext context)
				: base(info, context) { }

		}

		private APAdjust AddAdjustment(APAdjust adj)
		{
			if (Document.Current.CuryUnappliedBal == 0m && Document.Current.CuryOrigDocAmt > 0m)
			{
				throw new PXLoadInvoiceException();
			}
			return this.Adjustments.Insert(adj);
		}

		private void LoadInvoicesProc(bool LoadExistingOnly)
		{
			Dictionary<string, APAdjust> existing = new Dictionary<string, APAdjust>();
			APPayment currentDoc = Document.Current;
			try
			{
				if (currentDoc == null || currentDoc.VendorID == null || currentDoc.OpenDoc == false || currentDoc.DocType != APDocType.Check && currentDoc.DocType != APDocType.Prepayment && currentDoc.DocType != APDocType.Refund)
				{
					throw new PXLoadInvoiceException();
				}

				foreach (PXResult<APAdjust> res in Adjustments_Raw.Select())
				{
					APAdjust old_adj = (APAdjust)res;

					if (LoadExistingOnly == false)
					{
						old_adj = PXCache<APAdjust>.CreateCopy(old_adj);
						old_adj.CuryAdjgAmt = null;
						old_adj.CuryAdjgDiscAmt = null;
						old_adj.CuryAdjgWhTaxAmt = null;
						old_adj.CuryAdjgPPDAmt = null;
					}

					string s = string.Format("{0}_{1}", old_adj.AdjdDocType, old_adj.AdjdRefNbr);
					existing.Add(s, old_adj);
					Adjustments.Cache.Delete((APAdjust)res);
				}

				currentDoc.LineCntr++;
				if (Document.Cache.GetStatus(currentDoc) == PXEntryStatus.Notchanged)
				{
					Document.Cache.SetStatus(currentDoc, PXEntryStatus.Updated);
				}
				Document.Cache.IsDirty = true;

				foreach (KeyValuePair<string, APAdjust> res in existing)
				{
					APAdjust adj = new APAdjust();
					adj.AdjdDocType = res.Value.AdjdDocType;
					adj.AdjdRefNbr = res.Value.AdjdRefNbr;

					try
					{
						adj = PXCache<APAdjust>.CreateCopy(AddAdjustment(adj));
						if (res.Value.CuryAdjgWhTaxAmt != null && res.Value.CuryAdjgWhTaxAmt < adj.CuryAdjgWhTaxAmt)
						{
							adj.CuryAdjgWhTaxAmt = res.Value.CuryAdjgWhTaxAmt;
							adj = PXCache<APAdjust>.CreateCopy((APAdjust)this.Adjustments.Cache.Update(adj));
						}

						if (res.Value.CuryAdjgDiscAmt != null && res.Value.CuryAdjgDiscAmt < adj.CuryAdjgDiscAmt)
						{
							adj.CuryAdjgDiscAmt = res.Value.CuryAdjgDiscAmt;
							adj.CuryAdjgPPDAmt = res.Value.CuryAdjgDiscAmt;
							adj = PXCache<APAdjust>.CreateCopy((APAdjust)this.Adjustments.Cache.Update(adj));
						}

						if (res.Value.CuryAdjgAmt != null && res.Value.CuryAdjgAmt < adj.CuryAdjgAmt)
						{
							adj.CuryAdjgAmt = res.Value.CuryAdjgAmt;
							this.Adjustments.Cache.Update(adj);
						}
					}
					catch (PXSetPropertyException) { }
				}

				if (LoadExistingOnly)
				{
					return;
				}
				PXGraph graph = this;
				PXResultset<APInvoice> venddocs = GetVendDocs(currentDoc, graph, APSetup.Current);

				foreach (APInvoice invoice in venddocs)
				{
					string s = string.Format("{0}_{1}", invoice.DocType, invoice.RefNbr);
					if (existing.ContainsKey(s) == false)
					{
						APAdjust adj = new APAdjust();
						adj.AdjdDocType = invoice.DocType;
						adj.AdjdRefNbr = invoice.RefNbr;

						AddAdjustment(adj);
					}
				}

				if (currentDoc.CuryApplAmt < 0m)
				{
					List<APAdjust> debits = new List<APAdjust>();

					foreach (APAdjust adj in Adjustments_Raw.Select())
					{
						if (adj.AdjdDocType == APDocType.DebitAdj)
						{
							debits.Add(adj);
						}
					}

					debits.Sort((a, b) =>
						{
							return ((IComparable)a.CuryAdjgAmt).CompareTo(b.CuryAdjgAmt);
						});

					foreach (APAdjust adj in debits)
					{
						if (adj.CuryAdjgAmt <= -currentDoc.CuryApplAmt)
						{
							Adjustments.Delete(adj);
						}
						else
						{
							APAdjust copy = PXCache<APAdjust>.CreateCopy(adj);
							copy.CuryAdjgAmt += currentDoc.CuryApplAmt;
							Adjustments.Update(copy);
						}
					}
				}
			}
			catch (PXLoadInvoiceException)
			{
			}
		}

		public static PXResultset<APInvoice> GetVendDocs(APPayment currentAPPayment, PXGraph graph, APSetup currentAPSetup)
		{
			PXSelectBase<APInvoice> cmd = new PXSelectReadonly2<APInvoice,
							LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>>>>,
							LeftJoin<APAdjust2, On<APAdjust2.adjgDocType, Equal<APInvoice.docType>, And<APAdjust2.adjgRefNbr, Equal<APInvoice.refNbr>, And<APAdjust2.released, Equal<False>, And<APAdjust2.voided, Equal<False>>>>>,
							LeftJoin<APPayment, On<APPayment.docType, Equal<APInvoice.docType>, And<APPayment.refNbr, Equal<APInvoice.refNbr>, And<APPayment.docType, Equal<APDocType.prepayment>>>>>>>,
							Where<APInvoice.vendorID, Equal<Required<APPayment.vendorID>>,
											And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>,
											And<APInvoice.openDoc, Equal<True>,
											 And<APAdjust.adjgRefNbr, IsNull, And<APAdjust2.adjdRefNbr, IsNull,
											And<APInvoice.pendingPPD, NotEqual<True>,
											And<APPayment.refNbr, IsNull,
											And<Where<APInvoice.docDate, LessEqual<Required<APPayment.adjDate>>,
											And<APInvoice.finPeriodID, LessEqual<Required<APPayment.adjFinPeriodID>>,
											Or<Required<APPayment.docType>, Equal<APDocType.check>, And<Required<APSetup.earlyChecks>, Equal<True>,
											Or<Required<APPayment.docType>, Equal<APDocType.voidCheck>, And<Required<APSetup.earlyChecks>, Equal<True>,
											Or<Required<APPayment.docType>, Equal<APDocType.prepayment>, And<Required<APSetup.earlyChecks>, Equal<True>>>>>>>>>>>>>>>>>,
							OrderBy<Asc<APInvoice.dueDate, Asc<APInvoice.refNbr>>>>(graph);

			switch (currentAPPayment.DocType)
			{
				case APDocType.Refund:
					cmd.WhereAnd<Where<APInvoice.docType, Equal<APDocType.debitAdj>>>();
					break;
				case APDocType.Prepayment:
					cmd.WhereAnd<Where<APInvoice.docType, Equal<APDocType.invoice>, Or<APInvoice.docType, Equal<APDocType.creditAdj>>>>();
					break;
				case APDocType.Check:
					cmd.WhereAnd<Where<APInvoice.docType, Equal<APDocType.invoice>, Or<APInvoice.docType, Equal<APDocType.debitAdj>, Or<APInvoice.docType, Equal<APDocType.creditAdj>, Or<APInvoice.docType, Equal<APDocType.prepayment>, And<APInvoice.curyID, Equal<Required<APPayment.curyID>>>>>>>>();
					break;
				default:
					cmd.WhereAnd<Where<True, Equal<False>>>();
					break;
			}

			PXResultset<APInvoice> venddocs = cmd.Select(currentAPPayment.VendorID, currentAPPayment.AdjDate, currentAPPayment.AdjFinPeriodID, currentAPPayment.DocType, currentAPSetup.EarlyChecks, currentAPPayment.CuryID);

			venddocs.Sort((a, b) =>
			{
				int aSortOrder = 0;
				int bSortOrder = 0;

				if (currentAPPayment.CuryOrigDocAmt > 0m)
				{
					aSortOrder += (((APInvoice)a).DocType == APDocType.DebitAdj ? 0 : 1000);
					bSortOrder += (((APInvoice)b).DocType == APDocType.DebitAdj ? 0 : 1000);
				}
				else
				{
					aSortOrder += (((APInvoice)a).DocType == APDocType.DebitAdj ? 1000 : 0);
					bSortOrder += (((APInvoice)b).DocType == APDocType.DebitAdj ? 1000 : 0);
				}

				DateTime aDueDate = ((APInvoice)a).DueDate ?? DateTime.MinValue;
				DateTime bDueDate = ((APInvoice)b).DueDate ?? DateTime.MinValue;

				object aObj;
				object bObj;

				aSortOrder += (1 + aDueDate.CompareTo(bDueDate)) / 2 * 10;
				bSortOrder += (1 - aDueDate.CompareTo(bDueDate)) / 2 * 10;

				aObj = ((APInvoice)a).RefNbr;
				bObj = ((APInvoice)b).RefNbr;
				aSortOrder += (1 + ((IComparable)aObj).CompareTo(bObj)) / 2;
				bSortOrder += (1 - ((IComparable)aObj).CompareTo(bObj)) / 2;

				return aSortOrder.CompareTo(bSortOrder);
			});
			return venddocs;
		}

		public PXAction<APPayment> loadInvoices;
		[PXUIField(DisplayName = "Load Documents", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.Refresh)]
		[APMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable LoadInvoices(PXAdapter adapter)
		{
			LoadInvoicesProc(false);
			return adapter.Get();
		}

		public PXAction<APPayment> reverseApplication;
		[PXUIField(DisplayName = "Reverse Application", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton(Tooltip = Messages.ReverseApp, ImageKey = PX.Web.UI.Sprite.Main.Refresh)]
		[APMigrationModeDependentActionRestriction(
			restrictInMigrationMode: false,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable ReverseApplication(PXAdapter adapter)
		{
			APPayment payment = Document.Current;
			APAdjust application = Adjustments_History.Current;

			if (application == null) return adapter.Get();

			if (application.AdjType == ARAdjust.adjType.Adjusting)
			{
				throw new PXException(
					Common.Messages.IncomingApplicationCannotBeReversed,
					GetLabel.For<APDocType>(payment.DocType),
					GetLabel.For<APDocType>(application.AdjgDocType),
					application.AdjgRefNbr);
			}

			if (application.IsInitialApplication == true)
			{
				throw new PXException(Common.Messages.InitialApplicationCannotBeReversed);
			}
			else if (application.IsMigratedRecord != true &&
				APSetup.Current.MigrationMode == true)
				{
				throw new PXException(Messages.CannotReverseRegularApplicationInMigrationMode);
				}

			if (application.Voided != true 
				&& (
					APPaymentType.CanHaveBalance(application.AdjgDocType) 
					|| application.AdjgDocType == APDocType.Refund 
					|| application.AdjgDocType == APDocType.Check))
			{

				if (payment != null
					&& (payment.DocType != APDocType.DebitAdj || payment.PendingPPD != true)
					&& application.AdjdHasPPDTaxes == true
					&& application.PendingPPD != true)
				{
					APAdjust adjPPD = GetPPDApplication(this, application.AdjdDocType, application.AdjdRefNbr);

					if (adjPPD != null)
					{
						throw new PXSetPropertyException(Messages.PPDApplicationExists, adjPPD.AdjgRefNbr);
					}
				}
				if (payment.OpenDoc != true)
				{
					payment.OpenDoc = true;
					Document.Cache.RaiseRowSelected(payment);
				}

				APAdjust adj = PXCache<APAdjust>.CreateCopy(application);

				adj.Voided = true;
				adj.VoidAdjNbr = adj.AdjNbr;
				adj.Released = false;
				Adjustments.Cache.SetDefaultExt<APAdjust.isMigratedRecord>(adj);
				adj.AdjNbr = payment.LineCntr;
				adj.AdjBatchNbr = null;

				APAdjust adjnew = new APAdjust
				{
					AdjgDocType = adj.AdjgDocType,
					AdjgRefNbr = adj.AdjgRefNbr,
					AdjgBranchID = adj.AdjgBranchID,
					AdjdDocType = adj.AdjdDocType,
					AdjdRefNbr = adj.AdjdRefNbr,
					AdjdBranchID = adj.AdjdBranchID,
					VendorID = adj.VendorID,
					AdjNbr = adj.AdjNbr,
					AdjdCuryInfoID = adj.AdjdCuryInfoID,
					AdjdHasPPDTaxes = adj.AdjdHasPPDTaxes,
				};

				_AutoPaymentApp = true;
				adjnew = Adjustments.Insert(adjnew);

				if (adjnew == null)
				{
					return adapter.Get();
				}

				adj.CuryAdjgAmt = -1 * adj.CuryAdjgAmt;
				adj.CuryAdjgDiscAmt = -1 * adj.CuryAdjgDiscAmt;
				adj.CuryAdjgPPDAmt = -1 * adj.CuryAdjgPPDAmt;
				adj.AdjAmt = -1 * adj.AdjAmt;
				adj.AdjDiscAmt = -1 * adj.AdjDiscAmt;
				adj.AdjPPDAmt = -1 * adj.AdjPPDAmt;
				adj.CuryAdjdAmt = -1 * adj.CuryAdjdAmt;
				adj.CuryAdjdDiscAmt = -1 * adj.CuryAdjdDiscAmt;
				adj.CuryAdjdPPDAmt = -1 * adj.CuryAdjdPPDAmt;
				adj.RGOLAmt = -1 * adj.RGOLAmt;
				adj.AdjgCuryInfoID = payment.CuryInfoID;

				Adjustments.Update(adj);
				_AutoPaymentApp = false;
			}

			return adapter.Get();
		}
		public static APAdjust GetPPDApplication(PXGraph graph, string DocType, string RefNbr)
		{
			return PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>,
				And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>,
				And<APAdjust.released, Equal<True>,
				And<APAdjust.voided, NotEqual<True>,
				And<APAdjust.pendingPPD, Equal<True>>>>>>>.SelectSingleBound(graph, null, DocType, RefNbr);
		}
		public PXAction<APPayment> viewDocumentToApply;
		[PXUIField(
			DisplayName = Messages.ViewDocument,
			MapEnableRights = PXCacheRights.Select,
			MapViewRights = PXCacheRights.Select,
			Visible = false)]
		[PXLookupButton]
		public virtual IEnumerable ViewDocumentToApply(PXAdapter adapter)
		{
			APAdjust row = Adjustments.Current;
			if (!string.IsNullOrEmpty(row?.AdjdDocType) && !string.IsNullOrEmpty(row?.AdjdRefNbr))
			{
				if (row.AdjdDocType == APDocType.Check || (row.AdjgDocType == APDocType.Refund && row.AdjdDocType == APDocType.Prepayment))
				{
					APPaymentEntry iegraph = PXGraph.CreateInstance<APPaymentEntry>();
					iegraph.Document.Current = iegraph.Document.Search<APPayment.refNbr>(row.AdjdRefNbr, row.AdjdDocType);
					throw new PXRedirectRequiredException(iegraph, true, "View Document") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}
				else
				{
					APInvoiceEntry iegraph = PXGraph.CreateInstance<APInvoiceEntry>();
					iegraph.Document.Current = iegraph.Document.Search<APInvoice.refNbr>(row.AdjdRefNbr, row.AdjdDocType);
					throw new PXRedirectRequiredException(iegraph, true, "View Document") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}
			}

			return adapter.Get();
		}

		public PXAction<APPayment> viewApplicationDocument;
		[PXUIField(
			DisplayName = Messages.ViewAppDoc,
			MapEnableRights = PXCacheRights.Select,
			MapViewRights = PXCacheRights.Select,
			Visible = false)]
		[PXLookupButton(Tooltip = Messages.ViewAppDoc)]
		public virtual IEnumerable ViewApplicationDocument(PXAdapter adapter)
		{
				APAdjust row = Adjustments_History.Current;

			if (row == null) return adapter.Get();

			if (!string.IsNullOrEmpty(row.DisplayDocType) &&
				!string.IsNullOrEmpty(row.DisplayRefNbr))
				{
					PXGraph redirect;

				switch (row.DisplayDocType)
				{
					case APDocType.Check:
						case APDocType.Prepayment:
					case APDocType.Refund:
					case APDocType.VoidCheck:
					{
							APPaymentEntry docgraph = CreateInstance<APPaymentEntry>();
						docgraph.Document.Current = docgraph.Document.Search<APPayment.refNbr>(row.DisplayRefNbr, row.DisplayDocType);
							redirect = docgraph;
							break;
						}
						default:
						{
							APInvoiceEntry docgraph = CreateInstance<APInvoiceEntry>();
						docgraph.Document.Current = docgraph.Document.Search<APInvoice.refNbr>(row.DisplayRefNbr, row.DisplayDocType);
							redirect = docgraph;
							break;
						}
					}

				throw new PXRedirectRequiredException(redirect, true, Messages.ViewAppDoc) { Mode = PXBaseRedirectException.WindowMode.NewWindow };
			}

			return adapter.Get();
		}

		public PXAction<APPayment> validateAddresses;
		[PXUIField(DisplayName = CS.Messages.ValidateAddress, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, FieldClass = CS.Messages.ValidateAddress)]
		[PXButton]
		[APMigrationModeDependentActionRestriction(
			restrictInMigrationMode: false,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
		{
			foreach (APPayment current in adapter.Get<APPayment>())
			{
				if (current != null)
				{
					bool needSave = false;
					Save.Press();
					APAddress address = this.Remittance_Address.Select();
					if (address != null && address.IsDefaultAddress == false && address.IsValidated == false)
					{
						if (PXAddressValidator.Validate<APAddress>(this, address, true))
							needSave = true;
					}
					if (needSave == true)
						this.Save.Press();
				}
				yield return current;
			}
		}
		#endregion

		protected virtual void CATran_CashAccountID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void CATran_FinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void CATran_TranPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void CATran_ReferenceID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void CATran_CuryID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void APPayment_CuryID_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
		{
		}

		protected virtual void APPayment_DocType_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			e.NewValue = APDocType.Check;
		}

		protected virtual IEnumerable adjustments()
		{
			PXResultset<APAdjust, APInvoice> ret = new PXResultset<APAdjust, APInvoice>();

			foreach (PXResult<APAdjust, APInvoice, Standalone.APRegisterAlias> res
				in Adjustments_Invoices.Select())
			{
				APInvoice fullInvoice = res;
				PXCache<APRegister>.RestoreCopy(fullInvoice, (Standalone.APRegisterAlias)res);

				if (Adjustments.Cache.GetStatus((APAdjust)res) == PXEntryStatus.Notchanged)
				{
					CalcBalances<APInvoice>((APAdjust)res, fullInvoice, true);
				}

				ret.Add(new PXResult<APAdjust, APInvoice>(res, fullInvoice));
			}

			foreach (PXResult<APAdjust, APPayment, APInvoice, Standalone.APRegisterAlias> res
				in Adjustments_Payments.Select())
			{
				APPayment fullPayment = res;
				PXCache<APRegister>.RestoreCopy(fullPayment, (Standalone.APRegisterAlias)res);

				if (Adjustments.Cache.GetStatus((APAdjust)res) == PXEntryStatus.Notchanged)
				{
					CalcBalances<APPayment>((APAdjust)res, fullPayment, true);
				}

				ret.Add(new PXResult<APAdjust, APPayment>(res, fullPayment));
			}

			return ret;
		}

		protected virtual IEnumerable adjustments_history()
		{
            PXResultset<APAdjust> resultSet = new PXResultset<APAdjust>();

            PXSelectBase<APAdjust> outgoingApplications = new PXSelectJoin<
				APAdjust,
					LeftJoinSingleTable<APInvoice,
						On<APInvoice.docType, Equal<APAdjust.adjdDocType>,
						And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>,
					LeftJoin<Standalone.APRegisterAlias,
						On<Standalone.APRegisterAlias.docType, Equal<APAdjust.adjdDocType>,
						And<Standalone.APRegisterAlias.refNbr, Equal<APAdjust.adjdRefNbr>>>>>,
				Where<
					APAdjust.adjgDocType, Equal<Current<APPayment.docType>>,
					And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>,
					And<APAdjust.adjNbr, Less<Current<APPayment.lineCntr>>,
					And<APAdjust.isInitialApplication, NotEqual<True>>>>>>
                (this);

            foreach (PXResult<APAdjust, APInvoice, Standalone.APRegisterAlias> result in outgoingApplications.Select())
			{
				APInvoice fullInvoice = result;
				PXCache<APRegister>.RestoreCopy(fullInvoice, (Standalone.APRegisterAlias)result);

                resultSet.Add(new PXResult<APAdjust, APInvoice>(result, fullInvoice));
			}

            PXSelectBase<APAdjust> incomingApplications = new PXSelectJoin<
                APAdjust,
                InnerJoinSingleTable<APPayment,
                    On<APPayment.docType, Equal<APAdjust.adjgDocType>,
                    And<APPayment.refNbr, Equal<APAdjust.adjgRefNbr>>>,
                InnerJoin<Standalone.APRegisterAlias,
                    On<Standalone.APRegisterAlias.docType, Equal<APAdjust.adjgDocType>,
                    And<Standalone.APRegisterAlias.refNbr, Equal<APAdjust.adjgRefNbr>,
                    And<Standalone.APRegisterAlias.lineCntr, Greater<APAdjust.adjNbr>>>>,
                InnerJoin<CurrencyInfo,
                    On<Standalone.APRegisterAlias.curyInfoID, Equal<CurrencyInfo.curyInfoID>>>>>,
                Where<
                    APAdjust.adjdDocType, Equal<Current<APPayment.docType>>,
                    And<APAdjust.adjdRefNbr, Equal<Current<APPayment.refNbr>>>>>
                (this);

            foreach (PXResult<APAdjust, APPayment, Standalone.APRegisterAlias, CurrencyInfo> result in incomingApplications.Select())
            {
                APAdjust incomingApplication = result;
                APPayment appliedPayment = result;
                CurrencyInfo paymentCurrencyInfo = result;

                PXCache<APRegister>.RestoreCopy(appliedPayment, (Standalone.APRegisterAlias)result);

                incomingApplication.CuryDocBal = BalanceCalculation.CalculateApplicationDocumentBalance(
                    this.Caches<APAdjust>(),
                    paymentCurrencyInfo,
                    currencyinfo.Select(),
                    appliedPayment.DocBal,
                    appliedPayment.CuryDocBal);

                resultSet.Add(new PXResult<APAdjust, APPayment>(incomingApplication, appliedPayment));
            }

            return resultSet;
		}

		protected virtual IEnumerable adjustments_print()
		{
			if (Document.Current.DocType == APDocType.QuickCheck)
			{
				foreach (PXResult<APAdjust> res in Adjustments_Raw.Select())
				{
					Adjustments.Cache.Delete((APAdjust)res);
				}

				APPayment doc = Document.Current;
				doc.LineCntr++;

				APAdjust adj = new APAdjust();
				adj.AdjdDocType = doc.DocType;
				adj.AdjdRefNbr = doc.RefNbr;
				adj.AdjdBranchID = doc.BranchID;
				adj.AdjdOrigCuryInfoID = doc.CuryInfoID;
				adj.AdjgCuryInfoID = doc.CuryInfoID;
				adj.AdjdCuryInfoID = doc.CuryInfoID;

				adj = Adjustments.Insert(adj);

				adj.CuryDocBal = doc.CuryOrigDocAmt + doc.CuryOrigDiscAmt + doc.CuryOrigWhTaxAmt;
				adj.CuryDiscBal = doc.CuryOrigDiscAmt;
				adj.CuryWhTaxBal = doc.CuryOrigWhTaxAmt;

				adj = PXCache<APAdjust>.CreateCopy(adj);

				adj.AdjgDocType = doc.DocType;
				adj.AdjgRefNbr = doc.RefNbr;
				adj.AdjdAPAcct = doc.APAccountID;
				adj.AdjdAPSub = doc.APSubID;
				adj.AdjdCuryInfoID = doc.CuryInfoID;
				adj.AdjdDocDate = doc.DocDate;
				adj.AdjdFinPeriodID = doc.FinPeriodID;
				adj.AdjdTranPeriodID = doc.TranPeriodID;
				adj.AdjdOrigCuryInfoID = doc.CuryInfoID;
				adj.AdjgCuryInfoID = doc.CuryInfoID;
				adj.AdjgDocDate = doc.DocDate;
				adj.AdjgFinPeriodID = doc.FinPeriodID;
				adj.AdjgTranPeriodID = doc.TranPeriodID;
				adj.AdjNbr = doc.LineCntr;
				adj.AdjAmt = doc.OrigDocAmt;
				adj.AdjDiscAmt = doc.OrigDiscAmt;
				adj.AdjWhTaxAmt = doc.OrigWhTaxAmt;
				adj.RGOLAmt = 0m;
				adj.CuryAdjdAmt = doc.CuryOrigDocAmt;
				adj.CuryAdjdDiscAmt = doc.CuryOrigDiscAmt;
				adj.CuryAdjdWhTaxAmt = doc.CuryOrigWhTaxAmt;
				adj.CuryAdjgAmt = doc.CuryOrigDocAmt;
				adj.CuryAdjgDiscAmt = doc.CuryOrigDiscAmt;
				adj.CuryAdjgWhTaxAmt = doc.CuryOrigWhTaxAmt;
				adj.CuryDocBal = doc.CuryOrigDocAmt + doc.CuryOrigDiscAmt + doc.CuryOrigWhTaxAmt;
				adj.CuryDiscBal = doc.CuryOrigDiscAmt;
				adj.CuryWhTaxBal = doc.CuryOrigWhTaxAmt;
				adj.Released = false;
				adj.VendorID = doc.VendorID;
				Adjustments.Cache.Update(adj);
			}

			return Adjustments_Raw.Select();
		}

		public override int ExecuteUpdate(string viewName, IDictionary keys, IDictionary values, params object[] parameters)
		{
			if (viewName.ToLower() == "document" && values != null)
			{
				values["CuryApplAmt"] = PXCache.NotSetValue;
				values["CuryUnappliedBal"] = PXCache.NotSetValue;
			}
			return base.ExecuteUpdate(viewName, keys, values, parameters);
		}

		protected virtual void ParentFieldUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			if (!sender.ObjectsEqual<APPayment.adjDate, APPayment.adjFinPeriodID, APPayment.curyID, APPayment.branchID>(e.Row, e.OldRow))
			{
				foreach (APAdjust adj in Adjustments_Raw.Select())
				{
					adj.AdjgBranchID = ((APPayment)e.Row).BranchID;
					Adjustments.Cache.SmartSetStatus(adj, PXEntryStatus.Updated);
				}
			}

			if (!sender.ObjectsEqual<APPayment.docDate, APPayment.curyOrigDocAmt, APPayment.curyID, APPayment.hold, APPayment.cashAccountID, APPayment.pTInstanceID, APPayment.extRefNbr, APPayment.vendorID>(e.Row, e.OldRow))
			{
				foreach (PTInstTran iTr in this.ptInstanceTrans.Select())
				{
					this.ptInstanceTrans.Cache.SmartSetStatus(iTr, PXEntryStatus.Updated);
				}
			}
		}

		protected bool InternalCall => UnattendedMode;

		public APPaymentEntry()
		{
			APSetup setup = APSetup.Current;
			OpenPeriodAttribute.SetValidatePeriod<APPayment.adjFinPeriodID>(Document.Cache, null, PeriodValidation.DefaultSelectUpdate);

			RowUpdated.AddHandler<APPayment>(ParentFieldUpdated);

			created = new DocumentList<APPayment>(this);
		}

		public DocumentList<APPayment> created { get; }

		public virtual void Segregate(APAdjust adj, CurrencyInfo info, bool? onHold)
		{
			if (IsDirty)
			{
				Save.Press();
			}

			APInvoice apdoc = APInvoice_VendorID_DocType_RefNbr.Select(adj.VendorID, adj.AdjdDocType, adj.AdjdRefNbr);
			if (apdoc == null)
			{
				throw new AdjustedNotFoundException();
			}

			APPayment payment = created.Find<APPayment.vendorID, APPayment.vendorLocationID, APPayment.hidden>(apdoc.VendorID, apdoc.PayLocationID, false) ?? new APPayment();

			if ((adj.SeparateCheck != true || adj.AdjdDocType == APDocType.DebitAdj) && payment.RefNbr != null)
			{
				Document.Current = Document.Search<APPayment.refNbr>(payment.RefNbr, payment.DocType);
			}
			else if (adj.AdjdDocType == APDocType.DebitAdj)
			{
				throw new PXSetPropertyException(Messages.ZeroCheck_CannotPrint, PXErrorLevel.Warning);
			}
			else
			{
				Clear();
				Document.View.Answer = WebDialogResult.No;

				info.CuryInfoID = null;

				info = PXCache<CurrencyInfo>.CreateCopy(this.currencyinfo.Insert(info));

				payment = new APPayment
				{
					CuryInfoID = info.CuryInfoID,
					DocType = APDocType.Check,
					Hidden = adj.SeparateCheck
				};

				if (onHold.HasValue)
				{
					payment.Hold = onHold;
				}

				payment = PXCache<APPayment>.CreateCopy(Document.Insert(payment));

				payment.BranchID = PXAccess.GetBranchID();
				payment.VendorID = apdoc.VendorID;
				payment.VendorLocationID = apdoc.PayLocationID;
				payment.AdjDate = adj.AdjgDocDate;
				payment.AdjFinPeriodID = adj.AdjgFinPeriodID;
				payment.CashAccountID = apdoc.PayAccountID;
				payment.PaymentMethodID = apdoc.PayTypeID;
				payment.DocDesc = apdoc.DocDesc;

				payment = PXCache<APPayment>.CreateCopy(Document.Update(payment));

				if (payment.ExtRefNbr == null)
				{
					payment = Document.Update(payment);
				}

				CurrencyInfo b_info = PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<APPayment.curyInfoID>>>>.Select(this, null);
				b_info.CuryID = info.CuryID;
				b_info.CuryEffDate = info.CuryEffDate;
				b_info.CuryRateTypeID = info.CuryRateTypeID;
				b_info.CuryRate = info.CuryRate;
				b_info.RecipRate = info.RecipRate;
				b_info.CuryMultDiv = info.CuryMultDiv;
				currencyinfo.Update(b_info);
			}
		}

		public virtual void Segregate(APAdjust adj, CurrencyInfo info)
		{
			Segregate(adj, info, null);
		}


		public override void Persist()
		{
			foreach (APPayment doc in Document.Cache.Updated)
			{
				if (doc.OpenDoc == true && (bool?)Document.Cache.GetValueOriginal<APPayment.openDoc>(doc) == false && Adjustments_Raw.SelectSingle(doc.DocType, doc.RefNbr, doc.LineCntr) == null)
				{
					doc.OpenDoc = false;
					Document.Cache.RaiseRowSelected(doc);
				}
			}

			base.Persist();

			if (Document.Current != null)
			{
				APPayment existed = created.Find(Document.Current);

				if (existed == null)
				{
					created.Add(Document.Current);
				}
				else
				{
					Document.Cache.RestoreCopy(existed, Document.Current);
				}
			}
		}

		protected virtual void CurrencyInfo_CuryID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() && !string.IsNullOrEmpty(cashaccount.Current?.CuryID))
				{
					e.NewValue = cashaccount.Current.CuryID;
					e.Cancel = true;
				}
		}

		protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() && !string.IsNullOrEmpty(cashaccount.Current?.CuryRateTypeID))
				{
					e.NewValue = cashaccount.Current.CuryRateTypeID;
					e.Cancel = true;
				}
		}

		protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Document.Cache.Current != null)
			{
				e.NewValue = ((APPayment)Document.Cache.Current).DocDate;
				e.Cancel = true;
			}
		}

		protected virtual void APPayment_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			vendor.RaiseFieldUpdated(sender, e.Row);

			sender.SetDefaultExt<APInvoice.vendorLocationID>(e.Row);
		}

		protected virtual void APPayment_VendorLocationID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			location.RaiseFieldUpdated(sender, e.Row);

			sender.SetDefaultExt<APPayment.paymentMethodID>(e.Row);
			sender.SetDefaultExt<APPayment.aPAccountID>(e.Row);
			sender.SetDefaultExt<APPayment.aPSubID>(e.Row);

			try
			{
				SharedRecordAttribute.DefaultRecord<APPayment.remitAddressID>(sender, e.Row);
				SharedRecordAttribute.DefaultRecord<APPayment.remitContactID>(sender, e.Row);
			}
			catch (PXFieldValueProcessingException ex)
			{
				ex.ErrorValue = location.Current.LocationCD;
				throw;
			}
		}

		protected virtual void APPayment_ExtRefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.Row != null && ((APPayment)e.Row).DocType == APDocType.VoidCheck)
			{
				///avoid webdialog in <see cref=PaymentRefAttribute/> attribute
				e.Cancel = true;
			}
		}

		private static object GetAcctSub<Field>(PXCache cache, object data)
			where Field : IBqlField
		{
			object NewValue = cache.GetValueExt<Field>(data);
			PXFieldState state = NewValue as PXFieldState;
			return state != null ? state.Value : NewValue;
		}

		protected virtual void APPayment_APAccountID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (location.Current != null && e.Row != null)
			{
				e.NewValue = null;
				if (((APPayment)e.Row).DocType == APDocType.Prepayment)
				{
					e.NewValue = GetAcctSub<Vendor.prepaymentAcctID>(vendor.Cache, vendor.Current);
				}
				if (string.IsNullOrEmpty((string)e.NewValue))
				{
					e.NewValue = GetAcctSub<Location.aPAccountID>(location.Cache, location.Current);
				}
			}
		}

		protected virtual void APPayment_APSubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (location.Current != null && e.Row != null)
			{
				e.NewValue = null;
				if (((APPayment)e.Row).DocType == APDocType.Prepayment)
				{
					e.NewValue = GetAcctSub<Vendor.prepaymentSubID>(vendor.Cache, vendor.Current);
				}
				if (string.IsNullOrEmpty((string)e.NewValue))
				{
					e.NewValue = GetAcctSub<Location.aPSubID>(location.Cache, location.Current);
				}
			}
		}

		protected virtual void APPayment_CashAccountID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			APPayment payment = (APPayment)e.Row;

			if (payment == null || e.NewValue == null)
				return;

			CashAccount cashAccount = PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.SelectSingleBound(this, null, e.NewValue);

			if (cashAccount != null)
			{
				foreach (PXResult<APAdjust, APInvoice, Standalone.APRegisterAlias> res in Adjustments_Invoices.Select())
				{
					APAdjust adj = res;
					APInvoice invoice = res;

					PXCache<APRegister>.RestoreCopy(invoice, (Standalone.APRegisterAlias)res);
				}
			}
		}

		protected virtual void APPayment_CashAccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			APPayment payment = (APPayment)e.Row;
			cashaccount.RaiseFieldUpdated(sender, e.Row);

			if (_IsVoidCheckInProgress == false && PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
			{
				CurrencyInfo info = CurrencyInfoAttribute.SetDefaults<APPayment.curyInfoID>(sender, e.Row);

				string message = PXUIFieldAttribute.GetError<CurrencyInfo.curyEffDate>(currencyinfo.Cache, info);
				if (string.IsNullOrEmpty(message) == false)
				{
					sender.RaiseExceptionHandling<APPayment.adjDate>(e.Row, payment.DocDate, new PXSetPropertyException(message, PXErrorLevel.Warning));
				}

				if (info != null)
				{
					payment.CuryID = info.CuryID;
				}
			}

			sender.SetValueExt<APPayment.pTInstanceID>(e.Row, null); //??

			payment.Cleared = false;
			payment.ClearDate = null;

			if ((cashaccount.Current != null) && (cashaccount.Current.Reconcile == false))
			{
				payment.Cleared = true;
				payment.ClearDate = payment.DocDate;
			}

			sender.SetDefaultExt<APPayment.depositAsBatch>(e.Row);
			sender.SetDefaultExt<APPayment.depositAfter>(e.Row);
		}

		protected virtual void APPayment_PaymentMethodID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			paymenttype.RaiseFieldUpdated(sender, e.Row);

			sender.SetDefaultExt<APPayment.cashAccountID>(e.Row);
			sender.SetDefaultExt<APPayment.pTInstanceID>(e.Row);
			sender.SetDefaultExt<APPayment.printCheck>(e.Row);
		}


		protected virtual void APPayment_PrintCheck_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			switch (((APPayment)e.Row).DocType)
			{
				case APDocType.Refund:
				case APDocType.Prepayment:
				case APDocType.VoidCheck:
					e.NewValue = false;
					e.Cancel = true;
					break;
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="payment"></param>
		/// <returns></returns>
		protected virtual bool MustPrintCheck(APPayment payment)
		{
			return MustPrintCheck(payment, paymenttype.Current);
		}

		public static bool MustPrintCheck(IPrintCheckControlable payment, PaymentMethod paymentMethod)
			{
			return payment != null
				&& paymentMethod != null
				&& !APDocType.VoidCheck.Equals(payment.DocType)
				&& !APDocType.VoidQuickCheck.Equals(payment.DocType)
				&& !APDocType.Prepayment.Equals(payment.DocType)
				&& !APDocType.Refund.Equals(payment.DocType)
				&& !APDocType.DebitAdj.Equals(payment.DocType)
				&& payment.Printed != true
				&& payment.PrintCheck == true
				&& paymentMethod.PrintOrExport == true;
			}

		public static bool IsCheckReallyPrinted(IPrintCheckControlable payment)
		{
			return payment.Printed == true && payment.PrintCheck == true;
		}

		protected virtual void APPayment_PrintCheck_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			if (MustPrintCheck(e.Row as APPayment))
			{
				sender.SetValueExt<APPayment.extRefNbr>(e.Row, null);
			}
		}

		protected virtual void APPayment_AdjDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			if (((APPayment)e.Row).Released != true && ((APPayment)e.Row).VoidAppl != true)
			{
				CurrencyInfoAttribute.SetEffectiveDate<APPayment.adjDate>(sender, e);
				sender.SetDefaultExt<APPayment.depositAfter>(e.Row);
				LoadInvoicesProc(true);
			}
		}

		protected virtual void APPayment_AdjDate_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			APPayment payment = (APPayment) e.Row;

			if (payment.VoidAppl != true)
			{
				if (vendor.Current != null && vendor.Current.Vendor1099 == true)
				{
					string year1099 = ((DateTime)e.NewValue).Year.ToString();
					AP1099Year year = PXSelect<AP1099Year, 
												Where<AP1099Year.finYear, Equal<Required<AP1099Year.finYear>>,
														And<AP1099Year.branchID, Equal<Required<AP1099Year.branchID>>>>>
												.Select(this, year1099, BranchMaint.FindBranchByID(this, payment.BranchID).ParentBranchID);

					if (year != null && year.Status != "N")
					{
						throw new PXSetPropertyException(Messages.AP1099_PaymentDate_NotIn_OpenYear, PXUIFieldAttribute.GetDisplayName<APPayment.adjDate>(sender));
					}
				}
			}
		}

		protected virtual void APPayment_CuryOrigDocAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			if (!(bool)((APPayment)e.Row).Released)
			{
				sender.SetValueExt<APPayment.curyDocBal>(e.Row, ((APPayment)e.Row).CuryOrigDocAmt);
			}
		}

		protected virtual void APAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
			string errmsg = PXUIFieldAttribute.GetError<APAdjust.adjdRefNbr>(sender, e.Row);

			e.Cancel = (((APAdjust)e.Row).AdjdRefNbr == null || string.IsNullOrEmpty(errmsg) == false);
		}

		protected virtual void APAdjust_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			//Prepayment requests have Check CuryInfoID in AdjdCuryInfoID should not be deleted
			if (((APAdjust)e.Row).AdjdCuryInfoID != ((APAdjust)e.Row).AdjgCuryInfoID && ((APAdjust)e.Row).AdjdCuryInfoID != ((APAdjust)e.Row).AdjdOrigCuryInfoID && ((APAdjust)e.Row).VoidAdjNbr == null)
			{
				foreach (CurrencyInfo info in CurrencyInfo_CuryInfoID.Select(((APAdjust)e.Row).AdjdCuryInfoID))
				{
					currencyinfo.Delete(info);
				}
			}
		}

		public virtual void APAdjust_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			APAdjust doc = (APAdjust)e.Row;

			if ((e.Operation & PXDBOperation.Command) == PXDBOperation.Delete)
			{
				return;
			}

			if (doc.Released != true && doc.AdjNbr < Document.Current?.LineCntr)
			{
				sender.RaiseExceptionHandling<APAdjust.adjdRefNbr>(doc, doc.AdjdRefNbr, new PXSetPropertyException(AR.Messages.ApplicationStateInvalid));
			}

			if (((DateTime)doc.AdjdDocDate).CompareTo((DateTime)Document.Current.AdjDate) > 0 &&
				(doc.AdjgDocType != APDocType.Check && doc.AdjgDocType != APDocType.VoidCheck && doc.AdjgDocType != APDocType.Prepayment || APSetup.Current.EarlyChecks == false))
			{
				if (sender.RaiseExceptionHandling<APAdjust.adjdRefNbr>(e.Row, doc.AdjdRefNbr, new PXSetPropertyException(Messages.ApplDate_Less_DocDate, PXErrorLevel.RowError, PXUIFieldAttribute.GetDisplayName<APPayment.adjDate>(Document.Cache))))
				{
					throw new PXRowPersistingException(PXDataUtils.FieldName<APAdjust.adjdDocDate>(), doc.AdjdDocDate, Messages.ApplDate_Less_DocDate, PXUIFieldAttribute.GetDisplayName<APPayment.adjDate>(Document.Cache));
				}
			}

			if (((string)doc.AdjdFinPeriodID).CompareTo((string)Document.Current.AdjFinPeriodID) > 0 &&
				(doc.AdjgDocType != APDocType.Check && doc.AdjgDocType != APDocType.VoidCheck && doc.AdjgDocType != APDocType.Prepayment || APSetup.Current.EarlyChecks == false))
			{
				if (sender.RaiseExceptionHandling<APAdjust.adjdRefNbr>(e.Row, doc.AdjdRefNbr, new PXSetPropertyException(Messages.ApplPeriod_Less_DocPeriod, PXErrorLevel.RowError, PXUIFieldAttribute.GetDisplayName<APPayment.adjFinPeriodID>(Document.Cache))))
				{
					throw new PXRowPersistingException(PXDataUtils.FieldName<APAdjust.adjdFinPeriodID>(), doc.AdjdFinPeriodID, Messages.ApplPeriod_Less_DocPeriod, PXUIFieldAttribute.GetDisplayName<APPayment.adjFinPeriodID>(Document.Cache));
				}
			}

			if (doc.CuryDocBal < 0m)
			{
				sender.RaiseExceptionHandling<APAdjust.curyAdjgAmt>(e.Row, doc.CuryAdjgAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
			}

			if (doc.AdjgDocType != APDocType.QuickCheck && doc.CuryDiscBal < 0m)
			{
				sender.RaiseExceptionHandling<APAdjust.curyAdjgPPDAmt>(e.Row, doc.CuryAdjgPPDAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
			}

			if (doc.AdjgDocType != APDocType.QuickCheck && doc.CuryWhTaxBal < 0m)
			{
				sender.RaiseExceptionHandling<APAdjust.curyAdjgWhTaxAmt>(e.Row, doc.CuryAdjgWhTaxAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
			}

			doc.PendingPPD = doc.CuryAdjgPPDAmt != 0m && doc.AdjdHasPPDTaxes == true;
			if (doc.PendingPPD == true && doc.CuryDocBal != 0m && doc.Voided != true)
			{
				sender.RaiseExceptionHandling<APAdjust.curyAdjgPPDAmt>(e.Row, doc.CuryAdjgPPDAmt, new PXSetPropertyException(Messages.PartialPPD));
			}
		}

		protected virtual void APAdjust_AdjdRefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = Document.Current != null && (Document.Current.VoidAppl == true || Document.Current.DocType == APDocType.QuickCheck) || this._AutoPaymentApp;
		}

		protected virtual void APAdjust_AdjdRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			try
			{
				APAdjust adj = e.Row as APAdjust;

				if (adj.AdjdCuryInfoID == null)
				{
					foreach (PXResult<APInvoice, CurrencyInfo> res in PXSelectJoin<APInvoice,
																		 InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>>,
																		    Where<APInvoice.vendorID, Equal<Current<APPayment.vendorID>>,
																			  And<APInvoice.docType, Equal<Required<APInvoice.docType>>,
																			  And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>>.
																	   Select(this, adj.AdjdDocType, adj.AdjdRefNbr))
					{
						APAdjust_AdjdRefNbr_FieldUpdated<APInvoice>(res, adj);
						return;
					}

					foreach (PXResult<APPayment, CurrencyInfo> res in PXSelectJoin<APPayment,
																		 InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<APPayment.curyInfoID>>>,
																		     Where<APPayment.vendorID, Equal<Current<APPayment.vendorID>>,
																			   And<APPayment.docType, Equal<Required<APPayment.docType>>,
																			   And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>>.
																	   Select(this, adj.AdjdDocType, adj.AdjdRefNbr))
					{
						APAdjust_AdjdRefNbr_FieldUpdated<APPayment>(res, adj);
					}
				}
				else if (adj.AdjgCuryInfoID != null)
				{
					FillDiscAmts(adj);
					CalcBalances(adj as object, true, false);
				}
			}
			catch (PXSetPropertyException ex)
			{
				throw new PXException(ex.Message);
			}
		}

		private void APAdjust_AdjdRefNbr_FieldUpdated<T>(PXResult<T, CurrencyInfo> res, APAdjust adj)
			where T : APRegister, IInvoice, new()
		{
			CurrencyInfo info = (CurrencyInfo)res;
			CurrencyInfo info_copy = null;
			T invoice = (T)res;

				info_copy = PXCache<CurrencyInfo>.CreateCopy((CurrencyInfo)res);
				info_copy.CuryInfoID = null;
				info_copy = (CurrencyInfo)currencyinfo.Cache.Insert(info_copy);

				//valid for future Bills payed with Checks & future bills payed with Prepayments
				currencyinfo.Cache.SetValueExt<CurrencyInfo.curyEffDate>(info_copy, Document.Current.DocDate);

			adj.VendorID = invoice.VendorID;
			adj.AdjgDocDate = Document.Current.AdjDate;
			adj.AdjgCuryInfoID = Document.Current.CuryInfoID;
			adj.AdjdCuryInfoID = info_copy.CuryInfoID;
			adj.AdjdOrigCuryInfoID = info.CuryInfoID;
			adj.AdjdBranchID = invoice.BranchID;
			adj.AdjdAPAcct = invoice.APAccountID;
			adj.AdjdAPSub = invoice.APSubID;
			adj.AdjdDocDate = invoice.DocDate;
			adj.AdjdFinPeriodID = invoice.FinPeriodID;
			adj.AdjdHasPPDTaxes = invoice.HasPPDTaxes;
			adj.Released = false;
			adj.PendingPPD = false;

			CalcBalances<T>(adj, invoice, false, true);

			if (adj.CuryWhTaxBal >= 0m && adj.CuryDiscBal >= 0m && adj.CuryDocBal - adj.CuryWhTaxBal - adj.CuryDiscBal <= 0m)
			{
				//no amount suggestion is possible
				return;
			}

			decimal? CuryApplDiscAmt = (adj.AdjgDocType == APDocType.DebitAdj) ? 0m : adj.CuryDiscBal;
			decimal? CuryApplWhTaxAmt = (adj.AdjgDocType == APDocType.DebitAdj) ? 0m : adj.CuryWhTaxBal;
			decimal? CuryApplAmt = adj.CuryDocBal - CuryApplWhTaxAmt - CuryApplDiscAmt;
			decimal? CuryUnappliedBal = Document.Current.CuryUnappliedBal;

			if (Document.Current != null && adj.AdjgBalSign < 0m)
			{
				if (CuryUnappliedBal < 0m)
				{
					CuryApplAmt = Math.Min((decimal)CuryApplAmt, Math.Abs((decimal)CuryUnappliedBal));
				}
			}
			else if (Document.Current != null && CuryUnappliedBal > 0m && adj.AdjgBalSign > 0m && CuryUnappliedBal < CuryApplDiscAmt)
			{
				CuryApplAmt = CuryUnappliedBal;
				CuryApplDiscAmt = 0m;
			}
			else if (Document.Current != null && CuryUnappliedBal > 0m && adj.AdjgBalSign > 0m)
			{
				CuryApplAmt = Math.Min((decimal)CuryApplAmt, (decimal)CuryUnappliedBal);
			}
			else if (Document.Current != null && CuryUnappliedBal <= 0m && Document.Current.CuryOrigDocAmt > 0)
			{
				CuryApplAmt = 0m;
			}

			adj.CuryAdjgAmt = CuryApplAmt;
			adj.CuryAdjgDiscAmt = CuryApplDiscAmt;
			adj.CuryAdjgPPDAmt = CuryApplDiscAmt;
			adj.CuryAdjgWhTaxAmt = CuryApplWhTaxAmt;

			CalcBalances<T>(adj, invoice, true, true);
		}

		protected virtual void APAdjust_CuryDocBal_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			e.Cancel = true;
			if (InternalCall) return;

				if (e.Row != null && ((APAdjust)e.Row).AdjdCuryInfoID != null && ((APAdjust)e.Row).CuryDocBal == null && sender.GetStatus(e.Row) != PXEntryStatus.Deleted)
				{
					CalcBalances(e.Row, false);
				}
				if (e.Row != null)
				{
					e.NewValue = ((APAdjust)e.Row).CuryDocBal;
				}
			}

		protected virtual void APAdjust_CuryDiscBal_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			e.Cancel = true;
			if (InternalCall) return;

				if (e.Row != null && ((APAdjust)e.Row).AdjdCuryInfoID != null && ((APAdjust)e.Row).CuryDiscBal == null && sender.GetStatus(e.Row) != PXEntryStatus.Deleted)
				{
					CalcBalances(e.Row, false);
				}
				if (e.Row != null)
				{
					e.NewValue = ((APAdjust)e.Row).CuryDiscBal;
				}
			}

		protected virtual void APAdjust_CuryWhTaxBal_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			e.Cancel = true;
			if (InternalCall) return;

				if (e.Row != null && ((APAdjust)e.Row).AdjdCuryInfoID != null && ((APAdjust)e.Row).CuryWhTaxBal == null && sender.GetStatus(e.Row) != PXEntryStatus.Deleted)
				{
					CalcBalances(e.Row, false);
				}
				if (e.Row != null)
				{
					e.NewValue = ((APAdjust)e.Row).CuryWhTaxBal;
				}
			}

		protected virtual void APAdjust_AdjdCuryRate_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if ((decimal)e.NewValue <= 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_GT, ((int)0).ToString());
			}
		}

		protected virtual void APAdjust_AdjdCuryRate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;

			CurrencyInfo pay_info = PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<APAdjust.adjgCuryInfoID>>>>.SelectSingleBound(this, new object[] { e.Row });
			CurrencyInfo vouch_info = PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<APAdjust.adjdCuryInfoID>>>>.SelectSingleBound(this, new object[] { e.Row });

			decimal payment_docbal = (decimal)adj.CuryAdjgAmt;
			decimal discount_docbal = (decimal)adj.CuryAdjgDiscAmt;
			decimal invoice_amount;

			if (string.Equals(pay_info.CuryID, vouch_info.CuryID) && adj.AdjdCuryRate != 1m)
			{
				adj.AdjdCuryRate = 1m;
				vouch_info.SetCuryEffDate(currencyinfo.Cache, Document.Current.DocDate);
			}
			else if (string.Equals(vouch_info.CuryID, vouch_info.BaseCuryID))
			{
				adj.AdjdCuryRate = pay_info.CuryMultDiv == "M" ? 1 / pay_info.CuryRate : pay_info.CuryRate;
			}
			else
			{
				vouch_info.CuryRate = adj.AdjdCuryRate;
				vouch_info.RecipRate = Math.Round(1m / (decimal)adj.AdjdCuryRate, 8, MidpointRounding.AwayFromZero);
				vouch_info.CuryMultDiv = "M";
				PXCurrencyAttribute.CuryConvBase(sender, vouch_info, (decimal)adj.CuryAdjdAmt, out payment_docbal);
				PXCurrencyAttribute.CuryConvBase(sender, vouch_info, (decimal)adj.CuryAdjdDiscAmt, out discount_docbal);
				PXCurrencyAttribute.CuryConvBase(sender, vouch_info, (decimal)adj.CuryAdjdAmt + (decimal)adj.CuryAdjdDiscAmt, out invoice_amount);

				vouch_info.CuryRate = Math.Round((decimal)adj.AdjdCuryRate * (pay_info.CuryMultDiv == "M" ? (decimal)pay_info.CuryRate : 1m / (decimal)pay_info.CuryRate), 8, MidpointRounding.AwayFromZero);
				vouch_info.RecipRate = Math.Round((pay_info.CuryMultDiv == "M" ? 1m / (decimal)pay_info.CuryRate : (decimal)pay_info.CuryRate) / (decimal)adj.AdjdCuryRate, 8, MidpointRounding.AwayFromZero);

				if (payment_docbal + discount_docbal != invoice_amount)
					discount_docbal += invoice_amount - discount_docbal - payment_docbal;
			}

			if (Caches[typeof(CurrencyInfo)].GetStatus(vouch_info) == PXEntryStatus.Notchanged || Caches[typeof(CurrencyInfo)].GetStatus(vouch_info) == PXEntryStatus.Held)
			{
				Caches[typeof(CurrencyInfo)].SetStatus(vouch_info, PXEntryStatus.Updated);
			}

			if (payment_docbal != (decimal)adj.CuryAdjgAmt)
				sender.SetValue<APAdjust.curyAdjgAmt>(e.Row, payment_docbal);

			if (discount_docbal != (decimal)adj.CuryAdjgDiscAmt)
				sender.SetValue<APAdjust.curyAdjgDiscAmt>(e.Row, discount_docbal);

			CalcBalances(e.Row, true);
		}

		protected virtual void APAdjust_CuryAdjgAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null || adj.CuryWhTaxBal == null)
			{
				CalcBalances(e.Row, false);
			}

			if (adj.CuryDocBal == null)
			{
				sender.RaiseExceptionHandling<APAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr,
					new PXSetPropertyException(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<APAdjust.adjdRefNbr>(sender)));
				return;
			}

			if (adj.VoidAdjNbr == null && (decimal)e.NewValue < 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_GE, 0);
			}

			if (adj.VoidAdjNbr != null && (decimal)e.NewValue > 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_LE, 0);
			}

			if ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgAmt - (decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException(Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgAmt).ToString());
			}
		}

		protected virtual void APAdjust_CuryAdjgPPDAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			if (e.OldValue != null && ((APAdjust)e.Row).CuryDocBal == 0m && ((APAdjust)e.Row).CuryAdjgAmt < (decimal)e.OldValue)
			{
				((APAdjust)e.Row).CuryAdjgDiscAmt = 0m;
			}
			FillDiscAmts((APAdjust)e.Row);
			CalcBalances(e.Row, true);
		}
		protected static void FillDiscAmts(APAdjust adj)
		{
			adj.CuryAdjgDiscAmt = adj.CuryAdjgPPDAmt;
			adj.CuryAdjdDiscAmt = adj.CuryAdjdPPDAmt;
			adj.AdjDiscAmt = adj.AdjPPDAmt;
		}
		protected void FillPPDAmts(APAdjust adj)
		{
			adj.CuryAdjgPPDAmt = adj.CuryAdjgDiscAmt;
			adj.CuryAdjdPPDAmt = adj.CuryAdjdDiscAmt;
			adj.AdjPPDAmt = adj.AdjDiscAmt;
		}
		protected virtual void APAdjust_Voided_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CalcBalances(e.Row, true, false);
		}
		protected virtual void APAdjust_CuryAdjgPPDAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null || adj.CuryWhTaxBal == null)
			{
				CalcBalances(e.Row, false);
			}

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null)
			{
				sender.RaiseExceptionHandling<APAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr,
					new PXSetPropertyException(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<APAdjust.adjdRefNbr>(sender)));
				return;
			}

			if (adj.VoidAdjNbr == null && (decimal)e.NewValue < 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_GE, ((int)0).ToString());
			}

			if (adj.VoidAdjNbr != null && (decimal)e.NewValue > 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_LE, ((int)0).ToString());
			}

			if ((decimal)adj.CuryDiscBal + (decimal)adj.CuryAdjgPPDAmt - (decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException((decimal)adj.CuryDiscBal + (decimal)adj.CuryAdjgDiscAmt == 0 ? CS.Messages.Entry_EQ : Messages.Entry_LE, ((decimal)adj.CuryDiscBal + (decimal)adj.CuryAdjgPPDAmt).ToString());
			}

			if (adj.CuryAdjgAmt != null && (sender.GetValuePending<APAdjust.curyAdjgAmt>(e.Row) == PXCache.NotSetValue || (Decimal?)sender.GetValuePending<APAdjust.curyAdjgAmt>(e.Row) == adj.CuryAdjgAmt))
			{
				if ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgPPDAmt - (decimal)e.NewValue < 0)
				{
					throw new PXSetPropertyException((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgDiscAmt == 0 ? CS.Messages.Entry_EQ : Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgPPDAmt).ToString());
				}
			}
		}

		protected virtual void APAdjust_CuryAdjgDiscAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			APAdjust adj = e.Row as APAdjust;
			if (adj == null) return;

			FillPPDAmts(adj);
			CalcBalances(e.Row, true);
		}
		protected virtual void APAdjust_CuryAdjgAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CalcBalances(e.Row, true, false);
		}

		protected virtual void APAdjust_CuryAdjgWhTaxAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null || adj.CuryWhTaxBal == null)
			{
				CalcBalances(e.Row, false);
			}

			if (adj.CuryDocBal == null || adj.CuryWhTaxBal == null)
			{
				sender.RaiseExceptionHandling<APAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr,
					new PXSetPropertyException(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<APAdjust.adjdRefNbr>(sender)));
				return;
			}

			if (adj.VoidAdjNbr == null && (decimal)e.NewValue < 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_GE, 0);
			}

			if (adj.VoidAdjNbr != null && (decimal)e.NewValue > 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_LE, 0);
			}

			if ((decimal)adj.CuryWhTaxBal + (decimal)adj.CuryAdjgWhTaxAmt - (decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException(Messages.Entry_LE, ((decimal)adj.CuryWhTaxBal + (decimal)adj.CuryAdjgWhTaxAmt).ToString());
			}

			if (adj.CuryAdjgAmt != null && (sender.GetValuePending<APAdjust.curyAdjgAmt>(e.Row) == PXCache.NotSetValue || (decimal?)sender.GetValuePending<APAdjust.curyAdjgAmt>(e.Row) == adj.CuryAdjgAmt))
			{
				if ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgWhTaxAmt - (decimal)e.NewValue < 0)
				{
					throw new PXSetPropertyException(Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgWhTaxAmt).ToString());
				}
			}
		}

		protected virtual void APAdjust_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;
			if (adj == null || InternalCall) return;

			bool adjNotReleased = (bool)(adj.Released == false);
			PXUIFieldAttribute.SetEnabled<APAdjust.adjdDocType>(cache, adj, adj.AdjdRefNbr == null);
			PXUIFieldAttribute.SetEnabled<APAdjust.adjdRefNbr>(cache, adj, adj.AdjdRefNbr == null);
			PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgAmt>(cache, adj, adjNotReleased && (adj.Voided != true));
			PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgPPDAmt>(cache, adj, adjNotReleased && (adj.Voided != true));
			PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgWhTaxAmt>(cache, adj, adjNotReleased && (adj.Voided != true));
			PXUIFieldAttribute.SetEnabled<APAdjust.adjBatchNbr>(cache, adj, false);
			//
			PXUIFieldAttribute.SetVisible<APAdjust.adjBatchNbr>(cache, adj, !adjNotReleased);

			bool EnableCrossRate = false;
			if (adj.Released == false)
			{
				CurrencyInfo pay_info = PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<APAdjust.adjgCuryInfoID>>>>.SelectSingleBound(this, new object[] { e.Row });
				CurrencyInfo vouch_info = PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<APAdjust.adjdCuryInfoID>>>>.SelectSingleBound(this, new object[] { e.Row });

				EnableCrossRate = string.Equals(pay_info.CuryID, vouch_info.CuryID) == false && string.Equals(vouch_info.CuryID, vouch_info.BaseCuryID) == false;
			}
			PXUIFieldAttribute.SetEnabled<APAdjust.adjdCuryRate>(cache, adj, EnableCrossRate);
		}

		protected virtual void APAdjust_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;
			if (_IsVoidCheckInProgress == false && adj.Voided == true)
			{
				throw new PXSetPropertyException(ErrorMessages.CantUpdateRecord);
			}
		}
		protected virtual void APAdjust_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			APAdjust adj = (APAdjust)e.Row;
			foreach (APInvoice voucher in APInvoice_VendorID_DocType_RefNbr.Select(adj.VendorID, adj.AdjdDocType, adj.AdjdRefNbr))
			{
				PaymentEntry.WarnPPDiscount(this, adj.AdjgDocDate, voucher, adj, adj.CuryAdjgPPDAmt);
			}
		}
		

	protected virtual void APAdjust_CuryAdjgWhTaxAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CalcBalances(e.Row, true);
		}

		public bool TakeDiscAlways = false;

		private void CalcBalances(object row, bool isCalcRGOL)
		{
			CalcBalances(row, isCalcRGOL, !TakeDiscAlways);
		}

		private void CalcBalances(object row, bool isCalcRGOL, bool DiscOnDiscDate)
		{
			APAdjust adj = (APAdjust)row;

			foreach (APInvoice voucher in APInvoice_VendorID_DocType_RefNbr.Select(adj.VendorID, adj.AdjdDocType, adj.AdjdRefNbr))
			{
				CalcBalances<APInvoice>(adj, voucher, isCalcRGOL, DiscOnDiscDate);
				return;
			}

			foreach (APPayment payment in APPayment_VendorID_DocType_RefNbr.Select(adj.VendorID, adj.AdjdDocType, adj.AdjdRefNbr))
			{
				CalcBalances<APPayment>(adj, payment, isCalcRGOL, DiscOnDiscDate);
			}
		}

		private void CalcBalances<T>(APAdjust adj, T voucher, bool isCalcRGOL)
			where T : IInvoice
		{
			CalcBalances<T>(CurrencyInfo_CuryInfoID, adj, voucher, isCalcRGOL, !TakeDiscAlways);
		}

		private void CalcBalances<T>(APAdjust adj, T voucher, bool isCalcRGOL, bool DiscOnDiscDate)
			where T : IInvoice
		{
			CalcBalances<T>(CurrencyInfo_CuryInfoID, adj, voucher, isCalcRGOL, DiscOnDiscDate);
		}

		public static void CalcBalances<T>(PXSelectBase<CurrencyInfo> CurrencyInfo_CuryInfoID, APAdjust adj, T voucher, bool isCalcRGOL, bool DiscOnDiscDate)
			where T : IInvoice
		{
			bool isPendingPPD = adj.CuryAdjgPPDAmt != null && adj.CuryAdjgPPDAmt != 0m && adj.AdjdHasPPDTaxes == true;
			if (isPendingPPD)
			{
				adj.CuryAdjgDiscAmt = 0m;
				adj.CuryAdjdDiscAmt = 0m;
				adj.AdjDiscAmt = 0m;
			}
			PaymentEntry.CalcBalances<T, APAdjust>(CurrencyInfo_CuryInfoID, adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, voucher, adj);

			if (DiscOnDiscDate)
			{
				PaymentEntry.CalcDiscount<T, APAdjust>(adj.AdjgDocDate, voucher, adj);
			}
			

			CurrencyInfo pay_info = CurrencyInfo_CuryInfoID.Select(adj.AdjgCuryInfoID);
			CurrencyInfo vouch_info = CurrencyInfo_CuryInfoID.Select(adj.AdjdCuryInfoID);

			if (vouch_info != null && string.Equals(pay_info.CuryID, vouch_info.CuryID) == false)
			{
				decimal voucherCuryRateMultiplier = vouch_info.CuryMultDiv == "M" ? vouch_info.CuryRate.Value
																				  : 1 / vouch_info.CuryRate.Value;
				decimal payInfoCuryRateMultiplier = pay_info.CuryMultDiv == "M" ? 1 / pay_info.CuryRate.Value
																				: pay_info.CuryRate.Value;
				adj.AdjdCuryRate = Math.Round(voucherCuryRateMultiplier * payInfoCuryRateMultiplier, 8, MidpointRounding.AwayFromZero);
			}
			else
			{
				adj.AdjdCuryRate = 1m;
			}

			PaymentEntry.AdjustBalance<APAdjust>(CurrencyInfo_CuryInfoID, adj);
			if (isPendingPPD && adj.AdjPPDAmt == null && adj.Released != true)
			{
				APAdjust adjPPD = PXCache<APAdjust>.CreateCopy(adj);
				FillDiscAmts(adjPPD);

				PaymentEntry.AdjustBalance<APAdjust>(CurrencyInfo_CuryInfoID, adjPPD);
				adj.AdjPPDAmt = adjPPD.AdjDiscAmt;
			}

			if (isCalcRGOL && (adj.Voided == null || adj.Voided == false))
			{
				PaymentEntry.CalcRGOL<T, APAdjust>(CurrencyInfo_CuryInfoID, voucher, adj);
				adj.RGOLAmt = (bool)adj.ReverseGainLoss ? -1m * adj.RGOLAmt : adj.RGOLAmt;

				if (adj.AdjdDocType == APDocType.Prepayment && (adj.AdjgDocType == APDocType.Check || adj.AdjgDocType == APDocType.VoidCheck))
				{
					adj.RGOLAmt = 0m;
				}
				decimal? CuryAdjdPPDAmt = adj.CuryAdjdDiscAmt;
				if (isPendingPPD)
				{
					APAdjust adjPPD = PXCache<APAdjust>.CreateCopy(adj);
					FillDiscAmts(adjPPD);

					PaymentEntry.CalcRGOL<T, APAdjust>(CurrencyInfo_CuryInfoID, voucher, adjPPD);
					CuryAdjdPPDAmt = adjPPD.CuryAdjdDiscAmt;
				}

				adj.CuryAdjdPPDAmt = CuryAdjdPPDAmt;
			}

			if (isPendingPPD && adj.Voided != true)
			{
				adj.CuryDocBal -= adj.CuryAdjgPPDAmt;
				adj.DocBal -= adj.AdjPPDAmt;
				adj.CuryDiscBal -= adj.CuryAdjgPPDAmt;
				adj.DiscBal -= adj.AdjPPDAmt;
			}
		}

		protected virtual void CurrencyInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			CurrencyInfo info = e.Row as CurrencyInfo;
			if (info != null)
			{
				bool curyenabled = info.AllowUpdate(this.Adjustments.Cache) && Document.Current != null && Document.Current.Released == false;

				if (vendor.Current != null && !(bool)vendor.Current.AllowOverrideRate)
				{
					curyenabled = false;
				}

				PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyRateTypeID>(sender, info, curyenabled);
				PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyEffDate>(sender, info, curyenabled);
				PXUIFieldAttribute.SetEnabled<CurrencyInfo.sampleCuryRate>(sender, info, curyenabled);
				PXUIFieldAttribute.SetEnabled<CurrencyInfo.sampleRecipRate>(sender, info, curyenabled);
			}
		}

		protected virtual void CurrencyInfo_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			if (!sender.ObjectsEqual<CurrencyInfo.curyID, CurrencyInfo.curyRate, CurrencyInfo.curyMultDiv>(e.Row, e.OldRow))
			{
				foreach (APAdjust adj in PXSelect<APAdjust, Where<APAdjust.adjgCuryInfoID, Equal<Required<APAdjust.adjgCuryInfoID>>>>.Select(sender.Graph, ((CurrencyInfo)e.Row).CuryInfoID))
				{
					if (Adjustments.Cache.GetStatus(adj) == PXEntryStatus.Notchanged)
					{
						Adjustments.Cache.SetStatus(adj, PXEntryStatus.Updated);
					}

					CalcBalances(adj, true);

					if (adj.CuryDocBal < 0m)
					{
						Adjustments.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>(adj, adj.CuryAdjgAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
					}

					if (adj.CuryDiscBal < 0m)
					{
						Adjustments.Cache.RaiseExceptionHandling<APAdjust.curyAdjgPPDAmt>(adj, adj.CuryAdjgPPDAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
					}

					if (adj.CuryWhTaxBal < 0m)
					{
						Adjustments.Cache.RaiseExceptionHandling<APAdjust.curyAdjgWhTaxAmt>(adj, adj.CuryAdjgWhTaxAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
					}
				}
			}
		}

		protected virtual void APPayment_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			APPayment doc = (APPayment)e.Row;

			//true for DebitAdj and Prepayment Requests
			if (doc.Released != true && doc.CashAccountID == null
				&& sender.RaiseExceptionHandling<APPayment.cashAccountID>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(APPayment.cashAccountID).Name)))
				{
					throw new PXRowPersistingException(typeof(APPayment.cashAccountID).Name, null, ErrorMessages.FieldIsEmpty, typeof(APPayment.cashAccountID).Name);
				}

			//true for DebitAdj and Prepayment Requests
			if (doc.Released != true && string.IsNullOrEmpty(doc.PaymentMethodID)
				&& sender.RaiseExceptionHandling<APPayment.paymentMethodID>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(APPayment.paymentMethodID).Name)))
				{
					throw new PXRowPersistingException(typeof(APPayment.paymentMethodID).Name, null, ErrorMessages.FieldIsEmpty, typeof(APPayment.paymentMethodID).Name);
				}

			PaymentRefAttribute.SetUpdateCashManager<APPayment.extRefNbr>(sender, e.Row, doc.DocType != APDocType.VoidCheck && doc.DocType != APDocType.Refund);

			string errMsg;
			// VerifyAdjFinPeriodID() compares Payment "Application Period" only with applications, that have been released. Sometimes, this may cause an erorr 
			// during the action, while document is saved and closed (because of Persist() for each action) - this why doc.OpenDoc flag has been used as a criteria.
			if (doc.OpenDoc == true && !VerifyAdjFinPeriodID(doc, doc.AdjFinPeriodID, out errMsg))
			{
				if (sender.RaiseExceptionHandling<APPayment.adjFinPeriodID>(e.Row,
					FinPeriodIDAttribute.FormatForDisplay(doc.AdjFinPeriodID), new PXSetPropertyException(errMsg)))
				{
					throw new PXRowPersistingException(typeof(APPayment.adjFinPeriodID).Name, FinPeriodIDAttribute.FormatForError(doc.AdjFinPeriodID), errMsg);
				}
			}

		    if (APSetup.Current.SuggestPaymentAmount.GetValueOrDefault(false) && doc.CuryUnappliedBal.HasValue && doc.CuryUnappliedBal.Value < 0 && doc.CuryOrigDocAmt == 0)
		    {
		        doc.CuryOrigDocAmt = doc.CuryApplAmt;
		        doc.CuryUnappliedBal = 0;
		        Document.Cache.RaiseFieldUpdated<APPayment.curyOrigDocAmt>(doc, null);
		    }
		}

		protected virtual void APPayment_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
		{
			APPayment row = (APPayment)e.Row;
			if (e.Operation == PXDBOperation.Insert && e.TranStatus == PXTranStatus.Open)
			{
				if (row.DocType == APDocType.Check || row.DocType == APDocType.Prepayment)
				{
					string searchDocType = row.DocType == APDocType.Check ? APDocType.Prepayment : APDocType.Check;
					APPayment duplicatePayment = PXSelect<APPayment,
						Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>
						.Select(this, searchDocType, row.RefNbr);
					APInvoice inv = null;
					if (searchDocType == APDocType.Prepayment)
					{
						inv = PXSelect<APInvoice, Where<APInvoice.docType, Equal<APDocType.prepayment>, And<APInvoice.refNbr, Equal<Required<APPayment.refNbr>>>>>.Select(this, row.RefNbr);
					}
					if (duplicatePayment != null && inv == null)
					{
						throw new PXRowPersistedException(typeof(APPayment.refNbr).Name, row.RefNbr, Messages.SameRefNbr, searchDocType == APDocType.Check ? Messages.Check : Messages.Prepayment, row.RefNbr);
					}
				}
			}
		}

		protected virtual bool PaymentRefMustBeUnique => PaymentRefAttribute.PaymentRefMustBeUnique(paymenttype.Current);
		/// <summary>
		/// Determines whether the approval is required for the document.
		/// </summary>
		/// <param name="doc">The document for which the check should be performed.</param>
		/// <param name="cache">The cache.</param>
		/// <returns>Returns <c>true</c> if approval is required; otherwise, returns <c>false</c>.</returns>
		public bool IsApprovalRequired(APPayment doc, PXCache cache)
		{
			var isApprovalInstalled = PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>();
			var areMapsAssigned = Approval.GetAssignedMaps(doc, cache).Any();
			return (doc.DocType == APDocType.Check || doc.DocType == APDocType.Prepayment) && isApprovalInstalled && areMapsAssigned;
		}
		protected virtual void APPayment_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			APPayment doc = e.Row as APPayment;

			if (doc == null || InternalCall) return;
			bool dontApprove = !IsApprovalRequired(doc, cache);
			if (doc.DontApprove != dontApprove)
			{
				cache.SetValueExt<APPayment.dontApprove>(doc, dontApprove);
			}

			// We need this for correct tabs repainting
			// in migration mode.
			// 
			Adjustments.Cache.AllowSelect =
			PaymentCharges.AllowSelect = true;

			if (vendor.Current != null && doc.VendorID != vendor.Current.BAccountID)
			{
				vendor.Current = null;
			}

			if (finperiod.Current != null && !Equals(finperiod.Current.FinPeriodID, doc.AdjFinPeriodID))
			{
				finperiod.Current = null;
			}
			bool docTypeNotDebitAdj = (doc.DocType != APDocType.DebitAdj);
			PXUIFieldAttribute.SetVisible<APPayment.curyID>(cache, doc, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
			PXUIFieldAttribute.SetVisible<APPayment.cashAccountID>(cache, doc, docTypeNotDebitAdj);
			PXUIFieldAttribute.SetVisible<APPayment.cleared>(cache, doc, docTypeNotDebitAdj);
			PXUIFieldAttribute.SetVisible<APPayment.clearDate>(cache, doc, docTypeNotDebitAdj);
			PXUIFieldAttribute.SetVisible<APPayment.paymentMethodID>(cache, doc, docTypeNotDebitAdj);
			PXUIFieldAttribute.SetVisible<APPayment.extRefNbr>(cache, doc, docTypeNotDebitAdj);
			bool ptInstanceVisible = false;

			if (docTypeNotDebitAdj)
			{
				PaymentTypeInstance ptInstance = PXSelect<PaymentTypeInstance, Where<PaymentTypeInstance.cashAccountID, Equal<Required<APPayment.cashAccountID>>,
					And<PaymentTypeInstance.paymentMethodID, Equal<Required<APPayment.paymentMethodID>>>>>.Select(this, doc.CashAccountID, doc.PaymentMethodID);
				ptInstanceVisible = ptInstance != null;
			}
			PXUIFieldAttribute.SetVisible<APPayment.pTInstanceID>(cache, doc, ptInstanceVisible);
			//true for DebitAdj and Prepayment Requests
			bool docReleased = doc.Released == true;
			bool docOnHold = doc.Hold == true;
			bool docOpen = doc.OpenDoc == true;
			bool docVoidAppl = doc.VoidAppl == true;
			bool docReallyPrinted = IsDocReallyPrinted(doc);

			const bool isCuryEnabled = false;
			bool clearEnabled = docOnHold && (cashaccount.Current != null) && (cashaccount.Current.Reconcile == true);
			bool holdAdj = false;

			PXUIFieldAttribute.SetRequired<APPayment.cashAccountID>(cache, !docReleased);
			PXUIFieldAttribute.SetRequired<APPayment.paymentMethodID>(cache, !docReleased);

			PXUIFieldAttribute.SetRequired<APPayment.extRefNbr>(cache, !docReleased && PaymentRefMustBeUnique);

			PaymentRefAttribute.SetUpdateCashManager<APPayment.extRefNbr>(cache, e.Row, doc.DocType != APDocType.VoidCheck && doc.DocType != APDocType.Refund);


			bool allowDeposit = doc.DocType == APDocType.Refund;
			PXUIFieldAttribute.SetVisible<APPayment.depositAfter>(cache, doc, allowDeposit && (doc.DepositAsBatch == true));
			PXUIFieldAttribute.SetEnabled<APPayment.depositAfter>(cache, doc, false);
			PXUIFieldAttribute.SetRequired<APPayment.depositAfter>(cache, allowDeposit && (doc.DepositAsBatch == true));
			PXDefaultAttribute.SetPersistingCheck<APPayment.depositAfter>(cache, doc, allowDeposit && (doc.DepositAsBatch == true) ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);

			validateAddresses.SetEnabled(false);
			if (cache.GetStatus(doc) == PXEntryStatus.Notchanged
				&& doc.Status == APDocStatus.Open
				&& !docVoidAppl
				&& doc.AdjDate != null
				&& ((DateTime)doc.AdjDate).CompareTo((DateTime)Accessinfo.BusinessDate) < 0)
			{
				if (Adjustments_Raw.View.SelectSingleBound(new object[] { e.Row }) == null)
				{
					doc.AdjDate = Accessinfo.BusinessDate;

					FinPeriod adjFinPeriod = GL.FinPeriodIDAttribute.FindFinPeriodByDate(this, (DateTime)doc.AdjDate);

					doc.AdjTranPeriodID = adjFinPeriod?.FinPeriodID;
					doc.AdjFinPeriodID = doc.AdjTranPeriodID;
					cache.SetStatus(doc, PXEntryStatus.Held);
				}
			}
			bool isReclassified = false;
			if (doc.DocType == APDocType.DebitAdj && cache.GetStatus(doc) == PXEntryStatus.Inserted)
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				cache.AllowUpdate = false;
				cache.AllowDelete = false;
				Adjustments.Cache.SetAllEditPermissions(allowEdit: false);
				release.SetEnabled(false);
			}
			else if (docVoidAppl && !docReleased)
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<APPayment.adjDate>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<APPayment.adjFinPeriodID>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<APPayment.docDesc>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<APPayment.hold>(cache, doc, true);
				cache.AllowUpdate = true;
				cache.AllowDelete = true;
				Adjustments.Cache.SetAllEditPermissions(allowEdit: false);
				release.SetEnabled(!docOnHold);
			}
			else if (docReleased && docOpen)
			{
				//these to cases do not intersect, no need to evaluate complete
				holdAdj = Adjustments_Raw.Select().RowCast<APAdjust>().TakeWhile(adj => adj.Voided != true).Any(adj => adj.Hold == true);

				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<APPayment.adjDate>(cache, doc, !holdAdj);
				PXUIFieldAttribute.SetEnabled<APPayment.adjFinPeriodID>(cache, doc, !holdAdj);
				PXUIFieldAttribute.SetEnabled<APPayment.hold>(cache, doc, !holdAdj);

				cache.AllowDelete = false;
				cache.AllowUpdate = !holdAdj;

				Adjustments.Cache.SetAllEditPermissions(allowEdit: !holdAdj);
				release.SetEnabled(!docOnHold && !holdAdj);
			}
			else if (docReleased && !docOpen || docReallyPrinted)
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<APPayment.docDesc>(cache, doc, !docReleased);
				PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(cache, doc, !docReleased);
				cache.AllowUpdate = !docReleased;
				cache.AllowDelete = !docReleased && !docReallyPrinted;

				Adjustments.Cache.SetAllEditPermissions(allowEdit: false);

				release.SetEnabled(!docReleased);
			}
			else if (docVoidAppl)
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				cache.AllowDelete = false;
				cache.AllowUpdate = false;
				Adjustments.Cache.SetAllEditPermissions(allowEdit: false);
				release.SetEnabled(!docOnHold);
			}
			else
			{
				CATran tran = PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>>.Select(this, doc.CATranID);
				isReclassified = tran?.RefTranID != null;
				PXUIFieldAttribute.SetEnabled(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<APPayment.status>(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<APPayment.curyID>(cache, doc, isCuryEnabled);
				PXUIFieldAttribute.SetEnabled<APPayment.printCheck>(
					cache,
					doc,
					!docReallyPrinted
						&& doc.DocType != APDocType.VoidCheck
						&& doc.DocType != APDocType.Prepayment
						&& doc.DocType != APDocType.Refund
						&& doc.DocType != APDocType.DebitAdj);


				bool mustPrintCheck = MustPrintCheck(doc);

				PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(cache, doc, !mustPrintCheck && !isReclassified);
	

				cache.AllowDelete = true;
				cache.AllowUpdate = true;

				Adjustments.Cache.SetAllEditPermissions(allowEdit: true);
				release.SetEnabled(!docOnHold);
				PXUIFieldAttribute.SetEnabled<APPayment.curyOrigDocAmt>(cache, doc, !isReclassified);
				PXUIFieldAttribute.SetEnabled<APPayment.vendorID>(cache, doc, !isReclassified);
			}

			APAddress address = Remittance_Address.Select();
			bool enableAddressValidation = !docReleased && address != null && address.IsDefaultAddress != true && address.IsValidated != true;
			validateAddresses.SetEnabled(enableAddressValidation);
			PXUIFieldAttribute.SetEnabled<APPayment.cashAccountID>(cache, doc, !docReleased && !docReallyPrinted && !docVoidAppl && !isReclassified);
			PXUIFieldAttribute.SetEnabled<APPayment.paymentMethodID>(cache, doc, !docReleased && !docReallyPrinted && !docVoidAppl && !isReclassified);
			PXUIFieldAttribute.SetEnabled<APPayment.pTInstanceID>(cache, doc, !docReleased && !docReallyPrinted && !docVoidAppl && !isReclassified);
			PXUIFieldAttribute.SetEnabled<APPayment.cleared>(cache, doc, clearEnabled);
			PXUIFieldAttribute.SetEnabled<APPayment.clearDate>(cache, doc, clearEnabled && doc.Cleared == true);
			PXUIFieldAttribute.SetEnabled<APPayment.docType>(cache, doc);
			PXUIFieldAttribute.SetEnabled<APPayment.refNbr>(cache, doc);
			PXUIFieldAttribute.SetEnabled<APPayment.curyUnappliedBal>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<APPayment.curyApplAmt>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<APPayment.batchNbr>(cache, doc, false);

			voidCheck.SetEnabled(docReleased && doc.Voided != true && (doc.DocType == APDocType.Check || doc.DocType == APDocType.Prepayment) && !holdAdj);
			loadInvoices.SetEnabled(doc.VendorID != null && docOpen && !holdAdj
				&& (doc.DocType == APDocType.Check || doc.DocType == APDocType.Prepayment || doc.DocType == APDocType.Refund)
				&& !docReallyPrinted);

			SetDocTypeList(e.Row);

			editVendor.SetEnabled(vendor?.Current != null);
			if (doc.VendorID != null)
			{
				if (Adjustments_Raw.View.SelectSingleBound(new object[] { e.Row }) != null)
				{
					PXUIFieldAttribute.SetEnabled<APPayment.vendorID>(cache, doc, false);
				}
			}

			if (e.Row != null && ((APPayment)e.Row).CuryApplAmt == null)
			{
				bool IsReadOnly = (cache.GetStatus(e.Row) == PXEntryStatus.Notchanged);
				PXFormulaAttribute.CalcAggregate<APAdjust.curyAdjgAmt>(Adjustments.Cache, e.Row, IsReadOnly);
				cache.RaiseFieldUpdated<APPayment.curyApplAmt>(e.Row, null);

				PXDBCurrencyAttribute.CalcBaseValues<APPayment.curyApplAmt>(cache, e.Row);
				PXDBCurrencyAttribute.CalcBaseValues<APPayment.curyUnappliedBal>(cache, e.Row);
			}

			PXUIFieldAttribute.SetEnabled<APPayment.depositDate>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<APPayment.depositAsBatch>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<APPayment.deposited>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<APPayment.depositNbr>(cache, null, false);

			bool isDeposited = (!string.IsNullOrEmpty(doc.DepositNbr) && !string.IsNullOrEmpty(doc.DepositType));
			CashAccount cashAccount = cashaccount.Current;
			bool isClearingAccount = cashAccount != null && cashAccount.CashAccountID == doc.CashAccountID && cashAccount.ClearingAccount == true;
			bool enableDepositEdit = !isDeposited && cashAccount != null && (isClearingAccount || doc.DepositAsBatch != isClearingAccount);
			if (enableDepositEdit)
			{
				cache.RaiseExceptionHandling<APPayment.depositAsBatch>(doc, doc.DepositAsBatch,
					doc.DepositAsBatch != isClearingAccount
					? new PXSetPropertyException(AR.Messages.DocsDepositAsBatchSettingDoesNotMatchClearingAccountFlag, PXErrorLevel.Warning)
					: null);
			}
			PXUIFieldAttribute.SetEnabled<APPayment.depositAsBatch>(cache, doc, enableDepositEdit);
			PXUIFieldAttribute.SetEnabled<APPayment.depositAfter>(cache, doc, !isDeposited && isClearingAccount && doc.DepositAsBatch == true);
			PaymentCharges.Cache.SetAllEditPermissions(allowEdit: !docReleased && (doc.DocType == APDocType.Check || doc.DocType == APDocType.VoidCheck));

			bool paymentAllowsShowingPrintCheck = doc.DocType != APDocType.VoidCheck &&
												  doc.DocType != APDocType.DebitAdj &&
												  doc.DocType != APDocType.Prepayment &&
												  doc.DocType != APDocType.Refund ||
												  paymenttype.Current == null;

			PXUIFieldAttribute.SetVisible<APPayment.printCheck>(cache, doc, paymenttype.Current?.PrintOrExport == true && paymentAllowsShowingPrintCheck);
			reverseApplication.SetEnabled(
				doc.DocType != APDocType.VoidCheck
				&& doc.Voided != true);

			#region Migration Mode Settings

			bool isMigratedDocument = doc.IsMigratedRecord == true;
			bool isUnreleasedMigratedDocument = isMigratedDocument && !docReleased;
			bool isReleasedMigratedDocument = isMigratedDocument && doc.Released == true;
			bool isCuryInitDocBalEnabled = isUnreleasedMigratedDocument &&
				doc.DocType == APDocType.Prepayment;

			PXUIFieldAttribute.SetVisible<APPayment.curyUnappliedBal>(cache, doc, !isUnreleasedMigratedDocument);
			PXUIFieldAttribute.SetVisible<APPayment.curyInitDocBal>(cache, doc, isUnreleasedMigratedDocument);
			PXUIFieldAttribute.SetVisible<APPayment.displayCuryInitDocBal>(cache, doc, isReleasedMigratedDocument);
			PXUIFieldAttribute.SetEnabled<APPayment.curyInitDocBal>(cache, doc, isCuryInitDocBalEnabled);

			if (isMigratedDocument)
			{
				PXUIFieldAttribute.SetEnabled<APPayment.printCheck>(cache, doc, false);
			}

			if (isUnreleasedMigratedDocument)
			{
				Adjustments.Cache.AllowSelect =
				PaymentCharges.AllowSelect = false;
			}

			bool disableCaches = APSetup.Current?.MigrationMode == true
				? !isMigratedDocument
				: isUnreleasedMigratedDocument;
			if (disableCaches)
			{
				bool primaryCacheAllowInsert = Document.Cache.AllowInsert;
				bool primaryCacheAllowDelete = Document.Cache.AllowDelete;
				this.DisableCaches();
				Document.Cache.AllowInsert = primaryCacheAllowInsert;
				Document.Cache.AllowDelete = primaryCacheAllowDelete;
			}

			// We should notify the user that initial balance can be entered,
			// if there are now any errors on this box.
			// 
			if (isCuryInitDocBalEnabled)
			{
				if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<APPayment.curyInitDocBal>(cache, doc)))
				{
					cache.RaiseExceptionHandling<APPayment.curyInitDocBal>(doc, doc.CuryInitDocBal,
						new PXSetPropertyException(Messages.EnterInitialBalanceForUnreleasedMigratedDocument, PXErrorLevel.Warning));
				}
			}
			else
			{
				cache.RaiseExceptionHandling<APPayment.curyInitDocBal>(doc, doc.CuryInitDocBal, null);
			}
			if (isMigratedDocument)
			{
				cache.SetValue<APPayment.printCheck>(doc, false);
				PXUIFieldAttribute.SetEnabled<APPayment.printCheck>(cache, doc, false);
			}
			#endregion

			CheckForUnreleasedIncomingApplications(cache, doc);

			if (IsApprovalRequired(doc, cache))
			{
				if (doc.DocType == APDocType.Check || doc.DocType == APDocType.Prepayment)
				{
					//AP Check in Pending Approval, Rejected, Printed, Closed and Voided statuses should be disabled for editing.
					//AP Prepayments in Balanced, Pending Approval, Rejected, Closed, Voided  statuses should be disabled for editing.
					if (doc.Status == APDocStatus.PendingApproval
						|| doc.Status == APDocStatus.Rejected
						|| doc.Status == APDocStatus.Closed
						|| doc.Status == APDocStatus.Printed
						|| doc.Status == APDocStatus.Voided
						|| doc.Status == APDocStatus.PendingPrint && doc.DontApprove != true && doc.Approved == true
						|| doc.Status == APDocStatus.Balanced && doc.DontApprove != true && doc.Approved == true)
					{
						PXUIFieldAttribute.SetEnabled(cache, doc, false);

						Adjustments.Cache.AllowInsert = false;
						Adjustments_History.Cache.AllowInsert = false;
						Approval.Cache.AllowInsert = false;
						PaymentCharges.Cache.AllowInsert = false;

						Adjustments.Cache.AllowUpdate = false;
						Adjustments_History.Cache.AllowUpdate = false;
						Approval.Cache.AllowUpdate = false;
						PaymentCharges.Cache.AllowUpdate = false;
					}
					//In the documents with statuses Balanced, Rejected and Pending Approval, only possibility to change the Hold check box should be available.
					if (doc.Status == APDocStatus.PendingApproval
						|| doc.Status == APDocStatus.Rejected
						|| doc.Status == APDocStatus.Balanced
						|| doc.Status == APDocStatus.PendingPrint
						|| doc.Status == APDocStatus.Hold)
					{
						PXUIFieldAttribute.SetEnabled<APPayment.hold>(cache, doc, true);
					}

				}

				PXUIFieldAttribute.SetEnabled<APPayment.docType>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<APPayment.refNbr>(cache, doc, true);

			}

			if (doc.Status == APDocStatus.Printed && doc.DocType == APDocType.Check || doc.Status == APDocStatus.Balanced)
			{
				PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<APPayment.docDesc>(cache, doc, true);
			}

			if (doc.Status == APDocStatus.PendingPrint
					|| doc.Status == APDocStatus.Rejected
					|| doc.Status == APDocStatus.PendingApproval)
			{
				release.SetEnabled(false);
			}
		}
		
		protected virtual void CheckForUnreleasedIncomingApplications(PXCache sender, APPayment document)
		{
			if (document.Released != true || document.OpenDoc != true)
			{
				return;
			}

			APAdjust unreleasedIncomingApplication = PXSelect<
				APAdjust,
				Where<
					APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>,
					And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>,
					And<APAdjust.released, NotEqual<True>>>>>
				.Select(this, document.DocType, document.RefNbr);

			sender.ClearFieldErrors<APPayment.refNbr>(document);

			if (unreleasedIncomingApplication != null)
			{
				sender.DisplayFieldWarning<APPayment.refNbr>(
					document,
					null,
					PXLocalizer.LocalizeFormat(
						Common.Messages.CannotApplyDocumentUnreleasedIncomingApplicationsExist,
						GetLabel.For<APDocType>(unreleasedIncomingApplication.AdjgDocType),
						unreleasedIncomingApplication.AdjgRefNbr));

				Adjustments.Cache.AllowInsert =
				Adjustments.Cache.AllowUpdate =
				Adjustments.Cache.AllowDelete = false;
			}
		}

		public virtual bool IsDocReallyPrinted(APPayment doc)
		{
			return IsCheckReallyPrinted(doc);
		}

		public virtual void APPayment_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
		{
			APPayment row = (APPayment)e.Row;
			if (row != null && row.CuryApplAmt == null)
			{
				using (new PXConnectionScope())
				{
					RecalcApplAmounts(sender, row, true);
				}
			}
		}

		public virtual void RecalcApplAmounts(PXCache sender, APPayment row, bool aReadOnly)
		{
				PXFormulaAttribute.CalcAggregate<APAdjust.curyAdjgAmt>(Adjustments.Cache, row, aReadOnly);
			sender.RaiseFieldUpdated<APPayment.curyApplAmt>(row, null);
			PXDBCurrencyAttribute.CalcBaseValues<APPayment.curyApplAmt>(sender, row);
			PXDBCurrencyAttribute.CalcBaseValues<APPayment.curyUnappliedBal>(sender, row);
		}

		public static void SetDocTypeList(PXCache cache, string docType)
		{
			List<string> AllowedValues = new List<string>();
			List<string> AllowedLabels = new List<string>();

			if (docType == APDocType.Refund)
			{
				PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, APDocType.DebitAdj);
				PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, null, new string[] { APDocType.DebitAdj, APDocType.Prepayment }, new string[] { Messages.DebitAdj, Messages.Prepayment });
			}
			else if (docType == APDocType.Prepayment || docType == APDocType.DebitAdj)
			{
				PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, APDocType.Invoice);
				PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, null, new string[] { APDocType.Invoice, APDocType.CreditAdj }, new string[] { Messages.Invoice, Messages.CreditAdj });
			}
			else if (docType == APDocType.Check || docType == APDocType.VoidCheck)
			{
				PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, APDocType.Invoice);
				PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, null, new string[] { APDocType.Invoice, APDocType.DebitAdj, APDocType.CreditAdj, APDocType.Prepayment }, new string[] { Messages.Invoice, Messages.DebitAdj, Messages.CreditAdj, Messages.Prepayment });
			}
			else
			{
				PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, APDocType.Invoice);
				PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, null, new string[] { APDocType.Invoice, APDocType.CreditAdj, APDocType.Prepayment }, new string[] { Messages.Invoice, Messages.CreditAdj, Messages.Prepayment });
			}
		}

		private void SetDocTypeList(object Row)
		{
			APPayment row = Row as APPayment;
			if (row != null)
			{
				SetDocTypeList(Adjustments.Cache, row.DocType);
			}
		}
		protected virtual void APPayment_Cleared_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			APPayment payment = (APPayment)e.Row;
			if (payment.Cleared == true)
			{
				if (payment.ClearDate == null)
				{
					payment.ClearDate = payment.DocDate;
				}
			}
			else
			{
				payment.ClearDate = null;
			}
		}

		protected virtual void APPayment_DocDate_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (((APPayment)e.Row).Released == false && ((APPayment)e.Row).VoidAppl == false)
			{
				e.NewValue = ((APPayment)e.Row).AdjDate;
				e.Cancel = true;
			}
		}

		protected virtual void APPayment_FinPeriodID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if ((bool)((APPayment)e.Row).Released == false)
			{
				e.NewValue = ((APPayment)e.Row).AdjFinPeriodID;
				e.Cancel = true;
			}
		}

		protected virtual void APPayment_TranPeriodID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if ((bool)((APPayment)e.Row).Released == false)
			{
				e.NewValue = ((APPayment)e.Row).AdjTranPeriodID;
				e.Cancel = true;
			}
		}

		protected virtual void APPayment_DepositAfter_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			APPayment row = (APPayment)e.Row;
			if ((row.DocType == APDocType.Refund)
				&& row.DepositAsBatch == true)
			{
				e.NewValue = row.AdjDate;
				e.Cancel = true;
			}
		}

		protected virtual void APPayment_DepositAsBatch_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			APPayment row = (APPayment)e.Row;
			if ((row.DocType == APDocType.Refund))
			{
				sender.SetDefaultExt<APPayment.depositAfter>(e.Row);
			}
		}


		protected virtual void APPayment_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			APPayment payment = e.Row as APPayment;
			if (payment != null && payment.Released == false)
			{
				payment.DocDate = payment.AdjDate;
				payment.FinPeriodID = payment.AdjFinPeriodID;
				payment.TranPeriodID = payment.AdjTranPeriodID;

				sender.RaiseExceptionHandling<APPayment.finPeriodID>(e.Row, payment.FinPeriodID, null);
			}
		}
		protected virtual void APPayment_Hold_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			APPayment doc = e.Row as APPayment;
			if (PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
			{
				if (Approval.GetAssignedMaps(doc, sender).Any())
				{
					sender.SetValue<APPayment.hold>(doc, true);
				}
			}
		}
		protected virtual void APPayment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			APPayment row = (APPayment)e.Row;

			if (row.Released != true)
			{
				row.DocDate = row.AdjDate;

				row.FinPeriodID = row.AdjFinPeriodID;
				row.TranPeriodID = row.AdjTranPeriodID;

				sender.RaiseExceptionHandling<APPayment.finPeriodID>(row, row.FinPeriodID, null);
				if (row.PTInstanceID != null)
				{
					PTInstTran tran = this.ptInstanceTrans.Select();
					if (tran == null)
					{
						tran = new PTInstTran();
						tran = this.ptInstanceTrans.Insert(tran);
					}
				}
				else
				{
					foreach (PTInstTran it in this.ptInstanceTrans.Select())
					{
						this.ptInstanceTrans.Delete(it);
					}
				}
			}

			if (row.OpenDoc == true && row.Hold != true)
			{
                // It's should be allowed to enter a Check or 
                // VendorRefund document without any required 
                // applications, when migration mode is activated.
                //
				bool canHaveBalance = row.CanHaveBalance == true ||
                    row.IsMigratedRecord == true && row.CuryInitDocBal == 0m;

				if (canHaveBalance && row.VoidAppl != true && 
					(row.CuryUnappliedBal < 0m || row.CuryApplAmt < 0m && row.CuryUnappliedBal > row.CuryOrigDocAmt || row.CuryOrigDocAmt < 0m) ||
					!canHaveBalance && row.CuryUnappliedBal != 0m)
				{
                    sender.RaiseExceptionHandling<APPayment.curyOrigDocAmt>(row, row.CuryOrigDocAmt, new PXSetPropertyException(Messages.DocumentOutOfBalance, 
                        APSetup.Current.SuggestPaymentAmount.GetValueOrDefault(false) && row.CuryOrigDocAmt == 0m ? PXErrorLevel.Warning : PXErrorLevel.Error));
				}
				else
				{
					sender.RaiseExceptionHandling<APPayment.curyOrigDocAmt>(row, row.CuryOrigDocAmt, null);
				}
			}
			if (this.IsCopyPasteContext)
			{
				sender.SetValue<APPayment.printed>(row, false);
				sender.SetDefaultExt<APPayment.printCheck>(row);
			}
		}


		#region BusinessProcs

		public virtual void CreatePayment(APAdjust apdoc, CurrencyInfo info, bool setOffHold)
		{
			Segregate(apdoc, info, setOffHold ? true : (bool?)null);

			APAdjust adj = new APAdjust();
			adj.AdjdDocType = apdoc.AdjdDocType;
			adj.AdjdRefNbr = apdoc.AdjdRefNbr;

			//set origamt to zero to apply "full" amounts to invoices.
			this.Document.Cache.SetValueExt<APPayment.curyOrigDocAmt>(this.Document.Current, 0m);

			adj = PXCache<APAdjust>.CreateCopy(this.Adjustments.Insert(adj));

			if (TakeDiscAlways == true)
			{
				adj.CuryAdjgAmt = 0m;
				adj.CuryAdjgDiscAmt = 0m;
				adj.CuryAdjgWhTaxAmt = 0m;

				CalcBalances((object)adj, true);
				adj = PXCache<APAdjust>.CreateCopy((APAdjust)this.Adjustments.Cache.Update(adj));
			}

			if (apdoc.CuryAdjgAmt != null)
			{
				adj.CuryAdjgAmt = apdoc.CuryAdjgAmt;
				adj = PXCache<APAdjust>.CreateCopy((APAdjust)this.Adjustments.Cache.Update(adj));
			}

			if (apdoc.CuryAdjgDiscAmt != null)
			{
				adj.CuryAdjgDiscAmt = apdoc.CuryAdjgDiscAmt;
				adj = PXCache<APAdjust>.CreateCopy((APAdjust)this.Adjustments.Cache.Update(adj));
			}

			if (Document.Current.CuryApplAmt < 0m)
			{
				if (adj.CuryAdjgAmt <= -Document.Current.CuryApplAmt)
				{
					Adjustments.Delete(adj);
				}
				else
				{
					adj.CuryAdjgAmt += Document.Current.CuryApplAmt;
					Adjustments.Update(adj);
				}
			}

			decimal? CuryApplAmt = Document.Current.CuryApplAmt;

			APPayment copy = PXCache<APPayment>.CreateCopy(Document.Current);
			copy.CuryOrigDocAmt = CuryApplAmt;
			
			copy.DocDesc = GetPaymentDescription(
				copy.DocDesc, 
				vendor.Current.AcctCD, 
				PXSelect<APAdjust2, 
					Where<APAdjust2.adjgDocType, Equal<Current<APPayment.docType>>, 
						And<APAdjust2.adjgRefNbr, Equal<Current<APPayment.refNbr>>, 
						And<APAdjust2.adjNbr, Equal<Current<APPayment.lineCntr>>>>>>
				.SelectWindowed(this, 0, 2)
				.Count > 1);

			if (setOffHold && copy.Hold == true)
			{
				copy.Hold = false;
			}

			this.Document.Cache.Update(copy);
			this.Save.Press();

			apdoc.AdjgDocType = this.Document.Current.DocType;
			apdoc.AdjgRefNbr = this.Document.Current.RefNbr;
		}
		public virtual void CreatePayment(APAdjust apdoc, CurrencyInfo info)
		{
			CreatePayment(apdoc, info, false);
		}
		protected virtual string GetPaymentDescription(string descr, string vendor, bool multipleAdjust)
		{
			return multipleAdjust
				? String.Format(PXMessages.LocalizeNoPrefix(Messages.PaymentDescr), vendor)
				: descr;
		}

		public virtual void CreatePayment(APInvoice apdoc)
		{
			APPayment payment = Document.Current;

			if
			(payment == null ||
				!object.Equals(payment.VendorID, apdoc.VendorID) ||
				!object.Equals(payment.VendorLocationID, apdoc.PayLocationID) ||
				apdoc.SeparateCheck == true
			)
			{
				this.Clear();

				Location vend = PXSelect<Location, Where<Location.bAccountID, Equal<Required<Location.bAccountID>>,
					And<Location.locationID, Equal<Required<Location.locationID>>>>>.Select(this, apdoc.VendorID, apdoc.PayLocationID);
				if (vend == null)
				{
					throw new PXException(Messages.InternalError, 502);
				}

				if (apdoc.PayTypeID == null)
				{
					apdoc.PayTypeID = vend.PaymentMethodID;
				}

				int? payAccount = apdoc.PayAccountID ?? vend.CashAccountID;
				CashAccount ca = PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.SelectSingleBound(this, null, payAccount);
				if (ca == null)
				{
					throw new PXException(Messages.VendorMissingCashAccount);
				}

				if (ca.CuryID == apdoc.CuryID)
				{
					apdoc.PayAccountID = payAccount;
				}

				payment = new APPayment();
				switch (apdoc.DocType)
				{
					case APDocType.DebitAdj:
						payment.DocType = APDocType.Refund;
						break;
					default:
						payment.DocType = APDocType.Check;
						break;
				}

				payment = PXCache<APPayment>.CreateCopy(this.Document.Insert(payment));

				payment.VendorID = apdoc.VendorID;
				payment.VendorLocationID = apdoc.PayLocationID;
				payment.AdjDate = (DateTime.Compare((DateTime)Accessinfo.BusinessDate, (DateTime)apdoc.DocDate) < 0 ? apdoc.DocDate : Accessinfo.BusinessDate);
				payment.CashAccountID = apdoc.PayAccountID;
				payment.CuryID = apdoc.CuryID;
				payment.PaymentMethodID = apdoc.PayTypeID;
				payment.DocDesc = apdoc.DocDesc;

				this.FieldDefaulting.AddHandler<APPayment.cashAccountID>((sender, e) =>
				{
					if (apdoc.DocType == APDocType.Prepayment)
					{
						e.NewValue = null;
						e.Cancel = true;
					}
				});
				this.FieldDefaulting.AddHandler<CurrencyInfo.curyID>((sender, e) =>
				{
					if (e.Row != null)
					{
						e.NewValue = ((CurrencyInfo)e.Row).CuryID;
						e.Cancel = true;
					}
				});

				payment = Document.Update(payment);
			}

			APAdjust adj = new APAdjust();
			adj.AdjdDocType = apdoc.DocType;
			adj.AdjdRefNbr = apdoc.RefNbr;

			//set origamt to zero to apply "full" amounts to invoices.
			Document.SetValueExt<APPayment.curyOrigDocAmt>(payment, 0m);

			try
			{
				Adjustments.Insert(adj);
			}
			catch (PXSetPropertyException)
			{
				throw new AdjustedNotFoundException();
			}

			decimal? CuryApplAmt = payment.CuryApplAmt;

			this.Document.SetValueExt<APPayment.curyOrigDocAmt>(payment, CuryApplAmt);
			this.Document.Current = this.Document.Update(payment);
		}

		private bool _IsVoidCheckInProgress = false;

		protected virtual void APPayment_RefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (_IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}
		}

		protected virtual void APPayment_FinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (_IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}
		}

		protected virtual void APPayment_AdjFinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (_IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}

			APPayment doc = (APPayment)e.Row;

			string errMsg;
			if (!VerifyAdjFinPeriodID(doc, (string)e.NewValue, out errMsg))
			{
				e.NewValue = FinPeriodIDAttribute.FormatForDisplay((string)e.NewValue);
				throw new PXSetPropertyException(errMsg);
			}
		}

		protected virtual bool VerifyAdjFinPeriodID(APPayment doc, string newValue, out string errMsg)
		{
			errMsg = null;

			if (doc.Released == true && doc.FinPeriodID.CompareTo(newValue) > 0)
			{
				errMsg = string.Format(CS.Messages.Entry_GE, FinPeriodIDAttribute.FormatForError(doc.FinPeriodID));
				return false;
			}

			if (doc.DocType == APDocType.VoidCheck)
			{
				APPayment orig_payment = PXSelect<APPayment,
					Where2<Where<APPayment.docType, Equal<APDocType.check>,
							Or<APPayment.docType, Equal<APDocType.prepayment>>>,
						And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>
					.SelectSingleBound(this, null, doc.RefNbr);

				if (orig_payment != null && orig_payment.FinPeriodID.CompareTo(newValue) > 0)
				{
					errMsg = string.Format(CS.Messages.Entry_GE, FinPeriodIDAttribute.FormatForError(orig_payment.FinPeriodID));
					return false;
				}
			}
			else
			{
					// We should find maximal adjusting period of adjusted applications
					/// (excluding applications, that have been reversed in the same period)
					/// for the document, because applications in earlier period are not allowed.
					///
					APAdjust adjdmax = PXSelectJoin<APAdjust,
						LeftJoin<APAdjust2, On<
							APAdjust2.adjdDocType, Equal<APAdjust.adjdDocType>,
							And<APAdjust2.adjdRefNbr, Equal<APAdjust.adjdRefNbr>,
							And<APAdjust2.adjgDocType, Equal<APAdjust.adjgDocType>,
							And<APAdjust2.adjgRefNbr, Equal<APAdjust.adjgRefNbr>,
							And<APAdjust2.adjNbr, NotEqual<APAdjust.adjNbr>,
							And<Switch<Case<Where<APAdjust.voidAdjNbr, IsNotNull>, APAdjust.voidAdjNbr>, APAdjust.adjNbr>,
								Equal<Switch<Case<Where<APAdjust.voidAdjNbr, IsNotNull>, APAdjust2.adjNbr>, APAdjust2.voidAdjNbr>>,
							And<APAdjust2.adjgFinPeriodID, Equal<APAdjust.adjgFinPeriodID>,
							And<APAdjust2.released, Equal<True>,
							And<APAdjust2.voided, Equal<True>,
							And<APAdjust.voided, Equal<True>>>>>>>>>>>>,
						Where<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>,
							And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>,
							And<APAdjust.released, Equal<True>,
							And<APAdjust2.adjdRefNbr, IsNull>>>>,
						OrderBy<Desc<APAdjust.adjgFinPeriodID>>>
						.SelectSingleBound(this, null, doc.DocType, doc.RefNbr);

					if (adjdmax?.AdjgFinPeriodID.CompareTo(newValue) > 0)
				{
						errMsg = string.Format(CS.Messages.Entry_GE, FinPeriodIDAttribute.FormatForError(adjdmax.AdjgFinPeriodID));
						return false;
				}
			}

			return true;
		}

		public virtual void VoidCheckProc(APPayment doc)
		{
			this.Clear(PXClearOption.PreserveTimeStamp);
			this.Document.View.Answer = WebDialogResult.No;

			foreach (PXResult<APPayment, CurrencyInfo, Currency, Vendor> res in APPayment_CurrencyInfo_Currency_Vendor.Select(this, doc.DocType, doc.RefNbr, doc.VendorID))
			{
				doc = res;

				CurrencyInfo info = PXCache<CurrencyInfo>.CreateCopy(res);
				info.CuryInfoID = null;
				info.IsReadOnly = false;
				info = PXCache<CurrencyInfo>.CreateCopy(currencyinfo.Insert(info));

				APPayment payment = new APPayment
				{
					DocType = APDocType.VoidCheck,
					RefNbr = doc.RefNbr,
					CuryInfoID = info.CuryInfoID,
					VoidAppl = true
				};

				Document.Insert(payment);

				payment = PXCache<APPayment>.CreateCopy(res);

				payment.CuryInfoID = info.CuryInfoID;
				payment.VoidAppl = true;
				//must set for _RowSelected
				payment.CATranID = null;
				payment.Printed = true;
				payment.OpenDoc = true;
				payment.Released = false;

				Document.Cache.SetDefaultExt<APPayment.hold>(payment);
				Document.Cache.SetDefaultExt<APPayment.isMigratedRecord>(payment);
				payment.LineCntr = 0;
				payment.BatchNbr = null;
				payment.CuryOrigDocAmt = -1 * payment.CuryOrigDocAmt;
				payment.OrigDocAmt = -1 * payment.OrigDocAmt;
				payment.CuryInitDocBal = -1 * payment.CuryInitDocBal;
				payment.InitDocBal = -1 * payment.InitDocBal;
				payment.CuryChargeAmt = 0;
				payment.CuryApplAmt = null;
				payment.CuryUnappliedBal = null;
				payment.AdjDate = doc.DocDate;
				payment.AdjFinPeriodID = doc.FinPeriodID;
				payment.AdjTranPeriodID = doc.TranPeriodID;
				Document.Cache.SetDefaultExt<APPayment.cleared>(payment);
				Document.Cache.SetDefaultExt<APPayment.employeeID>(payment);
				Document.Cache.SetDefaultExt<APPayment.employeeWorkgroupID>(payment);
				payment.ClearDate = payment.DocDate;

				payment = Document.Update(payment);
				Document.Cache.SetDefaultExt<APPayment.printCheck>(payment);

				Document.Cache.SetValueExt<APPayment.adjFinPeriodID>(payment, FinPeriodIDAttribute.FormatForDisplay(doc.FinPeriodID));

				if (info != null)
				{
					CurrencyInfo b_info = PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<APPayment.curyInfoID>>>>.Select(this, null);
					b_info.CuryID = info.CuryID;
					b_info.CuryEffDate = info.CuryEffDate;
					b_info.CuryRateTypeID = info.CuryRateTypeID;
					b_info.CuryRate = info.CuryRate;
					b_info.RecipRate = info.RecipRate;
					b_info.CuryMultDiv = info.CuryMultDiv;
					currencyinfo.Update(b_info);
				}
			}

			foreach (PXResult<APAdjust, CurrencyInfo> adjres in PXSelectJoin<APAdjust, 
				InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<APAdjust.adjdCuryInfoID>>>, 
				Where<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, 
					And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, 
					And<APAdjust.voided, Equal<False>,
					And<APAdjust.isInitialApplication, NotEqual<True>>>>>>.Select(this, doc.DocType, doc.RefNbr))
			{
				APAdjust adj = PXCache<APAdjust>.CreateCopy((APAdjust)adjres);

				if ((doc.DocType != APDocType.DebitAdj || doc.PendingPPD != true) &&
				adj.AdjdHasPPDTaxes == true &&
				adj.PendingPPD != true)
				{
					APAdjust adjPPD = GetPPDApplication(this, adj.AdjdDocType, adj.AdjdRefNbr);
					if (adjPPD != null && (adjPPD.AdjgDocType != adj.AdjgDocType || adjPPD.AdjgRefNbr != adj.AdjgRefNbr))
					{
						adj = adjres;
						this.Clear();
						adj = (APAdjust)Adjustments.Cache.Update(adj);
						Document.Current = Document.Search<APPayment.refNbr>(adj.AdjgRefNbr, adj.AdjgDocType);
						Adjustments.Cache.RaiseExceptionHandling<APAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr,
							new PXSetPropertyException(Messages.PPDApplicationExists, PXErrorLevel.RowError, adjPPD.AdjgRefNbr));

						throw new PXSetPropertyException(Messages.PPDApplicationExists, adjPPD.AdjgRefNbr);
					}
				}

				adj.VoidAppl = true;
				adj.Released = false;
				Adjustments.Cache.SetDefaultExt<APAdjust.isMigratedRecord>(adj);
				adj.VoidAdjNbr = adj.AdjNbr;
				adj.AdjNbr = 0;
				adj.AdjBatchNbr = null;

				APAdjust adjnew = new APAdjust
				{
					AdjgDocType = adj.AdjgDocType,
					AdjgRefNbr = adj.AdjgRefNbr,
					AdjgBranchID = adj.AdjgBranchID,
					AdjdDocType = adj.AdjdDocType,
					AdjdRefNbr = adj.AdjdRefNbr,
					AdjdBranchID = adj.AdjdBranchID,
					VendorID = adj.VendorID,
					AdjdCuryInfoID = adj.AdjdCuryInfoID,
					AdjdHasPPDTaxes = adj.AdjdHasPPDTaxes,
				};

				if (this.Adjustments.Insert(adjnew) == null)
				{
					adj = (APAdjust)adjres;
					this.Clear();
					adj = (APAdjust)Adjustments.Cache.Update(adj);
					Document.Current = Document.Search<APPayment.refNbr>(adj.AdjgRefNbr, adj.AdjgDocType);
					Adjustments.Cache.RaiseExceptionHandling<APAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr, 
						new PXSetPropertyException(Messages.MultipleApplicationError, PXErrorLevel.RowError));

					throw new PXException(Messages.MultipleApplicationError);
				}

				adj.CuryAdjgAmt = -1 * adj.CuryAdjgAmt;
				adj.CuryAdjgDiscAmt = -1 * adj.CuryAdjgDiscAmt;
				adj.CuryAdjgWhTaxAmt = -1 * adj.CuryAdjgWhTaxAmt;
				adj.AdjAmt = -1 * adj.AdjAmt;
				adj.AdjDiscAmt = -1 * adj.AdjDiscAmt;
				adj.AdjWhTaxAmt = -1 * adj.AdjWhTaxAmt;
				adj.CuryAdjdAmt = -1 * adj.CuryAdjdAmt;
				adj.CuryAdjdDiscAmt = -1 * adj.CuryAdjdDiscAmt;
				adj.CuryAdjdWhTaxAmt = -1 * adj.CuryAdjdWhTaxAmt;
				adj.RGOLAmt = -1 * adj.RGOLAmt;
				adj.AdjgCuryInfoID = Document.Current.CuryInfoID;
				adj.CuryAdjgPPDAmt = -1 * adj.CuryAdjgPPDAmt;
				adj.AdjPPDAmt = -1 * adj.AdjPPDAmt;
				adj.CuryAdjdPPDAmt = -1 * adj.CuryAdjdPPDAmt;

				Adjustments.Update(adj);
			}

			PaymentCharges.ReverseCharges(doc, Document.Current);
		}
		#endregion
	}
}
