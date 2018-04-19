using PX.Data;
using PX.Data.EP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Objects.GL;
using PX.Objects.CM;
using PX.Objects.TX;
using PX.TM;

namespace PX.Objects.PM
{
	[PXCacheName(Messages.Proforma)]
	[PXPrimaryGraph(typeof(ProformaEntry))]
	[Serializable]
	[PXEMailSource]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMProforma : PX.Data.IBqlTable, IAssign
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		protected bool? _Selected = false;

		/// <summary>
		/// Indicates whether the record is selected for processing.
		/// </summary>
		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected
		{
			get
			{
				return _Selected;
			}
			set
			{
				_Selected = value;
			}
		}
		#endregion

		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
			public const int Length = 15;
		}
		protected String _RefNbr;
		[PXDBString(refNbr.Length, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault()]
		[PXSelector(typeof(Search<PMProforma.refNbr>), Filterable = true)]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
		[AutoNumber(typeof(Search<PMSetup.proformaNumbering>), typeof(AccessInfo.businessDate))]
		public virtual String RefNbr
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
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected String _Description;
		[PXDBString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Description")]
		[PXFieldDescription]
		public virtual String Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}
		#endregion
		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected String _Status;
		[PXDBString(1, IsFixed = true)]
		[ProformaStatus.List()]
		[PXDefault(ProformaStatus.OnHold)]
		[PXUIField(DisplayName = "Status", Required = true, Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}
		#endregion
		#region Hold
		public abstract class hold : PX.Data.IBqlField
		{
		}
		protected Boolean? _Hold;
		[PXDBBool()]
		[PXUIField(DisplayName = "Hold")]
		[PXDefault(true)]
		public virtual Boolean? Hold
		{
			get
			{
				return this._Hold;
			}
			set
			{
				this._Hold = value;
			}
		}
		#endregion
		#region Approved
		public abstract class approved : PX.Data.IBqlField
		{
		}
		protected Boolean? _Approved;
		[PXDBBool()]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Approved", Enabled = false)]
		public virtual Boolean? Approved
		{
			get
			{
				return this._Approved;
			}
			set
			{
				this._Approved = value;
			}
		}
		#endregion
		#region Rejected
		public abstract class rejected : IBqlField
		{
		}
		protected bool? _Rejected = false;
		[PXDBBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Rejected", Enabled = false)]
		public bool? Rejected
		{
			get
			{
				return _Rejected;
			}
			set
			{
				_Rejected = value;
			}
		}
		#endregion
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(typeof(Coalesce<
	Search<Location.cBranchID, Where<Location.bAccountID, Equal<Current<PMProforma.customerID>>, And<Location.locationID, Equal<Current<PMProforma.locationID>>>>>,
	Search<GL.Branch.branchID, Where<GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>), IsDetail = false)]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
		[PXDefault]
		[Project(Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
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
		#region CustomerID
		public abstract class customerID : PX.Data.IBqlField
		{
		}
		protected Int32? _CustomerID;
		[PXDefault]
		[Customer(DescriptionField = typeof(Customer.acctName), Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual Int32? CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				this._CustomerID = value;
			}
		}
		#endregion
		#region CustomerID_Customer_acctName
		public abstract class customerID_Customer_acctName : IBqlField { }
		#endregion
		#region BillAddressID
		public abstract class billAddressID : PX.Data.IBqlField
		{
		}
		protected Int32? _BillAddressID;

		/// <summary>
		/// The identifier of the <see cref="PMAddress">Billing Address object</see>, associated with the customer.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="PMAddress.AddressID"/> field.
		/// </value>
		[PXDBInt()]
		[PMAddress(typeof(Select2<Customer,
			InnerJoin<CR.Standalone.Location, On<CR.Standalone.Location.bAccountID, Equal<Customer.bAccountID>, And<CR.Standalone.Location.locationID, Equal<Customer.defLocationID>>>,
			InnerJoin<Address, On<Address.bAccountID, Equal<Customer.bAccountID>, And<Address.addressID, Equal<Customer.defBillAddressID>>>,
			LeftJoin<PMAddress, On<PMAddress.customerID, Equal<Address.bAccountID>, And<PMAddress.customerAddressID, Equal<Address.addressID>, And<PMAddress.revisionID, Equal<Address.revisionID>, And<PMAddress.isDefaultBillAddress, Equal<True>>>>>>>>,
			Where<Customer.bAccountID, Equal<Current<PMProforma.customerID>>>>), typeof(customerID))]
		public virtual Int32? BillAddressID
		{
			get
			{
				return this._BillAddressID;
			}
			set
			{
				this._BillAddressID = value;
			}
		}
		#endregion
		#region BillContactID
		public abstract class billContactID : PX.Data.IBqlField
		{
		}

		/// <summary>
		/// The identifier of the <see cref="ARContact">Billing Contact object</see>, associated with the customer.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="ARContact.ContactID"/> field.
		/// </value>
		[PXDBInt]
		[PXSelector(typeof(PMContact.contactID), ValidateValue = false)]    //Attribute for showing contact email field on Automatic Notifications screen in the list of availible emails for
																			//Invoices and Memos screen. Relies on the work of platform, which uses PXSelector to compose email list
		[PXUIField(DisplayName = "Billing Contact", Visible = false)]       //Attribute for displaying user friendly contact email field on Automatic Notifications screen in the list of availible emails.
		[PMContact(typeof(Select2<Customer,
							InnerJoin<
									  CR.Standalone.Location, On<CR.Standalone.Location.bAccountID, Equal<Customer.bAccountID>,
								  And<CR.Standalone.Location.locationID, Equal<Customer.defLocationID>>>,
							InnerJoin<
									  Contact, On<Contact.bAccountID, Equal<Customer.bAccountID>,
								  And<Contact.contactID, Equal<Customer.defBillContactID>>>,
							LeftJoin<
									 PMContact, On<PMContact.customerID, Equal<Contact.bAccountID>,
								 And<PMContact.customerContactID, Equal<Contact.contactID>,
								 And<PMContact.revisionID, Equal<Contact.revisionID>,
								 And<PMContact.isDefaultContact, Equal<True>>>>>>>>,
							Where<Customer.bAccountID, Equal<Current<PMProforma.customerID>>>>), typeof(customerID))]
		public virtual int? BillContactID
		{
			get;
			set;
		}
		#endregion
		#region LocationID
		public abstract class locationID : PX.Data.IBqlField
		{
		}
		protected Int32? _LocationID;

		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<PMProforma.customerID>>>), Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Location", DescriptionField = typeof(Location.descr))]
		[PXDefault]
		public virtual Int32? LocationID
		{
			get
			{
				return this._LocationID;
			}
			set
			{
				this._LocationID = value;
			}
		}
		#endregion
		#region TaxZoneID
		public abstract class taxZoneID : PX.Data.IBqlField
		{
		}
		protected String _TaxZoneID;

		/// <summary>
		/// The identifier of the <see cref="TaxZone"/> associated with the document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="TaxZone.TaxZoneID"/> field.
		/// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Customer Tax Zone")]
		[PXRestrictor(typeof(Where<TaxZone.isManualVATZone, Equal<False>>), TX.Messages.CantUseManualVAT)]
		[PXSelector(typeof(TaxZone.taxZoneID), DescriptionField = typeof(TaxZone.descr), Filterable = true)]
		public virtual String TaxZoneID
		{
			get
			{
				return this._TaxZoneID;
			}
			set
			{
				this._TaxZoneID = value;
			}
		}
		#endregion
		#region AvalaraCustomerUsageType
		public abstract class avalaraCustomerUsageType : PX.Data.IBqlField
		{
		}
		protected String _AvalaraCustomerUsageType;

		/// <summary>
		/// The customer entity type for reporting purposes. The field is used if the system is integrated with AvaTax by Avalara
		/// and the <see cref="FeaturesSet.AvalaraTax">Avalara Tax Integration</see> feature is enabled.
		/// </summary>
		/// <value>
		/// The field can have one of the values described in <see cref = "TXAvalaraCustomerUsageType.ListAttribute" />.
		/// Defaults to the <see cref="Location.CAvalaraCustomerUsageType">customer entity type</see>
		/// that is specified for the <see cref="CustomerLocationID">location of the customer</see>.
		/// </value>
		[PXDefault(typeof(Search<Location.cAvalaraCustomerUsageType, Where<Location.bAccountID, Equal<Current<customerID>>, And<Location.locationID, Equal<Current<locationID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Customer Usage Type")]
		[TX.TXAvalaraCustomerUsageType.List]
		public virtual String AvalaraCustomerUsageType
		{
			get
			{
				return this._AvalaraCustomerUsageType;
			}
			set
			{
				this._AvalaraCustomerUsageType = value;
			}
		}
		#endregion
		#region CuryID
		public abstract class curyID : PX.Data.IBqlField
		{
		}
		protected String _CuryID;
		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault(typeof(Search<Company.baseCuryID>))]
		[PXSelector(typeof(Currency.curyID))]
		public virtual String CuryID
		{
			get
			{
				return this._CuryID;
			}
			set
			{
				this._CuryID = value;
			}
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField
		{
		}
		
		/// <summary>
		/// The identifier of the <see cref="CurrencyInfo">CurrencyInfo</see> object associated with the document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="CurrencyInfoID"/> field.
		/// </value>
		[PXDBLong()]
		[CurrencyInfo(ModuleCode = "PM")]
		public virtual Int64? CuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region InvoiceDate
		public abstract class invoiceDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _InvoiceDate;

		/// <summary>
		/// The original date assigned by the customer to the customer document.
		/// </summary>
		[PXDBDate()]
		[PXDefault(TypeCode.DateTime, "01/01/1900")]
		[PXUIField(DisplayName = "Invoice Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? InvoiceDate
		{
			get
			{
				return this._InvoiceDate;
			}
			set
			{
				this._InvoiceDate = value;
			}
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		protected String _FinPeriodID;
		[OpenPeriod(typeof(PMProforma.invoiceDate))]
		[PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
		public virtual String FinPeriodID
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
		#region TermsID
		public abstract class termsID : PX.Data.IBqlField
		{
		}
		protected String _TermsID;

		/// <summary>
		/// The identifier of the <see cref="Terms">Credit Terms</see> object associated with the document.
		/// </summary>
		/// <value>
		/// Defaults to the <see cref="Customer.TermsID">credit terms</see> that are selected for the <see cref="CustomerID">customer</see>.
		/// Corresponds to the <see cref="Terms.TermsID"/> field.
		/// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXDefault(typeof(Search<PMProject.termsID, Where<PMProject.contractID, Equal<Current<PMProforma.projectID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Terms", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(Search<Terms.termsID, Where<Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof(Terms.descr), Filterable = true)]
		[Terms(typeof(invoiceDate), typeof(dueDate), typeof(discDate), null, null)]
		public virtual String TermsID
		{
			get
			{
				return this._TermsID;
			}
			set
			{
				this._TermsID = value;
			}
		}
		#endregion
		#region DueDate
		public abstract class dueDate : PX.Data.IBqlField
		{
		}

		/// <summary>
		/// The due date of the document.
		/// </summary>
		[PXDBDate()]
		[PXUIField(DisplayName = "Due Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? DueDate
		{
			get;set;
		}
		#endregion
		#region DiscDate
		public abstract class discDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _DiscDate;

		/// <summary>
		/// The date when the cash discount can be taken in accordance with the <see cref="ARInvoice.TermsID">credit terms</see>.
		/// </summary>
		[PXDBDate()]
		[PXUIField(DisplayName = "Cash Discount Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? DiscDate
		{
			get
			{
				return this._DiscDate;
			}
			set
			{
				this._DiscDate = value;
			}
		}
		#endregion
		#region WorkgroupID
		public abstract class workgroupID : PX.Data.IBqlField
		{
		}
		protected int? _WorkgroupID;

		/// <summary>
		/// The workgroup that is responsible for the document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
		/// </value>
		[PXDBInt]
		[PXDefault(typeof(Customer.workgroupID), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXCompanyTreeSelector]
		[PXUIField(DisplayName = "Workgroup", Visibility = PXUIVisibility.Visible)]
		public virtual int? WorkgroupID
		{
			get
			{
				return this._WorkgroupID;
			}
			set
			{
				this._WorkgroupID = value;
			}
		}
		#endregion
		#region OwnerID
		public abstract class ownerID : IBqlField { }
		protected Guid? _OwnerID;

		/// <summary>
		/// The <see cref="EPEmployee">Employee</see> responsible for the document.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="EPEmployee.PKID"/> field.
		/// </value>
		[PXDBGuid()]
		[PXDefault(typeof(Customer.ownerID), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXOwnerSelector(typeof(PMProforma.workgroupID))]
		[PXUIField(DisplayName = "Owner", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual Guid? OwnerID
		{
			get
			{
				return this._OwnerID;
			}
			set
			{
				this._OwnerID = value;
			}
		}
		#endregion
		#region LineCntr
		public abstract class lineCntr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineCntr;
		[PXDBInt()]
		[PXDefault(0)]
		public virtual Int32? LineCntr
		{
			get
			{
				return this._LineCntr;
			}
			set
			{
				this._LineCntr = value;
			}
		}
		#endregion
		#region Released
		public abstract class released : PX.Data.IBqlField
		{
		}
		protected Boolean? _Released;
		[PXDBBool()]
		[PXUIField(DisplayName = "Released")]
		[PXDefault(false)]
		public virtual Boolean? Released
		{
			get
			{
				return this._Released;
			}
			set
			{
				this._Released = value;
			}
		}
		#endregion
		#region EnableProgressive
		public abstract class enableProgressive : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Enable Progressive Tab")]
		public virtual Boolean? EnableProgressive
		{
			get;
			set;
		}
		#endregion
		#region EnableTransactional
		public abstract class enableTransactional : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Enable Transactions Tab")]
		public virtual Boolean? EnableTransactional
		{
			get;
			set;
		}
		#endregion

		#region CuryTransactionalTotal
		public abstract class curyTransactionalTotal : PX.Data.IBqlField
		{
		}
		[PXDBCurrency(typeof(curyInfoID), typeof(transactionalTotal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Time and Material Total")]
		public virtual Decimal? CuryTransactionalTotal
		{
			get; set;
		}
		#endregion
		#region TransactionalTotal
		public abstract class transactionalTotal : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Time and Material Total in Base Currency")]
		public virtual Decimal? TransactionalTotal
		{
			get; set;
		}
		#endregion
		#region CuryProgressiveTotal
		public abstract class curyProgressiveTotal : PX.Data.IBqlField
		{
		}
		[PXDBCurrency(typeof(curyInfoID), typeof(progressiveTotal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Progress Billing Total")]
		public virtual Decimal? CuryProgressiveTotal
		{
			get; set;
		}
		#endregion
		#region ProgressiveTotal
		public abstract class progressiveTotal : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Progress Billing Total in Base Currency")]
		public virtual Decimal? ProgressiveTotal
		{
			get; set;
		}
		#endregion
		#region CuryDetailTotal
		public abstract class curyDetailTotal : PX.Data.IBqlField
		{
		}
		[PXFormula(typeof(Add<curyTransactionalTotal, curyProgressiveTotal>))]
		[PXDBCurrency(typeof(curyInfoID), typeof(detailTotal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Detail Total")]
		public virtual Decimal? CuryDetailTotal
		{
			get; set;
		}
		#endregion
		#region DetailTotal
		public abstract class detailTotal : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Detail Total in Base Currency")]
		public virtual Decimal? DetailTotal
		{
			get; set;
		}
		#endregion
		#region CuryTaxTotal
		public abstract class curyTaxTotal : PX.Data.IBqlField
		{
		}
		[PXDBCurrency(typeof(curyInfoID), typeof(taxTotal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Total")]
		public virtual Decimal? CuryTaxTotal
		{
			get; set;
		}
		#endregion
		#region TaxTotal
		public abstract class taxTotal : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Total in Base Currency")]
		public virtual Decimal? TaxTotal
		{
			get; set;
		}
		#endregion
		#region CuryDocTotal
		public abstract class curyDocTotal : PX.Data.IBqlField
		{
		}
		[PXFormula(typeof(Add<curyTaxTotal, curyDetailTotal>))]
		[PXDBCurrency(typeof(curyInfoID), typeof(docTotal))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Invoice Total")]
		public virtual Decimal? CuryDocTotal
		{
			get; set;
		}
		#endregion
		#region Total
		public abstract class docTotal : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Invoice Total in Base Currency")]
		public virtual Decimal? DocTotal
		{
			get; set;
		}
		#endregion
		#region IsTaxValid
		public abstract class isTaxValid : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Tax is up to date", Enabled = false)]
		public virtual Boolean? IsTaxValid
		{
			get; set;
		}
		#endregion

		#region ARInvoiceDocType
		public abstract class aRInvoiceDocType : PX.Data.IBqlField
		{
		}
		[ARInvoiceType.List()]
		[PXUIField(DisplayName = "AR Doc. Type", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[PXDBString(3)]
		public virtual String ARInvoiceDocType
		{
			get; set;
		}
		#endregion
		#region ARInvoiceRefNbr
		public abstract class aRInvoiceRefNbr : PX.Data.IBqlField
		{
		}

		[PXUIField(DisplayName = "AR Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[PXSelector(typeof(Search<ARInvoice.refNbr, Where<ARInvoice.docType, Equal<Current<aRInvoiceDocType>>>>))]
		[PXDBString(15, IsUnicode = true)]
		public virtual String ARInvoiceRefNbr
		{
			get; set;
		}
		#endregion

		#region System Columns
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Guid? _NoteID;
		[PXNote(DescriptionField = typeof(PMProforma.refNbr))]
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

	public static class ProformaStatus
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { OnHold, PendingApproval, Open, Closed, Rejected },
				new string[] { Messages.OnHold, Messages.PendingApproval, Messages.Open, Messages.Closed, Messages.Rejected })
			{; }
		}
		public const string OnHold = "H";
		public const string PendingApproval = "A";
		public const string Open = "O";
		public const string Closed = "C";
		public const string Rejected = "R";

		public class open : Constant<string>
		{
			public open() : base(Open) {; }
		}
	}
}
