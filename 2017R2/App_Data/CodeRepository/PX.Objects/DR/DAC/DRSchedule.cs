using System;
using System.Diagnostics;

using PX.Data;
using PX.Data.EP;

using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.AR;
using PX.Objects.AP;
using PX.Objects.PM;

namespace PX.Objects.DR
{
	/// <summary>
	/// Encapsulates information about a deferred revenue or deferred expense 
	/// recognition schedule produced by an Accounts Payable 
	/// or an Accounts Receivable document line.
	/// The entities of this type are usually created automatically upon the release
	/// of the document line (see e.g. <see cref="ARTran"/>) that has a 
	/// <see cref="DRDeferredCode">deferral code</see> specified. A user can also
	/// create a custom deferral schedule (or edit an existing one) by using the 
	/// Deferral Schedule (DR201500) form, which corresponds to the
	/// <see cref="DraftScheduleMaint"/> graph.
	/// </summary>
	[Serializable]
	[DebuggerDisplay("SheduleID={ScheduleID} DocType={DocType} RefNbr={RefNbr}")]
	[PXPrimaryGraph(typeof(DraftScheduleMaint))]
	[PXCacheName(Messages.Schedule)]
	public partial class DRSchedule : PX.Data.IBqlTable
	{
		#region ScheduleID
		public abstract class scheduleID : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// The unique integer identifier of the deferral schedule.
		/// </summary>
		[PXDBIdentity]
		[PXFieldDescription]
		public virtual int? ScheduleID
		{
			get;
			set;
		}
		#endregion

		#region ScheduleNbr
		public abstract class scheduleNbr : PX.Data.IBqlField { }

		[PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault()]
		[PXUIField(DisplayName = Messages.ScheduleNbr)]
		[AutoNumber(typeof(DRSetup.scheduleNumberingID), typeof(DRSchedule.docDate))]
		[PXParent(
			typeof(Select<ARTran, 
				Where<ARTran.tranType, Equal<Current<DRSchedule.docType>>, 
					And<ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, 
					And<ARTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>,
					And<BatchModule.moduleAR, Equal<Current<DRSchedule.module>>>>>>>))]
		[PXParent(
			typeof(Select<APTran, 
				Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, 
					And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, 
					And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>,
					And<BatchModule.moduleAP, Equal<Current<DRSchedule.module>>>>>>>))]
		[PXSelector(
			typeof(DRSchedule.scheduleNbr),
			typeof(DRSchedule.scheduleNbr),
			typeof(DRSchedule.documentTypeEx),
			typeof(DRSchedule.refNbr),
			typeof(DRSchedule.bAccountID))]
		public virtual string ScheduleNbr {get; set;}
		#endregion

