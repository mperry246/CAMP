<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen DisplayName="Expense Receipt" Id="EP301020" OpenAs="Form" Type="SimpleScreen" Visible="false" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
    <sm:Container FormActionsToExpand="1" Name="ClaimDetails">
        <sm:Field ForceIsDisabled="true" Name="ReceiptID"/>
        <sm:Field ForceIsDisabled="true" Name="Status"/>
      
        <sm:Field Name="Date"  />
        <sm:Field Name="Description" Container="ReceiptDetailsExpenseDetails" />
        <sm:Field Name="ExpenseItem" PickerType="Attached" />
        <sm:Field Name="Currency" PickerType="Attached" Container="ReceiptDetailsExpenseDetails" />
        <sm:Field Name="Amount" Container="ReceiptDetailsExpenseDetails" />
        <sm:Field Name="TaxTotal" />
		<sm:Field Name="Quantity" Container="ReceiptDetailsExpenseDetails" />
		<sm:Field Name="UnitCost" Container="ReceiptDetailsExpenseDetails" />
        <sm:Field Name="ClaimAmount" />
      
        <sm:Field Container="ReceiptDetailsExpenseDetails" Name="ProjectContract" SelectorDisplayFormat="KeyDescription">
            <sm:SelectorContainer Name="1">
                <sm:Field Name="ContractID"/>
                <sm:Field Name="Description"/>
                <sm:Field Name="Customer"/>
                <sm:Field Name="Status"/>
            </sm:SelectorContainer>
        </sm:Field>
        <sm:Field Container="ReceiptDetailsExpenseDetails" Name="ProjectTask" SelectorDisplayFormat="KeyDescription">
            <sm:SelectorContainer Name="2">
                <sm:Field Name="TaskID"/>
                <sm:Field Name="Description"/>
                <sm:Field Name="Status"/>
            </sm:SelectorContainer>
        </sm:Field>
        <sm:Group Name="3" Collapsable="true" Collapsed="true" DisplayName="Details" Field="EmployeePart">
            <sm:Field Container="ReceiptDetailsExpenseDetails" Name="ExpenseClaim" SelectorDisplayFormat="KeyDescription" Special="AllowEdit">
                <sm:SelectorContainer Name="4">
                    <sm:Field Name="ReferenceNbr"/>
                    <sm:Field Name="Description"/>
                    <sm:Field Name="Date"/>
                    <sm:Field Name="ClaimTotal"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:Field Name="EmployeePart" Container="ReceiptDetailsExpenseDetails"/>
            <sm:Field Container="ReceiptDetailsFinancialDetails" Name="Billable"/>
            <sm:Field Container="ReceiptDetailsFinancialDetails" Name="Customer" SelectorDisplayFormat="KeyDescription">
                <sm:SelectorContainer Name="5">
                    <sm:Field Name="CustomerID"/>
                    <sm:Field Name="CustomerName"/>
                    <sm:Field Name="AddressLine1"/>
                    <sm:Field Name="AddressLine2"/>
                    <sm:Field Name="PostalCode"/>
                    <sm:Field Name="Phone1"/>
                    <sm:Field Name="City"/>
                    <sm:Field Name="Country"/>
                    <sm:Field Name="TaxRegistrationID"/>
                    <sm:Field Name="CurrencyID"/>
                    <sm:Field Name="Attention"/>
                    <sm:Field Name="CustomerClass"/>
                    <sm:Field Name="Status"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:Field Container="ReceiptDetailsFinancialDetails" Name="Location">
                <sm:SelectorContainer Name="6">
                    <sm:Field Name="LocationID"/>
                    <sm:Field Name="LocationName"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:Field Container="ReceiptDetailsTaxInfo" Name="TaxCategory" PickerType="Attached" SelectorDisplayFormat="KeyDescription"/>
            <sm:Field Container="ReceiptDetailsTaxInfo" Name="TaxZone" PickerType="Attached" SelectorDisplayFormat="KeyDescription"/>
            <sm:Field Container="ReceiptDetailsTaxInfo" Name="TaxCalculationMode" PickerType="Attached" SelectorDisplayFormat="KeyDescription"/>
        </sm:Group>
		
        <sm:Group Name="7" Collapsable="true" Collapsed="true" DisplayName="Note">
            <sm:Field Name="NoteText" TextType="PlainMultiLine"/>
        </sm:Group>
        <sm:Action After="Close" Behavior="Save" Context="Record" Name="Save"/>
        <sm:Action Behavior="Cancel" Context="Record" Name="Cancel"/>

        <sm:Attachments Name="8" ImageAdjustmentPreset="Receipt" />
    </sm:Container>
	<sm:Container ListActionsToExpand="1" Name="TaxDetails">
			<sm:Field Name="TaxID"/>
			<sm:Field Name="TaxRate"/>
			<sm:Field Name="TaxableAmount"/>
			<sm:Field Name="TaxAmount"/>

			<sm:Action Behavior="Delete" Context="Selection" Icon="system://Trash" Name="Delete"/>
		</sm:Container>
</sm:Screen>
