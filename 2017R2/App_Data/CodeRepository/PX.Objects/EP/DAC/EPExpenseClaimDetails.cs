using System;
using System.Collections.Generic;
using PX.Data;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.SM;
using PX.Objects.Common;

namespace PX.Objects.EP
{
	/// <summary>
	/// The current status of the expense receipt, which is set by the system.
	/// The fields that determine the status of a document are: 
	/// <see cref="EPExpenseClaimDetails.Hold"/>, <see cref="EPExpenseClaimDetails.Released"/>, 
	/// <see cref="EPExpenseClaimDetails.Approved"/>, <see cref="EPExpenseClaimDetails.Rejected"/>.
	/// </summary>
	/// <value>
	/// The field can have one of the following values:
	/// <c>"H"</c>: The receipt is new and has not been submitted for approval yet, or the receipt has been rejected and then put on hold while a user is adjusting it.
	/// <c>"A"</c>: The receipt is ready to be added to a claim after it has been approved (if approval is required for the receipt) 
	/// or after it has been submitted for further processing (if approval is not required).
	/// <c>"O"</c>: The receipt is pending approval.
	/// <c>"C"</c>: The receipt has been rejected.
	/// <c>"R"</c>: The expense claim associated with the receipt has been released.
	/// </value>
	public class EPExpenseClaimDetailsStatus : ILabelProvider
	{
		public const string ApprovedStatus = "A";
		public const string HoldStatus = "H";
		public const string ReleasedStatus = "R";
		public const string OpenStatus = "O";
		public const string RejectedStatus = "C";

