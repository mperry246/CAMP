using System;

using PX.Common;

namespace PX.Objects.AP
{
	[PXLocalizable(Messages.Prefix)]
	public static class Messages
	{
		// Add your messages here as follows (see line below):
		// public const string YourMessage = "Your message here.";
		#region Validation and Processing Messages
		public const string Prefix = "AP Error";
		public const string InternalError = IN.Messages.InternalError;
		public const string SheduleNextExecutionDateExceeded = GL.Messages.SheduleNextExecutionDateExceeded;
		public const string SheduleExecutionLimitExceeded = GL.Messages.SheduleExecutionLimitExceeded;
		public const string SheduleHasExpired = GL.Messages.SheduleHasExpired;
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string BatchesNotReleased = "One or more batches failed to release.";
		public const string Entry_LE = "Entry must be less or equal to {0}";
		public const string Entry_GE = "Entry must be greater or equal to {0}";
		public const string Document_Status_Invalid = "Document Status is invalid for processing.";
		public const string Document_OnHold_CannotRelease = "Document is On Hold and cannot be released.";
		public const string Check_NotPrinted_CannotRelease = "Check is not Printed and cannot be released.";
		public const string ZeroCheck_CannotPrint = "Zero Check cannot be Printed or Exported.";
		public const string UnknownDocumentType = "The document cannot be processed because the document type is unknown.";
		public const string Only_Invoices_MayBe_Payed = "Only Bills and Credit Adjustments can be selected for payment.";
		public const string Only_Open_Documents_MayBe_Processed = "Only Open documents can be selected for payment.";
		public const string VoidAppl_CheckNbr_NotMatchOrigPayment = "Void Check must have the same Reference Number as the voided payment.";
		public const string ApplDate_Less_DocDate = "{0} cannot be less than Document Date.";
		public const string ApplPeriod_Less_DocPeriod = "{0} cannot be less than Document Financial Period.";
		public const string ApplDate_Greater_DocDate = "{0} cannot be greater than Document Date.";
		public const string ApplPeriod_Greater_DocPeriod = "{0} cannot be greater than Document Financial Period.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string AP1099_Vendor_Cannot_Have_Discounts = "Terms discounts are not allowed for 1099 Vendors.";
		public const string AP1099_Vendor_Cannot_Have_Multiply_Installments = "Multiple Installments are not allowed for 1099 Vendors.";
		public const string AP1099_PaymentDate_NotIn_OpenYear = "Payment date {0} must fall into open 1099 Year.";
		public const string Employee_Cannot_Have_Discounts = "Terms discounts are not allowed for Employees.";
		public const string Employee_Cannot_Have_Multiply_Installments = "Multiple Installments are not allowed for Employees.";
		public const string DocumentBalanceNegative = "Document Balance will go negative. Document will not be released.";
		public const string PrepaymentNotPayedFull = "Prepayment '{0}' is not paid in full. Document will not be released.";
		public const string DocumentOutOfBalance = "The document is out of the balance.";

		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string QuickCheckOutOfBalance = "The payment amount should be less than or equal to the invoice amount.";
		public const string PrintedQuickCheckOutOfBalance = "The printed quick check is out of the balance.";
		public const string DocumentApplicationAlreadyVoided = "This document application is already voided. Document will not be released.";
		public const string PrepaymentCannotBeVoidedDueToUnreleasedCheck = "The prepayment request cannot be voided. It has been selected for payment with check {0}. To void the prepayment request, first remove the check application.";
		public const string PrepaymentCannotBeVoidedDueToReleasedCheck = "The prepayment request cannot be voided. It has been paid with check {0}.";
		public const string PrepaymentCheckCannotBeVoided = "Check {0} cannot be voided. The Prepayment {1} that is paid with this check has applications. To void the check, first reverse released applications and delete unreleased applications of the prepayment.";
		public const string AskUpdateLastRefNbr = "Do you want the system to update the AP Last Reference Number on the Cash Accounts (CA202000) form with entered number '{0}'?";

		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string CheckCuryNotPPCury = "Payment currency cannot be different from the selected Prepayment currency.";
		public const string CashCuryNotPPCury = "Cash Account currency cannot be different from Prepayment currency.";
		public const string PaymentTypeNoPrintCheck = "Payment Method '{0}' is not configured to print checks.";
		public const string PaymentTypeCantBeUsedInAP = "Payment Method '{0}' is not configured to use in Accounts Payable.";
		public const string PaymentTypeIsInactive = "Payment Method '{0}' is inactive.";
		public const string VendorCuryNotPPCury = "Vendor Cash Account currency is different from the Prepayment currency. Prepayment document will not be selected for Payment.";
		public const string VendorMissingCashAccount = "Cash Account is not set up for Vendor.";
		public const string VendorCuryDifferentDefPayCury = "Vendor currency is different from the default Cash Account Currency.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string DuplicateVendorRefDoc = "Document with this Vendor Ref. already exists for this Vendor - see document with type '{2}' and reference number '{0}' dated '{1:d}' .";
		public const string VendorClassChangeWarning = "Please confirm if you want to update current Vendor settings with the Vendor Class defaults. Original settings will be preserved otherwise.";
		public const string MultipleApplicationError = "Multiple applications exists for this document. Please reverse these applications individually and then void the document.";
		public const string PPVSubAccountMaskCanNotBeAssembled = "PPV Subaccount mask cannot be assembled correctly. Please, check settings for the Inventory Posting Class";
		public const string TaxVendorDeleteErr = "Cannot delete Tax Vendor. There are Taxes associated with this Vendor.";
		public const string Quick_Check_Cannot_Have_Multiply_Installments = "Multiple Installments are not allowed for Quick Checks.";
		public const string Multiply_Installments_Cannot_be_Reversed = "Multiple installments bill cannot be reversed, Please reverse original bill '{0}'.";
		public const string Check_Cannot_Unhold_Until_Printed = "Cannot remove from hold until check is printed.";
		public const string Application_Amount_Cannot_Exceed_Document_Amount = "Total application amount cannot exceed document amount.";
		public const string ProcessingOfLandedCostTransForAPDocFailed = "Processing of the Landed Cost for one or more AP Documents has failed";
		public const string PrepaymentAppliedToMultiplyInstallments = "No applications can be created for documents with multiple installment credit terms specified.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string PaymnetTypeIsNotValidForCashAccount = "Payment Method specified is not valid for this Cash Account";
		public const string AccountIsSameAsDeferred = "Transaction Account is same as Deferral Account specified in Deferred Code.";
		public const string DebitAdjustmentRowReferecesPOOrderOrPOReceipt = "By reversing an AP bill that was matched to a PO receipt, your PO Receipt lines will be marked as unbilled and associated PO accrual account will be affected upon release of this document.";
		public const string QuantityBilledIsGreaterThenPOReceiptQuantity = "Quantity billed is greater then the quantity in the original PO Receipt for this row";
		public const string APLanededCostTranForNonLCVendor = "This vendor is not a landed cost vendor. He may not have Landed Cost included in the document";
		public const string APPaymentDoesNotMatchCABatchByAccountOrPaymentType = "One of the Payments in selection have wrong Cash Account or PaymentMethod";
		public const string APLandedCost_NoPOReceiptNumberSpecified = "At least one PO Receipt Number should be specified for this Landed Cost";
		public const string EmployeeClassExists = "This ID is already used for the Employee Class.";
		public const string VendorIsInStatus = "The vendor status is '{0}'.";
		public const string VendorCannotBe = "Vendor can not be {0}.";
		public const string PaymentIsPayedByCheck = "Prepayment cannot be voided. Please void Check {0} instead.";
		public const string APPaymentsAreAddedToTheBatchButWasNotUpdatedCorrectly = "AP Payments have been successfully added to the Batch Payment {0}, but update of their statuses have failed.";
		public const string ConflictWithExistingCheckNumber = "A check with the number '{0}' already exists in the system. Please enter another number.";
		public const string NotAllowedToEditPaymentRefWitMultistub = "The Payment Ref. number cannot be edited because the check consists of multiple stubs. To change the Payment Ref. number, use the Reprint with New Number action on the Release Payments (AP505200) form.";
		public const string TooSmallCheckNumbersGap = "This check consists of {0} stubs that require consecutive numbers from {1} to {2}. Please enter another number for the first stub because the number {3} is already used for another check.";
		public const string VendorClassCanNotBeDeletedBecauseItIsUsed = "This Vendor Class can not be deleted because it is used in Accounts Payable Preferences.";
		public const string NextCheckNumberIsRequiredForProcessing = "Next Check Number is required to print AP Payments with 'Payment Ref.' empty";
		public const string NextCheckNumberCanNotBeInc = "Next Check Number can't be incremented."; 
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string NextCheckNumberIsRequiredError = "Next Check Number is required to print this payment";
		public const string PeriodHasAPDocsFromPO_LCToBeCreated = "There one or more pending AP Bill originating from the existing Landed Cost transaction in PO module which belong to this period. They have to be created and released before the period may be closed in AP. Please, check the screen 'Process Landed Cost'(PO.50.60.00)";
		public const string EFiling1099SelectedVendorsTooltip = "Create e-file for selected vendors";
		public const string EFiling1099AllVendorsTooltip = "Create e-file for all vendors";
		public const string PreliminaryAPExpenceBooking = "Preliminary AP Expense Booking";
		public const string PreliminaryAPExpenceBookingAdjustment = "Preliminary AP Expense Booking Adjustment";
		public const string PrebookingAccountIsRequiredForPrebooking = "Pre-releasing Account must be specified to perform Pre-releasing";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string PrebookingSubAccountIsRequiredForPrebooking = "Pre-releasing Sub Account must be specified to perform Pre-releasing";
		public const string InvoicesWithMultipleInstallmentTermsMayNotBePrebooked = "Invoices with multiple installments terms may not be pre-released";
		public const string LinkToThePrebookingBatchIsMissing = "The document {0} {1} is marked as pre-released, but the link to the Pre-releasing batch is missed";
		public const string PrebookedDocumentsMayNotBeVoidedAfterTheyAreReleased = "Pre-released Documents can not be voided after they are released";
		public const string LinkToThePrebookingBatchIsMissingVoidImpossible = "The document {0} {1} is marked as pre-released, but the link to the Pre-releasing batch is missed. Void operation may not be made";
		public const string PrebookedDocumentMayNotBeVoidedIfPaymentsWereAppliedToIt = "Pre-released Document may not be voided if payment(s) has been applied to it";
		public const string PrebookingBatchDoesNotExistsInTheSystemVoidImpossible = "Pre-releasing batch {0} {1} may not be found in DB. Void operation is impossible.";
		public const string APTransactionIsNotFoundInTheReversingBatch = "AP Transaction is not found in the reversing batch";
		public const string TaxesForThisDocumentHaveBeenReportedVoidIsNotPossible = "Tax report has been created for the document {0} {1}. Void operation is impossible.";
		public const string ThisDocumentConatinsTransactionsLinkToPOVoidIsNotPossible = "Document conatains details, linked to document(s) in Purchase Order Module. Void Operation is not possible";
		public const string SomeChargeNotRelatedWithCashAccount = "Some finance charges cannot be recorded to the specified cash account. Do you want to delete these finance charges and use the selected cash account?";
		public const string SomeChargeNotRelatedWithPaymentMethod = "Some finance charges cannot be recorded to the cash account associated with the specified payment method. Do you want to delete these finance charges and use the selected payment method?";
		public const string ReferenceNotValid = "Reference Number is not valid";
		public const string GroupUpdateConfirm = "Restriction Groups will be reset for all Vendors that belongs to this vendor class.  This might override the custom settings. Please confirm your action";
		public const string DocumentNotApprovedNotProceed = "Document is not approved for payment and will not be processed.";
		public const string DiscountCodeAlreadyExist = "Discount Code already exists.";
		public const string DocDiscDescr = "Group and Document Discount";
		public const string DiscountGreaterLineTotal = "Discount Total may not be greater than Line Total";
		public const string AccountMappingNotConfigured = "Account Task Mapping is not configured for the following Project: {0}, Account: {1}";
		public const string AccountMappingNotConfiguredForDiscount = "Account Task Mapping is not configured for a document discount line. Project: {0}, Account: {1}";
		public const string SameRefNbr = "{0} with reference number {1} already exists. Enter another reference number.";
		public const string DuplicateVendorPrice = "Duplicate Vendor Price.";
		public const string LastPriceWarning = "The system retains the last price and the current price for each item.";
		public const string HistoricalPricesWarning = "The system retains changes of the price records during {0} months.";
		public const string HistoricalPricesUnlimitedWarning = "The system retains changes of the price records for an unlimited period.";
		public const string UseCurrencyPrecisionWasSet = "The Use Currency Precision flag was set because the tax report precision is equal to the currency precision";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string ApplyTaxPeriodType = "Apply changes immideately? If you press 'Yes', then new Tax Year with {0} period type would start from {1}, otherwise new Tax Year would start from {2}";
		public const string TaxYearChange = "New Tax Year with {0} period type would start from {1}. You will not be able to undo this. Approve changes?";
		public const string TaxPeriodAskHeader = "Start new Tax Year?";
		public const string CannotChangeToFiscalYear = "{0} cannot be set to {1}, because the {2} cannot be earlier than 12 month before the current business date. The system determines the {2} by retrieving a start date from the latest financial year defined in the system which is {3}. Adjust the business date or generate needed financial years.";
		public const string DocumentCannotBeDeleted = "AP document created as a result of expense claim release cannot be deleted.";
		public const string APDocumentCurrencyDiffersFromSourceDocument = "The currency of the source document is different then the one of this document. Value may be recalculated or require correction";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string PayeeBelowMinimum1099 = "Payee has below minimum.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string CreateFileFailed1099 = "Creating file for one or more vendors has failed.";
		public const string RoundingAmountTooBig = "The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the General Ledger Preferences (GL.10.20.00) form.";
		public const string CannotEditTaxAmtWOFeature = "Tax Amount cannot be edited because the Net/Gross Entry Mode feature is not enabled.";
		public const string CannotEditTaxAmtWOAPSetup = "Tax Amount cannot be edited because \"Validate Tax Totals on Entry\" is not selected on the AP Preferences form.";
        public const string TaxTotalAmountDoesntMatch = "Tax Amount must be equal to Tax Total.";
		public const string NoRoundingGainLossAccSub = "Rounding gain or loss account or subaccount is not specified for {0} currency.";
		public const string BillShouldInBeTaxSettingsMode = "This operation is available only if the Tax Settings option is specified in the Tax Calculation Mode box on the Financial Details tab.";
		public const string INReceiptMustBeReleasedBeforePPV = "IN Receipt# '{0}' created from PO Receipt# '{1}' must be released before the Purchase Price Variance transaction may be processed";
		public const string CannotFindPOReceipt = "Purchase Receipt# '{0}' was not found.";
		public const string CannotFindINReceipt = "Inventory Receipt for Purchase Receipt# '{0}' was not found.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string CannotFindInventoryItem = "Inventory Item was not found.";
		public const string APAndReclassAccountBoxesShouldNotHaveTheSameAccounts = "The AP Account and the Reclassification Account boxes should not have the same accounts specified.";
		public const string APAndReclassAccountSubaccountBoxesShouldNotHaveTheSameAccountSubaccountPairs  = "The AP Account (subaccount) and the Reclassification Account (subaccount) boxes should not have the same account-subaccount pairs specified.";
		public const string ProcessingOfPPVTransactionForAPDocFailed = "Processing of the Purchase Price Variance adjustment for one or more AP Documents has failed";
		public const string ReasonCodeCannotNotFound = "Reason Code '{0}' cannot be found";
		public const string CannotStockItemInAPBillDirectly = "It is not allowed to enter Stock Items in AP Bills directly. Please use Purchase Orders instead.";
        public const string CannotAddNonStockKitInAPBillDirectly = "A non-stock kit cannot be added to an AP bill manually. Use the Purchase Orders (PO301000) form to prepare an AP bill for the corresponding purchase order.";
		public const string ExpSubAccountCanNotBeAssembled = "Expense Subaccount cannot be assembled correctly. Please check settings for the Inventory Posting Class";
		public const string AskUpdatePayBillsFilter = "Some documents are selected in the table. Once you change any criteria, all the documents will be unselected. Do you want to continue?";
		public const string DeductibleVATNotAllowedWPOLink = "Deductible VAT is not supported for stock items and non-stock items that require receipts and have links to PO";
		public const string NotZeroQtyRequireUOM = "UOM is required if Quantity is not equal to zero.";
		public const string ExistsUnappliedPayments = "There are open payments to 1099 vendors dated {0} that will not be included into the 1099-MISC Form for this year.";
		public const string Unefiled1099Branches = "There are branch(es) {0} that have 1099 history, but are not marked for e-filing. Information for these branches will not be included into the 1099 MISC Form e-file.";
		public const string FieldNotSetInPaymentMethod = "Payments cannot be processed. The '{0}' parameter is not specified for the '{1}' payment method on the Payment Methods (CA204000) form.";
		public const string ValueMustBeGreaterThanZero = "The value must be greater than zero";
		public const string AnotherPayBillsRunning = "Another 'Prepare Payments' process is already running. Please wait until it is finished.";
		public const string DocumentCannotBeScheduled = "The document cannot be added to a schedule. Only balanced documents originated in the Accounts Payable module can be added to a schedule.";
		public const string DebitAdjustmentReason = "Debit Adjustment";
		public const string CantPrintNonprintableCheck = "The check cannot be printed because the Print Check check box is not selected on the Remittance Information tab of the Checks and Payments (AP302000) form";
		public const string ChecksMayBePrintedInPendingPrintStatus = "The check cannot be printed for this document. Checks can be printed only for documents that have the Pending Print status.";
		public const string DocAmtMustBeGreaterZero = "The document amount cannot be less than zero.";
		public const string MigrationModeIsActivated = "Migration mode is activated in the Accounts Payable module.";
		public const string MigrationModeIsActivatedForRegularDocument = "The document cannot be processed because it was created when migration mode was deactivated. To process the document, clear the Activate Migration Mode check box on the Accounts Payable Preferences (AP101000) form.";
		public const string MigrationModeIsDeactivatedForMigratedDocument = "The document cannot be processed because it was created when migration mode was activated. To process the document, activate migration mode on the Accounts Payable Preferences (AP101000) form.";
		public const string CannotReleaseMigratedDocumentInNormalMode = "The document cannot be released because it has been created in migration mode but now migration mode is deactivated. Delete the document or activate migration mode on the Accounts Payable Preferences (AP101000) form.";
		public const string CannotReleaseNormalDocumentInMigrationMode = "The document cannot be released because it was created when migration mode was deactivated. To release the document, clear the Activate Migration Mode check box on the Accounts Payable Preferences (AP101000) form.";
		public const string CannotVoidMigratedPaymentWithInitialApplication = "The payment cannot be voided because it has been created in migration mode and contains an initial application. To proceed,  void the payment in migration mode and manually post a CA receipt to update the cash account.";
		public const string CannotReverseRegularApplicationInMigrationMode = "The application cannot be reversed because it was created when migration mode was deactivated. To process the application, clear the Activate Migration Mode check box on the Accounts Payable Preferences (AP101000) form.";
		public const string EnterInitialBalanceForUnreleasedMigratedDocument = AR.Messages.EnterInitialBalanceForUnreleasedMigratedDocument;
		public const string VendorInPayRelation = "This setting cannot be specified for the selected vendor '{0}' because this vendor is involved in the Vendor Relations functionality.";
		public const string NotSuppliedByVendor = "Only the current vendor or any vendor listed on the Supplied-by Vendors tab of the Vendors (AP303000) form for the current vendor can be specified in the Pay-to Vendor box.";
		public const string SuppliedByVendorNotAllowedInPayTo = "The vendor '{0}' cannot be specified as a pay-to vendor because it is already configured as a supplied-by vendor.";
		public const string LaborUnionNotAllowedInPayTo = "The vendor '{0}' cannot be specified as a pay-to vendor because it is configured as a Labor Union vendor on the Vendors (AP303000) form.";
		public const string TaxAgencyNotAllowedInPayTo = "The vendor '{0}' cannot be specified as a pay-to vendor because it is configured as a Tax Agency on the Vendors (AP303000) form.";
		public const string Vendor1099NotAllowedInPayTo = "The vendor '{0}' cannot be specified as a pay-to vendor because it is configured as a 1099 Vendor on the Vendors (AP303000) form.";
		public const string SameVendorNotAllowedInPayTo = "The vendor '{0}' currently selected in the Vendor ID box cannot be specified as a pay-to vendor.";
		public const string APBillHasDifferentVendorOrLocation = "The bill '{0}' has different vendor or vendor location than the purchase order '{1}'.";
		public const string APBillHasDifferentCury = "The currency '{1}' of the bill '{0}' differs from currency '{3}' of the purchase order '{2}'.";
		public const string AnotherAPBillExistsPO = "Cannot add a line to the AP Bill. Another open AP Bill ({0}) exists for the following line: Order Nbr# {1}, line {2} with the Inventory ID: {3}.";
		public const string AnotherAPBillExistsPR = "Cannot add a line to the AP Bill. Another open AP Bill ({0}) exists for the following line: PO Receipt# {1}, line {2} with the Inventory ID: {3}.";
		public const string ContinueValidatingBalancesForMultipleVendors = "Validation of balances for multiple vendors may take a significant amount of time. We recommend that you select a particular vendor for balance validation to reduce time of processing. To proceed with the current settings, click OK. To select a particular vendor, click Cancel.";
		public const string NoUnitCostFound = "Unit cost has been set to zero because no effective unit cost was found.";
		#endregion

