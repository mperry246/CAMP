using System;
using System.Collections.Generic;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL;

namespace PX.Objects.AP
{
	public class APDocType : ILabelProvider
	{
		public const string Invoice = "INV";
		public const string CreditAdj = "ACR";
		public const string DebitAdj = "ADR";
		public const string Check = "CHK";
		public const string VoidCheck = "VCK";
		public const string Prepayment = "PPM";
		public const string Refund = "REF";
		public const string QuickCheck = "QCK";
		public const string VoidQuickCheck = "VQC";
		public const string PrepaymentRequest = "PPR";

		protected static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = new ValueLabelList
		{
			{ Invoice, Messages.Invoice },
			{ CreditAdj, Messages.CreditAdj },
			{ DebitAdj, Messages.DebitAdj },
			{ Check, Messages.Check },
			{ VoidCheck, Messages.VoidCheck },
			{ Prepayment, Messages.Prepayment },
			{ Refund, Messages.Refund },
			{ QuickCheck, Messages.QuickCheck },
			{ VoidQuickCheck, Messages.VoidQuickCheck },
			{ PrepaymentRequest, Messages.PrepaymentRequest},
		};

		protected static readonly IEnumerable<ValueLabelPair> _valuePrintableLabelPairs = new ValueLabelList
		{
			{ Invoice, Messages.PrintInvoice },
			{ CreditAdj, Messages.PrintCreditAdj },
			{ DebitAdj, Messages.PrintDebitAdj },
			{ Check, Messages.PrintCheck },
			{ VoidCheck, Messages.PrintVoidCheck },
			{ Prepayment, Messages.PrintPrepayment },
			{ Refund,   Messages.PrintRefund },
			{ QuickCheck,   Messages.PrintQuickCheck },
			{ VoidQuickCheck, Messages.PrintVoidQuickCheck },
		};

		public static readonly string[] Values = 
		{
			Invoice,
			CreditAdj,
			DebitAdj,
			Check,
			VoidCheck,
			Prepayment,
			Refund,
			QuickCheck,
			VoidQuickCheck,
			PrepaymentRequest };
		public static readonly string[] Labels =
		{
			Messages.Invoice,
			Messages.CreditAdj,
			Messages.DebitAdj,
			Messages.Check,
			Messages.VoidCheck,
			Messages.Prepayment,
			Messages.Refund,
			Messages.QuickCheck,
			Messages.VoidQuickCheck,
			Messages.PrepaymentRequest
		};

		public IEnumerable<ValueLabelPair> ValueLabelPairs => _valueLabelPairs;

		public class ListAttribute : LabelListAttribute
		{
			public ListAttribute() : base(_valueLabelPairs)
			{ }
		}

		/// <summary>
		/// Defines a Selector of the AP Document types with shorter description.<br/>
		/// In the screens displayed as combo-box.<br/>
		/// Mostly used in the reports.<br/>
		/// </summary>
		public class PrintListAttribute : LabelListAttribute
		{
			public PrintListAttribute() : base(_valuePrintableLabelPairs)
			{ }
		}

		/// <summary>
		/// Defines a list of the AP Document types, which are used for approval.
		/// </summary>
		public class APApprovalDocTypeListAttribute : LabelListAttribute
		{
			private static readonly IEnumerable<ValueLabelPair> _approvalValueLabelPairs = new ValueLabelList
			{
				{ Invoice, Messages.Invoice },
				{ CreditAdj, Messages.CreditAdj},
				{ DebitAdj, Messages.DebitAdj},
				{ PrepaymentRequest, Messages.PrepaymentRequest},
				{ Check, Messages.Check },
				{ QuickCheck, Messages.QuickCheck },
				{ Prepayment, Messages.Prepayment }
			};


			public APApprovalDocTypeListAttribute() : base(_approvalValueLabelPairs)
			{ }
		}


		public class invoice : Constant<string>
		{
			public invoice() : base(Invoice) {; }
		}

		public class creditAdj : Constant<string>
		{
			public creditAdj() : base(CreditAdj) {; }
		}

		public class debitAdj : Constant<string>
		{
			public debitAdj() : base(DebitAdj) {; }
		}