		#region Module
		public abstract class module : PX.Data.IBqlField
		{
		}
		protected string _Module;
		/// <summary>
		/// The module from which the deferral schedule originates.
		/// </summary>
		/// <value>
		/// This field can have one of the following values:
		/// <c>"AR"</c>: Accounts Receivable, 
		/// <c>"AP"</c>: Accounts Payable.
		/// If the module specified is Accounts Payable, 
		/// the record is a deferred expense recognition schedule.
		/// If the module specified is Accounts Receivable, 
		/// the record is a deferred revenue recognition schedule.
		/// The value of this field depends on the value of the
		/// <see cref="DocumentTypeEx"/> field.
		/// </value>
		[PXDBString(2, IsFixed = true)]
		[PXDefault(BatchModule.AR)]
		[PXUIField(DisplayName = "Module")]
		[PX.Data.EP.PXFieldDescription]
		public virtual string Module
		{
			get
			{
				return this._Module;
			}
			set
			{
				this._Module = value;
			}
		}
		#endregion
		#region DocType
		public abstract class docType : PX.Data.IBqlField
		{
		}
		protected string _DocType;
		/// <summary>
		/// The type of the document that the deferral
		/// schedule corresponds to.
		/// </summary>
		/// <value>
		/// Corresponds to either the <see cref="ARTran.TranType"/>
		/// field or the <see cref="APTran.TranType"/> field.
		/// This field can have one of the values defined
		/// by the <see cref="ARDocType"/> or <see cref="APDocType"/> class.
		/// </value>
		[PXDefault(ARDocType.Invoice)]
		[PXDBString(3, IsFixed = true)]
		[PXUIField(DisplayName = "Doc. Type", Enabled = true)]
		[PX.Data.EP.PXFieldDescription]
		[ARDocType.List()]
		public virtual string DocType
		{
			get
			{
				return this._DocType;
			}
			set
			{
				this._DocType = value;
			}
		}
		#endregion
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}
		protected string _RefNbr;
		/// <summary>
		/// The reference number of the document that
		/// the deferral schedule corresponds to.
		/// </summary>
		/// <value>
		/// Corresponds to either the <see cref="ARTran.RefNbr"/>
		/// or the <see cref="APTran.RefNbr"/> field.
		/// This field can be empty for custom deferral
		/// schedules that are not attached to any document.
		/// </value>
		[PX.Data.EP.PXFieldDescription]
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Ref. Nbr.", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[DRDocumentSelector(typeof(DRSchedule.module), typeof(DRSchedule.docType), typeof(DRSchedule.bAccountID))]
		[PXFormula(typeof(Default<DRSchedule.documentType>))]
		public virtual string RefNbr
		{
			get
			{
				return this._RefNbr;
			}
			set
			{
				this._RefNbr = value;
			}
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected int? _LineNbr;
		/// <summary>
		/// The number of the document line, which produced the deferral schedule.
		/// </summary>
		/// <value>
		/// Corresponds to either the <see cref="ARTran.LineNbr"/>
		/// or the <see cref="APTran.LineNbr"/> field.
		/// </value>
		[PXDBInt]
		[PXUIField(DisplayName = Messages.LineNumber)]
		[DRLineSelector(typeof(DRSchedule.module), typeof(DRSchedule.docType), typeof(DRSchedule.refNbr))]
		[PXUIRequired(typeof(Where<DRSchedule.refNbr, IsNotNull>))]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region DocDate
		public abstract class docDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _DocDate;
		/// <summary>
		/// The date of the document, which contains the document line
		/// from which the deferral schedule originates.
		/// </summary>
		/// <value>
		/// Corresponds to either the <see cref="ARRegister.DocDate"/>
		/// or the <see cref="APRegister.DocDate"/> field.
		/// For custom deferral schedules, which are not attached to a document line,
		/// the value of the field is specified by the user manually and
		/// defaults to the <see cref="AccessInfo.BusinessDate">current 
		/// business date</see>.
		/// </value>
		[PXDBDate()]
		[PXDefault(TypeCode.DateTime, "01/01/1900")]
		[PXUIField(DisplayName = Messages.Date, Enabled = false)]
		public virtual DateTime? DocDate
		{
			get
			{
				return this._DocDate;
			}
			set
			{
				this._DocDate = value;
			}
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		protected string _FinPeriodID;
		/// <summary>
		/// The financial period of the document, which contains the
		/// document line from which the deferral schedule originates.
		/// </summary>
		/// <value>
		/// Corresponds to either the <see cref="ARRegister.FinPeriodID"/>
		/// or the <see cref="APRegister.FinPeriodID"/> field.
		/// For custom deferral schedules, the value of this field
		/// is determined by the <see cref="DocDate"/> field.
		/// </value>
		[PXDefault]
		[AROpenPeriod(typeof(DRSchedule.docDate))]
		[PXUIField(DisplayName = "Fin. Period", Enabled = false)]
		public virtual string FinPeriodID
		{
			get
			{
				return this._FinPeriodID;
			}
			set
			{
				this._FinPeriodID = value;
			}
		}
		#endregion
		#region BAccountID
		public abstract class bAccountID : PX.Data.IBqlField
		{
		}
		protected int? _BAccountID;
		/// <summary>
		/// The unique identifier of the <see cref="Customer"/>
		/// or <see cref="Vendor"/> record, which is associated with 
		/// the document line from which the deferral schedule originates.
		/// </summary>
		/// <value>
		/// Corresponds to either the <see cref="ARTran.CustomerID"/>
		/// or the <see cref="APTran.VendorID"/> field.
		/// For custom deferral schedules, the value of this
		/// field is specified by the user.
		/// </value>
		[PXDBInt]
		[PXSelector(
			typeof(Search<
				BAccountR.bAccountID, 
				Where<
					BAccountR.type, Equal<Current<DRSchedule.bAccountType>>,
					Or<BAccount.type, Equal<BAccountType.combinedType>,
					Or<Where<
						Current<DRSchedule.bAccountType>, Equal<BAccountType.vendorType>,
						And<BAccountR.type, Equal<BAccountType.employeeType>>>>>>>), 
			typeof(BAccountR.acctCD), 
			typeof(BAccountR.acctName), 
			typeof(BAccountR.type), 
			SubstituteKey = typeof(BAccountR.acctCD),
			DescriptionField = typeof(BAccountR.acctName))]
		[PXUIField(DisplayName = Messages.BusinessAccount, Visibility = PXUIVisibility.SelectorVisible, Required = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Null)]
		[PXFormula(typeof(Default<DRSchedule.documentType>))]
		public virtual int? BAccountID
		{
			get
			{
				return this._BAccountID;
			}
			set
			{
				this._BAccountID = value;
			}
		}
		#endregion
		#region BAccountLocID
		public abstract class bAccountLocID : PX.Data.IBqlField
		{
		}
		protected int? _BAccountLocID;
		/// <summary>
		/// The unique identifier of the <see cref="Location">location</see> 
		/// of the <see cref="BAccount">business account</see> associated
		/// with the schedule.
		/// This field affects the way <see cref="DRScheduleDetail.SubID">
		/// deferral components' subaccounts</see> are calculated during 
		/// schedule creation. For details, see <see cref="ScheduleCreator"/>.
		/// </summary>
		/// <value>
		/// Corresponds to either the <see cref="ARRegister.CustomerLocationID"/>
		/// or the <see cref="APRegister.VendorLocationID"/> field. 
		/// For manually created custom deferral schedules, the value defaults to the default 
		/// <see cref="Location"> of the <see cref="BAccount">business account</see> 
		/// specified by the <see cref="BAccountID"/> field.
		/// </value>
		[PXDefault(typeof(Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<DRSchedule.bAccountID>>>>))]
		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<DRSchedule.bAccountID>>>))]
		public virtual int? BAccountLocID
		{
			get
			{
				return this._BAccountLocID;
			}
			set
			{
				this._BAccountLocID = value;
			}
		}
		#endregion
		#region TranDesc
		public abstract class tranDesc : PX.Data.IBqlField
		{
		}
		protected string _TranDesc;
		/// <summary>
		/// The description of the document line from which the
		/// deferral schedule originates.
		/// </summary>
		/// <value>
		/// Corresponds to either the <see cref="ARTran.TranDesc"/>
		/// or <see cref="APTran.TranDesc"/> field.
		/// </value>
		[PXDBString(256, IsUnicode = true)]
		[PXUIField(DisplayName = "Transaction Descr.", Visibility = PXUIVisibility.Visible)]
		public virtual string TranDesc
		{
			get
			{
				return this._TranDesc;
			}
			set
			{
				this._TranDesc = value;
			}
		}
		#endregion
		#region IsCustom
		public abstract class isCustom : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsCustom;
		/// <summary>
		/// Indicates (if set to <c>true</c>) that the deferral schedule
		/// has been created manually by the user.
		/// </summary>
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Is Custom", Enabled = false)]
		public virtual Boolean? IsCustom
		{
			get
			{
				return this._IsCustom;
			}
			set
			{
				this._IsCustom = value;
			}
		}
		#endregion
		#region IsDraft
		public abstract class isDraft : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsDraft;
		/// <summary>
		/// Indicates (if set to <c>true</c>) that the deferral schedule
		/// is in draft mode, which allows the user to add and edit
		/// <see cref="DRScheduleDetail">schedule components</see>.
		/// This flag is reset to <c>false</c> after the release of
		/// any schedule components.
		/// </summary>
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Is Draft", Visible = false)]
		public virtual Boolean? IsDraft
		{
			get
			{
				return this._IsDraft;
			}
			set
			{
				this._IsDraft = value;
			}
		}
		#endregion
		#region ProjectID
		public abstract class projectID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="PMProject">
		/// project</see> associated with the schedule.
		/// This field affects the way <see cref="DRScheduleDetail.SubID">
		/// deferral components' subaccounts</see> are calculated during 
		/// schedule creation. For details, see <see cref="ScheduleCreator"/>.
		/// </summary>
		[ProjectDefault]
		[PXDimensionSelector(ProjectAttribute.DimensionName,
			typeof(Search<PMProject.contractID, Where<PMProject.isTemplate, NotEqual<True>>>),
			typeof(PMProject.contractCD),
			typeof(PMProject.contractCD), typeof(PMProject.customerID), typeof(PMProject.description), typeof(PMProject.status), DescriptionField = typeof(PMProject.description))]
		[PXDBInt]
		[PXUIField(DisplayName = "Project", Visibility = PXUIVisibility.Visible)]
		[PXFormula(typeof(Default<DRSchedule.documentType>))]
		[PXUIVerify(typeof(Where<
			DRSchedule.projectID, IsNull,
			Or<
				Selector<DRSchedule.bAccountID, BAccount.type>,
				Equal<BAccountType.vendorType>,
			Or<
				Selector<DRSchedule.projectID, PMProject.nonProject>,
				Equal<True>,
			Or<
				Selector<DRSchedule.projectID, PMProject.customerID>, 
				Equal<DRSchedule.bAccountID>>>>>),
			PXErrorLevel.Warning,
			PM.Warnings.ProjectCustomerDontMatchTheDocument)]
		public virtual int? ProjectID { get; set; }
		#endregion
		#region TaskID
		public abstract class taskID : IBqlField { }
		/// <summary>
		/// The unique identifier of the <see cref="PMTask">project 
		/// task</see> associated with the schedule.
		/// </summary>
		[ProjectTask(typeof(DRSchedule.projectID), DisplayName = "Project Task", AllowNullIfContract = true)]
		[PXFormula(typeof(Default<DRSchedule.documentType>))]
		public virtual int? TaskID { get; set; }
		#endregion

		#region DocumentType
		public abstract class documentType : PX.Data.IBqlField
		{
		}
		protected string _DocumentType;
		/// <summary>
		/// An extension of the <see cref="DocType"/>
		/// field that is used to disambiguate between
		/// Accounts Payable bills and Accounts Receivable
		/// invoices, both of which have the <c>"INV"</c>
		/// document type value.
		/// </summary>
		/// <value>
		/// This field can take one of the values defined
		/// by <see cref="DRScheduleDocumentType.ListAttribute"/>.
		/// </value>
		[PXString(3, IsFixed = true)]
		[DRScheduleDocumentType.List]
		[PXUIField(DisplayName = "Doc. Type", Enabled = false, Required = true)]
		public virtual string DocumentType
		{
			get
			{
				return this._DocumentType;
			}
			set
			{
				this._DocumentType = value;
			}
		}
		#endregion
		#region BAccountType
		public abstract class bAccountType : PX.Data.IBqlField
		{
		}
		protected string _BAccountType;
		/// <summary>
		/// The type of the <see cref="BAccount">business account</see>
		/// defined by the <see cref="BAccountID"/> field.
		/// </summary>
		/// <value>
		/// This field can have one of the following values:
		/// <c>"VE": Vendor</c>,
		/// <c>"CU": Customer</c>.
		/// </value>
		[PXUIField(DisplayName = "Entity Type")]
		[PXDefault(CR.BAccountType.CustomerType, PersistingCheck=PXPersistingCheck.Nothing)]
		[PXString(2, IsFixed = true)]
		[PXStringList(
			new string[] { CR.BAccountType.VendorType, CR.BAccountType.CustomerType, CR.BAccountType.EmployeeType },
			new string[] { CR.Messages.VendorType, CR.Messages.CustomerType, CR.Messages.EmployeeType })]
		public virtual string BAccountType
		{
			get
			{
				return this._BAccountType;
			}
			set
			{
				this._BAccountType = value;
			}
		}
		#endregion
		#region OrigLineAmt
		public abstract class origLineAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _OrigLineAmt;
		/// <summary>
		/// The original amount of the document line from which the
		/// deferral schedule originates.
		/// </summary>
		/// <value>
		/// Corresponds to either the <see cref="ARTran.TranAmt"/>
		/// or the <see cref="APTran.TranAmt"/> field.
		/// For custom schedules, which do not have a reference
		/// to a document line, this field has the <c>null</c>
		/// value.
		/// </value>
		[PXBaseCury]
		[PXUIField(DisplayName = Messages.LineAmount, Enabled = false)]
		public virtual Decimal? OrigLineAmt
		{
			get
			{
				return this._OrigLineAmt;
			}
			set
			{
				this._OrigLineAmt = value;
			}
		}
		#endregion
		#region DocumentTypeEx
		public abstract class documentTypeEx : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// A human-readable representation of the source document type.
		/// </summary>
		[PXString(3, IsFixed = true)]
		[DRScheduleDocumentType.List]
		[PXUIField(DisplayName = "Doc. Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual string DocumentTypeEx
		{
			[PXDependsOnFields(typeof(module), typeof(docType))]
			get
			{
				return DRScheduleDocumentType.BuildDocumentType(Module, DocType);
			}
		}
		#endregion

		#region TermStartDate
		public abstract class termStartDate : IBqlField { }

		protected DateTime? _TermStartDate;
		/// <summary>
		/// Defines the term start date for <see cref="DRScheduleDetail">
		/// deferral components</see> that have a flexible deferral code
		/// specified.
		/// </summary>
		[PXDBDate]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Term Start Date")]
		public virtual DateTime? TermStartDate
		{
			get { return _TermStartDate; }
			set { _TermStartDate = value; }
		}
		#endregion
		#region TermEndDate
		public abstract class termEndDate : IBqlField { }

		protected DateTime? _TermEndDate;
		/// <summary>
		/// Defines the term end date for <see cref="DRScheduleDetail">
		/// deferral components</see> that have a flexible deferral code
		/// specified.
		/// </summary>
		[PXDBDate]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Term End Date")]
		public virtual DateTime? TermEndDate
		{
			get { return _TermEndDate; }
			set { _TermEndDate = value; }
		}
		#endregion

		#region Status
		public abstract class status : IBqlField { }
		/// <summary>
		/// The status of the deferral schedule.
		/// </summary>
		/// <value>
		/// This field can have one of the values defined by 
		/// <see cref="DRScheduleStatus.ListAttribute"/>.
		/// </value>
		[PXString]
		[PXUIField(DisplayName = "Status")]
		[DRScheduleStatus.List]
		public string Status
		{
			get;
			set;
		}
		#endregion

		#region NoteID

		public abstract class noteID : IBqlField { }

		[PXNote(DescriptionField = typeof(DRSchedule.scheduleID))]
		public virtual Guid? NoteID { get; set; }

		#endregion

		#region System Columns
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
		[PXDBCreatedByID()]
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
		protected string _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual string CreatedByScreenID
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
		[PXDBCreatedDateTime()]
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
		[PXDBLastModifiedByID()]
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
		protected string _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual string LastModifiedByScreenID
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
		[PXDBLastModifiedDateTime()]
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

}
