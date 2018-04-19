using System;
using PX.Data;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using APPayment = PX.Objects.AP.APPayment;
using ARCashSale = PX.Objects.AR.Standalone.ARCashSale;

namespace PX.Objects.CA
{	
	[Serializable]
	[PXPrimaryGraph(new Type[] {
						typeof(CATranEntry),
						typeof(CashTransferEntry),
						typeof(CADepositEntry),
						typeof(AR.ARCashSaleEntry),
						typeof(AR.ARPaymentEntry),
						typeof(AP.APQuickCheckEntry),
						typeof(AP.APPaymentEntry),
						typeof(CABatchEntry),
						typeof(JournalEntry)
						},
					new Type[] {
						typeof(Select<CAAdj, Where<CAAdj.tranID, Equal<Current<CATran.tranID>>>>),
						typeof(Select<CATransfer, Where<CATransfer.tranIDIn, Equal<Current<CATran.tranID>>,
								Or<CATransfer.tranIDOut, Equal<Current<CATran.tranID>>>>>),
						typeof(Select<CADeposit, Where<CADeposit.tranType, Equal<Current<CATran.origTranType>>,
								And<CADeposit.refNbr, Equal<Current<CATran.origRefNbr>>>>>),
						typeof(Select<ARCashSale, Where<ARCashSale.docType, Equal<Current<CATran.origTranType>>,
							And<ARCashSale.refNbr,Equal<Current<CATran.origRefNbr>>>>>),
						typeof(Select<ARPayment,Where<ARPayment.docType, Equal<Current<CATran.origTranType>>,
							And<ARPayment.refNbr, Equal<Current<CATran.origRefNbr>>,
							And<Current<CATran.origModule>, Equal<BatchModule.moduleAR>>>>>),
						typeof(Select<APQuickCheck, Where<APQuickCheck.docType, Equal<Current<CATran.origTranType>>,
							And<APQuickCheck.refNbr, Equal<Current<CATran.origRefNbr>>>>>),
						typeof(Select<APPayment, Where<APPayment.docType, Equal<Current<CATran.origTranType>>,
							And<APPayment.refNbr, Equal<Current<CATran.origRefNbr>>,
							And<Current<CATran.origModule>, Equal<BatchModule.moduleAP>>>>>),
						typeof(Select<CABatch, Where<CABatch.batchNbr, Equal<Current<CATran.origRefNbr>>,
							And<Current<CATran.origModule>, Equal<BatchModule.moduleAP>, And<Current<CATran.origTranType>, Equal<CATranType.cABatch>>>>>),
						typeof(Select<Batch, Where<Batch.module, Equal<Current<CATran.origModule>>,
							And<Batch.batchNbr, Equal<Current<CATran.origRefNbr>>>>>)
					})]
	[PXCacheName(Messages.CATran)]
	public partial class CATran : IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected
			{
			get;
			set;
		}
		#endregion

