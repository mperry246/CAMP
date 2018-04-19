using System;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.TX;
using System.Collections;
using PX.Objects.CS;
using System.Collections.Generic;

namespace PX.Objects.PM
{
	[PXCacheName(Messages.Budget)]
	[Serializable]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMBudget : PX.Data.IBqlTable, IProjectFilter
	{
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
		[PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<projectID>>, And<PMBudget.type, Equal<Current<PMBudget.type>>>>>))]
		[PXDBLiteDefault(typeof(PMProject.contractID))]
		[PXDBInt(IsKey = true)]
		public virtual Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ProjectTaskID
		public abstract class projectTaskID : PX.Data.IBqlField
		{
		}

		public int? TaskID => ProjectTaskID;
		
		[PXParent(typeof(Select<PMTask, Where<PMTask.taskID, Equal<Current<projectTaskID>>, And<PMBudget.type, Equal<Current<PMBudget.type>>>>>))]
		[PXDBInt(IsKey = true)]
		public virtual Int32? ProjectTaskID
		{
			get;
			set;
		}
		#endregion
		#region CostCodeID
		public abstract class costCodeID : PX.Data.IBqlField
		{
		}
		protected Int32? _CostCodeID;
		[CostCode(IsKey = true)]
		public virtual Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion
		#region AccountGroupID
		public abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountGroupID;
		[PXDefault]
		[AccountGroupAttribute(IsKey=true)]
		public virtual Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
        [PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Inventory ID", Visibility = PXUIVisibility.Visible)]
		[PMInventorySelector]
		[PXParent(typeof(Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<inventoryID>>>>))]
		[PXDefault]
		public virtual Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion

		#region Type
		public abstract class type : PX.Data.IBqlField
		{
		}
		protected string _Type;
		[PXDBString(1)]
		[PXDefault]
		[PMAccountType.List]
		[PXUIField(DisplayName ="Type", Enabled = false)]
		public virtual string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}
		#endregion
		#region RevenueTaskID
		public abstract class revenueTaskID : PX.Data.IBqlField
		{
		}
		protected Int32? _RevenueTaskID;
		[PXDBInt]
		public virtual Int32? RevenueTaskID
		{
			get
			{
				return this._RevenueTaskID;
			}
			set
			{
				this._RevenueTaskID = value;
			}
		}
		#endregion
		#region RevenueInventoryID
		public abstract class revenueInventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _RevenueInventoryID;
		[PXDBInt]
		public virtual Int32? RevenueInventoryID
		{
			get
			{
				return this._RevenueInventoryID;
			}
			set
			{
				this._RevenueInventoryID = value;
			}
		}
		#endregion
		#region TaxCategoryID
		public abstract class taxCategoryID : PX.Data.IBqlField
		{
		}
		protected String _TaxCategoryID;
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Tax Category")]
		[PXSelector(typeof(TaxCategory.taxCategoryID), DescriptionField = typeof(TaxCategory.descr))]
		[PXRestrictor(typeof(Where<TaxCategory.active, Equal<True>>), TX.Messages.InactiveTaxCategory, typeof(TaxCategory.taxCategoryID))]
		public virtual String TaxCategoryID
		{
			get
			{
				return this._TaxCategoryID;
			}
			set
			{
				this._TaxCategoryID = value;
			}
		}
		#endregion

		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected String _Description;
		[PXDBString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}
		#endregion
		#region Qty
		public abstract class qty : PX.Data.IBqlField
		{
		}
		protected Decimal? _Qty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Budgeted Quantity")]
		public virtual Decimal? Qty
		{
			get
			{
				return this._Qty;
			}
			set
			{
				this._Qty = value;
			}
		}
		#endregion
		#region UOM
		public abstract class uOM : PX.Data.IBqlField
		{
		}
		protected String _UOM;
		[PXDefault(typeof(Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<PMBudget.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PMUnit(typeof(inventoryID))]
		public virtual String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		#region Rate
		public abstract class rate : PX.Data.IBqlField
		{
		}
		protected Decimal? _Rate;
		[PXDBPriceCost]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Unit Rate")]
		public virtual Decimal? Rate
		{
			get
			{
				return this._Rate;
			}
			set
			{
				this._Rate = value;
			}
		}
		#endregion
		#region Amount
		public abstract class amount : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXFormula(typeof(Mult<qty, rate>))]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Budgeted Amount")]
		public virtual Decimal? Amount
		{
			get;
			set;
		}
		#endregion
		#region RevisedQty
		public abstract class revisedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _RevisedQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Revised Quantity")]
		public virtual Decimal? RevisedQty
		{
			get
			{
				return this._RevisedQty;
			}
			set
			{
				this._RevisedQty = value;
			}
		}
		#endregion
		#region RevisedAmount
		public abstract class revisedAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _RevisedAmount;
		[PXDBBaseCury]
		[PXFormula(typeof(Mult<revisedQty, rate>))]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Revised Amount")]
		public virtual Decimal? RevisedAmount
		{
			get
			{
				return this._RevisedAmount;
			}
			set
			{
				this._RevisedAmount = value;
			}
		}
		#endregion
		#region InvoicedAmount
		public abstract class invoicedAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _InvoicedAmount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Draft Invoices Amount", Enabled = false)]
		public virtual Decimal? InvoicedAmount
		{
			get
			{
				return this._InvoicedAmount;
			}
			set
			{
				this._InvoicedAmount = value;
			}
		}
		#endregion


		#region ActualQty
		public abstract class actualQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _ActualQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Actual Quantity", Enabled=false)]
		public virtual Decimal? ActualQty
		{
			get
			{
				return this._ActualQty;
			}
			set
			{
				this._ActualQty = value;
			}
		}
		#endregion
		#region ActualAmount
		public abstract class actualAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _ActualAmount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Actual Amount", Enabled=false)]
		public virtual Decimal? ActualAmount
		{
			get
			{
				return this._ActualAmount;
			}
			set
			{
				this._ActualAmount = value;
			}
		}
		#endregion
		#region ChangeOrderQty
		public abstract class changeOrderQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _ChangeOrderQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Change Orders Quantity", Enabled = false, FieldClass = CostCodeAttribute.COSTCODE)]
		public virtual Decimal? ChangeOrderQty
		{
			get
			{
				return this._ChangeOrderQty;
			}
			set
			{
				this._ChangeOrderQty = value;
			}
		}
		#endregion
		#region ChangeOrderAmount
		public abstract class changeOrderAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _ChangeOrderAmount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Change Orders Amount", Enabled = false, FieldClass = CostCodeAttribute.COSTCODE)]
		public virtual Decimal? ChangeOrderAmount
		{
			get
			{
				return this._ChangeOrderAmount;
			}
			set
			{
				this._ChangeOrderAmount = value;
			}
		}
		#endregion
		#region CommittedQty
		public abstract class committedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _CommittedQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Committed Quantity", Enabled = false)]
		public virtual Decimal? CommittedQty
		{
			get
			{
				return this._CommittedQty;
			}
			set
			{
				this._CommittedQty = value;
			}
		}
		#endregion
		#region CommittedAmount
		public abstract class committedAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _CommittedAmount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Committed Amount", Enabled = false)]
		public virtual Decimal? CommittedAmount
		{
			get
			{
				return this._CommittedAmount;
			}
			set
			{
				this._CommittedAmount = value;
			}
		}
		#endregion
		#region CommittedOpenQty
		public abstract class committedOpenQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _CommittedOpenQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Committed Open Quantity", Enabled = false)]
		public virtual Decimal? CommittedOpenQty
		{
			get
			{
				return this._CommittedOpenQty;
			}
			set
			{
				this._CommittedOpenQty = value;
			}
		}
		#endregion
		#region CommittedOpenAmount
		public abstract class committedOpenAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _CommittedOpenAmount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Committed Open Amount", Enabled = false)]
		public virtual Decimal? CommittedOpenAmount
		{
			get
			{
				return this._CommittedOpenAmount;
			}
			set
			{
				this._CommittedOpenAmount = value;
			}
		}
		#endregion
		#region CommittedReceivedQty
		public abstract class committedReceivedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _CommittedReceivedQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Committed Received Quantity", Enabled = false)]
		public virtual Decimal? CommittedReceivedQty
		{
			get
			{
				return this._CommittedReceivedQty;
			}
			set
			{
				this._CommittedReceivedQty = value;
			}
		}
		#endregion
		#region CommittedInvoicedQty
		public abstract class committedInvoicedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _CommittedInvoicedQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Committed Invoiced Quantity", Enabled = false)]
		public virtual Decimal? CommittedInvoicedQty
		{
			get
			{
				return this._CommittedInvoicedQty;
			}
			set
			{
				this._CommittedInvoicedQty = value;
			}
		}
		#endregion
		#region CommittedInvoicedAmount
		public abstract class committedInvoicedAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _CommittedInvoicedAmount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Committed Invoiced Amount", Enabled = false)]
		public virtual Decimal? CommittedInvoicedAmount
		{
			get
			{
				return this._CommittedInvoicedAmount;
			}
			set
			{
				this._CommittedInvoicedAmount = value;
			}
		}
		#endregion
		#region ActualPlusOpenCommittedAmount
		public abstract class actualPlusOpenCommittedAmount : PX.Data.IBqlField
		{
		}

		[PXBaseCury]
		[PXUIField(DisplayName = "Actual + Open Committed Amount", Enabled = false)]
		public virtual Decimal? ActualPlusOpenCommittedAmount
		{
			get
			{
				return this.ActualAmount + this.CommittedOpenAmount;
			}
		}
		#endregion
		#region VarianceAmount
		public abstract class varianceAmount : PX.Data.IBqlField
		{
		}

		[PXBaseCury]
		[PXUIField(DisplayName = "Variance Amount", Enabled = false)]
		public virtual Decimal? VarianceAmount
		{
			get
			{
				return this.RevisedAmount - this.ActualPlusOpenCommittedAmount;
			}
		}
		#endregion
		#region Performance
		public abstract class performance : PX.Data.IBqlField
		{
		}
		protected Decimal? _Performance;
		[PXDecimal(2)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Performance (%)", Enabled = false)]
		public virtual Decimal? Performance
		{
			get
			{
				if (_RevisedAmount != 0)
					return (ActualPlusOpenCommittedAmount / _RevisedAmount) * 100;
				else
					return 0;
			}
		}
		#endregion
		#region IsProduction
		public abstract class isProduction : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsProduction;
		[PXDefault(false)]
		[PXDBBool()]
		[PXUIField(DisplayName = "Auto Completed (%)")]
		public virtual Boolean? IsProduction
		{
			get
			{
				return this._IsProduction;
			}
			set
			{
				this._IsProduction = value;
			}
		}
		#endregion
		#region CompletedPct
		public abstract class completedPct : PX.Data.IBqlField
		{
		}
		protected decimal? _CompletedPct;
		[PXDBDecimal(2, MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Completed (%)")]
		public virtual decimal? CompletedPct
		{
			get
			{
				return this._CompletedPct;
			}
			set
			{
				this._CompletedPct = value;
			}
		}
		#endregion
		#region AmountToInvoice
		public abstract class amountToInvoice : PX.Data.IBqlField
		{
		}
		protected Decimal? _AmountToInvoice;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Pending Invoice Amount")]
		public virtual Decimal? AmountToInvoice
		{
			get
			{
				return this._AmountToInvoice;
			}
			set
			{
				this._AmountToInvoice = value;
			}
		}
		#endregion
		#region PrepaymentPct
		public abstract class prepaymentPct : PX.Data.IBqlField
		{
		}
		[PXDecimal(2, MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck =PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Prepaid (%)")]
		public virtual decimal? PrepaymentPct
		{
			get;
			set;
		}
		#endregion
		#region PrepaymentAmount
		public abstract class prepaymentAmount : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Prepaid Amount")]
		public virtual Decimal? PrepaymentAmount
		{
			get;
			set;
		}
		#endregion
		#region PrepaymentAvailable
		public abstract class prepaymentAvailable : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Prepaid Available", Enabled = false)]
		public virtual Decimal? PrepaymentAvailable
		{
			get;
			set;
		}
		#endregion
		#region PrepaymentInvoiced
		public abstract class prepaymentInvoiced : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Prepaid Invoiced", Enabled = false)]
		public virtual Decimal? PrepaymentInvoiced
		{
			get;
			set;
		}
		#endregion
		#region LimitQty
		public abstract class limitQty : PX.Data.IBqlField
		{
		}
		protected Boolean? _LimitQty;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Limit Quantity")]
		public virtual Boolean? LimitQty
		{
			get
			{
				return this._LimitQty;
			}
			set
			{
				this._LimitQty = value;
			}
		}
		#endregion
		#region LimitAmount
		public abstract class limitAmount : PX.Data.IBqlField
		{
		}
		protected Boolean? _LimitAmount;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Limit Amount")]
		public virtual Boolean? LimitAmount
		{
			get
			{
				return this._LimitAmount;
			}
			set
			{
				this._LimitAmount = value;
			}
		}
		#endregion
		#region MaxQty
		public abstract class maxQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _MaxQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Maximum Quantity")]
		public virtual Decimal? MaxQty
		{
			get
			{
				return this._MaxQty;
			}
			set
			{
				this._MaxQty = value;
			}
		}
		#endregion
		#region MaxAmount
		public abstract class maxAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _MaxAmount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Maximum Amount")]
		public virtual Decimal? MaxAmount
		{
			get
			{
				return this._MaxAmount;
			}
			set
			{
				this._MaxAmount = value;
			}
		}
		#endregion
		
		#region LastCostToComplete
		public abstract class lastCostToComplete : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Last Cost To Complete", Enabled = false)]
		public virtual Decimal? LastCostToComplete
		{
			get;
			set;
		}
		#endregion
		#region CostToComplete
		public abstract class costToComplete : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Cost To Complete")]
		public virtual Decimal? CostToComplete
		{
			get;
			set;
		}
		#endregion
		#region LastPercentCompleted
		public abstract class lastPercentCompleted : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Last % Completed", Enabled = false)]
		public virtual Decimal? LastPercentCompleted
		{
			get;
			set;
		}
		#endregion
		#region PercentCompleted
		public abstract class percentCompleted : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "% Completed")]
		public virtual Decimal? PercentCompleted
		{
			get;
			set;
		}
		#endregion
		#region LastCostAtCompletion
		public abstract class lastCostAtCompletion : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Last Cost At Completion", Enabled = false)]
		public virtual Decimal? LastCostAtCompletion
		{
			get;
			set;
		}
		#endregion
		#region CostAtCompletion
		public abstract class costAtCompletion : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Cost At Completion")]
		public virtual Decimal? CostAtCompletion
		{
			get;
			set;
		}
		#endregion
		#region LineCntr
		public abstract class lineCntr : PX.Data.IBqlField
		{
		}
		[PXDBInt()]
		[PXDefault(0)]
		public virtual int? LineCntr { get; set; }
		#endregion
		#region SortOrder
		public abstract class sortOrder : PX.Data.IBqlField
		{
		}
		protected int _SortOrder = 0;
		[PXInt()]
		public virtual int? SortOrder
		{
			get
			{
				return _SortOrder;
			}
			set
			{
				_SortOrder = value.GetValueOrDefault();
			}
		}
		#endregion

		#region System Columns
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Guid? _NoteID;
        [PXNote]
		public virtual Guid? NoteID
		{
			get
			{
				return this._NoteID;
			}
			set
			{
				this._NoteID = value;
			}
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		protected Byte[] _tstamp;
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get
			{
				return this._tstamp;
			}
			set
			{
				this._tstamp = value;
			}
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected String _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime
		{
			get
			{
				return this._CreatedDateTime;
			}
			set
			{
				this._CreatedDateTime = value;
			}
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected String _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
		#endregion
				
		public virtual BudgetKeyTuple GetBudgetKey()
		{
			return new BudgetKeyTuple(ProjectID.GetValueOrDefault(),
				ProjectTaskID.GetValueOrDefault(),
				AccountGroupID.GetValueOrDefault(),
				InventoryID.GetValueOrDefault(PMInventorySelectorAttribute.EmptyInventoryID),
				CostCodeID.GetValueOrDefault(CostCodeAttribute.GetDefaultCostCode()));
		}
	}

	[PMBudgetAccum]
    [Serializable]
    [PXHidden]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMBudgetAccum : PMBudget
	{
		#region ProjectID
		public new abstract class projectID : PX.Data.IBqlField
		{
		}
		[PXDefault]
		[PXDBInt(IsKey=true)]
		public override Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ProjectTaskID
		public new abstract class projectTaskID : PX.Data.IBqlField
		{
		}
		[PXDefault]
		[PXDBInt(IsKey = true)]
		public override Int32? ProjectTaskID
		{
			get;set;
		}
		#endregion
		#region AccountGroupID
		public new abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		[PXDefault]
		[PXDBInt(IsKey = true)]
		public override Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region InventoryID
		public new abstract class inventoryID : PX.Data.IBqlField
		{
		}
		[PXDefault]
		[PXDBInt(IsKey = true)]
		public override Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region CostCodeID
		public new abstract class costCodeID : PX.Data.IBqlField
		{
		}
		
		[PXDefault]
		[PXDBInt(IsKey = true)]
		public override Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion

		#region UOM
		public new abstract class uOM : PX.Data.IBqlField
		{
		}
		[PMUnit(typeof(inventoryID))]
		public override String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		#region ActualQty
		public new abstract class actualQty : PX.Data.IBqlField
		{
		}
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public override Decimal? ActualQty
		{
			get
			{
				return this._ActualQty;
			}
			set
			{
				this._ActualQty = value;
			}
		}
		#endregion
		#region InvoicedAmount
		public new abstract class invoicedAmount : PX.Data.IBqlField
		{
		}

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public override Decimal? InvoicedAmount
		{
			get
			{
				return this._InvoicedAmount;
			}
			set
			{
				this._InvoicedAmount = value;
			}
		}
		#endregion
		#region ActualAmount
		public new abstract class actualAmount : PX.Data.IBqlField
		{
		}
		
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public override Decimal? ActualAmount
		{
			get
			{
				return this._ActualAmount;
			}
			set
			{
				this._ActualAmount = value;
			}
		}
		#endregion
		#region CommittedQty
		public new abstract class committedQty : PX.Data.IBqlField
		{
		}
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public override Decimal? CommittedQty
		{
			get
			{
				return this._CommittedQty;
			}
			set
			{
				this._CommittedQty = value;
			}
		}
		#endregion
		#region CommittedAmount
		public new abstract class committedAmount : PX.Data.IBqlField
		{
		}

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public override Decimal? CommittedAmount
		{
			get
			{
				return this._CommittedAmount;
			}
			set
			{
				this._CommittedAmount = value;
			}
		}
		#endregion
		#region CommittedOpenQty
		public new abstract class committedOpenQty : PX.Data.IBqlField
		{
		}
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public override Decimal? CommittedOpenQty
		{
			get
			{
				return this._CommittedOpenQty;
			}
			set
			{
				this._CommittedOpenQty = value;
			}
		}
		#endregion
		#region CommittedOpenAmount
		public new abstract class committedOpenAmount : PX.Data.IBqlField
		{
		}

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public override Decimal? CommittedOpenAmount
		{
			get
			{
				return this._CommittedOpenAmount;
			}
			set
			{
				this._CommittedOpenAmount = value;
			}
		}
		#endregion

		public new abstract class committedReceivedQty : PX.Data.IBqlField
		{
		}
				
		public new abstract class committedInvoicedQty : PX.Data.IBqlField
		{
		}

		public new abstract class committedInvoicedAmount : PX.Data.IBqlField
		{
		}
	}

	[PXBreakInheritance]
	[Serializable]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMRevenueBudget : PMBudget
	{
		#region ProjectID
		public new abstract class projectID : PX.Data.IBqlField
		{
		}
		[PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<projectID>>, And<PMRevenueBudget.type, Equal<GL.AccountType.income>>>>))]
		[PXDBLiteDefault(typeof(PMProject.contractID))]
		[PXDBInt(IsKey = true)]
		public override Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ProjectTaskID
		public new abstract class projectTaskID : PX.Data.IBqlField
		{
		}
		[PXParent(typeof(Select<PMTask, Where<PMTask.taskID, Equal<Current<projectTaskID>>, And<PMRevenueBudget.type, Equal<GL.AccountType.income>>>>))]
		[ProjectTask(typeof(PMProject.contractID), IsKey = true, AlwaysEnabled = true, DirtyRead = true)]
		public override Int32? ProjectTaskID
		{
			get;
			set;
		}
		#endregion
		#region CostCodeID
		public new abstract class costCodeID : PX.Data.IBqlField
		{
		}
		[CostCode(IsKey = true)]
		public override Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion
		#region AccountGroupID
		public new abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		[PXDefault]
		[AccountGroup(typeof(Where<PMAccountGroup.type, Equal<GL.AccountType.income>>), IsKey = true)]
		public override Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region InventoryID
		public new abstract class inventoryID : PX.Data.IBqlField
		{
		}
		
		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Inventory ID")]
		[PXDefault]
		[PMInventorySelector]
		public override Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region Type
		public new abstract class type : PX.Data.IBqlField
		{
		}
		[PXDBString(1)]
		[PXDefault(GL.AccountType.Income)]
		[PMAccountType.List()]
		[PXUIField(DisplayName = "Budget Type", Visible = false, Enabled = false)]
		public override string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}
		#endregion
		public new abstract class revenueTaskID : PX.Data.IBqlField { }
		public new abstract class revenueInventoryID : PX.Data.IBqlField { }
		public new abstract class taxCategoryID : PX.Data.IBqlField	{}
		public new abstract class description : PX.Data.IBqlField {}
		public new abstract class qty : PX.Data.IBqlField { }
		#region UOM
		public new abstract class uOM : PX.Data.IBqlField
		{
		}
		[PXDefault(typeof(Search<InventoryItem.salesUnit, Where<InventoryItem.inventoryID, Equal<Current<PMRevenueBudget.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PMUnit(typeof(inventoryID))]
		public override String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		public new abstract class rate : PX.Data.IBqlField { }
		public new abstract class amount : PX.Data.IBqlField { }
		public new abstract class revisedQty : PX.Data.IBqlField { }
		public new abstract class revisedAmount : PX.Data.IBqlField { }
		public new abstract class changeOrderQty : PX.Data.IBqlField { }
		public new abstract class changeOrderAmount : PX.Data.IBqlField { }
		public new abstract class actualQty : PX.Data.IBqlField { }
		public new abstract class actualAmount : PX.Data.IBqlField { }
		public new abstract class committedQty : PX.Data.IBqlField { }
		public new abstract class committedAmount : PX.Data.IBqlField { }
		public new abstract class committedOpenQty : PX.Data.IBqlField { }
		public new abstract class committedOpenAmount : PX.Data.IBqlField { }
		public new abstract class committedReceivedQty : PX.Data.IBqlField { }
		public new abstract class committedInvoicedQty : PX.Data.IBqlField { }
		public new abstract class committedInvoicedAmount : PX.Data.IBqlField { }
		public new abstract class actualPlusOpenCommittedAmount : PX.Data.IBqlField { }
		public new abstract class varianceAmount : PX.Data.IBqlField { }
		public new abstract class performance : PX.Data.IBqlField { }
		public new abstract class completedPct : PX.Data.IBqlField { }
		public new abstract class amountToInvoice : PX.Data.IBqlField {}

		#region LastCostToComplete
		public new abstract class lastCostToComplete : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Last Cost To Complete", Enabled = false, Visible = false, Visibility = PXUIVisibility.Invisible)]
		public override Decimal? LastCostToComplete
		{
			get;
			set;
		}
		#endregion
		#region CostToComplete
		public new abstract class costToComplete : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Cost To Complete", Enabled = false, Visible = false, Visibility = PXUIVisibility.Invisible)]
		public override Decimal? CostToComplete
		{
			get;
			set;
		}
		#endregion
		#region LastPercentCompleted
		public new abstract class lastPercentCompleted : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Last % Completed", Enabled = false, Visible = false, Visibility = PXUIVisibility.Invisible)]
		public override Decimal? LastPercentCompleted
		{
			get;
			set;
		}
		#endregion
		#region PercentCompleted
		public new abstract class percentCompleted : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "% Completed", Enabled = false, Visible = false, Visibility = PXUIVisibility.Invisible)]
		public override Decimal? PercentCompleted
		{
			get;
			set;
		}
		#endregion
		#region LastCostAtCompletion
		public new abstract class lastCostAtCompletion : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Last Cost At Completion", Enabled = false, Visible=false, Visibility = PXUIVisibility.Invisible)]
		public override Decimal? LastCostAtCompletion
		{
			get;
			set;
		}
		#endregion
		#region CostAtCompletion
		public new abstract class costAtCompletion : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Cost At Completion", Enabled = false, Visible = false, Visibility = PXUIVisibility.Invisible)]
		public override Decimal? CostAtCompletion
		{
			get;
			set;
		}
		#endregion

		public new abstract class lineCntr : PX.Data.IBqlField
		{
		}
		
		#region IsProduction
		public new abstract class isProduction : PX.Data.IBqlField
		{
		}
		[PXDefault(false)]
		[PXDBBool()]
		public override Boolean? IsProduction
		{
			get
			{
				return this._IsProduction;
			}
			set
			{
				this._IsProduction = value;
			}
		}
		#endregion
	}

	[PXBreakInheritance]
	[Serializable]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMCostBudget : PMBudget
	{
		#region ProjectID
		public new abstract class projectID : PX.Data.IBqlField
		{
		}
		[PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<projectID>>, And<PMCostBudget.type, Equal<GL.AccountType.expense>>>>))]
		[PXDBLiteDefault(typeof(PMProject.contractID))]
		[PXDBInt(IsKey = true)]
		public override Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ProjectTaskID
		public new abstract class projectTaskID : PX.Data.IBqlField
		{
		}
		[PXParent(typeof(Select<PMTask, Where<PMTask.taskID, Equal<Current<projectTaskID>>, And<PMCostBudget.type, Equal<GL.AccountType.expense>>>>))]
		[ProjectTask(typeof(PMProject.contractID), IsKey = true, AlwaysEnabled = true, DirtyRead = true)]
		public override Int32? ProjectTaskID
		{
			get;
			set;
		}
		#endregion
		#region CostCodeID
		public new abstract class costCodeID : PX.Data.IBqlField
		{
		}
		[CostCode(IsKey = true)]
		public override Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion
		#region AccountGroupID
		public new abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		[PXDefault]
		[AccountGroup(typeof(Where<PMAccountGroup.isExpense, Equal<True>>), IsKey = true)]
		public override Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region InventoryID
		public new abstract class inventoryID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Inventory ID")]
		[PXDefault]
		[PMInventorySelector]
		public override Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion

		#region Type
		public new abstract class type : PX.Data.IBqlField
		{
		}
		[PXDBString(1)]
		[PXDefault(GL.AccountType.Expense)]
		[PMAccountType.List()]
		[PXUIField(DisplayName = "Budget Type", Visible = false, Enabled = false)]
		public override string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}
		#endregion
		#region RevenueTaskID
		public new abstract class revenueTaskID : PX.Data.IBqlField
		{
		}
		[PXUIField(DisplayName = "Revenue Task")]
		[PXDBInt]
		//[PXSelector(typeof(Search5<PMTask.taskID, 
		//	InnerJoin<PMRevenueBudget, On<PMTask.projectID, Equal<PMRevenueBudget.projectID>, And<PMTask.taskID, Equal<PMRevenueBudget.projectTaskID>>>>,
		//	Where<PMRevenueBudget.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMRevenueBudget.type, Equal<GL.AccountType.income>>>,
		//	Aggregate<GroupBy<PMTask.taskID>>>), 
		//	typeof(PMTask.description),	SubstituteKey =typeof(PMTask.taskCD))]
		[PMRevenueBudgetLineTaskSelector]
		public override Int32? RevenueTaskID
		{
			get
			{
				return this._RevenueTaskID;
			}
			set
			{
				this._RevenueTaskID = value;
			}
		}
		#endregion
		#region RevenueInventoryID
		public new abstract class revenueInventoryID : PX.Data.IBqlField
		{
		}
		[PXUIField(DisplayName ="Revenue Item")]
		[PXDBInt]
		//[PMInventorySelector(typeof(Search2<InventoryItem.inventoryID,
		//	InnerJoin<PMRevenueBudget, On<PMRevenueBudget.inventoryID, Equal<InventoryItem.inventoryID>>>,
		//	Where<PMRevenueBudget.projectID, Equal<Current<PMCostBudget.projectID>>, 
		//	And<PMRevenueBudget.projectTaskID, Equal<Current<PMCostBudget.revenueTaskID>>>>>), typeof(PMRevenueBudget.description), SubstituteKey = typeof(InventoryItem.inventoryCD))]
		[PMRevenueBudgetLineSelector]
		public override Int32? RevenueInventoryID
		{
			get
			{
				return this._RevenueInventoryID;
			}
			set
			{
				this._RevenueInventoryID = value;
			}
		}
		#endregion
		public new abstract class taxCategoryID : PX.Data.IBqlField { }
		public new abstract class description : PX.Data.IBqlField { }
		public new abstract class qty : PX.Data.IBqlField { }
		#region UOM
		public new abstract class uOM : PX.Data.IBqlField
		{
		}
		[PXDefault(typeof(Search<InventoryItem.purchaseUnit, Where<InventoryItem.inventoryID, Equal<Current<PMCostBudget.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PMUnit(typeof(inventoryID))]
		public override String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		public new abstract class rate : PX.Data.IBqlField { }
		public new abstract class amount : PX.Data.IBqlField { }
		public new abstract class revisedQty : PX.Data.IBqlField { }
		public new abstract class revisedAmount : PX.Data.IBqlField { }
		public new abstract class actualQty : PX.Data.IBqlField { }
		public new abstract class actualAmount : PX.Data.IBqlField { }
		public new abstract class changeOrderQty : PX.Data.IBqlField { }
		public new abstract class changeOrderAmount : PX.Data.IBqlField { }
		public new abstract class committedQty : PX.Data.IBqlField { }
		public new abstract class committedAmount : PX.Data.IBqlField { }
		public new abstract class committedOpenQty : PX.Data.IBqlField { }
		public new abstract class committedOpenAmount : PX.Data.IBqlField { }
		public new abstract class committedReceivedQty : PX.Data.IBqlField { }
		public new abstract class committedInvoicedQty : PX.Data.IBqlField { }
		public new abstract class committedInvoicedAmount : PX.Data.IBqlField { }
		public new abstract class actualPlusOpenCommittedAmount : PX.Data.IBqlField { }
		public new abstract class varianceAmount : PX.Data.IBqlField { }
		public new abstract class performance : PX.Data.IBqlField { }
		public new abstract class isProduction : PX.Data.IBqlField { }

		public new abstract class lineCntr : PX.Data.IBqlField
		{
		}

	}


	[PXBreakInheritance]
	[Serializable]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMOtherBudget : PMBudget
	{
		#region ProjectID
		public new abstract class projectID : PX.Data.IBqlField
		{
		}
		[PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<projectID>>, And<PMOtherBudget.type, NotEqual<GL.AccountType.income>, And<PMOtherBudget.type, NotEqual<GL.AccountType.expense>>>>>))]
		[PXDBLiteDefault(typeof(PMProject.contractID))]
		[PXDBInt(IsKey = true)]
		public override Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ProjectTaskID
		public new abstract class projectTaskID : PX.Data.IBqlField
		{
		}
		[PXParent(typeof(Select<PMTask, Where<PMTask.taskID, Equal<Current<projectTaskID>>, And<PMOtherBudget.type, NotEqual<GL.AccountType.income>, And<PMOtherBudget.type, NotEqual<GL.AccountType.expense>>>>>))]
		[ProjectTask(typeof(PMProject.contractID), IsKey = true, AlwaysEnabled = true, DirtyRead = true)]
		public override Int32? ProjectTaskID
		{
			get;
			set;
		}
		#endregion
		#region CostCodeID
		public new abstract class costCodeID : PX.Data.IBqlField
		{
		}
		[CostCode(IsKey = true)]
		public override Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion
		#region AccountGroupID
		public new abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		[PXDefault]
		[AccountGroup(typeof(Where<PMAccountGroup.isExpense, Equal<False>, And<PMAccountGroup.type, NotEqual<GL.AccountType.income>>>), IsKey = true)]
		public override Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region InventoryID
		public new abstract class inventoryID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Inventory ID")]
		[PXDefault]
		[PMInventorySelector]
		public override Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion

		#region Type
		public new abstract class type : PX.Data.IBqlField
		{
		}
		[PXDBString(1)]
		[PXDefault(GL.AccountType.Asset)]
		public override string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}
		#endregion
				
		public new abstract class taxCategoryID : PX.Data.IBqlField { }
		public new abstract class description : PX.Data.IBqlField { }
		public new abstract class qty : PX.Data.IBqlField { }
		#region UOM
		public new abstract class uOM : PX.Data.IBqlField
		{
		}
		[PXDefault(typeof(Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<PMOtherBudget.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PMUnit(typeof(inventoryID))]
		public override String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		public new abstract class rate : PX.Data.IBqlField { }
		public new abstract class amount : PX.Data.IBqlField { }
		public new abstract class revisedQty : PX.Data.IBqlField { }
		public new abstract class revisedAmount : PX.Data.IBqlField { }
		public new abstract class actualQty : PX.Data.IBqlField { }
		public new abstract class actualAmount : PX.Data.IBqlField { }
		public new abstract class changeOrderQty : PX.Data.IBqlField { }
		public new abstract class changeOrderAmount : PX.Data.IBqlField { }
		public new abstract class committedQty : PX.Data.IBqlField { }
		public new abstract class committedAmount : PX.Data.IBqlField { }
		public new abstract class committedOpenQty : PX.Data.IBqlField { }
		public new abstract class committedOpenAmount : PX.Data.IBqlField { }
		public new abstract class committedReceivedQty : PX.Data.IBqlField { }
		public new abstract class committedInvoicedQty : PX.Data.IBqlField { }
		public new abstract class committedInvoicedAmount : PX.Data.IBqlField { }
		public new abstract class actualPlusOpenCommittedAmount : PX.Data.IBqlField { }
		public new abstract class varianceAmount : PX.Data.IBqlField { }
		public new abstract class performance : PX.Data.IBqlField { }
		public new abstract class isProduction : PX.Data.IBqlField { }

		public new abstract class lineCntr : PX.Data.IBqlField
		{
		}

	}

	[Serializable]
	public class PMRevenueBudgetLineTaskSelectorAttribute : PXCustomSelectorAttribute
	{
		
		public PMRevenueBudgetLineTaskSelectorAttribute() : base(typeof(PMTask.taskID), typeof(PMTask.taskCD), typeof(PMTask.description))
		{
			SubstituteKey = typeof(PMTask.taskCD);
			DescriptionField = typeof(PMTask.description);

		}

		protected virtual IEnumerable GetRecords()
		{
			var selectRevenueBudget = new PXSelect<PMRevenueBudget, 
				Where<PMRevenueBudget.projectID, Equal<Current<PMProject.contractID>>,
				And<PMRevenueBudget.type, Equal<GL.AccountType.income>>>>(_Graph);

			HashSet<int> budgetedTasks = new HashSet<int>();
			foreach(PMRevenueBudget budget in selectRevenueBudget.Select())
			{
				budgetedTasks.Add(budget.TaskID.Value);
			}


			var select = new PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>>>(this._Graph);

			foreach (PMTask task in select.Select())
			{
				if (budgetedTasks.Contains(task.TaskID.Value))
				{
					yield return task;
				}				
			}
		}
	}

	[Serializable]
	public class PMRevenueBudgetLineSelectorAttribute : PXCustomSelectorAttribute
	{
		public PMRevenueBudgetLineSelectorAttribute() : base(typeof(InventoryItem.inventoryID), typeof(InventoryItem.inventoryCD), typeof(InventoryItem.descr))
		{
			SubstituteKey = typeof(InventoryItem.inventoryCD);
			DescriptionField = typeof(InventoryItem.descr);
		}

		protected virtual IEnumerable GetRecords()
		{
			var select = new PXSelectJoin<PMRevenueBudget,
				InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<PMRevenueBudget.inventoryID>>>,
				Where<PMRevenueBudget.projectID, Equal<Current<PMCostBudget.projectID>>,
				And<PMRevenueBudget.projectTaskID, Equal<Current<PMCostBudget.revenueTaskID>>>>>(this._Graph);

			foreach (PXResult<PMRevenueBudget, InventoryItem> res in select.Select())
			{
				InventoryItem item = (InventoryItem)res;
				PMRevenueBudget budget = (PMRevenueBudget)res;

				item.Descr = budget.Description;

				yield return item;
			}
		}
	}
}
