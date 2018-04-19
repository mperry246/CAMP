using System;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.TX;

namespace PX.Objects.EP
{
	[Serializable]
    [PXCacheName(Messages.EPTax)]
    public partial class EPTax : TaxDetail, IBqlTable
	{
		#region ClaimDetailID
		public abstract class claimDetailID : IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(EPExpenseClaimDetails.claimDetailID))]
		[PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
		[PXParent(typeof(Select<EPExpenseClaimDetails, 
			Where<EPExpenseClaimDetails.claimDetailID, Equal<Current<EPTax.claimDetailID>>>>))]
		public virtual int? ClaimDetailID
		{
			get;
			set;
		}
		#endregion
		#region TaxID
		public abstract class taxID : IBqlField
		{
		}
		[PXDBString(Tax.taxID.Length, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Tax ID")]
		[PXSelector(typeof(Tax.taxID), DescriptionField = typeof(Tax.descr))]
		public override string TaxID
		{
			get
			{
				return this._TaxID;
			}
			set
			{
				this._TaxID = value;
			}
		}
		#endregion
		#region IsTipTax
		public abstract class isTipTax : IBqlField { }
		[PXDBBool(IsKey = true)]
		[PXDefault(false)]
		public virtual bool? IsTipTax
		{
			get;
			set;
		}
		#endregion
		#region TaxRate
		public abstract class taxRate : IBqlField
		{
		} 
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : IBqlField
		{
		}
		[PXDBLong]
		[CurrencyInfo(typeof(EPExpenseClaimDetails.curyInfoID))]
		public override long? CuryInfoID
		{
			get
			{
				return this._CuryInfoID;
			}
			set
			{
				this._CuryInfoID = value;
			}
		}
		#endregion
		#region CuryTaxableAmt
		public abstract class curyTaxableAmt : IBqlField
		{
		}

		[PXDBCurrency(typeof(EPTax.curyInfoID), typeof(EPTax.taxableAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
		public virtual decimal? CuryTaxableAmt
		{
			get;
			set;
		}
		#endregion
		#region TaxableAmt
		public abstract class taxableAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
		public virtual decimal? TaxableAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryTaxAmt
		public abstract class curyTaxAmt : IBqlField
		{
		}

		[PXDBCurrency(typeof(EPTax.curyInfoID), typeof(EPTax.taxAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
		public virtual decimal? CuryTaxAmt
		{
			get;
			set;
		}
		#endregion
		#region TaxAmt
		public abstract class taxAmt : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
		public virtual decimal? TaxAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryExpenseAmt
		public abstract class curyExpenseAmt : IBqlField
		{
		}
		[PXDBCurrency(typeof(EPTax.curyInfoID), typeof(EPTax.expenseAmt))]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Expense Amount", Visibility = PXUIVisibility.Visible)]
		public override decimal? CuryExpenseAmt
		{
			get;
			set;
		}
		#endregion
		#region ExpenseAmt
		public abstract class expenseAmt : IBqlField
		{
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : IBqlField
		{
		}

		[PXDBTimestamp]
		public virtual byte[] tstamp
		{
			get;
			set;
		}
		#endregion
	}
}
