using System;

using PX.Data;

namespace PX.Objects.Common
{
	/// <summary>
	/// An attribute allowing declarative conditional control over
	/// <see cref="PXUIFieldAttribute.Visible"/> property of a field.
	/// </summary>
	public class PXUIVisibleAttribute : PXBaseConditionAttribute, IPXRowSelectedSubscriber
	{
		public PXUIVisibleAttribute(Type conditionType)
			: base(conditionType)
		{ }

		public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			if (e.Row == null || _Condition == null)
			{
				return;
			}

			SetVisible(sender, e.Row, _FieldName, Condition);
		}

		public static void SetVisible(PXCache sender, object row, string fieldName, Type conditionType)
		{
			if (row == null || conditionType == null)
			{
				return;
			}

			PXUIFieldAttribute.SetVisible(
				sender, 
				row, 
				fieldName, 
				GetConditionResult(sender, row, conditionType));
		}

		public static void SetVisible(PXCache sender, object row, string fieldName)
		{
			if (row == null)
			{
				return;
			}

			SetVisible(sender, row, fieldName, GetCondition<PXUIVisibleAttribute>(sender, row, fieldName));
		}

		public static void SetEnabled<Field>(PXCache sender, object row, Type conditionType)
			where Field : IBqlField
		{
			if (row == null || conditionType == null)
			{
				return;
			}

			SetVisible(sender, row, typeof(Field).Name, conditionType);
		}

		public static void SetEnabled<Field>(PXCache sender, object row)
			where Field : IBqlField
		{
			if (row == null)
			{
				return;
			}

			SetEnabled<Field>(sender, row, GetCondition<PXUIVisibleAttribute, Field>(sender, row));
		}

	}
}
