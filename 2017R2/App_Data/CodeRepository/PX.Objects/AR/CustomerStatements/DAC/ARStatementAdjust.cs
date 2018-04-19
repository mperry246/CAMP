using System;

using PX.Data;

using PX.Objects.GL;
using PX.Objects.Common;

namespace PX.Objects.AR
{
	/// <summary>
	/// Represents an <see cref="ARAdjust">application</see> 
	/// record, which has been included into a <see cref="ARStatement">
	/// Customer Statement</see>. Statement application details are 
	/// created automatically during the Prepare Statements (AR503000) 
	/// process, which corresponds to the <see cref="ARStatementProcess"/> 
	/// graph. The records of this type are created only for 
	/// <see cref="ARStatementType.BalanceBroughtForward">Balance
	/// Brought Forward</see> statements.
	/// </summary>
	[Serializable]
	[PXCacheName(Messages.ARStatementAdjust)]
	public class ARStatementAdjust : IBqlTable, IDocumentAdjustment, IAdjustmentAmount
	{
		#region BranchID
		public abstract class branchID : IBqlField { }
		/// <summary>
		/// The unique identifier of the branch of the
		/// <see cref="ARStatement">Customer Statement</see> that 
		/// the application detail belongs to. This field is a part 
		/// of the compound key of the record and is a part of the
		/// compound foreign key reference to the parent 
		/// <see cref="ARStatement">Customer Statement</see> record.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARStatement.BranchID"/> field.
		/// </value>
		[Branch(IsKey = true)]
		[PXDefault]
		public virtual int? BranchID
		{
			get;
			set;
		}
		#endregion
		#region CustomerID
		public abstract class customerID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="Customer"/>, for which
		/// the parent <see cref="ARStatement">Customer Statement</see> has
		/// been prepared. This field is a part of the compound key of the
		/// record and is a part of the compound foreign key reference to the
		/// parent <see cref="ARStatement">Customer Statement</see> record.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARStatement.CustomerID"/> field.
		/// </value>
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = Messages.Customer, IsReadOnly = true)]
		public virtual int? CustomerID
		{
			get;
			set;
		}
		#endregion
		#region StatementDate
		public abstract class statementDate : IBqlField { }
		/// <summary>
		/// The date of <see cref="ARStatement">Customer Statement</see> that
		/// the application detail belongs to. This field is a part of the
		/// compound key of the record and is a part of the compound foreign
		/// key reference to the parent <see cref="ARStatement">Customer
		/// Statement</see> record.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARStatement.StatementDate"/> field.
		/// </value>
		[PXDBDate(IsKey = true)]
		[PXParent(typeof(Select<
			ARStatement,
			Where<
				ARStatement.branchID, Equal<Current<ARStatementAdjust.branchID>>,
				And<ARStatement.customerID, Equal<Current<ARStatementAdjust.customerID>>,
				And<ARStatement.statementDate, Equal<Current<ARStatementAdjust.statementDate>>,
				And<ARStatement.curyID, Equal<Current<ARStatementAdjust.curyID>>>>>>>))]
		[PXDefault]
		[PXUIField(DisplayName = Messages.StatementDate)]
		public virtual DateTime? StatementDate
		{
			get;
			set;
		}
		#endregion
		#region CuryID
		public abstract class curyID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="Currency"/> of the
		/// <see cref="ARStatement">Customer Statement</see> that the
		/// application detail belongs to. This field is a part of the 
		/// compound key of the record and is a part of the compound foreign
		/// key reference to the parent <see cref="ARStatement">Customer
		/// Statement</see> record.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARStatement.CuryID"/> field.
		/// </value>
		[PXDBString(5, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = CM.Messages.Currency)]
		public virtual string CuryID
		{
			get;
			set;
		}
		#endregion
		#region AdjdDocType
		public abstract class adjdDocType : IBqlField { }
		/// <summary>
		/// The type of the <see cref="ARRegister">adjusted document</see>. 
		/// This field is a part of the compound key of the record and 
		/// is a part of the compound foreign key reference to the 
		/// <see cref="ARAdjust">application</see> record.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjdDocType"/> field.
		/// The field can have one of the values described in the
		/// <see cref="ARDocType"/> class.
		/// </value>
		[PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
		[PXDefault]
		[PXUIField(DisplayName = Messages.DocType, Visibility = PXUIVisibility.Visible)]
		[LabelList(typeof(ARDocType))]
		public virtual string AdjdDocType
		{
			get;
			set;
		}
		#endregion
		#region AdjdRefNbr
		public abstract class adjdRefNbr : IBqlField { }
		/// <summary>
		/// The reference number of the <see cref="ARRegister">adjusted document</see>. 
		/// This field is a part of the compound key of the record and is a part
		/// of the compound key reference to the <see cref="ARAdjust">
		/// application</see> record.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjdRefNbr"/> field.
		/// </value>
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault]
		[PXUIField(DisplayName = Messages.RefNbr, Visibility = PXUIVisibility.Visible)]
		public virtual string AdjdRefNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjdBranchID
		public abstract class adjdBranchID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="Branch"/> of the 
		/// adjusted document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjdBranchID"/> field.
		/// </value>
		[Branch]
		[PXDefault]
		public virtual int? AdjdBranchID
		{
			get;
			set;
		}
		#endregion
		#region AdjdCustomerID
		public abstract class adjdCustomerID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="Customer"/> of 
		/// the adjusted document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjdCustomerID"/> field.
		/// </value>
		[PXDBInt]
		[PXDefault]
		public virtual int? AdjdCustomerID
		{
			get;
			set;
		}
		#endregion
		#region AdjdCuryID
		public abstract class adjdCuryID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="Currency"/> of
		/// the adjusted document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="CM.CurrencyInfo.CuryID"/> field.
		/// </value>
		[PXDBString]
		[PXDefault]
		public virtual string AdjdCuryID
		{
			get;
			set;
		}
		#endregion
		#region AdjgDocType
		public abstract class adjgDocType : IBqlField { }
		/// <summary>
		/// The type of the <see cref="ARRegister">adjusting document</see>. 
		/// This field is a part of the compound key of the record and is a 
		/// part of the compound foreign key reference to the 
		/// <see cref="ARAdjust">application</see> record.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjgDocType"/> field.
		/// The field can have one of the values described in the
		/// <see cref="ARDocType"/> class.
		/// </value>
		[PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
		[PXDefault]
		[PXUIField(DisplayName = Messages.DocType, Visibility = PXUIVisibility.Visible, Visible = false)]
		[LabelList(typeof(ARDocType))]
		public virtual string AdjgDocType
		{
			get;
			set;
		}
		#endregion
		#region AdjgRefNbr
		public abstract class adjgRefNbr : IBqlField { }
		/// <summary>
		/// The reference number of the <see cref="ARRegister">adjusting 
		/// document</see>. This field is a part of the compound key 
		/// of the record and is a part of the compound foreign key 
		/// reference to the <see cref="ARAdjust">application</see> record.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjgRefNbr"/> field.
		/// </value>
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = Messages.RefNbr, Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual string AdjgRefNbr
		{
			get;
			set;
		}
		#endregion
		#region AdjgBranchID
		public abstract class adjgBranchID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="Branch"/> of 
		/// the adjusting document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjgBranchID"/> field.
		/// </value>
		[Branch]
		[PXDefault]
		public virtual int? AdjgBranchID
		{
			get;
			set;
		}
		#endregion
		#region AdjgCustomerID
		public abstract class adjgCustomerID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="Customer"/> of 
		/// the adjusting document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjgBranchID"/> field.
		/// </value>
		[PXDBInt]
		[PXDefault]
		public virtual int? AdjgCustomerID
		{
			get;
			set;
		}
		#endregion
		#region AdjgCuryID
		public abstract class adjgCuryID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="Currency"/> of
		/// the adjusting document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="CM.CurrencyInfo.CuryID"/> field.
		/// </value>
		[PXDBString]
		[PXDefault]
		public virtual string AdjgCuryID
		{
			get;
			set;
		}
		#endregion
		#region AdjNbr
		public abstract class adjNbr : IBqlField { }
		/// <summary>
		/// The sequential number of the adjustment batch 
		/// from the parent <see cref="ARAdjust">application</see> 
		/// record. This field is part of the compound key of the 
		/// record and is part of the compound foreign key reference 
		/// to the parent <see cref="ARAdjust">application</see> record.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjNbr"/> field.
		/// </value>
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = Messages.AdjustmentNbr, Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
		public virtual int? AdjNbr
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjgAmt
		public abstract class curyAdjgAmt : IBqlField { }
		/// <summary>
		/// The application amount in document currency, from the 
		/// viewpoint of the <see cref="ARRegister">adjusting document</see>.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.CuryAdjgAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = Messages.AmountPaid, Visibility = PXUIVisibility.Visible)]
		public virtual decimal? CuryAdjgAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjgDiscAmt
		public abstract class curyAdjgDiscAmt : IBqlField { }
		/// <summary>
		/// The amount of cash discount taken in document currency, from 
		/// the viewpoint of the <see cref="ARRegister">adjusting document</see>.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.CuryAdjgDiscAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = Messages.CashDiscountTaken, Visibility = PXUIVisibility.Visible)]
		public virtual decimal? CuryAdjgDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjgWOAmt
		public abstract class curyAdjgWOAmt : IBqlField { }
		/// <summary>
		/// The amount of balance write-off in the document currency, from
		/// the viewpoint of the <see cref="ARRegister">adjusting document</see>.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.CuryAdjgWOAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = Messages.BalanceWriteOff, Visibility = PXUIVisibility.Visible)]
		public virtual decimal? CuryAdjgWOAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjdAmt
		public abstract class curyAdjdAmt : IBqlField {	}
		/// <summary>
		/// The application amount in document currency, from the 
		/// viewpoint of the <see cref="ARRegister">adjusted document</see>.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.CuryAdjdAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryAdjdAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjdDiscAmt
		public abstract class curyAdjdDiscAmt : IBqlField { }
		/// <summary>
		/// The amount of cash discount taken in document currency, from 
		/// the viewpoint of the <see cref="ARRegister">adjusted document</see>.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.CuryAdjdDiscAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = Messages.CashDiscountTaken, Visibility = PXUIVisibility.Visible)]
		public virtual decimal? CuryAdjdDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryAdjdWOAmt
		public abstract class curyAdjdWOAmt : IBqlField { }
		/// <summary>
		/// The amount of balance write-off in the document currency, from
		/// the viewpoint of the <see cref="ARRegister">adjusted document</see>.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.CuryAdjdWOAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryAdjdWOAmt
		{
			get;
			set;
		}
		#endregion
		#region AdjAmt
		public abstract class adjAmt : IBqlField { }
		/// <summary>
		/// The application amount in base currency, from the viewpoint
		/// of the <see cref="ARRegister">adjusted document</see>.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? AdjAmt
		{
			get;
			set;
		}
		#endregion
		#region AdjDiscAmt
		public abstract class adjDiscAmt : IBqlField { }
		/// <summary>
		/// The amount of cash discount taken in base currency, from 
		/// the viewpoint of the <see cref="ARRegister">adjusted 
		/// document</see>.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjDiscAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? AdjDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region AdjWOAmt
		public abstract class adjWOAmt : IBqlField { }
		/// <summary>
		/// The amount of balance write-off in base currency, from 
		/// the viewpoint of the <see cref="ARRegister">adjusted 
		/// document</see>.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.AdjWOAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? AdjWOAmt
		{
			get;
			set;
		}
		#endregion
		#region RGOLAmt
		public abstract class rGOLAmt : IBqlField { }
		/// <summary>
		/// The amount of gain or loss realized on the <see cref="ARRegister">
		/// adjusted document</see> in base currency, from the viewpoint
		/// of the adjusted document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARAdjust.RGOLAmt"/> field.
		/// </value>
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? RGOLAmt
		{
			get;
			set;
		}
		#endregion
		#region IsSelfApplyingVoidApplication
		public abstract class isSelfVoidingVoidApplication : IBqlField { }
		[PXDBBool]
		public virtual bool? IsSelfVoidingVoidApplication
		{
			get;
			set;
		}
		#endregion
		#region ReverseGainLoss
		public abstract class reverseGainLoss : IBqlField { }
		[PXBool]
		[PXDependsOnFields(typeof(adjdDocType))]
		public virtual bool? ReverseGainLoss => ARDocType.SignBalance(AdjdDocType) == -1m;
		#endregion

		#region Explicit Interface Implementations
		string IDocumentAdjustment.Module => BatchModule.AR;
		decimal? IAdjustmentAmount.AdjThirdAmount
		{
			get { return AdjWOAmt; }
			set { AdjWOAmt = value; }
		}
		decimal? IAdjustmentAmount.CuryAdjgThirdAmount
		{
			get { return CuryAdjgWOAmt; }
			set { CuryAdjgWOAmt = value; }
		}
		decimal? IAdjustmentAmount.CuryAdjdThirdAmount
		{
			get { return CuryAdjdWOAmt; }
			set { CuryAdjdWOAmt = value; }
		}
		#endregion

		#region IsIncomingApplication
		public abstract class isIncomingApplication : IBqlField { }
		/// <summary>
		/// Indicates (if set to <c>true</c>) that the application detail
		/// is incoming relative to the parent <see cref="ARStatement">
		/// Customer Statement</see> record. If and only if this flag is set
		/// to <c>true</c>, the ending balance of the parent statement 
		/// will account for the write-off, RGOL, and cash discount amounts
		/// contained in the application.
		/// </summary>
		[PXBool]
		[PXDependsOnFields(
			typeof(branchID), 
			typeof(adjdBranchID),
			typeof(curyID),
			typeof(adjdCuryID),
			typeof(customerID),
			typeof(adjdCustomerID))]
		public virtual bool? IsIncomingApplication => 
			BranchID == AdjdBranchID
			&& CuryID == AdjdCuryID
			&& CustomerID == AdjdCustomerID;
		#endregion
		#region IsOutgoingApplication
		public abstract class isOutgoingApplication : IBqlField { }
		/// <summary>
		/// Indicates (if set to <c>true</c>) that the application detail
		/// is outgoing relative to the parent <see cref="ARStatement">
		/// Customer Statement</see> record. When this flag is set
		/// to <c>true</c>, the ending balance of the parent statement will 
		/// not account for the GL amounts contained in the application,
		/// such as write-off, RGOL, and cash discount amounts.
		/// </summary>
		[PXBool]
		[PXDependsOnFields(
			typeof(branchID),
			typeof(adjgBranchID),
			typeof(curyID),
			typeof(adjgCuryID),
			typeof(customerID),
			typeof(adjgCustomerID))]
		public virtual bool? IsOutgoingApplication =>
			BranchID == AdjgBranchID
			&& CuryID == AdjgCuryID
			&& CustomerID == AdjgCustomerID;
		#endregion
		#region IsInterBranchApplication
		public abstract class isInterBranchApplication : IBqlField { }
		/// <summary>
		/// If set to <c>true</c>, indicates that the parent 
		/// <see cref="ARAdjust">application</see> affects documents 
		/// that originate in different <see cref="Branch">branches</see>.
		/// </summary>
		[PXBool]
		[PXDependsOnFields(typeof(adjgBranchID), typeof(adjdBranchID))]
		public virtual bool? IsInterBranchApplication => AdjgBranchID != AdjdBranchID;
		#endregion
		#region IsInterCurrencyApplication
		public abstract class isInterCurrencyApplication : IBqlField { }
		/// <summary>
		/// If set to <c>true</c>, indicates that the parent
		/// <see cref="ARAdjust">application</see> affects documents
		/// that have different <see cref="Currency">currencies</see>.
		/// </summary>
		[PXBool]
		[PXDependsOnFields(typeof(adjgCuryID), typeof(adjdCuryID))]
		public virtual bool? IsInterCurrencyApplication => AdjgCuryID != AdjdCuryID;
		#endregion
		#region IsInterCustomerApplication
		public abstract class isInterCustomerApplication : IBqlField { }
		/// <summary>
		/// If set to <c>true</c>, indicates that the parent
		/// <see cref="ARAdjust">application</see> affects documents
		/// that belong to different <see cref="Customer">customers</see>.
		/// </summary>
		[PXBool]
		[PXDependsOnFields(typeof(adjgCustomerID), typeof(adjdCustomerID))]
		public virtual bool? IsInterCustomerApplication => AdjgCustomerID != AdjdCustomerID;
		#endregion
		#region IsInterStatementApplication
		public abstract class isInterStatementApplication : IBqlField { }
		/// <summary>
		/// If set to <c>true</c>, indicates that the parent
		/// <see cref="ARAdjust">application</see> affects two 
		/// <see cref="ARStatement">Customer Statements</see> at once.
		/// If and only if this flag is set to <c>true</c>, the ending balance
		/// of the parent statement will account for the application amount. 
		/// </summary>
		[PXBool]
		[PXDependsOnFields(
			typeof(isInterBranchApplication),
			typeof(isInterCurrencyApplication),
			typeof(isInterCustomerApplication))]
		public virtual bool? IsInterStatementApplication =>
			IsInterBranchApplication == true
			|| IsInterCurrencyApplication == true
			|| IsInterCustomerApplication == true;
		#endregion
		#region SignBalanceDelta
		/// <summary>
		/// The sign with which the application amount affects the
		/// <see cref="ARStatement">customer statement</see> balance.
		/// </summary>
		public abstract class signBalanceDelta : IBqlField { }
		[PXDecimal]
		[PXDependsOnFields(
			typeof(isIncomingApplication),
			typeof(adjgDocType),
			typeof(adjdDocType))]
		public virtual decimal? SignBalanceDelta
		{
			get
			{
				decimal sign = IsIncomingApplication == true ? -1m : 1m;

				// For these types of applications, the balance
				// of the statement is affected in a reverse manner.
				// -
				bool flipSign =
					AdjgDocType == ARDocType.Refund
					|| AdjdDocType == ARDocType.CreditMemo
						&& (AdjgDocType == ARDocType.Payment || AdjgDocType == ARDocType.VoidPayment)
					|| AdjdDocType == ARDocType.SmallCreditWO
						&& (AdjgDocType == ARDocType.Payment || AdjgDocType == ARDocType.CreditMemo);

				return flipSign ? -sign : sign;
			}
		}
		#endregion
	}
}
