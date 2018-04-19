using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.Common;

namespace PX.Objects.AR
{
	/// <summary>
	/// A projection DAC over <see cref="ARHistory"/> that is intended to close the gaps 
	/// in AR history records. (The gaps in AR history records appear if AR history records 
	/// do not exist for every financial period defined in the system.) That is, the purpose 
	/// of this DAC is to calculate the <see cref="LastActivityPeriod">last activity period</see> 
	/// for every existing <see cref="FinPeriod">financial period</see>, so that inquiries and reports 
	/// that produce information for a given financial period can look at the latest available 
	/// <see cref="ARHistory"/> record. For example, this projection is used in the Customer 
	/// Summary (AR401000) form, which corresponds to the <see cref="ARCustomerBalanceEnq"/> graph.
	/// </summary>
	[Serializable]
	[PXProjection(typeof(Select5<ARHistory,
		InnerJoin<GL.FinPeriod,
			On<GL.FinPeriod.finPeriodID, GreaterEqual<ARHistory.finPeriodID>>>,
		Aggregate<
		GroupBy<ARHistory.branchID,
		GroupBy<ARHistory.customerID,
		GroupBy<ARHistory.accountID,
		GroupBy<ARHistory.subID,
		Max<ARHistory.finPeriodID,
		GroupBy<GL.FinPeriod.finPeriodID>>>>>>>>))]
	[PXPrimaryGraph(
		new Type[] {
			typeof(ARDocumentEnq),
			typeof(ARCustomerBalanceEnq)
		},
		new Type[] {
			typeof(Where<BaseARHistoryByPeriod.customerID, IsNotNull>),
			typeof(Where<BaseARHistoryByPeriod.customerID, IsNull>)
		},
		Filters = new Type[] {
			typeof(ARDocumentEnq.ARDocumentFilter),
			typeof(ARCustomerBalanceEnq.ARHistoryFilter)
		})]
	[PXCacheName(Messages.BaseARHistoryByPeriod)]
	public partial class BaseARHistoryByPeriod : PX.Data.IBqlTable
	{
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(IsKey = true, BqlField = typeof(ARHistory.branchID))]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region CustomerID
		public abstract class customerID : PX.Data.IBqlField
		{
		}
		protected Int32? _CustomerID;
		[Customer(IsKey = true, BqlField = typeof(ARHistory.customerID), CacheGlobal = true)]
		public virtual Int32? CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				this._CustomerID = value;
			}
		}
		#endregion
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[Account(IsKey = true, BqlField = typeof(ARHistory.accountID))]
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
		#region SubID
		public abstract class subID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubID;
		[SubAccount(IsKey = true, BqlField = typeof(ARHistory.subID))]
		public virtual Int32? SubID
		{
			get
			{
				return this._SubID;
			}
			set
			{
				this._SubID = value;
			}
		}
		#endregion
		#region LastActivityPeriod
		public abstract class lastActivityPeriod : PX.Data.IBqlField
		{
		}
		protected String _LastActivityPeriod;
		[GL.FinPeriodID(BqlField = typeof(ARHistory.finPeriodID))]
		public virtual String LastActivityPeriod
		{
			get
			{
				return this._LastActivityPeriod;
			}
			set
			{
				this._LastActivityPeriod = value;
			}
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		protected String _FinPeriodID;
		[GL.FinPeriodID(IsKey = true, BqlField = typeof(GL.FinPeriod.finPeriodID))]
		public virtual String FinPeriodID
		{
			get
			{
				return this._FinPeriodID;
			}
			set
			{
				this._FinPeriodID = value;
			}
		}
		#endregion

	}

	/// <summary>
	/// A projection DAC over <see cref="CuryARHistory"/> that is intended to close the gaps 
	/// in AR history records. (The gaps in AR history records appear if AR history records do 
	/// not exist for every financial period defined in the system.) That is, the purpose of 
	/// this DAC is to calculate the <see cref="LastActivityPeriod">last activity period</see> 
	/// for every existing <see cref="FinPeriod">financial period</see>, so that inquiries and 
	/// reports that produce information for a given financial period can look at the latest
	/// available <see cref="CuryARHistory"/> record. For example, this projection is
	/// used in the Customer Summary (AR401000) form, which corresponds to the
	/// <see cref="ARCustomerBalanceEnq"/> graph.
	/// </summary>
	[Serializable]
	[PXProjection(typeof(Select5<CuryARHistory,
		InnerJoin<GL.FinPeriod,
			On<GL.FinPeriod.finPeriodID, GreaterEqual<CuryARHistory.finPeriodID>>>,
		Aggregate<
		GroupBy<CuryARHistory.branchID,
		GroupBy<CuryARHistory.customerID,
		GroupBy<CuryARHistory.accountID,
		GroupBy<CuryARHistory.subID,
		GroupBy<CuryARHistory.curyID,
		Max<CuryARHistory.finPeriodID,
		GroupBy<GL.FinPeriod.finPeriodID>>>>>>>>>))]
	[PXCacheName(Messages.ARHistoryByPeriod)]
	public partial class ARHistoryByPeriod : PX.Data.IBqlTable
	{
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(IsKey = true, BqlField = typeof(CuryARHistory.branchID))]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region CustomerID
		public abstract class customerID : PX.Data.IBqlField
		{
		}
		protected Int32? _CustomerID;
		[Customer(IsKey = true, BqlField = typeof(CuryARHistory.customerID))]
		public virtual Int32? CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				this._CustomerID = value;
			}
		}
		#endregion
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[Account(IsKey = true, BqlField = typeof(CuryARHistory.accountID))]
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
		#region SubID
		public abstract class subID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubID;
		[SubAccount(IsKey = true, BqlField = typeof(CuryARHistory.subID))]
		public virtual Int32? SubID
		{
			get
			{
				return this._SubID;
			}
			set
			{
				this._SubID = value;
			}
		}
		#endregion
		#region CuryID
		public abstract class curyID : PX.Data.IBqlField
		{
		}
		protected String _CuryID;
		[PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL", BqlField = typeof(CuryARHistory.curyID))]
		public virtual String CuryID
		{
			get
			{
				return this._CuryID;
			}
			set
			{
				this._CuryID = value;
			}
		}
		#endregion
		#region LastActivityPeriod
		public abstract class lastActivityPeriod : PX.Data.IBqlField
		{
		}
		protected String _LastActivityPeriod;
		[GL.FinPeriodID(BqlField = typeof(CuryARHistory.finPeriodID))]
		public virtual String LastActivityPeriod
		{
			get
			{
				return this._LastActivityPeriod;
			}
			set
			{
				this._LastActivityPeriod = value;
			}
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		protected String _FinPeriodID;
		[GL.FinPeriodID(IsKey = true, BqlField = typeof(GL.FinPeriod.finPeriodID))]
		public virtual String FinPeriodID
		{
			get
			{
				return this._FinPeriodID;
			}
			set
			{
				this._FinPeriodID = value;
			}
		}
		#endregion

	}

	[PX.Objects.GL.TableAndChartDashboardType]
	public class ARCustomerBalanceEnq : PXGraph<ARCustomerBalanceEnq>
	{
		#region Internal Types
		[Serializable]
		public partial class ARHistoryFilter : IBqlTable
		{
			#region BranchID
			public abstract class branchID : IBqlField
			{
			}
			[Branch]
			[PXRestrictor(typeof(Where<True, Equal<True>>), GL.Messages.BranchInactive, ReplaceInherited = true)]
			public virtual int? BranchID
			{
				get;
				set;
			}
			#endregion
			#region ARAcctID
			public abstract class aRAcctID : IBqlField { }
			[GL.Account(null,typeof(Search5<Account.accountID,
						InnerJoin<ARHistory, On<Account.accountID, Equal<ARHistory.accountID>>>,
						Where<Match<Current<AccessInfo.userName>>>,
					   Aggregate<GroupBy<Account.accountID>>>),
				DisplayName = "AR Account", DescriptionField = typeof(GL.Account.description))]
			public virtual int? ARAcctID
			{
				get;
				set;
			}
			#endregion
			#region ARSubID
			public abstract class aRSubID : IBqlField { }
			[GL.SubAccount(DisplayName = "AR Sub.", DescriptionField = typeof(GL.Sub.description), Visible = false)]
			public virtual int? ARSubID
			{
				get;
				set;
			}
			#endregion
			#region SubCD
			public abstract class subCD : IBqlField { }
			[PXDBString(30, IsUnicode = true)]
			[PXUIField(DisplayName = "AR Subaccount", Visibility = PXUIVisibility.Invisible, FieldClass = SubAccountAttribute.DimensionName)]
			[PXDimension("SUBACCOUNT", ValidComboRequired = false)]
			public virtual string SubCD
			{
				get;
				set;
			}
			#endregion
			#region CuryID
			public abstract class curyID : IBqlField { }
			[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
			[PXSelector(typeof(CM.Currency.curyID), CacheGlobal = true)]
			[PXUIField(DisplayName = "Currency ID")]
			public virtual string CuryID
			{
				get;
				set;
			}
			#endregion
			#region CustomerClassID
			public abstract class customerClassID : IBqlField { }
			[PXDBString(10, IsUnicode = true)]
			[PXSelector(typeof(CustomerClass.customerClassID), DescriptionField = typeof(CustomerClass.descr), CacheGlobal = true)]
			[PXUIField(DisplayName = "Customer Class")]
			public virtual string CustomerClassID
			{
				get;
				set;
			}
			#endregion
			#region Status
			public abstract class status : IBqlField { }
			[PXDBString(1, IsFixed = true)]
			[PXUIField(DisplayName = "Status")]
			[BAccount.status.List]
			public virtual string Status
			{
				get;
				set;
			}
			#endregion
			#region ShowWithBalanceOnly
			public abstract class showWithBalanceOnly : IBqlField { }
			[PXDBBool]
			[PXDefault(true)]
			[PXUIField(DisplayName = "Customers with Balance Only")]
			public virtual bool? ShowWithBalanceOnly
			{
				get;
				set;
			}
			#endregion
			#region Period
			public abstract class period : IBqlField { }
			
			[ARAnyPeriodFilterable]
			[PXUIField(DisplayName = "Period", Visibility = PXUIVisibility.Visible)]
			public virtual string Period
			{
				get;
				set;
			}
			#endregion
			#region ByFinancialPeriod
			public abstract class byFinancialPeriod : IBqlField { }

			[PXDBBool]
			[PXDefault(true)]
			[PXUIField(DisplayName = "By Financial Period")]
			public virtual bool? ByFinancialPeriod
			{
				get;
				set;
			}
			#endregion
			#region SubCD Wildcard
			public abstract class subCDWildcard : IBqlField { };
			[PXDBString(30, IsUnicode = true)]
			public virtual String SubCDWildcard
			{
				get
				{
					return SubCDUtils.CreateSubCDWildcard(this.SubCD, SubAccountAttribute.DimensionName);
				}
			}
            #endregion
            #region CustomerID
            public abstract class customerID : IBqlField { }
            [PXDBInt]
            public virtual int? CustomerID
            {
                get;
                set;
            }
            #endregion
            #region SplitByCurrency
            public abstract class splitByCurrency : IBqlField { }
            [PXDBBool]
            [PXDefault(true)]
            [PXUIField(DisplayName = Common.Messages.SplitByCurrency)]
            public virtual bool? SplitByCurrency
            {
                get;
                set;
            }
            #endregion
            #region CuryBalanceSummary
            public abstract class curyBalanceSummary : PX.Data.IBqlField
			{
			}
			protected Decimal? _CuryBalanceSummary;
			[PXCury(typeof(ARHistoryFilter.curyID))]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "Total Balance (Currency)", Enabled = false)]
			public virtual Decimal? CuryBalanceSummary
			{
				get
				{
					return this._CuryBalanceSummary;
				}
				set
				{
					this._CuryBalanceSummary = value;
				}
			}
			#endregion
			#region BalanceSummary
			public abstract class balanceSummary : PX.Data.IBqlField
			{
			}
			protected Decimal? _BalanceSummary;
			[PXDBBaseCury]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "Total Balance", Enabled = false)]
			public virtual Decimal? BalanceSummary
			{
				get
				{
					return this._BalanceSummary;
				}
				set
				{
					this._BalanceSummary = value;
				}
			}
			#endregion

			#region CuryRevaluedSummary
			public abstract class curyRevaluedSummary : PX.Data.IBqlField
			{
			}
			protected Decimal? _CuryRevaluedSummary;
			[PXCury(typeof(ARHistoryFilter.curyID))]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "Total Unrealized Gain/Loss", Enabled = false)]
			public virtual Decimal? CuryRevaluedSummary
			{
				get
				{
					return this._CuryRevaluedSummary;
				}
				set
				{
					this._CuryRevaluedSummary = value;
				}
			}
			#endregion
			#region RevaluedSummary
			public abstract class revaluedSummary : PX.Data.IBqlField
			{
			}
			protected Decimal? _RevaluedSummary;
			[PXDBBaseCury]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "Total Unrealized Gain/Loss", Enabled = false)]
			public virtual Decimal? RevaluedSummary
			{
				get
				{
					return this._RevaluedSummary;
				}
				set
				{
					this._RevaluedSummary = value;
				}
			}
			#endregion

			#region DepositsSummary
			public abstract class depositsSummary : PX.Data.IBqlField
			{
			}
			protected Decimal? _DepositsSummary;
			[PXDBBaseCury]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "Total Prepayments", Enabled = false)]
            public virtual Decimal? DepositsSummary
            {
				get;
				set;
			}
			#endregion
			#region IncludeChildAccounts
			public abstract class includeChildAccounts : IBqlField { }

			[PXDBBool]
			[PXDefault(typeof(Search<CS.FeaturesSet.parentChildAccount>))]
			[PXUIField(DisplayName = "Consolidate by Parent")]
			public virtual bool? IncludeChildAccounts { get; set; }
			#endregion
			
		}

        public partial class ARHistorySummary : IBqlTable
	    {
            #region CuryBalanceSummary
            public abstract class curyBalanceSummary : IBqlField { }

            [PXCury(typeof(ARHistoryFilter.curyID))]
            [PXDefault(TypeCode.Decimal, "0.0")]
            [PXUIField(DisplayName = "Total Balance (Currency)", Enabled = false)]
            public virtual decimal? CuryBalanceSummary
            {
                get;
                set;
            }
            #endregion
            #region BalanceSummary
            public abstract class balanceSummary : IBqlField { }

            [PXBaseCury]
            [PXDefault(TypeCode.Decimal, "0.0")]
            [PXUIField(DisplayName = "Total Balance", Enabled = false)]
            public virtual decimal? BalanceSummary
            {
                get;
                set;
            }
            #endregion
			#region IncludeChildAccounts
			public abstract class includeChildAccounts : IBqlField { }
			public virtual bool? IncludeChildAccounts { get; set; }
			#endregion
			#region CuryRevaluedSummary
			public abstract class curyRevaluedSummary : IBqlField { }

            [PXCury(typeof(ARHistoryFilter.curyID))]
            [PXDefault(TypeCode.Decimal, "0.0")]
            [PXUIField(DisplayName = "Total Unrealized Gain/Loss", Enabled = false)]
            public virtual decimal? CuryRevaluedSummary
            {
                get;
                set;
            }
			#endregion
			#region RevaluedSummary
			public abstract class revaluedSummary : IBqlField { }

            [PXBaseCury]
            [PXDefault(TypeCode.Decimal, "0.0")]
            [PXUIField(DisplayName = "Total Unrealized Gain/Loss", Enabled = false)]
            public virtual decimal? RevaluedSummary
            {
                get;
                set;
            }
            #endregion
            #region CuryDepositsSummary
            public abstract class curyDepositsSummary : IBqlField { }

            [PXCury(typeof(ARHistoryFilter.curyID))]
            [PXDefault(TypeCode.Decimal, "0.0")]
            [PXUIField(DisplayName = "Total Prepayments (Currency)", Enabled = false)]
            public virtual decimal? CuryDepositsSummary
            {
                get;
                set;
            }
            #endregion
            #region DepositsSummary
            public abstract class depositsSummary : IBqlField { }
            [PXBaseCury]
            [PXDefault(TypeCode.Decimal, "0.0")]
            [PXUIField(DisplayName = "Total Prepayments", Enabled = false)]
            public virtual decimal? DepositsSummary
            {
                get;
                set;
            }
            #endregion
            #region Calculated
            public abstract class calculated : IBqlField { }
            /// <summary>
            /// Specifies (if set to <c>true</c>) that the <see cref="ARCustomerBalanceEnq.history"/> delegate calculated the summary.
            /// </summary>
            [PXBool]
            [PXDefault(false)]
            public virtual bool? Calculated
            {
                get;
                set;
            }
            #endregion
            public virtual void ClearSummary()
            {
                this.BalanceSummary = decimal.Zero;
                this.RevaluedSummary = decimal.Zero;
                this.DepositsSummary = decimal.Zero;
                this.CuryBalanceSummary = decimal.Zero;
                this.CuryRevaluedSummary = decimal.Zero;
                this.CuryDepositsSummary = decimal.Zero;
                this.Calculated = false;
            }
        }

        [Serializable]
		public partial class ARHistoryResult : IBqlTable
		{
			#region CustomerID
			public abstract class customerID : PX.Data.IBqlField { }

            [PXDBInt]
			[PXDefault]
			public virtual int? CustomerID
			{
				get;
				set;
			}
			#endregion
			#region AcctCD
			public abstract class acctCD : IBqlField { }

            [PXDimensionSelector(CustomerAttribute.DimensionName, typeof(CA.Light.Customer.acctCD), typeof(acctCD),
                typeof(CA.Light.Customer.acctCD), typeof(CA.Light.Customer.acctName))]
            [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
			[PXUIField(DisplayName = "Customer ID", Visibility = PXUIVisibility.SelectorVisible)]
			public virtual string AcctCD
			{
				get;
				set;
			}
			#endregion
			#region AcctName
			public abstract class acctName : IBqlField { }

			[PXDBString(60, IsUnicode = true)]
			[PXUIField(DisplayName = "Customer Name", Visibility = PXUIVisibility.SelectorVisible)]
			public virtual string AcctName
			{
				get;
				set;
			}
			#endregion
			#region FinPeriodID
			public abstract class finPeriodID : IBqlField { }

			[GL.FinPeriodID]
			[PXUIField(DisplayName = "Last Activity Period", Visibility = PXUIVisibility.SelectorVisible)]
			public virtual string FinPeriodID
			{
				get;
				set;
			}
			#endregion
			#region CuryID
			public abstract class curyID : IBqlField { }

			[PXDBString(5, IsUnicode = true)]
			[PXUIField(DisplayName = "Currency ID", Visibility = PXUIVisibility.SelectorVisible)]
			public virtual string CuryID
			{
				get;
				set;
			}
			#endregion
			#region CuryBegBalance
			public abstract class curyBegBalance : IBqlField { }

			[PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency Beginning Balance", Visible = false)]
			public virtual decimal? CuryBegBalance
			{
				get;
				set;
			}
			#endregion
			#region BegBalance
			public abstract class begBalance : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "Beginning Balance", Visible = false)]
			public virtual decimal? BegBalance
			{
				get;
				set;
			}
			#endregion
			#region CuryEndBalance
			public abstract class curyEndBalance : IBqlField { }

			[PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency Ending Balance", Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
			public virtual decimal? CuryEndBalance
			{
				get;
				set;
			}
			#endregion
			#region EndBalance
			public abstract class endBalance : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "Ending Balance", Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
			public virtual decimal? EndBalance
			{
				get;
				set;
			}
			#endregion
			#region CuryBalance
			public abstract class curyBalance : IBqlField { }

			[PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency Balance", Visible = false)]
			public virtual decimal? CuryBalance
			{
				get;
				set;
			}
			#endregion
			#region Balance
			public abstract class balance : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "Balance", Visible=false)]
			public virtual decimal? Balance
			{
				get;
				set;
			}
			#endregion

			#region CurySales
			public abstract class curySales: IBqlField { }

			[PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency PTD Sales")]
			public virtual decimal? CurySales
			{
				get;
				set;
			}
			#endregion
			#region Sales
			public abstract class sales : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "PTD Sales")]
			public virtual decimal? Sales
			{
				get;
				set;
			}
			#endregion

			#region CuryPayments
			public abstract class curyPayments : IBqlField { }

			[PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency PTD Payments")]
			public virtual decimal? CuryPayments
			{
				get;
				set;
			}
			#endregion
			#region Payments
			public abstract class payments : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "PTD Payments")]
			public virtual decimal? Payments
			{
				get;
				set;
			}
			#endregion
			#region CuryDiscount
			public abstract class curyDiscount : IBqlField { } 

			[PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency PTD Cash Discount Taken")]
			public virtual decimal? CuryDiscount
			{
				get;
				set;
			}
			#endregion
			#region Discount
			public abstract class discount : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "PTD Cash Discount Taken")]
			public virtual decimal? Discount
			{
				get;
				set;
			}
			#endregion
			#region RGOL
			public abstract class rGOL : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "PTD Realized Gain/Loss")]
			public virtual decimal? RGOL
			{
				get;
				set;
			}
			#endregion
			#region CuryCrAdjustments
			public abstract class curyCrAdjustments : IBqlField { }

			[PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency PTD Credit Memos")]
			public virtual decimal? CuryCrAdjustments
			{
				get;
				set;
			}
			#endregion
			#region CrAdjustments
			public abstract class crAdjustments : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "PTD Credit Memos")]
			public virtual decimal? CrAdjustments
			{
				get;
				set;
			}
			#endregion
			#region CuryDrAdjustments
			public abstract class curyDrAdjustments : IBqlField { }

			[PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency PTD Debit Memos")]
			public virtual decimal? CuryDrAdjustments
			{
				get;
				set;
			}
			#endregion
			#region DrAdjustments
			public abstract class drAdjustments : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "PTD Debit Memos")]
			public virtual decimal? DrAdjustments
			{
				get;
				set;
			}
			#endregion
			#region COGS
			public abstract class cOGS : IBqlField { }

			[PXDBBaseCury]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "PTD COGS")]
			public virtual decimal? COGS
			{
				get;
				set;
			}
			#endregion
			#region FinPtdRevaluated
			public abstract class finPtdRevaluated : IBqlField { }

			[PXDBBaseCury]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "Unrealized Gain/Loss")]
			public virtual decimal? FinPtdRevaluated
			{
				get;
				set;
			}
			#endregion
			#region CuryFinCharges
			public abstract class curyFinCharges : IBqlField { }

			[PXDBBaseCury]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "Currency PTD Overdue Charges", Visible = true)]
			public virtual decimal? CuryFinCharges
			{
				get;
				set;
			}
			#endregion
			#region FinCharges
			public abstract class finCharges : IBqlField { }

			[PXDBBaseCury]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "PTD Overdue Charges", Visible = true)]
			public virtual decimal? FinCharges
			{
				get;
				set;
			}
			#endregion
			#region CuryDeposits
			public abstract class curyDeposits : IBqlField { }

			[PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency PTD Prepayments")]
			public virtual decimal? CuryDeposits
			{
				get;
				set;
			}
			#endregion
			#region Deposits
			public abstract class deposits : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "PTD Prepayments")]
			public virtual decimal? Deposits
			{
				get;
				set;
			}
			#endregion
			#region CuryDepositsBalance
			public abstract class curyDepositsBalance : IBqlField { }

            [PXDBCury(typeof(ARHistoryResult.curyID))]
			[PXUIField(DisplayName = "Currency Prepayments Balance")]
			public virtual decimal? CuryDepositsBalance
			{
				get;
				set;
			}
			#endregion
			#region DepositsBalance
			public abstract class depositsBalance : IBqlField { }

			[PXBaseCury]
			[PXUIField(DisplayName = "Prepayments Balance")]
			public virtual decimal? DepositsBalance
			{
				get;
				set;
			}
			#endregion
			#region Converted
			public abstract class converted : IBqlField { }

			[PXDBBool]
			[PXDefault(false)]
			[PXUIField(DisplayName = "Converted to Base Currency", Visible = false, Enabled = false)]
			public virtual bool? Converted
			{
				get;
				set;
			}
			#endregion
			#region NoteID
			public abstract class noteID : IBqlField { }

			[PXNote]
			public virtual Guid? NoteID
			{
				get;
				set;
			}
			#endregion
			public virtual void RecalculateEndBalance()
			{
				const decimal zero = 0m;
				this.RecalculateBalance();
				this.EndBalance = (this.BegBalance ?? zero) +
							   +(this.Balance ?? zero);
				this.CuryEndBalance = (this.CuryBegBalance ?? zero) +
							   +(this.CuryBalance ?? zero);
			}

			public virtual void RecalculateBalance()
			{
				const decimal zero = 0m;
				this.Balance = (this.Sales ?? zero)
							   - (this.Payments ?? zero)
							   - (this.Discount ?? zero)							   
							   + (this.RGOL ?? zero)
							   - (this.CrAdjustments ?? zero)
							   + (this.FinCharges ?? zero)
							   + (this.DrAdjustments ?? zero);
				this.CuryBalance = (this.CurySales ?? zero)
							   - (this.CuryPayments ?? zero)
							   - (this.CuryDiscount ?? zero)							   
							   - (this.CuryCrAdjustments ?? zero)
							   + (this.CuryFinCharges ?? zero)
							   + (this.CuryDrAdjustments ?? zero);
							   
			}

			public virtual void CopyValueToCuryValue(string aBaseCuryID)
			{
				this.CuryBegBalance = this.BegBalance ?? Decimal.Zero;
				this.CurySales = this.Sales ?? Decimal.Zero;
				this.CuryPayments = this.Payments ?? Decimal.Zero;
				this.CuryDiscount = this.Discount ?? Decimal.Zero;
				this.CuryFinCharges = this.FinCharges?? Decimal.Zero;
				this.CuryCrAdjustments = this.CrAdjustments ?? Decimal.Zero;
				this.CuryDrAdjustments = this.DrAdjustments ?? Decimal.Zero;
				this.CuryDeposits = this.Deposits ?? Decimal.Zero;
				this.CuryDepositsBalance = this.DepositsBalance ?? Decimal.Zero;
				this.CuryEndBalance = this.EndBalance ?? Decimal.Zero;
				this.CuryID = aBaseCuryID;
				this.Converted = true;
			}
		}
        #endregion

        [Serializable]
		[PXProjection(typeof(Select4<CuryARHistory,
			Aggregate<
			GroupBy<CuryARHistory.branchID,
			GroupBy<CuryARHistory.customerID,
			GroupBy<CuryARHistory.accountID,
			GroupBy<CuryARHistory.subID,
			GroupBy<CuryARHistory.curyID,
			Max<CuryARHistory.finPeriodID
			>>>>>>>>))]
		[PXCacheName(Messages.ARLatestHistory)]
		public partial class ARLatestHistory : PX.Data.IBqlTable
		{
			#region BranchID
			public abstract class branchID : PX.Data.IBqlField
			{
			}
			protected Int32? _BranchID;
			[PXDBInt(IsKey = true, BqlField = typeof(CuryARHistory.branchID))]
			[PXSelector(typeof(Branch.branchID), SubstituteKey = typeof(Branch.branchCD))]
			public virtual Int32? BranchID
			{
				get
				{
					return this._BranchID;
				}
				set
				{
					this._BranchID = value;
				}
			}
			#endregion
			#region CustomerID
			public abstract class customerID : PX.Data.IBqlField
			{
			}
			protected Int32? _CustomerID;
			[PXDBInt(IsKey = true, BqlField = typeof(CuryARHistory.customerID))]
			public virtual Int32? CustomerID
			{
				get
				{
					return this._CustomerID;
				}
				set
				{
					this._CustomerID = value;
				}
			}
			#endregion
			#region AccountID
			public abstract class accountID : PX.Data.IBqlField
			{
			}
			protected Int32? _AccountID;
			[PXDBInt(IsKey = true, BqlField = typeof(CuryARHistory.accountID))]
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
			#region SubID
			public abstract class subID : PX.Data.IBqlField
			{
			}
			protected Int32? _SubID;
			[PXDBInt(IsKey = true, BqlField = typeof(CuryARHistory.subID))]
			public virtual Int32? SubID
			{
				get
				{
					return this._SubID;
				}
				set
				{
					this._SubID = value;
				}
			}
			#endregion
			#region CuryID
			public abstract class curyID : PX.Data.IBqlField
			{
			}
			protected String _CuryID;
			[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof(CuryARHistory.curyID))]
			public virtual String CuryID
			{
				get
				{
					return this._CuryID;
				}
				set
				{
					this._CuryID = value;
				}
			}
			#endregion
			#region LastActivityPeriod
			public abstract class lastActivityPeriod : PX.Data.IBqlField
			{
			}
			protected String _LastActivityPeriod;
			[GL.FinPeriodID(BqlField = typeof(CuryARHistory.finPeriodID))]
			public virtual String LastActivityPeriod
			{
				get
				{
					return this._LastActivityPeriod;
				}
				set
				{
					this._LastActivityPeriod = value;
				}
			}
			#endregion
		}
		[Serializable]
		[PXHidden]
		public sealed class ARH : CuryARHistory
		{
			#region BranchID
			public new abstract class branchID : PX.Data.IBqlField { }
			#endregion
			#region AccountID
			public new abstract class accountID : PX.Data.IBqlField { }
			#endregion
			#region SubID
			public abstract new class subID : PX.Data.IBqlField { }
			#endregion
			#region FinPeriodID
			public new abstract class finPeriodID : PX.Data.IBqlField { }
			#endregion
			#region CustomerID
			public new abstract class customerID : PX.Data.IBqlField { }
			#endregion
			#region CuryID
			public new abstract class curyID : PX.Data.IBqlField { }
			#endregion
			#region FinBegBalance
			public new abstract class finBegBalance : PX.Data.IBqlField { }
			#endregion
			#region FinYtdBalance
			public new abstract class finYtdBalance : PX.Data.IBqlField { }
			#endregion

			#region TranBegBalance
			public new abstract class tranBegBalance : PX.Data.IBqlField { }
			#endregion
			#region TranYtdBalance
			public new abstract class tranYtdBalance : PX.Data.IBqlField { }
			#endregion

			#region CuryFinBegBalance
			public new abstract class curyFinBegBalance : PX.Data.IBqlField { }
			#endregion
			#region CuryFinYtdBalance
			public new abstract class curyFinYtdBalance : PX.Data.IBqlField { }
			#endregion

			#region CuryTranBegBalance
			public new abstract class curyTranBegBalance : PX.Data.IBqlField { }
			#endregion
			#region CuryTranYtdBalance
			public new abstract class curyTranYtdBalance : PX.Data.IBqlField { }
			#endregion

			#region CuryTranPtdDeposits
			public abstract new class curyTranPtdDeposits : PX.Data.IBqlField { }
			#endregion
			#region CuryTranYtdDeposits
			public abstract new class curyTranYtdDeposits : PX.Data.IBqlField { }
			#endregion
			#region TranPtdDeposits
			public abstract new class tranPtdDeposits : PX.Data.IBqlField { }
			#endregion
			#region TranYtdDeposits
			public abstract new class tranYtdDeposits : PX.Data.IBqlField { }
			#endregion

			#region CuryFinPtdDeposits
			public abstract new class curyFinPtdDeposits : PX.Data.IBqlField { }
			#endregion
			#region CuryFinYtdDeposits
			public abstract new class curyFinYtdDeposits : PX.Data.IBqlField { }
			#endregion
			#region FinPtdDeposits
			public abstract new class finPtdDeposits : PX.Data.IBqlField { }
			#endregion
			#region FinYtdDeposits
			public abstract new class finYtdDeposits : PX.Data.IBqlField { }
			#endregion


			#region FinPtdRevaluated
			public abstract class finPtdRevaluated : PX.Data.IBqlField { }
			#endregion

		}

		private sealed class decimalZero : Constant<decimal>
		{
			public decimalZero()
				: base(0m)
			{
			}
		}

		#region Public Membsers
		public PXFilter<ARHistoryFilter> Filter;
        [PXFilterable]
		public PXSelect<ARHistoryResult> History;
        [PXVirtualDAC]
        public PXFilter<ARHistorySummary> Summary;
        public PXSetup<ARSetup> ARSetup;
		public PXSetup<Company> Company;
		#endregion
		
		#region Ctor + Overrides

	    public ARCustomerBalanceEnq()
	    {
	        ARSetup setup = ARSetup.Current;
	        Company company = this.Company.Current;
	        this.History.Cache.AllowDelete = false;
	        this.History.Cache.AllowInsert = false;
	        this.History.Cache.AllowUpdate = false;
	        this.reports.MenuAutoOpen = true;
	        this.reports.AddMenuAction(this.aRBalanceByCustomerReport);
	        this.reports.AddMenuAction(this.customerHistoryReport);
	        this.reports.AddMenuAction(this.aRAgedPastDueReport);
	        this.reports.AddMenuAction(this.aRAgedOutstandingReport);
	    }
		public override bool IsDirty
		{
			get
			{
				return false;
			}
		}
		#endregion

		#region Actions

		public PXCancel<ARHistoryFilter> Cancel;
		public PXAction<ARHistoryFilter> viewDetails;
		public PXAction<ARHistoryFilter> previousPeriod;
		public PXAction<ARHistoryFilter> nextPeriod;
		public PXAction<ARHistoryFilter> aRBalanceByCustomerReport;
		public PXAction<ARHistoryFilter> customerHistoryReport;
		public PXAction<ARHistoryFilter> aRAgedPastDueReport;
		public PXAction<ARHistoryFilter> aRAgedOutstandingReport;
		public PXAction<ARHistoryFilter> reports;

		
		[PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXPreviousButton]
		public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
		{
			ARHistoryFilter filter = Filter.Current as ARHistoryFilter;
			FinPeriod nextperiod = FiscalPeriodUtils.FindPrevPeriod(this, filter.Period, true);
			if (nextperiod != null)
			{
				filter.Period = nextperiod.FinPeriodID;
			}
			return adapter.Get();
		}

		[PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXNextButton]
		public virtual IEnumerable NextPeriod(PXAdapter adapter)
		{
			ARHistoryFilter filter = Filter.Current as ARHistoryFilter;
			FinPeriod nextperiod = FiscalPeriodUtils.FindNextPeriod(this, filter.Period, true);
			if (nextperiod != null)
			{
				filter.Period = nextperiod.FinPeriodID;
			}
			return adapter.Get();
		}

		[PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXEditDetailButton]
		public virtual IEnumerable ViewDetails(PXAdapter adapter)
		{
			if (this.History.Current != null && this.Filter.Current != null)
			{
				ARHistoryResult res = this.History.Current;
				ARHistoryFilter currentFilter = this.Filter.Current;
				ARDocumentEnq graph = PXGraph.CreateInstance<ARDocumentEnq>();

				ARDocumentEnq.ARDocumentFilter filter = graph.Filter.Current;
				Copy(filter, currentFilter);
				filter.CustomerID = res.CustomerID;				
				filter.BalanceSummary = null;
				graph.Filter.Update(filter);
				filter = graph.Filter.Select();
				throw new PXRedirectRequiredException(graph, "Customer Details");

			}
			return Filter.Select();
		}

		[PXUIField(DisplayName = "Reports", MapEnableRights = PXCacheRights.Select)]
		[PXButton(SpecialType = PXSpecialButtonType.Report)]
		protected virtual IEnumerable Reports(PXAdapter adapter)
		{
			return adapter.Get();
		}


		[PXUIField(DisplayName = Messages.ARBalanceByCustomerReport, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		public virtual IEnumerable ARBalanceByCustomerReport(PXAdapter adapter)
		{
			ARHistoryFilter filter = Filter.Current;
			ARHistoryResult history = History.Current;

			if (filter != null && history != null)
			{
                CA.Light.Customer customer = PXSelect<CA.Light.Customer, Where<CA.Light.Customer.bAccountID, Equal<Current<ARHistoryResult.customerID>>>>.Select(this);
				Dictionary<string, string> parameters = new Dictionary<string, string>();
				if (!string.IsNullOrEmpty(filter.Period))
				{
					parameters["PeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(filter.Period);
				}
				parameters["CustomerID"] = customer.AcctCD;
				throw new PXReportRequiredException(parameters, "AR632500", PXBaseRedirectException.WindowMode.NewWindow, Messages.ARBalanceByCustomerReport);
			}
			return adapter.Get();
		}


		[PXUIField(DisplayName = Messages.CustomerHistoryReport, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		public virtual IEnumerable CustomerHistoryReport(PXAdapter adapter)
		{
			ARHistoryFilter filter = Filter.Current;
			ARHistoryResult history = History.Current;
			if (filter != null && history != null)
			{
                CA.Light.Customer customer = PXSelect<CA.Light.Customer, Where<CA.Light.Customer.bAccountID, Equal<Current<ARHistoryResult.customerID>>>>.Select(this);
				Dictionary<string, string> parameters = new Dictionary<string, string>();
				if (!string.IsNullOrEmpty(filter.Period))
				{
					parameters["FromPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(filter.Period);
					parameters["ToPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(filter.Period);
				}
				parameters["CustomerID"] = customer.AcctCD;
				throw new PXReportRequiredException(parameters, "AR652000", PXBaseRedirectException.WindowMode.NewWindow , Messages.CustomerHistoryReport);
			}
			return adapter.Get();
		}


		[PXUIField(DisplayName = Messages.ARAgedPastDueReport, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		public virtual IEnumerable ARAgedPastDueReport(PXAdapter adapter)
		{
			ARHistoryResult history = History.Current;
			if (history != null)
			{
                CA.Light.Customer customer = PXSelect<CA.Light.Customer, Where<CA.Light.Customer.bAccountID, Equal<Current<ARHistoryResult.customerID>>>>.Select(this);
				Dictionary<string, string> parameters = new Dictionary<string, string>();
				parameters["CustomerID"] = customer.AcctCD;
				throw new PXReportRequiredException(parameters, "AR631000" ,PXBaseRedirectException.WindowMode.NewWindow, Messages.ARAgedPastDueReport);
			}
			return adapter.Get();
		}


		[PXUIField(DisplayName = Messages.ARAgedOutstandingReport, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		public virtual IEnumerable ARAgedOutstandingReport(PXAdapter adapter)
		{
			ARHistoryResult history = History.Current;
			if (history != null)
			{
                CA.Light.Customer customer = PXSelect<CA.Light.Customer, Where<CA.Light.Customer.bAccountID, Equal<Current<ARHistoryResult.customerID>>>>.Select(this);
				Dictionary<string, string> parameters = new Dictionary<string, string>();
				parameters["CustomerID"] = customer.AcctCD;
				throw new PXReportRequiredException(parameters, "AR631500", PXBaseRedirectException.WindowMode.NewWindow , Messages.ARAgedOutstandingReport);
			}
			return adapter.Get();
		}
        #endregion

        #region Select Overrides	

	    protected virtual IEnumerable history()
	    {
            Summary.Cache.Clear();
            ARHistoryFilter header = Filter.Current;
	        ARHistoryResult[] empty = null;
	        if (header == null)
	        {
	            return empty;
	        }

	        Dictionary<KeyValuePair<int, string>, ARHistoryResult> result;
	        if (header.Period == null)
	        {
	            RetrieveHistory(header, out result);
	        }
	        else
	        {
	            RetrieveHistoryForPeriod(header, out result);
	        }

            Summary.Update(Summary.Current);


            bool anyDoc = result.Count > 0;
	        this.viewDetails.SetEnabled(anyDoc);

            return result.Values;
	    }

        protected virtual IEnumerable summary()
        {
            if (Summary.Current.Calculated == false)
            {
                var summary = Summary.Cache.CreateInstance() as ARHistorySummary;
                summary.ClearSummary();
                Summary.Insert(summary);
                History.Select();
            }

            yield return Summary.Current;
        }

        protected virtual void RetrieveHistory(ARHistoryFilter header, out Dictionary<KeyValuePair<int, string>, ARHistoryResult> result) 
		{
			result = new Dictionary<KeyValuePair<int, string>, ARHistoryResult>();
			bool isCurySelected = string.IsNullOrEmpty(header.CuryID) == false;
			bool splitByCurrency = header.SplitByCurrency ?? false;
			bool useFinancial = (header.ByFinancialPeriod == true);
			
			#region FiscalPeriodUndefined

			List<Type> typesList = new List<Type>
			{
				typeof(Select5<,,>), typeof(ARLatestHistory),
				typeof(LeftJoin<CA.Light.Customer, On<ARLatestHistory.customerID, Equal<CA.Light.Customer.bAccountID>,
						And<Match<CA.Light.Customer, Current<AccessInfo.userName>>>>,
					LeftJoin<Sub, On<ARLatestHistory.subID, Equal<Sub.subID>>,
					LeftJoin<CuryARHistory, On<ARLatestHistory.accountID, Equal<CuryARHistory.accountID>,
						And<ARLatestHistory.branchID, Equal<CuryARHistory.branchID>,
						And<ARLatestHistory.customerID, Equal<CuryARHistory.customerID>,
						And<ARLatestHistory.subID, Equal<CuryARHistory.subID>,
						And<ARLatestHistory.curyID, Equal<CuryARHistory.curyID>,
						And<ARLatestHistory.lastActivityPeriod, Equal<CuryARHistory.finPeriodID>>>>>>>>>>),
				typeof(Aggregate<>)
			};
				
			typesList.AddRange(BqlHelper.GetDecimalFieldsAggregate<CuryARHistory>(this));

			Type typeCustomer = header.IncludeChildAccounts == true
				? typeof(CA.Light.Customer.consolidatingBAccountID)
				: typeof(CA.Light.Customer.bAccountID);

			typesList.AddRange(
				new Type[]
				{
				typeof(GroupBy<,>), typeof(ARLatestHistory.lastActivityPeriod),
				typeof(GroupBy<,>), typeof(ARLatestHistory.curyID),
					typeof(GroupBy<>), typeCustomer
				});

			Type select = BqlCommand.Compose(typesList.ToArray());
            select = BqlCommand.AppendJoin(select, BqlCommand.Compose(typeof(LeftJoin<,>),
                typeof(CA.Light.CustomerMaster), typeof(On<,>), typeof(CA.Light.CustomerMaster.bAccountID), typeof(Equal<>), typeCustomer));

            BqlCommand cmd = BqlCommand.CreateInstance(select);
			PXView view = new PXView(this, false, cmd);

            view.WhereAnd<Where<CA.Light.Customer.bAccountID, IsNotNull>>();
            view.WhereAnd<Where<CA.Light.CustomerMaster.bAccountID, IsNotNull>>();

            if (header.BranchID != null)
			{
				view.WhereAnd<Where<ARLatestHistory.branchID, Equal<Current<ARHistoryFilter.branchID>>>>();
			}

			if (header.ARAcctID != null)
			{
				view.WhereAnd<Where<ARLatestHistory.accountID, Equal<Current<ARHistoryFilter.aRAcctID>>>>();
			}

			if (header.ARSubID != null)
			{
				view.WhereAnd<Where<ARLatestHistory.subID, Equal<Current<ARHistoryFilter.aRSubID>>>>();
			}

			if (isCurySelected)
			{
				view.WhereAnd<Where<ARLatestHistory.curyID, Equal<Current<ARHistoryFilter.curyID>>>>();
			}

			AppendCommonWhereFilters(view, header);

            var summary = Summary.Cache.CreateInstance() as ARHistorySummary;
            summary.ClearSummary();

            foreach (PXResult<ARLatestHistory, CA.Light.Customer, Sub, CuryARHistory, CA.Light.CustomerMaster> record in view.SelectMulti())
			{
                CA.Light.CustomerMaster customer = record;
				CuryARHistory history = record;
				ARHistoryResult res = new ARHistoryResult();
				CopyFrom(res, customer);
				CopyFrom(res, history, useFinancial);
				res.FinPeriodID = history.FinPeriodID;
				string keyCuryID = (isCurySelected || splitByCurrency) ? history.CuryID : this.Company.Current.BaseCuryID;

				if (!isCurySelected && !splitByCurrency)
				{
					res.CopyValueToCuryValue(this.Company.Current.BaseCuryID);
					res.RecalculateEndBalance();
				}

                if (IsExcludedByZeroBalances(header.ShowWithBalanceOnly, res))
                {
                    continue;
                }

                KeyValuePair<int, string> key = new KeyValuePair<int, string>(customer.BAccountID.Value, keyCuryID);
				if (result.ContainsKey(key))
				{
					AggregateLatest(result[key], res);
				}
				else
				{
					result[key] = res;
				}

                Aggregate(summary, res);
			}

            summary.Calculated = true;
            Summary.Update(summary);
            #endregion
        }

		protected virtual void RetrieveHistoryForPeriod(ARHistoryFilter header, out Dictionary<KeyValuePair<int, string>, ARHistoryResult> result) 
		{
			result = new Dictionary<KeyValuePair<int, string>, ARHistoryResult>();
			bool isCurySelected = string.IsNullOrEmpty(header.CuryID) == false;
			bool splitByCurrency = header.SplitByCurrency ?? false;			
			bool useFinancial = (header.ByFinancialPeriod == true);
			
			#region Specific Fiscal Period

			List<Type> typesList = new List<Type>
			{
				typeof(Select5<,,,>), typeof(ARHistoryByPeriod),
				typeof(LeftJoin<CA.Light.Customer, On<ARHistoryByPeriod.customerID, Equal<CA.Light.Customer.bAccountID>,
						And<Match<CA.Light.Customer, Current<AccessInfo.userName>>>>,
					LeftJoin<Sub, On<ARHistoryByPeriod.subID, Equal<Sub.subID>>,
					LeftJoin<CuryARHistory, On<ARHistoryByPeriod.accountID, Equal<CuryARHistory.accountID>,
						And<ARHistoryByPeriod.branchID, Equal<CuryARHistory.branchID>,
						And<ARHistoryByPeriod.customerID, Equal<CuryARHistory.customerID>,
						And<ARHistoryByPeriod.subID, Equal<CuryARHistory.subID>,
						And<ARHistoryByPeriod.curyID, Equal<CuryARHistory.curyID>,
						And<ARHistoryByPeriod.finPeriodID, Equal<CuryARHistory.finPeriodID>>>>>>>,
					LeftJoin<ARH, On<ARHistoryByPeriod.accountID, Equal<ARH.accountID>,
						And<ARHistoryByPeriod.branchID, Equal<ARH.branchID>,
						And<ARHistoryByPeriod.customerID, Equal<ARH.customerID>,
						And<ARHistoryByPeriod.subID, Equal<ARH.subID>,
						And<ARHistoryByPeriod.curyID, Equal<ARH.curyID>,
						And<ARHistoryByPeriod.lastActivityPeriod, Equal<ARH.finPeriodID>>>>>>>>>>>),
				typeof(Where<ARHistoryByPeriod.finPeriodID, Equal<Current<ARHistoryFilter.period>>>),
				typeof(Aggregate<>)
			};

			typesList.AddRange(BqlHelper.GetDecimalFieldsAggregate<CuryARHistory>(this));
			typesList.AddRange(BqlHelper.GetDecimalFieldsAggregate<ARH>(this));
			
			Type typeCustomer = header.IncludeChildAccounts == true
				? typeof(CA.Light.Customer.consolidatingBAccountID)
				: typeof(CA.Light.Customer.bAccountID);

			typesList.AddRange(
				new Type[]
				{
				typeof(GroupBy<,>), typeof(ARHistoryByPeriod.lastActivityPeriod),
				typeof(GroupBy<,>), typeof(ARHistoryByPeriod.finPeriodID),
				typeof(GroupBy<,>), typeof(ARHistoryByPeriod.curyID),
					typeof(GroupBy<>), typeCustomer
				});

			Type select = BqlCommand.Compose(typesList.ToArray());
            select = BqlCommand.AppendJoin(select, BqlCommand.Compose(typeof(LeftJoin<,>),
                typeof(CA.Light.CustomerMaster), typeof(On<,>), typeof(CA.Light.CustomerMaster.bAccountID), typeof(Equal<>), typeCustomer));

            BqlCommand cmd = BqlCommand.CreateInstance(select);
			PXView view = new PXView(this, false, cmd);

            view.WhereAnd<Where<CA.Light.Customer.bAccountID, IsNotNull>>();
            view.WhereAnd<Where<CA.Light.CustomerMaster.bAccountID, IsNotNull>>();

            if (isCurySelected)
			{
				view.WhereAnd<Where<ARHistoryByPeriod.curyID, Equal<Current<ARHistoryFilter.curyID>>>>();
			}

			if (header.BranchID != null)
			{
				view.WhereAnd<Where<ARHistoryByPeriod.branchID, Equal<Current<ARHistoryFilter.branchID>>>>();
			}

			if (header.ARAcctID != null)
			{
				view.WhereAnd<Where<ARHistoryByPeriod.accountID, Equal<Current<ARHistoryFilter.aRAcctID>>>>();
			}

			if (header.ARSubID != null)
			{
				view.WhereAnd<Where<ARHistoryByPeriod.subID, Equal<Current<ARHistoryFilter.aRSubID>>>>();
			}

			AppendCommonWhereFilters(view, header);

            var summary = Summary.Cache.CreateInstance() as ARHistorySummary;
            summary.ClearSummary();

            foreach (PXResult<ARHistoryByPeriod, CA.Light.Customer, Sub, CuryARHistory, ARH, CA.Light.CustomerMaster> record in view.SelectMulti())
			{
                CA.Light.CustomerMaster customer = record;
				CuryARHistory history = record;
				ARH lastActivity = record;
				ARHistoryByPeriod hstByPeriod = record;
				ARHistoryResult res = new ARHistoryResult();
				CopyFrom(res, customer);
				CopyFrom(res, history, useFinancial);

				res.FinPeriodID = lastActivity.FinPeriodID;
				if (string.IsNullOrEmpty(res.CuryID))
				{
					res.CuryID = hstByPeriod.CuryID;
				}

				string keyCuryID = (isCurySelected || splitByCurrency) ? hstByPeriod.CuryID : this.Company.Current.BaseCuryID;
				KeyValuePair<int, string> key = new KeyValuePair<int, string>(customer.BAccountID.Value, keyCuryID);

				if ((history.FinPeriodID == null) || (history.FinPeriodID != lastActivity.FinPeriodID))
				{
					if (useFinancial)
					{
						res.EndBalance = res.BegBalance = lastActivity.FinYtdBalance ?? Decimal.Zero;
						res.CuryEndBalance = res.CuryBegBalance = lastActivity.CuryFinYtdBalance ?? Decimal.Zero;
						res.DepositsBalance = -lastActivity.FinYtdDeposits ?? Decimal.Zero;
						res.CuryDepositsBalance = -lastActivity.CuryFinYtdDeposits ?? Decimal.Zero;
					}
					else
					{
						res.EndBalance = res.BegBalance = lastActivity.TranYtdBalance ?? Decimal.Zero;
						res.CuryEndBalance = res.CuryBegBalance = lastActivity.CuryTranYtdBalance ?? Decimal.Zero;
						res.CuryDepositsBalance = -lastActivity.CuryTranYtdDeposits ?? Decimal.Zero;
						res.DepositsBalance = -lastActivity.TranYtdDeposits ?? Decimal.Zero;
					}
				}

				if ((!isCurySelected) && splitByCurrency == false)
				{
					res.CopyValueToCuryValue(this.Company.Current.BaseCuryID);
				}

                if (IsExcludedByZeroBalances(header.ShowWithBalanceOnly, res))
                {
                    continue;
                }

                if (result.ContainsKey(key))
				{
					var resultRowToAgregate = result[key];

					if (string.CompareOrdinal(resultRowToAgregate.FinPeriodID, res.FinPeriodID) < 0)
					{
						resultRowToAgregate.FinPeriodID = res.FinPeriodID;
					}
					
					Aggregate(resultRowToAgregate, res);
				}
				else
				{
					result[key] = res;
				}

                Aggregate(summary, res);
            }

            summary.Calculated = true;
            Summary.Update(summary);
		    #endregion
        }

		protected virtual void AppendCommonWhereFilters(PXView view, ARHistoryFilter filter)
		{
			if (!SubCDUtils.IsSubCDEmpty(filter.SubCD))
			{
				view.WhereAnd<Where<Sub.subCD, Like<Current<ARHistoryFilter.subCDWildcard>>>>();
			}

			if (filter.CustomerClassID != null)
			{
				view.WhereAnd<Where<CA.Light.Customer.customerClassID, Equal<Current<ARHistoryFilter.customerClassID>>>>();
			}

			if (filter.CustomerID != null)
			{
				view.WhereAnd<Where<CA.Light.Customer.bAccountID, Equal<Current<ARHistoryFilter.customerID>>,
					Or<CA.Light.Customer.consolidatingBAccountID, Equal<Current<ARHistoryFilter.customerID>>,
						And<Current<ARHistoryFilter.includeChildAccounts>, Equal<True>>>>>();
			}

			if (filter.Status != null)
			{
				view.WhereAnd<Where<CA.Light.Customer.status, Equal<Current<ARHistoryFilter.status>>>>();
			}
		}
		#endregion

		#region Event Subscribers

		public virtual void ARHistoryFilter_CuryID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			cache.SetDefaultExt<ARHistoryFilter.splitByCurrency>(e.Row);
		}


		public virtual void ARHistoryFilter_SubCD_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}
		public virtual void ARHistoryFilter_ARAcctID_ExceptionHandling(PXCache cache, PXExceptionHandlingEventArgs e)
		{
			ARHistoryFilter header = e.Row as ARHistoryFilter;
			if (header != null)
			{
				e.Cancel = true;
				header.ARAcctID = null;
			}
		}
		public virtual void ARHistoryFilter_ARSubID_ExceptionHandling(PXCache cache, PXExceptionHandlingEventArgs e)
		{
			ARHistoryFilter header = e.Row as ARHistoryFilter;
			if (header != null)
			{
				e.Cancel = true;
				header.ARSubID = null;
			}
		}
		public virtual void ARHistoryFilter_CuryID_ExceptionHandling(PXCache cache, PXExceptionHandlingEventArgs e)
		{
			ARHistoryFilter header = e.Row as ARHistoryFilter;
			if (header != null)
			{
				e.Cancel = true;
				header.CuryID = null;
			}
		}
		public virtual void ARHistoryFilter_Period_ExceptionHandling(PXCache cache, PXExceptionHandlingEventArgs e)
		{
			ARHistoryFilter header = e.Row as ARHistoryFilter;
			if (header != null)
			{
				e.Cancel = true;
				header.Period = null;
			}
		}
		public virtual void ARHistoryFilter_CustomerClassID_ExceptionHandling(PXCache cache, PXExceptionHandlingEventArgs e)
		{
			ARHistoryFilter header = e.Row as ARHistoryFilter;
			if (header != null)
			{
				e.Cancel = true;
				header.CustomerClassID = null;
			}
		}
		protected virtual void ARHistoryFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			ARHistoryFilter row = e.Row as ARHistoryFilter;
			if (row == null) return;		

			Company company = this.Company.Current;
			bool mcFeatureInstalled = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
			PXUIFieldAttribute.SetVisible<ARHistoryFilter.showWithBalanceOnly>(sender, row, true);
			PXUIFieldAttribute.SetVisible<ARHistoryFilter.byFinancialPeriod>(sender, row, true);
			PXUIFieldAttribute.SetVisible<ARHistoryFilter.curyID>(sender, row, mcFeatureInstalled);

			PXUIFieldAttribute.SetVisible<ARHistoryFilter.includeChildAccounts>(sender, row, PXAccess.FeatureInstalled<CS.FeaturesSet.parentChildAccount>());

			bool isCurySelected = string.IsNullOrEmpty(row.CuryID) == false;
			bool isBaseCurySelected = string.IsNullOrEmpty(row.CuryID) == false && (company.BaseCuryID == row.CuryID);
			bool splitByCurrency = (row.SplitByCurrency ?? false);

			PXUIFieldAttribute.SetVisible<ARHistoryFilter.splitByCurrency>(sender, row, mcFeatureInstalled && !isCurySelected);
			PXUIFieldAttribute.SetEnabled<ARHistoryFilter.splitByCurrency>(sender, row, mcFeatureInstalled && !isCurySelected);

            PXUIFieldAttribute.SetRequired<ARHistoryFilter.branchID>(sender, false);

			PXCache detailCache = this.History.Cache;
			bool hideCuryColumns = (!mcFeatureInstalled) || (isBaseCurySelected) || (!isCurySelected && !splitByCurrency);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyID>(this.History.Cache, null, mcFeatureInstalled && (isCurySelected || splitByCurrency));
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyBalance>(detailCache, null, !hideCuryColumns);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyPayments>(detailCache, null, !hideCuryColumns);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curySales>(detailCache, null, !hideCuryColumns);			
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyDiscount>(detailCache, null, !hideCuryColumns);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyCrAdjustments>(detailCache, null, !hideCuryColumns);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyDrAdjustments>(detailCache, null, !hideCuryColumns);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyDeposits>(detailCache, null, !hideCuryColumns);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyDepositsBalance>(detailCache, null, !hideCuryColumns);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyBegBalance>(detailCache, null, !hideCuryColumns);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyEndBalance>(detailCache, null, !hideCuryColumns);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.rGOL>(History.Cache, null, mcFeatureInstalled);

			PXUIFieldAttribute.SetVisible<ARHistoryResult.balance>(detailCache, null, false);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.curyBalance>(detailCache, null, false);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.finPeriodID>(detailCache, null);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.begBalance>(detailCache, null);
			PXUIFieldAttribute.SetVisible<ARHistoryResult.endBalance>(detailCache, null);	
		}

        protected virtual void ARHistorySummary_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
        {
            var row = (ARHistorySummary)e.Row;
            if (row == null)
            {
                return;
            }

            bool isForeignCurrency = string.IsNullOrEmpty(Filter.Current.CuryID) == false && (Company.Current.BaseCuryID != Filter.Current.CuryID);

            PXUIFieldAttribute.SetVisible<ARHistorySummary.curyBalanceSummary>(sender, row, isForeignCurrency);
            PXUIFieldAttribute.SetVisible<ARHistorySummary.curyDepositsSummary>(sender, row, isForeignCurrency);
        }
        #endregion

        #region Utility Functions

        protected virtual string GetLastActivityPeriod(int? aCustomerID)
		{
			PXSelectBase<CuryARHistory> activitySelect = new PXSelect<CuryARHistory, Where<CuryARHistory.customerID, Equal<Required<CuryARHistory.customerID>>>, OrderBy<Desc<CuryARHistory.finPeriodID>>>(this);
			CuryARHistory result = (CuryARHistory)activitySelect.View.SelectSingle(aCustomerID);
			if (result != null)
				return result.FinPeriodID;
			return null;
		}

		protected virtual void CopyFrom(ARHistoryResult aDest, CA.Light.Customer aCustomer)
		{
			aDest.AcctCD = aCustomer.AcctCD;
			aDest.AcctName = aCustomer.AcctName;
			aDest.CuryID = aCustomer.CuryID;
			aDest.CustomerID = aCustomer.BAccountID;
			aDest.NoteID = aCustomer.NoteID;
		}

		protected virtual void CopyFrom(ARHistoryResult aDest, CuryARHistory aHistory, bool aIsFinancial)
		{
			if (aIsFinancial)
			{
				aDest.CuryBegBalance = aHistory.CuryFinBegBalance ?? Decimal.Zero;
				aDest.CurySales = aHistory.CuryFinPtdSales ?? Decimal.Zero;
				aDest.CuryPayments = aHistory.CuryFinPtdPayments ?? Decimal.Zero;
				aDest.CuryDiscount = aHistory.CuryFinPtdDiscounts ?? Decimal.Zero;
				aDest.CuryCrAdjustments = aHistory.CuryFinPtdCrAdjustments ?? Decimal.Zero;
				aDest.CuryDrAdjustments = aHistory.CuryFinPtdDrAdjustments ?? Decimal.Zero;
				aDest.CuryDeposits = aHistory.CuryFinPtdDeposits ?? Decimal.Zero;
				aDest.CuryDepositsBalance = -aHistory.CuryFinYtdDeposits ?? Decimal.Zero;
				aDest.CuryFinCharges = aHistory.CuryFinPtdFinCharges ??  Decimal.Zero;
				
				aDest.BegBalance = aHistory.FinBegBalance ?? Decimal.Zero;
				aDest.Sales = aHistory.FinPtdSales?? Decimal.Zero;
				aDest.Payments = aHistory.FinPtdPayments ?? Decimal.Zero;
				aDest.Discount = aHistory.FinPtdDiscounts ?? Decimal.Zero;
				aDest.RGOL = -aHistory.FinPtdRGOL ?? Decimal.Zero;
				aDest.CrAdjustments = aHistory.FinPtdCrAdjustments ?? Decimal.Zero;
				aDest.DrAdjustments = aHistory.FinPtdDrAdjustments ?? Decimal.Zero;
				aDest.Deposits = aHistory.FinPtdDeposits ?? Decimal.Zero;
				aDest.DepositsBalance = -aHistory.FinYtdDeposits ?? Decimal.Zero;
				aDest.FinCharges = aHistory.FinPtdFinCharges ??  Decimal.Zero;
				aDest.COGS = aHistory.FinPtdCOGS?? Decimal.Zero;
				aDest.FinPtdRevaluated = aHistory.FinPtdRevalued ?? Decimal.Zero;
				aDest.CuryID = aHistory.CuryID;
			}
			else
			{
				aDest.CuryBegBalance = aHistory.CuryTranBegBalance ?? Decimal.Zero;
				aDest.CurySales = aHistory.CuryTranPtdSales ?? Decimal.Zero;
				aDest.CuryPayments = aHistory.CuryTranPtdPayments ?? Decimal.Zero;
				aDest.CuryDiscount = aHistory.CuryTranPtdDiscounts ?? Decimal.Zero;
				aDest.CuryCrAdjustments = aHistory.CuryTranPtdCrAdjustments ?? Decimal.Zero;
				aDest.CuryDrAdjustments = aHistory.CuryTranPtdDrAdjustments ?? Decimal.Zero;
				aDest.CuryDeposits = aHistory.CuryTranPtdDeposits ?? Decimal.Zero;
				aDest.CuryDepositsBalance = -aHistory.CuryTranYtdDeposits ?? Decimal.Zero;
				aDest.CuryFinCharges = aHistory.CuryTranPtdFinCharges ?? Decimal.Zero;

				aDest.BegBalance = aHistory.TranBegBalance ?? Decimal.Zero;
				aDest.Sales = aHistory.TranPtdSales ?? Decimal.Zero;
				aDest.Payments = aHistory.TranPtdPayments ?? Decimal.Zero;
				aDest.Discount = aHistory.TranPtdDiscounts ?? Decimal.Zero;
				aDest.RGOL = -aHistory.TranPtdRGOL ?? Decimal.Zero;
				aDest.CrAdjustments = aHistory.TranPtdCrAdjustments ?? Decimal.Zero;
				aDest.DrAdjustments = aHistory.TranPtdDrAdjustments ?? Decimal.Zero;
				aDest.Deposits = aHistory.TranPtdDeposits ?? Decimal.Zero;
				aDest.DepositsBalance = -aHistory.TranYtdDeposits ?? Decimal.Zero;
				aDest.FinCharges = aHistory.TranPtdFinCharges ?? Decimal.Zero;
				aDest.COGS = aHistory.TranPtdCOGS ?? Decimal.Zero;
				aDest.FinPtdRevaluated = aHistory.FinPtdRevalued ?? Decimal.Zero;
			
				aDest.CuryID = aHistory.CuryID;
			}
			aDest.RecalculateEndBalance();
		}
		protected virtual void CopyFrom(ARHistoryResult aDest, CuryARHistory aHistory, bool aUseCurrency, bool aIsFinancial)
		{
			if (aIsFinancial)
			{
				if (aUseCurrency)
				{
					aDest.Sales = aHistory.CuryFinPtdSales ?? 0m;
					aDest.Payments = aHistory.CuryFinPtdPayments ?? 0m;
					aDest.Discount = aHistory.CuryFinPtdDiscounts ?? 0m;
					aDest.RGOL = 0m;
					aDest.CrAdjustments = aHistory.CuryFinPtdCrAdjustments ?? 0m;
					aDest.DrAdjustments = aHistory.CuryFinPtdDrAdjustments ?? 0m;
					aDest.BegBalance = aHistory.CuryFinBegBalance ?? 0m;
					aDest.CuryID = aHistory.CuryID;					
					aDest.FinPtdRevaluated = Decimal.Zero;
					aDest.Deposits = aHistory.CuryFinPtdDeposits ?? Decimal.Zero;
					aDest.DepositsBalance = -aHistory.CuryFinYtdDeposits ?? Decimal.Zero;					
				}
				else
				{
					aDest.Sales = aHistory.FinPtdSales ?? 0m;
					aDest.Payments = aHistory.FinPtdPayments ?? 0m;
					aDest.Discount = aHistory.FinPtdDiscounts ?? 0m;
					aDest.RGOL = -aHistory.FinPtdRGOL ?? 0m;
					aDest.CrAdjustments = aHistory.FinPtdCrAdjustments ?? 0m;
					aDest.DrAdjustments = aHistory.FinPtdDrAdjustments ?? 0m;
					aDest.BegBalance = aHistory.FinBegBalance ?? 0m;
					aDest.COGS = aHistory.FinPtdCOGS?? 0m;
					aDest.FinCharges = aHistory.FinPtdFinCharges ?? 0m;
					aDest.FinPtdRevaluated = aHistory.FinPtdRevalued ?? Decimal.Zero;
					aDest.Deposits = aHistory.FinPtdDeposits ?? Decimal.Zero;
					aDest.DepositsBalance = -aHistory.FinYtdDeposits ?? Decimal.Zero;					
				}
			}
			else
			{
				if (aUseCurrency)
				{
					aDest.Sales = aHistory.CuryTranPtdSales ?? 0m;
					aDest.Payments = aHistory.CuryTranPtdPayments ?? 0m;
					aDest.Discount = aHistory.CuryTranPtdDiscounts ?? 0m;
					aDest.RGOL = 0m;
					aDest.CrAdjustments = aHistory.CuryTranPtdCrAdjustments ?? 0m;
					aDest.DrAdjustments = aHistory.CuryTranPtdDrAdjustments ?? 0m;
					aDest.BegBalance = aHistory.CuryTranBegBalance ?? 0m;
					aDest.CuryID = aHistory.CuryID;
					aDest.FinPtdRevaluated = Decimal.Zero;
					aDest.Deposits = aHistory.CuryTranPtdDeposits ?? Decimal.Zero;
					aDest.DepositsBalance = -aHistory.CuryTranYtdDeposits ?? Decimal.Zero;
					
				}
				else
				{
					aDest.Sales = aHistory.TranPtdSales ?? 0m;
					aDest.Payments = aHistory.TranPtdPayments ?? 0m;
					aDest.Discount = aHistory.TranPtdDiscounts ?? 0m;
					aDest.RGOL = -aHistory.TranPtdRGOL ?? 0m;
					aDest.CrAdjustments = aHistory.TranPtdCrAdjustments ?? 0m;
					aDest.DrAdjustments = aHistory.TranPtdDrAdjustments ?? 0m;
					aDest.BegBalance = aHistory.TranBegBalance ?? 0m;
					aDest.COGS = aHistory.TranPtdCOGS ?? 0m;
					aDest.FinCharges = aHistory.TranPtdFinCharges ?? 0m;
					aDest.FinPtdRevaluated = aHistory.FinPtdRevalued ?? Decimal.Zero;
					aDest.Deposits = aHistory.TranPtdDeposits ?? Decimal.Zero;
					aDest.DepositsBalance = -aHistory.TranYtdDeposits ?? Decimal.Zero;
				}
			}
			aDest.RecalculateEndBalance();
		}
		protected virtual void Aggregate(ARHistoryResult aDest, ARHistoryResult aSrc)
		{
			aDest.CuryBegBalance += aSrc.CuryBegBalance ?? Decimal.Zero;
			aDest.CuryCrAdjustments += aSrc.CuryCrAdjustments ?? Decimal.Zero;
			aDest.CuryDrAdjustments += aSrc.CuryDrAdjustments ?? Decimal.Zero;
			aDest.CuryDiscount += aSrc.CuryDiscount ?? Decimal.Zero;
			aDest.CurySales += aSrc.CurySales ?? Decimal.Zero;
			aDest.CuryPayments += aSrc.CuryPayments ?? Decimal.Zero;
			aDest.CuryFinCharges += aSrc.CuryFinCharges ?? Decimal.Zero;			
			aDest.CuryDeposits += aSrc.CuryDeposits ?? Decimal.Zero;
			aDest.CuryDepositsBalance += aSrc.CuryDepositsBalance ?? Decimal.Zero;

			aDest.BegBalance += aSrc.BegBalance ?? Decimal.Zero;
			aDest.CrAdjustments += aSrc.CrAdjustments ?? Decimal.Zero;
			aDest.DrAdjustments += aSrc.DrAdjustments ?? Decimal.Zero;
			aDest.Discount += aSrc.Discount ?? Decimal.Zero;
			aDest.Sales+= aSrc.Sales ?? Decimal.Zero;
			aDest.Payments += aSrc.Payments ?? Decimal.Zero;
			aDest.FinCharges += aSrc.FinCharges ?? Decimal.Zero;
			aDest.RGOL += aSrc.RGOL ?? Decimal.Zero;
			aDest.FinPtdRevaluated += aSrc.FinPtdRevaluated ?? Decimal.Zero;
			aDest.Deposits += aSrc.Deposits ?? Decimal.Zero;
			aDest.DepositsBalance += aSrc.DepositsBalance ?? Decimal.Zero;
			
			aDest.RecalculateEndBalance();
		}

	    protected virtual void Aggregate(ARHistorySummary aDest, ARHistoryResult aSrc)
        {
            aDest.CuryBalanceSummary += aSrc.CuryEndBalance ?? decimal.Zero;
            aDest.BalanceSummary += aSrc.EndBalance ?? decimal.Zero;

            aDest.RevaluedSummary += aSrc.FinPtdRevaluated ?? decimal.Zero;

            aDest.CuryDepositsSummary += aSrc.CuryDepositsBalance ?? decimal.Zero;
            aDest.DepositsSummary += aSrc.DepositsBalance ?? decimal.Zero;
        }
        protected virtual void AggregateLatest(ARHistoryResult aDest, ARHistoryResult aSrc)
		{
			if (aSrc.FinPeriodID == aDest.FinPeriodID)
			{
				Aggregate(aDest, aSrc);
			}
			else
			{
				if (string.Compare(aSrc.FinPeriodID, aDest.FinPeriodID) < 0)
				{
					//Just update Beg Balance
					aDest.BegBalance += aSrc.EndBalance ?? Decimal.Zero;
					aDest.DepositsBalance += aSrc.DepositsBalance ?? Decimal.Zero;
					aDest.CuryBegBalance += aSrc.CuryEndBalance ?? Decimal.Zero;
					aDest.CuryDepositsBalance += aSrc.CuryDepositsBalance ?? Decimal.Zero;
				}
				else
				{
					//Invert 
					aDest.BegBalance = (aDest.EndBalance ?? Decimal.Zero) + (aSrc.BegBalance ?? Decimal.Zero);
					aDest.CrAdjustments = aSrc.CrAdjustments ?? Decimal.Zero;
					aDest.DrAdjustments = aSrc.DrAdjustments ?? Decimal.Zero;
					aDest.Discount = aSrc.Discount ?? Decimal.Zero;
					aDest.Sales = aSrc.Sales ?? Decimal.Zero;
					aDest.Payments = aSrc.Payments ?? Decimal.Zero;
					aDest.RGOL = aSrc.RGOL ?? Decimal.Zero;
					aDest.FinPeriodID = aSrc.FinPeriodID;
					aDest.FinPtdRevaluated = aSrc.FinPtdRevaluated ?? Decimal.Zero;
					aDest.FinCharges = aSrc.FinCharges ?? Decimal.Zero;
					aDest.Deposits = aSrc.Deposits ?? Decimal.Zero;
					aDest.DepositsBalance = (aDest.DepositsBalance ?? Decimal.Zero) + (aSrc.DepositsBalance ?? Decimal.Zero);

					aDest.CuryBegBalance = (aDest.CuryEndBalance ?? Decimal.Zero) + (aSrc.CuryBegBalance ?? Decimal.Zero);
					aDest.CuryCrAdjustments = aSrc.CuryCrAdjustments ?? Decimal.Zero;
					aDest.CuryDrAdjustments = aSrc.CuryDrAdjustments ?? Decimal.Zero;
					aDest.CuryDiscount = aSrc.CuryDiscount ?? Decimal.Zero;
					aDest.CurySales = aSrc.CurySales ?? Decimal.Zero;
					aDest.CuryPayments = aSrc.CuryPayments ?? Decimal.Zero;
					aDest.CuryFinCharges = aSrc.CuryFinCharges ??Decimal.Zero;
					aDest.CuryDeposits = aSrc.CuryDeposits ?? Decimal.Zero;
					aDest.CuryDepositsBalance = (aDest.CuryDepositsBalance ?? Decimal.Zero) + (aSrc.CuryDepositsBalance ?? Decimal.Zero);					
				}
				aDest.RecalculateEndBalance();
			}
		}

	    protected virtual bool IsExcludedByZeroBalances(bool? showWithBalanceOnly, ARHistoryResult historyResult)
	    {
	        return (showWithBalanceOnly ?? false) 
             && ((historyResult.EndBalance ?? decimal.Zero) == decimal.Zero)
	         && ((historyResult.FinPtdRevaluated ?? decimal.Zero) == decimal.Zero)
	         && ((historyResult.DepositsBalance ?? decimal.Zero) == decimal.Zero)
	         && ((historyResult.CuryEndBalance ?? decimal.Zero) == decimal.Zero)
	         && ((historyResult.CuryDepositsBalance ?? decimal.Zero) == decimal.Zero);
	    }

        public static void Copy(ARDocumentEnq.ARDocumentFilter filter, ARHistoryFilter histFilter)
		{
			filter.BranchID = histFilter.BranchID;
			filter.Period = histFilter.Period;
			filter.SubCD = histFilter.SubCD;
			filter.ARAcctID = histFilter.ARAcctID;
			filter.ARSubID = histFilter.ARSubID;
			filter.CuryID = histFilter.CuryID;
			filter.ByFinancialPeriod = histFilter.ByFinancialPeriod;
			filter.IncludeChildAccounts = histFilter.IncludeChildAccounts;
		}
		public static void Copy(ARHistoryFilter histFilter, ARDocumentEnq.ARDocumentFilter filter)
		{
			histFilter.BranchID = filter.BranchID;
			histFilter.CustomerID = filter.CustomerID;
			histFilter.Period = filter.Period;
			histFilter.SubCD = filter.SubCD;
			histFilter.ARAcctID = filter.ARAcctID;
			histFilter.ARSubID = filter.ARSubID;
			histFilter.CuryID = filter.CuryID;
			histFilter.ByFinancialPeriod = filter.ByFinancialPeriod;
			histFilter.IncludeChildAccounts = filter.IncludeChildAccounts;
		}

		#endregion
	}
}
