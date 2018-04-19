using System;
using System.Collections.Generic;
using System.Linq;

using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using PX.Objects.DR.Descriptor;

using PX.Data;

namespace PX.Objects.Common.Aging
{
	public static class AgingEngine
	{
		/// <param name="shortFormat">
		/// If set to <c>true</c>, then no "Past Due" or "Outstanding" postfix will
		/// be appended to the bucket description, making <paramref name="agingDirection"/>
		/// parameter irrelevant.
		/// </param>
		public static string GetDayAgingBucketDescription(
			int? lowerExclusiveBucketBoundary,
			int? upperInclusiveBucketBoundary,
			AgingDirection agingDirection,
			bool shortFormat)
		{
			if (lowerExclusiveBucketBoundary == null)
			{
				string description = agingDirection == AgingDirection.Backwards
					? Messages.Current
					: Messages.PastDue;

				return PXMessages.LocalizeNoPrefix(description);
			}
			else if (lowerExclusiveBucketBoundary != null && upperInclusiveBucketBoundary == null)
			{
				string descriptionFormat = shortFormat 
					? Messages.OverDays
					: agingDirection == AgingDirection.Backwards
						? Messages.OverDaysPastDue
						: Messages.OverDaysOutstanding;

				return PXMessages.LocalizeFormatNoPrefix(descriptionFormat, lowerExclusiveBucketBoundary.Value);
			}
			else if (lowerExclusiveBucketBoundary != null && upperInclusiveBucketBoundary != null)
			{
				string descriptionFormat = shortFormat
					? Messages.IntervalDays
					: agingDirection == AgingDirection.Backwards
						? Messages.IntervalDaysPastDue
						: Messages.IntervalDaysOutstanding;

				return PXMessages.LocalizeFormatNoPrefix(
					descriptionFormat,
					lowerExclusiveBucketBoundary.Value + 1,
					upperInclusiveBucketBoundary.Value);
			}
	
			return null;
		}

		/// <param name="shortFormat">
		/// If set to <c>true</c>, then no "Past Due" or "Outstanding" postfix will
		/// be appended to bucket descriptions, making <paramref name="agingDirection"/>
		/// parameter irrelevant.
		/// </param>
		public static IEnumerable<string> GetDayAgingBucketDescriptions(
			AgingDirection agingDirection,
			IEnumerable<int> bucketBoundaries,
			bool shortFormat)
		{
			if (bucketBoundaries == null) throw new ArgumentNullException(nameof(bucketBoundaries));

			if (!bucketBoundaries.Any())
			{
				yield return GetDayAgingBucketDescription(null, null, AgingDirection.Backwards, shortFormat);
				yield break;
			}

			int? currentLowerBoundary = null;
			int? currentUpperBoundary = null;

			// Generate descriptions for the "current" and 
			// fully bounded buckets.
			// -
			foreach (int boundary in bucketBoundaries)
			{
				currentLowerBoundary = currentUpperBoundary;
				currentUpperBoundary = boundary;

				yield return GetDayAgingBucketDescription(
					currentLowerBoundary, 
					currentUpperBoundary,
					agingDirection,
					shortFormat);
			}

			// Generate description for the "over" bucket.
			// -
			currentLowerBoundary = currentUpperBoundary;
			currentUpperBoundary = null;

			yield return GetDayAgingBucketDescription(
				currentLowerBoundary,
				currentUpperBoundary,
				agingDirection,
				shortFormat);
		}

		public static IEnumerable<string> GetPeriodAgingBucketDescriptions(
			IFinancialPeriodProvider financialPeriodProvider,
			DateTime currentDate,
			AgingDirection agingDirection,
			int numberOfBuckets)
		{
			if (financialPeriodProvider == null) throw new ArgumentNullException(nameof(financialPeriodProvider));
			if (numberOfBuckets <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfBuckets));
						
			short periodStep = (short)(agingDirection == AgingDirection.Backwards ? -1 : 1);

			// Must not re-use any existing graphs due to possible query 
			// caching of financial periods, which can impact localization 
			// of their descriptions, see AC-84505.
			// -
			PXGraph graph = new PXGraph();

			FinPeriod currentPeriod = financialPeriodProvider.GetFinPeriodByID(
				graph,
				financialPeriodProvider.GetPeriodFromDate(graph, currentDate));

			yield return currentPeriod.Descr;

			--numberOfBuckets;
			
