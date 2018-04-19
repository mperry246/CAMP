using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.CT
{
	[Serializable]
	[PXProjection(typeof(Select5<FinPeriod,
						LeftJoin<ContractRenewalHistory,
							On<FinPeriod.startDate, NotEqual<FinPeriod.endDate>,
								And<FinPeriod.finDate, GreaterEqual<ContractRenewalHistory.effectiveFrom>,
								And<FinPeriod.finDate, GreaterEqual<ContractRenewalHistory.activationDate>,
								And<ContractRenewalHistory.status, NotEqual<Contract.status.draft>,
								And<ContractRenewalHistory.status, NotEqual<Contract.status.inApproval>,
								And<ContractRenewalHistory.status, NotEqual<Contract.status.inUpgrade>,
								And<ContractRenewalHistory.status, NotEqual<Contract.status.pendingActivation>,
								And<ContractRenewalHistory.effectiveFrom, IsNotNull,
								And<Where<FinPeriod.startDate, LessEqual<ContractRenewalHistory.expireDate>,
									Or<ContractRenewalHistory.expireDate, IsNull>>>>>>>>>>>,
						LeftJoin<Contract,
							On<Where<ContractRenewalHistory.contractID, Equal<Contract.contractID>>>>>,
						Where<FinPeriod.startDate, LessEqual<Contract.terminationDate>,
							Or<Contract.terminationDate, IsNull>>,
						Aggregate<GroupBy<FinPeriod.finDate,
									GroupBy<ContractRenewalHistory.contractID,
									Max<ContractRenewalHistory.revID>>>>>)
		)]
	[PXCacheName("Contract revision by period")]
	public class ContractRevisionByPeriod : IBqlTable
	{
		#region FinPeriodID
		public abstract class finPeriodID : IBqlField { }
		[FinPeriodID(IsKey = true, BqlField = typeof(FinPeriod.finPeriodID))]
		public virtual String FinPeriodID
		{
			get;
			set;
		}
		#endregion
		#region ContractID
		public abstract class contractID : IBqlField
		{
		}
		[PXDBIdentity(IsKey = true, BqlField = typeof(ContractRenewalHistory.contractID))]
		[PXUIField(DisplayName = "Contract ID")]
		public virtual Int32? ContractID
		{
			get;
			set;
		}
		#endregion
		#region RevID
		public abstract class revID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(ContractRenewalHistory.revID))]
		[PXUIField(DisplayName = "Revision Number")]
		public virtual int? RevID
		{
			get;
			set;
		}
		#endregion
		#region ActivationDate
		public abstract class activationDate : IBqlField
		{
		}

		[PXDBDate(BqlField = typeof(ContractRenewalHistory.activationDate))]
		public virtual DateTime? ActivationDate
		{
			get;
			set;
		}
		#endregion
		#region EffectiveFrom
		public abstract class effectiveFrom : IBqlField { }

		[PXDBDate(BqlField = typeof(ContractRenewalHistory.effectiveFrom))]
		[PXUIField(DisplayName = "Effective From")]
		public virtual DateTime? EffectiveFrom
		{
			get;
			set;
		}
		#endregion
		#region ExpireDate
		public abstract class expireDate : IBqlField { }

		[PXDBDate(BqlField = typeof(ContractRenewalHistory.expireDate))]
		[PXUIField(DisplayName = "Expiration Date")]
		public virtual DateTime? ExpireDate
		{
			get;
			set;
		}
		#endregion
		#region TerminationDate
		public abstract class terminationDate : IBqlField { }

		[PXDBDate(BqlField = typeof(Contract.terminationDate))]
		public virtual DateTime? TerminationDate
		{
			get;
			set;
		}
		#endregion
		#region StartFinPeriod
		public abstract class startFinPeriod : IBqlField
		{
		}

		[PXDBDate(BqlField = typeof(FinPeriod.startDate))]
		[PXUIField(DisplayName = "Start Date")]
		public virtual DateTime? StartFinPeriod { get; set; }
		#endregion
		#region EndFinPeriod
		public abstract class endFinPeriod : IBqlField
		{
		}

		[PXDBDate(BqlField = typeof(FinPeriod.endDate))]
		public virtual DateTime? EndFinPeriod { get; set; }
		#endregion
		#region New
		public abstract class newCount : IBqlField
		{
		}
		[PXInt]
		public virtual int? NewCount
		{
			[PXDependsOnFields(typeof(effectiveFrom),
								typeof(startFinPeriod),
								typeof(endFinPeriod))]
			get
			{
				if (EffectiveFrom >= StartFinPeriod &&
					EffectiveFrom < EndFinPeriod)
				{
					return 1;
				}
				else
				{
					return 0;
				}
			}
			set
			{

			}
		}
		#endregion
		#region Expired
		public abstract class expiredCount : IBqlField
		{
		}
		[PXDBInt]
		[PXUIField(DisplayName = "Expired")]
		public virtual int? ExpiredCount
		{
			[PXDependsOnFields(typeof(terminationDate),
								typeof(expireDate),
								typeof(startFinPeriod),
								typeof(endFinPeriod))]
			get
			{
				if (ExpireDate >= StartFinPeriod &&	ExpireDate < EndFinPeriod ||
					TerminationDate >= StartFinPeriod && TerminationDate < EndFinPeriod)
				{
					return 1;
				}
				else
				{
					return 0;
				}
			}
			set
			{

			}
		}
		#endregion
	}
}
