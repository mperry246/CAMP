using System;
using System.Collections.Generic;
using System.Linq;
using PX.Common;

using PX.Data;

namespace PX.Objects.Common.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool IsEmpty<T>(this IEnumerable<T> sequence) => !sequence.Any();

		/// <returns>
		/// <c>true</c>, if the specified sequence contains exactly 
		/// one element, <c>false</c> otherwise.
		/// </returns>
		public static bool IsSingleElement<T>(this IEnumerable<T> sequence)
		{
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));

			using (IEnumerator<T> enumerator = sequence.GetEnumerator())
				return enumerator.MoveNext() && !enumerator.MoveNext();
		}


		public static IEnumerable<TNode> DistinctByKeys<TNode>(this IEnumerable<TNode> sequence, PXGraph graph)
			where TNode : class, IBqlTable, new()
			=> DistinctByKeys(sequence, graph?.Caches[typeof(TNode)]);

		/// <summary>
		/// Given a sequence of records and a <see cref="PXCache"/> object,
		/// returns a sequence of elements that have different keys.
		/// </summary>
		/// <remarks>The collection of keys is defined by <see cref="PXCache.Keys"/>.</remarks>
		public static IEnumerable<TNode> DistinctByKeys<TNode>(this IEnumerable<TNode> sequence, PXCache cache)
			where TNode : class, IBqlTable, new()
		{
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (cache == null) throw new ArgumentNullException(nameof(cache));

			return sequence.Distinct(new RecordKeyComparer<TNode>(cache));
		}

		/// <summary>
		/// Returns the index of the first element that satisfies the 
		/// specified predicate, or a negative value in case such an
		/// element cannot be found.
		/// </summary>
		/// <remarks>
		/// In case when the element satisfying the predicate cannot
		/// be found, the value returned by this method corresponds
		/// to the negative number of elements in the sequence, further
		/// decremented by one.
		/// </remarks>
		public static int FindIndex<T>(this IEnumerable<T> sequence, Predicate<T> predicate)
		{
			int index = 0;

			foreach (T element in sequence)
			{
				if (predicate(element)) return index;
				++index;
			}

			return -index - 1;
		}

		/// <returns>
		/// <c>true</c>, if the specified sequence contains at least
		/// two elements, <c>false</c> otherwise.
		/// </returns>
		public static bool HasAtLeastTwoItems<T>(this IEnumerable<T> sequence) => sequence.HasAtLeast(2);

		/// <summary>
		/// Flattens a sequence of element groups into a sequence of elements.
		/// </summary>
		public static IEnumerable<TValue> Flatten<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> sequenceOfGroups)
			=> sequenceOfGroups.SelectMany(x => x);
	}
}
