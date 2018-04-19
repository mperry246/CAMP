using System;
using System.Runtime.Serialization;

using PX.Data;

namespace PX.Objects.GL.Exceptions
{
	public class FinancialPeriodOffsetNotFoundException : PXFinPeriodException
	{
		/// <summary>
		/// Gets the financial period ID for which the offset period was not found.
		/// </summary>
		public string FinancialPeriodId
		{
			get;
			private set;
		}

		/// <summary>
		/// The positive or negative number of periods offset from the <see cref="FinancialPeriodId"/>,
		/// for which the financial period was not found.
		/// </summary>
		public int Offset
		{
			get;
			private set;
		}

		public FinancialPeriodOffsetNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }

		/// <param name="financialPeriodId">
		/// The financial period ID in the internal representation. 
		/// It will automatically be formatted for display in the error message.
		/// </param>
		public FinancialPeriodOffsetNotFoundException(string financialPeriodId, int offset)
			: base(string.IsNullOrEmpty(financialPeriodId) 
				? Messages.NoPeriodsDefined 
				: offset == 0 
					? PXLocalizer.LocalizeFormat(Messages.NoFinancialPeriodWithId, FinPeriodIDAttribute.FormatForError(financialPeriodId))
					: offset == -1
						? PXLocalizer.LocalizeFormat(Messages.NoFinancialPeriodBefore, FinPeriodIDAttribute.FormatForError(financialPeriodId))
						: offset == 1
							? PXLocalizer.LocalizeFormat(Messages.NoFinancialPeriodAfter, FinPeriodIDAttribute.FormatForError(financialPeriodId))
							: PXLocalizer.LocalizeFormat(Messages.NoFinancialPeriodForOffset, Math.Abs(offset), FinPeriodIDAttribute.FormatForError(financialPeriodId)))
		{
			FinancialPeriodId = financialPeriodId;
			Offset = offset;
		}
	}
}