		#region Translatable Strings used in the code
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string VendorViewLocation = "Location Details";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string VendorNewLocation = "Add Location";
		public const string NewInvoice = "Enter New Bill";
		public const string NewPayment = "Enter New Payment";
		public const string MultiplyInstallmentsTranDesc = "Multiple Installment split";
		public const string AskConfirmation = "Confirmation";
		public const string Warning = "Warning";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string DeferredExpenseTranDesc = "Deferred Expense Recognition";
		public const string ReprintCaption = "Reprint";
		public const string CloseYear = "Close Year";
		public const string Times = "(times)";
		public const string DocDateSelection = "Document Date Selection";
		public const string Periods = "Periods";
		public const string ViewAPDocument = "View AP Document";
		public const string Shipping = "Shipping";
		public const string Remittance = "Remittance";
		public const string ViewCustomer = "View Customer";
		public const string ViewBusnessAccount = "View Business Account";
		public const string ExtendToCustomer = "Extend To Customer";
		public const string LandedCostAccrualCorrection = "Landed Cost Accrual correction";
		public const string LandedCostVariance = "Landed Cost Variance";
		public const string AddPostponedLandedCost = "Add Postponed Landed Cost";
		public const string LandedCostSplit = "Landed Cost Split";
		public const string VendorID = "Vendor ID";
		public const string Approved = "Approved";
		public const string Approve = "Approve";
		public const string Year1099SummaryReport = "Year 1099 Summary";
		public const string Year1099DetailReport = "Year 1099 Detail";
		public const string APBalanceByVendorReport = "AP Balance by Vendor";
		public const string VendorHistoryReport = "Vendor History";
		public const string APAgedPastDueReport = "AP Aged Past Due";
		public const string APAgedOutstandingReport = "AP Aged Outstanding";
		public const string APRegisterReport = "AP Register";
		public const string ViewBatch = "View Batch";
		public const string ReclassifyGLBatch = "Reclassify GL Batch";
		public const string ReverseApp = "Reverse Application";
		public const string ViewAppDoc = "View Application Document";
		public const string ViewAPDiscountSequence = "View Discount Sequence";
		public const string ViewDocument = "View Document";
		public const string SearchableTitleVendor = "Vendor: {0}";
		public const string SearchableTitleDocument = "AP {0}: {1} - {3}";
		public const string PaymentDescr = "Payment for {0}";
		public const string Owner = "Owner";
		public const string WorkgroupID = "Workgroup ID";
		public const string ApprovalWorkGroupID = "Approval Workgroup ID";
		public const string Approver = "Approver";
        public const string Selected = "Selected";
		public const string FailedGetFrom = CA.Messages.FailedGetFrom;
		public const string FailedGetTo = CA.Messages.FailedGetTo;
		public const string DocTypeNotSupported = "The document type is not supported or implemented.";
		public const string EmptyValuesFromAvalara = "Avalara returned the TaxSummary record with an empty TaxName and empty JurisCode.";
		public const string Process = AR.Messages.Process;
		public const string ProcessAll = AR.Messages.ProcessAll;
		public const string POReceiptBelongsAnotherVendor = "PO receipt {0} belongs to another vendor.";
		public const string WrittingError = "The system encountered at least one error while writing the {0} record (writing information about {1}). Errors: {2}. ";
		public const string Updating = "Updating";
		#endregion

