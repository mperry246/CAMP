using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.GL;

namespace PX.Objects.CA
{
	public class CAAPARTranType : ILabelProvider
	{
		public IEnumerable<ValueLabelPair> ValueLabelPairs => listAll;

		protected static readonly ValueLabelPair.KeyComparer Comparer = new ValueLabelPair.KeyComparer();

		protected static readonly ValueLabelList listGL = new ValueLabelList
		{
			new GLTranType().ValueLabelPairs
		};

		protected static readonly ValueLabelList listAR = new ValueLabelList
		{
			new ARDocType().ValueLabelPairs,
			listGL
		};

		protected static readonly ValueLabelList restrictedListAR = new ValueLabelList
		{
			{ARDocType.Invoice,     AR.Messages.Invoice     },
			{ARDocType.CashSale,    AR.Messages.CashSale    },
			{ARDocType.CreditMemo,  AR.Messages.CreditMemo  },
			{ARDocType.DebitMemo,   AR.Messages.DebitMemo   },
			{ARDocType.Payment,     AR.Messages.Payment     },
			{ARDocType.Prepayment,  AR.Messages.Prepayment  }
		};

		protected static readonly ValueLabelList listAP = new ValueLabelList
		{
			new APDocType().ValueLabelPairs,
			listGL,
			{CATranType.CABatch,    AP.Messages.APBatch }
		};

		protected static readonly ValueLabelList restrictedListAP = new ValueLabelList
		{
			{APDocType.Invoice,     AP.Messages.Invoice     },
			{APDocType.QuickCheck,  AP.Messages.QuickCheck  },
			{APDocType.Check,       AP.Messages.Check       },
			{APDocType.CreditAdj,   AP.Messages.CreditAdj   },
			{APDocType.DebitAdj,    AP.Messages.DebitAdj    },
			{APDocType.Prepayment,  AP.Messages.Prepayment  }
		};

		protected static readonly ValueLabelList restrictedListARAPIntersect = new ValueLabelList
		{
			{ARDocType.Invoice,     CA.Messages.ARAPInvoice },
			{ARDocType.Prepayment,  CA.Messages.ARAPPrepayment}
		};

		protected static readonly ValueLabelList listARAPIntersect = new ValueLabelList
		{
			restrictedListARAPIntersect,
			{ARDocType.Refund,      CA.Messages.ARAPRefund  }
		};

		protected static readonly ValueLabelList listCA = new ValueLabelList
		{
			new CATranType().FullValueLabelPairs,
			listGL
		};

		protected static readonly ValueLabelList restrictedListCA = new ValueLabelList
		{
			{CATranType.CAAdjustment,CA.Messages.CAAdjustment}
		};

		protected static readonly ValueLabelList listAll = new ValueLabelList
		{
			new CATranType().FullValueLabelPairs,
			new GLTranType().ValueLabelPairs,
			{ CATranType.CABatch,	AP.Messages.APBatch } ,
			new APDocType().ValueLabelPairs.Except(listARAPIntersect, Comparer),
			new ARDocType().ValueLabelPairs.Except(listARAPIntersect, Comparer),
			listARAPIntersect
		};

		protected static readonly ValueLabelList restrictedListAll = new ValueLabelList
		{
			listGL,
			restrictedListCA,
			restrictedListAP.Except(restrictedListARAPIntersect, Comparer),
			restrictedListAR.Except(restrictedListARAPIntersect, Comparer),
			restrictedListARAPIntersect
		};

		public class ListByModuleRestrictedAttribute : ListByModuleAttribute
		{
			public ListByModuleRestrictedAttribute(Type moduleField)
				: base(moduleField, restrictedListAR, restrictedListAP, restrictedListCA, listGL, restrictedListAll)
			{ }
		}
		public class ListByModuleAttribute : LabelListAttribute
		{
			protected ValueLabelList _listAR;
			protected ValueLabelList _listAP;
			protected ValueLabelList _listCA;
			protected ValueLabelList _listGL;
			protected ValueLabelList _listAll;

			protected Type _ModuleField;
			protected string lastModule;
			protected ListByModuleAttribute(Type moduleField, ValueLabelList listAR, ValueLabelList listAP, ValueLabelList listCA,
				ValueLabelList listGL, ValueLabelList listAll) : base(listAll)
			{
				_ModuleField = moduleField;
				_listAR = listAR;
				_listAP = listAP;
				_listCA = listCA;
				_listGL = listGL;
				_listAll = listAll;
			}
			public ListByModuleAttribute(Type moduleField)
				: this(moduleField, listAR, listAP, listCA,	listGL, listAll)
			{ }