		private static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = new ValueLabelList
	{
		{ HoldStatus, Messages.HoldStatus },
		{ ApprovedStatus, Messages.ApprovedStatus },
		{ OpenStatus, Messages.OpenStatus },
		{ ReleasedStatus, Messages.ReleasedStatus },
		{ RejectedStatus, Messages.RejectedStatus  },
	};
		public IEnumerable<ValueLabelPair> ValueLabelPairs => _valueLabelPairs;
		public class ListAttribute : LabelListAttribute
		{
			public ListAttribute() : base(_valueLabelPairs)
			{ }
		}
	}
	/// <summary>
	/// Contains the main properties of the expense receipt document, which is a record reflecting that an employee performed 
	/// a transaction while working for your organization, thus incurring certain expenses.
	/// An expense receipt can be edited on the Expense Receipt (EP301020) form (which corresponds to the <see cref="ExpenseClaimDetailEntry"/> graph).
	/// The list of expense receipts is edited on the Expense Receipts (EP301010) form (which corresponds to the <see cref="ExpenseClaimDetailMaint"/> graph).
	/// </summary>
	[Serializable]
	[PXPrimaryGraph(typeof(ExpenseClaimDetailEntry))]
	[PXCacheName(Messages.ExpenseReceipt)]
	[PXEMailSource]
	public partial class EPExpenseClaimDetails : IBqlTable, PX.Data.EP.IAssign
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		[PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Selected")]
		public bool? Selected
		{
			get;
			set;
		}
		#endregion

		#region ClaimDetailID
		public abstract class claimDetailID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The identifier of the receipt record in the system, which the system assigns automatically when you save a newly entered receipt.
		/// This field is the key field.
		/// </summary>
		[PXDBIdentity(IsKey = true)]
		[PXUIField(DisplayName = "Receipt ID", Visibility = PXUIVisibility.Invisible)]
		[EPExpenceReceiptSelector]
		public virtual int? ClaimDetailID
		{
			get;
			set;
		}
		#endregion

		#region ClaimDetailCD
		public abstract class claimDetailCD : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The user-friendly unique identifier of the receipt.
		/// </summary>
		[PXString(10)]
		[PXUIField(DisplayName = "Receipt ID", Visibility = PXUIVisibility.Visible)]
		public virtual string ClaimDetailCD
		{
			get
			{
				return ClaimDetailID == null || ClaimDetailID < -1 ? null : (string)ClaimDetailID.ToString();
			}
		}
		#endregion

		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The company branch that will incur the expenses. If multiple expense receipts associated with different branches are added to one expense claim, 
		/// the branch specified for the claim on the Financial Details tab of the Expense Claim (EP301000) form (which corresponds to the <see cref="ExpenseClaimEntry"/> graph)
		/// will reimburse the expenses and the branches specified in this box for the receipts will incur the expenses.
		/// </summary>
		[GL.Branch(typeof(EPExpenseClaim.branchID))]
		public virtual int? BranchID
		{
			get;
			set;
		}
		#endregion

		#region RefNbr
		public abstract class refNbr : IBqlField
		{
		}
		/// <summary>
		/// The reference number, which usually matches the number of the original receipt.
		/// </summary>
		[PXDBString(15, IsUnicode = true)]
		[PXDBDefault(typeof(EPExpenseClaim.refNbr), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXParent(typeof(Select<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Current2<refNbr>>>>))]
		[PXUIField(DisplayName = "Expense Claim Ref. Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<EPExpenseClaim.refNbr>), DescriptionField = typeof(EPExpenseClaim.docDesc), ValidateValue = false, DirtyRead = true)]
		[PXFieldDescription]
		public virtual string RefNbr
		{
			get;
			set;
		}
		#endregion

		#region RefNbrNotFiltered
		public abstract class refNbrNotFiltered : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The service field that is used in PXFormula for the <see cref="HoldClaim"/> and <see cref="StatusClaim"/> fields, which don't need restrictions of original RefNbr in a selector.
		/// </summary>
		[PXString(15, IsUnicode = true)]
		[PXFormula(typeof(Current<EPExpenseClaimDetails.refNbr>))]
		[PXVirtualSelector(typeof(Search<EPExpenseClaim.refNbr>), ValidateValue = false)]
		public virtual String RefNbrNotFiltered
		{
			get
			{
				return RefNbr;
			}

			set
			{

			}
		}
		#endregion

		#region EmployeeID
		public abstract class employeeID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The identifier of the <see cref="EPEmployee">employee</see> who is claiming the expenses.
		/// </summary>
		/// <value>
		/// Corresponds to the value of the <see cref="EPEmployee.bAccountID"/> field.
		/// </value>
		[PXDBInt]
		[PXDefault(typeof(EPExpenseClaim.employeeID))]
		[PXSubordinateAndWingmenSelector]
		[PXUIField(DisplayName = "Claimed by", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual int? EmployeeID
		{
			get;
			set;
		}
		#endregion

		#region OwnerID
		public abstract class ownerID : IBqlField
		{
		}
		/// <summary>
		/// The <see cref="EPEmployee">employee</see> responsible 
		/// for the document approval process.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="EPEmployee.PKID"/> field.
		/// </value>
		[PXDBGuid]
		[PX.TM.PXOwnerSelector]
		[PXUIField(DisplayName = "Owner")]
		public virtual Guid? OwnerID
		{
			get;
			set;
		}
		#endregion

		#region WorkgroupID
		public abstract class workgroupID : IBqlField
		{
		}
		/// <summary>
		/// The workgroup that is responsible for the document approval process.
		/// </summary>
		[PXDBInt]
		[PX.TM.PXCompanyTreeSelector]
		[PXUIField(DisplayName = "Workgroup")]
		public virtual int? WorkgroupID
		{
			get;
			set;
		}
		#endregion

		#region Hold
		public abstract class hold : IBqlField
		{
		}
		/// <summary>
		/// Specifies (if set to <c>true</c>) that the expense receipt has the On Hold <see cref="EPExpenseClaimDetails.Status">status</see>,
		/// which means that the receipt can be edited but cannot be added to a claim and released.
		/// </summary>
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(Visible = false)]
		public virtual bool? Hold
		{
			get;
			set;
		}
		#endregion

		#region Approved
		public abstract class approved : IBqlField
		{
		}
		/// <summary>
		/// Specifies (if set to <c>true</c>) that the expense receipt has been approved by a responsible person
		/// and has the Approved <see cref="EPExpenseClaimDetails.Status">status</see>.
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Approved", Enabled = false, Visible = false)]
		public virtual bool? Approved
		{
            get;
            set;
		}
		#endregion

		#region Rejected
		public abstract class rejected : IBqlField
		{
		}
		/// <summary>
		/// Specifies (if set to <c>true</c>) that the expense receipt has been rejected by a responsible person.
		/// When the receipt is rejected, its <see cref="EPExpenseClaimDetails.Status">status</see> changes to Rejected.
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(Visible = false)]
		public bool? Rejected
		{
			get;
			set;
		}
		#endregion

		#region CuryInfoID
		public abstract class curyInfoID : IBqlField
		{
		}
		/// <summary>
		/// The identifier of the <see cref="PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
		/// </summary>
		/// <value>
		/// Is generated automatically and corresponds to the <see cref="PX.Objects.CM.CurrencyInfo.CurrencyInfoID"/> field.
		/// </value>
		[PXDBLong]
		[CurrencyInfo(typeof(EPExpenseClaim.curyInfoID), CuryIDField = "CuryID", CuryDisplayName = "Currency")]
		public virtual long? CuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region CuryID
		public abstract class curyID : IBqlField
		{
		}

        /// <summary>
        /// The code of the <see cref="PX.Objects.CM.Currency">currency</see> of the document.
        /// By default, the receipt currency is the currency specified as the default for the employee.
        /// </summary>
        /// <value>
        /// Defaults to the <see cref="Company.BaseCuryID">company's base currency</see>.
        /// </value>
        [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
        [PXDefault(typeof(Search<Company.baseCuryID>))]
        [PXSelector(typeof(Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>>>))]
		[PXUIField(DisplayName = "Currency")]
		public virtual string CuryID
		{
			get;
			set;
		}
		#endregion

		#region ClaimCuryInfoID
		public abstract class claimCuryInfoID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The code of the <see cref="PX.Objects.CM.Currency">currency</see> of the claim in which the current receipt is included.
		/// </summary>
		/// <value>
		/// Defaults to the <see cref="Company.BaseCuryID">company's base currency</see>.
		/// </value>
		[PXDBLong]
		[CurrencyInfo(typeof(EPExpenseClaim.curyInfoID), CuryIDField = "ClaimCuryID", CuryDisplayName = "Claim Currency")]
		public virtual long? ClaimCuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region ExpenseDate
		public abstract class expenseDate : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The date of the receipt. By default, the current business date is used when a new receipt is created.
		/// </summary>
		[PXDBDate]
		[PXDefault(typeof(Search<EPExpenseClaim.docDate, Where<EPExpenseClaim.refNbr, Equal<Current<EPExpenseClaimDetails.refNbr>>>>))]
		[PXUIField(DisplayName = "Date")]
		public virtual DateTime? ExpenseDate
		{
			get;
			set;
		}
		#endregion

		#region CuryTaxTotal
		public abstract class curyTaxTotal : IBqlField
		{
		}
		/// <summary>
		/// The total amount of taxes associated with the document in the <see cref="CuryID">currency of the document</see>.
		/// </summary>
		[PXDBCurrency(typeof(curyInfoID), typeof(taxTotal))]
		[PXUIField(DisplayName = "Tax Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? CuryTaxTotal
		{
			get;
			set;
		}
		#endregion
		#region TaxTotal
		public abstract class taxTotal : IBqlField
		{
		}
		/// <summary>
		/// The total amount of taxes associated with the document 
		/// in the <see cref="Company.BaseCuryID">base currency of the company</see>.
		/// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? TaxTotal
		{
			get;
			set;
		}
		#endregion
		#region CuryTaxTipTotal
		public abstract class curyTaxTipTotal : IBqlField
		{
		}
		/// <summary>
		/// A fake field for correct working a Tax attribute. Always must be zero because a tip is tax exempt.
		/// The total amount of tips taxes associated with the document in the <see cref="CuryID">currency of the document</see>.
		/// </summary>
		[PXCurrency(typeof(curyInfoID), typeof(taxTipTotal))]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? CuryTaxTipTotal
		{
			get;
			set;
		}
		#endregion
		#region TaxTipTotal
		public abstract class taxTipTotal : IBqlField
		{
		}
		/// <summary>
		/// A fake field for correct working a Tax attribute. Always must be zero because a tip is tax exempt. 
		/// The total amount of tips taxes associated with the document 
		/// in the <see cref="Company.BaseCuryID">base currency of the company</see>.
		/// </summary>
		[PXDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? TaxTipTotal
		{
			get;
			set;
		}
		#endregion
		#region CuryTaxRoundDiff
		public abstract class curyTaxRoundDiff : IBqlField { }

		[PXDBCurrency(typeof(curyInfoID), typeof(taxRoundDiff), BaseCalc = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Discrepancy", Enabled = false)]
		public decimal? CuryTaxRoundDiff
		{
			get;
			set;
		}
		#endregion
		#region TaxRoundDiff
		public abstract class taxRoundDiff : IBqlField { }

		[PXDBBaseCury()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public decimal? TaxRoundDiff
		{
			get;
			set;
		}
		#endregion

		#region ExpenseRefNbr
		public abstract class expenseRefNbr : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The reference number, which usually matches the number of the original receipt.
		/// </summary>
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Ref. Nbr.")]
		public virtual string ExpenseRefNbr
		{
			get;
			set;
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The <see cref="InventoryItem">non-stock item</see>  of the expense, which determines the financial accounts, 
		/// the default tax category, and the unit of measure used for the receipt.
		/// </summary>
		[PXDefault]
		[Inventory(DisplayName = "Expense Item")]
		[PXRestrictor(typeof(Where<InventoryItem.itemType, Equal<INItemTypes.expenseItem>>), Messages.InventoryItemIsType)]
		[PXForeignReference(typeof(Field<inventoryID>.IsRelatedTo<InventoryItem.inventoryID>))]
		public virtual int? InventoryID
		{
			get;
			set;
		}
		#endregion

		#region TaxZoneID
		public abstract class taxZoneID : PX.Data.IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Tax Zone", Required = false)]
		[PXDefault(typeof(Coalesce<Search<EPEmployee.receiptAndClaimTaxZoneID,
										Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>>,
									Search2<Location.vTaxZoneID,
										RightJoin<EPEmployee, On<EPEmployee.defLocationID, Equal<Location.locationID>>>,
										Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXSelector(typeof(TaxZone.taxZoneID), DescriptionField = typeof(TaxZone.descr), Filterable = true)]
		public virtual String TaxZoneID
		{
			get;
			set;
		}
		#endregion
		#region TaxCalcMode
		public abstract class taxCalcMode : IBqlField { }

		/// <summary>
		/// The tax calculation mode, which defines which amounts (tax-inclusive or tax-exclusive) 
		/// should be entered in the detail lines of a document. 
		/// This field is displayed only if the <see cref="FeaturesSet.NetGrossEntryMode"/> field is set to <c>true</c>.
		/// </summary>
		/// <value>
		/// The field can have one of the following values:
		/// <c>"T"</c> (Tax Settings): The tax amount for the document is calculated according to the settings of the applicable tax or taxes.
		/// <c>"G"</c> (Gross): The amount in the document detail line includes a tax or taxes.
		/// <c>"N"</c> (Net): The amount in the document detail line does not include taxes.
		/// </value>
		[PXDBString(1, IsFixed = true)]
        [PXDefault(TaxCalculationMode.TaxSetting)]
        [TaxCalculationMode.List]
		[PXUIField(DisplayName = "Tax Calculation Mode")]
		public virtual string TaxCalcMode
		{
			get;
			set;
		}
		#endregion
		#region TaxCategoryID
		public abstract class taxCategoryID : IBqlField
		{
		}
		/// <summary>
		/// The tax category associated with the expense item.
		/// </summary>
		/// <value>
		/// Corresponds to the value of the <see cref="TaxCategory.TaxCategoryID"/> field.
		/// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Tax Category", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(TaxCategory.taxCategoryID), DescriptionField = typeof(TaxCategory.descr))]
		[PXRestrictor(typeof(Where<TaxCategory.active, Equal<True>>), TX.Messages.InactiveTaxCategory, typeof(TaxCategory.taxCategoryID))]
		[PXFormula(typeof(Selector<inventoryID, InventoryItem.taxCategoryID>))]
		public virtual string TaxCategoryID
		{
			get;
			set;
		}
        #endregion
        #region HasWithHoldTax
        public abstract class hasWithHoldTax : IBqlField { }
        [PXBool]
        [RestrictWithholdingTaxCalcMode(typeof(taxZoneID), typeof(taxCalcMode))]
        public virtual bool? HasWithHoldTax
        {
            get;
            set;
        }
        #endregion
        #region HasUseTax
        public abstract class hasUseTax : IBqlField { }
        [PXBool]
        [RestrictUseTaxCalcMode(typeof(taxZoneID), typeof(taxCalcMode))]
        public virtual bool? HasUseTax
        {
            get;
            set;
        }
        #endregion

        #region UOM
        public abstract class uOM : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The <see cref="INUnit">unit of measure</see> of the expense item.
		/// </summary>
		[PXDefault]
		[INUnit(typeof(EPExpenseClaimDetails.inventoryID), DisplayName = "UOM")]
		[PXUIEnabled(typeof(Where<inventoryID, IsNotNull, And<FeatureInstalled<FeaturesSet.multipleUnitMeasure>>>))]
		[PXFormula(typeof(Switch<Case<Where<inventoryID, IsNull>, Null>, Selector<inventoryID, InventoryItem.purchaseUnit>>))]
		public virtual string UOM
		{
			get;
			set;
		}
		#endregion
		#region CuryTipAmt
		public abstract class curyTipAmt : IBqlField
		{
		}
		[PXDBCurrency(typeof(EPExpenseClaimDetails.curyInfoID), typeof(EPExpenseClaimDetails.tipAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tip Amount")]
		[EPTaxTip]
		public virtual decimal? CuryTipAmt
		{
			get;
			set;
		}
		#endregion
		#region TipAmt
		public abstract class tipAmt : IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Original Tip Amount")]
		public virtual decimal? TipAmt
		{
			get;
			set;
		}
		#endregion
		#region TaxTipCategoryID
		public abstract class taxTipCategoryID : IBqlField
		{
		}
		/// <summary>
		/// The tax category associated with the tip item.
		/// </summary>
		/// <value>
		/// Corresponds to the value of the <see cref="TaxCategory.TaxCategoryID"/> field.
		/// </value>
		[PXDBString(10, IsUnicode = true)]
		public virtual string TaxTipCategoryID
		{
			get;
			set;
		}
		#endregion
		#region Qty
		public abstract class qty : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The quantity of the expense item that the employee purchased according to the receipt. 
		/// The quantity is expressed in the <see cref="INUnit">unit of measure</see> specified 
		/// for the selected expense <see cref="InventoryItem">non-stock item</see>.
		/// </summary>
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "1.0")]
		[PXUIField(DisplayName = "Quantity", Visibility = PXUIVisibility.Visible)]
		[PXUIVerify(typeof(Where<qty, NotEqual<decimal0>, Or<Selector<contractID, Contract.nonProject>, Equal<True>>>), PXErrorLevel.Error, Messages.ValueShouldBeNonZero)]
		public virtual decimal? Qty
		{
			get;
			set;
		}
		#endregion
		#region CuryUnitCost
		public abstract class curyUnitCost : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The cost of one unit of the expense item in the <see cref="CuryID">currency of the document</see>. 
		/// If a standard cost is specified for the expense <see cref="InventoryItem">non-stock item</see>, the standard cost is used as the default unit cost.
		/// </summary>
		[PXDBCurrency(typeof(Search<CommonSetup.decPlPrcCst>), typeof(EPExpenseClaimDetails.curyInfoID), typeof(EPExpenseClaimDetails.unitCost))]
		[PXUIField(DisplayName = "Unit Cost", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryUnitCost
		{
			get;
			set;
		}
		#endregion
		#region UnitCost
		public abstract class unitCost : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The cost of one unit of the expense item in the <see cref = "Company.BaseCuryID" />base currency of the company</see>. 
		/// If a standard cost is specified for the expense <see cref="InventoryItem">non-stock item</see>, the standard cost is used as the default unit cost.
		/// </summary>
		[PXDBPriceCost]
		[PXDefault(typeof(Search<INItemCost.lastCost, Where<INItemCost.inventoryID, Equal<Current<EPExpenseClaimDetails.inventoryID>>>>))]
		public virtual decimal? UnitCost
		{
			get;
			set;
		}
		#endregion
		#region CuryEmployeePart
		public abstract class curyEmployeePart : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The part of the total amount that will not be paid back to the employee in the <see cref="CuryID">currency of the document</see>.
		/// </summary>
		[PXDBCurrency(typeof(EPExpenseClaimDetails.curyInfoID), typeof(EPExpenseClaimDetails.employeePart), MinValue = 0)]
		[PXUIField(DisplayName = "Employee Part")]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIEnabled(typeof(Where<curyExtCost, GreaterEqual<decimal0>>))]
		[PXFormula(typeof(Switch<Case<Where<curyExtCost, Less<decimal0>>, decimal0>, curyEmployeePart>))]
		[PXUIVerify(
			typeof(Where<curyEmployeePart, Equal<decimal0>,
						Or<curyEmployeePart, Less<decimal0>,
						And<curyEmployeePart, GreaterEqual<curyExtCost>,
						Or<curyEmployeePart, Greater<decimal0>,
						And<curyEmployeePart, LessEqual<curyExtCost>>>>>>), PXErrorLevel.Error, Messages.EmployeePartExceed)]
		[PXUIVerify(
			typeof(Where<curyEmployeePart, Equal<decimal0>,
						Or<curyEmployeePart, Greater<decimal0>,
						And<curyExtCost, Greater<decimal0>,
						Or<curyEmployeePart, Less<decimal0>,
						And<curyExtCost, Less<decimal0>>>>>>), PXErrorLevel.Error, Messages.EmployeePartSign)]
		public virtual decimal? CuryEmployeePart
		{
			get;
			set;
		}
		#endregion
		#region EmployeePart
		public abstract class employeePart : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The part of the total amount that will not be paid back to the employee in the <see cref = "Company.BaseCuryID" />base currency of the company</see>. 
		/// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Original Employee Part")]
		public virtual decimal? EmployeePart
		{
			get;
			set;
		}
		#endregion
		#region CuryExtCost
		public abstract class curyExtCost : IBqlField
		{
		}
		/// <summary>
		/// The total amount of the receipt in the <see cref="CuryID">currency of the document</see>.
		/// </summary>
		[PXDBCurrency(typeof(curyInfoID), typeof(extCost))]
		[PXUIField(DisplayName = "Amount")]
		[PXFormula(typeof(Mult<qty, curyUnitCost>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryExtCost
		{
			get;
			set;
		}
		#endregion
		#region ExtCost
		public abstract class extCost : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The total amount of the receipt in the <see cref = "Company.BaseCuryID" />base currency of the company</see>. 
		/// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Original Total Amount")]
		public virtual decimal? ExtCost
		{
			get;
			set;
		}
		#endregion
		#region CuryTaxAmt
		public abstract class curyTaxAmt : IBqlField
		{
		}
		[PXDBCurrency(typeof(curyInfoID), typeof(taxAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
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
		public virtual decimal? TaxAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryTaxableAmtFromTax
		public abstract class curyTaxableAmtFromTax : IBqlField
		{
		}
		[PXDBCurrency(typeof(curyInfoID), typeof(taxableAmtFromTax))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryTaxableAmtFromTax
		{
			get;
			set;
		}
		#endregion
		#region TaxableAmtFromTax
		public abstract class taxableAmtFromTax : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? TaxableAmtFromTax
		{
			get;
			set;
		}
		#endregion
		#region CuryTaxableAmt
		public abstract class curyTaxableAmt : IBqlField
		{
		}
		[PXDBCurrency(typeof(curyInfoID), typeof(taxableAmt))]
		[PXFormula(typeof(Sub<curyExtCost, curyEmployeePart>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
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
		public virtual decimal? TaxableAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryTranAmt
		public abstract class curyTranAmt : IBqlField
		{
        }

        /// <summary>
        /// The amount to be reimbursed to the employee, which is calculated as the difference between the total amount 
        /// and the employee part in the <see cref="CuryID">currency of the document</see>.
        /// </summary>
        [PXDBCurrency(typeof(curyInfoID), typeof(tranAmt))]
		[PXFormula(typeof(Add<curyTaxableAmt, curyTipAmt>))]
		[PXUIField(DisplayName = "Claim Amount", Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryTranAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryTranAmtWithTaxes
		public abstract class curyTranAmtWithTaxes : IBqlField
		{
		}
		[PXDBCurrency(typeof(EPExpenseClaimDetails.curyInfoID), typeof(EPExpenseClaimDetails.tranAmtWithTaxes))]
		[PXFormula(typeof(Add<curyAmountWithTaxes, curyTipAmt>))]
		[PXUIField(DisplayName = "Claim Amount", Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryTranAmtWithTaxes
		{
			get;
			set;
		}
		#endregion
		#region CuryAmountWithTaxes
		public abstract class curyAmountWithTaxes : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryAmountWithTaxes
		{
			get;
			set;
		}
		#endregion
		#region TranAmt
		public abstract class tranAmt : IBqlField
		{
		}
		/// <summary>
		/// The amount to be reimbursed to the employee, which is calculated as the difference between the total amount 
		/// and the employee part in the <see cref = "Company.BaseCuryID" />base currency of the company</see>.
		/// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Original Claim Amount")]
		public virtual decimal? TranAmt
		{
			get;
			set;
		}
		#endregion
		#region TranAmtWithTaxes
		public abstract class tranAmtWithTaxes : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Original Claim Amount with Taxes")]
		public virtual decimal? TranAmtWithTaxes
		{
			get;
			set;
		}
		#endregion
		#region ClaimCuryTranAmt
		public abstract class claimCuryTranAmt : IBqlField
		{
		}
        /// <summary>
        /// The amount to be reimbursed to the employee, which is calculated as the difference between the total amount 
        /// and the employee part in the <see cref = "Company.BaseCuryID" />currency of the claim</see> in which the current receipt is included.
        /// </summary>
        [PXDBCurrency(typeof(claimCuryInfoID), typeof(claimTranAmt))]
        [PXUIField(DisplayName = "Amount in Claim Curr.", Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ClaimCuryTranAmt
		{
			get;
			set;
		}
		#endregion

		#region CuryNetAmount
		public abstract class curyNetAmount : IBqlField
		{
		}
		[PXCurrency(typeof(curyInfoID), typeof(netAmount))]
		[PXFormula(typeof(Sub<curyTranAmtWithTaxes, curyTaxTotal>))]
		[PXUIField(DisplayName = "Net Amount", Enabled = false, Visible = false)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? CuryNetAmount
		{
			get;
			set;
		}
		#endregion
		#region NetAmount
		public abstract class netAmount : IBqlField
		{
		}
		[PXDecimal(4)]
		public virtual decimal? NetAmount
		{
			get;
			set;
		}
		#endregion

		#region ClaimTranAmt
		public abstract class claimTranAmt : IBqlField
		{
		}
		/// <summary>
		/// The amount to be reimbursed to the employee, which is calculated as the difference between the total amount 
		/// and the employee part in the <see cref = "Company.BaseCuryID" />base currency of the company of the claim</see> in in which the current receipt is included.
		/// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount in Claim Original")]
		public virtual decimal? ClaimTranAmt
		{
			get;
			set;
		}
		#endregion

		#region ClaimCuryTranAmtWithTaxes
		public abstract class claimCuryTranAmtWithTaxes : IBqlField
		{
		}

		[PXDBCurrency(typeof(claimCuryInfoID), typeof(claimTranAmtWithTaxes))]
		[PXUIField(DisplayName = "Amount in Claim Curr.", Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUnboundFormula(typeof(Switch<Case<Where<refNbr, IsNotNull>, claimCuryTranAmtWithTaxes>, decimal0>), 
			typeof(SumCalc<EPExpenseClaim.curyDocBal>))]
		public virtual decimal? ClaimCuryTranAmtWithTaxes
		{
			get;
			set;
		}
		#endregion
		#region ClaimTranAmtWithTaxes
		public abstract class claimTranAmtWithTaxes : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount in Claim Original")]
		public virtual decimal? ClaimTranAmtWithTaxes
		{
			get;
			set;
		}
		#endregion

		#region ClaimCuryTaxTotal
		public abstract class claimCuryTaxTotal : IBqlField
		{
		}

		[PXDBCurrency(typeof(claimCuryInfoID), typeof(taxTotal), BaseCalc = false)]
		[PXUIField(DisplayName = "Amount in Claim Curr.", Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUnboundFormula(typeof(Switch<Case<Where<refNbr, IsNotNull>, claimCuryTaxTotal>, decimal0>),
			typeof(SumCalc<EPExpenseClaim.curyTaxTotal>))]
		public virtual decimal? ClaimCuryTaxTotal
		{
			get;
			set;
		}
		#endregion
		#region ClaimTaxTotal
		public abstract class claimTaxTotal : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount in Claim Original")]
		public virtual decimal? ClaimTaxTotal
		{
			get;
			set;
		}
		#endregion

		#region ClaimCuryTaxRoundDiff
		public abstract class claimCuryTaxRoundDiff : IBqlField
		{
		}

		[PXDBCurrency(typeof(claimCuryInfoID), typeof(claimTaxRoundDiff))]
		[PXUnboundFormula(typeof(Switch<Case<Where<refNbr, IsNotNull>, claimCuryTaxRoundDiff>, decimal0>),
			typeof(SumCalc<EPExpenseClaim.curyTaxRoundDiff>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ClaimCuryTaxRoundDiff
		{
			get;
			set;
		}
		#endregion
		#region ClaimTaxRoundDiff
		public abstract class claimTaxRoundDiff : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ClaimTaxRoundDiff
		{
			get;
			set;
		}
		#endregion

		#region ClaimCuryVatExemptTotal
		public abstract class claimCuryVatExemptTotal : IBqlField
		{
		}

		[PXDBCurrency(typeof(claimCuryInfoID), typeof(claimVatExemptTotal))]
		[PXUnboundFormula(typeof(Switch<Case<Where<refNbr, IsNotNull>, claimCuryVatExemptTotal>, decimal0>),
			typeof(SumCalc<EPExpenseClaim.curyVatExemptTotal>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ClaimCuryVatExemptTotal
		{
			get;
			set;
		}
		#endregion
		#region ClaimVatExemptTaxTotal
		public abstract class claimVatExemptTotal : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ClaimVatExemptTotal
		{
			get;
			set;
		}
		#endregion
		#region ClaimCuryVatTaxableTotal
		public abstract class claimCuryVatTaxableTotal : IBqlField
		{
		}

		[PXDBCurrency(typeof(claimCuryInfoID), typeof(claimVatTaxableTotal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUnboundFormula(typeof(Switch<Case<Where<refNbr, IsNotNull>, claimCuryVatTaxableTotal>, decimal0>),
			typeof(SumCalc<EPExpenseClaim.curyVatTaxableTotal>))]
		public virtual decimal? ClaimCuryVatTaxableTotal
		{
			get;
			set;
		}
		#endregion
		#region ClaimVatTaxableTotal
		public abstract class claimVatTaxableTotal : IBqlField
		{
		}

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ClaimVatTaxableTotal
		{
			get;
			set;
		}
		#endregion

		#region CuryVatExemptTotal
		public abstract class curyVatExemptTotal : IBqlField
		{
		}
		
		[PXDBCurrency(typeof(curyInfoID), typeof(vatExemptTotal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryVatExemptTotal
		{
			get;
			set;
		}
		#endregion
		#region VatExemptTaxTotal
		public abstract class vatExemptTotal : IBqlField
		{
		}
		
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? VatExemptTotal
		{
			get;
			set;
		}
		#endregion
		#region CuryVatTaxableTotal
		public abstract class curyVatTaxableTotal : IBqlField
		{
		}
		
		[PXDBCurrency(typeof(curyInfoID), typeof(vatTaxableTotal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryVatTaxableTotal
		{
			get;
			set;
		}
		#endregion
		#region VatTaxableTotal
		public abstract class vatTaxableTotal : IBqlField
		{
		}
		
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? VatTaxableTotal
		{
			get;
			set;
		}
		#endregion

		#region TranDesc
		public abstract class tranDesc : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The description of the expense.
		/// </summary>
		[PXDBString(256, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.Visible)]
		public virtual string TranDesc
		{
			get;
			set;
		}
		#endregion
		#region CustomerID
		public abstract class customerID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The <see cref = "Customer.bAccountID" />customer</see>, which should be specified if the employee incurred the expenses while working for a particular customer. 
		/// If a contract or project is selected, the customer associated with this contract or project is automatically filled in and the box becomes read-only.
		/// </summary>
		[PXDefault(typeof(EPExpenseClaim.customerID), PersistingCheck = PXPersistingCheck.Nothing)]
		[CustomerActive(DescriptionField = typeof(Customer.acctName))]
		[PXUIRequired(typeof(billable))]
		[PXUIEnabled(typeof(Where<contractID, IsNull, Or<Selector<contractID, Contract.nonProject>, Equal<True>, Or<Selector<contractID, Contract.customerID>, IsNull>>>))]
		[PXUIVerify(typeof(Where<refNbr, IsNull,
								Or<Current2<customerID>, IsNull,
								Or<Selector<refNbr, EPExpenseClaim.customerID>, IsNull,
								Or<Current2<customerID>, Equal<Selector<refNbr, EPExpenseClaim.customerID>>,
								Or<billable, NotEqual<True>,
								Or<Selector<contractID, Contract.nonProject>, Equal<False>>>>>>>),
					PXErrorLevel.Warning, Messages.CustomerDoesNotMatch)]
		[PXFormula(typeof(Switch<Case<Where<Selector<contractID, Contract.nonProject>, Equal<False>>, Selector<contractID, Contract.customerID>>, Null>))]
		public virtual int? CustomerID
		{
			get;
			set;
		}
		#endregion
		#region CustomerLocationID
		public abstract class customerLocationID : IBqlField
		{
		}

        /// <summary>
        /// The location of the customer related to the expenses.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="Location.LocationID"/> field.
        /// </value>
        [PXDefault(typeof(Search<Customer.defLocationID, 
            Where<Customer.bAccountID, Equal<Current<EPExpenseClaimDetails.customerID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIRequired(typeof(billable))]
        [LocationID(typeof(Where<Location.bAccountID, Equal<Current2<customerID>>>), DescriptionField = typeof(Location.descr))]
		[PXUIEnabled(typeof(Where<Current2<customerID>, IsNotNull, And<Where<contractID, IsNull, 
			Or<Selector<contractID, Contract.nonProject>, Equal<True>, Or<Selector<contractID, Contract.customerID>, IsNull>>>>>))]
		[PXFormula(typeof(Switch<Case<Where<Current2<customerID>, IsNull>, Null>, Selector<customerID, Customer.defLocationID>>))]
		public virtual int? CustomerLocationID
		{
			get;
			set;
		}
		#endregion
		#region ContractID
		public abstract class contractID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The <see cref = "Contract.ContractID"/> project or contract</see>, which should be specified if the 
		/// employee incurred the expenses while working on a particular project or contract. 
		/// You can select a project or contract only if the Project Management or Contract Management feature, 
		/// respectively, is enabled on the Enable/Disable Features (CS100000) form.
		/// </summary>
		[PXDBInt]
		[PXUIField(DisplayName = "Project/Contract")]
		[PXDimensionSelector(ContractAttribute.DimensionName,
							typeof(Search2<Contract.contractID,
										   LeftJoin<EPEmployeeContract,
													On<EPEmployeeContract.contractID, Equal<Contract.contractID>,
													And<EPEmployeeContract.employeeID, Equal<Current2<employeeID>>>>>,
										   Where<Contract.isTemplate, Equal<False>,
												 And<Contract.isActive, Equal<True>,
												 And<Contract.isCompleted, Equal<False>,
												 And<Where<Contract.nonProject, Equal<True>,
														   Or2<Where<Contract.baseType, Equal<Contract.ContractBaseType>,
														   And<FeatureInstalled<FeaturesSet.contractManagement>>>,
														   Or<Contract.baseType, Equal<PMProject.ProjectBaseType>,
                                                           And2<Where<Contract.visibleInEA, Equal<True>>, 
														   And2<FeatureInstalled<FeaturesSet.projectModule>,
														   And2<Match<Current<AccessInfo.userName>>,
														   And<Where<Contract.restrictToEmployeeList, Equal<False>,
														   Or<EPEmployeeContract.employeeID, IsNotNull>>>>>>>>>>>>>,
										   OrderBy<Desc<Contract.contractCD>>>),
							 typeof(Contract.contractCD),
							 typeof(Contract.contractCD),
							 typeof(Contract.description),
							 typeof(Contract.customerID),
							 typeof(Contract.status),
							 Filterable = true,
							 ValidComboRequired = true,
							 CacheGlobal = true,
							 DescriptionField = typeof(Contract.description))]
		[ProjectDefault(BatchModule.EP, AccountType = typeof(expenseAccountID))]
		public virtual int? ContractID
		{
			get;
			set;
		}
		#endregion


		#region TaskID
		public abstract class taskID : PX.Data.IBqlField
		{
		}

        /// <summary>
        /// The <see cref="PMTask">project task</see> to which the expenses are related. 
        /// This box is available only if the Project Management feature is enabled on the Enable/Disable Features (CS100000) form.
        /// </summary>
        [EPExpenseAllowProjectTaskAttribute(typeof(EPExpenseClaimDetails.contractID), BatchModule.EA, DisplayName = "Project Task")]
        [PXUIEnabled(typeof(Where<contractID, IsNotNull, And<Selector<contractID, Contract.baseType>, Equal<PMProject.ProjectBaseType>>>))]
		[PXFormula(typeof(Switch<Case<Where<contractID, IsNull, Or<Selector<contractID, Contract.baseType>, NotEqual<PMProject.ProjectBaseType>>>, Null>, taskID>))]
		public virtual int? TaskID
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
		[CostCode(typeof(expenseAccountID), typeof(taskID))]
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
		#region Billable
		public abstract class billable : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// Indicates (if set to <c>true</c>) that the customer should be billed for the claim amount. 
		/// You can use the Bill Expense Claims (EP502000) form to bill the customer if no project is specified.
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Billable")]
		public virtual bool? Billable
		{
			get;
			set;
		}
		#endregion
		#region Billed
		public abstract class billed : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// Indicates (if set to <c>true</c>) that current receipt was billed.
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Billed")]
		public virtual bool? Billed
		{
			get;
			set;
		}
		#endregion

		#region Released
		public abstract class released : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// Specifies (if set to <c>true</c>) that the expense receipt was released 
		/// and has the Released <see cref="EPExpenseClaimDetails.Status">status</see>.
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Released", Visible = false)]
		public virtual bool? Released
		{
			get;
			set;
		}
		#endregion
		#region ExpenseAccountID
		public abstract class expenseAccountID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The <see cref="Account">expense account</see> to which the system records the part of the expense to be paid back to the employee.
		/// </summary>
		[PXDefault]
		[PXFormula(typeof(Selector<inventoryID, InventoryItem.cOGSAcctID>))]
		[Account(DisplayName = "Expense Account", Visibility = PXUIVisibility.Visible)]
		[PXUIVerify(typeof(Where<Current2<contractID>, IsNull,
								Or<Current2<expenseAccountID>, IsNull,
								Or<Selector<contractID, Contract.nonProject>, Equal<True>,
								Or<Selector<contractID, Contract.baseType>, Equal<Contract.ContractBaseType>,
								Or<Selector<expenseAccountID, Account.accountGroupID>, IsNotNull>>>>>),
					PXErrorLevel.Error, Messages.AccountGroupIsNotAssignedForAccount, typeof(expenseAccountID))]//, account.AccountCD.Trim())]
		public virtual int? ExpenseAccountID
		{
			get;
			set;
		}
		#endregion
		#region ExpenseSubID
		public abstract class expenseSubID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The corresponding <see cref="Sub">subaccount</see> the system uses to record the part of the expense to be paid back to the employee. 
		/// The segments of the expense subaccount are combined according to the settings specified on the Time and Expenses Preferences (EP101000) form.
		/// </summary>
		[PXDefault]
		[PXFormula(typeof(Default<inventoryID, contractID, taskID, customerLocationID>))]
		[PXFormula(typeof(Default<employeeID>))]
		[SubAccount(typeof(EPExpenseClaimDetails.expenseAccountID), DisplayName = "Expense Sub.", Visibility = PXUIVisibility.Visible)]
		public virtual int? ExpenseSubID
		{
			get;
			set;
		}
		#endregion
		#region SalesAccountID
		public abstract class salesAccountID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The <see cref="Account">sales account</see> to which the system records the part of the amount to charge the customer for. 
		/// If the <see cref="Billable">Billable</see> check box is selected, the sales account specified for the expense non-stock item is filled in by default.
		/// </summary>
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Switch<Case<Where<billable, Equal<True>>, Selector<inventoryID, InventoryItem.salesAcctID>>, Null>))]
		[PXUIRequired(typeof(billable))]
		[PXUIEnabled(typeof(billable))]
		[Account(DisplayName = "Sales Account", Visibility = PXUIVisibility.Visible)]

		public virtual int? SalesAccountID
		{
			get;
			set;
		}
		#endregion
		#region SalesSubID
		public abstract class salesSubID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The corresponding <see cref="Sub">subaccount</see> the system uses to record the amount to charge the customer for. 
		/// If the <see cref="Billable">Billable</see> check box is selected, the sales subaccount specified for the expense non-stock item is filled in by default. 
		/// The segments of the sales subaccount are combined according to the settings specified on the Time and Expenses Preferences (EP101000) form.
		/// </summary>
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<billable, inventoryID, contractID, taskID>))]
		[PXFormula(typeof(Default<employeeID, customerLocationID>))]
		[SubAccount(typeof(EPExpenseClaimDetails.salesAccountID), DisplayName = "Sales Sub.", Visibility = PXUIVisibility.Visible)]
		[PXUIRequired(typeof(billable))]
		[PXUIEnabled(typeof(billable))]
		public virtual int? SalesSubID
		{
			get;
			set;
		}
		#endregion
		#region ARDocType
		public abstract class aRDocType : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The type of AR document created as a result of billing a claim.
		/// </summary>
		/// <value>
		/// The field can have one of the values described in <see cref="ARDocType.ListAttribute"/>.
		/// </value>
		[PXDBString(3, IsFixed = true)]
		[ARDocType.List]
		[PXUIField(DisplayName = "AR Doument Type", Visibility = PXUIVisibility.Visible, Enabled = false, TabOrder = -1)]
		public virtual string ARDocType
		{
			get;
			set;
		}
		#endregion
		#region ARRefNbr
		public abstract class aRRefNbr : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The reference number of the AR document created as a result of billing a claim.
		/// </summary>
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "AR Reference Nbr.", Enabled = false)]
		[PXSelector(typeof(Search<ARInvoice.refNbr, Where<ARInvoice.docType, Equal<Optional<EPExpenseClaimDetails.aRDocType>>>>))]
		public virtual string ARRefNbr
		{
			get;
			set;
		}
		#endregion
		#region APDocType
		public abstract class aPDocType : IBqlField
		{
		}
		/// <summary>
		/// The type of ARPdocument created as a result of releasing a claim.
		/// </summary>
		/// <value>
		/// The field can have one of the values described in <see cref="APDocType.ListAttribute"/>.
		/// </value>
		[PXDBString(3, IsFixed = true)]
		[APDocType.List]
		[PXUIField(DisplayName = "AP Document Type", Visibility = PXUIVisibility.Visible, Enabled = false, TabOrder = -1)]
		public virtual string APDocType
		{
			get;
			set;
		}
		#endregion
		#region APRefNbr
		public abstract class aPRefNbr : IBqlField
		{
		}
		/// <summary>
		/// The reference number of the AR document created as a result of releasing a claim.
		/// </summary>
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "AP Reference Nbr.", Enabled = false, Visible = false)]
		[PXSelector(typeof(Search<APInvoice.refNbr, Where<APInvoice.docType, Equal<Optional<EPExpenseClaimDetails.aPDocType>>>>))]
		public virtual string APRefNbr
		{
			get;
			set;
		}
		#endregion

		#region Status
		public abstract class status : IBqlField
		{
		}
		/// <summary>
		/// The status of the expense receipt.
		/// </summary>
		/// <value>
		/// The field can have one of the values described in <see cref="EPExpenseClaimDetailsStatus.ListAttribute"/>.
		/// </value>
		[PXDBString(1, IsFixed = true)]
		[PXDefault(EPExpenseClaimDetailsStatus.HoldStatus)]
		[PXUIField(DisplayName = "Status", Enabled = false)]
		[EPExpenseClaimDetailsStatus.List()]
		public virtual string Status
		{
			get;
			set;
		}
		#endregion
		#region Status Claim
		public abstract class statusClaim : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The status of the Expense Claim (EP301000) form (which corresponds to the <see cref="ExpenseClaimEntry"/> graph).
		/// </summary>
		/// <value>
		/// The field can have one of the values described in <see cref="EPExpenseClaimStatus.ListAttribute"/>.
		/// </value>
		[PXString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Expense Claim Status", Enabled = false)]
		[EPExpenseClaimStatus.List()]
		[PXFormula(typeof(Switch<Case<Where<EPExpenseClaimDetails.refNbr, IsNotNull>,
							Selector<EPExpenseClaimDetails.refNbrNotFiltered, EPExpenseClaim.status>>,
							Null>))]
		public virtual String StatusClaim
		{
			get;
			set;
		}
		#endregion
		#region Hold Claim
		public abstract class holdClaim : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// Specifies (if set to <c>true</c>) that the Expense Claim (EP301000) (which corresponds to the <see cref="ExpenseClaimEntry"/> graph) 
		/// has the On Hold <see cref="EPExpenseClaim.Status">status</see>,
		/// which means that user can pick another claim, otherwise user cannot change claim.
		/// </summary>
		[PXBool()]
		[PXFormula(typeof(Switch<Case<Where<EPExpenseClaimDetails.refNbr, IsNotNull>,
									Selector<EPExpenseClaimDetails.refNbrNotFiltered, EPExpenseClaim.hold>>,
									True>))]
		[PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(Visible = false)]
		public virtual Boolean? HoldClaim
		{
			get;
			set;
		}

		#endregion
		#region CreatedFromClaim
		public abstract class createdFromClaim : IBqlField
		{ }
		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? CreatedFromClaim
		{
			get;
			set;
		}
		#endregion

		#region SubmitedDate
		public abstract class submitedDate : IBqlField
		{
		}
		[PXDBDateAndTime]
		public DateTime? SubmitedDate
		{
			get;
			set;
		}
        #endregion

        #region LegacyReceipt
        public abstract class legacyReceipt : IBqlField
        {
        }
        [PXDBBool]
        [PXDefault(false)]
        public bool? LegacyReceipt
        {
            get;
            set;
        }
        #endregion

        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
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
		public abstract class createdByScreenID : PX.Data.IBqlField
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
		public abstract class createdDateTime : PX.Data.IBqlField
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
		public abstract class lastModifiedByID : PX.Data.IBqlField
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
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
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
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		[PXSearchable(SM.SearchCategory.TM, Messages.SearchableTitleExpenseReceipt, new Type[] { typeof(EPExpenseClaimDetails.refNbr), typeof(EPExpenseClaimDetails.employeeID), typeof(EPEmployee.acctName) },
			new Type[] { typeof(EPExpenseClaimDetails.tranDesc) },
			NumberFields = new Type[] { typeof(EPExpenseClaimDetails.refNbr) },
			Line1Format = "{0:d}{1}{2}", Line1Fields = new Type[] { typeof(EPExpenseClaimDetails.expenseDate), typeof(EPExpenseClaimDetails.status), typeof(EPExpenseClaimDetails.refNbr) },
			Line2Format = "{0}", Line2Fields = new Type[] { typeof(EPExpenseClaimDetails.tranDesc) },
			SelectForFastIndexing = typeof(Select2<EPExpenseClaimDetails, InnerJoin<EPEmployee, On<EPExpenseClaimDetails.employeeID, Equal<EPEmployee.bAccountID>>>>),
			SelectDocumentUser = typeof(Select2<Users,
			InnerJoin<EPEmployee, On<Users.pKID, Equal<EPEmployee.userID>>>,
			Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>>)
		)]
		[PXNote(
		DescriptionField = typeof(EPExpenseClaimDetails.claimDetailID),
		Selector = typeof(EPExpenseClaimDetails.claimDetailID),
		ShowInReferenceSelector = true)]
		public virtual Guid? NoteID
		{
			get;
			set;
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		[PXDBTimestamp]
		public virtual byte[] tstamp
		{
			get;
			set;
		}
		#endregion

		#region Obsolete
		#region ProjectDescription
		[Obsolete("Will be removed in Acumtica 2018 R1")]
		public abstract class projectDescription : IBqlField { }
		[Obsolete("Will be removed in Acumtica 2018 R1")]
		[PXFormula(typeof(Selector<EPExpenseClaimDetails.contractID, Contract.description>))]
		[PXUIField(DisplayName = "Project Description", IsReadOnly = true)]
		[PXString]
		public virtual string ProjectDescription
		{
			get;
			set;
		}
		#endregion
		#region ProjectTaskDescription
		[Obsolete("Will be removed in Acumtica 2018 R1")]
		public abstract class projectTaskDescription : IBqlField { }
		[Obsolete("Will be removed in Acumtica 2018 R1")]
		[PXUIField(DisplayName = "Project Task Description", IsReadOnly = true)]
		[PXFormula(typeof(Selector<EPExpenseClaimDetails.taskID, PMTask.description>))]
		[PXString]
		public virtual string ProjectTaskDescription
		{
			get;
			set;
		}
		#endregion
		#region byCorporateCard
		[Obsolete("Will be removed in Acumtica 2018 R1")]
		public abstract class byCorporateCard : PX.Data.IBqlField
		{
		}
		[Obsolete("Will be removed in Acumtica 2018 R1")]
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Paid by Corporate Card")]
		public virtual bool? ByCorporateCard
		{
			get;
			set;
		}
		#endregion
		#region Submited
		[Obsolete("Will be removed in Acumtica 2018 R1")]
		public abstract class submited : IBqlField
		{
		}
		[Obsolete("Will be removed in Acumtica 2018 R1")]
        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Submitted", Enabled = false)]
        public bool? Submited
        {
            get;
            set;
        }
		#endregion
		#endregion
	}
}