<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB202000.aspx.cs" Inherits="Page_XB202000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" BorderStyle="NotSet"
        PrimaryView="ContractClasses" SuspendUnloading="False"
        TypeName="MaxQ.Products.RBRR.ContractClassMaint">
        <CallbackCommands>
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="CopyDefaults" PopupCommand=""
                PopupCommandTarget="" PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="ContractClasses" NoteIndicator="True"
        TabIndex="1300">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="S"
                LabelsWidth="XM">
            </px:PXLayoutRule>
            <px:PXSelector ID="edClassID" runat="server" DataField="ClassID" DataMember="_XRBContClass_"
                CommitChanges="True" DataSourceID="ds" DisplayMode="Value">
            </px:PXSelector>
            <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" Size="L" AlreadyLocalized="False" DefaultLocale="">
            </px:PXTextEdit>
            <px:PXNumberEdit ID="edPerpetRetenPct" runat="server" DataField="PerpetRetenPct" AlreadyLocalized="False" DefaultLocale="">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edGrowthFactor" runat="server" DataField="GrowthFactor" AlreadyLocalized="False" DefaultLocale="">
            </px:PXNumberEdit>
            <px:PXCheckBox ID="edDepositRequired" runat="server"
                DataField="DepositRequired" Text="Deposit Required" AlreadyLocalized="False">
            </px:PXCheckBox>
            <px:PXNumberEdit ID="edDfltDepositPct" runat="server"
                DataField="DfltDepositPct" AlreadyLocalized="False" DefaultLocale="">
            </px:PXNumberEdit>
            <px:PXDropDown ID="edDfltDepositUsage" runat="server"
                DataField="DfltDepositUsage" Size="SM">
            </px:PXDropDown>
            <px:PXCheckBox ID="edDfltAutoRenew" runat="server" DataField="DfltAutoRenew"
                Text="Auto Renew Default" AlreadyLocalized="False">
            </px:PXCheckBox>
            <px:PXSelector ID="edDfltBillingProfileID" runat="server" AllowEdit="True"
                DataField="DfltBillingProfileID" Size="XM" edit="1">
            </px:PXSelector>
            <px:PXCheckBox ID="edDfltExcludeReprice" runat="server"
                DataField="DfltExcludeReprice" Text="Exclude from Auto-Repricing by Default" AlreadyLocalized="False">
            </px:PXCheckBox>
            <px:PXDropDown ID="edEmailCustOption" runat="server" Size="SM"
                DataField="EmailCustOption">
            </px:PXDropDown>
            <px:PXSelector ID="edRenewOppTmplt" runat="server" DataField="RenewOppTmplt" Size="M">
            </px:PXSelector>
            <px:PXSegmentMask ID="edARContractTmpltID" runat="server" DataField="ARContractTmpltID" Size="SM">
            </px:PXSegmentMask>
            <px:PXLayoutRule runat="server" GroupCaption="Contract Deferral">
            </px:PXLayoutRule>
            <px:PXSelector ID="edUnbilledARAccountID" runat="server" DataField="UnbilledARAccountID" Size="M">
            </px:PXSelector>
            <px:PXSelector ID="edUnbilledARSubID" runat="server" DataField="UnbilledARSubID" Size="M">
            </px:PXSelector>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="400px" Style="z-index: 100"
        DataSourceID="ds" DataMember="CurrentClass" Width="100%">
        <Items>
            <px:PXTabItem Text="Renewal Forecast">
                <Template>
                    <px:PXGrid ID="grid0" runat="server" DataSourceID="ds" Height="300px" SkinID="DetailsInTab"
                        Width="100%" SyncPosition="True"
                        NoteIndicator="True" TabIndex="12000" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataMember="XRBClassRenewRecords">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXNumberEdit ID="edAge" runat="server" DataField="Age" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edRenewalPct" runat="server" DataField="RenewalPct" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="Age" TextAlign="Right" Width="150">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="RenewalPct" TextAlign="Right" Width="150px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Late">
                <Template>
                    <px:PXGrid ID="grid1" runat="server" DataSourceID="ds" Height="300px" SkinID="DetailsInTab"
                        Width="100%" SyncPosition="True"
                        NoteIndicator="True" TabIndex="19300" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataMember="ClassLateRecords">
                                <RowTemplate>
                                    <px:PXMaskEdit ID="LateID" runat="server" DataField="LateID" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXMaskEdit>
                                    <px:PXTextEdit ID="Descr" runat="server" DataField="Descr" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXSelector ID="ActivityID" runat="server" DataField="ActivityID">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="DaysLate" runat="server" DataField="DaysLate" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXCheckBox ID="ReqManualIntrvn" runat="server" DataField="ReqManualIntrvn"
                                        Text="Manual Intervention Required" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edCancelContract" runat="server" DataField="CancelContract"
                                        Text="Cancel Contract" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXSelector ID="edCancellationReason" runat="server"
                                        DataField="CancellationReason">
                                    </px:PXSelector>
                                    <px:PXCheckBox ID="edCancelAutoRenew" runat="server"
                                        DataField="CancelAutoRenew" Text="Cancel Auto Renewal" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="LateID">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActivityID" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="DaysLate" TextAlign="Right" Width="90px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ReqManualIntrvn" TextAlign="Center" Type="CheckBox"
                                        Width="180px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CancelContract" TextAlign="Center" Type="CheckBox"
                                        Width="110px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CancellationReason" DisplayMode="Text" Width="175px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CancelAutoRenew" TextAlign="Center" Type="CheckBox"
                                        Width="150px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar>
                            <CustomItems>
                                <px:PXToolBarButton Text="Load Defaults" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="CopyDefaults" Target="ds">
                                    </AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Attributes">
                                <Template>
                    <px:PXGrid runat="server" ID="AttributeGrid" Width="100%" DataSourceID="ds" Height="100%" SkinID="DetailsInTab" BorderWidth="0px" MatrixMode="True">
                        <AutoSize Enabled="True" />
                        <Levels>
                            <px:PXGridLevel DataMember="AttributeGroup">
                                <RowTemplate>
                                    <px:PXSelector ID="edCRAttributeID" runat="server" DataField="AttributeID" AutoRefresh="true" AllowEdit="true" FilterByAllFields="True" />
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="AttributeID" Width="108px" AutoCallBack="True" />
                                    <px:PXGridColumn  DataField="Description" Width="351px" />
                                    <px:PXGridColumn DataField="SortOrder" TextAlign="Right" Width="108px" />
                                    <px:PXGridColumn  DataField="Required" TextAlign="Center" Type="CheckBox" />
                                    <px:PXGridColumn AllowNull="True" DataField="CSAttribute__IsInternal" TextAlign="Center" Type="CheckBox" />
					                <px:PXGridColumn AllowNull="False" DataField="ControlType" Type="DropDownList" Width="90px" />
                                    <px:PXGridColumn AllowNull="True" DataField="DefaultValue" Width="100px" RenderEditorText="True" />
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar></ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
    </px:PXTab>
</asp:Content>
