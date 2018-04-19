using System;
using PX.Data;
using PX.Data.EP;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.TM;

namespace PX.Objects.CA
{
	[PXCacheName(Messages.CADeposit)]
	[Serializable]
	public partial class CADeposit : IBqlTable, ICADocument 
	{
        /// <summary>
        /// Implementation of the ICADocument interface.
        /// </summary>
        public string DocType
		{
			get
			{
				return this.TranType;
			}
		}
		#region TranType
		public abstract class tranType : IBqlField
		{
		}

        /// <summary>
        /// The type of the deposit.
        /// This field is a part of the compound key of the deposit.
        /// The field can have one of the following values:
        /// <c>"CDT"</c>: CA Deposit,
        /// <c>"CVD"</c>: CA Void Deposit.
        /// </summary>
		[PXDBString(3, IsFixed = true, IsKey = true)]
		[CATranType.DepositList]
		[PXDefault(CATranType.CADeposit)]
		[PXUIField(DisplayName = "Tran. Type", Enabled = true, Visibility = PXUIVisibility.SelectorVisible)]
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
        /// The reference number of the deposit.
        /// This field is a part of the compound key of the deposit.
        /// </summary>
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
		[CADepositType.Numbering]
		[CADepositType.RefNbr(typeof(Search<CADeposit.refNbr, Where<CADeposit.tranType, Equal<Current<CADeposit.tranType>>>>))]
		public virtual string RefNbr
		{
			get;
			set;
		}
		#endregion
		
		#region ExtRefNbr
		public abstract class extRefNbr : IBqlField
		{
		}

