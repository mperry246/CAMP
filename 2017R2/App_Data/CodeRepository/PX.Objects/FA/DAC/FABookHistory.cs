using PX.Objects.CM;

namespace PX.Objects.FA
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	[PXCacheName(Messages.FABookHistory)]
	public partial class FABookHistory : PX.Data.IBqlTable
	{
		#region AssetID
		public abstract class assetID : PX.Data.IBqlField
		{
		}
		protected Int32? _AssetID;
		/// <summary>
		/// A reference to <see cref="FixedAsset"/>.
		/// The field is a part of the primary key.
		/// The full primary key contains <see cref="AssetID"/>, <see cref="BookID"/>, and <see cref="FinPeriodID"/> fields.
		/// </summary>
		/// <value>
		/// An integer identifier of the fixed asset. 
		/// This is a required field. 
		/// </value>
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public virtual Int32? AssetID
		{
			get
			{
				return this._AssetID;
			}
			set
			{
				this._AssetID = value;
			}
		}
		#endregion
		#region BookID
		public abstract class bookID : PX.Data.IBqlField
		{
		}
		protected Int32? _BookID;
		/// <summary>
		/// A reference to <see cref="FABook"/>.
		/// The field is a part of the primary key.
		/// The full primary key contains <see cref="AssetID"/>, <see cref="BookID"/>, and <see cref="FinPeriodID"/> fields.
		/// </summary>
		/// <value>
		/// An integer identifier of the book. 
		/// This is a required field. 
		/// </value>
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public virtual Int32? BookID
		{
			get
			{
				return this._BookID;
			}
			set
			{
				this._BookID = value;
			}
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		protected String _FinPeriodID;
		/// <summary>
		/// The financial period.
		/// The field is a part of the primary key.
		/// The full primary key contains <see cref="AssetID"/>, <see cref="BookID"/>, and <see cref="FinPeriodID"/> fields.
		/// </summary>
		/// <value>
		/// This is a required field. 
		/// </value>
		[GL.FinPeriodID(IsKey = true)]
		[PXDefault()]
		public virtual String FinPeriodID
		{
			get
			{
				return this._FinPeriodID;
			}
			set
			{
				this._FinPeriodID = value;
			}
		}
		#endregion
		#region Closed
		public abstract class closed : PX.Data.IBqlField
		{
		}
		protected Boolean? _Closed;
		/// <summary>
		/// A flag that indicates (if set to <c>true</c>) that the financial period is closed for the depreciation.
		/// </summary>
		/// <value>
		/// The financial period is closed when the depreciation amount of the fixed asset in the financial period is calculated and successfully posted to the General Ledger module.
		/// By default, the value is set to false.
		/// </value>
		[PXDBBool()]
		[PXDefault(false)]
		public virtual Boolean? Closed
		{
			get
			{
				return this._Closed;
			}
			set
			{
				this._Closed = value;
			}
		}
		#endregion
		#region Reopen
		public abstract class reopen : IBqlField {}
		/// <summary>
		/// A flag that indicates (if set to <c>true</c>) that the financial period is reopened.
		/// </summary>
		/// <value>
		/// The financial period becomes reopen, if, in the financial period, negative depreciation adjustment to zero amount has been performed manually.
		/// By default, the value is set to false.
		/// </value>
		[PXBool]
		[PXDefault(false)]
		public virtual Boolean? Reopen { get; set; }
		#endregion
		#region Suspended
		public abstract class suspended : PX.Data.IBqlField
		{
		}
		protected Boolean? _Suspended;
		/// <summary>
		/// A flag that indicates (if set to <c>true</c>) that the fixed asset is suspended in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is set to false.
		/// </value>
		[PXDBBool()]
		[PXDefault(false)]
		public virtual Boolean? Suspended
		{
			get
			{
				return this._Suspended;
			}
			set
			{
				this._Suspended = value;
			}
		}
		#endregion
        #region BegBal
		public abstract class begBal : PX.Data.IBqlField
		{
		}
		protected Decimal? _BegBal;
		/// <summary>
		/// The beginning balance (net value) of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BegBal
		{
			get
			{
				return this._BegBal;
			}
			set
			{
				this._BegBal = value;
			}
		}
		#endregion
		#region YtdBal
		public abstract class ytdBal : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdBal;
		/// <summary>
		/// The ending balance (net value) of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdBal
		{
			get
			{
				return this._YtdBal;
			}
			set
			{
				this._YtdBal = value;
			}
		}
		#endregion
		#region BegDeprBase
		public abstract class begDeprBase : PX.Data.IBqlField
		{
		}
		protected Decimal? _BegDeprBase;
		/// <summary>
		/// The beginning value of the depreciation basis of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BegDeprBase
		{
			get
			{
				return this._BegDeprBase;
			}
			set
			{
				this._BegDeprBase = value;
			}
		}
		#endregion
        #region PtdDeprBase
        public abstract class ptdDeprBase : PX.Data.IBqlField
        {
        }
        protected Decimal? _PtdDeprBase;
		/// <summary>
		/// The change of the depreciation basis of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? PtdDeprBase
        {
            get
            {
                return this._PtdDeprBase;
            }
            set
            {
                this._PtdDeprBase = value;
            }
        }
        #endregion
		#region YtdDeprBase
		public abstract class ytdDeprBase : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdDeprBase;
		/// <summary>
		/// The ending value of the depreciation basis of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdDeprBase
		{
			get
			{
				return this._YtdDeprBase;
			}
			set
			{
				this._YtdDeprBase = value;
			}
		}
		#endregion
		#region YtdAcquired
		public abstract class ytdAcquired : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdAcquired;
		/// <summary>
		/// The ending value of the acquisition amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdAcquired
		{
			get
			{
				return this._YtdAcquired;
			}
			set
			{
				this._YtdAcquired = value;
			}
		}
		#endregion
		#region PtdAcquired
		public abstract class ptdAcquired : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdAcquired;
		/// <summary>
		/// The change of the acquisition amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdAcquired
		{
			get
			{
				return this._PtdAcquired;
			}
			set
			{
				this._PtdAcquired = value;
			}
		}
		#endregion
        #region YtdReconciled
        public abstract class ytdReconciled : PX.Data.IBqlField
        {
        }
        protected Decimal? _YtdReconciled;
		/// <summary>
		/// The ending value of the GL reconciliation amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? YtdReconciled
        {
            get
            {
                return this._YtdReconciled;
            }
            set
            {
                this._YtdReconciled = value;
            }
        }
        #endregion
        #region PtdReconciled
        public abstract class ptdReconciled : PX.Data.IBqlField
        {
        }
        protected Decimal? _PtdReconciled;
		/// <summary>
		/// The change of the GL reconciliation amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? PtdReconciled
        {
            get
            {
                return this._PtdReconciled;
            }
            set
            {
                this._PtdReconciled = value;
            }
        }
        #endregion
        #region YtdDepreciated
		public abstract class ytdDepreciated : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdDepreciated;
		/// <summary>
		/// The accumulated depreciation amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdDepreciated
		{
			get
			{
				return this._YtdDepreciated;
			}
			set
			{
				this._YtdDepreciated = value;
			}
		}
		#endregion
		#region PtdDepreciated
		public abstract class ptdDepreciated : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdDepreciated;
		/// <summary>
		/// The change of the depreciation amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdDepreciated
		{
			get
			{
				return this._PtdDepreciated;
			}
			set
			{
				this._PtdDepreciated = value;
			}
		}
		#endregion
        #region PtdAdjusted
        public abstract class ptdAdjusted : PX.Data.IBqlField
        {
        }
        protected Decimal? _PtdAdjusted;
		/// <summary>
		/// The change of the depreciation amount of the fixed asset because of split of the assets in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? PtdAdjusted
        {
            get
            {
                return this._PtdAdjusted;
            }
            set
            {
                this._PtdAdjusted = value;
            }
        }
        #endregion
        #region PtdDeprDisposed
        public abstract class ptdDeprDisposed : PX.Data.IBqlField
        {
        }
        protected Decimal? _PtdDeprDisposed;
		/// <summary>
		/// The change of the depreciation amount of the fixed asset because of disposal of the assets in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? PtdDeprDisposed
        {
            get
            {
                return this._PtdDeprDisposed;
            }
            set
            {
                this._PtdDeprDisposed = value;
            }
        }
        #endregion
        #region PtdCalculated
		public abstract class ptdCalculated : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdCalculated;
		/// <summary>
		/// The calculated depreciation amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdCalculated
		{
			get
			{
				return this._PtdCalculated;
			}
			set
			{
				this._PtdCalculated = value;
			}
		}
		#endregion
		#region YtdCalculated
		public abstract class ytdCalculated : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdCalculated;
		/// <summary>
		/// The calculated depreciation amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdCalculated
		{
			get
			{
				return this._YtdCalculated;
			}
			set
			{
				this._YtdCalculated = value;
			}
		}
		#endregion
		#region YtdBonus
		public abstract class ytdBonus : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdBonus;
		/// <summary>
		/// The bonus amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdBonus
		{
			get
			{
				return this._YtdBonus;
			}
			set
			{
				this._YtdBonus = value;
			}
		}
		#endregion
		#region PtdBonus
		public abstract class ptdBonus : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdBonus;
		/// <summary>
		/// The change of the bonus amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdBonus
		{
			get
			{
				return this._PtdBonus;
			}
			set
			{
				this._PtdBonus = value;
			}
		}
		#endregion
		#region YtdBonusTaken
		public abstract class ytdBonusTaken : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdBonusTaken;
		/// <summary>
		/// The taken bonus amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdBonusTaken
		{
			get
			{
				return this._YtdBonusTaken;
			}
			set
			{
				this._YtdBonusTaken = value;
			}
		}
		#endregion
		#region PtdBonusTaken
		public abstract class ptdBonusTaken : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdBonusTaken;
		/// <summary>
		/// The taken bonus amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdBonusTaken
		{
			get
			{
				return this._PtdBonusTaken;
			}
			set
			{
				this._PtdBonusTaken = value;
			}
		}
		#endregion
		#region YtdBonusCalculated
		public abstract class ytdBonusCalculated : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdBonusCalculated;
		/// <summary>
		/// The calculated bonus amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdBonusCalculated
		{
			get
			{
				return this._YtdBonusCalculated;
			}
			set
			{
				this._YtdBonusCalculated = value;
			}
		}
		#endregion
		#region PtdBonusCalculated
		public abstract class ptdBonusCalculated : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdBonusCalculated;
		/// <summary>
		/// The calculated bonus amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdBonusCalculated
		{
			get
			{
				return this._PtdBonusCalculated;
			}
			set
			{
				this._PtdBonusCalculated = value;
			}
		}
		#endregion
		#region YtdBonusRecap
		public abstract class ytdBonusRecap : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdBonusRecap;
		/// <summary>
		/// The recaptured bonus amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdBonusRecap
		{
			get
			{
				return this._YtdBonusRecap;
			}
			set
			{
				this._YtdBonusRecap = value;
			}
		}
		#endregion
		#region PtdBonusRecap
		public abstract class ptdBonusRecap : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdBonusRecap;
		/// <summary>
		/// The recaptured bonus amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdBonusRecap
		{
			get
			{
				return this._PtdBonusRecap;
			}
			set
			{
				this._PtdBonusRecap = value;
			}
		}
		#endregion
		#region YtdTax179
		public abstract class ytdTax179 : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdTax179;
		/// <summary>
		/// The Tax 179 amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdTax179
		{
			get
			{
				return this._YtdTax179;
			}
			set
			{
				this._YtdTax179 = value;
			}
		}
		#endregion
		#region PtdTax179
		public abstract class ptdTax179 : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdTax179;
		/// <summary>
		/// The change of the Tax 179 amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdTax179
		{
			get
			{
				return this._PtdTax179;
			}
			set
			{
				this._PtdTax179 = value;
			}
		}
		#endregion
		#region YtdTax179Taken
		public abstract class ytdTax179Taken : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdTax179Taken;
		/// <summary>
		/// The taken Tax 179 amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdTax179Taken
		{
			get
			{
				return this._YtdTax179Taken;
			}
			set
			{
				this._YtdTax179Taken = value;
			}
		}
		#endregion
		#region PtdTax179Taken
		public abstract class ptdTax179Taken : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdTax179Taken;
		/// <summary>
		/// The taken Tax 179 amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdTax179Taken
		{
			get
			{
				return this._PtdTax179Taken;
			}
			set
			{
				this._PtdTax179Taken = value;
			}
		}
		#endregion
		#region YtdTax179Calculated
		public abstract class ytdTax179Calculated : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdTax179Calculated;
		/// <summary>
		/// The calculated Tax 179 amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdTax179Calculated
		{
			get
			{
				return this._YtdTax179Calculated;
			}
			set
			{
				this._YtdTax179Calculated = value;
			}
		}
		#endregion
		#region PtdTax179Calculated
		public abstract class ptdTax179Calculated : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdTax179Calculated;
		/// <summary>
		/// The calculated Tax 179 amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdTax179Calculated
		{
			get
			{
				return this._PtdTax179Calculated;
			}
			set
			{
				this._PtdTax179Calculated = value;
			}
		}
		#endregion
		#region YtdTax179Recap
		public abstract class ytdTax179Recap : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdTax179Recap;
		/// <summary>
		/// The recaptured Tax 179 amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdTax179Recap
		{
			get
			{
				return this._YtdTax179Recap;
			}
			set
			{
				this._YtdTax179Recap = value;
			}
		}
		#endregion
		#region PtdTax179Recap
		public abstract class ptdTax179Recap : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdTax179Recap;
		/// <summary>
		/// The recaptured Tax 179 amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdTax179Recap
		{
			get
			{
				return this._PtdTax179Recap;
			}
			set
			{
				this._PtdTax179Recap = value;
			}
		}
		#endregion
		#region YtdRevalueAmount
		public abstract class ytdRevalueAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdRevalueAmount;
		/// <summary>
		/// A reserved field, which is currently not used.
		/// </summary>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdRevalueAmount
		{
			get
			{
				return this._YtdRevalueAmount;
			}
			set
			{
				this._YtdRevalueAmount = value;
			}
		}
		#endregion
		#region PtdRevalueAmount
		public abstract class ptdRevalueAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdRevalueAmount;
		/// <summary>
		/// A reserved field, which is currently not used.
		/// </summary>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdRevalueAmount
		{
			get
			{
				return this._PtdRevalueAmount;
			}
			set
			{
				this._PtdRevalueAmount = value;
			}
		}
		#endregion
		#region YtdDisposalAmount
		public abstract class ytdDisposalAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdDisposalAmount;
		/// <summary>
		/// The sale amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdDisposalAmount
		{
			get
			{
				return this._YtdDisposalAmount;
			}
			set
			{
				this._YtdDisposalAmount = value;
			}
		}
		#endregion
		#region PtdDisposalAmount
		public abstract class ptdDisposalAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdDisposalAmount;
		/// <summary>
		/// The sale amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdDisposalAmount
		{
			get
			{
				return this._PtdDisposalAmount;
			}
			set
			{
				this._PtdDisposalAmount = value;
			}
		}
		#endregion
		#region YtdRGOL
		public abstract class ytdRGOL : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdRGOL;
		/// <summary>
		/// The positive gain or negative loss amount of the fixed asset at the end of the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdRGOL
		{
			get
			{
				return this._YtdRGOL;
			}
			set
			{
				this._YtdRGOL = value;
			}
		}
		#endregion
		#region PtdRGOL
		public abstract class ptdRGOL : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdRGOL;
		/// <summary>
		/// The positive gain or negative loss amount of the fixed asset in the financial period.
		/// </summary>
		/// <value>
		/// By default, the value is 0.0.
		/// </value>
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdRGOL
		{
			get
			{
				return this._PtdRGOL;
			}
			set
			{
				this._PtdRGOL = value;
			}
		}
		#endregion
		#region YtdSuspended
		public abstract class ytdSuspended : PX.Data.IBqlField
		{
		}
		protected Int32? _YtdSuspended;
		/// <summary>
		/// The service field.
		/// </summary>
		/// <value>
		/// The number of financial periods at the end of which the fixed asset has been suspended.
		/// </value>
		[PXDBInt()]
		[PXDefault(0)]
		public virtual Int32? YtdSuspended
		{
			get
			{
				return this._YtdSuspended;
			}
			set
			{
				this._YtdSuspended = value;
			}
		}
		#endregion
		#region YtdReversed
		public abstract class ytdReversed : PX.Data.IBqlField
		{
		}
		protected Int32? _YtdReversed;
		/// <summary>
		/// The service field.
		/// </summary>
		/// <value>
		/// The number of financial periods at the end of which the fixed asset has been reversed from suspend.
		/// </value>
		[PXDBInt()]
		[PXDefault(0)]
		public virtual Int32? YtdReversed
		{
			get
			{
				return this._YtdReversed;
			}
			set
			{
				this._YtdReversed = value;
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
	}

	[Serializable()]
    [PXHidden]
	public partial class FABookHistory2 : FABookHistory
	{
		#region AssetID
		public new abstract class assetID : PX.Data.IBqlField
		{
		}
		#endregion
		#region BookID
		public new abstract class bookID : PX.Data.IBqlField
		{
		}
		#endregion
		#region FinPeriodID
		public new abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		#endregion
		#region Closed
		public new abstract class closed : PX.Data.IBqlField
		{
		}
		#endregion
		#region Suspended
		public new abstract class suspended : PX.Data.IBqlField
		{
		}
		#endregion
	}

	[PXProjection(typeof(Select5<FABookHistory, 
		InnerJoin<FABookHistory2, 
			On<FABookHistory2.assetID, Equal<FABookHistory.assetID>, 
			And<FABookHistory2.bookID, Equal<FABookHistory.bookID>, 
			And<FABookHistory2.finPeriodID, GreaterEqual<FABookHistory.finPeriodID>, 
			And<FABookHistory2.suspended, Equal<False>>>>>>,
		Aggregate<
			GroupBy<FABookHistory.assetID,
			GroupBy<FABookHistory.bookID,
			GroupBy<FABookHistory.finPeriodID,
			Min<FABookHistory2.finPeriodID,
			Max<FABookHistory.ptdDeprBase,
            Max<FABookHistory.ptdAdjusted>>>>>>>>))]
    [Serializable]
    [PXHidden]
	public partial class FABookHistoryNextPeriod : IBqlTable
	{
		#region AssetID
		public abstract class assetID : PX.Data.IBqlField
		{
		}
		protected Int32? _AssetID;
		[PXDBInt(IsKey = true, BqlField = typeof(FABookHistory.assetID))]
		[PXDefault()]
		public virtual Int32? AssetID
		{
			get
			{
				return this._AssetID;
			}
			set
			{
				this._AssetID = value;
			}
		}
		#endregion
		#region BookID
		public abstract class bookID : PX.Data.IBqlField
		{
		}
		protected Int32? _BookID;
		[PXDBInt(IsKey = true, BqlField = typeof(FABookHistory.bookID))]
		[PXDefault()]
		public virtual Int32? BookID
		{
			get
			{
				return this._BookID;
			}
			set
			{
				this._BookID = value;
			}
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		protected String _FinPeriodID;
		[PXDBString(6, IsKey = true, IsFixed = true, BqlField = typeof(FABookHistory.finPeriodID))]
		[PXDefault()]
		public virtual String FinPeriodID
		{
			get
			{
				return this._FinPeriodID;
			}
			set
			{
				this._FinPeriodID = value;
			}
		}
		#endregion
		#region NextPeriodID
		public abstract class nextPeriodID : PX.Data.IBqlField
		{
		}
		protected String _NextPeriodID;
		[PXDBString(6, IsFixed = true, BqlField=typeof(FABookHistory2.finPeriodID))]
		[PXDefault()]
		public virtual String NextPeriodID
		{
			get
			{
				return this._NextPeriodID;
			}
			set
			{
				this._NextPeriodID = value;
			}
		}
		#endregion
		#region PtdDeprBase
		public abstract class ptdDeprBase : PX.Data.IBqlField
		{
		}
		protected Decimal? _PtdDeprBase;
		[PXDBBaseCury(BqlField = typeof(FABookHistory.ptdDeprBase))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? PtdDeprBase
		{
			get
			{
				return this._PtdDeprBase;
			}
			set
			{
				this._PtdDeprBase = value;
			}
		}
		#endregion
        #region PtdAdjusted
        public abstract class ptdAdjusted : PX.Data.IBqlField
        {
        }
        protected Decimal? _PtdAdjusted;
        [PXDBBaseCury(BqlField = typeof(FABookHistory.ptdAdjusted))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? PtdAdjusted
        {
            get
            {
                return this._PtdAdjusted;
            }
            set
            {
                this._PtdAdjusted = value;
            }
        }
        #endregion
    }
	
    [PXProjection(typeof(Select4<FABookHistory, 
        Aggregate<GroupBy<FABookHistory.assetID, GroupBy<FABookHistory.bookID, Max<FABookHistory.finPeriodID>>>>>))]
    [Serializable]
    [PXHidden]
    public partial class FABookHistoryMax : IBqlTable
    {
        #region AssetID
        public abstract class assetID : PX.Data.IBqlField
        {
        }
        protected Int32? _AssetID;
        [PXDBInt(IsKey = true, BqlField = typeof(FABookHistory.assetID))]
        [PXDefault()]
        public virtual Int32? AssetID
        {
            get
            {
                return this._AssetID;
            }
            set
            {
                this._AssetID = value;
            }
        }
        #endregion
        #region BookID
        public abstract class bookID : PX.Data.IBqlField
        {
        }
        protected Int32? _BookID;
        [PXDBInt(IsKey = true, BqlField = typeof(FABookHistory.bookID))]
        [PXDefault()]
        public virtual Int32? BookID
        {
            get
            {
                return this._BookID;
            }
            set
            {
                this._BookID = value;
            }
        }
        #endregion
        #region FinPeriodID
        public abstract class finPeriodID : PX.Data.IBqlField
        {
        }
        protected String _FinPeriodID;
        [GL.FinPeriodID(BqlField = typeof(FABookHistory.finPeriodID))]
        [PXDefault()]
        public virtual String FinPeriodID
        {
            get
            {
                return this._FinPeriodID;
            }
            set
            {
                this._FinPeriodID = value;
            }
        }
        #endregion
    }

	[PXProjection(typeof(Select2<FABookHistoryMax,
        InnerJoin<FABookHistory, On<FABookHistoryMax.assetID, Equal<FABookHistory.assetID>, And<FABookHistoryMax.bookID, Equal<FABookHistory.bookID>, And<FABookHistoryMax.finPeriodID, Equal<FABookHistory.finPeriodID>>>>,
		InnerJoin<FABook, On<FABook.bookID, Equal<FABookHistory.bookID>>>>>))]
    [Serializable]
	[PXCacheName(Messages.FABookHistoryRecon)]
	public partial class FABookHistoryRecon : IBqlTable
	{ 
		#region AssetID
		public abstract class assetID : PX.Data.IBqlField
		{
		}
		protected Int32? _AssetID;
		[PXDBInt(IsKey = true, BqlField = typeof(FABookHistory.assetID))]
		[PXDefault()]
		public virtual Int32? AssetID
		{
			get
			{
				return this._AssetID;
			}
			set
			{
				this._AssetID = value;
			}
		}
		#endregion
		#region BookID
		public abstract class bookID : PX.Data.IBqlField
		{
		}
		protected Int32? _BookID;
		[PXDBInt(IsKey = true, BqlField = typeof(FABookHistory.bookID))]
		[PXDefault()]
		public virtual Int32? BookID
		{
			get
			{
				return this._BookID;
			}
			set
			{
				this._BookID = value;
			}
		}
		#endregion
		#region UpdateGL
		public abstract class updateGL : PX.Data.IBqlField
		{
		}
		protected Boolean? _UpdateGL;
		[PXDBBool(BqlField = typeof(FABook.updateGL))]
		public virtual Boolean? UpdateGL
		{
			get
			{
				return this._UpdateGL;
			}
			set
			{
				this._UpdateGL = value;
			}
		}
		#endregion
		#region YtdAcquired
		public abstract class ytdAcquired : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdAcquired;
		[PXDBBaseCury(BqlField = typeof(FABookHistory.ytdAcquired))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdAcquired
		{
			get
			{
				return this._YtdAcquired;
			}
			set
			{
				this._YtdAcquired = value;
			}
		}
		#endregion
		#region YtdReconciled
		public abstract class ytdReconciled : PX.Data.IBqlField
		{
		}
		protected Decimal? _YtdReconciled;
		[PXDBBaseCury(BqlField = typeof(FABookHistory.ytdReconciled))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? YtdReconciled
		{
			get
			{
				return this._YtdReconciled;
			}
			set
			{
				this._YtdReconciled = value;
			}
		}
		#endregion
    }

	[Serializable]
	[PXCacheName(Messages.FABookHistoryByPeriod)]
	[PXProjection(typeof(Select5<FABookHistory,
		InnerJoin<FABookPeriod, 
			On<FABookPeriod.bookID, Equal<FABookHistory.bookID>,
				And<FABookPeriod.finPeriodID, GreaterEqual<FABookHistory.finPeriodID>>>>,
		Where<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>, 
		Aggregate<
			GroupBy<FABookHistory.assetID,
			GroupBy<FABookHistory.bookID,
			GroupBy<FABookPeriod.finPeriodID,
			Max<FABookHistory.finPeriodID>>>>>>))]
	public partial class FABookHistoryByPeriod : IBqlTable
	{
		#region AssetID
		public abstract class assetID : IBqlField {}
		[PXDBInt(IsKey = true, BqlField = typeof(FABookHistory.assetID))]
		public virtual int? AssetID { get; set; }
		#endregion

		#region BookID
		public abstract class bookID : IBqlField {}
		[PXDBInt(IsKey = true, BqlField = typeof(FABookHistory.bookID))]
		public virtual int? BookID { get; set; }
		#endregion

		#region FinPeriodID
		public abstract class finPeriodID : IBqlField {}
		[GL.FinPeriodID(IsKey = true, BqlField = typeof(FABookPeriod.finPeriodID))]
		public virtual string FinPeriodID { get; set; }
		#endregion

		#region LastActivityPeriod
		public abstract class lastActivityPeriod : IBqlField {}
		[GL.FinPeriodID(BqlField = typeof(FABookHistory.finPeriodID))]
		[PXUIField(DisplayName = "Last Activity Period", Visibility = PXUIVisibility.Invisible)]
		public virtual string LastActivityPeriod { get; set; }
		#endregion
	}
}
