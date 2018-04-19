<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="FS300200.aspx.cs" Inherits="Page_FS300200" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True"
        BorderStyle="NotSet" PrimaryView="AppointmentRecords" SuspendUnloading="False"
        TypeName="PX.Objects.FS.AppointmentEntry" style="float:left">
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand Name="Delete" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="ViewDirectionOnMap" PopupCommand=""
                PopupCommandTarget="" PopupPanel="" Text="" Visible="False" />
            <px:PXDSCallbackCommand Name="ViewStartGPSOnMap" PopupCommand=""
                PopupCommandTarget="" PopupPanel="" Text="" Visible="False" />
            <px:PXDSCallbackCommand Name="ViewCompleteGPSOnMap" PopupCommand=""
                PopupCommandTarget="" PopupPanel="" Text="" Visible="False" />
            <px:PXDSCallbackCommand Name="ValidateAddress" Visible="False" />
            <px:PXDSCallbackCommand Name="openStaffSelectorFromStaffTab" Visible="False" RepaintControls="All" />
            <px:PXDSCallbackCommand Name="openStaffSelectorFromServiceTab" Visible="False" RepaintControls="All" />
            <px:PXDSCallbackCommand Name="OpenServiceSelector" Visible="False" RepaintControls="All" />
            <px:PXDSCallbackCommand Name="createNewCustomer" PopupCommand=""
                PopupCommandTarget="" PopupPanel="" Text="" Visible="False" />
            <px:PXDSCallbackCommand Name="SelectCurrentService" Visible="False" />
            <px:PXDSCallbackCommand Name="SelectCurrentStaff" Visible="False" />
            <px:PXDSCallbackCommand Name="OpenPostingDocument" Visible="False" />
			<px:PXDSCallbackCommand Name="startService" Visible="False" />
			<px:PXDSCallbackCommand Name="completeService" Visible="False" />
		</CallbackCommands>
        <DataTrees>
            <px:PXTreeDataMember TreeView="TreeWFStages" TreeKeys="WFStageID" />
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
      <px:PXLabel runat="server" Height="30px"></px:PXLabel>
      <px:PXLabel runat="server" Height="66px"></px:PXLabel>
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
    <px:PXToolBar ID="toolbar1" runat="server" SkinID="Navigation" BackColor="Transparent" CommandSourceID="ds">
        <Items>
            <px:PXToolBarSeperator ></px:PXToolBarSeperator>
        </Items>
        <Layout ItemsAlign="Left" ></Layout>
    </px:PXToolBar>
    <div style="clear: left" ></div>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="mainForm" runat="server" DataSourceID="ds" Style="z-index: 100"  NotifyIndicator="True" FilesIndicator="True"
		Width="100%" DataMember="AppointmentRecords" TabIndex="29900" AllowCollapse="True" MarkRequired="Dynamic">
        <Template>
            <px:PXLayoutRule runat="server" StartRow="True" StartColumn="True" ControlSize="S">
            </px:PXLayoutRule>
            <px:PXSelector ID="edSrvOrdType" runat="server"
                DataField="SrvOrdType" DataSourceID="ds"
                AllowEdit="True">
            </px:PXSelector>
            <px:PXSelector ID="edRefNbr" runat="server" DataField="RefNbr" AutoRefresh="True" DataSourceID="ds">
            </px:PXSelector>
            <px:PXSelector ID="edSORefNbr" runat="server" DataField="SORefNbr"
                DataSourceID="ds" NullText=" <NEW>" AutoRefresh="True" AllowEdit="True"
                CommitChanges="True">
            </px:PXSelector>
            <px:PXTreeSelector CommitChanges="True" ID="edWFStageID" runat="server" DataField="WFStageID"
                TreeDataMember="TreeWFStages" TreeDataSourceID="ds" PopulateOnDemand="True" InitialExpandLevel="0" ShowRootNode="False">
                <DataBindings>
                    <px:PXTreeItemBinding TextField="WFStageCD" ValueField="WFStageCD">
                    </px:PXTreeItemBinding>
                </DataBindings>
            </px:PXTreeSelector>
            <px:PXLabel runat="server" />
            <px:PXLabel runat="server" />
            <px:PXLayoutRule runat="server" ColumnSpan="2">
            </px:PXLayoutRule>
            <px:PXTextEdit ID="edDocDesc" runat="server" DataField="DocDesc" CommitChanges="True">
            </px:PXTextEdit>
            <px:PXLayoutRule runat="server"
                StartColumn="True">
            </px:PXLayoutRule>
            <px:PXFormView ID="ServiceOrderHeader" runat="server"
                Caption="ServiceOrder Header" DataMember="ServiceOrderRelated" MarkRequired="Dynamic"
                DataSourceID="ds" RenderStyle="Simple" TabIndex="1500" DefaultControlID="edCustomerID">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="150px">
                    </px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" Merge="True">
                    </px:PXLayoutRule>
                    <px:PXSegmentMask ID="edCustomerID" runat="server" AllowEdit="True"
                        CommitChanges="True" DataField="CustomerID" DataSourceID="ds">
                    </px:PXSegmentMask>
                    <px:PXLayoutRule runat="server">
                    </px:PXLayoutRule>
                    <px:PXSegmentMask ID="edLocationID" runat="server" DataField="LocationID"
                            CommitChanges="True" AutoRefresh="True" AllowEdit="True">
                    </px:PXSegmentMask>
                    <px:PXSelector ID="edBranchID" runat="server" DataField="BranchID"
                        DataSourceID="ds" CommitChanges="True">
                    </px:PXSelector>
                    <px:PXSelector ID="edBranchLocationID" runat="server" DataField="BranchLocationID"
                        DataSourceID="ds" AllowEdit="True" CommitChanges="True"
                        AutoRefresh="True">
                    </px:PXSelector>
                    <px:PXSegmentMask ID="edProjectID" runat="server" AutoRefresh="True"
                        CommitChanges="True" DataField="ProjectID" AllowEdit="True">
                    </px:PXSegmentMask>
                    <px:PXSelector ID="edRoomID" runat="server" DataField="RoomID"
                        AllowEdit="True" DataSourceID="ds" AutoRefresh="True" CommitChanges="True">
                    </px:PXSelector>
                </Template>
            </px:PXFormView>
            <px:PXSelector ID="edDfltProjectTaskID" runat="server" DataField="DfltProjectTaskID"
                DisplayMode="Value" AllowEdit = "True" AutoRefresh="True" CommitChanges="True">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXDropDown ID="Status" runat="server" DataField="Status" Enabled="False">
            </px:PXDropDown>
            <px:PXCheckBox ID="edHold" runat="server" CommitChanges="True"
                DataField="Hold">
            </px:PXCheckBox>
            <px:PXCheckBox ID="Confirmed" runat="server" CommitChanges="True"
                DataField="Confirmed" Text="Confirmed">
            </px:PXCheckBox>
            <px:PXCheckBox ID="edUnreachedCustomer" runat="server" CommitChanges="True"
                DataField="UnreachedCustomer">
            </px:PXCheckBox>
            <px:PXCheckBox ID="edTimeRegistered" runat="server" CommitChanges="True" Enabled="False"
                DataField="TimeRegistered">
            </px:PXCheckBox>
            <px:PXCheckBox ID="edValidatedByDispatcher" runat="server" DataField="ValidatedByDispatcher">
            </px:PXCheckBox>
			<px:PXNumberEdit ID="edEstimatedLineTotal" runat="server" DataField="EstimatedLineTotal" LabelWidth="120px">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edLineotal" runat="server" DataField="LineTotal" LabelWidth="120px">
            </px:PXNumberEdit>
            <px:PXCheckBox ID="edIsRouteAppoinment" runat="server"
                DataField="IsRouteAppoinment">
            </px:PXCheckBox>
            <px:PXCheckBox ID="edShowAttendees" runat="server"
                DataField="Mem_ShowAttendees">
            </px:PXCheckBox>
            <px:PXLayoutRule runat="server" LabelsWidth="SM" StartRow="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" StartRow="True">
            </px:PXLayoutRule>
            <px:PXPanel ID="PXPanel1" runat="server" RenderSimple="True"
                RenderStyle="Simple">
                <px:PXLayoutRule runat="server" StartColumn="True">
                </px:PXLayoutRule>
                <px:PXLayoutRule runat="server" GroupCaption="Scheduled Date and Time">
                </px:PXLayoutRule>
                <px:PXDateTimeEdit ID="ScheduledDateTimeBegin_Date" runat="server"
                    CommitChanges="True" DataField="ScheduledDateTimeBegin_Date">
                </px:PXDateTimeEdit>
                <px:PXDateTimeEdit ID="ScheduledDateTimeBegin_Time" runat="server"
                    CommitChanges="True" DataField="ScheduledDateTimeBegin_Time" TimeMode="True">
                </px:PXDateTimeEdit>
                <px:PXDateTimeEdit ID="ScheduledDateTimeEnd_Time" runat="server"
                    CommitChanges="True" DataField="ScheduledDateTimeEnd_Time" TimeMode="True">
                </px:PXDateTimeEdit>
				<px:PXCheckBox ID="edHandleManuallyScheduleTime" runat="server"
                    DataField="HandleManuallyScheduleTime" AlignLeft="False">
                </px:PXCheckBox>
                <px:PXMaskEdit ID="edEstimatedDurationTotal" runat="server"
                    DataField="EstimatedDurationTotal" Enabled="False" Size="S">
                </px:PXMaskEdit>
                <px:PXLayoutRule runat="server" StartColumn="True"
                    ControlSize="SM">
                </px:PXLayoutRule>
                <px:PXLayoutRule runat="server" GroupCaption="Actual Date and Time">
                </px:PXLayoutRule>
                <px:PXDateTimeEdit ID="edExecutionDate" runat="server"
                    CommitChanges="True" DataField="ExecutionDate">
                </px:PXDateTimeEdit>
                <px:PXDateTimeEdit ID="edActualDateTimeBegin_Time" runat="server"
                    CommitChanges="True" DataField="ActualDateTimeBegin_Time" TimeMode="True">
                </px:PXDateTimeEdit>
                <px:PXDateTimeEdit ID="edActualDateTimeEnd_Time" runat="server"
                    CommitChanges="True" DataField="ActualDateTimeEnd_Time" TimeMode="True">
                </px:PXDateTimeEdit>
				<px:PXCheckBox ID="edHandleManuallyActualTime" runat="server"
                    DataField="HandleManuallyActualTime" AlignLeft="False">
                </px:PXCheckBox>
                <px:PXMaskEdit ID="edActualDurationTotal" runat="server"
                    DataField="ActualDurationTotal" Enabled="False" Size="S">
                </px:PXMaskEdit>
            </px:PXPanel>
        </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds"
        DataMember="AppointmentSelected">
		<Items>
            <px:PXTabItem Text="Services">
                <Template>
                      <px:PXGrid ID="PXGrid4" runat="server" DataSourceID="ds" SkinID="DetailsInTab"
                        TabIndex="1700" Height="100%" Width="100%" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel
                                DataMember="AppointmentDetServices" DataKeyNames="AppointmentID,AppDetID,SODetID">
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
                                                <px:PXLabel ID="PXLabel1" runat="server"></px:PXLabel>
                                                <px:PXGrid ID="PXGridSelectedEmployees" runat="server" DataSourceID="ds"
                                                    Style="z-index: 100" Width="100%" SkinID="Inquire" TabIndex="800"
                                                    SyncPosition="True" AllowPaging="True" AdjustPageSize="Auto" Height="200px">
                                                    <Levels>
                                                        <px:PXGridLevel
                                                            DataMember="EmployeeGridFilter" DataKeyNames="EmployeeID">
                                                            <Columns>
                                                                <px:PXGridColumn
                                                                    DataField="Mem_Selected" TextAlign="Center" Type="CheckBox" Width="80px" CommitChanges="True">
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
                                                <px:PXGridLevel DataMember="ServiceRecords" DataKeyNames="InventoryCD">
                                                    <Columns>
                                                        <px:PXGridColumn DataField="InventoryCD" Width="120px">
                                                        </px:PXGridColumn>
                                                        <px:PXGridColumn DataField="Descr" Width="200px">
                                                        </px:PXGridColumn>
                                                    </Columns>
                                                </px:PXGridLevel>
                                            </Levels>
                                            <AutoSize Enabled="True" MinHeight="250"/>
                                            <ActionBar ActionsText="False" DefaultAction="SelectCurrentService" PagerVisible="False">
                                                <CustomItems>
                                                    <px:PXToolBarButton Key="SelectCurrentService">
					                                    <AutoCallBack Target="ds" Command="SelectCurrentService" />
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
                                    <px:PXSelector ID="edSODetID" runat="server" DataField="SODetID"
                                        CommitChanges="True" NullText="<NEW>" AutoRefresh="True">
                                    </px:PXSelector>
                                    <px:PXDropDown ID="Status" runat="server" DataField="Status" CommitChanges="True">
                                    </px:PXDropDown>
                                    <px:PXDropDown ID="edLineType" runat="server" CommitChanges="True"
                                        DataField="LineType">
                                    </px:PXDropDown>
                                    <px:PXSegmentMask ID="edServiceID" runat="server" DataField="ServiceID"
                                            AllowEdit ="True" CommitChanges="True" AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXDropDown ID="edBillingRule" runat="server" DataField="BillingRule" Size="SM">
                                    </px:PXDropDown>
                                    <px:PXTextEdit ID="edTranDesc" runat="server" DataField="TranDesc">
                                    </px:PXTextEdit>
                                    <px:PXDropDown ID="edEquipmentAction" runat="server" DataField="EquipmentAction">
                                    </px:PXDropDown>
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
                                    <px:PXMaskEdit ID="edEstimatedDuration" runat="server"
                                        DataField="EstimatedDuration" CommitChanges="True">
                                    </px:PXMaskEdit>
									<px:PXNumberEdit ID="edEstimatedQty" runat="server" DataField="EstimatedQty" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXCheckBox ID="edKeepActualDateTimes" runat="server" DataField="KeepActualDateTimes">
                                    </px:PXCheckBox>
                                    <px:PXDateTimeEdit ID="edActualDateTimeBegin_Time2" runat="server"
                                        DataField="ActualDateTimeBegin_Time" TimeMode="True" CommitChanges="True" />
                                    <px:PXDateTimeEdit ID="edActualDateTimeEnd_Time2" runat="server"
                                        DataField="ActualDateTimeEnd_Time" TimeMode="True" CommitChanges="True" />
                                    <px:PXMaskEdit ID="edActualDuration" runat="server" DataField="ActualDuration" CommitChanges="True">
                                    </px:PXMaskEdit>
                                    <px:PXNumberEdit ID="Qty" runat="server" DataField="Qty" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edUnitPrice" runat="server" DataField="UnitPrice" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="TranAmt" runat="server" DataField="TranAmt">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edProjectTaskID" runat="server" DataField="ProjectTaskID" AutoRefresh="True" CommitChanges="True" DisplayMode="Value" AllowEdit = "True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edSourceSalesOrderRefNbr" runat="server" AllowEdit="True" DataField="SourceSalesOrderRefNbr"  >
                                    </px:PXSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="SODetID" CommitChanges="True" NullText="<NEW>" Width="85px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LineType" CommitChanges="True" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ServiceID" CommitChanges="True" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="BillingRule" CommitChanges="True" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TranDesc" Width="200px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SMEquipmentID" Width="100px" AutoCallBack = "True" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="NewTargetEquipmentLineNbr" TextAlign="Right" CommitChanges="True" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ComponentID" TextAlign="Right" CommitChanges="True" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EquipmentLineRef" TextAlign="Right" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="StaffID" Width="120px" AutoCallBack = "True"  NullText="<SPLIT>">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Warranty" Width="70px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="IsBillable" TextAlign="Center" Type="CheckBox" Width="65px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="IsPrepaid" TextAlign="Center" Type="CheckBox" Width="65px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="AcctID" Width="108px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SubID" Width="180px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EstimatedDuration" Width="95px">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="EstimatedQty" TextAlign="Right" Width="75px" CommitChanges="true">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="KeepActualDateTimes" TextAlign="Center" Type="CheckBox" Width="75px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActualDateTimeBegin_Time" CommitChanges="True" Width="80px" NullText="<SPLIT>">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActualDateTimeEnd_Time" CommitChanges="True" Width="80px" NullText="<SPLIT>">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActualDuration" Width="95px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" TextAlign="Right" Width="70px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UnitPrice" Width="100px" TextAlign="Right" CommitChanges="True">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="EstimatedTranAmt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TranAmt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ProjectTaskID" CommitChanges="True" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SourceSalesOrderRefNbr" Width="120px">
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
                                <px:PXToolBarButton Key="cmdOpenStaffSelectorFromServiceTab">
                                    <AutoCallBack Target="ds" Command="OpenStaffSelectorFromServiceTab" ></AutoCallBack>
                                </px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdStartService">
                                    <AutoCallBack Target="ds" Command="startService" ></AutoCallBack>
                                </px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdCompleteService">
                                    <AutoCallBack Target="ds" Command="completeService" ></AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Inventory Items">
                <Template>
                      <px:PXGrid ID="PXGrid4" runat="server" DataSourceID="ds" SkinID="DetailsInTab"
                        TabIndex="1700" Height="100%" Width="100%" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel
                                DataMember="AppointmentDetParts" DataKeyNames="AppointmentID,AppDetID,SODetID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" GroupCaption="Detail Info"
                                        StartGroup="True">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="edSODetID2" runat="server" DataField="SODetID"
                                        CommitChanges="True" NullText="<NEW>" AutoRefresh="True">
                                    </px:PXSelector>
                                    <px:PXDropDown ID="edStatus2" runat="server" DataField="Status">
                                    </px:PXDropDown>
                                    <px:PXDropDown ID="edLineType2" runat="server" DataField="LineType"
                                        CommitChanges="True">
                                    </px:PXDropDown>
                                    <px:PXSegmentMask ID="InventoryID2" runat="server" DataField="InventoryID" CommitChanges="True"
                                        AllowEdit="True">
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
                                    <px:PXCheckBox ID="edIsBillable2" runat="server" DataField="IsBillable">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edIsPrepaid2" runat="server" DataField="IsPrepaid">
                                    </px:PXCheckBox>
                                    <px:PXSegmentMask ID="edAcctID2" runat="server" CommitChanges="True" DataField="AcctID" AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edSubID2" runat="server" DataField="SubID" AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edSiteID2" runat="server" DataField="SiteID" AllowEdit="True" AutoRefresh="True" CommitChanges="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="PXGrid4" Name="FSAppointmentDet.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" ></px:PXControlParam>
                                            <px:PXControlParam ControlID="PXGrid4" Name="FSAppointmentDet.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" ></px:PXControlParam>
                                         </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edSiteLocationID" runat="server" DataField="SiteLocationID" AutoRefresh="True" CommitChanges="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="PXGrid4" Name="FSAppointmentDet.siteID" PropertyName="DataValues[&quot;SiteID&quot;]" Type="String" ></px:PXControlParam>
                                        </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXSelector ID="edUOM2" runat="server" DataField="UOM">
                                    </px:PXSelector>
									<px:PXNumberEdit ID="edEstimatedQty2" runat="server" DataField="EstimatedQty" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="Qty2" runat="server" DataField="Qty" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edUnitPrice2" runat="server" DataField="UnitPrice" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="TranAmt2" runat="server" DataField="TranAmt">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edProjectTaskID2" runat="server"
                                        DataField="ProjectTaskID" AutoRefresh="True" CommitChanges="True"  AllowEdit="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edSourceSalesOrderRefNbr2" runat="server" AllowEdit="True" DataField="SourceSalesOrderRefNbr"  >
                                    </px:PXSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="SODetID" CommitChanges="True" NullText="<NEW>" Width="85px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LineType" Width="100px" CommitChanges="True">
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
                                    <px:PXGridColumn DataField="IsBillable" TextAlign="Center" Type="CheckBox" Width="65px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="IsPrepaid" TextAlign="Center" Type="CheckBox" Width="65px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="AcctID" Width="108px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SubID" Width="180px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SiteID" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SubItemID" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SiteLocationID" AllowShowHide="Server" Width="81px" NullText="<SPLIT>" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UOM">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="EstimatedQty" TextAlign="Right" Width="75px" CommitChanges="true">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" Width="75px" TextAlign="Right" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UnitPrice" TextAlign="Right" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
									<px:PXGridColumn DataField="EstimatedTranAmt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TranAmt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ProjectTaskID" CommitChanges="True" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SourceSalesOrderRefNbr" Width="120px">
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
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXFormView runat="server" Caption="Customer Info" MarkRequired="Dynamic"
                        DataMember="ServiceOrderRelated" RenderStyle="Simple" DataSourceID="ds"
                        ID="CustomerInfoFormView">
                        <Template>
                            <px:PXLayoutRule runat="server" GroupCaption="Customer Info" StartGroup="True" ControlSize="SM" LabelsWidth="S">
                            </px:PXLayoutRule>
                            <px:PXSelector ID="edContactID" runat="server" DataField="ContactID"
                                AllowEdit="True" CommitChanges="True" AutoRefresh="True">
                            </px:PXSelector>
                        </Template>
                    </px:PXFormView>
                    <px:PXFormView runat="server" Caption="ServiceOrder Detail"
                        DataMember="ServiceOrderRelated" RenderStyle="Simple" DataSourceID="ds"
                        ID="ServiceOrderDetail">
                        <Template>
                            <px:PXLayoutRule runat="server" GroupCaption="Appointment Contact" ControlSize="SM" LabelsWidth="S">
                            </px:PXLayoutRule>
                            <px:PXTextEdit ID="edAttention" runat="server" DataField="Attention">
                            </px:PXTextEdit>
                            <px:PXMailEdit ID="edEMail" runat="server" DataField="EMail">
                            </px:PXMailEdit>
                            <px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1">
                            </px:PXMaskEdit >
                            <px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2">
                            </px:PXMaskEdit>
                            <px:PXMaskEdit ID="edFax" runat="server" DataField="Fax">
                            </px:PXMaskEdit >
                        </Template>
                    </px:PXFormView>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXFormView runat="server" Caption="Billing Info"
                        DataMember="ServiceOrderRelated" RenderStyle="Simple" DataSourceID="ds"
                        ID="BillingInfoFormView">
                        <Template>
                            <px:PXLayoutRule runat="server" GroupCaption="Billing Info" StartGroup="True" ControlSize="SM" LabelsWidth="S">
                            </px:PXLayoutRule>
                            <px:PXSegmentMask ID="edBillCustomerID" runat="server"
                                DataField="BillCustomerID" CommitChanges="True">
                            </px:PXSegmentMask>
                            <px:PXSegmentMask ID="edBillLocationID" runat="server"
                                DataField="BillLocationID">
                            </px:PXSegmentMask>
                        </Template>
                    </px:PXFormView>
                    <px:PXLayoutRule runat="server" GroupCaption="Commission" StartGroup="True" ControlSize="SM" LabelsWidth="S">
                    </px:PXLayoutRule>
                    <px:PXSegmentMask ID="edSalesPersonID" runat="server" DataField="SalesPersonID" CommitChanges="True" AutoRefresh="True"></px:PXSegmentMask>
                    <px:PXCheckBox ID="edCommissionable" runat="server" CommitChanges="True" DataField="Commissionable"></px:PXCheckBox>
                    <px:PXFormView runat="server" Caption="ServiceOrder Detail"
                        DataMember="ServiceOrderRelated" RenderStyle="Simple" DataSourceID="ds"
                        ID="ServiceOrderDetail2">
                        <Template>
                            <px:PXLayoutRule runat="server" GroupCaption="Appointment Address" StartGroup="True" ControlSize="SM" LabelsWidth="S">
                            </px:PXLayoutRule>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXCheckBox ID="edAddressValidated" runat="server"
                                DataField="AddressValidated" Text="Address Validated" AlignLeft="True" Enabled = "False">
                            </px:PXCheckBox>
                            <px:PXButton ID="btnValidateAddress" runat="server" Height="21px"
                                Text="Validate Address" Width="120px">
                                <AutoCallBack Command="ValidateAddress" Target="ds">
                                </AutoCallBack>
                            </px:PXButton>
                            <px:PXLayoutRule runat="server" EndGroup="True">
                            </px:PXLayoutRule>
                            <px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1" CommitChanges="True">
                            </px:PXTextEdit>
                            <px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2" CommitChanges="True">
                            </px:PXTextEdit>
                            <px:PXTextEdit ID="edCity" runat="server" DataField="City" CommitChanges="True">
                            </px:PXTextEdit>
                            <px:PXSelector ID="edCountryID" runat="server" DataField="CountryID"
                                AllowEdit="True" DataSourceID="ds" CommitChanges="True">
                            </px:PXSelector>
                            <px:PXSelector ID="edState" runat="server" DataField="State" AllowEdit="True"
                                DataSourceID="ds" CommitChanges="True">
                            </px:PXSelector>
                            <px:PXMaskEdit ID="edPostalCode" runat="server" DataField="PostalCode" Size="S" CommitChanges="True">
                            </px:PXMaskEdit>
                        </Template>
                    </px:PXFormView>
                </Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Staff">
                <Template>
                    <px:PXGrid ID="staffGrid" runat="server" DataSourceID="ds" SkinID="Details" SyncPosition="True" KeepPosition="True"
                        TabIndex="6500" Height="100%" Width="100%">
                        <Levels>
                            <px:PXGridLevel
                                DataMember="AppointmentEmployees" DataKeyNames="AppointmentID, LineNbr" >
                                <RowTemplate>
                                    <px:PXTextEdit ID="edEmployeeLineRef" runat="server" DataField="LineRef">
                                    </px:PXTextEdit>
                                    <px:PXSelector ID="EmployeeID" runat="server" DataField="EmployeeID"
                                        AllowEdit="True"
                                        CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edType" runat="server" DataField="Type">
                                    </px:PXTextEdit>
                                    <px:PXCheckBox ID="edIsDriver" runat="server" DataField="IsDriver"
                                        Text="IsDriver" Width="80px">
                                    </px:PXCheckBox>
                                    <px:PXSelector ID="edServiceLineRef" runat="server" CommitChanges="True" DataField="ServiceLineRef" AutoRefresh="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edFSAppointmentDetEmployee__ServiceID" runat="server" DataField="FSAppointmentDetEmployee__ServiceID" Enabled="False" AutoRefresh="True" AllowEdit="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edFSAppointmentDetEmployee__TranDesc" runat="server" DataField="FSAppointmentDetEmployee__TranDesc" Enabled="False">
                                    </px:PXTextEdit>
                                    <px:PXDateTimeEdit ID="edActualDateTimeBegin_Time3" runat="server"
                                        DataField="ActualDateTimeBegin_Time" TimeMode="True" CommitChanges="True" />
                                    <px:PXDateTimeEdit ID="edActualDateTimeEnd_Time3" runat="server"
                                        DataField="ActualDateTimeEnd_Time" TimeMode="True" CommitChanges="True" />
                                    <px:PXMaskEdit ID="edActualDuration3" runat="server" DataField="ActualDuration" CommitChanges="True">
                                    </px:PXMaskEdit>
                                    <px:PXSelector ID="edEarningType" runat="server" AutoRefresh="True" CommitChanges="True" DataField="EarningType" DataSourceID="ds">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="Comment" runat="server" DataField="Comment">
                                    </px:PXTextEdit>
                                    <px:PXCheckBox ID="edApprovedTime" runat="server" DataField="ApprovedTime"
                                        Text="Approved Time">
                                    </px:PXCheckBox>
                                    <px:PXSelector ID="edTimeCardCD" runat="server" DataField="TimeCardCD" AllowEdit ="True">
                                    </px:PXSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="LineRef" Width="85px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EmployeeID" Width="200px"
                                        AllowShowHide="False" CommitChanges="True" DisplayMode="Hint">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Type" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="IsDriver" TextAlign="Center" Type="CheckBox" Width="75px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ServiceLineRef" Width="85px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FSAppointmentDetEmployee__ServiceID" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FSAppointmentDetEmployee__TranDesc" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="KeepActualDateTimes" TextAlign="Center" Type="CheckBox" Width="75px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActualDateTimeBegin_Time" CommitChanges="True" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActualDateTimeEnd_Time" CommitChanges="True" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActualDuration" Width="95px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TrackTime" TextAlign="Center" Type="CheckBox" Width="75px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EarningType" Width="95px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Comment" Width="300px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ApprovedTime" Width="80px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TimeCardCD" Width="80px"/>
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
			<px:PXTabItem Text="Resource Equipment">
                <Template>
                    <px:PXGrid ID="PXGrid2" runat="server" DataSourceID="ds" SkinID="Details"
                        TabIndex="2100" Height="100%" Width="100%">
                        <Levels>
                            <px:PXGridLevel DataMember="AppointmentResources" DataKeyNames="AppointmentID,SMEquipmentID">
                                <RowTemplate>
                                    <px:PXSelector ID="edRSMEquipmentID" runat="server" DataField="SMEquipmentID"
                                        AllowEdit="True" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edFSEquipment__Descr" runat="server"
                                        DataField="FSEquipment__Descr">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="EComment" runat="server" DataField="Comment">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="SMEquipmentID" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FSEquipment__Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Comment" Width="400px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Location">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXFormView runat="server" Caption="Location"
                        DataMember="AppointmentSelected" RenderStyle="Simple" DataSourceID="ds"
                        ID="AppointmentSelectedFormView">
                        <Template>
                            <px:PXLayoutRule StartGroup="True" GroupCaption="Appointment Location" runat="server">
                            </px:PXLayoutRule>
                            <px:PXNumberEdit ID="edMapLatitude" runat="server" DataField="MapLatitude" CommitChanges = "True">
                            </px:PXNumberEdit>
                            <px:PXNumberEdit ID="edMapLongitude" runat="server" DataField="MapLongitude" CommitChanges = "True">
                            </px:PXNumberEdit>
                            <px:PXNumberEdit ID="edDriveTime" runat="server" DataField="DriveTime">
                            </px:PXNumberEdit>
                            <px:PXButton ID="btnViewDirectionOnMap" runat="server" Height="21px"
                                Text="View on Map" Width="94px">
                                <AutoCallBack Command="ViewDirectionOnMap" Target="ds">
                                </AutoCallBack>
                            </px:PXButton>
                            <px:PXLayoutRule runat="server" StartColumn="True">
                            </px:PXLayoutRule>
                            <px:PXLayoutRule StartGroup="True" GroupCaption="Start Location" runat="server">
                            </px:PXLayoutRule>
                            <px:PXNumberEdit ID="edGPSLatitudeStart" runat="server" DataField="GPSLatitudeStart" CommitChanges = "True">
                            </px:PXNumberEdit>
                            <px:PXNumberEdit ID="edGPSLongitudeStart" runat="server" DataField="GPSLongitudeStart" CommitChanges = "True">
                            </px:PXNumberEdit>
                            <px:PXButton ID="btnViewStartGPSOnMap" runat="server" Height="21px"
                                Text="View on Map" Width="94px">
                                <AutoCallBack Command="ViewStartGPSOnMap" Target="ds">
                                </AutoCallBack>
                            </px:PXButton>
                            <px:PXLayoutRule runat="server" StartColumn="True">
                            </px:PXLayoutRule>
                            <px:PXLayoutRule StartGroup="True" GroupCaption="Complete Location" runat="server">
                            </px:PXLayoutRule>
                            <px:PXNumberEdit ID="edGPSLatitudeComplete" runat="server" DataField="GPSLatitudeComplete" CommitChanges = "True">
                            </px:PXNumberEdit>
                            <px:PXNumberEdit ID="edGPSLongitudeComplete" runat="server" DataField="GPSLongitudeComplete" CommitChanges = "True">
                            </px:PXNumberEdit>
                            <px:PXButton ID="btnViewCompleteGPSOnMap" runat="server" Height="21px"
                                Text="View on Map" Width="94px">
                                <AutoCallBack Command="ViewCompleteGPSOnMap" Target="ds">
                                </AutoCallBack>
                            </px:PXButton>
                            <px:PXTextEdit ID="edMem_GPSLatitudeLongitude" runat="server"
                                DataField="Mem_GPSLatitudeLongitude" AlignLeft="True" Enabled = "False">
                            </px:PXTextEdit>

                        </Template>
                    </px:PXFormView>
                </Template>
            </px:PXTabItem>
		    <px:PXTabItem Text="Attendees" BindingContext="mainForm" LoadOnDemand="True" RepaintOnDemand="False">
                <Template>
                    <px:PXGrid ID="PXGridAttendees" runat="server" DataSourceID="ds" FilesIndicator="False"
                        NoteIndicator="False" SkinID="DetailsInTab" TabIndex="2300" Height="100%"
                        Width="100%">
                        <Levels>
                            <px:PXGridLevel DataMember="AppointmentAttendees" DataKeyNames="AppointmentID,AttendeeID">
                                <RowTemplate>
                                    <px:PXSegmentMask ID="edCustomerID" runat="server" DataField="CustomerID"
                                        CommitChanges="True"  AllowEdit="True">
                                    </px:PXSegmentMask>
                                    <px:PXSelector ID="edContactID" runat="server" DataField="ContactID"
                                        CommitChanges="True" AutoRefresh="True">
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
                                    <px:PXTextEdit ID="edComment" runat="server" DataField="Comment">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="CustomerID" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ContactID" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_CustomerContactName"  Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Confirmed" Width="60px" TextAlign="Center"
                                        Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_EMail" Width="150px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_Phone1" Width="120px">
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
            <px:PXTabItem Text="Invoice Info">
                <Template>
                    <px:PXGrid ID="PXGridPostInfo" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Height="130px" SkinID="Inquire" AdjustPageSize="Auto"
                        TabIndex="2300" SyncPosition="True" KeepPosition="True">
                        <Levels>
                            <px:PXGridLevel DataMember="AppointmentPostedIn">
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
                    <px:PXLayoutRule runat="server" StartRow="True">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edServiceContractID" runat="server" DataField="ServiceContractID" Enabled = "False" AllowEdit="True">
                    </px:PXSelector>
                    <px:PXSelector ID="edScheduleID" runat="server" DataField="ScheduleID" Enabled = "False" AllowEdit="True">
                    </px:PXSelector>
					<px:PXTextEdit ID="edRecurrenceDescription" runat="server" DataField="ScheduleRecord.RecurrenceDescription" Enabled="false">
                    </px:PXTextEdit>
                </Template>
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
	        <px:PXTabItem Text="Route Info" BindingContext="mainForm" VisibleExp="DataControls[&quot;edIsRouteAppoinment&quot;].Value == true">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edRouteID" runat="server" DataField="RouteID" AllowEdit="True" Enabled="false">
                    </px:PXSelector>
                    <px:PXSelector ID="edRouteDocumentID" runat="server" DataField="RouteDocumentID" Enabled="false" AllowEdit="true">
                    </px:PXSelector>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Pickup/Delivery Items" LoadOnDemand="True" RepaintOnDemand="False">
			    <Template>
                    <px:PXGrid ID="PXGrid1" runat="server" TabIndex="4900" DataSourceID="ds"
                        SkinID="DetailsInTab" Height="100%" Width="100%" SyncPosition="True" >
                        <Levels>
                            <px:PXGridLevel
                                DataMember="PickupDeliveryItems" DataKeyNames="AppointmentID, ServiceID, SODetID, InventoryID">
                                <RowTemplate>
                                    <px:PXSelector ID="edSODetID3" runat="server" CommitChanges="True" DataField="SODetID" AutoRefresh="True">
                                    </px:PXSelector>
                                    <px:PXSegmentMask ID="edServiceID2" runat="server" CommitChanges="True" AutoRefresh="True" DataField="ServiceID" AllowEdit="True">
                                    </px:PXSegmentMask>
                                    <px:PXDropDown ID="edMem_ServiceType" runat="server" DataField="Mem_ServiceType" Size="SM">
                                    </px:PXDropDown>
                                    <px:PXSegmentMask ID="edInventoryID2" runat="server" CommitChanges="True" DataField="InventoryID" AllowEdit="True" AutoRefresh="True">
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edSubItemID" runat="server" DataField="SubItemID" AutoRefresh="True" NullText="<SPLIT>">
                                            <Parameters>
                                                <px:PXControlParam ControlID="PXGrid1" Name="FSAppointmentInventoryItem.InventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]"
                                                    Type="String" ></px:PXControlParam>
                                            </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXTextEdit ID="edTranDesc3" runat="server" DataField="TranDesc">
                                    </px:PXTextEdit>
                                    <px:PXSegmentMask ID="edSiteID3" runat="server" DataField="SiteID" AllowEdit="True">
                                    </px:PXSegmentMask>
                                    <px:PXSelector ID="edUOM3" runat="server" AutoRefresh="True" DataField="UOM">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edQty2" runat="server" DataField="Qty" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edUnitPrice3" runat="server" DataField="UnitPrice" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edTranAmt2" runat="server" DataField="TranAmt">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edProjectTaskID3" runat="server" DataField="ProjectTaskID" AllowEdit="True">
                                    </px:PXSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn CommitChanges="True" DataField="SODetID" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ServiceID" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_ServiceType" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="InventoryID" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SubItemID" DisplayFormat="&gt;AA-A" Width="45px" NullText="<SPLIT>" AutoCallBack="true" >
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TranDesc" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SiteID" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UOM" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" Width="100px" TextAlign="Right" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UnitPrice" TextAlign="Right" Width="120px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TranAmt" TextAlign="Right" Width="150px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ProjectTaskID" Width="150px" CommitChanges="True">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode InitNewRow="True"/>
                        <AutoSize Enabled="True" />
                    </px:PXGrid>
                </Template>
			</px:PXTabItem>
            <px:PXTabItem Text="Signature">
                <Template>
                    <px:PXLayoutRule runat="server" StartRow="True" LabelsWidth="M" ControlSize="XM"></px:PXLayoutRule>
                    <px:PXTextEdit ID="edFullNameSignature" runat="server" DataField="FullNameSignature">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edAdditionalCommentsCustomer" runat="server" DataField="AdditionalCommentsCustomer">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edAdditionalCommentsStaff" runat="server" DataField="AdditionalCommentsStaff">
                    </px:PXTextEdit>
                    <px:PXCheckBox ID="edAgreementSignature" runat="server" AlignLeft="True" DataField="AgreementSignature">
                    </px:PXCheckBox>
				</Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Delivery Notes" BindingContext="mainForm" VisibleExp="DataControls[&quot;edIsRouteAppoinment&quot;].Value == true">
                <Template>
                    <px:PXRichTextEdit ID="edDeliveryNotes" runat="server" DataField="DeliveryNotes"
                        Style="width: 100%;height: 120px" AllowAttached="true" AllowSearch="true"
                        AllowMacros="true" AllowLoadTemplate="false" AllowSourceMode="true">
                        <AutoSize Enabled="True" MinHeight="216" />
                    </px:PXRichTextEdit>
                </Template>
			</px:PXTabItem>
		</Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXTab>
</asp:Content>
