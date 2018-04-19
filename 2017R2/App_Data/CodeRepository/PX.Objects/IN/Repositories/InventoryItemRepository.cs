using System;

using PX.Data;

namespace PX.Objects.IN.Repositories
{
	public class InventoryItemRepository
	{
		protected readonly PXGraph _graph;

		public InventoryItemRepository(PXGraph graph)
		{
			if (graph == null) throw new ArgumentNullException(nameof(graph));

			_graph = graph;
		}

		public InventoryItem FindByID(int? inventoryItemID) => 
			PXSelect<
				InventoryItem,
				Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>
			.SelectWindowed(_graph, 0, 1, inventoryItemID);
	}
}
