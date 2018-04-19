using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Common;

namespace PX.Objects.PM
{
	[PXLocalizable(Messages.Prefix)]
	public static class Messages
	{
		#region Validation and Processing Messages
		public const string Prefix = "PM Error";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string AccountGroupAttributeNotValid = "One or more Attributes are not valid.";
		public const string Account_FK = "Account Group cannot be deleted. One or more Accounts are mapped to this Account Group.";
		public const string AccountDiactivate_FK = "Account Group cannot be deactivated. One or more Accounts are mapped to this Account Group.";
		public const string ProjectStatus_FK = "Account Group cannot be deleted. Project Status table contains one or more references to the given Account Group.";
		public const string OnlyPlannedCanbeDeleted = "Once Activated the record cannot be deleted. Only Tasks that are in 'Planning' or 'Cancelled' can be deleted.";
		public const string StartEndDateInvalid = "Planned Start Date for the given Task should be before the Planned End Date.";
		public const string UncompletedTasksExist = "Project can only be Completed if all Tasks are completed. {0} Task(s) are still incomplete.";
		public const string ProjectIsCanceled = "Project is Canceled and cannot be used for data entry.";
		public const string ProjectIsCompleted = "Project is Completed and cannot be used for data entry.";
		public const string ProjectIsSuspended = "Project is Suspended and cannot be used for data entry.";
		public const string ProjectTaskIsCompleted = "Task is Completed and cannot be used for data entry.";
		public const string ProjectTaskIsCompletedDetailed = "Task is Completed and cannot be used for data entry. Project:'{0}' Task:'{1}'";
		public const string ProjectTaskIsCanceled = "Task is Canceled and cannot be used for data entry.";
		public const string HasRollupData = "Cannot delete Balance record since it already has rollup data associated with it.";
		public const string HasTranData = "Cannot delete Task since it already has at least one Transaction associated with it.";
		public const string HasActivityData = "Cannot delete Task since it already has at least on Activity associated with it.";
		public const string HasTimeCardItemData = "Cannot delete Task since it already has at least one Time Card Item Record associated with it.";
		public const string ValidationFailed = "One or more rows failed to validate. Please correct and try again.";
		public const string NoAccountGroup = "Record is associated with Project whereas Account '{0}' is not associated with any Account Group";
		public const string InactiveProjectsCannotBeBilled = "Inactive Project cannot be billed.";
		public const string CancelledProjectsCannotBeBilled = "Cancelled Project cannot be billed.";
		public const string NoNextBillDateProjectCannotBeBilled = "Project can not be Billed if Next Billing Date is empty.";
		public const string NoCustomer = "This Project has no Customer associated with it and thus cannot be billed.";
		public const string BillingIDIsNotDefined = "Billing Rule must be defined for the task for Auto Budget to work.";
		public const string FailedToEmulateExpenses = "Failed to emulate Expenses when running Auto Budget. Probably there is no Expense Account Group in the Budget.";
		public const string FailedToCalcDescFormula = "Failed to calculate Description formula in the allocation rule:{0}, Step{1}. Formula:{2} Error:{3}";
		public const string FailedToCalcAmtFormula = "Failed to calculate Amount formula in the allocation rule:{0}, Step{1}. Formula:{2} Error:{3}";
		public const string FailedToCalcQtyFormula = "Failed to calculate Quantity formula in the allocation rule:{0}, Step{1}. Formula:{2} Error:{3}";
		public const string FailedToCalcBillQtyFormula = "Failed to calculate Billable Quantity formula in the allocation rule:{0}, Step{1}. Formula:{2} Error:{3}";
		public const string FailedToCalcDescFormula_Billing = "Failed to calculate the Description in the {0} billing rule using the {1} expression with the following error: {2}.";
		public const string FailedToCalcAmtFormula_Billing = "Failed to calculate the Amount in the {0} billing rule using the {1} expression with the following error: {2}.";
		public const string FailedToCalcQtyFormula_Billing = "Failed to calculate the Quantity in the {0} billing rule using the {1} expression with the following error: {2}.";
		public const string FailedToCalcInvDescFormula_Billing = "Failed to calculate the Invoice Description in the {0} billing rule using the {1} expression with the following error: {2}.";
		public const string PeriodsOverlap = "Overlapping time intervals are not allowed";
		public const string Activities = "Activities";
		public const string RangeOverlapItself = "Range for the summary step should not refer to itself.";
		public const string RangeOverlapFuture = "Range for the summary step should not refer future steps.";
		public const string ReversalExists = "Reversal for the given allocation already exist. Allocation can be reversed only once. RefNbr of the reversal document is {0}.";
		public const string TaskAlreadyExists = "Task with this ID already exists in the Project.";
		public const string AllocationStepFailed = "Failed to Process Step: {0} during Allocation for Task: {1}. Check Trace for details.";
		public const string DebitProjectNotFound = "Step '{0}': Debit Project was not found in the system.";
		public const string CreditProjectNotFound = "Step '{0}': Credit Project was not found in the system.";
		public const string DebitTaskNotFound = "Step '{0}': Failed to assign Debit Task. Task '{1}' was not found for the given Project '{2}'";
		public const string CreditTaskNotFound = "Step '{0}': Failed to assign Credi Task. Task '{1}' was not found for the given Project '{2}'";
		public const string AccountGroupInBillingRuleNotFound = "Billing Rule {0} has invalid Account Group. Account Group with the the given ID '{1}' was not found in the system.";
		public const string AccountGroupInAllocationStepFromNotFound = "Billing Rule {0} / Allocation Step {1} / From has invalid Account Group. Account Group with the the given ID '{2}' was not found in the system.";
		public const string AccountGroupInAllocationStepToNotFound = "Billing Rule {0} / Allocation Step {1} / To has invalid Account Group. Account Group with the the given ID '{2}' was not found in the system.";
		public const string ProjectInTaskNotFound = "Task '{0}' has invalid Project associated with it. Project with the ID '{1}' was not found in the system.";
		public const string TaskNotFound = "Task with the given id was not found in the system. ProjectID='{0}' TaskID='{1}'";
		public const string ProjectNotFound = "Project with the given id was not found in the system. ProjectID='{0}'";
		public const string AccountNotFound = "Account with the given id was not found in the system. AccountID='{0}'";
		public const string AutoAllocationFailed = "Auto-allocation of Project Transactions failed.";
		public const string AutoReleaseFailed = "Auto-release of allocated Project Transactions failed. Please try to release this document manually.";
		public const string AutoReleaseARFailed = "Auto-release of ARInvoice document created during billing failed. Please try to release this document manually.";
		public const string AutoReleaseOfReversalFailed = "During Billing ARInvoice was created successfully. PM Reversal document was created successfully. Auto-release of PM Reversal document failed. Please try to release this document manually.";
		public const string BillingRuleAccountIsNotConfiguredForBilling = "Billing Rule '{0}' is configured to get its Account from the Billing Rule but Account is not configured for the Billing Rule '{0}'.";
		public const string BillingRuleAccountIsNotConfiguredForBillingRecurent = "Recurent Billing '{0}' is configured to get its Account from the Recurent Billing Item but Account is not configured for the Billing Rule '{0}'.";
		public const string AccountGroupAccountIsNotConfiguredForBilling = "The '{0}' billing rule is configured to get its Sales Account from the Account Group but the Default Account is not configured for the '{1}' account group.";
		public const string ProjectAccountIsNotConfiguredForBilling = "The '{0}' billing rule is configured to get its Sales Account from the Project but the Default Account is not configured for the '{1}' project.";
		public const string ProjectAccountIsNotConfiguredForBillingRecurent = "The '{0}' recurrent billing is configured to get its Sales Account from the Project but the Default Account is not configured for the '{1}' project.";
		public const string TaskAccountIsNotConfiguredForBilling = "Billing Rule '{0}' is configured to get its Account from the Task but Default Account is not configured for Project '{1}' Task '{2}'.";
		public const string TaskAccountIsNotConfiguredForBillingRecurent = "Recurent Billing '{0}' is configured to get its Account from the Task but Default Account is not configured for Project '{1}' Task '{2}'.";
		public const string InventoryAccountIsNotConfiguredForBilling = "Billing Rule '{0}' is configured to get its Account from the Inventory Item but Sales Account is not configured for Inventory Item '{1}'";
		public const string InventoryAccountIsNotConfiguredForBillingRecurent = "Recurent Billing '{0}' is configured to get its Account from the Inventory Item but Sales Account is not configured for Inventory Item";
		public const string CustomerAccountIsNotConfiguredForBilling = "Billing Rule '{0}' is configured to get its Account from the Customer but Sales Account is not configured for Customer '{1}'";
		public const string CustomerAccountIsNotConfiguredForBillingRecurent = "Recurent Billing '{0}' is configured to get its Account from the Customer but Sales Account is not configured for Customer '{1}'";
		public const string EmployeeAccountIsNotConfiguredForBilling = "Billing Rule '{0}' is configured to get its Account from the Employee but Sales Account is not configured for Employee '{1}'";
		public const string DefaultAccountIsNotConfiguredForBilling = "The '{0}' billing rule is configured to get its Account from the Account Group but no default account is selected for the '{1}' account group.";
		public const string SubAccountCannotBeComposed = "Billing Rule '{0}' will not be able the compose the subaccount since account was not determined.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string SubAccountCannotBeComposedRecurent = "Recurent Billing '{0}' will not be able the compose the subaccount since account was not determined.";
		public const string EmployeeNotInProjectList = "Project is configured to restrict employees that are not in the Project's Employee list. Given Employee is not assigned to the Project.";
		public const string RateTypeNotDefinedForStep = "Rate Type is not defined for step {0}";
		public const string RateTypeNotDefinedForBilling = "The rate type is not defined for the '{1}' step of the '{0}' billing rule.";
		public const string RateNotDefinedForStep = "The @Rate is not defined for the {1} step of the {0} billing rule. Check Trace for details.";
		public const string RateNotDefinedForStepAllocation = "The @Rate is not defined for the {1} step of the {0} allocation rule. Check Trace for details.";
		public const string InactiveTask = "Project Task '{0}' is inactive.";
		public const string CompletedTask = "Project Task '{0}' is completed.";
		public const string TaskInvisibleInModule = "Project Task '{0}' is invisible in {1} module.";
		public const string InvisibleTask = "Project Task '{0}' is invisible.";
		public const string InactiveContract = "Given Project/Contract '{0}' is inactive";
		public const string CompleteContract = "Given Project/Contract '{0}' is completed";
		public const string TemplateContract = "Given Project/Contract '{0}' is a template";
		public const string ProjectInvisibleInModule = "The '{0}' project is invisible in the module.";
		public const string CancelledContract = "The '{0}' project/contract is cancelled.";
		public const string DebitAccountGroupIsRequired = "Allocation Rule Step {0} is not defined correctly. Debit Account Group is required.";
		public const string AtleastOneAccountGroupIsRequired = "Allocation Rule Step {0} is not defined correctly. At least either Debit or Credit Account Group is required.";
		public const string DebitAccountEqualCreditAccount = "Debit Account matches Credit Account.";
		public const string DebitAccountGroupEqualCreditAccountGroup = "Debit Account Group matches Credit Account Group.";
		public const string AccountGroupIsRequired = "Failed to Release PM Transaction '{0}': Account Group is required.";
		public const string InvalidAllocationRule = "Allocation Step '{0}' is not valid. When applied to transactions in Task '{1}' failed to set Account Group. Please correct your Allocation rules and try again.";
		public const string PostToGLFailed = "Failed to Automatically Post GLBatch created during release of PM document.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string UnitConversionNotDefinedForItem = "Failed to Convert from {0} to {1}. Unit conversion is not defined for {2}";
		public const string UnitConversionNotDefinedForItemOnBudgetUpdate = "Failed to Convert from {0} to {1} when updating the Budget for the Project. Unit conversion is not defined for {2}";
		public const string SourceSubNotSpecified = "Allocation rule is configured to use the source subaccount of transaction that is being allocated but the Subaccount is not set for the original transaction. Please correct your allocation step. Allocation Rule:{0} Step:{1}";
		public const string StepSubNotSpecified = "Allocation rule is configured to use the subaccount of allocation step but the subaccount is not set up. Please correct your allocation step. Allocation Rule:{0} Step:{1}";
		public const string OtherSourceIsEmpty = "Allocation rule is configured to take Debit Account from the source transaction and use it as a Credit Account of allocated transaction but the Debit Account is not set for the source transaction. Rule:{0} Step:{1} Transaction Description:{2}";
		public const string ProjectIsNullAfterAllocation = "In Step {0} Transaction that is processed has a null ProjectID. Please check the allocation rules in the preceding steps.";
		public const string TaskIsNullAfterAllocation = "In Step {0} Transaction that is processed has a null TaskID. Please check the allocation rules in the preceding steps.";
		public const string AccountGroupIsNullAfterAllocation = "In Step {0} Transaction that is processed has a null AllocationID. Please check the allocation rules in the preceding steps.";
		public const string StepSubMaskSpecified = "Subaccount Mask is not set in allocation step. Please correct your allocation step. Allocation Rule:{0} Step:{1}";
		public const string RateTableIsInvalid = "One or more validations failed for the given Rate Table sequence. Combinations of entities within sequence must be unique. The following combinations are not unique:";
		public const string ProjectIdIsNotSpecifiedForActivity = "ProjectID is not specified for the activity {0}:{1}";
		public const string ProjectRefError = "This record cannot be deleted. One or more projects are referencing this document.";
		public const string ValueMustBeGreaterThanZero = "The value must be greater than zero";
		public const string LocationNotFound = "Failed to create allocation transaction. The Location specified on the Task level is not valid. Please check and try again.";
		public const string GenericFieldErrorOnAllocation = "Failed to create allocation transaction.";
		public const string CommitmentsAutoCannotBeModified = "Commitment records with type 'Auto' cannot be deleted or modified.";
		public const string OtherUomUsedInTransaction = "Cannot set the UOM on the budget line. There already exists one or more transactions with a different UOM.";
		public const string UomNotDefinedForBudget = "The value of the Actual Qty. will not be updated if no UOM is defined.";
		public const string PrepaidAmountDecreased = "The Prepaid Amount can only be decreased from the auto assigned value.";
		public const string PrepaimentLessThanInvoiced = "The Prepaid Amount can not be decreased less than the already invoiced amount.";
		public const string ProjectExpired = "The project is expired.";
		public const string ProjectTaskExpired = "The project task is expired.";
		public const string TaskIsCompleted = "The project task is completed.";
		public const string PrepaymentAmointExceedsRevisedAmount = "The Prepaid Amount exceeds the uninvoiced balance.";
		public const string NoProgressiveRule = "The billing rule of the task contains only Time and Material steps. The Completed (%) and Pending Invoice Amount columns are not used for billing.";
		public const string BudgetNotDefinedForCostCode = "The budget is not defined for the cost code. Either define the budget or select a different cost code.";
		public const string UnreleasedProforma = "Pro Forma documents should be released in the sequence they were created. You cannot release the document until you release the following documents that precede the current one: {0}.";
		public const string EmailSendFailed = "Cannot send the email. Mailing settings are not configured on the Project Preferences (PM101000) form. Please check Trace for details.";
		public const string GroupedAllocationsBillLater = "The selected option is not available when a line represents a group of allocated transactions.";
		public const string DefaultAccountInProjectDoesnotMatchAccountGroup = "The '{0}' billing rule is configured to get its Sales Account from the Project but the selected '{1}' account does not match the '{2}' account group of the revenue budget.";
		public const string DefaultAccountInTaskDoesnotMatchAccountGroup = "The '{0}' billing rule is configured to get its Sales Account from the Task but the selected '{1}' account does not match the '{2}' account group of the revenue budget.";
		public const string SalesAccountInItemDoesnotMatchAccountGroup = "The '{0}' billing rule is configured to get its Sales Account from the Inventory Item but the selected '{1}' account does not match the '{2}' account group of the revenue budget.";
		public const string NonProjectCodeIsInvalid = "Non-Project is not a valid option.";
		public const string Overlimit = "The validation of the Max Limit Amount value has failed. Do one of the following: adjust the amounts of the document, adjust the limits of the budget, or select Ignore in the Validate T&M Revenue Budget Limits box on the Project Preferences (PM101000) form.";
		public const string OverlimitHint = "The validation of the Max Limit Amount has failed.";
		public const string UnreleasedPreviousInvoice = "You cannot release the pro forma invoice until you release the {1} {0} on the Invoices and Memos (AR301000) form.";
		public const string UnreleasedProformaOrInvoice = "All existing pro forma and Accounts Receivable invoices of the project have to be released before changing this setting.";
		public const string RevenueAccountIsNotMappedToAccountGroup = "Revenue Account {0} is not mapped to Account Group.";
		public const string DefaultAccountIsNotConfigured = "Default Account is required but not configured for Account group: {0}";
		public const string ReservedForProject = "Item reserved for Project Module to represent N/A item.";
		public const string NoBillingRule = "An invoice cannot be created because no billing rule is specified for the task.";
		public const string InclusiveTaxNotSupported = "Inclusive tax is not supported.";
		public const string FailedGetFromAddress = "The system has failed to obtain the From address from the pro forma invoice.";
		public const string FailedGetToAddress = "The system has failed to obtain the To address from the pro forma invoice.";
		public const string TaskReferencesRequiredAttributes = "The project tasks cannot be activated because required attributes of the tasks have no values. Please, use the Project Tasks (PM302000) form to fill in required attribute values.";
		public const string AtleastOneTaskWasNotActivated = "At least one task could not be activated. Please, review the list of errors.";
		#endregion

