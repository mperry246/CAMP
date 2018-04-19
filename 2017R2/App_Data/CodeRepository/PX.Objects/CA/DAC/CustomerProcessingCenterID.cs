using System;
using PX.Data;
using PX.Objects.AR;

namespace PX.Objects.CA
{
	[PXCacheName("Customer Processing Center ID")]
	[Serializable]
	public partial class CustomerProcessingCenterID : IBqlTable
	{
		#region InstanceID
		public abstract class instanceID : IBqlField
		{
		}

		[PXDBIdentity(IsKey = true)]
		public virtual int? InstanceID
			{
			get;
			set;
		}
		#endregion
		#region BAccountID
		public abstract class bAccountID : IBqlField
		{
		}

		[Customer(DescriptionField = typeof(Customer.acctName))]
		[PXParent(typeof(Select<Customer, Where<Customer.bAccountID, Equal<Current<CustomerProcessingCenterID.bAccountID>>>>))]
		public virtual int? BAccountID
			{
			get;
			set;
		}
		#endregion
		#region CCProcessingCenterID
		public abstract class cCProcessingCenterID : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true)]
		[PXDefault]
		[PXParent(typeof(Select<CCProcessingCenter, 
			Where<CCProcessingCenter.processingCenterID, Equal<Current<CustomerProcessingCenterID.cCProcessingCenterID>>>>))]
		[PXUIField(DisplayName = "Proc. Center ID")]
		public virtual string CCProcessingCenterID
		{
			get;
			set;
		}
		#endregion
		#region CustomerCCPID
		public abstract class customerCCPID : IBqlField
		{
		}
		[PXDBString(1024, IsUnicode = true)]
		[PXDBDefault]
		[PXUIField(DisplayName = "Customer CCPID")]
		public virtual string CustomerCCPID
		{
			get;
			set;
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
	}
}
