<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="DR201500.aspx.cs"
    Inherits="Page_DR201500" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<script type="text/javascript">
		var pageName = px.getPageName();
		window.localStorage.setItem(pageName + "_iwidth", 1000);
		window.localStorage.setItem(pageName + "_iheight", 900);
		window.localStorage.setItem(pageName + "_owidth", 1000);
		window.localStorage.setItem(pageName + "_oheight", 900);
		</script>
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.DR.DraftScheduleMaint" PrimaryView="Schedule">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Cancel" PopupVisible="true" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" PopupVisible="True" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="ViewBatch" PostData="Self" DependOnGrid="grid" Visible="false" />
            <px:PXDSCallbackCommand Name="GenerateTransactions" DependOnGrid="componentGrid" PostData="Self" Visible="false" />
            <px:PXDSCallbackCommand Name="ViewSchedule" PostData="Self" DependOnGrid="gridSchedules" Visible="false" />
            <px:PXDSCallbackCommand Name="Release" PostData="Self" RepaintControls="All" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Caption="Deferral Schedule" DataMember="Schedule"
        NoteIndicator="True" FilesIndicator="True" ActivityIndicator="True" ActivityField="NoteActivity" TabIndex="100" MarkRequired="Dynamic">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
            <px:PXSelector ID="edScheduleNumber" runat="server" DataField="ScheduleNbr" DataSourceID="ds" />
            <px:PXDateTimeEdit CommitChanges="True" ID="edDocDate" runat="server" DataField="DocDate" ValidateRequestMode="Inherit" />
            <px:PXSelector CommitChanges="True" ID="edFinPeriodID" runat="server" DataField="FinPeriodID" DataSourceID="ds" />
            <px:PXCheckBox ID="chkIsCustom" Visible="False" runat="server" DataField="IsCustom" ValidateRequestMode="Inherit" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
            <px:PXDropDown CommitChanges="True" ID="edDocumentType" runat="server" DataField="DocumentType" />
            <px:PXSelector CommitChanges="True" ID="edRefNbr" runat="server" DataField="RefNbr" AutoRefresh="True" DataSourceID="ds" />
            <px:PXSelector CommitChanges="True" runat="server" DataField="LineNbr" ID="edLineNbr" AutoRefresh="True" DataSourceID="ds" AutoCallBack="true"/>
            <px:PXNumberEdit CommitChanges="True" runat="server" DataField="OrigLineAmt" ID="edOrigLineAmt" ValidateRequestMode="Inherit" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
            <px:PXSelector CommitChanges="True" ID="edBAccountID" runat="server" DataField="BAccountID" DataSourceID="ds" AutoRefresh="True" />
            <px:PXSegmentMask ID="edBAccountLocID" runat="server" DataField="BAccountLocID" DataSourceID="ds" />
            <px:PXSegmentMask ID="PXSegmentMask1" runat="server" DataField="ProjectID" CommitChanges="True" DataSourceID="ds" />
            <px:PXSegmentMask ID="PXSegmentMask2" runat="server" DataField="TaskID" DataSourceID="ds" />
            <px:PXLayoutRule runat="server" ControlSize="S" LabelsWidth="S" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXDateTimeEdit ID="edTermStartDate" runat="server" DataField="TermStartDate" CommitChanges="true">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit ID="edTermEndDate" runat="server" DataField="TermEndDate" CommitChanges="true">
            </px:PXDateTimeEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" DataSourceID="ds" DataMember="DocumentProperties">
        <Items>
            <px:PXTabItem Text="Details">
                <Template>
                    <px:PXSplitContainer runat="server" ID="sp1" SplitterPosition="300" SkinID="Horizontal" Height="200px" 
											Panel1MinSize="180" Panel2MinSize="180">
                        <AutoSize Enabled="true" MinHeight="360" />
                        <Template1>
                            <px:PXGrid ID="componentGrid" runat="server" DataSourceID="ds" Width="100%" SkinID="DetailsInTab" Caption="Components" Height="100px" SyncPosition="True" KeepPosition="True" TabIndex="200">
                                <AutoCallBack Target="grid" Command="Refresh"/>
                                <AutoSize Enabled="true" />
                                <Levels>
                                    <px:PXGridLevel DataMember="Components" >
                                        <RowTemplate>
                                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" ></px:PXLayoutRule>
                                            <px:PXSelector ID="edComponentID" runat="server" DataField="ComponentID" ></px:PXSelector>
                                            <px:PXDropDown ID="edStatus2" runat="server" DataField="Status" Enabled="False" ></px:PXDropDown>
                                            <px:PXSegmentMask ID="edAccountID2" runat="server" DataField="AccountID" ></px:PXSegmentMask>
                                            <px:PXNumberEdit ID="edTotalAmt" runat="server" DataField="TotalAmt" Enabled="False" ></px:PXNumberEdit>
                                            <px:PXSegmentMask ID="edSubID2" runat="server" DataField="SubID" ></px:PXSegmentMask>
                                            <px:PXNumberEdit ID="edDefAmt" runat="server" DataField="DefAmt" Enabled="False" ></px:PXNumberEdit>
                                            <px:PXSelector ID="edDefCode" runat="server" DataField="DefCode" Enabled="False" AutoRefresh="true"></px:PXSelector>
                                            <px:PXSegmentMask ID="edDefAcctID" runat="server" DataField="DefAcctID" ></px:PXSegmentMask>
                                            <px:PXSegmentMask ID="edDefSubID" runat="server" DataField="DefSubID" ></px:PXSegmentMask>
                                        </RowTemplate>
                                        <Columns>
                                            <px:PXGridColumn DataField="ComponentID" Width="108px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="DefCode" Width="91px" AutoCallBack="True" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="DefAcctID" Width="108px" AutoCallBack="True"></px:PXGridColumn>
                                            <px:PXGridColumn DataField="DefSubID" Width="91px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="AccountID" Width="61px" AutoCallBack="True"></px:PXGridColumn>
                                            <px:PXGridColumn DataField="SubID" Width="108px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="TotalAmt" TextAlign="Right" Width="81px" AutoCallBack="True"></px:PXGridColumn>
                                            <px:PXGridColumn DataField="DefAmt" TextAlign="Right" Width="108px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="DefTotal" TextAlign="Right" Width="108px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="Status" RenderEditorText="True" Width="54px" ></px:PXGridColumn>
	<px:PXGridColumn DataField="MilestoneID" Width="100" />
	<px:PXGridColumn DataField="MilestoneCompDate" Width="110" /></Columns>
                                    </px:PXGridLevel>
                                </Levels>
                                <ActionBar DefaultAction="cmdViewBatch">
                                    <CustomItems>
                                        <px:PXToolBarButton Text="Generate Transactions" Key="cmdGenTran">
                                            <AutoCallBack Target="ds" Command="GenerateTransactions" />
                                        </px:PXToolBarButton>
                                    </CustomItems>
                                </ActionBar>
                            </px:PXGrid>
                        </Template1>
                        <Template2>
                            <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" SkinID="DetailsInTab" Caption="Transactions" SyncPosition="True" TabIndex="600">
                                <Levels>
                                    <px:PXGridLevel DataMember="Transactions">
                                        <Columns>
                                            <px:PXGridColumn DataField="LineNbr" TextAlign="Right" Width="63px" />
                                            <px:PXGridColumn DataField="Status" RenderEditorText="True" Width="54px" />
                                            <px:PXGridColumn DataField="RecDate" Width="90px" AutoCallBack="True"/>
                                            <px:PXGridColumn DataField="TranDate" Width="90px" AutoCallBack="True"/>
                                            <px:PXGridColumn DataField="Amount" TextAlign="Right" Width="81px" AutoCallBack="True"/>
                                            <px:PXGridColumn DataField="AccountID" Width="81px" AutoCallBack="True" />
                                            <px:PXGridColumn DataField="SubID" Width="108px" />
                                            <px:PXGridColumn DataField="FinPeriodID" Width="63px" AutoCallBack="True"/>
                                            <px:PXGridColumn DataField="BranchID" Width="81px" />
                                            <px:PXGridColumn DataField="BatchNbr" Width="81px" LinkCommand="ViewBatch" />
                                            <px:PXGridColumn DataField="IsSamePeriod" TextAlign="Center" AllowShowHide="Server" Type="CheckBox" />
                                        </Columns>
                                        <RowTemplate>
                                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                                            <px:PXNumberEdit ID="edScheduleID" runat="server" DataField="ScheduleID" ValidateRequestMode="Inherit" />
                                            <px:PXNumberEdit ID="edLineNbr" runat="server" DataField="LineNbr" Enabled="False" ValidateRequestMode="Inherit" />
                                            <px:PXDropDown ID="edStatus" runat="server" DataField="Status" Enabled="False" />
                                            <px:PXDateTimeEdit ID="edRecDate" runat="server" DataField="RecDate" ValidateRequestMode="Inherit" />
                                            <px:PXDateTimeEdit ID="edTranDate" runat="server" DataField="TranDate" Enabled="False" ValidateRequestMode="Inherit" />
                                            <px:PXNumberEdit ID="edAmount" runat="server" DataField="Amount" ValidateRequestMode="Inherit" />
                                            <px:PXSegmentMask ID="edAccountID" runat="server" DataField="AccountID" AllowEdit="True" />
                                            <px:PXSegmentMask ID="edSubID" runat="server" DataField="SubID" />
                                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                                            <px:PXMaskEdit ID="edFinPeriodID" runat="server" DataField="FinPeriodID" Enabled="False" InputMask="##-####" ValidateRequestMode="Inherit" />
                                            <px:PXTextEdit ID="edBatchNbr" runat="server" DataField="BatchNbr" ValidateRequestMode="Inherit" />
                                        </RowTemplate>
                                        <Layout FormViewHeight="" />
                                    </px:PXGridLevel>
                                </Levels>
                                <AutoSize Enabled="True" />
                                <Mode AllowUpload="True" />
                            </px:PXGrid>
                        </Template2>
                    </px:PXSplitContainer>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem LoadOnDemand="true" Text="Original Schedules" BindingContext="form" VisibleExp="DataControls[&quot;chkIsCustom&quot;].Value == False">
                <Template>
                    <px:PXGrid ID="gridSchedules" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Height="100%" SkinID="Details" 
                        BorderStyle="None">
                        <Levels>
                            <px:PXGridLevel DataMember="Associated" DataKeyNames="ScheduleNbr">
                                <Columns>
                                    <px:PXGridColumn DataField="ScheduleNbr" Width="108px" LinkCommand ="ViewSchedule"/>
                                    <px:PXGridColumn Width="200px" DataField="TranDesc" />
                                    <px:PXGridColumn DataField="DocumentTypeEx" />
                                    <px:PXGridColumn DataField="RefNbr" />
                                    <px:PXGridColumn DataField="FinPeriodID" />
                                </Columns>
                                <Layout FormViewHeight="" />
                            </px:PXGridLevel>
                        </Levels>
                        <Parameters>
                            <px:PXControlParam ControlID="form" Name="ScheduleNbr" PropertyName="NewDataKey[&quot;ScheduleNbr&quot;]" Type="String" />
                        </Parameters>
                        <AutoSize Enabled="True" MinHeight="260" />
                        <Mode AllowAddNew="False" AllowDelete="False" />
                        <ActionBar DefaultAction="gridSchedules">
							<Actions>
								<AddNew Enabled="False" />
								<Delete Enabled="False" />
							</Actions>
                            <CustomItems>
                                <px:PXToolBarButton Text="View Schedule" Key="gridSchedules">
                                    <AutoCallBack Command="ViewSchedule" Target="ds" />
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Enabled="True" Container="Window" />
    </px:PXTab>
</asp:Content>