		#region Translatable Strings used in the code
		public const string ViewTask = "View Task";
		public const string ViewProject = "View Project";
		public const string ViewExternalCommitment = "View External Commitment";
		public const string NewTask = "New Task";
		public const string OffBalance = "Off-Balance";
		public const string SetupNotConfigured = "Project Management Setup is not configured.";
		public const string ViewBalance = "View Details";
		public const string ViewCommitments = "View Commitments";
		public const string ViewRates = "View Rates";
		public const string NonProjectDescription = "Non-Project Code.";
		public const string FullDetail = "Full Detail";
		public const string ProcAllocate = "Allocate";
		public const string ProcAllocateAll = "Allocate All";
		public const string ProcBill = "Bill";
		public const string ProcBillAll = "Bill All";
		public const string Release = "Release";
		public const string ReleaseAll = "Release All";
		public const string Approve = "Approve";
		public const string ApproveAll = "Approve All";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string EstimateBudget = "Estimate Budget";
		public const string ViewTransactions = "View Transactions";
		public const string PrjDescription = "Project Description";
		public const string Bill = "Run Project Billing";
		public const string BillTip = "Runs billing for the Next Billing Date";
		public const string Allocate = "Allocate";
		public const string AddTasks = "Add Tasks";
		public const string ActivateTasks = "Activate Tasks";
		public const string CompleteTasks = "Complete Tasks";
		public const string AddCommonTasks = "Add Common Tasks";
		public const string CreateTemplate = "Create Template";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string CreateTemplateTip = "Creates Template from the current Project.";
		public const string AutoBudget = "Auto-Budget Time and Material Revenue";
		public const string AutoBudgetTip = "Creates projected budget based on the expenses and Allocation Rules";
		public const string Actions = "Actions";
		public const string Filter = "Filter";
		public const string Reject = "Reject";
		public const string Assign = "Assign";
		public const string ApprovalDate = "Approval Date";
		public const string ReverseAllocation = "Reverse Allocation";
		public const string ReverseAllocationTip = "Reverses Released Allocation";
		public const string ViewAllocationSource = "View Allocation Source";
		public const string ViewBatch = "View Batch";
		public const string ProjectSearchTitle = "Project: {0} - {2}";
		public const string AccountGroup = "Account Group";
		public const string RateCode = "Rate Code";
		public const string Task = "Task";
		public const string Item = "Item";
		public const string FailedEmulateBilling = "The billing emulation cannot be run because of incorrect configuration. The sales account in the invoice is not mapped to any account group.";
		public const string InvalidScheduleType = "The schedule type is invalid.";
		public const string ArgumentIsNullOrEmpty = CR.Messages.ArgumentIsNullOrEmpty;
		public const string AllocationReversalBilling = "Allocation Reversal on Billing";
		public const string AllocationReversalNonBilling = "Allocation Reversal to Non-Billable";
		public const string ProjectAttributeNotSupport = "ProjectAttribute does not support the given module.";
		public const string ProjectTaskAttributeNotSupport = "ProjectTaskAttribute does not support the given module.";
		public const string FailedSelectProjectTask = "The system failed to select a project task with the given keys: ProjectID={0}, TaskID={1}.";
		public const string TaskIdEmptyError = "Task ID cannot be empty.";
		public const string CreateCommitment = "Create External Commitment";
		public const string ViewProforma = "View Pro Forma";
		public const string Prepayment = "Prepayment";
		public const string Total = "Total:";
		public const string PMTax = "PM Tax Detail";
		public const string PMTaxTran = "PM Tax";
		#endregion

