using System;
using PX.Data;

namespace PX.Objects.GL
{
	[PXCacheName(Messages.BranchAcctMap)]
	[Serializable]
	[PXHidden]
	public partial class BranchAcctMap : IBqlTable
	{
		#region BranchID
		public abstract class branchID : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDBLiteDefault(typeof(Branch.branchID))]
		public virtual int? BranchID
		{
			get;
			set;
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXLineNbr(typeof(Branch.acctMapNbr))]
		public virtual int? LineNbr
		{
			get;
			set;
		}
		#endregion
		#region FromBranchID
		public abstract class fromBranchID : IBqlField
		{
		}
		[PXDBInt]
		public virtual int? FromBranchID
		{
			get;
			set;
		}
		#endregion
		#region ToBranchID
		public abstract class toBranchID : IBqlField
		{
		}
		[PXDBInt]
		public virtual int? ToBranchID
		{
			get;
			set;
		}
		#endregion
		#region FromAccountCD
		public abstract class fromAccountCD : IBqlField
		{
		}
		[PXDefault]
		[AccountRaw]
		public virtual string FromAccountCD
		{
			get;
			set;
		}
		#endregion
		#region ToAccountCD
		public abstract class toAccountCD : IBqlField
		{
		}
		[PXDefault]
		[AccountRaw]
		public virtual string ToAccountCD
		{
			get;
			set;
		}
		#endregion
		#region MapAccountID
		public abstract class mapAccountID : IBqlField
		{
		}
		[Account(DescriptionField = typeof(Account.description))]
		[PXDefault]
		public virtual int? MapAccountID
		{
			get;
			set;
		}
		#endregion
		#region MapSubID
		public abstract class mapSubID : IBqlField
		{
		}
		[SubAccount(typeof(BranchAcctMap.mapAccountID), DescriptionField = typeof(Account.description))]
		[PXDefault]
		public virtual int? MapSubID
		{
			get;
			set;
		}
		#endregion
	}

	[PXProjection(typeof(Select<BranchAcctMap, Where<BranchAcctMap.branchID, Equal<BranchAcctMap.fromBranchID>>>), Persistent = true)]
	[PXCacheName(Messages.BranchAcctMapFrom)]
	[Serializable]
	public partial class BranchAcctMapFrom : IBqlTable
	{
		#region BranchID
		public abstract class branchID : IBqlField
		{
		}
		[PXDBInt(IsKey = true, BqlTable = typeof(BranchAcctMap))]
		[PXDBLiteDefault(typeof(Branch.branchID))]
		public virtual int? BranchID
		{
			get;
			set;
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true, BqlTable = typeof(BranchAcctMap))]
		[PXLineNbr(typeof(Branch.acctMapNbr))]
		public virtual int? LineNbr
		{
			get;
			set;
		}
		#endregion
		#region FromBranchID
		public abstract class fromBranchID : IBqlField
		{
		}
		[PXDBInt(BqlTable = typeof(BranchAcctMap))]
		[PXDBLiteDefault(typeof(Branch.branchID))]
		public virtual int? FromBranchID
		{
			get;
			set;
		}
		#endregion
		#region ToBranchID
		public abstract class toBranchID : IBqlField
		{
		}
		[PXDBInt(BqlTable = typeof(BranchAcctMap))]
		[PXSelector(typeof(Search<Branch.branchID, Where<Branch.branchID, NotEqual<Current<BranchAcctMapFrom.branchID>>>>), SubstituteKey = typeof(Branch.branchCD))]
		[PXUIField(DisplayName = "Destination Branch")]
		[PXRestrictor(typeof(Where<Branch.active, Equal<True>>), GL.Messages.BranchInactive)]
		public virtual int? ToBranchID
		{
			get;
			set;
		}
		#endregion
		#region FromAccountCD
		public abstract class fromAccountCD : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true, InputMask = "", BqlTable = typeof(BranchAcctMap))]
		[PXDefault]
		[PXDimensionSelector("ACCOUNT", typeof(Account.accountCD), DescriptionField = typeof(Account.description))]
		[PXUIField(DisplayName = "Account From")]
		public virtual string FromAccountCD
		{
			get;
			set;
		}
		#endregion
		#region ToAccountCD
		public abstract class toAccountCD : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true, InputMask = "", BqlTable = typeof(BranchAcctMap))]
		[PXDefault]
		[PXDimensionSelector("ACCOUNT", typeof(Account.accountCD), DescriptionField = typeof(Account.description))]
		[PXUIField(DisplayName = "Account To")]
		public virtual string ToAccountCD
		{
			get;
			set;
		}
		#endregion
		#region MapAccountID
		public abstract class mapAccountID : IBqlField
		{
		}
		[Account(null, typeof(Search<Account.accountID,
			Where<Account.isCashAccount, Equal<False>, And<Account.curyID, IsNull>>>),
			DescriptionField = typeof(Account.description), BqlTable = typeof(BranchAcctMap), DisplayName = "Offset Account")]
		[PXDefault]
		public virtual int? MapAccountID
		{
			get;
			set;
		}
		#endregion
		#region MapSubID
		public abstract class mapSubID : IBqlField
		{
		}
		[SubAccount(typeof(BranchAcctMapFrom.mapAccountID), DisplayName = "Offset Subaccount", DescriptionField = typeof(Account.description), BqlTable = typeof(BranchAcctMap))]
		[PXDefault]
		public virtual int? MapSubID
		{
			get;
			set;
		}
		#endregion
	}

	[PXProjection(typeof(Select<BranchAcctMap, Where<BranchAcctMap.branchID, Equal<BranchAcctMap.toBranchID>>>), Persistent = true)]
	[PXCacheName(Messages.BranchAcctMapTo)]
	[Serializable]
	public partial class BranchAcctMapTo : IBqlTable
	{
		#region BranchID
		public abstract class branchID : IBqlField
		{
		}
		[PXDBInt(IsKey = true, BqlTable = typeof(BranchAcctMap))]
		[PXDBLiteDefault(typeof(Branch.branchID))]
		public virtual int? BranchID
		{
			get;
			set;
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true, BqlTable = typeof(BranchAcctMap))]
		[PXLineNbr(typeof(Branch.acctMapNbr))]
		public virtual int? LineNbr
		{
			get;
			set;
		}
		#endregion
		#region FromBranchID
		public abstract class fromBranchID : IBqlField
		{
		}
		[PXDBInt(BqlTable = typeof(BranchAcctMap))]
		[PXSelector(typeof(Search<Branch.branchID, Where<Branch.branchID, NotEqual<Current<BranchAcctMapFrom.branchID>>>>), SubstituteKey = typeof(Branch.branchCD))]
		[PXUIField(DisplayName = "Destination Branch")]
		[PXRestrictor(typeof(Where<Branch.active, Equal<True>>), GL.Messages.BranchInactive)]
		public virtual int? FromBranchID
		{
			get;
			set;
		}
		#endregion
		#region ToBranchID
		public abstract class toBranchID : IBqlField
		{
		}
		[PXDBInt(BqlTable = typeof(BranchAcctMap))]
		[PXDBLiteDefault(typeof(Branch.branchID))]
		public virtual int? ToBranchID
		{
			get;
			set;
		}
		#endregion
		#region FromAccountCD
		public abstract class fromAccountCD : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true, InputMask = "", BqlTable = typeof(BranchAcctMap))]
		[PXDefault]
		[PXDimensionSelector("ACCOUNT", typeof(Account.accountCD), DescriptionField = typeof(Account.description))]
		[PXUIField(DisplayName = "Account From")]
		public virtual string FromAccountCD
		{
			get;
			set;
		}
		#endregion
		#region ToAccountCD
		public abstract class toAccountCD : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true, InputMask = "", BqlTable = typeof(BranchAcctMap))]
		[PXDefault]
		[PXDimensionSelector("ACCOUNT", typeof(Account.accountCD), DescriptionField = typeof(Account.description))]
		[PXUIField(DisplayName = "Account To")]
		public virtual string ToAccountCD
		{
			get;
			set;
		}
		#endregion
		#region MapAccountID
		public abstract class mapAccountID : IBqlField
		{
		}
		[Account(null, typeof(Search<Account.accountID,
			Where<Account.isCashAccount, Equal<False>, And<Account.curyID, IsNull>>>),
			DescriptionField = typeof(Account.description), BqlTable = typeof(BranchAcctMap), DisplayName = "Offset Account")]
		[PXDefault]
		public virtual int? MapAccountID
		{
			get;
			set;
		}
		#endregion
		#region MapSubID
		public abstract class mapSubID : IBqlField
		{
		}
		[SubAccount(typeof(BranchAcctMapTo.mapAccountID), DisplayName = "Offset Subaccount", DescriptionField = typeof(Account.description), BqlTable = typeof(BranchAcctMap))]
		[PXDefault]
		public virtual int? MapSubID
		{
			get;
			set;
		}
		#endregion
	}
}
