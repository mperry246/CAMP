using System;
using PX.Data;

namespace CAMPCustomization
{
    [Serializable]
    public class CAMPDiscountReasonCode : IBqlTable
    {
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }
        protected int? _BranchID;
        [PX.Objects.GL.Branch(typeof(Search<PX.Objects.GL.Branch.branchID,
            Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>), IsDetail = false)]
        [PXUIField(DisplayName = "Branch")]
        [PXSelector(typeof(PX.SM.Branch.branchID), SubstituteKey = typeof(PX.SM.Branch.branchCD))]
        public virtual int? BranchID
        {
            get
            {
                return this._BranchID;
            }
            set
            {
                this._BranchID = value;
            }
        }
        #endregion

        #region DiscountReasonID
        public abstract class discountReasonID : PX.Data.IBqlField
        {
        }
        [PXDBIdentity()]
        [PXUIField(Visibility = PXUIVisibility.Invisible, Visible = false)]
        public virtual int? DiscountReasonID { get; set; }

        #endregion

        #region DiscountReasonCD
        public abstract class discountReasonCD : PX.Data.IBqlField
        {
        }
        [PXDBString(15, IsKey = true, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Discount ReasonID", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string DiscountReasonCD { get; set; }

        #endregion

        #region Description
        public abstract class description : PX.Data.IBqlField
        {
        }
        protected string _Description;
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
        #endregion

        #region DefaultPct
        public abstract class defaultPct : PX.Data.IBqlField
        {
        }
        [PXDBDecimal(6, MinValue = -100, MaxValue = 100)]
        [PXUIField(DisplayName = "Default Percent")]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? DefaultPct { get; set; }
        #endregion

        #region MaxPct
        public abstract class maxPct : PX.Data.IBqlField
        {
        }
        [PXDBDecimal(6, MinValue = -100, MaxValue = 100)]
        [PXUIField(DisplayName = "Max Percent")]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? MaxPct { get; set; }

        #endregion

        #region AllowChanges
        public abstract class allowChanges : PX.Data.IBqlField
        {
        }
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Allow Changes", Visibility = PXUIVisibility.Visible)]
        public virtual bool? AllowChanges { get; set; }
        #endregion

        #region AllowOverMax
        public abstract class allowOverMax : PX.Data.IBqlField
        {
        }
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Allow Over Max", Visibility = PXUIVisibility.Visible)]
        public virtual bool? AllowOverMax { get; set; }
        #endregion


        #region Tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }
        protected byte[] _tstamp;
        [PXDBTimestamp()]
        public virtual byte[] tstamp
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
        protected string _CreatedByScreenID;
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID
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
        protected string _LastModifiedByScreenID;
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID
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
}
