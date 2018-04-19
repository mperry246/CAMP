using System;
using PX.Data;

namespace CAMPCustomization
{
    [Serializable]
    public class CAMPSingleProfileDocuments : IBqlTable
    {
        #region BranchID
        [PXDBInt()]
        [PXUIField(DisplayName = "Branch ID")]
        public virtual int? BranchID { get; set; }
        public abstract class branchID : IBqlField { }
        #endregion

        #region DocType
        [PXDBString(3, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Doc Type")]
        public virtual string DocType { get; set; }
        public abstract class docType : IBqlField { }
        #endregion

        #region RefNbr
        [PXDBString(15, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Ref Nbr")]
        public virtual string RefNbr { get; set; }
        public abstract class refNbr : IBqlField { }
        #endregion

        #region Profile Nbr
        [PXDBString(50, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Profile  Nbr")]
        public virtual string ProfileNbr { get; set; }
        public abstract class profileNbr : IBqlField { }
        #endregion

        #region Manufacturer
        [PXDBString(60, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Manufacturer")]
        public virtual string Manufacturer { get; set; }
        public abstract class manufacturer : IBqlField { }
        #endregion

        #region Model
        [PXDBString(200, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Model")]
        public virtual string Model { get; set; }
        public abstract class model : IBqlField { }
        #endregion

        #region Serial Nbr
        [PXDBString(200, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Serial  Nbr")]
        public virtual string SerialNbr { get; set; }
        public abstract class serialNbr : IBqlField { }
        #endregion

        #region Registration Nbr
        [PXDBString(200, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Registration  Nbr")]
        public virtual string RegistrationNbr { get; set; }
        public abstract class registrationNbr : IBqlField { }
        #endregion

        #region EHM Registration Nbr
        [PXDBString(100, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "EHM  Registration  Nbr")]
        public virtual string EHMRegistrationNbr { get; set; }
        public abstract class eHMRegistrationNbr : IBqlField { }
        #endregion

        #region Profile_Count
        [PXDBInt()]
        [PXUIField(DisplayName = "Profile_ Count")]
        public virtual int? Profile_Count { get; set; }
        public abstract class profile_Count : IBqlField { }
        #endregion
    }
}