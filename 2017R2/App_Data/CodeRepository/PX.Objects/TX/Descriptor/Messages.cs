using System;

using PX.Common;
using PX.Data;

namespace PX.Objects.TX
{
	[PXLocalizable(Messages.Prefix)]
	public static class Messages
	{
		/* Add your messages here as follows (see line below):
		 * public const string YourMessage = "Your message here."; */

		#region Validation and Processing Messages
		public const string DeductiblePPDTaxProhibited = "The combination of a partially deductible VAT and a cash discount that reduces taxable amount on early payment is not supported.";
		public const string TaxAgencyStatusIs = "The tax agency status is '{0}'.";
		public const string TaxPeriodStatusIs = "The tax period status is '{0}'.";
		public const string CannotProcessW = "The record cannot be processed because the Tax Doc. Nbr box is empty. To proceed, fill in the box.";
        public const string Prefix = "TX Error";
		public const string Document_Status_Invalid = "Document Status is invalid for processing.";
		public const string Only_Prepared_CanBe_Adjusted = "You can only adjust Tax Reports with Prepared status.";
		public const string DocumentOutOfBalance = "Document is out of balance.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string TaxAlreadyBlocked = "Tax is blocked already.";
		public const string TaxAlreadyInList = "This tax is already included into the list.";
		public const string NetTaxMustBeTax = "Net Tax line must have 'Tax' type.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string NetTaxMustExist = "Net Tax line does not exist for this Vendor.";
		public const string UseTaxExcludedFromTotals = "Use Tax is excluded from Tax Total.";
		public const string NoLinesMatchTax = "No lines match the tax.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string TaxCategoryIsReferenced = "This Tax Category is referenced by {0} record and cannot be deleted.";
		public const string TaxableCategoryIDIsNotSet = "Taxable Category ID is not entered in the Import Settings. Please correct this and try again.";
		public const string TaxAgencyWithoutTran = "Tax Agency has no tax transactions.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string CannotPrepareReportForClosedOrPreparedPeriod = "Cannot prepare tax report for Closed or Prepared period.";
		public const string CannotPrepareReportPreviousOpen = "Cannot prepare tax report for open period when previous period isn't closed.";
		public const string CannotPrepareReportExistPrepared = "Cannot prepare tax report for period when previous prepared period isn't closed.";
		public const string CannotCloseReportForNotPreparedPeriod = "Cannot close tax report for Closed or Open period.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
		public const string CannotCloseReportForNotPreparedBranch = "Cannot close tax report. Branch {0} is not prepared for selected period.";
        public const string TaxBoxNumbersMustBeUnique = "Tax box numbers must be unique.";
		public const string CannotAdjustTaxForClosedOrPreparedPeriod = "Cannot adjust tax for Closed or Prepared period '{0}'.";
		public const string OriginalDocumentAlreadyContainsTaxRecord = "Original document already contains tax record.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
        public const string AnAdjustmentToReportedTaxPeriodWillBeMade = "An adjustment to the reported tax period will be made.";
		public const string NoAdjustmentToReportedTaxPeriodWillBeMade = "There is no transactions to adjust in the selected reporting period.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
	    public const string TaxGroupUsesTaxAmountButHasNoNetLine = "Tax group uses tax amount but has no net line set.";
	    public const string TheseTwoOptionsCantBeCombined = "These two options can't be combined.";
	    public const string ThisOptionCanOnlyBeUsedWithTaxTypeVAT = "This option can only be used with tax type VAT.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
	    public const string TaxRateMustBe0WhenUsingThisOption = "Tax rate must be 0 when using this option.";
        public const string TheseTwoOptionsShouldBeCombined = "These two options should be combined.";
		public const string CannotPrepareReportDefineRoundingAccounts = "The tax report cannot be prepared because the accounts to be used for recording rounding amounts are not specified on the General Ledger Preferences (GL102000) form. To proceed, specify the Rounding Gain/Loss Account (and subaccounts, if applicable).";
		public const string TaxReportRateNotFound = "Tax report preparing failed. There is no currency rate to convert tax from currency '{0}' to report currency '{1}' for date '{2}'";
		public const string FailedToGetTaxes = "Failed to get Taxes from Avalara. Check Trace for details";
		public const string FailedToApplyTaxes = "The tax amount calculated by Avalara AvaTax cannot be applied to the document.";
		public const string FailedToCancelTaxes = "Failed to Cancel the Tax on Avalara during the rollback. Details in Trace.";
	    
