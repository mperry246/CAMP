<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="FS202300.aspx.cs" Inherits="Page_FS202300" Title="Untitled Page" %>
    <%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

        <asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
            <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="SvrOrdTypeRecords" TypeName="PX.Objects.FS.SvrOrdTypeMaint">
                <CallbackCommands>
                    <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
                    <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
                    <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
                    <px:PXDSCallbackCommand Name="Last" PostData="Self" />
                </CallbackCommands>
            </px:PXDataSource>
        </asp:Content>
        <asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
            <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Height="135px" DataMember="SvrOrdTypeRecords"
            TabIndex="900" DefaultControlID="edSrvOrdType" FilesIndicator="true">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" StartRow="True" ControlSize="SM" Merge="True">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edSrvOrdType" runat="server" AutoRefresh="True" DataField="SrvOrdType" DataSourceID="ds" Size="XS" LabelWidth="170px">
                    </px:PXSelector>
                    <px:PXCheckBox ID="edActive" runat="server" DataField="Active" Text="Active">
                    </px:PXCheckBox>
                    <px:PXLayoutRule runat="server">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edSrvOrdNumberingID" runat="server" DataField="SrvOrdNumberingID" DataSourceID="ds" LabelWidth="170px"
                    AllowEdit="True">
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" ColumnSpan="2">
                    </px:PXLayoutRule>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" LabelWidth="170px" Width="283px">
                        <AutoSize Enabled="True" />
                    </px:PXTextEdit>
                    <px:PXDropDown ID="edBehavior" runat="server" CommitChanges="True" DataField="Behavior" LabelWidth="170px" Size="SM">
                    </px:PXDropDown>
                    <px:PXLayoutRule runat="server" ControlSize="SM" StartColumn="True">
                    </px:PXLayoutRule>
                </Template>
            </px:PXFormView>
        </asp:Content>
        <asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
            <px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" DataMember="CurrentSrvOrdTypeRecord" MarkRequired="Dynamic">
                <Items>
                    <px:PXTabItem Text="Preferences">
                        <Template>
                            <px:PXLayoutRule 
                                runat="server" 
                                StartColumn="True" 
                                GroupCaption="General Settings" 
                                SuppressLabel="True" 
                                ColumnWidth="XL">
                            </px:PXLayoutRule>
                            <%-- General Settings Fields--%>
                            <px:PXCheckBox ID="edBAccountRequired" runat="server" DataField="BAccountRequired">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edCompleteSrvOrdWhenSrvDone" runat="server" DataField="CompleteSrvOrdWhenSrvDone" Size="XXL">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edRequireContact" runat="server" DataField="RequireContact" CommitChanges="True">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edRequireRoute" runat="server" DataField="RequireRoute">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edSingleAppointment" runat="server" DataField="SingleAppointment">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edSingleService" runat="server" DataField="SingleService">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edRequireAddressValidation" runat="server" DataField="RequireAddressValidation">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edRequireRoom" runat="server" DataField="RequireRoom">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edRequireCustomerSignature" runat="server" DataField="RequireCustomerSignature">
                            </px:PXCheckBox>
                            <%-- /General Settings Fields--%>
                            <px:PXLayoutRule runat="server"></px:PXLayoutRule>
                            <px:PXLayoutRule runat="server" GroupCaption="Appointment Settings" SuppressLabel="True" ControlSize="M" LabelsWidth="M">
                            </px:PXLayoutRule>
                            <%-- Appointment Settings Fields--%>
                            <px:PXCheckBox ID="edAppWithMultEmp" runat="server" DataField="AppWithMultEmp" Text="AppWithMultEmp">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edAppWithoutSrv" runat="server" DataField="AppWithoutSrv" Text="AppWithoutSrv">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edAppWithoutEmp" runat="server" DataField="AppWithoutEmp" Text="AppWithoutEmp">
                            </px:PXCheckBox>
                            <px:PXGroupBox ID="gbAppAddressSource" runat="server" Caption="Take Address and Contact Information From" DataField="AppAddressSource" CommitChanges="True">
                                <Template>
                                    <px:PXRadioButton ID="gbAppAddressSource_op0" runat="server" GroupName="gbAppAddressSource" Text="Business Account"
                                    Value="BA" />
                                    <px:PXRadioButton ID="gbAppAddressSource_op1" runat="server" GroupName="gbAppAddressSource" Text="Customer Contact"
                                    Value="CC" />
                                    <px:PXRadioButton ID="gbAppAddressSource_op2" runat="server" GroupName="gbAppAddressSource" Text="Branch Location"
                                    Value="BL" />
                                </Template>
                                <ContentLayout Layout="Stack"></ContentLayout>
                            </px:PXGroupBox>
                            <%-- /Appointment Settings Fields--%>
                            <px:PXLayoutRule 
                                runat="server" 
                                StartColumn="True" 
                                GroupCaption="Invoice Generation Settings">
                            </px:PXLayoutRule>
                            <%-- Posting Settings Fields--%>
                            <px:PXGroupBox 
                                ID="gbPostTo" 
                                runat="server" 
                                Caption="Generate Invoices In" 
                                DataField="PostTo" 
                                CommitChanges="True">
                                <Template>
                                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="SM">
                                    </px:PXLayoutRule>
                                    <px:PXRadioButton ID="gbPostTo_op0" runat="server" GroupName="gbPostTo" Text="Accounts Receivable" Value="AR" />
                                    <px:PXRadioButton ID="gbPostTo_op1" runat="server" GroupName="gbPostTo" Text="Sales Order" Value="SO" />
                                    <px:PXRadioButton ID="gbPostTo_op2" runat="server" GroupName="gbPostTo" Text="None" Value="NA" />
                                </Template>
                                <ContentLayout Layout="Stack"></ContentLayout>
                            </px:PXGroupBox>
                            <px:PXCheckBox ID="edPostNegBalanceToAp" runat="server" AlignLeft="True" DataField="PostNegBalanceToAp" CommitChanges="True">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edEnableINPosting" runat="server" AlignLeft="True" DataField="EnableINPosting" CommitChanges="True">
                            </px:PXCheckBox>
                            <px:PXSelector ID="edPostOrderType" runat="server" AllowEdit="True" AutoRefresh="True" CommitChanges="True" DataField="PostOrderType" DataSourceID="ds" LabelWidth="170px" Size="XM" Width="200px">
                            </px:PXSelector>
                            <px:PXSelector ID="edPostOrderTypeNegativeBalance" runat="server" AllowEdit="True" AutoRefresh="True" CommitChanges="True" DataField="PostOrderTypeNegativeBalance" DataSourceID="ds" LabelWidth="170px" Size="XM" Width="200px">
                            </px:PXSelector>
                            <px:PXSelector ID="edDfltTermIDARSO" runat="server" AllowEdit="True" AutoRefresh="True" DataField="DfltTermIDARSO" DataSourceID="ds"
                            LabelWidth="170px" Size="XM" Width="200px">
                            </px:PXSelector>
                            <px:PXSelector ID="edDfltTermIDAP" runat="server" AllowEdit="True" AutoRefresh="True" DataField="DfltTermIDAP" DataSourceID="ds"
                            LabelWidth="170px" Size="XM" Width="200px">
                            </px:PXSelector>
                            <px:PXDropDown ID="edSalesAcctSource" runat="server" CommitChanges="True" DataField="SalesAcctSource" LabelWidth="170px"
                            Size="XM" Width="200px">
                            </px:PXDropDown>
                            <px:PXSegmentMask ID="edCombineSubFrom" runat="server" CommitChanges="True" DataField="CombineSubFrom" LabelWidth="170px"
                            Size="XM" Width="200px">
                            </px:PXSegmentMask>
                            <px:PXSegmentMask ID="edSubID" runat="server" DataField="SubID" LabelWidth="170px" Size="XM" Width="200px">
                            </px:PXSegmentMask>
                            <px:PXCheckBox ID="edAllowInvoiceOnlyClosedAppointment" runat="server"  AlignLeft="True" DataField="AllowInvoiceOnlyClosedAppointment">
                            </px:PXCheckBox>
                             <%-- Posting Settingss Fields--%>
                            <px:PXLayoutRule runat="server" GroupCaption="Commission" StartGroup="True" ControlSize="SM" LabelsWidth="S">
                            </px:PXLayoutRule>
                            <px:PXSegmentMask ID="edSalesPersonID" runat="server" DataField="SalesPersonID" CommitChanges="True" AutoRefresh="True"></px:PXSegmentMask>
                            <px:PXCheckBox ID="edCommissionable" runat="server" CommitChanges="True" DataField="Commissionable"></px:PXCheckBox>
                            <px:PXLayoutRule runat="server" GroupCaption="Integrating with Time & Expenses" LabelsWidth="L" StartGroup="True">
                            </px:PXLayoutRule>
                            <px:PXCheckBox ID="edRequireTimeApprovalToInvoice" runat="server" AlignLeft="True" 
                                CommitChanges="True" DataField="RequireTimeApprovalToInvoice">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edCreateTimeActivitiesFromAppointment" runat="server" AlignLeft="True" 
                                CommitChanges="True" DataField="CreateTimeActivitiesFromAppointment">
                            </px:PXCheckBox>
                            <px:PXSelector ID="edDfltEarningType" runat="server" AutoRefresh="True" CommitChanges="True" DataField="DfltEarningType" DataSourceID="ds" LabelWidth="170px" Size="M">
                            </px:PXSelector>
                        </Template>
                    </px:PXTabItem>
                    <px:PXTabItem Text="Time Behavior">
                        <Template>
                            <px:PXLayoutRule ID="PXLayoutRule1"
                                runat="server" 
                                StartColumn="True"
                                ColumnWidth="500px">
                            </px:PXLayoutRule>
                            <px:PXGroupBox ID="edStartAppointmentActionBehavior" runat="server" Caption="Start Appointment Action Behavior" CommitChanges="True" DataField="StartAppointmentActionBehavior" Width="500px">
                            <Template>
                                <px:PXRadioButton ID="edStartAppointmentActionBehavior_op1" runat="server" Value="HO" GroupName="edStartAppointmentActionBehavior"/>
                                <px:PXRadioButton ID="edStartAppointmentActionBehavior_op2" runat="server" Value="HS" GroupName="edStartAppointmentActionBehavior" />
                                <px:PXRadioButton ID="edStartAppointmentActionBehavior_op3" runat="server" Value="NO" GroupName="edStartAppointmentActionBehavior" />
                            </Template>
                            </px:PXGroupBox>
                            <px:PXGroupBox ID="edCompleteAppointmentActionBehavior" runat="server" Caption="Complete Appointment Action Behavior" CommitChanges="True" DataField="CompleteAppointmentActionBehavior" Width="500px">
                            <Template>
                                <px:PXRadioButton ID="edCompleteAppointmentActionBehavior_op1" runat="server" Value="HO" GroupName="edCompleteAppointmentActionBehavior"/>
                                <px:PXRadioButton ID="edCompleteAppointmentActionBehavior_op2" runat="server" Value="HS" GroupName="edCompleteAppointmentActionBehavior" />
                                <px:PXRadioButton ID="edCompleteAppointmentActionBehavior_op3" runat="server" Value="NO" GroupName="edCompleteAppointmentActionBehavior" />
                            </Template>
                            </px:PXGroupBox>
                            <px:PXCheckBox ID="edUpdateServiceActualDateTimeBegin" runat="server" DataField="UpdateServiceActualDateTimeBegin" AlignLeft="True">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edUpdateServiceActualDateTimeEnd" runat="server" DataField="UpdateServiceActualDateTimeEnd" CommitChanges="True" AlignLeft="True">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edKeepActualDateTimes" runat="server" DataField="KeepActualDateTimes" AlignLeft="True">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edUpdateHeaderActualDateTimes" runat="server" DataField="UpdateHeaderActualDateTimes" AlignLeft="True">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edRequireServiceActualDateTimes" runat="server" DataField="RequireServiceActualDateTimes" AlignLeft="True">
                            </px:PXCheckBox>
                        </Template>
                    </px:PXTabItem>
                    <px:PXTabItem Text="Problem Codes">
                        <Template>
                            <px:PXGrid ID="PXGridProblems" runat="server" DataSourceID="ds" SkinID="DetailsInTab" Width="100%" AllowPaging="True" AdjustPageSize="Auto"
                            Height="200px" TabIndex="11300" FilesIndicator="False" NoteIndicator="False">
                                <Levels>
                                    <px:PXGridLevel DataMember="SrvOrdTypeProblemRecords" DataKeyNames="SrvOrdType,ProblemID">
                                        <RowTemplate>
                                            <px:PXSelector ID="edProblemID" runat="server" DataField="ProblemID" AutoRefresh="True">
                                            </px:PXSelector>
                                            <px:PXTextEdit ID="edFSProblem__Descr" runat="server" DataField="FSProblem__Descr" Enabled="False">
                                            </px:PXTextEdit>
                                        </RowTemplate>
                                        <Columns>
                                            <px:PXGridColumn DataField="ProblemID" Width="120px" CommitChanges="True">
                                            </px:PXGridColumn>
                                            <px:PXGridColumn DataField="FSProblem__Descr" Width="200px">
                                            </px:PXGridColumn>
                                        </Columns>
                                    </px:PXGridLevel>
                                </Levels>
                                <AutoSize Enabled="True" />
                            </px:PXGrid>
                        </Template>
                    </px:PXTabItem>
                    <px:PXTabItem Text="Mailing Settings">
                        <Template>
                            <px:PXSplitContainer runat="server" ID="sp1" SplitterPosition="350" SkinID="Horizontal" Height="500px" SavePosition="True">
                                <AutoSize Enabled="True"></AutoSize>
                                <Template1>
                                    <px:PXGrid ID="gridNS" runat="server" SkinID="DetailsInTab" Width="100%" Height="150px" Caption="Mailings" AdjustPageSize="Auto"
                                    AllowPaging="True" DataSourceID="ds">
                                        <AutoCallBack Target="gridNR" Command="Refresh"></AutoCallBack>
                                        <AutoSize Enabled="True"></AutoSize>
                                        <Levels>
                                            <px:PXGridLevel DataMember="NotificationSources" DataKeyNames="SourceID,SetupID">
                                                <RowTemplate>
                                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M"></px:PXLayoutRule>
                                                    <px:PXDropDown ID="edFormat" runat="server" DataField="Format"></px:PXDropDown>
                                                    <px:PXSegmentMask ID="edNBranchID" runat="server" DataField="NBranchID"></px:PXSegmentMask>
                                                    <px:PXCheckBox ID="chkActive" runat="server" Checked="True" DataField="Active"></px:PXCheckBox>
                                                    <px:PXSelector ID="edSetupID" runat="server" DataField="SetupID"></px:PXSelector>
                                                    <px:PXSelector ID="edReportID" runat="server" DataField="ReportID" ValueField="ScreenID"></px:PXSelector>
                                                    <px:PXSelector ID="edNotificationID" runat="server" DataField="NotificationID" ValueField="Name"></px:PXSelector>
                                                    <px:PXSelector Size="s" ID="edEMailAccountID" runat="server" DataField="EMailAccountID" DisplayMode="Text"></px:PXSelector>

                                                </RowTemplate>
                                                <Columns>
                                                    <px:PXGridColumn DataField="SetupID" Width="100px" AutoCallBack="True"></px:PXGridColumn>
                                                    <px:PXGridColumn DataField="NBranchID" AutoCallBack="True" Label="Branch" Width="100px"></px:PXGridColumn>
                                                    <px:PXGridColumn DataField="EMailAccountID" Width="200px" DisplayMode="Text"></px:PXGridColumn>
                                                    <px:PXGridColumn DataField="ReportID" Width="150px" AutoCallBack="True"></px:PXGridColumn>
                                                    <px:PXGridColumn DataField="NotificationID" Width="150px" AutoCallBack="True"></px:PXGridColumn>
                                                    <px:PXGridColumn DataField="Format" RenderEditorText="True" AutoCallBack="True"></px:PXGridColumn>
                                                    <px:PXGridColumn DataField="Active" TextAlign="Center" Type="CheckBox"></px:PXGridColumn>

                                                </Columns>
                                                <Layout FormViewHeight=""></Layout>
                                            </px:PXGridLevel>
                                        </Levels>
                                    </px:PXGrid>
                                </Template1>
                                <Template2>
                                    <px:PXGrid ID="gridNR" runat="server" SkinID="DetailsInTab" Width="100%" Caption="Recipients" AdjustPageSize="Auto" AllowPaging="True"
                                    DataSourceID="ds">
                                        <Parameters>
                                            <px:PXSyncGridParam ControlID="gridNS"></px:PXSyncGridParam>
                                        </Parameters>
                                        <CallbackCommands>
                                            <Save CommitChangesIDs="gridNR" RepaintControls="None"></Save>
                                            <FetchRow RepaintControls="None"></FetchRow>
                                        </CallbackCommands>
                                        <Levels>
                                            <px:PXGridLevel DataMember="NotificationRecipients" DataKeyNames="NotificationID">
                                                <Columns>
                                                    <px:PXGridColumn DataField="ContactType" Width="100px" AutoCallBack="True">
                                                    </px:PXGridColumn>
                                                    <px:PXGridColumn DataField="OriginalContactID" Visible="False" AllowShowHide="False"></px:PXGridColumn>
                                                    <px:PXGridColumn DataField="ContactID" Width="250px">
                                                        <NavigateParams>
                                                            <px:PXControlParam Name="ContactID" ControlID="gridNR" PropertyName="DataValues[&quot;OriginalContactID&quot;]"></px:PXControlParam>
                                                        </NavigateParams>
                                                    </px:PXGridColumn>
                                                    <px:PXGridColumn DataField="Format" Width="60px" AutoCallBack="True">
                                                    </px:PXGridColumn>
                                                    <px:PXGridColumn DataField="Active" TextAlign="Center" Type="CheckBox" Width="60px">
                                                    </px:PXGridColumn>
                                                    <px:PXGridColumn DataField="Hidden" TextAlign="Center" Type="CheckBox" Width="60px">
                                                    </px:PXGridColumn>
                                                </Columns>
                                                <RowTemplate>
                                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M"></px:PXLayoutRule>
                                                    <px:PXSelector ID="edContactID" runat="server" DataField="ContactID" AutoRefresh="True" ValueField="DisplayName" AllowEdit="True">
                                                    </px:PXSelector>
                                                </RowTemplate>
                                                <Layout FormViewHeight=""></Layout>
                                            </px:PXGridLevel>
                                        </Levels>
                                        <AutoSize Enabled="True"></AutoSize>
                                    </px:PXGrid>
                                </Template2>
                            </px:PXSplitContainer>
                        </Template>
                    </px:PXTabItem>
                </Items>
                <AutoSize Container="Window" Enabled="True" MinHeight="150" />
            </px:PXTab>
        </asp:Content>