		public class check : Constant<string>
		{
			public check() : base(Check) {; }
		}

		public class voidCheck : Constant<string>
		{
			public voidCheck() : base(VoidCheck) {; }
		}

		public class prepayment : Constant<string>
		{
			public prepayment() : base(Prepayment) {; }
		}

		public class refund : Constant<string>
		{
			public refund() : base(Refund) {; }
		}

		public class quickCheck : Constant<string>
		{
			public quickCheck() : base(QuickCheck) {; }
		}

		public class voidQuickCheck : Constant<string>
		{
			public voidQuickCheck() : base(VoidQuickCheck) {; }
		}
		public class prepaymentRequest : Constant<string>
		{
			public prepaymentRequest() : base(PrepaymentRequest) {; }
		}

		public static string DocClass(string DocType)
		{
			switch (DocType)
			{
				case Invoice:
				case CreditAdj:
				case DebitAdj:
				case QuickCheck:
				case VoidQuickCheck:
					return GLTran.tranClass.Normal;
				case Check:
				case VoidCheck:
				case Refund:
					return GLTran.tranClass.Payment;
				case Prepayment:
					return GLTran.tranClass.Charge;
				default:
					return null;
			}
		}

		public static bool? Payable(string DocType)
		{
			switch (DocType)
			{
				case Invoice:
				case CreditAdj:
					return true;
				case Check:
				case DebitAdj:
				case VoidCheck:
				case Prepayment:
				case Refund:
				case QuickCheck:
				case VoidQuickCheck:
					return false;
				default:
					return null;
			}
		}

		public static Int16? SortOrder(string DocType)
		{
			switch (DocType)
			{
				case Invoice:
				case CreditAdj:
				case QuickCheck:
					return 0;
				case Prepayment:
					return 1;
				case DebitAdj:
					return 2;
				case Check:
					return 3;
				case VoidCheck:
				case VoidQuickCheck:
					return 4;
				case Refund:
					return 5;
				default:
					return null;
			}
		}

		public static Decimal? SignBalance(string DocType)
		{
			switch (DocType)
			{
				case Refund:
				case Invoice:
				case CreditAdj:
					return 1m;
				case DebitAdj:
				case Check:
				case VoidCheck:
					return -1m;
				case Prepayment:
					return -1m;
				case QuickCheck:
				case VoidQuickCheck:
					return 0m;
				default:
					return null;
			}
		}

		public static Decimal? SignAmount(string DocType)
		{
			switch (DocType)
			{
				case Refund:
				case Invoice:
				case CreditAdj:
				case QuickCheck:
					return 1m;
				case DebitAdj:
				case Check:
				case VoidCheck:
				case VoidQuickCheck:
					return -1m;
				case Prepayment:
					return -1m;
				default:
					return null;
			}
		}

		public static string TaxDrCr(string DocType)
		{
			switch (DocType)
			{
				//Invoice Types
				case Invoice:
				case CreditAdj:
				case QuickCheck:
					return DrCr.Debit;
				case DebitAdj:
				case VoidQuickCheck:
					return DrCr.Credit;
				//Payment Types
				case Check:
				case Prepayment:
				case VoidCheck:
					return DrCr.Debit;
				case Refund:
					return DrCr.Credit;
				default:
					return DrCr.Debit;
			}
		}
		public static bool? HasNegativeAmount(string docType)
		{
			switch (docType)
			{
				case Refund:
				case Invoice:
				case CreditAdj:
				case QuickCheck:
				case DebitAdj:
				case Check:
				case Prepayment:
				case VoidQuickCheck:
					return false;
				case VoidCheck:
					return true;
				default:
					return null;
			}
		}

		/// <summary>
		/// Query if "Pre-Release" process allowed for the AP document type.
		/// </summary>
		/// <param name="docType">Type of the AP document.</param>
		/// <returns/>
		public static bool IsPrebookingAllowedForType(string docType)
		{
			switch (docType)
			{
				case Invoice:
				case CreditAdj:
				case DebitAdj:
				case QuickCheck:
					return true;
				default:
					return false;
			}
		}
	}
}