		public const string OneOrMoreTaxTransactionsFromPreviousPeriodsWillBeReported = "One or more tax transactions from the previous periods will be reported into the current period.";
        public const string EffectiveTaxNotFound = "Can't find effective rate for '{0}' (type '{1}')";
        public const string InactiveTaxCategory = "Tax Category '{0}' is inactive";
        public const string BucketContainsOnlyAggregateLines = "Tax reporting group {0} contains only aggregate amount lines! Please review your tax reporting setup.";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
        public const string BucketIsSubSetofAnotherBucket = "Tax group contains the same tax amount lines as tax group '{0}'";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
        public const string TempLineisAggregate = "Report line {0} is detailed by tax zones and is an aggregate. Lines cannot be detailed by tax zones and aggregates at the same time";
		public const string ClaimableAndPayableAccountsAreTheSame = "Tax Claimable and Tax Payable accounts and subaccounts for Tax {0} are the same. It's impossible to enter this Tax via GL in this configuration.";
		public const string TaxRateNotSpecified = "The {0} tax rate is not specified in the settings of the selected tax.";
		public const string NoTaxRevForTaxType = "Tax {0} has no configuration for {1} tax type, but there are tax transactions of this type.";
		public const string CheckTaxConfig = "Please check tax configuration.";
		public const string TaxReportCannotBeReleased = "The tax report cannot be released and the tax period cannot be closed because unreleased tax adjustments exist in the selected tax period.";
		public const string TheTaxReportSettingsCannotBeModified = "The tax report settings cannot be modified because there is a prepared tax report in the system for the selected tax agency. You need to release or void the prepared tax report before modifying the settings.";
		public const string SelectedDateBelongsToTheTaxPeriodThatIsGreaterThanTheSpecifiedOne = "Selected date belongs to the tax period that is greater than the specified one.";
		public const string CannotClosePeriod = "Cannot close tax period.";
		public const string ReportingPeriodDoesNotExistForTheTaxAgency = "Reporting period '{0}' does not exist for the tax agency '{1}'.";
		public const string TheReportingGroupAlreadyContainsTheReportLine = "The reporting group '{0}' already contains the report line '{1}'.";
		public const string ThereAreUnreportedTrans = "There are unreported transactions for tax(es) {0} in branch {1}. Please void this tax period and prepare it again.";
		public const string WrongTaxConfig = "There are transactions for tax(es) {0} that could not be included into report.";
		public const string RecalculateExtCost = "Do you want the system to recalculate the amount(s) in the '{0}' column?";
		public const string NoNewZonesLoaded = "No new Tax Zones found. All required Report Lines are already created.";
		public const string NoLinesByTaxZone = "This Tax Report doesn't have any Report Line that requires splitting by Tax Zones.";
		public const string NoReverseInManualVAT = "Reverse VAT cannot be used for manual tax entry.";
		public const string NoDeductibleInManualVAT = "Deductible VAT cannot be used for manual tax entry.";		
		public const string TaxPayableAccountNotSpecified = "The Tax Payable account should be specified for tax agency '{0}'.";
		public const string TaxPayableSubNotSpecified = "The Tax Payable account should be specified for tax agency '{0}'.";
		public const string ExternalTaxVendorNotFound = "Tax Vendor is required but not found for the External TaxZone.";
		public const string TaxReportHasUnprocessedSVAT = "In the tax period, there are payment applications for which pending VAT amount has not been recognized yet. To recognize pending VAT, use the Recognize Output VAT (TX503000) form and the Recognize Input VAT (TX503500) form.";
		public const string MultInstallmentTermsWithSVAT = "The document cannot be processed because VAT of the Pending type (recognized by payments) cannot be applied to documents with the multiple installment credit terms specified.";
		public const string CannotReleaseTaxReportNoFinancialPeriodForPeriodEndDate = "The tax report cannot be released and the tax period cannot be closed because there is no financial period defined for the end date ({0}) of the '{1}' tax period. To proceed, create and activate the necessary financial periods on the Financial Periods (GL633000) form.";
		public const string FinancialPeriodInactiveDocumentsWillBePostedToFirstOpenPeriod = "The financial period corresponding to the end date ({0}) of the '{1}' tax period is inactive. The generated documents will be posted to the first available open period. To override this behaviour, activate the financial period on the Financial Periods (GL633000) form before releasing the tax report.";
		public const string FinancialPeriodClosedInAPDocumentsWillBePostedToFirstOpenPeriod = "The financial period corresponding to the end date ({0}) of the '{1}' tax period is closed in the Accounts Payable module. The generated documents will be posted to the first available open period. To override this behaviour, activate 'Allow Posting to Closed Periods' on the General Ledger Preferences (GL102000) form before releasing the tax report.";
		public const string TaxReportCannotBeReleasedMigrationModeNetTax = "The tax report cannot be released and the tax period cannot be closed because Net Tax lines are configured for the tax agency and migration mode is activated in the Accounts Payable module.";
		public const string RatesForCurrencyWereNotProvided = "Rates for the {0} currency have not been specified on the Currency Rates (CM301000) form. To proceed, specify the rates.";
		public const string RateForCurrenciesAndDateNotFound = "No exchange rate from {0} to {1} has been found for the date {2}. Specify the rate on the Currency Rates (CM301000) form.";
		#endregion

