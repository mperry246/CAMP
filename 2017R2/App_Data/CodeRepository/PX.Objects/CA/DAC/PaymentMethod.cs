using System;
using PX.Api;
using PX.Data;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using PX.Objects.Common.Attributes;

namespace PX.Objects.CA
{
	public static class PaymentMethodType
	{
		public const string CreditCard = "CCD";
		public const string CashOrCheck = "CHC";
		public const string DirectDeposit = "DDT";

		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(new string[] { CreditCard, CashOrCheck, DirectDeposit },
										  new string[] { "Credit Card", "Cash/Check", "Direct Deposit" })
			{ }
		}

		public class creditCard : Constant<string>
		{
			public creditCard() : base(CreditCard) { }
		}

		public class cashOrCheck : Constant<string>
		{
			public cashOrCheck() : base(CashOrCheck) { }
		}

		public class directDeposit : Constant<string>
		{
			public directDeposit() : base(DirectDeposit) { }
		}
	}

	[PXCacheName(Messages.PaymentMethod)]
	[Serializable]
	[PXPrimaryGraph(typeof(PaymentMethodMaint))]
	public partial class PaymentMethod : IBqlTable
	{
		#region PaymentMethodID
		public abstract class paymentMethodID : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = " Payment Method ID", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<PaymentMethod.paymentMethodID>))]
		[PXReferentialIntegrityCheck]
		public virtual string PaymentMethodID
		{
			get;
			set;
		}
		#endregion
		#region PMInstanceID
		public abstract class pMInstanceID : IBqlField
		{
		}
		[PXDBForeignIdentity(typeof(PMInstance))]
		public virtual int? PMInstanceID
		{
			get;
			set;
		}
		#endregion
		#region Descr
		public abstract class descr : IBqlField
		{
		}
		[PXDBLocalizableString(60, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual string Descr
		{
			get;
			set;
		}
		#endregion
		#region PaymentType
		public abstract class paymentType : IBqlField
		{
		}
		[PXDBString(3, IsFixed = true)]
		[PXDefault(PaymentMethodType.CashOrCheck, PersistingCheck = PXPersistingCheck.NullOrBlank)]
		[PaymentMethodType.List]
		[PXUIField(DisplayName = "Means of Payment", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string PaymentType
		{
			get;
			set;
		}
		#endregion
		#region DefaultCashAccountID
		public abstract class defaultCashAccountID : IBqlField
		{
		}
        [PXInt]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? DefaultCashAccountID
		{
			get;
			set;
		}
		#endregion
		#region IsActive
		public abstract class isActive : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Active")]
		public virtual bool? IsActive
		{
			get;
			set;
		}
		#endregion
		#region UseForAR
		public abstract class useForAR : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Use in AR", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual bool? UseForAR
		{
			get;
			set;
		}
		#endregion
		#region UseForAP
		public abstract class useForAP : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Use in AP", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual bool? UseForAP
		{
			get;
			set;
		}
		#endregion
		#region UseForCA
		public abstract class useForCA : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Require Remittance Information for Cash Account")]
		public virtual bool? UseForCA
		{
			get;
			set;
		}
		#endregion
		#region APAdditionalProcessing

		public abstract class aPAdditionalProcessing : IBqlField
		{
			public const string PrintChecks = "P";
			public const string CreateBatchPayment = "B";
			public const string NotRequired = "N";

			public class printChecks : Constant<string>
			{
				public printChecks() : base(PrintChecks) { }
			}

			public class createBatchPayment : Constant<string>
			{
				public createBatchPayment() : base(CreateBatchPayment) { }
			}

			public class notRequired : Constant<string>
			{
				public notRequired() : base(NotRequired) { }
			}

			public class ListAttribute : PXStringListAttribute
			{
				public ListAttribute()
					: base(new string[] { PrintChecks, CreateBatchPayment, NotRequired },
							new string[] { "Print Checks", "Create Batch Payments", "Not Required" })
				{
				}
			}
		}


		[PXString]
		[aPAdditionalProcessing.List]
		[PXDefault(aPAdditionalProcessing.NotRequired)]
		[PXDBCalced(typeof(Switch<Case<Where<PaymentMethod.aPPrintChecks, Equal<True>>, aPAdditionalProcessing.printChecks,
								  Case<Where<PaymentMethod.aPCreateBatchPayment, Equal<True>>, aPAdditionalProcessing.createBatchPayment>>,
								  aPAdditionalProcessing.notRequired>), typeof(string))]
		public virtual string APAdditionalProcessing
		{
			get;
			set;
		}
		#endregion
		#region APCreateBatchPayment
		public abstract class aPCreateBatchPayment : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Create Batch Payment")]
		public virtual bool? APCreateBatchPayment
		{
			get;
			set;
		}
		#endregion
		#region APBatchExportSYMappingID
		public abstract class aPBatchExportSYMappingID : IBqlField { }
		[PXDBGuid]
		[PXUIField(DisplayName = "Export Scenario", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(Search<SYMapping.mappingID, Where<SYMapping.mappingType, Equal<SYMapping.mappingType.typeExport>>>), SubstituteKey = typeof(SYMapping.name))]
		[PXDefault]
		[PXUIRequired(typeof(Where<PaymentMethod.useForAP, Equal<True>,
			And<PaymentMethod.aPCreateBatchPayment, Equal<True>>>))]
		public virtual Guid? APBatchExportSYMappingID
		{
			get;
			set;
		}
		#endregion
		#region APPrintChecks
		public abstract class aPPrintChecks : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Print Checks")]
		public virtual bool? APPrintChecks
		{
			get;
			set;
		}
		#endregion
		#region APCheckReportID
		public abstract class aPCheckReportID : IBqlField { }
		[PXDBString(8, InputMask = "CC.CC.CC.CC")]
		[PXUIField(DisplayName = "Report")]
		[PXSelector(typeof(Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.ap_>, And<SiteMap.url, Like<urlReports>>>>), typeof(SiteMap.screenID), typeof(SiteMap.title), Headers = new string[] { Messages.ReportID, Messages.ReportName }, DescriptionField = typeof(SiteMap.title))]
		[PXDefault]
		[PXUIRequired(typeof(Where<PaymentMethod.useForAP, Equal<True>,
			And<PaymentMethod.aPPrintChecks, Equal<True>>>))]
		public virtual string APCheckReportID
		{
			get;
			set;
		}
		#endregion
		#region APStubLines
		public abstract class aPStubLines : IBqlField { }
		[PXDBShort]
		[PXDefault((short)10)]
		[PXUIField(DisplayName = "Lines per Stub")]
		public virtual short? APStubLines
		{
			get;
			set;
		}
		#endregion
		#region APPrintRemittance
		public abstract class aPPrintRemittance : IBqlField
		{
		}
		[PXDBBool]
		[PXUIField(DisplayName = "Print Remittance Report", Visibility = PXUIVisibility.Visible)]
		[PXDefault(false)]
		public virtual bool? APPrintRemittance
		{
			get;
			set;
		}
		#endregion
		#region APRemittanceReportReportID
		public abstract class aPRemittanceReportID : IBqlField { }
		[PXDBString(8, InputMask = "CC.CC.CC.CC")]
		[PXUIField(DisplayName = "Remittance Report")]
		[PXSelector(typeof(Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.ap_>, And<SiteMap.url, Like<urlReports>>>>), typeof(SiteMap.screenID), typeof(SiteMap.title), Headers = new string[] { Messages.ReportID, Messages.ReportName }, DescriptionField = typeof(SiteMap.title))]
		[PXDefault]
		[PXUIRequired(typeof(Where<PaymentMethod.useForAP, Equal<True>,
			And<PaymentMethod.aPPrintChecks, Equal<True>,
			And<PaymentMethod.aPPrintRemittance, Equal<True>>>>))]
		public virtual string APRemittanceReportID
		{
			get;
			set;
		}
		#endregion
		#region APRequirePaymentRef
		public abstract class aPRequirePaymentRef : IBqlField { }


		[PXDBBool]
		[PXUIField(DisplayName = "Require Unique Payment Ref.")]
		[PXDefault(true)]
		public virtual bool? APRequirePaymentRef
		{
			get;
			set;
		}
		#endregion

		#region ARIsProcessingRequired
		public abstract class aRIsProcessingRequired : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Integrated Processing")]
		public virtual bool? ARIsProcessingRequired
		{
			get;
			set;
		}
		#endregion
		#region ARIsOnePerCustomer
		public abstract class aRIsOnePerCustomer : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "One Instance Per Customer")]
		public virtual bool? ARIsOnePerCustomer
		{
			get;
			set;
		}
		#endregion
		#region ARDepositAsBatch
		public abstract class aRDepositAsBatch : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Batch Deposit")]
		public virtual bool? ARDepositAsBatch
		{
			get;
			set;
		}
		#endregion
		#region ARVoidOnDepositAccount
		public abstract class aRVoidOnDepositAccount : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Void On Clearing Account")]
		public virtual bool? ARVoidOnDepositAccount
		{
			get;
			set;
		}
		#endregion
		#region ARDefaultVoidDateToDocumentDate
		public abstract class aRDefaultVoidDateToDocumentDate : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Default Void Date to Document Date")]
		public virtual bool? ARDefaultVoidDateToDocumentDate
		{
			get;
			set;
		}
		#endregion
		#region ARHasBillingInfo
		public abstract class aRHasBillingInfo : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Has Billing Information")]
		public virtual bool? ARHasBillingInfo
		{
			get;
			set;
		}
		#endregion

		#region NoteID
		public abstract class noteID : IBqlField
		{
		}
		[PXNote(DescriptionField = typeof(PaymentMethod.paymentMethodID))]
		public virtual Guid? NoteID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : IBqlField
		{
		}
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField
		{
		}
		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField
		{
		}
		[PXDBCreatedDateTime]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? CreatedDateTime
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : IBqlField
		{
		}
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField
		{
		}
		[PXDBLastModifiedByScreenID]
		public virtual string LastModifiedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField
		{
		}
		[PXDBLastModifiedDateTime]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : IBqlField
		{
		}
		[PXDBTimestamp]
		public virtual byte[] tstamp
		{
			get;
			set;
		}
		#endregion
		#region IsAccountNumberRequired
		public abstract class isAccountNumberRequired : IBqlField
		{
		}

		[PXBool]
		[PXUIField(DisplayName = "Require Card/Account Number")]
		public virtual bool? IsAccountNumberRequired
		{
			[PXDependsOnFields(typeof(aRIsOnePerCustomer))]
			get
			{
				return this.ARIsOnePerCustomer.HasValue ? (!this.ARIsOnePerCustomer.Value) : this.ARIsOnePerCustomer;
			}

			set
			{
				this.ARIsOnePerCustomer = (value.HasValue ? !(value.Value) : value);
			}
		}
		#endregion
		#region PrintOrExport
		public abstract class printOrExport : IBqlField { }
		[PXBool]
		[PXUIField(DisplayName = "Print Checks/Export", Visibility = PXUIVisibility.Visible)]
		[PXFormula(typeof(IIf<Where<PaymentMethod.aPPrintChecks, Equal<True>, Or<PaymentMethod.aPCreateBatchPayment, Equal<True>>>, True, False>))]
		public virtual bool? PrintOrExport
		{
			get;
			set;
		}
		#endregion
		#region APAllowInstances
		public abstract class aPAllowInstances : IBqlField
		{
		}
		[Obsolete("Will be removed in Acumatica 2018 R1")]
		[PXBool]
		[PXUIField(DisplayName = "Use for Corporate Credit Cards")]
		public virtual bool? APAllowInstances
		{
			get;
			set;
		}
		#endregion
	}

	[Serializable]
	[PXHidden]
	[PXCacheName(Messages.PMInstance)]
	public partial class PMInstance : PX.Data.IBqlTable
	{
		#region PMInstanceID
		public abstract class pMInstanceID : IBqlField
		{
		}
		[PXDBIdentity]
		public virtual int PMInstanceID
		{
			get;
			set;
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : IBqlField
		{
		}
		[PXDBTimestamp]
		public virtual byte[] tstamp
		{
			get;
			set;
		}
		#endregion
	}

	[PXHidden]
	public partial class PaymentMethodActive : PaymentMethod
	{
		#region PaymentMethodID
		new public abstract class paymentMethodID : IBqlField
		{
		}
		#endregion
		#region IsActive
		new public abstract class isActive : IBqlField
		{
		}
		#endregion
	}
}
