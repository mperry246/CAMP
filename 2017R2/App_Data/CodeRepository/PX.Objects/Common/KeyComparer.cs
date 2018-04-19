using System.Collections.Generic;

using PX.Data;
using System;

namespace PX.Objects.Common
{
	/// <summary>
	/// Compares two data records based on the equality of their keys.
	/// The collection of keys is defined by the specified
	/// <see cref="PXCache"/> object.
	/// </summary>
	public class RecordKeyComparer<TRecord> : IEqualityComparer<TRecord>
		where TRecord : class, IBqlTable, new()
	{
		private PXCache _cache;

		public RecordKeyComparer(PXCache cache)
		{
			if (cache == null) throw new ArgumentNullException(nameof(cache));

			_cache = cache;
		}

		public bool Equals(TRecord first, TRecord second)
			=> _cache.ObjectsEqual(first, second);

		public int GetHashCode(TRecord record)
			=> _cache.GetObjectHashCode(record);
	}
}
