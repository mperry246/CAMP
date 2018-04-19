using System;
using PX.Data;
using PX.Data.EP;
using PX.Objects.CM;
using PX.Objects.GL;

namespace PX.Objects.CA
{
	[Serializable]
	[PXPrimaryGraph(typeof(CashForecastEntry))]
	[PXCacheName(Messages.CashTransactions)]
	public partial class CashForecastTran : IBqlTable
	{
		#region TranID
		public abstract class tranID : IBqlField
		{
		}

		[PXDBIdentity(IsKey = true)]
		[PXUIField(DisplayName = "Document Number", Visible = false)]
		public virtual int? TranID
		{
			get;
			set;
		}
		#endregion
		#region TranDate
		public abstract class tranDate : IBqlField
		{
		}

		[PXDBDate]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Tran. Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? TranDate
		{
			get;
			set;
		}
		#endregion
		#region CashAccountID
		public abstract class cashAccountID : IBqlField
		{
		}

		[CashAccount(DisplayName = "Cash Account", Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(CashAccount.descr))]
		[PXDefault]
		public virtual int? CashAccountID
		{
			get;
			set;
		}
		#endregion
		#region DrCr
		public abstract class drCr : IBqlField
		{
		}

		[PXDefault(CADrCr.CADebit)]
		[PXDBString(1, IsFixed = true)]
		[CADrCr.List]
		[PXUIField(DisplayName = "Disb. / Receipt", Enabled = true, Visible = true, Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string DrCr
		{
			get;
			set;
		}
		#endregion
		#region TranDesc
		public abstract class tranDesc : IBqlField
		{
		}

		[PXDBString(256, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault]
		[PXFieldDescription]
		public virtual string TranDesc
		{
			get;
			set;
		}
		#endregion
		#region CuryID
		public abstract class curyID : IBqlField
		{
		}

		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
		[PXUIField(DisplayName = "Currency")]
		[PXDefault(typeof(Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CashForecastTran.cashAccountID>>>>))]
		[PXSelector(typeof(Currency.curyID))]
		public virtual string CuryID
		{
			get;
			set;
		}
		#endregion
		#region CuryTranAmt
		public abstract class curyTranAmt : IBqlField
		{
		}

		[PXDBCury(typeof(CashForecastTran.curyID))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount")]
		public virtual decimal? CuryTranAmt
		{
			get;
			set;
		}
		#endregion
		#region TranAmt
		public abstract class tranAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tran. Amount", Enabled = false)]
		public virtual decimal? TranAmt
		{
			get;
			set;
		}
		#endregion
		#region NoteID
		public abstract class noteID : IBqlField
		{
		}

		[PXNote(new Type[0], DescriptionField = typeof(CashForecastTran.tranDesc))]
		public virtual Guid? NoteID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : IBqlField
		{
		}

		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField
		{
		}

		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField
		{
		}

		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : IBqlField
		{
		}

		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField
		{
		}

		[PXDBLastModifiedByScreenID]
		public virtual string LastModifiedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField
		{
		}

		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
	}
}