		#region Graph Names
		public const string RegisterEntry = "Register Entry";
		public const string ProjectEntry = "Project Entry";
		public const string ProjectTaskEntry = "Project Task Entry";
		public const string ProjectBalanceEntry = "Project Balance Entry";
		public const string ProjectBalanceByPeriodEntry = "Project Balance By Period Entry";
		public const string CommitmentEntry = "Commitment Entry";
		public const string ProjectAttributeGroupMaint = "Project Attribute Maintenance";
		public const string AccountGroupMaint = "Account Group Maintenance";
		public const string EquipmentMaint = "Equipment Maintenance";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string RateCodeMaint = "Rate Code Maintenance";
		public const string RateDefinitionMaint = "Rate Definition Maintenance";
		public const string RateMaint = "Rate Maintenance";
		public const string BillingMaint = "Billing Maintenance";
		public const string RegisterRelease = "Register Release";
		public const string AllocationProcess = "Allocation Process By Task";
		public const string Process = "Process";
		public const string ProcessAll = "Process All";
		public const string AllocationProcessByProject = "Allocation Process By Project";
		public const string BillingProcess = "Billing Process";
		public const string ReverseUnbilledProcess = "Reverse Unbilled Process";
		public const string TransactionInquiry = "Transactions Inquiry";
		public const string PMSetup = "Project Management Setup";
		public const string PMSetupMaint = "Project Preferences";
		public const string TaskInquiry = "Tasks Inquiry";
		public const string TemplateMaint = "Project Template Maintenance";
		public const string TemplateTaskMaint = "Project Task Template Maintenance";
		public const string TemplateGlobalTaskListMaint = "Common Task List Template Maintenance";
		public const string TemplateGlobalTaskMaint = "Project Task Template Maintenance";
		public const string PMAllocator = "Project Allocator";
		public const string PMAddress = "PM Address";
		public const string PMContact = "PM Contact";
		public const string PMProFormaAddress = "PM ProForma Address";
		public const string PMProFormaContact = "PM ProForma Contact";
		#endregion