		#region BegBal
		public abstract class begBal : IBqlField
		{
		}
		[PXCury(typeof(CATran.curyID))]
		[PXUIField(Visible = false)]
		public virtual decimal? BegBal
		{
			get;
			set;
		}
		#endregion
		#region EndBal
		public abstract class endBal : IBqlField
		{
		}
		[PXCury(typeof(CATran.curyID))]
		[PXUIField(DisplayName = "Ending Balance", Enabled = false)]
		public virtual decimal? EndBal
		{
			get;
			set;
		}
		#endregion
		#region DayDesc
		public abstract class dayDesc : IBqlField
		{
		}
		[PXString(20, IsUnicode = true)]
		[PXUIField(DisplayName = "Day of Week", Enabled = false)]
		public virtual string DayDesc
		{
			get;
			set;
		}
		#endregion
		#region OrigModule
		public abstract class origModule : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true)]
		[PXDefault]
        [BatchModule.List]
		[PXUIField(DisplayName = "Module")]
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
		[PXDBString(3, IsFixed = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Tran. Type")]
        [CAAPARTranType.ListByModule(typeof(origModule))]
        public virtual string OrigTranType
		{
			get;
			set;
		}
		#endregion
		#region OrigRefNbr
		public abstract class origRefNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Orig. Doc. Number")]
		public virtual string OrigRefNbr
		{
			get;
			set;
		}
		#endregion
		#region IsPaymentChargeTran
		public abstract class isPaymentChargeTran : IBqlField
		{
		}

		/// <summary>
		/// Indicates that CATran was created by ARPaymentChargeTran or APPaymentChargeTran
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? IsPaymentChargeTran
		{
			get;
			set;
		}
		#endregion
		#region OrigLineNbr
		public abstract class origLineNbr : IBqlField
		{
		}
		[PXDBInt]
		public virtual int? OrigLineNbr
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
		[PXUIField(DisplayName = "Document Ref.", Visibility = PXUIVisibility.Visible)]
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
		[PXDefault]
		[CashAccount(DisplayName = "Cash Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(CashAccount.descr))]
		public virtual int? CashAccountID
			{
			get;
			set;
		}
		#endregion
		#region TranID
		public abstract class tranID : IBqlField
		{
		}
		[PXDBLongIdentity(IsKey = true)]
		[PXUIField(DisplayName = "Document Number")]
		[PXVerifySelector(typeof(Search<CATran.tranID, Where<CATran.cashAccountID, Equal<Current<CARecon.cashAccountID>>, And<Where<CATran.reconNbr, IsNull, Or<CATran.reconNbr, Equal<Current<CARecon.reconNbr>>>>>>>),
			typeof(CATran.extRefNbr),
			typeof(CATran.tranDate),
			typeof(CATran.origModule),
			typeof(CATran.origTranType),
			typeof(CATran.origRefNbr),
			typeof(CATran.status),
			typeof(CATran.curyDebitAmt),
			typeof(CATran.curyCreditAmt),
			typeof(CATran.tranDesc),
			typeof(CATran.cleared),
			typeof(CATran.clearDate), 
			VerifyField = false, DescriptionField = typeof(CATran.extRefNbr))]
		public virtual long? TranID
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
		[PXUIField(DisplayName = "Doc. Date")]
		[CADailyAccumulator]
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
		[PXDefault]
		[PXDBString(1, IsFixed = true)]
		[CADrCr.List]
		[PXUIField(DisplayName = "Disb. / Receipt")]
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
		[PXDBInt]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXSelector(typeof(BAccountR.bAccountID),
						SubstituteKey = typeof(BAccountR.acctCD),
					 DescriptionField = typeof(BAccountR.acctName))]
		[PXUIField(DisplayName = "Business Account", Visibility = PXUIVisibility.Visible)]
		public virtual int? ReferenceID
			{
			get;
			set;
		}
		#endregion
		#region ReferenceName
		public abstract class referenceName : IBqlField
		{
		}
		[PXUIField(DisplayName = "Business Name", Visibility = PXUIVisibility.Visible)]
		[PXString(60, IsUnicode = true)]
		public virtual string ReferenceName
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
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.Visible)]
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
		[TranPeriodID(typeof(CATran.tranDate))]
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
		[FinPeriodSelector(typeof(CATran.tranDate))]
		[PXDefault]
		[PXUIField(DisplayName = "Post Period")]
		public virtual string FinPeriodID
		{
			get;
			set;
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : IBqlField
		{
		}
		[PXDBLong]
		public virtual long? CuryInfoID
			{
			get;
			set;
		}
		#endregion
		#region Hold
		public abstract class hold : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(typeof(Search<CASetup.holdEntry>))]
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
		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? Released
			{
			get;
			set;
		}
		#endregion
		#region Posted
		public abstract class posted : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? Posted
			{
			get;
			set;
		}
		#endregion
		#region Status
		public abstract class status : IBqlField
		{ 
		}
		[PXString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Status", Enabled = false)]
		[BatchStatus.List]
		public virtual string Status
		{
			[PXDependsOnFields(typeof(posted), typeof(released), typeof(hold))]
			get
			{
				if (this.Posted == true)
				{
					if (this.Released == true)
					{
						return GL.BatchStatus.Posted;
					}
					else
					{
						return GL.BatchStatus.Unposted;
				}
				}
				else if (this.Released == true && this.Posted != true)
				{
					return GL.BatchStatus.Released;
				}
				else if (this.Hold == true)
				{
					return GL.BatchStatus.Hold;
				}
				else
				{
					return GL.BatchStatus.Balanced;
				}
			}

			set
			{ 
			}
		}
		#endregion
		#region Reconciled
		public abstract class reconciled : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Reconciled")]
		public virtual bool? Reconciled
			{
			get;
			set;
		}
		#endregion
		#region ReconDate
		public abstract class reconDate : IBqlField
		{
		}
		[PXDBDate]
		public virtual DateTime? ReconDate
		{
			get;
			set;
		}
		#endregion
		#region ReconNbr
		public abstract class reconNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Reconciled Number", Enabled = false)]
		[PXParent(typeof(Select<CARecon, Where<CARecon.reconNbr, Equal<Current<CATran.reconNbr>>>>), UseCurrent = true, LeaveChildren = true)]
		public virtual string ReconNbr
		{
			get;
			set;
		}
		#endregion
		#region CuryTranAmt
		public abstract class curyTranAmt : IBqlField
		{
		}
		[PXDBCurrency(typeof(CATran.curyInfoID), typeof(CATran.tranAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible)]
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
		[PXUIField(DisplayName = "Tran. Amount")]
		public virtual decimal? TranAmt
		{
			get;
			set;
		}
		#endregion
		#region BatchNbr
		public abstract class batchNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Batch Number")]
		public virtual string BatchNbr
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
		[PXDefault(typeof(Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CATran.cashAccountID>>>>))]
		[PXSelector(typeof(Currency.curyID))]
		public virtual string CuryID
			{
			get;
			set;
		}
		#endregion
		#region Cleared
		public abstract class cleared : IBqlField
		{
		}
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
		[PXDBDate]
		[PXUIField(DisplayName = "Clear Date")]
		public virtual DateTime? ClearDate
		{
			get;
			set;
		}
		#endregion
		#region CuryDebitAmt
		public abstract class curyDebitAmt : IBqlField
		{
		}
		[PXDecimal]
		[PXUIField(DisplayName = "Receipt")]
		public virtual decimal? CuryDebitAmt
		{
			[PXDependsOnFields(typeof(drCr), typeof(curyTranAmt))]
			get
			{
					return (this.DrCr == CADrCr.CADebit) ? this.CuryTranAmt : 0m;
				}

			set
			{
			}
		}
		#endregion
		#region CuryCreditAmt
		public abstract class curyCreditAmt : IBqlField
		{
		}
		[PXDecimal]
		[PXUIField(DisplayName = "Disbursement")]
		public virtual decimal? CuryCreditAmt
		{
			[PXDependsOnFields(typeof(drCr), typeof(curyTranAmt))]
			get
			{
					return (this.DrCr == CADrCr.CACredit) ? -this.CuryTranAmt : 0m;
				}

			set
			{
			}
		}
		#endregion
		#region CuryClearedDebitAmt
		public abstract class curyClearedDebitAmt : IBqlField
		{
		}
		[PXDecimal]
		[PXUIField(DisplayName = "Receipt")]
		public virtual decimal? CuryClearedDebitAmt
		{
			[PXDependsOnFields(typeof(cleared), typeof(drCr), typeof(curyTranAmt))]
			get
			{
				return (this.Cleared == true && this.DrCr == CADrCr.CADebit) ? this.CuryTranAmt : 0m;
			}

			set
			{
			}
		}
		#endregion
		#region CuryClearedCreditAmt
		public abstract class curyClearedCreditAmt : IBqlField
		{
		}
		[PXDecimal]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Disbursement")]
		public virtual decimal? CuryClearedCreditAmt
		{
			[PXDependsOnFields(typeof(cleared), typeof(drCr), typeof(curyTranAmt))]
			get
			{
				return (this.Cleared == true && this.DrCr == CADrCr.CACredit) ? -this.CuryTranAmt : 0m;
			}

			set
			{
			}
		}
		#endregion
		#region NoteID
		public abstract class noteID : IBqlField
		{
		}
		[PXNote(DescriptionField = typeof(CATran.tranID))]
		public virtual Guid? NoteID
		{
			get;
			set;
		}
		#endregion
		#region RefTranAccountID
		public abstract class refTranAccountID : IBqlField
		{
		}
		[CashAccount(DisplayName = "ChildTran Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(CashAccount.descr))]
		public virtual int? RefTranAccountID
		{
			get;
			set;
		}
		#endregion
		#region RefTranID
		public abstract class refTranID : IBqlField
		{
		}
		[PXDBLong]		
		public virtual long? RefTranID
			{
			get;
			set;
		}
		#endregion
        #region RefSplitLineNbr
        public abstract class refSplitLineNbr : IBqlField
        {
        }
        [PXDBInt]
        public virtual int? RefSplitLineNbr
            {
			get;
			set;
        }
        #endregion
        #region VoidedTranID
        public abstract class voidedTranID : IBqlField
        {
        }
        [PXDBLong]
        public virtual long? VoidedTranID
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
        #region Methods

        public static void Redirect(PXCache sender, CATran catran)
		{
            if (catran == null)
                return;

            if (sender != null)
                sender.IsDirty = false;

            Common.RedirectionToOrigDoc.TryRedirect(catran.OrigTranType, catran.OrigRefNbr, catran.OrigModule);
        }
		#endregion
	}
}
