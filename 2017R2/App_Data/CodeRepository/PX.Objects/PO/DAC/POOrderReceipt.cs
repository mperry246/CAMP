namespace PX.Objects.PO
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public partial class POOrderReceipt : PX.Data.IBqlTable
	{
		#region ReceiptNbr
		public abstract class receiptNbr : PX.Data.IBqlField
		{
		}
		protected String _ReceiptNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDBDefault(typeof(POReceipt.receiptNbr))]
		[PXDefault()]
		[PXParent(typeof(Select<POReceipt,Where<POReceipt.receiptNbr,Equal<Current<POOrderReceipt.receiptNbr>>>>))]
		public virtual String ReceiptNbr
		{
			get
			{
				return this._ReceiptNbr;
			}
			set
			{
				this._ReceiptNbr = value;
			}
		}
		#endregion
		#region POType
		public abstract class pOType : PX.Data.IBqlField
		{
		}
		protected String _POType;
		[PXDBString(2, IsKey = true, IsFixed = true)]
		[PXDefault()]
		public virtual String POType
		{
			get
			{
				return this._POType;
			}
			set
			{
				this._POType = value;
			}
		}
		#endregion
		#region PONbr
		public abstract class pONbr : PX.Data.IBqlField
		{
		}
		protected String _PONbr;
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDefault()]
		public virtual String PONbr
		{
			get
			{
				return this._PONbr;
			}
			set
			{
				this._PONbr = value;
			}
		}
		#endregion
		#region ReceiptNoteID
		public abstract class receiptNoteID : PX.Data.IBqlField
		{
		}
		protected Guid? _ReceiptNoteID;
		[PXDBGuid(IsImmutable = true)]
		[PXDefault(typeof(POReceipt.noteID), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Guid? ReceiptNoteID
		{
			get
			{
				return this._ReceiptNoteID;
			}
			set
			{
				this._ReceiptNoteID = value;
			}
		}
		#endregion
		#region OrderNoteID
		public abstract class orderNoteID : PX.Data.IBqlField
		{
		}
		protected Guid? _OrderNoteID;
		[SO.SOOrderShipment.CopiedNoteID]
		public virtual Guid? OrderNoteID
		{
			get
			{
				return this._OrderNoteID;
			}
			set
			{
				this._OrderNoteID = value;
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
	}
}