		#region View Names
		public const string Selection = "Selection";
		public const string ProjectAnswers = "Project Answers";
		public const string TaskAnswers = "Task Answers";
		public const string AccountGroupAnswers = "Account Group Answers";
		public const string EquipmentAnswers = "Equipment Answers";
		public const string PMTasks = "Tasks";
		public const string Approval = "Approval";
		public const string Commitments = "Commitments";
		public const string Budget = "Budget";
		#endregion

		#region DAC Names
		public const string Project = "Project";
		//public const string PMDocument = "PM Document";
		public const string ProjectTask = "Project Task";
		public const string PMTaskRate = "PM Task Rate";
		public const string PMProjectRate = "PM Project Rate";
		public const string PMItemRate = "PM Item Rate";
		public const string PMEmployeeRate = "PM Item Employee";
		public const string PMRate = "PM Rate";
		public const string PMRateType = "PM Rate Type";
		public const string PMRateTable = "PM Rate Table";
	    public const string PMProjectTemplate = "Project Template";
		public const string PMRateDefinition = "PM Rate Lookup Rule";
		public const string PMRateSequence = "PM Rate Lookup Rule Sequence";
		//public const string PMBillingRule = "PM Billing Rule";
		//public const string PMBilling = "PM Billing";
		public const string PMAccountTask = "PM Account Task";
		public const string PMAccountGroupRate = "PM Account Group Rate";
		//public const string PMAllocation = "PM Allocation";
		public const string PMAllocationStep = "PM Allocation Rule";
		public const string PMAllocationSourceTran = "PM Allocation Source Transaction";
		public const string PMAllocationAuditTran = "PM Allocation Audit Transaction";
        public const string SelectedTask = "Tasks for Addition";
        public const string PMAccountGroupName = "Project Account Group Name";
        public const string PMTran = "Project Transaction";
        public const string PMProjectStatus = "Project Status";
        public const string PMHistory = "Project History";
		public const string Employee = "Employee";
		public const string Inventory = "Inventory";
		public const string CostCode = "Cost Code";
		public const string Proforma = "Pro Forma Invoice";
		public const string ProformaLine = "Pro Forma Line";
		public const string PMRegister = "Project Register";
		public const string BillingRule = "Billing Rule";
		public const string BillingRuleStep = "Billing Rule Step";
		public const string AllocationRule = "Allocation Rule";
		public const string Commitment = "Commitment Record";
		#endregion

