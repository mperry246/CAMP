using System;

using PX.Data;

namespace PX.Objects.GL
{
	[TableAndChartDashboardType]
	public class GLHistoryValidate : PXGraph<GLHistoryValidate>
	{
		#region Internal Types Definition
		[Serializable]
		public class GLIntegrityCheckFilter : IBqlTable
		{
			public abstract class finPeriodID : IBqlField { }

			[FinPeriodSelector]
			[PXDefault(typeof(Search<
				FinPeriod.finPeriodID,
				Where<
					FinPeriod.closed, Equal<False>>,
				OrderBy<
					Asc<FinPeriod.finPeriodID>>>))]
			[PXUIField(DisplayName = Messages.FinPeriod)]
			public virtual string FinPeriodID
			{
				get;
				set;
			}
		}
		#endregion

		public GLHistoryValidate()
		{
			GLSetup setup = glsetup.Current;

			LedgerList.SetProcessCaption(Messages.ProcValidate);
			LedgerList.SetProcessAllCaption(Messages.ProcValidateAll);

			PXUIFieldAttribute.SetEnabled<Ledger.selected>(LedgerList.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<Ledger.ledgerCD>(LedgerList.Cache, null, false);
			PXUIFieldAttribute.SetEnabled<Ledger.descr>(LedgerList.Cache, null, false);
		}

		public PXFilter<GLIntegrityCheckFilter> Filter;
		public PXCancel<GLIntegrityCheckFilter> Cancel;

		[PXFilterable]
		public PXFilteredProcessing<
			Ledger, 
			GLIntegrityCheckFilter,
			Where<
				Ledger.balanceType, Equal<LedgerBalanceType.actual>, 
				Or<Ledger.balanceType,Equal<LedgerBalanceType.report>>>> 
			LedgerList;

		public PXSetup<GLSetup> glsetup;

		protected virtual void GLIntegrityCheckFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			GLIntegrityCheckFilter filter = Filter.Current;

			bool errorsOnForm = PXUIFieldAttribute.GetErrors(sender,null).Count > 0;
			LedgerList.SetProcessEnabled(!errorsOnForm);
			LedgerList.SetProcessAllEnabled(!errorsOnForm);

			LedgerList.SetProcessDelegate(
				(PostGraph postGraph, Ledger ledger) => Validate(postGraph, ledger, filter));
		}

		private static void Validate(PostGraph graph, Ledger ledger, GLIntegrityCheckFilter filter)
		{
			if (string.IsNullOrEmpty(filter.FinPeriodID))
			{
				throw new PXException(Messages.Prefix + ": " + Messages.ProcessingRequireFinPeriodID);
			}

			while (RunningFlagScope<PostGraph>.IsRunning)
			{
				System.Threading.Thread.Sleep(10);
			}

            using (new RunningFlagScope<GLHistoryValidate>())
            {
                graph.Clear();
                graph.IntegrityCheckProc(ledger, filter.FinPeriodID);
            }
		}

		protected virtual void GLIntegrityCheckFilter_FinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (string.IsNullOrEmpty(e.NewValue?.ToString()))
			{
				throw new PXSetPropertyException(Messages.Prefix + ": " + Messages.ProcessingRequireFinPeriodID, "finPeriodID");
			}
		}
	}
}
