using System;
using PX.Data;

namespace PX.Objects.RUTROT
{
	[Serializable]
	public class RUTROTSetup : IBqlTable
	{
		#region NsMain
		public abstract class nsMain : IBqlField { }
		[PXDBString]
		[PXDefault]
		[PXUIField(DisplayName = "NsMain", Enabled = true, Visible = true, Visibility = PXUIVisibility.Visible, FieldClass = RUTROTMessages.FieldClass)]
		public virtual string NsMain
		{
			get;
			set;
		}
		#endregion

		#region NsHtko
		public abstract class nsHtko : IBqlField { }
		[PXDBString]
		[PXDefault]
		[PXUIField(DisplayName = "NsHtko", Enabled = true, Visible = true, Visibility = PXUIVisibility.Visible, FieldClass = RUTROTMessages.FieldClass)]
		public virtual string NsHtko
		{
			get;
			set;
		}
		#endregion

		#region NsXsi
		public abstract class nsXsi : IBqlField { }
		[PXDBString]
		[PXDefault]
		[PXUIField(DisplayName = "NsXsi", Enabled = true, Visible = true, Visibility = PXUIVisibility.Visible, FieldClass = RUTROTMessages.FieldClass)]
		public virtual string NsXsi
		{
			get;
			set;
		}
		#endregion

		#region Schema location
		public abstract class schemaLocation : IBqlField { }
		[PXDBString]
		[PXDefault]
		[PXUIField(DisplayName = "Schema location", Enabled = true, Visible = true, Visibility = PXUIVisibility.Visible, FieldClass = RUTROTMessages.FieldClass)]
		public virtual string SchemaLocation
		{
			get;
			set;
		}
		#endregion

		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		[PXDBTimestamp]
		public virtual byte[] tstamp
		{
			get;
			set;
		}
		#endregion

		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		[PXDBLastModifiedByScreenID]
		public virtual string LastModifiedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
	}
}
