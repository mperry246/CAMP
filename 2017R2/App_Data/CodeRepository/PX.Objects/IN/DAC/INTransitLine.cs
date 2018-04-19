namespace PX.Objects.IN
{
    using System;
    using PX.Data;
    using PX.Objects.CS;
    using System.Diagnostics;
    using Data.EP;

    [Serializable()]
    [PXPrimaryGraph(typeof(INTransferEntry))]
    [PXCacheName(Messages.InTransitLine)]
    public partial class INTransitLine : PX.Data.IBqlTable
    {
        #region CostSiteID
        public abstract class costSiteID : PX.Data.IBqlField
        {
        }
        protected Int32? _CostSiteID;
        [PXDBForeignIdentity(typeof(INCostSite))]
        public virtual Int32? CostSiteID
        {
            get
            {
                return this._CostSiteID;
            }
            set
            {
                this._CostSiteID = value;
            }
        }
        #endregion

        #region TransferNbr
        public abstract class transferNbr : PX.Data.IBqlField
        {
        }
        protected String _TransferNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true)]
        [PXFieldDescription]
        public virtual String TransferNbr
        {
            get
            {
                return this._TransferNbr;
            }
            set
            {
                this._TransferNbr = value;
            }
        }
        #endregion

        #region TransferLineNbr
        public abstract class transferLineNbr : PX.Data.IBqlField
        {
        }
        protected Int32? _TransferLineNbr;

		[PXDBInt(IsKey = true)]
        [PXFieldDescription]
        public virtual Int32? TransferLineNbr
        {
            get
            {
                return this._TransferLineNbr;
            }
            set
            {
                this._TransferLineNbr = value;
            }
        }
        #endregion

        #region SOOrderType
        public abstract class sOOrderType : PX.Data.IBqlField
        {
        }
        protected String _SOOrderType;
        [PXDBString(2, IsFixed = true)]
        public virtual String SOOrderType
        {
            get
            {
                return this._SOOrderType;
            }
            set
            {
                this._SOOrderType = value;
            }
        }
        #endregion
        #region SOOrderNbr
        public abstract class sOOrderNbr : PX.Data.IBqlField
        {
        }
        protected String _SOOrderNbr;
        [PXDBString(15, InputMask = "", IsUnicode = true)]
        public virtual String SOOrderNbr
        {
            get
            {
                return this._SOOrderNbr;
            }
            set
            {
                this._SOOrderNbr = value;
            }
        }
        #endregion

        #region SOOrderLineNbr
        public abstract class sOOrderLineNbr : PX.Data.IBqlField
        {
        }
        protected Int32? _SOOrderLineNbr;

        [PXDBInt]
        public virtual Int32? SOOrderLineNbr
        {
            get
            {
                return this._SOOrderLineNbr;
            }
            set
            {
                this._SOOrderLineNbr = value;
            }
        }
        #endregion

        #region SOShipmentType
        public abstract class sOShipmentType : PX.Data.IBqlField
        {
        }
        protected String _SOShipmentType;
        [PXDBString(1, IsFixed = true)]
        public virtual String SOShipmentType
        {
            get
            {
                return this._SOShipmentType;
            }
            set
            {
                this._SOShipmentType = value;
            }
        }
        #endregion
        #region SOShipmentNbr
        public abstract class sOShipmentNbr : PX.Data.IBqlField
        {
        }
        protected String _SOShipmentNbr;
        [PXDBString(15, InputMask = "", IsUnicode = true)]
        public virtual String SOShipmentNbr
        {
            get
            {
                return this._SOShipmentNbr;
            }
            set
            {
                this._SOShipmentNbr = value;
            }
        }
        #endregion

        #region SOShipmentLineNbr
        public abstract class sOShipmentLineNbr : PX.Data.IBqlField
        {
        }
        protected Int32? _SOShipmentLineNbr;

        [PXDBInt]
        public virtual Int32? SOShipmentLineNbr
        {
            get
            {
                return this._SOShipmentLineNbr;
            }
            set
            {
                this._SOShipmentLineNbr = value;
            }
        }
        #endregion

        #region SiteID
        public abstract class siteID : PX.Data.IBqlField
        {
        }
        protected Int32? _SiteID;
        [IN.Site(DisplayName = "Warehouse ID", DescriptionField = typeof(INSite.descr))]
        public virtual Int32? SiteID
        {
            get
            {
                return this._SiteID;
            }
            set
            {
                this._SiteID = value;
            }
        }
        #endregion
        #region ToSiteID
        public abstract class toSiteID : PX.Data.IBqlField
        {
        }
        protected Int32? _ToSiteID;
        [IN.ToSite(DisplayName = "To Warehouse ID", DescriptionField = typeof(INSite.descr))]
        public virtual Int32? ToSiteID
        {
            get
            {
                return this._ToSiteID;
            }
            set
            {
                this._ToSiteID = value;
            }
        }
        #endregion

        #region OrigModule
        public abstract class origModule : PX.Data.IBqlField
        {
            public const string PI = "PI";

            public class List : PXStringListAttribute
            {
				public List() : base(
					new[]
                {
						Pair(GL.BatchModule.SO, GL.Messages.ModuleSO),
						Pair(GL.BatchModule.PO, GL.Messages.ModulePO),
						Pair(GL.BatchModule.IN, GL.Messages.ModuleIN),
						Pair(PI, Messages.ModulePI),
						Pair(GL.BatchModule.AP, GL.Messages.ModuleAP),
					}) {}
            }
        }
        protected String _OrigModule;
        [PXDBString(2, IsFixed = true)]
        [PXDefault(GL.BatchModule.IN)]
        [origModule.List]
        public virtual String OrigModule
        {
            get
            {
                return this._OrigModule;
            }
            set
            {
                this._OrigModule = value;
            }
        }
        #endregion

        #region ToLocationID
        public abstract class toLocationID : PX.Data.IBqlField
        {
        }
        protected Int32? _ToLocationID;
        [IN.Location(DisplayName = "To Location ID")]
        public virtual Int32? ToLocationID
        {
            get
            {
                return this._ToLocationID;
            }
            set
            {
                this._ToLocationID = value;
            }
        }
        #endregion

        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }
        protected Guid? _NoteID;
        [PXNote(DescriptionField = typeof(INTransitLine.transferNbr),
            Selector = typeof(INTransitLine.transferNbr))]
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
        #region RefNoteID
        public abstract class refNoteID : PX.Data.IBqlField
        {
        }
        protected Guid? _RefNoteID;
        [PXDBGuid()]
        public virtual Guid? RefNoteID
        {
            get
            {
                return this._RefNoteID;
            }
            set
            {
                this._RefNoteID = value;
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
}
