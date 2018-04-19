namespace PX.Objects.TX
{
	using System;
	using PX.Data;
	using PX.Data.Maintenance;
	using PX.Objects.GL;
	
	[System.SerializableAttribute()]
	public partial class TXAvalaraSetup : PX.Data.IBqlTable
	{
		#region IsActive
		public abstract class isActive : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsActive;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Active")]
		public virtual Boolean? IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				this._IsActive = value;
			}
		}
		#endregion
		#region Account
		public abstract class account : PX.Data.IBqlField
		{
		}
		protected String _Account;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Account")]
		public virtual String Account
		{
			get
			{
				return this._Account;
			}
			set
			{
				this._Account = value;
			}
		}
		#endregion
		#region Licence
		public abstract class licence : PX.Data.IBqlField
		{
		}
		protected String _Licence;
		[PXDBString(30, IsUnicode = true)]
        [PXUIField(DisplayName = "License Key")]
		public virtual String Licence
		{
			get
			{
				return this._Licence;
			}
			set
			{
				this._Licence = value;
			}
		}
		#endregion
		#region Url
		public abstract class url : PX.Data.IBqlField
		{
		}
		protected String _Url;
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "URL")]
		public virtual String Url
		{
			get
			{
				return this._Url;
			}
			set
			{
				this._Url = value;
			}
		}
		#endregion
		#region SendRevenueAccount
		public abstract class sendRevenueAccount : PX.Data.IBqlField
		{
		}
		protected Boolean? _SendRevenueAccount;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Send Sales Account to AvaTax")]
		public virtual Boolean? SendRevenueAccount
		{
			get
			{
				return this._SendRevenueAccount;
			}
			set
			{
				this._SendRevenueAccount = value;
			}
		}
		#endregion
		#region ShowAllWarnings
		public abstract class showAllWarnings : PX.Data.IBqlField
		{
		}
		protected Boolean? _ShowAllWarnings;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Display all warning messages")]
		public virtual Boolean? ShowAllWarnings
		{
			get
			{
				return this._ShowAllWarnings;
			}
			set
			{
				this._ShowAllWarnings = value;
			}
		}
		#endregion
		#region EnableLogging
		public abstract class enableLogging : PX.Data.IBqlField
		{
		}
		protected Boolean? _EnableLogging;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Enable Logging")]
		public virtual Boolean? EnableLogging
		{
			get
			{
				return this._EnableLogging;
			}
			set
			{
				this._EnableLogging = value;
			}
		}
		#endregion
		#region Timeout
		public abstract class timeout : PX.Data.IBqlField
		{
		}
		protected Int32? _Timeout;
		[PXDBInt()]
		[PXDefault(30)]
		[PXUIField(DisplayName = "Request Timeout (sec.)")]
		public virtual Int32? Timeout
		{
			get
			{
				return this._Timeout;
			}
			set
			{
				this._Timeout = value;
			}
		}
		#endregion
		#region DisableTaxCalculation
		public abstract class disableTaxCalculation : PX.Data.IBqlField
		{
		}
		protected Boolean? _DisableTaxCalculation;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Disable Tax Calculation")]
		public virtual Boolean? DisableTaxCalculation
		{
			get
			{
				return this._DisableTaxCalculation;
			}
			set
			{
				this._DisableTaxCalculation = value;
			}
		}
		#endregion
		#region AlwaysCheckAddress
		public abstract class alwaysCheckAddress : PX.Data.IBqlField
		{
		}
		protected Boolean? _AlwaysCheckAddress;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Always check address before calculating tax")]
		public virtual Boolean? AlwaysCheckAddress
		{
			get
			{
				return this._AlwaysCheckAddress;
			}
			set
			{
				this._AlwaysCheckAddress = value;
			}
		}
		#endregion
		#region ShowTaxDetails
		public abstract class showTaxDetails : PX.Data.IBqlField
		{
		}
		protected Boolean? _ShowTaxDetails;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Show Tax Details")]
		public virtual Boolean? ShowTaxDetails
		{
			get
			{
				return this._ShowTaxDetails;
			}
			set
			{
				this._ShowTaxDetails = value;
			}
		}
		#endregion
		#region DisableAddressValidation
		public abstract class disableAddressValidation : PX.Data.IBqlField
		{
		}
		protected Boolean? _DisableAddressValidation;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Disable address validation")]
		public virtual Boolean? DisableAddressValidation
		{
			get
			{
				return this._DisableAddressValidation;
			}
			set
			{
				this._DisableAddressValidation = value;
			}
		}
		#endregion
		#region AddressInUppercase
		public abstract class addressInUppercase : PX.Data.IBqlField
		{
		}
		protected Boolean? _AddressInUppercase;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Return results in uppercase")]
		public virtual Boolean? AddressInUppercase
		{
			get
			{
				return this._AddressInUppercase;
			}
			set
			{
				this._AddressInUppercase = value;
			}
		}
		#endregion
		#region IsInclusiveTax
		public abstract class isInclusiveTax : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsInclusiveTax;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Inclusive Tax")]
		public virtual Boolean? IsInclusiveTax
		{
			get
			{
				return this._IsInclusiveTax;
			}
			set
			{
				this._IsInclusiveTax = value;
			}
		}
		#endregion
		#region System Columns
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		protected Byte[] _tstamp;
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get
			{
				return this._tstamp;
			}
			set
			{
				this._tstamp = value;
			}
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected String _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime
		{
			get
			{
				return this._CreatedDateTime;
			}
			set
			{
				this._CreatedDateTime = value;
			}
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected String _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
		#endregion
	}

	public static class TXAvalaraCustomerUsageType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
					new[]
					{
						Pair(FederalGovt, Messages.FederalGovt),
						Pair(StateLocalGovt, Messages.StateLocalGovt),
						Pair(TribalGovt, Messages.TribalGovt),
						Pair(ForeignDiplomat, Messages.ForeignDiplomat),
						Pair(CharitableOrg, Messages.CharitableOrg),
						Pair(Religious, Messages.Religious),
						Pair(Resale, Messages.Resale),
						Pair(AgriculturalProd, Messages.AgriculturalProd),
						Pair(IndustrialProd, Messages.IndustrialProd),
						Pair(DirectPayPermit, Messages.DirectPayPermit),
						Pair(DirectMail, Messages.DirectMail),
						Pair(Other, Messages.Other),
						Pair(Education, Messages.Education),
						Pair(LocalGovt, Messages.LocalGovt),
						Pair(ComAquaculture, Messages.ComAquaculture),
						Pair(ComFishery, Messages.ComFishery),
						Pair(NonResident, Messages.NonResident)
					}) { }
		}

		public const string FederalGovt = "A";
		public const string StateLocalGovt = "B";
		public const string TribalGovt = "C";
		public const string ForeignDiplomat = "D";
		public const string CharitableOrg = "E";
		public const string Religious = "F";
		public const string Resale = "G";
		public const string AgriculturalProd = "H";
		public const string IndustrialProd = "I";
		public const string DirectPayPermit = "J";
		public const string DirectMail = "K";
		public const string Other = "L";
		public const string Education = "M";
		public const string LocalGovt = "N";
		public const string ComAquaculture = "P";
		public const string ComFishery = "Q";
		public const string NonResident = "R";

		public const string A = FederalGovt;
		public const string B = StateLocalGovt;
		public const string C = TribalGovt;
		public const string D = ForeignDiplomat;
		public const string E = CharitableOrg;
		public const string F = Religious;
		public const string G = Resale;
		public const string H = AgriculturalProd;
		public const string I = IndustrialProd;
		public const string J = DirectPayPermit;
		public const string K = DirectMail;
		public const string L = Other;
		public const string M = Education;
		public const string N = LocalGovt;
		public const string P = ComAquaculture;
		public const string Q = ComFishery;
		public const string R = NonResident;
	}
}
