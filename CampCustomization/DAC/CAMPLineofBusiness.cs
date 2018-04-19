using System;
using PX.Data;

namespace CAMPCustomization
{
    [System.SerializableAttribute()]
    [PXPrimaryGraph(typeof(CAMPLineofBusinessMaint))]
    public class CAMPLineofBusiness : PX.Data.IBqlTable
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

        #region LineofBusinessID
        public abstract class lineofBusinessID : PX.Data.IBqlField
        {
        }
        [PXDBIdentity()]
        [PXUIField(Visibility = PXUIVisibility.Invisible, Visible = false)]
        public virtual int? LineofBusinessID { get; set; }

        #endregion
        #region LineofBusinessCD
        public abstract class lineofBusinessCD : PX.Data.IBqlField
        {
        }
        [PXDBString(15, IsKey = true, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "LineofBusiness ID")]
        [PXSelector(typeof(Search<CAMPLineofBusiness.lineofBusinessCD,
                                    Where<CAMPLineofBusiness.branchID,
                                        Equal<Current<AccessInfo.branchID>>>>),
            new Type[]{
                typeof(CAMPLineofBusiness.lineofBusinessCD),
                typeof(CAMPLineofBusiness.description),
                typeof(CAMPLineofBusiness.defaultSubaccount)},
            DescriptionField = typeof(CAMPProfileNumber.description))]
        public virtual string LineofBusinessCD { get; set; }

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
        #region DefaultSubaccount
        public abstract class defaultSubaccount : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "DefaultSubaccount")]
        [PXSelector(typeof(PX.Objects.GL.Sub.subID),
            SubstituteKey = typeof(PX.Objects.GL.Sub.subCD),
            DescriptionField = typeof(PX.Objects.GL.Sub.description))]
        public virtual int? DefaultSubaccount { get; set; }

        #endregion
        #region PriceClass
        public abstract class priceClass : PX.Data.IBqlField
        {
        }
        [PXDBInt()]
        [PXUIField(DisplayName = "PriceClass")]
        [PXSelector(typeof(PX.Objects.IN.INPriceClass.priceClassID),
            DescriptionField = typeof(PX.Objects.IN.INPriceClass.description))]
        public virtual int? PriceClass { get; set; }
        #endregion

        //New
        #region Phone1
        public abstract class phone1 : PX.Data.IBqlField
        {
        }
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Phone1")]
        public virtual string Phone1 { get; set; }
        #endregion
        #region Phone2
        public abstract class phone2 : PX.Data.IBqlField
        {
        }
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Phone2")]
        public virtual string Phone2 { get; set; }
        #endregion
        #region Fax
        public abstract class fax : PX.Data.IBqlField
        {
        }
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Fax")]
        public virtual string Fax { get; set; }
        #endregion
        #region Web
        public abstract class web : PX.Data.IBqlField
        {
        }
        [PXDBWeblink]
        [PXUIField(DisplayName = "Web")]
        //[PXMassMergableField]
        public virtual string Web { get; set; }
        #endregion
        #region Email
        public abstract class email : PX.Data.IBqlField
        {
        }
        [PXDBEmail]
        [PXUIField(DisplayName = "Email", Visibility = PXUIVisibility.SelectorVisible)]
        //[PXMassMergableField]
        public virtual string Email { get; set; }
        #endregion
        #region Engine
        public abstract class engine : PX.Data.IBqlField
        {
        }
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Engine", Visibility = PXUIVisibility.Visible)]
        public virtual bool? Engine { get; set; }
        #endregion
        #region WireBank
        public abstract class wireBank : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Wire Bank")]
        public virtual string WireBank { get; set; }
        #endregion
        #region WireAddress
        public abstract class wireAddress : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Wire Address")]
        public virtual string WireAddress { get; set; }
        #endregion
        #region WireABA
        public abstract class wireABA : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Wire ABA")]
        public virtual string WireABA { get; set; }
        #endregion
        #region WireAccount
        public abstract class wireAccount : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Wire Account")]
        public virtual string WireAccount { get; set; }
        #endregion
        #region WireName
        public abstract class wireName : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Wire Name")]
        public virtual string WireName { get; set; }
        #endregion
        #region WireSwiftCode
        public abstract class wireSwiftCode : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Wire Swift Code")]
        public virtual string WireSwiftCode { get; set; }
        #endregion
        #region WireIBAN
        public abstract class wireIBAN : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Wire IBAN")]
        public virtual string WireIBAN { get; set; }
        #endregion
        #region WireBeneficiary
        public abstract class wireBeneficiary : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Wire Beneficiary")]
        public virtual string WireBeneficiary { get; set; }
        #endregion
        #region LBWireName
        public abstract class lBWireName : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "LB/Wire Name")]
        public virtual string LBWireName { get; set; }
        #endregion
        #region LBWireAddressLine
        public abstract class lBWireAddressLine : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "LB/Wire Address Line")]
        public virtual string LBWireAddressLine { get; set; }
        #endregion
        #region LBWireCityStateZipcode
        public abstract class lBWireCityStateZipcode : PX.Data.IBqlField
        {
        }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "LB/Wire City State Zipcode")]
        public virtual string LBWireCityStateZipcode { get; set; }
        #endregion
        #region OvernightLBWireName
        public abstract class overnightLBWireName : PX.Data.IBqlField
        {
        }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Overnight LB/Wire Name")]
        public virtual string OvernightLBWireName { get; set; }
        #endregion
        #region OvernightLBWireAddressLine
        public abstract class overnightLBWireAddressLine : PX.Data.IBqlField
        {
        }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Overnight LB/Wire Address Line")]
        public virtual string OvernightLBWireAddressLine { get; set; }
        #endregion
        #region OvernightLBWireCityStateZipcode
        public abstract class overnightLBWireCityStateZipcode : PX.Data.IBqlField
        {
        }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Overnight LB/Wire City State Zipcode")]
        public virtual string OvernightLBWireCityStateZipcode { get; set; }
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