			public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
			{
				string module = (string)sender.GetValue(e.Row, _ModuleField.Name);
				if (module != lastModule)
				{
					if (sender.Graph.GetType() == typeof(PXGraph)) //report mode
					{
						SetNewList(sender, _listAll);
					}
					else
					switch (module)
					{
						case GL.BatchModule.AP:
							SetNewList(sender, _listAP);
							break;
						case GL.BatchModule.AR:
							SetNewList(sender, _listAR);
							break;
						case GL.BatchModule.CA:
							SetNewList(sender, _listCA);
							break;
						case GL.BatchModule.GL:
							SetNewList(sender, _listGL);
							break;
						default:
							SetNewList(sender, _listAll);
							break;
					}
				lastModule = module;
					}
					base.FieldSelecting(sender, e);
				}


			protected void SetNewList(PXCache sender, ValueLabelList valueLabelList)
			{
				List<ListByModuleAttribute> list = new List<ListByModuleAttribute>();
				list.Add(this);
				SetListInternal(list, valueLabelList.Select(pair => pair.Value).ToArray(), valueLabelList.Select(pair => pair.Label).ToArray(), sender);
			}

			protected override void TryLocalize(PXCache sender)
			{
				base.TryLocalize(sender);
				RipDynamicLabels(_listAP.Select(pair => pair.Label).ToArray(), sender);
				RipDynamicLabels(_listAR.Select(pair => pair.Label).ToArray(), sender);
				RipDynamicLabels(_listCA.Select(pair => pair.Label).ToArray(), sender);
				RipDynamicLabels(_listGL.Select(pair => pair.Label).ToArray(), sender);
			}

		}
		#region Backward Compatibility 
		[Obsolete("CAAPARTranType.GLEntry is obsolete and will be removed in Acumatica 2018R1. Use GL.GLTranType.GLEntry instead.")]
		public const string GLEntry = GLTranType.GLEntry;
		[Obsolete("CAAPARTranType.CATransfer is obsolete and will be removed in Acumatica 2018R1. Use CATranType.CATransfer instead.")]
		public const string CATransfer = CATranType.CATransfer;
		[Obsolete("CAAPARTranType.CATransferOut is obsolete and will be removed in Acumatica 2018R1. Use CATranType.CATransferOut instead.")]
		public const string CATransferOut = CATranType.CATransferOut;
		[Obsolete("CAAPARTranType.CATransferIn is obsolete and will be removed in Acumatica 2018R1. Use CATranType.CATransferIn instead.")]
		public const string CATransferIn = CATranType.CATransferIn;
		[Obsolete("CAAPARTranType.CATransferExp is obsolete and will be removed in Acumatica 2018R1. Use CATranType.CATransferExp instead.")]
		public const string CATransferExp = CATranType.CATransferExp;
		[Obsolete("CAAPARTranType.CAAdjustment is obsolete and will be removed in Acumatica 2018R1. Use CATranType.CAAdjustment instead.")]
		public const string CAAdjustment = CATranType.CAAdjustment;
		[Obsolete("CAAPARTranType.CADeposit is obsolete and will be removed in Acumatica 2018R1. Use CATranType.CADeposit instead.")]
		public const string CADeposit = CATranType.CADeposit;
		[Obsolete("CAAPARTranType.CAVoidDeposit is obsolete and will be removed in Acumatica 2018R1. Use CATranType.CAVoidDeposit instead.")]
		public const string CAVoidDeposit = CATranType.CAVoidDeposit;

		[Obsolete("CAAPARTranType.gLEntry is obsolete and will be removed in Acumatica 2018R1. Use GL.GLTranType.gLEntry instead.")]
		public class gLEntry : GLTranType.gLEntry { }
		[Obsolete("CAAPARTranType.cATransfer is obsolete and will be removed in Acumatica 2018R1. Use CATranType.cATransfer instead.")]
		public class cATransfer : CATranType.cATransfer { }
		[Obsolete("CAAPARTranType.cATransferOut is obsolete and will be removed in Acumatica 2018R1. Use CATranType.cATransferOut instead.")]
		public class cATransferOut : CATranType.cATransferOut { }
		[Obsolete("CAAPARTranType.cATransferIn is obsolete and will be removed in Acumatica 2018R1. Use CATranType.cATransferIn instead.")]
		public class cATransferIn : CATranType.cATransferIn { }
		[Obsolete("CAAPARTranType.cATransferExp is obsolete and will be removed in Acumatica 2018R1. Use CATranType.cATransferExp instead.")]
		public class cATransferExp : CATranType.cATransferExp { }
		[Obsolete("CAAPARTranType.cAAdjustment is obsolete and will be removed in Acumatica 2018R1. Use CATranType.cAAdjustment instead.")]
		public class cAAdjustment : CATranType.cAAdjustment { }
		#endregion
	}
}