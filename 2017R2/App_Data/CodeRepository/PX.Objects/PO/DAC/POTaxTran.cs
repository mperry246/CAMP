namespace PX.Objects.PO
{
    using System;
    using PX.Data;
    using PX.Objects.TX;
    using PX.Objects.CM;
    using PX.Objects.CS;

    [System.SerializableAttribute()]
	[PXCacheName(Messages.POTaxTran)]
    public partial class POTaxTran : TaxDetail, PX.Data.IBqlTable
    {
        #region RecordID
        public abstract class recordID : PX.Data.IBqlField
        {
        }
        protected Int32? _RecordID;
        [PXDBIdentity(IsKey = true)]
        public virtual Int32? RecordID
        {
            get
            {
                return this._RecordID;
            }
            set
            {
                this._RecordID = value;
            }
        }
        #endregion
        #region OrderType
        public abstract class orderType : PX.Data.IBqlField
        {
        }
        protected String _OrderType;
        [PXDBString(2, IsFixed = true, IsKey = true)]
        [PXDBDefault(typeof(POOrder.orderType), DefaultForUpdate = false)]
        [PXUIField(DisplayName = "Order Type", Enabled = false, Visible = false)]
        public virtual String OrderType
        {
            get
            {
                return this._OrderType;
            }
            set
            {
                this._OrderType = value;
            }
        }
        #endregion
        #region OrderNbr
        public abstract class orderNbr : PX.Data.IBqlField
        {
        }
        protected String _OrderNbr;
        [PXDBString(15, IsUnicode = true, InputMask = "", IsKey = true)]
        [PXDBDefault(typeof(POOrder.orderNbr), DefaultForUpdate = false)]
        [PXUIField(DisplayName = "Order Nbr.", Enabled = false, Visible = false)]
        public virtual String OrderNbr
        {
            get
            {
                return this._OrderNbr;
            }
            set
            {
                this._OrderNbr = value;
            }
        }
        #endregion
        #region LineNbr
        public abstract class lineNbr : PX.Data.IBqlField
        {
        }
        protected Int32? _LineNbr;
        [PXDBInt(IsKey = true)]
        [PXDefault(LineNbrValue)]
        [PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
		[PXParent(typeof(Select<POOrder, Where<POOrder.orderType, Equal<Current<POTaxTran.orderType>>, And<POOrder.orderNbr, Equal<Current<POTaxTran.orderNbr>>>>>))]
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
        [PXUIField(DisplayName = "Tax ID", Visibility = PXUIVisibility.Visible)]
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
        #region JurisType
        public abstract class jurisType : PX.Data.IBqlField
        {
        }
        protected String _JurisType;
        [PXDBString(9, IsUnicode = true)]
        [PXUIField(DisplayName = "Tax Jurisdiction Type")]
        public virtual String JurisType
        {
            get
            {
                return this._JurisType;
            }
            set
            {
                this._JurisType = value;
            }
        }
        #endregion
        #region JurisName
        public abstract class jurisName : PX.Data.IBqlField
        {
        }
        protected String _JurisName;
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Tax Jurisdiction Name")]
        public virtual String JurisName
        {
            get
            {
                return this._JurisName;
            }
            set
            {
                this._JurisName = value;
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
        [CurrencyInfo(typeof(POOrder.curyInfoID))]
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
        #region CuryTaxableAmt
        public abstract class curyTaxableAmt : PX.Data.IBqlField
        {
        }
		protected decimal? _CuryTaxableAmt;
        [PXDBCurrency(typeof(POTaxTran.curyInfoID), typeof(POTaxTran.taxableAmt))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
        [PXUnboundFormula(typeof(Switch<Case<Where<WhereExempt<POTaxTran.taxID>>, POTaxTran.curyTaxableAmt>, decimal0>), typeof(SumCalc<POOrder.curyVatExemptTotal>))]
        [PXUnboundFormula(typeof(Switch<Case<Where<WhereTaxable<POTaxTran.taxID>>, POTaxTran.curyTaxableAmt>, decimal0>), typeof(SumCalc<POOrder.curyVatTaxableTotal>))]
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
		#region CuryOpenTaxableAmt
		public abstract class curyOpenTaxableAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryOpenTaxableAmt;
		[PXDBCurrency(typeof(POTaxTran.curyInfoID), typeof(POTaxTran.openTaxableAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Open Taxable Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? CuryOpenTaxableAmt
		{
			get
			{
				return this._CuryOpenTaxableAmt;
			}
			set
			{
				this._CuryOpenTaxableAmt = value;
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
		#region OpenTaxableAmt
		public abstract class openTaxableAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _OpenTaxableAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? OpenTaxableAmt
		{
			get
			{
				return this._OpenTaxableAmt;
			}
			set
			{
				this._OpenTaxableAmt = value;
			}
		}
		#endregion
        #region CuryTaxAmt
        public abstract class curyTaxAmt : PX.Data.IBqlField
        {
        }
		protected decimal? _CuryTaxAmt;
        [PXDBCurrency(typeof(POTaxTran.curyInfoID), typeof(POTaxTran.taxAmt))]
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
		#region CuryOpenTaxAmt
		public abstract class curyOpenTaxAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryOpenTaxAmt;
		[PXDBCurrency(typeof(POTaxTran.curyInfoID), typeof(POTaxTran.openTaxAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Open Tax Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? CuryOpenTaxAmt
		{
			get
			{
				return this._CuryOpenTaxAmt;
			}
			set
			{
				this._CuryOpenTaxAmt = value;
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
		[PXDBCurrency(typeof(POTaxTran.curyInfoID), typeof(POTaxTran.expenseAmt))]
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
		#region OpenTaxAmt
		public abstract class openTaxAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _OpenTaxAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? OpenTaxAmt
		{
			get
			{
				return this._OpenTaxAmt;
			}
			set
			{
				this._OpenTaxAmt = value;
			}
		}
		#endregion
		#region TaxZoneID
		public abstract class taxZoneID : IBqlField { }
        [PXDBString(10, IsUnicode = true)]
        public virtual string TaxZoneID
        {
            get;
            set;
        }
        #endregion

        public class lineNbrValue : Constant<Int32>
        {
            public lineNbrValue() : base(LineNbrValue) { ;}
        }

        public const Int32 LineNbrValue = int.MaxValue;
    }
}