		#region Graph Names
		public const string APDocumentRelease = "Bills and Adjustments Release Process";
		public const string APReleaseProcess = "AP Release Process";
		public const string APInvoiceEntry = "Enter Bill";
		public const string APPaymentEntry = "Create Check";
		public const string APApproveBills = "Approve Bills for Payment";
		public const string APPayBills = "Pay Bills";
		public const string APPayBill = "Pay Bill";
		public const string APReverseBill = "Reverse Bill";
		public const string APPrintChecks = "Checks Printing Process";
		public const string APReleaseChecks = "Check Release Process";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string APPaymentCreation = "AP Payment Creation";
		public const string APDocumentEnq = "Vendor Details";
		public const string APVendorBalanceEnq = "Vendor Balance Inquiry - Summary";
		public const string APPendingInvoicesEnq = "Bills Pending Payment Inquiry";
		public const string APChecksToPrintEnq = "Checks Pending to Process Inquiry";
		public const string APIntegrityCheck = "Vendor Balances Validation Process";
		public const string APScheduleMaint = "AP Scheduled Tasks Maintenance";
		public const string APScheduleProcess = "AP Scheduled Tasks Processing";
		public const string APScheduleRun = "AP Scheduled Tasks List";
		public const string APSetupMaint = "Setup Accounts Payable";
		public const string VendorClassMaint = "Vendor Class Maintenance";
		public const string VendorMaint = "Vendor Maintenance";
		public const string Vendor = "Vendor";
		public const string VendorLocation = "Vendor Location";
		public const string APAccess = "Vendor Access";
		public const string APAccessDetail = "Vendor Access Detail";
		public const string VendorLocationMaint = "Vendor Locations Maintenance";
		public const string AP1099DetailEnq = "1099 Details Inquiry";
		public const string AP1099SummaryEnq = "1099 Summary Inquiry";
		public const string APPayment = "Payment";
		public const string APAdjust = "Adjust";
		public const string APAdjustHistory = "Adjust History";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string APDocumentLandedCostTranProcessing = "AP Invoice Landed Cost Processing";
		public const string LandedCostTranR = "Postponed Landed Cost";
		public const string LocationAPAccountSub = "Location GL Accounts";
		public const string LocationAPPaymentInfo = "Location Payment Settings";
		public const string APPaySelReport = "Bill For Payment";
		public const string APPayNotSelReport = "Bill For Approval";
		public const string APCashRequirementReport = "Cash Requirement";
		#endregion