		#region Translatable Strings used in the code
		public const string CurrentBatchRecord = "Current batch record";
		public const string TranDesc1 = "{0}";
		public const string TranDesc2 = "{0},{1}";
		public const string DefaultInputGroup = "Default Input Group";
		public const string DefaultOutputGroup = "Default Output Group";
		public const string TaxAmount = "Tax Amount";
		public const string TaxableAmount = "Taxable Amount";
        public const string TaxOutDateTooEarly = "Entered date is before some of tax revisions' starting dates";
		public const string GLEntry = "GL Entry";
		public const string ReversingGLEntry = "Reversing GL Entry";
		public const string AvalaraSetupNotConfigured = "Avalara Setup is not configured.";
		public const string DocumentTaxCalculationModeNotEnabled = "Document Tax Calculation mode is not enabled!";
		public const string MethodMustBeOverridden = "Method must be overridden in module-specific tax attributes!";
		public const string AreSureToShortenTaxYear = "Are you sure you want to shorten the tax year?";
		public const string SubsequentTaxYearsWillBeDeleted = "Because the end date of the tax year has been changed, the subsequent tax years will be deleted. Please review and modify the next tax year settings if required.";
		public const string AvalaraConnectSuccessAskHeader = "Setup";
		public const string AvalaraConnectSuccessAskMsg = "The connection to Avalara was successful. The version of the service is {0}.";
		public const string UnexpectedCall = "Unexpected call";
		public const string InvalidModule = "Invalid module {0}: Only AP, AR, or CA is expected.";
		public const string ProcessCaptionPost = "Post";
		public const string ProcessCaptionPostAll = "Post All";
		#endregion

		#region Graph Names
		public const string ReportTax = "Tax Preparation Process";
		public const string ReportTaxReview = "Tax Filing Process";
		public const string ReportTaxProcess = "Tax Report Creator";
		[Obsolete("This message is not used anymore and will be removed in Acumatica 2018R1")]
        public const string TaxHistoryManager = "Tax History Manager";
		public const string ReportTaxDetail = "Tax Report Details Inquiry";
		public const string SalesTaxMaint = "Taxes Maintenance";
		public const string TaxAdjustmentEntry = "Tax Adjustment Entry";
		public const string TaxBucketMaint = "Tax Reporting Groups Maintenance";
		public const string TaxCategoryMaint = "Tax Categories Maintenance";
		public const string TaxReportMaint = "Tax Report Setting Maintenance";
		public const string TaxZoneMaint = "Tax Zones Maintenance";
		public const string TaxExplorer = "Tax Explorer";
		public const string TaxImport = "Tax Import";
		public const string TaxImportSettings = "Tax Import Settings";
		public const string TaxImportDataMaint = "Tax Import Data Maintenance";
		public const string TaxImportZipDataMaint = "Tax Import Zip Data Maintenance";
		public const string AvalaraMaint = "AvaTax Configuration";
		#endregion

