using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Objects.Common;

namespace PX.Objects.CA
{
	public class DepositType : CATranType
	{
		public override IEnumerable<ValueLabelPair> ValueLabelPairs => DepositValueLabelList;
	}

	public class CATranType : ILabelProvider
	{
		protected static readonly ValueLabelList ValueLabelList = new ValueLabelList
		{
			{ CATransferOut,	Messages.CATransferOut},
			{ CATransferIn,		Messages.CATransferIn },
			{ CATransferExp,	Messages.CATransferExp},
			{ CAAdjustment,		Messages.CAAdjustment }
		};

		protected static readonly ValueLabelList DepositValueLabelList = new ValueLabelList
		{
			{ CADeposit,		Messages.CADeposit},
			{ CAVoidDeposit,	Messages.CAVoidDeposit }
		};

		protected static readonly ValueLabelList FullValueLabelList = new ValueLabelList
		{
			ValueLabelList,
			DepositValueLabelList,
			{CATransfer,		Messages.CATransfer }
		};

		public virtual IEnumerable<ValueLabelPair> ValueLabelPairs => ValueLabelList;

		public IEnumerable<ValueLabelPair> FullValueLabelPairs => FullValueLabelList;

		public static readonly string[] Values = ValueLabelList.Select(pair => pair.Value).ToArray();
		public static readonly string[] Labels = ValueLabelList.Select(pair => pair.Label).ToArray();

		public class ListAttribute : LabelListAttribute
		{
			public ListAttribute()
				: base(ValueLabelList)
			{ }
		}

		/// <summary>
		/// Selector. Defines a list of possible CADeposit types - namely <br/>
		/// CADeposit and CAVoidDeposit <br/>
		/// <example>
		/// [CATranType.DepositList()]
		/// </example>
		/// </summary>
		public class DepositListAttribute : LabelListAttribute
		{
			public DepositListAttribute()
				: base(DepositValueLabelList)
			{ }
		}

		public const string CATransferOut = "CTO";
		public const string CATransferIn = "CTI";
		public const string CATransferExp = "CTE";
		public const string CATransferRGOL = "CTG";
		public const string CAAdjustment = "CAE";
		////<summary>This tran type was previously used for base currency rounding in the CA Release.</summary>
		public const string CAAdjustmentRGOL = "CAG";
		public const string CADeposit = "CDT";
		public const string CAVoidDeposit = "CVD";
		public const string CABatch = "CBT";
		public const string CATransfer = "CT%";

		public class cATransferOut : Constant<string>
		{
			public cATransferOut() : base(CATransferOut) { }
		}

		public class cATransferIn : Constant<string>
		{
			public cATransferIn() : base(CATransferIn) { }
		}

		public class cATransferExp : Constant<string>
		{
			public cATransferExp() : base(CATransferExp) { }
		}

		public class cAAdjustment : Constant<string>
		{
			public cAAdjustment() : base(CAAdjustment) { }
		}

		public class cADeposit : Constant<string>
		{
			public cADeposit() : base(CADeposit) { }
		}

		public class cAVoidDeposit : Constant<string>
		{
			public cAVoidDeposit() : base(CAVoidDeposit) { }
		}

		public class cABatch : Constant<string>
		{
			public cABatch() : base(CABatch) { }
		}

		public class cATransfer : Constant<string>
		{
			public cATransfer() : base(CATransfer) { }
		}

		public static bool IsTransfer(string tranType)
		{
			return tranType == CATransferOut || tranType == CATransferIn;
		}
	}
}
