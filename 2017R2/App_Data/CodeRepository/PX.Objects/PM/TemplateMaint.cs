using PX.Data;
using PX.Objects.CS;
using System.Collections;
using PX.Objects.GL;
using PX.Objects.AR;
using PX.Objects.EP;
using PX.Objects.CT;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using PX.Objects.CR;
using PX.SM;
using PX.Objects.CA;

namespace PX.Objects.PM
{
    public class TemplateMaint : PXGraph<TemplateMaint, PMProject>, PXImportAttribute.IPXPrepareItems
	{
        #region DAC Attributes Override

        #region PMProject
        [PXDimensionSelector(ProjectAttribute.DimensionNameTemplate,
            typeof(Search<PMProject.contractCD, Where<PMProject.baseType, Equal<PMProject.ProjectBaseType>, And<PMProject.isTemplate, Equal<True>>>>),
            typeof(PMProject.contractCD),
            typeof(PMProject.contractCD), typeof(PMProject.description), typeof(PMProject.status), DescriptionField = typeof(PMProject.description))]
		[PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
        [PXDefault]
        [PXUIField(DisplayName = "Template ID", Visibility = PXUIVisibility.SelectorVisible)]
        protected virtual void PMProject_ContractCD_CacheAttached(PXCache sender)
        {
        }

        [Project(Visibility = PXUIVisibility.Invisible, Visible = false)]
        protected virtual void PMProject_TemplateID_CacheAttached(PXCache sender)
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        protected virtual void PMProject_IsTemplate_CacheAttached(PXCache sender)
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(Visibility = PXUIVisibility.Invisible, Visible = false)]
        protected virtual void PMProject_NonProject_CacheAttached(PXCache sender)
        {
        }

        [PXDBString(1, IsFixed = true)]
        [ProjectStatus.TemplStatusList]
        [PXDefault(ProjectStatus.OnHold)]
        [PXUIField(DisplayName = "Status", Required = true, Visibility = PXUIVisibility.SelectorVisible)]
        protected virtual void PMProject_Status_CacheAttached(PXCache sender)
        {
        }

        [PXDBDate]
        protected virtual void PMProject_StartDate_CacheAttached(PXCache sender)
        {
        }

        #endregion

