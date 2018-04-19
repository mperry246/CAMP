using System;
using PX.Data;
using PX.Data.EP;
using System.Diagnostics;
using PX.TM;
using PX.Objects.EP;
using PX.SM;

namespace PX.Objects.EP
{
	[Serializable]
	public class EPRuleTree : EPRule
	{
	}

	[System.SerializableAttribute()]
	[PXPrimaryGraph(typeof(EPAssignmentMaint))]
	[DebuggerDisplay("Name={Name} WorkgroupID={WorkgroupID} RuleID={RuleID}")]
	[PXCacheName(Messages.EPRule)]
	public partial class EPRule : IBqlTable
	{
		#region RuleID
		public abstract class ruleID : IBqlField { }
		
		[PXSequentialNote(new Type[0], SuppressActivitiesCount = true, IsKey = true)]
		[PXUIField(DisplayName = "Rule ID")]
		public virtual Guid? RuleID { get; set; }
		#endregion

		#region AssignmentMapID
		public abstract class assignmentMapID : IBqlField { }
		
		[PXDBInt]
		[PXDBDefault(typeof(EPAssignmentMap.assignmentMapID))]
		[PXParent(typeof(Select<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Current<EPRule.assignmentMapID>>>>))]
		public virtual int? AssignmentMapID { get; set; }
		#endregion
		#region StepID
		public abstract class stepID : IBqlField { }
		
		[PXDBGuid]
		[PXUIField(DisplayName = "Step ID")]
		public virtual Guid? StepID { get; set; }
		#endregion

		#region Sequence
		public abstract class sequence : IBqlField { }
		
		[PXDBInt]
		[PXUIField(DisplayName = "Seq.", Enabled = false)]
		[PXDefault(1)]
		public virtual int? Sequence { get; set; }
		#endregion
		#region Name
		public abstract class name : IBqlField { }

		[PXDBString(60, IsUnicode = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
		public virtual string Name { get; set; }
        #endregion

        #region StepName
        public abstract class stepname : IBqlField
        {
        }
        [PXString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Step")]
        public virtual string StepName { get; set; }
        #endregion

        #region Icon
        public abstract class icon : IBqlField { }

		[PXString(250)]
		public virtual string Icon { get; set; }
		#endregion
		#region RuleType
		public abstract class ruleType : IBqlField { }
		
		[PXDBString(1, IsFixed = true)]
		[EPRuleType.List()]
		[PXUIField(DisplayName = "Approver")]
		[PXDefault(EPRuleType.Direct)]
		public virtual string RuleType { get; set; }
		#endregion
		#region ApproveType
		public abstract class approveType : IBqlField { }

		[PXDBString(1, IsFixed = true)]
		[EPApproveType.List()]
		[PXUIField(DisplayName = "On Approval")]
		[PXDefault(EPApproveType.Wait)]
		public virtual string ApproveType { get; set; }
		#endregion
		#region EmptyStepType
		public abstract class emptyStepType : IBqlField { }

		[PXDBString(1, IsFixed = true)]
		[EPEmptyStepType.List()]
		[PXUIField(DisplayName = "If No Approver Found")]
		[PXDefault(EPEmptyStepType.Next)]
		public virtual string EmptyStepType { get; set; }
		#endregion

		#region WaitTime
		public abstract class waitTime : IBqlField { }
		
		[PXDefault(0)]
		[PXUIField(DisplayName = "Decision Wait Time", Visibility = PXUIVisibility.Visible)]
		[PXDBTimeSpanLong(Format = TimeSpanFormatType.DaysHoursMinites)]
		public virtual int? WaitTime { get; set; }
		#endregion		

		#region WorkgroupID
		public abstract class workgroupID : IBqlField { }

		[PXDBInt()]
		[PXUIField(DisplayName = "Workgroup")]
		[PXCompanyTreeSelector]
		public virtual int? WorkgroupID { get; set; }
		#endregion
		#region OwnerID
		public abstract class ownerID : IBqlField { }

		[PXDBGuid()]
		[PXOwnerSelector(typeof(EPRule.workgroupID))]
		[PXUIField(DisplayName = "Employee")]
		public virtual Guid? OwnerID { get; set; }
		#endregion
		#region OwnerSource
		public abstract class ownerSource : IBqlField { }

		[PXDBString(250)]
		[PXUIField(DisplayName = "Employee", Visibility = PXUIVisibility.Visible, Required = true)]
		public virtual string OwnerSource { get; set; }
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
		public virtual DateTime? LastModifiedDateTime { get; set; }
		#endregion
		#region tstamp
		public abstract class Tstamp : IBqlField { }

		[PXDBTimestamp]
		public virtual byte[] tstamp { get; set; }
		#endregion
	}

	public static class EPAssignRuleType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
					new string[] { Direct, Document },
					new string[] { Messages.DirectEmployee, Messages.DocumentEmployee })
			{
				;
			}
		}

		public const string Direct = "D";
		public const string Document = "E";

		public class direct : Constant<string>
		{
			public direct() : base(Direct)
			{
			}
		}
		public class document : Constant<string>
		{
			public document() : base(Document)
			{
			}
		}
	}

	public static class EPRuleType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
					new string[] { Direct, Document, Filter },
					new string[] { Messages.DirectEmployee, Messages.DocumentEmployee, Messages.FilterEmployee })
			{
				;
			}
		}

		public const string Direct = "D";
		public const string Document = "E";
		public const string Filter = "F";

		public class direct : Constant<string>
		{
			public direct() : base(Direct)
			{
			}
		}
		public class document : Constant<string>
		{
			public document() : base(Document)
			{
			}
		}
		public class filter : Constant<string>
		{
			public filter() : base(Filter)
			{
			}
		}

	}

	public static class EPApproveType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
					new [] { Wait, Complete, Approve },
					new [] { Messages.WaitForApproval, Messages.CompleteStep, Messages.ApproveEntireDoc })
			{
			}
		}

		public const string Wait = "W";
		public const string Complete = "C";
		public const string Approve = "A";

		public class wait : Constant<string>
		{
			public wait() : base(Wait)
			{
			}
		}
		public class complete : Constant<string>
		{
			public complete() : base(Complete)
			{
			}
		}
		public class approve : Constant<string>
		{
			public approve() : base(Approve)
			{
			}
		}

	}

	public static class EPEmptyStepType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
					new string[] { Reject, Approve, Next },
					new string[] { Messages.RejectDoc, Messages.ApproveDoc, Messages.NextStep })
			{
			}
		}

		public const string Reject = "R";
		public const string Approve = "A";
		public const string Next = "N";

		public class reject : Constant<string>
		{
			public reject() : base(Reject)
			{
			}
		}
		public class approve : Constant<string>
		{
			public approve() : base(Approve)
			{
			}
		}
		public class next : Constant<string>
		{
			public next() : base(Next)
			{
			}
		}

	}
}