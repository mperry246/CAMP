using System;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.TX;
using System.Collections;
using PX.Objects.CS;

namespace PX.Objects.PM
{

	[Serializable]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMBudgetProduction : PX.Data.IBqlTable, IProjectFilter
	{
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
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
		[PXDBInt(IsKey = true)]
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
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt(IsKey = true)]
		[PXLineNbr(typeof(PMCostBudget.lineCntr))]
		[PXParent(typeof(Select<PMCostBudget, Where<PMCostBudget.projectID, Equal<Current<projectID>>,
			And<PMCostBudget.projectTaskID, Equal<Current<projectTaskID>>,
			And<PMCostBudget.accountGroupID, Equal<Current<accountGroupID>>,
			And<PMCostBudget.costCodeID, Equal<Current<costCodeID>>,
			And<PMCostBudget.inventoryID, Equal<Current<inventoryID>>>>>>>>))]
		[PXUIField(DisplayName = "Line Nbr.", Visible = false)]
		public virtual Int32? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
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
		#region PercentCompleted
		public abstract class percentCompleted : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "% Complete")]
		public virtual Decimal? PercentCompleted
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
	}

}
