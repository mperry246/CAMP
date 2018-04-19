using PX.Data;
using System;

namespace PX.Objects.Common.DataIntegrity
{
	[Serializable]
	public class DataIntegrityLog : IBqlTable
	{
		public class logEntryID : IBqlField { }
		[PXDBIdentity(IsKey = true)]
		public virtual int? LogEntryID { get; set; }

		public class utcTime : IBqlField { }
		[PXDBDateAndTime]
		public virtual DateTime? UtcTime { get; set; }

		public class inconsistencyCode : IBqlField { }
		[PXDBString(30)]
		public virtual string InconsistencyCode { get; set; }

		public class exceptionMessage : IBqlField { }
		[PXDBString(255, IsUnicode = true)]
		public virtual string ExceptionMessage { get; set; }

		public class contextInfo : IBqlField { }
		[PXDBText(IsUnicode = true)]
		public virtual string ContextInfo { get; set; }

		public class userID : IBqlField { }
		[PXDBGuid]
		public virtual Guid? UserID { get; set; }

		public class userBranchID : IBqlField { }
		[PXDBInt]
		public virtual int? UserBranchID { get; set; }
	}
}
