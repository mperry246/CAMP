using System;
using System.Collections.Generic;

using PX.Data;
using PX.Objects.GL;

namespace PX.Objects.DR.Descriptor
{
	public interface IFinancialPeriodProvider
	{
		/// <summary>
		/// Returns the financial period for a given date.
		/// </summary>
		FinPeriod FindFinPeriodByDate(PXGraph graph, DateTime? dateTime);

		/// <summary>
		/// Returns the financial period ID from a given date.
		/// </summary>
		string GetPeriodFromDate(PXGraph graph, DateTime? dateTime);

		/// <summary>
		/// Returns the start date for the given financial period ID.
		/// </summary>
		DateTime PeriodStartDate(PXGraph graph, string financialPeriodID);

		/// <summary>
		/// Returns the start date for the given financial period ID.
		/// </summary>
		DateTime PeriodEndDate(PXGraph graph, string financialPeriodID);

		/// <summary>
		/// Returns the ID of the period by skipping the specified number of periods
		/// from the given period ID. 
		/// </summary>
		/// <param name="offset">Number of periods to skip over.</param>
		string PeriodPlusPeriod(PXGraph graph, string financialPeriodID, short offset);

		/// <summary>
		/// Returns the financial period given its ID.
		/// </summary>
		FinPeriod GetFinPeriodByID(PXGraph graph, string financialPeriodID);

		/// <summary>
		/// Returns a minimal set of financial periods that contain a given date interval
		/// within them, excluding any adjustment periods.
		/// </summary>
		/// <param name="graph">The graph which will be used when performing a select DB query.</param>
		/// <param name="startDate">The starting date of the date interval.</param>
		/// <param name="endDate">The ending date of the date interval.</param>
		IEnumerable<FinPeriod> PeriodsBetweenInclusive(PXGraph graph, DateTime startDate, DateTime endDate);
	}

	public static class FinancialPeriodProvider
	{
		private class DefaultFinPeriodProvider : IFinancialPeriodProvider
		{
			public FinPeriod FindFinPeriodByDate(PXGraph graph, DateTime? dateTime)
			{
				return FinPeriodIDAttribute.FindFinPeriodByDate(graph, dateTime);
			}

			public string GetPeriodFromDate(PXGraph graph, DateTime? dateTime)
			{
				return FinPeriodIDAttribute.PeriodFromDate(graph, dateTime);
			}

			public DateTime PeriodStartDate(PXGraph graph, string financialPeriodID)
			{
				return FinPeriodIDAttribute.PeriodStartDate(graph, financialPeriodID);
			}

			public DateTime PeriodEndDate(PXGraph graph, string financialPeriodID)
			{
				return FinPeriodIDAttribute.PeriodEndDate(graph, financialPeriodID);
			}

			public string PeriodPlusPeriod(PXGraph graph, string financialPeriodID, short offset)
			{
				return FinPeriodIDAttribute.PeriodPlusPeriod(graph, financialPeriodID, offset);
			}

			public IEnumerable<FinPeriod> PeriodsBetweenInclusive(
				PXGraph graph,
				DateTime startDate,
				DateTime endDate)
			{
				return FinPeriodIDAttribute.PeriodsBetweenInclusive(graph, startDate, endDate);
			}

			public FinPeriod GetFinPeriodByID(PXGraph graph, string financialPeriodID)
			{
				return FinPeriodIDAttribute.GetByID(graph, financialPeriodID);
			}
		}

		/// <summary>
		/// Returns a default implementation of <see cref="IFinancialPeriodProvider"/>
		/// that delegates all its responsibilities to <see cref="FinPeriodIDAttribute"/>.
		/// </summary>
		public static IFinancialPeriodProvider Default => new DefaultFinPeriodProvider();
	}
}