        /// <summary>
        /// The external reference number of the deposit.
        /// </summary>
		[PXDBString(40, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Document Ref.", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string ExtRefNbr
		{
			get;
			set;
		}
		#endregion
		#region CashAccountID
		public abstract class cashAccountID : IBqlField
		{
		}

        /// <summary>
        /// The cash account (usually a bank account) to which the deposit will be posted.
        /// </summary>
		[PXDefault]
		[CashAccount(null, typeof(Search<CashAccount.cashAccountID, Where2<Match<Current<AccessInfo.userName>>, And<CashAccount.clearingAccount, Equal<CS.boolFalse>>>>), DisplayName = "Cash Account", Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(CashAccount.descr))]
		public virtual int? CashAccountID
		{
			get;
			set;
		}
		#endregion
		#region TranDate
		public abstract class tranDate : IBqlField
		{
		}

        /// <summary>
        /// The date of the deposit.
        /// </summary>
		[PXDBDate]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Deposit Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? TranDate
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
		[PXDefault(CADrCr.CADebit)]
		[PXDBString(1, IsFixed = true)]
		[CADrCr.List]
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

        /// <summary>
        /// A detailed description of the deposit.
        /// </summary>
		[PXDBString(256, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual string TranDesc
		{
			get;
			set;
		}
		#endregion
		#region TranPeriodID
		public abstract class tranPeriodID : IBqlField
		{
		}

        /// <summary>
        /// The<see cref="FinPeriod"> financial period</see> of the document.
        /// </summary>
        /// <value>
        /// Is determined by the <see cref="CADeposit.TranDate">date of the document</see>.
        /// A user can override the value of this field (unlike <see cref="CADeposit.FinPeriodID"/>).
        /// </value>
        [OpenPeriod(typeof(CADeposit.tranDate))]
		public virtual string TranPeriodID
		{
			get;
			set;
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : IBqlField
		{
		}

        /// <summary>
        /// <see cref="FinPeriod">Financial Period</see> of the document.
        /// </summary>
        /// <value>
        /// Defaults to the period to which the <see cref="CADeposit.TranDate"/> belongs, but can be overridden by a user.
        /// </value>
        [OpenPeriod(typeof(CADeposit.tranDate))]
		[PXUIField(DisplayName = "Fin. Period")]
		public virtual string FinPeriodID
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
        /// A check box that indicates (if selected) that the deposit is on hold, which means it may be edited but cannot be released.
        /// </summary>
		[PXDBBool]
		[PXDefault(typeof(Search<CASetup.holdEntry>))]
		[PXUIField(DisplayName = "Hold")]
		public virtual bool? Hold
		{
			get;
			set;
		}
		#endregion
		#region Released
		public abstract class released : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the deposit is released.
        /// </summary>
		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? Released
		{
			get;
			set;
		}
		#endregion
		#region Voided
		public abstract class voided : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the deposit is voided.
        /// </summary>
        [PXDBBool]
		[PXUIField(DisplayName = "Voided", Visibility = PXUIVisibility.Visible)]
		[PXDefault(false)]
		public virtual bool? Voided
		{
			get;
			set;
		}
        #endregion
        #region Status
        /// <summary>
        /// The status of the deposit, which the system assigns automatically.
        /// This is virtual field and has no representation in the database.
        /// The field can have one of the following values: 
        /// <c>"H"</c>: On Hold;
        /// <c>"B"</c>: Balanced;
        /// <c>"R"</c>: Released;
        /// <c>"V"</c>: Voided.
        /// </summary>
        [PXString(1, IsFixed = true)]
		[PXDefault(CADepositStatus.Balanced, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[CADepositStatus.List]
		public virtual string Status
		{
			[PXDependsOnFields(typeof(hold), typeof(released), typeof(voided))]
            get
            {
                if (Hold.HasValue && Hold == true)
                {
                    return CADepositStatus.Hold;
                }
				if (Released.HasValue && Released == true)
				{
                    return (this.Voided == true) ? CADepositStatus.Voided : CADepositStatus.Released;
				}
                return CADepositStatus.Balanced;
            }

            set
			{
			}
		}
		#endregion
		#region CuryID
		public abstract class curyID : IBqlField
		{
		}

        /// <summary>
        /// The currency of the deposit.
        /// </summary>
        /// <value>
        /// Corresponds to the currency of the cash account <see cref="CashAccount.CuryID"/>.
        /// </value>
		[PXDBString(5, InputMask = ">LLLLL", IsUnicode = true)]
		[PXUIField(DisplayName = "Currency", Enabled = false)]
		[PXDefault(typeof(Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CADeposit.cashAccountID>>>>))]
		[PXSelector(typeof(Currency.curyID))]
		public virtual string CuryID
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
		[CurrencyInfo(ModuleCode = "CA")]
		public virtual long? CuryInfoID
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
        /// The total amount of the deposit, including the cash amount minus the charge total amount in the selected currency.
        /// </summary>
		[PXDBCurrency(typeof(CADeposit.curyInfoID), typeof(CADeposit.tranAmt))]
		[PXFormula(typeof(Add<CADeposit.curyDetailTotal, Add<CADeposit.curyExtraCashTotal, Mult<CADeposit.curyChargeTotal, CADeposit.chargeMult>>>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total Amount", Enabled = false)]
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

        /// <summary>
        /// The total amount of the deposit, including the cash amount minus the charge total amount in the base currency.
        /// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tran Amount", Enabled = false)]
		public virtual decimal? TranAmt
		{
			get;
			set;
		}
		#endregion		
		#region CuryDetailTotal
		public abstract class curyDetailTotal : IBqlField
		{
		}

        /// <summary>
        /// The line total amount of the deposit in the selected currency.
        /// </summary>
		[PXDBCurrency(typeof(CADeposit.curyInfoID), typeof(CADeposit.detailTotal))]
		[PXUIField(DisplayName = "Deposits Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryDetailTotal
		{
			get;
			set;
		}
		#endregion
		#region DetailTotal
		public abstract class detailTotal : IBqlField
		{
		}

        /// <summary>
        /// The line total amount of the deposit in the base currency.
        /// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? DetailTotal
		{
			get;
			set;
		}
        #endregion
        #region EntryTypeID
        [Obsolete(Common.Messages.FieldIsObsoleteRemoveInAcumatica7)]
        public abstract class entryTypeID : IBqlField
		{
		}
        [Obsolete(Common.Messages.FieldIsObsoleteRemoveInAcumatica7)]
		[PXDBString(10, IsUnicode = true)]
		[PXSelector(typeof(Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>,
									Where<CashAccountETDetail.accountID, Equal<Current<CADeposit.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>>>>), DescriptionField = typeof(CAEntryType.descr))]
		[PXUIField(DisplayName = "Entry Type", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string EntryTypeID
		{
			get;
			set;
		}
		#endregion
		#region CuryChargeTotal
		public abstract class curyChargeTotal : IBqlField
		{
		}

        /// <summary>
        /// The total amount of any charges that apply to the deposit in the selected currency.
        /// </summary>
		[PXDBCurrency(typeof(CADeposit.curyInfoID), typeof(CADeposit.chargeTotal))]
		[PXUIField(DisplayName = "Charge Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryChargeTotal
		{
			get;
			set;
		}
		#endregion
		#region ChargeTotal
		public abstract class chargeTotal : IBqlField
		{
		}

        /// <summary>
        /// The total amount of any charges that apply to the deposit in the base currency.
        /// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ChargeTotal
		{
			get;
			set;
		}
		#endregion
		#region CuryExtraCashTotal
		public abstract class curyExtraCashTotal : IBqlField
		{
		}

        /// <summary>
        /// The total amount of cash to be deposited through this deposit in the selected currency.
        /// </summary>
		[PXDBCurrency(typeof(CADeposit.curyInfoID), typeof(CADeposit.extraCashTotal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Cash Drop Amount", Visibility = PXUIVisibility.Visible, Enabled = true)]
		public virtual decimal? CuryExtraCashTotal
		{
			get;
			set;
		}
		#endregion
		#region ExtraCashTotal
		public abstract class extraCashTotal : IBqlField
		{
		}

        /// <summary>
        /// The total amount of cash to be deposited through this deposit in the base currency.
        /// </summary>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ExtraCashTotal
		{
			get;
			set;
		}
		#endregion
		#region ExtraCashAccountID
		public abstract class extraCashAccountID : IBqlField
		{
		}

        /// <summary>
        /// The cash account (usually a Cash On Hand account) from which you want to also deposit some amount on the bank account.
        /// </summary>
        [CashAccount(null, typeof(Search<CashAccount.cashAccountID, Where<CashAccount.curyID, Equal<Current<CADeposit.curyID>>,
            And<CashAccount.cashAccountID, NotEqual<Current<CADeposit.cashAccountID>>>>>), DisplayName = "Cash Drop Account",
															Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(CashAccount.descr))]
		public virtual int? ExtraCashAccountID
		{
			get;
			set;
		}
		#endregion
		#region CuryControlAmt
		public abstract class curyControlAmt : IBqlField
		{
		}

        /// <summary>
        /// The control total of the deposit, which should be equal to <see cref="CADeposit.CuryTranAmt">the total amount in the selected currency</see>.
        /// </summary>
		[PXDBCurrency(typeof(CADeposit.curyInfoID), typeof(CADeposit.controlAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Control Total")]
		public virtual decimal? CuryControlAmt
		{
			get;
			set;
		}
		#endregion
		#region ControlAmt
		public abstract class controlAmt : IBqlField
		{
		}

        /// <summary>
        /// The control total of the deposit, which should be equal to <see cref="CADeposit.TranAmt">the total amount in the base currency</see>.
        /// </summary>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ControlAmt
		{
			get;
			set;
		}
		#endregion
		#region LineCntr
		public abstract class lineCntr : IBqlField
		{
		}

        /// <summary>
        /// The counter of detail lines.
        /// The field depends on <see cref="CADepositDetail.LineNbr"/>.
        /// </summary>
		[PXDBInt]
		[PXDefault(0)]
		public virtual int? LineCntr
		{
			get;
			set;
		}
		#endregion
		#region TranID
		public abstract class tranID : IBqlField
		{
		}

        /// <summary>
        /// The identifier of the deposit-related cash transaction that recorded to the cash account (usually a bank account).
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CATran.TranID"/> field.
        /// </value>
		[PXDBLong]
		[DepositTranID]
		[PXSelector(typeof(Search<CATran.tranID>), DescriptionField = typeof(CATran.batchNbr))]
		public virtual long? TranID
		{
			get;
			set;
		}
		#endregion
		#region CashTranID
		public abstract class cashTranID : IBqlField
		{
		}

        /// <summary>
        /// The identifier of the cash drop transaction.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CATran.TranID"/> field.
        /// The field can be empty.
        /// </value>
		[PXDBLong]
		[DepositCashTranID]
		[PXSelector(typeof(Search<CATran.tranID>), DescriptionField = typeof(CATran.batchNbr))]
		public virtual long? CashTranID
		{
			get;
			set;
		}
		#endregion		
		#region ChargeTranID
		public abstract class chargeTranID : IBqlField
		{
		}

        /// <summary>
        /// The identifier of the cash transaction made for deposit charges.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CATran.TranID"/> field.
        /// </value>
		[PXDBLong]
		[DepositChargeTranID]
		[PXSelector(typeof(Search<CATran.tranID>), DescriptionField = typeof(CATran.batchNbr))]
		public virtual long? ChargeTranID
		{
			get;
			set;
		}
		#endregion		
		#region ChargesSeparate
		public abstract class chargesSeparate : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the charges for the deposit will be booked as a separate transaction in GL. 
        /// If the field is set to <c>false</c>, charges will be deducted from the deposit amount.
        /// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Separate Charges")]
		public virtual bool? ChargesSeparate
		{
			get;
			set;
		}
		#endregion
		#region Cleared
		public abstract class cleared : IBqlField
		{
		}

        /// <summary>
        /// A check box that indicates (if selected) that the deposit was cleared with the bank.
        /// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Cleared")]
		public virtual bool? Cleared
		{
			get;
			set;
		}
		#endregion
        #region ClearDate
        public abstract class clearDate : IBqlField
        {
        }

        /// <summary>
        /// The date when the deposit has been cleared with the bank reconciliation statement.
        /// </summary>
        [PXDBDate]
        [PXUIField(DisplayName = "Clear Date")]
        public virtual DateTime? ClearDate
        {
            get;
            set;
        }
        #endregion
        #region TranID_CATran_batchNbr
        /// <summary>
        /// The reference number of the batch.
        /// The field is used as a navigation link to the Journal Transactions(GL301000) form, where you can view batch details.
        /// The value of the field is filled in by the <see cref="CADepositEntry.CADeposit_TranID_CATran_BatchNbr_FieldSelecting"/> method.
        /// </summary>
        public abstract class tranID_CATran_batchNbr : IBqlField
		{
		}
		#endregion
		#region WorkgroupID
		public abstract class workgroupID : IBqlField
		{
		}

        /// <summary>
        /// The workgroup to which the deposit is assigned for processing.
        /// </summary>
        [PXDBInt]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXCompanyTreeSelector]
		[PXUIField(DisplayName = "Workgroup", Visibility = PXUIVisibility.Visible)]
		public virtual int? WorkgroupID
		{
			get;
			set;
		}
		#endregion
		#region OwnerID
		public abstract class ownerID : IBqlField { }

        /// <summary>
        /// The owner of the deposit, which is the employee who controls the processing.
        /// </summary>
        [PXDBGuid]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXOwnerSelector(typeof(CADeposit.workgroupID))]
		[PXUIField(DisplayName = "Owner", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual Guid? OwnerID
		{
			get;
			set;
		}
		#endregion		
		#region NoteID
		public abstract class noteID : IBqlField
		{
		}
		[PXSearchable(SM.SearchCategory.CA, "{0} {1}", new Type[] { typeof(CADeposit.tranType), typeof(CADeposit.refNbr) },
			new Type[] { typeof(CADeposit.tranDesc), typeof(CADeposit.extRefNbr) },
			NumberFields = new Type[] { typeof(CADeposit.refNbr) },
			Line1Format = "{0}{1:d}{3}", Line1Fields = new Type[] { typeof(CADeposit.extRefNbr), typeof(CADeposit.tranDate), typeof(CADeposit.cashAccountID), typeof(Account.accountCD) },
			Line2Format = "{0}", Line2Fields = new Type[] { typeof(CADeposit.tranDesc) })]
		[PXNote(DescriptionField = typeof(CADeposit.refNbr))]
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
		#region ChargeMult
		public abstract class chargeMult : IBqlField
		{
		}

        /// <summary>
        /// The coefficient of the charge total amount in the calculation of the deposit total amount.
        /// The field depends on the <see cref="ChargesSeparate"/> field.
        /// The field is read-only; it is not displayed on any Acumatica ERP form.
        /// This is virtual field and has no representation in the database.
        /// </summary>
        /// <value>
        /// If the charges for the deposit are specified by a separate transaction (that is, if the <see cref="ChargesSeparate"/> field is set to <c>true</c>), the value of the field is <c>"decimal.Zero"</c>.
        /// If the charges for the deposit are deducted from the deposit amount (that is, if the <see cref="ChargesSeparate"/> field is set to <c>false</c>), the value of the field is <c>"decimal.MinusOne"</c>.
        /// </value>
        [PXDecimal(2)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Control Total")]
		public virtual decimal? ChargeMult
		{
			[PXDependsOnFields(typeof(chargesSeparate))]
			get
			{
				return (this.ChargesSeparate == true ? decimal.Zero : decimal.MinusOne);
			}
		}
		#endregion
	}

	public class CADepositType
	{
        /// <summary>
        /// Specialized selector for CABatch RefNbr.<br/>
        /// By default, defines the following set of columns for the selector:<br/>
        /// CADeposit.refNbr, CADeposit.tranDate, CADeposit.finPeriodID,
		/// CADeposit.cashAccountID, CADeposit.curyID, CADeposit.curyTranAmt,
		/// CADeposit.extRefNbr
        /// <example>
        /// [CADepositType.RefNbr(typeof(Search/<CADeposit.refNbr, Where/<CADeposit.tranType/, Equal/<Current/<CADeposit.tranType/>/>/>/>))]
        /// </example>
        /// </summary>
		public class RefNbrAttribute : PXSelectorAttribute
		{
			public RefNbrAttribute(Type searchType) : base(
                searchType,
				typeof(CADeposit.refNbr),
				typeof(CADeposit.tranDate),
				typeof(CADeposit.finPeriodID),
				typeof(CADeposit.cashAccountID),
				typeof(CADeposit.curyID),
				typeof(CADeposit.curyTranAmt),
				typeof(CADeposit.extRefNbr))
			{
			}
		}

        /// <summary>
        /// Specialized for CADeposit version of the <see cref="AutoNumberAttribute"/><br/>
        /// It defines how the new numbers are generated for the AR Invoice. <br/>
        /// References CADeposit.tranType and CADeposit.tranDate fields of the document,<br/>
        /// and also define a link between  numbering ID's defined in CASetup (namely CASetup.registerNumberingID)<br/>
        /// and CADeposit: <br/>
        /// </summary>		
		public class NumberingAttribute : CS.AutoNumberAttribute
		{
			public NumberingAttribute() : base(
                typeof(CADeposit.tranType), 
                typeof(CADeposit.tranDate), 
                new string[] { CATranType.CADeposit, CATranType.CAVoidDeposit}, 
                new Type[] { typeof(CASetup.registerNumberingID), null }) { }
		}
	}

	public class CADepositStatus
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new string[] { Balanced, Hold, Released, Voided },
				new string[] { Messages.Balanced, Messages.Hold, Messages.Released, Messages.Voided }) { }
		}

		public const string Balanced = "B";
		public const string Hold = "H";
		public const string Released = "R";
		public const string Voided = "V";

		public class balanced : Constant<string>
		{
			public balanced() : base(Balanced) { }
		}

		public class hold : Constant<string>
		{
			public hold() : base(Hold) { }
		}

		public class released : Constant<string>
		{
			public released() : base(Released) { }
		}

		public class voided : Constant<string>
		{
			public voided() : base(Voided) { }
		}
	}
}