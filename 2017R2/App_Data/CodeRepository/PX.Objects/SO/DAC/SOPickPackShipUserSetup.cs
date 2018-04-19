using System;
using PX.Data;
using PX.SM;
using PX.Objects.SO;

//Temporary namespace for part of Pick-Pack-Ship functinonality. Will be refactored in 2018Rx. Do not use.
namespace PX.Objects.PPS
{
	//This class is added for integrity purposes only. Do not use.
	[PXHidden]
	class SOPickPackShipUserSetup
	{
		#region UserID
		public abstract class userID : IBqlField
		{
		}
		protected Guid? _UserID;
		[PXDBGuid(IsKey = true)]
		[PXUIField(DisplayName = "User ID", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual Guid? UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				this._UserID = value;
			}
		}
		#endregion
		#region PromptLocation
		public abstract class promptLocation : PX.Data.IBqlField
		{
		}
		protected Boolean? _PromptLocation;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Prompt for Location", FieldClass = PX.Objects.IN.LocationAttribute.DimensionName)]
		public virtual Boolean? PromptLocation
		{
			get
			{
				return this._PromptLocation;
			}
			set
			{
				this._PromptLocation = value;
			}
		}
		#endregion
		#region ShipmentConfirmation
		public abstract class shipmentConfirmation : PX.Data.IBqlField
		{
		}
		protected Boolean? _ShipmentConfirmation;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Print Shipment Confirmation Automatically")]
		public virtual Boolean? ShipmentConfirmation
		{
			get
			{
				return this._ShipmentConfirmation;
			}
			set
			{
				this._ShipmentConfirmation = value;
			}
		}
		#endregion
		#region ShipmentConfirmationQueue
		public abstract class shipmentConfirmationQueue : PX.Data.IBqlField
		{
		}
		protected String _ShipmentConfirmationQueue;
		[PXDBString(10)]
		[PXUIField(DisplayName = "Print Queue")]
		public virtual String ShipmentConfirmationQueue
		{
			get
			{
				return this._ShipmentConfirmationQueue;
			}
			set
			{
				this._ShipmentConfirmationQueue = value;
			}
		}
		#endregion
		#region ShipmentLabels
		public abstract class shipmentLabels : PX.Data.IBqlField
		{
		}
		protected Boolean? _ShipmentLabels;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Print Shipment Labels Automatically")]
		public virtual Boolean? ShipmentLabels
		{
			get
			{
				return this._ShipmentLabels;
			}
			set
			{
				this._ShipmentLabels = value;
			}
		}
		#endregion
		#region ShipmentLabelsQueue
		public abstract class shipmentLabelsQueue : PX.Data.IBqlField
		{
		}
		protected String _ShipmentLabelsQueue;
		[PXDBString(10)]
		[PXUIField(DisplayName = "Print Queue")]
		public virtual String ShipmentLabelsQueue
		{
			get
			{
				return this._ShipmentLabelsQueue;
			}
			set
			{
				this._ShipmentLabelsQueue = value;
			}
		}
		#endregion
		#region UseScale
		public abstract class useScale : PX.Data.IBqlField
		{
		}
		protected Boolean? _UseScale;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Use Digital Scale")]
		public virtual Boolean? UseScale
		{
			get
			{
				return this._UseScale;
			}
			set
			{
				this._UseScale = value;
			}
		}
		#endregion
		#region ScaleID
		public abstract class scaleID : PX.Data.IBqlField
		{
		}
		protected String _ScaleID;
		[PXDBString(10)]
		[PXUIField(DisplayName = "Scale")]
		public virtual String ScaleID
		{
			get
			{
				return this._ScaleID;
			}
			set
			{
				this._ScaleID = value;
			}
		}
		#endregion
	}
}
