namespace PX.Objects.PM
{
	using System;
	using PX.Data;
	using PX.Objects.CM;
	using PX.Objects.TX;

	/// <summary>
	/// Represents a line-level tax detail of an Proforma document.
	/// The entities of this type cannot be edited directly. Instead,
	/// <see cref="TaxBaseAttribute"/> descendants aggregate them to 
	/// <see cref="ARTaxTran"/> records, which can be edited on the Invoices 
	/// and Memos (AR301000) and Cash Sales (AR304000) forms (corresponding
	/// to the <see cref="ARInvoiceEntry"/> and <see cref="ARCashSaleEntry"/> 
	/// graphs, recpectively).
	/// </summary>
	[System.SerializableAttribute()]
	[PXCacheName(Messages.PMTax)]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public partial class PMTax : TaxDetail, PX.Data.IBqlTable
	{
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}
		protected String _RefNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDBDefault(typeof(PMProforma.refNbr))]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual String RefNbr
		{
			get
			{
				return this._RefNbr;
			}
			set
			{
				this._RefNbr = value;
			}
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
		[PXParent(typeof(Select<PMProforma, Where<PMProforma.refNbr, Equal<Current<PMTax.refNbr>>>>))]
		[PXParent(typeof(Select<PMProformaTransactLine, Where<PMProformaTransactLine.refNbr, Equal<Current<PMTax.refNbr>>, And<PMProformaTransactLine.lineNbr, Equal<Current<PMTax.lineNbr>>,And<PMProformaTransactLine.type, Equal<PMProformaLineType.transaction>>>>>), LeaveChildren = true)]
		[PXParent(typeof(Select<PMProformaProgressLine, Where<PMProformaProgressLine.refNbr, Equal<Current<PMTax.refNbr>>, And<PMProformaProgressLine.lineNbr, Equal<Current<PMTax.lineNbr>>,And<PMProformaProgressLine.type, Equal<PMProformaLineType.progressive>>>>>), LeaveChildren = true)]
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
		#region TaxID
		public abstract class taxID : PX.Data.IBqlField
		{
		}
		[PXDBString(Tax.taxID.Length, IsUnicode = true, IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Tax ID")]
		[PXSelector(typeof(Tax.taxID), DescriptionField = typeof(Tax.descr))]
		public override String TaxID
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
		#region TaxRate
		public abstract class taxRate : PX.Data.IBqlField
		{
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField
		{
		}
		[PXDBLong()]
		[CurrencyInfo(typeof(PMProforma.curyInfoID))]
		public override Int64? CuryInfoID
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
		#region CuryOrigTaxableAmt
		public abstract class curyOrigTaxableAmt : PX.Data.IBqlField
		{
		}

		[PXDBCurrency(typeof(PMTax.curyInfoID), typeof(PMTax.origTaxableAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryOrigTaxableAmt { get; set; }
		#endregion
		#region OrigTaxableAmt
		public abstract class origTaxableAmt : PX.Data.IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? OrigTaxableAmt { get; set; }
		#endregion
		#region CuryTaxableAmt
		public abstract class curyTaxableAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryTaxableAmt;
		[PXDBCurrency(typeof(PMTax.curyInfoID), typeof(PMTax.taxableAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? CuryTaxableAmt
		{
			get
			{
				return this._CuryTaxableAmt;
			}
			set
			{
				this._CuryTaxableAmt = value;
			}
		}
		#endregion
		#region TaxableAmt
		public abstract class taxableAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _TaxableAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? TaxableAmt
		{
			get
			{
				return this._TaxableAmt;
			}
			set
			{
				this._TaxableAmt = value;
			}
		}
		#endregion
		#region CuryTaxAmt
		public abstract class curyTaxAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryTaxAmt;
		[PXDBCurrency(typeof(PMTax.curyInfoID), typeof(PMTax.taxAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? CuryTaxAmt
		{
			get
			{
				return this._CuryTaxAmt;
			}
			set
			{
				this._CuryTaxAmt = value;
			}
		}
		#endregion
		#region TaxAmt
		public abstract class taxAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _TaxAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? TaxAmt
		{
			get
			{
				return this._TaxAmt;
			}
			set
			{
				this._TaxAmt = value;
			}
		}
		#endregion
		#region CuryExpenseAmt
		public abstract class curyExpenseAmt : PX.Data.IBqlField
		{
		}
		[PXDBCurrency(typeof(PMTax.curyInfoID), typeof(PMTax.expenseAmt))]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Expense Amount", Visibility = PXUIVisibility.Visible)]
		public override Decimal? CuryExpenseAmt
		{
			get; set;
		}
		#endregion
		#region ExpenseAmt
		public abstract class expenseAmt : PX.Data.IBqlField
		{
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
	}
}
