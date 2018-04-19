﻿namespace PX.Objects.AR
{
    using System;
    using PX.Data;
    using PX.Objects.IN;
    using PX.Objects.CM;
    using PX.Objects.TX;
    using PX.Objects.CS;

	/// <summary>
	/// Represents an Accounts Receivable price worksheet. Price worksheets are used 
	/// to conveniently make mass changes to <see cref="ARSalesPrice">sales prices</see>.
	/// Prices defined in the worksheet (<see cref="ARPriceWorksheetDetail"/>) update sales 
	/// prices upon worksheet release. The entities of this type can be edited on the
	/// Sales Price Worksheets (AR202010) form, which corresponds to the <see 
	/// cref="ARPriceWorksheetMaint"/> graph.
	/// </summary>
    [System.SerializableAttribute()]
    [PXPrimaryGraph(typeof(ARPriceWorksheetMaint))]
	[PXCacheName(Messages.ARPriceWorksheet)]
	public partial class ARPriceWorksheet : PX.Data.IBqlTable
    {
        #region RefNbr
        public abstract class refNbr : PX.Data.IBqlField
        {           
            #region Numbering
            public class NumberingAttribute : AutoNumberAttribute
            {
                public NumberingAttribute()
                    : base(typeof(ARSetup.priceWSNumberingID), typeof(ARPriceWorksheet.effectiveDate)) { ; }
            }
            #endregion
        }
        protected String _RefNbr;
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault()]
        [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
		[PXSelector(typeof(Search<ARPriceWorksheet.refNbr>), new Type[] { typeof(ARPriceWorksheet.refNbr), typeof(ARPriceWorksheet.status), typeof(ARPriceWorksheet.descr), typeof(ARPriceWorksheet.effectiveDate) })]
        [refNbr.Numbering()]
        //[PXFieldDescription]
        public virtual String RefNbr
        {
            get
            {
                return this._RefNbr;
            }
            set
            {
                this._RefNbr = value;
            }
        }
        #endregion
        #region Status
        public abstract class status : PX.Data.IBqlField
        {
        }
        protected String _Status;
        [PXDBString(1, IsFixed = true)]
        [PXDefault(SPWorksheetStatus.Hold)]
        [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        [SPWorksheetStatus.List()]
        public virtual String Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
        #endregion
        #region Hold
        public abstract class hold : PX.Data.IBqlField
        {
        }
        protected Boolean? _Hold;
        [PXDBBool()]
        [PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
        [PXDefault(true)]
        //[PXNoUpdate]
        public virtual Boolean? Hold
        {
            get
            {
                return this._Hold;
            }
            set
            {
                this._Hold = value;
            }
        }
        #endregion
        #region Approved
        public abstract class approved : PX.Data.IBqlField
        {
        }
        protected Boolean? _Approved;
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Approved", Visibility = PXUIVisibility.Visible, Enabled = false)]
        public virtual Boolean? Approved
        {
            get
            {
                return this._Approved;
            }
            set
            {
                this._Approved = value;
            }
        }
        #endregion
        #region Descr
        public abstract class descr : PX.Data.IBqlField
        {
        }
        protected String _Descr;
        [PXDBString(150, IsUnicode = true)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
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
        #region EffectiveDate
        public abstract class effectiveDate : PX.Data.IBqlField
        {
        }
        protected DateTime? _EffectiveDate;
        [PXDefault(typeof(AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXDBDate()]
        [PXUIField(DisplayName = "Effective Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? EffectiveDate
        {
            get
            {
                return this._EffectiveDate;
            }
            set
            {
                this._EffectiveDate = value;
            }
        }
        #endregion
        #region ExpirationDate
        public abstract class expirationDate : PX.Data.IBqlField
        {
        }
        protected DateTime? _ExpirationDate;
        [PXDBDate()]
        [PXUIField(DisplayName = "Expiration Date", Visibility = PXUIVisibility.Visible)]
        public virtual DateTime? ExpirationDate
        {
            get
            {
                return this._ExpirationDate;
            }
            set
            {
                this._ExpirationDate = value;
            }
        }
        #endregion
        #region IsPromotional
        public abstract class isPromotional : IBqlField
        {
        }
        protected bool? _IsPromotional;
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Promotional")]
        public virtual bool? IsPromotional
        {
            get
            {
                return _IsPromotional;
            }
            set
            {
                _IsPromotional = value;
            }
        }
        #endregion
        #region OverwriteOverlapping
        public abstract class overwriteOverlapping : IBqlField
        {
        }
        protected bool? _OverwriteOverlapping;
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Overwrite Overlapping Prices")]
        public virtual bool? OverwriteOverlapping
        {
            get
            {
                return _OverwriteOverlapping;
            }
            set
            {
                _OverwriteOverlapping = value;
            }
        }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }
        protected Guid? _NoteID;
        [PXNote(new Type[0])]
        public virtual Guid? NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
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
        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
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
        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
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
    public class SPWorksheetStatus
    {
        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute()
                : base(
                new string[] { Hold, Open, PendingApproval, Released },
                new string[] { Messages.Hold, Messages.Open, Messages.PendingApproval, Messages.Released }) { ; }
        }

        public const string Hold = "H";
        public const string Open = "N";
        public const string PendingApproval = "P";
        public const string Released = "R";

        public class hold : Constant<string>
        {
            public hold() : base(Hold) { ;}
        }

        public class open : Constant<string>
        {
            public open() : base(Open) { ;}
        }

        public class pendingApproval : Constant<string>
        {
            public pendingApproval() : base(PendingApproval) { ;}
        }

        public class released : Constant<string>
        {
            public released() : base(Released) { ;}
        }

    }
}