<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen Id="AP301000" Type="SimpleScreen" DisplayName="Bills and Adjustments" Visible="false" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	<sm:Container Name="DocumentSummary" FieldsToShow="9" ContainerActionsToExpand="1" ListActionsToExpand="0" FormActionsToExpand="0">
        <sm:Field Name="Type"/>
        <sm:Field Name="ReferenceNbr"/>
        <sm:Field Name="Status"/>
        <sm:Field Name="Date"/>
        <sm:Field Name="VendorRef"/>
        <sm:Field Name="Description"/>
        <sm:Field Name="Vendor"/>
        <sm:Field Name="Currency"/>
		<sm:Field Name="Terms"/>
		<sm:Field Name="Amount" ForceIsVisible="true"/>
		
		<sm:Group Name="NotesGroup" Collapsable="true" Collapsed="true" DisplayName="Notes">
            <sm:Field Name="NoteText" TextType="PlainMultiLine"/>
        </sm:Group>
		
        <sm:Action Behavior="Record" Context="Record" Name="ApproveAction" SyncLongOperation="true"/>
        <sm:Action Behavior="Record" Context="Record" Name="RejectAction" SyncLongOperation="true"/>    
    </sm:Container>

	<sm:Container Name="DocumentDetails" FieldsToShow="4" ContainerActionsToExpand="1" ListActionsToExpand="0" FormActionsToExpand="0">
        <sm:Field Name="Branch"/>
		<sm:Field Name="InventoryID"/>
        <sm:Field Name="TransactionDescr" ListPriority="99"/>
		<sm:Field Name="Quantity" ListPriority="97"/>
        <sm:Field Name="UOM" ListPriority="96"/>
		<sm:Field Name="UnitCost"/>
		<sm:Field Name="DiscountAmount"/>
		<sm:Field Name="Amount" ListPriority="98"/>
		<sm:Field Name="Project"/>
		<sm:Field Name="ProjectTask"/>
		<sm:Field Name="TaxCategory"/>
		<sm:Field Name="PONumber"/> <!--hyperlink that navigates to the Purchase Orders screen-->
		<sm:Field Name="POReceiptNbr"/> <!--hyperlink that navigates to the Purchase Orders screen-->
    </sm:Container>

	<sm:Container Name="TaxDetails" FieldsToShow="4" ContainerActionsToExpand="1" ListActionsToExpand="0" FormActionsToExpand="0">
        <sm:Field Name="TaxID"/>
        <sm:Field Name="TaxRate"/>
        <sm:Field Name="TaxableAmount"/>
        <sm:Field Name="TaxAmount"/>
    </sm:Container>

	<sm:Container Name="ApprovalDetails" FieldsToShow="4">
        <sm:Attachments Name="ApprovalDetailsAttachments" Disabled="true"/>
        
		<sm:Field Name="Approver" ListPriority="99"/>
		<sm:Field Name="ApproverName" ListPriority="98"/>
		<sm:Field Name="ApprovedByName"/>
		<sm:Field Name="Date" ListPriority="97"/>
		<sm:Field Name="Status" ListPriority="96"/>
		<sm:Field Name="Workgroup"/>
	</sm:Container>

	<sm:Container Name="DiscountDetails" FieldsToShow="4" ContainerActionsToExpand="1" ListActionsToExpand="0" FormActionsToExpand="0">
        <sm:Field Name="SkipDiscount"/>
		<sm:Field Name="DiscountCode" ListPriority="99"/>
        <sm:Field Name="SequenceID" ListPriority="97"/>
		<sm:Field Name="TypeType" ListPriority="98"/>
		<sm:Field Name="ManualDiscount"/>
        <sm:Field Name="DiscountableAmt"/>
		<sm:Field Name="DiscountPercent"/>
		<sm:Field Name="DiscountableQty"/>
		<sm:Field Name="DiscountAmt" ListPriority="96"/>
    </sm:Container>

</sm:Screen>