		#region DAC Names
		public const string VendorPaymentTypeDetail = "Payment Type Detail";
		public const string VendorBalanceSummary = "Balance Summary";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string VendorNotificationSource = "Vendor Notification Source";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string VendorNotificationRecipient = "Vendor Notification Recipient";
		public const string APInvoice = "AP Invoice";
		public const string APSetup = "Accounts Payable Preferences";
		public const string APTran = "AP Transactions";
		public const string APTaxTran = "AP Tax Details";
		public const string APLandedCostTran = "AP Landed Cost";
		public const string VendorClass = "Vendor Class";
		public const string Document = "Document";
		public const string APAddress = "AP Address";
		public const string APContact = "AP Contact";
		public const string APHistory = "AP History";
		public const string APHistoryForReport = "AP History for Report";
		public const string APHistoryByPeriod = "AP History by Period";
		public const string BaseAPHistoryByPeriod = "Base AP History by Period";

		public const string BalanceByVendor = "AP Balance by Vendor";   //MMK 2011/10/03
		public const string VendorHistory = "Vendor History";
		public const string APAgedPastDue = "AP Aged Past Due";
		public const string APAgedOutstanding = "AP Aged Outstanding";
		public const string APDocumentRegister = "AP Document Register";
		public const string RepVendorDetails = "Vendor Profile";

