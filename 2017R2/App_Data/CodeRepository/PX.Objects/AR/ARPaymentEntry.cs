using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using PX.Common;

using PX.Data;
using PX.Data.Api.Export;

using PX.Objects.AR.Standalone;
using PX.Objects.Common;
using PX.Objects.AR.MigrationMode;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;

using SOAdjust = PX.Objects.SO.SOAdjust;
using SOOrder = PX.Objects.SO.SOOrder;
using SOOrderEntry = PX.Objects.SO.SOOrderEntry;
using SOOrderType = PX.Objects.SO.SOOrderType;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR;
using PX.Objects.EP;
using PX.Objects.Common.Extensions;

namespace PX.Objects.AR
{
	[Serializable]
	[NonOptimizable(new Type[] { typeof(ARPayment.adjDate), typeof(ARPayment.curyApplAmt) })]
	public class ARPaymentEntry : ARDataEntryGraph<ARPaymentEntry, ARPayment>
	{
		#region Internal Type Definition

		[Serializable]
		public partial class CCPaymentInfo : IBqlTable
		{
			#region PCTranNumber
			public abstract class pCTranNumber : PX.Data.IBqlField
			{
			}
			protected String _PCTranNumber;
			[PXDBString(50, IsUnicode = true)]
			[PXDefault()]
			[PXUIField(DisplayName = "Proc. Center Tran. Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
			public virtual String PCTranNumber
			{
				get
				{
					return this._PCTranNumber;
				}
				set
				{
					this._PCTranNumber = value;
				}
			}
			#endregion
			#region AuthNumber
			public abstract class authNumber : PX.Data.IBqlField
			{
			}
			protected String _AuthNumber;
			[PXDBString(50, IsUnicode = true)]
			[PXDefault()]
			[PXUIField(DisplayName = "Proc. Center Auth. Nbr.")]
			public virtual String AuthNumber
			{
				get
				{
					return this._AuthNumber;
				}
				set
				{
					this._AuthNumber = value;
				}
			}
			#endregion
		}

		[Serializable]
		public partial class LoadOptions : IBqlTable
		{
			#region FromDate
			public abstract class fromDate : IBqlField
			{
			}
			protected DateTime? _FromDate;
			[PXDBDate]
			[PXUIField(DisplayName = "From Date")]
			public virtual DateTime? FromDate
			{
				get
				{
					return _FromDate;
				}
				set
				{
					_FromDate = value;
				}
			}
			#endregion
			#region TillDate
			public abstract class tillDate : IBqlField
			{
			}
			protected DateTime? _TillDate;
			[PXDBDate]
			[PXUIField(DisplayName = "To Date")]
			[PXDefault(typeof(ARPayment.adjDate), PersistingCheck = PXPersistingCheck.Nothing)]
			public virtual DateTime? TillDate
			{
				get
				{
					return _TillDate;
				}
				set
				{
					_TillDate = value;
				}
			}
			#endregion
			#region StartRefNbr
			public abstract class startRefNbr : IBqlField
			{
			}
			protected string _StartRefNbr;
			[PXDBString(15, IsUnicode = true)]
			[PXUIField(DisplayName = "Start Ref. Nbr.")]
			[PXSelector(typeof(Search2<ARInvoice.refNbr,
				InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>,
				LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>,
					And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>,
					And<ARAdjust.released, NotEqual<True>,
					And<ARAdjust.voided, NotEqual<True>,
					And<Where<ARAdjust.adjgDocType, NotEqual<Current<ARRegister.docType>>,
						Or<ARAdjust.adjgRefNbr, NotEqual<Current<ARRegister.refNbr>>>>>>>>>,
				LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARInvoice.docType>,
					And<ARAdjust2.adjgRefNbr, Equal<ARInvoice.refNbr>,
					And<ARAdjust2.released, NotEqual<True>,
					And<ARAdjust2.voided, NotEqual<True>>>>>>>>,
				Where<ARInvoice.released, Equal<True>,
					And<ARInvoice.openDoc, Equal<True>,
					And<ARAdjust.adjgRefNbr, IsNull,
					And<ARAdjust2.adjdRefNbr, IsNull,
					And<ARInvoice.docDate, LessEqual<Current<ARPayment.adjDate>>,
					And<ARInvoice.finPeriodID, LessEqual<Current<ARPayment.adjFinPeriodID>>,
					And<ARInvoice.pendingPPD, NotEqual<True>,
					And<Where<ARInvoice.customerID, Equal<Optional<ARPayment.customerID>>,
						Or<Customer.consolidatingBAccountID, Equal<Optional<ARRegister.customerID>>>>>>>>>>>>>))]
			public virtual string StartRefNbr
			{
				get
				{
					return _StartRefNbr;
				}
				set
				{
					_StartRefNbr = value;
				}
			}
			#endregion
			#region EndRefNbr
			public abstract class endRefNbr : IBqlField
			{
			}
			protected string _EndRefNbr;
			[PXDBString(15, IsUnicode = true)]
			[PXUIField(DisplayName = "End Ref. Nbr.")]
			[PXSelector(typeof(Search2<ARInvoice.refNbr,
				InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>,
				LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>,
					And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>,
					And<ARAdjust.released, NotEqual<True>,
					And<ARAdjust.voided, NotEqual<True>,
					And<Where<ARAdjust.adjgDocType, NotEqual<Current<ARRegister.docType>>,
						Or<ARAdjust.adjgRefNbr, NotEqual<Current<ARRegister.refNbr>>>>>>>>>,
				LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARInvoice.docType>,
					And<ARAdjust2.adjgRefNbr, Equal<ARInvoice.refNbr>,
					And<ARAdjust2.released, NotEqual<True>,
					And<ARAdjust2.voided, NotEqual<True>>>>>>>>,
				Where<ARInvoice.released, Equal<True>,
					And<ARInvoice.openDoc, Equal<True>,
					And<ARAdjust.adjgRefNbr, IsNull,
					And<ARAdjust2.adjdRefNbr, IsNull,
					And<ARInvoice.docDate, LessEqual<Current<ARPayment.adjDate>>,
					And<ARInvoice.finPeriodID, LessEqual<Current<ARPayment.adjFinPeriodID>>,
					And<ARInvoice.pendingPPD, NotEqual<True>,
					And<Where<ARInvoice.customerID, Equal<Optional<ARPayment.customerID>>,
						Or<Customer.consolidatingBAccountID, Equal<Optional<ARRegister.customerID>>>>>>>>>>>>>))]
			public virtual string EndRefNbr
			{
				get
				{
					return _EndRefNbr;
				}
				set
				{
					_EndRefNbr = value;
				}
			}
			#endregion
			#region StartOrderNbr
			public abstract class startOrderNbr : IBqlField
			{
			}
			protected String _StartOrderNbr;
			[PXDBString(15, IsUnicode = true)]
			[PXUIField(DisplayName = "Start Order Nbr.")]
			[PXSelector(typeof(Search2<SOOrder.orderNbr, LeftJoin<SOOrderType, On<SOOrderType.orderType, Equal<SOOrder.orderType>>>,
					 Where<SOOrder.customerID, Equal<Optional<ARPayment.customerID>>,
					   And<SOOrder.openDoc, Equal<True>,
					   And<SOOrder.orderDate, LessEqual<Current<ARPayment.adjDate>>,
					   And<Where<SOOrderType.aRDocType, Equal<ARDocType.invoice>, Or<SOOrderType.aRDocType, Equal<ARDocType.debitMemo>>>>>>>>))]
			public virtual String StartOrderNbr
			{
				get
				{
					return _StartOrderNbr;
				}
				set
				{
					_StartOrderNbr = value;
				}
			}
			#endregion
			#region EndOrderNbr
			public abstract class endOrderNbr : IBqlField
			{
			}
			protected String _EndOrderNbr;
			[PXDBString(15, IsUnicode = true)]
			[PXUIField(DisplayName = "End Order Nbr.")]
			[PXSelector(typeof(Search2<SOOrder.orderNbr, LeftJoin<SOOrderType, On<SOOrderType.orderType, Equal<SOOrder.orderType>>>,
					 Where<SOOrder.customerID, Equal<Optional<ARPayment.customerID>>,
					   And<SOOrder.openDoc, Equal<True>,
					   And<SOOrder.orderDate, LessEqual<Current<ARPayment.adjDate>>,
					   And<Where<SOOrderType.aRDocType, Equal<ARDocType.invoice>, Or<SOOrderType.aRDocType, Equal<ARDocType.debitMemo>>>>>>>>))]
			public virtual String EndOrderNbr
			{
				get
				{
					return _EndOrderNbr;
				}
				set
				{
					_EndOrderNbr = value;
				}
			}
			#endregion
			#region MaxDocs
			public abstract class maxDocs : IBqlField
			{
			}
			protected int? _MaxDocs;
			[PXDBInt]
			[PXUIField(DisplayName = "Max. Number of Documents")]
			[PXDefault(999, PersistingCheck = PXPersistingCheck.Nothing)]
			public virtual int? MaxDocs
			{
				get
				{
					return _MaxDocs;
				}
				set
				{
					_MaxDocs = value;
				}
			}

			#endregion
			#region LoadChildDocuments
			public abstract class loadChildDocuments : IBqlField
			{
				public const string None = "NOONE";
				public const string ExcludeCRM = "EXCRM";
				public const string IncludeCRM = "INCRM";

				public class none : Constant<string> { public none() : base(None) { } }
				public class excludeCRM : Constant<string> { public excludeCRM() : base(ExcludeCRM) { } }
				public class includeCRM : Constant<string> { public includeCRM() : base(IncludeCRM) { } }

				public class ListAttribute : PXStringListAttribute
				{
					public ListAttribute() : base(
						new[] { None, ExcludeCRM, IncludeCRM },
						new[] { Messages.None, Messages.ExcludeCRM, Messages.IncludeCRM })
					{ }
				}

				public class NoCRMListAttribute : PXStringListAttribute
				{
					public NoCRMListAttribute()
						: base(
							new[] { None, ExcludeCRM },
							new[] { Messages.None, Messages.ExcludeCRM })
					{ }
				}
			}

			[PXDBString(5, IsFixed = true)]
			[PXUIField(DisplayName = "Include Child Documents")]
			[loadChildDocuments.List]
			[PXDefault(loadChildDocuments.None)]
			public virtual string LoadChildDocuments { get; set; }
			#endregion
			#region OrderBy
			public abstract class orderBy : IBqlField
			{
				#region List
				public class ListAttribute : PXStringListAttribute
				{
					public ListAttribute()
						: base(
						new[] { DueDateRefNbr, DocDateRefNbr, RefNbr },
						new[] { Messages.DueDateRefNbr, Messages.DocDateRefNbr, Messages.RefNbr })
					{ }
				}

				public const string DueDateRefNbr = "DUE";
				public const string DocDateRefNbr = "DOC";
				public const string RefNbr = "REF";
				#endregion
			}
			protected string _OrderBy;
			[PXDBString(3, IsFixed = true)]
			[PXUIField(DisplayName = "Order by")]
			[orderBy.List]
			[PXDefault(orderBy.DueDateRefNbr)]
			public virtual string OrderBy
			{
				get
				{
					return _OrderBy;
				}
				set
				{
					_OrderBy = value;
				}
			}
			#endregion
			#region SOOrderBy
			public abstract class sOOrderBy : IBqlField
			{
				#region List
				public class ListAttribute : PXStringListAttribute
				{
					public ListAttribute()
						: base(
						new[] { OrderDateOrderNbr, OrderNbr },
						new[] { Messages.OrderDateOrderNbr, Messages.OrderNbr })
					{ }
				}

				public const string OrderDateOrderNbr = "DAT";
				public const string OrderNbr = "ORD";
				#endregion
			}
			protected string _SOOrderBy;
			[PXDBString(3, IsFixed = true)]
			[PXUIField(DisplayName = "Order by")]
			[sOOrderBy.List]
			[PXDefault(sOOrderBy.OrderDateOrderNbr)]
			public virtual string SOOrderBy
			{
				get
				{
					return _SOOrderBy;
				}
				set
				{
					_SOOrderBy = value;
				}
			}
			#endregion
			#region IsInvoice
			public abstract class isInvoice : IBqlField
			{
			}
			protected bool? _IsInvoice;
			[PXDBBool]
			public virtual bool? IsInvoice
			{
				get
				{
					return _IsInvoice;
				}
				set
				{
					_IsInvoice = value;
				}
			}

			#endregion
		}

		#endregion

		#region PXSelect views
		/// <summary>
		/// Necessary for proper cache resolution inside selector
		/// on <see cref="ARAdjust.DisplayRefNbr"/>.
		/// </summary>
		public PXSelect<Standalone.ARRegister> dummy_register;

		public ToggleCurrency<ARPayment> CurrencyView;

		[PXViewName(Messages.ARPayment)]
		[PXCopyPasteHiddenFields(typeof(ARPayment.extRefNbr))]
		public PXSelectJoin<
			ARPayment,
			LeftJoinSingleTable<Customer,
				On<Customer.bAccountID, Equal<ARPayment.customerID>>>,
			Where<
				ARPayment.docType, Equal<Optional<ARPayment.docType>>,
				And<Where<
					Customer.bAccountID, IsNull,
					Or<Match<Customer, Current<AccessInfo.userName>>>>>>>
			Document;

		public PXSelect<ARPayment,
			Where<ARPayment.docType, Equal<Current<ARPayment.docType>>,
				And<ARPayment.refNbr, Equal<Current<ARPayment.refNbr>>>>> CurrentDocument;

		[PXViewName(Messages.DocumentsToApply)]
		[PXCopyPasteHiddenView]
		public PXSelectJoin<ARAdjust,
			LeftJoin<ARInvoice, On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>,
				And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>,
			InnerJoin<Standalone.ARRegisterAlias,
				On<Standalone.ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>,
				And<Standalone.ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>>>,
			Where<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>,
				And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>,
				And<ARAdjust.adjNbr, Equal<Current<ARPayment.adjCntr>>>>>> Adjustments;

		public PXSelect<ARAdjust,
			Where<ARAdjust.adjgDocType, Equal<Optional<ARPayment.docType>>,
				And<ARAdjust.adjgRefNbr, Equal<Optional<ARPayment.refNbr>>,
				And<ARAdjust.adjNbr, Equal<Optional<ARPayment.adjCntr>>>>>> Adjustments_Raw;

		public PXSelectJoin<
			ARAdjust,
				InnerJoinSingleTable<ARInvoice,
					On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>,
					And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>,
				InnerJoin<Standalone.ARRegisterAlias,
					On<Standalone.ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>,
					And<Standalone.ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>,
				InnerJoin<CurrencyInfo,
					On<CurrencyInfo.curyInfoID, Equal<Standalone.ARRegisterAlias.curyInfoID>>>>>,
			Where<
				ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>,
				And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>,
				And<ARAdjust.adjNbr, Equal<Current<ARPayment.adjCntr>>>>>>
			Adjustments_Invoices;

		public PXSelectJoin<
			ARAdjust,
				InnerJoinSingleTable<ARPayment,
					On<ARPayment.docType, Equal<ARAdjust.adjdDocType>,
					And<ARPayment.refNbr, Equal<ARAdjust.adjdRefNbr>>>,
				InnerJoin<Standalone.ARRegisterAlias,
					On<Standalone.ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>,
					And<Standalone.ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>>>,
			Where<
				ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>,
				And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>,
				And<ARAdjust.adjNbr, Equal<Current<ARPayment.adjCntr>>>>>>
			Adjustments_Payments;

		[PXViewName(Messages.ApplicationHistory)]
		[PXCopyPasteHiddenView]
		public PXSelectJoin<
			ARAdjust,
				LeftJoin<ARInvoice,
					On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>,
					And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>>,
			Where<
				ARAdjust.adjgDocType, Equal<Optional<ARPayment.docType>>,
				And<ARAdjust.adjgRefNbr, Equal<Optional<ARPayment.refNbr>>,
				And<ARAdjust.adjNbr, Less<Optional<ARPayment.adjCntr>>>>>>
			Adjustments_History;

		[PXViewName(Messages.OrdersToApply)]
		[PXCopyPasteHiddenView]
		public PXSelectJoin<
			SOAdjust,
				LeftJoin<SOOrder,
					On<SOOrder.orderType, Equal<SOAdjust.adjdOrderType>,
					And<SOOrder.orderNbr, Equal<SOAdjust.adjdOrderNbr>>>>,
			Where<
				SOAdjust.adjgDocType, Equal<Current<ARPayment.docType>>,
				And<SOAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>>>>
			SOAdjustments;

		public PXSelectJoin<SOAdjust, InnerJoin<SOOrder, On<SOOrder.orderType, Equal<SOAdjust.adjdOrderType>, And<SOOrder.orderNbr, Equal<SOAdjust.adjdOrderNbr>>>, InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<SOOrder.curyInfoID>>>>, Where<SOAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<SOAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>>>> SOAdjustments_Orders;
		public PXSelect<SOAdjust, Where<SOAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<SOAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>>>> SOAdjustments_Raw;
		public PXSelect<SOOrder, Where<SOOrder.customerID, Equal<Required<SOOrder.customerID>>, And<SOOrder.orderType, Equal<Required<SOOrder.orderType>>, And<SOOrder.orderNbr, Equal<Required<SOOrder.orderNbr>>>>>> SOOrder_CustomerID_OrderType_RefNbr;

		public PXSetup<ARSetup> arsetup;
		public PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<ARPayment.curyInfoID>>>> currencyinfo;

		[PXReadOnlyView]
		public PXSelect<ARInvoice> dummy_ARInvoice;
		[PXReadOnlyView]
		public PXSelect<CATran> dummy_CATran;
		public PXSelect<GL.Branch, Where<GL.Branch.branchID, Equal<Required<GL.Branch.branchID>>>> CurrentBranch;


		public PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Optional<CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
		public PXSelectJoin<ARInvoice, InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>> ARInvoice_DocType_RefNbr;
		public PXSelectJoin<ARPayment, InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<ARPayment.curyInfoID>>>, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>> ARPayment_DocType_RefNbr;

		[PXViewName(Messages.Customer)]
		public PXSetup<
			Customer,
			Where<Customer.bAccountID, Equal<Optional<ARPayment.customerID>>>> customer;

		[PXViewName(Messages.CustomerLocation)]
		public PXSetup<
			Location,
			Where<
				Location.bAccountID, Equal<Current<ARPayment.customerID>>,
				And<Location.locationID, Equal<Optional<ARPayment.customerLocationID>>>>>
			location;

		public PXSetup<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>> customerclass;

		public PXSelect<ARBalances> arbalances;

		[PXViewName(Messages.CashAccount)]
		public PXSetup<
			CashAccount,
			Where<CashAccount.cashAccountID, Equal<Current<ARPayment.cashAccountID>>>> cashaccount;

		public PXSetup<FinPeriod, Where<FinPeriod.finPeriodID, Equal<Optional<ARPayment.adjFinPeriodID>>>> finperiod;
		public PXSetup<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<ARPayment.paymentMethodID>>>> paymentmethod;

		[PXViewName(Messages.CreditCardProcessingInfo)]
		public PXSelectReadonly<
			CCProcTran,
			Where<
				CCProcTran.refNbr, Equal<Current<ARPayment.refNbr>>,
				And<Where<
					CCProcTran.docType, Equal<Current<ARPayment.docType>>,
					// CC Transactions are available for both the main and voiding document.
					// -
					Or<ARDocType.voidPayment, Equal<Current<ARPayment.docType>>>>>>,
			OrderBy<
				Desc<CCProcTran.tranNbr>>>
			ccProcTran;

		public PXSelect<CCProcTran> ccAttachedTran;
		public PXSetup<GLSetup> glsetup;

		public ARPaymentChargeSelect<ARPayment, ARPayment.paymentMethodID, ARPayment.cashAccountID, ARPayment.docDate, ARPayment.finPeriodID, ARPayment.pMInstanceID,
			Where<ARPaymentChargeTran.docType, Equal<Current<ARPayment.docType>>,
				And<ARPaymentChargeTran.refNbr, Equal<Current<ARPayment.refNbr>>>>> PaymentCharges;

		public PXFilter<CCPaymentInfo> ccPaymentInfo;

		public PXFilter<LoadOptions> loadOpts;

		public PXSelect<GLVoucher, Where<True, Equal<False>>> Voucher;
		public PXSelect<ARSetupApproval,
			Where<Current<ARPayment.docType>, Equal<ARDocType.refund>,
				And<ARSetupApproval.docType, Equal<ARDocType.refund>>>> SetupApproval;
		#endregion

        [PXViewName(Messages.CustomerCredit)]
        public CustomerCreditHelperForPayments ARCustomerCreditHelper;
		[PXViewName(EP.Messages.Approval)]
		public EPApprovalAutomationWithoutHoldDefaulting<ARPayment, ARPayment.approved, ARPayment.rejected, ARPayment.hold, ARSetupApproval> Approval;

		private const string ClassName = "ARPaymentEntry";
		#region Cache Attached
		[PXDBString(3, IsFixed = true)]
		[PXDBDefault(typeof(ARRegister.docType))]
		[PXUIField(DisplayName = Messages.DocType)]
		protected virtual void CCProcTran_DocType_CacheAttached(PXCache sender)
		{ }

		[PXDBString(15, IsUnicode = true)]
		[PXDBDefault(typeof(ARRegister.refNbr))]
		[PXParent(typeof(Select<
			ARPayment,
			Where<
				ARPayment.refNbr, Equal<Current<CCProcTran.refNbr>>,
				And<
					ARPayment.docType, Equal<Current<CCProcTran.docType>>>>>))]
		[PXUIField(DisplayName = Messages.DocRefNbr)]
		protected virtual void CCProcTran_RefNbr_CacheAttached(PXCache sender)
		{ }

		[PXDBTimestamp(RecordComesFirst = true)]
		protected virtual void CATran_tstamp_CacheAttached(PXCache sender)
		{ }

		#region ARAdjust
		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Switch<
			Case<Where<
				ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>,
				And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>>>,
				ARAdjust.adjType.adjusted>,
			ARAdjust.adjType.adjusting>))]
		protected virtual void ARAdjust_AdjType_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[BatchNbr(typeof(Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAR>>>),
			IsMigratedRecordField = typeof(ARAdjust.isMigratedRecord))]
		protected virtual void ARAdjust_AdjBatchNbr_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Switch<
				Case<Where<ARAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>, ARAdjust.adjdDocType,
				Case<Where<ARAdjust.adjType, Equal<ARAdjust.adjType.adjusting>>, ARAdjust.adjgDocType>>>))]
		protected virtual void ARAdjust_DisplayDocType_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Switch<
				Case<Where<ARAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>, ARAdjust.adjdRefNbr,
				Case<Where<ARAdjust.adjType, Equal<ARAdjust.adjType.adjusting>>, ARAdjust.adjgRefNbr>>>))]
		protected virtual void ARAdjust_DisplayRefNbr_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Selector<ARAdjust.displayRefNbr, Selector<Standalone.ARRegister.customerID, BAccount.acctCD>>))]
		protected virtual void ARAdjust_DisplayCustomerID_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Selector<ARAdjust.displayRefNbr, Standalone.ARRegister.docDate>))]
		protected virtual void ARAdjust_DisplayDocDate_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Selector<ARAdjust.displayRefNbr, Standalone.ARRegister.docDesc>))]
		protected virtual void ARAdjust_DisplayDocDesc_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(Selector<ARAdjust.displayRefNbr, Standalone.ARRegister.curyID>))]
		protected virtual void ARAdjust_DisplayCuryID_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(IIf<
			Where<ARAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>,
			ARAdjust.curyAdjgAmt,
			ARAdjust.curyAdjdAmt>))]
		protected virtual void ARAdjust_DisplayCuryAmt_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(IIf<
			Where<ARAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>,
			ARAdjust.curyAdjgPPDAmt,
			ARAdjust.curyAdjdPPDAmt>))]
		protected virtual void ARAdjust_DisplayCuryPPDAmt_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXFormula(typeof(IIf<
			Where<ARAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>,
			ARAdjust.curyAdjgWOAmt,
			ARAdjust.curyAdjdWOAmt>))]
		protected virtual void ARAdjust_DisplayCuryWOAmt_CacheAttached(PXCache sender) { }

		// We need this code to disable CurrencyInfo attribute on the SOOrder.CuryInfoID field, 
		// which is may set an incorrect value to the current CurrencyInfo.IsReadOnly flag.
		//
		[PXDBLong]
		protected virtual void SOOrder_CuryInfoID_CacheAttached(PXCache sender) { }

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[CurrencyInfo]
		[PXFormula(typeof(IIf<Where<ARAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>, ARAdjust.adjgCuryInfoID, ARAdjust.adjdCuryInfoID>))]
		protected virtual void ARAdjust_DisplayCuryInfoID_CacheAttached(PXCache sender) { }

		[PXDefault]
		[PXDBInt]
		[PXSelector(typeof(Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeARPayment>>>),
				DescriptionField = typeof(EPAssignmentMap.name))]
		[PXUIField(DisplayName = "Approval Map")]
		protected virtual void EPApproval_AssignmentMapID_CacheAttached(PXCache sender)
		{
		}
		[PXDBDate]
		[PXDefault(typeof(ARPayment.docDate), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
		{
		}

		[PXDBInt]
		[PXDefault(typeof(ARPayment.customerID), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
		{
		}

		[PXDBString(60, IsUnicode = true)]
		[PXDefault(typeof(ARPayment.docDesc), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
		{
		}

		[PXDBLong]
		[CurrencyInfo(typeof(ARPayment.curyInfoID))]
		protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
		{
		}

		[PXDBDecimal]
		[PXDefault(typeof(ARPayment.curyOrigDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
		{
		}

		[PXDBDecimal]
		[PXDefault(typeof(ARPayment.origDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
		{
		}
		protected virtual void EPApproval_SourceItemType_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Document.Current != null)
			{
				e.NewValue = new ARDocType.ListAttribute()
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
		#region Properties
		protected bool _AutoPaymentApp = false;
		public bool AutoPaymentApp
		{
			get
			{
				return this._AutoPaymentApp;
			}
			set
			{
				this._AutoPaymentApp = value;
			}
		}
		#endregion

		#region CallBack Handlers
		public PXAction<ARPayment> newCustomer;
		[PXUIField(DisplayName = "New Customer", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable NewCustomer(PXAdapter adapter)
		{
			CustomerMaint graph = PXGraph.CreateInstance<CustomerMaint>();
			throw new PXRedirectRequiredException(graph, "New Customer");
		}

		public PXAction<ARPayment> editCustomer;
		[PXUIField(DisplayName = "Edit Customer", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable EditCustomer(PXAdapter adapter)
		{
			if (customer.Current != null)
			{
				CustomerMaint graph = PXGraph.CreateInstance<CustomerMaint>();
				graph.BAccount.Current = customer.Current;
				throw new PXRedirectRequiredException(graph, "Edit Customer");
			}
			return adapter.Get();
		}

		public PXAction<ARPayment> customerDocuments;
		[PXUIField(DisplayName = "Customer Details", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable CustomerDocuments(PXAdapter adapter)
		{
			if (customer.Current != null)
			{
				ARDocumentEnq graph = PXGraph.CreateInstance<ARDocumentEnq>();
				graph.Filter.Current.CustomerID = customer.Current.BAccountID;
				graph.Filter.Select();
				throw new PXRedirectRequiredException(graph, "Customer Details");
			}
			return adapter.Get();
		}

		[PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: false,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public override IEnumerable Release(PXAdapter adapter)
		{
			PXCache cache = Document.Cache;
			List<ARRegister> list = new List<ARRegister>();
			foreach (ARPayment ardoc in adapter.Get<ARPayment>())
			{
				if (!(bool)ardoc.Hold)
				{
					cache.Update(ardoc);
					list.Add(ardoc);
				}
			}
			if (list.Count == 0)
			{
				throw new PXException(Messages.Document_Status_Invalid);
			}
			Save.Press();
			PXLongOperation.StartOperation(this, delegate () { ARDocumentRelease.ReleaseDoc(list, false); });
			return list;
		}
		[PXUIField(DisplayName = "Void", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: false,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public override IEnumerable VoidCheck(PXAdapter adapter)
		{
			List<ARPayment> result = new List<ARPayment>();

			ARPayment payment = Document.Current;

			if (payment == null) return adapter.Get();

			if (payment != null &&
				payment.Released == true &&
				payment.Voided == false
				&& (payment.DocType == ARDocType.Payment ||
					payment.DocType == ARDocType.Prepayment ||
					payment.SelfVoidingDoc == true))
			{
				ARAdjust refundApplication = PXSelect<ARAdjust,
					Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>,
						And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>,
						And<ARAdjust.adjgDocType, Equal<ARDocType.refund>,
						And<ARAdjust.voided, NotEqual<True>>>>>>
					.SelectWindowed(this, 0, 1, payment.DocType, payment.RefNbr);

				if (refundApplication != null && refundApplication.IsSelfAdjustment() != true)
				{
					throw new PXException(
						Common.Messages.DocumentHasBeenRefunded,
						GetLabel.For<ARDocType>(payment.DocType),
						Document.Current.RefNbr,
						GetLabel.For<ARDocType>(refundApplication.AdjgDocType),
						refundApplication.AdjgRefNbr);
				}

				if (arsetup.Current.MigrationMode != true &&
					payment.IsMigratedRecord == true &&
					payment.CuryInitDocBal != payment.CuryOrigDocAmt)
				{
					throw new PXException(Messages.MigrationModeIsDeactivatedForMigratedDocument);
				}

				if (arsetup.Current.MigrationMode == true &&
					payment.IsMigratedRecord == true &&
					Adjustments_History
						.Select()
						.RowCast<ARAdjust>()
						.Any(application =>
							application.Voided != true &&
							application.VoidAppl != true &&
							application.IsMigratedRecord != true &&
							application.IsInitialApplication != true))
				{
					throw new PXException(Common.Messages.CannotVoidPaymentRegularUnreversedApplications);
				}

				ARPayment voidPayment = Document.Search<ARPayment.refNbr>(payment.RefNbr, ARDocType.VoidPayment);

				if (voidPayment != null)
				{
					result.Add(payment);
					return result;
				}

				// Delete unreleased applications
				// -
				bool anyApplicationDeleted = false;

				foreach (ARAdjust application in Adjustments_Raw.Select())
				{
					Adjustments.Cache.Delete(application);
					anyApplicationDeleted = true;
				}

				if (!anyApplicationDeleted
					&& payment.OpenDoc == true
					&& PXUIFieldAttribute.GetError<ARPayment.adjFinPeriodID>(Document.Cache, payment) != null)
				{
					Document.Cache.SetStatus(payment, PXEntryStatus.Notchanged);
				}

				Save.Press();

				ARPayment document = PXCache<ARPayment>.CreateCopy(payment);

				if (document.SelfVoidingDoc != true
					&& (
						paymentmethod.Current == null
						|| paymentmethod.Current.ARDefaultVoidDateToDocumentDate != true))
				{
					document.DocDate = Accessinfo.BusinessDate > document.DocDate
						? Accessinfo.BusinessDate
						: document.DocDate;

					string businessPeriodID = FinPeriodIDAttribute.PeriodFromDate(this, document.DocDate);
					document.AdjFinPeriodID = businessPeriodID;
					document.AdjTranPeriodID = businessPeriodID;

					finperiod.Cache.Current = finperiod.View.SelectSingleBound(new object[] { document });
				}

				FiscalPeriodUtils.VerifyFinPeriod<ARPayment.adjFinPeriodID, FinPeriod.aPClosed>(
					this,
					Document.Cache,
					document,
					finperiod);

				if (document.DepositAsBatch == true
					&& !string.IsNullOrEmpty(document.DepositNbr)
					&& document.Deposited != true)
				{
					throw new PXException(Messages.ARPaymentIsIncludedIntoCADepositAndCannotBeVoided);
				}

				try
				{
					_IsVoidCheckInProgress = true;

					if (payment.SelfVoidingDoc == true)
					{
						SelfVoidingProc(document);
					}
					else
					{
						VoidCheckProc(document);
					}
				}
				finally
				{
					_IsVoidCheckInProgress = false;
				}

				Document.Cache.RaiseExceptionHandling<ARPayment.finPeriodID>(Document.Current, Document.Current.FinPeriodID, null);

				result.Add(Document.Current);
				return result;
			}
			else if (
				payment.Released != true
				&& payment.Voided == false
				&& (payment.DocType == ARDocType.Payment || payment.DocType == ARDocType.Prepayment))
			{
				if (CCProcTranHelper.HasCCTransactions(ccProcTran))
				{
					ARPayment document = payment;

					document.Voided = true;
					document.OpenDoc = false;

					foreach (ARAdjust application in Adjustments_Raw.Select())
					{
						if (application.Voided == true) continue;

						ARAdjust applicationCopy = (ARAdjust)Caches[typeof(ARAdjust)].CreateCopy(application);
						applicationCopy.Voided = true;

						Caches[typeof(ARAdjust)].Update(applicationCopy);
					}

					if (document.CATranID != null && document.CashAccountID != null)
					{
						CATran cashTransaction = PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>>.Select(this, document.CATranID);

						if (cashTransaction != null)
						{
							Caches[typeof(CATran)].Delete(cashTransaction);
						}

						document.CATranID = null;
					}

					document = Document.Update(document);
					Save.Press();
				}
			}
			return adapter.Get();
		}

		protected class PXLoadInvoiceException : Exception
		{
			public PXLoadInvoiceException() { }

			public PXLoadInvoiceException(SerializationInfo info, StreamingContext context)
				: base(info, context) { }
		}

		private ARAdjust AddAdjustment(ARAdjust adj)
		{
			if (Document.Current.CuryUnappliedBal == 0m && Document.Current.CuryOrigDocAmt > 0m)
			{
				throw new PXLoadInvoiceException();
			}
			return this.Adjustments.Insert(adj);
		}

		public virtual void LoadInvoicesProc(bool LoadExistingOnly)
		{
			LoadInvoicesProc(LoadExistingOnly, null);
		}

		public virtual void LoadInvoicesProc(bool LoadExistingOnly, LoadOptions opts)
		{
			Dictionary<string, ARAdjust> existing = new Dictionary<string, ARAdjust>();
			ARPayment currentDoc = Document.Current;
			InternalCall = true;
			try
			{
				if (currentDoc == null ||
					currentDoc.CustomerID == null ||
					currentDoc.OpenDoc == false ||
					currentDoc.DocType != ARDocType.Payment &&
					currentDoc.DocType != ARDocType.Prepayment &&
					currentDoc.DocType != ARDocType.CreditMemo)
				{
					throw new PXLoadInvoiceException();
				}

				foreach (PXResult<ARAdjust> res in Adjustments_Raw.Select())
				{
					ARAdjust old_adj = (ARAdjust)res;

					if (LoadExistingOnly == false)
					{
						old_adj = PXCache<ARAdjust>.CreateCopy(old_adj);
						old_adj.CuryAdjgAmt = null;
						old_adj.CuryAdjgDiscAmt = null;
						old_adj.CuryAdjgPPDAmt = null;
					}

					string s = string.Format("{0}_{1}", old_adj.AdjdDocType, old_adj.AdjdRefNbr);
					existing.Add(s, old_adj);
					Adjustments.Cache.Delete((ARAdjust)res);
				}

				currentDoc.AdjCntr++;
				if (Document.Cache.GetStatus(currentDoc) == PXEntryStatus.Notchanged || Document.Cache.GetStatus(currentDoc) == PXEntryStatus.Held)
				{
					Document.Cache.SetStatus(currentDoc, PXEntryStatus.Updated);
				}
				Document.Cache.IsDirty = true;

				foreach (KeyValuePair<string, ARAdjust> res in existing)
				{
					ARAdjust adj = new ARAdjust();
					adj.AdjdDocType = res.Value.AdjdDocType;
					adj.AdjdRefNbr = res.Value.AdjdRefNbr;

					try
					{
						adj = PXCache<ARAdjust>.CreateCopy(AddAdjustment(adj));
						if (res.Value.CuryAdjgDiscAmt != null && res.Value.CuryAdjgDiscAmt < adj.CuryAdjgDiscAmt)
						{
							adj.CuryAdjgDiscAmt = res.Value.CuryAdjgDiscAmt;
							adj.CuryAdjgPPDAmt = res.Value.CuryAdjgDiscAmt;
							adj = PXCache<ARAdjust>.CreateCopy((ARAdjust)this.Adjustments.Cache.Update(adj));
						}

						if (res.Value.CuryAdjgAmt != null && res.Value.CuryAdjgAmt < adj.CuryAdjgAmt)
						{
							adj.CuryAdjgAmt = res.Value.CuryAdjgAmt;
							adj = PXCache<ARAdjust>.CreateCopy((ARAdjust)this.Adjustments.Cache.Update(adj));
						}
						if (res.Value.WriteOffReasonCode != null)
						{
							adj.WriteOffReasonCode = res.Value.WriteOffReasonCode;
							adj.CuryAdjgWOAmt = res.Value.CuryAdjgWOAmt;
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
				PXResultset<ARInvoice> custdocs = GetCustDocs(opts, currentDoc, arsetup.Current, graph);

				foreach (PXResult<ARInvoice, CurrencyInfo> res in custdocs)
				{
					ARInvoice invoice = res;
					CurrencyInfo info = res;

					string s = string.Format("{0}_{1}", invoice.DocType, invoice.RefNbr);
					if (existing.ContainsKey(s) == false)
					{
						ARAdjust adj = new ARAdjust();
						adj.AdjdDocType = invoice.DocType;
						adj.AdjdRefNbr = invoice.RefNbr;

						PXSelectorAttribute.StoreCached<ARAdjust.adjdRefNbr>(Adjustments.Cache, adj, new ARAdjust.ARInvoice { DocType = adj.AdjdDocType, RefNbr = adj.AdjdRefNbr });
						ARInvoice_DocType_RefNbr.StoreCached(new PXCommandKey(new object[] { invoice.DocType, invoice.RefNbr }), new List<object> { res });
						CurrencyInfo_CuryInfoID.StoreCached(new PXCommandKey(new object[] { info.CuryInfoID }), new List<object> { info });
						//PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Optional<CurrencyInfo.curyInfoID>>>>.StoreCached(this, new PXCommandKey(new object[] { info.CuryInfoID }), new List<object> { info });

						AddAdjustment(adj);
					}
				}

				if (currentDoc.CuryApplAmt < 0m)
				{
					List<ARAdjust> credits = new List<ARAdjust>();

					foreach (ARAdjust adj in Adjustments_Raw.Select())
					{
						if (adj.AdjdDocType == ARDocType.CreditMemo)
						{
							credits.Add(adj);
						}
					}

					credits.Sort((a, b) =>
					{
						return ((IComparable)a.CuryAdjgAmt).CompareTo(b.CuryAdjgAmt);
					});

					foreach (ARAdjust adj in credits)
					{
						if (adj.CuryAdjgAmt <= -currentDoc.CuryApplAmt)
						{
							Adjustments.Delete(adj);
						}
						else
						{
							ARAdjust copy = PXCache<ARAdjust>.CreateCopy(adj);
							copy.CuryAdjgAmt += currentDoc.CuryApplAmt;
							Adjustments.Update(copy);
						}
					}
				}

			}
			catch (PXLoadInvoiceException)
			{
			}
			finally
			{
				InternalCall = false;
			}
		}

		public static PXResultset<ARInvoice> GetCustDocs(LoadOptions opts, ARPayment currentARPayment, ARSetup currentARSetup, PXGraph graph)
		{
			PXSelectBase<ARInvoice> cmd = new PXSelectReadonly2<ARInvoice,
				InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>,
				InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>,
				LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>,
					And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>,
					And<ARAdjust.released, NotEqual<True>,
					And<ARAdjust.voided, NotEqual<True>,
					And<Where<ARAdjust.adjgDocType, NotEqual<Required<ARRegister.docType>>,
						Or<ARAdjust.adjgRefNbr, NotEqual<Required<ARRegister.refNbr>>>>>>>>>,
				LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARInvoice.docType>,
					And<ARAdjust2.adjgRefNbr, Equal<ARInvoice.refNbr>,
					And<ARAdjust2.released, NotEqual<True>,
					And<ARAdjust2.voided, NotEqual<True>>>>>>>>>,
				Where<ARInvoice.docType, NotEqual<Required<ARPayment.docType>>,
					And<ARInvoice.released, Equal<True>,
					And<ARInvoice.openDoc, Equal<True>,
					And<ARAdjust.adjgRefNbr, IsNull,
					And<ARAdjust2.adjgRefNbr, IsNull,
					And<ARInvoice.pendingPPD, NotEqual<True>>>>>>>,
					OrderBy<Asc<ARInvoice.dueDate, Asc<ARInvoice.refNbr>>>>(graph);

			if (opts != null)
			{
				if (opts.FromDate != null)
				{
					cmd.WhereAnd<Where<ARInvoice.docDate, GreaterEqual<Required<LoadOptions.fromDate>>>>();
				}
				if (opts.TillDate != null)
				{
					cmd.WhereAnd<Where<ARInvoice.docDate, LessEqual<Required<LoadOptions.tillDate>>>>();
				}
				if (!string.IsNullOrEmpty(opts.StartRefNbr))
				{
					cmd.WhereAnd<Where<ARInvoice.refNbr, GreaterEqual<Required<LoadOptions.startRefNbr>>>>();
				}
				if (!string.IsNullOrEmpty(opts.EndRefNbr))
				{
					cmd.WhereAnd<Where<ARInvoice.refNbr, LessEqual<Required<LoadOptions.endRefNbr>>>>();
				}
			}

			var loadChildDocs = opts == null ? LoadOptions.loadChildDocuments.None : opts.LoadChildDocuments;
			switch (loadChildDocs)
			{
				case LoadOptions.loadChildDocuments.IncludeCRM:
					cmd.WhereAnd<Where<ARInvoice.customerID, Equal<Required<ARRegister.customerID>>,
									Or<Customer.consolidatingBAccountID, Equal<Required<ARRegister.customerID>>>>>();
					break;
				case LoadOptions.loadChildDocuments.ExcludeCRM:
					cmd.WhereAnd<Where<ARInvoice.customerID, Equal<Required<ARRegister.customerID>>,
									Or<Customer.consolidatingBAccountID, Equal<Required<ARRegister.customerID>>,
										And<ARInvoice.docType, NotEqual<ARDocType.creditMemo>>>>>();
					break;
				default:
					cmd.WhereAnd<Where<ARInvoice.customerID, Equal<Required<ARRegister.customerID>>>>();
					break;
			}

			switch (currentARPayment.DocType)
			{
				case ARDocType.Payment:
					cmd.WhereAnd<Where<ARInvoice.docType, Equal<ARDocType.invoice>,
						Or<ARInvoice.docType, Equal<ARDocType.debitMemo>,
						Or<ARInvoice.docType, Equal<ARDocType.creditMemo>,
						Or<ARInvoice.docType, Equal<ARDocType.finCharge>>>>>>();
					break;
				case ARDocType.Prepayment:
				case ARDocType.CreditMemo:
					cmd.WhereAnd<Where<ARInvoice.docType, Equal<ARDocType.invoice>,
						Or<ARInvoice.docType, Equal<ARDocType.debitMemo>,
						Or<ARInvoice.docType, Equal<ARDocType.finCharge>>>>>();
					break;
				default:
					cmd.WhereAnd<Where<True, Equal<False>>>();
					break;
			}

			List<object> parametrs = new List<object>();
			parametrs.Add(currentARPayment.DocType);
			parametrs.Add(currentARPayment.RefNbr);
			parametrs.Add(currentARPayment.DocType);
			if (opts != null)
			{
				if (opts.FromDate != null)
				{
					parametrs.Add(opts.FromDate);
				}
				if (opts.TillDate != null)
				{
					parametrs.Add(opts.TillDate);
				}
				if (!string.IsNullOrEmpty(opts.StartRefNbr))
				{
					parametrs.Add(opts.StartRefNbr);
				}
				if (!string.IsNullOrEmpty(opts.EndRefNbr))
				{
					parametrs.Add(opts.EndRefNbr);
				}
			}

			switch (loadChildDocs)
			{
				case LoadOptions.loadChildDocuments.IncludeCRM:
				case LoadOptions.loadChildDocuments.ExcludeCRM:
					parametrs.Add(currentARPayment.CustomerID);
					parametrs.Add(currentARPayment.CustomerID);
					break;
				default:
					parametrs.Add(currentARPayment.CustomerID);
					break;
			}

			PXResultset<ARInvoice> custdocs = opts == null || opts.MaxDocs == null ? cmd.Select(parametrs.ToArray()) : cmd.SelectWindowed(0, (int)opts.MaxDocs, parametrs.ToArray());

			custdocs.Sort(new Comparison<PXResult<ARInvoice>>(delegate (PXResult<ARInvoice> a, PXResult<ARInvoice> b)
			{
				int aSortOrder = 0;
				int bSortOrder = 0;

				if (currentARPayment.CuryOrigDocAmt > 0m)
				{
					aSortOrder += (((ARInvoice)a).DocType == ARDocType.CreditMemo ? 0 : 1000);
					bSortOrder += (((ARInvoice)b).DocType == ARDocType.CreditMemo ? 0 : 1000);
				}
				else
				{
					aSortOrder += (((ARInvoice)a).DocType == ARDocType.CreditMemo ? 1000 : 0);
					bSortOrder += (((ARInvoice)b).DocType == ARDocType.CreditMemo ? 1000 : 0);
				}

				if (currentARSetup.FinChargeFirst == true)
				{
					aSortOrder += (((ARInvoice)a).DocType == ARDocType.FinCharge ? 0 : 100);
					bSortOrder += (((ARInvoice)b).DocType == ARDocType.FinCharge ? 0 : 100);
				}

				DateTime aDueDate = ((ARInvoice)a).DueDate ?? DateTime.MinValue;
				DateTime bDueDate = ((ARInvoice)b).DueDate ?? DateTime.MinValue;

					object aObj;
					object bObj;

				string orderBy = opts?.OrderBy ?? LoadOptions.orderBy.DueDateRefNbr;

				switch (orderBy)
					{
						case LoadOptions.orderBy.RefNbr:

							aObj = ((ARInvoice)a).RefNbr;
							bObj = ((ARInvoice)b).RefNbr;
							aSortOrder += (1 + ((IComparable)aObj).CompareTo(bObj)) / 2;
							bSortOrder += (1 - ((IComparable)aObj).CompareTo(bObj)) / 2;
							break;

						case LoadOptions.orderBy.DocDateRefNbr:

							aObj = ((ARInvoice)a).DocDate;
							bObj = ((ARInvoice)b).DocDate;
							aSortOrder += (1 + ((IComparable)aObj).CompareTo(bObj)) / 2 * 10;
							bSortOrder += (1 - ((IComparable)aObj).CompareTo(bObj)) / 2 * 10;

							aObj = ((ARInvoice)a).RefNbr;
							bObj = ((ARInvoice)b).RefNbr;
							aSortOrder += (1 + ((IComparable)aObj).CompareTo(bObj)) / 2;
							bSortOrder += (1 - ((IComparable)aObj).CompareTo(bObj)) / 2;
							break;
						default:
							aSortOrder += (1 + aDueDate.CompareTo(bDueDate)) / 2 * 10;
							bSortOrder += (1 - aDueDate.CompareTo(bDueDate)) / 2 * 10;


							aObj = ((ARInvoice)a).RefNbr;
							bObj = ((ARInvoice)b).RefNbr;
							aSortOrder += (1 + ((IComparable)aObj).CompareTo(bObj)) / 2;
							bSortOrder += (1 - ((IComparable)aObj).CompareTo(bObj)) / 2;
							break;
					}

				return aSortOrder.CompareTo(bSortOrder);
			}));
			return custdocs;
		}

		public virtual void LoadOptions_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			LoadOptions filter = (LoadOptions)e.Row;
			if (filter != null)
			{
				PXUIFieldAttribute.SetVisible<LoadOptions.startRefNbr>(sender, filter, filter.IsInvoice == true);
				PXUIFieldAttribute.SetVisible<LoadOptions.endRefNbr>(sender, filter, filter.IsInvoice == true);
				PXUIFieldAttribute.SetVisible<LoadOptions.orderBy>(sender, filter, filter.IsInvoice == true);

				bool currentCustomerHasChildren =
					PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>() &&
					Document.Current != null && Document.Current.CustomerID != null &&
					((Customer)PXSelect<Customer, Where<Customer.parentBAccountID, Equal<Required<Customer.parentBAccountID>>,
													And<Customer.consolidateToParent, Equal<True>>>>
						.Select(this, Document.Current.CustomerID) != null);

				PXUIFieldAttribute.SetVisible<LoadOptions.loadChildDocuments>(sender, filter, filter.IsInvoice == true && currentCustomerHasChildren);
				PXUIFieldAttribute.SetVisible<LoadOptions.startOrderNbr>(sender, filter, filter.IsInvoice == false);
				PXUIFieldAttribute.SetVisible<LoadOptions.endOrderNbr>(sender, filter, filter.IsInvoice == false);
				PXUIFieldAttribute.SetVisible<LoadOptions.sOOrderBy>(sender, filter, filter.IsInvoice == false);
			}
		}

		public PXAction<ARPayment> loadInvoices;
		[PXUIField(DisplayName = "Load Documents", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.Refresh)]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable LoadInvoices(PXAdapter adapter)
		{
			if (loadOpts != null && loadOpts.Current != null)
			{
				loadOpts.Current.IsInvoice = true;
			}
			if (loadOpts.AskExt() == WebDialogResult.OK)
			{
				LoadInvoicesProc(false, loadOpts.Current);
			}
			return adapter.Get();
		}

		public PXAction<ARPayment> loadOrders;
		[PXUIField(DisplayName = "Load Orders", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.Refresh)]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable LoadOrders(PXAdapter adapter)
		{
			if (loadOpts != null && loadOpts.Current != null)
			{
				loadOpts.Current.IsInvoice = false;
			}
			if (loadOpts.AskExt() == WebDialogResult.OK)
			{
				LoadOrdersProc(false, loadOpts.Current);
			}
			return adapter.Get();
		}

		public virtual void LoadOrdersProc(bool LoadExistingOnly, LoadOptions opts)
		{
			Dictionary<string, SOAdjust> existing = new Dictionary<string, SOAdjust>();

			InternalCall = true;
			try
			{
				if (Document.Current == null || Document.Current.CustomerID == null || Document.Current.OpenDoc == false || !(Document.Current.DocType == ARDocType.Payment || Document.Current.DocType == ARDocType.Prepayment))
				{
					throw new PXLoadInvoiceException();
				}

				foreach (PXResult<SOAdjust> res in SOAdjustments_Raw.Select())
				{
					SOAdjust old_adj = (SOAdjust)res;

					if (LoadExistingOnly == false)
					{
						old_adj = PXCache<SOAdjust>.CreateCopy(old_adj);
						old_adj.CuryAdjgAmt = null;
						old_adj.CuryAdjgDiscAmt = null;
					}

					string s = string.Format("{0}_{1}", old_adj.AdjdOrderType, old_adj.AdjdOrderNbr);
					existing.Add(s, old_adj);
					Adjustments.Cache.Delete((SOAdjust)res);
				}

				if (Document.Cache.GetStatus(Document.Current) == PXEntryStatus.Notchanged || Document.Cache.GetStatus(Document.Current) == PXEntryStatus.Held)
				{
					Document.Cache.SetStatus(Document.Current, PXEntryStatus.Updated);
				}
				Document.Cache.IsDirty = true;

				foreach (KeyValuePair<string, SOAdjust> res in existing)
				{
					SOAdjust adj = new SOAdjust();
					adj.AdjdOrderType = res.Value.AdjdOrderType;
					adj.AdjdOrderNbr = res.Value.AdjdOrderNbr;

					try
					{
						adj = PXCache<SOAdjust>.CreateCopy(AddSOAdjustment(adj) ?? adj);
						if (res.Value.CuryAdjgDiscAmt != null && res.Value.CuryAdjgDiscAmt < adj.CuryAdjgDiscAmt)
						{
							adj.CuryAdjgDiscAmt = res.Value.CuryAdjgDiscAmt;
							adj = PXCache<SOAdjust>.CreateCopy((SOAdjust)this.Adjustments.Cache.Update(adj));
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

				PXSelectBase<SOOrder> cmd = new PXSelectJoin<SOOrder,
					 InnerJoin<SOOrderType, On<SOOrderType.orderType, Equal<SOOrder.orderType>>,
					 InnerJoin<Terms, On<Terms.termsID, Equal<SOOrder.termsID>>>>,
					 Where<SOOrder.customerID, Equal<Optional<ARPayment.customerID>>,
					   And<SOOrder.openDoc, Equal<True>,
					   And<SOOrder.orderDate, LessEqual<Current<ARPayment.adjDate>>,
					   And2<Where<SOOrderType.aRDocType, Equal<ARDocType.invoice>, Or<SOOrderType.aRDocType, Equal<ARDocType.debitMemo>>>,
					   And<Terms.installmentType, NotEqual<TermsInstallmentType.multiple>>>>>>,
				 OrderBy<Asc<SOOrder.orderDate,
						 Asc<SOOrder.orderNbr>>>>(this);

				if (opts != null)
				{
					if (opts.FromDate != null)
					{
						cmd.WhereAnd<Where<SOOrder.orderDate, GreaterEqual<Current<LoadOptions.fromDate>>>>();
					}
					if (opts.TillDate != null)
					{
						cmd.WhereAnd<Where<SOOrder.orderDate, LessEqual<Current<LoadOptions.tillDate>>>>();
					}
					if (!string.IsNullOrEmpty(opts.StartOrderNbr))
					{
						cmd.WhereAnd<Where<SOOrder.orderNbr, GreaterEqual<Current<LoadOptions.startOrderNbr>>>>();
					}
					if (!string.IsNullOrEmpty(opts.EndOrderNbr))
					{
						cmd.WhereAnd<Where<SOOrder.orderNbr, LessEqual<Current<LoadOptions.endOrderNbr>>>>();
					}
				}

				PXResultset<SOOrder> custdocs = opts == null || opts.MaxDocs == null ? cmd.Select() : cmd.SelectWindowed(0, (int)opts.MaxDocs);

				custdocs.Sort(new Comparison<PXResult<SOOrder>>(delegate (PXResult<SOOrder> a, PXResult<SOOrder> b)
				{
					if (arsetup.Current.FinChargeFirst == true)
					{
						int aSortOrder = (((SOOrder)a).DocType == ARDocType.FinCharge ? 0 : 1);
						int bSortOrder = (((SOOrder)b).DocType == ARDocType.FinCharge ? 0 : 1);
						int ret = ((IComparable)aSortOrder).CompareTo(bSortOrder);
						if (ret != 0) return ret;
					}

					if (opts == null)
					{
						object aOrderDate = ((SOOrder)a).OrderDate ?? DateTime.MinValue;
						object bOrderDate = ((SOOrder)b).OrderDate ?? DateTime.MinValue;
						return ((IComparable)aOrderDate).CompareTo(bOrderDate);
					}
					else
					{
						object aObj;
						object bObj;
						int ret;
						switch (opts.OrderBy)
						{
							case LoadOptions.sOOrderBy.OrderNbr:

								aObj = ((SOOrder)a).OrderNbr;
								bObj = ((SOOrder)b).OrderNbr;
								return ((IComparable)aObj).CompareTo(bObj);

							case LoadOptions.sOOrderBy.OrderDateOrderNbr:
							default:
								aObj = ((SOOrder)a).OrderDate ?? DateTime.MinValue;
								bObj = ((SOOrder)b).OrderDate ?? DateTime.MinValue;
								ret = ((IComparable)aObj).CompareTo(bObj);
								if (ret != 0) return ret;

								aObj = ((SOOrder)a).OrderNbr;
								bObj = ((SOOrder)b).OrderNbr;
								return ((IComparable)aObj).CompareTo(bObj);
						}
					}
				}));

				foreach (SOOrder invoice in custdocs)
				{
					string s = string.Format("{0}_{1}", invoice.OrderType, invoice.OrderNbr);
					if (existing.ContainsKey(s) == false)
					{
						SOAdjust adj = new SOAdjust();
						adj.AdjdOrderType = invoice.OrderType;
						adj.AdjdOrderNbr = invoice.OrderNbr;
						AddSOAdjustment(adj);
					}
				}
			}
			catch (PXLoadInvoiceException)
			{
			}
			finally
			{
				InternalCall = false;
			}
		}

		public PXAction<ARPayment> reverseApplication;
		[PXUIField(DisplayName = "Reverse Application", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.Refresh)]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: false,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable ReverseApplication(PXAdapter adapter)
		{
			ARAdjust application = Adjustments_History.Current;
			ARPayment payment = Document.Current;

			if (application == null) return adapter.Get();

			if (application.AdjType == ARAdjust.adjType.Adjusting)
			{
				throw new PXException(
					Common.Messages.IncomingApplicationCannotBeReversed,
					GetLabel.For<ARDocType>(payment.DocType),
					GetLabel.For<ARDocType>(application.AdjgDocType),
					application.AdjgRefNbr);
			}

			if (application.IsInitialApplication == true)
			{
				throw new PXException(Common.Messages.InitialApplicationCannotBeReversed);
			}
			else if (application.IsMigratedRecord != true &&
				arsetup.Current.MigrationMode == true)
			{
				throw new PXException(Messages.CannotReverseRegularApplicationInMigrationMode);
			}

			if (application.Voided != true
				&& (
					ARPaymentType.CanHaveBalance(application.AdjgDocType)
					|| application.AdjgDocType == ARDocType.Refund))
			{
				if (payment != null
					&& (payment.DocType != ARDocType.CreditMemo || payment.PendingPPD != true)
					&& application.AdjdHasPPDTaxes == true
					&& application.PendingPPD != true)
				{
					ARAdjust adjPPD = GetPPDApplication(this, application.AdjdDocType, application.AdjdRefNbr);

					if (adjPPD != null)
					{
						throw new PXSetPropertyException(Messages.PPDApplicationExists, adjPPD.AdjgRefNbr);
					}
				}

				if (Document.Current.OpenDoc == false)
				{
					Document.Current.OpenDoc = true;
					DateTime? tmpDateTime = payment.AdjDate;
					Document.Cache.RaiseRowSelected(payment);
					Document.Cache.SetValueExt<ARPayment.adjDate>(payment, tmpDateTime);
				}

				ARAdjust adjnew = CreateReversingApp(application, payment);

				if (adjnew == null) return adapter.Get();
			}

			return adapter.Get();
		}

		public static ARAdjust GetPPDApplication(PXGraph graph, string DocType, string RefNbr)
		{
			return PXSelect<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>,
				And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>,
				And<ARAdjust.released, Equal<True>,
				And<ARAdjust.voided, NotEqual<True>,
				And<ARAdjust.pendingPPD, Equal<True>>>>>>>.SelectSingleBound(graph, null, DocType, RefNbr);
		}

		public PXAction<ARPayment> viewDocumentToApply;
		[PXUIField(DisplayName = "View Document", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton()]
		public virtual IEnumerable ViewDocumentToApply(PXAdapter adapter)
		{
			ARAdjust row = Adjustments.Current;
			if (!string.IsNullOrEmpty(row?.AdjdDocType) && !string.IsNullOrEmpty(row?.AdjdRefNbr))
			{
				if (row.AdjdDocType == ARDocType.Payment || (row.AdjgDocType == ARDocType.Refund && row.AdjdDocType == ARDocType.Prepayment))
				{
					ARPaymentEntry iegraph = PXGraph.CreateInstance<ARPaymentEntry>();
					iegraph.Document.Current = iegraph.Document.Search<ARPayment.refNbr>(row.AdjdRefNbr, row.AdjdDocType);
					throw new PXRedirectRequiredException(iegraph, true, "View Document") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}
				else
				{
					ARInvoiceEntry iegraph = PXGraph.CreateInstance<ARInvoiceEntry>();
					iegraph.Document.Current = iegraph.Document.Search<ARInvoice.refNbr>(row.AdjdRefNbr, row.AdjdDocType);
					throw new PXRedirectRequiredException(iegraph, true, "View Document") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}
			}

			return adapter.Get();
		}

		public PXAction<ARPayment> viewApplicationDocument;
		[PXUIField(DisplayName = "View Application Document", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton()]
		public virtual IEnumerable ViewApplicationDocument(PXAdapter adapter)
		{
			ARAdjust application = Adjustments_History.Current;

			if (application == null ||
				string.IsNullOrEmpty(application.DisplayDocType) ||
				string.IsNullOrEmpty(application.DisplayRefNbr))
			{
				return adapter.Get();
			}

			PXGraph targetGraph;

			switch (application.DisplayDocType)
			{
				case ARDocType.Payment:
				case ARDocType.Prepayment:
				case ARDocType.Refund:
				case ARDocType.VoidPayment:
				case ARDocType.SmallBalanceWO:
					{
						ARPaymentEntry documentGraph = CreateInstance<ARPaymentEntry>();
						documentGraph.Document.Current = documentGraph.Document.Search<ARPayment.refNbr>(
							application.DisplayRefNbr,
							application.DisplayDocType);

						targetGraph = documentGraph;
						break;
					}
				default:
					{
						ARInvoiceEntry documentGraph = CreateInstance<ARInvoiceEntry>();
						documentGraph.Document.Current = documentGraph.Document.Search<ARInvoice.refNbr>(
							application.DisplayRefNbr,
							application.DisplayDocType);

						targetGraph = documentGraph;
						break;
					}
			}

			throw new PXRedirectRequiredException(
				targetGraph,
				true,
				"View Application Document")
			{
				Mode = PXBaseRedirectException.WindowMode.NewWindow
			};
		}

		protected SOAdjust AddSOAdjustment(SOAdjust adj)
		{
			if (Document.Current.CuryUnappliedBal == 0m && Document.Current.CuryOrigDocAmt > 0m)
			{
				throw new PXLoadInvoiceException();
			}
			return this.SOAdjustments.Insert(adj);
		}

		public PXAction<ARPayment> viewSODocumentToApply;
		[PXUIField(
			DisplayName = "View Order",
			MapEnableRights = PXCacheRights.Select,
			MapViewRights = PXCacheRights.Select,
			Visible = false)]
		[PXLookupButton]
		public virtual IEnumerable ViewSODocumentToApply(PXAdapter adapter)
		{
			SOAdjust row = SOAdjustments.Current;
			if (row != null && !(String.IsNullOrEmpty(row.AdjdOrderType) || String.IsNullOrEmpty(row.AdjdOrderNbr)))
			{
				SOOrderEntry iegraph = PXGraph.CreateInstance<SOOrderEntry>();
				iegraph.Document.Current = iegraph.Document.Search<SOOrder.orderNbr>(row.AdjdOrderNbr, row.AdjdOrderType);
				if (iegraph.Document.Current != null)
				{
					throw new PXRedirectRequiredException(iegraph, true, "View Document") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}
			}
			return adapter.Get();
		}

		public PXAction<ARPayment> viewCurrentBatch;
		[PXUIField(DisplayName = "View Batch", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable ViewCurrentBatch(PXAdapter adapter)
		{
			ARAdjust row = Adjustments_History.Current;
			if (row != null && !String.IsNullOrEmpty(row.AdjBatchNbr))
			{
				JournalEntry graph = PXGraph.CreateInstance<JournalEntry>();
				graph.BatchModule.Current = PXSelect<Batch,
										Where<Batch.module, Equal<BatchModule.moduleAR>,
										And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>
										.Select(this, row.AdjBatchNbr);
				if (graph.BatchModule.Current != null)
				{
					throw new PXRedirectRequiredException(graph, true, "View Batch") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}
			}
			return adapter.Get();
		}

		public static void CheckValidPeriodForCCTran(PXGraph graph)
		{
			DateTime trandate = PXTimeZoneInfo.Today;
			FinPeriod finPeriod = FinPeriodIDAttribute.FindFinPeriodByDate(graph, trandate);
			PXCache paymentsCache = graph.Caches[typeof(ARPayment)];
			foreach (PXEventSubscriberAttribute attr in paymentsCache.GetAttributesReadonly<ARPayment.adjFinPeriodID>())
			{
				if (attr is AROpenPeriodAttribute)
				{
					try
					{
						((AROpenPeriodAttribute)attr).IsValidPeriod(paymentsCache, paymentsCache.Current, finPeriod?.FinPeriodID);
					}
					catch (PXException e)
					{
						throw new PXException(Messages.CannotCaptureInInvalidPeriod, trandate.ToString("d", graph.Culture), e.MessageNoPrefix);
					}
					break;
				}
			}
		}

		public static void UpdateARPaymentAndSetWarning(IBqlTable aTable)
		{
			var toProc = (ARPayment) aTable;
			if (toProc.Released == false)
			{
				var paymentGraph = CreateInstance<ARPaymentEntry>();
			paymentGraph.Document.Current = PXSelect<ARPayment, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>,
					And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>
						.SelectWindowed(paymentGraph, 0, 1, toProc.DocType, toProc.RefNbr);
				var currTran = CCProcTranHelper.FindCCLastSuccessfulTran(paymentGraph.ccProcTran);
			if (currTran != null)
			{
				if (toProc.AdjDate != null && !(PXLongOperation.GetCustomInfo() is PXProcessingInfo))
				{
					PXLongOperation.SetCustomInfo(new DocDateWarningDisplay(toProc.AdjDate.Value));
				}
				toProc.DocDate = currTran.EndTime.Value.Date;
				toProc.AdjDate = currTran.EndTime.Value.Date;
					if (string.IsNullOrEmpty(toProc.ExtRefNbr))
				{
					toProc.ExtRefNbr = currTran.PCTranNumber;
				}
				paymentGraph.Document.Update(toProc);
				paymentGraph.Save.Press();
				paymentGraph.Document.Cache.RestoreCopy(aTable, paymentGraph.Document.Current);
			}
		}
		}

		public static void UpdateARPayment(IBqlTable aTable)
		{
			ARPayment toProc = (ARPayment)aTable;
			if (toProc.Released == false)
			{
				ARPaymentEntry paymentGraph = PXGraph.CreateInstance<ARPaymentEntry>();
			paymentGraph.Document.Current = PXSelect<ARPayment, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>,
					And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>
						.SelectWindowed(paymentGraph, 0, 1, toProc.DocType, toProc.RefNbr);
			CCProcTran currTran = CCProcTranHelper.FindCCLastSuccessfulTran(paymentGraph.ccProcTran);
			if (currTran != null)
			{
				toProc.DocDate = currTran.EndTime.Value.Date;
				toProc.AdjDate = currTran.EndTime.Value.Date;
				paymentGraph.Document.Update(toProc);
				paymentGraph.Save.Press();
				paymentGraph.Document.Cache.RestoreCopy(aTable, paymentGraph.Document.Current);
			}
		}
		}

		#region Credit Card Processing Buttons
		public PXAction<ARPayment> captureCCPayment;
		[PXUIField(DisplayName = "Capture CC Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable CaptureCCPayment(PXAdapter adapter)
		{
			var methodName = ClassName + ".CaptureCCPayment";
			PXTrace.WriteInformation($"{methodName} started.");
			List<ARPayment> list = new List<ARPayment>();
			foreach (ARPayment doc in adapter.Get<ARPayment>())
			{
				PXTrace.WriteInformation($"{methodName}. RefNbr:{doc.RefNbr}; CustomerID:{doc.CustomerID}; CuryOrigDocAmt:{doc.CuryOrigDocAmt}; UserName:{Accessinfo.UserName}");
				CheckValidPeriodForCCTran(this);
				list.Add(doc);
				CCPaymentEntry.CaptureCCPayment<ARPayment>(doc, ccProcTran, doc.Released == false && this.arsetup.Current.IntegratedCCProcessing == true, UpdateARPaymentAndSetWarning);
			}
			return list;
		}

		public PXAction<ARPayment> authorizeCCPayment;
		[PXUIField(DisplayName = "Authorize CC Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable AuthorizeCCPayment(PXAdapter adapter)
		{
			var methodName = ClassName + ".AuthorizeCCPayment";
			PXTrace.WriteInformation($"{methodName} started.");
			List<ARPayment> list = new List<ARPayment>();
			foreach (ARPayment doc in adapter.Get<ARPayment>())
			{
				PXTrace.WriteInformation($"{methodName}. RefNbr:{doc.RefNbr}; CustomerID:{doc.CustomerID}; CuryOrigDocAmt:{doc.CuryOrigDocAmt}; UserName:{Accessinfo.UserName}");
				list.Add(doc);
				CCPaymentEntry.AuthorizeCCPayment<ARPayment>(doc, ccProcTran);
			}
			return list;
		}

		public PXAction<ARPayment> voidCCPayment;
		[PXUIField(DisplayName = "Void CC Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable VoidCCPayment(PXAdapter adapter)
		{
			var methodName = ClassName + ".VoidCCPayment";
			PXTrace.WriteInformation($"{methodName} started.");
			List<ARPayment> list = new List<ARPayment>();
			foreach (ARPayment doc in adapter.Get<ARPayment>())
			{
				PXTrace.WriteInformation($"{methodName}. RefNbr:{doc.RefNbr}; CustomerID:{doc.CustomerID}; CuryOrigDocAmt:{doc.CuryOrigDocAmt}; UserName:{Accessinfo.UserName}");
				list.Add(doc);
				CCPaymentEntry.VoidCCPayment<ARPayment>(doc, ccProcTran, doc.VoidAppl == true && doc.Released == false && arsetup.Current.IntegratedCCProcessing == true, UpdateARPayment);
			}

			return list;
		}

		public PXAction<ARPayment> creditCCPayment;
		[PXUIField(DisplayName = "Refund CC Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable CreditCCPayment(PXAdapter adapter)
		{
			var methodName = ClassName + ".CreditCCPayment";
			PXTrace.WriteInformation($"{methodName} started.");
			List<ARPayment> list = new List<ARPayment>();
			foreach (ARPayment doc in adapter.Get<ARPayment>())
			{
				PXTrace.WriteInformation($"{methodName}. RefNbr:{doc.RefNbr}; CustomerID:{doc.CustomerID}; CuryOrigDocAmt:{doc.CuryOrigDocAmt}; UserName:{Accessinfo.UserName}");
				list.Add(doc);
				string PCRefTranNbr = doc.RefTranExtNbr;
				if (String.IsNullOrEmpty(doc.RefTranExtNbr))
				{
					this.Document.Cache.RaiseExceptionHandling<ARPayment.refTranExtNbr>(this.Document.Current, doc.RefTranExtNbr, new PXSetPropertyException(Messages.ERR_PCTransactionNumberOfTheOriginalPaymentIsRequired));
				}
				else
				{
					CheckValidPeriodForCCTran(this);
					CCPaymentEntry.CreditCCPayment<ARPayment>(doc, PCRefTranNbr, ccProcTran, doc.Released == false && this.arsetup.Current.IntegratedCCProcessing == true, UpdateARPaymentAndSetWarning);
				}
			}

			return list;
		}


		public PXAction<ARPayment> recordCCPayment;
		[PXUIField(DisplayName = "Record CC Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable RecordCCPayment(PXAdapter adapter)
		{
			var methodName = ClassName + ".RecordCCPayment";
			PXTrace.WriteInformation($"{methodName} started.");
			if (this.Document.Current != null &&
				this.Document.Current.Released == false &&
				this.Document.Current.IsCCPayment == true)
			{
				var dialogResult = this.Document.AskExt();
				if (dialogResult == WebDialogResult.OK || (IsContractBasedAPI && dialogResult == WebDialogResult.Yes))
				{
					CCPaymentInfo parameters = this.ccPaymentInfo.Current;
					bool failed = false;
					if (string.IsNullOrEmpty(parameters.PCTranNumber))
					{
						if (this.ccPaymentInfo.Cache.RaiseExceptionHandling<CCPaymentInfo.pCTranNumber>(parameters,
							null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(CCPaymentInfo.pCTranNumber).Name)))
							throw new PXRowPersistingException(typeof(CCPaymentInfo.pCTranNumber).Name, null, ErrorMessages.FieldIsEmpty, typeof(CCPaymentInfo.pCTranNumber).Name);
						failed = true;
					}

					if (string.IsNullOrEmpty(parameters.AuthNumber))
					{
						if (this.ccPaymentInfo.Cache.RaiseExceptionHandling<CCPaymentInfo.authNumber>(parameters,
							null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(CCPaymentInfo.authNumber).Name)))
							throw new PXRowPersistingException(typeof(CCPaymentInfo.authNumber).Name, null, ErrorMessages.FieldIsEmpty, typeof(CCPaymentInfo.authNumber).Name);
						failed = true;
					}
					if (failed)
						return adapter.Get();

					List<ARPayment> list = new List<ARPayment>();
					foreach (ARPayment doc in adapter.Get<ARPayment>())
					{
						PXTrace.WriteInformation($"{methodName}. RefNbr:{doc.RefNbr}; CustomerID:{doc.CustomerID}; CuryOrigDocAmt:{doc.CuryOrigDocAmt}; UserName:{Accessinfo.UserName}");
						list.Add(doc);
						if (doc.DocType == ARDocType.Payment || doc.DocType == ARDocType.Prepayment)
						{
							CCPaymentEntry.RecordCCPayment<ARPayment>(doc, parameters.PCTranNumber, parameters.AuthNumber, ccProcTran, (doc.Released == false && this.arsetup.Current.IntegratedCCProcessing == true));
						}
						else if (doc.DocType == ARDocType.Refund)
						{
							CCPaymentEntry.RecordCCCredit<ARPayment>(doc, doc.RefTranExtNbr, parameters.PCTranNumber, parameters.AuthNumber, ccProcTran, (doc.Released == false && this.arsetup.Current.IntegratedCCProcessing == true));
						}
					}
					return list;
				}
			}
			ccPaymentInfo.View.Clear();
			ccPaymentInfo.Cache.Clear();
			return adapter.Get();
		}

		public PXAction<ARPayment> captureOnlyCCPayment;
		[PXUIField(DisplayName = "Extern. Authorized CC Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		[ARMigrationModeDependentActionRestriction(
			restrictInMigrationMode: true,
			restrictForRegularDocumentInMigrationMode: true,
			restrictForUnreleasedMigratedDocumentInNormalMode: true)]
		public virtual IEnumerable CaptureOnlyCCPayment(PXAdapter adapter)
		{
			var methodName = ClassName + ".CaptureOnlyCCPayment";
			PXTrace.WriteInformation($"{methodName} started.");
			if (this.Document.Current != null &&
					this.Document.Current.Released == false &&
					this.Document.Current.IsCCPayment == true
					&& this.ccPaymentInfo.AskExt(initAuthCCInfo) == WebDialogResult.OK)
			{
				CCPaymentInfo parameters = this.ccPaymentInfo.Current;
				if (string.IsNullOrEmpty(parameters.AuthNumber))
				{
					if (this.ccPaymentInfo.Cache.RaiseExceptionHandling<CCPaymentInfo.authNumber>(parameters,
						null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(CCPaymentInfo.authNumber).Name)))
						throw new PXRowPersistingException(typeof(CCPaymentInfo.authNumber).Name, null, ErrorMessages.FieldIsEmpty, typeof(CCPaymentInfo.authNumber).Name);
					return adapter.Get();
				}

				List<ARPayment> list = new List<ARPayment>();
				foreach (ARPayment doc in adapter.Get<ARPayment>())
				{
					PXTrace.WriteInformation($"{methodName}. RefNbr:{doc.RefNbr}; CustomerID:{doc.CustomerID}; CuryOrigDocAmt:{doc.CuryOrigDocAmt}; UserName:{Accessinfo.UserName}");
					list.Add(doc);
					CCPaymentEntry.CaptureOnlyCCPayment<ARPayment>(doc, parameters.AuthNumber, ccProcTran, (doc.Released == false && this.arsetup.Current.IntegratedCCProcessing == true));
				}
				return list;
			}
			ccPaymentInfo.View.Clear();
			ccPaymentInfo.Cache.Clear();
			return adapter.Get();
		}

		public virtual void clearCCInfo(PXGraph aGraph, string ViewName)
		{
			CCPaymentInfo filter = this.ccPaymentInfo.Current;
			filter.PCTranNumber = filter.AuthNumber = null;
		}

		public virtual void initAuthCCInfo(PXGraph aGraph, string ViewName)
		{
			CCPaymentInfo filter = this.ccPaymentInfo.Current;
			filter.PCTranNumber = filter.AuthNumber = null;
			PXUIFieldAttribute.SetVisible<CCPaymentInfo.pCTranNumber>(this.ccPaymentInfo.Cache, filter, false);
		}
		#endregion


		#endregion

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXRemoveBaseAttribute(typeof(ARPaymentType.ListExAttribute))]
		[ARPaymentType.List]
		protected virtual void ARPayment_DocType_CacheAttached(PXCache sender)
		{

		}

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

		protected virtual void ARPayment_DocType_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			e.NewValue = ARDocType.Payment;
		}

		protected virtual IEnumerable adjustments()
		{
			var ret = new PXResultset<ARAdjust, ARInvoice, Standalone.ARRegisterAlias>();

			int startRow = PXView.StartRow;
			int totalRows = 0;

			if (Document.Current == null || Document.Current.DocType != ARDocType.Refund)
			{
				foreach (PXResult<ARAdjust, ARInvoice, Standalone.ARRegisterAlias> res in
					Adjustments_Invoices.View.Select(
						PXView.Currents,
						null,
						PXView.Searches,
						PXView.SortColumns,
						PXView.Descendings,
						PXView.Filters,
						ref startRow,
						PXView.MaximumRows,
						ref totalRows))
				{
					ARInvoice fullInvoice = res;
					PXCache<ARRegister>.RestoreCopy(fullInvoice, (Standalone.ARRegisterAlias)res);

					if (Adjustments.Cache.GetStatus((ARAdjust)res) == PXEntryStatus.Notchanged)
					{
						CalcBalances<ARInvoice>((ARAdjust)res, fullInvoice, true, false);
					}

					ret.Add(new PXResult<ARAdjust, ARInvoice, Standalone.ARRegisterAlias>(res, fullInvoice, res));
				}
			}
			else
			{
				foreach (PXResult<ARAdjust, ARPayment, Standalone.ARRegisterAlias> res in
					Adjustments_Payments.View.Select(
						PXView.Currents,
						null,
						PXView.Searches,
						PXView.SortColumns,
						PXView.Descendings,
						PXView.Filters,
						ref startRow,
						PXView.MaximumRows,
						ref totalRows))
				{
					ARPayment fullPayment = res;
					PXCache<ARRegister>.RestoreCopy(fullPayment, (Standalone.ARRegisterAlias)res);

					if (Adjustments.Cache.GetStatus((ARAdjust)res) == PXEntryStatus.Notchanged)
					{
						CalcBalances<ARPayment>((ARAdjust)res, fullPayment, true, false);
					}

					ret.Add(new PXResult<ARAdjust, ARPayment, Standalone.ARRegisterAlias>(res, fullPayment, res));
				}
			}
			PXView.StartRow = 0;

			return ret;
		}

		protected virtual IEnumerable adjustments_history()
		{
			PXResultset<ARAdjust> resultSet = new PXResultset<ARAdjust>();

			BqlCommand outgoingApplications = new Select2<
				ARAdjust,
					LeftJoinSingleTable<ARInvoice,
						On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>,
						And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>,
					LeftJoin<ARRegisterAlias,
						On<ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>,
						And<ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>>>,
				Where<
					ARAdjust.adjgDocType, Equal<Optional<ARPayment.docType>>,
					And<ARAdjust.adjgRefNbr, Equal<Optional<ARPayment.refNbr>>,
					And<ARAdjust.adjNbr, Less<Optional<ARPayment.adjCntr>>,
					And<ARAdjust.isInitialApplication, NotEqual<True>>>>>>();

			outgoingApplications.EnsureParametersEqual(Adjustments_History.View.BqlSelect);

			PXView outgoingApplicationsView = new PXView(this, false, outgoingApplications);

			foreach (PXResult<ARAdjust, ARInvoice, ARRegisterAlias> result in outgoingApplicationsView.SelectMulti(PXView.Parameters))
			{
				ARInvoice fullInvoice = result;
				PXCache<ARRegister>.RestoreCopy(fullInvoice, (ARRegisterAlias)result);

				resultSet.Add(new PXResult<ARAdjust, ARInvoice>(result, fullInvoice));
			}

			BqlCommand incomingApplications = new Select2<
				ARAdjust,
					InnerJoinSingleTable<ARPayment,
						On<ARPayment.docType, Equal<ARAdjust.adjgDocType>,
						And<ARPayment.refNbr, Equal<ARAdjust.adjgRefNbr>>>,
					InnerJoin<ARRegisterAlias,
						On<ARRegisterAlias.docType, Equal<ARAdjust.adjgDocType>,
						And<ARRegisterAlias.refNbr, Equal<ARAdjust.adjgRefNbr>,
						And<ARRegisterAlias.adjCntr, Greater<ARAdjust.adjNbr>>>>,
					InnerJoin<CurrencyInfo,
						On<ARRegisterAlias.curyInfoID, Equal<CurrencyInfo.curyInfoID>>>>>,
				Where<
					ARAdjust.adjdDocType, Equal<Optional<ARPayment.docType>>,
					And<ARAdjust.adjdRefNbr, Equal<Optional<ARPayment.refNbr>>>>>();

			PXView incomingApplicationsView = new PXView(this, false, incomingApplications);

			foreach (PXResult<ARAdjust, ARPayment, ARRegisterAlias, CurrencyInfo> result in incomingApplicationsView.SelectMulti(PXView.Parameters))
			{
				ARAdjust incomingApplication = result;
				ARPayment appliedPayment = result;
				CurrencyInfo paymentCurrencyInfo = result;

				PXCache<ARRegister>.RestoreCopy(appliedPayment, (ARRegisterAlias)result);

				BalanceCalculation.CalculateApplicationDocumentBalance(
					this.Caches<ARAdjust>(),
					appliedPayment,
					incomingApplication,
					paymentCurrencyInfo,
					currencyinfo.Select());

				resultSet.Add(new PXResult<ARAdjust, ARPayment>(incomingApplication, appliedPayment));
			}

			return resultSet;
		}

		protected virtual IEnumerable soadjustments()
		{
			PXResultset<SOAdjust, SOOrder> ret = new PXResultset<SOAdjust, SOOrder>();

			int startRow = PXView.StartRow;
			int totalRows = 0;

			if (Document.Current == null || Document.Current.DocType != ARDocType.Refund)
			{
				foreach (PXResult<SOAdjust, SOOrder> res in SOAdjustments_Orders.View.Select(PXView.Currents, null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows))
				{
					if (SOAdjustments.Cache.GetStatus((SOAdjust)res) == PXEntryStatus.Notchanged)
					{
						SOOrder invoice = PXCache<SOOrder>.CreateCopy(res);
						SOAdjust adj = res;

						SOAdjust other = PXSelectGroupBy<SOAdjust,
							Where<SOAdjust.voided, Equal<False>,
							And<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>,
							And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>,
							And<Where<SOAdjust.adjgDocType, NotEqual<Required<SOAdjust.adjgDocType>>, Or<SOAdjust.adjgRefNbr, NotEqual<Required<SOAdjust.adjgRefNbr>>>>>>>>,
							Aggregate<GroupBy<SOAdjust.adjdOrderType,
							GroupBy<SOAdjust.adjdOrderNbr, Sum<SOAdjust.curyAdjdAmt, Sum<SOAdjust.adjAmt>>>>>>.Select(this, adj.AdjdOrderType, adj.AdjdOrderNbr, adj.AdjgDocType, adj.AdjgRefNbr);
						if (other != null && other.AdjdOrderNbr != null)
						{
							invoice.CuryDocBal -= other.CuryAdjdAmt;
							invoice.DocBal -= other.AdjAmt;
						}

						CalcBalances<SOOrder>(adj, invoice, true, false);
					}
					ret.Add(res);
				}
			}

			PXView.StartRow = 0;

			return ret;
		}

		protected void CalcBalances<T>(SOAdjust adj, T invoice, bool isCalcRGOL)
			where T : class, IBqlTable, IInvoice
		{
			CalcBalances<T>(adj, invoice, isCalcRGOL, true);
		}

		protected void CalcBalances<T>(SOAdjust adj, T invoice, bool isCalcRGOL, bool DiscOnDiscDate)
			where T : class, IBqlTable, IInvoice
		{
			PaymentEntry.CalcBalances<T, SOAdjust>(CurrencyInfo_CuryInfoID, adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, invoice, adj);
			if (DiscOnDiscDate)
			{
				PaymentEntry.CalcDiscount<T, SOAdjust>(adj.AdjgDocDate, invoice, adj);
			}
			PaymentEntry.WarnDiscount<T, SOAdjust>(this, adj.AdjgDocDate, invoice, adj);

			PaymentEntry.AdjustBalance<SOAdjust>(CurrencyInfo_CuryInfoID, adj);
			if (isCalcRGOL && (adj.Voided != true))
			{
				PaymentEntry.CalcRGOL<T, SOAdjust>(CurrencyInfo_CuryInfoID, invoice, adj);
				adj.RGOLAmt = (bool)adj.ReverseGainLoss ? -adj.RGOLAmt : adj.RGOLAmt;
			}
		}
		protected void CalcBalances(SOAdjust row, bool isCalcRGOL)
		{
			CalcBalances(row, isCalcRGOL, true);
		}

		protected void CalcBalances(SOAdjust adj, bool isCalcRGOL, bool DiscOnDiscDate)
		{
			foreach (PXResult<SOOrder> res in SOOrder_CustomerID_OrderType_RefNbr.Select(adj.CustomerID, adj.AdjdOrderType, adj.AdjdOrderNbr))
			{
				SOOrder invoice = PXCache<SOOrder>.CreateCopy(res);

				internalCall = true;

				SOAdjust other = PXSelectGroupBy<SOAdjust,
				Where<SOAdjust.voided, Equal<False>,
				And<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>,
				And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>,
				And<Where<SOAdjust.adjgDocType, NotEqual<Required<SOAdjust.adjgDocType>>, Or<SOAdjust.adjgRefNbr, NotEqual<Required<SOAdjust.adjgRefNbr>>>>>>>>,
				Aggregate<GroupBy<SOAdjust.adjdOrderType,
				GroupBy<SOAdjust.adjdOrderNbr, Sum<SOAdjust.curyAdjdAmt, Sum<SOAdjust.adjAmt>>>>>>.Select(this, adj.AdjdOrderType, adj.AdjdOrderNbr, adj.AdjgDocType, adj.AdjgRefNbr);

				if (other != null && other.AdjdOrderNbr != null)
				{
					invoice.CuryDocBal -= other.CuryAdjdAmt;
					invoice.DocBal -= other.AdjAmt;
				}
				internalCall = false;

				CalcBalances<SOOrder>(adj, invoice, isCalcRGOL, DiscOnDiscDate);
				return;
			}
		}


		protected virtual void ParentFieldUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			if (!sender.ObjectsEqual<ARPayment.adjDate, ARPayment.adjFinPeriodID, ARPayment.curyID, ARPayment.branchID>(e.Row, e.OldRow))
			{
				foreach (ARAdjust adj in Adjustments_Raw.Select())
				{
					adj.AdjgBranchID = ((ARPayment)e.Row).BranchID;
					Adjustments.Cache.SmartSetStatus(adj, PXEntryStatus.Updated);
				}
			}
		}

		public ARPaymentEntry()
			: base()
		{
			LoadOptions opt = loadOpts.Current;
			ARSetup setup = arsetup.Current;
			OpenPeriodAttribute.SetValidatePeriod<ARPayment.adjFinPeriodID>(Document.Cache, null, PeriodValidation.DefaultSelectUpdate);

			RowUpdated.AddHandler<ARPayment>(ParentFieldUpdated);

			setup.CreditCheckError = false;
		}

		public override int ExecuteUpdate(string viewName, IDictionary keys, IDictionary values, params object[] parameters)
		{
			if (viewName.Equals(nameof(Document), StringComparison.OrdinalIgnoreCase)
				&& values != null)
			{
				values[nameof(ARPayment.CurySOApplAmt)] = PXCache.NotSetValue;
				values[nameof(ARPayment.CuryApplAmt)] = PXCache.NotSetValue;
				values[nameof(ARPayment.CuryWOAmt)] = PXCache.NotSetValue;
				values[nameof(ARPayment.CuryUnappliedBal)] = PXCache.NotSetValue;
			}

			return base.ExecuteUpdate(viewName, keys, values, parameters);
		}

		public override void Persist()
		{
			foreach (ARPayment doc in Document.Cache.Updated)
			{
				if (doc.SelfVoidingDoc == true)
				{
					int countAdjVoided = Adjustments_Raw.Select(doc.DocType, doc.RefNbr, doc.AdjCntr).Count(adj => ((ARAdjust)adj).Voided == true);
					int countAdjHistory = Adjustments_History.Select(doc.DocType, doc.RefNbr, doc.AdjCntr).Count();
					if (countAdjVoided > 0 && countAdjVoided != countAdjHistory)
					{
						throw new PXException(Messages.SelfVoidingDocPartialReverse);
					}
				}

				if (doc.OpenDoc == true && (bool?)Document.Cache.GetValueOriginal<ARPayment.openDoc>(doc) == false && Adjustments_Raw.SelectSingle(doc.DocType, doc.RefNbr, doc.AdjCntr) == null)
				{
					doc.OpenDoc = false;
					Document.Cache.RaiseRowSelected(doc);
				}
			}
			base.Persist();
		}

		#region SOAdjust Events
		protected virtual void SOAdjust_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			SOAdjust adj = (SOAdjust)e.Row;
			if (adj == null)
			{
				return;
			}

			bool adjNotReleased = (adj.Released != true);

			PXUIFieldAttribute.SetEnabled<SOAdjust.adjdOrderType>(cache, adj, adjNotReleased);
			PXUIFieldAttribute.SetEnabled<SOAdjust.adjdOrderNbr>(cache, adj, adjNotReleased);
			PXUIFieldAttribute.SetEnabled<SOAdjust.curyAdjgAmt>(cache, adj, adjNotReleased);
		}

		protected virtual void SOAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
			string errmsg = PXUIFieldAttribute.GetError<SOAdjust.adjdOrderNbr>(sender, e.Row);

			e.Cancel = (((SOAdjust)e.Row).AdjdOrderNbr == null || string.IsNullOrEmpty(errmsg) == false);
		}

		protected virtual void SOAdjust_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
		{
			if (((SOAdjust)e.Row).CuryAdjdBilledAmt > 0m)
			{
				throw new PXSetPropertyException(ErrorMessages.CantDeleteRecord);
			}
		}

		protected virtual void SOAdjust_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			if (((SOAdjust)e.Row).AdjdCuryInfoID != ((SOAdjust)e.Row).AdjgCuryInfoID && ((SOAdjust)e.Row).AdjdCuryInfoID != ((SOAdjust)e.Row).AdjdOrigCuryInfoID)
			{
				foreach (CurrencyInfo info in CurrencyInfo_CuryInfoID.Select(((SOAdjust)e.Row).AdjdCuryInfoID))
				{
					currencyinfo.Delete(info);
				}
			}
		}

		protected virtual void SOAdjust_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			SOAdjust doc = (SOAdjust)e.Row;

			if ((e.Operation & PXDBOperation.Command) == PXDBOperation.Delete)
			{
				return;
			}

			if (((DateTime)doc.AdjdOrderDate).CompareTo((DateTime)Document.Current.AdjDate) > 0)
			{
				if (sender.RaiseExceptionHandling<SOAdjust.adjdOrderNbr>(e.Row, doc.AdjdOrderNbr, new PXSetPropertyException(AR.Messages.ApplDate_Less_DocDate, PXErrorLevel.RowError, PXUIFieldAttribute.GetDisplayName<ARPayment.adjDate>(Document.Cache))))
				{
					throw new PXRowPersistingException(PXDataUtils.FieldName<SOAdjust.adjdOrderDate>(), doc.AdjdOrderDate, AR.Messages.ApplDate_Less_DocDate, PXUIFieldAttribute.GetDisplayName<ARPayment.adjDate>(Document.Cache));
				}
			}
			/*
			if (((string)doc.AdjdFinPeriodID).CompareTo((string)Document.Current.AdjFinPeriodID) > 0)
			{
				if (sender.RaiseExceptionHandling<SOAdjust.adjdOrderNbr>(e.Row, doc.AdjdOrderNbr, new PXSetPropertyException(AR.Messages.ApplPeriod_Less_DocPeriod, PXErrorLevel.RowError, PXUIFieldAttribute.GetDisplayName<ARPayment.adjFinPeriodID>(Document.Cache))))
				{
					throw new PXRowPersistingException(PXDataUtils.FieldName<SOAdjust.adjdFinPeriodID>(), doc.AdjdFinPeriodID, AR.Messages.ApplPeriod_Less_DocPeriod, PXUIFieldAttribute.GetDisplayName<ARPayment.adjFinPeriodID>(Document.Cache));
				}
			}
			*/
			if (doc.CuryDocBal < 0m)
			{
				sender.RaiseExceptionHandling<SOAdjust.curyAdjgAmt>(e.Row, doc.CuryAdjgAmt, new PXSetPropertyException(AR.Messages.DocumentBalanceNegative));
			}


			/*
			if (Document.Current.DocType == ARDocType.Prepayment)
			{
				if (((DateTime)doc.AdjdOrderDate).CompareTo((DateTime)Document.Current.DocDate) < 0)
				{
					if (sender.RaiseExceptionHandling<SOAdjust.adjdOrderNbr>(e.Row, doc.AdjdOrderNbr, new PXSetPropertyException(AR.Messages.ApplDate_Less_DocDate, PXErrorLevel.RowError, PXUIFieldAttribute.GetDisplayName<ARPayment.docDate>(Document.Cache))))
					{
						throw new PXRowPersistingException(PXDataUtils.FieldName<SOAdjust.adjdDocDate>(), doc.AdjdOrderDate, AR.Messages.ApplDate_Less_DocDate, PXUIFieldAttribute.GetDisplayName<ARPayment.docDate>(Document.Cache));
					}
				}

				if (((string)doc.AdjdFinPeriodID).CompareTo((string)Document.Current.FinPeriodID) < 0)
				{
					if (sender.RaiseExceptionHandling<SOAdjust.adjdOrderNbr>(e.Row, doc.AdjdOrderNbr, new PXSetPropertyException(AR.Messages.ApplPeriod_Greater_DocPeriod, PXErrorLevel.RowError, PXUIFieldAttribute.GetDisplayName<ARPayment.finPeriodID>(Document.Cache))))
					{
						throw new PXRowPersistingException(PXDataUtils.FieldName<SOAdjust.adjdFinPeriodID>(), doc.AdjdFinPeriodID, AR.Messages.ApplPeriod_Greater_DocPeriod, PXUIFieldAttribute.GetDisplayName<ARPayment.finPeriodID>(Document.Cache));
					}
				}
			}
			*/
		}

		protected virtual void SOAdjust_AdjdOrderNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			SOAdjust adj = (SOAdjust)e.Row;

			if (adj != null)
			{
				if (e.NewValue != null)
				{
					var ord = PXSelectorAttribute.Select<SOAdjust.adjdOrderNbr>(sender, adj, e.NewValue);
					if (ord == null)
					{
						throw new PXSetPropertyException<SOAdjust.adjdOrderNbr>(Messages.WrongOrderNbr, PXErrorLevel.Error);
					}
				}

				if (e.ExternalCall)
				{
					if (PXSelectJoin<SOOrder,
							 InnerJoin<Terms, On<Terms.termsID, Equal<SOOrder.termsID>>>,
							 Where<SOOrder.orderType, Equal<Required<SOAdjust.adjdOrderType>>,
							 And<SOOrder.orderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>,
							 And<Terms.installmentType, NotEqual<TermsInstallmentType.single>>>>>.Select(this, adj.AdjdOrderType, e.NewValue).Count() > 0)
					{
						throw new PXSetPropertyException(Messages.PrepaymentAppliedToMultiplyInstallments);
					}
				}

				e.Cancel = (Document.Current != null && Document.Current.VoidAppl == true || this._AutoPaymentApp);
			}
		}

		protected virtual void SOAdjust_AdjdOrderNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			try
			{
				SOAdjust adj = (SOAdjust)e.Row;
				if (adj.AdjdCuryInfoID == null)
				{
					foreach (PXResult<SOOrder, CurrencyInfo> res in PXSelectJoin<SOOrder, InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<SOOrder.curyInfoID>>>, Where<SOOrder.orderType, Equal<Required<SOOrder.orderType>>, And<SOOrder.orderNbr, Equal<Required<SOOrder.orderNbr>>>>>.Select(this, adj.AdjdOrderType, adj.AdjdOrderNbr))
					{
						SOAdjust_AdjdOrderNbr_FieldUpdated<SOOrder>(res, adj);
						return;
					}
				}
			}
			catch (PXSetPropertyException ex)
			{
				throw new PXException(ex.Message);
			}
		}

		protected virtual void SOAdjust_AdjdOrderType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			SOAdjust adj = (SOAdjust)e.Row;
			if (adj != null && (string)e.OldValue != adj.AdjdOrderType)
			{
				var value = sender.GetValue<SOAdjust.adjdOrderNbr>(adj);

				try
				{
					sender.RaiseFieldVerifying<SOAdjust.adjdOrderNbr>(adj, ref value);
				}
				catch(PXSetPropertyException<SOAdjust.adjdOrderNbr>)
				{
					sender.SetValue<SOAdjust.adjdOrderNbr>(adj, null);
				}
			}
		}

		private void SOAdjust_AdjdOrderNbr_FieldUpdated<T>(PXResult<T, CurrencyInfo> res, SOAdjust adj)
			where T : SOOrder, IInvoice, new()
		{
			CurrencyInfo info_copy = PXCache<CurrencyInfo>.CreateCopy((CurrencyInfo)res);
			info_copy.CuryInfoID = null;
			info_copy = (CurrencyInfo)currencyinfo.Cache.Insert(info_copy);
			T invoice = PXCache<T>.CreateCopy((T)res);

			//currencyinfo.Cache.SetValueExt<CurrencyInfo.curyEffDate>(info_copy, Document.Current.DocDate);
			info_copy.SetCuryEffDate(currencyinfo.Cache, Document.Current.DocDate);

			adj.CustomerID = Document.Current.CustomerID;
			adj.AdjgDocDate = Document.Current.AdjDate;
			adj.AdjgCuryInfoID = Document.Current.CuryInfoID;
			adj.AdjdCuryInfoID = info_copy.CuryInfoID;
			adj.AdjdOrigCuryInfoID = invoice.CuryInfoID;
			adj.AdjdOrderDate = invoice.OrderDate > Document.Current.AdjDate
				? Document.Current.AdjDate
				: invoice.OrderDate;
			adj.Released = false;

			SOAdjust other = PXSelectGroupBy<SOAdjust,
				Where<SOAdjust.voided, Equal<False>,
				And<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>,
				And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>,
				And<Where<SOAdjust.adjgDocType, NotEqual<Required<SOAdjust.adjgDocType>>, Or<SOAdjust.adjgRefNbr, NotEqual<Required<SOAdjust.adjgRefNbr>>>>>>>>,
				Aggregate<GroupBy<SOAdjust.adjdOrderType,
				GroupBy<SOAdjust.adjdOrderNbr, Sum<SOAdjust.curyAdjdAmt, Sum<SOAdjust.adjAmt>>>>>>.Select(this, adj.AdjdOrderType, adj.AdjdOrderNbr, adj.AdjgDocType, adj.AdjgRefNbr);
			if (other != null && other.AdjdOrderNbr != null)
			{
				invoice.CuryDocBal -= other.CuryAdjdAmt;
				invoice.DocBal -= other.AdjAmt;
			}

			CalcBalances<T>(adj, invoice, false);

			decimal? CuryApplAmt = adj.CuryDocBal - adj.CuryDiscBal;
			decimal? CuryApplDiscAmt = adj.CuryDiscBal;
			decimal? CuryUnappliedBal = Document.Current.CuryUnappliedBal;

			if (adj.CuryDiscBal >= 0m && adj.CuryDocBal - adj.CuryDiscBal <= 0m)
			{
				//no amount suggestion is possible
				return;
			}

			if (Document.Current != null && string.IsNullOrEmpty(Document.Current.DocDesc))
			{
				Document.Current.DocDesc = invoice.OrderDesc;
			}
			if (Document.Current != null && CuryUnappliedBal > 0m)
			{
				CuryApplAmt = Math.Min((decimal)CuryApplAmt, (decimal)CuryUnappliedBal);

				if (CuryApplAmt + CuryApplDiscAmt < adj.CuryDocBal)
				{
					CuryApplDiscAmt = 0m;
				}
			}
			else if (Document.Current != null && CuryUnappliedBal <= 0m && ((ARPayment)Document.Current).CuryOrigDocAmt > 0)
			{
				CuryApplAmt = 0m;
				CuryApplDiscAmt = 0m;
			}

			SOAdjustments.Cache.SetValue<SOAdjust.curyAdjgAmt>(adj, 0m);
			SOAdjustments.Cache.SetValue<SOAdjust.curyAdjgDiscAmt>(adj, 0m);
			SOAdjustments.Cache.SetValueExt<SOAdjust.curyAdjgAmt>(adj, CuryApplAmt);
			SOAdjustments.Cache.SetValueExt<SOAdjust.curyAdjgDiscAmt>(adj, CuryApplDiscAmt);

			CalcBalances<T>(adj, invoice, true);
		}


		protected virtual void SOAdjust_CuryDocBal_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			if (!internalCall)
			{
				if (e.Row != null && ((SOAdjust)e.Row).AdjdCuryInfoID != null && ((SOAdjust)e.Row).CuryDocBal == null && sender.GetStatus(e.Row) != PXEntryStatus.Deleted)
				{
					CalcBalances((SOAdjust)e.Row, false, false);
				}
				if (e.Row != null)
				{
					e.NewValue = ((SOAdjust)e.Row).CuryDocBal;
				}
			}
			e.Cancel = true;
		}

		protected virtual void SOAdjust_CuryDiscBal_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			if (!internalCall)
			{
				if (e.Row != null && ((SOAdjust)e.Row).AdjdCuryInfoID != null && ((SOAdjust)e.Row).CuryDiscBal == null && sender.GetStatus(e.Row) != PXEntryStatus.Deleted)
				{
					CalcBalances((SOAdjust)e.Row, false, false);
				}
				if (e.Row != null)
				{
					e.NewValue = ((SOAdjust)e.Row).CuryDiscBal;
				}
			}
			e.Cancel = true;
		}
		protected virtual void SOAdjust_CuryAdjgAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			SOAdjust adj = (SOAdjust)e.Row;

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null)
			{
				CalcBalances((SOAdjust)e.Row, false, false);
			}

			if (adj.CuryDocBal == null)
			{
				throw new PXSetPropertyException<SOAdjust.adjdOrderNbr>(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<SOAdjust.adjdOrderNbr>(sender));
			}

			if ((decimal)e.NewValue < 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_GE, ((int)0).ToString());
			}


			if ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgAmt - (decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgAmt).ToString());
			}
		}

		protected virtual void SOAdjust_CuryAdjgAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CalcBalances((SOAdjust)e.Row, true, false);
		}

		protected virtual void SOAdjust_CuryAdjgDiscAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			SOAdjust adj = (SOAdjust)e.Row;

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null)
			{
				CalcBalances((SOAdjust)e.Row, false, false);
			}

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null)
			{
				throw new PXSetPropertyException<SOAdjust.adjdOrderNbr>(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<SOAdjust.adjdOrderNbr>(sender));
			}

			if ((decimal)e.NewValue < 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_GE, ((int)0).ToString());
			}

			if ((decimal)adj.CuryDiscBal + (decimal)adj.CuryAdjgDiscAmt - (decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_LE, ((decimal)adj.CuryDiscBal + (decimal)adj.CuryAdjgDiscAmt).ToString());
			}

			if (adj.CuryAdjgAmt != null && (sender.GetValuePending<SOAdjust.curyAdjgAmt>(e.Row) == PXCache.NotSetValue || (Decimal?)sender.GetValuePending<SOAdjust.curyAdjgAmt>(e.Row) == adj.CuryAdjgAmt))
			{
				if ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgDiscAmt - (decimal)e.NewValue < 0)
				{
					throw new PXSetPropertyException(CS.Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgDiscAmt).ToString());
				}
			}
		}

		protected virtual void SOAdjust_CuryAdjgDiscAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CalcBalances((SOAdjust)e.Row, true, false);
		}

		#endregion


		protected virtual void CurrencyInfo_CuryID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
			{
				if (cashaccount.Current != null && !string.IsNullOrEmpty(cashaccount.Current.CuryID))
				{
					e.NewValue = cashaccount.Current.CuryID;
					e.Cancel = true;
				}
			}
		}

		protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
			{
				if (cashaccount.Current != null && !string.IsNullOrEmpty(cashaccount.Current.CuryRateTypeID))
				{
					e.NewValue = cashaccount.Current.CuryRateTypeID;
					e.Cancel = true;
				}
			}
		}

		protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Document.Cache.Current != null)
			{
				e.NewValue = ((ARPayment)Document.Cache.Current).DocDate;
				e.Cancel = true;
			}
		}
		#region ARPayment Events

		protected virtual void ARPayment_CustomerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			customer.RaiseFieldUpdated(sender, e.Row);

			{
				sender.SetDefaultExt<ARInvoice.customerLocationID>(e.Row);
				sender.SetDefaultExt<ARPayment.paymentMethodID>(e.Row);
			}
		}

		protected virtual void ARPayment_CustomerLocationID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			location.RaiseFieldUpdated(sender, e.Row);

			sender.SetDefaultExt<ARPayment.aRAccountID>(e.Row);
			sender.SetDefaultExt<ARPayment.aRSubID>(e.Row);
			sender.SetDefaultExt<ARPayment.branchID>(e.Row);
		}

		protected virtual void ARPayment_Hold_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			ARPayment doc = e.Row as ARPayment;
			if (PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
			{
				if (this.Approval.GetAssignedMaps(doc, sender).Any())
				{
					sender.SetValue<ARPayment.hold>(doc, true);
				}
			}
		}
		protected virtual void ARPayment_ExtRefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			ARPayment row = (ARPayment)e.Row;
			if (e.Row != null && ((ARPayment)e.Row).DocType == ARDocType.VoidPayment)
			{
				//avoid webdialog in PaymentRef attribute
				e.Cancel = true;
			}
			else
			{
				if (string.IsNullOrEmpty((string)e.NewValue) == false && string.IsNullOrEmpty(row.PaymentMethodID) == false)
				{
					PaymentMethod pm = this.paymentmethod.Current;
					ARPayment dup = null;
					if (pm.IsAccountNumberRequired == true)
					{
						dup = PXSelectReadonly<ARPayment, Where<ARPayment.customerID, Equal<Current<ARPayment.customerID>>,
											And<ARPayment.pMInstanceID, Equal<Current<ARPayment.pMInstanceID>>,
											And<ARPayment.extRefNbr, Equal<Required<ARPayment.extRefNbr>>,
											And<ARPayment.voided, Equal<False>,
											And<Where<ARPayment.docType, NotEqual<Current<ARPayment.docType>>,
											Or<ARPayment.refNbr, NotEqual<Current<ARPayment.refNbr>>>>>>>>>>.Select(this, e.NewValue);
					}
					else
					{
						dup = PXSelectReadonly<ARPayment, Where<ARPayment.customerID, Equal<Current<ARPayment.customerID>>,
											And<ARPayment.paymentMethodID, Equal<Current<ARPayment.paymentMethodID>>,
											And<ARPayment.extRefNbr, Equal<Required<ARPayment.extRefNbr>>,
											And<ARPayment.voided, Equal<False>,
											And<Where<ARPayment.docType, NotEqual<Current<ARPayment.docType>>,
										 Or<ARPayment.refNbr, NotEqual<Current<ARPayment.refNbr>>>>>>>>>>.Select(this, e.NewValue);
					}
					if (dup != null)
					{
						sender.RaiseExceptionHandling<ARPayment.extRefNbr>(e.Row, e.NewValue, new PXSetPropertyException(Messages.DuplicateCustomerPayment, PXErrorLevel.Warning, dup.ExtRefNbr, dup.DocDate, dup.DocType, dup.RefNbr));
					}
				}
			}
		}



		private object GetAcctSub<Field>(PXCache cache, object data)
			where Field : IBqlField
		{
			object NewValue = cache.GetValueExt<Field>(data);
			if (NewValue is PXFieldState)
			{
				return ((PXFieldState)NewValue).Value;
			}
			else
			{
				return NewValue;
			}
		}

		protected virtual void ARPayment_ARAccountID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (customer.Current != null && e.Row != null)
			{
				if (((ARPayment)e.Row).DocType == ARDocType.Prepayment)
				{
					e.NewValue = GetAcctSub<Customer.prepaymentAcctID>(customer.Cache, customer.Current);
				}
				if (string.IsNullOrEmpty((string)e.NewValue))
				{
					e.NewValue = GetAcctSub<CR.Location.aRAccountID>(location.Cache, location.Current);
				}
			}
		}

		protected virtual void ARPayment_ARSubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (customer.Current != null && e.Row != null)
			{
				if (((ARPayment)e.Row).DocType == ARDocType.Prepayment)
				{
					e.NewValue = GetAcctSub<Customer.prepaymentSubID>(customer.Cache, customer.Current);
				}
				if (string.IsNullOrEmpty((string)e.NewValue))
				{
					e.NewValue = GetAcctSub<Location.aRSubID>(location.Cache, location.Current);
				}
			}
		}

		protected virtual void ARPayment_PaymentMethodID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<ARPayment.pMInstanceID>(e.Row);
			sender.SetDefaultExt<ARPayment.cashAccountID>(e.Row);
		}

		protected virtual void ARPayment_PMInstanceID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<ARPayment.refTranExtNbr>(e.Row);
			sender.SetValuePending<ARPayment.refTranExtNbr>(e.Row, String.Empty);
			sender.SetDefaultExt<ARPayment.cashAccountID>(e.Row);
		}

		protected virtual void ARPayment_CashAccountID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			ARPayment payment = (ARPayment)e.Row;
			if (this._IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}
		}

		protected virtual void ARPayment_CashAccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARPayment payment = (ARPayment)e.Row;
			if (cashaccount.Current == null || cashaccount.Current.CashAccountID != payment.CashAccountID)
			{
				cashaccount.Current = (CashAccount)PXSelectorAttribute.Select<ARPayment.cashAccountID>(sender, e.Row);
			}

			if (_IsVoidCheckInProgress == false && PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
			{
				CurrencyInfo info = CurrencyInfoAttribute.SetDefaults<ARPayment.curyInfoID>(sender, e.Row);

				string message = PXUIFieldAttribute.GetError<CurrencyInfo.curyEffDate>(currencyinfo.Cache, info);
				if (string.IsNullOrEmpty(message) == false)
				{
					sender.RaiseExceptionHandling<ARPayment.adjDate>(e.Row, payment.DocDate, new PXSetPropertyException(message, PXErrorLevel.Warning));
				}

				if (info != null)
				{
					payment.CuryID = info.CuryID;
				}
			}

			//sender.SetDefaultExt<ARPayment.branchID>(e.Row);
			payment.Cleared = false;
			payment.ClearDate = null;
			sender.SetDefaultExt<ARPayment.depositAsBatch>(e.Row);
			sender.SetDefaultExt<ARPayment.depositAfter>(e.Row);


			if ((cashaccount.Current != null) && (cashaccount.Current.Reconcile == false))
			{
				payment.Cleared = true;
				payment.ClearDate = payment.DocDate;
			}
		}
		protected virtual void ARPayment_Cleared_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARPayment payment = (ARPayment)e.Row;
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
		protected virtual void ARPayment_AdjDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARPayment payment = (ARPayment)e.Row;
			if (payment.Released == false && payment.VoidAppl == false)
			{
				CurrencyInfoAttribute.SetEffectiveDate<ARPayment.adjDate>(sender, e);

				sender.SetDefaultExt<ARPayment.depositAfter>(e.Row);
				LoadInvoicesProc(true);
			}
		}

		protected virtual void ARPayment_AdjDate_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			ARPayment doc = (ARPayment)e.Row;

			if (doc.DocType == ARDocType.VoidPayment)
			{
				ARPayment orig_payment = PXSelect<ARPayment, Where2<Where<ARPayment.docType, Equal<ARDocType.payment>, Or<ARPayment.docType, Equal<ARDocType.prepayment>>>, And<ARPayment.refNbr, Equal<Current<ARPayment.refNbr>>>>>.SelectSingleBound(this, new object[] { e.Row });
				if (orig_payment != null && orig_payment.DocDate > (DateTime)e.NewValue)
				{
					throw new PXSetPropertyException(CS.Messages.Entry_GE, orig_payment.DocDate.Value.ToString("d"));
				}
			}
		}

		protected virtual void ARPayment_CuryOrigDocAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			if (!(bool)((ARPayment)e.Row).Released)
			{
				sender.SetValueExt<ARPayment.curyDocBal>(e.Row, ((ARPayment)e.Row).CuryOrigDocAmt);
			}
		}

		protected virtual void ARPayment_RefTranExtNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		private bool IsApprovalRequired(ARPayment doc, PXCache cache)
		{
			var isApprovalInstalled = PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>();
			var areMapsAssigned = Approval.GetAssignedMaps(doc, cache).Any();
			return doc.DocType == ARDocType.Refund && isApprovalInstalled && areMapsAssigned;
		}
		#endregion
		protected virtual void ARAdjust_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			ARAdjust adj = e.Row as ARAdjust;
			if (adj == null)
				return;

			bool adjNotReleased = adj.Released != true;
			bool adjNotVoided = adj.Voided != true;

			PXUIFieldAttribute.SetEnabled<ARAdjust.adjdDocType>(cache, adj, adj.AdjdRefNbr == null);
			PXUIFieldAttribute.SetEnabled<ARAdjust.adjdRefNbr>(cache, adj, adj.AdjdRefNbr == null);
			PXUIFieldAttribute.SetEnabled<ARAdjust.curyAdjgAmt>(cache, adj, adjNotReleased && adjNotVoided);
			PXUIFieldAttribute.SetEnabled<ARAdjust.curyAdjgPPDAmt>(cache, adj, adjNotReleased && adjNotVoided);
			PXUIFieldAttribute.SetEnabled<ARAdjust.adjBatchNbr>(cache, adj, false);

			PXUIFieldAttribute.SetVisible<ARAdjust.adjBatchNbr>(cache, adj, !adjNotReleased);

			if (Document.Current != null)
			{
				Customer adjdCustomer = PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Select(this, adj.AdjdCustomerID);
				if (adjdCustomer != null)
				{
					bool smallBalanceAllow = adjdCustomer.SmallBalanceAllow == true && adjdCustomer.SmallBalanceLimit > 0 && adj.AdjdDocType != ARDocType.CreditMemo;
					PXUIFieldAttribute.SetEnabled<ARAdjust.curyAdjgWOAmt>(cache, adj, adjNotReleased && adjNotVoided && smallBalanceAllow);
					PXUIFieldAttribute.SetEnabled<ARAdjust.writeOffReasonCode>(cache, adj, smallBalanceAllow && Document.Current.SelfVoidingDoc != true);
				}
			}

			bool EnableCrossRate = false;
			if (adj.Released != true)
			{
				CurrencyInfo pay_info = CurrencyInfo_CuryInfoID.Select(adj.AdjgCuryInfoID);
				CurrencyInfo vouch_info = CurrencyInfo_CuryInfoID.Select(adj.AdjdCuryInfoID);
				EnableCrossRate = !string.Equals(pay_info.CuryID, vouch_info.CuryID) && !string.Equals(vouch_info.CuryID, vouch_info.BaseCuryID);
			}
			PXUIFieldAttribute.SetEnabled<ARAdjust.adjdCuryRate>(cache, adj, EnableCrossRate);
		}

		protected virtual void ARAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
			string errmsg = PXUIFieldAttribute.GetError<ARAdjust.adjdRefNbr>(sender, e.Row);

			e.Cancel = (((ARAdjust)e.Row).AdjdRefNbr == null || string.IsNullOrEmpty(errmsg) == false);
		}

		protected virtual void ARAdjust_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
		{
			ARAdjust adj = (ARAdjust)e.Row;
			if (_IsVoidCheckInProgress == false && adj.Voided == true)
			{
				throw new PXSetPropertyException(ErrorMessages.CantUpdateRecord);
			}
		}

		protected virtual void ARAdjust_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			if (((ARAdjust)e.Row).AdjdCuryInfoID != ((ARAdjust)e.Row).AdjgCuryInfoID && ((ARAdjust)e.Row).AdjdCuryInfoID != ((ARAdjust)e.Row).AdjdOrigCuryInfoID && ((ARAdjust)e.Row).VoidAdjNbr == null)
			{
				foreach (CurrencyInfo info in CurrencyInfo_CuryInfoID.Select(((ARAdjust)e.Row).AdjdCuryInfoID))
				{
					currencyinfo.Delete(info);
				}
			}
		}

		protected virtual void ARAdjust_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			ARAdjust doc = (ARAdjust)e.Row;

			if ((e.Operation & PXDBOperation.Command) == PXDBOperation.Delete)
			{
				return;
			}

			if (doc.Released != true && doc.AdjNbr < Document.Current?.AdjCntr)
			{
				sender.RaiseExceptionHandling<ARAdjust.adjdRefNbr>(doc, doc.AdjdRefNbr, new PXSetPropertyException(Messages.ApplicationStateInvalid));
			}

			if (doc.CuryDocBal < 0m)
			{
				sender.RaiseExceptionHandling<ARAdjust.curyAdjgAmt>(e.Row, doc.CuryAdjgAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
			}

			if (doc.CuryDiscBal < 0m)
			{
				sender.RaiseExceptionHandling<ARAdjust.curyAdjgPPDAmt>(e.Row, doc.CuryAdjgPPDAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
			}

			if (doc.CuryWOBal < 0m)
			{
				sender.RaiseExceptionHandling<ARAdjust.curyAdjgWOAmt>(e.Row, doc.CuryAdjgWOAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
			}

			doc.PendingPPD = doc.CuryAdjgPPDAmt != 0m && doc.AdjdHasPPDTaxes == true;
			if (doc.PendingPPD == true && doc.CuryDocBal != 0m && doc.Voided != true)
			{
				sender.RaiseExceptionHandling<ARAdjust.curyAdjgPPDAmt>(e.Row, doc.CuryAdjgPPDAmt, new PXSetPropertyException(Messages.PartialPPD));
			}

			if (doc.CuryAdjgWOAmt > 0 && string.IsNullOrEmpty(doc.WriteOffReasonCode))
			{
				if (sender.RaiseExceptionHandling<ARAdjust.writeOffReasonCode>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<ARAdjust.writeOffReasonCode>(sender))))
				{
					throw new PXRowPersistingException(PXDataUtils.FieldName<ARAdjust.writeOffReasonCode>(), null, ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<ARAdjust.writeOffReasonCode>(sender));
				}
			}
		}

		protected virtual void ARAdjust_AdjdRefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = (Document.Current != null && Document.Current.VoidAppl == true || this._AutoPaymentApp);
		}

		protected virtual void ARAdjust_AdjdRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			try
			{
				ARAdjust adj = (ARAdjust)e.Row;
				if (adj.AdjdCuryInfoID == null)
				{
					foreach (PXResult<ARInvoice, CurrencyInfo> res in ARInvoice_DocType_RefNbr.Select(adj.AdjdDocType, adj.AdjdRefNbr))
					{
						ARAdjust_AdjdRefNbr_FieldUpdated<ARInvoice>(res, adj);
						return;
					}

					foreach (PXResult<ARPayment, CurrencyInfo> res in ARPayment_DocType_RefNbr.Select(adj.AdjdDocType, adj.AdjdRefNbr))
					{
						ARAdjust_AdjdRefNbr_FieldUpdated<ARPayment>(res, adj);
					}
				}
				else if (adj.AdjgCuryInfoID != null)
				{
					FillDiscAmts(adj);
					CalcBalances(adj, true, false);
				}
			}
			catch (PXSetPropertyException ex)
			{
				throw new PXException(ex.Message);
			}
		}

		private void ARAdjust_AdjdRefNbr_FieldUpdated<T>(PXResult<T, CurrencyInfo> res, ARAdjust adj)
			where T : ARRegister, IInvoice, new()
		{
			CurrencyInfo info_copy = PXCache<CurrencyInfo>.CreateCopy((CurrencyInfo)res);
			info_copy.CuryInfoID = null;
			info_copy = (CurrencyInfo)currencyinfo.Cache.Insert(info_copy);
			T invoice = (T)res;

			//currencyinfo.Cache.SetValueExt<CurrencyInfo.curyEffDate>(info_copy, Document.Current.DocDate);
			info_copy.SetCuryEffDate(currencyinfo.Cache, Document.Current.DocDate);

			adj.CustomerID = Document.Current.CustomerID;
			adj.AdjgDocDate = Document.Current.AdjDate;
			adj.AdjgCuryInfoID = Document.Current.CuryInfoID;
			adj.AdjdCustomerID = invoice.CustomerID;
			adj.AdjdCuryInfoID = info_copy.CuryInfoID;
			adj.AdjdOrigCuryInfoID = invoice.CuryInfoID;
			adj.AdjdBranchID = invoice.BranchID;
			adj.AdjdARAcct = invoice.ARAccountID;
			adj.AdjdARSub = invoice.ARSubID;
			adj.AdjdDocDate = invoice.DocDate;
			adj.AdjdFinPeriodID = invoice.FinPeriodID;
			adj.AdjdHasPPDTaxes = invoice.HasPPDTaxes;
			adj.Released = false;
			adj.PendingPPD = false;

			CalcBalances<T>(adj, invoice, false);

			decimal? CuryApplAmt = adj.CuryDocBal - adj.CuryDiscBal;
			decimal? CuryApplDiscAmt = adj.CuryDiscBal;
			decimal? CuryUnappliedBal = Document.Current.CuryUnappliedBal;

			if (adj.CuryDiscBal >= 0m && adj.CuryDocBal - adj.CuryDiscBal <= 0m)
			{
				//no amount suggestion is possible
				return;
			}

			if (Document.Current != null && adj.AdjgBalSign < 0m)
			{
				if (CuryUnappliedBal < 0m)
				{
					CuryApplAmt = Math.Min((decimal)CuryApplAmt, Math.Abs((decimal)CuryUnappliedBal));
				}
			}
			else if (Document.Current != null && CuryUnappliedBal > 0m && adj.AdjgBalSign > 0m)
			{
				CuryApplAmt = Math.Min((decimal)CuryApplAmt, (decimal)CuryUnappliedBal);

				if (CuryApplAmt + CuryApplDiscAmt < adj.CuryDocBal)
				{
					CuryApplDiscAmt = 0m;
				}
			}
			else if (Document.Current != null && CuryUnappliedBal <= 0m && ((ARPayment)Document.Current).CuryOrigDocAmt > 0)
			{
				CuryApplAmt = 0m;
				CuryApplDiscAmt = 0m;
			}

			adj.CuryAdjgAmt = CuryApplAmt;
			adj.CuryAdjgDiscAmt = CuryApplDiscAmt;
			adj.CuryAdjgPPDAmt = CuryApplDiscAmt;
			adj.CuryAdjgWOAmt = 0m;

			CalcBalances<T>(adj, invoice, true);
		}

		protected bool internalCall;

		protected virtual void ARAdjust_CuryDocBal_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			e.Cancel = true;

			ARAdjust application = e.Row as ARAdjust;

			if (application == null) return;

			if (!internalCall)
			{
				if (application.AdjdCuryInfoID != null &&
					application.CuryDocBal == null &&
					sender.GetStatus(application) != PXEntryStatus.Deleted)
				{
					CalcBalances(application, false, false);
				}

				e.NewValue = ((ARAdjust)e.Row).CuryDocBal;
			}
		}

		protected virtual void ARAdjust_CuryDiscBal_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			if (!internalCall)
			{
				if (e.Row != null && ((ARAdjust)e.Row).AdjdCuryInfoID != null && ((ARAdjust)e.Row).CuryDiscBal == null && sender.GetStatus(e.Row) != PXEntryStatus.Deleted)
				{
					CalcBalances((ARAdjust)e.Row, false, false);
				}
				if (e.Row != null)
				{
					e.NewValue = ((ARAdjust)e.Row).CuryDiscBal;
				}
			}
			e.Cancel = true;
		}
		protected virtual void ARAdjust_CuryAdjgAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			ARAdjust adj = (ARAdjust)e.Row;

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null || adj.CuryWOBal == null)
			{
				CalcBalances((ARAdjust)e.Row, false, false);
			}

			if (adj.CuryDocBal == null)
			{
				sender.RaiseExceptionHandling<ARAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr,
					new PXSetPropertyException(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<ARAdjust.adjdRefNbr>(sender)));
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

			if ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgAmt - (decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgAmt).ToString());
			}
		}

		protected virtual void ARAdjust_CuryAdjgAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CalcBalances((ARAdjust)e.Row, true, false);
		}

		protected virtual void ARAdjust_CuryAdjgPPDAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			ARAdjust adj = (ARAdjust)e.Row;

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null || adj.CuryWOBal == null)
			{
				CalcBalances((ARAdjust)e.Row, false, false);
			}

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null)
			{
				sender.RaiseExceptionHandling<ARAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr,
					new PXSetPropertyException(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<ARAdjust.adjdRefNbr>(sender)));
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

			decimal remainingCashDiscountBalance = (adj.CuryDiscBal ?? 0m) + (adj.CuryAdjgPPDAmt ?? 0m);

			if (remainingCashDiscountBalance - (decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException(
					Messages.AmountEnteredExceedsRemainingCashDiscountBalance,
					remainingCashDiscountBalance.ToString());
			}

			if (adj.CuryAdjgAmt != null && (sender.GetValuePending<ARAdjust.curyAdjgAmt>(e.Row) == PXCache.NotSetValue || (Decimal?)sender.GetValuePending<ARAdjust.curyAdjgAmt>(e.Row) == adj.CuryAdjgAmt))
			{
				if ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgPPDAmt - (decimal)e.NewValue < 0)
				{
					throw new PXSetPropertyException(CS.Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgPPDAmt).ToString());
				}
			}
		}

		protected virtual void ARAdjust_CuryAdjgDiscAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARAdjust adj = e.Row as ARAdjust;
			if (adj == null) return;

			FillPPDAmts(adj);
			CalcBalances(adj, true, false);
		}

		protected virtual void ARAdjust_CuryAdjgPPDAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARAdjust adj = e.Row as ARAdjust;
			if (adj == null) return;

			FillDiscAmts(adj);
			CalcBalances(adj, true, false);
		}

		protected void FillDiscAmts(ARAdjust adj)
		{
			adj.CuryAdjgDiscAmt = adj.CuryAdjgPPDAmt;
			adj.CuryAdjdDiscAmt = adj.CuryAdjdPPDAmt;
			adj.AdjDiscAmt = adj.AdjPPDAmt;
		}

		protected void FillPPDAmts(ARAdjust adj)
		{
			adj.CuryAdjgPPDAmt = adj.CuryAdjgDiscAmt;
			adj.CuryAdjdPPDAmt = adj.CuryAdjdDiscAmt;
			adj.AdjPPDAmt = adj.AdjDiscAmt;
		}

		protected virtual void ARAdjust_CuryAdjgWOAmt_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			ARAdjust adj = (ARAdjust)e.Row;

			if (adj.CuryDocBal == null || adj.CuryDiscBal == null || adj.CuryWOBal == null)
			{
				CalcBalances((ARAdjust)e.Row, false, false);
			}

			if (adj.CuryDocBal == null || adj.CuryWOBal == null)
			{
				sender.RaiseExceptionHandling<ARAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr,
					new PXSetPropertyException(ErrorMessages.FieldIsEmpty, PXErrorLevel.Error, PXUIFieldAttribute.GetDisplayName<ARAdjust.adjdRefNbr>(sender)));
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

			if ((decimal)adj.CuryWOBal + (decimal)adj.CuryAdjgWOAmt - (decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_LE, ((decimal)adj.CuryWOBal + (decimal)adj.CuryAdjgWOAmt).ToString());
			}

			if (adj.CuryAdjgAmt != null && (sender.GetValuePending<ARAdjust.curyAdjgAmt>(e.Row) == PXCache.NotSetValue || (Decimal?)sender.GetValuePending<ARAdjust.curyAdjgAmt>(e.Row) == adj.CuryAdjgAmt))
			{
				if ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgWOAmt - (decimal)e.NewValue < 0)
				{
					throw new PXSetPropertyException(CS.Messages.Entry_LE, ((decimal)adj.CuryDocBal + (decimal)adj.CuryAdjgWOAmt).ToString());
				}
			}
		}

		protected virtual void ARAdjust_AdjdCuryRate_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if ((decimal?)e.NewValue <= 0m)
			{
				throw new PXSetPropertyException(CS.Messages.Entry_GT, ((int)0).ToString());
			}
		}

		protected virtual void ARAdjust_AdjdCuryRate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARAdjust adj = (ARAdjust)e.Row;

			CurrencyInfo pay_info = CurrencyInfo_CuryInfoID.Select(adj.AdjgCuryInfoID);
			CurrencyInfo vouch_info = CurrencyInfo_CuryInfoID.Select(adj.AdjdCuryInfoID);

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
				sender.SetValue<ARAdjust.curyAdjgAmt>(e.Row, payment_docbal);

			if (discount_docbal != (decimal)adj.CuryAdjgDiscAmt)
				sender.SetValue<ARAdjust.curyAdjgDiscAmt>(e.Row, discount_docbal);

			FillPPDAmts(adj);
			CalcBalances(adj, true);
		}

		protected virtual void ARAdjust_CuryAdjgWOAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CalcBalances((ARAdjust)e.Row, true, false);
		}

		protected virtual void ARAdjust_Voided_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CalcBalances((ARAdjust)e.Row, true, false);
		}
		#endregion
		protected void CalcBalances(ARAdjust row, bool isCalcRGOL)
		{
			CalcBalances(row, isCalcRGOL, true);
		}

		protected void CalcBalances(ARAdjust adj, bool isCalcRGOL, bool DiscOnDiscDate)
		{
			foreach (ARInvoice invoice in ARInvoice_DocType_RefNbr.Select(adj.AdjdDocType, adj.AdjdRefNbr))
			{
				CalcBalances<ARInvoice>(adj, invoice, isCalcRGOL, DiscOnDiscDate);
				return;
			}

			foreach (ARPayment payment in ARPayment_DocType_RefNbr.Select(adj.AdjdDocType, adj.AdjdRefNbr))
			{
				CalcBalances<ARPayment>(adj, payment, isCalcRGOL, DiscOnDiscDate);
			}
		}

		protected void CalcBalances<T>(ARAdjust adj, T invoice, bool isCalcRGOL)
			where T : class, IBqlTable, IInvoice, new()
		{
			CalcBalances<T>(adj, invoice, isCalcRGOL, true);
		}

		protected void CalcBalances<T>(ARAdjust adj, T invoice, bool isCalcRGOL, bool DiscOnDiscDate)
			where T : class, IBqlTable, IInvoice, new()
		{
			bool isPendingPPD = adj.CuryAdjgPPDAmt != null && adj.CuryAdjgPPDAmt != 0m && adj.AdjdHasPPDTaxes == true;
			if (isPendingPPD)
			{
				adj.CuryAdjgDiscAmt = 0m;
				adj.CuryAdjdDiscAmt = 0m;
				adj.AdjDiscAmt = 0m;
			}

			if (this._AutoPaymentApp)
			{
				internalCall = true;
				ARAdjust unreleased = PXSelectGroupBy<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.released, Equal<False>, And<ARAdjust.voided, Equal<False>, And<Where<ARAdjust.adjgDocType, NotEqual<Required<ARAdjust.adjgDocType>>, Or<ARAdjust.adjgRefNbr, NotEqual<Required<ARAdjust.adjgRefNbr>>>>>>>>>, Aggregate<GroupBy<ARAdjust.adjdDocType, GroupBy<ARAdjust.adjdRefNbr, Sum<ARAdjust.curyAdjdAmt, Sum<ARAdjust.adjAmt, Sum<ARAdjust.curyAdjdDiscAmt, Sum<ARAdjust.adjDiscAmt>>>>>>>>.Select(this, adj.AdjdDocType, adj.AdjdRefNbr, adj.AdjgDocType, adj.AdjgRefNbr);
				internalCall = false;
				if (unreleased != null && unreleased.AdjdRefNbr != null)
				{
					invoice = PXCache<T>.CreateCopy(invoice);
					invoice.CuryDocBal -= (unreleased.CuryAdjdAmt + unreleased.CuryAdjdDiscAmt);
					invoice.DocBal -= (unreleased.AdjAmt + unreleased.AdjDiscAmt + unreleased.RGOLAmt);
					invoice.CuryDiscBal -= unreleased.CuryAdjdDiscAmt;
					invoice.DiscBal -= unreleased.AdjDiscAmt;
				}
				this._AutoPaymentApp = false;
			}

			PaymentEntry.CalcBalances<T, ARAdjust>(CurrencyInfo_CuryInfoID, adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, invoice, adj);
			if (DiscOnDiscDate)
			{
				PaymentEntry.CalcDiscount<T, ARAdjust>(adj.AdjgDocDate, invoice, adj);
			}
			PaymentEntry.WarnPPDiscount<T, ARAdjust>(this, adj.AdjgDocDate, invoice, adj, adj.CuryAdjgPPDAmt);

			CurrencyInfo pay_info = CurrencyInfo_CuryInfoID.Select(adj.AdjgCuryInfoID);
			CurrencyInfo vouch_info = CurrencyInfo_CuryInfoID.Select(adj.AdjdCuryInfoID);

			if (vouch_info != null && string.Equals(pay_info.CuryID, vouch_info.CuryID) == false)
			{
				adj.AdjdCuryRate = Math.Round((vouch_info.CuryMultDiv == "M" ? (decimal)vouch_info.CuryRate : 1 / (decimal)vouch_info.CuryRate) * (pay_info.CuryMultDiv == "M" ? 1 / (decimal)pay_info.CuryRate : (decimal)pay_info.CuryRate), 8, MidpointRounding.AwayFromZero);
			}
			else
			{
				adj.AdjdCuryRate = 1m;
			}

			Customer invoiceCustomer = PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Select(this, adj.AdjdCustomerID);
			if (invoiceCustomer != null && invoiceCustomer.SmallBalanceAllow == true && adj.AdjgDocType != ARDocType.Refund && adj.AdjdDocType != ARDocType.CreditMemo)
			{
				decimal payment_smallbalancelimit;
				CurrencyInfo payment_info = CurrencyInfo_CuryInfoID.Select(adj.AdjgCuryInfoID);
				PXDBCurrencyAttribute.CuryConvCury(CurrencyInfo_CuryInfoID.Cache, payment_info, invoiceCustomer.SmallBalanceLimit ?? 0m, out payment_smallbalancelimit);
				adj.CuryWOBal = payment_smallbalancelimit;
				adj.WOBal = invoiceCustomer.SmallBalanceLimit;

				invoice.CuryWhTaxBal = payment_smallbalancelimit;
				invoice.WhTaxBal = invoiceCustomer.SmallBalanceLimit;
			}
			else
			{
				adj.CuryWOBal = 0m;
				adj.WOBal = 0m;

				invoice.CuryWhTaxBal = 0m;
				invoice.WhTaxBal = 0m;
			}

			PaymentEntry.AdjustBalance<ARAdjust>(CurrencyInfo_CuryInfoID, adj);

			//Fill AdjPPDAmt
			if (isPendingPPD && adj.AdjPPDAmt == null && adj.Released != true)
			{
				ARAdjust adjPPD = PXCache<ARAdjust>.CreateCopy(adj);
				FillDiscAmts(adjPPD);

				PaymentEntry.AdjustBalance<ARAdjust>(CurrencyInfo_CuryInfoID, adjPPD);
				adj.AdjPPDAmt = adjPPD.AdjDiscAmt;
			}

			if (isCalcRGOL && (adj.Voided != true))
			{
				PaymentEntry.CalcRGOL<T, ARAdjust>(CurrencyInfo_CuryInfoID, invoice, adj);
				adj.RGOLAmt = adj.ReverseGainLoss == true ? -1m * adj.RGOLAmt : adj.RGOLAmt;

				//Fill CuryAdjdPPDAmt
				decimal? CuryAdjdPPDAmt = adj.CuryAdjdDiscAmt;
				if (isPendingPPD)
				{
					ARAdjust adjPPD = PXCache<ARAdjust>.CreateCopy(adj);
					FillDiscAmts(adjPPD);

					PaymentEntry.CalcRGOL<T, ARAdjust>(CurrencyInfo_CuryInfoID, invoice, adjPPD);
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
			if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() && info != null)
			{
				bool curyenabled = info.AllowUpdate(this.Adjustments.Cache);

				if (customer.Current != null && customer.Current.AllowOverrideRate == false)
				{
					curyenabled = false;
				}

				if (curyenabled)
				{
					ARPayment payment = PXSelect<ARPayment, Where<ARPayment.curyInfoID, Equal<Current<CurrencyInfo.curyInfoID>>>>.Select(this);

					if (payment != null && (payment.VoidAppl == true || payment.Released == true))
					{
						curyenabled = false;
					}
				}

				info.IsReadOnly = !curyenabled;

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
				foreach (ARAdjust adj in PXSelect<ARAdjust, Where<ARAdjust.adjgCuryInfoID, Equal<Required<ARAdjust.adjgCuryInfoID>>>>.Select(sender.Graph, ((CurrencyInfo)e.Row).CuryInfoID))
				{
					if (Adjustments.Cache.GetStatus(adj) == PXEntryStatus.Notchanged)
					{
						Adjustments.Cache.SetStatus(adj, PXEntryStatus.Updated);
					}

					CalcBalances(adj, true);

					if (adj.CuryDocBal < 0m)
					{
						Adjustments.Cache.RaiseExceptionHandling<ARAdjust.curyAdjgAmt>(adj, adj.CuryAdjgAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
					}

					if (adj.CuryDiscBal < 0m)
					{
						Adjustments.Cache.RaiseExceptionHandling<ARAdjust.curyAdjgPPDAmt>(adj, adj.CuryAdjgPPDAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
					}

					if (adj.CuryWOBal < 0m)
					{
						Adjustments.Cache.RaiseExceptionHandling<ARAdjust.curyAdjgWOAmt>(adj, adj.CuryAdjgWOAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
					}
				}
				foreach (SOAdjust soAdj in PXSelect<SOAdjust, Where<SOAdjust.adjgCuryInfoID, Equal<Required<SOAdjust.adjgCuryInfoID>>>>.Select(sender.Graph, ((CurrencyInfo)e.Row).CuryInfoID))
				{
					if (SOAdjustments.Cache.GetStatus(soAdj) == PXEntryStatus.Notchanged)
					{
						SOAdjustments.Cache.SetStatus(soAdj, PXEntryStatus.Updated);
					}

					CalcBalances(soAdj, true);

					if (soAdj.CuryDocBal < 0m)
					{
						SOAdjustments.Cache.RaiseExceptionHandling<SOAdjust.curyAdjgAmt>(soAdj, soAdj.CuryAdjgAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
					}

				}
			}
		}

		protected virtual void ARPayment_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			ARPayment doc = (ARPayment)e.Row;

			if (doc.DocType != ARDocType.CreditMemo && doc.DocType != ARDocType.SmallBalanceWO && doc.CashAccountID == null)
			{
				if (sender.RaiseExceptionHandling<ARPayment.cashAccountID>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(ARPayment.cashAccountID).Name)))
				{
					throw new PXRowPersistingException(typeof(ARPayment.cashAccountID).Name, null, ErrorMessages.FieldIsEmpty, typeof(ARPayment.cashAccountID).Name);
				}
			}

			if (doc.DocType != ARDocType.CreditMemo && doc.DocType != ARDocType.SmallBalanceWO && String.IsNullOrEmpty(doc.PaymentMethodID))
			{
				if (sender.RaiseExceptionHandling<ARPayment.paymentMethodID>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(ARPayment.paymentMethodID).Name)))
				{
					throw new PXRowPersistingException(typeof(ARPayment.paymentMethodID).Name, null, ErrorMessages.FieldIsEmpty, typeof(ARPayment.paymentMethodID).Name);
				}
			}

			PaymentMethod pm = this.paymentmethod.Current;
			PXDefaultAttribute.SetPersistingCheck<ARPayment.pMInstanceID>(sender, doc, (doc.DocType != ARDocType.CreditMemo && doc.DocType != ARDocType.SmallBalanceWO
				&& doc.PMInstanceID == null && pm?.IsAccountNumberRequired == true) ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);

			if (doc.OpenDoc == true && doc.Hold != true && IsPaymentUnbalanced(doc))
			{
				throw new PXRowPersistingException(typeof(ARPayment.curyOrigDocAmt).Name, doc.CuryOrigDocAmt, Messages.DocumentOutOfBalance);
			}

			PaymentRefAttribute.SetUpdateCashManager<ARPayment.extRefNbr>(sender, e.Row, ((ARPayment)e.Row).DocType != ARDocType.VoidPayment);

			string errMsg;
			// VerifyAdjFinPeriodID() compares Payment "Application Period" only with applications, that have been released. Sometimes, this may cause an erorr 
			// during the action, while document is saved and closed (because of Persist() for each action) - this why doc.OpenDoc flag has been used as a criteria.
			if (doc.OpenDoc == true && !VerifyAdjFinPeriodID(doc, doc.AdjFinPeriodID, out errMsg))
			{
				if (sender.RaiseExceptionHandling<ARPayment.adjFinPeriodID>(e.Row,
					FinPeriodIDAttribute.FormatForDisplay(doc.AdjFinPeriodID), new PXSetPropertyException(errMsg)))
				{
					throw new PXRowPersistingException(typeof(ARPayment.adjFinPeriodID).Name, FinPeriodIDAttribute.FormatForError(doc.AdjFinPeriodID), errMsg);
				}
			}
		}

		protected bool InternalCall = false;

		protected class PaymentState
		{
			public readonly bool isCCVoided = false;
			public readonly bool isCCCaptured = false;
			public readonly bool isCCPreAuthorized = false;
			public readonly bool isCCRefunded = false;
			public readonly bool isCCVoidingAttempted = false; //Special flag for VoidPayment Release logic
			public readonly string Description;
			public readonly CCProcTran lastTran;

			public PaymentState()
			{

			}

			public PaymentState(IEnumerable<PXResult<CCProcTran>> ccProcTrans)
			{
				lastTran = null;
				CCPaymentState ccPaymentState = CCProcTranHelper.ResolveCCPaymentState(ccProcTrans, out lastTran);
				isCCVoided = (ccPaymentState & CCPaymentState.Voided) != 0;
				isCCCaptured = (ccPaymentState & CCPaymentState.Captured) != 0;
				isCCPreAuthorized = (ccPaymentState & CCPaymentState.PreAuthorized) != 0;
				isCCRefunded = (ccPaymentState & CCPaymentState.Refunded) != 0;
				isCCVoidingAttempted = (ccPaymentState & CCPaymentState.VoidFailed) != 0;
				Description = CCProcTranHelper.FormatCCPaymentState(ccPaymentState);
			}
		}
		protected virtual void ARPayment_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			ARPayment doc = e.Row as ARPayment;

			if (doc == null || InternalCall)
			{
				return;
			}

			bool dontApprove = !IsApprovalRequired(doc, cache);
			if (doc.DontApprove != dontApprove)
			{
				cache.SetValueExt<ARPayment.dontApprove>(doc, dontApprove);
			}
			// We need this for correct tabs repainting
			// in migration mode.
			// 
			Adjustments.Cache.AllowSelect =
			SOAdjustments.Cache.AllowSelect =
			PaymentCharges.Cache.AllowSelect = true;

			bool docIsMemoOrBalanceWO = doc.DocType == ARDocType.CreditMemo || doc.DocType == ARDocType.SmallBalanceWO;
			bool isPMInstanceRequired = false;

			if (!string.IsNullOrEmpty(doc.PaymentMethodID))
			{
				isPMInstanceRequired = paymentmethod.Current?.IsAccountNumberRequired ?? false;
			}

			PXUIFieldAttribute.SetVisible<ARPayment.curyID>(cache, doc, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
			PXUIFieldAttribute.SetVisible<ARPayment.cashAccountID>(cache, doc, !docIsMemoOrBalanceWO);
			PXUIFieldAttribute.SetVisible<ARPayment.cleared>(cache, doc, !docIsMemoOrBalanceWO);
			PXUIFieldAttribute.SetVisible<ARPayment.clearDate>(cache, doc, !docIsMemoOrBalanceWO);
			PXUIFieldAttribute.SetVisible<ARPayment.paymentMethodID>(cache, doc, !docIsMemoOrBalanceWO);
			PXUIFieldAttribute.SetVisible<ARPayment.pMInstanceID>(cache, doc, !docIsMemoOrBalanceWO);
			PXUIFieldAttribute.SetVisible<ARPayment.extRefNbr>(cache, doc, !docIsMemoOrBalanceWO);

			PXUIFieldAttribute.SetRequired<ARPayment.cashAccountID>(cache, !docIsMemoOrBalanceWO);
			PXUIFieldAttribute.SetRequired<ARPayment.pMInstanceID>(cache, !docIsMemoOrBalanceWO && isPMInstanceRequired);
			PXUIFieldAttribute.SetEnabled<ARPayment.pMInstanceID>(cache, e.Row, !docIsMemoOrBalanceWO && isPMInstanceRequired);
			PXDefaultAttribute.SetPersistingCheck<ARPayment.pMInstanceID>(cache, doc, (!docIsMemoOrBalanceWO && isPMInstanceRequired) ? PXPersistingCheck.NullOrBlank
																																	  : PXPersistingCheck.Nothing);

			bool isDepositAfterEditable = doc.DocType == ARDocType.Payment ||
										  doc.DocType == ARDocType.Prepayment ||
										  doc.DocType == ARDocType.Refund ||
										  doc.DocType == ARDocType.CashSale;

			PXUIFieldAttribute.SetVisible<ARPayment.depositAfter>(cache, doc, isDepositAfterEditable && doc.DepositAsBatch == true);
			PXUIFieldAttribute.SetEnabled<ARPayment.depositAfter>(cache, doc, false);
			PXUIFieldAttribute.SetRequired<ARPayment.depositAfter>(cache, isDepositAfterEditable && doc.DepositAsBatch == true);

			PXPersistingCheck depositAfterPersistCheck = (isDepositAfterEditable && doc.DepositAsBatch == true) ? PXPersistingCheck.NullOrBlank
																												: PXPersistingCheck.Nothing;
			PXDefaultAttribute.SetPersistingCheck<ARPayment.depositAfter>(cache, doc, depositAfterPersistCheck);

			PXUIFieldAttribute.SetVisible<ARAdjust.adjdCustomerID>(Adjustments.Cache, null, PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>());
			PXUIFieldAttribute.SetVisible<ARAdjust.adjdCustomerID>(Adjustments_History.Cache, null, PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>());

			OpenPeriodAttribute.SetValidatePeriod<ARPayment.adjFinPeriodID>(cache, doc, doc.OpenDoc == true ? PeriodValidation.DefaultSelectUpdate
																											: PeriodValidation.DefaultUpdate);

			CashAccount cashAccount = this.cashaccount.Current;
			bool isClearingAccount = (cashAccount != null && cashAccount.CashAccountID == doc.CashAccountID && cashAccount.ClearingAccount == true);

			bool curyenabled = false;
			bool docNotOnHold = doc.Hold == false;
			bool docOnHold = doc.Hold == true;
			bool docNotReleased = doc.Released == false;
			bool docReleased = doc.Released == true;
			bool docOpen = doc.OpenDoc == true;
			bool docNotVoided = doc.Voided == false;
			bool clearEnabled = docOnHold && cashaccount.Current?.Reconcile == true;
			bool holdAdj = false;
			bool docClosed = doc.Status == ARDocStatus.Closed;

			#region Credit Card Processing

			bool enableCCProcess = false;
			bool docTypePayment = doc.DocType == ARDocType.Payment || doc.DocType == ARDocType.Prepayment;
			doc.IsCCPayment = false;

			// We should always process migrated CC Payments 
			// as regular documents.
			//
			if (doc.IsMigratedRecord != true &&
				paymentmethod.Current != null &&
				paymentmethod.Current.PaymentType == CA.PaymentMethodType.CreditCard &&
				paymentmethod.Current.IsAccountNumberRequired == true)
			{
				doc.IsCCPayment = true;
				enableCCProcess = docTypePayment || doc.DocType == ARDocType.Refund || doc.DocType == ARDocType.VoidPayment;
			}

			enableCCProcess = enableCCProcess && !doc.Voided.Value;
			PXPersistingCheck extRefNbrPersistCheck = PXPersistingCheck.Null;

			if (docIsMemoOrBalanceWO || enableCCProcess || arsetup.Current.RequireExtRef == false)
				extRefNbrPersistCheck = PXPersistingCheck.Nothing;

			PXDefaultAttribute.SetPersistingCheck<ARPayment.extRefNbr>(cache, doc, extRefNbrPersistCheck);

			PaymentState CurrPaymentState = enableCCProcess ? new PaymentState(ccProcTran.Select())
															: new PaymentState();
			doc.CCPaymentStateDescr = CurrPaymentState.Description;

			bool canAuthorize = docNotOnHold && docTypePayment && !(CurrPaymentState.isCCPreAuthorized || CurrPaymentState.isCCCaptured);
			bool canCapture = docNotOnHold && docTypePayment && !CurrPaymentState.isCCCaptured;
			bool canVoid = docNotOnHold && (doc.DocType == ARDocType.VoidPayment && (CurrPaymentState.isCCCaptured || CurrPaymentState.isCCPreAuthorized)) ||
						   (CurrPaymentState.isCCPreAuthorized && docTypePayment);
			bool canCredit = docNotOnHold && doc.DocType == ARDocType.Refund && !CurrPaymentState.isCCRefunded;

			this.authorizeCCPayment.SetEnabled(enableCCProcess && canAuthorize);
			this.captureCCPayment.SetEnabled(enableCCProcess && canCapture);
			this.voidCCPayment.SetEnabled(enableCCProcess && canVoid);
			this.creditCCPayment.SetEnabled(enableCCProcess && canCredit);
			this.captureOnlyCCPayment.SetEnabled(enableCCProcess && canAuthorize);

			this.recordCCPayment.SetEnabled(enableCCProcess && ((canCapture && !CurrPaymentState.isCCPreAuthorized) || canCredit));

			PXUIFieldAttribute.SetVisible<ARPayment.cCPaymentStateDescr>(cache, doc, enableCCProcess);
			PXUIFieldAttribute.SetVisible<ARPayment.refTranExtNbr>(cache, doc, doc.DocType == ARDocType.Refund && enableCCProcess);
			#endregion

			if (cache.GetStatus(e.Row) == PXEntryStatus.Notchanged && doc.Status == ARDocStatus.Open && doc.VoidAppl == false &&
				doc.AdjDate != null && ((DateTime)doc.AdjDate).CompareTo((DateTime)Accessinfo.BusinessDate) < 0)
			{
				if (Adjustments_Raw.View.SelectSingleBound(new object[] { e.Row }) == null)
				{
					FinPeriod finPeriod = FinPeriodIDAttribute.FindFinPeriodByDate(this, (DateTime)Accessinfo.BusinessDate);

					if (finPeriod != null)
					{
						doc.AdjDate = Accessinfo.BusinessDate;
						doc.AdjTranPeriodID = finPeriod.FinPeriodID;
						doc.AdjFinPeriodID = finPeriod.FinPeriodID;
						cache.SetStatus(e.Row, PXEntryStatus.Held);
					}
				}
			}

			bool isReclassified = false;

			if (doc.DocType == ARDocType.CreditMemo && cache.GetStatus(doc) == PXEntryStatus.Inserted)
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				cache.AllowUpdate = false;
				cache.AllowDelete = false;
				Adjustments.Cache.AllowDelete = false;
				Adjustments.Cache.AllowUpdate = false;
				Adjustments.Cache.AllowInsert = false;
				release.SetEnabled(false);
			}
			else if ((bool)doc.VoidAppl && docNotReleased)
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<ARPayment.adjDate>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARPayment.adjFinPeriodID>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARPayment.docDesc>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARPayment.depositAfter>(cache, doc, isDepositAfterEditable && doc.DepositAsBatch == true);
				cache.AllowUpdate = true;
				cache.AllowDelete = true;
				Adjustments.Cache.AllowDelete = false;
				Adjustments.Cache.AllowUpdate = false;
				Adjustments.Cache.AllowInsert = false;
				release.SetEnabled(docNotOnHold);
			}
			else if (docReleased && docOpen)
			{
				int AdjCount = Adjustments_Raw.Select().Count;

				foreach (ARAdjust adj in Adjustments_Raw.Select())
				{
					if ((bool)adj.Voided)
					{
						break;
					}

					if (adj.Hold == true)
					{
						holdAdj = true;
						break;
					}
				}

				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<ARPayment.adjDate>(cache, doc, !holdAdj);
				PXUIFieldAttribute.SetEnabled<ARPayment.adjFinPeriodID>(cache, doc, !holdAdj);
				PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, doc, !holdAdj);

				cache.AllowDelete = false;
				cache.AllowUpdate = !holdAdj;
				Adjustments.Cache.AllowDelete = !holdAdj;
				Adjustments.Cache.AllowInsert = !holdAdj && doc.SelfVoidingDoc != true;
				Adjustments.Cache.AllowUpdate = !holdAdj;

				release.SetEnabled(docNotOnHold && !holdAdj && AdjCount != 0);
			}
			else if (docReleased && !docOpen)
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				cache.AllowDelete = false;
				cache.AllowUpdate = isClearingAccount;
				Adjustments.Cache.AllowDelete = false;
				Adjustments.Cache.AllowUpdate = false;
				Adjustments.Cache.AllowInsert = false;
				release.SetEnabled(false);
			}
			else if ((bool)doc.VoidAppl)
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				cache.AllowDelete = false;
				cache.AllowUpdate = false;
				Adjustments.Cache.AllowDelete = false;
				Adjustments.Cache.AllowUpdate = false;
				Adjustments.Cache.AllowInsert = false;
				release.SetEnabled(docNotOnHold);
			}
			else if ((bool)doc.Voided)
			{
				//Document is voided
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				cache.AllowDelete = false;
				cache.AllowUpdate = false;
				Adjustments.Cache.AllowDelete = false;
				Adjustments.Cache.AllowUpdate = false;
				Adjustments.Cache.AllowInsert = false;
				release.SetEnabled(false);
			}
			else if (enableCCProcess && (CurrPaymentState.isCCPreAuthorized || CurrPaymentState.isCCCaptured))
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<ARPayment.adjDate>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARPayment.adjFinPeriodID>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARPayment.docDate>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARPayment.extRefNbr>(cache, doc, true);
				cache.AllowDelete = false;
				cache.AllowUpdate = true;
				Adjustments.Cache.AllowDelete = true;
				Adjustments.Cache.AllowUpdate = true;
				Adjustments.Cache.AllowInsert = true;
				release.SetEnabled(docNotOnHold);
			}
			else
			{
				CATran tran = PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>>.Select(this, doc.CATranID);
				isReclassified = tran?.RefTranID != null;

				PXUIFieldAttribute.SetEnabled(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARPayment.status>(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<ARPayment.curyID>(cache, doc, curyenabled);
				PXUIFieldAttribute.SetEnabled<ARPayment.refTranExtNbr>(cache, doc, enableCCProcess && ((doc.DocType == ARDocType.Refund) && !CurrPaymentState.isCCRefunded));
				cache.AllowDelete = !CCProcTranHelper.HasSuccessfulCCTrans(ccProcTran);
				cache.AllowUpdate = true;
				Adjustments.Cache.AllowDelete = true;
				Adjustments.Cache.AllowUpdate = true;
				Adjustments.Cache.AllowInsert = true;
				release.SetEnabled(docNotOnHold);
				PXUIFieldAttribute.SetEnabled<ARPayment.curyOrigDocAmt>(cache, doc, !isReclassified);
				PXUIFieldAttribute.SetEnabled<ARPayment.cashAccountID>(cache, doc, !isReclassified);
				PXUIFieldAttribute.SetEnabled<ARPayment.pMInstanceID>(cache, doc, !isReclassified && isPMInstanceRequired);
				PXUIFieldAttribute.SetEnabled<ARPayment.paymentMethodID>(cache, doc, !isReclassified);
				PXUIFieldAttribute.SetEnabled<ARPayment.extRefNbr>(cache, doc, !isReclassified);
				PXUIFieldAttribute.SetEnabled<ARPayment.customerID>(cache, doc, !isReclassified);
			}

			bool allowEditSOAdjustments = !(docClosed || doc.VoidAppl == true || doc.Voided == true);
			SOAdjustments.Cache.AllowUpdate = allowEditSOAdjustments;
			SOAdjustments.Cache.AllowDelete = allowEditSOAdjustments;
			SOAdjustments.Cache.AllowInsert = allowEditSOAdjustments;

			PXUIFieldAttribute.SetEnabled<ARPayment.docType>(cache, doc);
			PXUIFieldAttribute.SetEnabled<ARPayment.refNbr>(cache, doc);
			PXUIFieldAttribute.SetEnabled<ARPayment.curyUnappliedBal>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<ARPayment.curyApplAmt>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<ARPayment.curyWOAmt>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<ARPayment.batchNbr>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<ARPayment.cleared>(cache, doc, clearEnabled);
			PXUIFieldAttribute.SetEnabled<ARPayment.clearDate>(cache, doc, clearEnabled && doc.Cleared == true);

			bool enableVoidCheck = docReleased &&
				docNotVoided &&
				(docTypePayment || doc.SelfVoidingDoc == true);

			bool isCCStateClear = (!(CurrPaymentState.isCCCaptured || CurrPaymentState.isCCPreAuthorized)) ||
				CurrPaymentState.isCCVoided ||
				CurrPaymentState.isCCRefunded;

			if (docNotReleased && !enableVoidCheck && docTypePayment && docNotVoided)
			{
				if (CCProcTranHelper.HasCCTransactions(ccProcTran) && (isCCStateClear || (CurrPaymentState.isCCPreAuthorized && CurrPaymentState.isCCVoidingAttempted)))
					enableVoidCheck = true;
			}

			voidCheck.SetEnabled(enableVoidCheck && !holdAdj);

			bool loadActionsEnabled = doc.CustomerID != null && (bool)doc.OpenDoc && !holdAdj &&
									  (doc.DocType == ARDocType.Payment || doc.DocType == ARDocType.Prepayment || doc.DocType == ARDocType.CreditMemo);

			loadInvoices.SetEnabled(loadActionsEnabled);
			loadOrders.SetEnabled(loadActionsEnabled);

			#region CCProcessing integrated with doc
			if (enableCCProcess && this.arsetup.Current.IntegratedCCProcessing == true && docNotReleased)
			{
				if ((bool)doc.VoidAppl == false)
				{
					release.SetEnabled(docNotOnHold && docOpen && CurrPaymentState.isCCCaptured);
				}
				else
				{
					//We should allow release if CCPayment has just pre-authorization - it will expire anyway.
					release.SetEnabled(docNotOnHold && docOpen && (isCCStateClear || (CurrPaymentState.isCCPreAuthorized && CurrPaymentState.isCCVoidingAttempted)));
				}
			}
			#endregion

			SetDocTypeList(e.Row);
			editCustomer.SetEnabled(customer?.Current != null);

			if (doc.CustomerID != null)
			{
				if (Adjustments_Raw.View.SelectSingleBound(new object[] { e.Row }) != null ||
					SOAdjustments_Raw.View.SelectSingleBound(new object[] { e.Row }) != null)
				{
					PXUIFieldAttribute.SetEnabled<ARPayment.customerID>(cache, doc, false);
				}
			}

			PXUIFieldAttribute.SetEnabled<ARPayment.cCPaymentStateDescr>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<ARPayment.depositDate>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<ARPayment.depositAsBatch>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<ARPayment.deposited>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<ARPayment.depositNbr>(cache, null, false);

			if (doc.CuryApplAmt == null)
			{
				RecalcApplAmounts(cache, doc);
			}

			if (doc.CurySOApplAmt == null)
			{
				RecalcSOApplAmounts(cache, doc);
			}

			bool isDeposited = string.IsNullOrEmpty(doc.DepositNbr) == false && string.IsNullOrEmpty(doc.DepositType) == false;
			bool enableDepositEdit = !isDeposited && cashAccount != null && (isClearingAccount || doc.DepositAsBatch != isClearingAccount);

			if (enableDepositEdit)
			{
				var exc = doc.DepositAsBatch != isClearingAccount ? new PXSetPropertyException(Messages.DocsDepositAsBatchSettingDoesNotMatchClearingAccountFlag, PXErrorLevel.Warning)
																  : null;

				cache.RaiseExceptionHandling<ARPayment.depositAsBatch>(doc, doc.DepositAsBatch, exc);
			}

			PXUIFieldAttribute.SetEnabled<ARPayment.depositAsBatch>(cache, doc, enableDepositEdit);
			PXUIFieldAttribute.SetEnabled<ARPayment.depositAfter>(cache, doc, !isDeposited && isClearingAccount && doc.DepositAsBatch == true);

			bool allowPaymentChargesEdit = doc.Released != true && (doc.DocType == ARDocType.Payment || doc.DocType == ARDocType.VoidPayment || doc.DocType == ARDocType.Prepayment);
			this.PaymentCharges.Cache.AllowInsert = allowPaymentChargesEdit;
			this.PaymentCharges.Cache.AllowUpdate = allowPaymentChargesEdit;
			this.PaymentCharges.Cache.AllowDelete = allowPaymentChargesEdit;

			bool reversalActionEnabled =
				doc.DocType != ARDocType.SmallBalanceWO
				&& doc.DocType != ARDocType.VoidPayment
				&& doc.Voided != true;

			reverseApplication.SetEnabled(reversalActionEnabled);

			#region Migration Mode Settings

			bool isMigratedDocument = doc.IsMigratedRecord == true;
			bool isUnreleasedMigratedDocument = isMigratedDocument && !docReleased;
			bool isReleasedMigratedDocument = isMigratedDocument && docReleased;
			bool isCuryInitDocBalEnabled = isUnreleasedMigratedDocument &&
				(doc.DocType == ARDocType.Payment || doc.DocType == ARDocType.Prepayment);

			PXUIFieldAttribute.SetVisible<ARPayment.curyUnappliedBal>(cache, doc, !isUnreleasedMigratedDocument);
			PXUIFieldAttribute.SetVisible<ARPayment.curyInitDocBal>(cache, doc, isUnreleasedMigratedDocument);
			PXUIFieldAttribute.SetVisible<ARPayment.displayCuryInitDocBal>(cache, doc, isReleasedMigratedDocument);
			PXUIFieldAttribute.SetEnabled<ARPayment.curyInitDocBal>(cache, doc, isCuryInitDocBalEnabled);

			if (isUnreleasedMigratedDocument)
			{
				Adjustments.Cache.AllowSelect =
				SOAdjustments.Cache.AllowSelect =
				PaymentCharges.Cache.AllowSelect = false;
			}

			bool disableCaches = arsetup.Current?.MigrationMode == true
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
				if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<ARPayment.curyInitDocBal>(cache, doc)))
				{
					cache.RaiseExceptionHandling<ARPayment.curyInitDocBal>(doc, doc.CuryInitDocBal,
						new PXSetPropertyException(Messages.EnterInitialBalanceForUnreleasedMigratedDocument, PXErrorLevel.Warning));
				}
			}
			else
			{
				cache.RaiseExceptionHandling<ARPayment.curyInitDocBal>(doc, doc.CuryInitDocBal, null);
			}
			#endregion

			CheckForUnreleasedIncomingApplications(cache, doc);

			#region Approval
			if (IsApprovalRequired(doc, cache))
			{
				if (doc.Status == ARDocStatus.PendingApproval || doc.Status == ARDocStatus.Rejected)
				{
					release.SetEnabled(false);
				}

				if (doc.DocType == ARDocType.Refund && (doc.Status == ARDocStatus.PendingApproval
														|| doc.Status == ARDocStatus.Rejected || doc.Status == ARDocStatus.Balanced && doc.DontApprove == false))
				{
					Adjustments.Cache.AllowInsert = false;
					Adjustments_History.Cache.AllowInsert = false;
					SOAdjustments.Cache.AllowInsert = false;
					PaymentCharges.Cache.AllowInsert = false;
					Approval.Cache.AllowInsert = false;
					Adjustments.Cache.AllowUpdate = false;
					Adjustments_History.Cache.AllowUpdate = false;
					SOAdjustments.Cache.AllowUpdate = false;
					PaymentCharges.Cache.AllowUpdate = false;
					Approval.Cache.AllowUpdate = false;
					Adjustments.Cache.AllowDelete = false;
					Adjustments_History.Cache.AllowDelete = false;
					SOAdjustments.Cache.AllowDelete = false;
					CurrentDocument.Cache.AllowDelete = false;
					PaymentCharges.Cache.AllowDelete = false;
					Approval.Cache.AllowDelete = false;
				}

				if (doc.DocType == ARDocType.Refund)
				{
					if ((doc.Status == ARDocStatus.PendingApproval || doc.Status == ARDocStatus.Rejected ||
						 doc.Status == ARDocStatus.Closed || doc.Status == ARDocStatus.Balanced) && doc.DontApprove == false)
					{
						PXUIFieldAttribute.SetEnabled(cache, doc, false);
					}
					if (doc.Status == ARDocStatus.PendingApproval || doc.Status == ARDocStatus.Rejected ||
						doc.Status == ARDocStatus.Balanced)
					{
						PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, doc, true);
						cache.AllowDelete = true;
					}
				}
			}
			PXUIFieldAttribute.SetEnabled<ARPayment.docType>(cache, doc, true);
			PXUIFieldAttribute.SetEnabled<ARPayment.refNbr>(cache, doc, true);
			#endregion
		}

		protected virtual void CheckForUnreleasedIncomingApplications(PXCache sender, ARPayment document)
		{
			if (document.Released != true || document.OpenDoc != true)
			{
				return;
			}

			ARAdjust unreleasedIncomingApplication = PXSelect<
				ARAdjust,
				Where<
					ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>,
					And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>,
					And<ARAdjust.released, NotEqual<True>>>>>
				.Select(this, document.DocType, document.RefNbr);

			sender.ClearFieldErrors<ARPayment.refNbr>(document);

			if (unreleasedIncomingApplication != null)
			{
				sender.DisplayFieldWarning<ARPayment.refNbr>(
					document,
					null,
					PXLocalizer.LocalizeFormat(
						Common.Messages.CannotApplyDocumentUnreleasedIncomingApplicationsExist,
						GetLabel.For<ARDocType>(unreleasedIncomingApplication.AdjgDocType),
						unreleasedIncomingApplication.AdjgRefNbr));

				Adjustments.Cache.AllowInsert =
				Adjustments.Cache.AllowUpdate =
				Adjustments.Cache.AllowDelete =
				SOAdjustments.Cache.AllowInsert =
				SOAdjustments.Cache.AllowUpdate =
				SOAdjustments.Cache.AllowDelete = false;

				loadInvoices.SetEnabled(false);
				loadOrders.SetEnabled(false);
			}
		}

		protected virtual void ARPayment_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
		{
			ARPayment row = e.Row as ARPayment;

			if (row != null && (row.CuryApplAmt == null || row.CurySOApplAmt == null) && !e.IsReadOnly)
			{
				using (new PXConnectionScope())
				{
					RecalcApplAmounts(sender, row);
					RecalcSOApplAmounts(sender, row);
				}
			}
		}
		public virtual void RecalcApplAmounts(PXCache sender, ARPayment row)
		{
			bool IsReadOnly = (sender.GetStatus(row) == PXEntryStatus.Notchanged);

			PXFormulaAttribute.CalcAggregate<ARAdjust.curyAdjgAmt>(Adjustments.Cache, row, IsReadOnly);
			sender.RaiseFieldUpdated<ARPayment.curyApplAmt>(row, null);

			PXDBCurrencyAttribute.CalcBaseValues<ARPayment.curyApplAmt>(sender, row);
			PXDBCurrencyAttribute.CalcBaseValues<ARPayment.curyUnappliedBal>(sender, row);

			PXFormulaAttribute.CalcAggregate<ARAdjust.curyAdjgWOAmt>(Adjustments.Cache, row, IsReadOnly);
			PXDBCurrencyAttribute.CalcBaseValues<ARPayment.curyWOAmt>(sender, row);
		}

		public virtual void RecalcSOApplAmounts(PXCache sender, ARPayment row)
		{
			bool IsReadOnly = (sender.GetStatus(row) == PXEntryStatus.Notchanged);

			PXFormulaAttribute.CalcAggregate<SOAdjust.curyAdjgAmt>(SOAdjustments.Cache, row, IsReadOnly);
			sender.RaiseFieldUpdated<ARPayment.curySOApplAmt>(row, null);

			PXDBCurrencyAttribute.CalcBaseValues<ARPayment.curySOApplAmt>(sender, row);
			PXDBCurrencyAttribute.CalcBaseValues<ARPayment.curyUnappliedBal>(sender, row);
		}

		public static void SetDocTypeList(PXCache cache, string docType)
		{
			string defValue = ARDocType.Invoice;
			List<string> values = new List<string>();
			List<string> labels = new List<string>();

			if (docType == ARDocType.Refund)
			{
				defValue = ARDocType.CreditMemo;
				values.AddRange(new string[] { ARDocType.CreditMemo, ARDocType.Payment, ARDocType.Prepayment });
				labels.AddRange(new string[] { Messages.CreditMemo, Messages.Payment, Messages.Prepayment });
			}
			else if (docType == ARDocType.Payment || docType == ARDocType.VoidPayment)
			{
				values.AddRange(new string[] { ARDocType.Invoice, ARDocType.DebitMemo, ARDocType.CreditMemo, ARDocType.FinCharge });
				labels.AddRange(new string[] { Messages.Invoice, Messages.DebitMemo, Messages.CreditMemo, Messages.FinCharge });
			}
			else
			{
				values.AddRange(new string[] { ARDocType.Invoice, ARDocType.DebitMemo, ARDocType.FinCharge });
				labels.AddRange(new string[] { Messages.Invoice, Messages.DebitMemo, Messages.FinCharge });
			}

			if (!PXAccess.FeatureInstalled<FeaturesSet.overdueFinCharges>() && values.Contains(ARDocType.FinCharge) && labels.Contains(Messages.FinCharge))
			{
				values.Remove(ARDocType.FinCharge);
				labels.Remove(Messages.FinCharge);
			}

			PXDefaultAttribute.SetDefault<ARAdjust.adjdDocType>(cache, defValue);
			PXStringListAttribute.SetList<ARAdjust.adjdDocType>(cache, null, values.ToArray(), labels.ToArray());
		}

		private void SetDocTypeList(object Row)
		{
			ARPayment row = Row as ARPayment;
			if (row != null)
			{
				SetDocTypeList(Adjustments.Cache, row.DocType);
			}
		}

		protected virtual void ARPayment_Hold_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			PXBoolAttribute.ConvertValue(e);
			if (e.Row != null && e.NewValue != null)
			{
				if ((bool)e.NewValue && ((ARPayment)e.Row).Status == "B")
				{
					sender.SetValue<ARPayment.status>(e.Row, "H");
				}
				else if (!(bool)e.NewValue && ((ARPayment)e.Row).Status == "H")
				{
					sender.SetValue<ARPayment.status>(e.Row, "B");
				}
			}
		}

		protected virtual void ARPayment_DocDate_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (((ARPayment)e.Row).Released == false)
			{
				e.NewValue = ((ARPayment)e.Row).AdjDate;
				e.Cancel = true;
			}
		}

		protected virtual void ARPayment_DocDate_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			ARPayment row = e.Row as ARPayment;
			if (row == null) return;
			PaymentState CurrPaymentState = new PaymentState(ccProcTran.Select());
			if (row.VoidAppl == false)
			{
				if (CurrPaymentState.isCCCaptured && CurrPaymentState.lastTran != null && DateTime.Compare(((DateTime?)e.NewValue).Value, CurrPaymentState.lastTran.EndTime.Value.Date) != 0)
				{
					sender.RaiseExceptionHandling<ARPayment.docDate>(row, CurrPaymentState.lastTran.EndTime.Value.Date, new PXSetPropertyException(Messages.PaymentAndCaptureDatesDifferent, PXErrorLevel.Warning));
				}
			}
			else
			{
				if (CurrPaymentState.isCCRefunded && CurrPaymentState.lastTran != null && DateTime.Compare(((DateTime?)e.NewValue).Value, CurrPaymentState.lastTran.EndTime.Value.Date) != 0)
				{
					sender.RaiseExceptionHandling<ARPayment.docDate>(row, CurrPaymentState.lastTran.EndTime.Value.Date, new PXSetPropertyException(Messages.PaymentAndCaptureDatesDifferent, PXErrorLevel.Warning));
				}
			}
		}

		protected virtual void ARPayment_FinPeriodID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if ((bool)((ARPayment)e.Row).Released == false)
			{
				e.NewValue = ((ARPayment)e.Row).AdjFinPeriodID;
				e.Cancel = true;
			}
		}

		protected virtual void ARPayment_TranPeriodID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if ((bool)((ARPayment)e.Row).Released == false)
			{
				e.NewValue = ((ARPayment)e.Row).AdjTranPeriodID;
				e.Cancel = true;
			}
		}

		protected virtual void ARPayment_DepositAfter_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			ARPayment row = (ARPayment)e.Row;
			if ((row.DocType == ARDocType.Payment || row.DocType == ARDocType.Prepayment || row.DocType == ARDocType.CashSale || row.DocType == ARDocType.Refund)
				&& row.DepositAsBatch == true)
			{
				e.NewValue = row.AdjDate;
				e.Cancel = true;
			}
		}

		protected virtual void ARPayment_DepositAsBatch_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARPayment row = (ARPayment)e.Row;
			if ((row.DocType == ARDocType.Payment || row.DocType == ARDocType.Prepayment || row.DocType == ARDocType.CashSale || row.DocType == ARDocType.Refund))
			{
				sender.SetDefaultExt<ARPayment.depositAfter>(e.Row);
			}
		}
		protected virtual void ARPayment_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			ARCustomerCreditHelper.UpdateARBalances(sender, null, e.Row);
		}

		protected virtual void ARPayment_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			ARPayment payment = (ARPayment)e.Row;
			if (payment.Released == false)
			{
				payment.DocDate = payment.AdjDate;
				payment.FinPeriodID = payment.AdjFinPeriodID;
				payment.TranPeriodID = payment.AdjTranPeriodID;

				sender.RaiseExceptionHandling<ARPayment.finPeriodID>(e.Row, payment.FinPeriodID, null);
			}
			
			ARCustomerCreditHelper.UpdateARBalances(sender, e.Row, null);
		}

		protected virtual void ARPayment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			ARPayment payment = (ARPayment)e.Row;
			PaymentState CurrPaymentState = new PaymentState(ccProcTran.Select());
			if (payment.Released != true
				&& (((ARSetup)arsetup.Select()).IntegratedCCProcessing != true
					|| !CurrPaymentState.isCCCaptured && !CurrPaymentState.isCCRefunded))
			{
				payment.DocDate = payment.AdjDate;
				payment.FinPeriodID = payment.AdjFinPeriodID;
				payment.TranPeriodID = payment.AdjTranPeriodID;

				sender.RaiseExceptionHandling<ARPayment.finPeriodID>(e.Row, payment.FinPeriodID, null);
			}

			if (payment.OpenDoc == true && payment.Hold != true)
			{
				if (IsPaymentUnbalanced(payment))
				{
					sender.RaiseExceptionHandling<ARPayment.curyOrigDocAmt>(payment, payment.CuryOrigDocAmt, new PXSetPropertyException(Messages.DocumentOutOfBalance));
				}
				else
				{
					sender.RaiseExceptionHandling<ARPayment.curyOrigDocAmt>(payment, null, null);
				}
			}

			ARCustomerCreditHelper.UpdateARBalances(sender, e.Row, e.OldRow);
		}

		public virtual bool IsPaymentUnbalanced(ARPayment payment)
		{
			// It's should be allowed to enter a CustomerRefund 
			// document without any required applications, 
			// when migration mode is activated.
			// 
			bool canHaveBalance = payment.CanHaveBalance == true ||
				payment.IsMigratedRecord == true && payment.CuryInitDocBal == 0m;

			return
				canHaveBalance && payment.VoidAppl != true
			&& (payment.CuryUnappliedBal < 0m
				|| payment.CuryApplAmt < 0m && payment.CuryUnappliedBal > payment.CuryOrigDocAmt
				|| payment.CuryOrigDocAmt < 0m)
				|| !canHaveBalance && payment.CuryUnappliedBal != 0m && payment.SelfVoidingDoc != true;
		}

		public virtual void CreatePayment(ARInvoice ardoc)
		{
			CreatePayment(ardoc, null, null, null, true);
		}
		public virtual void CreatePayment(ARInvoice ardoc, CurrencyInfo info, DateTime? paymentDate, string aFinPeriod, bool overrideDesc)
		{
			ARPayment payment = this.Document.Current;
			CCProcTran authTran = FindPreAuthTran(this, ardoc, Decimal.Zero); //Check if any preauth exists
			if (payment == null || object.Equals(payment.CustomerID, ardoc.CustomerID) == false
				|| (ardoc.PMInstanceID != null && payment.PMInstanceID != ardoc.PMInstanceID) || authTran != null)
			{
				this.Clear();
				if (info != null)
				{
					info.CuryInfoID = null;
					info = PXCache<CurrencyInfo>.CreateCopy(this.currencyinfo.Insert(info));
				}
				payment = PXCache<ARPayment>.CreateCopy(this.Document.Insert(new ARPayment()));
				if (info != null)
					payment.CuryInfoID = info.CuryInfoID;

				payment.DocType = ARDocType.Payment;
				payment.CustomerID = ardoc.CustomerID;
				payment.CustomerLocationID = ardoc.CustomerLocationID;
				if (overrideDesc)
				{
					payment.DocDesc = ardoc.DocDesc;
				}

				if (paymentDate.HasValue)
				{
					payment.AdjDate = paymentDate;
				}
				else
				{
					payment.AdjDate = (DateTime.Compare((DateTime)this.Accessinfo.BusinessDate, (DateTime)ardoc.DocDate) < 0 ? ardoc.DocDate : this.Accessinfo.BusinessDate);
				}
				if (!String.IsNullOrEmpty(aFinPeriod))
					payment.AdjFinPeriodID = aFinPeriod;

				if (string.IsNullOrEmpty(ardoc.PaymentMethodID) == false)
				{
					payment.PaymentMethodID = ardoc.PaymentMethodID;
				}

				if (ardoc.PMInstanceID != null)
				{
					payment.PMInstanceID = ardoc.PMInstanceID;
				}

				if (ardoc.CashAccountID != null)
				{
					payment.CashAccountID = ardoc.CashAccountID;
				}

				payment = this.Document.Update(payment);

				if (info != null)
				{
					CurrencyInfo b_info = (CurrencyInfo)PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<ARPayment.curyInfoID>>>>.Select(this, null);
					b_info.CuryID = info.CuryID;
					b_info.CuryEffDate = info.CuryEffDate;
					b_info.CuryRateTypeID = info.CuryRateTypeID;
					b_info.CuryRate = info.CuryRate;
					b_info.RecipRate = info.RecipRate;
					b_info.CuryMultDiv = info.CuryMultDiv;
					this.currencyinfo.Update(b_info);
				}
			}

			ARAdjust adj = new ARAdjust();
			adj.AdjdDocType = ardoc.DocType;
			adj.AdjdRefNbr = ardoc.RefNbr;

			//set origamt to zero to apply "full" amounts to invoices.
			Document.Current.CuryOrigDocAmt = 0m;
			Document.Update(Document.Current);

			this.Adjustments.Insert(adj);

			decimal? CuryApplAmt = Document.Current.CuryApplAmt;

			Document.Current.CuryOrigDocAmt = CuryApplAmt;
			Document.Update(Document.Current);

			//Search for auth transactions
			if (ardoc.PMInstanceID != null)
			{
				authTran = FindPreAuthTran(this, ardoc, CuryApplAmt.Value);
				if (authTran != null)
				{
					CCProcTran attachedTran = this.ccAttachedTran.Search<CCProcTran.tranNbr>(authTran.TranNbr);
					attachedTran.DocType = payment.DocType;
					attachedTran.RefNbr = payment.RefNbr;
					attachedTran = this.ccAttachedTran.Update(attachedTran);
				}
			}
		}

		public static CCProcTran FindPreAuthTran(PXGraph aGraph, ARInvoice aARDoc, Decimal aRequiredAmount)
		{
			CCProcTran authTran = null;
			if (aARDoc.PMInstanceID != null)
			{
				PXSelectBase<CCProcTran> authTranSelect = new PXSelect<CCProcTran, Where<CCProcTran.origDocType, Equal<Required<CCProcTran.origDocType>>,
																					And<CCProcTran.origRefNbr, Equal<Required<CCProcTran.origRefNbr>>,
																					And<CCProcTran.refNbr, IsNull>>>, OrderBy<Desc<CCProcTran.tranNbr>>>(aGraph);
				foreach (PXResult<SO.SOOrder, ARTran> it in PXSelectJoin<SO.SOOrder, InnerJoin<ARTran,
								On<ARTran.sOOrderType, Equal<SO.SOOrder.orderType>,
								And<ARTran.sOOrderNbr, Equal<SO.SOOrder.orderNbr>>>>,
							Where<ARTran.tranType, Equal<Required<ARTran.tranType>>,
							And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>>.Select(aGraph, aARDoc.DocType, aARDoc.RefNbr))
				{
					SO.SOOrder soDoc = (SO.SOOrder)it;
					if (soDoc.PMInstanceID == aARDoc.PMInstanceID)
					{
						CCProcTran tran = CCProcTranHelper.FindCCPreAthorizing(authTranSelect.Select(soDoc.OrderType, soDoc.OrderNbr));
						if (tran != null && tran.Amount >= aRequiredAmount)
						{
							if (authTran != null)
							{
								if (tran.Amount < authTran.Amount) //Searching authorized amt closest to required
									authTran = tran;
							}
							else
							{
								authTran = tran;
							}
						}
					}
				}
			}
			return authTran;
		}
		#region BusinessProcs

		private bool _IsVoidCheckInProgress = false;

		protected virtual void ARPayment_RefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (_IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}
		}

		protected virtual void ARPayment_FinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			ARPayment doc;
			if (_IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}
			else
			if ((doc = e.Row as ARPayment) != null)
			{
				PaymentState ps = new PaymentState(ccProcTran.Select());

				if ((doc.Released == true || ps.isCCCaptured) && doc.AdjFinPeriodID.CompareTo((string)e.NewValue) < 0)
				{
					e.NewValue = FinPeriodIDAttribute.FormatForDisplay((string)e.NewValue);
					throw new PXSetPropertyException(CS.Messages.Entry_LE, FinPeriodIDAttribute.FormatForError(doc.AdjFinPeriodID));
				}
			}
		}

		protected virtual void ARPayment_AdjFinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (_IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}

			ARPayment doc = (ARPayment)e.Row;

			string errMsg;
			if (!VerifyAdjFinPeriodID(doc, (string)e.NewValue, out errMsg))
			{
				e.NewValue = FinPeriodIDAttribute.FormatForDisplay((string)e.NewValue);
				throw new PXSetPropertyException(errMsg);
			}
		}

		protected virtual bool VerifyAdjFinPeriodID(ARPayment doc, string newValue, out string errMsg)
		{
			errMsg = null;
			PaymentState ps = new PaymentState(ccProcTran.Select());

			if ((doc.Released == true || ps.isCCCaptured) &&
				doc.FinPeriodID.CompareTo(newValue) > 0)
			{
				errMsg = string.Format(CS.Messages.Entry_GE, FinPeriodIDAttribute.FormatForError(doc.FinPeriodID));
				return false;
			}

			if (doc.DocType == ARDocType.VoidPayment)
			{
				ARPayment orig_payment = PXSelect<ARPayment,
					Where2<Where<ARPayment.docType, Equal<ARDocType.payment>,
							Or<ARPayment.docType, Equal<ARDocType.prepayment>>>,
						And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>
					.SelectSingleBound(this, null, doc.RefNbr);

				if (orig_payment != null && orig_payment.FinPeriodID.CompareTo(newValue) > 0)
				{
					errMsg = string.Format(CS.Messages.Entry_GE, FinPeriodIDAttribute.FormatForError(orig_payment.FinPeriodID));
					return false;
				}
			}
			else
			{
				try
				{
					internalCall = true;

					/// We should find maximal adjusting period of adjusted applications 
					/// (excluding applications, that have been reversed in the same period)
					/// for the document, because applications in earlier period are not allowed.
					/// 
					ARAdjust adjdmax = PXSelectJoin<ARAdjust,
						LeftJoin<ARAdjust2, On<
							ARAdjust2.adjdDocType, Equal<ARAdjust.adjdDocType>,
							And<ARAdjust2.adjdRefNbr, Equal<ARAdjust.adjdRefNbr>,
							And<ARAdjust2.adjgDocType, Equal<ARAdjust.adjgDocType>,
							And<ARAdjust2.adjgRefNbr, Equal<ARAdjust.adjgRefNbr>,
							And<ARAdjust2.adjNbr, NotEqual<ARAdjust.adjNbr>,
							And<Switch<Case<Where<ARAdjust.voidAdjNbr, IsNotNull>, ARAdjust.voidAdjNbr>, ARAdjust.adjNbr>,
								Equal<Switch<Case<Where<ARAdjust.voidAdjNbr, IsNotNull>, ARAdjust2.adjNbr>, ARAdjust2.voidAdjNbr>>,
							And<ARAdjust2.adjgFinPeriodID, Equal<ARAdjust.adjgFinPeriodID>,
							And<ARAdjust2.released, Equal<True>,
							And<ARAdjust2.voided, Equal<True>,
							And<ARAdjust.voided, Equal<True>>>>>>>>>>>>,
					Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>,
						And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>,
							And<ARAdjust.released, Equal<True>,
							And<ARAdjust2.adjdRefNbr, IsNull>>>>,
						OrderBy<Desc<ARAdjust.adjgFinPeriodID>>>
						.SelectSingleBound(this, null, doc.DocType, doc.RefNbr);

					if (adjdmax?.AdjgFinPeriodID.CompareTo(newValue) > 0)
					{
						errMsg = string.Format(CS.Messages.Entry_GE, FinPeriodIDAttribute.FormatForError(adjdmax.AdjgFinPeriodID));
						return false;
					}
				}
				finally
				{
					internalCall = false;
				}
			}

			return true;
		}

		public virtual void VoidCheckProcExt(ARPayment doc)
		{
			try
			{
				_IsVoidCheckInProgress = true;
				this.VoidCheckProc(doc);
			}
			finally
			{
				_IsVoidCheckInProgress = false;
			}
		}

		public virtual void SelfVoidingProc(ARPayment doc)
		{
			ARPayment payment = PXCache<ARPayment>.CreateCopy(doc);

			if (payment.OpenDoc == false)
			{
				payment.OpenDoc = true;
				Document.Cache.RaiseRowSelected(payment);
			}

			foreach (PXResult<ARAdjust> adjres in PXSelectJoin<ARAdjust,
				InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<ARAdjust.adjdCuryInfoID>>>,
				Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>,
					And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>,
					And<ARAdjust.voided, NotEqual<True>>>>>.Select(this, payment.DocType, payment.RefNbr))
			{
				CreateReversingApp(adjres, payment);
			}

			payment.CuryApplAmt = null;
			payment.CuryUnappliedBal = null;
			payment.CuryWOAmt = null;

			Document.Update(payment);
		}

		public virtual ARAdjust CreateReversingApp(ARAdjust adj, ARPayment payment)
		{
			ARAdjust adjold = PXCache<ARAdjust>.CreateCopy(adj);

			adjold.Voided = true;
			adjold.VoidAdjNbr = adjold.AdjNbr;
			adjold.Released = false;
			Adjustments.Cache.SetDefaultExt<ARAdjust.isMigratedRecord>(adjold);
			adjold.AdjNbr = payment.AdjCntr;
			adjold.AdjBatchNbr = null;
			adjold.StatementDate = null;

			ARAdjust adjnew = new ARAdjust();
			adjnew.AdjgDocType = adjold.AdjgDocType;
			adjnew.AdjgRefNbr = adjold.AdjgRefNbr;
			adjnew.AdjgBranchID = adjold.AdjgBranchID;
			adjnew.AdjdDocType = adjold.AdjdDocType;
			adjnew.AdjdRefNbr = adjold.AdjdRefNbr;
			adjnew.AdjdBranchID = adjold.AdjdBranchID;
			adjnew.CustomerID = adjold.CustomerID;
			adjnew.AdjdCustomerID = adjold.AdjdCustomerID;
			adjnew.AdjNbr = adjold.AdjNbr;
			adjnew.AdjdCuryInfoID = adjold.AdjdCuryInfoID;
			adjnew.AdjdHasPPDTaxes = adj.AdjdHasPPDTaxes;

			_AutoPaymentApp = true;
			adjnew = Adjustments.Insert(adjnew);

			if (adjnew != null)
			{
				adjold.CuryAdjgAmt = -1 * adjold.CuryAdjgAmt;
				adjold.CuryAdjgDiscAmt = -1 * adjold.CuryAdjgDiscAmt;
				adjold.CuryAdjgPPDAmt = -1 * adjold.CuryAdjgPPDAmt;
				adjold.CuryAdjgWOAmt = -1 * adjold.CuryAdjgWOAmt;
				adjold.AdjAmt = -1 * adjold.AdjAmt;
				adjold.AdjDiscAmt = -1 * adjold.AdjDiscAmt;
				adjold.AdjPPDAmt = -1 * adjold.AdjPPDAmt;
				adjold.AdjWOAmt = -1 * adjold.AdjWOAmt;
				adjold.CuryAdjdAmt = -1 * adjold.CuryAdjdAmt;
				adjold.CuryAdjdDiscAmt = -1 * adjold.CuryAdjdDiscAmt;
				adjold.CuryAdjdPPDAmt = -1 * adjold.CuryAdjdPPDAmt;
				adjold.CuryAdjdWOAmt = -1 * adjold.CuryAdjdWOAmt;
				adjold.RGOLAmt = -1 * adjold.RGOLAmt;
				adjold.AdjgCuryInfoID = payment.CuryInfoID;
			}

			Adjustments.Update(adjold);
			_AutoPaymentApp = false;

			return adjnew;
		}

		public virtual void VoidCheckProc(ARPayment doc)
		{
			this.Clear(PXClearOption.PreserveTimeStamp);

			foreach (PXResult<ARPayment, CurrencyInfo, Currency, Customer> res in ARPayment_CurrencyInfo_Currency_Customer.Select(this, (object)doc.DocType, doc.RefNbr, doc.CustomerID))
			{
				CurrencyInfo info = PXCache<CurrencyInfo>.CreateCopy((CurrencyInfo)res);
				info.CuryInfoID = null;
				info.IsReadOnly = false;
				info = PXCache<CurrencyInfo>.CreateCopy(this.currencyinfo.Insert(info));

				ARPayment payment = new ARPayment();
				payment.DocType = ((ARPayment)res).DocType;
				payment.RefNbr = ((ARPayment)res).RefNbr;
				payment.CuryInfoID = info.CuryInfoID;
				payment.VoidAppl = true;

				Document.Insert(payment);

				payment = PXCache<ARPayment>.CreateCopy((ARPayment)res);

				payment.CuryInfoID = info.CuryInfoID;
				payment.VoidAppl = true;
				payment.CATranID = null;

				// Must be set for RowSelected event handler
				payment.OpenDoc = true;
				payment.Released = false;
				Document.Cache.SetDefaultExt<ARPayment.hold>(payment);
				Document.Cache.SetDefaultExt<ARPayment.isMigratedRecord>(payment);
				payment.LineCntr = 0;
				payment.AdjCntr = 0;
				payment.BatchNbr = null;
				payment.CuryOrigDocAmt = -1 * payment.CuryOrigDocAmt;
				payment.OrigDocAmt = -1 * payment.OrigDocAmt;
				payment.CuryInitDocBal = -1 * payment.CuryInitDocBal;
				payment.InitDocBal = -1 * payment.InitDocBal;
				payment.CuryChargeAmt = 0;
				payment.CuryConsolidateChargeTotal = 0;
				payment.CuryApplAmt = null;
				payment.CuryUnappliedBal = null;
				payment.CuryWOAmt = null;
				payment.DocDate = doc.DocDate;
				payment.AdjDate = doc.DocDate;
				payment.AdjFinPeriodID = doc.AdjFinPeriodID;
				payment.AdjTranPeriodID = doc.AdjTranPeriodID;
				payment.StatementDate = null;

				string paymentMethod = payment.PaymentMethodID;
				if (payment.DepositAsBatch == true)
				{
					if (!String.IsNullOrEmpty(payment.DepositNbr))
					{
						PaymentMethod pm = PXSelectReadonly<PaymentMethod,
									Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Select(this, payment.PaymentMethodID);
						bool voidOnDepositAccount = pm.ARVoidOnDepositAccount ?? false;
						if (!voidOnDepositAccount)
						{
							if (payment.Deposited == false)
							{
								throw new PXException(Messages.ARPaymentIsIncludedIntoCADepositAndCannotBeVoided);
							}
							PXResult<CADeposit, CashAccount> depositRes = (PXResult<CADeposit, CashAccount>)PXSelectJoin<CADeposit, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CADeposit.cashAccountID>>>,
												Where<CADeposit.tranType, Equal<Required<CADeposit.tranType>>,
													And<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>>>>.Select(this, payment.DepositType, payment.DepositNbr);

							if (depositRes == null)
							{
								throw new PXException(Messages.PaymentRefersToInvalidDeposit, GetLabel.For<DepositType>(payment.DepositType), payment.DepositNbr);
							}
							CADeposit deposit = depositRes;
							payment.CashAccountID = deposit.CashAccountID;
						}
						else
						{
							payment.DepositType = null;
							payment.DepositNbr = null;
							payment.Deposited = false;
						}
					}
				}

				payment = this.Document.Update(payment);
				if (payment.PaymentMethodID != paymentMethod)
				{
					payment.PaymentMethodID = paymentMethod;
					payment = this.Document.Update(payment);
				}

				this.Document.Cache.SetValueExt<ARPayment.adjFinPeriodID>(payment, FinPeriodIDAttribute.FormatForDisplay(doc.AdjFinPeriodID));

				if (info != null)
				{
					CurrencyInfo b_info = (CurrencyInfo)PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<ARPayment.curyInfoID>>>>.Select(this, null);
					b_info.CuryID = info.CuryID;
					b_info.CuryEffDate = info.CuryEffDate;
					b_info.CuryRateTypeID = info.CuryRateTypeID;
					b_info.CuryRate = info.CuryRate;
					b_info.RecipRate = info.RecipRate;
					b_info.CuryMultDiv = info.CuryMultDiv;
					this.currencyinfo.Update(b_info);
				}
			}

			foreach (PXResult<ARAdjust, CurrencyInfo> adjres in PXSelectJoin<ARAdjust,
				InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<ARAdjust.adjdCuryInfoID>>>,
				Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>,
					And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>,
					And<ARAdjust.voided, NotEqual<True>,
					And<ARAdjust.isInitialApplication, NotEqual<True>>>>>>.Select(this, doc.DocType, doc.RefNbr))
			{
				ARAdjust adj = PXCache<ARAdjust>.CreateCopy((ARAdjust)adjres);

				if ((doc.DocType != ARDocType.CreditMemo || doc.PendingPPD != true) &&
					adj.AdjdHasPPDTaxes == true &&
					adj.PendingPPD != true)
				{
					ARAdjust adjPPD = GetPPDApplication(this, adj.AdjdDocType, adj.AdjdRefNbr);
					if (adjPPD != null && (adjPPD.AdjgDocType != adj.AdjgDocType || adjPPD.AdjgRefNbr != adj.AdjgRefNbr))
					{
						adj = adjres;
						this.Clear();
						adj = (ARAdjust)Adjustments.Cache.Update(adj);
						Document.Current = Document.Search<ARPayment.refNbr>(adj.AdjgRefNbr, adj.AdjgDocType);
						Adjustments.Cache.RaiseExceptionHandling<ARAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr,
							new PXSetPropertyException(Messages.PPDApplicationExists, PXErrorLevel.RowError, adjPPD.AdjgRefNbr));

						throw new PXSetPropertyException(Messages.PPDApplicationExists, adjPPD.AdjgRefNbr);
					}
				}

				adj.VoidAppl = true;
				adj.Released = false;
				Adjustments.Cache.SetDefaultExt<ARAdjust.isMigratedRecord>(adj);
				adj.VoidAdjNbr = adj.AdjNbr;
				adj.AdjNbr = 0;
				adj.AdjBatchNbr = null;
				adj.StatementDate = null;

				ARAdjust adjnew = new ARAdjust();
				adjnew.AdjgDocType = adj.AdjgDocType;
				adjnew.AdjgRefNbr = adj.AdjgRefNbr;
				adjnew.AdjgBranchID = adj.AdjgBranchID;
				adjnew.AdjdDocType = adj.AdjdDocType;
				adjnew.AdjdRefNbr = adj.AdjdRefNbr;
				adjnew.AdjdBranchID = adj.AdjdBranchID;
				adjnew.CustomerID = adj.CustomerID;
				adjnew.AdjdCustomerID = adj.AdjdCustomerID;
				adjnew.AdjdCuryInfoID = adj.AdjdCuryInfoID;
				adjnew.AdjdHasPPDTaxes = adj.AdjdHasPPDTaxes;

				if (this.Adjustments.Insert(adjnew) == null)
				{
					adj = (ARAdjust)adjres;
					this.Clear();
					adj = (ARAdjust)this.Adjustments.Cache.Update(adj);
					this.Document.Current = this.Document.Search<ARPayment.refNbr>(adj.AdjgRefNbr, adj.AdjgDocType);
					this.Adjustments.Cache.RaiseExceptionHandling<ARAdjust.adjdRefNbr>(adj, adj.AdjdRefNbr, new PXSetPropertyException(Messages.MultipleApplicationError, PXErrorLevel.RowError));

					throw new PXException(Messages.MultipleApplicationError);
				}

				adj.CuryAdjgAmt = -1 * adj.CuryAdjgAmt;
				adj.CuryAdjgDiscAmt = -1 * adj.CuryAdjgDiscAmt;
				adj.CuryAdjgPPDAmt = -1 * adj.CuryAdjgPPDAmt;
				adj.CuryAdjgWOAmt = -1 * adj.CuryAdjgWOAmt;
				adj.AdjAmt = -1 * adj.AdjAmt;
				adj.AdjDiscAmt = -1 * adj.AdjDiscAmt;
				adj.AdjPPDAmt = -1 * adj.AdjPPDAmt;
				adj.AdjWOAmt = -1 * adj.AdjWOAmt;
				adj.CuryAdjdAmt = -1 * adj.CuryAdjdAmt;
				adj.CuryAdjdDiscAmt = -1 * adj.CuryAdjdDiscAmt;
				adj.CuryAdjdPPDAmt = -1 * adj.CuryAdjdPPDAmt;
				adj.CuryAdjdWOAmt = -1 * adj.CuryAdjdWOAmt;
				adj.RGOLAmt = -1 * adj.RGOLAmt;
				adj.AdjgCuryInfoID = this.Document.Current.CuryInfoID;

				this.Adjustments.Update(adj);
			}
			PaymentCharges.ReverseCharges(doc, Document.Current);
		}

		#endregion

		public class CurrencyInfoSelect : PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>>
		{
			public class PXView : Data.PXView
			{
				protected Dictionary<long?, object> _cached = new Dictionary<long?, object>();

				public PXView(PXGraph graph, bool isReadOnly, BqlCommand select)
					: base(graph, isReadOnly, select)
				{
				}

				public PXView(PXGraph graph, bool isReadOnly, BqlCommand select, Delegate handler)
					: base(graph, isReadOnly, select, handler)
				{
				}

				public override object SelectSingle(params object[] parameters)
				{
					object result = null;
					if (!_cached.TryGetValue((long?)parameters[0], out result))
					{
						result = base.SelectSingle(parameters);

						if (result != null)
						{
							_cached.Add((long?)parameters[0], result);
						}
					}

					return result;
				}

				public override List<object> SelectMulti(params object[] parameters)
				{
					List<object> ret = new List<object>();

					object item;
					if ((item = SelectSingle(parameters)) != null)
					{
						ret.Add(item);
					}

					return ret;
				}

				public override void Clear()
				{
					_cached.Clear();
					base.Clear();
				}
			}


			public CurrencyInfoSelect(PXGraph graph)
				: base(graph)
			{
				View = new PXView(graph, false, new Select<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>>());

				graph.RowDeleted.AddHandler<CurrencyInfo>(CurrencyInfo_RowDeleted);
			}

			public virtual void CurrencyInfo_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
			{
				View.Clear();
			}
		}
	}
}
