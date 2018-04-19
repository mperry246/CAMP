<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen DisplayName="Routes" Id="FS304000" OpenAs="Form" Type="SimpleScreen" Visible="false" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

    <sm:Container Name="RouteRecords" FormActionsToExpand="0" >
        <sm:Layout Name="RouteNbrLine" Template="Inline">        
            <sm:Field Name="RouteNbr">
                <sm:SelectorContainer Name="RouteNbrSelector" FieldsToShow="2">
                    <sm:Field Name="RouteNbr" ListDisplayFormat="CaptionValue"/>
                    <sm:Field Name="Date"     ListDisplayFormat="CaptionValue"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:Field Name="TripNbr"/>
        </sm:Layout>     

        <sm:Layout Name="RouteLine" Template="Inline">
            <sm:Field Name="Route" SelectorDisplayFormat="KeyDescription">
                <sm:SelectorContainer Name="RouteSelector" FieldsToShow="2">
                    <sm:Field Name="RouteID"     ListDisplayFormat="CaptionValue"/>
                    <sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
                </sm:SelectorContainer>
            </sm:Field>
            <sm:Field Name="Status"/>
        </sm:Layout>

        <sm:Layout Name="DateLine" Template="Inline">
            <sm:Field Name="Date"/>
            <sm:Field Name="StartTime"/>
        </sm:Layout>

		<sm:Field Name="Driver" SelectorDisplayFormat="KeyDescription">
			<sm:SelectorContainer Name="DriverSelector" FieldsToShow="2">
				<sm:Field Name="DriverID"   ListDisplayFormat="CaptionValue"/>
				<sm:Field Name="DriverName" ListDisplayFormat="CaptionValue"/>
			</sm:SelectorContainer>
		</sm:Field>
		
		<sm:Field Name="AdditionalDriver" SelectorDisplayFormat="KeyDescription">
			<sm:SelectorContainer Name="AdditionalDriverSelector" FieldsToShow="2">
				<sm:Field Name="DriverID"   ListDisplayFormat="CaptionValue"/>
				<sm:Field Name="DriverName" ListDisplayFormat="CaptionValue"/>
			</sm:SelectorContainer>
		</sm:Field>

        <sm:Field Name="Vehicle" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue">
            <sm:SelectorContainer Name="VehicleSelector" FieldsToShow="2">
                <sm:Field Name="VehicleID" ListDisplayFormat="CaptionValue"/>
                <sm:Field Name="Description" ListDisplayFormat="CaptionValue"/>
            </sm:SelectorContainer>
        </sm:Field>

        <sm:Group Name="ActualTimeGroup" DisplayName="Actual Time" Collapsable="true" Collapsed="true">
            <sm:Layout Name="ActualTimeLine" Template="Inline">
                <sm:Field Name="ActualStartTime" Container="ActualTime" Weight="1"/>
                <sm:Field Name="ActualEndTime"   Container="ActualTime" Weight="1"/>
                <sm:Field Name="ActualDuration"  Container="ActualTime" Weight="1"/> 
            </sm:Layout>
        </sm:Group>

        <sm:Group DisplayName="Additional Info" Collapsable="true" Collapsed="true"  Name="AdditionalInfo">
            <sm:Layout Name="MilesLine" Template="Inline">
                <sm:Field Name="Miles"  Container="AdditionalInfo" Weight="2"/>
                <sm:Field Name="Weight" Container="AdditionalInfo" Weight="1"/>
            </sm:Layout>

            <sm:Layout Name="FuelLine" Template="Inline">
                <sm:Field Name="FuelQty" Container="AdditionalInfo"/>
                <sm:Field Name="FuelType" Container="AdditionalInfo"/>
                <sm:Field Name="Oil" Container="AdditionalInfo"/>
            </sm:Layout>

            <sm:Layout Name="AntiFreezeLine" Template="Inline">
                <sm:Field Name="AntiFreeze" Container="AdditionalInfo"/>
                <sm:Field Name="DEF" Container="AdditionalInfo"/>
                <sm:Field Name="Propane"  Container="AdditionalInfo"/>
            </sm:Layout>

            <sm:Field Name="GPSLatitudeLongitude" Container="LocationLocationCompleteLocation" Special="GpsCoords" ForceIsDisabled="false" ForceIsVisible="false"/>
        </sm:Group>

        <sm:Action Name="Save"   Context="Record"    Behavior="Save"/>
        <sm:Action Name="Insert" Context="Container" Behavior="Create"/>
        <sm:Action Name="Delete" Context="Record" Behavior="Delete"/>     

		<sm:Action Name="StartRouteActionsMenu"     Context="Record" Behavior="Record"/> 
        <sm:Action Name="CompleteRouteActionsMenu"  Context="Record" Behavior="Record"/>  
        <sm:Action Name="CancelRouteActionsMenu"    Context="Record" Behavior="Record"/>   
		<sm:Action Name="ReopenRouteActionsMenu"    Context="Record" Behavior="Record"/>  
        <sm:Action Name="UncloseRouteActionsMenu"   Context="Record" Behavior="Record"/>
    </sm:Container>

    <sm:Container FieldsToShow="8" Name="Appointments">
		<sm:Field Name="AppointmentNbr" DisplayName="App. Nbr." ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="AddressLine1" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="Status" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="City" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="CustomerID" DisplayName="Cust. ID" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="State" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="LocationID" DisplayName="Loc. ID" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="Description" DisplayName="Descr." ListDisplayFormat="CaptionValue"/>

        <sm:Action Behavior="Open" Context="Container" Name="OpenAppointment" Redirect="true"/>
    </sm:Container>
</sm:Screen>