		public const string APInvoiceDiscountDetail = "AP Invoice Discount Detail";

		public const string AP1099Box = "AP 1099 Box";
		public const string AP1099History = "AP 1099 History";
		public const string AP1099Year = "AP 1099 Year";
		public const string CompanyBAccount1099 = "Company Business Account";
		public const string APAROrd = "APAROrd";

		public const string APDiscount = "AP Discount";
		public const string APDiscountLocation = "AP Discount Location";
		public const string APDiscountVendor = "AP Discount Vendor";
		public const string APNotification = "AP Notification";
		public const string APPaymentChargeTran = "AP Financial Charge Transaction";
		public const string APPriceWorksheet = "AP Price Worksheet";
		public const string APPriceWorksheetDetail = "AP Price Worksheet Detail";
		public const string APSetupApproval = "AP Approval Preferences";
		public const string APTax = "AP Tax Detail";
		public const string APLatestHistory = "AP Latest History";
		public const string APVendorPrice = "AP Vendor Price";
		public const string APVendorRefNbr = "APVendorRefNbr";
		public const string CABankStatementAdjustment = "Bank Statement Application";
		public const string CuryAPHistory = "Currency AP History";
		#endregion

		#region Document Type
		public const string Invoice = "Bill";
		public const string CreditAdj = "Credit Adj.";
		public const string DebitAdj = "Debit Adj.";
		public const string Check = "Check";
		public const string Prepayment = "Prepayment";
		public const string Refund = "Vendor Refund";
		public const string VoidCheck = "Voided Check";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string DeferredExpense = "Deferred Expense";
		public const string QuickCheck = "Quick Check";
		public const string VoidQuickCheck = "Void Quick Check";
		public const string APBatch = "AP Batch";
		public const string PrepaymentRequest = "Prepayment Req.";
		#endregion

