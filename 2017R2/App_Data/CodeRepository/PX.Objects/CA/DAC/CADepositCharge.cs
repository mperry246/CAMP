using System;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.GL;


namespace PX.Objects.CA
{
    /// <summary>
    /// Contains the main properties of deposit charges.
    /// Deposit charges are created on the Bank Deposits (CA305000) form (which corresponds to the <see cref="CADepositEntry"/> graph)
    /// based on the settings of the CA Deposit and Cash Account clearing accounts.
    /// </summary>
    [Serializable]
	[PXCacheName(Messages.CADepositCharge)]
	public partial class CADepositCharge : IBqlTable
	{
		#region TranType
		public abstract class tranType : IBqlField
		{
		}

        /// <summary>
        /// The type of the parent document.
        /// This field is a part of the compound key of the document.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="CADeposit.TranType"/> field.
        /// </value>
		[PXDBString(3, IsFixed = true, IsKey = true)]
		[CATranType.DepositList]
		[PXDefault(typeof(CADeposit.tranType))]
		[PXUIField(DisplayName = "Tran. Type", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string TranType
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
        /// The reference number of the document.
        /// This field is a part of the compound key of the document.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="CADeposit.RefNbr"/> field.
        /// </value>
        [PXDBString(15, IsKey = true, InputMask = "", IsUnicode = true)]
		[PXDBDefault(typeof(CADeposit.refNbr))]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
		[PXParent(typeof(Select<CADeposit, Where<CADeposit.tranType, Equal<Current<CADepositCharge.tranType>>,
									And<CADeposit.refNbr, Equal<Current<CADepositCharge.refNbr>>>>>))]
		public virtual string RefNbr
		{
			get;
			set;
		}
		#endregion
		#region EntryTypeID
	    public abstract class entryTypeID : IBqlField
	    {
	    }

        /// <summary>
        /// The entry type of the bank charges that apply to the deposit. 
        /// The entry types can be configured on the Entry Types (CA203000) form.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="CAEntryType.EntryTypeId"/> field.
        /// </value>
        [PXDBString(10, IsKey = true, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Charge", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail,
								On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>,
								And<CashAccountETDetail.accountID, Equal<Current<CADeposit.cashAccountID>>>>>,
								Where<CAEntryType.module, Equal<GL.BatchModule.moduleCA>,
								And<CAEntryType.useToReclassifyPayments, Equal<False>>>>))]
		public virtual string EntryTypeID
		{
			get;
			set;
		}
		#endregion
		#region DepositAcctID
		public abstract class depositAcctID : IBqlField
		{
		}

        /// <summary>
        /// The cash account from which the charge is taken when the deposit is made.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="CADeposit.CashAccountID"/> field.
        /// </value>
        [PXDefault(typeof(CADeposit.cashAccountID))]
		[CashAccount(null, typeof(Search<CashAccount.cashAccountID>), IsKey = true, DisplayName = "Clearing Account")]
		public virtual int? DepositAcctID
		{
			get;
			set;
		}
		#endregion
		#region PaymentMethodID
		public abstract class paymentMethodID : IBqlField
		{
		}

        /// <summary>
        /// The payment method of the deposited payment to which the charge rate should be applied. 
        /// This field is a part of the primary key.
        /// The field is useful if a bank establishes different charge rates for different payment methods. 
        /// In the case, when payments with different payment methods are mixed in one deposit, you can group multiple records with different rates by the payment method. 
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="PaymentMethod.PaymentMethodID"/> field.
        /// If the value of the field is an empty string (which is the default value), the charge rate is applied to deposited payments regardless of their payment method.
        /// </value>
        [PXDBString(10, IsKey = true, IsUnicode = true)]
		[PXDefault("")]
		[PXUIField(DisplayName = "Payment Method")]
		[PXSelector(typeof(PaymentMethod.paymentMethodID))]
		public virtual string PaymentMethodID
		{
			get;
			set;
		}
		#endregion
		#region AccountID
		public abstract class accountID : IBqlField
		{
		}

        /// <summary>
        /// The expense account to which the charges are recorded.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="CAEntryType.AccountID"/> field.
        /// </value>
        [PXDefault(typeof(Search<CAEntryType.accountID, Where<CAEntryType.entryTypeId, Equal<Current<CADepositCharge.entryTypeID>>>>))]
		[GL.Account(DisplayName = "Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(GL.Account.description))]
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

        /// <summary>
        /// The subaccount to be used with the expense account.
        /// </summary>
        /// <value>
        /// Corresponds to the value of the <see cref="CAEntryType.SubID"/> field.
        /// </value>
		[PXDefault(typeof(Search<CAEntryType.subID, Where<CAEntryType.entryTypeId, Equal<Current<CADepositCharge.entryTypeID>>>>))]
		[SubAccount(typeof(CADepositCharge.accountID), DisplayName = "Clearing Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Sub.description))]
		public virtual int? SubID
		{
			get;
			set;
		}
		#endregion
		#region DrCr
		public abstract class drCr : IBqlField
		{
		}

        /// <summary>
        /// The balance type of the deposit.
        /// </summary>
        /// <value>
        /// The field can have one of the following values:
        /// <c>"D"</c>: Receipt,
        /// <c>"C"</c>: Disbursement.
        /// </value>
		[PXDefault(CADrCr.CACredit)]
		[PXDBString(1, IsFixed = true)]
		[CADrCr.List]
		[PXUIField(DisplayName = "Disb. / Receipt")]
		public virtual string DrCr
		{
			get;
			set;
		}
		#endregion
		#region ChargeRate
	    public abstract class chargeRate : IBqlField
	    {
	    }

        /// <summary>
        /// The rate of the bank charges.
        /// </summary>
        [PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]		
		[PXUIField(DisplayName = "Charge Rate", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual decimal? ChargeRate
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
        /// The identifier of the exchange rate record for the deposit.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CurrencyInfo.CuryInfoID"/> field.
        /// </value>
        [PXDBLong]
		[CurrencyInfo(typeof(CADeposit.curyInfoID))]
		public virtual long? CuryInfoID
		{
			get;
			set;
		}
		#endregion		
		#region CuryChargeableAmt
		public abstract class curyChargeableAmt : IBqlField
		{
		}

        /// <summary>
        /// The amount to be used as a base for the charges in the selected currency.
        /// </summary>
		[PXDBCurrency(typeof(CADepositCharge.curyInfoID), typeof(CADepositCharge.chargeableAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Chargeable Amount", Visibility = PXUIVisibility.Visible)]
		public virtual decimal? CuryChargeableAmt
		{
			get;
			set;
		}
		#endregion
		#region ChargeableAmt
		public abstract class chargeableAmt : IBqlField
		{
		}

        /// <summary>
        /// The amount to be used as a base for the charges in the base currency.
        /// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]		
		public virtual decimal? ChargeableAmt
		{
			get;
			set;
		}
		#endregion	
		#region CuryChargeAmt
		public abstract class curyChargeAmt : IBqlField
		{
		}

        /// <summary>
        /// The amount of the charges in the selected currency.
        /// </summary>
		[PXDBCurrency(typeof(CADepositCharge.curyInfoID), typeof(CADepositCharge.chargeAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXFormula(typeof(Mult<CADepositCharge.curyChargeableAmt, Div<CADepositCharge.chargeRate, CS.decimal100>>), typeof(SumCalc<CADeposit.curyChargeTotal>))]
		[PXUIField(DisplayName = "Charge Amount", Visibility = PXUIVisibility.Visible)]
		public virtual decimal? CuryChargeAmt
		{
			get;
			set;
		}
		#endregion
		#region ChargeAmt
		public abstract class chargeAmt : IBqlField
		{
		}

        /// <summary>
        /// The amount of the charges in the base currency.
        /// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Charge Amount")]
		public virtual decimal? ChargeAmt
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