using PX.Data;

namespace PX.Objects.Common.Extensions
{
	public static class PXGraphClassExtensions
	{
		public static void DisableCaches(this PXGraph graph)
		{
			foreach (PXCache graphCache in graph.Caches.Values)
			{
				graphCache.AllowInsert =
				graphCache.AllowUpdate =
				graphCache.AllowDelete = false;
			}
		}
	}
}