		#region Report Document Type
		public const string PrintInvoice = "BILL";
		public const string PrintCreditAdj = "CRADJ";
		public const string PrintDebitAdj = "DRADJ";
		public const string PrintCheck = "CHECK";
		public const string PrintPrepayment = "PREPAY";
		public const string PrintRefund = "REF";
		public const string PrintVoidCheck = "VOIDCK";
		public const string PrintQuickCheck = "QCHECK";
		public const string PrintVoidQuickCheck = "VOIDQCK";
		#endregion

		#region Document Status
		public const string Hold = "On Hold";
		public const string Balanced = "Balanced";
		public const string Voided = "Voided";
		public const string Scheduled = "Scheduled";
		public const string Open = "Open";
		public const string Closed = "Closed";
		public const string Printed = "Printed";
		public const string Prebooked = "Pre-Released";
		public const string PendingApproval = "Pending Approval";
		public const string Rejected = "Rejected";
		public const string Reserved = "Reserved";
		public const string PendingPrint = "Pending Print";
		#endregion

		#region
		public const string PeriodHasUnreleasedDocs = "Period has Unreleased Documents";
		public const string PeriodHasHoldDocs		= "Period has Hold Documents";
		public const string PeriodHasPrebookedDocs = "Period has Pre-released Documents";
		#endregion