        #region PMTask
        [Project(typeof(Where<PMProject.isTemplate, Equal<True>>), DisplayName = "Project ID", IsKey = true, DirtyRead=true)]
        [PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
        [PXDBLiteDefault(typeof(PMProject.contractID))]
        protected virtual void PMTask_ProjectID_CacheAttached(PXCache sender)
        {
        }

        [PXDBString(1, IsFixed = true)]
        [PXDefault(ProjectTaskStatus.Active)]
        [PXUIField(Visibility = PXUIVisibility.Invisible, Visible = false)]
        protected virtual void PMTask_Status_CacheAttached(PXCache sender)
        {
        }

        [Customer(DescriptionField = typeof(Customer.acctName), Visibility = PXUIVisibility.Invisible, Visible = false)]
        protected virtual void PMTask_CustomerID_CacheAttached(PXCache sender)
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Automatically Include in Project")]
        protected virtual void PMTask_AutoIncludeInPrj_CacheAttached(PXCache sender)
        {
        }

        #endregion

        #region EPEquipmentRate

        [PXDBInt(IsKey = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Equipment ID")]
        [PXSelector(typeof(EPEquipment.equipmentID), DescriptionField = typeof(EPEquipment.description), SubstituteKey = typeof(EPEquipment.equipmentCD))]
        protected virtual void EPEquipmentRate_EquipmentID_CacheAttached(PXCache sender)
        {
        }


        [PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<EPEquipmentRate.projectID>>>>))]
        [PXDBLiteDefault(typeof(PMProject.contractID))]
        [PXDBInt(IsKey = true)]
        protected virtual void EPEquipmentRate_ProjectID_CacheAttached(PXCache sender)
        {
        }

        [PXDBInt]
		[PXUIField(DisplayName = "Run Rate for Project")]
        [PXDimensionSelector(InventoryAttribute.DimensionName, typeof(Search<InventoryItem.inventoryID, Where<InventoryItem.itemType, Equal<INItemTypes.nonStockItem>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<Match<Current<AccessInfo.userName>>>>>>), typeof(InventoryItem.inventoryCD))]
        protected virtual void EPEquipmentRate_RunRateItemID_CacheAttached(PXCache sender)
        {
        }

        [PXDBInt]
		[PXUIField(DisplayName = "Setup Rate for Project")]
        [PXDimensionSelector(InventoryAttribute.DimensionName, typeof(Search<InventoryItem.inventoryID, Where<InventoryItem.itemType, Equal<INItemTypes.nonStockItem>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<Match<Current<AccessInfo.userName>>>>>>), typeof(InventoryItem.inventoryCD))]
        protected virtual void EPEquipmentRate_SetupRateItemID_CacheAttached(PXCache sender)
        {
        }

        [PXDBInt]
		[PXUIField(DisplayName = "Suspend Rate for Project")]
        [PXDimensionSelector(InventoryAttribute.DimensionName, typeof(Search<InventoryItem.inventoryID, Where<InventoryItem.itemType, Equal<INItemTypes.nonStockItem>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<Match<Current<AccessInfo.userName>>>>>>), typeof(InventoryItem.inventoryCD))]
        protected virtual void EPEquipmentRate_SuspendRateItemID_CacheAttached(PXCache sender)
        {
        }

        #endregion

		#region EPEmployeeContract
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Employee ID", Visibility = PXUIVisibility.Visible)]
		[EP.PXEPEmployeeSelector()]
		[PXCheckUnique(Where = typeof(Where<EPEmployeeContract.contractID, Equal<Current<EPEmployeeContract.contractID>>>))]
		protected virtual void EPEmployeeContract_EmployeeID_CacheAttached(PXCache sender)
		{
		}

		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(PMProject.contractID))]
		[PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<EPEmployeeContract.contractID>>>>))]
		[PXCheckUnique(Where = typeof(Where<EPEmployeeContract.employeeID, Equal<Current<EPEmployeeContract.employeeID>>>))]
		protected virtual void EPEmployeeContract_ContractID_CacheAttached(PXCache sender)
		{
		}
		#endregion

		[PXDBString(1, IsFixed = true)]
		[BillingType.ListForProject()]
		[PXUIField(DisplayName = "Billing Period")]
		protected virtual void ContractBillingSchedule_Type_CacheAttached(PXCache sender)
		{
		}

		#region PMDetail
		
		[PXDBInt()]
		[PXDBDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void PMDetail_ContractItemID_CacheAttached(PXCache sender) { }

		
		[PXDBInt()]
		[PXDBLiteDefault(typeof(PMTask.taskID))]
		[PXParent(typeof(Select<PMTask, Where<PMTask.projectID, Equal<Current<PMDetail.contractID>>, And<PMTask.taskID, Equal<Current<PMDetail.taskID>>>>>))]
		protected virtual void PMDetail_TaskID_CacheAttached(PXCache sender) { }

		#endregion

		#region NotificationSource
		[PXDBGuid(IsKey = true)]
		[PXSelector(typeof(Search<NotificationSetup.setupID,
			Where<NotificationSetup.sourceCD, Equal<PMNotificationSource.project>>>),
			 SubstituteKey = typeof(NotificationSetup.notificationCD))]
		[PXUIField(DisplayName = "Mailing ID")]
		protected virtual void NotificationSource_SetupID_CacheAttached(PXCache sender)
		{
		}
		[PXDBString(10, IsUnicode = true)]
		protected virtual void NotificationSource_ClassID_CacheAttached(PXCache sender)
		{
		}
		[GL.Branch(null, IsDetail = false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXCheckUnique(typeof(NotificationSource.setupID), IgnoreNulls = false,
			Where = typeof(Where<NotificationSource.refNoteID, Equal<Current<NotificationSource.refNoteID>>>))]
		protected virtual void NotificationSource_NBranchID_CacheAttached(PXCache sender)
		{

		}
		[PXDBString(8, InputMask = "CC.CC.CC.CC")]
		[PXUIField(DisplayName = "Report")]
		[PXDefault(typeof(Search<NotificationSetup.reportID,
			Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>),
			PersistingCheck = PXPersistingCheck.Nothing)]
		[PXSelector(typeof(Search<SiteMap.screenID,
			Where<SiteMap.url, Like<urlReports>,
				And<Where<SiteMap.screenID, Like<PXModule.pm_>>>>,
			OrderBy<Asc<SiteMap.screenID>>>), typeof(SiteMap.screenID), typeof(SiteMap.title),
			Headers = new string[] { CA.Messages.ReportID, CA.Messages.ReportName },
			DescriptionField = typeof(SiteMap.title))]
		[PXFormula(typeof(Default<NotificationSource.setupID>))]
		protected virtual void NotificationSource_ReportID_CacheAttached(PXCache sender)
		{
		}
		#endregion

		#region NotificationRecipient
		[PXDBInt]
		[PXDBLiteDefault(typeof(NotificationSource.sourceID))]
		protected virtual void NotificationRecipient_SourceID_CacheAttached(PXCache sender)
		{
		}
		[PXDBString(10)]
		[PXDefault]
		[NotificationContactType.ProjectTemplateList]
		[PXUIField(DisplayName = "Contact Type")]
		[PXCheckUnique(typeof(NotificationRecipient.contactID),
			Where = typeof(Where<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>,
			And<NotificationRecipient.refNoteID, Equal<Current<PMProject.noteID>>>>))]
		protected virtual void NotificationRecipient_ContactType_CacheAttached(PXCache sender)
		{
		}
		[PXDBInt]
		[PXUIField(DisplayName = "Contact ID")]
		[PXNotificationContactSelector(typeof(NotificationRecipient.contactType),
			typeof(Search2<Contact.contactID,
				LeftJoin<EPEmployee,
					  On<EPEmployee.parentBAccountID, Equal<Contact.bAccountID>,
					  And<EPEmployee.defContactID, Equal<Contact.contactID>>>>,
				Where<Current<NotificationRecipient.contactType>, Equal<NotificationContactType.employee>,
			  And<EPEmployee.acctCD, IsNotNull>>>)
			, DirtyRead = true)]
		protected virtual void NotificationRecipient_ContactID_CacheAttached(PXCache sender)
		{
		}
		[PXDBString(10, IsUnicode = true)]
		protected virtual void NotificationRecipient_ClassID_CacheAttached(PXCache sender)
		{
		}
		[PXString()]
		[PXUIField(DisplayName = "Email", Enabled = false)]
		protected virtual void NotificationRecipient_Email_CacheAttached(PXCache sender)
		{
		}
		#endregion

		#endregion

		#region Views/Selects

		public PXSelect<PMProject, Where<PMProject.baseType, Equal<PMProject.ProjectBaseType>,
            And<PMProject.isTemplate, Equal<True>>>> Project;
        public PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMProject.contractID>>>> ProjectProperties;

        public PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<PMProject.contractID>>>> Billing;
		[PXImport(typeof(PMProject))]
		[PXFilterable]
        public PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>>> Tasks;
		[PXFilterable]
        public PXSelectJoin<EPEquipmentRate, InnerJoin<EPEquipment, On<EPEquipmentRate.equipmentID, Equal<EPEquipment.equipmentID>>>, Where<EPEquipmentRate.projectID, Equal<Current<PMProject.contractID>>>> EquipmentRates;
		public PXSelect<PMAccountTask, Where<PMAccountTask.projectID, Equal<Current<PMProject.contractID>>>> Accounts;
        public PXSetup<PMSetup> Setup;
        public PXSetup<Company> Company;
		[PXCopyPasteHiddenFields(typeof(PMCostBudget.revisedQty), typeof(PMCostBudget.revisedAmount), typeof(PMCostBudget.costToComplete), typeof(PMCostBudget.costAtCompletion), typeof(PMCostBudget.completedPct))]
		[PXImport(typeof(PMProject))]
		[PXFilterable]
		public PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMCostBudget.type, Equal<GL.AccountType.expense>>>> CostBudget;

		[PXCopyPasteHiddenFields(typeof(PMRevenueBudget.completedPct), typeof(PMRevenueBudget.revisedQty), typeof(PMRevenueBudget.revisedAmount), typeof(PMRevenueBudget.amountToInvoice))]
		[PXImport(typeof(PMProject))]
		[PXFilterable]
		public PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMRevenueBudget.type, Equal<GL.AccountType.income>>>> RevenueBudget;
				
		public PXSelectJoin<EPEmployeeContract,
			InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPEmployeeContract.employeeID>>>,
			Where<EPEmployeeContract.contractID, Equal<Current<PMProject.contractID>>>> EmployeeContract;
		public PXSelectJoin<EPContractRate
				, LeftJoin<IN.InventoryItem, On<IN.InventoryItem.inventoryID, Equal<EPContractRate.labourItemID>>
					, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<EPContractRate.earningType>>>
					>
				, Where<EPContractRate.employeeID, Equal<Optional<EPEmployeeContract.employeeID>>, And<EPContractRate.contractID, Equal<Optional<PMProject.contractID>>>>
				, OrderBy<Asc<EPContractRate.contractID>>
				> ContractRates;

		[PXHidden]
		public PXSelect<PMDetail, Where<PMDetail.contractID, Equal<Current<PMTask.projectID>>>> BillingItems;

		[PXViewName(Messages.ProjectAnswers)]
		public TemplateAttributeList<PMProject> Answers;

		[PXHidden]
		public TemplateAttributeList<PMTask> TaskAnswers;

		public PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<PMProject.noteID>>>> Answer;
		public EPDependNoteList<NotificationSource, NotificationSource.refNoteID, PMProject> NotificationSources;
		public PXSelect<NotificationRecipient, Where<NotificationRecipient.sourceID, Equal<Optional<NotificationSource.sourceID>>>> NotificationRecipients;

		#endregion

		#region Actions/Buttons

		public PXAction<PMProject> viewTask;
        [PXUIField(DisplayName = Messages.ViewTask, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton(ImageKey = PX.Web.UI.Sprite.Main.DataEntry)]
        public IEnumerable ViewTask(PXAdapter adapter)
        {
            if (Tasks.Current != null && Project.Cache.GetStatus(Project.Current) != PXEntryStatus.Inserted)
            {
                TemplateTaskMaint graph = CreateInstance<TemplateTaskMaint>();
                graph.Task.Current = PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Select(this, Tasks.Current.ProjectID, Tasks.Current.TaskID);

                throw new PXPopupRedirectException(graph, Messages.ProjectTaskEntry + " - " + Messages.ViewTask, true);
            }
            return adapter.Get();
        }

        #endregion

        public TemplateMaint()
        {
            if (Setup.Current == null)
            {
                throw new PXException(Messages.SetupNotConfigured);
            }

			if (CostCodeAttribute.UseCostCode())
			{
				PXStringListAttribute.SetList<PMProject.budgetLevel>(Project.Cache, null, new string[] { BudgetLevels.Task, BudgetLevels.CostCode }, new string[] { Messages.Task, Messages.BudgetLevel_CostCode });
			}
			else
			{
				PXStringListAttribute.SetList<PMProject.budgetLevel>(Project.Cache, null, new string[] { BudgetLevels.Task, BudgetLevels.Item }, new string[] { Messages.Task, Messages.BudgetLevel_Item });
			}
		}

        #region Event Handlers

        
        protected virtual void _(Events.RowInserted<PMProject> e)
		{
			bool BillingCacheIsDirty = Billing.Cache.IsDirty;
			ContractBillingSchedule schedule = new ContractBillingSchedule { ContractID = e.Row.ContractID };
			Billing.Insert(schedule);
			Billing.Cache.IsDirty = BillingCacheIsDirty;

			var select = new PXSelect<NotificationSetup, Where<NotificationSetup.module, Equal<GL.BatchModule.modulePM>>>(this);
			var select2 = new PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Required<NotificationSetupRecipient.setupID>>>>(this);

			bool NotificationSourcesCacheIsDirty = NotificationSources.Cache.IsDirty;
			bool NotificationRecipientsCacheIsDirty = NotificationRecipients.Cache.IsDirty;
			foreach (NotificationSetup setup in select.Select())
			{
				NotificationSource source = new NotificationSource();
				source.SetupID = setup.SetupID;
				source.Active = setup.Active;
				source.EMailAccountID = setup.EMailAccountID;
				source.NotificationID = setup.NotificationID;
				source.ReportID = setup.ReportID;
				source.Format = setup.Format;

				NotificationSources.Insert(source);

				foreach (NotificationSetupRecipient setupRecipient in select2.Select(setup.SetupID))
				{
					NotificationRecipient recipient = new NotificationRecipient();
					recipient.SetupID = setupRecipient.SetupID;
					recipient.Active = setupRecipient.Active;
					recipient.ContactID = setupRecipient.ContactID;
					recipient.Hidden = setupRecipient.Hidden;
					recipient.ContactType = setupRecipient.ContactType;
					recipient.Format = setup.Format;

					NotificationRecipients.Insert(recipient);
				}
			}

			NotificationSources.Cache.IsDirty = NotificationSourcesCacheIsDirty;
			NotificationRecipients.Cache.IsDirty = NotificationRecipientsCacheIsDirty;
		}


		protected virtual void PMProject_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
		{
			PMProject row = e.Row as PMProject;
			if (row != null)
			{
				PMProject project = PXSelect<PMProject, Where<PMProject.templateID, Equal<Required<PMProject.contractID>>>>.Select(this, row.ContractID);

				if (project != null)
				{
					e.Cancel = true;
					throw new PXException(Messages.ProjectRefError);
				}
			}
		}

        protected virtual void PMProject_LocationID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            PMProject row = e.Row as PMProject;
            if (row != null)
            {
                sender.SetDefaultExt<PMProject.defaultSubID>(e.Row);
            }
        }

        protected virtual void PMProject_CuryID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
        {
            PMProject row = e.Row as PMProject;
            if (row != null && Company.Current != null)
            {
                row.CuryID = Company.Current.BaseCuryID;
            }
        }

		protected virtual void EPEmployeeContract_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			ContractRates.View.Cache.AllowInsert = e.Row != null;
		}

		protected virtual void EPEmployeeContract_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			EPEmployeeContract oldRow = (EPEmployeeContract)e.OldRow;
			EPEmployeeContract newRow = (EPEmployeeContract)e.Row;
			if (oldRow == null)
				return;
			EPContractRate.UpdateKeyFields(this, oldRow.ContractID, oldRow.EmployeeID, newRow.ContractID, newRow.EmployeeID);
		}

		protected virtual void _(Events.RowSelected<PMProject> e)
		{
			//unconditional visibility for Copy-Paste to work properly:
			//The Copy-Paste export creates a new Project instance with default settings and extracts the UI containers from the Page.
			//Do not set PXUIVisibility.Invisible to columns that we intent to export via Copy-Paste:

			bool enforceVisibility = false;
			if (e.Cache.GetStatus(e.Row) == PXEntryStatus.Inserted)
			{
				if (e.Row.CustomerID == null && string.IsNullOrEmpty(e.Row.Description))
				{
					//there is very high probablity that this instance was created by the system to extract the containers on UI.
					enforceVisibility = true;
				}
			}

			if (e.Row != null)
			{
				PXUIFieldAttribute.SetVisible<PMProject.curyID>(e.Cache, e.Row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInGL>(e.Cache, e.Row, Setup.Current.VisibleInGL == true);
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInAP>(e.Cache, e.Row, Setup.Current.VisibleInAP == true);
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInAR>(e.Cache, e.Row, Setup.Current.VisibleInAR == true);
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInSO>(e.Cache, e.Row, Setup.Current.VisibleInSO == true);
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInPO>(e.Cache, e.Row, Setup.Current.VisibleInPO == true);
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInTA>(e.Cache, e.Row, Setup.Current.VisibleInTA == true);
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInEA>(e.Cache, e.Row, Setup.Current.VisibleInEA == true);
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInIN>(e.Cache, e.Row, Setup.Current.VisibleInIN == true);
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInCA>(e.Cache, e.Row, Setup.Current.VisibleInCA == true);
				PXUIFieldAttribute.SetEnabled<PMProject.visibleInCR>(e.Cache, e.Row, Setup.Current.VisibleInCR == true);
				PXUIFieldAttribute.SetEnabled<PMProject.templateID>(e.Cache, e.Row, e.Row.TemplateID == null && e.Cache.GetStatus(e.Row) == PXEntryStatus.Inserted);
							
				PXUIFieldAttribute.SetVisible<PMCostBudget.inventoryID>(CostBudget.Cache, null, !CostCodeAttribute.UseCostCode());
				PXUIFieldAttribute.SetVisible<PMCostBudget.revenueInventoryID>(CostBudget.Cache, null, e.Row.BudgetLevel == BudgetLevels.Item);

				PXUIFieldAttribute.SetVisibility<PMCostBudget.inventoryID>(CostBudget.Cache, null, !CostCodeAttribute.UseCostCode() ? PXUIVisibility.Visible : PXUIVisibility.Invisible);
				PXUIFieldAttribute.SetVisibility<PMCostBudget.revenueInventoryID>(CostBudget.Cache, null, e.Row.BudgetLevel == BudgetLevels.Item ? PXUIVisibility.Visible : PXUIVisibility.Invisible);
				PXUIFieldAttribute.SetVisibility<PMCostBudget.inventoryID>(CostBudget.Cache, null, !CostCodeAttribute.UseCostCode() ? PXUIVisibility.Visible : PXUIVisibility.Invisible);

				PXUIFieldAttribute.SetVisible<PMRevenueBudget.inventoryID>(RevenueBudget.Cache, null, !CostCodeAttribute.UseCostCode() && e.Row.BudgetLevel == BudgetLevels.Item || IsCopyPasteContext || IsExport || IsImport || enforceVisibility);
				PXUIFieldAttribute.SetVisible<PMRevenueBudget.costCodeID>(RevenueBudget.Cache, null, CostCodeAttribute.UseCostCode() && e.Row.BudgetLevel == BudgetLevels.CostCode || IsCopyPasteContext || IsExport || IsImport || enforceVisibility);
				PXUIFieldAttribute.SetRequired<PMRevenueBudget.inventoryID>(RevenueBudget.Cache, e.Row.BudgetLevel == BudgetLevels.Item);
				PXUIFieldAttribute.SetVisible<PMRevenueBudget.prepaymentAmount>(RevenueBudget.Cache, null, PrepaymentVisible());
				PXUIFieldAttribute.SetVisible<PMRevenueBudget.prepaymentPct>(RevenueBudget.Cache, null, PrepaymentVisible());
				PXUIFieldAttribute.SetVisible<PMRevenueBudget.limitQty>(RevenueBudget.Cache, null, false);
				PXUIFieldAttribute.SetVisible<PMRevenueBudget.maxQty>(RevenueBudget.Cache, null, false);
				PXUIFieldAttribute.SetVisible<PMRevenueBudget.limitAmount>(RevenueBudget.Cache, null, LimitsVisible());
				PXUIFieldAttribute.SetVisible<PMRevenueBudget.maxAmount>(RevenueBudget.Cache, null, LimitsVisible());

				PXUIVisibility inventoryIDVisibility = (!CostCodeAttribute.UseCostCode() && e.Row.BudgetLevel == BudgetLevels.Item || IsCopyPasteContext || IsExport || IsImport || enforceVisibility) ? PXUIVisibility.Visible : PXUIVisibility.Invisible;
				PXUIVisibility costCodeIDVisibility = (CostCodeAttribute.UseCostCode() && e.Row.BudgetLevel == BudgetLevels.CostCode || IsCopyPasteContext || IsExport || IsImport || enforceVisibility) ? PXUIVisibility.Visible : PXUIVisibility.Invisible;

				PXUIFieldAttribute.SetVisibility<PMRevenueBudget.inventoryID>(RevenueBudget.Cache, null, inventoryIDVisibility);
				PXUIFieldAttribute.SetVisibility<PMRevenueBudget.costCodeID>(RevenueBudget.Cache, null, costCodeIDVisibility);
				PXUIFieldAttribute.SetVisibility<PMRevenueBudget.prepaymentAmount>(RevenueBudget.Cache, null, PrepaymentVisible() ? PXUIVisibility.Visible : PXUIVisibility.Invisible);
				PXUIFieldAttribute.SetVisibility<PMRevenueBudget.prepaymentPct>(RevenueBudget.Cache, null, PrepaymentVisible() ? PXUIVisibility.Visible : PXUIVisibility.Invisible);
				PXUIFieldAttribute.SetVisibility<PMRevenueBudget.limitQty>(RevenueBudget.Cache, null, false ? PXUIVisibility.Visible : PXUIVisibility.Invisible);
				PXUIFieldAttribute.SetVisibility<PMRevenueBudget.maxQty>(RevenueBudget.Cache, null, false ? PXUIVisibility.Visible : PXUIVisibility.Invisible);
				PXUIFieldAttribute.SetVisibility<PMRevenueBudget.limitAmount>(RevenueBudget.Cache, null, LimitsVisible() ? PXUIVisibility.Visible : PXUIVisibility.Invisible);
				PXUIFieldAttribute.SetVisibility<PMRevenueBudget.maxAmount>(RevenueBudget.Cache, null, LimitsVisible() ? PXUIVisibility.Visible : PXUIVisibility.Invisible);

				PXUIFieldAttribute.SetVisibility<PMTask.retainagePct>(Tasks.Cache, null, CostCodeAttribute.UseCostCode() ? PXUIVisibility.Visible : PXUIVisibility.Invisible);
				PXUIFieldAttribute.SetVisible<PMTask.retainagePct>(Tasks.Cache, null, CostCodeAttribute.UseCostCode());
			}
		}

		#region Revenue Budget
		protected virtual void _(Events.RowSelected<PMRevenueBudget> e)
		{
			if (e.Row != null)
			{
				PXUIFieldAttribute.SetEnabled<PMRevenueBudget.limitQty>(e.Cache, e.Row, !string.IsNullOrEmpty(e.Row.UOM));
				PXUIFieldAttribute.SetEnabled<PMRevenueBudget.maxQty>(e.Cache, e.Row, e.Row.LimitQty == true && !string.IsNullOrEmpty(e.Row.UOM));
				PXUIFieldAttribute.SetEnabled<PMRevenueBudget.maxAmount>(e.Cache, e.Row, e.Row.LimitAmount == true);

				if ((e.Row.Qty != 0 || e.Row.RevisedQty != 0) && string.IsNullOrEmpty(e.Row.UOM))
				{
					if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMRevenueBudget.uOM>(e.Cache, e.Row)))
						PXUIFieldAttribute.SetWarning<PMRevenueBudget.uOM>(e.Cache, e.Row, Messages.UomNotDefinedForBudget);
				}
				else
				{
					string errorText = PXUIFieldAttribute.GetError<PMRevenueBudget.uOM>(e.Cache, e.Row);
					if (errorText == PXLocalizer.Localize(Messages.UomNotDefinedForBudget))
					{
						PXUIFieldAttribute.SetWarning<PMRevenueBudget.uOM>(e.Cache, e.Row, null);
					}
				}
			}
		}
				
		protected virtual void _(Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.costCodeID> e)
		{
			if (CostCodeAttribute.UseCostCode() && Project.Current?.BudgetLevel == BudgetLevels.CostCode)
				e.Cache.SetDefaultExt<PMRevenueBudget.description>(e.Row);
		}

		protected virtual void _(Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID> e)
		{
			if (!CostCodeAttribute.UseCostCode())
			{
				//Current record may be in process of importing from excel. In this case all we have is pending values for description, Uom, Rate
				string pendingDescription = null;
				string pendingUom = null;

				object pendingDescriptionObj = CostBudget.Cache.GetValuePending<PMRevenueBudget.description>(e.Row);
				object pendingUomObj = CostBudget.Cache.GetValuePending<PMRevenueBudget.uOM>(e.Row);

				if (pendingDescriptionObj != null && pendingDescriptionObj != PXCache.NotSetValue)
					pendingDescription = (string)pendingDescriptionObj;

				if (pendingUomObj != null && pendingUomObj != PXCache.NotSetValue)
					pendingUom = (string)pendingUomObj;

				object pendingRate = RevenueBudget.Cache.GetValuePending<PMRevenueBudget.rate>(e.Row);

				if (string.IsNullOrEmpty(pendingDescription))
					e.Cache.SetDefaultExt<PMRevenueBudget.description>(e.Row);
				if (e.Row.AccountGroupID == null)
					e.Cache.SetDefaultExt<PMRevenueBudget.accountGroupID>(e.Row);

				if (string.IsNullOrEmpty(pendingUom))
					e.Cache.SetDefaultExt<PMRevenueBudget.uOM>(e.Row);

				if (pendingRate == null)
					e.Cache.SetDefaultExt<PMRevenueBudget.rate>(e.Row);
			}
		}

		protected virtual void _(Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description> e)
		{
			if (e.Row == null) return;

			if (CostCodeAttribute.UseCostCode())
			{
				if (Project.Current.BudgetLevel == BudgetLevels.CostCode)
				{
					if (e.Row.CostCodeID != null)
					{
						PMCostCode costCode = PXSelectorAttribute.Select<PMRevenueBudget.costCodeID>(e.Cache, e.Row) as PMCostCode;
						if (costCode != null)
						{
							e.NewValue = costCode.Description;
						}
					}
				}
				else if (Project.Current.BudgetLevel == BudgetLevels.Task)
				{
					if (e.Row.ProjectTaskID != null)
					{
						PMTask projectTask = PXSelectorAttribute.Select<PMRevenueBudget.projectTaskID>(e.Cache, e.Row) as PMTask;
						if (projectTask != null)
						{
							e.NewValue = projectTask.Description;
						}
					}
				}
			}
			else
			{
				if (Project.Current.BudgetLevel == BudgetLevels.Task)
				{
					if (e.Row.ProjectTaskID != null)
					{
						PMTask projectTask = PXSelectorAttribute.Select<PMRevenueBudget.projectTaskID>(e.Cache, e.Row) as PMTask;
						if (projectTask != null)
						{
							e.NewValue = projectTask.Description;
						}
					}
				}
				else
				{
					if (e.Row.InventoryID != null && e.Row.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID)
					{
						InventoryItem item = PXSelectorAttribute.Select<PMRevenueBudget.inventoryID>(e.Cache, e.Row) as InventoryItem;
						if (item != null)
						{
							e.NewValue = item.Descr;
						}
					}
				}
			}
		}

		protected virtual void _(Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.accountGroupID> e)
		{
			if (e.Row == null) return;

			var select = new PXSelect<PMAccountGroup, Where<PMAccountGroup.type, Equal<GL.AccountType.income>>>(this);

			var resultset = select.SelectWindowed(0, 2);

			if (resultset.Count == 1)
			{
				e.NewValue = ((PMAccountGroup)resultset).GroupID;
			}
			else
			{
				if (e.Row.InventoryID != null && e.Row.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID)
				{
					InventoryItem item = PXSelectorAttribute.Select<PMRevenueBudget.inventoryID>(e.Cache, e.Row) as InventoryItem;
					if (item != null)
					{
						Account account = PXSelectorAttribute.Select<InventoryItem.salesAcctID>(Caches[typeof(InventoryItem)], item) as Account;
						if (account != null && account.AccountGroupID != null)
						{
							e.NewValue = account.AccountGroupID;
						}
					}
				}
			}
		}

		protected virtual void _(Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID> e)
		{
			e.Cache.SetDefaultExt<PMRevenueBudget.description>(e.Row);
		}

		protected virtual void _(Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.inventoryID> e)
		{
			e.NewValue = PMInventorySelectorAttribute.EmptyInventoryID;
		}

		protected virtual void _(Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.costCodeID> e)
		{
			if (Project.Current != null)
			{
				if (Project.Current.BudgetLevel != BudgetLevels.CostCode)
				{
					e.NewValue = CostCodeAttribute.GetDefaultCostCode();
				}
			}
		}


		protected virtual void _(Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.rate> e)
		{
			if (Project.Current != null)
			{
				if (e.Row.InventoryID != null && e.Row.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID)
				{
					string customerPriceClass = ARPriceClass.EmptyPriceClass;
										
					CM.CurrencyInfo dummy = new CM.CurrencyInfo();
					dummy.CuryID = Accessinfo.BaseCuryID;
					dummy.BaseCuryID = Accessinfo.BaseCuryID;
					dummy.CuryRate = 1;

					e.NewValue = ARSalesPriceMaint.CalculateSalesPrice(Caches[typeof(PMTran)], customerPriceClass, e.Row.InventoryID, dummy, e.Row.UOM, Accessinfo.BusinessDate.Value, true);
				}
			}
		}

		protected virtual void _(Events.RowPersisting<PMRevenueBudget> e)
		{
			if (e.Operation != PXDBOperation.Delete && Project.Current != null)
			{
				if (Project.Current.BudgetLevel == BudgetLevels.Item && string.IsNullOrEmpty(e.Row.Description))
				{
					e.Cache.RaiseExceptionHandling<PMRevenueBudget.description>(e.Row, null, new PXSetPropertyException<PMRevenueBudget.description>(Data.ErrorMessages.FieldIsEmpty, nameof(PMRevenueBudget.Description)));
					throw new PXRowPersistingException(nameof(PMRevenueBudget.Description), null, ErrorMessages.FieldIsEmpty, nameof(PMRevenueBudget.Description));
				}
			}
		}

				
		protected virtual void _(Events.FieldSelecting<PMRevenueBudget, PMRevenueBudget.prepaymentPct> e)
		{
			if (e.Row != null)
			{
				decimal budgetedAmount = e.Row.Amount.GetValueOrDefault();
				decimal result = 0;

				if (budgetedAmount != 0)
					result = e.Row.PrepaymentAmount.GetValueOrDefault() * 100 / budgetedAmount;

				PXFieldState fieldState = PXDecimalState.CreateInstance(result, (short)2, nameof(PMRevenueBudget.prepaymentPct), false, 0, Decimal.MinValue, Decimal.MaxValue);
				e.ReturnState = fieldState;
			}
		}

		protected virtual void _(Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.prepaymentPct> e)
		{
			if (e.Row != null)
			{
				decimal budgetedAmount = e.Row.Amount.GetValueOrDefault();
				decimal prepayment = Math.Max(0, (budgetedAmount * e.Row.PrepaymentPct.GetValueOrDefault() / 100m));

				e.Cache.SetValueExt<PMRevenueBudget.prepaymentAmount>(e.Row, prepayment);
			}
		}

		protected virtual void _(Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.prepaymentAmount> e)
		{
			decimal budgetedAmount = e.Row.Amount.GetValueOrDefault();
			decimal? prepayment = (decimal?)e.NewValue;
			
			if (prepayment > budgetedAmount )
			{
				e.Cache.RaiseExceptionHandling<PMRevenueBudget.prepaymentAmount>(e.Row, e.NewValue, new PXSetPropertyException<PMRevenueBudget.prepaymentAmount>(Messages.PrepaymentAmointExceedsRevisedAmount, PXErrorLevel.Warning));
			}
		}

		protected virtual void _(Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.prepaymentAmount> e)
		{
			if (e.Row != null)
			{
				e.Row.PrepaymentAvailable = e.Row.PrepaymentAmount.GetValueOrDefault() - e.Row.PrepaymentInvoiced.GetValueOrDefault();
			
			}
		}

		#endregion

		#region Cost Budget

		protected virtual void _(Events.RowSelected<PMCostBudget> e)
		{
			if (e.Row != null)
			{
				if ((e.Row.Qty != 0 || e.Row.RevisedQty != 0) && string.IsNullOrEmpty(e.Row.UOM))
				{
					if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMCostBudget.uOM>(e.Cache, e.Row)))
						PXUIFieldAttribute.SetWarning<PMCostBudget.uOM>(e.Cache, e.Row, Messages.UomNotDefinedForBudget);
				}
				else
				{
					string errorText = PXUIFieldAttribute.GetError<PMCostBudget.uOM>(e.Cache, e.Row);
					if (errorText == PXLocalizer.Localize(Messages.UomNotDefinedForBudget))
					{
						PXUIFieldAttribute.SetWarning<PMCostBudget.uOM>(e.Cache, e.Row, null);
					}
				}
			}
		}
				
		protected virtual void _(Events.FieldDefaulting<PMCostBudget, PMCostBudget.rate> e)
		{
			if (Project.Current != null)
			{
				if (e.Row.InventoryID != null && e.Row.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID)
				{
					InventoryItem item = (InventoryItem)PXSelectorAttribute.Select<PMCostBudget.inventoryID>(e.Cache, e.Row);
					e.NewValue = item?.StdCost;
				}
			}
		}

		protected virtual void _(Events.FieldDefaulting<PMCostBudget, PMCostBudget.inventoryID> e)
		{
			e.NewValue = PMInventorySelectorAttribute.EmptyInventoryID;
		}

		protected virtual void _(Events.FieldDefaulting<PMCostBudget, PMCostBudget.costCodeID> e)
		{
			if (Project.Current != null)
			{
				if (Project.Current.BudgetLevel != BudgetLevels.CostCode)
				{
					e.NewValue = CostCodeAttribute.GetDefaultCostCode();
				}
			}
		}
		

		protected virtual void _(Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID> e)
		{
			if (!CostCodeAttribute.UseCostCode())
			{
				//Current record may be in process of importing from excel. In this case all we have is pending values for description, Uom, Rate
				string pendingDescription = null;
				string pendingUom = null;

				object pendingDescriptionObj = CostBudget.Cache.GetValuePending<PMCostBudget.description>(e.Row);
				object pendingUomObj = CostBudget.Cache.GetValuePending<PMCostBudget.uOM>(e.Row);

				if (pendingDescriptionObj != null && pendingDescriptionObj != PXCache.NotSetValue)
					pendingDescription = (string)pendingDescriptionObj;

				if (pendingUomObj != null && pendingUomObj != PXCache.NotSetValue)
					pendingUom = (string)pendingUomObj;

				object pendingRate = CostBudget.Cache.GetValuePending<PMCostBudget.rate>(e.Row);

				if (string.IsNullOrEmpty(pendingDescription))
					e.Cache.SetDefaultExt<PMCostBudget.description>(e.Row);
				if (e.Row.AccountGroupID == null)
					e.Cache.SetDefaultExt<PMCostBudget.accountGroupID>(e.Row);

				if (string.IsNullOrEmpty(pendingUom))
					e.Cache.SetDefaultExt<PMCostBudget.uOM>(e.Row);

				if (pendingRate == null)
					e.Cache.SetDefaultExt<PMCostBudget.rate>(e.Row);
			}
		}

		protected virtual void _(Events.FieldUpdated<PMCostBudget, PMCostBudget.revenueTaskID> e)
		{
			var select = new PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMCostBudget.projectID>>,
				And<PMRevenueBudget.projectTaskID, Equal<Current<PMCostBudget.revenueTaskID>>,
				And<PMRevenueBudget.inventoryID, Equal<Current<PMCostBudget.inventoryID>>>>>>(this);

			PMRevenueBudget revenue = select.Select();

			if (revenue == null)
				e.Row.RevenueInventoryID = null;
		}


		protected virtual void _(Events.FieldDefaulting<PMCostBudget, PMCostBudget.accountGroupID> e)
		{
			if (e.Row == null) return;
			if (e.Row.InventoryID != null && e.Row.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID)
			{
				InventoryItem item = PXSelectorAttribute.Select<PMCostBudget.inventoryID>(e.Cache, e.Row) as InventoryItem;
				if (item != null)
				{
					Account account = PXSelectorAttribute.Select<InventoryItem.cOGSAcctID>(Caches[typeof(InventoryItem)], item) as Account;
					if (account != null && account.AccountGroupID != null)
					{
						e.NewValue = account.AccountGroupID;
					}
				}
			}
		}

		protected virtual void _(Events.FieldUpdated<PMCostBudget, PMCostBudget.costCodeID> e)
		{
			e.Cache.SetDefaultExt<PMCostBudget.description>(e.Row);
		}

		protected virtual void _(Events.FieldDefaulting<PMCostBudget, PMCostBudget.description> e)
		{
			if (e.Row == null) return;

			if (CostCodeAttribute.UseCostCode())
			{
				if (e.Row.CostCodeID != null && e.Row.CostCodeID != CostCodeAttribute.GetDefaultCostCode())
				{
					PMCostCode costCode = PXSelectorAttribute.Select<PMCostBudget.costCodeID>(e.Cache, e.Row) as PMCostCode;
					if (costCode != null)
					{
						e.NewValue = costCode.Description;
					}
				}
			}
			else
			{
				if (e.Row.InventoryID != null && e.Row.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID)
				{
					InventoryItem item = PXSelectorAttribute.Select<PMCostBudget.inventoryID>(e.Cache, e.Row) as InventoryItem;
					if (item != null)
					{
						e.NewValue = item.Descr;
					}
				}
			}
		}
				
		#endregion
				
		Dictionary<int?, int?> persistedTask = new Dictionary<int?, int?>();
		int? negativeKey = null;
		protected virtual void _(Events.RowPersisting<PMTask> e)
		{
			if (e.Operation == PXDBOperation.Insert)
			{
				negativeKey = e.Row.TaskID;
			}
		}

		protected virtual void _(Events.RowPersisted<PMTask> e)
		{
			if (e.Operation == PXDBOperation.Insert && e.TranStatus == PXTranStatus.Open && negativeKey != null)
			{
				int? newKey = e.Row.TaskID;

				foreach (PMCostBudget budget in CostBudget.Cache.Inserted)
				{
					if (budget.RevenueTaskID != null && budget.RevenueTaskID == negativeKey)
					{
						CostBudget.Cache.SetValue<PMCostBudget.revenueTaskID>(budget, newKey);
						if (!persistedTask.ContainsKey(newKey))
							persistedTask.Add(newKey, negativeKey);
					}
				}

				negativeKey = null;
			}

			if (e.Operation == PXDBOperation.Insert && e.TranStatus == PXTranStatus.Aborted)
			{
				foreach (PMCostBudget budget in CostBudget.Cache.Inserted)
				{
					if (budget.RevenueTaskID != null && persistedTask.TryGetValue(e.Row.TaskID, out negativeKey))
					{
						CostBudget.Cache.SetValue<PMCostBudget.revenueTaskID>(budget, negativeKey);
					}
				}

				foreach (PMCostBudget budget in CostBudget.Cache.Updated)
				{
					if (budget.RevenueTaskID != null && persistedTask.TryGetValue(e.Row.TaskID, out negativeKey))
					{
						CostBudget.Cache.SetValue<PMCostBudget.revenueTaskID>(budget, negativeKey);
					}
				}
			}
		}

		#region Notification Events
		protected virtual void _(Events.RowSelected<NotificationRecipient> e)
		{
			if (e.Row == null)
				return;

			Contact contact = PXSelectorAttribute.Select<NotificationRecipient.contactID>(e.Cache, e.Row) as Contact;
			if (contact != null)
			{
				e.Row.Email = contact.EMail;
			}
		}

		protected virtual void _(Events.RowSelected<NotificationSource> e)
		{
			if (e.Row != null)
			{
				NotificationSetup ns = PXSelect<NotificationSetup, Where<NotificationSetup.setupID, Equal<Required<NotificationSetup.setupID>>>>.Select(this, e.Row.SetupID);
				if (ns != null && ns.NotificationCD == ProformaEntry.ProformaNotificationCD)
				{
					PXUIFieldAttribute.SetEnabled<NotificationSource.active>(e.Cache, e.Row, false);
				}
				else
				{
					PXUIFieldAttribute.SetEnabled<NotificationSource.active>(e.Cache, e.Row, true);
				}
			}
		}
		#endregion

		#endregion

		public virtual bool PrepaymentVisible()
		{
			if (Project.Current != null)
			{
				return Project.Current.PrepaymentEnabled == true;
			}

			return false;
		}

		public virtual bool LimitsVisible()
		{
			if (Project.Current != null)
			{
				return Project.Current.LimitsEnabled == true;
			}

			return false;
		}

		#region PMImport Implementation
		public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
		{
			if (viewName == nameof(RevenueBudget))
			{
				string accountGroupCD = null;
				if (keys.Contains(nameof(PMRevenueBudget.AccountGroupID)))
				{
					//Import file could be missing the AccountGroupID field and hence the Default value could be set by the DefaultEventHandler

					object keyVal = keys[nameof(PMRevenueBudget.AccountGroupID)];

					if (keyVal is int)
					{
						PMAccountGroup accountGroup = PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Required<PMAccountGroup.groupID>>>>.Select(this, keyVal);
						if (accountGroup != null)
						{
							return accountGroup.Type == GL.AccountType.Income;
						}
					}
					else
					{
						accountGroupCD = (string)keys[nameof(PMRevenueBudget.AccountGroupID)];
					}
				}

				if (!string.IsNullOrEmpty(accountGroupCD))
				{
					PMAccountGroup accountGroup = PXSelect<PMAccountGroup, Where<PMAccountGroup.groupCD, Equal<Required<PMAccountGroup.groupCD>>>>.Select(this, accountGroupCD);
					if (accountGroup != null)
					{
						return accountGroup.Type == GL.AccountType.Income;
					}
				}
				else
				{
					return true;
				}

				return false;

			}
			else if (viewName == nameof(CostBudget))
			{
				string accountGroupCD = null;
				if (keys.Contains(nameof(PMCostBudget.AccountGroupID)))
				{
					accountGroupCD = (string)keys[nameof(PMCostBudget.AccountGroupID)];
				}

				if (!string.IsNullOrEmpty(accountGroupCD))
				{
					PMAccountGroup accountGroup = PXSelect<PMAccountGroup, Where<PMAccountGroup.groupCD, Equal<Required<PMAccountGroup.groupCD>>>>.Select(this, accountGroupCD);
					if (accountGroup != null)
					{
						return accountGroup.IsExpense == true;
					}
				}
				else
				{
					return true;
				}

				return false;
			}
			

			return true;
		}

		public bool RowImporting(string viewName, object row)
		{
			return true;
		}

		public bool RowImported(string viewName, object row, object oldRow)
		{
			return oldRow == null;
		}

		public void PrepareItems(string viewName, IEnumerable items) { }
		#endregion
	}

	public class TemplateAttributeList<T> : CRAttributeList<T>
	{
		public TemplateAttributeList(PXGraph graph) : base(graph) { }

		protected override void ReferenceRowPersistingHandler(PXCache sender, PXRowPersistingEventArgs e)
		{
			var row = e.Row;

			if (row == null) return;

			var answersCache = _Graph.Caches[typeof(CSAnswers)];

			foreach (CSAnswers answer in answersCache.Cached)
			{
				if (e.Operation == PXDBOperation.Delete)
				{
					answersCache.Delete(answer);
				}				
			}			
		}

		protected override void RowPersistingHandler(PXCache sender, PXRowPersistingEventArgs e)
		{
			if (e.Operation != PXDBOperation.Insert && e.Operation != PXDBOperation.Update) return;

			var row = e.Row as CSAnswers;
			if (row == null) return;

			if (!row.RefNoteID.HasValue)
			{
				e.Cancel = true;
				RowPersistDeleted(sender, row);
			}
		}
	}
}
