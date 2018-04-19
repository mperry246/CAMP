<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="FS300100.aspx.cs" Inherits="Page_FS300100" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" PrimaryView="ServiceOrderRecords"
        TypeName="PX.Objects.FS.ServiceOrderEntry" >
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="ViewDirectionOnMap" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ValidateAddress" Visible="False"/>
            <px:PXDSCallbackCommand Name="OpenSource" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="createNewCustomer" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="OpenStaffSelectorFromServiceTab" Visible="False" RepaintControls="All"/>
            <px:PXDSCallbackCommand Name="OpenStaffSelectorFromStaffTab" Visible="False" RepaintControls="All"/>
            <px:PXDSCallbackCommand Name="OpenServiceSelector" Visible="False" RepaintControls="All"/>
            <px:PXDSCallbackCommand Name="OpenAppointmentScreen" Visible="False" RepaintControls="All"/>
            <px:PXDSCallbackCommand Name="SelectCurrentService" Visible="False"></px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="OpenServiceOrderScreen" Visible="False"></px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="OpenPostingDocument" Visible="False"></px:PXDSCallbackCommand>
        </CallbackCommands>
        <DataTrees>
            <px:PXTreeDataMember TreeView="TreeWFStages" TreeKeys="WFStageID"/>
        </DataTrees>
    </px:PXDataSource>
    <%-- Employee Selector --%>
    <px:PXSmartPanel
        ID="PXSmartPanelStaffSelector"
        runat="server"
        Caption="Staff Selector"
        CaptionVisible="True"
        Key="StaffSelectorFilter"
        AutoCallBack-Command="Refresh"
        AutoCallBack-Target="staffGrid"
        ShowAfterLoad="True"
        Width="1100px"
        Height="900px"
        CloseAfterAction ="True"
        AutoReload="True"
        LoadOnDemand="True"
        TabIndex="8500"
        AutoRepaint="true"
        CallBackMode-CommitChanges="True"
        CallBackMode-PostData="Page" AllowResize="False">
        <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
        <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Height="100px" DataMember="StaffSelectorFilter" TabIndex="700" SkinID="Transparent">
            <Template>
                <px:PXLayoutRule runat="server" StartColumn="True"
                    LabelsWidth="SM" ControlSize="M">
                </px:PXLayoutRule>
                <px:PXSelector ID="edServiceLineRef2" runat="server" CommitChanges="True" DataField="ServiceLineRef" AutoRefresh="True" DisplayMode="Value">
                </px:PXSelector>
                <px:PXTextEdit ID="edPostalCode" runat="server" DataField="PostalCode"
                    Size="SM" SuppressLabel="False">
                </px:PXTextEdit>
                <px:PXSelector ID="edGeoZoneID" runat="server" DataField="GeoZoneID" CommitChanges = "True">
                </px:PXSelector>
                <px:PXTextEdit ID="edStaffConcat" runat="server" DataField="StaffConcat"
                    Size="L" SuppressLabel="False" Enabled="False">
                </px:PXTextEdit>

                <px:PXLayoutRule runat="server" StartRow="True"
                    LabelsWidth="SM" ControlSize="M">
                </px:PXLayoutRule>
                <px:PXLayoutRule runat="server" StartRow="True">
                </px:PXLayoutRule>
            </Template>
        </px:PXFormView>
                    <px:PXGrid ID="PXGridServiceSkills" runat="server" DataSourceID="ds"
                     Style="z-index: 100" Width="600px" SkinID="Inquire" TabIndex="900"
                     SyncPosition="True" AllowPaging="True" AdjustPageSize="Auto"
                     Height="350px" AutoAdjustColumns="True">
                        <Levels>
                            <px:PXGridLevel DataMember="SkillGridFilter" DataKeyNames="SkillCD">
                                <Columns>
                                    <px:PXGridColumn DataField="Mem_Selected" Width="90px" TextAlign="Center" Type="CheckBox" CommitChanges="true" >
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SkillCD" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_ServicesList" Width="225px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="350" />
                        <ActionBar PagerVisible="Bottom" ActionsText="False">
                        </ActionBar>
                        <Mode AllowAddNew="False" AllowDelete="False" />
                    </px:PXGrid>
                    <px:PXGrid ID="PXGridLicenseType" runat="server"
                    AdjustPageSize="Auto" AllowPaging="True" DataSourceID="ds" Height="350px"
                    SkinID="Inquire" Style="z-index: 100" SyncPosition="True" TabIndex="910"
                    Width="600px" AutoAdjustColumns="True" FilesIndicator="false" NoteIndicator ="false">
                        <Levels>
                            <px:PXGridLevel DataMember="LicenseTypeGridFilter" DataKeyNames="LicenseTypeCD">
                                <Columns>
                                    <px:PXGridColumn DataField="Mem_Selected" TextAlign="Center" Type="CheckBox"
                                      CommitChanges="true" Width="90px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LicenseTypeCD" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="350" />
                        <ActionBar ActionsText="False" PagerVisible="Bottom">
                        </ActionBar>
                        <Mode AllowAddNew="False" AllowDelete="False" />
                    </px:PXGrid>
        <px:PXLayoutRule runat="server" StartColumn="True">
        </px:PXLayoutRule>
        <px:PXLabel runat="server" Height="30px" />
        <px:PXLabel runat="server" Height="66px" />
        <px:PXGrid ID="PXGridStaffAvailable" runat="server" DataSourceID="ds"
            Style="z-index: 100" Width="380px" SkinID="Inquire" TabIndex="500"
            SyncPosition = "True" AllowPaging="True" AdjustPageSize="Auto" AutoAdjustColumns="True"
            FilesIndicator="false" NoteIndicator ="false">
        <Levels>
          <px:PXGridLevel DataMember="StaffRecords" DataKeyNames="AcctCD">
                    <Columns>
                        <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox"
                                      CommitChanges="true" Width="90px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="Type" Width="90px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="AcctCD" Width="120px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="AcctName" Width="200px">
                        </px:PXGridColumn>
                    </Columns>
          </px:PXGridLevel>
        </Levels>
        <AutoSize Enabled="True" MinHeight="704"/>
            <Mode AllowAddNew="False" AllowDelete="False"/>
      </px:PXGrid>
        <px:PXLayoutRule runat="server" StartRow="True">
        </px:PXLayoutRule>
        <px:PXButton ID="closeStaffSelector" runat="server" DialogResult="Cancel" Text="Close" AlignLeft="True">
        </px:PXButton>
    </px:PXSmartPanel>
    <%--/ Employee Selector --%>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <%--Service Order Type Selector--%>
    <px:PXSmartPanel
        ID="ServiceOrderTypeSelector"
        runat="server"
        Caption="Select the new Service Order Type"
        CaptionVisible="True"
        Key="ServiceOrderTypeSelector"
        TabIndex="17900"
        ShowAfterLoad="True"
        Width="400px"
        Height="100px">
        <px:PXLayoutRule runat="server" StartColumn="True">
        </px:PXLayoutRule>
        <px:PXFormView ID="DriverRouteForm" runat="server" DataMember="ServiceOrderTypeSelector"
            DataSourceID="ds" TabIndex="1600" SkinID="Transparent">
            <Template>
                <px:PXLayoutRule runat="server" StartRow="True" ControlSize="SM" LabelsWidth="SM"
                    StartColumn="True">
                </px:PXLayoutRule>
                <px:PXSelector ID="edSrvOrdType" runat="server"
                AutoRefresh="True" DataField="SrvOrdType" DataSourceID="ds" CommitChanges="True">
                </px:PXSelector>
            </Template>
        </px:PXFormView>
        <px:PXLayoutRule runat="server" StartRow="True" Merge="True">
        </px:PXLayoutRule>
        <px:PXButton ID="PXButton1" runat="server" DialogResult="OK" Text="Proceed"
            AlignLeft="True" Width="125px">
        </px:PXButton>
        <px:PXButton ID="PXButton2" runat="server" DialogResult="Cancel" Text="Close"
            AlignLeft="True">
        </px:PXButton>
    </px:PXSmartPanel>
    <%--/Service Order Type Selector--%>
    <px:PXFormView ID="mainForm" runat="server" DataSourceID="ds" Style="z-index: 100"
        Width="100%" Caption="Service Order" MarkRequired="Dynamic" NotifyIndicator="True" FilesIndicator="True"
        DataMember="ServiceOrderRecords" TabIndex="3700" DefaultControlID="edCustomerID" AllowCollapse="True">
        <Template>
            <px:PXLayoutRule runat="server" ControlSize="S" StartColumn="True" StartRow="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edSrvOrdType" runat="server" AllowEdit="True"
                AutoRefresh="True" DataField="SrvOrdType" DataSourceID="ds">
            </px:PXSelector>
            <px:PXSelector ID="edRefNbr" runat="server" AutoRefresh="True"
                DataField="RefNbr" DataSourceID="ds">
            </px:PXSelector>
            <px:PXTreeSelector CommitChanges="True" ID="edWFStageID" runat="server" DataField="WFStageID"
                TreeDataMember="TreeWFStages" TreeDataSourceID="ds" PopulateOnDemand="True"
                InitialExpandLevel="0" ShowRootNode="False">
                <DataBindings>
                    <px:PXTreeItemBinding TextField="WFStageCD" ValueField="WFStageCD">
                    </px:PXTreeItemBinding>
                </DataBindings>
            </px:PXTreeSelector>
            <px:PXTextEdit ID="edCustWorkOrderRefNbr" runat="server" DataField="CustWorkOrderRefNbr" AutoRefresh="True">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edCustPORefNbr" runat="server" DataField="CustPORefNbr" AutoRefresh="True">
            </px:PXTextEdit>
			<px:PXLabel runat="server" />
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXSegmentMask ID="edCustomerID" runat="server" AllowEdit="True"
                CommitChanges="True" DataField="CustomerID" DataSourceID="ds">
            </px:PXSegmentMask>
            <px:PXSegmentMask ID="edLocationID" runat="server" AllowEdit="True"
                AutoRefresh="True" CommitChanges="True" DataField="LocationID"
                DataSourceID="ds">
            </px:PXSegmentMask>
            <px:PXSelector ID="edBranchID" runat="server" CommitChanges="True"
                DataField="BranchID">
            </px:PXSelector>
            <px:PXSelector ID="edBranchLocationID" runat="server" AllowEdit="True"
                AutoRefresh="True" CommitChanges="True" DataField="BranchLocationID"
                DataSourceID="ds" >
            </px:PXSelector>
            <px:PXSegmentMask ID="edProjectID" runat="server" AutoRefresh="True"
                CommitChanges="True" DataField="ProjectID" AllowEdit="True">
            </px:PXSegmentMask>
            <px:PXSelector ID="edDfltProjectTaskID" runat="server" DataField="DfltProjectTaskID"
                DisplayMode="Value" AllowEdit = "True" AutoRefresh="True" CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector ID="edRoomID" runat="server" AllowEdit="True" DataField="RoomID"
                AutoRefresh="True" >
            </px:PXSelector>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXDropDown ID="edStatus" runat="server" DataField="Status" Enabled="False">
            </px:PXDropDown>
            <px:PXCheckBox ID="edHold" runat="server" DataField="Hold" Text="Hold" CommitChanges="True">
            </px:PXCheckBox>
            <px:PXCheckBox ID="edQuote" runat="server" DataField="Quote" Text="Quote"
                CommitChanges="True" Enabled="False">
            </px:PXCheckBox>
            <px:PXCheckBox ID="edShowAttendees" runat="server"
                DataField="Mem_ShowAttendees">
            </px:PXCheckBox>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
			<px:PXMaskEdit ID="edEstimatedDurationTotal" runat="server"
                DataField="EstimatedDurationTotal" Enabled="False" Size="S">
            </px:PXMaskEdit>
			<px:PXMaskEdit ID="edApptDurationTotal" runat="server"
                DataField="ApptDurationTotal" Enabled="False" Size="S">
            </px:PXMaskEdit>
            <px:PXNumberEdit ID="edEstimatedOrderTotal" runat="server" DataField="EstimatedOrderTotal">
            </px:PXNumberEdit>
			<px:PXNumberEdit ID="edApptOrderTotal" runat="server" DataField="ApptOrderTotal">
            </px:PXNumberEdit>
            <px:PXLayoutRule runat="server" StartRow="True">
            </px:PXLayoutRule>
            <px:PXTextEdit ID="edDocDesc" runat="server" DataField="DocDesc">
            </px:PXTextEdit>
            <px:PXLayoutRule runat="server" StartRow="True" ColumnSpan="2">
            </px:PXLayoutRule>
            <px:PXPanel ID="PXPanel2" runat="server" Caption="Date and Time Info" RenderStyle="Fieldset"
                ContentLayout-Layout="Cavas">
                <px:PXLayoutRule runat="server" ColumnWidth="M" StartColumn="True">
                </px:PXLayoutRule>
                <px:PXDateTimeEdit ID="edOrderDate" runat="server" DataField="OrderDate" CommitChanges="True">
                </px:PXDateTimeEdit>
                <px:PXDateTimeEdit ID="edPromisedDate" runat="server" DataField="PromisedDate" CommitChanges="True">
                </px:PXDateTimeEdit>
                <px:PXLayoutRule runat="server" StartColumn="True">
                </px:PXLayoutRule>
                <px:PXDateTimeEdit ID="edSLAETA_Date" runat="server" DataField="SLAETA_Date">
                </px:PXDateTimeEdit>
                <px:PXDateTimeEdit ID="edSLAETA_Time" runat="server" DataField="SLAETA_Time"
                    TimeMode="True">
                </px:PXDateTimeEdit>
            </px:PXPanel>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="100%" DataSourceID="ds" MarkRequired="Dynamic"
        DataMember="ServiceOrderRelated">
        <Items>
            <px:PXTabItem Text="Services">
                <Template>
                    <px:PXGrid ID="PXGrid2" runat="server" DataSourceID="ds" TabIndex="-12436" SkinID="Details"
                        Width="100%" Height="100%" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataMember="ServiceOrderDetServices" DataKeyNames="SOID,SODetID">
                            <RowTemplate>
                                <px:PXLayoutRule runat="server" StartColumn="True">
                                </px:PXLayoutRule>
                                    <px:PXSmartPanel
                                        ID="PXSmartPanelServiceSelector"
                                        runat="server"
                                        Caption="Service Selector"
                                        CaptionVisible="True"
                                        Key="ServiceSelectorFilter"
                                        AutoCallBack-Command="Refresh"
                                        AutoCallBack-Target="PXGridStaffSelected"
                                        ShowAfterLoad="True"
                                        Width="600px"
                                        ShowMaximizeButton="True"
                                        CloseAfterAction="True"
                                        AutoReload="True"
                                        HideAfterAction="False"
                                        LoadOnDemand="True"
                                        Height="600px" TabIndex="8500"
                                        AutoRepaint="True"
                                        >
                                        <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="SM">
                                        </px:PXLayoutRule>
                                        <px:PXFormView ID="PXFormViewFilter" runat="server"  TabIndex="1600" SkinID="Transparent"
                                            DataMember="ServiceSelectorFilter" DataSourceID="ds">
                                            <Template>
                                                <px:PXSelector ID="edServiceClassID" runat="server" DataField="ServiceClassID"
                                                    DataSourceID="ds" Size="M" AutoRefresh="True" CommitChanges="True">
                                                </px:PXSelector>
                                                <px:PXLabel runat="server" />
                                                <px:PXGrid ID="PXGridSelectedEmployees" runat="server" DataSourceID="ds"
                                                    Style="z-index: 100" Width="100%" SkinID="Inquire" TabIndex="800"
                                                    SyncPosition="True" AllowPaging="True" AdjustPageSize="Auto" Height="200px">
                                                    <Levels>
                                                        <px:PXGridLevel DataKeyNames="EmployeeID"
                                                            DataMember="EmployeeGridFilter">
                                                            <Columns>
                                                                <px:PXGridColumn
                                                                    DataField="Mem_Selected" TextAlign="Center" Type="CheckBox" Width="80px"
                                                                    CommitChanges="True">
                                                                </px:PXGridColumn>
                                                                <px:PXGridColumn DataField="EmployeeID" Width="120px">
                                                                </px:PXGridColumn>
                                                                <px:PXGridColumn DataField="EmployeeID_BAccountStaffMember_acctName" Width="200px">
                                                                </px:PXGridColumn>
                                                            </Columns>
                                                        </px:PXGridLevel>
                                                    </Levels>
                                                    <AutoSize Enabled="True" MinHeight="250" />
                                                    <ActionBar PagerVisible="Bottom" ActionsText="False">
                                                    </ActionBar>
                                                    <Mode AllowAddNew="False" AllowDelete="False" />
                                                </px:PXGrid>
                                            </Template>
                                        </px:PXFormView>
                                        <px:PXLayoutRule runat="server" StartRow="True">
                                        </px:PXLayoutRule>
                                        <px:PXGrid ID="PXGridAvailableServices" runat="server" DataSourceID="ds"
                                                Style="z-index: 100" Width="100%" SkinID="Inquire" TabIndex="500"
                                                SyncPosition = "True" AllowPaging="True" AdjustPageSize="Auto" Height="200px">
                                            <Levels>
                                                <px:PXGridLevel DataKeyNames="InventoryCD" DataMember="ServiceRecords">
                                                    <Columns>
                                                        <px:PXGridColumn DataField="InventoryCD" Width="120px">
                                                        </px:PXGridColumn>
                                                        <px:PXGridColumn DataField="Descr" Width="200px">
                                                        </px:PXGridColumn>
                                                    </Columns>
                                                </px:PXGridLevel>
                                            </Levels>
                                            <AutoSize Enabled="True" MinHeight="250"/>
                                            <ActionBar ActionsText="False" DefaultAction="SelectCurrentService"  PagerVisible="False">
                                                <CustomItems>
                                                    <px:PXToolBarButton Key="SelectCurrentService">
                                                        <AutoCallBack Target="ds" Command="SelectCurrentService"/>
                                                    </px:PXToolBarButton>
                                                </CustomItems>
                                            </ActionBar>
                                            <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
                                        </px:PXGrid>
                                        <px:PXLayoutRule runat="server" StartRow="True">
                                        </px:PXLayoutRule>
                                        <px:PXButton ID="PXButtonCloseServiceSelector" runat="server" DialogResult="Cancel" Text="Close"
                                        AlignLeft="True"/>
                                    </px:PXSmartPanel>
                                    <px:PXLayoutRule runat="server" StartColumn="True" GroupCaption="Detail Info"
                                        StartGroup="True" StartRow="True">
                                    </px:PXLayoutRule>
                                    <px:PXTextEdit ID="LineRef" runat="server" DataField="LineRef" NullText="<NEW>">
                                    </px:PXTextEdit>
                                    <px:PXCheckBox ID="edScheduled" runat="server" DataField="Scheduled">
                                    </px:PXCheckBox>
                                    <px:PXDropDown ID="edStatus" runat="server" DataField="Status">
                                    </px:PXDropDown>
                                    <px:PXDropDown ID="edLineType" runat="server" CommitChanges="True"
                                        DataField="LineType">
                                    </px:PXDropDown>
                                    <px:PXSegmentMask ID="edServiceID" runat="server" DataField="ServiceID" AllowEdit="True" CommitChanges="True"
                                           AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXDropDown ID="edBillingRule" runat="server" DataField="BillingRule" Size="SM">
                                    </px:PXDropDown>
                                    <px:PXTextEdit ID="edTranDesc" runat="server" DataField="TranDesc">
                                    </px:PXTextEdit>
                                    <px:PXSelector ID="edSMEquipmentID" runat="server" DataField="SMEquipmentID" AutoRefresh="True" CommitChanges="True" AllowEdit="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edNewTargetEquipmentLineNbr" runat="server" DataField="NewTargetEquipmentLineNbr" AutoRefresh="True" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edComponentID" runat="server" DataField="ComponentID" AutoRefresh="True" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edEquipmentLineRef" runat="server" DataField="EquipmentLineRef" AutoRefresh="True" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXSegmentMask ID="edStaffID" runat="server" DataField="StaffID" AutoRefresh="True" CommitChanges="True" NullText="<SPLIT>">
                                    </px:PXSegmentMask>
                                    <px:PXCheckBox ID="edWarranty" runat="server" DataField="Warranty">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edIsBillable" runat="server" DataField="IsBillable">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edIsPrepaid" runat="server" DataField="IsPrepaid">
                                    </px:PXCheckBox>
                                    <px:PXSegmentMask ID="edAcctID" runat="server" CommitChanges="True" DataField="AcctID" AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edSubID" runat="server" DataField="SubID" AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXMaskEdit ID="edEstimatedDuration" runat="server" DataField="EstimatedDuration" CommitChanges="True">
                                    </px:PXMaskEdit>
									<px:PXNumberEdit ID="edEstimatedQty" runat="server" DataField="EstimatedQty">
                                    </px:PXNumberEdit>
									<px:PXNumberEdit ID="edApptNumber" runat="server" DataField="ApptNumber">
                                    </px:PXNumberEdit>
									<px:PXNumberEdit ID="edApptDuration" runat="server" DataField="ApptDuration">
                                    </px:PXNumberEdit>
									<px:PXNumberEdit ID="edApptQty" runat="server" DataField="apptQty">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edUnitPrice" runat="server" DataField="UnitPrice" CommitChanges = "True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edEstimatedTranAmt" runat="server" DataField="EstimatedTranAmt">
                                    </px:PXNumberEdit>
									<px:PXNumberEdit ID="edApptTranAmt" runat="server" DataField="ApptTranAmt">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="ProjectTaskID" runat="server" DataField="ProjectTaskID"
                                        DisplayMode="Value" AllowEdit = "True" AutoRefresh="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edMem_LastReferencedBy" runat="server" AllowEdit="True"
                                    DataField="Mem_LastReferencedBy"  >
                                    </px:PXSelector>
                                    <px:PXCheckBox ID="edServEnablePO" runat="server" DataField="EnablePO">
                                    </px:PXCheckBox>
                                    <px:PXSegmentMask ID="edServPOVendorID" runat="server" DataField="POVendorID" AllowEdit="true" CommitChanges="true">
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edServPOVendorLocationID" runat="server" DataField="POVendorLocationID" AllowEdit="true" AutoRefresh="true">
                                    </px:PXSegmentMask>
                                    <px:PXSelector ID="edServPONbr" runat="server" DataField="PONbr" AllowEdit="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edServPOStatus" runat="server" DataField="POStatus" AllowEdit="True">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="LineRef" NullText="<NEW>" Width="85px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Scheduled" TextAlign="Center" Type="CheckBox" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="LineType" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ServiceID" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="BillingRule" CommitChanges="True" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TranDesc" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SMEquipmentID" Width="100px" AutoCallBack = "True" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="NewTargetEquipmentLineNbr" TextAlign="Right" CommitChanges="True" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ComponentID" TextAlign="Right" CommitChanges="True" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EquipmentLineRef" TextAlign="Right" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="StaffID" Width="120px" CommitChanges="True" NullText="<SPLIT>">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Warranty" Width="70px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="IsBillable" Width="65px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="IsPrepaid" Width="65px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="AcctID" Width="108px">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="SubID" Width="180px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EstimatedDuration" Width="95px" CommitChanges="True">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="EstimatedQty" Width="95px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UnitPrice" Width="100px" TextAlign="Right" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EstimatedTranAmt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="ApptDuration" Width="95px">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="apptQty" Width="95px">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="ApptTranAmt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="ApptNumber" Width="95px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ProjectTaskID" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_LastReferencedBy" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EnablePO" TextAlign="Center" Type="CheckBox" Width="65px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="POVendorID" Width="100px" CommitChanges="true">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="POVendorLocationID" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PONbr" TextAlign="Left">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="POStatus" TextAlign="Left">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowFormEdit="True" InitNewRow="True"/>
                        <AutoSize Enabled="True" />
                        <ActionBar ActionsText="False" PagerVisible="False">
                            <CustomItems>
                                <px:PXToolBarButton Key="cmdOpenServiceSelector">
                                    <AutoCallBack Target="ds" Command="OpenServiceSelector" ></AutoCallBack>
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Key="cmdopenStaffSelectorFromServiceTab">
                                    <AutoCallBack Target="ds" Command="openStaffSelectorFromServiceTab" ></AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Inventory Items">
                <Template>
                    <px:PXGrid ID="PXGrid2" runat="server" DataSourceID="ds" TabIndex="-12436" SkinID="Details"
                        Width="100%" Height="100%" KeepPosition="true" SyncPosition="true">
                        <Levels>
                            <px:PXGridLevel DataMember="ServiceOrderDetParts" DataKeyNames="SOID,SODetID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" GroupCaption="Detail Info"
                                        StartGroup="True">
                                    </px:PXLayoutRule>
                                    <px:PXTextEdit ID="LineRef2" runat="server" DataField="LineRef" NullText="<NEW>">
                                    </px:PXTextEdit>
                                    <px:PXDropDown ID="edStatus2" runat="server" DataField="Status">
                                    </px:PXDropDown>
                                    <px:PXDropDown ID="LineType2" runat="server" DataField="LineType"
                                        CommitChanges="True">
                                    </px:PXDropDown>
                                    <px:PXSegmentMask ID="InventoryID2" runat="server" DataField="InventoryID"
                                        AllowEdit="True" CommitChanges="True" AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXTextEdit ID="edTranDesc2" runat="server" DataField="TranDesc">
                                    </px:PXTextEdit>
                                    <px:PXDropDown ID="edEquipmentAction2" runat="server" DataField="EquipmentAction" CommitChanges="True">
                                    </px:PXDropDown>
                                    <px:PXSelector ID="edSMEquipmentID2" runat="server" DataField="SMEquipmentID" AutoRefresh="True" CommitChanges="True" AllowEdit="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edNewTargetEquipmentLineNbr2" runat="server" DataField="NewTargetEquipmentLineNbr" AutoRefresh="True" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edComponentID2" runat="server" DataField="ComponentID" AutoRefresh="True" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edEquipmentLineRef2" runat="server" DataField="EquipmentLineRef" AutoRefresh="True" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edSuspendedTargetEquipmentID" runat="server" DataField="SuspendedTargetEquipmentID">
                                    </px:PXNumberEdit>
                                    <px:PXCheckBox ID="edWarranty2" runat="server" DataField="Warranty">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edIsBillable2" runat="server" DataField="IsBillable" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edIsPrepaid2" runat="server" DataField="IsPrepaid">
                                    </px:PXCheckBox>
                                    <px:PXSegmentMask ID="edAcctID2" runat="server" CommitChanges="True" DataField="AcctID" AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edSubID2" runat="server" DataField="SubID" AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edSiteID2" runat="server" DataField="SiteID" AllowEdit="True" AutoRefresh="True" CommitChanges="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="PXGrid2" Name="FSSODet.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" ></px:PXControlParam>
                                            <px:PXControlParam ControlID="PXGrid2" Name="FSSODet.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" ></px:PXControlParam>
                                         </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edSiteLocationID" runat="server" DataField="SiteLocationID" AutoRefresh="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="PXGrid2" Name="FSSODet.siteID" PropertyName="DataValues[&quot;SiteID&quot;]" Type="String" ></px:PXControlParam>
                                        </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXCheckBox ID="edEnablePO" runat="server" DataField="EnablePO">
                                    </px:PXCheckBox>
                                    <px:PXSegmentMask ID="edPOVendorID" runat="server" DataField="POVendorID" AllowEdit="true" CommitChanges="true">
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edPOVendorLocationID" runat="server" DataField="POVendorLocationID" AllowEdit="true" AutoRefresh="true">
                                    </px:PXSegmentMask>
                                    <px:PXSelector ID="edPONbr" runat="server" DataField="PONbr" AllowEdit="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edPOStatus" runat="server" DataField="POStatus" AllowEdit="True">
                                    </px:PXTextEdit>
                                    <px:PXSelector ID="edUOM2" runat="server" DataField="UOM">
                                    </px:PXSelector>
									<px:PXNumberEdit ID="edApptNumber2" runat="server" DataField="ApptNumber">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="EstimatedQty2" runat="server" DataField="EstimatedQty" CommitChanges = "True">
                                    </px:PXNumberEdit>
									<px:PXNumberEdit ID="edApptQty2" runat="server" DataField="ApptQty">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edUnitPrice2" runat="server" DataField="UnitPrice" CommitChanges = "True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="EstimatedTranAmt2" runat="server" DataField="EstimatedTranAmt">
                                    </px:PXNumberEdit>
									<px:PXNumberEdit ID="edApptTranAmt2" runat="server" DataField="ApptTranAmt">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="ProjectTaskID2" runat="server" DataField="ProjectTaskID"
                                        AllowEdit = "True" AutoRefresh="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edMem_LastReferencedBy2" runat="server" AllowEdit="True"
                                    DataField="Mem_LastReferencedBy">
                                    </px:PXSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="LineRef" NullText="<NEW>" Width="85px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LineType" CommitChanges="True" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="InventoryID" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TranDesc" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EquipmentAction" Width="150px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SMEquipmentID" Width="140px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="NewTargetEquipmentLineNbr" TextAlign="Right" Width="140px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ComponentID" TextAlign="Right" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EquipmentLineRef" TextAlign="Right" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SuspendedTargetEquipmentID" TextAlign="Right" Width="140px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Warranty" Width="70px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="IsBillable" Width="65px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="IsPrepaid" Width="65px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>                                    
                                    <px:PXGridColumn DataField="AcctID" Width="108px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SubID" Width="180px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SiteID" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SiteLocationID" AllowShowHide="Server" Width="81px" NullText="<SPLIT>" CommitChanges="True">
                                    </px:PXGridColumn>                                    
                                    <px:PXGridColumn DataField="UOM">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="EstimatedQty" TextAlign="Right" Width="75px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UnitPrice" Width="100px" TextAlign="Right" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EstimatedTranAmt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="ApptTranAmt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="ApptQty" TextAlign="Right" Width="75px">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="ApptNumber" TextAlign="Right" Width="75px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ProjectTaskID" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_LastReferencedBy" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EnablePO" TextAlign="Center" Type="CheckBox" Width="65px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="POVendorID" Width="100px" CommitChanges="true">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="POVendorLocationID" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PONbr" TextAlign="Left">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="POStatus" TextAlign="Left">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowFormEdit="True" InitNewRow="True"/>
                        <AutoSize Enabled="True" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="General Info">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="M"
                        LabelsWidth="SM">
                    </px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" GroupCaption="Main Customer Contact"
                        StartGroup="True">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edContactID" runat="server" DataField="ContactID"
                        AllowEdit="True" AutoRefresh="True" CommitChanges="True"   >
                    </px:PXSelector>
                    <px:PXTextEdit ID="edAttention" runat="server" DataField="Attention">
                    </px:PXTextEdit>
                    <px:PXMailEdit ID="edEMail" runat="server" DataField="EMail">
                    </px:PXMailEdit>
                    <px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1">
                    </px:PXMaskEdit>
                    <px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2">
                    </px:PXMaskEdit>
                    <px:PXMaskEdit ID="edFax" runat="server" DataField="Fax">
                    </px:PXMaskEdit>
                    <px:PXLayoutRule runat="server" GroupCaption="Billing Info"
                        StartGroup="True">
                    </px:PXLayoutRule>
                    <px:PXSegmentMask ID="edBillCustomerID" runat="server" AllowEdit="True"
                        DataField="BillCustomerID" DataSourceID="ds" CommitChanges="True">
                    </px:PXSegmentMask>
                    <px:PXSegmentMask ID="edBillLocationID" runat="server"
                        DataField="BillLocationID" DataSourceID="ds" AllowEdit = "True" AutoRefresh="True">
                    </px:PXSegmentMask>
                    <px:PXLayoutRule runat="server" GroupCaption="Commission" StartGroup="True">
                    </px:PXLayoutRule>
                    <px:PXSegmentMask ID="edSalesPersonID" runat="server" DataField="SalesPersonID" CommitChanges="True" AutoRefresh="True"></px:PXSegmentMask>
                    <px:PXCheckBox ID="edCommissionable" runat="server" DataField="Commissionable"></px:PXCheckBox>
                    <px:PXLayoutRule runat="server" GroupCaption="Problem and Resolution"
                        StartGroup="True">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="ProblemID" runat="server" AllowEdit="True"
                        DataField="ProblemID" DataSourceID="ds">
                    </px:PXSelector>
                    <px:PXSelector ID="CauseID" runat="server" AllowEdit="True" DataField="CauseID"
                        DataSourceID="ds">
                    </px:PXSelector>
                    <px:PXSelector ID="ResolutionID" runat="server" AllowEdit="True"
                        DataField="ResolutionID" DataSourceID="ds">
                    </px:PXSelector>
                    <px:PXDateTimeEdit ID="ResolutionDate" runat="server"
                        DataField="ResolutionDate">
                    </px:PXDateTimeEdit>
                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="M">
                    </px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" GroupCaption="Service Order Details">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edWFStageID" runat="server" DataField="WFStageID"
                        AllowEdit="True"   >
                    </px:PXSelector>
                    <px:PXDropDown ID="edSeverity" runat="server" DataField="Severity">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edPriority" runat="server" DataField="Priority">
                    </px:PXDropDown>
                    <px:PXSegmentMask ID="edAssignedEmpID" runat="server" DataField="AssignedEmpID">
                    </px:PXSegmentMask>
                    <px:PXLayoutRule runat="server" GroupCaption="Appointment Address">
                    </px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" Merge="True">
                    </px:PXLayoutRule>
                    <px:PXCheckBox ID="edAddressValidated" runat="server"
                        DataField="AddressValidated" Text="Address Validated" AlignLeft="True"
                        Enabled="False" CommitChanges="True">
                    </px:PXCheckBox>
                    <px:PXButton ID="btnValidateAddress" runat="server" Height="21px"
                        Text="Validate Address" Width="120px">
                        <AutoCallBack Command="ValidateAddress" Target="ds">
                        </AutoCallBack>
                    </px:PXButton>
                    <px:PXLayoutRule runat="server" EndGroup="True">
                    </px:PXLayoutRule>
                    <px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1"
                        CommitChanges="True">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2"
                        CommitChanges="True">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edCity" runat="server" DataField="City" CommitChanges="True">
                    </px:PXTextEdit>
                    <px:PXSelector ID="edCountryID" runat="server" AllowEdit="True"
                        DataField="CountryID" CommitChanges="True"   >
                    </px:PXSelector>
                    <px:PXSelector ID="edState" runat="server" AllowEdit="True" DataField="State"
                        CommitChanges="True"   >
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" Merge="True">
                    </px:PXLayoutRule>
                    <px:PXMaskEdit ID="edPostalCode" runat="server" DataField="PostalCode" Size="S"
                        CommitChanges="True">
                    </px:PXMaskEdit>
                    <px:PXButton ID="btnViewDirectionOnMap" runat="server" Height="21px"
                        Text="View on Map" Width="94px">
                        <AutoCallBack Command="ViewDirectionOnMap" Target="ds">
                        </AutoCallBack>
                    </px:PXButton>
                    <px:PXLayoutRule runat="server" EndGroup="True">
                    </px:PXLayoutRule>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Appointments" BindingContext="mainForm" LoadOnDemand="True" RepaintOnDemand="False">
                <Template>
                    <px:PXGrid ID="PXGrid1" runat="server" DataSourceID="ds" FilesIndicator="False"
                        NoteIndicator="False" SkinID="Inquire" TabIndex="2300" Height="100%"
                        Width="100%">
                        <Levels>
                            <px:PXGridLevel DataMember="ServiceOrderAppointments" DataKeyNames="SrvOrdType,RefNbr">
                                <RowTemplate>
                                    <px:PXSelector ID="RefNbr" runat="server" DataField="RefNbr" AllowEdit="True">
                                    </px:PXSelector>
                                    <px:PXCheckBox ID="Confirmed" runat="server" DataField="Confirmed"
                                        Text="Confirmed">
                                    </px:PXCheckBox>
                                    <px:PXDropDown ID="Status" runat="server" DataField="Status" Enabled="False">
                                    </px:PXDropDown>
                                    <px:PXDateTimeEdit ID="ScheduledDateTimeBegin_Date" runat="server"
                                        DataField="ScheduledDateTimeBegin_Date">
                                    </px:PXDateTimeEdit>
                                    <px:PXDateTimeEdit ID="ScheduledDateTimeBegin_Time" runat="server"
                                        DataField="ScheduledDateTimeBegin_Time">
                                    </px:PXDateTimeEdit>
                                    <px:PXDateTimeEdit ID="ScheduledDateTimeEnd_Time" runat="server"
                                        DataField="ScheduledDateTimeEnd_Time">
                                    </px:PXDateTimeEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="RefNbr" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Confirmed" Width="80px" TextAlign="Center"
                                        Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ScheduledDateTimeBegin_Date" Width="90px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ScheduledDateTimeBegin_Time" Width="90px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ScheduledDateTimeEnd_Time" Width="90px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <ActionBar ActionsText="False" PagerVisible="False">
                            <CustomItems>
                                <px:PXToolBarButton Key="cmdOpenAppointmentScreen">
                                    <AutoCallBack Target="ds" Command="OpenAppointmentScreen"></AutoCallBack>
                                    <PopupCommand Target="PXGrid1" Command="Refresh"></PopupCommand>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Invoice Info" BindingContext="mainForm" LoadOnDemand="True" RepaintOnDemand="False">
                <Template>
                    <px:PXFormView ID="InvoiceFlags" runat="server" DataMember="ServiceOrderRelated"
                    RenderStyle="Simple" SkinID="Transparent">
                        <Template>
                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
                            <px:PXCheckBox ID="edMem_Invoiced" runat="server" DataField="Mem_Invoiced" AlignLeft="True"/>
                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
                            <px:PXCheckBox ID="edAllowInvoice" runat="server" DataField="AllowInvoice" AlignLeft="True"/>
                        </Template>
                    </px:PXFormView>
                    <px:PXGrid ID="PXGridPostInfo" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Height="130px" SkinID="Inquire" AdjustPageSize="Auto"
                        TabIndex="2300" SyncPosition="True" KeepPosition="True">
                        <Levels>
                            <px:PXGridLevel DataMember="ServiceOrderPostedIn">
                                <RowTemplate>
                                    <px:PXSelector ID="edFSPostBatch__BatchNbr" runat="server" DataField="FSPostBatch__BatchNbr" AllowEdit="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edMem_PostedIn" runat="server" DataField="Mem_PostedIn">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edMem_DocType" runat="server" DataField="Mem_DocType">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edMem_DocNbr" runat="server" DataField="Mem_DocNbr">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="FSPostBatch__BatchNbr" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_PostedIn" Width="50px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_DocType" Width="50px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_DocNbr" Width="100px" LinkCommand="OpenPostingDocument">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Source Info">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXDropDown ID="edSourceType" runat="server" DataField="SourceType"
                        AllowEdit="True" Enabled="False" Size="M">
                    </px:PXDropDown>
                    <px:PXTextEdit ID="edSourceDocType" runat="server" DataField="SourceDocType"
                        Enabled="False" Size="M">
                    </px:PXTextEdit>
                    <px:PXLayoutRule runat="server" Merge="True">
                    </px:PXLayoutRule>
                    <px:PXTextEdit ID="edSourceRefNbr" runat="server" DataField="SourceRefNbr"
                        Enabled="False" Size="S">
                        <LinkCommand Target="ds" Command="OpenSource"></LinkCommand>
                    </px:PXTextEdit>
                    <px:PXLayoutRule runat="server">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edServiceContractID" runat="server" DataField="ServiceContractID"
                        AllowEdit="True" DataSourceID="ds" Enabled="False">
                    </px:PXSelector>
                    <px:PXSelector ID="edScheduleID" runat="server" DataField="ScheduleID"
                        AllowEdit="True" Enabled="False" Size="S">
                    </px:PXSelector>
					<px:PXTextEdit ID="edRecurrenceDescription" runat="server" DataField="ScheduleRecord.RecurrenceDescription" Enabled="false">
                    </px:PXTextEdit>
                </Template>
                <ContentLayout ControlSize="M" LabelsWidth="150px" />
            </px:PXTabItem>
            <px:PXTabItem Text="Comment">
                <Template>
                    <px:PXRichTextEdit ID="edLongDescr" runat="server" DataField="LongDescr"
                        Style="width: 100%;height: 120px" AllowAttached="true" AllowSearch="true"
                        AllowMacros="true" AllowLoadTemplate="false" AllowSourceMode="true">
                        <AutoSize Enabled="True" MinHeight="216" />
                    </px:PXRichTextEdit>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Staff" BindingContext="mainForm" LoadOnDemand="True" RepaintOnDemand="False">
                <Template>
                    <px:PXGrid ID="staffGrid" runat="server" DataSourceID="ds" FilesIndicator="False"
                        NoteIndicator="False" SkinID="DetailsInTab" TabIndex="2300" Height="100%"
                        Width="100%">
                        <Levels>
                            <px:PXGridLevel DataMember="ServiceOrderEmployees">
                                <RowTemplate>
                                    <px:PXSegmentMask ID="edEmployeeID2" runat="server" DataField="EmployeeID" AutoRefresh="True" CommitChanges="True" AllowEdit="True">
                                    </px:PXSegmentMask>
                                    <px:PXTextEdit ID="edType" runat="server" DataField="Type">
                                    </px:PXTextEdit>
                                    <px:PXSelector ID="edServiceLineRef" runat="server" CommitChanges="True" DataField="ServiceLineRef" AutoRefresh="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edFSSODetEmployee__ServiceID" runat="server" DataField="FSSODetEmployee__ServiceID" Enabled="False" AutoRefresh="True" AllowEdit="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edFSSODetEmployee__TranDesc" runat="server" DataField="FSSODetEmployee__TranDesc" Enabled="False">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edComment" runat="server" DataField="Comment">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="EmployeeID" Width="200px" CommitChanges="True" DisplayMode="Hint">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Type" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ServiceLineRef" Width="85px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FSSODetEmployee__ServiceID" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FSSODetEmployee__TranDesc" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Comment" Width="200px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <ActionBar ActionsText="False" PagerVisible="False">
                            <CustomItems>
                                <px:PXToolBarButton Key="cmdOpenStaffSelectorFromStaffTab">
                                    <AutoCallBack Target="ds" Command="OpenStaffSelectorFromStaffTab" ></AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Resource Equipment" BindingContext="mainForm" LoadOnDemand="True" RepaintOnDemand="False">
                <Template>
                    <px:PXGrid ID="PXGrid3" runat="server" DataSourceID="ds" FilesIndicator="False"
                        NoteIndicator="False" SkinID="Details" TabIndex="2300" Height="100%"
                        Width="100%">
                        <Levels>
                            <px:PXGridLevel DataMember="ServiceOrderEquipment"  DataKeyNames="SOID,SMEquipmentID">
                                <RowTemplate>
                                    <px:PXSelector ID="edRSMEquipmentID" runat="server" DataField="SMEquipmentID"
                                        AllowEdit="True" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edRFSEquipment__Descr" runat="server"
                                        DataField="FSEquipment__Descr">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edCommentEquiment" runat="server" DataField="Comment">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="SMEquipmentID" Width="150px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FSEquipment__Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Comment" Width="200px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Attendees" BindingContext="mainForm" LoadOnDemand="True" RepaintOnDemand="False">
                <Template>
                    <px:PXGrid ID="PXGridAttendees" runat="server" DataSourceID="ds" FilesIndicator="False"
                        NoteIndicator="False" SkinID="DetailsInTab" TabIndex="2300" Height="100%"
                        Width="100%">
                        <Levels>
                            <px:PXGridLevel DataMember="ServiceOrderAttendees">

                                <RowTemplate>
                                    <px:PXSegmentMask ID="edCustomerID2" runat="server" DataField="CustomerID" CommitChanges="True" AllowEdit="True">
                                    </px:PXSegmentMask>
                                    <px:PXSelector ID="edContactID" runat="server" DataField="ContactID" CommitChanges="True" AutoRefresh="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edMem_CustomerContactName" runat="server" DataField="Mem_CustomerContactName">
                                    </px:PXTextEdit>
                                    <px:PXCheckBox ID="edConfirmed" runat="server" DataField="Confirmed"
                                        Text="Confirmed">
                                    </px:PXCheckBox>
                                    <px:PXTextEdit ID="edMem_EMail" runat="server" DataField="Mem_EMail">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edMem_Phone1" runat="server" DataField="Mem_Phone1">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edCommentAttendee" runat="server" DataField="Comment">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="CustomerID" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ContactID" Width="200px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_CustomerContactName" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Confirmed" TextAlign="Center" Type="CheckBox"
                                        Width="60px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_EMail" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_Phone1" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Comment" Width="200px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <ActionBar ActionsText="False" PagerVisible="False">
                            <CustomItems>
                                <px:PXToolBarButton Key="createNewCustomer" >
                                    <AutoCallBack Target="ds" Command="CreateNewCustomer" ></AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Related Service Orders" BindingContext="mainForm" LoadOnDemand="True" RepaintOnDemand="False">
                <Template>
                    <px:PXGrid ID="edRelatedServiceOrders" runat="server" DataSourceID="ds" FilesIndicator="False"
                        NoteIndicator="False" SkinID="DetailsInTab" TabIndex="2300" Height="100%" KeepPosition="True"
                        Width="100%" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataMember="RelatedServiceOrders">
                                <RowTemplate>
                                    <px:PXSelector ID="auxSrvOrdType" runat="server" DataField="SrvOrdType" AllowEdit="True"></px:PXSelector>
                                    <px:PXSelector ID="auxRefNbr" runat="server" DataField="RefNbr" AllowEdit="True"></px:PXSelector>
                                    <px:PXTextEdit ID="edDocDesc" runat="server" DataField="DocDesc">
                                    </px:PXTextEdit>
                                    <px:PXDropDown ID="auxStatus" runat="server" DataField="Status" Enabled="False"></px:PXDropDown>
                                    <px:PXDateTimeEdit ID="auxOrderDate" runat="server"
                                        DataField="OrderDate">
                                    </px:PXDateTimeEdit>
                                </RowTemplate>
                                <Columns>
                                 <px:PXGridColumn DataField="SrvOrdType" Width="140px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="RefNbr" Width="120px" LinkCommand="OpenServiceOrderScreen">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="DocDesc" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="OrderDate" Width="120px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <ActionBar ActionsText="False" PagerVisible="False"></ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXTab>
</asp:Content>
