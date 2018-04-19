using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.PM
{
	public class PMCommitmentSelect : PMCommitmentSelect<PMCommitment, Where<PMCommitment.type, IsNotNull>>
	{
		public PMCommitmentSelect(PXGraph graph, Delegate handle)
			: base(graph, handle)
		{			
		}

		public PMCommitmentSelect(PXGraph graph)
			: base(graph)
		{
		}
	}

	public class PMCommitmentSelect<T, Where> : PXSelect<T, Where>
		where T : class, IBqlTable, new()
		where Where : IBqlWhere, new()
	{
		public PMCommitmentSelect(PXGraph graph, Delegate handle)
			: base(graph, handle)
		{
			OnInit();
		}

		public PMCommitmentSelect(PXGraph graph)
			: base(graph)
		{
			OnInit();
		}

		protected virtual void OnInit()
		{
			if (!PXAccess.FeatureInstalled<FeaturesSet.projectModule>())
				return;

			if (!_Graph.Views.Caches.Contains(typeof(PMCommitment)))
				_Graph.Views.Caches.Add(typeof(PMCommitment));

			if (!_Graph.Views.Caches.Contains(typeof(PMHistoryAccum)))
				_Graph.Views.Caches.Add(typeof(PMHistoryAccum));

			if (!_Graph.Views.Caches.Contains(typeof(PMBudgetAccum)))
				_Graph.Views.Caches.Add(typeof(PMBudgetAccum));

			_Graph.RowInserted.AddHandler<T>(PMCommitment_RowInserted);
			_Graph.RowUpdated.AddHandler<T>(PMCommitment_RowUpdated);
			_Graph.RowDeleted.AddHandler<T>(PMCommitment_RowDeleted);
		}

		protected virtual void PMCommitment_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			PMCommitment row = e.Row as PMCommitment;
			RollUpCommitmentBalance(sender, row, 1);
		}

		protected virtual void PMCommitment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			PMCommitment row = e.Row as PMCommitment;
			PMCommitment oldRow = e.OldRow as PMCommitment;

			RollUpCommitmentBalance(sender, oldRow, -1);
			RollUpCommitmentBalance(sender, row, 1);
		}

		protected virtual void PMCommitment_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			PMCommitment row = e.Row as PMCommitment;
			RollUpCommitmentBalance(sender, row, -1);
		}

		public virtual void RollUpCommitmentBalance(PXCache sender, PMCommitment row, int sign)
		{
			if (row == null)
				throw new ArgumentNullException();

			if (row.ProjectID == null || row.ProjectTaskID == null || row.AccountGroupID == null)
				return;
						
			ProjectBalance pb = CreateProjectBalance();
			PMBudget existing = pb.SelectProjectBalance(row);
			ProjectBalance.RollupQty rollupQty = pb.CalculateRollupQty<PMCommitment>(row, existing, row.Qty);
			ProjectBalance.RollupQty rollupOpenQty = pb.CalculateRollupQty<PMCommitment>(row, existing, row.OpenQty);
			ProjectBalance.RollupQty rollupReceivedQty = pb.CalculateRollupQty<PMCommitment>(row, existing, row.ReceivedQty);
			ProjectBalance.RollupQty rollupInvoicedQty = pb.CalculateRollupQty<PMCommitment>(row, existing, row.InvoicedQty);

			PMAccountGroup ag = PXSelectorAttribute.Select<PMCommitment.accountGroupID>(sender.Graph.Caches[typeof(PMCommitment)], row) as PMAccountGroup;
			
			PMBudgetAccum ps = new PMBudgetAccum();
			ps.ProjectID = row.ProjectID;
			ps.ProjectTaskID = row.ProjectTaskID;
			ps.AccountGroupID = row.AccountGroupID;
			ps.InventoryID = existing != null ? existing.InventoryID : PMInventorySelectorAttribute.EmptyInventoryID;
			ps.CostCodeID = row.CostCodeID ?? CostCodeAttribute.GetDefaultCostCode();
			ps.UOM = rollupQty.UOM;
			if (existing != null)
			{
				ps.IsProduction = existing.IsProduction;
				ps.Type = existing.Type;
			}
			else
			{
				if ( ag != null)
				{
					if (ag.Type == PMAccountType.OffBalance)
						ps.Type = ag.IsExpense == true ? GL.AccountType.Expense : ag.Type;
					else
						ps.Type = ag.Type;
				}
			}
						
			ps = (PMBudgetAccum)sender.Graph.Caches[typeof(PMBudgetAccum)].Insert(ps);
			ps.CommittedQty += sign * rollupQty.Qty;
			ps.CommittedOpenQty += sign * rollupOpenQty.Qty;
			ps.CommittedReceivedQty += sign * rollupReceivedQty.Qty;
			ps.CommittedInvoicedQty += sign * rollupInvoicedQty.Qty;
			ps.CommittedAmount += sign * row.Amount.GetValueOrDefault();
			ps.CommittedOpenAmount += sign * row.OpenAmount.GetValueOrDefault();
			ps.CommittedInvoicedAmount += sign * row.InvoicedAmount.GetValueOrDefault();
		}

		public virtual ProjectBalance CreateProjectBalance()
		{
			return new ProjectBalance(this._Graph);
		}
	}
		
	public abstract class PMCommitmentAttribute : PXEventSubscriberAttribute, IPXRowInsertedSubscriber, IPXRowUpdatedSubscriber, IPXRowDeletedSubscriber
	{
		protected Type primaryEntity;
		protected Type detailEntity;
		
		public PMCommitmentAttribute(Type primaryEntity)
		{
			this.primaryEntity = primaryEntity;
		}

		public override void CacheAttached(PXCache sender)
		{
			base.CacheAttached(sender);

			this.detailEntity = sender.GetItemType();

			if (!PXAccess.FeatureInstalled<FeaturesSet.projectModule>())
				return;
			
			sender.Graph.RowUpdated.AddHandler(primaryEntity, DocumentRowUpdated);
		}
				
		public void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			Guid? commitmentID = (Guid?) sender.GetValue(e.Row, FieldOrdinal);

			DeleteCommitment(sender, commitmentID);
		}

		public void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			SyncCommitment(sender, e.Row);
		}

		public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			if (IsCommitmentSyncRequired(sender, e.Row, e.OldRow))
			{
				SyncCommitment(sender, e.Row);
			}
		}

		public abstract void DocumentRowUpdated(PXCache sender, PXRowUpdatedEventArgs e);
		
		protected abstract bool IsCommitmentSyncRequired(PXCache sender, object row, object oldRow);
		
		protected abstract bool EraseCommitment(PXCache sender, object row);
		
		protected abstract int? GetAccountGroup(PXCache sender, object row);
		
		protected abstract PMCommitment FromRecord(PXCache sender, object row);


		protected virtual void SyncCommitment(PXCache sender, object row)
		{
			SyncCommitment(sender, row, false);
		}
		protected virtual void SyncCommitment(PXCache sender, object row, bool skipCommitmentSelect)
		{
			if (!IsCommitmentTrackingEnabled(sender))
				return;

			PXCache detailCache = sender.Graph.Caches[detailEntity];
			Guid? commitmentID = (Guid?)detailCache.GetValue(row, FieldOrdinal);
			if (EraseCommitment(sender, row))
			{
				DeleteCommitment(sender, commitmentID);
				detailCache.SetValue(row, FieldOrdinal, null);
				detailCache.SmartSetStatus(row, PXEntryStatus.Updated);
			}
			else
			{
				int? accountGroup = GetAccountGroup(sender, row);

				PMCommitment commitment = null;
				if (!skipCommitmentSelect && commitmentID != null)
				{
					commitment = PXSelect<PMCommitment, Where<PMCommitment.commitmentID, Equal<Required<PMCommitment.commitmentID>>>>.Select(sender.Graph, commitmentID);
				}

				if (commitment == null)
				{
					commitment = FromRecord(sender, row);

					sender.Graph.Caches[typeof(PMCommitment)].Insert(commitment);

					if (commitment.CommitmentID != commitmentID)
					{
						detailCache.SetValue(row, FieldOrdinal, commitment.CommitmentID);
						detailCache.SmartSetStatus(row, PXEntryStatus.Updated);
					}
				}
				else
				{
					PMCommitment container = FromRecord(sender, row);
					commitment.AccountGroupID = accountGroup;
					commitment.ProjectID = container.ProjectID;
					commitment.ProjectTaskID = container.TaskID;
					commitment.UOM = container.UOM;
					commitment.Qty = container.Qty;
					commitment.Amount = container.Amount;
					commitment.ReceivedQty = container.ReceivedQty;
					commitment.OpenQty = container.OpenQty;
					commitment.OpenAmount = container.OpenAmount;
					if (container.InvoicedIsReadonly != true)
					{
						commitment.InvoicedQty = container.InvoicedQty;
						commitment.InvoicedAmount = container.InvoicedAmount;
					}
					commitment.InvoicedIsReadonly = container.InvoicedIsReadonly;
					commitment.RefNoteID = container.RefNoteID;
					commitment.InventoryID = container.InventoryID;
					commitment.CostCodeID = container.CostCodeID;

					sender.Graph.Caches[typeof(PMCommitment)].Update(commitment);

				}
			}
		}

		protected virtual void DeleteCommitment(PXCache sender, Guid? commitmentID)
		{
			if (commitmentID != null)
			{
				PMCommitment commitment = PXSelect<PMCommitment, Where<PMCommitment.commitmentID, Equal<Required<PMCommitment.commitmentID>>>>.Select(sender.Graph, commitmentID);
				if (commitment != null )
				{
					sender.Graph.Caches[typeof(PMCommitment)].Delete(commitment);
				}
			}
		}

		protected virtual bool IsCommitmentTrackingEnabled(PXCache sender)
		{
			if (!PXAccess.FeatureInstalled<FeaturesSet.projectModule>())
				return false;

			PMSetup setup = PXSelect<PMSetup>.Select(sender.Graph);

			if (setup == null)
				return false;

			return setup.CommitmentTracking == true;
		}

		protected virtual void AddInvoiced(PXCache sender, PMCommitment container)
		{
			AddInvoicedBase(sender, container);
		}

		private static void AddInvoicedBase(PXCache sender, PMCommitment container)
		{
			PMCommitment commitment = (PMCommitment)sender.Graph.Caches[typeof(PMCommitment)].Locate(container);
			if (commitment == null)
			{
				commitment = PXSelect<PMCommitment, Where<PMCommitment.commitmentID, Equal<Required<PMCommitment.commitmentID>>>>.Select(sender.Graph, container.CommitmentID);
			}

			if (commitment != null)
			{
				decimal quantity = container.InvoicedQty.GetValueOrDefault();
				if (commitment.UOM != container.UOM)
				{
					if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
					{
						decimal inBase = IN.INUnitAttribute.ConvertToBase(sender.Graph.Caches[typeof(PMCommitment)], commitment.InventoryID, container.UOM, quantity, IN.INPrecision.QUANTITY);
						try
						{
							quantity = IN.INUnitAttribute.ConvertFromBase(sender.Graph.Caches[typeof(PMCommitment)], commitment.InventoryID, commitment.UOM, inBase, IN.INPrecision.QUANTITY);
						}
						catch (PX.Objects.IN.PXUnitConversionException ex)
						{
							IN.InventoryItem item = PXSelectorAttribute.Select(sender.Graph.Caches[typeof(PMCommitment)], commitment, "inventoryID") as IN.InventoryItem;
							string msg = PXMessages.LocalizeFormatNoPrefixNLA(Messages.UnitConversionNotDefinedForItemOnBudgetUpdate, item?.BaseUnit, commitment.UOM, item?.InventoryCD);

							throw new PXException(msg, ex);
						}
					}
					else
					{
						quantity = IN.INUnitAttribute.ConvertGlobalUnits(sender.Graph, container.UOM, commitment.UOM, quantity, IN.INPrecision.QUANTITY);
					}
				}

				commitment.InvoicedAmount += container.InvoicedAmount.GetValueOrDefault();
				commitment.InvoicedQty += quantity;

				sender.Graph.Caches[typeof(PMCommitment)].Update(commitment);
			}
		}

		public static void Sync(PXCache sender, object data)
		{
			PMCommitmentAttribute commitmentAttribute = null;
			foreach (PXEventSubscriberAttribute attr in sender.GetAttributesReadonly("commitmentID"))
			{
				if (attr is PMCommitmentAttribute)
				{
					commitmentAttribute = (PMCommitmentAttribute)attr;
					break;
				}
			}

			if (commitmentAttribute != null)
			{
				commitmentAttribute.SyncCommitment(sender, data, true);
			}
		}

		public static void AddToInvoiced(PXCache sender, PMCommitment container)
		{
			PMCommitmentAttribute commitmentAttribute = null;
			foreach (PXEventSubscriberAttribute attr in sender.GetAttributesReadonly("commitmentID"))
			{
				if (attr is PMCommitmentAttribute)
				{
					commitmentAttribute = (PMCommitmentAttribute)attr;
					break;
				}
			}

			if (commitmentAttribute != null)
			{
				commitmentAttribute.AddInvoiced(sender, container);
			}
			else
			{
				AddInvoicedBase(sender, container);
			}
		}
	}

	public class POCommitmentAttribute : PMCommitmentAttribute
	{
		public POCommitmentAttribute():base(typeof(POOrder))
		{
		}

		public override void DocumentRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			POOrder row = e.Row as POOrder;
			POOrder oldRow = e.OldRow as POOrder;
			
			if (IsCommitmentSyncRequired(sender, row, oldRow))
			{
				var selectDetails = new PXSelect<POLine, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Optional<POOrder.orderNbr>>>>, OrderBy<Asc<POLine.orderType, Asc<POLine.orderNbr, Asc<POLine.lineNbr>>>>>(sender.Graph);
				foreach (POLine line in selectDetails.Select())
				{
					this.SyncCommitment(sender, line);
				}
			}
		}

		protected override bool EraseCommitment(PXCache sender, object row)
		{
			POLine poline = (POLine)row;
			POOrder order = (POOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(POOrder));
			
			if (order.OrderType == POOrderType.Blanket || order.OrderType == POOrderType.StandardBlanket || order.Hold == true || order.Approved != true || ((order.Cancelled == true || poline.Cancelled == true) && poline.ReceivedQty == 0) || poline.TaskID == null )
			{
				return true;
			}
			else
			{
				return GetAccountGroup(sender, row) == null;
			}
		}

		protected override PMCommitment FromRecord(PXCache sender, object row)
		{
			POLine poline = (POLine)row;
			POOrder order = (POOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(POOrder));

			PMCommitment commitment = new PMCommitment();
			commitment.Type = PMCommitmentType.Internal;
			commitment.CommitmentID = poline.CommitmentID ?? Guid.NewGuid();
			commitment.AccountGroupID = GetAccountGroup(sender, row);
			commitment.ProjectID = poline.ProjectID;
			commitment.ProjectTaskID = poline.TaskID;
			commitment.UOM = poline.UOM;
			commitment.Qty = poline.OrderQty;
			commitment.Amount = poline.ExtCost;
			commitment.OpenQty = poline.OpenQty;
			commitment.OpenAmount = poline.OpenAmt;
			commitment.ReceivedQty = poline.ReceivedQty;
			commitment.InvoicedQty = 0;
			commitment.InvoicedAmount = 0;
			commitment.InvoicedIsReadonly = true;
			commitment.RefNoteID = order.NoteID;
			commitment.InventoryID = poline.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID;
			commitment.CostCodeID = poline.CostCodeID ?? CostCodeAttribute.GetDefaultCostCode();

			if (poline.Cancelled == true)
			{
				commitment.Qty = poline.ReceivedQty;
				commitment.Amount = poline.ReceivedCost;
			}

			return commitment;
		}

		protected override int? GetAccountGroup(PXCache sender, object row)
		{
			POLine poline = (POLine)row;
			InventoryItem item = (InventoryItem)PXSelectorAttribute.Select<POLine.inventoryID>(sender.Graph.Caches[detailEntity], row);
			if (item != null && item.StkItem == true && item.COGSAcctID != null)
			{
				Account account = (Account)PXSelectorAttribute.Select<InventoryItem.cOGSAcctID>(sender.Graph.Caches[typeof(InventoryItem)], item);
				if (account != null && account.AccountGroupID != null)
				{
					return account.AccountGroupID;
				}
			}
			else
			{
				Account account = (Account)PXSelectorAttribute.Select<POLine.expenseAcctID>(sender.Graph.Caches[detailEntity], row);
				if (account != null && account.AccountGroupID != null)
				{
					return account.AccountGroupID;
				}
			}


			return null;
		}

		protected override bool IsCommitmentSyncRequired(PXCache sender, object row, object oldRow)
		{
			return IsCommitmentSyncRequired((POLine)row, (POLine)oldRow);
		}

		private bool IsCommitmentSyncRequired(POLine row, POLine oldRow)
		{
			return row.OrderQty != oldRow.OrderQty
				|| row.ExtCost != oldRow.ExtCost
				|| row.OpenQty != oldRow.OpenQty
				|| row.OpenAmt != oldRow.OpenAmt
				|| row.RequestedDate != oldRow.RequestedDate
				|| row.ProjectID != oldRow.ProjectID
				|| row.TaskID != oldRow.TaskID
				|| row.ExpenseAcctID != oldRow.ExpenseAcctID
				|| row.InventoryID != oldRow.InventoryID
				|| row.CostCodeID != oldRow.CostCodeID
				|| row.UOM != oldRow.UOM;
		}

		protected virtual bool IsCommitmentSyncRequired(PXCache sender, POOrder row, POOrder oldRow)
		{
			bool? originalApproved = (bool?)sender.GetValueOriginal<POOrder.approved>(row); //This hack is required cause when the document is pre-approved by EPApprovalAutomation the RowUpdated event is not fired. 

			return row.Hold != oldRow.Hold || row.Cancelled != oldRow.Cancelled || row.Approved != oldRow.Approved || (row.Approved == true && originalApproved != true);
		}

	}

	public class POCommitmentExAttribute : POCommitmentAttribute
	{
		protected override bool EraseCommitment(PXCache sender, object row)
		{
			POLineUOpen poline = (POLineUOpen)row;
			POOrder order = (POOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(POOrder));

			if (order.OrderType == POOrderType.Blanket || order.OrderType == POOrderType.StandardBlanket || order.Hold == true || order.Approved != true || order.Cancelled == true || poline.Cancelled == true || poline.TaskID == null)
			{
				return true;
			}
			else
			{
				return GetAccountGroup(sender, row) == null;
			}
		}

		protected override PMCommitment FromRecord(PXCache sender, object row)
		{
			POLineUOpen poline = (POLineUOpen)row;
			POOrder order = (POOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(POOrder));

			PMCommitment commitment = new PMCommitment();
			commitment.Type = PMCommitmentType.Internal;
			commitment.CommitmentID = poline.CommitmentID ?? Guid.NewGuid();
			commitment.AccountGroupID = GetAccountGroup(sender, row);
			commitment.ProjectID = poline.ProjectID;
			commitment.ProjectTaskID = poline.TaskID;
			commitment.UOM = poline.UOM;
			commitment.Qty = poline.OrderQty;
			commitment.Amount = poline.ExtCost;
			commitment.OpenQty = poline.OpenQty;
			commitment.OpenAmount = poline.OpenAmt;
			commitment.ReceivedQty = poline.ReceivedQty;
			commitment.InvoicedQty = 0;
			commitment.InvoicedAmount = 0;
			commitment.InvoicedIsReadonly = true;
			commitment.RefNoteID = order.NoteID;
			commitment.InventoryID = poline.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID;
			commitment.CostCodeID = poline.CostCodeID ?? CostCodeAttribute.GetDefaultCostCode();

			return commitment;
		}

		protected override int? GetAccountGroup(PXCache sender, object row)
		{
			POLineUOpen poline = (POLineUOpen)row;
			InventoryItem item = PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Select(sender.Graph, poline.InventoryID);
			if (item != null && item.StkItem == true && item.COGSAcctID != null)
			{
				Account account = (Account)PXSelectorAttribute.Select<InventoryItem.cOGSAcctID>(sender.Graph.Caches[typeof(InventoryItem)], item);
				if (account != null && account.AccountGroupID != null)
				{
					return account.AccountGroupID;
				}
			}
			else
			{
				Account account = (Account)PXSelectorAttribute.Select<POLineUOpen.expenseAcctID>(sender.Graph.Caches[detailEntity], row);
				if (account != null && account.AccountGroupID != null)
				{
					return account.AccountGroupID;
				}
			}

			return null;
		}

		protected override bool IsCommitmentSyncRequired(PXCache sender, object row, object oldRow)
		{
			return IsCommitmentSyncRequired((POLineUOpen)row, (POLineUOpen)oldRow);
		}

		private bool IsCommitmentSyncRequired(POLineUOpen row, POLineUOpen oldRow)
		{
			return row.OrderQty != oldRow.OrderQty
				|| row.ExtCost != oldRow.ExtCost
				|| row.OpenQty != oldRow.OpenQty
				|| row.OpenAmt != oldRow.OpenAmt
				|| row.RequestedDate != oldRow.RequestedDate
				|| row.ProjectID != oldRow.ProjectID
				|| row.TaskID != oldRow.TaskID
				|| row.ExpenseAcctID != oldRow.ExpenseAcctID
				|| row.InventoryID != oldRow.InventoryID
				|| row.CostCodeID != oldRow.CostCodeID
				|| row.UOM != oldRow.UOM;
		}
	}

	public class SOCommitmentAttribute : PMCommitmentAttribute
	{
		public SOCommitmentAttribute() : base(typeof(SOOrder))
		{
		}

		public override void DocumentRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			SOOrder row = e.Row as SOOrder;
			SOOrder oldRow = e.OldRow as SOOrder;

			if (IsCommitmentSyncRequired(row, oldRow))
			{
				var selectDetails = new PXSelect<SOLine, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Optional<SOOrder.orderNbr>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.lineNbr>>>>>(sender.Graph);
				foreach (SOLine line in selectDetails.Select())
				{
					this.SyncCommitment(sender, line);
				}
			}
		}

		protected override bool EraseCommitment(PXCache sender, object row)
		{
			SOLine line = (SOLine)row;
			SOOrder order = (SOOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(SOOrder));
			SOOrderType orderType = (SOOrderType) PXSelectorAttribute.Select<SOOrder.orderType>(sender.Graph.Caches[typeof(SOOrder)], order);
			
			if (order.Hold == true && order.ShipmentCntr == 0 || order.Cancelled == true && order.ShipmentCntr == 0 || orderType.CommitmentTracking != true || line.TaskID == null)
			{
				return true;
			}
			else
			{
				return GetAccountGroup(sender, row) == null;
			}
		}

		protected override PMCommitment FromRecord(PXCache sender, object row)
		{
			SOLine line = (SOLine)row;
			SOOrder order = (SOOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(SOOrder));

			int sign = line.Operation == SOOperation.Issue ? 1 : -1;

			PMCommitment commitment = new PMCommitment();
			commitment.Type = PMCommitmentType.Internal;
			commitment.CommitmentID = line.CommitmentID ?? Guid.NewGuid();
			commitment.AccountGroupID = GetAccountGroup(sender, row);
			commitment.ProjectID = line.ProjectID;
			commitment.ProjectTaskID = line.TaskID;
			commitment.UOM = line.UOM;
			commitment.Qty = sign * line.OrderQty;
			commitment.Amount = sign * line.LineAmt;
			if (order.Hold == true || order.Cancelled == true)
			{
				//Back Order put on Hold.
				commitment.OpenQty = 0;
				commitment.OpenAmount = 0;
			}
			else
			{
				commitment.OpenQty = sign * (line.RequireShipping == true ? line.OpenQty : line.UnbilledQty);
				commitment.OpenAmount = sign * (line.RequireShipping == true ? line.OpenAmt : line.UnbilledAmt);
			}
			commitment.ReceivedQty = sign * line.ShippedQty;
			commitment.InvoicedQty = 0;
			commitment.InvoicedAmount = 0;
			commitment.InvoicedIsReadonly = true;
			commitment.RefNoteID = order.NoteID;
			commitment.InventoryID = line.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID;
			commitment.CostCodeID = line.CostCodeID ?? CostCodeAttribute.GetDefaultCostCode();

			return commitment;
		}

		protected override int? GetAccountGroup(PXCache sender, object row)
		{
			Account account = (Account)PXSelectorAttribute.Select<SOLine.salesAcctID>(sender.Graph.Caches[detailEntity], row);
			if (account != null && account.AccountGroupID != null)
			{
				return account.AccountGroupID;
			}
			
			return null;
		}

		protected override bool IsCommitmentSyncRequired(PXCache sender, object row, object oldRow)
		{
			return IsCommitmentSyncRequired((SOLine)row, (SOLine)oldRow);
		}

		private bool IsCommitmentSyncRequired(SOLine row, SOLine oldRow)
		{
			return row.OrderQty != oldRow.OrderQty
				|| row.LineAmt != oldRow.LineAmt
				|| row.UnbilledQty != oldRow.UnbilledQty
				|| row.UnbilledAmt != oldRow.UnbilledAmt
				|| row.OpenQty != oldRow.OpenQty
				|| row.OpenAmt != oldRow.OpenAmt
				|| row.ShipDate != oldRow.ShipDate
				|| row.ProjectID != oldRow.ProjectID
				|| row.TaskID != oldRow.TaskID
				|| row.SalesAcctID != oldRow.SalesAcctID
				|| row.InventoryID != oldRow.InventoryID
				|| row.CostCodeID != oldRow.CostCodeID
				|| row.Completed != oldRow.Completed
				|| row.UOM != oldRow.UOM;
		}

		protected virtual bool IsCommitmentSyncRequired(SOOrder row, SOOrder oldRow)
		{
			return row.Hold != oldRow.Hold || row.Cancelled != oldRow.Cancelled || row.Approved != oldRow.Approved;
		}
	}

	public class SOCommitmentMiscLineAttribute : SOCommitmentAttribute
	{
		protected override bool EraseCommitment(PXCache sender, object row)
		{
			SOMiscLine2 line = (SOMiscLine2)row;
			SOOrder order = (SOOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(SOOrder));
			SOOrderType orderType = (SOOrderType)PXSelectorAttribute.Select<SOOrder.orderType>(sender.Graph.Caches[typeof(SOOrder)], order);

			if (order.Hold == true || order.Cancelled == true || orderType.CommitmentTracking != true || line.TaskID == null)
			{
				return true;
			}
			else
			{
				return GetAccountGroup(sender, row) == null;
			}
		}

		protected override PMCommitment FromRecord(PXCache sender, object row)
		{
			SOMiscLine2 line = (SOMiscLine2)row;
			SOOrder order = (SOOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(SOOrder));

			int sign = line.Operation == SOOperation.Issue ? 1 : -1;

			PMCommitment commitment = new PMCommitment();
			commitment.Type = PMCommitmentType.Internal;
			commitment.CommitmentID = line.CommitmentID ?? Guid.NewGuid();
			commitment.AccountGroupID = GetAccountGroup(sender, row);
			commitment.ProjectID = line.ProjectID;
			commitment.ProjectTaskID = line.TaskID;
			commitment.UOM = line.UOM;
			commitment.Qty = sign * line.OrderQty;
			commitment.Amount = sign * line.LineAmt;
			commitment.OpenQty = sign * line.UnbilledQty;
			commitment.OpenAmount = sign * line.UnbilledAmt;
			commitment.ReceivedQty = 0;
			commitment.InvoicedQty = 0;
			commitment.InvoicedAmount = 0;
			commitment.InvoicedIsReadonly = true;
			commitment.RefNoteID = order.NoteID;
			commitment.InventoryID = line.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID;
			commitment.CostCodeID = line.CostCodeID ?? CostCodeAttribute.GetDefaultCostCode();

			return commitment;
		}

		protected override int? GetAccountGroup(PXCache sender, object row)
		{
			Account account = (Account)PXSelectorAttribute.Select<SOMiscLine2.salesAcctID>(sender.Graph.Caches[detailEntity], row);
			if (account != null && account.AccountGroupID != null)
			{
				return account.AccountGroupID;
			}

			return null;
		}

		protected override bool IsCommitmentSyncRequired(PXCache sender, object row, object oldRow)
		{
			return IsCommitmentSyncRequired((SOMiscLine2)row, (SOMiscLine2)oldRow);
		}

		private bool IsCommitmentSyncRequired(SOMiscLine2 row, SOMiscLine2 oldRow)
		{
			return row.LineAmt != oldRow.LineAmt
				|| row.UnbilledQty != oldRow.UnbilledQty
				|| row.UnbilledAmt != oldRow.UnbilledAmt
				|| row.ProjectID != oldRow.ProjectID
				|| row.TaskID != oldRow.TaskID
				|| row.SalesAcctID != oldRow.SalesAcctID
				|| row.InventoryID != oldRow.InventoryID
				|| row.CostCodeID != oldRow.CostCodeID
				|| row.UOM != oldRow.UOM;
		}

		protected override bool IsCommitmentSyncRequired(SOOrder row, SOOrder oldRow)
		{
			return false;
		}
	}

	public class SOCommitmentLine4Attribute : SOCommitmentAttribute
	{
		protected override bool EraseCommitment(PXCache sender, object row)
		{
			SOOrder order = (SOOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(SOOrder));
			SOOrderType orderType = (SOOrderType)PXSelectorAttribute.Select<SOOrder.orderType>(sender.Graph.Caches[typeof(SOOrder)], order);

			if (order.Hold == true || order.Cancelled == true || orderType.CommitmentTracking != true || sender.GetValue(row, "TaskID") == null)
			{
				return true;
			}
			else
			{
				return GetAccountGroup(sender, row) == null;
			}
		}

		protected override PMCommitment FromRecord(PXCache sender, object row)
		{
			SOLine4 line = (SOLine4)row;
			SOOrder order = (SOOrder)PXParentAttribute.SelectParent(sender.Graph.Caches[detailEntity], row, typeof(SOOrder));

			int sign = line.Operation == SOOperation.Issue ? 1 : -1;

			PMCommitment commitment = new PMCommitment();
			commitment.Type = PMCommitmentType.Internal;
			commitment.CommitmentID = (Guid?)sender.GetValue(row, "CommitmentID") ?? Guid.NewGuid();
			commitment.AccountGroupID = GetAccountGroup(sender, row);
			commitment.ProjectID = (int?)sender.GetValue(row, "ProjectID");
			commitment.ProjectTaskID = (int?)sender.GetValue(row, "TaskID");
			commitment.UOM = line.UOM;
			commitment.Qty = sign * line.OrderQty;
			commitment.Amount = sign * (decimal?)sender.GetValue(row, "LineAmt");
			if (order.Hold == true || order.Cancelled == true)
			{
				//Back Order put on Hold.
				commitment.OpenQty = 0;
				commitment.OpenAmount = 0;
			}
			else
			{
				commitment.OpenQty = sign * line.OpenQty;
				commitment.OpenAmount = sign * line.OpenAmt;
			}
			commitment.ReceivedQty = sign * line.ShippedQty;
			commitment.InvoicedQty = 0;
			commitment.InvoicedAmount = 0;
			commitment.InvoicedIsReadonly = true;
			commitment.RefNoteID = order.NoteID;
			commitment.InventoryID = line.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID;

			return commitment;
		}

		protected override int? GetAccountGroup(PXCache sender, object row)
		{
			Account account = (Account)PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Select(sender.Graph, sender.GetValue(row, "SalesAcctID"));
			if (account != null && account.AccountGroupID != null)
			{
				return account.AccountGroupID;
			}

			return null;
		}

		protected override bool IsCommitmentSyncRequired(PXCache sender, object row, object oldRow)
		{
			return IsCommitmentSyncRequired((SOLine4)row, (SOLine4)oldRow);
		}

		private bool IsCommitmentSyncRequired(SOLine4 row, SOLine4 oldRow)
		{
			return row.UnbilledQty != oldRow.UnbilledQty
			  || row.UnbilledAmt != oldRow.UnbilledAmt
			  || row.OpenQty != oldRow.OpenQty
			  || row.OpenAmt != oldRow.OpenAmt;

		}

		protected override bool IsCommitmentSyncRequired(SOOrder row, SOOrder oldRow)
		{
			return false;
		}


	}
}