		#region Combo Values
		public const string NotStarted = "NotStarted";
		public const string Active = "Active";
		public const string Canceled = "Canceled";
		public const string Completed = "Completed";
		public const string InPlanning = "In Planning";
		public const string OnHold = "On Hold";
		public const string Suspend = "Suspended";
		public const string PendingApproval = "Pending Approval";
		public const string Open = "Open";
		public const string Closed = "Closed";
		public const string Rejected = "Rejected";

		public const string Hold = "Hold";
		public const string Balanced = "Balanced";
		public const string Released = "Released";


		public const string GroupTypes_Project = "Project";
		public const string GroupTypes_Task = "Task";
		public const string GroupTypes_AccountGroup = "Account Group";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string GroupTypes_Transaction = "Transaction";
		public const string GroupTypes_Equipment = "Equipment";


		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string Action_Hold = "Hold";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string Action_Post = "Post";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string Action_SumAndPost = "Sum & Post";

		public const string None = "None";

		public const string Origin_Source = "Use Source";
		public const string Origin_Change = "Replace";
		public const string Origin_FromAccount = "From Account";
		public const string Origin_None = "None";
		public const string Origin_DebitSource = "Debit Source";
		public const string Origin_CreditSource = "Credit Source";

		public const string PMMethod_Transaction = "Allocate Transactions";
		public const string PMMethod_Budget = "Allocate Budget";

