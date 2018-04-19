using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Data.EP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.GL;

namespace PX.Objects.PM
{
	/// <summary>
	/// Account Group that is used in the Project Module. 
	/// Group of GL accounts of the same type. Every GL Account can be included into one and only one group of the same type as the account (Asset, Liability, Income and Expense).  
	/// It is also possible to create non accounting GL Group, that can be used for collection and trace of any statistical information on the project that is not related to finance. 
	/// Account groups are used to classify project budget and balance information. Essentially in the scope of project they play the same role as the GL account in terms of GL Ledger. 
	/// Account groups are mapped to GL accounts to provide transfer and mapping of the project related financial information from the GL module to Project Management Module. 
	/// Account groups are not mapped to GL accounts one to one to reduce the number of the account groups compare to the number of the natural GL accounts. 
	/// The other reason is the ability to create non - financial account groups to enter and analyze the information that is not directly linked to finance.
	/// </summary>
	[PXPrimaryGraph(typeof(AccountGroupMaint))]
    [Serializable]
    [PXCacheName(Messages.PMAccountGroupName)]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public partial class PMAccountGroup : IBqlTable
	{
		#region GroupID
		public abstract class groupID : PX.Data.IBqlField
		{
		}
		protected Int32? _GroupID;

		/// <summary>
		/// Gets or Sets the AccountGroup identifier.
		/// </summary>
		[PXDBIdentity()]
		[PXSelector(typeof(PMAccountGroup.groupID))]
		public virtual Int32? GroupID
		{
			get
			{
				return this._GroupID;
			}
			set
			{
				this._GroupID = value;
			}
		}
		#endregion
		#region GroupCD
		public abstract class groupCD : PX.Data.IBqlField
		{
		}
		protected String _GroupCD;
		/// <summary>
		/// Gets or Sets the AccountGroup identifier.
		/// This is a segmented key and format is configured under segmented key maintenance screen in CS module.
		/// </summary>
		[PXDimensionSelector(AccountGroupAttribute.DimensionName,
			typeof(Search<PMAccountGroup.groupCD>),
			typeof(PMAccountGroup.groupCD),
			typeof(PMAccountGroup.groupCD), typeof(PMAccountGroup.description), typeof(PMAccountGroup.type), typeof(PMAccountGroup.isActive), DescriptionField = typeof(PMTask.description))]
		[PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDefault()]
		[PXUIField(DisplayName = "Account Group ID", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual String GroupCD
		{
			get
			{
				return this._GroupCD;
			}
			set
			{
				this._GroupCD = value;
			}
		}
		#endregion
		
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected String _Description;
		/// <summary>
		/// Gets or Sets the AccountGroup description.
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
		#region IsActive
		public abstract class isActive : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsActive;
		/// <summary>
		/// Gets or sets whether Account group is active or not.
		/// </summary>
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Active")]
		public virtual Boolean? IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				this._IsActive = value;
			}
		}
		#endregion
		#region IsExpense
		public abstract class isExpense : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsExpense;
		
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Expense")]
		public virtual Boolean? IsExpense
		{
			get
			{
				return this._IsExpense;
			}
			set
			{
				this._IsExpense = value;
			}
		}
		#endregion
		#region Type
		public abstract class type : PX.Data.IBqlField
		{
		}
		protected string _Type;
		/// <summary>
		/// Type of the account group: Asset, Liability, Expense, Income and Off-Balance. The type of asset is essentially the same as the type of GL account.
		/// For the account group of balance sheet type (Assets and Liabilities) only the balance sheet GL accounts can be selected.
		/// For the account group of P&L type (Income & Expenses) only P&L GL accounts can be selected.
		/// For the Off-Balance - GL account is not applicable.
		/// </summary>
		[PXDBString(1)]
		[PXDefault(GL.AccountType.Expense)]
		[PMAccountType.List()]
		[PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible)]
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
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[PXDBInt]
		public virtual Int32? AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				this._AccountID = value;
			}
		}
		#endregion
		#region SortOrder
		public abstract class sortOrder : PX.Data.IBqlField
		{
		}
		protected Int16? _SortOrder;
		/// <summary>
		/// Gets or sets sort order. Sort order is used in displaying the Balances for the Project.
		/// </summary>
		[PXDBShort()]
		[PXUIField(DisplayName = "Sort Order")]
		public virtual Int16? SortOrder
		{
			get
			{
				return this._SortOrder;
			}
			set
			{
				this._SortOrder = value;
			}
		}
		#endregion
		#region Attributes
		/// <summary>
		/// Gets or sets entity attributes.
		/// </summary>
		[CRAttributesField(typeof(PMAccountGroup.classID))]
		public virtual string[] Attributes { get; set; }

		public abstract class classID : IBqlField
		{
		}
		/// <summary>
		/// Gets ClassID for the attributes. Always returns <see cref="GroupTypes.AccountGroup"/>
		/// </summary>
		[PXString(20)]
		public virtual string ClassID
		{
			get { return GroupTypes.AccountGroup; }
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

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public static class PMAccountType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new[]
				{
					Pair(GL.AccountType.Asset, GL.Messages.Asset),
					Pair(GL.AccountType.Liability, GL.Messages.Liability),
					Pair(GL.AccountType.Income, GL.Messages.Income),
					Pair(GL.AccountType.Expense, GL.Messages.Expense),
					Pair(OffBalance, Messages.OffBalance),
				}) {}
		}

		public const string OffBalance = "O"; 
		public class offBalance : Constant<string>
		{
			public offBalance() : base(OffBalance) { ;}
		}
				
	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public static class AccountGroupStatus
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(new[] {Pair(Active, Messages.Active)}) {}
		}
		public const string Active = "A";
	}
}
