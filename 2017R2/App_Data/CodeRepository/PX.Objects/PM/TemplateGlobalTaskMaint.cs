using System;
using System.Collections.Generic;
using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System.Collections;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.CT;
using PX.Objects.CR;

namespace PX.Objects.PM
{
	public class TemplateGlobalTaskMaint : PXGraph<TemplateGlobalTaskMaint>
	{
		#region DAC Attributes Override

        #region PMTask
        [PXDBInt(IsKey = true)]
        [PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
        [PXDefault(typeof(Search<PMProject.contractID, Where<PMProject.nonProject, Equal<True>>>))]
        protected virtual void PMTask_ProjectID_CacheAttached(PXCache sender)
        {
        }

        [PXDimensionSelector(ProjectTaskAttribute.DimensionName,
            typeof(Search<PMTask.taskCD, Where<PMTask.projectID, Equal<Current<PMTask.projectID>>>>),
            typeof(PMTask.taskCD),
            typeof(PMTask.taskCD), typeof(PMTask.locationID), typeof(PMTask.description), typeof(PMTask.status), DescriptionField = typeof(PMTask.description))]
		[PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
        [PXDefault]
        [PXUIField(DisplayName = "Task ID", Visibility = PXUIVisibility.SelectorVisible)]
        protected virtual void PMTask_TaskCD_CacheAttached(PXCache sender)
        {
        }

        [PXDBString(1, IsFixed = true)]
        [PXDefault(ProjectTaskStatus.Active)]
        [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.Invisible, Visible = false)]
        protected virtual void PMTask_Status_CacheAttached(PXCache sender)
        {
        }

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInGL>))]
		[PXUIField(DisplayName = "GL")]
		protected virtual void PMTask_VisibleInGL_CacheAttached(PXCache sender){}

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInAP>))]
		[PXUIField(DisplayName = "AP")]
		protected virtual void PMTask_VisibleInAP_CacheAttached(PXCache sender) { }

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInAR>))]
		[PXUIField(DisplayName = "AR")]
		protected virtual void PMTask_VisibleInAR_CacheAttached(PXCache sender) { }

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInCA>))]
		[PXUIField(DisplayName = "CA")]
		protected virtual void PMTask_VisibleInCA_CacheAttached(PXCache sender) { }

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInCR>))]
		[PXUIField(DisplayName = "CRM")]
		protected virtual void PMTask_VisibleInCR_CacheAttached(PXCache sender) { }

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInTA>))]
		[PXUIField(DisplayName = "Time Entries")]
		protected virtual void PMTask_VisibleInTA_CacheAttached(PXCache sender) { }

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInEA>))]
		[PXUIField(DisplayName = "Expenses")]
		protected virtual void PMTask_VisibleInEA_CacheAttached(PXCache sender) { }

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInIN>))]
		[PXUIField(DisplayName = "IN")]
		protected virtual void PMTask_VisibleInIN_CacheAttached(PXCache sender) { }

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInSO>))]
		[PXUIField(DisplayName = "SO")]
		protected virtual void PMTask_VisibleInSO_CacheAttached(PXCache sender) { }

		[PXDBBool()]
		[PXDefault(typeof(Search<PMSetup.visibleInPO>))]
		[PXUIField(DisplayName = "PO")]
		protected virtual void PMTask_VisibleInPO_CacheAttached(PXCache sender) { }
		
		[PXDBString(PMRateTable.rateTableID.Length, IsUnicode = true)]
		[PXUIField(DisplayName = "Rate Table")]
		[PXSelector(typeof(PMRateTable.rateTableID), DescriptionField = typeof(PMRateTable.description))]
		protected virtual void PMTask_RateTableID_CacheAttached(PXCache sender) { }

		#endregion

		#region PMBudget
		
        [PXDefault]
        [AccountGroupAttribute(IsKey=true)]
        protected virtual void PMBudget_AccountGroupID_CacheAttached(PXCache sender)
        {
        }

        [PXDefault(typeof(PMTask.projectID))]
        [PXDBInt(IsKey = true)]
        protected virtual void PMBudget_ProjectID_CacheAttached(PXCache sender)
        {
        }

        [PXDBLiteDefault(typeof(PMTask.taskID))]
        [PXDBInt(IsKey = true)]
        [PXParent(typeof(Select<PMTask, Where<PMTask.projectID, Equal<Current<PM.PMBudget.projectID>>, And<PMTask.taskID, Equal<Current<PM.PMBudget.projectTaskID>>>>>))]
        protected virtual void PMBudget_ProjectTaskID_CacheAttached(PXCache sender)
        {
        }
		       
       
        [PXDBBool]
        [PXDefault(true)]
        protected virtual void PMBudget_IsTemplate_CacheAttached(PXCache sender)
        {
        }
        #endregion

        #region PMDetail
        
        [PXDBInt]
        [PXDBDefault(typeof(PMTask.projectID))]
        protected virtual void PMDetail_ContractID_CacheAttached(PXCache sender) { }

        [PXDBInt]
        [PXDBDefault(typeof(PMTask.taskID))]
        [PXParent(typeof(Select<PMTask, Where<PMTask.projectID, Equal<Current<PMDetail.contractID>>, And<PMTask.taskID, Equal<Current<PMDetail.taskID>>>>>))]
        protected virtual void PMDetail_TaskID_CacheAttached(PXCache sender) { }

		[PXDBInt()]
		[PXDBDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void PMDetail_ContractItemID_CacheAttached(PXCache sender) { }

		[PXDefault(ResetUsageOption.Never)]
		[PXUIField(DisplayName = "Reset Usage")]
		[PXDBString(1, IsFixed = true)]
		[ResetUsageOption.ListForProjectAttribute()]
		protected virtual void PMDetail_ResetUsage_CacheAttached(PXCache sender) { }

		[PXDBString(1, IsFixed = true)]
		[PMAccountSource.RecurentList()]
		[PXDefault(PMAccountSource.Customer)]
		[PXUIField(DisplayName = "Account Source", Required = true)]
		protected virtual void PMDetail_AccountSource_CacheAttached(PXCache sender)
		{
		}
		[Account(DisplayName = "Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Account.description))]
		protected virtual void PMDetail_AccountID_CacheAttached(PXCache sender)
		{
		}
		[PMRecurentBillSubAccountMask]
		protected virtual void PMDetail_SubMask_CacheAttached(PXCache sender)
		{
		}
		[SubAccount(typeof(PMDetail.accountID), DisplayName = "Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Sub.description))]
		protected virtual void PMDetail_SubID_CacheAttached(PXCache sender)
		{
		}

        [PXDBBool]
        [PXDefault(false)]
		[PXUIField(DisplayName = "Prepayment", Visibility = PXUIVisibility.Invisible, Visible = false)]
        protected virtual void PMDetail_PrePayment_CacheAttached(PXCache sender) { }

		[PXDBInt(MinValue = 1)]
		[PXDefault(1, PersistingCheck = PXPersistingCheck.Null)]
		protected virtual void PMDetail_RevID_CacheAttached(PXCache sender) { }

		[PXDBInt(IsKey = true)]
		[PXLineNbr(typeof(PMTask.lineCtr))]
		[PXUIField(DisplayName = "Line Nbr.", Visible = false)]
		protected virtual void PMDetail_LineNbr_CacheAttached(PXCache sender) { }
        #endregion
        #endregion

		#region Views/Selects

		public PXSelectJoin<PMTask, LeftJoin<PMProject, On<PMTask.projectID, Equal<PMProject.contractID>>>, Where<PMProject.nonProject, Equal<True>>> Task;
		public PXSelect<PMTask, Where<PMTask.taskID, Equal<Current<PMTask.taskID>>>> TaskProperties;
        public PXSelect<PMDetail,
            Where<PMDetail.contractID, Equal<Current<PMTask.projectID>>,
            And<PMDetail.taskID, Equal<Current<PMTask.taskID>>>>> BillingItems;
        
        public PXSelectJoin<PMBudget, LeftJoin<PMAccountGroup, On<PMBudget.accountGroupID, Equal<PMAccountGroup.groupID>>>, 
            Where<PMBudget.projectID, Equal<Current<PMTask.projectID>>, 
            And<PMBudget.projectTaskID, Equal<Current<PMTask.taskID>>>>> Budget;

		[PXViewName(Messages.TaskAnswers)]
		public CRAttributeList<PMTask> Answers;
		public PXSetup<PMSetup> Setup;
		public PXSetup<Company> CompanySetup;
        #endregion

        #region	Actions/Buttons

        public PXSave<PMTask> Save;
		public PXCancel<PMTask> Cancel;
		public PXInsert<PMTask> Insert;
		public PXDelete<PMTask> Delete;
		public PXFirst<PMTask> First;
		public PXPrevious<PMTask> previous;
		public PXNext<PMTask> next;
		public PXLast<PMTask> Last;
						
        #endregion		 

		public TemplateGlobalTaskMaint()
		{
			if (Setup.Current == null)
			{
				throw new PXException(Messages.SetupNotConfigured);
			}
		}

		#region Event Handlers

		protected virtual void _(Events.FieldDefaulting<PMBudget, PMBudget.costCodeID> e)
		{
			if (!CostCodeAttribute.UseCostCode())
			{
				e.NewValue = CostCodeAttribute.GetDefaultCostCode();
			}
		}

		protected virtual void _(Events.FieldUpdated<PMBudget, PMBudget.accountGroupID> e)
		{
			PMAccountGroup ag = PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Required<PMAccountGroup.groupID>>>>.Select(this, e.Row.AccountGroupID);
			if (ag != null)
			{
				if (ag.IsExpense == true)
				{
					e.Row.Type = GL.AccountType.Expense;
				}
				else
				{
					e.Row.Type = ag.Type;
				}
			}
		}

		protected virtual void _(Events.RowSelected<PMTask> e)
		{
			if (e.Row != null)
			{
				PMProject prj = PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>.SelectSingleBound(this, new object[] { e.Row });
                PXUIFieldAttribute.SetEnabled<PMTask.autoIncludeInPrj>(e.Cache, e.Row, prj != null && prj.NonProject != true);
				PXUIFieldAttribute.SetVisible<PMBudget.inventoryID>(Budget.Cache, null, !CostCodeAttribute.UseCostCode());
				PXUIFieldAttribute.SetVisibility<PMBudget.inventoryID>(Budget.Cache, null, !CostCodeAttribute.UseCostCode() ? PXUIVisibility.Visible : PXUIVisibility.Invisible);

			}
		}

		protected virtual void _(Events.RowSelected<PMBudget> e)
		{
			if (e.Row != null)
			{
				PXUIFieldAttribute.SetEnabled<PMBudget.isProduction>(e.Cache, e.Row, e.Row.Type == GL.AccountType.Expense);
			}
		}

        protected virtual void PMTask_CustomerID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
        {
            PMTask row = e.Row as PMTask;
            if(row == null) return;

            PMProject prj = PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>.SelectSingleBound(this, new object[] { row });
            if(prj != null && prj.NonProject == true)
            {
                e.Cancel = true;
            }
        }

        
		protected virtual void PMTask_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			PMTask row = e.Row as PMTask;
			if (row != null)
			{
				sender.SetDefaultExt<PMTask.customerID>(e.Row);
			}
		}

		
        protected virtual void PMDetail_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
        {
            PMDetail row = e.Row as PMDetail;
            if (row != null)
            {
                row.ContractID = Task.Current.ProjectID;

                #region Check for Uniqueness
                if (row.InventoryID.HasValue)
                {
                    PMDetail item = PXSelect<PMDetail,
                        Where<PMDetail.inventoryID, Equal<Required<InventoryItem.inventoryID>>,
                        And<PMDetail.contractID, Equal<Current<PMTask.projectID>>,
                        And<PMDetail.taskID, Equal<Current<PMTask.taskID>>>>>>.SelectWindowed(this, 0, 1, row.InventoryID);

                    if (item != null && item.PMDetailID != row.PMDetailID)
                    {
                        sender.RaiseExceptionHandling<PMDetail.inventoryID>(row, row.InventoryID, new PXException(CT.Messages.ItemNotUnique));
                        e.Cancel = true;
                    }
                }
                #endregion
            }
        }

        protected virtual void PMDetail_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
        {
            PMDetail row = e.Row as PMDetail;
            if (row != null)
            {
                #region Check for Uniqueness
                if (row.InventoryID.HasValue)
                {
                    PMDetail item = PXSelect<PMDetail,
                        Where<PMDetail.inventoryID, Equal<Required<InventoryItem.inventoryID>>,
                        And<PMDetail.contractID, Equal<Current<PMTask.projectID>>,
                        And<PMDetail.taskID, Equal<Current<PMTask.taskID>>>>>>.SelectWindowed(this, 0, 1, row.InventoryID);

                    if (item != null && item.PMDetailID != row.PMDetailID)
                    {
                        sender.RaiseExceptionHandling<PMDetail.inventoryID>(row, row.InventoryID, new PXException(CT.Messages.ItemNotUnique));
                        e.Cancel = true;
                    }
                }
                #endregion
            }
        }

        protected virtual void PMDetail_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            OnPMDetailInventoryIDFieldUpdated(sender, e);
        }

		protected virtual void OnPMDetailInventoryIDFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			PMDetail row = (PMDetail)e.Row;
			if (row == null) return;

			InventoryItem item = PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Select(this, row.InventoryID);
			if (item != null)
			{
				row.UOM = item.SalesUnit;
				row.ItemFee = item.BasePrice;
				row.Description = item.Descr;
				PMProject project = PXSelect<PMProject,
					Where<PMProject.contractID, Equal<Required<PMDetail.contractID>>>>.Select(this, row.ContractID);

				if (project != null && project.CustomerID != null)
				{
					AR.Customer customer = PXSelectReadonly<AR.Customer, Where<AR.Customer.bAccountID, Equal<Required<AR.Customer.bAccountID>>>>.Select(this, project.CustomerID);
					if (!string.IsNullOrEmpty(customer?.LocaleName))
					{
						row.Description = PXDBLocalizableStringAttribute.GetTranslation(Caches[typeof(InventoryItem)], item, nameof(InventoryItem.Descr), customer.LocaleName);
					}
				}
				sender.SetDefaultExt<PMDetail.curyItemFee>(e.Row);
			}
		}

        protected virtual void PMDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
        {
            PMDetail row = e.Row as PMDetail;
            if (row != null && Task.Current != null)
            {
                PXUIFieldAttribute.SetEnabled<PMDetail.included>(sender, row, Task.Current.IsActive != true);
			}
        }

        #endregion
    }
}