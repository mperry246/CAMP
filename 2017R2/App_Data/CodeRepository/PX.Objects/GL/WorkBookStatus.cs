using PX.Data;

namespace PX.Objects.GL
{
	public class WorkBookStatus
	{
        /// <summary>
        /// The active status of the workbook.
        /// This status means that the workbook is visible in selector on the Workbooks (GL307000) form and new voucher batches can be added to the workbook.
        /// </summary>
        public const short Active = (short)0;
        /// <summary>
        /// The inactive (disabled) status of the workbook.
        /// This status means that the workbook is visible in selector on the Workbooks (GL307000) form and new voucher batches cannot be added to the workbook.
        /// </summary>
        public const short Inactive = (short)1;
        /// <summary>
        /// The hidden status of the workbook.
        /// This status means that the workbook is not visible in selector on the Workbooks (GL307000) form and new voucher batches cannot be added to the workbook.
        /// </summary>
        public const short Hidden = (short)2;

		public const string ActiveName = "Active";
		public const string InactiveName = "Inactive";
		public const string HiddenName = "Hidden";

		public class active : Constant<short>
		{
			public active() : base(Active) { }
		}

		public class inactive : Constant<short>
		{
			public inactive() : base(Inactive) { }
		}

		public class hidden : Constant<short>
		{
			public hidden() : base(Hidden) { }
		}
	}
}
