using System;
using System.Collections.Generic;
using System.Text;
using PX.Data;

namespace PX.Objects.Common.Extensions
{
	public static class PXCacheExtensions
	{
		public static IEqualityComparer<TDAC> GetKeyComparer<TDAC>(this PXCache<TDAC> cache) 
			where TDAC : class, IBqlTable, new()
		{
			return new CustomComparer<TDAC>(cache.GetObjectHashCode, cache.ObjectsEqual);
		}

		public static void ClearFieldErrors<TField>(this PXCache cache, object record)
			where TField : IBqlField
			=> cache.RaiseExceptionHandling<TField>(record, null, null);

		public static void ClearFieldSpecificError<TField>(this PXCache cache, object record, string errorMsg, params object[] errorMessageArguments)
			where TField : IBqlField
		{
			String fieldError = PXUIFieldAttribute.GetError<TField>(cache, record);
			String specificError = PXMessages.LocalizeFormatNoPrefix(errorMsg, errorMessageArguments);
			if (String.Equals(fieldError, specificError, StringComparison.CurrentCulture))
				cache.ClearFieldErrors<TField>(record);
		}

		public static void DisplayFieldError<TField>(
			this PXCache cache,
			object record,
			PXErrorLevel errorLevel,
			string message,
			params object[] errorMessageArguments)
			where TField : IBqlField
		{
			PXSetPropertyException<TField> setPropertyException =
				new PXSetPropertyException<TField>(message, errorLevel, errorMessageArguments);

			if (cache.RaiseExceptionHandling<TField>(record, null, setPropertyException))
			{
				throw setPropertyException;
			}
		}

		public static void DisplayFieldError<TField>(
			this PXCache cache,
			object record,
			string message,
			params object[] errorMessageArguments)
			where TField : IBqlField
			=> DisplayFieldError<TField>(cache, record, PXErrorLevel.Error, message, errorMessageArguments);

		public static void DisplayFieldWarning<TField>(
			this PXCache cache,
			object record,
			object newValue,
			string message,
			params object[] errorMessageArguments)
			where TField : IBqlField
		{
			PXSetPropertyException<TField> setPropertyException =
				new PXSetPropertyException<TField>(message, PXErrorLevel.Warning, errorMessageArguments);

			if (cache.RaiseExceptionHandling<TField>(record, newValue, setPropertyException))
			{
				throw setPropertyException;
			}
		}

		public static string GetFullDescription(this PXCache cache, object record)
		{
			if (cache == null) throw new ArgumentNullException(nameof(cache));

			StringBuilder description = new StringBuilder($"{ cache.GetItemType().Name }.");

			if (record == null)
			{
				description.Append(" [NULL]");
				return description.ToString();
			}

			foreach (Type field in cache.BqlFields)
			{
				description.Append(
					$" {field.Name}: {cache.GetValue(record, field.Name) ?? "[NULL]"};");
			}

			return description.ToString();
		}

		/// <summary>
		/// A PXCache extension method that sets all cache permissions to edit data: <see cref="PXCache.AllowInsert"/>, <see cref="PXCache.AllowDelete"/>, <see cref="PXCache.AllowUpdate"/>.
		/// </summary>
		/// <param name="cache">The cache to set permissions.</param>
		/// <param name="allowEdit">True to set all permissions to allow edit of data, false to prohibit edit.</param>
		public static void SetAllEditPermissions(this PXCache cache, bool allowEdit)
		{
			if (cache == null)
				throw new ArgumentNullException(nameof(cache));

			cache.AllowInsert = allowEdit;
			cache.AllowDelete = allowEdit;
			cache.AllowUpdate = allowEdit;
		}
	}
}
