<?xml version="1.0" encoding="UTF-8"?>
<sm:Screen DisplayName="Contact Summary" Id="CR302000" OpenAs="Form" Type="SimpleScreen" Visible="false" xmlns:sm="http://acumatica.com/mobilesitemap" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

	<sm:Container Name="ContactSummary">
		<sm:Field Name="FirstName"/>
		<sm:Field Name="LastName"/>
		<sm:Field Name="BusinessAccount"/>
		<sm:Field Name="JobTitle"/>
		<sm:Field Container="DetailsContact" Name="Email"/>

		<sm:Field Container="DetailsContact" Name="Phone1Type" />
		<sm:Field Container="DetailsContact" Name="Phone1" />

		<sm:Field Container="DetailsContact" Name="Phone2Type"/>
		<sm:Field Container="DetailsContact" Name="Phone2"/>

		<sm:ContainerLink Name="ActivitiesLink" Container="Activities" Control="ListItem" />

		<sm:Group Name="DetailsGroup" Collapsable="true" Collapsed="true" DisplayName="Details">
			<sm:Field Name="Active"/>
			<sm:Field Name="ContactClass"/>
			<sm:Field Name="Owner" PickerType="Attached"/>
			<sm:Field Name="Workgroup" PickerType="Attached"/>
			<sm:Field Container="DetailsContact" Name="Phone3Type"/>
			<sm:Field Container="DetailsContact" Name="Phone3"/>
			<sm:Field Container="DetailsContact" Name="FaxType"/>
			<sm:Field Container="DetailsContact" Name="Fax"/>
			<sm:Field Container="DetailsContact" Name="Web"/>
		</sm:Group>

		<sm:Group Name="AddressGroup" Collapsable="true" Collapsed="true" DisplayName="Address">
			<sm:Field Container="DetailsAddressSameAsInAccount" Name="SameAsInAccount"/>
			<sm:Field Container="DetailsAddress" Name="AddressLine1"/>
			<sm:Field Container="DetailsAddress" Name="AddressLine2"/>
			<sm:Field Container="DetailsAddress" Name="City"/>
			<sm:Field Container="DetailsAddress" Name="State"/>
			<sm:Field Container="DetailsAddress" Name="PostalCode"/>
			<sm:Field Container="DetailsAddress" Name="Country"/>
		</sm:Group>

		<sm:Group Name="CRMGroup" Collapsable="true" Collapsed="true" DisplayName="CRM">
			<sm:Field Container="DetailsCRM" Name="ContactMethod"/>
			<sm:Field Container="DetailsCRM" Name="DoNotCall"/>
			<sm:Field Container="DetailsCRM" Name="DoNotFax"/>
			<sm:Field Container="DetailsCRM" Name="DoNotEmail"/>
			<sm:Field Container="DetailsCRM" Name="DoNotMail"/>
			<sm:Field Container="DetailsCRM" Name="NoMassMail"/>
			<sm:Field Container="DetailsCRM" Name="NoMarketing"/>
			<sm:Field Container="DetailsCRMActivityStatistics" Name="LastIncomingActivity" ForceIsDisabled="true"/>
			<sm:Field Container="DetailsCRMActivityStatistics" Name="LastOutgoingActivity" ForceIsDisabled="true"/>
			<sm:Field Container="DetailsCRM" Name="CompanyName"/>
			<sm:Field Container="DetailsCRM" Name="ParentAccount"/>
		</sm:Group>

		<sm:Group Name="PersonGroup" Collapsable="true" Collapsed="true" DisplayName="Person">
			<sm:Field Container="DetailsPerson" Name="LanguageLocale"/>
			<sm:Field Container="DetailsCRM" Name="DateOfBirth"/>
			<sm:Field Container="DetailsPerson" Name="DateOfBirth"/>
			<sm:Field Container="DetailsPerson" Name="Gender"/>
			<sm:Field Container="DetailsPerson" Name="MaritalStatus"/>
			<sm:Field Container="DetailsPerson" Name="SpousePartnerName"/>
		</sm:Group>

		<sm:Group Name="LeadHistoryGroup" Collapsable="true" Collapsed="true" DisplayName="Lead History">
			<sm:Field Container="AdditionalInfoLeadHistory" Name="Source"/>
			<sm:Field Container="AdditionalInfoLeadHistory" Name="SourceCampaign"/>
			<sm:Field Container="AdditionalInfoLeadHistory" Name="Status"/>
			<sm:Field Container="AdditionalInfoLeadHistory" Name="Reason"/>
			<sm:Field Container="AdditionalInfoLeadHistory" Name="ConvertedBy"/>
			<sm:Field Container="AdditionalInfoLeadHistory" Name="QualificationDate"/>
		</sm:Group>

		<sm:Group Name="AttributesGroup" Collapsable="true" Collapsed="true" DisplayName="Attributes">
			<sm:Attributes Name="ContactAttributes" From="Attributes"/>
		</sm:Group>

		<sm:Action Context="Record" Name="Save" Behavior="Save" After="Close"/>
		<sm:Action Context="Record" Name="Cancel" Behavior="Cancel"/>
	</sm:Container>

	<sm:Container FieldsToShow="3" Name="Activities">
		<sm:Field Name="Type"/>
		<sm:Field Name="StartDate"/>
		<sm:Field ForceType="String" Name="Summary"/>


		<sm:Action Behavior="Open" Context="Container" Name="ViewActivity" Redirect="true"/>

		<sm:Action Behavior="Record" Context="Container" Name="NewTask" Redirect="true"/>
		<sm:Action Behavior="Record" Context="Container" Name="NewEvent" Redirect="true"/>
		<sm:Action Behavior="Record" Context="Container" Name="NewMailActivity" Redirect="true"/>
		<sm:Action Behavior="Record" Context="Container" Name="CNewActivity" Redirect="true"/>
		<sm:Action Behavior="Record" Context="Container" Name="ENewActivity" Redirect="true"/>
		<sm:Action Behavior="Record" Context="Container" Name="MNewActivity" Redirect="true"/>
		<sm:Action Behavior="Record" Context="Container" Name="NNewActivity" Redirect="true"/>
		<sm:Action Behavior="Record" Context="Container" Name="PNewActivity" Redirect="true"/>
		<sm:Action Behavior="Record" Context="Container" Name="RNewActivity" Redirect="true"/>
		<sm:Action Behavior="Record" Context="Container" Name="WNewActivity" Redirect="true"/>
	</sm:Container>

	<sm:Container Name="Relations" FieldsToShow="4" ContainerActionsToExpand="1" ListActionsToExpand="1">
		<sm:Field Name="Role"/>
		<sm:Field Name="Type"/>
		<sm:Field Name="Document"/>
		<sm:Field Name="Contact"/>
	</sm:Container>

	<sm:Container Name="Opportunities" FieldsToShow="4" ContainerActionsToExpand="1" ListActionsToExpand="1">
		<sm:Field Name="OpportunityID"/>
		<sm:Field Name="Total"/>
		<sm:Field Name="Subject"/>
		<sm:Field Name="Stage"/>

		<sm:Action Behavior="Open" Context="Container" Name="OpportunitiesViewDetails" Redirect="true"/>
		<sm:Action Behavior="Create" Context="Container" Name="AddOpportunityAction" Redirect="true"  Icon="system://Plus"/>
	</sm:Container>

	<sm:Container Name="Cases" FieldsToShow="5" ContainerActionsToExpand="1" ListActionsToExpand="1">
		<sm:Field Name="CaseID"/>
		<sm:Field Name="Status"/>
		<sm:Field Name="ClassID"/>
		<sm:Field Name="Reason"/>
		<sm:Field Name="Subject"/>

		<sm:Action Behavior="Open" Context="Container" Name="CasesViewDetails" Redirect="true"/>
		<sm:Action Behavior="Create" Context="Container" Name="AddCaseAction" Redirect="true" Icon="system://Plus"/>
	</sm:Container>

	<sm:Container Name="Campaigns" FieldsToShow="2" ContainerActionsToExpand="1" ListActionsToExpand="1">
		<sm:Field Name="CampaignID" ListPriority="98"/>
		<sm:Field Name="CampaignName" ListPriority="99"/>		
		<sm:Action Behavior="Delete" Context="Selection" Name="Delete"/>
	</sm:Container>

	<sm:Container Name="MarketingLists" FieldsToShow="2" ContainerActionsToExpand="1" ListActionsToExpand="1">
		<sm:Field Name="Subscribed" ListDisplayFormat="CaptionValue" ListPriority="98"/>          
		<sm:Field Name="MarketingListID"/>
		<sm:Field Name="ListName" ListPriority="99"/>
		<sm:Field Name="DynamicList" ListDisplayFormat="CaptionValue"/>	
		<sm:Action Behavior="Delete" Context="Selection" Name="DeleteMarketingList"/>
	</sm:Container>
</sm:Screen>