		#region AP Mask Codes
			public const string MaskItem = "Non-Stock Item";
            public const string MaskVendor = "Vendor";
            public const string MaskLocation = "Vendor Location";
			public const string MaskEmployee = "Employee";
			public const string MaskCompany = "Branch";
			public const string MaskProject = "Project";
			public const string MaskTask = "Project Task";
		#endregion

		#region Pay By
		public const string DueDate = "Due Date";
		public const string DiscountDate = "Discount Date";
		#endregion

		#region Check Processsing Option
		public const string ReleaseChecks = "Release";
		public const string ReprintChecksWithNewNumber = "Reprint with New Number";
		public const string ReprintChecks = "Reprint";
		public const string Void = "Void Prepayment";
		#endregion

		#region Price Basis
		public const string LastCost = "Last Cost";
		public const string StdCost = "Avg./Std. Cost";
		public const string CurrentPrice = "Source Price";
		public const string PendingPrice = "Pending Price";
		public const string RecommendedPrice = "MSRP";
		#endregion

		#region DiscountAppliedTo
		public const string ExtendedPrice = "Extended Cost";
		public const string SalesPrice = "Unit Cost";
		#endregion

		#region Discount Target
		public const string VendorUnconditional = "Unconditional";
		public const string VendorAndInventory = "Item";
		public const string VendorInventoryPrice = "Item Price Class";
		public const string Vendor_Location = "Location";
		public const string VendorLocationaAndInventory = "Item and Location";
		#endregion

