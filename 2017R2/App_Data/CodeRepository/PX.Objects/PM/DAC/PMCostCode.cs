using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.EP;

namespace PX.Objects.PM
{
	[PXCacheName(Messages.CostCode)]
	[PXPrimaryGraph(typeof(CostCodeMaint))]
	[Serializable]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMCostCode : PX.Data.IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		protected bool? _Selected = false;

		/// <summary>
		/// Gets or sets whether the task is selected in the grid.
		/// </summary>
		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected
		{
			get
			{
				return _Selected;
			}
			set
			{
				_Selected = value;
			}
		}
		#endregion
				
		#region CostCodeID
		public abstract class costCodeID : PX.Data.IBqlField
		{
		}
		protected Int32? _CostCodeID;
		/// <summary>
		/// Gets or sets unique identifier.
		/// </summary>
		[PXDBIdentity()]
		[PXSelector(typeof(PMCostCode.costCodeID))]
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
		#region CostCodeCD
		public abstract class costCodeCD : PX.Data.IBqlField
		{
			public const string DimensionName = "COSTCODE";
		}
		protected String _CostCodeCD;
		/// <summary>
		/// Get or sets unique identifier.
		/// This is a segmented key and format is configured under segmented key maintenance screen in CS module.
		/// </summary>
		/// 
		[PXDimensionSelector(costCodeCD.DimensionName, typeof(Search<PMCostCode.costCodeID>), typeof(PMCostCode.costCodeCD))]
		//[PXDimension(costCodeCD.DimensionName)]
		[PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDefault()]
		[PXUIField(DisplayName = "Cost Code", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual String CostCodeCD
		{
			get
			{
				return this._CostCodeCD;
			}
			set
			{
				this._CostCodeCD = value;
			}
		}
		#endregion


		#region IsDefault
		public abstract class isDefault : IBqlField
		{
		}
		protected bool? _IsDefault = false;

		/// <summary>
		/// Gets or sets whether the task is selected in the grid.
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? IsDefault
		{
			get
			{
				return _IsDefault;
			}
			set
			{
				_IsDefault = value;
			}
		}
		#endregion
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected String _Description;
		/// <summary>
		/// Gets or sets description
		/// </summary>
		[PXDBString(250, IsUnicode = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
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

		#region System Columns
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Guid? _NoteID;
		[PXNote(DescriptionField = typeof(PMCostCode.costCodeCD))]
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
