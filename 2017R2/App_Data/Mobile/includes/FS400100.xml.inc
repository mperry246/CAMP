<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen DisplayName="Appointments" Id="FS400100" Type="FilterListScreen" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">    
    <sm:Container Name="Selection">                  
        <sm:Field Name="FromScheduledDate" />  
		<sm:Field Name="ToScheduledDate" />  
        <sm:Field Name="StaffMember" SelectorDisplayFormat="KeyDescription"/>                 		
    </sm:Container>

    <sm:Container Name="Appointments" FieldsToShow="8" >
        <sm:Field Name="AppointmentNbr" DisplayName="App. Nbr." ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="ScheduledDate" DisplayName="Date" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="Status" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="ScheduledStartTime" DisplayName="Start Time" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="Customer" DisplayName="Cust. ID" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="State" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="Location" DisplayName="Loc. ID" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="Description" DisplayName="Descr." ListDisplayFormat="CaptionValue"/>

        <sm:Action Behavior="Open" Context="Container" Name="EditDetail" Redirect="true"/>
        <sm:Action Behavior="Create" Context="Container" Icon="system://Plus" Name="CreateNew" Redirect="true"/>
    </sm:Container>   
</sm:Screen>

