using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.PM
{
	public class ProjectBalance
	{
		protected PXGraph graph;
		protected ProjectSettingsManager settings;

		public ProjectBalance(PXGraph graph)
		{
			if (graph == null)
				throw new ArgumentNullException();

			this.graph = graph;
			this.settings = new ProjectSettingsManager();
		}

		public virtual List<Result> Calculate(PMTran tran, Account acc, PMAccountGroup ag, Account offsetAcc, PMAccountGroup offsetAg)
		{
			List<Result> list = new List<Result>();
			if (tran.TaskID != null)
			{
				int invert = 1;
				if (tran.TranType == BatchModule.PM && acc.Type != ag.Type)
				{
					//Invert transactions that originated in PM. All other transactions were already inverted when they were transformed from GLTran to PMTran.
					invert = -1;
				}

				bool debitcreditcancelout = false;
				if (offsetAcc != null && acc.AccountID == offsetAcc.AccountID)
				{
					debitcreditcancelout = true;
				}

				if (string.IsNullOrEmpty(ag.Type))
				{
					//offbalance tran.
					PMAccountGroup tranAG = PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Required<PMAccountGroup.groupID>>>>.Select(graph, tran.AccountGroupID);
					if (tranAG != null)
					{
						//DEBIT ONLY
						if (tranAG.Type == AccountType.Income || tranAG.Type == AccountType.Liability)
							list.Add(Calculate(tran, tranAG, null, -1 * invert));
						else
							list.Add(Calculate(tran, tranAG, null, 1 * invert));
					}

				}
				else
				{
					if (!debitcreditcancelout)
					{
						//DEBIT
						if (acc.Type == AccountType.Income || acc.Type == AccountType.Liability)
							list.Add(Calculate(tran, ag, acc, -1 * invert));
						else
							list.Add(Calculate(tran, ag, acc, 1 * invert));
					}
				}
				
				//CREDIT				
				if (offsetAcc != null && offsetAg != null && offsetAcc.AccountID != null && offsetAg.GroupID != null && !debitcreditcancelout)
				{
					int offsetInvert = 1;
					if (offsetAcc.Type != offsetAg.Type)
					{
						offsetInvert = -1;
					}

					if (offsetAcc.Type == AccountType.Income || offsetAcc.Type == AccountType.Liability)
						list.Add(Calculate(tran, offsetAg, offsetAcc, 1 * offsetInvert));
					else
						list.Add(Calculate(tran, offsetAg, offsetAcc, -1 * offsetInvert));
				}
			}

			return list;
		}

		public virtual Result Calculate(PMTran pmt, PMAccountGroup ag, Account acc, int mult)
		{
			PMBudget status = SelectProjectBalance(pmt);

			int? inventoryID = status != null ? status.InventoryID : PMInventorySelectorAttribute.EmptyInventoryID;
			int? costCodeID = status != null ? status.CostCodeID : CostCodeAttribute.GetDefaultCostCode();
			RollupQty rollup = null;

			if (settings.CostBudgetUpdateMode == CostBudgetUpdateModes.Detailed && ag.IsExpense == true && pmt.InventoryID != settings.EmptyInventoryID)
			{
				if (status == null || status.InventoryID == settings.EmptyInventoryID )
				{
					rollup = new RollupQty(pmt.UOM, pmt.Qty);
					inventoryID = pmt.InventoryID;
					if (pmt.CostCodeID != null)
					{
						costCodeID = pmt.CostCodeID;
					}
				}
			}

			if (rollup == null)
				rollup = CalculateRollupQty(pmt, status);

			List<PMHistory> list = new List<PMHistory>();

			PMTaskTotal ta = null;
			PMBudget ps = null;
			
			if (pmt.TaskID != null && (rollup.Qty != 0 || pmt.Amount != 0)) //TaskID will be null for Contract
			{				
				ps = new PMBudget();
				ps.ProjectID = pmt.ProjectID;
				ps.ProjectTaskID = pmt.TaskID;
				ps.AccountGroupID = ag.GroupID;
				if (ag.Type == PMAccountType.OffBalance)
					ps.Type = ag.IsExpense == true ? GL.AccountType.Expense : ag.Type;
				else
					ps.Type = ag.Type;
				ps.InventoryID = inventoryID;
				ps.CostCodeID = costCodeID;
				ps.UOM = rollup.UOM;
				if (status != null)
				{
					ps.IsProduction = status.IsProduction;
				}
								
				decimal amt = mult * pmt.Amount.GetValueOrDefault();

				if (!string.IsNullOrEmpty(ps.UOM))
					ps.ActualQty = rollup.Qty; // commented out otherwise invoice produces -ve Qty. * mult;
				ps.ActualAmount = amt;
				
				#region PMTask Totals Update

				ta = new PMTaskTotal();
				ta.ProjectID = pmt.ProjectID;
				ta.TaskID = pmt.TaskID;
								
				string accType = null;
				int multFix = 1;//flip back the sign if it was changed because of ag.Type<>acc.type
				if (pmt.TranType == BatchModule.PM && acc != null && !string.IsNullOrEmpty(acc.Type))
				{
					//Only transactions that originated in PM were inverted and require to be fixed.
					accType = ag.Type;

					if (acc.Type != ag.Type)
						multFix = -1;

				}
				else
				{
					accType = ag.Type;
				}

				switch (accType)
				{
					case AccountType.Asset:
						ta.Asset = amt * multFix;
						break;
					case AccountType.Liability:
						ta.Liability = amt * multFix;
						break;
					case AccountType.Income:
						ta.Income = amt * multFix;
						break;
					case AccountType.Expense:
						ta.Expense = amt * multFix;
						break;
				}
				
				#endregion

				#region History
				PMHistory hist = new PMHistory();
				hist.ProjectID = pmt.ProjectID;
				hist.ProjectTaskID = pmt.TaskID;
				hist.AccountGroupID = ag.GroupID;
				hist.InventoryID = pmt.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID;
				hist.CostCodeID = pmt.CostCodeID ?? CostCodeAttribute.GetDefaultCostCode();
				hist.PeriodID = pmt.FinPeriodID;
				decimal baseQty = 0;
				list.Add(hist);
				if (pmt.InventoryID != null && pmt.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID && pmt.Qty != 0 && !string.IsNullOrEmpty(rollup.UOM) )
				{
					if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
					{
						baseQty = mult * IN.INUnitAttribute.ConvertToBase(graph.Caches[typeof(PMHistory)], pmt.InventoryID, pmt.UOM, pmt.Qty.Value, PX.Objects.IN.INPrecision.QUANTITY);
					}
					else
					{
						IN.InventoryItem initem = PXSelectorAttribute.Select<PMTran.inventoryID>(graph.Caches[typeof(PMTran)], pmt) as IN.InventoryItem;
						if (initem != null)
							baseQty = mult * IN.INUnitAttribute.ConvertGlobalUnits(graph, pmt.UOM, initem.BaseUnit, pmt.Qty ?? 0, IN.INPrecision.QUANTITY);
					}
				}
				hist.FinPTDAmount = amt;
				hist.FinYTDAmount = amt;
				hist.FinPTDQty = baseQty;
				hist.FinYTDQty = baseQty;
				if (pmt.FinPeriodID == pmt.TranPeriodID)
				{
					hist.TranPTDAmount = amt;
					hist.TranYTDAmount = amt;
					hist.TranPTDQty = baseQty;
					hist.TranYTDQty = baseQty;
				}
				else
				{
					PMHistory tranHist = new PMHistory();
					tranHist.ProjectID = pmt.ProjectID;
					tranHist.ProjectTaskID = pmt.TaskID;
					tranHist.AccountGroupID = ag.GroupID;
					tranHist.InventoryID = pmt.InventoryID ?? PM.PMInventorySelectorAttribute.EmptyInventoryID;
					tranHist.CostCodeID = pmt.CostCodeID ?? CostCodeAttribute.GetDefaultCostCode();
					tranHist.PeriodID = pmt.TranPeriodID;
					list.Add(tranHist);
					tranHist.TranPTDAmount = amt;
					tranHist.TranYTDAmount = amt;
					tranHist.TranPTDQty = baseQty;
					tranHist.TranYTDQty = baseQty;
				}
				#endregion
			}
			return new Result(list, ps, ta);
		}

		public virtual RollupQty CalculateRollupQty<T>(T row, PMBudget status) where T : IBqlTable, IQuantify
		{
			return CalculateRollupQty(row, status, row.Qty);
		}
		public virtual RollupQty CalculateRollupQty<T>(T row, PMBudget status, decimal? quantity) where T : IBqlTable, IQuantify
		{			
			string UOM = null;
			decimal rollupQty = 0;
			if (status == null)
			{
				//Status does not exist for given Inventory and <Other> is not present.
			}
			else
			{
				if (status.InventoryID == PMInventorySelectorAttribute.EmptyInventoryID)
				{
					//<Other> item is present. Update only if UOMs are convertable.
					decimal convertedQty;
					if (IN.INUnitAttribute.TryConvertGlobalUnits(graph, row.UOM, status.UOM, quantity.GetValueOrDefault(), IN.INPrecision.QUANTITY, out convertedQty))
					{
						rollupQty = convertedQty;
						UOM = status.UOM;
					}
				}
				else
				{
					UOM = status.UOM;

					//Item matches. Convert to UOM of ProjectStatus.
					if (status.UOM != row.UOM && !string.IsNullOrEmpty(status.UOM) &&!string.IsNullOrEmpty(row.UOM))
					{
						if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
						{
							decimal inBase = IN.INUnitAttribute.ConvertToBase(graph.Caches[typeof(T)], row.InventoryID, row.UOM, quantity ?? 0, IN.INPrecision.QUANTITY);
							try
							{
								rollupQty = IN.INUnitAttribute.ConvertFromBase(graph.Caches[typeof(T)], row.InventoryID, status.UOM, inBase, IN.INPrecision.QUANTITY);
							}
							catch (PX.Objects.IN.PXUnitConversionException ex)
							{
								IN.InventoryItem item = PXSelectorAttribute.Select(graph.Caches[typeof(T)], row, "inventoryID") as IN.InventoryItem;
								string msg = PXMessages.LocalizeFormatNoPrefixNLA(Messages.UnitConversionNotDefinedForItemOnBudgetUpdate, item?.BaseUnit, status.UOM, item?.InventoryCD);

								throw new PXException(msg, ex);

							}
						}
						else
						{
							rollupQty = IN.INUnitAttribute.ConvertGlobalUnits(graph, row.UOM, status.UOM, quantity ?? 0, IN.INPrecision.QUANTITY);
						}
					}
					else if(!string.IsNullOrEmpty(status.UOM))
					{
						rollupQty = quantity ?? 0;
					}
				}
			}

			return new RollupQty(UOM, rollupQty);
		}


		public virtual PMBudget SelectProjectBalance(IProjectFilter filter)
		{
			if (CostCodeAttribute.UseCostCode())
			{
				return SelectProjectBalanceByCostCodes(filter);
			}
			else
			{
				return SelectProjectBalanceByInventory(filter);
			}
		}

		public virtual PMBudget SelectProjectBalanceByCostCodes(IProjectFilter filter)
		{
			PXSelectBase<PMBudget> selectBudget = new PXSelect<PMBudget,
				Where<PMBudget.accountGroupID, Equal<Required<PMBudget.accountGroupID>>,
				And<PMBudget.projectID, Equal<Required<PMBudget.projectID>>,
				And<PMBudget.projectTaskID, Equal<Required<PMBudget.projectTaskID>>,
				And<Where<PMBudget.costCodeID, Equal<Required<PMBudget.costCodeID>>, Or<PMBudget.costCodeID, Equal<Required<PMBudget.costCodeID>>>>>>>>>(graph);

			PMBudget withCostCode = null;
			PMBudget withoutCostCode = null;

			foreach (PMBudget item in selectBudget.Select(filter.AccountGroupID, filter.ProjectID, filter.TaskID, filter.CostCodeID, CostCodeAttribute.GetDefaultCostCode()))//0..2 records
			{
				if (item.CostCodeID == CostCodeAttribute.GetDefaultCostCode())
				{
					withoutCostCode = item;
				}
				else
				{
					withCostCode = item;
				}
			}
						
			return withCostCode ?? withoutCostCode;
		}

		public virtual PMBudget SelectProjectBalanceByInventory(IProjectFilter filter)
		{
			PXSelectBase<PMBudget> selectBudget = new PXSelect<PMBudget,
				Where<PMBudget.accountGroupID, Equal<Required<PMBudget.accountGroupID>>,
				And<PMBudget.projectID, Equal<Required<PMBudget.projectID>>,
				And<PMBudget.projectTaskID, Equal<Required<PMBudget.projectTaskID>>,
				And<Where<PMBudget.inventoryID, Equal<Required<PMBudget.inventoryID>>, Or<PMBudget.inventoryID, Equal<Required<PMBudget.inventoryID>>>>>>>>>(graph);

			PMBudget withInventory = null;
			PMBudget withoutInventory = null;

			foreach (PMBudget item in selectBudget.Select(filter.AccountGroupID, filter.ProjectID, filter.TaskID, filter.InventoryID, PMInventorySelectorAttribute.EmptyInventoryID))//0..2 records
			{
				if (item.InventoryID == PMInventorySelectorAttribute.EmptyInventoryID)
				{
					withoutInventory = item;
				}
				else
				{
					withInventory = item;
				}
			}

			return withInventory ?? withoutInventory;
		}


		public class RollupQty
		{
			public RollupQty(string uom, decimal? qty)
			{
				this.UOM = uom;
				this.Qty = qty;
			}

			public string UOM { get; protected set; }
			public decimal? Qty { get; protected set; }
		} 

		public class Result
		{
			public Result(IList<PMHistory> history, PMBudget status, PMTaskTotal taskTotal)
			{
				this.History = history;
				this.Status = status;
				this.TaskTotal = taskTotal;
			}

			public IList<PMHistory> History	{ get; protected set; }
			public PMBudget Status { get; protected set; }
			public PMTaskTotal TaskTotal { get; protected set; }
		}
	}

	public interface IProjectFilter
	{
		int? AccountGroupID { get; }
		int? ProjectID { get; }
		int? TaskID { get; }
		int? InventoryID { get; }
		int? CostCodeID { get; }
	}

	public interface IQuantify
	{
		int? InventoryID { get; }
		string UOM { get; }
		decimal? Qty { get; }
	}
}
