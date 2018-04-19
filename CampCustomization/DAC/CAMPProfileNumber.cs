using System;
using PX.Data;

namespace CAMPCustomization
{
    [Serializable]
    public class CAMPProfileNumber : IBqlTable
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

        #region ProfileNumberID
        public abstract class profileNumberID : PX.Data.IBqlField
        {
        }
        [PXDBIdentity()]
        [PXUIField(Visibility = PXUIVisibility.Invisible, Visible = false)]
        public virtual int? ProfileNumberID { get; set; }
        #endregion

        #region ProfileNumberCD
        public abstract class profileNumberCD : PX.Data.IBqlField
        {
        }
        [PXDBString(30, IsKey = true, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Profile Number")]
        public virtual string ProfileNumberCD { get; set; }
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

        #region ManufacturerID
        public abstract class manufacturerID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        // [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Manufacturer", IsReadOnly = true)]
        [PXSelector(typeof(CAMPManufacturer.manufacturerID),
            SubstituteKey = typeof(CAMPManufacturer.manufacturerCD),
            DescriptionField = typeof(CAMPManufacturer.description))]
        public virtual int? ManufacturerID { get; set; }
        #endregion

        #region TapeModelID
        public abstract class tapeModelID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Model", Required = true)]
        [PXSelector(typeof(CAMPTapeModel.tapeModelID),
            new Type[]{
                typeof(CAMPTapeModel.tapeModelCD),
                typeof(CAMPTapeModel.description)},
            SubstituteKey = typeof(CAMPTapeModel.tapeModelCD),
            DescriptionField = typeof(CAMPTapeModel.description))]
        public virtual int? TapeModelID { get; set; }
        #endregion

        #region SerialNbr
        public abstract class serialNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Serial Number")]
        public virtual string SerialNbr { get; set; }
        #endregion

        #region RegistrationNbr
        public abstract class registrationNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Registration Number")]
        public virtual string RegistrationNbr { get; set; }

        #endregion

        #region OperatingFAR
        public abstract class operatingFAR : PX.Data.IBqlField
        {
        }
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "OperatingFAR", Visible = false)]
        public virtual string OperatingFAR { get; set; }
        #endregion

        #region Status

        public abstract class status : PX.Data.IBqlField
        {
        }
        [PXDBString(1, IsUnicode = true)]
        [PXUIField(DisplayName = "Status", Visible = false)]
        public virtual string Status { get; set; }

        #endregion

        #region AutoDeactivation
        public abstract class autoDeactivation : PX.Data.IBqlField
        {
        }
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Auto Deactivation", Visible = false)]
        public virtual string AutoDeactivation { get; set; }

        #endregion

        #region AutoDeactivationDate
        public abstract class autoDeactivationDate : PX.Data.IBqlField
        {
        }
        [PXDBDate()]
        [PXUIField(DisplayName = "Last Actuals Date", Visible = false)]
        public virtual DateTime? AutoDeactivationDate { get; set; }
        #endregion

        #region ReasonforDeactivation
        public abstract class reasonforDeactivation : PX.Data.IBqlField
        {
        }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Reason for Deactivation", Visible = false)]
        public virtual string ReasonforDeactivation { get; set; }
        #endregion

        #region AnalystAssigned
        public abstract class analystAssigned : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "AnalystAssigned", Visible = false)]
        public virtual int? AnalystAssigned { get; set; }
        #endregion

        #region OwnerID
        public abstract class ownerID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "Owner", Visible = false)]
        public virtual int? OwnerID { get; set; }
        #endregion

        #region Oeid
        public abstract class oEID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "OE", Visible = false)]
        public virtual int? OEID { get; set; }
        #endregion

        #region MaintenanceID
        public abstract class maintenanceID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "Maintenance", Visible = false)]
        public virtual int? MaintenanceID { get; set; }
        #endregion

        #region OperatorID
        public abstract class operatorID : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "Operator", Visible = false)]
        public virtual int? OperatorID { get; set; }
        #endregion

        #region Camo
        public abstract class cAMO : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "CAMO", Visible = false)]
        public virtual int? CAMO { get; set; }
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



        #region Opsnbr
        public abstract class oPSNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "OPS Nbr", Visible = true)]
        public virtual string OPSNbr { get; set; }
        #endregion

    }
}