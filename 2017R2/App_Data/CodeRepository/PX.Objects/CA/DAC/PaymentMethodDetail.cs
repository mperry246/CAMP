using System;
using PX.Data;

namespace PX.Objects.CA
{
	[Serializable]
	[PXCacheName(Messages.PaymentMethodDetail)]
	public partial class PaymentMethodDetail : IBqlTable
	{
		#region PaymentMethodID
		public abstract class paymentMethodID : IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault(typeof(PaymentMethod.paymentMethodID))]
		[PXUIField(DisplayName = "Payment Method", Visible = false)]
		[PXParent(typeof(Select<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<PaymentMethodDetail.paymentMethodID>>>>))]
		public virtual string PaymentMethodID
			{
			get;
			set;
		}
		#endregion
		#region UseFor
		public abstract class useFor : IBqlField
		{
		}
		[PXDBString(1, IsFixed = true, IsKey = true)]
		[PXDefault(PaymentMethodDetailUsage.UseForAll)]
		[PXUIField(DisplayName = "Used In")]
		public virtual string UseFor
		{
			get;
			set;
		}
		#endregion		

		#region DetailID
		public abstract class detailID : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "ID", Visible = true)]
		public virtual string DetailID
			{
			get;
			set;
		}
		#endregion

		#region Descr
		public abstract class descr : IBqlField
		{
		}

		[PXDBLocalizableString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description")]
		public virtual string Descr
			{
			get;
			set;
		}
		#endregion
		#region EntryMask
		public abstract class entryMask : IBqlField
		{
		}
		[PXDBString(255)]
		[PXUIField(DisplayName = "Entry Mask")]
		public virtual string EntryMask
		{
			get;
			set;
		}
		#endregion
		#region ValidRegexp
		public abstract class validRegexp : IBqlField
		{
		}
		[PXDBString(255)]
		[PXUIField(DisplayName = "Validation Reg. Exp.")]
		public virtual string ValidRegexp
		{
			get;
			set;
		}
		#endregion
		#region DisplayMask
		public abstract class displayMask : IBqlField
		{
		}
		[PXDBString(255)]
		[PXUIField(DisplayName = "Display Mask", Enabled = false)]
		public virtual string DisplayMask
		{
			get;
			set;
		}
		#endregion
		#region IsEncrypted
		public abstract class isEncrypted : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Encrypted")]
		public virtual bool? IsEncrypted
			{
			get;
			set;
		}
		#endregion
		#region IsRequired
		public abstract class isRequired : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Required")]
		public virtual bool? IsRequired
			{
			get;
			set;
		}
		#endregion
		#region IsIdentifier
		public abstract class isIdentifier : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Card/Account No")]
		[Common.UniqueBool(typeof(PaymentMethodDetail.paymentMethodID))]
		public virtual bool? IsIdentifier
			{
			get;
			set;
		}
			#endregion
		#region IsExpirationDate
		public abstract class isExpirationDate : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Exp. Date")]
		[Common.UniqueBool(typeof(PaymentMethodDetail.paymentMethodID))]
		public virtual bool? IsExpirationDate
			{
			get;
			set;
		}
		#endregion
		#region IsOwnerName
		public abstract class isOwnerName : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Name on Card")]
		[Common.UniqueBool(typeof(PaymentMethodDetail.paymentMethodID))]
		public virtual bool? IsOwnerName
			{
			get;
			set;
		}
		#endregion
		#region IsCCProcessingID
		public abstract class isCCProcessingID : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Payment Profile ID")]
		[Common.UniqueBool(typeof(PaymentMethodDetail.paymentMethodID))]
		public virtual bool? IsCCProcessingID
		{
			get;
			set;
		}
		#endregion
		#region IsCVV
		public abstract class isCVV : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsCVV;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "CVV Code")]
		[Common.UniqueBool(typeof(PaymentMethodDetail.paymentMethodID))]
		public virtual Boolean? IsCVV
		{
			get
			{
				return this._IsCVV;
			}
			set
			{
				this._IsCVV = value;
			}
		}
		#endregion
		#region OrderIndex
		public abstract class orderIndex : IBqlField
		{
			}
		[PXDBShort]
		[PXUIField(DisplayName = "Sort Order")]
		public virtual short? OrderIndex
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
	}

	public class PaymentMethodDetailUsage
	{

		public const string UseForVendor = "V";
		public const string UseForCashAccount = "C";
		public const string UseForAll = "A";
		public const string UseForARCards = "R";
		public const string UseForAPCards = "P";


		public class useForVendor : Constant<string>
		{
			public useForVendor() : base(UseForVendor) { }
		}

		public class useForCashAccount : Constant<string>
		{
			public useForCashAccount() : base(UseForCashAccount) { }
		}

		public class useForAll : Constant<string>
		{
			public useForAll() : base(UseForAll) { }
		}

		public class useForARCards : Constant<string>
		{
			public useForARCards() : base(UseForARCards) { }
		}

		public class useForAPCards : Constant<string>
		{
			public useForAPCards() : base(UseForAPCards) { }
		}

	}
}