			while (numberOfBuckets > 1)
			{
				currentPeriod = financialPeriodProvider.GetFinPeriodByID(
					graph, financialPeriodProvider.PeriodPlusPeriod(graph, currentPeriod.FinPeriodID, periodStep));

				yield return currentPeriod.Descr;

				--numberOfBuckets;
			}

			if (numberOfBuckets > 0)
			{
				yield return PXMessages.LocalizeFormatNoPrefix(
					agingDirection == AgingDirection.Backwards
						? Messages.BeforeMonth
						: Messages.AfterMonth,
					currentPeriod.Descr);
			}
		}

		/// <summary>
		/// Given the current date and the aging bucket boundaries (in maximum 
		/// inclusive days from the current date), calculates the days difference
		/// between the current date and the test date, returning a zero-based number 
		/// of aging bucket that the test date falls into.
		/// </summary>
		/// <param name="bucketBoundaries">
		/// Upper inclusive boundaries, in days, of the aging buckets.
		/// The first element of this sequence defines the upper inclusive
		/// boundary of the current bucket (it is usually zero).
		/// The total number of buckets would be equal to the number of elements
		/// in the sequence, plus one. The values in the sequence should
		/// be strictly non-decreasing.
		/// </param>
		/// <returns>
		/// The number of the aging bucket that the <paramref name="dateToAge"/>
		/// falls into, in the [0; N] interval, where N is the the number of elements 
		/// in <paramref name="bucketBoundaries"/>. The value of N corresponds
		/// to the last aging bucket, which encompasses all dates that exceed
		/// the maximum bucket boundary.
		/// </returns>
		public static int AgeByDays(
			DateTime currentDate, 
			DateTime dateToAge,
			AgingDirection agingDirection,
			IEnumerable<int> bucketBoundaries)
		{
			if (bucketBoundaries == null) throw new ArgumentNullException(nameof(bucketBoundaries));
			if (!bucketBoundaries.Any()) return 0;

			if (agingDirection == AgingDirection.Forward)
			{
				agingDirection = AgingDirection.Backwards;
				Utilities.Swap(ref currentDate, ref dateToAge);
			}

			int days = currentDate.Subtract(dateToAge).Days;

			int bucketIndex = bucketBoundaries.FindIndex(boundary => boundary >= days);

			if (bucketIndex < 0)
			{
				bucketIndex = -bucketIndex - 1;
			}

			return bucketIndex;
		}

		public static int AgeByDays(
			DateTime currentAge,
			DateTime dateToAge,
			AgingDirection agingDirection,
			params int[] bucketBoundaries)
			=> AgeByDays(currentAge, dateToAge, agingDirection, bucketBoundaries as IEnumerable<int>);

		/// <summary>
		/// Given the current date and the number of period-based aging buckets, 
		/// returns the zero-based number of bucket that the specified test date 
		/// falls into.
		/// </summary>
		/// <param name="numberOfBuckets">
		/// The total number of period-based buckets, including the "Current" 
		/// and "Over" bucket. For backwards aging, the "Current" bucket encompasses 
		/// dates in the same (or later) financial period as the current date, and 
		/// the "Over" bucket corresponds to dates that are at least (numberOfBuckets - 1) 
		/// periods back in time from the current date.
		/// </param>
		public static int AgeByPeriods(
			PXGraph graph,
			DateTime currentDate, 
			DateTime dateToAge, 
			IFinancialPeriodProvider financialPeriodProvider,
			AgingDirection agingDirection,
			int numberOfBuckets)
		{
			if (graph == null) throw new ArgumentNullException(nameof(graph));
			if (financialPeriodProvider == null) throw new ArgumentNullException(nameof(financialPeriodProvider));
			if (numberOfBuckets <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfBuckets));

			if (agingDirection == AgingDirection.Forward)
			{
				agingDirection = AgingDirection.Backwards;
				Utilities.Swap(ref currentDate, ref dateToAge);
			}

			if (dateToAge > currentDate) return 0;

			int bucketNumber = financialPeriodProvider
				.PeriodsBetweenInclusive(graph, dateToAge, currentDate)
				.Count();

			--bucketNumber;

			if (bucketNumber < 0)
			{
				// No financial periods found between the dates,
				// cannot proceed with aging.
				// -
				throw new PXException(GL.Messages.NoPeriodsDefined);
			}

			if (bucketNumber > numberOfBuckets - 1)
			{
				// Force into the last ("over") aging bucket.
				// -
				bucketNumber = numberOfBuckets - 1;
			}

			return bucketNumber;
		}
	}
}
