<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="EP305000.aspx.cs" Inherits="Page_EP305000"
    Title="Time Card" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="true" Width="100%" TypeName="PX.Objects.EP.TimeCardMaint" PrimaryView="Document">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Cancel" PopupVisible="true" />
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" PopupVisible="True" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="Approve" Visible="false" />
            <px:PXDSCallbackCommand Name="Reject" Visible="false" />
            <px:PXDSCallbackCommand Name="Release" Visible="false" />
            <px:PXDSCallbackCommand Name="Correct" Visible="false" />
            <px:PXDSCallbackCommand Name="Submit" Visible="false" />
            <px:PXDSCallbackCommand Name="Edit" Visible="false" />
            <px:PXDSCallbackCommand Name="PreloadFromTasks" Visible="false" />
            <px:PXDSCallbackCommand Name="PreloadFromPreviousTimecard" Visible="false" />
            <px:PXDSCallbackCommand Name="PreloadHolidays" Visible="false" />
            <px:PXDSCallbackCommand Name="NormalizeTimecard" Visible="false" />
            <px:PXDSCallbackCommand Name="ViewActivity" Visible="False" CommitChanges="True" DependOnGrid="gridActivities" />
            <px:PXDSCallbackCommand Name="View" Visible="False" CommitChanges="True" DependOnGrid="gridActivities" StateColumn="RefNoteID" />
            <px:PXDSCallbackCommand Name="CreateActivity" Visible="False" CommitChanges="True" DependOnGrid="gridActivities" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="View" Visible="False" DependOnGrid="gridDetails" />
            <px:PXDSCallbackCommand DependOnGrid="grid" Name="OpenAppointment" Visible="False" />

        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Document" NoteIndicator="True" FilesIndicator="True" ActivityIndicator="True"
        ActivityField="NoteActivity" LinkIndicator="True" NotifyIndicator="True" Caption="Document Summary" DefaultControlID="edTimeCardCD" TabIndex="14900">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="M" />
            <px:PXSelector ID="edTimeCardCD" runat="server" DataField="TimeCardCD" AutoRefresh="True" DataSourceID="ds" />
            <px:PXDropDown ID="edStatus" runat="server" DataField="Status" Enabled="False" />
            <px:PXSelector CommitChanges="True" ID="edWeekID" runat="server" DataField="WeekID" TextMode="Search" DataSourceID="ds" ValueField="WeekID" DisplayMode="Text"/>
            <px:PXLayoutRule ID="PXLayoutRule4" runat="server" StartColumn="True" LabelsWidth="S" ControlSize="M" />
            <px:PXSelector CommitChanges="True" ID="edEmployeeID" runat="server" DataField="EmployeeID" TextField="AcctCD" ValueField="AcctCD" TextMode="Search" NullText="<SELECT>"
                DataSourceID="ds" />
            <px:PXDropDown ID="PXDropDown1" runat="server" DataField="TimecardType" Enabled="False" />
             <px:PXTextEdit runat="server" DataField="OrigTimecardCD" ID="PXTextEdit1" Enabled="False"/>

            <px:PXLayoutRule ID="PXLayoutRule1" runat="server" GroupCaption="Regular" StartColumn="True" StartGroup="True" />
            <px:PXTimeSpan runat="server" DataField="TimeSpentCalc" ID="RegularTime" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" SummaryMode="true"/>
            <px:PXTimeSpan ID="BillableTime" runat="server" DataField="TimeBillableCalc" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" SummaryMode="true"/>
            <px:PXLayoutRule ID="PXLayoutRule2" runat="server" GroupCaption="Overtime" StartColumn="True" StartGroup="True" />
            <px:PXTimeSpan runat="server" DataField="OvertimeSpentCalc" ID="OvertimeSpentCalc" Enabled="False" SuppressLabel="True" Size="XS" InputMask="hh:mm" MaxHours="99" SummaryMode="true"/>
            <px:PXTimeSpan ID="BillableOvertime" runat="server" DataField="OvertimeBillableCalc" Enabled="False" SuppressLabel="True" Size="XS" InputMask="hh:mm" MaxHours="99" SummaryMode="true"/>
            <px:PXLayoutRule ID="PXLayoutRule3" runat="server" GroupCaption="Total" StartColumn="True" StartGroup="True" />
            <px:PXTimeSpan ID="edTimeSpent" runat="server" DataField="TotalSpentCalc" Enabled="false" Size="XS" SuppressLabel="True" InputMask="hh:mm" MaxHours="99" SummaryMode="true"/>
            <px:PXTimeSpan ID="PXMaskEdit1" runat="server" DataField="TotalBillableCalc" Enabled="false" SuppressLabel="True" Size="XS" InputMask="hh:mm" MaxHours="99" SummaryMode="true"/>
        </Template>
    </px:PXFormView>
    <px:PXSmartPanel ID="PanelAddTasks" runat="server" Height="296px" Style="z-index: 108;
        left: 216px; position: absolute; top: 171px" Width="573px" Caption="Preload from Tasks"
        CaptionVisible="True" Key="Tasks" AutoCallBack-Command="Refresh" AutoCallBack-Enabled="True"
        AutoCallBack-Target="gridAddTasks">
        <px:PXGrid ID="gridAddTasks" runat="server" Height="240px" Width="100%" DataSourceID="ds"
            SkinID="Inquire">
            <AutoSize Enabled="true" />
            <Levels>
                <px:PXGridLevel DataMember="Tasks">
                    <Columns>
                        <px:PXGridColumn AllowCheckAll="True" AllowNull="False" DataField="Selected" Label="Selected" TextAlign="Center" Type="CheckBox" Width="60px" />
                        <px:PXGridColumn DataField="Subject" Width="108px"/>
                        <px:PXGridColumn DataField="ProjectID" Width="108px"/>
                        <px:PXGridColumn DataField="ProjectTaskID" Width="108px"/>
                    </Columns>
                </px:PXGridLevel>
            </Levels>
            <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
        </px:PXGrid>
        <px:PXPanel ID="PXPanel1" runat="server" SkinID="Buttons">
            <px:PXButton ID="PXButton2" runat="server" DialogResult="OK" Text="Add " CommandName="PreloadFromTasks" CommandSourceID="ds" />
            <px:PXButton ID="PXButton3" runat="server" DialogResult="Cancel" Text="Cancel" />
        </px:PXPanel>
    </px:PXSmartPanel>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="400px" Style="z-index: 100;" Width="100%">
        <Items>
            <px:PXTabItem Text="Summary">
                <Template>
                    <px:PXGrid ID="gridDetails" runat="server" DataSourceID="ds" Height="100%" Width="100%" SkinID="DetailsInTab" RenderDefaultEditors="true"
                        Style="border-right: 0px; border-left: 0px; border-top: 0px" SyncPosition="true">
                        <Levels>
                            <px:PXGridLevel DataMember="Summary">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                                    <px:PXSelector ID="edEarningType" runat="server" DataField="EarningType" />
                                    <px:PXSegmentMask ID="edProjectTaskID" runat="server" DataField="ProjectTaskID" AutoRefresh="true" />
                                     <px:PXSegmentMask ID="edCostCodeID" runat="server" DataField="CostCodeID" AutoRefresh="true" />
                                    <px:PXSegmentMask SuppressLabel="True" ID="edProjectID" runat="server" DataField="ProjectID" AutoRefresh="true" />
                                    <px:PXTimeSpan ID="PXMaskEditMon" runat="server" DataField="Mon" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan1" runat="server" DataField="Tue" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan2" runat="server" DataField="Wed" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan3" runat="server" DataField="Thu" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan4" runat="server" DataField="Fri" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan5" runat="server" DataField="Sat" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan6" runat="server" DataField="Sun" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan7" runat="server" DataField="TimeSpent" InputMask="hh:mm" MaxHours="99" SummaryMode="true"/>
                                    <px:PXTextEdit ID="edProjectDescription" runat="server" DataField="ProjectDescription" />
                                    <px:PXTextEdit ID="edProjectTaskDescription" runat="server" DataField="ProjectTaskDescription"/>
                                    <px:PXSelector ID="edParentNoteID" runat="server" DataField="ParentNoteID" AllowEdit="True" DisplayMode="Text" TextMode="Search" TextField="Subject"  />
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="EarningType" AutoCallBack="true" Width="108px" />
                                    <px:PXGridColumn DataField="JobID" AutoCallBack="true" Width="108px" />
                                    <px:PXGridColumn DataField="ShiftID" AutoCallBack="true" Width="108px" />
                                    <px:PXGridColumn DataField="ParentNoteID" AutoCallBack="true" Width="108px" TextField="Summary" DisplayMode="Text" />
                                    <px:PXGridColumn DataField="ProjectID" AutoCallBack="true" Width="108px" />
                                    <px:PXGridColumn DataField="ProjectDescription" Width="108px" SyncVisible="False" SyncVisibility="False" Visible="False" />
                                    <px:PXGridColumn DataField="ProjectTaskID" AutoCallBack="true" Width="108px"  />
                                    <px:PXGridColumn DataField="ProjectTaskDescription" Width="108px" SyncVisible="False" SyncVisibility="False" Visible="False" />
                                    <px:PXGridColumn DataField="CostCodeID" AutoCallBack="true" Width="108px" />
                                    <px:PXGridColumn DataField="Mon" AutoCallBack="true" RenderEditorText="True" />
                                    <px:PXGridColumn DataField="Tue" AutoCallBack="true" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="Wed" AutoCallBack="true" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="Thu" AutoCallBack="true" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="Fri" AutoCallBack="true" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="Sat" AutoCallBack="true" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="Sun" AutoCallBack="true" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="TimeSpent" Width="90px" RenderEditorText="True" />
                                    <px:PXGridColumn DataField="IsBillable" Width="63px" Type="CheckBox" />
                                    <px:PXGridColumn DataField="Description" Width="200px" />
                                    <px:PXGridColumn DataField="ApprovalStatus" Width="100px" />
                                    <px:PXGridColumn DataField="ApproverID" Width="100px" />
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoCallBack Target="gridActivities" Command="Refresh" />
                        <AutoSize Enabled="True" />
                        <Mode AllowAddNew="true" AllowUpdate="true" AllowDelete="true" AllowUpload="True"/>
                        <ActionBar>
                            <CustomItems>
                                <px:PXToolBarButton Text="Prelaod From Task" CommandName="preloadFromTasks" CommandSourceID="ds" />
                                <px:PXToolBarButton Text="Prelaod From Previos Timecard" CommandName="PreloadFromPreviousTimecard" CommandSourceID="ds" />
                                <px:PXToolBarButton Text="Prelaod Holidays" CommandName="PreloadHolidays" CommandSourceID="ds" />
                                <px:PXToolBarButton Text="Normalize Timecard" CommandName="NormalizeTimecard" CommandSourceID="ds" />
                            </CustomItems>
                            <Actions>
                                <AddNew Enabled="true" />
                                <EditRecord Enabled="true" />
                                <Delete Enabled="true" />
                            </Actions>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Details">
                <Template>
                    <px:PXGrid ID="gridActivities" runat="server" DataSourceID="ds" Height="100%" Width="100%" SkinID="DetailsInTab"
                        Style="border-right: 0px; border-left: 0px; border-bottom: 0px" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataMember="Activities">
                                <Columns>
                                    <px:PXGridColumn DataField="Date_Date" DataType="DateTime" Width="90px" AutoCallBack="true" />
                                    <px:PXGridColumn DataField="EarningTypeID" Width="90px" AutoCallBack="true"/>
                                    <px:PXGridColumn DataField="JobID" AutoCallBack="true" Width="108px" />
                                    <px:PXGridColumn DataField="ShiftID" AutoCallBack="true" Width="108px" />
                                    <px:PXGridColumn DataField="ParentTaskNoteID" Width="90px" AutoCallBack="true" TextField="Summary" DisplayMode="Text"/>
                                    <px:PXGridColumn DataField="ProjectID" Width="90px" AutoCallBack="true"/>
                                    <px:PXGridColumn DataField="ProjectTaskID" Width="90px"  AutoCallBack="true"/>
									<px:PXGridColumn DataField="CostCodeID" AutoCallBack="true" Width="90px" />
                                    <px:PXGridColumn DataField="AppointmentID" Width="90px" CommitChanges="True" LinkCommand="OpenAppointment" />
                                    <px:PXGridColumn DataField="AppEmpID" Width="90px" CommitChanges="True" />
                                    <px:PXGridColumn DataField="ServiceID" Width="70px" />
                                    <px:PXGridColumn DataField="Date_Time" Width="70px" TimeMode="True" CommitChanges="True" />
                                    <px:PXGridColumn DataField="TimeSpent" Width="90px" AutoCallBack="true" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="IsBillable" Type="CheckBox" Width="63px" />
                                    <px:PXGridColumn DataField="BillableTimeCalc" Width="90px" AutoCallBack="true" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="BillableOvertimeCalc" Width="90px" AutoCallBack="true" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="Summary" Width="230px" />
                                    <px:PXGridColumn DataField="RegularTimeCalc" Width="63px" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="OvertimeCalc" Width="63px" RenderEditorText="True"/>
                                    <px:PXGridColumn DataField="OvertimeMultiplierCalc" Width="63px" />
                                    <px:PXGridColumn DataField="ApprovalStatus" Width="90px" />
                                    <px:PXGridColumn DataField="Day" Width="90px" AutoCallBack="true" />
                                    <px:PXGridColumn DataField="CRCase__CaseCD" Width="90px" />
                                    <px:PXGridColumn DataField="ContractEx__ContractCD" Width="90px" />
                                    <px:PXGridColumn DataField="IsActivityExists"/>
                                </Columns>
                                <RowTemplate>
                                    <px:PXSelector ID="edEarningType2" runat="server" DataField="EarningTypeID" />
                                    <px:PXSegmentMask ID="edProjectTaskID2" runat="server" DataField="ProjectTaskID" AutoRefresh="true" />
									<px:PXSegmentMask ID="edCostCodeID2" runat="server" DataField="CostCodeID" AutoRefresh="true" />
                                    <px:PXSelector runat="server" ID="edServiceID" DataField="ServiceID" AllowEdit="True" AutoRefresh="True" />
                                    <px:PXSelector runat="server" ID="edAppointmentID" DataField="AppointmentID" AllowEdit="True" AutoRefresh="True" />
                                    <px:PXSelector runat="server" ID="edAppointmentCustomerID" DataField="AppointmentCustomerID" AllowEdit="True" AutoRefresh="True" />
                                    <px:PXSegmentMask SuppressLabel="True" ID="edProjectID2" runat="server" DataField="ProjectID" AutoRefresh="true" />
                                    <px:PXTextEdit ID="edActSummary" runat="server" DataField="Summary" />
                                    <px:PXTimeSpan ID="PXTimeSpan8" runat="server" DataField="TimeSpent" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan9" runat="server" DataField="BillableTimeCalc" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan10" runat="server" DataField="BillableOvertimeCalc" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan11" runat="server" DataField="RegularTimeCalc" InputMask="hh:mm" />
                                    <px:PXTimeSpan ID="PXTimeSpan12" runat="server" DataField="OvertimeCalc" InputMask="hh:mm" />
                                    <px:PXSelector ID="edParentTaskNoteID" runat="server" DataField="ParentTaskNoteID" AllowEdit="True" DisplayMode="Text" TextMode="Search" TextField="Subject"  />
                                    <px:PXSelector ID="edCRCase__CaseCD" runat="server" DataField="CRCase__CaseCD" AllowEdit="True" />
                                    <px:PXSelector ID="edContractEx__ContractCD" runat="server" DataField="ContractEx__ContractCD" AllowEdit="True" />
                                    <px:PXDropDown ID="edDay" runat="server" DataField="Day" />                                    
                                </RowTemplate>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode InitNewRow="true" />
                        <AutoSize Enabled="True" />
                        <ActionBar DefaultAction="cmdViewActivity">
                            <CustomItems>
                                <px:PXToolBarButton Key="cmdViewActivity" Visible="false">
                                    <ActionBar MenuVisible="false" />
                                    <AutoCallBack Command="ViewActivity" Target="ds" />
                                    <PopupCommand Command="Refresh" Target="gridDetails" ActiveBehavior="true">
                                        <Behavior RepaintControlsIDs="gridActivities" />
                                    </PopupCommand>
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Key="cmdCreateActivity">
                                    <ActionBar MenuVisible="false" />
                                    <AutoCallBack Command="CreateActivity" Target="ds" />
                                    <PopupCommand Command="Refresh" Target="gridDetails" ActiveBehavior="true">
                                        <Behavior RepaintControlsIDs="gridActivities" />
                                    </PopupCommand>
                                </px:PXToolBarButton>
                                <px:PXToolBarButton DependOnGrid="gridActivities" StateColumn="IsActivityExists">
                                    <AutoCallBack Command="View" Target="ds"   />
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <ClientEvents CommandState="correctEditButtons" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Materials">
                <Template>
                    <px:PXGrid ID="gridItems" runat="server" DataSourceID="ds" Height="100%" Width="100%" SkinID="DetailsInTab" SyncPosition="true">
                        <Levels>
                            <px:PXGridLevel DataMember="Items">
                                <Columns>
                                    <px:PXGridColumn DataField="ProjectID" AutoCallBack="true" Width="90px" />
                                    <px:PXGridColumn DataField="TaskID" AutoCallBack="true" Width="90px" />
                                    <px:PXGridColumn DataField="CostCodeID" AutoCallBack="true" Width="90px" />
                                    <px:PXGridColumn DataField="InventoryID" Width="90px" AutoCallBack="true" />
                                    <px:PXGridColumn DataField="Description" Width="108px" />
                                    <px:PXGridColumn DataField="UOM" Width="108px" AutoCallBack="true" />
                                    <px:PXGridColumn AllowNull="False" DataField="Mon" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn AllowNull="False" DataField="Tue" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn AllowNull="False" DataField="Wed" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn AllowNull="False" DataField="Thu" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn AllowNull="False" DataField="Fri" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn AllowNull="False" DataField="Sat" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn AllowNull="False" DataField="Sun" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn AllowNull="False" DataField="TotalQty" TextAlign="Right" Width="63px" />
                                </Columns>
                                <RowTemplate>
                                    <px:PXSegmentMask SuppressLabel="True" ID="edProjectID3" runat="server" DataField="ProjectID" AutoRefresh="true" />
                                    <px:PXSegmentMask ID="edProjectTaskID3" runat="server" DataField="TaskID" AutoRefresh="true" />
                                     <px:PXSegmentMask ID="edCostCodeID3" runat="server" DataField="CostCodeID" AutoRefresh="true" />
                                    <px:PXSegmentMask ID="edInventoryID" runat="server" DataField="InventoryID" AutoRefresh="true" />
                                    <px:PXSelector ID="edUOM" runat="server" DataField="UOM" AutoRefresh="true" />
                                </RowTemplate>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode InitNewRow="true" />
                        <AutoSize Enabled="True" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Approval" RepaintOnDemand="false">
                <Template>
                    <px:PXGrid ID="gridApproval" runat="server" DataSourceID="ds" Width="100%" SkinID="DetailsInTab" NoteIndicator="True">
                        <AutoSize Enabled="true" />
                        <Mode AllowAddNew="false" AllowDelete="false" AllowUpdate="false" />
                        <ActionBar>
                            <Actions>
                                <AddNew Enabled="false" />
                                <EditRecord Enabled="false" />
                                <Delete Enabled="false" />
                            </Actions>
                        </ActionBar>
                        <Levels>
                            <px:PXGridLevel DataMember="Approval" DataKeyNames="ApprovalID,AssignmentMapID">
                                <Columns>
                                    <px:PXGridColumn DataField="WorkgroupID" Width="80px" />
                                    <px:PXGridColumn DataField="ApproverEmployee__AcctCD" Width="160px" />
                                    <px:PXGridColumn DataField="ApproverEmployee__AcctName" Width="160px" />
                                    <px:PXGridColumn DataField="ApprovedByEmployee__AcctCD" Width="100px" />
                                    <px:PXGridColumn DataField="ApprovedByEmployee__AcctName" Width="160px" />
                                    <px:PXGridColumn DataField="ApproveDate" Width="90px" />
                                    <px:PXGridColumn AllowNull="False" AllowUpdate="False" DataField="Status" RenderEditorText="True" />
									<px:PXGridColumn DataField="AssignmentMapID"  Visible="false" SyncVisible="false" Width="160px"/>
									<px:PXGridColumn DataField="RuleID" Visible="false" SyncVisible="false" Width="160px" />
									<px:PXGridColumn DataField="CreatedDateTime" Visible="false" SyncVisible="false" Width="100px" />
                                </Columns>
                                <Layout FormViewHeight="" />
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="250" />
    </px:PXTab>
</asp:Content>
