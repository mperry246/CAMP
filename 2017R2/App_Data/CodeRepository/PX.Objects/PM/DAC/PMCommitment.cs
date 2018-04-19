using System;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.CM;
using PX.Objects.GL;
using System.Text;

namespace PX.Objects.PM
{
	[PXCacheName(Messages.Commitment)]
	[Serializable]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public partial class PMCommitment : PX.Data.IBqlTable, IProjectFilter, IQuantify
	{		
		#region CommitmentID
		public abstract class commitmentID : IBqlField
		{
		}
		protected Guid? _CommitmentID;
		[PXDefault]
		[PXDBGuid(IsKey =true)]
		public virtual Guid? CommitmentID
		{
			get
			{
				return _CommitmentID;
			}
			set
			{
				_CommitmentID = value;
			}
		}
		#endregion

		#region Type
		public abstract class type : PX.Data.IBqlField
		{
		}
		protected string _Type;
		[PXDBString(1)]
		[PXDefault(PMCommitmentType.Internal)]
		[PMCommitmentType.List()]
		[PXUIField(DisplayName = "Type")]
		public virtual string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}
		#endregion
		#region AccountGroupID
		public abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountGroupID;
		[PXDefault]
		[AccountGroup()]
		public virtual Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
		[PXDefault]
		[PXRestrictor(typeof(Where<PMProject.nonProject, Equal<False>>), PM.Messages.NonProjectCodeIsInvalid)]
		[PXRestrictor(typeof(Where<PMProject.isCancelled, Equal<False>>), PM.Messages.CancelledContract, typeof(PMProject.contractCD))]
		[ProjectBase]
		public virtual Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ProjectTaskID
		public abstract class projectTaskID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectTaskID;
		[PXDefault]
		[ActiveOrInPlanningProjectTask(typeof(PMCommitment.projectID))]
		public virtual Int32? ProjectTaskID
		{
			get
			{
				return this._ProjectTaskID;
			}
			set
			{
				this._ProjectTaskID = value;
			}
		}
		public int? TaskID
		{
			get { return ProjectTaskID; }
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[PXUIField(DisplayName = "Inventory ID")]
		[PXDBInt()]
		[PMInventorySelector]
		public virtual Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region CostCodeID
		public abstract class costCodeID : PX.Data.IBqlField
		{
		}
		protected Int32? _CostCodeID;
		[CostCode]
		public virtual Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion
		#region ExtRefNbr
		public abstract class extRefNbr : PX.Data.IBqlField
		{
		}
		protected String _ExtRefNbr;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "External Ref. Nbr")]
		public virtual String ExtRefNbr
		{
			get
			{
				return this._ExtRefNbr;
			}
			set
			{
				this._ExtRefNbr = value;
			}
		}
		#endregion
		#region UOM
		public abstract class uOM : PX.Data.IBqlField
		{
		}
		protected String _UOM;
		[PMUnit(typeof(PMCommitment.inventoryID))]
		public virtual String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		#region Qty
		public abstract class qty : PX.Data.IBqlField
		{
		}
		protected Decimal? _Qty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Quantity")]
		public virtual Decimal? Qty
		{
			get
			{
				return this._Qty;
			}
			set
			{
				this._Qty = value;
			}
		}
		#endregion
		#region Amount
		public abstract class amount : PX.Data.IBqlField
		{
		}
		protected Decimal? _Amount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount")]
		public virtual Decimal? Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				this._Amount = value;
			}
		}
		#endregion
		#region ReceivedQty
		public abstract class receivedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _ReceivedQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Received Quantity")]
		public virtual Decimal? ReceivedQty
		{
			get
			{
				return this._ReceivedQty;
			}
			set
			{
				this._ReceivedQty = value;
			}
		}
		#endregion
		#region InvoicedQty
		public abstract class invoicedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _InvoicedQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Invoiced Quantity")]
		public virtual Decimal? InvoicedQty
		{
			get
			{
				return this._InvoicedQty;
			}
			set
			{
				this._InvoicedQty = value;
			}
		}
		#endregion
		#region InvoicedAmount
		public abstract class invoicedAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _InvoicedAmount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Invoiced Amount")]
		public virtual Decimal? InvoicedAmount
		{
			get
			{
				return this._InvoicedAmount;
			}
			set
			{
				this._InvoicedAmount = value;
			}
		}
		#endregion
		#region InvoicedIsReadonly
		public abstract class invoicedIsReadonly : IBqlField
		{
		}
		protected bool? _InvoicedIsReadonly = false;
		[PXBool]
		[PXDefault(false)]
		public virtual bool? InvoicedIsReadonly
		{
			get
			{
				return _InvoicedIsReadonly;
			}
			set
			{
				_InvoicedIsReadonly = value;
			}
		}
		#endregion

		#region OpenQty
		public abstract class openQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _OpenQty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Open Quantity")]
		public virtual Decimal? OpenQty
		{
			get
			{
				return this._OpenQty;
			}
			set
			{
				this._OpenQty = value;
			}
		}
		#endregion
		#region OpenAmount
		public abstract class openAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _OpenAmount;
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Open Amount")]
		public virtual Decimal? OpenAmount
		{
			get
			{
				return this._OpenAmount;
			}
			set
			{
				this._OpenAmount = value;
			}
		}
		#endregion
		#region RefNoteID
		public abstract class refNoteID : PX.Data.IBqlField
		{
		}
		protected Guid? _RefNoteID;
		[PXUIField(DisplayName = "Related Document")]
		[PXRefNote()]
		public virtual Guid? RefNoteID
		{
			get
			{
				return this._RefNoteID;
			}
			set
			{
				this._RefNoteID = value;
			}
		}
		public class PXRefNoteAttribute : PX.Data.PXRefNoteAttribute
		{
			public PXRefNoteAttribute()
				: base()
			{
			}

			public class PXLinkState : PXStringState
			{
				protected object[] _keys;
				protected Type _target;

				public object[] keys
				{
					get { return _keys; }
				}

				public Type target
				{
					get { return _target; }
				}

				public PXLinkState(object value)
					: base(value)
				{
				}

				public static PXFieldState CreateInstance(object value, Type target, object[] keys)
				{
					PXLinkState state = value as PXLinkState;
					if (state == null)
					{
						PXFieldState field = value as PXFieldState;
						if (field != null && field.DataType != typeof(object) && field.DataType != typeof(string))
						{
							return field;
						}
						state = new PXLinkState(value);
					}
					if (target != null)
					{
						state._target = target;
					}
					if (keys != null)
					{
						state._keys = keys;
					}

					return state;
				}
			}

			public override void CacheAttached(PXCache sender)
			{
				base.CacheAttached(sender);

				PXButtonDelegate del = delegate (PXAdapter adapter)
				{
					PXCache cache = adapter.View.Graph.Caches[typeof(PMCommitment)];
					if (cache.Current != null)
					{
						object val = cache.GetValueExt(cache.Current, _FieldName);

						PXLinkState state = val as PXLinkState;
						if (state != null)
						{
							helper.NavigateToRow(state.target.FullName, state.keys, PXRedirectHelper.WindowMode.NewWindow);
						}
						else
						{
							helper.NavigateToRow((Guid?)cache.GetValue(cache.Current, _FieldName), PXRedirectHelper.WindowMode.NewWindow);
						}
					}

					return adapter.Get();
				};

				string ActionName = sender.GetItemType().Name + "$" + _FieldName + "$Link";
				sender.Graph.Actions[ActionName] = (PXAction)Activator.CreateInstance(typeof(PXNamedAction<>).MakeGenericType(typeof(CommitmentInquiry.ProjectBalanceFilter)), new object[] { sender.Graph, ActionName, del, new PXEventSubscriberAttribute[] { new PXUIFieldAttribute { MapEnableRights = PXCacheRights.Select } } });
			}

			public virtual object GetEntityRowID(PXCache cache, object[] keys)
			{
				return GetEntityRowID(cache, keys, ", ");
			}

			public static object GetEntityRowID(PXCache cache, object[] keys, string separator)
			{
				StringBuilder result = new StringBuilder();
				int i = 0;
				foreach (string key in cache.Keys)
				{
					if (i >= keys.Length) break;
					object val = keys[i++];
					cache.RaiseFieldSelecting(key, null, ref val, true);

					if (val != null)
					{
						if (result.Length != 0) result.Append(separator);
						result.Append(val.ToString().TrimEnd());
					}
				}
				return result.ToString();
			}
		}

		#endregion

		#region System Columns
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Guid? _NoteID;
		[PXNote]
		public virtual Guid? NoteID
		{
			get
			{
				return this._NoteID;
			}
			set
			{
				this._NoteID = value;
			}
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		protected Byte[] _tstamp;
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get
			{
				return this._tstamp;
			}
			set
			{
				this._tstamp = value;
			}
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected String _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
		[PXDBCreatedDateTime]
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
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected String _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
		#endregion
	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public static class PMCommitmentType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Internal, External },
				new string[] { Messages.CommitmentType_Internal, Messages.CommitmentType_External })
			{ }


		}

		public const string Internal = "I";
		public const string External = "E";
		public class internalType : Constant<string>
		{
			public internalType() : base(Internal) {; }
		}
		public class externalType : Constant<string>
		{
			public externalType() : base(External) {; }
		}

	}
}
