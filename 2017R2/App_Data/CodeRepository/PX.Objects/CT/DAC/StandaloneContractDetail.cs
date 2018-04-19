using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;

namespace PX.Objects.CT.Standalone
{
	public class ContractDetail : IBqlTable
	{

		#region ContractDetailID
		public abstract class contractDetailID : IBqlField
		{
		}
		[PXDBIdentity(IsKey = true)]
		[PXUIField(DisplayName = "Contract Detail ID")]
		public virtual Int32? ContractDetailID
		{
			get;
			set;
		}
		#endregion
		#region ContractID
		public abstract class contractID : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Contract ID")]
		public virtual Int32? ContractID
		{
			get;
			set;
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : IBqlField
		{
		}
		[PXDBInt]
		public virtual Int32? LineNbr
		{
			get;
			set;
		}
		#endregion
		#region RevID
		public abstract class revID : IBqlField
		{
		}
		[PXDBInt(MinValue = 1, IsKey = true)]
		[PXUIField(DisplayName = "Revision Number")]
		public virtual int? RevID
		{
			get;
			set;
		}
		#endregion
		#region Qty
		public abstract class qty : IBqlField
		{
		}
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Quantity")]
		public virtual Decimal? Qty
		{
			get;
			set;
		}
		#endregion
		#region FixedRecurringPrice
		public abstract class fixedRecurringPrice : IBqlField { }
		[PXUIField(DisplayName = "Price/Percent")]
		[PXDBDecimal(6)]
		public decimal? FixedRecurringPrice
		{
			get;
			set;
		}
		#endregion
		#region ContractItemID
		public abstract class contractItemID : IBqlField
		{
		}
		[PXDBInt()]
		[PXDefault()]
		[PXDimensionSelector(ContractItemAttribute.DimensionName, typeof(Search<ContractItem.contractItemID>),
																	typeof(ContractItem.contractItemCD),
																	typeof(ContractItem.contractItemCD), typeof(ContractItem.descr), DescriptionField = typeof(ContractItem.descr))]
		[PXUIField(DisplayName = "Item Code")]
		public virtual Int32? ContractItemID
		{
			get;
			set;
		}
		#endregion
		#region FixedRecurringPriceOption
		public abstract class fixedRecurringPriceOption : IBqlField { }
		[RecurringOption.List]
		[PXUIField(DisplayName = "Fixed Recurring")]
		[PXDBString(1, IsFixed = true)]
		[PXFormula(typeof(Selector<ContractDetail.contractItemID, ContractItem.fixedRecurringPriceOption>))]
		public string FixedRecurringPriceOption
		{
			[PXDependsOnFields(typeof(contractItemID))]
			get;
			set;
		}
		#endregion
		#region BasePrice
		public abstract class basePrice : IBqlField { }
		[PXUIField(DisplayName = "Price/Percent")]
		[PXDBDecimal(6)]
		[PXFormula(typeof(Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.baseItemID>, IsNull>, decimal0>, Selector<ContractDetail.contractItemID, ContractItem.basePrice>>))]
		public decimal? BasePrice
		{
			[PXDependsOnFields(typeof(contractItemID))]
			get;
			set;
		}
		#endregion
		#region BasePriceOption
		public abstract class basePriceOption : IBqlField { }
		[PriceOption.List]
		[PXUIField(DisplayName = "Setup Pricing")]
		[PXDBString(1, IsFixed = true)]
		[PXFormula(typeof(Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.baseItemID>, IsNull>, PriceOption.manually>, Selector<ContractDetail.contractItemID, ContractItem.basePriceOption>>))]
		public string BasePriceOption
		{
			[PXDependsOnFields(typeof(contractItemID))]
			get;
			set;
		}
		#endregion
		#region BasePriceVal
		public abstract class basePriceVal : IBqlField { }
		[PXDecimal(6)]
		[PXFormula(typeof(GetItemPriceValue<
			ContractDetail.contractID,
			ContractDetail.contractItemID,
			ContractDetailType.ContractDetailSetup,
			ContractDetail.basePriceOption,
			Selector<ContractDetail.contractItemID, ContractItem.baseItemID>,
			ContractDetail.basePrice,
			ContractDetail.basePriceVal,
			ContractDetail.qty,
			historyStartDate>))]
		[PXUIField(DisplayName = "Setup Price")]
		public decimal? BasePriceVal
		{
			[PXDependsOnFields(typeof(contractID),
				typeof(contractItemID),
				typeof(basePriceOption),
				typeof(basePrice),
				typeof(basePriceVal),
				typeof(qty),
				typeof(historyStartDate))]
			get;
			set;
		}
		#endregion
		#region HistoryStatus
		public abstract class historyStatus : IBqlField
		{
		}
		[PXString(1, IsFixed = true)]
		[PXDBScalar(typeof(Search<ContractRenewalHistory.status, Where<ContractRenewalHistory.contractID, Equal<contractID>,
								And<ContractRenewalHistory.revID, Equal<revID>>>>))]
		[Contract.status.List]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual String HistoryStatus
		{
			[PXDependsOnFields(typeof(contractID),
								typeof(revID))]
			get;
			set;
		}
		#endregion
		#region HistoryNextDate
		public abstract class historyNextDate : IBqlField
		{
		}
		[PXDate]
		[PXDBScalar(typeof(Search<ContractRenewalHistory.nextDate, Where<ContractRenewalHistory.contractID, Equal<contractID>,
								And<ContractRenewalHistory.revID, Equal<revID>>>>))]
		[PXUIField(DisplayName = "Next Billing Date", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual DateTime? HistoryNextDate
		{
			[PXDependsOnFields(typeof(contractID),
								typeof(revID))]
			get;
			set;
		}
		#endregion
		#region HistoryActivationDate
		public abstract class historyActivationDate : IBqlField
		{
		}

		[PXDate]
		[PXDBScalar(typeof(Search<ContractRenewalHistory.activationDate, Where<ContractRenewalHistory.contractID, Equal<contractID>,
								And<ContractRenewalHistory.revID, Equal<revID>>>>))]
		public virtual DateTime? HistoryActivationDate
		{
			[PXDependsOnFields(typeof(contractID),
								typeof(revID))]
			get;
			set;
		}
		#endregion
		#region HistoryStartDate
		public abstract class historyStartDate : IBqlField
		{
		}

		[PXDate]
		[PXDBScalar(typeof(Search<ContractRenewalHistory.startDate, Where<ContractRenewalHistory.contractID, Equal<contractID>,
								And<ContractRenewalHistory.revID, Equal<revID>>>>))]
		public virtual DateTime? HistoryStartDate
		{
			[PXDependsOnFields(typeof(contractID),
				typeof(revID))]
			get;
			set;
		}
		#endregion
		#region HistoryExpireDate
		public abstract class historyExpireDate : IBqlField
		{
		}

		[PXDate]
		[PXDBScalar(typeof(Search<ContractRenewalHistory.expireDate, Where<ContractRenewalHistory.contractID, Equal<contractID>,
								And<ContractRenewalHistory.revID, Equal<revID>>>>))]
		public virtual DateTime? HistoryExpireDate
		{
			[PXDependsOnFields(typeof(contractID),
								typeof(revID))]
			get;
			set;
		}
		#endregion
		#region HistoryTerminationDate
		public abstract class historyTerminationDate : IBqlField { }

		[PXDate]
		[PXDBScalar(typeof(Search<ContractRenewalHistory.terminationDate, Where<ContractRenewalHistory.contractID, Equal<contractID>,
								And<ContractRenewalHistory.revID, Equal<revID>>>>))]
		public virtual DateTime? HistoryTerminationDate
		{
			[PXDependsOnFields(typeof(contractID),
				typeof(revID))]
			get;
			set;
		}
		#endregion
		#region BillingScheduleNextDate
		public abstract class billingScheduleNextDate : IBqlField
		{
		}
		[PXDate]
		[PXDBScalar(typeof(Search<ContractBillingSchedule.nextDate, Where<ContractBillingSchedule.contractID, Equal<contractID>>>))]
		[PXUIField(DisplayName = "Next Billing Date", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual DateTime? BillingScheduleNextDate
		{
			[PXDependsOnFields(typeof(contractID),
								typeof(revID))]
			get;
			set;
		}
		#endregion
		#region FixedRecurringPriceVal
		public abstract class fixedRecurringPriceVal : IBqlField { }
		[PXDecimal(6)]
		[PXFormula(typeof(GetItemPriceValue<
			ContractDetail.contractID,
			ContractDetail.contractItemID,
			ContractDetailType.ContractDetail,
			ContractDetail.fixedRecurringPriceOption,
			Selector<ContractDetail.contractItemID, ContractItem.recurringItemID>,
			ContractDetail.fixedRecurringPrice,
			ContractDetail.basePriceVal,
			ContractDetail.qty,
			Switch<
				Case<Where<historyStatus, Equal<Contract.status.draft>,
					Or<historyStatus, Equal<Contract.status.pendingActivation>>>,
					IsNull<historyActivationDate, historyStartDate>,
				Case<Where<historyStatus, Equal<Contract.status.active>,
					Or<historyStatus, Equal<Contract.status.inUpgrade>>>,
					IsNull<historyNextDate, Current<AccessInfo.businessDate>>,
				Case<Where<historyStatus, Equal<Contract.status.expired>>,
					IsNull<billingScheduleNextDate, historyExpireDate>,
				Case<Where<historyStatus, Equal<Contract.status.canceled>>,
					IsNull<historyTerminationDate, Current<AccessInfo.businessDate>>>>>>,
				Current<AccessInfo.businessDate>>>))]
		[PXUIField(DisplayName = "Recurring Price")]
		public decimal? FixedRecurringPriceVal
		{
			[PXDependsOnFields(typeof(contractID),
				typeof(contractItemID),
				typeof(fixedRecurringPriceOption),
				typeof(fixedRecurringPrice),
				typeof(basePriceVal),
				typeof(qty),
				typeof(historyStatus),
				typeof(historyNextDate),
				typeof(historyActivationDate),
				typeof(historyStartDate),
				typeof(historyExpireDate),
				typeof(historyTerminationDate),
				typeof(billingScheduleNextDate))]
			get;
			set;
		}
		#endregion
	}
}

	
