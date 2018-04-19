using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.CA
{
	[Serializable]
	[PXProjection(typeof(Select5<CARecon,
		InnerJoin<FinPeriod,
			On<FinPeriod.endDate, Greater<CARecon.reconDate>,
			And<CARecon.reconciled, Equal<boolTrue>, And<CARecon.voided, Equal<boolFalse>>>>>,
		Aggregate<GroupBy<CARecon.cashAccountID,
			Max<CARecon.reconDate,
			GroupBy<FinPeriod.finPeriodID
		>>>>>))]
	[PXCacheName(Messages.CAReconByPeriod)]
	public partial class CAReconByPeriod : IBqlTable
	{
		#region CashAccountID
		public abstract class cashAccountID : IBqlField { }
		[CashAccount(IsKey = true, BqlField = typeof(CARecon.cashAccountID))]
		public virtual int? CashAccountID
		{
			get;
			set;
		}
		#endregion
		#region LastReconDate
		public abstract class lastReconDate : IBqlField { }
		[PXDBDate(BqlField = typeof(CARecon.reconDate))]
		[PXUIField(DisplayName = "Last Reconciliation Date")]
		public virtual DateTime? LastReconDate
		{
			get;
			set;
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : IBqlField { }

		[FinPeriodID(IsKey = true, BqlField = typeof(FinPeriod.finPeriodID))]
		public virtual string FinPeriodID
		{
			get;
			set;
		}
		#endregion
	}
}
