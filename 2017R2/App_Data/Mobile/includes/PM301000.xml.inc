<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen DisplayName="Projects" Icon="system://Network" Id="PM301000" Type="SimpleScreen" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

    <sm:Container ContainerActionsToExpand="1" FieldsToShow="4" FormActionsToExpand="0" ListActionsToExpand="0" Name="ProjectSummary" UseOptimizedExport="true">
        <sm:Attachments Name="ProjectSummaryAttachments" Disabled="true"/>
        <sm:Field ForceType="String" FormPriority="299" ListPriority="99" Name="ProjectID" DisplayName="Pr" ListDisplayFormat="CaptionValue" />
        <sm:Field FormPriority="298" ListPriority="98" Name="Customer" SelectorDisplayFormat="Key" DisplayName="Cr" ListDisplayFormat="CaptionValue" />
        <sm:Field FormPriority="296" ListPriority="97" Name="Description"/>
        <sm:Field FormPriority="295" Name="Hold"/>
        <sm:Field FormPriority="294" ListPriority="96" Name="Status"/>
		<sm:Field FormPriority="293" Name="Template"/>
        <sm:Group Name="ProjectSummaryGroup" Collapsable="true" Collapsed="true" DisplayName="Project Summary" FormPriority="199">
            <sm:Field Name="Assets"/>
            <sm:Field Name="Liabilities" />
            <sm:Field Name="Income" />
            <sm:Field Name="Expenses" />
            <sm:Field Container="SummaryProjectProperties" Name="StartDate"/>
            <sm:Field Container="SummaryProjectProperties" Name="EndDate"/>
            <sm:Field Container="SummaryProjectProperties" Name="ProjectManager"/>
        </sm:Group>

          

        <sm:Group Name="ProjectSettingsGroup" Collapsable="true" Collapsed="true" DisplayName="Project Settings" FormPriority="197">
            <sm:Field Container="SummaryBillingAndAllocationSettingsBillingPeriod" Name="BillingPeriod"/>
            <sm:Field Container="SummaryBillingAndAllocationSettingsBillingPeriod" Name="NextBillingDate"/>
            <sm:Field Container="SummaryBillingAndAllocationSettingsBillingPeriod" Name="LastBillingDate"/>
            <sm:Field Container="SummaryBillingAndAllocationSettings" Name="AllocationRule"/>
            <sm:Field Container="SummaryBillingAndAllocationSettings" Name="BillingRule"/>
            <sm:Field Container="SummaryBillingAndAllocationSettings" Name="RateTable"/>
			<sm:Field Container="SummaryBillingAndAllocationSettings" Name="Terms"/>
			<sm:Field Container="SummaryBillingAndAllocationSettings" Name="Branch" SelectorDisplayFormat="Key"/>
            <sm:Field Container="SummaryBillTo" Name="CustomerLocation" SelectorDisplayFormat="Key"/>
        </sm:Group>
		
     

        <sm:ContainerLink Name="InvoicesLink" Container="Invoices" Control="ListItem" FormPriority="100"/>
        <sm:ContainerLink Name="TasksLink" Container="Tasks" Control="ListItem" FormPriority="99"/>
        <sm:ContainerLink Name="BalancesLink" Container="Balances" Control="ListItem" FormPriority="98"/>
        <sm:ContainerLink Name="EmployeesLink" Container="Employees" Control="ListItem" FormPriority="97"/>
        <sm:ContainerLink Name="EquipmentLink" Container="Equipment" Control="ListItem" FormPriority="96"/>
        <sm:ContainerLink Name="AttributesLink" Container="Attributes" Control="ListItem" FormPriority="95"/>

        

        <sm:Action Behavior="Create" Context="Container" Icon="system://Plus" Name="Insert"/>
        <sm:Action Behavior="Delete" Context="Selection" Icon="system://Trash" Name="Delete"/>

        <sm:Action Behavior="Save" Context="Record" Name="Save"/>
        <sm:Action Behavior="Cancel" Context="Record" Name="Cancel"/>

        <sm:Action Behavior="Record" Context="Record" Name="ApproveAction"/>
        <sm:Action Behavior="Record" Context="Record" Name="RejectAction"/>
    </sm:Container>

  

     <sm:Container ContainerActionsToExpand="1" FieldsToShow="6" ListActionsToExpand="0" Name="Invoices">
        <sm:Attachments Name="InvoicesAttachments" Disabled="true"/>

        <sm:Field Name="ProFormaDate"/>
        <sm:Field Name="ProFormaReferenceNbr" ListPriority="99" DisplayName="PF.Nbr" ListDisplayFormat="CaptionValue" />
        <sm:Field Name="Description"/>
        <sm:Field Name="Status"/>
        <sm:Field Name="InvoiceTotal" ListPriority="97" DisplayName="PF.Total" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="ARDocDate" ListPriority="96" DisplayName="AR.Date" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="ARDocType"/>
        <sm:Field Name="ARReferenceNbr" ListPriority="95" DisplayName="AR.Nbr" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="ARDocStatus" ListPriority="94" DisplayName="AR.Status" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="ARDocDescription"/>
        <sm:Field Name="ARDocAmount" ListPriority="93" DisplayName="AR.Amt" ListDisplayFormat="CaptionValue"/>
  
    </sm:Container>

    <sm:Container ContainerActionsToExpand="1" FieldsToShow="4" FormActionsToExpand="1" ListActionsToExpand="1" Name="Tasks">
        <sm:Attachments Name="TasksAttachments" Disabled="true"/>

        <sm:Field ForceType="String" Name="TaskID" ListPriority="100"/>
        <sm:Field Name="Description" ListPriority="97"/>
        <sm:Field Name="StartDate"/>
        <sm:Field Name="EndDate"/>
        <sm:Field Name="RateTable"/>
        <sm:Field Name="BillingRule"/>
        <sm:Field Name="AllocationRule"/>
        <sm:Field Name="Status"  ListPriority="99"/>       
        <sm:Field Name="Completed" ListPriority="98"/>
        <sm:Field Name="Approver"/>
        <sm:Field Name="BillingOption"/>
       

        <sm:Action Behavior="Create" Context="Container" Icon="system://Plus" Name="Insert"/>
        <sm:Action Behavior="Delete" Context="Selection" Icon="system://Trash" Name="Delete"/>

        <sm:Action Behavior="Record" Context="Record" DisplayName="View" Name="ViewTask" Redirect="true"/>
    </sm:Container>

    <sm:Container Name="Balances" FieldsToShow="5" ContainerActionsToExpand="1" ListActionsToExpand="0">
    <sm:Attachments Name="BalancesAttachments" Disabled="true"/>
    <sm:Field Name="AccountGroup" ListPriority="99"/>
    <sm:Field Name="Description"  ListPriority="98"/>
    <sm:Field Name="OriginalBudgetedAmount"/>
    <sm:Field Name="CurrentBudgetedAmount" ListPriority="97" DisplayName="Bdg.Amt" ListDisplayFormat="CaptionValue"/>
    <sm:Field Name="ActualAmount" ListPriority="96" DisplayName="Act.Amt" ListDisplayFormat="CaptionValue"/>
    <sm:Field Name="CommittedAmount"/>
    <sm:Field Name="ActualOpenCommittedAmount" />
    <sm:Field Name="VarianceAmount"/>
    <sm:Field Name="Performance" ListPriority="94" DisplayName="Performance" ListDisplayFormat="CaptionValue"/>
    </sm:Container>

    <sm:Container ContainerActionsToExpand="1" FieldsToShow="3" ListActionsToExpand="0" Name="Employees">
        <sm:Attachments Name="EmployeesAttachments" Disabled="true"/>

        <sm:Field Name="EmployeeID" SelectorDisplayFormat="Key"/>
        <sm:Field Name="EmployeeName"/>
        <sm:Field Name="Department"/>

        <sm:Action Behavior="Create" Context="Container" Icon="system://Plus" Name="Insert"/>
        <sm:Action Behavior="Delete" Context="Selection" Icon="system://Trash" Name="Delete"/>
    </sm:Container>

    <sm:Container ContainerActionsToExpand="1" FieldsToShow="3" ListActionsToExpand="0" Name="Equipment">
        <sm:Attachments Name="EquipmentAttachments" Disabled="true"/>

        <sm:Field ListPriority="99" Name="EquipmentID" SelectorDisplayFormat="Key" />
        <sm:Field ListPriority="98" Name="Description"/>
        <sm:Field ListPriority="97" Name="Active" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="Run-RateItem"/>
        <sm:Field Name="RunRate"/>
        <sm:Field Name="Setup-RateItem"/>
        <sm:Field Name="SetupRate"/>
        <sm:Field Name="Suspend-RateItem"/>
        <sm:Field Name="SuspendRate"/>

        <sm:Action Behavior="Create" Context="Container" Icon="system://Plus" Name="Insert"/>
        <sm:Action Behavior="Delete" Context="Selection" Icon="system://Trash" Name="Delete"/>
    </sm:Container>

   <sm:Container Attributes="true" Name="Attributes"/>

</sm:Screen>
