<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen DisplayName="Sales Orders" Icon="system://Cash" Id="SO301000" ExpandSelector="OrderType" Type="SimpleScreen" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <sm:Container Name="OrderSummary" FieldsToShow="7" ContainerActionsToExpand="1" ListActionsToExpand="2" DisplayName="Document Summary" UseOptimizedExport="true">
    <sm:Field Name="OrderType" FormPriority="399" />
    <sm:Field Name="OrderNbr" ListPriority="99" FormPriority="398" ForceIsDisabled="true"/>
    <sm:Field Name="Hold" FormPriority="397"/>
    <sm:Field Name="Status" ListPriority="98" FormPriority="396"/>
    <sm:Field Name="Approved" FormPriority="395" ForceIsDisabled="true"/>
    <sm:Field Name="Date" ListPriority="97" FormPriority="394"/>
    <sm:Field Name="RequestedOn" FormPriority="393"/>
    <sm:Field Name="CustomerOrder" FormPriority="392"/>
    <sm:Field Name="Customer" ListPriority="96" FormPriority="391"/>
    <sm:Field Name="DestinationWarehouse" FormPriority="390"/>
    <sm:Field Name="Location" ListPriority="95" FormPriority="389"/>
    <sm:Field Name="Currency" ListPriority="94" FormPriority="388"/>
    <sm:Field Name="CreditHold" FormPriority="387"/>
    <sm:Field Name="Project" FormPriority="386"/>
    <sm:Field Name="Description" FormPriority="385"/>
    <sm:Field Name="OrderedQty" FormPriority="384"/>
    <sm:Field Name="VATExemptTotal" FormPriority="383"/>
    <sm:Field Name="VATTaxableTotal" FormPriority="382"/>
    <sm:Field Name="TaxTotal" FormPriority="381"/>
    <sm:Field Name="OrderTotal" ListPriority="93" FormPriority="380"/>
    <sm:Field Name="ControlTotal" FormPriority="379"/>

    <sm:ContainerLink Name="DocumentDetailsLink" Container="DocumentDetails" Control="ListItem" FormPriority="299"/>
    <sm:ContainerLink Name="ApprovalDetailsLink" Container="ApprovalDetails" Control="ListItem" FormPriority="298"/>
    <sm:ContainerLink Name="TaxDetailsLink" Container="TaxDetails" Control="ListItem" FormPriority="297"/>

    <sm:Group Name="BillToInfoGroup" Collapsable="true" Collapsed="true" DisplayName="Bill-To Info" FormPriority="199">
      <sm:Field Container="FinancialSettingsBillToContact" Name="OverrideContact"/>
      <sm:Field Container="FinancialSettingsBillToContact" Name="BusinessName"/>
      <sm:Field Container="FinancialSettingsBillToContact" Name="Attention"/>
      <sm:Field Container="FinancialSettingsBillToContact" Name="Phone1"/>
      <sm:Field Container="FinancialSettingsBillToContact" Name="Email"/>
      <sm:Field Container="FinancialSettingsBillToAddress" Name="OverrideAddress"/>
      <sm:Field Container="FinancialSettingsBillToAddress" Name="AddressLine1"/>
      <sm:Field Container="FinancialSettingsBillToAddress" Name="AddressLine2"/>
      <sm:Field Container="FinancialSettingsBillToAddress" Name="City"/>
      <sm:Field Container="FinancialSettingsBillToAddress" Name="Country"/>
      <sm:Field Container="FinancialSettingsBillToAddress" Name="State"/>
      <sm:Field Container="FinancialSettingsBillToAddress" Name="PostalCode"/>
    </sm:Group>

    <sm:Group Name="FinInfoGroup" Collapsable="true" Collapsed="true" DisplayName="Financial Information" FormPriority="198">
      <sm:Field Container="FinancialSettingsFinancialInformation" Name="Branch"/>
      <sm:Field Container="FinancialSettingsFinancialInformation" Name="CustomerTaxZone"/>
      <sm:Field Container="FinancialSettingsFinancialInformation" Name="EntityUsageType"/>
      <sm:Field Container="FinancialSettingsFinancialInformation" Name="Terms"/>
      <sm:Field Container="FinancialSettingsFinancialInformation" Name="DueDate"/>
      <sm:Field Container="FinancialSettingsFinancialInformation" Name="CashDiscountDate"/>
      <sm:Field Container="FinancialSettingsFinancialInformation" Name="Owner"/>
    </sm:Group>

    <sm:Group Name="PaymentSettingsGroup" Collapsable="true" Collapsed="true" DisplayName="Payment Settings" FormPriority="197">
      <sm:Field Container="PaymentSettings" Name="PaymentMethod"/>
      <sm:Field Container="PaymentSettings" Name="CardAccountNo"/>
      <sm:Field Container="PaymentSettings" Name="CashAccount"/>
      <sm:Field Container="PaymentSettings" Name="PaymentRef"/>
    </sm:Group>

    <sm:Group Name="ShipToInfoGroup" Collapsable="true" Collapsed="true" DisplayName="Ship-To Info" FormPriority="196">
      <sm:Field Container="ShippingSettingsShipToContact" Name="OverrideContact"/>
      <sm:Field Container="ShippingSettingsShipToContact" Name="BusinessName"/>
      <sm:Field Container="ShippingSettingsShipToContact" Name="Attention"/>
      <sm:Field Container="ShippingSettingsShipToContact" Name="Phone1"/>
      <sm:Field Container="ShippingSettingsShipToContact" Name="Email"/>
      <sm:Field Container="ShippingSettingsShipToAddress" Name="OverrideAddress"/>
      <sm:Field Container="ShippingSettingsShipToAddress" Name="AddressLine1"/>
      <sm:Field Container="ShippingSettingsShipToAddress" Name="AddressLine2"/>
      <sm:Field Container="ShippingSettingsShipToAddress" Name="City"/>
      <sm:Field Container="ShippingSettingsShipToAddress" Name="Country"/>
      <sm:Field Container="ShippingSettingsShipToAddress" Name="State"/>
      <sm:Field Container="ShippingSettingsShipToAddress" Name="PostalCode"/>
    </sm:Group>

    <sm:Group Name="ShipInfoGroup" Collapsable="true" Collapsed="true" DisplayName="Shipping Information" FormPriority="195">
      <sm:Field Container="ShippingSettingsShippingInformation" Name="SchedShipment"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="ShippingRule"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="CancelBy"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="PreferredWarehouseID"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="ShipVia"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="FOBPoint"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="ShippingTerms"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="ShippingZone"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="ResidentialDelivery"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="SaturdayDelivery"/>
      <sm:Field Container="ShippingSettingsShippingInformation" Name="Insurance"/>
    </sm:Group>

    <sm:ContainerLink Name="DiscountDetailsLink" Container="DiscountDetails" Control="ListItem" FormPriority="159"/>
    <sm:ContainerLink Name="ShipmentsLink" Container="Shipments" Control="ListItem" FormPriority="158"/>
    <sm:ContainerLink Name="PaymentsLink" Container="Payments" Control="ListItem" FormPriority="157"/>

    <sm:Group Name="OrderTotalsGroup" Collapsable="true" Collapsed="true" DisplayName="Order Totals" FormPriority="156">
      <sm:Field Container="TotalsOrderTotals" Name="LineTotal"/>
      <sm:Field Container="TotalsOrderTotals" Name="MiscTotal"/>
      <sm:Field Container="TotalsOrderTotals" Name="DiscountTotal" ForceIsDisabled="true"/>
      <sm:Field Container="TotalsOrderTotals" Name="TaxTotal"/>
    </sm:Group>

    <sm:Group Name="FreightInfoGroup" Collapsable="true" Collapsed="true" DisplayName="Freight Info" FormPriority="155">
      <sm:Field Container="TotalsFreightInfo" Name="OrderWeight"/>
      <sm:Field Container="TotalsFreightInfo" Name="OrderVolume"/>
      <sm:Field Container="TotalsFreightInfo" Name="PackageWeight"/>
      <sm:Field Container="TotalsFreightInfo" Name="FreightCost"/>
      <sm:Field Container="TotalsFreightInfo" Name="Freight"/>
      <sm:Field Container="TotalsFreightInfo" Name="PremiumFreight" ForceIsDisabled="true"/>
      <sm:Field Container="TotalsFreightInfo" Name="FreightTaxCategory" ForceIsDisabled="true"/>
    </sm:Group>

    <sm:Group Name="CalcAmountsGroup" Collapsable="true" Collapsed="true" DisplayName="Calculated Amounts and Quantities" FormPriority="154">
      <sm:Field Container="TotalsCalculatedAmountsAndQuantities" Name="UnshippedQuantity"/>
      <sm:Field Container="TotalsCalculatedAmountsAndQuantities" Name="UnshippedAmount"/>
      <sm:Field Container="TotalsCalculatedAmountsAndQuantities" Name="UnbilledQuantity"/>
      <sm:Field Container="TotalsCalculatedAmountsAndQuantities" Name="UnbilledAmount"/>
      <sm:Field Container="TotalsCalculatedAmountsAndQuantities" Name="PaymentsTotal"/>
      <sm:Field Container="TotalsCalculatedAmountsAndQuantities" Name="PreAuthorizedAmount"/>
      <sm:Field Container="TotalsCalculatedAmountsAndQuantities" Name="UnpaidBalance"/>
    </sm:Group>

    <sm:Action Name="Insert" Context="Container" Behavior="Create" Icon="system://Plus"/>

    <sm:Action Behavior="Void" Context="Selection" Name="ApproveAction" SyncLongOperation="true" After="Refresh"/>
    <sm:Action Behavior="Void" Context="Selection" Name="RejectAction" SyncLongOperation="true" After="Refresh"/>
    <sm:Action Behavior="Record" Context="Record" Name="ApproveAction" SyncLongOperation="true" After="Refresh"/>
    <sm:Action Behavior="Record" Context="Record" Name="RejectAction" SyncLongOperation="true" After="Refresh"/>

    <sm:Action Behavior="Delete" Context="Selection" Icon="system://Trash" Name="Delete" DisplayName="Delete" SyncLongOperation="true"/>

    <sm:Action Behavior="Save" Context="Record" Name="Save"/>
    <sm:Action Behavior="Cancel" Context="Record" Name="Cancel"/>
  </sm:Container>

  <sm:Container Name="DocumentDetails" FieldsToShow="9" ContainerActionsToExpand="1" ListActionsToExpand="0" FormActionsToExpand="1">
    <sm:Field Name="Branch" ListPriority="99"/>
    <sm:Field Name="InventoryID" ListPriority="98"/>
    <sm:Field Name="AlternateID"/>
    <sm:Field Name="Subitem" ListPriority="97"/>
    <sm:Field Name="FreeItem"/>
    <sm:Field Name="Warehouse" ListPriority="95"/>
    <sm:Field Name="LineDescription"/>
    <sm:Field Name="UOM" ListPriority="93"/>
    <sm:Field Name="Quantity" ListPriority="94" ForceRequired="true"/>
    <sm:Field Name="QtyOnShipments"/>
    <sm:Field Name="OpenQty"/>
    <sm:Field Name="UnitPrice"/>
    <sm:Field Name="DiscountPercent"/>
    <sm:Field Name="DiscountAmount"/>
    <sm:Field Name="DiscountCode"/>
    <sm:Field Name="DiscUnitPrice"/>
    <sm:Field Name="UnbilledAmount"/>
    <sm:Field Name="ExtPrice" ListPriority="92"/>
    <sm:Field Name="RequestedOn"/>
    <sm:Field Name="ShipOn" ListPriority="96"/>
    <sm:Field Name="ShippingRule"/>
    <sm:Field Name="UndershipThreshold"/>
    <sm:Field Name="OvershipThreshold"/>
    <sm:Field Name="Completed" ListPriority="91"/>
    <sm:Field Name="MarkForPO"/>
    <sm:Field Name="POSource"/>
    <sm:Field Name="TaxCategory"/>
    <sm:Field Name="ProjectTask"/>
    <sm:Field Name="Availability" Special="StatusField" ForceIsVisible="true" />

    <sm:Action Behavior="Create" Context="Container" Name="Insert" Icon="system://Plus"/>

    <sm:Action Behavior="Delete" Context="Selection" Name="Delete" Icon="system://Trash"/>

    <sm:Action Behavior="Void" Context="Record" Name="LSSOLineBinLotSerial" Redirect="true" RedirectToContainer="Allocations$List"/>
  </sm:Container>

  <sm:Container Name="Allocations" FieldsToShow="7" ContainerActionsToExpand="1" ListActionsToExpand="0" Visible="false">
    <sm:Field Name="Subitem" ListPriority="99"/>
    <sm:Field Name="ShipOn" ListPriority="98"/>
    <sm:Field Name="Allocated" ListPriority="97"/>
    <sm:Field Name="AllocWarehouse" ListPriority="96"/>
    <sm:Field Name="Completed"/>
    <sm:Field Name="LotSerialNbr" ListPriority="95"/>
    <sm:Field Name="Quantity" ListPriority="94"/>
    <sm:Field Name="QtyOnShipments"/>
    <sm:Field Name="QtyReceived"/>
    <sm:Field Name="UOM" ListPriority="93"/>
    <sm:Field Name="RelatedDocument"/>

    <sm:Action Behavior="Create" Context="Container" Name="Insert" Icon="system://Plus"/>
    <sm:Action Behavior="Delete" Context="Selection" Name="Delete" Icon="system://Trash"/>
  </sm:Container>

  <sm:Container Name="TaxDetails" FieldsToShow="4" ContainerActionsToExpand="1" ListActionsToExpand="0">
    <sm:Field Name="TaxID"/>
    <sm:Field Name="TaxRate"/>
    <sm:Field Name="TaxableAmount"/>
    <sm:Field Name="TaxAmount"/>

    <sm:Action Behavior="Create" Context="Container" Name="Insert" Icon="system://Plus"/>
    <sm:Action Behavior="Delete" Context="Selection" Name="Delete" Icon="system://Trash"/>
  </sm:Container>

  <sm:Container Name="DiscountDetails" FieldsToShow="5" ContainerActionsToExpand="1" ListActionsToExpand="0">
    <sm:Field Name="SkipDiscount" ListPriority="99"/>
    <sm:Field Name="DiscountCode" ListPriority="98"/>
    <sm:Field Name="SequenceID" ListPriority="97"/>
    <sm:Field Name="Type" ListPriority="96"/>
    <sm:Field Name="DiscountAmt" ListPriority="95" ForceIsVisible="false"/>
    <sm:Field Name="ManualDiscount"/>
    <sm:Field Name="DiscountableAmt"/>
    <sm:Field Name="DiscountableQty"/>
    <sm:Field Name="Discount"/>
    <sm:Field Name="FreeItem"/>
    <sm:Field Name="FreeItemQty"/>

    <sm:Action Behavior="Create" Context="Container" Name="Insert" Icon="system://Plus"/>
    <sm:Action Behavior="Delete" Context="Selection" Name="Delete" Icon="system://Trash"/>
  </sm:Container>

  <sm:Container Name="Shipments" FieldsToShow="4">
    <sm:Field Name="ShipmentNbr" ListPriority="99"/>
    <sm:Field Name="ShipmentType"/>
    <sm:Field Name="Status" ListPriority="98"/>
    <sm:Field Name="ShipmentDate" ListPriority="97"/>
    <sm:Field Name="ShippedQty" ListPriority="96"/>
    <sm:Field Name="ShippedWeight"/>
    <sm:Field Name="ShippedVolume"/>
    <sm:Field Name="InvoiceType"/>
    <sm:Field Name="InvoiceNbr"/>
    <sm:Field Name="InventoryDocType"/>
    <sm:Field Name="InventoryRefNbr"/>
  </sm:Container>

  <sm:Container Name="Payments" FieldsToShow="4">
    <sm:Field Name="DocType" ListPriority="99"/>
    <sm:Field Name="ReferenceNbr" ListPriority="98"/>
    <sm:Field Name="AppliedToOrder" ListPriority="97"/>
    <sm:Field Name="TransferredToInvoice"/>
    <sm:Field Name="Balance" ListPriority="96"/>
    <sm:Field Name="Status" ForceIsDisabled="true"/>
    <sm:Field Name="PaymentRef"/>
    <sm:Field Name="PaymentMethod" ForceIsDisabled="true"/>
    <sm:Field Name="CashAccount"/>
    <sm:Field Name="PaymentAmount" ForceIsDisabled="true"/>
    <sm:Field Name="Currency"/>
  </sm:Container>

  <sm:Container Name="ApprovalDetails" FieldsToShow="4">
    <sm:Field Name="Approver" ListPriority="99"/>
    <sm:Field Name="ApproverName" ListPriority="98"/>
    <sm:Field Name="ApprovedBy"/>
    <sm:Field Name="ApprovedByName"/>
    <sm:Field Name="Date" ListPriority="97"/>
    <sm:Field Name="Status" ListPriority="96"/>
    <sm:Field Name="Workgroup"/>
  </sm:Container>
</sm:Screen>
