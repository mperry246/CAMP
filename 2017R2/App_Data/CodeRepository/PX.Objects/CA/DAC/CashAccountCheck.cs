using System;
using PX.Data;
using PX.Objects.AP;

namespace PX.Objects.CA
{
	[Serializable]
	[PXCacheName(Messages.CashAccountCheck)]
	public partial class CashAccountCheck : IBqlTable
	{
		#region AccountID
		public abstract class accountID : IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Cash Account ID", Visible = false)]
		public virtual int? AccountID
		{
			get;
			set;
		}
		#endregion
		#region PaymentMethodID
		public abstract class paymentMethodID : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Payment Method")]
		[PXSelector(typeof(PaymentMethod.paymentMethodID))]
		public virtual string PaymentMethodID
		{
			get;
			set;
		}
		#endregion
		#region CashAccountCheckID
		public abstract class cashAccountCheckID : IBqlField { }
		[PXDBIdentity]
		public virtual int? CashAccountCheckID
		{
			get;
			set;
		}
		#endregion
		#region CheckNbr
		public abstract class checkNbr : IBqlField { }

		[PXDBString(40, IsKey = true, IsUnicode = true)]
		[PXUIField(DisplayName = "Check Number")]
		[PXDefault]
		[PXParent(typeof(Select<APAdjust, Where<APAdjust.stubNbr, Equal<Current<CashAccountCheck.checkNbr>>,
			And<APAdjust.paymentMethodID, Equal<Current<CashAccountCheck.paymentMethodID>>,
			And<APAdjust.cashAccountID, Equal<Current<CashAccountCheck.accountID>>>>>>))]
		public virtual string CheckNbr
		{
			get;
			set;
		}
		#endregion
		#region DocType
		public abstract class docType : IBqlField
		{
		}

		[PXDBString(3, IsFixed = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Document Type", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[APDocType.List]
		public virtual string DocType
		{
			get;
			set;
		}
		#endregion
		#region RefNbr
		public abstract class refNbr : IBqlField
		{
		}

		[PXDBString(15, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual string RefNbr
		{
			get;
			set;
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : IBqlField
		{
		}
		[PXDBString(6, IsFixed = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Application Period", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[GL.FinPeriodIDFormatting]
		public virtual string FinPeriodID
		{
			get;
			set;
		}
		#endregion
		#region DocDate
		public abstract class docDate : IBqlField
		{
		}
		[PXDBDate]
		[PXDefault]
		[PXUIField(DisplayName = "Document Date", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual DateTime? DocDate
		{
			get;
			set;
		}
		#endregion
		#region VendorID
		public abstract class vendorID : IBqlField
		{
		}

		[PXDefault]
		[Vendor]
		public virtual int? VendorID
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
	}
}
