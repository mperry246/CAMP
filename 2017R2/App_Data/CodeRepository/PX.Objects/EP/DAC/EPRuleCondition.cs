using System;
using PX.Data;

namespace PX.Objects.EP
{
	[Serializable]
	[PXCacheName(Messages.EPRuleCondition)]
	[PXTable]
	public class EPRuleCondition : EPRuleBaseCondition, IBqlTable
	{
		#region RuleID
		public abstract class ruleID : IBqlField { }

		[PXDBGuid(IsKey = true)]
		[PXUIField(DisplayName = "Rule ID")]
		[PXFormula(typeof(Current<EPRule.ruleID>))]
		[PXParent(typeof(Select<EPRule, Where<EPRule.ruleID, Equal<Current<EPRuleCondition.ruleID>>>>))]
		public override Guid? RuleID { get; set; }
		#endregion
		#region RowNbr
		public abstract class rowNbr : IBqlField { }

		[PXDBShort(IsKey = true)]
		[PXDefault]
		[RowNbr]
		public virtual short? RowNbr { get; set; }
		#endregion
		
		#region OpenBrackets
		public abstract class openBrackets : IBqlField { }

		[PXDBInt]
		[PXIntList(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, new string[] { "-", "(", "((", "(((", "((((", "(((((", "Activity Exists (", "Activity Not Exists (" })]
		[PXUIField(DisplayName = "Brackets")]
		[PXDefault(0)]
		public override int? OpenBrackets { get; set; }
		#endregion
		#region Entity
		public abstract class entity : IBqlField { }

		[PXDBString(EntityLength, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Entity")]
		public override string Entity { get; set; }
		#endregion
		#region FieldName
		public abstract class fieldName : IBqlField { }

		[PXDBString(FieldLength)]
		[PXDefault("")]
		[PXUIField(DisplayName = "Field Name", Required = true)]
		[PXFormula(typeof(Default<EPAssignmentRule.entity>))]
		public override string FieldName { get; set; }
		#endregion
		#region Condition
		public abstract class condition : IBqlField { }

		[PXDBInt]
		[PXDefault((int)PXCondition.EQ)]
		[PXUIField(DisplayName = "Condition", Required = true)]
		[PXIntList(
			new int[]
			{
				(int)PXCondition.EQ,
				(int)PXCondition.NE,
				(int)PXCondition.GT,
				(int)PXCondition.GE,
				(int)PXCondition.LT,
				(int)PXCondition.LE,
				(int)PXCondition.BETWEEN,
				(int)PXCondition.LIKE,
				(int)PXCondition.RLIKE,
				(int)PXCondition.LLIKE,
				(int)PXCondition.ISNULL,
				(int)PXCondition.ISNOTNULL
			},
			new string[] 
			{
				"Equals",
				"Does Not Equal",
				"Is Greater Than",
				"Is Greater Than or Equal To",
				"Is Less Than",
				"Is Less Than or Equal To",
				"Is Between",
				"Contains",
				"Starts With",
				"Ends With",
				"Is Null",
				"Is Not Null"
			})]
		public override int? Condition { get; set; }
		#endregion
		#region IsRelative
		public abstract class isRelative : IBqlField { }

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Is Relative")]
		public override bool? IsRelative { get; set; }
		#endregion
		#region IsField
		public abstract class isField : IBqlField { }

		[PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "From Doc.")]
		public override bool? IsField { get; set; }
		#endregion
		#region Value
		public abstract class value : IBqlField { }

		[PXDBString(128, IsUnicode = true)]
		[PXUIField(DisplayName = "Value")]
		public override string Value { get; set; }
		#endregion
		#region Value2
		public abstract class value2 : IBqlField { }

		[PXDBString(128, IsUnicode = true)]
		[PXUIField(DisplayName = "Value 2")]
		public override string Value2 { get; set; }
		#endregion
		#region CloseBrackets
		public abstract class closeBrackets : IBqlField { }

		[PXDBInt]
		[PXIntList(new int[] { 0, 1, 2, 3, 4, 5 }, new string[] { "-", ")", "))", ")))", "))))", ")))))" })]
		[PXUIField(DisplayName = "Brackets")]
		[PXDefault(0)]
		public override int? CloseBrackets { get; set; }
		#endregion
		#region Operator
		public abstract class operatoR : IBqlField { }

		[PXDBInt]
		[PXIntList(new int[] { 0, 1 }, new string[] { "And", "Or" })]
		[PXUIField(FieldName = "Operator")]
		[PXDefault(0)]
		public override int? Operator { get; set; }
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

	public class EPRuleBaseCondition
	{
		public const int EntityLength = 60;
		public const int FieldLength = 128;
		
		public virtual Guid? RuleID { get; set; }
		public virtual int? OpenBrackets { get; set; }
		public virtual string Entity { get; set; }
		public virtual string FieldName { get; set; }
		public virtual int? Condition { get; set; }
		public virtual bool? IsRelative { get; set; }
		public virtual bool? IsField { get; set; }
		public virtual string Value { get; set; }
		public virtual string Value2 { get; set; }
		public virtual int? CloseBrackets { get; set; }
		public virtual int? Operator { get; set; }
	}
}