		#region Tax Calculation Mode
		public const string TaxGross = "Gross";
		public const string TaxNet = "Net";
		public const string TaxSetting = "Tax Settings";

		#endregion

		#region E-File 1099 include
		public const string TransmitterOnly = "Transmitter Only";
		public const string AllMarkedBranches = "All Marked Branches";
		#endregion

		#region Link Line
		public const string LinkLine = "Link Line";
		public const string POOrderMode = "Purchase Order";
		public const string POReceiptMode = "Purchase Receipt";
		public const string NoLinkedtoReceipt = "A line of the \"Goods for IN\" type must be linked to a purchase receipt line.";
		public const string HasNoLinkedtoReceipt = "All lines of the \"Goods for IN\" type must be linked to a purchase receipt lines.";
		#endregion

		#region Vendor Update Settigs
		public const string VendorUpdateNone = "None";
		public const string VendorUpdatePurchase = "On PO Entry";
		public const string VendorUpdateReceipt = "On Receipt Entry";
		public const string VendorUpdateReceiptRelease = "On Receipt Release";
		public const string VendorUpdateAPBillRelease = "On AP Bill Release";
		#endregion

		#region VAT Recalculation 
		public const string PPDDebitAdjustmentDescr = "Debit Adjustment Description";
		public const string CashDiscountTaken = "Cash Discount Taken";
		public const string DiscountedTaxableAmount = "Discounted Taxable Amount";
		public const string DiscountedTaxableTotal = "Discounted Taxable Total";
		public const string TaxOnDiscountedPrice = "Tax on Discounted Price";
		public const string UnprocessedPPDExists = "The report cannot be generated. There are documents with unprocessed cash discounts. To proceed, process these documents by generating and releasing debit adjustments on the Generate VAT Debit Adjustments (AP.50.45.00) form.";
		public const string UnprocessedPPDExistsClosing = "There are documents with unprocessed cash discounts. Before you proceed, process these documents by generating and releasing debit adjustments on the Generate VAT Debit Adjustments (AP.50.45.00) form.";
		public const string PaidPPD = "This document has been paid in full. To close the document, apply the cash discount by generating a debit adjustment on the Generate VAT Debit Adjustments (AP.50.45.00) form.";
		public const string PPDApplicationExists = AR.Messages.PPDApplicationExists;
		public const string PartialPPD = AR.Messages.PartialPPD;
		public const string PendingPPD = "VAT Adjustment";
		public const string DeductiblePPDTaxProhibitedForReleasing = "The document cannot be released because the system does not support processing of a partially deductible VAT with a cash discount that reduces taxable amount on early payment.";
		#endregion

		public const string ViewInvoice = "View Invoice";

        public const string Days = "Days";
    }
}
