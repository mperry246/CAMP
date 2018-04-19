using System;
using PX.Data;

namespace CAMPCustomization
{
    [System.SerializableAttribute()]
    [PXPrimaryGraph(typeof(CAMPContractTypeMaint))]
    public class CAMPContractType : PX.Data.IBqlTable
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

        #region ContractTypeID
        public abstract class contractTypeID : PX.Data.IBqlField
        {
        }
        [PXDBIdentity()]
        [PXUIField(Visibility = PXUIVisibility.Invisible, Visible = false)]
        public virtual int? ContractTypeID { get; set; }

        #endregion
        #region ContractTypeCD
        public abstract class contractTypeCD : PX.Data.IBqlField
        {
        }
        [PXDBString(15, IsKey = true, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "ContractType ID")]
        public virtual string ContractTypeCD { get; set; }

        #endregion
        #region Description
        public abstract class description : PX.Data.IBqlField
        {
        }
        protected string _Description;
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
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

        #region UpdateContractType
        public abstract class updateContractType : PX.Data.IBqlField
        {
        }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Update Contract Type")]
        public virtual bool? UpdateContractType { get; set; }

        #endregion

        #region NextContractTypeID
        public abstract class nextContractTypeID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXSelector(typeof(CAMPContractType.contractTypeID),
            SubstituteKey = typeof(CAMPContractType.contractTypeCD),
            DescriptionField = typeof(CAMPContractType.description))]
        [PXUIField(DisplayName = "Next Contract Type")]
        public virtual int? NextContractTypeID { get; set; }
        #endregion





        #region User00
        public abstract class user00 : PX.Data.IBqlField
        {
        }
        protected string _User00;
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "User00")]
        public virtual string User00
        {
            get
            {
                return this._User00;
            }
            set
            {
                this._User00 = value;
            }
        }
        #endregion
        #region User01
        public abstract class user01 : PX.Data.IBqlField
        {
        }
        protected string _User01;
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "User01")]
        public virtual string User01
        {
            get
            {
                return this._User01;
            }
            set
            {
                this._User01 = value;
            }
        }
        #endregion
        #region User02
        public abstract class user02 : PX.Data.IBqlField
        {
        }
        protected string _User02;
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "User02")]
        public virtual string User02
        {
            get
            {
                return this._User02;
            }
            set
            {
                this._User02 = value;
            }
        }
        #endregion
        #region UserNbr00
        public abstract class userNbr00 : PX.Data.IBqlField
        {
        }
        protected int? _UserNbr00;
        [PXDBInt()]
        [PXUIField(DisplayName = "UserNbr00")]
        public virtual int? UserNbr00
        {
            get
            {
                return this._UserNbr00;
            }
            set
            {
                this._UserNbr00 = value;
            }
        }
        #endregion
        #region UserNbr01
        public abstract class userNbr01 : PX.Data.IBqlField
        {
        }
        protected int? _UserNbr01;
        [PXDBInt()]
        [PXUIField(DisplayName = "UserNbr01")]
        public virtual int? UserNbr01
        {
            get
            {
                return this._UserNbr01;
            }
            set
            {
                this._UserNbr01 = value;
            }
        }
        #endregion
        #region UserNbr02
        public abstract class userNbr02 : PX.Data.IBqlField
        {
        }
        protected int? _UserNbr02;
        [PXDBInt()]
        [PXUIField(DisplayName = "UserNbr02")]
        public virtual int? UserNbr02
        {
            get
            {
                return this._UserNbr02;
            }
            set
            {
                this._UserNbr02 = value;
            }
        }
        #endregion
        #region UserDate00
        public abstract class userDate00 : PX.Data.IBqlField
        {
        }
        protected DateTime? _UserDate00;
        [PXDBDate()]
        [PXUIField(DisplayName = "UserDate00")]
        public virtual DateTime? UserDate00
        {
            get
            {
                return this._UserDate00;
            }
            set
            {
                this._UserDate00 = value;
            }
        }
        #endregion
        #region UserDate01
        public abstract class userDate01 : PX.Data.IBqlField
        {
        }
        protected DateTime? _UserDate01;
        [PXDBDate()]
        [PXUIField(DisplayName = "UserDate01")]
        public virtual DateTime? UserDate01
        {
            get
            {
                return this._UserDate01;
            }
            set
            {
                this._UserDate01 = value;
            }
        }
        #endregion
        #region UserDate02
        public abstract class userDate02 : PX.Data.IBqlField
        {
        }
        protected DateTime? _UserDate02;
        [PXDBDate()]
        [PXUIField(DisplayName = "UserDate02")]
        public virtual DateTime? UserDate02
        {
            get
            {
                return this._UserDate02;
            }
            set
            {
                this._UserDate02 = value;
            }
        }
        #endregion

        #region tstamp
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