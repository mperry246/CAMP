using System;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.CA
{
    [Obsolete("Will be removed in Acumatica ERP 2018R2")]
    [Serializable]
	[PXHidden]
	public partial class AddDetailFilter : IBqlTable
	{
		#region OrigModule
		public abstract class origModule : IBqlField
		{
		}
		[PXString(2, IsFixed = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Orig. Module")]
		public virtual string OrigModule
		{
			get;
			set;
		}
		#endregion
		#region OrigTranType
		public abstract class origTranType : IBqlField
		{
		}
		[PXString(3)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Transaction Type")]
		[CAAPARTranType.ListByModule(typeof(origModule))]
		public virtual string OrigTranType
		{
			get;
			set;
		}
		#endregion
		#region TranDate
		public abstract class tranDate : IBqlField
		{
		}
		[PXDate]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Doc. Date")]
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
		[PXDefault]
		[CashAccount(DisplayName = "Cash Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(CashAccount.descr))]
		public virtual int? CashAccountID
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
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[PXSelector(typeof(Currency.curyID))]
		public virtual string CuryID
		{
			get;
			set;
		}
		#endregion
		#region ExtRefNbr
		public abstract class extRefNbr : IBqlField
		{
		}
		[PXDBString(40, IsUnicode = true)]
		[PXUIField(DisplayName = "Document Ref.")]
		public virtual string ExtRefNbr
		{
			get;
			set;
		}
		#endregion
		#region EntryTypeID
		public abstract class entryTypeID : IBqlField
		{
		}
		[PXString(10, IsUnicode = true)]
		[PXSelector(typeof(Search<CAEntryType.entryTypeId>))]
		[PXUIField(DisplayName = "Entry Type")]
		public virtual string EntryTypeID
		{
			get;
			set;
		}
		#endregion
		#region Descr
		public abstract class descr : IBqlField
		{
		}
		[PXString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description")]
		public virtual string Descr
		{
			get;
			set;
		}
		#endregion
		#region AccountID
		public abstract class accountID : IBqlField
		{
		}
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[Account(DisplayName = "Offset Account")]
		public virtual int? AccountID
		{
			get;
			set;
		}
		#endregion
		#region SubID
		public abstract class subID : IBqlField
		{
		}
		[SubAccount(typeof(AddDetailFilter.accountID), DisplayName = "Offset Subaccount")]
		public virtual int? SubID
		{
			get;
			set;
		}
		#endregion
		#region CuryTranAmt
		public abstract class curyTranAmt : IBqlField
		{
		}
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Amount")]
		[PXDecimal(2)]
		public virtual decimal? CuryTranAmt
		{
			get;
			set;
		}
		#endregion
		#region DrCr
		public abstract class drCr : IBqlField
		{
		}
		[PXDefault(GL.DrCr.Debit)]
		[PXString(1, IsFixed = true)]
		[CADrCr.List]
		[PXUIField(DisplayName = "Debit / Credit")]
		public virtual string DrCr
		{
			get;
			set;
		}
		#endregion
		#region ReferenceID
		public abstract class referenceID : IBqlField
		{
		}
		[PXInt]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXSelector(typeof(BAccount.bAccountID),
						SubstituteKey = typeof(BAccount.acctCD),
					 DescriptionField = typeof(BAccount.acctName))]
		[PXUIField(DisplayName = "Business Account", Visibility = PXUIVisibility.Visible)]
		public virtual int? ReferenceID
		{
			get;
			set;
		}
		#endregion
	}
}
