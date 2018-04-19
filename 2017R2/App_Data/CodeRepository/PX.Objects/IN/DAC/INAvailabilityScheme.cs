using System;
using PX.Data;

namespace PX.Objects.IN
{
	[Serializable]
	[PXPrimaryGraph(typeof(INAvailabilitySchemeMaint))]
	[PXCacheName(Messages.INAvailabilityScheme)]
	public class INAvailabilityScheme : IBqlTable
	{
		#region AvailabilitySchemeID
		public abstract class availabilitySchemeID : IBqlField { }
		[PXDefault]
		[PXDBString(10, IsKey = true, IsUnicode = true)]
		[PXUIField(DisplayName = "Availability Calculation Rule", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(INAvailabilityScheme.availabilitySchemeID))]
		public virtual string AvailabilitySchemeID { get; set; }
		#endregion
		#region Description
		public abstract class description : IBqlField { }
		[PXDBString(250, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string Description { get; set; }
		#endregion

		#region InclQtySOReverse
		public abstract class inclQtySOReverse : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Include Qty. on Returns")]
		public virtual bool? InclQtySOReverse { get; set; }
		#endregion
		#region InclQtySOBackOrdered
		public abstract class inclQtySOBackOrdered : IBqlField { }
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Deduct Qty. on Back Orders")]
		public virtual bool? InclQtySOBackOrdered { get; set; }
		#endregion
		#region InclQtySOPrepared
		public abstract class inclQtySOPrepared : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Deduct Qty. on Sales Prepared")]
		public virtual bool? InclQtySOPrepared { get; set; }
		#endregion
		#region InclQtySOBooked
		public abstract class inclQtySOBooked : IBqlField { }
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Deduct Qty. on Sales Orders")]
		public virtual bool? InclQtySOBooked { get; set; }
		#endregion
		#region InclQtySOShipped
		public abstract class inclQtySOShipped : IBqlField { }
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Deduct Qty. Shipped")]
		public virtual bool? InclQtySOShipped { get; set; }
		#endregion
		#region InclQtySOShipping
		public abstract class inclQtySOShipping : IBqlField { }
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Deduct Qty. Allocated")]
		public virtual bool? InclQtySOShipping { get; set; }
		#endregion
		#region InclQtyInTransit
		public abstract class inclQtyInTransit : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Include Qty. in Transit")]
		public virtual bool? InclQtyInTransit { get; set; }
		#endregion
		#region InclQtyPOReceipts
		public abstract class inclQtyPOReceipts : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Include Qty. on PO Receipts")]
		public virtual bool? InclQtyPOReceipts { get; set; }
		#endregion
		#region InclQtyPOPrepared
		public abstract class inclQtyPOPrepared : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Include Qty. on Purchase Prepared")]
		public virtual bool? InclQtyPOPrepared { get; set; }
		#endregion
		#region InclQtyPOOrders
		public abstract class inclQtyPOOrders : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Include Qty. on Purchase Orders")]
		public virtual bool? InclQtyPOOrders { get; set; }
		#endregion
		#region InclQtyINIssues
		public abstract class inclQtyINIssues : IBqlField { }
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Deduct Qty. on Issues")]
		public virtual bool? InclQtyINIssues { get; set; }
		#endregion
		#region InclQtyINReceipts
		public abstract class inclQtyINReceipts : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Include Qty. on Receipts")]
		public virtual bool? InclQtyINReceipts { get; set; }
		#endregion
		#region InclQtyINAssemblyDemand
		public abstract class inclQtyINAssemblyDemand : IBqlField { }
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Deduct Qty. of Kit Assembly Demand")]
		public virtual bool? InclQtyINAssemblyDemand { get; set; }
		#endregion
		#region InclQtyINAssemblySupply
		public abstract class inclQtyINAssemblySupply : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Include Qty. of Kit Assembly Supply")]
		public virtual bool? InclQtyINAssemblySupply { get; set; }
        #endregion
        #region InclQtyProductionSupplyPrepared
        public abstract class inclQtyProductionSupplyPrepared : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionSupplyPrepared;
        /// <summary>
        /// Production / Manufacturing 
        /// Specifies (if set to <c>true</c>) that the Product Supply Prepared quantity is added to the total item availability.  
        /// </summary>
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Include Qty. of Production Supply Prepared")]
        public virtual Boolean? InclQtyProductionSupplyPrepared
        {
            get
            {
                return this._InclQtyProductionSupplyPrepared;
            }
            set
            {
                this._InclQtyProductionSupplyPrepared = value;
            }
        }
        #endregion
        #region InclQtyProductionSupply
        public abstract class inclQtyProductionSupply : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionSupply;
        /// <summary>
        /// Production / Manufacturing 
        /// Specifies (if set to <c>true</c>) that the Product Supply quantity is added to the total item availability.  
        /// </summary>
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Include Qty. of Production Supply")]
        public virtual Boolean? InclQtyProductionSupply
        {
            get
            {
                return this._InclQtyProductionSupply;
            }
            set
            {
                this._InclQtyProductionSupply = value;
            }
        }
        #endregion
        #region InclQtyProductionDemandPrepared
        public abstract class inclQtyProductionDemandPrepared : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionDemandPrepared;
        /// <summary>
        /// Production / Manufacturing 
        /// Specifies (if set to <c>true</c>) that the Production Demand Prepared quantity is deducted from the total item availability.  
        /// </summary>
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Deduct Qty. on Production Demand Prepared")]
        public virtual Boolean? InclQtyProductionDemandPrepared
        {
            get
            {
                return this._InclQtyProductionDemandPrepared;
            }
            set
            {
                this._InclQtyProductionDemandPrepared = value;
            }
        }
        #endregion
        #region InclQtyProductionDemand
        public abstract class inclQtyProductionDemand : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionDemand;
        /// <summary>
        /// Production / Manufacturing 
        /// Specifies (if set to <c>true</c>) that the Production Demand quantity is deducted from the total item availability.  
        /// </summary>
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Deduct Qty. on Production Demand")]
        public virtual Boolean? InclQtyProductionDemand
        {
            get
            {
                return this._InclQtyProductionDemand;
            }
            set
            {
                this._InclQtyProductionDemand = value;
            }
        }
        #endregion
        #region InclQtyProductionAllocated
        public abstract class inclQtyProductionAllocated : PX.Data.IBqlField
        {
        }
        protected Boolean? _InclQtyProductionAllocated;
        /// <summary>
        /// Production / Manufacturing 
        /// Specifies (if set to <c>true</c>) that the Production Allocated quantity is deducted from the total item availability.  
        /// </summary>
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Deduct Qty. on Production Allocated")]
        public virtual Boolean? InclQtyProductionAllocated
        {
            get
            {
                return this._InclQtyProductionAllocated;
            }
            set
            {
                this._InclQtyProductionAllocated = value;
            }
        }
        #endregion

        #region tstamp
        public abstract class Tstamp : IBqlField { }
		[PXDBTimestamp]
		public virtual byte[] tstamp { get; set; }
		#endregion
		#region CreatedByID
		public abstract class createdByID : IBqlField { }
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID { get; set; }
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField { }
		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID { get; set; }
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField { }
		[PXDBCreatedDateTime]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? CreatedDateTime { get; set; }
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : IBqlField { }
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID { get; set; }
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField { }
		[PXDBLastModifiedByScreenID]
		public virtual string LastModifiedByScreenID { get; set; }
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField { }
		[PXDBLastModifiedDateTime]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		#endregion
	}
}
