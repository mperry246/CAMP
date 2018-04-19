<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB290000.aspx.cs" Inherits="Page_XB290000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Header"
        TypeName="MaxQ.Products.RBRR.ContractQuickEntry" SuspendUnloading="False">
        <CallbackCommands>
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="CreateUpdateCustomer"
                PopupCommand="" PopupCommandTarget="" PopupPanel="" Text="">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="LoadTemplate" PopupCommand=""
                PopupCommandTarget="" PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand CommitChanges="True" Name="CreateUpdateContract"
                PopupCommand="" PopupCommandTarget="" PopupPanel="" Text="" Visible="True">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand CommitChanges="True" Name="ExplodeBundle"
                PopupCommand="" PopupCommandTarget="" PopupPanel="" Text="">
            </px:PXDSCallbackCommand>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="Header" TabIndex="1000">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edQuickEntryCD" runat="server" CommitChanges="True" DataField="QuickEntryCD">
            </px:PXSelector>
            <px:PXSelector ID="edQuickClassID" runat="server" CommitChanges="True" DataField="QuickClassID">
            </px:PXSelector>
            <px:PXCheckBox ID="edNewCustomer" runat="server" CommitChanges="True" DataField="NewCustomer"
                Text="New Customer">
            </px:PXCheckBox>
            <px:PXSelector ID="edCustomerID" runat="server" CommitChanges="True"
                DataField="CustomerID" AllowEdit="True" edit="1">
            </px:PXSelector>
            <px:PXTextEdit ID="edCustomerName" runat="server" DataField="CustomerName">
            </px:PXTextEdit>
            <px:PXSelector ID="edCustomerClassID" runat="server" CommitChanges="True"
                DataField="CustomerClassID">
            </px:PXSelector>
            <px:PXSelector ID="edARCustomerID" runat="server" CommitChanges="True"
                DataField="ARCustomerID" AllowEdit="True" edit="1">
            </px:PXSelector>
            <px:PXSelector ID="edCustomerLocationID" runat="server"
                DataField="CustomerLocationID" CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector ID="edBillProfCustomerID" runat="server" CommitChanges="True"
                DataField="BillProfCustomerID" AllowEdit="True" edit="1">
            </px:PXSelector>
            <px:PXSelector ID="edSvcParmID" runat="server" DataField="SvcParmID"
                AllowEdit="True" CommitChanges="True" edit="1">
            </px:PXSelector>
            <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edExtRef" runat="server" DataField="ExtRef">
            </px:PXTextEdit>
            <px:PXSelector ID="edCreateActivityID" runat="server"
                DataField="CreateActivityID">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" GroupCaption="Bundle" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edBundleID" runat="server" DataField="BundleID"
                CommitChanges="True">
            </px:PXSelector>
            <px:PXDropDown ID="edTrialType" runat="server" DataField="TrialType">
            </px:PXDropDown>
            <px:PXLayoutRule runat="server" Merge="True">
            </px:PXLayoutRule>
            <px:PXNumberEdit ID="edTrialLen" runat="server" DataField="TrialLen">
            </px:PXNumberEdit>
            <px:PXDropDown ID="edTrialInt" runat="server" DataField="TrialInt"
                SuppressLabel="True">
            </px:PXDropDown>
            <px:PXLayoutRule runat="server">
            </px:PXLayoutRule>
            <px:PXNumberEdit ID="edCuryTrialPrice" runat="server"
                DataField="CuryTrialPrice">
            </px:PXNumberEdit>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edCuryID" runat="server" CommitChanges="True" DataField="CuryID">
            </px:PXSelector>
            <px:PXDateTimeEdit ID="edBegDate" runat="server" CommitChanges="True" DataField="BegDate">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit ID="edEndDate" runat="server" CommitChanges="True" DataField="EndDate">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit ID="edTermEndDate" runat="server" DataField="TermEndDate">
            </px:PXDateTimeEdit>
            <px:PXDropDown ID="edOrdType" runat="server" CommitChanges="True" DataField="OrdType">
            </px:PXDropDown>
            <px:PXDropDown ID="edContractType" runat="server" CommitChanges="True" DataField="ContractType">
            </px:PXDropDown>
            <px:PXSelector ID="edClassID" runat="server" DataField="ClassID"
                CommitChanges="True">
            </px:PXSelector>
            <px:PXDropDown ID="edTotPeriods" runat="server" DataField="TotPeriods"
                CommitChanges="True">
            </px:PXDropDown>
            <px:PXDropDown ID="edPerPetTotPeriods" runat="server"
                DataField="PerPetTotPeriods">
            </px:PXDropDown>
            <px:PXSelector ID="edSOOrderType" runat="server" DataField="SOOrderType">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" Merge="True">
            </px:PXLayoutRule>
            <px:PXNumberEdit ID="edRenewalLen" runat="server" DataField="RenewalLen">
            </px:PXNumberEdit>
            <px:PXDropDown ID="edRenewalInt" runat="server" DataField="RenewalInt"
                SuppressLabel="True">
            </px:PXDropDown>
            <px:PXLayoutRule runat="server">
            </px:PXLayoutRule>
            <px:PXCheckBox ID="edAutoRenew" runat="server" DataField="AutoRenew"
                Text="Auto Renew" CommitChanges="True">
            </px:PXCheckBox>
            <px:PXSelector ID="edContractID" runat="server" DataField="ContractID" AllowEdit="True"
                DisplayMode="Text" edit="1">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" GroupCaption="Customer Address"
                StartColumn="True" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXTextEdit ID="edCustAddressLine1" runat="server"
                DataField="CustAddressLine1">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edCustAddressLine2" runat="server"
                DataField="CustAddressLine2">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edCustCity" runat="server" DataField="CustCity">
            </px:PXTextEdit>
            <px:PXSelector ID="edCustCountryID" runat="server" DataField="CustCountryID"
                CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector ID="edCustState" runat="server" DataField="CustState">
            </px:PXSelector>
            <px:PXTextEdit ID="edCustPostalCode" runat="server" DataField="CustPostalCode">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edCustPhone1" runat="server" DataField="CustPhone1">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edCustEMail" runat="server" DataField="CustEMail">
            </px:PXTextEdit>
            <px:PXLayoutRule runat="server" GroupCaption="Payment Method" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edPaymentMethodID" runat="server" CommitChanges="True" AutoRefresh="True"
                DataField="PaymentMethodID">
            </px:PXSelector>
            <px:PXSelector ID="edProcessingCenterID" runat="server" CommitChanges="True"
                DataField="ProcessingCenterID">
            </px:PXSelector>
            <px:PXSelector ID="edPMInstanceID" runat="server" CommitChanges="True" TextField="descr"
                DataField="PMInstanceID">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" GroupCaption="Deposit" StartGroup="True"
                StartColumn="True">
            </px:PXLayoutRule>
            <px:PXNumberEdit ID="edCuryDepositAmt" runat="server"
                DataField="CuryDepositAmt" CommitChanges="True">
            </px:PXNumberEdit>
            <px:PXDropDown ID="edDepositUsage" runat="server" DataField="DepositUsage">
            </px:PXDropDown>
            <px:PXSelector ID="edDepositInventoryID" runat="server"
                DataField="DepositInventoryID">
            </px:PXSelector>
            <px:PXCheckBox ID="edCreateDepositInvc" runat="server"
                DataField="CreateDepositInvc" Text="Create Deposit Invoice">
            </px:PXCheckBox>
            <px:PXDateTimeEdit ID="edDepositInvcDate" runat="server"
                DataField="DepositInvcDate">
            </px:PXDateTimeEdit>
            <px:PXSelector ID="edDepositPaymentMethodID" runat="server" CommitChanges="True" AutoRefresh="True"
                DataField="DepositPaymentMethodID">
            </px:PXSelector>
            <px:PXSelector ID="edDepositProcessingCenterID" runat="server" CommitChanges="True"
                DataField="DepositProcessingCenterID">
            </px:PXSelector>
            <px:PXSelector ID="edDepositPMInstanceID" runat="server" CommitChanges="True" TextField="descr"
                DataField="DepositPMInstanceID">
            </px:PXSelector>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="400px" Style="z-index: 100"
        DataSourceID="ds" DataMember="CurrentQuickEntry" Width="100%">
        <Items>
            <px:PXTabItem Text="Details">
                <Template>
                    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
                        Height="150px" SkinID="Details" TabIndex="11500" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="LineNbr,QuickEntryID" DataMember="Details">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="edInventoryID" runat="server" DataField="InventoryID"
                                        CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXNumberEdit ID="edQty" runat="server" DataField="Qty" CommitChanges="True" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edSiteID" runat="server" DataField="SiteID"
                                        CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edUOM" runat="server" DataField="UOM" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edCuryUnitPrice" runat="server" DataField="CuryUnitPrice"
                                        CommitChanges="True" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXCheckBox ID="edManualDisc" runat="server" DataField="ManualDisc" CommitChanges="True"
                                        AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXNumberEdit ID="edCuryDiscAmt" runat="server" DataField="CuryDiscAmt"
                                        CommitChanges="True" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edDiscPct" runat="server" DataField="DiscPct"
                                        CommitChanges="True" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edCuryAmount" runat="server" DataField="CuryAmount" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edAccountID" runat="server" DataField="AccountID"
                                        CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edSubID" runat="server" DataField="SubID">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edDeferredCode" runat="server" DataField="DeferredCode">
                                    </px:PXSelector>
                                    <px:PXDateTimeEdit ID="edBeginBillingDate" runat="server" CommitChanges="True"
                                        DataField="BeginBillingDate" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXDateTimeEdit>
                                    <px:PXNumberEdit ID="edNbrBills" runat="server" DataField="NbrBills" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXDropDown ID="edTotPeriods" runat="server" CommitChanges="True"
                                        DataField="TotPeriods">
                                    </px:PXDropDown>
                                    <px:PXCheckBox ID="edNotRenewable" runat="server" DataField="NotRenewable"
                                        Text="Not Renewable" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edCreateFirstBill" runat="server"
                                        DataField="CreateFirstBill" Text="Create First Bill" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edTrial" runat="server" DataField="Trial" Text="Trial" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edExplodeBundle" runat="server" AlreadyLocalized="False" DataField="ExplodeBundle" Text="Explode Bundle">
                                    </px:PXCheckBox>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="InventoryID" CommitChanges="True" Width="108px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" TextAlign="Right" Width="100px"
                                        CommitChanges="True">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SiteID" Width="120px" CommitChanges="True">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UOM" CommitChanges="True">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CuryUnitPrice" TextAlign="Right" Width="100px"
                                        CommitChanges="True">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CuryExtPrice" TextAlign="Right" Width="100px"
                                        CommitChanges="True">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>

                                    <px:PXGridColumn DataField="ManualDisc" TextAlign="Center" Type="CheckBox" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="DiscPct" TextAlign="Right" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CuryDiscAmt" TextAlign="Right" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CuryAmount" TextAlign="Right" Width="100px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="AccountID" CommitChanges="True" Width="100px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SubID" Width="120px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="DeferredCode" Width="95px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="BeginBillingDate"
                                        Width="150px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="NbrBills" TextAlign="Right" Width="105px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="TotPeriods" TextAlign="Right"
                                        Width="115px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="NotRenewable" TextAlign="Center" Type="CheckBox"
                                        Width="110px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CreateFirstBill" TextAlign="Center" Type="CheckBox"
                                        Width="60px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Trial" TextAlign="Center" Type="CheckBox"
                                        Width="60px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ExplodeBundle" TextAlign="Center" Type="CheckBox" Width="70px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                        <ActionBar ActionsText="False">
                            <CustomItems>
                                <px:PXToolBarButton Text="Load Template">
                                    <AutoCallBack Command="LoadTemplate" Target="ds">
                                    </AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Attributes">
                <Template>
                    <px:PXGrid ID="PXGridAnswers" runat="server" DataSourceID="ds" Width="100%" Height="100%" SkinID="Inquire" MatrixMode="True">
                        <Levels>
                            <px:PXGridLevel DataMember="Answers">
                                <Columns>
                                    <px:PXGridColumn DataField="AttributeID" TextAlign="Left" Width="220px" AllowShowHide="False" TextField="AttributeID_description" />
                                    <px:PXGridColumn DataField="isRequired" TextAlign="Center" Type="CheckBox" Width="75px" />
                                    <px:PXGridColumn DataField="Value" Width="148px" />
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="true" />
                        <Mode AllowAddNew="False" AllowColMoving="False" AllowDelete="False" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
    </px:PXTab>

    <px:PXSmartPanel ID="popupTemplate" runat="server" CaptionVisible="true" Caption="Load Template"
        Height="90px" Width="350px" DesignView="Content" Key="Tmplt">
        <px:PXFormView ID="frmTemplate" runat="server" DataSourceID="ds" Style="z-index: 100"
            Width="100%" DataMember="Tmplt" TabIndex="16364">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="S" ControlSize="SM">
                </px:PXLayoutRule>
                <px:PXSelector CommitChanges="True" ID="edTemplateID" runat="server" DataField="TemplateID">
                </px:PXSelector>
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="PXPanel4" runat="server" SkinID="Buttons">
            <px:PXButton ID="cmdATemplateOK" runat="server" DialogResult="OK" Text="OK">
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
</asp:Content>
