<%@ Page Language="C#" MasterPageFile="~/MasterPages/TabView.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB101000.aspx.cs" Inherits="Page_XB101000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/TabView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="XRBSetupRecords"
        SuspendUnloading="False" TypeName="MaxQ.Products.RBRR.SetupMaint">
        <CallbackCommands>
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" Visible="False" />
            <px:PXDSCallbackCommand Name="CopyPaste" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Delete" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="First" PostData="Self" Visible="False" />
            <px:PXDSCallbackCommand Name="Previous" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Next" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Last" PostData="Self" Visible="False" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXTab ID="tab" runat="server" DataSourceID="ds" Height="219px" Style="z-index: 100" Width="100%"
        DataMember="XRBSetupRecords">
        <Items>
            <px:PXTabItem Text="General">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" GroupCaption="Contract Options" StartGroup="True"
                        ControlSize="SM" LabelsWidth="XXL">
                    </px:PXLayoutRule>
                    <px:PXDropDown ID="edDfltContractType" runat="server"
                        DataField="DfltContractType">
                    </px:PXDropDown>
                    <px:PXNumberEdit ID="edDfltRenewDays" runat="server" DataField="DfltRenewDays" TabIndex="20" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXNumberEdit>
                    <px:PXLayoutRule runat="server" ControlSize="S" LabelsWidth="XXL" Merge="True">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edDfltContrLen" runat="server" DataField="DfltContrLen" TabIndex="30" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXNumberEdit>
                    <px:PXDropDown ID="edDfltContrInt" runat="server" DataField="DfltContrInt" TabIndex="40" SuppressLabel="True">
                    </px:PXDropDown>
                    <px:PXLayoutRule runat="server">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edDfltContrClassID" runat="server" DataField="DfltContrClassID">
                    </px:PXSelector>
                    <px:PXSelector ID="edDfltQuickClassID" runat="server" DataField="DfltQuickClassID">
                    </px:PXSelector>
                    <px:PXDropDown ID="edChargeCard" runat="server" AllowNull="False" DataField="ChargeCard"
                        SelectedIndex="-1" TabIndex="50" Size="M">
                    </px:PXDropDown>
                    <px:PXDropDown ID="AutoRenewStatus" runat="server" DataField="AutoRenewStatus">
                    </px:PXDropDown>
                    <px:PXNumberEdit ID="edClearPayMthdConsecSoft" runat="server" DataField="ClearPayMthdConsecSoft" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edLateFeeDays" runat="server" DataField="LateFeeDays" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXNumberEdit>
                    <px:PXCheckBox ID="edClearPayMthdHardDecline" runat="server" DataField="ClearPayMthdHardDecline"
                        Text="Clear Payment Method on Hard Decline" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkNotesToSO" runat="server" Checked="True" DataField="NotesToSO"
                        TabIndex="70" Text="Copy Notes from Contracts to Bills" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edNotesToContract" runat="server"
                        DataField="NotesToContract" Text="Copy Notes from Invoices to Contracts" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkSumBill" runat="server" DataField="SumBill" TabIndex="80" Text="Enable Summary Billing by Default" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkCloseExpiredContract" runat="server" DataField="CloseExpiredContract"
                        TabIndex="90" Text="Close Expired Contracts" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edAutoCreateContracts" runat="server" DataField="AutoCreateContracts"
                        Text="Automatically Create Contracts From AR Invoices" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXDropDown ID="edAutoGenDescrOption" runat="server" AllowNull="False" DataField="AutoGenDescrOption" Size="SM">
                    </px:PXDropDown>
                    <px:PXCheckBox ID="edCopyPerPetNotes" runat="server" DataField="CopyPerPetNotes" Size="XL" Text="Copy Scheduled Billing Notes on Perpetual Contracts" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXDropDown ID="edPriceDfltMthd" runat="server" AllowNull="False" DataField="PriceDfltMthd"
                        SelectedIndex="-1" TabIndex="100" Size="XM">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edDefCodeDfltMthd" runat="server"
                        DataField="DefCodeDfltMthd" Size="XM">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edDfltRenewalPriceMthd" runat="server"
                        DataField="DfltRenewalPriceMthd" Size="XM">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edDepositReqOpenOpt" runat="server"
                        DataField="DepositReqOpenOpt" Size="XM">
                    </px:PXDropDown>
                    <px:PXSelector ID="edDfltDepositInventoryID" runat="server"
                        DataField="DfltDepositInventoryID" Size="XM">
                    </px:PXSelector>
                    <px:PXDropDown ID="edInvoiceDateMthd" runat="server"
                        DataField="InvoiceDateMthd">
                    </px:PXDropDown>
                    <px:PXCheckBox ID="edReplaceVoidLineDflt" runat="server"
                        DataField="ReplaceVoidLineDflt" Text="Replace Voided Bills Default" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXDropDown ID="edVoidDateOption" runat="server" DataField="VoidDateOption" Size="M">
                    </px:PXDropDown>
                    <px:PXCheckBox ID="edRenewalUOMOption" runat="server" DataField="SyncRenewalUOM"
                        Text="Sync Renewal UOM with Contract UOM" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edInitMode" runat="server" DataField="InitMode" Text="Initialize Mode" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edDfltARCustomerID" runat="server" DataField="DfltARCustomerID" Text="Default AR Customer to Customer's Parent Account" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXDropDown ID="edInvoiceCustOption" runat="server" DataField="InvoiceCustOption">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edPopulateDescrOption" runat="server" Size="M"
                        DataField="PopulateDescrOption">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edDfltBillingFreq" runat="server"
                        DataField="DfltBillingFreq">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edEmailCustOption" runat="server"
                        DataField="EmailCustOption">
                    </px:PXDropDown>
                    <px:PXCheckBox ID="edRevSchedAct" runat="server" DataField="RevSchedAct" Text="Recognize All Revenue in Current Period upon Contract Cancellation" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edDfltSalesPersonID" runat="server" AlreadyLocalized="False" DataField="DfltSalesPersonID" Text="Default Salesperson onto Contracts">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edDefSchedDelay" runat="server" AlreadyLocalized="False" CommitChanges="True" DataField="DefSchedDelay" Text="Delay Deferral Schedules After Day in Month">
                    </px:PXCheckBox>
                    <px:PXNumberEdit ID="edDefSchedDelayDay" runat="server" AlreadyLocalized="False" DataField="DefSchedDelayDay">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edRenewNoticeDays" runat="server" AlreadyLocalized="False" DataField="RenewNoticeDays" DefaultLocale="">
                    </px:PXNumberEdit>
                    <px:PXCheckBox ID="edOneOccurOneTime" runat="server" AlreadyLocalized="False" DataField="OneOccurOneTime">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edDfltTermsBackOff" runat="server" AlreadyLocalized="False" DataField="DfltTermsBackOff">
                    </px:PXCheckBox>
                    <px:PXLayoutRule runat="server" ControlSize="SM" LabelsWidth="L" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" GroupCaption="Numbering Sequences" StartGroup="True">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edContractNumberingID" runat="server" DataField="ContractNumberingID" AllowEdit="True" edit="1">
                    </px:PXSelector>
                    <px:PXSelector ID="edQuickEntryNumberingID" runat="server" DataField="QuickEntryNumberingID" AllowEdit="True" edit="1">
                    </px:PXSelector>
                    <px:PXSelector ID="edMeterNumberingID" runat="server" DataField="MeterNumberingID" AllowEdit="True" edit="1">
                    </px:PXSelector>
                    <px:PXSelector ID="edPromiseNumberingID" runat="server" DataField="PromiseNumberingID" AllowEdit="True" edit="1">
                    </px:PXSelector>
                    <px:PXSelector ID="edMSANumberingID" runat="server" DataField="MSANumberingID" AllowEdit="True" edit="1">
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" EndGroup="True" GroupCaption="Sales Order Contract Options"
                        ControlSize="XM" LabelsWidth="L">
                    </px:PXLayoutRule>
                    <px:PXDropDown ID="edFCastOMSlsAcct" runat="server" AllowNull="False" DataField="FCastOMSlsAcct"
                        SelectedIndex="-1" TabIndex="110">
                    </px:PXDropDown>
                    <px:PXLayoutRule runat="server" ControlSize="SM">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edDfltSOTypeId" runat="server" DataField="DfltSOTypeId">
                    </px:PXSelector>
                    <px:PXSelector ID="edDfltRSOTypeId" runat="server" DataField="DfltRSOTypeId">
                    </px:PXSelector>
                    <px:PXSelector ID="edDfltDestSiteID" runat="server" DataField="DfltDestSiteID" Size="XM">
                    </px:PXSelector>
                    <px:PXDropDown ID="edDfltTransferLocOption" runat="server" DataField="DfltTransferLocOption" Size="S">
                    </px:PXDropDown>
                    <px:PXLayoutRule runat="server" GroupCaption="Activity Billing"
                        StartGroup="True" LabelsWidth="L">
                    </px:PXLayoutRule>
                    <px:PXCheckBox ID="edActivityBillingEnabled" runat="server"
                        DataField="ActivityBillingEnabled" Text="Activity Billing Enabled" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="BillingActivity" runat="server" DataField="BillingActivity" Text="Write Billing Activity Records" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edRevenueActivity" runat="server" DataField="RevenueActivity"
                        Text="Write Billing Activity Records" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edBillingActNumberingID" runat="server" DataField="BillingActNumberingID">
                    </px:PXSelector>
                    <px:PXSelector ID="edRevenueActNumberingID" runat="server" DataField="RevenueActNumberingID">
                    </px:PXSelector>
                    <px:PXSelector ID="edBillingProfNumberingID" runat="server" DataField="BillingProfNumberingID">
                    </px:PXSelector>
                    <px:PXSelector ID="edTrustNumberingID" runat="server" DataField="TrustNumberingID">
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" GroupCaption="Contract Deferral" StartGroup="True" ControlSize="M">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edUnbilledARAccountID" runat="server" DataField="UnbilledARAccountID">
                    </px:PXSelector>
                    <px:PXSegmentMask ID="edUnbilledARSubID" runat="server" DataField="UnbilledARSubID">
                    </px:PXSegmentMask>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Late Defaults">
                <Template>
                    <px:PXGrid ID="grid1" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 14px;"
                        Width="100%" Height="150px" SkinID="Details" SyncPosition="True"
                        TabIndex="8700" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataMember="LateRecords" DataKeyNames="LateID">
                                <RowTemplate>
                                    <px:PXTextEdit ID="LateID" runat="server" DataField="LateID" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="Descr" runat="server" DataField="Descr" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXSelector ID="ActivityID" runat="server" DataField="ActivityID" DisplayMode="Text">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="DaysLate" runat="server" DataField="DaysLate" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXCheckBox ID="ReqManualIntrvn" runat="server" DataField="ReqManualIntrvn" Text="Manual Intervention Required" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edHoldContract" runat="server" CommitChanges="True"
                                        DataField="HoldContract" Text="Hold Contract" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edCancelContract" runat="server" DataField="CancelContract"
                                        Text="Cancel Contract" CommitChanges="True" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXSelector ID="edCancellationReason" runat="server" DataField="CancellationReason">
                                    </px:PXSelector>
                                    <px:PXCheckBox ID="edCancelAutoRenew" runat="server" DataField="CancelAutoRenew"
                                        Text="Cancel Auto Renewal" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="LateID">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActivityID" DisplayMode="Text" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="DaysLate" TextAlign="Right" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ReqManualIntrvn" TextAlign="Center" Type="CheckBox"
                                        Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="HoldContract" TextAlign="Center" Type="CheckBox"
                                        Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CancelContract" Width="120px" CommitChanges="True"
                                        TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CancellationReason" Width="175px"
                                        DisplayMode="Text">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CancelAutoRenew" TextAlign="Center" Type="CheckBox"
                                        Width="150px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="200" />
    </px:PXTab>
</asp:Content>
