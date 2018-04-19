<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen DisplayName="Routes" Id="FS403900" Type="FilterListScreen" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">    
    <sm:Container Name="Selection">    
        <sm:Field Name="From" />       
		<sm:Field Name="To" />                       
        <sm:Field Name="Driver" SelectorDisplayFormat="KeyDescription"/> 
    </sm:Container>

    <sm:Container Name="Routes" FieldsToShow="6" >
        <sm:Field Name="RouteNbr" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="Driver" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="Status" ListDisplayFormat="CaptionValue" />
        <sm:Field Name="Date" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="RouteID" SelectorDisplayFormat="KeyDescription" ListDisplayFormat="CaptionValue"/>
        <sm:Field Name="StartTime" ListDisplayFormat="CaptionValue"/>
		
		<sm:Action Behavior="Open" Context="Container" Name="EditDetail" Redirect="true"/>
        <sm:Action Behavior="Create" Context="Container" Icon="system://Plus" Name="CreateNew" Redirect="true"/>
    </sm:Container>   
</sm:Screen>