		public const string PMRestrict_AllProjects = "All Projects";
		public const string PMRestrict_CustomerProjects = "Customer Projects";

		public const string PMProForma_Select = "<SELECT>";
		public const string PMProForma_Release = "Release";

		public const string PMBillingType_Transaction = "Time and Material";
		public const string PMBillingType_Budget = "Progress Billing";

		public const string PMSelectOption_Transaction = "Not Allocated Transactions";
		public const string PMSelectOption_Step = "From Previous Allocation Steps";

		public const string MaskSource = "Source";
		public const string AllocationStep = "Allocation Step";
		public const string ProjectDefault = "Project Default";
		public const string TaskDefault = "Task Default";

		public const string OnBilling = "By Billing Period";
		public const string OnTaskCompletion = "On Task Completion";
		public const string OnProjectCompetion = "On Project Completion";

		public const string AccountSource_None = "None";
		public const string AccountSource_SourceTransaction = "Source Transaction"; 
		public const string AccountSource_BillingRule = "Billing Rule";
		public const string AccountSource_Project = "Project";
		public const string AccountSource_ProjectAccrual = "Project Accrual";
		public const string AccountSource_Task = "Task";
		public const string AccountSource_Task_Accrual = "Task Accrual";
		public const string AccountSource_InventoryItem = "Inventory Item";
		public const string AccountSource_LaborItem = "Labor Item";
		public const string AccountSource_LaborItem_Accrual = "Labor Item Accrual";
		public const string AccountSource_Customer = "Customer";
		public const string AccountSource_Resource = "Resource";
		public const string AccountSource_Employee = "Employee";
		public const string AccountSource_Branch = "Branch";
		public const string AccountSource_CurrentBranch = "Current Branch";
		public const string AccountSource_RecurentBillingItem = "Recurring Item";
		public const string AccountSource_AccountGroup = "Account Group";

