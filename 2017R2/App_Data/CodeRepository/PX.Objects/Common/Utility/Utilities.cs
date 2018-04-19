using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.Common
{
	public static class Utilities
	{
		public static void Swap<T>(ref T first, ref T second)
		{
			T temp = first;
			first = second;
			second = temp;
		}
	}
}
