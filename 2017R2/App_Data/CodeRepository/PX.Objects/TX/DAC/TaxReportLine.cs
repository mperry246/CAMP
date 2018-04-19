namespace PX.Objects.TX
{
	using System;
	using PX.Data;
	using PX.Objects.AP;
	using PX.Objects.GL;


	/// <summary>
	/// Represents a line of a tax report. The class defines the structure of the tax report for a particular tax agency, and is a part of formation amount rules.
	/// The line is mapped as many-to-many to reporting groups(TaxBucket) and through them to taxes.
    /// </summary>
	[System.SerializableAttribute()]
	[PXCacheName(Messages.TaxReportLine)]
	public partial class TaxReportLine : PX.Data.IBqlTable
	{
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;

		/// <summary>
		/// The foreign key to <see cref="PX.Objects.AP.Vendor">, which specifies a tax agency to which the report line belongs.
		/// The field is a part of the primary key.
		/// </summary>
		[PXDBInt(IsKey = true)]
		[PXDefault]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;

		/// <summary>
		/// The number of the report line. The field is a part of the primary key.
		/// </summary>
		[PXDBInt(IsKey = true)]
		[PXLineNbr(typeof(Vendor))]
		[PXParent(typeof(Select<Vendor, Where<Vendor.bAccountID, Equal<Current<TaxReportLine.vendorID>>>>))]
		[PXUIField(DisplayName="Report Line", Visibility=PXUIVisibility.SelectorVisible, Enabled=false)]
		public virtual Int32? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region LineType
		public abstract class lineType : PX.Data.IBqlField
		{
		}
		protected String _LineType;

		/// <summary>
		/// The type of the report line, which indicates whether the tax amount or taxable amount should be used to update the line.
		/// </summary>
		/// <value>
		/// The field can have one of the following values:
		/// <c>"P"</c>: Tax amount.
		/// <c>"A"</c>: Taxable amount.
		/// </value>
		[PXDBString(1, IsFixed = true)]
		[PXDefault(TaxReportLineType.TaxAmount)]
		[PXUIField(DisplayName="Update With", Visibility=PXUIVisibility.Visible)]
		[TaxReportLineType.List]
		public virtual String LineType
		{
			get
			{
				return this._LineType;
			}
			set
			{
				this._LineType = value;
			}
		}
		#endregion
		#region LineMult
		public abstract class lineMult : PX.Data.IBqlField
		{
		}
		protected Int16? _LineMult;

		/// <summary>
		/// The rule (sign) of updating the report line.
		/// </summary>
		/// <value>
		/// The field can have one of the following values:
		/// <c>"1"</c>: +Output-Input.
		/// <c>"-1"</c>: +Input-Output.
		/// </value>
		[PXDBShort()]
		[PXDefault((short)1)]
		[PXUIField(DisplayName="Update Rule", Visibility=PXUIVisibility.Visible)]
		[PXIntList(new int[] { 1, -1 }, new string[] { "+Output-Input", "+Input-Output" })]
		public virtual Int16? LineMult
		{
			get
			{
				return this._LineMult;
			}
			set
			{
				this._LineMult = value;
			}
		}
		#endregion
		#region TaxZoneID
		public abstract class taxZoneID : PX.Data.IBqlField
		{
		}
		protected String _TaxZoneID;

		/// <summary>
		/// The foreign key to <see cref="TaxZone"/>.
		/// If the field contains NULL, the report line contains aggregated data for all tax zones.
		/// </summary>
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName="Tax Zone ID", Visibility=PXUIVisibility.Visible, Required=false)]
		[PXSelector(typeof(Search<TaxZone.taxZoneID>))]
		public virtual String TaxZoneID
		{
			get
			{
				return this._TaxZoneID;
			}
			set
			{
				this._TaxZoneID = value;
			}
		}
		#endregion
		#region NetTax
		public abstract class netTax : PX.Data.IBqlField
		{
		}
		protected Boolean? _NetTax;

		/// <summary>
		/// The field indicates (if set to <c>true</c>) that the line shows the net tax amount.
		/// </summary>
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Net Tax", Visibility = PXUIVisibility.Visible, Enabled = true)]
		public virtual Boolean? NetTax
		{
			get
			{
				return this._NetTax;
			}
			set
			{
				this._NetTax = value;
			}
		}
		#endregion
		#region TempLine
		public abstract class tempLine : PX.Data.IBqlField
		{
		}
		protected Boolean? _TempLine;

		/// <summary>
		/// Specifies (if set to <c>true</c>) that the report line should be sliced by tax zones, and that the line is parent.
		/// </summary>
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Detail by Tax Zones", Visibility = PXUIVisibility.Visible, Enabled = true)]
		public virtual Boolean? TempLine
		{
			get
			{
				return this._TempLine;
			}
			set
			{
				this._TempLine = value;
			}
		}
		#endregion
		#region TempLineNbr
		public abstract class tempLineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _TempLineNbr;

		/// <summary>
		/// The reference to the parent line (<see cref="TaxReportLine.LineNbr"/>).
		/// The child lines are created for each tax zone if the <see cref="TempLine"/> of the parent line is set to <c>true</c>.
		/// </summary>
		[PXDBInt()]
		[PXUIField(DisplayName = "Parent Line")]
		public virtual Int32? TempLineNbr
		{
			get
			{
				return this._TempLineNbr;
			}
			set
			{
				this._TempLineNbr = value;
			}
		}
		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}
		protected String _Descr;

		/// <summary>
		/// The description of the report line, which can be specified by the user.
		/// </summary>
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName="Description", Visibility=PXUIVisibility.SelectorVisible)]
		public virtual String Descr
		{
			get
			{
				return this._Descr;
			}
			set
			{
				this._Descr = value;
			}
		}
		#endregion
        #region ReportLineNbr
        public abstract class reportLineNbr : PX.Data.IBqlField
        {
        }
        protected String _ReportLineNbr;

		/// <summary>
		/// The number of the corresponding box of the original report form; the number is unique for each tax agency.
		/// </summary>
		[PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Tax Box Number", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String ReportLineNbr
        {
            get
            {
                return this._ReportLineNbr;
            }
            set
            {
                this._ReportLineNbr = value;
            }
        }
        #endregion
        #region BucketSum
        public abstract class bucketSum : PX.Data.IBqlField
        {
        }

		/// <summary>
		/// The calculation rule, which is filled in by the system automatically if the report line is an aggregate line and the appropriate settings have been specified.
		/// </summary>
		[PXString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Calc. Rule", Visibility = PXUIVisibility.SelectorVisible, Enabled=false)]
        public virtual String BucketSum { get; set; }
        #endregion
		#region HideReportLine
		public abstract class hideReportLine : PX.Data.IBqlField
		{
		}
		protected Boolean? _HideReportLine;

		/// <summary>
		/// Specifies (if set to <c>true</c>) that the line will not be included in the tax report during generation of the report.
		/// </summary>
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Hide Report Line", Visibility = PXUIVisibility.Visible, Enabled = true)]
		public virtual Boolean? HideReportLine
		{
			get
			{
				return this._HideReportLine;
			}
			set
			{
				this._HideReportLine = value;
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
	}

	public class TaxReportLineType
	{
		public const string TaxAmount = "P";
		public const string TaxableAmount = "A";
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(new string[] { TaxAmount, TaxableAmount }, new string[] { "Tax Amount", "Taxable Amount" })
			 {
			 }
		}

		public class taxAmount : Constant<string>
		{
			public taxAmount():base(TaxAmount)
			{
			}
		}
		public class taxableAmount : Constant<string>
		{
			public taxableAmount()
				: base(TaxableAmount)
			{
			}
		}

	}
}