		#region Cache Names
		public const string TaxZone = "Tax Zone";
		public const string TaxAdjustment = "Tax Adjustment";
		public const string TaxCategory = "Tax Category";
		public const string Tax = "Tax";
		public const string VendorMaster = "Tax Agency";

		[Obsolete("The constant is obsolete and will be removed in Acumatica 8.0.")]
		public const string ReportingPeriod = "Reporting Period";

		public const string TaxPeriod = "Tax Period";
		public const string TaxTransaction = "Tax Transaction";
		public const string TaxReportLine = "Tax Report Line";
		public const string TaxReportSummary = "Tax Report Summary";
		public const string TaxDetailReport = "Tax Report Detail";
		public const string TaxBucket = "Tax Group";
		public const string TaxBucketLine = "Tax Group Line";
		public const string TaxCategoryDet = "Tax Category Detail";
		public const string TaxDetailReportCurrency = "Tax Detail Report Currency";
		public const string TaxHistory = "Tax History";
		public const string TaxHistorySum = "Tax History Sum";
		public const string TaxRev = "Tax Revision";
		public const string TaxTranReport = "Tax Transaction for Report";
		public const string TaxYear = "Tax Year";
		public const string TaxZoneDet = "Tax Zone Detail";
		public const string TaxZoneZip = "Tax Zone Zip Code";
		public const string SVATConversionHist = "SVAT Conversion History";
		#endregion

		#region Combo Values
		// TaxType
		public const string Output = "Output";
		public const string Input = "Input";
		#region Tax Bucket Type
		public const string Purchase = "Purchase";
		#endregion

		#region Tax Period Type
		public const string HalfMonth = "Half a Month";
		public const string Month = "Month";
		public const string TwoMonths = "Two Months";
		public const string Quarter = "Quarter";
		public const string HalfYear = "Half a Year";
		public const string Year = "Year";
		public const string FinancialPeriod = "Financial Period";

		[Obsolete(Common.Messages.PropertyIsObsoleteRemoveInAcumatica8)]
		public const string SemiMonthly = "Semi-Monthly";

		[Obsolete(Common.Messages.PropertyIsObsoleteRemoveInAcumatica8)]
		public const string Monthly = "Monthly";

		[Obsolete(Common.Messages.PropertyIsObsoleteRemoveInAcumatica8)]
		public const string Quarterly = "Quarterly";

		[Obsolete(Common.Messages.PropertyIsObsoleteRemoveInAcumatica8)]
		public const string Yearly = "Yearly";

		[Obsolete(Common.Messages.PropertyIsObsoleteRemoveInAcumatica8)]
		public const string FiscalPeriod = "By Financial Period";

		[Obsolete(Common.Messages.PropertyIsObsoleteRemoveInAcumatica8)]
        public const string BiMonthly = "Once in Two Months";

		[Obsolete(Common.Messages.PropertyIsObsoleteRemoveInAcumatica8)]
		public const string SemiAnnually = "Semi-Annually";
		#endregion

		#region Tax Period Status
		public const string Prepared = "Prepared";
		public const string Open = "Open";
		public const string Closed = "Closed";
		#endregion

		#region Adjustment Status
		public const string AdjHold = "On Hold";
		public const string AdjBalanced = "Balanced";
		public const string AdjReleased = "Released";
		#endregion

		#region Adjustment Types
		public const string AdjustOutput = "Adjust Output";
		public const string AdjustInput = "Adjust Input";
		#endregion

		#region VAT Invoice Types
		public const string InputVAT = "Input VAT";
		public const string OutputVAT = "Output VAT";
		#endregion

		#region Tax Type
		public const string Sales = "Sales";
		public const string Use = "Use";
		public const string VAT = "VAT";
		public const string Withholding = "Withholding";
		#endregion
		//CSTaxTermsDiscount
		public const string DiscountToTaxableAmount = "Reduces Taxable Amount";
		public const string DiscountToPromtPayment = "Reduces Taxable Amount on Early Payment";
		public const string DiscountToTotalAmount = "Does Not Affect Taxable Amount";

