using System;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.AR.BQL;

namespace PX.Objects.AR
{
	/// <summary>
	/// This Class is used in the AR Statement report. Cash Sales and Cash Returns 
	/// are excluded to prevent them from appearing in the Statement Report.
	/// </summary>
	[PXCacheName(Messages.ARStatementDetailInfo)]
	[PXProjection(typeof(Select2<
		ARStatementDetail,
			InnerJoin<ARRegister, 
				On<ARStatementDetail.docType, Equal<ARRegister.docType>,
				And<ARStatementDetail.refNbr, Equal<ARRegister.refNbr>>>,
			LeftJoin<Standalone.ARInvoice, 
				On<Standalone.ARInvoice.docType, Equal<ARRegister.docType>,
				And<Standalone.ARInvoice.refNbr, Equal<ARRegister.refNbr>>>,
			LeftJoin<Standalone.ARPayment, 
				On<Standalone.ARPayment.docType, Equal<ARRegister.docType>,
				And<Standalone.ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>>,
		Where<IsNotSelfApplying<ARRegister.docType>>>), 
		Persistent = false)]
	[Serializable]
	public partial class ARStatementDetailInfo : ARStatementDetail
	{
		#region BranchID
		public new abstract class branchID : IBqlField { }
		#endregion
		#region StatementDate
		public new abstract class statementDate : IBqlField { }
		#endregion
		#region CuryID
		public new abstract class curyID : IBqlField { }
		#endregion
		#region DocType
		public new abstract class docType : IBqlField { }
		#endregion
		#region RefNbr
		public new abstract class refNbr : IBqlField { }
		#endregion
		#region DocBalance
		public new abstract class docBalance : IBqlField { }
		#endregion
		#region CuryDocBalance
		public new abstract class curyDocBalance : IBqlField { }
		#endregion
		#region IsOpen
		public abstract new class isOpen : IBqlField { }
		#endregion
		#region DocStatementDate
		public abstract class docStatementDate : IBqlField { }
		[PXDBDate(BqlField = typeof(ARRegister.statementDate))]
		public virtual DateTime? DocStatementDate
		{
			get;
			set;
		}
		#endregion
		#region DocDesc
		public abstract class docDesc : IBqlField { }
		[PXDBString(60, IsUnicode = true, BqlField = typeof(ARRegister.docDesc))]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string DocDesc
		{
			get;
			set;
		}
		#endregion

		#region PrintDocType
		public abstract class printDocType : IBqlField { }
		[PXString(3, IsFixed = true)]
		[ARDocType.PrintList]
		[PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.Visible, Enabled = true)]
		public virtual string PrintDocType
		{
			get
			{
				return this.DocType;
			}
		}
		#endregion
		#region Payable
		public abstract class payable : IBqlField { }

		public virtual bool? Payable
		{
			[PXDependsOnFields(typeof(docType))]
			get
			{
				return ARDocType.Payable(this.DocType);
			}		
		}
        #endregion
        #region BalanceSign
        public abstract class balanceSign : IBqlField { }

        public virtual decimal? BalanceSign
        {
            [PXDependsOnFields(typeof(docType))]
            get
            {
                return ARDocType.SignBalance(this.DocType);
            }
        }
		#endregion
		#region DocDate
		public abstract class docDate : IBqlField { }
		[PXDBDate(BqlField = typeof(ARRegister.docDate))]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? DocDate
		{
			get;
			set;
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : IBqlField { }
		[PXDBLong(BqlField=typeof(ARRegister.curyInfoID))]
		public virtual long? CuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region CuryOrigDocAmt
		public abstract class curyOrigDocAmt : IBqlField { }
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBCurrency(typeof(ARStatementDetailInfo.curyInfoID), typeof(ARStatementDetailInfo.origDocAmt), BqlField = typeof(ARRegister.curyOrigDocAmt))]
		[PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual decimal? CuryOrigDocAmt
		{
			get;
			set;
		}
		#endregion
		#region OrigDocAmt
		public abstract class origDocAmt : IBqlField { }
		[PXDBBaseCury(BqlField = typeof(ARRegister.origDocAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? OrigDocAmt
		{
			get;
			set;
		}
		#endregion
		#region InvoiceNbr
		public abstract class invoiceNbr : IBqlField { }
		[PXDBString(40, IsUnicode = true,BqlField= typeof(Standalone.ARInvoice.invoiceNbr))]
		[PXUIField(DisplayName = "Customer Ref. Nbr.", Visibility = PXUIVisibility.SelectorVisible, Required=false)]
		public virtual string InvoiceNbr
		{
			get;
			set;
		}
		#endregion
		#region DueDate
		public abstract class dueDate : IBqlField { }
		[PXDBDate(BqlField=typeof(ARRegister.dueDate))]
		[PXUIField(DisplayName = "Due Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? DueDate
		{
			get;
			set;
		}
		#endregion
		#region ExtRefNbr
		public abstract class extRefNbr : IBqlField { }
		[PXDBString(40, IsUnicode = true, BqlField=typeof(Standalone.ARPayment.extRefNbr))]
		[PXUIField(DisplayName = "Payment Ref.", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string ExtRefNbr
		{
			get;
			set;
		}
		#endregion
		#region DocExtRefNbr
		[PXString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Ext. Ref.#", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string DocExtRefNbr
		{
			[PXDependsOnFields(typeof(payable),typeof(invoiceNbr),typeof(extRefNbr))]
			get
			{
				return this.Payable.HasValue
					? (this.Payable.Value ? this.InvoiceNbr : this.ExtRefNbr)
					: string.Empty;
			}

		}
		#endregion
		#region CuryOrigDocAmtSigned
		
		[PXDecimal]
		[PXUIField(DisplayName = "Origin. Amt", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual decimal? CuryOrigDocAmtSigned
		{
            [PXDependsOnFields(typeof(docType), typeof(balanceSign), typeof(curyOrigDocAmt))]
			get
			{
                return BalanceSign * CuryOrigDocAmt;
			}			
		}
		#endregion
		#region OrigDocAmtSigned
		[PXDecimal]
		public virtual decimal? OrigDocAmtSigned
		{
			[PXDependsOnFields(typeof(docType), typeof(balanceSign), typeof(origDocAmt))]
			get
			{
                return BalanceSign * OrigDocAmt;
			}
		}
		#endregion

		#region CuryDocBalanceSigned

		[PXDecimal]
		[PXUIField(DisplayName = "Amount Due", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual decimal? CuryDocBalanceSigned
		{
			[PXDependsOnFields(typeof(payable), typeof(curyDocBalance))]
			get
			{
				return this.Payable.HasValue
					? (this.Payable.Value ? this.CuryDocBalance	: -this.CuryDocBalance)
					: null;
			}
		}
		#endregion
		#region DocBalanceSigned
		[PXDecimal]
		public virtual decimal? DocBalanceSigned
		{
			[PXDependsOnFields(typeof(payable),typeof(docBalance))]
			get
			{
				return this.Payable.HasValue
					? (this.Payable.Value ? this.DocBalance : -this.DocBalance)
					: null;
			}
		}
		#endregion		
	}
}