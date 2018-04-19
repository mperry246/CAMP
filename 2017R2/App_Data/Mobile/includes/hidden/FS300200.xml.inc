<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen DisplayName="Appointments" Id="FS300200" OpenAs="Form" Type="SimpleScreen" Visible="false" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

    <sm:Container Name="AppointmentRecords" FormActionsToExpand="0"> 
        <sm:Layout Name="ServiceOrderTypeLine" Template="Inline">
            <sm:Field Name="ServiceOrderType" SelectorDisplayFormat="KeyDescription" Weight="2">
                <sm:SelectorContainer Name="ServiceOrderTypeSelector" FieldsToShow="2">
                    <sm:Field Name="ServiceOrderType"   ListDisplayFormat="CaptionValue"/>
                    <sm:Field Name="Description"        ListDisplayFormat="CaptionValue"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:RecordActionLink Name="ViewDirectionOnMap"/>
        </sm:Layout>       

        <sm:Layout Name="ServiceOrderNbrLine" Template="Inline">
            <sm:Field Name="ServiceOrderNbr">
                <sm:SelectorContainer Name="ServiceOrderNbrSelector" FieldsToShow="1">
                    <sm:Field Name="ServiceOrderNbr" ListDisplayFormat="CaptionValue"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:Field Name="AppointmentNbr" ForceIsDisabled="true" />
        </sm:Layout>
		
		<sm:Field Name="Status"/>
        <sm:Field Name="Description"/>

        <sm:Field Name="Customer" Container="ServiceOrderHeader" SelectorDisplayFormat="KeyDescription">
            <sm:SelectorContainer Name="CustomerSelector" FieldsToShow="2">
                <sm:Field Name="AccountID"      ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="AccountName"    ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>

        <sm:Field Name="Location" Container="ServiceOrderHeader" SelectorDisplayFormat="KeyDescription">
            <sm:SelectorContainer Name="LocationSelector">
                <sm:Field Name="LocationID"     ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="LocationName"   ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="LocationType"   ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>

        <sm:Field Name="BranchLocation" Container="ServiceOrderHeader" SelectorDisplayFormat="KeyDescription">
            <sm:SelectorContainer Name="BranchLocationSelector" FieldsToShow="2">
                <sm:Field Name="BranchLocationID"   ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="Description"        ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>      
        
        <sm:Field Name="Description" Container="Comment" DisplayName="Comment" TextType="HTML"/>

        <sm:Group Name="DateTimeGroup" DisplayName="Date And Time" Collapsable="true" Collapsed="true">
            <sm:Layout Name="SchedTimeLine" Template="Inline">
                <sm:Field Name="ScheduledStartDate" Container="ScheduledDateAndTime" Weight="1"/> 
                <sm:Field Name="ScheduledStartTime" Container="ScheduledDateAndTime" Weight="1"/>
                <sm:Field Name="ScheduledEndTime"   Container="ScheduledDateAndTime" Weight="1"/>
            </sm:Layout>
            <sm:Layout Name="ActualTimeLine" Template="Inline">
                <sm:Field Name="ActualStartDate" Container="ActualDateAndTime" Weight="1"/> 
                <sm:Field Name="ActualStartTime" Container="ActualDateAndTime" Weight="1"/>
                <sm:Field Name="ActualEndTime"   Container="ActualDateAndTime" Weight="1"/>
            </sm:Layout>
        </sm:Group>

        <sm:Group Name="AddressGroup" DisplayName="Address" Collapsable="true" Collapsed="true">
            <sm:Field Name="AddressLine1" Container="GeneralInfoCommissionServiceOrderDetail"/> 
            <sm:Field Name="AddressLine2" Container="GeneralInfoCommissionServiceOrderDetail"/>
            <sm:Layout Name="CityLine" Template="Inline">
                <sm:Field Name="City"  Container="GeneralInfoCommissionServiceOrderDetail"/>
                <sm:Field Name="State" Container="GeneralInfoCommissionServiceOrderDetail" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue">
                    <sm:SelectorContainer Name="StateSelector" FieldsToShow="2">
                        <sm:Field Name="StateID"   ListDisplayFormat="CaptionValue"/>
                        <sm:Field Name="StateName" ListDisplayFormat="CaptionValue"/>
                    </sm:SelectorContainer>
                </sm:Field>  
            </sm:Layout>

            <sm:Layout Name="CountryLine" Template="Inline">
                <sm:Field Name="Country" Container="GeneralInfoCommissionServiceOrderDetail" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue" Weight="1">
    				<sm:SelectorContainer Name="CountrySelector" FieldsToShow="2">
    					<sm:Field Name="CountryID" ListDisplayFormat="CaptionValue"/>
    					<sm:Field Name="Country"   ListDisplayFormat="CaptionValue"/>
    				</sm:SelectorContainer>
    			</sm:Field>
                <sm:Field Name="PostalCode" Container="GeneralInfoCommissionServiceOrderDetail" Weight="1"/>
                <sm:RecordActionLink Name="ViewDirectionOnMap"/>
            </sm:Layout>

            <sm:Field Name="GPSLatitudeLongitude" Container="LocationLocationCompleteLocation" Special="GpsCoords" ForceIsDisabled="false" ForceIsVisible="false"/>
        </sm:Group>

        <sm:Group Name="ContactGroup" DisplayName="Contact" Collapsable="true" Collapsed="true">
            <sm:Field Name="ContactID" Container="GeneralInfoCustomerInfoCustomerInfo" SelectorDisplayFormat="KeyDescription">
               <sm:SelectorContainer Name="ContactSelector" FieldsToShow="2">
                    <sm:Field Name="DisplayName_" ListDisplayFormat="CaptionValue"/>
                    <sm:Field Name="Email"        ListDisplayFormat="CaptionValue"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:Field Name="Email" Container="GeneralInfoServiceOrderDetailAppointmentContact" Special="EmailSend"/>
            <sm:Layout Name="PhoneLine" Template="Inline">
                <sm:Field Name="Phone1" Container="GeneralInfoServiceOrderDetailAppointmentContact" Special="PhoneCall"/>
                <sm:Field Name="Phone2" Container="GeneralInfoServiceOrderDetailAppointmentContact" Special="PhoneCall"/>
            </sm:Layout>
        </sm:Group> 
		
		<sm:Group DisplayName="Source Info" Collapsable="true" Collapsed="true">
            <sm:Layout Template="Inline">
                <sm:Field Name="ServiceContract" Container="SourceInfo"/>
                <sm:Field Name="Schedule" Container="SourceInfo"/>
            </sm:Layout>
            <sm:Field Name="RecurrenceDescription" Container="SourceInfoScheduleRecord" ForceIsDisabled="true"/> 
        </sm:Group> 

        <sm:ContainerLink Name="ServicesLink" Container="Services" Control="ListItem"/>
        <sm:ContainerLink Name="ItemLink" Container="InventoryItems" Control="ListItem"/>
        <sm:ContainerLink Name="StaffLink" Container="Staff" Control="ListItem"/>
        <sm:ContainerLink Name="ResourceLink" Container="ResourceEquipment" Control="ListItem"/>
        <sm:ContainerLink Name="PickupItemsLink" Container="PickupDeliveryItems" Control="ListItem"/>

        <sm:Group Name="SignatureGroup" DisplayName="Signature" Collapsable="true" Collapsed="true">
            <sm:Field Name="FullName"                       Container="Signature"/>
            <sm:Field Name="AdditionalCommentsFromCustomer" Container="Signature"/>    
            <sm:Field Name="AdditionalCommentsFromStaff"    Container="Signature"/>     
            <sm:Field Name="IThePersonAboveAgreeThatTheAppointmentHasBeenCompleted" Container="Signature"/>
            <sm:Field Name="CustomerSignature" Container="Signature" ForceIsVisible="False"/>    
        </sm:Group>   
        
        <sm:Action Name="Save"   Context="Record"    Behavior="Save" SyncLongOperation="true"/>         
        <sm:Action Name="Insert" Context="Container" Behavior="Create"/>
        <sm:Action Name="Delete" Context="Record"    Behavior="Delete"/>  

        <sm:Action Name="StartAppointmentMenuActions"         Context="Record" Behavior="Record"/> 
        <sm:Action Name="CompleteAppointmentMenuActions"      Context="Record" Behavior="Record"/>  
        <sm:Action Name="CancelAppointmentMenuActions"   	  Context="Record" Behavior="Record"/>
		<sm:Action Name="ViewDirectionOnMap"                  Context="Record" Behavior="Void" Redirect="true"/>
		<sm:Action Name="SignReport"       					  Context="Record" Behavior="SignReport" DisplayName="Sign Report" />
		<sm:Action Name="PrintAppointmentReportReportsMenu"   Context="Record" Behavior="Record" Redirect="true" DisplayName="Preview Report"/> 
		<sm:Action Name="EmailSignedAppointmentMenuActions"   Context="Record" Behavior="Record"/>
    </sm:Container>

    <sm:Container Name="Services" FieldsToShow="10" FormActionsToExpand="1">
        <sm:Layout Name="LineRefLine" Template="Inline">
            <sm:Field Name="LineRef" ListDisplayFormat="CaptionValue" Weight="1" ListPriority="97">
             <sm:SelectorContainer Name="LineRefSelector" FieldsToShow="2">
                <sm:Field Name="LineRef" ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>
            <sm:Field Name="Status" ListDisplayFormat="CaptionValue" Weight="1"/>
            <sm:Field Name="LineType" ListDisplayFormat="CaptionValue" Weight="1" ListPriority="99"/>
        </sm:Layout>

		<sm:Field Name="ServiceID" SelectorDisplayFormat="Key" ListDisplayFormat="CaptionValue" Weight="1" ListPriority="95">
			<sm:SelectorContainer Name="ServiceSelector" FieldsToShow="2">
				<sm:Field Name="ServiceID"/>
				<sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
			</sm:SelectorContainer>
		</sm:Field>
		<sm:Field Name="Description" ListDisplayFormat="CaptionValue" Weight="2" ListPriority="93"/>

		<sm:Layout Template="Inline">
			<sm:Field Name="TargetEquipmentID" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue" ListPriority="91"/>
			<sm:Field Name="ModelEquipmentLineNbr"/>
		</sm:Layout> 
		
		<sm:Layout Template="Inline">
			<sm:Field Name="ComponentID"/>
			<sm:Field Name="ComponentLineRef"/>
		</sm:Layout> 
		
		<sm:Field Name="StaffMemberID"     SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue"/>

        <sm:Layout Name="QuantityLine" Template="Inline">
            <sm:Field Name="EstimatedDuration" ListDisplayFormat="CaptionValue" ListPriority="96"/>
            <sm:Field Name="Quantity"          ListDisplayFormat="CaptionValue" ListPriority="98" DisplayName="Qty"/>
        </sm:Layout>

        <sm:Layout Name="SvcActualTimeLine" Template="Inline">
    		<sm:Field Name="ActualStartTime"  ListDisplayFormat="CaptionValue" Weight="1" ListPriority="94"/>
    		<sm:Field Name="ActualEndTime"    ListDisplayFormat="CaptionValue" Weight="1" ListPriority="92"/>
    		<sm:Field Name="ActualDuration"   ListDisplayFormat="CaptionValue" Weight="1" ListPriority="90"/>
        </sm:Layout>
 
		<sm:Layout Name="WarantyLine" Template="Inline">
            <sm:Field Name="Warranty"/>
            <sm:Field Name="Billable"/> 
        </sm:Layout>    
	
        <sm:Layout Name="UnitPriceLine" Template="Inline">
            <sm:Field Name="UnitPrice"/>
            <sm:Field Name="TransactionAmount"/> 
        </sm:Layout>      

		<sm:Field Name="ProjectTask" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue">
            <sm:SelectorContainer Name="ProjTaskSelector" FieldsToShow="2">
                <sm:Field Name="TaskID"      ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>

        <sm:Field Name="NoteText"/>

        <sm:Action Name="Insert" Context="Container" Behavior="Create"/>        
        <sm:Action Name="Delete" Context="Selection" Behavior="Delete"/>
		<sm:Action Name="StartService" Context="Selection" Behavior="Void"/> 
		<sm:Action Name="CompleteService" Context="Selection" Behavior="Void"/> 
        <sm:Action Name="StartService" Context="Record" Behavior="Void"/> 
        <sm:Action Name="CompleteService" Context="Record" Behavior="Void"/> 		
    </sm:Container>     

    <sm:Container Name="InventoryItems" FieldsToShow="7" FormActionsToExpand="1" >
        <sm:Layout Name="InvLineRefLine" Template="Inline">
            <sm:Field Name="LineRef"  ListDisplayFormat="CaptionValue" Weight="1" ListPriority="97"/>
            <sm:Field Name="Status"   ListDisplayFormat="CaptionValue" Weight="1"/>
            <sm:Field Name="LineType" ListDisplayFormat="CaptionValue" Weight="1" ListPriority="99"/>
        </sm:Layout>      

		<sm:Field Name="InventoryID" SelectorDisplayFormat="Key" ListDisplayFormat="CaptionValue" ListPriority="95">
			<sm:SelectorContainer Name="InvItemSelector" FieldsToShow="2">
				<sm:Field Name="InventoryID" ListDisplayFormat="CaptionValue"/>
				<sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
			</sm:SelectorContainer>
		</sm:Field>
		
		<sm:Field Name="Description" ListDisplayFormat="CaptionValue" Weight="2" ListPriority="93"/>
	
		<sm:Field Name="EquipmentAction"/>
        
        <sm:Layout Template="Inline">
			<sm:Field Name="TargetEquipmentID" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue" DisplayName="Target Equipment" ListPriority="94"/>
			<sm:Field Name="ModelEquipmentLineNbr"/>
		</sm:Layout> 
		
		<sm:Layout Template="Inline">
			<sm:Field Name="ComponentID"/>
			<sm:Field Name="ComponentLineRef"/>
		</sm:Layout> 
		
		<sm:Field Name="Quantity" ListDisplayFormat="CaptionValue" DisplayName="Qty" ListPriority="98"/>
      
		<sm:Layout Template="Inline">
            <sm:Field Name="Warranty"/>
            <sm:Field Name="Billable"/> 
        </sm:Layout> 

        <sm:Field Name="Warehouse" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue" ListPriority="96">
            <sm:SelectorContainer Name="WarehouseSelector" FieldsToShow="2">
                <sm:Field Name="WarehouseID" ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>

        <sm:Layout Name="InvUnitPriceLine" Template="Inline">
            <sm:Field Name="UnitPrice"/>
            <sm:Field Name="TransactionAmount"/>
            <sm:Field Name="UOM"/>
        </sm:Layout>

        <sm:Field Name="NoteText"/>        

        <sm:Action Name="Insert" Context="Container" Behavior="Create"/>        
        <sm:Action Name="Delete" Context="Selection" Behavior="Delete"/>                
    </sm:Container>      

    <sm:Container Name="Staff" FieldsToShow="7" FormActionsToExpand="1">
        <sm:Layout Name="StaffMemberLine" Template="Inline">
            <sm:Field Name="LineRef" ListDisplayFormat="CaptionValue" Weight="1"/>
            <sm:Field Name="StaffMember" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue" Weight="2" ListPriority="99">
                <sm:SelectorContainer Name="StaffMemberSelector" FieldsToShow="2">
                    <sm:Field Name="StaffMemberID"   ListDisplayFormat="CaptionValue"/>
                    <sm:Field Name="StaffMemberName" ListDisplayFormat="CaptionValue"/>
                </sm:SelectorContainer>
            </sm:Field>
        </sm:Layout>

        <sm:Layout Name="ServiceLineRefLine" Template="Inline">
            <sm:Field Name="ServiceLineRef" SelectorDisplayFormat="Key" ListDisplayFormat="CaptionValue" Weight="1" ListPriority="97">
                <sm:SelectorContainer Name="ServiceLineRefSelector" FieldsToShow="2">
                    <sm:Field Name="LineRef"   ListDisplayFormat="CaptionValue"/>
                    <sm:Field Name="ServiceID" ListDisplayFormat="CaptionValue"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:Field Name="ServiceID" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue" Weight="2" ListPriority="95"/>
        </sm:Layout>

        <sm:Layout Name="StaffActualTimeLine" Template="Inline">
            <sm:Field Name="ActualStartTime" ListDisplayFormat="CaptionValue" Weight="1" ListPriority="98"/>
            <sm:Field Name="ActualEndTime"   ListDisplayFormat="CaptionValue" Weight="1" ListPriority="96"/>
            <sm:Field Name="ActualDuration"  ListDisplayFormat="CaptionValue" Weight="1" ListPriority="94"/>
        </sm:Layout>

        <sm:Layout Name="TrackTimeLine" Template="Inline">
            <sm:Field Name="TrackTime"   ListDisplayFormat="CaptionValue"/>
            <sm:Field Name="EarningType" ListDisplayFormat="CaptionValue"/>
        </sm:Layout>
        
        <sm:Field Name="Comment" ListDisplayFormat="CaptionValue" ListPriority="93"/>

        <sm:Action Name="Insert" Context="Container" Behavior="Create"/>        
        <sm:Action Name="Delete" Context="Selection" Behavior="Delete"/>                        
    </sm:Container> 

    <sm:Container Name="ResourceEquipment" FieldsToShow="2" FormActionsToExpand="1">
        <sm:Field Name="EquipmentID" ListDisplayFormat="CaptionValue">
            <sm:SelectorContainer Name="EquipmentSelector" FieldsToShow="2">
                <sm:Field Name="EquipmentNbr" ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>

        <sm:Field Name="Comment" ListDisplayFormat="CaptionValue"/>

        <sm:Action Name="Insert" Context="Container" Behavior="Create"/>        
        <sm:Action Name="Delete" Context="Selection" Behavior="Delete"/>                        
    </sm:Container> 

    <sm:Container Name="PickupDeliveryItems" FieldsToShow="5" FormActionsToExpand="1">
        <sm:Layout Name="PickupItemsLineRefLine" Template="Inline">        
            <sm:Field Name="LineRef" SelectorDisplayFormat="Key" ListDisplayFormat="CaptionValue" ListPriority="97">
                <sm:SelectorContainer Name="PickupItemsLineRefSelector" FieldsToShow="2">
                    <sm:Field Name="LineRef"   ListDisplayFormat="CaptionValue"/>
                    <sm:Field Name="ServiceID" ListDisplayFormat="CaptionValue"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:Field Name="ServiceID"          SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue" DisplayName="Service" ListPriority="95"/> 
            <sm:Field Name="PickupDeliverItems" ListPriority="98"/> 
        </sm:Layout>
        
        <sm:Field Name="PickupDeliveryItemID" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue" DisplayName="Item" ListPriority="99">
            <sm:SelectorContainer Name="PickupItemSelector" FieldsToShow="2">
                <sm:Field Name="InventoryID" ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>

        <sm:Field Name="Quantity" DisplayName="Qty" ListPriority="96"/>
        
        <sm:Layout Name="PickupItemsUnitPriceLine" Template="Inline">
            <sm:Field Name="UnitPrice"/>
            <sm:Field Name="TransactionAmount"/>
            <sm:Field Name="UOM"/>
        </sm:Layout>

        <sm:Field Name="Warehouse" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue">
            <sm:SelectorContainer Name="PickupItemsWarehouseSelector" FieldsToShow="2">
                <sm:Field Name="WarehouseID" ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>

        <sm:Field Name="ProjectTask" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue">
            <sm:SelectorContainer Name="PickupItemsProjectTaskSelector" FieldsToShow="2">
                <sm:Field Name="TaskID"      ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>

        <sm:Field Name="NoteText"/>
        <sm:Field Name="ServiceCommands"/>

        <sm:Action Name="Insert" Context="Container" Behavior="Create"/>        
        <sm:Action Name="Delete" Context="Selection" Behavior="Delete"/>        
    </sm:Container>
</sm:Screen>