		public const string Allocation = "Allocation";
		public const string Timecard = "Time Card";
		public const string Case = "Case";
		public const string ExpenseClaim = "Expense Claim";
		public const string EquipmentTimecard = "Equipment Time Card";
		public const string AllocationReversal = "Allocation Reversal";
		public const string Reversal = "Reversal";
		public const string CreditMemo = "Credit Memo";
		public const string UnbilledRemainder = "Unbilled Remainder";
		public const string ProformaBilling = "Proforma Billing";


		public const string PMReverse_OnInvoice = "On Invoice Release";
		public const string PMReverse_OnBilling = "On Project Billing";
		public const string PMReverse_Never = "Never";

		public const string PMNoRateOption_SetOne = "Set @Rate to 1";
		public const string PMNoRateOption_SetZero = "Set @Rate to 0";
		public const string PMNoRateOption_RaiseError = "Raise Error";
		public const string PMNoRateOption_NoAllocate = "Do not allocate";
		public const string PMNoRateOption_NoBill = "Do not bill";

		public const string PMDateSource_Transaction = "Original Transaction";
		public const string PMDateSource_Allocation = "Allocation Date";

		public const string Included = "Include Trans. created on billing date";
		public const string Excluded = "Exclude Trans. created on billing date";

		public const string Manual = "Manual";
		public const string ByQuantity = "Budgeted Quantity";
		public const string ByAmount = "Budgeted Amount";

