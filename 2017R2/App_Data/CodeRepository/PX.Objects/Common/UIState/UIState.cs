using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;

namespace PX.Objects.Common
{
	public static class UIState
	{
		public static void RaiseOrHideError<T>(PXCache cache, object row, bool isIncorrect, string message, PXErrorLevel errorLevel, params object[] parameters)
			where T : IBqlField
		{
			if (isIncorrect)
			{
				cache.RaiseExceptionHandling<T>(row, cache.GetValue<T>(row), new PXSetPropertyException(message, errorLevel, parameters));
			}
			else
			{
				cache.RaiseExceptionHandling<T>(row, cache.GetValue<T>(row), null);
			}
		}
	}
}
