using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.Common
{
	[PXDynamicButton(new string[] { PasteLineCommand, ResetOrderCommand },
					 new string[] { Messages.PasteLine, Messages.ResetOrder },
					 TranslationKeyType = typeof(Messages))]
	public class PXOrderedSelect<Primary, Table, Where, OrderBy> : PXOrderedSelectBase<Primary, Table>
		where Primary : class, IBqlTable, new()
		where Table : class, IBqlTable, ISortOrder, new()
		where Where : IBqlWhere, new()
		where OrderBy : IBqlOrderBy, new()
	{
		public PXOrderedSelect(PXGraph graph)
		{
			_Graph = graph;
			Initialize();

			View = new PXView(graph, false, new Select<Table, Where, OrderBy>());
		}
		public PXOrderedSelect(PXGraph graph, Delegate handler)
		{
			_Graph = graph;
			Initialize();

			View = new PXView(graph, false, new Select<Table, Where, OrderBy>(), handler);

		}
	}

	[PXDynamicButton(new string[] { PasteLineCommand, ResetOrderCommand },
					 new string[] { Messages.PasteLine, Messages.ResetOrder },
					 TranslationKeyType = typeof(Messages))]
	public class PXOrderedSelect<Primary, Table, Join, Where, OrderBy> : PXOrderedSelectBase<Primary, Table>
		where Primary : class, IBqlTable, new()
		where Table : class, IBqlTable, ISortOrder, new()
		where Join : IBqlJoin, new()
		where Where : IBqlWhere, new()
		where OrderBy : IBqlOrderBy, new()
	{
		public PXOrderedSelect(PXGraph graph)
		{
			_Graph = graph;
			Initialize();

			View = new PXView(graph, false, new Select2<Table, Join, Where, OrderBy>());
		}
		public PXOrderedSelect(PXGraph graph, Delegate handler)
		{
			_Graph = graph;
			Initialize();

			View = new PXView(graph, false, new Select2<Table, Join, Where, OrderBy>(), handler);
		}
	}

	public abstract class PXOrderedSelectBase<Primary, Table> : PXSelectBase<Table>
		where Primary : class, IBqlTable, new()
		where Table : class, IBqlTable, ISortOrder, new()
	{
		public const string PasteLineCommand = "PasteLine";
		public const string ResetOrderCommand = "ResetOrder";

		/// <summary>
		/// Default value is TRUE. Performance customization point. Override in Initialize() method to allow gaps in numbering when record is deleted. 
		/// </summary>
		protected bool RenumberTailOnDelete = true;

		public virtual void Initialize()
		{
			AddActions(_Graph);
			_Graph.RowInserted.AddHandler<Table>(OnRowInserted);
			_Graph.RowDeleted.AddHandler<Table>(OnRowDeleted);
			_Graph.OnBeforePersist += OnBeforeGraphPersist;
		}

		public IComparer<PXResult> CustomComparer { get; set; }

		[PXButton()]
		protected virtual IEnumerable PasteLine(PXAdapter adapter)
		{
			string[] sorts = this.View.GetExternalSorts();
			
			if (sorts != null && sorts.Length > 0 )
			{
				if (sorts.Length == 1 && string.Equals(sorts[0], nameof(ISortOrder.SortOrder), StringComparison.InvariantCultureIgnoreCase) )
				{
					//ok
				}
				else
				{
					throw new PXException(Messages.GridIsAlreadySorted);
				}
			}
			
			Table focus = GetFocus();
			if (focus != null)
			{
				IList<Table> moved = GetMoved();

				if ( moved.Count > 0 )
				{
					PasteLines(focus, moved);
				}
			}

			return adapter.Get();
		}

		[PXButton()]
		protected virtual IEnumerable ResetOrder(PXAdapter adapter)
		{
			if (CustomComparer != null)
			{
				RenumberAll(CustomComparer);
			}
			else
			{
				RenumberAll();
			}

			return adapter.Get();
		}

		protected virtual void PasteLines(Table focus, IList<Table> moved)
		{
			if (Cache.AllowUpdate)
			{
				int focusSortOrder = focus.SortOrder.GetValueOrDefault();
				int sortOrder = focusSortOrder;
				HashSet<int> movedLines = new HashSet<int>();
				foreach (Table line in moved)
				{
					movedLines.Add(line.LineNbr.GetValueOrDefault());
					if (line.SortOrder < sortOrder)
					{
						sortOrder = line.SortOrder.Value;
					}
				}

				List<Table> head = new List<Table>();
				List<Table> body = new List<Table>();
				List<Table> tail = new List<Table>();

				foreach (Table line in Select())
				{
					bool lineIsMoved = movedLines.Contains(line.LineNbr.GetValueOrDefault());

					if (lineIsMoved == true)
					{
						body.Add(line);
					}
					else
					{
						if (Cache.InsertPositionMode)
						{
							if (line.SortOrder >= sortOrder && line.SortOrder <= focusSortOrder)
							{
								head.Add(line);
							}
							else if (line.SortOrder >= sortOrder && line.SortOrder > focusSortOrder)
							{
								tail.Add(line);
							}
						}
						else
						{
							if (line.SortOrder >= sortOrder && line.SortOrder < focusSortOrder)
							{
								head.Add(line);
							}
							else if (line.SortOrder >= sortOrder && line.SortOrder >= focusSortOrder)
							{
								tail.Add(line);
							}
						}
					}
				}

				head.AddRange(body);
				head.AddRange(tail);
				foreach (Table line in head)
				{
					if (line.SortOrder != sortOrder)
					{
						Cache.SetValue(line, nameof(ISortOrder.SortOrder), sortOrder);
						Cache.SmartSetStatus(line, PXEntryStatus.Updated);
						Cache.IsDirty = true;
					}
					sortOrder++;
				}

				View.Clear();//clears stored cache so that grid lines are reordered.
			}
		}

		protected virtual void AddActions(PXGraph graph)
		{
			AddAction(graph, PasteLineCommand, Messages.PasteLine, PasteLine);
			AddAction(graph, ResetOrderCommand, Messages.ResetOrder, ResetOrder);
		}
				
		protected PXAction AddAction(PXGraph graph, string name, string displayName, PXButtonDelegate handler)
		{
			PXUIFieldAttribute uiAtt = new PXUIFieldAttribute
			{
				DisplayName = PXMessages.LocalizeNoPrefix(displayName),
				MapEnableRights = PXCacheRights.Select
			};

			List<PXEventSubscriberAttribute> addAttrs = new List<PXEventSubscriberAttribute> { uiAtt };
			PXNamedAction<Primary> res = new PXNamedAction<Primary>(graph, name, handler, addAttrs.ToArray());
			graph.Actions[name] = res;
			return res;
		}

		protected virtual Table GetFocus()
		{
			return GetItemByKeys(Cache.InsertPosition);
		}

		protected virtual IList<Table> GetMoved()
		{
			List<Table> result = new List<Table>();
			if (Cache.RowsToMove != null )
			{
				foreach (var key in Cache.RowsToMove)
				{
					Table item = GetItemByKeys(key);
					if (item!= null)
					{
						result.Add(item);
					}
				}
			}

			return result;
		}

		protected virtual Table GetItemByKeys(Dictionary<string, object> keys)
		{
			Table result = null;

			if (keys != null && keys.Count > 0)
			{
				object current = Cache.Current;

				try
				{
					if (Cache.Locate(keys) == 1)
					{
						result = Cache.Current as Table;
					}
				}
				finally
				{
					Cache.Current = current;
				}

			}

			return result;
		}

		protected virtual void OnRowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			if (e.Row != null)
			{
				Table focus = GetFocus();
				if (focus != null)
				{
					InsertAboveFocus((Table)e.Row, focus);
				}

				ISortOrder row = (ISortOrder)e.Row;
				if (row.SortOrder == null)
				{
					row.SortOrder = row.LineNbr;
				}
			}
		}

		protected virtual void OnRowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			if (e.Row != null)
			{
				Table row = (Table)e.Row;
				if (row.SortOrder != null && !IsPrimaryEntityNewlyInserted && !IsPrimaryEntityDeleted && RenumberTailOnDelete)
				{
					int sortorder = row.SortOrder.Value;

					bool renumber = false;
					foreach (Table line in Select())
					{
						if (!renumber)
						{
							if (line.SortOrder >= sortorder)
							{
								renumber = true;
							}
							else
								continue;
						}
						
						if (renumber)
						{
							Cache.SetValue(line, nameof(ISortOrder.SortOrder), sortorder);
							Cache.SmartSetStatus(line, PXEntryStatus.Updated);
							sortorder++;							
						}
					}
				}
			}
		}


		protected virtual void InsertAboveFocus(Table row, Table focus)
		{
			int focusSortOrder = focus.SortOrder.Value;
			int sortorder = focusSortOrder;
			bool renumber = false;
			foreach (Table line in Select())
			{
				if (line.LineNbr == row.LineNbr)
					continue;

				if (!renumber)
				{
					if (line.SortOrder >= sortorder)
					{
						renumber = true;
					}
					else
						continue;
				}

				if (renumber)
				{
					sortorder++;
					Cache.SetValue(line, nameof(ISortOrder.SortOrder), sortorder);
					Cache.SmartSetStatus(line, PXEntryStatus.Updated);
				}
			}

			Cache.SetValue(row, nameof(ISortOrder.SortOrder), focusSortOrder);
			Cache.SmartSetStatus(row, PXEntryStatus.Updated);
		}

		protected virtual void OnBeforeGraphPersist(PXGraph graph)
		{
			if (IsPrimaryEntityNewlyInserted)
			{
				RenumberAll();
			}
			else
			{
				RenumberTail();
			}
		}

		public virtual void RenumberAll()
		{
			int sortorder = 0;
			foreach (Table line in Select())
			{
				sortorder++;
				if (line.SortOrder != sortorder)
				{
					Cache.SetValue(line, nameof(ISortOrder.SortOrder), sortorder);
					Cache.SmartSetStatus(line, PXEntryStatus.Updated);
					Cache.IsDirty = true;
				}
			}
		}

		public virtual void RenumberTail()
		{
			List<Table> addedList = new List<Table>();
			int lastSortorder = 0;

			foreach (Table item in Cache.Inserted)
			{
				if (Cache.GetStatus(item) == PXEntryStatus.Inserted)
				{
					addedList.Add(item);
				}
			}

			if (addedList.Count == 0)
				return;

			foreach (Table item in Select())
			{
				if (Cache.GetStatus(item) != PXEntryStatus.Inserted && Cache.GetStatus(item) != PXEntryStatus.Deleted)
				{
					lastSortorder = Math.Max(lastSortorder, item.SortOrder.GetValueOrDefault());
				}
			}

			addedList = addedList.OrderBy(x => x.SortOrder.HasValue).ThenBy(x => x.SortOrder).ThenBy(x => x.LineNbr).ToList();

			foreach (Table item in addedList)
			{
				if (item.SortOrder > lastSortorder)
				{
					lastSortorder++;
					Cache.SetValue(item, nameof(ISortOrder.SortOrder), lastSortorder);
					Cache.SmartSetStatus(item, PXEntryStatus.Updated);
					Cache.IsDirty = true;
				}
			}
		}

		public virtual void RenumberAll(IComparer<PXResult> comparer)
		{
			PXResultset<Table> res = Select();
			List<PXResult> list = new List<PXResult>(res.Count);

			foreach (PXResult item in res)
			{
				list.Add(item);
			}

			list.Sort(comparer);

			int sortorder = 0;
			foreach (PXResult item in list)
			{
				sortorder++;
				Table line = PXResult.Unwrap<Table>(item);
				if (line.SortOrder != sortorder)
				{
					Cache.SetValue(line, nameof(ISortOrder.SortOrder), sortorder);
					Cache.SmartSetStatus(line, PXEntryStatus.Updated);
					Cache.IsDirty = true;
				}
			}

			View.Clear();//clears stored cache so that grid lines are reordered.
		}
		
		protected virtual bool IsPrimaryEntityNewlyInserted
		{
			get
			{
				object doc = _Graph.Caches[typeof(Primary)].Current;
				if (doc != null && _Graph.Caches[typeof(Primary)].GetStatus(doc) == PXEntryStatus.Inserted)
				{
					return true;
				}

				return false;
			}
		}

		protected virtual bool IsPrimaryEntityDeleted
		{
			get
			{
				object doc = _Graph.Caches[typeof(Primary)].Current;
				if (doc != null)
				{
					PXEntryStatus status = _Graph.Caches[typeof(Primary)].GetStatus(doc);
					return status == PXEntryStatus.Deleted || status == PXEntryStatus.InsertedDeleted;
				}
				return false;
			}
		}
	}

	public interface ISortOrder
	{
		int? SortOrder { get; set; }
		int? LineNbr { get; }
	}
}
