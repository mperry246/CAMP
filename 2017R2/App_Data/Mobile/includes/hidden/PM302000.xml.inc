<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen DisplayName="Project Tasks" Icon="system://Network" Id="PM302000" Type="SimpleScreen" Visible="false" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

    <sm:Container ContainerActionsToExpand="1" FieldsToShow="4" FormActionsToExpand="0" ListActionsToExpand="0" Name="TaskSummary">
        <sm:Attachments Name="TaskSummaryAttachments" Disabled="true"/>

        <sm:Field FormPriority="299" ListPriority="99" Name="ProjectID" SelectorDisplayFormat="Key" />
        <sm:Field FormPriority="298" ListPriority="97" Name="TaskID" ForceType="String" />
        <sm:Field FormPriority="296" ListPriority="96" Name="Status" />
        <sm:Field FormPriority="295" ListPriority="95" Name="Description"/>
        
        <sm:Group Name="SummaryGroup" Collapsable="true" Collapsed="true" DisplayName="Project Task Summary" FormPriority="199">
            <sm:Field Container="SummaryTaskProperties" Name="PlannedStartDate"/>
            <sm:Field Container="SummaryTaskProperties" Name="PlannedEndDate"/>
            <sm:Field Container="SummaryTaskProperties" Name="StartDate"/>
            <sm:Field Container="SummaryTaskProperties" Name="EndDate"/>
            <sm:Field Container="SummaryTaskProperties" Name="CompletionMethod"/>
            <sm:Field Container="SummaryTaskProperties" Name="Completed"/>
            <sm:Field Container="SummaryTaskProperties" Name="Approver"/>
            <sm:Field Container="SummaryDefaultValues" Name="Branch" />
            <sm:Field Name="Customer" SelectorDisplayFormat="Key" />
            <sm:Field Container="SummaryBillingAndAllocationSettings" Name="Location"/>
        </sm:Group>
        <sm:Group Name="SettingsGroup" Collapsable="true" Collapsed="true" DisplayName="Project Task Settings" FormPriority="199">
            <sm:Field FormPriority="295" Container="SummaryBillingAndAllocationSettings" Name="RateTable"/>
            <sm:Field FormPriority="294" Container="SummaryBillingAndAllocationSettings" Name="AllocationRule"/>
            <sm:Field FormPriority="293" Container="SummaryBillingAndAllocationSettings" Name="BillingRule"/>
            <sm:Field Container="SummaryBillingAndAllocationSettings" Name="BillingOption"/>
        </sm:Group>

        
        <sm:ContainerLink Name="AttributesLink" Container="Attributes" Control="ListItem" FormPriority="98"/>

        <sm:Action Behavior="Create" Context="Container" Icon="system://Plus" Name="Insert"/>
        <sm:Action Behavior="Delete" Context="Selection" Icon="system://Trash" Name="Delete"/>

        <sm:Action After="Close" Behavior="Save" Context="Record" Name="Save"/>
        <sm:Action Behavior="Cancel" Context="Record" Name="Cancel"/>
    </sm:Container>

    <sm:Container Attributes="true" Name="Attributes"/>

</sm:Screen>
