using System;

using PX.Data;

using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;
using PX.Data.ReferentialIntegrity.Attributes;

namespace PX.Objects.AR
{
	/// <summary>
	/// Represents an overdue charge code, which is used to calculate overdue charges
	/// used for late payments in the collection process. The record encapsulates
	/// various information about the overdue charge, such as the calculation method,
	/// overdue fee rates, and overdue GL accounts. The entities of this type are 
	/// created and edited on the Overdue Charges (AR204500) form, which corresponds
	/// to the <see cref="ARFinChargesMaint"/> graph. The overdue charge codes are
	/// then used in the Calculate Overdue Charges (AR507000) processing, which
	/// corresponds to the <see cref="ARFinChargesApplyMaint"/> graph.
	/// </summary>
	[Serializable]
	[PXPrimaryGraph(typeof(ARFinChargesMaint))]
	[PXCacheName(Messages.ARFinCharge)]
	public partial class ARFinCharge : IBqlTable
	{
		#region FinChargeID
		public abstract class finChargeID : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
		[PXUIField(DisplayName = "Overdue Charge ID", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(ARFinCharge.finChargeID))]
		public virtual string FinChargeID
		{
			get;
			set;
		}
		#endregion
		#region FinChargeDesc
		public abstract class finChargeDesc : IBqlField
		{
		}
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description")]
		public virtual string FinChargeDesc
		{
			get;
			set;
		}
		#endregion
		#region TermsID
		public abstract class termsID : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
		[PXDefault]
		[PXUIField(DisplayName = "Terms", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<Terms.termsID,
			Where<Terms.visibleTo, Equal<TermsVisibleTo.all>,
			Or<Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof(Terms.descr), Filterable = true)]
		public virtual string TermsID
		{
			get;
			set;
		}
		#endregion
		#region BaseCurFlag
		public abstract class baseCurFlag : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Base Currency")]
		public virtual bool? BaseCurFlag
		{
			get;
			set;
		}
		#endregion
		#region MinFinChargeFlag
		public abstract class minFinChargeFlag : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Use Line Minimum Amount")]
		public virtual bool? MinFinChargeFlag
		{
			get;
			set;
		}
		#endregion
		#region MinFinChargeAmount
		public abstract class minFinChargeAmount : IBqlField
		{
		}
		[PXDBBaseCury(MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Min. Amount")]
		public virtual decimal? MinFinChargeAmount
		{
			get;
			set;
		}
		#endregion
		#region LineThreshold
		public abstract class lineThreshold : IBqlField
		{
		}
		[PXBaseCury(MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Threshold")]
		public virtual decimal? LineThreshold
		{
			get
			{
				return this.MinFinChargeAmount;
			}

			set
			{
				this.MinFinChargeAmount = value;
			}
		}
		#endregion
		#region FixedAmount
		public abstract class fixedAmount : IBqlField
		{
		}
		[PXBaseCury(MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount")]
		public virtual decimal? FixedAmount
		{
			get
			{
				return this.MinFinChargeAmount;
			}

			set
			{
				this.MinFinChargeAmount = value;
			}
		}
		#endregion
		#region MinChargeDocumentAmt
		public abstract class minChargeDocumentAmt : IBqlField
		{
		}
		[PXDBBaseCury(MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total Threshold")]
		public virtual decimal? MinChargeDocumentAmt
		{
			get;
			set;
		}
		#endregion
		#region PercentFlag
		public abstract class percentFlag : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Use Percent Rate")]
		public virtual bool? PercentFlag
		{
			get;
			set;
		}
		#endregion
		#region FinChargeAcctID
		public abstract class finChargeAccountID : IBqlField
		{
		}

		[PXDefault]
		[PXNonCashAccount(DisplayName = "Overdue Charge Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Account.description), Required = true)]
		public virtual int? FinChargeAccountID
		{
			get;
			set;
		}
		#endregion
		#region FinChargeSubID
		public abstract class finChargeSubID : IBqlField
		{
		}

		[PXDefault]
		[SubAccount(typeof(ARFinCharge.finChargeAccountID), DisplayName = "Overdue Charge Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Sub.description), Required = true)]
		public virtual int? FinChargeSubID
		{
			get;
			set;
		}
		#endregion
		#region TaxCategoryID
		public abstract class taxCategoryID : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Tax Category", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(TaxCategory.taxCategoryID), DescriptionField = typeof(TaxCategory.descr))]
		[PXRestrictor(typeof(Where<TaxCategory.active, Equal<True>>), TX.Messages.InactiveTaxCategory, typeof(TaxCategory.taxCategoryID))]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXForeignReference(typeof(Field<ARFinCharge.taxCategoryID>.IsRelatedTo<TaxCategory.taxCategoryID>))]
		public virtual string TaxCategoryID
		{
			get;
			set;
		}
		#endregion
		#region FeeAcctID
		public abstract class feeAccountID : IBqlField
		{
		}
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXNonCashAccount(DisplayName = "Fee Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Account.description))]
		public virtual int? FeeAccountID
		{
			get;
			set;
		}
		#endregion
		#region FeeSubID
		public abstract class feeSubID : IBqlField
		{
		}

		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[SubAccount(typeof(ARFinCharge.finChargeAccountID), DisplayName = "Fee Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Sub.description))]
		public virtual int? FeeSubID
		{
			get;
			set;
		}
		#endregion
		#region FeeAmount
		public abstract class feeAmount : IBqlField
		{
		}
		[PXDBDecimal(MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Fee Amount", Visibility = PXUIVisibility.Visible, Enabled = true)]
		public virtual decimal? FeeAmount
		{
			get;
			set;
		}
		#endregion
		#region FeeDesc
		public abstract class feeDesc : IBqlField
		{
		}
		[PXDBLocalizableString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Fee Description")]
		public virtual string FeeDesc
		{
			get;
			set;
		}
		#endregion
		#region CalculationMethod
		public abstract class calculationMethod : IBqlField
		{
		}
		[PXDBInt]
		[PXDefault(0)]
		[PXIntList(new int[]
		{
			OverdueCalculationMethod.InterestOnBalance,
			OverdueCalculationMethod.InterestOnProratedBalance,
			OverdueCalculationMethod.InterestOnArrears
		}, new string[]
		{
			Messages.InterestOnBalance,
			Messages.InterestOnProratedBalance,
			Messages.InterestOnArrears
		})]
		[PXUIField(DisplayName = "Calculation Method", Visibility = PXUIVisibility.Visible)]
		public virtual int? CalculationMethod
		{
			get;
			set;
		}
		#endregion
		#region ChargingMethod
		public abstract class chargingMethod : IBqlField
		{
		}
		[PXInt]
		[PXDefault(1)]
		[PXIntList(new int[]
		{
			OverdueChargingMethod.FixedAmount,
			OverdueChargingMethod.PercentWithThreshold,
			OverdueChargingMethod.PercentWithMinAmount
		}, new string[]
		{
			Messages.FixedAmount,
			Messages.PercentWithThreshold,
			Messages.PercentWithMinAmount
		})]
		[PXUIField(DisplayName = "Charging Method")]
		public virtual int? ChargingMethod
		{
			get
			{
				if (MinFinChargeFlag == true && PercentFlag == false)
				{
					return OverdueChargingMethod.FixedAmount;
				}
				else if (MinFinChargeFlag == false && PercentFlag == true)
				{
					return OverdueChargingMethod.PercentWithThreshold;
				}
				else if (MinFinChargeFlag == true && PercentFlag == true)
				{
					return OverdueChargingMethod.PercentWithMinAmount;
				}
				else
				{
					return null;
				}
			}

			set
			{
				switch (value)
				{
					case OverdueChargingMethod.FixedAmount:
						MinFinChargeFlag = true;
						PercentFlag = false;
						break;
					case OverdueChargingMethod.PercentWithThreshold:
						MinFinChargeFlag = false;
						PercentFlag = true;
						break;
					case OverdueChargingMethod.PercentWithMinAmount:
						MinFinChargeFlag = true;
						PercentFlag = true;
						break;
				}
			}
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
		#region NoteID
		public abstract class noteID : IBqlField
		{
		}
		[PXNote]
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
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
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
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
	}

	public class OverdueChargingMethod
	{
		public const int FixedAmount = 1;
		public const int PercentWithThreshold = 2;
		public const int PercentWithMinAmount = 3;
	}

	public class OverdueCalculationMethod
	{
		public const int InterestOnBalance = 0;
		public const int InterestOnProratedBalance = 1;
		public const int InterestOnArrears = 2;
	}
}