		public const string CommitmentType_Internal = "Internal";
		public const string CommitmentType_External = "External";

		public const string TaskType_Combined = "Combined";
		public const string TaskType_Expense = "Cost";
		public const string TaskType_Revenue = "Revenue";

		public const string Progressive = "Progressive";
		public const string Transaction = "Transactions";

		public const string Option_BillNow = "Bill";
		public const string Option_WriteOffRemainder = "Write Off Remainder";
		public const string Option_HoldRemainder = "Hold Remainder";
		public const string Option_Writeoff = "Write Off";

		public const string BudgetLevel_Item = "Task and Item";
		public const string BudgetLevel_CostCode = "Task and Cost Code";


		public const string Validation_Error = "Validate";
		public const string Validation_Warning = "Ignore";

		public const string BudgetUpdate_Detailed = "Detailed";
		public const string BudgetUpdate_Summary = "Summary";
		#endregion


		#region Field Display Names

		public const string CreditAccountGroup = "Credit Account Group";
		public const string FinPTDAmount = "Financial PTD Amount";
		public const string FinPTDQuantity = "Financial PTD Quantity";
		public const string BudgetPTDAmount = "Budget PTD Amount";
		public const string BudgetPTDQuantity = "Budget PTD Quantity";
		public const string RevisedPTDAmount = "Revised PTD Amount";
		public const string RevisedPTDQuantity = "Revised PTD Quantity";
		#endregion

		public static string GetLocal(string message)
		{
			return PXLocalizer.Localize(message, typeof(Message).FullName);
		}
	}

	[PXLocalizable(Warnings.Prefix)]
	public static class Warnings
	{
		public const string Prefix = "PM Warning";

		public const string AccountIsUsed = "This account is already added to the '{0}' account group. By clicking Save, you will move the account to the currently selected account group.";
		public const string StartDateOverlow = "Start Date for the given Task falls outside the Project Start and End date range.";
		public const string EndDateOverlow = "End Date for the given Task falls outside the Project Start and End date range.";
		public const string ProjectIsCompleted = "Project is Completed. It will not be available for data entry.";
		public const string ProjectIsNotActive = "Project is Not Active. Please Activate Project.";
		public const string HasAllocatedTrans = "Allocated Transaction(s) associated with this Task was created using different Billing Rule ('{0}')";
		public const string NothingToAllocate = "Transactions were not created during the allocation.";
		public const string NothingToBill = "Invoice was not created during the billing. Nothing to bill.";
		public const string ProjectCustomerDontMatchTheDocument = "Customer on the Document doesn't match the Customer on the Project or Contract.";
	}
}
