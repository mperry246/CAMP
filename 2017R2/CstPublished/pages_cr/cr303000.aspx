<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
	ValidateRequest="false" CodeFile="CR303000.aspx.cs" Inherits="Page_CR303000"
	Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<script type="text/javascript">
		function refreshTasksAndEvents(ds, context)
		{
			if (context.command == "Cancel")
			{
				var top = window.top;
				if (top != window && top.MainFrame != null) top.MainFrame.refreshEventsInfo();
			}
		}
	</script>
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.CR.BusinessAccountMaint"
		PrimaryView="BAccount">
		<ClientEvents CommandPerformed="refreshTasksAndEvents" />
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Cancel" PopupVisible="true" />
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" PopupVisible="true" />
			<px:PXDSCallbackCommand Name="First" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="Action" StartNewGroup="true" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="Merge" Visible="False" CommitChanges="True"/>
            <px:PXDSCallbackCommand Name="MarkAsValidated" Visible="False" />
            <px:PXDSCallbackCommand Name="CheckForDuplicates"  Visible="False" />
			<px:PXDSCallbackCommand DependOnGrid="grdContacts" Name="Contacts_ViewDetails" Visible="False" />
			<px:PXDSCallbackCommand Name="AddContact" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand DependOnGrid="grdLocations" Name="Locations_ViewDetails"
				Visible="False" />
			<px:PXDSCallbackCommand Name="AddLocation" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand DependOnGrid="grdLocations" Name="SetDefaultLocation" Visible="False"
				RepaintControlsIDs="grdLocations" />
			<px:PXDSCallbackCommand Name="AddOpportunity" CommitChanges="True" Visible="False" />
			<px:PXDSCallbackCommand Name="Opportunities_ViewDetails" Visible="False" DependOnGrid="gridOpportunities" />
            <px:PXDSCallbackCommand Name="Opportunities_BAccount_ViewDetails" Visible="False" DependOnGrid="gridOpportunities" />
			<px:PXDSCallbackCommand Name="Opportunities_Contact_ViewDetails" Visible="False" DependOnGrid="gridOpportunities" />
			<px:PXDSCallbackCommand Name="AddCase" CommitChanges="True" Visible="False" />
			<px:PXDSCallbackCommand Name="Cases_ViewDetails" Visible="False" DependOnGrid="gridCases" />
			<px:PXDSCallbackCommand DependOnGrid="grdContracts" Name="Contracts_ViewDetails"
				Visible="False" />
			<px:PXDSCallbackCommand DependOnGrid="grdContracts" Name="Contracts_Location_ViewDetails"
				Visible="False" />
			<px:PXDSCallbackCommand Name="Contracts_BAccount_ViewDetails" Visible="False" DependOnGrid="grdContracts"/>
			<px:PXDSCallbackCommand DependOnGrid="grdOrders" Name="Orders_ViewDetails" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewVendor" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewCustomer" Visible="False" />
			<px:PXDSCallbackCommand Name="ConverToCustomer" Visible="False" />
			<px:PXDSCallbackCommand Name="ConverToVendor" Visible="False" />
			<px:PXDSCallbackCommand Name="NewTask" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="NewEvent" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="NewActivity" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="NewMailActivity" DependOnGrid="grdContacts" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ViewActivity" DependOnGrid="gridActivities" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="OpenActivityOwner" Visible="False" CommitChanges="True" DependOnGrid="gridActivities" />
			<px:PXDSCallbackCommand Name="ViewDefLocationOnMap" CommitChanges="True" Visible="false" />
			<px:PXDSCallbackCommand Name="ViewMainOnMap" CommitChanges="true" Visible="false" />
			<px:PXDSCallbackCommand Name="ValidateAddresses" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="Members_CRCampaign_ViewDetails" Visible="False" CommitChanges="True"
				DependOnGrid="grdCampaignHistory" />
			<px:PXDSCallbackCommand Name="Subscriptions_CRMarketingList_ViewDetails" Visible="False"
				CommitChanges="True" DependOnGrid="grdMarketingLists" />
            <px:PXDSCallbackCommand Name="Relations_TargetDetails" Visible="False" CommitChanges="True"	DependOnGrid="grdRelations" />
			<px:PXDSCallbackCommand Name="Relations_EntityDetails" Visible="False" CommitChanges="True"	DependOnGrid="grdRelations" />
			<px:PXDSCallbackCommand Name="Relations_ContactDetails" Visible="False" CommitChanges="True" DependOnGrid="grdRelations" />
            <px:PXDSCallbackCommand Visible="false" DependOnGrid="PXGridDuplicates" Name="Duplicates_Contact_ViewDetails" />
			<px:PXDSCallbackCommand Visible="false" DependOnGrid="PXGridDuplicates" Name="ViewDuplicateAccount" />
		    <px:PXDSCallbackCommand Name="syncSalesforce" Visible="false" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXSmartPanel ID="pnlChangeID" runat="server"  Caption="Specify New ID"
        CaptionVisible="true" DesignView="Hidden" LoadOnDemand="true" Key="ChangeIDDialog" CreateOnDemand="false" AutoCallBack-Enabled="true"
        AutoCallBack-Target="formChangeID" AutoCallBack-Command="Refresh" CallBackMode-CommitChanges="True" CallBackMode-PostData="Page"
        AcceptButtonID="btnOK">
            <px:PXFormView ID="formChangeID" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" CaptionVisible="False"
                DataMember="ChangeIDDialog">
                <ContentStyle BackColor="Transparent" BorderStyle="None" />
                <Template>
                    <px:PXLayoutRule ID="rlAcctCD" runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                    <px:PXSegmentMask ID="edAcctCD" runat="server" DataField="CD" />
                </Template>
            </px:PXFormView>
            <px:PXPanel ID="pnlChangeIDButton" runat="server" SkinID="Buttons">
                <px:PXButton ID="btnOK" runat="server" DialogResult="OK" Text="OK" >
                    <AutoCallBack Target="formChangeID" Command="Save" />
                </px:PXButton>
				<px:PXButton ID="PXButton3" runat="server" DialogResult="Cancel" Text="Cancel" />						
            </px:PXPanel>
    </px:PXSmartPanel>
	<px:PXFormView ID="form" runat="server" Width="100%" Caption="Account Summary"
		DataMember="BAccount" DataSourceID="ds" NoteIndicator="True" LinkIndicator="True"
		NotifyIndicator="True" FilesIndicator="True" DefaultControlID="edAcctCD">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="SM"
				ControlSize="XM" />
			<px:PXSegmentMask ID="edAcctCD" runat="server" DataField="AcctCD" AutoRefresh="True"
				FilterByAllFields="True" />
			<px:PXTextEdit CommitChanges="True" ID="edAcctName" runat="server" DataField="AcctName" />
			<px:PXDropDown ID="edStatus" runat="server" DataField="Status" Size="S" />
			<px:PXLayoutRule ID="PXLayoutRule2" runat="server" StartColumn="True" LabelsWidth="SM"
				ControlSize="XM" />
			<px:PXSelector ID="OwnerID" runat="server" DataField="OwnerID" TextMode="Search"
				DisplayMode="Text" FilterByAllFields="True" AutoRefresh="True"/>
			<px:PXSelector CommitChanges="True" ID="edWorkgroupID" runat="server" DataField="WorkgroupID"
				TextMode="Search" DisplayMode="Text" FilterByAllFields="True" />
			<px:PXDropDown ID="edType" runat="server" DataField="Type" Enabled="False" Size="SM" />
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXTab ID="tab" runat="server" DataSourceID="ds" Height="500px" Width="100%" DataMember="CurrentBAccount">
		<Items>
			<px:PXTabItem Text="Details" RepaintOnDemand="false">
				<Template>
					<px:PXLayoutRule ID="PXLayoutRule3" runat="server" StartColumn="True" ControlSize="XM"
						LabelsWidth="SM" ></px:PXLayoutRule>
					<px:PXLayoutRule ID="PXLayoutRule4" runat="server" GroupCaption="Main Contact" StartGroup="True" ></px:PXLayoutRule>
					<px:PXFormView ID="frmDefContact" runat="server" CaptionVisible="False" DataMember="DefContact"
						DataSourceID="ds" SkinID="Transparent">
						<Template>
							<px:PXLayoutRule ID="PXLayoutRule5" runat="server" StartColumn="True" LabelsWidth="SM"
								ControlSize="XM" ></px:PXLayoutRule>
							<px:PXTextEdit ID="edFullName" runat="server" DataField="FullName" ></px:PXTextEdit>
                            <px:PXLayoutRule runat="server" Merge="True" ></px:PXLayoutRule>
                            <px:PXLabel ID="PXLabel1" runat="server" Size="SM" Text="Attention"></px:PXLabel>
							<px:PXTextEdit ID="edSalutation" runat="server" DataField="Salutation" SuppressLabel="true"></px:PXTextEdit>
                            <px:PXLayoutRule runat="server"></px:PXLayoutRule>
							<px:PXMailEdit ID="edEMail" runat="server" DataField="EMail" CommandName="NewMailActivity"
								CommandSourceID="ds" CommitChanges="True"></px:PXMailEdit>
							<px:PXLinkEdit ID="edWebSite" runat="server" DataField="WebSite" CommitChanges="True"></px:PXLinkEdit>
					        <px:PXMaskEdit ID="Phone1" runat="server" DataField="Phone1" ></px:PXMaskEdit>
					        <px:PXMaskEdit ID="Phone2" runat="server" DataField="Phone2" ></px:PXMaskEdit>
                            <px:PXMaskEdit ID="Fax" runat="server" DataField="Fax" ></px:PXMaskEdit>
							<px:PXLayoutRule ID="PXLayoutRule23" runat="server" ></px:PXLayoutRule>
                            <px:PXCheckBox ID="edDuplicateFoundFrm" runat="server" DataField="DuplicateFound" Visible="False" ></px:PXCheckBox>                            
						</Template>
						<ContentLayout OuterSpacing="None" ></ContentLayout>
						<ContentStyle BackColor="Transparent" BorderStyle="None">
						</ContentStyle>
					</px:PXFormView>
					<px:PXLayoutRule ID="PXLayoutRule6" runat="server" GroupCaption="Main Address" StartGroup="True" ></px:PXLayoutRule>
					<px:PXFormView ID="frmDefAddress" runat="server" CaptionVisible="False" DataSourceID="ds"
						DataMember="AddressCurrent" SkinID="Transparent">
						<Template>
							<px:PXLayoutRule ID="PXLayoutRule7" runat="server" StartColumn="True" LabelsWidth="SM"
								ControlSize="XM" ></px:PXLayoutRule>
							<px:PXCheckBox ID="chkIsValidated" runat="server" DataField="IsValidated" Enabled="False" ></px:PXCheckBox>
							<px:PXLayoutRule ID="PXLayoutRule18" runat="server" ></px:PXLayoutRule>
							<px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1" ></px:PXTextEdit>
							<px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2" ></px:PXTextEdit>
							<px:PXTextEdit ID="edCity" runat="server" DataField="City" ></px:PXTextEdit>
							<px:PXSelector ID="edState" runat="server" AutoRefresh="True" DataField="State" CommitChanges="true"
								FilterByAllFields="True" TextMode="Search" DisplayMode="Hint" ></px:PXSelector>
							<px:PXLayoutRule ID="PXLayoutRule15" runat="server" Merge="True" ></px:PXLayoutRule>
							<px:PXMaskEdit ID="edPostalCode" runat="server" DataField="PostalCode" Size="S" CommitChanges="True" ></px:PXMaskEdit>
							<px:PXButton ID="btnViewOnMap" runat="server" CommandName="ViewMainOnMap" CommandSourceID="ds"
								Size="xs" Text="View On Map" Height="20" ></px:PXButton>
							<px:PXLayoutRule ID="PXLayoutRule9" runat="server" ></px:PXLayoutRule>
							<px:PXSelector ID="edCountryID" runat="server" AllowEdit="True" DataField="CountryID"
								FilterByAllFields="True" TextMode="Search" DisplayMode="Hint" CommitChanges="True" ></px:PXSelector>
						</Template>
						<ContentLayout OuterSpacing="None" ></ContentLayout>
						<ContentStyle BackColor="Transparent" BorderStyle="None">
						</ContentStyle>
					</px:PXFormView>
					<px:PXLayoutRule ID="PXLayoutRule11" runat="server" StartGroup="True" GroupCaption="CRM" StartColumn="True" ControlSize="XM" LabelsWidth="M" ></px:PXLayoutRule>
					<px:PXSelector ID="ClassID" runat="server" CommitChanges="True" DataField="ClassID"
						FilterByAllFields="True" TextMode="Search" DisplayMode="Hint" ></px:PXSelector>
					<px:PXTextEdit ID="edAcctReferenceNbr" runat="server" DataField="AcctReferenceNbr" ></px:PXTextEdit>
					<px:PXSegmentMask ID="edParentBAccountID" runat="server" DataField="ParentBAccountID"
						AllowEdit="True" FilterByAllFields="True" TextMode="Search" DisplayMode="Hint" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXDropDown ID="edDuplicateStatus1" runat="server" DataField="Contact__DuplicateStatus" Enabled="False"></px:PXDropDown>                  
                    <px:PXDateTimeEdit ID="edLastIncomingDate" runat="server" DataField="BAccountActivityStatistics.LastIncomingActivityDate" Enabled="False" Size="SM"></px:PXDateTimeEdit>
                    <px:PXDateTimeEdit ID="edLastOutgoingDate" runat="server" DataField="BAccountActivityStatistics.LastOutgoingActivityDate" Enabled="False" Size="SM"></px:PXDateTimeEdit>
					<px:PXSelector ID="edCampaignSourceID" runat="server" DataField="CampaignSourceID" AllowEdit="True" TextMode="Search" DisplayMode="Hint" FilterByAllFields="True" ></px:PXSelector>
                    <px:PXSelector ID="edLanguageID" runat="server" AllowEdit="True" DataField="DefContact.LanguageID" TextMode="Search" DataSourceID="ds" DisplayMode="Hint"  ></px:PXSelector>
					<px:PXLayoutRule ID="PXLayoutRule10" runat="server" ControlSize="XM" LabelsWidth="SM"
						StartColumn="True" ></px:PXLayoutRule>
				</Template>
			</px:PXTabItem>
            <px:PXTabItem Text="Duplicates" BindingContext="frmDefContact" VisibleExp="DataControls[&quot;edDuplicateFoundFrm&quot;].Value == true" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="PXGridDuplicates" runat="server" DataSourceID="ds" SkinID="Inquire" Width="100%"
						Height="200px" MatrixMode="True">
					    <ActionBar>
							<CustomItems>
					            <px:PXToolBarButton Text="Merge" Key="cmdMerge">
                                        <AutoCallBack Command="Merge" Target="ds"></AutoCallBack>
                                        <PopupCommand Command="Cancel" Target="ds" ></PopupCommand>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Duplicates">
								<Columns>
								    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" Width="80px" AllowCheckAll="True"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Contact2__ContactType" TextAlign="Left" Width="100px" AllowShowHide="False" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Contact2__DuplicateStatus" TextAlign="Left" Width="140px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Contact2__LastModifiedDateTime" TextAlign="Left" Width="160px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Contact2__DisplayName" TextAlign="Left" Width="180px" AllowShowHide="False"  LinkCommand="Duplicates_Contact_ViewDetails" ></px:PXGridColumn>									
									<px:PXGridColumn DataField="Contact2__BAccountID" TextAlign="Left" Width="180px" LinkCommand="ViewDuplicateAccount"  ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="BAccountR__Type" TextAlign="Left" Width="80px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="BAccountR__AcctName" TextAlign="Left" Width="180px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Contact2__Email" TextAlign="Left" Width="180px" AllowShowHide="False" ></px:PXGridColumn>
								</Columns>
								<Layout FormViewHeight="" ></Layout>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="200" ></AutoSize>
						<ActionBar PagerVisible="False">
							<Actions>
								<Search Enabled="False" ></Search>
							</Actions>
						</ActionBar>
                        <Mode AllowAddNew="False" AllowColMoving="False" AllowDelete="False" ></Mode>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Attributes">
				<Template>
					<px:PXGrid ID="PXGridAnswers" runat="server" DataSourceID="ds" SkinID="Inquire" Width="100%"
						Height="200px" MatrixMode="True">
						<Levels>
							<px:PXGridLevel DataMember="Answers">
								<Columns>
									<px:PXGridColumn DataField="AttributeID" TextAlign="Left" Width="250px" AllowShowHide="False"
										TextField="AttributeID_description" ></px:PXGridColumn>
    								<px:PXGridColumn DataField="isRequired" TextAlign="Center" Type="CheckBox" Width="75px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Value" Width="300px" AllowShowHide="False" AllowSort="False" ></px:PXGridColumn>
								</Columns>
								<Layout FormViewHeight="" ></Layout>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="200" ></AutoSize>
						<ActionBar>
							<Actions>
								<Search Enabled="False" ></Search>
							</Actions>
						</ActionBar>
                        <Mode AllowAddNew="False" AllowColMoving="False" AllowDelete="False" ></Mode>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Activities" LoadOnDemand="True">
				<Template>
					<pxa:PXGridWithPreview ID="gridActivities" runat="server" DataSourceID="ds" Width="100%"
						AllowSearch="True" DataMember="Activities" AllowPaging="true" NoteField="NoteText"
						FilesField="NoteFiles" BorderWidth="0px" GridSkinID="Inquire" SplitterStyle="z-index: 100; border-top: solid 1px Gray;  border-bottom: solid 1px Gray"
						PreviewPanelStyle="z-index: 100; background-color: Window" PreviewPanelSkinID="Preview"
						BlankFilterHeader="All Activities" MatrixMode="true" PrimaryViewControlID="form">
						<ActionBar DefaultAction="cmdViewActivity" CustomItemsGroup="0" PagerVisible="False">
							<CustomItems>
								<px:PXToolBarButton Key="cmdAddTask">
									<AutoCallBack Command="NewTask" Target="ds" ></AutoCallBack>
								</px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdAddEvent">
									<AutoCallBack Command="NewEvent" Target="ds" ></AutoCallBack>
								</px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdAddEmail">
									<AutoCallBack Command="NewMailActivity" Target="ds" ></AutoCallBack>
								</px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdAddActivity">
									<AutoCallBack Command="NewActivity" Target="ds" ></AutoCallBack>
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Activities">
								<Columns>
									<px:PXGridColumn DataField="IsCompleteIcon" Width="21px" AllowShowHide="False" AllowResize="False"
										ForceExport="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="PriorityIcon" Width="21px" AllowShowHide="False" AllowResize="False"
										ForceExport="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CRReminder__ReminderIcon" Width="21px" AllowShowHide="False" AllowResize="False"
										ForceExport="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ClassIcon" Width="31px" AllowShowHide="False" AllowResize="False"
										ForceExport="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ClassInfo" Width="60px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="RefNoteID" Visible="false" AllowShowHide="False" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Subject" LinkCommand="ViewActivity" Width="297px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="UIStatus" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Released" TextAlign="Center" Type="CheckBox" Width="80px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="StartDate" DisplayFormat="g" Width="120px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="CreatedDateTime" DisplayFormat="g" Width="120px" Visible="False" ></px:PXGridColumn>
									<px:PXGridColumn DataField="TimeSpent" Width="80px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CreatedByID" Visible="false" AllowShowHide="False" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CreatedByID_Creator_Username" Visible="false"
										SyncVisible="False" SyncVisibility="False" Width="108px">
										<NavigateParams>
											<px:PXControlParam Name="PKID" ControlID="gridActivities" PropertyName="DataValues[&quot;CreatedByID&quot;]" ></px:PXControlParam>
										</NavigateParams>
									</px:PXGridColumn>
									<px:PXGridColumn DataField="WorkgroupID" Width="90px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OwnerID" LinkCommand="OpenActivityOwner" Width="150px" DisplayMode="Text"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ProjectID" Width="80px" AllowShowHide="true" Visible="false" SyncVisible="false" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ProjectTaskID" Width="80px" AllowShowHide="true" Visible="false" SyncVisible="false"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Source" Width="120" AllowResize="True" ></px:PXGridColumn>
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<PreviewPanelTemplate>
							<px:PXHtmlView ID="edBody" runat="server" DataField="body" TextMode="MultiLine"
								MaxLength="50" Width="100%" Height="100%" SkinID="Label" >
                                      <AutoSize Container="Parent" Enabled="true" ></AutoSize>
                                </px:PXHtmlView>
						</PreviewPanelTemplate>
						<AutoSize Enabled="true" ></AutoSize>
						<GridMode AllowAddNew="False" AllowDelete="False" AllowFormEdit="False" AllowUpdate="False" AllowUpload="False" ></GridMode>
					</pxa:PXGridWithPreview>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Contacts" LoadOnDemand="true">
				<Template>
					<px:PXGrid ID="grdContacts" runat="server" DataSourceID="ds" Height="522px" Width="100%"
						SkinID="Inquire" AllowSearch="True">
						<Levels>
							<px:PXGridLevel DataMember="Contacts">
								<Columns>
									<px:PXGridColumn DataField="DisplayName" Width="280px" LinkCommand="Contacts_ViewDetails" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Salutation" Width="160px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="IsActive" TextAlign="Center" Type="CheckBox" Width="60px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Address__City" Width="180px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="EMail" Width="200px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Phone1" Width="140px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="WorkgroupID" Width="108px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ContactType" Width="108px"></px:PXGridColumn>
									<px:PXGridColumn DataField="OwnerID" Width="108px" DisplayMode="Text" ></px:PXGridColumn>
								</Columns>
								<Layout FormViewHeight="" ></Layout>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" ></AutoSize>
						<ActionBar DefaultAction="cmdViewContact" PagerVisible="False">
							<CustomItems>
								<px:PXToolBarButton ImageKey="AddNew" Tooltip="Add New Contact" DisplayStyle="Image">
									<AutoCallBack Command="AddContact" Target="ds">
									</AutoCallBack>
									<PopupCommand Command="Refresh" Target="grdContacts">
									</PopupCommand>
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Contact Details" Key="cmdViewContact" Visible="False">
									<AutoCallBack Command="Contacts_ViewDetails" Target="ds">
									</AutoCallBack>
									<PopupCommand Command="Refresh" Target="grdContacts">
									</PopupCommand>
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" ></Mode>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Delivery Settings" LoadOnDemand="true">
				<Template>
					<px:PXFormView ID="frmDefLocation1" runat="server" DataMember="DefLocation" DataSourceID="ds"
						Width="100%" CaptionVisible="False" SkinID="Transparent">
						<Template>
							<px:PXLayoutRule ID="PXLayoutRule12" runat="server" StartColumn="True" LabelsWidth="SM"
								ControlSize="XM" ></px:PXLayoutRule>
							<px:PXLayoutRule ID="PXLayoutRule13" runat="server" StartGroup="True" GroupCaption="Shipping Contact" ></px:PXLayoutRule>
							<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkIsContactSameAsMain"
								runat="server" DataField="IsContactSameAsMain" ></px:PXCheckBox>
							<px:PXPanel ID="PXPanel3" runat="server" RenderStyle="Simple" RenderSimple="True">
								<px:PXFormView ID="frmDefLocationContact" runat="server" DataMember="DefLocationContact"
									DataSourceID="ds" SkinID="Transparent">
									<Template>
										<px:PXLayoutRule ID="PXLayoutRule14" runat="server" LabelsWidth="SM" ControlSize="XM"
											StartColumn="True" ></px:PXLayoutRule>
                                        <px:PXLayoutRule ID="PXLayoutRule8" runat="server" Merge="True" ></px:PXLayoutRule>
                                        <px:PXLabel ID="lblSalutation1" runat="server">Attention:</px:PXLabel>
										<px:PXTextEdit ID="edSalutation" runat="server" DataField="Salutation" SuppressLabel="True" ></px:PXTextEdit>
                                        <px:PXLayoutRule ID="PXLayoutRule22" runat="server" ></px:PXLayoutRule>
										<px:PXMailEdit ID="edEMail" runat="server" DataField="EMail" ></px:PXMailEdit>
										<px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1" ></px:PXMaskEdit>
										<px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2" ></px:PXMaskEdit>
										<px:PXMaskEdit ID="edFax" runat="server" DataField="Fax" ></px:PXMaskEdit>
									</Template>
									<ContentLayout OuterSpacing="None" ></ContentLayout>
								</px:PXFormView>
							</px:PXPanel>
							<px:PXLayoutRule ID="PXLayoutRule15" runat="server" GroupCaption="Shipping Address"
								StartGroup="True" ></px:PXLayoutRule>
							<px:PXLayoutRule ID="PXLayoutRule16" runat="server" Merge="True" ></px:PXLayoutRule>
							<px:PXPanel ID="PXPanel4" runat="server" RenderStyle="Simple" RenderSimple="True">
								<px:PXFormView ID="frmDefLocationAddress" runat="server" DataMember="DefLocationAddress"
									DataSourceID="ds" SkinID="Transparent">
									<Template>
										<px:PXLayoutRule ID="PXLayoutRule14" runat="server" Merge="True" SuppressLabel="False" ></px:PXLayoutRule>
										<px:PXFormView ID="frmDefLocationCurrent" runat="server" DataMember="DefLocationCurrent"
											DataSourceID="ds" RenderStyle="Simple">
											<Template>
												<px:PXLayoutRule ID="PXLayoutRule14" runat="server" SuppressLabel="False" LabelsWidth="SM" ></px:PXLayoutRule>
												<px:PXCheckBox ID="chkIsSameAsMaint" runat="server" DataField="IsAddressSameAsMain"
													CommitChanges="true">
												</px:PXCheckBox>
											</Template>
											<ContentStyle BackColor="Transparent"></ContentStyle>
										</px:PXFormView>
										<px:PXCheckBox ID="chkIsValidated" runat="server" DataField="IsValidated" ></px:PXCheckBox>
										<px:PXLayoutRule ID="PXLayoutRule17" runat="server" LabelsWidth="SM" ControlSize="XM" ></px:PXLayoutRule>
										<px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1" ></px:PXTextEdit>
										<px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2" ></px:PXTextEdit>
										<px:PXTextEdit ID="edCity" runat="server" DataField="City" ></px:PXTextEdit>
										<px:PXSelector ID="edCountryID2" runat="server" AllowEdit="True" DataField="CountryID"
											FilterByAllFields="True" TextMode="Search" DisplayMode="Hint" CommitChanges="True" ></px:PXSelector>
										<px:PXSelector ID="edState2" runat="server" AutoRefresh="True" DataField="State"
											CommitChanges="true" FilterByAllFields="True" TextMode="Search" DisplayMode="Hint" ></px:PXSelector>
										<px:PXLayoutRule ID="PXLayoutRule18" runat="server" Merge="True" LabelsWidth="SM" ></px:PXLayoutRule>
										<px:PXMaskEdit Size="S" ID="edPostalCode" runat="server" DataField="PostalCode" CommitChanges="true" ></px:PXMaskEdit>
										<px:PXButton ID="btnViewOnMap1" runat="server" CommandName="ViewDefLocationOnMap"
											CommandSourceID="ds" Size="xs" Text="View On Map" Height="20">
										</px:PXButton>
									</Template>
									<ContentLayout OuterSpacing="None" ></ContentLayout>
								</px:PXFormView>
							</px:PXPanel>
							<px:PXLayoutRule ID="PXLayoutRule19" runat="server" ></px:PXLayoutRule>
							<px:PXLayoutRule ID="PXLayoutRule20" runat="server" ControlSize="XM" LabelsWidth="SM"
								StartColumn="True" ></px:PXLayoutRule>
							<px:PXLayoutRule ID="PXLayoutRule21" runat="server" StartGroup="True" GroupCaption="Default Location Settings" ></px:PXLayoutRule>
							<px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" ></px:PXTextEdit>
							<px:PXTextEdit ID="edTaxRegistrationID" runat="server" DataField="TaxRegistrationID" ></px:PXTextEdit>
							<px:PXSelector ID="edCTaxZoneID" runat="server" DataField="CTaxZoneID" AllowEdit="True" ></px:PXSelector>
							<px:PXSelector ID="edCBranchID" runat="server" DataField="CBranchID" AllowEdit="True" ></px:PXSelector>
							<px:PXSelector ID="edCPriceClassID" runat="server" DataField="CPriceClassID" AllowEdit="True" ></px:PXSelector>
							<px:PXLayoutRule ID="PXLayoutRule1" runat="server" GroupCaption="Shipping Instructions" ></px:PXLayoutRule>
							<px:PXSegmentMask ID="edCSiteID" runat="server" DataField="CSiteID" AllowEdit="True" ></px:PXSegmentMask>
							<px:PXSelector CommitChanges="True" ID="edCarrierID" runat="server" DataField="CCarrierID"
								AllowEdit="True" ></px:PXSelector>
							<px:PXSelector ID="edShipTermsID" runat="server" DataField="CShipTermsID" AllowEdit="True" ></px:PXSelector>
							<px:PXSelector ID="edShipZoneID" runat="server" DataField="CShipZoneID" AllowEdit="True" ></px:PXSelector>
							<px:PXSelector ID="edFOBPointID" runat="server" DataField="CFOBPointID" AllowEdit="True" ></px:PXSelector>
                             <px:PXCheckBox ID="chkResedential" runat="server" DataField="CResedential" ></px:PXCheckBox>
                            <px:PXCheckBox ID="chkSaturdayDelivery" runat="server" DataField="CSaturdayDelivery" ></px:PXCheckBox>
							<px:PXCheckBox ID="chkInsurance" runat="server" DataField="CInsurance" ></px:PXCheckBox>
							<px:PXDropDown ID="edCShipComplete" runat="server" DataField="CShipComplete" ></px:PXDropDown>
							<px:PXNumberEdit ID="edCOrderPriority" runat="server" DataField="COrderPriority" ></px:PXNumberEdit>
							<px:PXNumberEdit ID="edLeadTime" runat="server" DataField="CLeadTime" ></px:PXNumberEdit>
						</Template>
						<ContentStyle BackColor="Transparent" BorderStyle="None">
						</ContentStyle>
					</px:PXFormView>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Locations" LoadOnDemand="true">
				<Template>
					<px:PXGrid ID="grdLocations" runat="server" DataSourceID="ds" Height="100%" Width="100%"
						SkinID="Inquire" AllowSearch="True">
						<Levels>
							<px:PXGridLevel DataMember="Locations">
								<Columns>
									<px:PXGridColumn DataField="LocationCD" LinkCommand="Locations_ViewDetails" Width="80px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Descr" Width="220px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="IsActive" TextAlign="Center" Type="CheckBox" Width="60px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="IsDefault" TextAlign="Center" Type="CheckBox" Width="60px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Address__City" Width="180px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Address__CountryID" RenderEditorText="True" AutoCallBack="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Address__State" Width="120px" RenderEditorText="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CTaxZoneID" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CPriceClassID" Width="81px" AllowShowHide="Server" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CSalesAcctID" Width="108px" AutoCallBack="True" AllowShowHide="Server" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CSalesSubID" Width="100px" AllowShowHide="Server" ></px:PXGridColumn>
								</Columns>
								<Layout FormViewHeight="" ></Layout>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" ></AutoSize>
						<LevelStyles>
							<RowForm Height="400px" Width="800px">
							</RowForm>
						</LevelStyles>
						<ActionBar DefaultAction="cmdViewLocation">
							<CustomItems>
								<px:PXToolBarButton ImageKey="AddNew" Tooltip="Add New Location" DisplayStyle="Image">
									<AutoCallBack Command="AddLocation" Target="ds" ></AutoCallBack>
									<PopupCommand Command="Refresh" Target="grdLocations" ></PopupCommand>
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Location Details" Key="cmdViewLocation" Visible="False">
									<AutoCallBack Command="Locations_ViewDetails" Target="ds" ></AutoCallBack>
									<PopupCommand Command="Refresh" Target="grdLocations" ></PopupCommand>
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Set as Default">
									<AutoCallBack Command="SetDefaultLocation" Target="ds" ></AutoCallBack>
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" ></Mode>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Relations" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="grdRelations" runat="server" Height="400px" Width="100%" AllowPaging="True" SyncPosition="True" SyncPositionWithGraph="True" MatrixMode="True"
						ActionsPosition="Top" AllowSearch="true" DataSourceID="ds" SkinID="Details">
						<Levels>
							<px:PXGridLevel DataMember="Relations">
								<Columns>
                              <px:PXGridColumn DataField="Role" Width="120px"  CommitChanges="True"></px:PXGridColumn>
                              <px:PXGridColumn DataField="IsPrimary" Width="80px" Type="CheckBox" TextAlign="Center" CommitChanges="true"></px:PXGridColumn>
                              <px:PXGridColumn DataField="TargetType" Width="120px"  CommitChanges="True"></px:PXGridColumn>
                              <px:PXGridColumn DataField="TargetNoteID" Width="120px" DisplayMode="Text"  LinkCommand="Relations_TargetDetails" CommitChanges="True"></px:PXGridColumn>
                              <px:PXGridColumn DataField="EntityID" Width="160px" AutoCallBack="true" LinkCommand="Relations_EntityDetails" CommitChanges="True"></px:PXGridColumn>
                              <px:PXGridColumn DataField="Name" Width="200px" ></px:PXGridColumn>
                              <px:PXGridColumn DataField="ContactID" Width="160px" AutoCallBack="true" TextAlign="Left" TextField="ContactName" DisplayMode="Text" LinkCommand="Relations_ContactDetails" ></px:PXGridColumn>
                              <px:PXGridColumn DataField="Email" Width="120px" ></px:PXGridColumn>
                              <px:PXGridColumn DataField="AddToCC" Width="70px" Type="CheckBox" TextAlign="Center" ></px:PXGridColumn>
								</Columns>
								<RowTemplate>
                              <px:PXSelector ID="edTargetNoteID" runat="server" DataField="TargetNoteID" FilterByAllFields="True" AutoRefresh="True" ></px:PXSelector>
                              <px:PXSelector ID="edRelEntityID" runat="server" DataField="EntityID" FilterByAllFields="True" AutoRefresh="True" ></px:PXSelector>
									<px:PXSelector ID="edRelContactID" runat="server" DataField="ContactID" FilterByAllFields="True" AutoRefresh="True" ></px:PXSelector>
								</RowTemplate>
							</px:PXGridLevel>
						</Levels>
                        <Mode InitNewRow="True" ></Mode>
                        <AutoSize Enabled="True" MinHeight="100" MinWidth="100" ></AutoSize>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Opportunities" LoadOnDemand="true">
				<Template>
					<px:PXGrid ID="gridOpportunities" runat="server" DataSourceID="ds" Height="423px"
						Width="100%" AllowSearch="True" ActionsPosition="Top" SkinID="Inquire">
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" ></AutoSize>
						<Levels>
							<px:PXGridLevel DataMember="Opportunities">
								<Columns>
									<px:PXGridColumn DataField="OpportunityID" Width="130px" LinkCommand="Opportunities_ViewDetails" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OpportunityName" Width="151px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="StageID" Width="108px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CROpportunityProbability__Probability" TextAlign="Right" Width="100px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Status" Width="81px" RenderEditorText="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CuryProductsAmount" TextAlign="Right" Width="81px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CuryID" Width="54px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CloseDate" Width="130px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="BAccount__AcctCD" Width="100px" LinkCommand="Opportunities_BAccount_ViewDetails"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="BAccount__AcctName" Width="150px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Contact__DisplayName" Width="120px" LinkCommand="Opportunities_Contact_ViewDetails" ></px:PXGridColumn>
									<px:PXGridColumn DataField="WorkgroupID" Width="108px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OwnerID" Width="108px" DisplayMode="Text" ></px:PXGridColumn>
								</Columns>
								<Layout FormViewHeight="" ></Layout>
							</px:PXGridLevel>
						</Levels>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" ></Mode>
						<ActionBar DefaultAction="cmdOpportunityDetails" PagerVisible="False">
							<CustomItems>
								<px:PXToolBarButton Key="cmdAddOpportunity" ImageKey="AddNew" Tooltip="Add New Opportunity" DisplayStyle="Image">
									<AutoCallBack Command="AddOpportunity" Target="ds" ></AutoCallBack> 
									<ActionBar GroupIndex="0" Order="4"></ActionBar>
									<PopupCommand Command="Refresh" Target="gridOpportunities" ></PopupCommand>
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Opportunity Details" Key="cmdOpportunityDetails">
									<AutoCallBack Command="Opportunities_ViewDetails" Target="ds" ></AutoCallBack>
									<ActionBar GroupIndex="2" ></ActionBar>
									<PopupCommand Command="Refresh" Target="gridOpportunities" ></PopupCommand>
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Cases" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="gridCases" runat="server" DataSourceID="ds" Height="423px" Width="100%"
						AllowSearch="True" SkinID="Inquire" AllowPaging="true" AdjustPageSize="Auto"
						BorderWidth="0px">
						<ActionBar DefaultAction="cmdViewCaseDetails" PagerVisible="False">
							<CustomItems>
								<px:PXToolBarButton Key="cmdAddCase" ImageKey="AddNew" Tooltip="Add New Case" DisplayStyle="Image">
									<AutoCallBack Command="AddCase" Target="ds" ></AutoCallBack>
									<PopupCommand Command="Refresh" Target="gridCases" ></PopupCommand>
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Case Details" Key="cmdViewCaseDetails" Visible="false">
									<ActionBar GroupIndex="0" ></ActionBar>
									<AutoCallBack Command="Cases_ViewDetails" Target="ds" ></AutoCallBack>
									<PopupCommand Command="Refresh" Target="gridCases" ></PopupCommand>
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Cases">
								<Columns>
									<px:PXGridColumn DataField="CaseCD" Width="80px" LinkCommand="Cases_ViewDetails" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Subject" Width="300px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CaseClassID" Width="80px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ContractID" Width="90px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Severity" Width="54px" RenderEditorText="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Status" Width="72px" RenderEditorText="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Resolution" Width="72px" RenderEditorText="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CreatedDateTime" Width="90px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="InitResponse" Width="63px" DisplayFormat="###:##:##" ></px:PXGridColumn>
									<px:PXGridColumn DataField="TimeEstimated" Width="63px" DisplayFormat="###:##:##" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ResolutionDate" Width="90px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="WorkgroupID" Width="108px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OwnerID" Width="108px" DisplayMode="Text" ></px:PXGridColumn>
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" ></AutoSize>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" ></Mode>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Contracts" LoadOnDemand="true">
				<Template>
					<px:PXGrid ID="grdContracts" runat="server" DataSourceID="ds" Height="522px" Width="100%"
						SkinID="Inquire">
						<Levels>
							<px:PXGridLevel DataMember="Contracts">
								<Columns>
									<px:PXGridColumn DataField="ContractCD" LinkCommand="Contracts_ViewDetails" Width="120px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="BAccount__AcctCD" Width="100px" LinkCommand="Contracts_BAccount_ViewDetails"></px:PXGridColumn>
									<px:PXGridColumn DataField="BAccount__AcctName" Width="150px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Description" Width="200px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="LocationID" Width="80px" LinkCommand="Contracts_Location_ViewDetails" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Status" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ExpireDate" Width="90px" ></px:PXGridColumn>
								</Columns>
								<Layout FormViewHeight="" ></Layout>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" ></AutoSize>
						<ActionBar DefaultAction="cmdViewContract" PagerVisible="False">
							<CustomItems>
								<px:PXToolBarButton Text="Contract Details" Key="cmdViewContract" Visible="False">
									<ActionBar GroupIndex="2" ></ActionBar>
									<AutoCallBack Command="Contracts_ViewDetails" Target="ds" ></AutoCallBack>
									<PopupCommand Command="Refresh" Target="grdContracts" ></PopupCommand>
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" ></Mode>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Orders" LoadOnDemand="true">
				<Template>
					<px:PXGrid ID="grdOrders" runat="server" DataSourceID="ds" Height="522px" Width="100%"
						SkinID="Inquire" AllowSearch="True">
						<Levels>
							<px:PXGridLevel DataMember="Orders">
								<Columns>
									<px:PXGridColumn DataField="OrderType" Width="72px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OrderNbr" Width="90px" LinkCommand="Orders_ViewDetails" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OrderDesc" Width="117px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CustomerOrderNbr" Width="90px" LinkCommand="Orders_ViewDetails" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Status" Width="81px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="RequestDate" Width="81px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ShipDate" Width="81px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ShipVia" Width="90px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ShipZoneID" Width="81px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OrderWeight" TextAlign="Right" Width="81px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OrderVolume" TextAlign="Right" Width="81px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OrderQty" TextAlign="Right" Width="81px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CuryID" Width="54px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CuryOrderTotal" TextAlign="Right" Width="81px" ></px:PXGridColumn>
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" ></AutoSize>
						<ActionBar DefaultAction="cmdViewOrder" PagerVisible="False">
							<CustomItems>
								<px:PXToolBarButton Text="Order Details" Key="cmdViewOrder" Visible="False">
									<ActionBar GroupIndex="2" ></ActionBar>
									<AutoCallBack Command="Orders_ViewDetails" Target="ds" ></AutoCallBack>
									<PopupCommand Command="Refresh" Target="grdOrders" ></PopupCommand>
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" ></Mode>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Campaigns" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="grdCampaignHistory" runat="server" Height="400px" Width="100%" Style="z-index: 100"
						AllowPaging="True" ActionsPosition="Top" AllowSearch="true" DataSourceID="ds"
						SkinID="Details">
						<Levels>
							<px:PXGridLevel DataMember="Members">
								<Columns>
									<px:PXGridColumn DataField="CampaignID" Width="200px" AutoCallBack="true" LinkCommand="Members_CRCampaign_ViewDetails" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CRCampaign__CampaignName" Width="200px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CRCampaign__Status" Width="200px" Visible="false" SyncVisible="false" ></px:PXGridColumn>
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" ></AutoSize>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Marketing Lists" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="grdMarketingLists" runat="server" Height="400px" Width="100%"
						AllowPaging="True" ActionsPosition="Top" AllowSearch="true" DataSourceID="ds"
						SkinID="Details">
						<Levels>
							<px:PXGridLevel DataMember="Subscriptions">
								<Columns>
								    <px:PXGridColumn DataField="IsSubscribed" Width="105px" Type="CheckBox" TextAlign="Center" ></px:PXGridColumn>
									<px:PXGridColumn DataField="MarketingListID" Width="130px" AutoCallBack="true" LinkCommand="Subscriptions_CRMarketingList_ViewDetails"
										TextField="CRMarketingList__MailListCode" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CRMarketingList__Name" Width="200px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Format" Width="80px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CRMarketingList__IsDynamic" Width="100px" Type="CheckBox" TextAlign="Center" ></px:PXGridColumn>
								    <px:PXGridColumn DataField="Contact__MemberName" Width="200px" ></px:PXGridColumn>
								    <px:PXGridColumn DataField="Contact__Email" Width="200px" ></px:PXGridColumn>
								    <px:PXGridColumn DataField="Contact__Phone1" Width="200px" ></px:PXGridColumn>
								</Columns>
								<RowTemplate>
                                    <px:PXSelector ID="edMarketingListID" runat="server" DataField="MarketingListID" AllowEdit="false" ></px:PXSelector>
                                </RowTemplate>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" ></AutoSize>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
		<px:PXTabItem Text="Sync Status">
		    <Template>
		        <px:PXGrid ID="syncGrid" runat="server" DataSourceID="ds" Height="150px" Width="100%" ActionsPosition="Top" SkinID="Inquire" SyncPosition="true">
		            <Levels>
		                <px:PXGridLevel DataMember="SyncRecs" DataKeyNames="SyncRecordID">
		                    <Columns>
		                        <px:PXGridColumn DataField="SYProvider__Name" Width="200px" ></px:PXGridColumn>                                    
		                        <px:PXGridColumn DataField="RemoteID" Width="200px" CommitChanges="True" LinkCommand="GoToSalesforce" ></px:PXGridColumn>
		                        <px:PXGridColumn DataField="Status" Width="120px" ></px:PXGridColumn>
		                        <px:PXGridColumn DataField="Operation" Width="100px" ></px:PXGridColumn>      
		                        <px:PXGridColumn DataField="LastErrorMessage" Width="230" ></px:PXGridColumn>
		                        <px:PXGridColumn DataField="LastAttemptTS" Width="120px" DisplayFormat="g" ></px:PXGridColumn>
		                        <px:PXGridColumn DataField="AttemptCount" Width="120px" ></px:PXGridColumn>
                                <px:PXGridColumn DataField="SFEntitySetup__ImportScenario" Width="150" ></px:PXGridColumn>
                                <px:PXGridColumn DataField="SFEntitySetup__ExportScenario" Width="150" ></px:PXGridColumn>
		                    </Columns>                               
		                    <Layout FormViewHeight="" ></Layout>
		                </px:PXGridLevel>
		            </Levels>
		            <ActionBar>                        
		                <CustomItems>
		                    <px:PXToolBarButton Key="SyncSalesforce">
		                        <AutoCallBack Command="SyncSalesforce" Target="ds"></AutoCallBack>
		                    </px:PXToolBarButton>
		                </CustomItems>
		            </ActionBar>
		            <Mode InitNewRow="true" ></Mode>
		            <AutoSize Enabled="True" MinHeight="150" ></AutoSize>
		        </px:PXGrid>
		    </Template>
		</px:PXTabItem>
			<px:PXTabItem Text="MaxQ Contracts">
				<Template>
					<px:PXGrid runat="server" ID="VirtualContainer1" AllowPaging="True" SyncPosition="True" SkinID="Inquire" Style='height:400px;width:100%;'>
						<Levels>
							<px:PXGridLevel DataMember="MaxQContracts">
								<Columns>
									<px:PXGridColumn DataField="ContractCDRevNbr" Width="200px" LinkCommand="ViewContract" />
									<px:PXGridColumn DataField="Descr" Width="200px" />
									<px:PXGridColumn DataField="Status" Width="125px" />
									<px:PXGridColumn DataField="ContractType" Width="225px" />
									<px:PXGridColumn DataField="BegDate" Width="125px" />
									<px:PXGridColumn DataField="EndDate" Width="100px" /></Columns>
								<RowTemplate>
									<px:PXTextEdit runat="server" ID="CstColumnEditor2" DataField="ContractCDRevNbr" />
									<px:PXTextEdit runat="server" ID="CstColumnEditor3" DataField="Descr" />
									<px:PXDropDown runat="server" ID="CstColumnEditor4" DataField="Status" />
									<px:PXDateTimeEdit runat="server" ID="CstColumnEditor5" DataField="BegDate" />
									<px:PXDateTimeEdit runat="server" ID="CstColumnEditor6" DataField="EndDate" />
									<px:PXDropDown runat="server" ID="CstColumnEditor7" DataField="ContractType" /></RowTemplate></px:PXGridLevel></Levels>
						<ActionBar ActionsVisible="True" /></px:PXGrid></Template></px:PXTabItem></Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="250" MinWidth="300" ></AutoSize>
	</px:PXTab>
	<px:PXSmartPanel ID="pnlActivityContacts" runat="server" Height="81px" Style="z-index: 108;
		left: 352px; position: absolute; top: 99px" Width="376px" CaptionVisible="True"
		Caption="Select Contact" DesignView="Content" LoadOnDemand="True" Key="ActivityContacts"
		AutoCallBack-Target="frmActivityContacts" AutoCallBack-Command="Refresh">
		<div style="padding-top: 9px; padding-left: 9px; padding-right: 9px">
			<px:PXFormView ID="frmActivityContacts" runat="server" DataSourceID="ds"
				Width="100%" DataMember="ActivityContacts" SkinID="Transparent">
				<Template>
					<table id="tbl" style="vertical-align: top; height: 20px;" cellspacing="0" cellpadding="0">
						<tr align="left">
							<td>
								<px:PXLabel ID="lblContactID" runat="server">Contact :</px:PXLabel>
							</td>
							<td style="width: 9px;">
								&nbsp;
							</td>
							<td align="left">
								<px:PXSelector CommitChanges="True" ID="edContactID" runat="server" DataField="ContactID"
									LabelID="lblContactID" Width="261px" TextField="DisplayName" AutoRefresh="true"
									TextMode="Search" DisplayMode="Hint" FilterByAllFields="True" />
							</td>
						</tr>
					</table>
				</Template>
			</px:PXFormView>
		</div>
		<div style="padding: 9px; text-align: right;">
			<px:PXButton ID="btnSave" runat="server" DialogResult="OK" Text="OK" Width="63px"
				Height="20px" />
			<px:PXButton ID="btnCancel" runat="server" DialogResult="Cancel" Text="Cancel" Width="63px"
				Height="20px" Style="margin-left: 5px" />
		</div>
	</px:PXSmartPanel>
       <px:PXSmartPanel ID="spMergeParamsDlg" runat="server" Height="400"
        Width="600" Caption="Please resolve the conflicts" CaptionVisible="True" Key="Duplicates" LoadOnDemand="True" ShowAfterLoad="true"
        AutoCallBack-Enabled="true" AutoCallBack-Target="formMerge" AutoCallBack-Command="Refresh"
        CallBackMode-CommitChanges="True" CallBackMode-PostData="Page"
        AcceptButtonID="btnOk" CancelButtonID="btnCancel">
        <px:PXFormView ID="formMerge" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" SkinID="Transparent"
            DataMember="mergeParams">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule4" runat="server" StartColumn="True" />
                <px:PXSelector CommitChanges="True" ID="edBAccountID" runat="server" DataField="BAccountID" FilterByAllFields="True" DisplayMode="Text" TextMode="Search" AutoRefresh="True"/>                
            </Template>
        </px:PXFormView>
        <px:PXGrid ID="grdFields" runat="server" Width="100%" DataSourceID="ds" MatrixMode="True" AutoAdjustColumns="true">
                    <Levels>
                        <px:PXGridLevel DataMember="ValueConflicts">
                            <Columns>
                                <px:PXGridColumn DataField="DisplayName" />
                                <px:PXGridColumn DataField="Value" AutoCallBack="True" />
                            </Columns>
                            <Layout ColumnsMenu="False" />
                            <Mode AllowAddNew="false" AllowDelete="false" />
                        </px:PXGridLevel>
                    </Levels>
                    <ActionBar>
                        <Actions>
                            <ExportExcel Enabled="False" />
                            <AddNew Enabled="False" />
                            <FilterShow Enabled="False" />
                            <FilterSet Enabled="False" />
                            <Save Enabled="False" />
                            <Delete Enabled="False" />
                            <NoteShow Enabled="False" />
                            <Search Enabled="False" />
                            <AdjustColumns Enabled="False" />
                        </Actions>
                    </ActionBar>
                    <CallbackCommands>
                        <Save PostData="Page" />
                    </CallbackCommands>
                      <AutoSize Enabled="true" />
                </px:PXGrid>
        <px:PXPanel ID="PXPanel2" runat="server" SkinID="Buttons">
            <px:PXButton ID="PXButton1" runat="server" Text="OK" DialogResult="OK" />
            <px:PXButton ID="PXButton2" runat="server" Text="Cancel" DialogResult="Cancel" />
        </px:PXPanel>
    </px:PXSmartPanel>
</asp:Content>