		#region SVAT Tax Reversal Methods
		public const string OnPayments = "On Payments";
		public const string OnDocuments = "On Documents";
		#endregion

		#region Tax Entry Ref. Nbr.
		public const string DocumentRefNbr = "Document Ref. Nbr.";
		public const string PaymentRefNbr = "Payment Ref. Nbr.";
		public const string TaxInvoiceNbr = "Tax Invoice Nbr.";
		public const string ManuallyEntered = "Manually Entered";
		#endregion
		#endregion

		#region Custom Actions
		public const string NewVendor = "New Vendor";
		public const string EditVendor = "Edit Vendor";
		public const string ReviewBatch = "Review Batch";
		public const string Release = "Release";
		public const string Document = "Document";
		public const string AdjustTax = "Adjust Tax";
		public const string NewAdjustment = "New Adjustment";
		public const string VoidReport = "Void Report";
		public const string ClosePeriod = "Close Period";
		public const string ViewDocuments = "View Documents";
		public const string Report = "Report";
		public const string PrepareTaxReport = "Prepare Tax Report";
		public const string ReleaseTaxReport = "Release Tax Report";
		public const string ViewTaxPeriods = "View Tax Periods";
		public const string Review = "Review";
		public const string TestConnection = "Test Connection";
	    public const string ViewGroupDetails = "Group Details";
		public const string CreateReportLinesForNewTaxZones = "Reload Tax Zones";
		public const string TaxSummary = "Tax Summary";
		public const string TaxDetails = "Tax Details";
		#endregion

		public const string Avalara = "Avalara";
	    public const string AvalaraUrlIsMissing = "URL is missing.";
		public const string AvalaraIsNotActive = "Avalara is not activated.";
		public const string AvalaraBranchToCompanyCodeMappingIsMissing = "Branch to Company Code mapping in the Avalara Setup is missing.";
		public const string Custom = "Custom";
		public const string ConnectionToAvalaraFailed = "Connection to Avalara failed.";
		public const string FailedToDeleteFromAvalara = "Failed to delete Taxes from Avalara. Details in the Trace.";
		public const string AvalaraTaxId = "Avalara {0}";
		public const string AvalaraTaxFor = "Avalara {0} tax for {1}";

		#region Avalara Customer Usage
		public const string FederalGovt = "Federal Government";
		public const string StateLocalGovt = "State/Local Govt.";
		public const string TribalGovt = "Tribal Government";
		public const string ForeignDiplomat = "Foreign Diplomat";
		public const string CharitableOrg = "Charitable Organization";
		public const string Religious = "Religious";
		public const string Resale = "Resale";
		public const string AgriculturalProd = "Agricultural Production";
		public const string IndustrialProd = "Industrial Prod/Mfg.";
		public const string DirectPayPermit = "Direct Pay Permit";
		public const string DirectMail = "Direct Mail";
		public const string Other = "Other";
		public const string Education = "Education";
		public const string LocalGovt = "Local Government";
		public const string ComAquaculture = "Commercial Aquaculture";
		public const string ComFishery = "Commercial Fishery";
		public const string NonResident = "Non-resident";

		public const string A = FederalGovt;
		public const string B = StateLocalGovt;
		public const string C = TribalGovt;
		public const string D = ForeignDiplomat;
		public const string E = CharitableOrg;
		public const string F = Religious;
		public const string G = Resale;
		public const string H = AgriculturalProd;
		public const string I = IndustrialProd;
		public const string J = DirectPayPermit;
		public const string K = DirectMail;
		public const string L = Other;
		public const string M = Education;
		public const string N = LocalGovt;
		public const string P = ComAquaculture;
		public const string Q = ComFishery;
		public const string R = NonResident;
		#endregion

		public const string CantUseManualVAT = "Selected tax zone is configured for manual VAT entry mode and is not available for this document. ";

		#region Field Labels

		public const string NetTaxAmount = "Net Tax Amount";

		#endregion
	}
}
