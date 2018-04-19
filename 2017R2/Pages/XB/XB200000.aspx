<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB200000.aspx.cs" Inherits="Page_XB200000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <script type="text/javascript">
        function grid_AfterRowChange(sender, arg) {
            if (sender.activeRow) {
                var Generated = sender.activeRow.getCell('manuallybooked').getValue();

                //alert(Generated);

                //var items = sender.actionsTop.toolBar.items;
                //for (var i = 0; i < items.length; i++) {
                //    var item = items[i];
                //    if (item.type == "PXToolBarButton" && (item.autoCallBack.command == "GenerateOrder" || item.autoCallBack.command == "DistTemplateLoad")) {
                //        if (Generated == false)
                //            item.autoCallBack.enabled = true;
                //        else
                //            item.autoCallBack.enabled = false;
                //        //item.setEnabled(!Generated);
                //    }
                //}
            }
        }

    </script>
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="XRBContrHdrRecords"
        TypeName="MaxQ.Products.RBRR.ContractMaint" SuspendUnloading="False">
        <CallbackCommands>
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Last" PostData="Self">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ViewMeter" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AddAdHocPayment" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AddBillingActivity" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="MarkGenerated" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ViewEditBill" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="CopyDistToLaterBills" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="DistTemplateLoad" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AutoGenTemplateLoad" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ViewAutoGenTaxes" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="GenerateOrder" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ViewTaxes" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ViewPromiseToPay" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ViewBillingActivity" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ViewPayment" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ViewAdHocPayment" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AddPromiseToPay" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="RewriteContract" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="RenewContract" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AutoGenSchedule" Visible="False" CommitChanges="True">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AddBundle" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="RemoveContact" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AddContact" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AddNewContact" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="VoidOrder" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="XRBContrHdrRecords" NoteIndicator="True" FilesIndicator="True" ActivityIndicator="True" TabIndex="10900">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="SM">
            </px:PXLayoutRule>
            <px:PXSelector ID="edContractCD" runat="server" DataField="ContractCD" DataSourceID="ds"
                FilterByAllFields="True">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" ControlSize="XS">
            </px:PXLayoutRule>
            <px:PXSelector ID="RevisionNbr" runat="server" DataField="RevisionNbr" DataSourceID="ds">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" ControlSize="XM">
            </px:PXLayoutRule>
            <px:PXSelector CommitChanges="True" ID="edCustomerID" runat="server" DataField="CustomerID"
                DataMember="_Customer_" DataSourceID="ds" AllowEdit="True" edit="1" FilterByAllFields="True"
                AutoRefresh="True">
            </px:PXSelector>
            <px:PXSelector CommitChanges="True" ID="edARCustomerID" runat="server" DataField="ARCustomerID"
                DataMember="_Customer_" DataSourceID="ds" AllowEdit="True" edit="1" FilterByAllFields="True">
            </px:PXSelector>
            <px:PXSelector ID="BillProfCustomerID" runat="server" DataField="BillProfCustomerID"
                DataSourceID="ds" CommitChanges="True" AllowEdit="True" edit="1" FilterByAllFields="True">
            </px:PXSelector>
            <px:PXSelector ID="edSvcParmID" runat="server" DataField="SvcParmID" DataSourceID="ds"
                AllowEdit="True" edit="1" FilterByAllFields="True" CommitChanges="True" AutoRefresh="True">
            </px:PXSelector>
            <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
            </px:PXTextEdit>
            <px:PXDropDown CommitChanges="True" ID="edContractType" runat="server" AllowNull="False"
                DataField="ContractType" SelectedIndex="-1" Size="SM">
            </px:PXDropDown>
            <px:PXDropDown ID="edCurContractType" runat="server" DataField="CurContractType"
                Size="SM">
            </px:PXDropDown>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM">
            </px:PXLayoutRule>
            <pxa:PXCurrencyRate ID="edCuryID" runat="server" DataField="CuryID" DataMember="_Currency_"
                RateTypeView="_XRBContrHdr_CurrencyInfo_"></pxa:PXCurrencyRate>
            <px:PXDropDown CommitChanges="True" ID="edStatus" runat="server" AllowNull="False"
                DataField="Status" SelectedIndex="-1" Size="SM">
            </px:PXDropDown>
            <px:PXDateTimeEdit CommitChanges="True" ID="edBegDate" runat="server" DataField="BegDate">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit CommitChanges="True" ID="edEndDate" runat="server" DataField="EndDate">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit ID="edRenewalDate" runat="server" DataField="RenewalDate">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit ID="edTermEndDate" runat="server" DataField="TermEndDate" CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXDropDown CommitChanges="True" ID="edOrdType" runat="server" AllowNull="False"
                DataField="OrdType" SelectedIndex="-1" Size="SM">
            </px:PXDropDown>
            <px:PXDropDown ID="edTotPeriods" runat="server" DataField="TotPeriods" Size="SM">
            </px:PXDropDown>
            <px:PXSelector ID="edLateID" runat="server" DataField="LateID" Size="M">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="SM">
            </px:PXLayoutRule>
            <px:PXNumberEdit ID="edCuryTotal" runat="server" DataField="CuryTotal">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edCuryTotBilled" runat="server" DataField="CuryTotBilled">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="CuryTotBilledAftTax" runat="server" DataField="CuryTotBilledAftTax">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edCuryTotPaid" runat="server" DataField="CuryTotPaid">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edCuryBalDue" runat="server" DataField="CuryBalDue">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edDaysLate" runat="server" DataField="DaysLate">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edCuryLateAmt" runat="server" DataField="CuryLateAmt">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edCuryDepositBal" runat="server" DataField="CuryDepositBal">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edCuryPrePayBal" runat="server" DataField="CuryPrePayBal">
            </px:PXNumberEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="300px" DataSourceID="ds" DataMember="CurrentContract">
        <Items>
            <px:PXTabItem Text="Billing Schedule">
                <Template>
                    <table cellpadding="0" cellspacing="0" style="height: 300px; width: 100%">
                        <tr>
                            <td style="height: 49%; vertical-align: top">
                                <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px"
                                    SkinID="DetailsInTab" SyncPosition="True" AdjustPageSize="Auto" MinHeight="150px"
                                    AllowPaging="True" TabIndex="25764" KeepPosition="True" TemporaryFilterCaption="Filter Applied">
                                    <AutoCallBack Command="Refresh" Target="grid0">
                                    </AutoCallBack>
                                    <ClientEvents AfterRowChange="grid_AfterRowChange" AfterRefresh="grid_AfterRowChange"
                                        AfterRowInsert="grid_AfterRowChange" />
                                    <AutoSize Enabled="True"></AutoSize>
                                    <Levels>
                                        <px:PXGridLevel DataMember="ContractDetails" DataKeyNames="ContractID,LineNbr">
                                            <RowTemplate>
                                                <px:PXLayoutRule runat="server" StartColumn="True">
                                                </px:PXLayoutRule>
                                                <px:PXDropDown ID="edSchedType" runat="server" DataField="SchedType" CommitChanges="True">
                                                </px:PXDropDown>
                                                <px:PXDateTimeEdit ID="edGenDate" runat="server" DataField="GenDate" CommitChanges="True" AlreadyLocalized="False">
                                                </px:PXDateTimeEdit>
                                                <px:PXSelector ID="edMilestoneID" runat="server" DataField="MilestoneID">
                                                </px:PXSelector>
                                                <px:PXDateTimeEdit ID="edMilestoneCompDate" runat="server" DataField="MilestoneCompDate" CommitChanges="True" AlreadyLocalized="False">
                                                </px:PXDateTimeEdit>
                                                <px:PXTextEdit ID="edComments" runat="server" DataField="Comments" AlreadyLocalized="False">
                                                </px:PXTextEdit>
                                                <px:PXCheckBox ID="edManuallyBooked" runat="server" DataField="ManuallyBooked" Text="Generated"
                                                    CommitChanges="True" AlreadyLocalized="False">
                                                </px:PXCheckBox>
                                                <px:PXTextEdit ID="edSOOrdNbr" runat="server" DataField="SOOrdNbr" AlreadyLocalized="False">
                                                </px:PXTextEdit>
                                                <px:PXTextEdit ID="edARRefNbr" runat="server" DataField="ARRefNbr" AlreadyLocalized="False">
                                                </px:PXTextEdit>
                                                <px:PXLayoutRule runat="server" StartColumn="True">
                                                </px:PXLayoutRule>
                                                <px:PXSelector ID="edPerPost" runat="server" DataField="PerPost">
                                                </px:PXSelector>
                                                <px:PXTextEdit ID="edOrderType" runat="server" DataField="OrderType" AlreadyLocalized="False">
                                                </px:PXTextEdit>
                                                <px:PXNumberEdit ID="edCuryAmount1" runat="server" DataField="CuryAmount" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXTextEdit ID="edCustomerOrderNbr" runat="server" DataField="CustomerOrderNbr" AlreadyLocalized="False">
                                                </px:PXTextEdit>
                                                <px:PXCheckBox ID="edSumBill" runat="server" DataField="SumBill" Text="Summary Billing" AlreadyLocalized="False">
                                                </px:PXCheckBox>
                                                <px:PXCheckBox ID="edNotRenewable" runat="server" DataField="NotRenewable" Text="Not Renewable" AlreadyLocalized="False">
                                                </px:PXCheckBox>
                                                <px:PXLayoutRule runat="server" StartColumn="True">
                                                </px:PXLayoutRule>
                                                <px:PXTextEdit ID="edAutoGenId" runat="server" DataField="AutoGenId" AlreadyLocalized="False">
                                                </px:PXTextEdit>
                                                <px:PXTextEdit ID="edAutoGenDescr" runat="server" DataField="AutoGenDescr" AlreadyLocalized="False">
                                                </px:PXTextEdit>
                                                <px:PXSelector ID="edPaymentMethodID1" runat="server" DataField="PaymentMethodID"
                                                    CommitChanges="True" Size="XM">
                                                </px:PXSelector>
                                                <px:PXSelector ID="edProcessingCenterID1" runat="server" Size="SM" DataField="ProcessingCenterID">
                                                </px:PXSelector>
                                                <px:PXSelector ID="edPMInstanceID1" runat="server" DataField="PMInstanceID" DisplayMode="Text"
                                                    Size="XM">
                                                </px:PXSelector>
                                                <px:PXCheckBox ID="edVoided" runat="server" DataField="Voided" Text="Void" AlreadyLocalized="False">
                                                </px:PXCheckBox>
                                                <px:PXCheckBox ID="edOrdCrtdByContr" runat="server" AlreadyLocalized="False" DataField="OrdCrtdByContr" Text="OrdCrtdByContr">
                                                </px:PXCheckBox>
                                                <px:PXNumberEdit ID="edLineNbr" runat="server" DataField="LineNbr" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                            </RowTemplate>
                                            <Columns>
                                                <px:PXGridColumn DataField="SchedType" Width="125px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="GenDate" Width="108px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="MilestoneID" Width="108px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="MilestoneCompDate" Width="160px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="Comments" Width="120px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="ManuallyBooked" CommitChanges="True" TextAlign="Center"
                                                    Type="CheckBox" Width="108px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="SOOrdNbr">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="ARRefNbr">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="PerPost" Width="108px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="OrderType">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CuryAmount" TextAlign="Right" Width="100px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CustomerOrderNbr" Width="120px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="SumBill" TextAlign="Center" Type="CheckBox" Width="108px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="NotRenewable" Width="108px" TextAlign="Center" Type="CheckBox">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="AutoGenId" Width="108px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="AutoGenDescr" Width="225px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="PaymentMethodID" Width="118px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="ProcessingCenterID" Width="140px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="PMInstanceID" TextAlign="Left" Width="200px" DisplayMode="Text">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="Voided" TextAlign="Center" Type="CheckBox" Width="60px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="OrdCrtdByContr" TextAlign="Center" Type="CheckBox" Width="80px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="LineNbr" TextAlign="Right">
                                                </px:PXGridColumn>
                                            </Columns>
                                        </px:PXGridLevel>
                                    </Levels>
                                    <ActionBar ActionsText="False">
                                        <CustomItems>
                                            <px:PXToolBarButton Text="Auto Generate Schedule" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                                <AutoCallBack Command="AutoGenSchedule" Target="ds">
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                            <px:PXToolBarSeperator SuppressHtmlEncoding="False">
                                            </px:PXToolBarSeperator>
                                            <px:PXToolBarButton Text="Generate Bill" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                                <AutoCallBack Command="GenerateOrder" Target="ds">
                                                    <Behavior CommitChanges="True" />
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                            <px:PXToolBarSeperator SuppressHtmlEncoding="False">
                                            </px:PXToolBarSeperator>
                                            <px:PXToolBarButton Text="View/Edit Bill" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                                <AutoCallBack Target="ds" Command="ViewEditBill">
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                            <px:PXToolBarSeperator SuppressHtmlEncoding="False">
                                            </px:PXToolBarSeperator>
                                            <px:PXToolBarButton Text="Void Bill" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                                <AutoCallBack Target="ds" Command="VoidOrder">
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                            <px:PXToolBarSeperator SuppressHtmlEncoding="False">
                                            </px:PXToolBarSeperator>
                                            <px:PXToolBarButton Text="Copy Distributions to Later Bills" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                                <AutoCallBack Command="CopyDistToLaterBills" Target="ds">
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                            <px:PXToolBarSeperator SuppressHtmlEncoding="False">
                                            </px:PXToolBarSeperator>
                                            <px:PXToolBarButton Text="Load Template" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                                <AutoCallBack Command="DistTemplateLoad" Target="ds">
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                            <px:PXToolBarSeperator SuppressHtmlEncoding="False">
                                            </px:PXToolBarSeperator>
                                            <px:PXToolBarButton Text="Mark Generated" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                                <AutoCallBack Command="MarkGenerated" Target="ds">
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                        </CustomItems>
                                    </ActionBar>
                                    <Mode AllowFormEdit="True" />
                                </px:PXGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 1%">
                                <px:PXSplitter ID="spl2" runat="server" SkinID="Horizontal" AllowCollapse="False"
                                    Panel1MinSize="150" Panel2MinSize="150" SavePosition="True">
                                    <AutoSize Enabled="True"></AutoSize>
                                </px:PXSplitter>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 50%; vertical-align: top">
                                <px:PXGrid ID="grid0" runat="server" DataSourceID="ds" SkinID="DetailsInTab" Style="left: 0px; top: 0px;"
                                    Width="100%" AdjustPageSize="Auto" AllowPaging="True" SyncPosition="True"
                                    Caption="Distributions" TabIndex="25964" TemporaryFilterCaption="Filter Applied">
                                    <Levels>
                                        <px:PXGridLevel DataMember="Distributions" DataKeyNames="ContractID,ContrDetLineNbr,LineNbr">
                                            <RowTemplate>
                                                <px:PXLayoutRule runat="server" StartColumn="True">
                                                </px:PXLayoutRule>
                                                <px:PXSelector ID="edInventoryID2" runat="server" DataField="InventoryID">
                                                </px:PXSelector>
                                                <px:PXTextEdit ID="edDescr2" runat="server" DataField="Descr" AlreadyLocalized="False">
                                                </px:PXTextEdit>
                                                <px:PXSegmentMask ID="SubItemID" runat="server" DataField="SubItemID">
                                                </px:PXSegmentMask>
                                                <px:PXSelector ID="edPriceClassID2" runat="server" DataField="PriceClassID">
                                                </px:PXSelector>
                                                <px:PXNumberEdit ID="edQty2" runat="server" DataField="Qty" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXSelector ID="edSiteID" runat="server" DataField="SiteID">
                                                </px:PXSelector>
                                                <px:PXSelector ID="edUOM2" runat="server" DataField="UOM" CommitChanges="True">
                                                </px:PXSelector>
                                                <px:PXNumberEdit ID="edCuryUnitPrice2" runat="server" DataField="CuryUnitPrice" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXNumberEdit ID="edDiscPct" runat="server" DataField="DiscPct" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXNumberEdit ID="edCuryDiscAmt" runat="server" DataField="CuryDiscAmt" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXCheckBox ID="ManualDisc" runat="server" DataField="ManualDisc" Text="ManualDisc" AlreadyLocalized="False">
                                                </px:PXCheckBox>
                                                <px:PXLayoutRule runat="server" StartColumn="True">
                                                </px:PXLayoutRule>
                                                <px:PXNumberEdit ID="edCuryLineAmt2" runat="server" DataField="CuryLineAmt" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXSelector ID="edAccountID2" runat="server" DataField="AccountID">
                                                </px:PXSelector>
                                                <px:PXSegmentMask ID="edSubID2" runat="server" DataField="SubID">
                                                </px:PXSegmentMask>
                                                <px:PXSelector ID="edTaskID2" runat="server" DataField="TaskID">
                                                </px:PXSelector>
                                                <px:PXSelector ID="edSalesPersonID2" runat="server" DataField="SalesPersonID">
                                                </px:PXSelector>
                                                <px:PXSelector ID="DeferredCode" runat="server" DataField="DeferredCode">
                                                </px:PXSelector>
                                                <px:PXSelector ID="edTaxCategoryID2" runat="server" DataField="TaxCategoryID">
                                                </px:PXSelector>
                                                <px:PXCheckBox ID="Commissionable" runat="server" DataField="Commissionable" Text="Commissionable" AlreadyLocalized="False">
                                                </px:PXCheckBox>
                                                <px:PXDropDown ID="PMDeltaOption" runat="server" DataField="PMDeltaOption">
                                                </px:PXDropDown>
                                                <px:PXLayoutRule runat="server" StartColumn="True">
                                                </px:PXLayoutRule>
                                                <px:PXNumberEdit ID="edCuryEndUserPrc2" runat="server" DataField="CuryEndUserPrc" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXCheckBox ID="NotRenewable2" runat="server" DataField="NotRenewable" Text="Not Renewable" AlreadyLocalized="False">
                                                </px:PXCheckBox>
                                                <px:PXDateTimeEdit ID="edBegDate" runat="server" DataField="BegDate" CommitChanges="True" AlreadyLocalized="False">
                                                </px:PXDateTimeEdit>
                                                <px:PXDateTimeEdit ID="edEndDate" runat="server" DataField="EndDate" CommitChanges="True" AlreadyLocalized="False">
                                                </px:PXDateTimeEdit>
                                                <px:PXSelector ID="edRenewedContrId" runat="server" DataField="RenewedContrId">
                                                </px:PXSelector>
                                                <px:PXNumberEdit ID="edNbrRenewals" runat="server" DataField="NbrRenewals" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXSelector ID="edOrigContractId" runat="server" DataField="OrigContractId">
                                                </px:PXSelector>
                                                <px:PXCheckBox ID="NoCopy" runat="server" DataField="NoCopy" Text="Do Not Copy" AlreadyLocalized="False">
                                                </px:PXCheckBox>
                                                <px:PXTextEdit ID="edMeterID" runat="server" DataField="MeterID" AlreadyLocalized="False">
                                                </px:PXTextEdit>
                                                <px:PXLayoutRule runat="server" StartColumn="True">
                                                </px:PXLayoutRule>
                                                <px:PXNumberEdit ID="edCurMeterReading" runat="server" DataField="CurMeterReading" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXSelector ID="edDiscountID" runat="server" DataField="DiscountID">
                                                </px:PXSelector>
                                                <px:PXSelector ID="edRenewalInventoryID" runat="server" DataField="RenewalInventoryID" CommitChanges="True">
                                                </px:PXSelector>
                                                <px:PXNumberEdit ID="edRenewalQty" runat="server" DataField="RenewalQty" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXSelector ID="edRenewalUOM" runat="server" DataField="RenewalUOM">
                                                </px:PXSelector>
                                                <px:PXDropDown ID="edRenewalPriceMthd" runat="server" DataField="RenewalPriceMthd" CommitChanges="True">
                                                </px:PXDropDown>
                                                <px:PXNumberEdit ID="edCuryRenewalPrice" runat="server" DataField="CuryRenewalPrice" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXNumberEdit ID="edRenewalPricePct" runat="server" DataField="RenewalPricePct" AlreadyLocalized="False">
                                                </px:PXNumberEdit>
                                                <px:PXSelector ID="edRenewalPriceClassID" runat="server" DataField="RenewalPriceClassID">
                                                </px:PXSelector>
                                                <px:PXSelector ID="edContractItemID" runat="server" DataField="ContractItemID">
                                                </px:PXSelector>
                                            </RowTemplate>
                                            <Columns>
                                                <px:PXGridColumn DataField="InventoryID" Width="108px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="Descr" Width="200px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="SubItemID" Width="120px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="PriceClassID" Width="108px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn AllowNull="False" DataField="Qty" TextAlign="Right" Width="100px"
                                                    CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="SiteID" Width="108px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="UOM" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn AllowNull="False" DataField="CuryUnitPrice" TextAlign="Right" Width="100px"
                                                    CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="DiscPct" TextAlign="Right" Width="100px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CuryDiscAmt" TextAlign="Right" Width="100px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="ManualDisc" TextAlign="Center" Width="108px" Type="CheckBox"
                                                    CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CuryLineAmt" TextAlign="Right" Width="100px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="AccountID" TextAlign="Right" Width="100px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="SubID" Width="120px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="TaskID">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="SalesPersonID" TextAlign="Right">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="DeferredCode">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="TaxCategoryID">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="Commissionable" Type="CheckBox" TextAlign="Center" Width="100px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="PMDeltaOption" Type="DropDownList">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CuryEndUserPrc" Width="100px" TextAlign="Right">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="NotRenewable" Width="100px" AllowNull="False" TextAlign="Center"
                                                    Type="CheckBox">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="BegDate" Width="90px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="EndDate" Width="90px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="RenewedContrId" Width="210px" DisplayMode="Text">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="NbrRenewals" Width="108px" AllowNull="False" TextAlign="Right">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="OrigContractId" Width="210px" DisplayMode="Text">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="NoCopy" TextAlign="Center" Type="CheckBox" Width="100px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="MeterID">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CurMeterReading" TextAlign="Right" Width="100px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="DiscountID">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="RenewalInventoryID" Width="108px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn AllowNull="False" DataField="RenewalQty" TextAlign="Right" Width="100px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="RenewalUOM">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="RenewalPriceMthd" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CuryRenewalPrice" TextAlign="Right" Width="100px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="RenewalPricePct" Width="100px" TextAlign="Right">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn AllowNull="False" DataField="ContrDetLineNbr" TextAlign="Right" Width="108px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn AllowNull="False" DataField="LineNbr" TextAlign="Right" Width="108px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="RenewalPriceClassID" Width="108px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="ContractItemID" Width="108px">
                                                </px:PXGridColumn>
                                            </Columns>
                                        </px:PXGridLevel>
                                    </Levels>
                                    <AutoSize Enabled="True"></AutoSize>
                                    <ActionBar ActionsText="False">
                                        <CustomItems>
                                            <px:PXToolBarButton Text="View Meter" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                                <AutoCallBack Command="ViewMeter" Target="ds">
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                            <px:PXToolBarButton Text="Add Bundle Item" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                                <AutoCallBack Command="AddBundle" Target="ds">
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                        </CustomItems>
                                    </ActionBar>
                                    <Mode AllowFormEdit="True" />
                                </px:PXGrid>
                            </td>
                        </tr>
                    </table>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Other Information">
                <Template>
                    <px:PXFormView ID="formOtherInfo" runat="server" CaptionVisible="False" DataMember="CurrentContract"
                        DataSourceID="ds" TabIndex="19300" Width="1019px">
                        <Template>
                            <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="M" StartColumn="True">
                            </px:PXLayoutRule>
                            <px:PXLayoutRule runat="server" GroupCaption="Miscellaneous Options" StartGroup="True">
                            </px:PXLayoutRule>
                            <px:PXSelector CommitChanges="True" ID="edTermsID" runat="server" DataField="TermsID"
                                DataMember="_Terms_">
                            </px:PXSelector>
                            <px:PXSelector CommitChanges="True" ID="edSalesPersonID" runat="server" DataField="SalesPersonID"
                                DataMember="_SalesPerson_">
                            </px:PXSelector>
                            <px:PXSelector ID="edCustomerLocationID" runat="server" DataField="CustomerLocationID"
                                DataSourceID="ds" CommitChanges="True" AutoRefresh="True">
                            </px:PXSelector>
                            <px:PXDropDown ID="edPerPetTotPeriods" runat="server" DataField="PerPetTotPeriods"
                                Size="SM">
                            </px:PXDropDown>
                            <px:PXTextEdit ID="edExtRef1" runat="server" DataField="ExtRef1" AlreadyLocalized="False">
                            </px:PXTextEdit>
                            <px:PXTextEdit ID="edCustomerOrderNbr" runat="server" DataField="CustomerOrderNbr" AlreadyLocalized="False" DefaultLocale="">
                            </px:PXTextEdit>
                            <px:PXNumberEdit ID="edCuryCtlTotal" runat="server" DataField="CuryCtlTotal" AlreadyLocalized="False">
                            </px:PXNumberEdit>
                            <px:PXSelector ID="ProjectID" runat="server" DataField="ProjectID" DataSourceID="ds">
                            </px:PXSelector>
                            <px:PXSelector CommitChanges="True" ID="edClassID" runat="server" DataField="ClassID"
                                DataMember="_XRBContClass_" DataSourceID="ds">
                            </px:PXSelector>
                            <px:PXDateTimeEdit ID="edContractDate" runat="server" DataField="ContractDate" AlreadyLocalized="False">
                            </px:PXDateTimeEdit>
                            <px:PXDateTimeEdit ID="CancelOn" runat="server" DataField="CancelOn" CommitChanges="True" AlreadyLocalized="False">
                            </px:PXDateTimeEdit>
                            <px:PXLayoutRule runat="server" ControlSize="SM">
                            </px:PXLayoutRule>
                            <px:PXSelector ID="CancelCode" runat="server" DataField="CancelCode" DataSourceID="ds">
                            </px:PXSelector>
                            <px:PXSelector ID="edStateCode" runat="server" DataField="StateCode">
                            </px:PXSelector>
                            <px:PXCheckBox ID="edSigned" runat="server" DataField="Signed" Text="Contract Signed" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edChargeCancelFee" runat="server" DataField="ChargeCancelFee"
                                Text="Charge Cancellation Fee" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edChargeExitFee" runat="server" DataField="ChargeExitFee" Text="Charge Exit Fee" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXSelector ID="edCreateActivityID" runat="server" DataField="CreateActivityID"
                                DataSourceID="ds">
                            </px:PXSelector>
                            <px:PXCheckBox ID="edExcludeReprice" runat="server" DataField="ExcludeReprice"
                                Text="Exclude from Auto-Repricing" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXSelector ID="edPriceClassID" runat="server" DataField="PriceClassID"
                                DataSourceID="ds">
                            </px:PXSelector>
                            <px:PXSelector ID="edARContractID" runat="server" DataField="ARContractID" AllowEdit="True" edit="1">
                            </px:PXSelector>
                            <px:PXSelector ID="edMSAID" runat="server" DataField="MSAID" AllowEdit="True" edit="1">
                            </px:PXSelector>
                            <px:PXCheckBox ID="edTermsBackOff" runat="server" DataField="TermsBackOff"
                                AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXLayoutRule runat="server" GroupCaption="Renewal Options">
                            </px:PXLayoutRule>
                            <px:PXDropDown ID="edFCastOption" runat="server" DataField="FCastOption" Size="XM">
                            </px:PXDropDown>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXNumberEdit ID="edRenewalLen" runat="server" DataField="RenewalLen" AlreadyLocalized="False">
                            </px:PXNumberEdit>
                            <px:PXDropDown ID="edRenewalInt" runat="server" DataField="RenewalInt" Size="S" SuppressLabel="True">
                            </px:PXDropDown>
                            <px:PXLayoutRule runat="server">
                            </px:PXLayoutRule>
                            <px:PXCheckBox CommitChanges="True" ID="chkNotRenewable" runat="server" DataField="NotRenewable" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXCheckBox CommitChanges="True" ID="chkAutoRenew" runat="server" DataField="AutoRenew" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXSelector ID="NRenewRsnCd" runat="server" DataField="NRenewRsnCd" DataSourceID="ds"
                                edit="1" Size="SM">
                            </px:PXSelector>
                            <px:PXSelector ID="edRenewOppTmplt" runat="server" DataField="RenewOppTmplt">
                            </px:PXSelector>
                            <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="SM" LabelsWidth="XS">
                            </px:PXLayoutRule>
                            <px:PXLayoutRule runat="server" GroupCaption="Acknowledgement Report Options" StartGroup="True">
                            </px:PXLayoutRule>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXCheckBox ID="edAckPrinted" runat="server" DataField="AckPrinted" Size="SM"
                                Text="Acknowledgement Report Printed" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edAckDontPrint" runat="server" DataField="AckDontPrint" Size="SM"
                                Text="Don't Print Acknowledgement Report" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXCheckBox ID="edAckEmailed" runat="server" DataField="AckEmailed" Size="SM"
                                Text="Acknowledgement Report Emailed" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edAckDontEmail" runat="server" DataField="AckDontEmail" Size="SM"
                                Text="Don't Email Acknowledgement Report" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXLayoutRule runat="server" GroupCaption="Renewal Report Options" StartGroup="True">
                            </px:PXLayoutRule>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXCheckBox ID="edRenewRptPrinted" runat="server" DataField="RenewRptPrinted"
                                Size="SM" Text="Renewal Report Printed" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edRenewRptDontPrint" runat="server" DataField="RenewRptDontPrint"
                                Size="SM" Text="Don't Print Renewal Report" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXCheckBox ID="edRenewRptEmailed" runat="server" DataField="RenewRptEmailed"
                                Size="SM" Text="Renewal Report Emailed" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXCheckBox ID="edRenewRptDontEmail" runat="server" DataField="RenewRptDontEmail"
                                Size="SM" Text="Don't Email Renewal Report" AlreadyLocalized="False">
                            </px:PXCheckBox>
                            <px:PXLayoutRule runat="server">
                            </px:PXLayoutRule>
                            <px:PXLayoutRule runat="server" GroupCaption="Deposit" StartGroup="True" LabelsWidth="SM">
                            </px:PXLayoutRule>
                            <px:PXDropDown ID="edDepositType" runat="server" CommitChanges="True" DataField="DepositType">
                            </px:PXDropDown>
                            <px:PXNumberEdit ID="edDepositPct" runat="server" DataField="DepositPct" CommitChanges="True" AlreadyLocalized="False">
                            </px:PXNumberEdit>
                            <px:PXNumberEdit ID="edCuryDepositAmt" runat="server" DataField="CuryDepositAmt"
                                CommitChanges="True" AlreadyLocalized="False">
                            </px:PXNumberEdit>
                            <px:PXDropDown ID="edDepositUsage" runat="server" DataField="DepositUsage" Size="SM">
                            </px:PXDropDown>
                            <px:PXSelector ID="edDepositInventoryID" runat="server" DataField="DepositInventoryID"
                                Size="XM">
                            </px:PXSelector>
                            <px:PXDateTimeEdit ID="edDepositInvcDate" runat="server" DataField="DepositInvcDate" AlreadyLocalized="False">
                            </px:PXDateTimeEdit>
                            <px:PXSelector ID="edDepositInvcNbr" runat="server" DataField="DepositInvcNbr" AllowEdit="True"
                                Size="S" edit="1">
                            </px:PXSelector>
                            <px:PXTextEdit ID="edDepositRefNbr" runat="server" DataField="DepositRefNbr" Size="S" AlreadyLocalized="False">
                            </px:PXTextEdit>
                            <px:PXSelector ID="edDepositPaymentMethodID" runat="server" CommitChanges="True"
                                AutoRefresh="True" DataField="DepositPaymentMethodID">
                            </px:PXSelector>
                            <px:PXSelector ID="edDepositProcessingCenterID" runat="server" CommitChanges="True"
                                DataField="DepositProcessingCenterID">
                            </px:PXSelector>
                            <px:PXSelector ID="edDepositPMInstanceID" runat="server" CommitChanges="True" TextField="descr"
                                DataField="DepositPMInstanceID">
                            </px:PXSelector>
                        </Template>
                        <ContentStyle BackColor="Transparent" BorderStyle="None">
                        </ContentStyle>
                    </px:PXFormView>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Sales Orders Information" VisibleExp="Page.SelectedModuleType == OM">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXSelector CommitChanges="True" ID="edSOOrderType" runat="server" DataField="SOOrderType"
                        DataMember="_SOOrderType_">
                    </px:PXSelector>
                    <px:PXTextEdit ID="edCustomerRefNbr" runat="server" DataField="CustomerRefNbr" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXLayoutRule runat="server" ColumnSpan="2">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edShipVia" runat="server" DataField="ShipVia" DataMember="_Carrier_">
                    </px:PXSelector>
                    <px:PXSelector ID="edDestSiteID" runat="server" DataField="DestSiteID" Size="XM">
                    </px:PXSelector>
                    <px:PXDropDown ID="edTransferLocOption" runat="server" DataField="TransferLocOption" Size="S">
                    </px:PXDropDown>
                    <px:PXLayoutRule runat="server" ControlSize="XM" GroupCaption="Ship-To Info" LabelsWidth="M"
                        StartGroup="True">
                    </px:PXLayoutRule>
                    <px:PXTextEdit ID="edShipFullName" runat="server" DataField="ShipFullName" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edShipSalutation" runat="server" DataField="ShipSalutation" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edShipAddressLine1" runat="server" DataField="ShipAddressLine1" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edShipAddressLine2" runat="server" DataField="ShipAddressLine2" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edShipCity" runat="server" DataField="ShipCity" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edShipState" runat="server" DataField="ShipState" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edShipPostalCode" runat="server" DataField="ShipPostalCode" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edShipCountryID" runat="server" DataField="ShipCountryID" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edShipPhone" runat="server" DataField="ShipPhone" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Ad Hoc Payments">
                <Template>
                    <px:PXGrid ID="grdAdHocPay" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 14px;"
                        Width="100%" Height="150px" SkinID="DetailsInTab" SyncPosition="True"
                        AllowSearch="True" TemporaryFilterCaption="Filter Applied">
                        <ActionBar DefaultAction="cmdViewAdHocPayment">
                            <Actions>
                                <Save Enabled="False"></Save>
                                <AddNew Enabled="False"></AddNew>
                                <Delete Enabled="False"></Delete>
                                <EditRecord Enabled="False"></EditRecord>
                            </Actions>
                            <CustomItems>
                                <px:PXToolBarButton Text="Add Payment" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="AddAdHocPayment" Target="ds">
                                    </AutoCallBack>
                                    <PopupCommand Command="Refresh" Target="grdAdHocPay" />
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Text="View Payment Details" Visible="False" Key="cmdViewAdHocPayment" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="ViewAdHocPayment" Target="ds">
                                    </AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <Levels>
                            <px:PXGridLevel DataMember="AdHocPayments" DataKeyNames="PaymentID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="edPaymentID" runat="server" DataField="PaymentID" DataMember="_XPMPaymentHdr_">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edAmount" runat="server" DataField="Amount" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXDateTimeEdit ID="edNextAttempt" runat="server" DataField="NextAttempt" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXDateTimeEdit>
                                    <px:PXDateTimeEdit ID="edPaymentDate" runat="server" DataField="PaymentDate" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXDateTimeEdit>
                                    <px:PXMaskEdit ID="edPaymentMethodID" runat="server" DataField="PaymentMethodID" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXMaskEdit>
                                    <px:PXDropDown ID="edStatus" runat="server" AllowNull="False" DataField="Status"
                                        SelectedIndex="-1">
                                    </px:PXDropDown>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="PaymentID" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Amount">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="NextAttempt" Width="155px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PaymentDate" Width="105px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PaymentMethodID" Width="150px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status" Width="100px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" AllowFormEdit="True" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Payment History">
                <Template>
                    <px:PXGrid ID="gridSchedPayment" runat="server" DataSourceID="ds" Style="z-index: 100; left: 1px; top: 13px;"
                        Width="100%" Height="150px" SkinID="DetailsInTab" SyncPosition="True"
                        AllowSearch="True" TemporaryFilterCaption="Filter Applied">
                        <ActionBar DefaultAction="cmdViewPayment">
                            <Actions>
                                <Save Enabled="False"></Save>
                                <AddNew Enabled="False"></AddNew>
                                <Delete Enabled="False"></Delete>
                                <EditRecord Enabled="False"></EditRecord>
                            </Actions>
                            <CustomItems>
                                <px:PXToolBarButton Text="View Payment Details" Visible="False" Key="cmdViewPayment" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="ViewPayment" Target="ds">
                                    </AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <Levels>
                            <px:PXGridLevel DataMember="PaymentHistory" DataKeyNames="PaymentID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="edPaymentID3" runat="server" DataField="PaymentID" DataMember="_XPMPaymentHdr_">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edAmount3" runat="server" DataField="Amount" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXTextEdit ID="edDescr3" runat="server" DataField="Descr" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXDateTimeEdit ID="edNextAttempt3" runat="server" DataField="NextAttempt" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXDateTimeEdit>
                                    <px:PXDateTimeEdit ID="edPaymentDate3" runat="server" DataField="PaymentDate" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXDateTimeEdit>
                                    <px:PXMaskEdit ID="edPaymentMethodID3" runat="server" DataField="PaymentMethodID" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXMaskEdit>
                                    <px:PXDropDown ID="edStatus3" runat="server" AllowNull="False" DataField="Status"
                                        SelectedIndex="-1">
                                    </px:PXDropDown>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="PaymentID" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Amount">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="NextAttempt" Width="155px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PaymentDate" Width="105px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PaymentMethodID" Width="150px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status" Width="100px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" AllowFormEdit="True" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Default Payment Information">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edPaymentMethodID" runat="server" DataField="PaymentMethodID"
                        AutoRefresh="True" DataMember="_PaymentMethod_" CommitChanges="True">
                    </px:PXSelector>
                    <px:PXSelector ID="edProcessingCenterID" runat="server" DataField="ProcessingCenterID">
                    </px:PXSelector>
                    <px:PXSelector CommitChanges="True" ID="edPMInstanceID" runat="server" DataField="PMInstanceID"
                        AutoGenerateColumns="True" AutoRefresh="True" TextField="descr">
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" StartColumn="True" SuppressLabel="True">
                    </px:PXLayoutRule>
                    <px:PXGrid ID="grdPMInstanceDetails" runat="server" DataSourceID="ds" SkinID="Attributes"
                        BorderStyle="None" MatrixMode="True" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataMember="DefPaymentMethodInstanceDetails" DataKeyNames="PMInstanceID,PaymentMethodID,DetailID">
                                <Columns>
                                    <px:PXGridColumn AllowUpdate="False" DataField="DetailID_PaymentMethodDetail_descr"
                                        Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Value" Width="200px" RenderEditorText="True">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                    <px:PXLayoutRule runat="server" StartColumn="True" SuppressLabel="True">
                    </px:PXLayoutRule>
                    <px:PXFormView ID="frmDefPMInstance" runat="server" CaptionVisible="False" DataMember="DefPaymentMethodInstance"
                        DataSourceID="ds">
                        <Template>
                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                            </px:PXLayoutRule>
                            <px:PXTextEdit ID="edDescr" runat="server" AllowNull="False" DataField="Descr" AlreadyLocalized="False" DefaultLocale="">
                            </px:PXTextEdit>
                        </Template>
                        <ContentStyle BorderStyle="None">
                        </ContentStyle>
                    </px:PXFormView>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Billing Activities">
                <Template>
                    <px:PXGrid ID="grdActivities" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 14px;"
                        Width="100%" Height="150px" SkinID="DetailsInTab" SyncPosition="True"
                        AllowSearch="True" TabIndex="5300" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataMember="BillingActivities" DataKeyNames="BillActivityCD">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="edBillActivityCD" runat="server" DataField="BillActivityCD">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edActivityID" runat="server" DataField="ActivityID">
                                    </px:PXSelector>
                                    <px:PXDateTimeEdit ID="edActivityDate" runat="server" DataField="ActivityDate" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXDateTimeEdit>
                                    <px:PXNumberEdit ID="edFlatExt" runat="server" DataField="FlatExt" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edFlatFee" runat="server" DataField="FlatFee" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edFlatQty" runat="server" DataField="FlatQty" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXDropDown ID="edInvcMethod" runat="server" DataField="InvcMethod">
                                    </px:PXDropDown>
                                    <px:PXNumberEdit ID="edPct" runat="server" DataField="Pct" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edPctExt" runat="server" DataField="PctExt" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXCheckBox ID="edProcessed" runat="server" DataField="Processed" Text="Processed" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXCheckBox ID="edRated" runat="server" DataField="Rated" Text="Rated" AlreadyLocalized="False">
                                    </px:PXCheckBox>
                                    <px:PXNumberEdit ID="edTotalFee" runat="server" DataField="TotalFee" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="BillActivityCD">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActivityID" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActivityDate" Width="90px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FlatExt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FlatFee" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FlatQty" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="InvcMethod">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Pct" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PctExt" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Processed" TextAlign="Center" Type="CheckBox" Width="60px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Rated" TextAlign="Center" Type="CheckBox" Width="60px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TotalFee" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar DefaultAction="cmdViewBillingActivity">
                            <CustomItems>
                                <px:PXToolBarButton Text="Add Billing Activity" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="AddBillingActivity" Target="ds">
                                    </AutoCallBack>
                                    <PopupCommand Command="Refresh" Target="grdActivities">
                                    </PopupCommand>
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Text="View Activity Details" Visible="False" Key="cmdViewBillingActivity" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="ViewBillingActivity" Target="ds">
                                    </AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <Mode AllowAddNew="False" AllowUpdate="False" AllowFormEdit="True" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Promise to Pay">
                <Template>
                    <px:PXGrid ID="grdPromisePay" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 14px;"
                        Width="100%" Height="300px" SkinID="DetailsInTab" SyncPosition="True"
                        AllowSearch="True" TabIndex="5300" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataMember="PromisesToPay" DataKeyNames="PromiseID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="SM" LabelsWidth="M">
                                    </px:PXLayoutRule>
                                    <px:PXMaskEdit ID="edPayPromiseID" runat="server" DataField="PromiseID" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXMaskEdit>
                                    <px:PXTextEdit ID="edPayDescr" runat="server" DataField="Descr" Size="L" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXDateTimeEdit ID="edPayPromiseDate" runat="server" DataField="PromiseDate" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXDateTimeEdit>
                                    <px:PXDropDown ID="edPayStatus" runat="server" DataField="Status" Size="XS">
                                    </px:PXDropDown>
                                    <px:PXNumberEdit ID="edPayTotPromised" runat="server" DataField="TotPromised" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edPayTotPaid" runat="server" DataField="TotPaid" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="PromiseID" Width="100px" LinkCommand="ViewPromiseToPay">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PromiseDate" Width="125px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TotPromised" TextAlign="Right" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TotPaid" TextAlign="Right" Width="200px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar DefaultAction="cmdViewPromiseToPay">
                            <CustomItems>
                                <px:PXToolBarButton Text="Add Promise to Pay" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="AddPromiseToPay" Target="ds">
                                    </AutoCallBack>
                                    <PopupCommand Command="Refresh" Target="grdPromisePay">
                                    </PopupCommand>
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Text="Promise Details" Key="cmdViewPromiseToPay" Visible="False" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="ViewPromiseToPay" Target="ds">
                                    </AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" AllowFormEdit="True" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Meters">
                <Template>
                    <px:PXGrid ID="grdMeters" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 14px;"
                        Width="100%" Height="300px" SkinID="DetailsWithFilter" SyncPosition="True" TabIndex="5300" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataMember="Meters" DataKeyNames="MeterID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="SM" LabelsWidth="M">
                                    </px:PXLayoutRule>
                                    <px:PXMaskEdit ID="edMeterMeterID" runat="server" DataField="MeterID" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXMaskEdit>
                                    <px:PXTextEdit ID="edMeterDescription" runat="server" DataField="Description" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edMeterExternalID" runat="server" DataField="ExternalID" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXSelector ID="edMeterInventoryID" runat="server" DataField="InventoryID">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edMeterLastBilledQty" runat="server" DataField="LastBilledQty" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXNumberEdit>
                                    <px:PXTextEdit ID="edMeterMeterType" runat="server" DataField="MeterType" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="MeterID" Width="100px" LinkCommand="ViewCustomerMeter">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Description" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ExternalID" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="InventoryID" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LastBilledQty" TextAlign="Right" Width="125px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="MeterType" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ContractCD" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="RevisionNbr" Width="100px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowFormEdit="True" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Contacts">
                <Template>
                    <px:PXGrid ID="grdContacts" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 14px;"
                        Width="100%" Height="300px" SkinID="DetailsInTab" SyncPosition="True"
                        AllowSearch="True" TabIndex="5300" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="ContactID" DataMember="ConnectedContacts">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server">
                                    </px:PXLayoutRule>
                                    <px:PXTextEdit ID="edFirstName" runat="server" DataField="FirstName" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edLastName" runat="server" DataField="LastName" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edAddress__AddressLine1" runat="server" DataField="Address__AddressLine1" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edAddress__AddressLine2" runat="server" DataField="Address__AddressLine2" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edAddress__City" runat="server" DataField="Address__City" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXSelector ID="edAddress__State" runat="server" DataField="Address__State">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edAddress__PostalCode" runat="server" DataField="Address__PostalCode" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edEMail" runat="server" DataField="EMail" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edPhone1" runat="server" DataField="Phone1" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXDropDown ID="edPhone1Type" runat="server" DataField="Phone1Type">
                                    </px:PXDropDown>
                                    <px:PXDropDown ID="edGender" runat="server" DataField="Gender">
                                    </px:PXDropDown>
                                    <px:PXDateTimeEdit ID="edBirthday" runat="server" DataField="Birthday" AlreadyLocalized="False" DefaultLocale="">
                                    </px:PXDateTimeEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="FirstName" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LastName" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__AddressLine1" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__AddressLine2" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__City" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__State" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__PostalCode" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EMail" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Phone1" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Phone1Type">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Gender">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Birthday" Width="90px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar>
                            <CustomItems>
                                <px:PXToolBarButton Text="Remove Contact" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="RemoveContact" Target="ds">
                                    </AutoCallBack>
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Text="Add Contact" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="AddContact" Target="ds">
                                    </AutoCallBack>
                                    <PopupCommand Command="Refresh" Target="grdContacts" />
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Text="Add New Contact" AlreadyLocalized="False" SuppressHtmlEncoding="False">
                                    <AutoCallBack Command="AddNewContact" Target="ds">
                                    </AutoCallBack>
                                    <PopupCommand Command="Refresh" Target="grdContacts" />
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" AllowFormEdit="True" />
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
        <AutoSize Container="Window" Enabled="True" MinHeight="150"></AutoSize>
    </px:PXTab>
    <px:PXSmartPanel ID="popupDistTaxes" runat="server" CaptionVisible="true" Caption="Taxes"
        Height="290px" Width="700px" DesignView="Content" AutoCallBack-Command="Refresh"
        AutoCallBack-Enabled="True" AutoCallBack-Target="grdDistTaxes" Key="DistributionTaxes">
        <px:PXGrid ID="grdDistTaxes" runat="server" DataSourceID="ds" Style="z-index: 100;"
            Width="100%" Height="150px" SkinID="Details">
            <Levels>
                <px:PXGridLevel DataKeyNames="ContractID,ContrDetLineNbr,DistLineNbr,LineNbr" DataMember="DistributionTaxes">
                    <RowTemplate>
                        <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                        </px:PXLayoutRule>
                        <px:PXSelector ID="edTaxID" runat="server" AllowNull="False" DataField="TaxID" DataMember="_Tax_">
                        </px:PXSelector>
                    </RowTemplate>
                    <Columns>
                        <px:PXGridColumn AllowNull="False" DataField="TaxID">
                        </px:PXGridColumn>
                    </Columns>
                </px:PXGridLevel>
            </Levels>
        </px:PXGrid>
        <px:PXPanel ID="PXPanel1" runat="server" SkinID="Buttons">
            <px:PXButton ID="DistTaxOK" runat="server" DialogResult="OK" Text="OK">
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
    <px:PXSmartPanel ID="popupAutoGen" runat="server" CaptionVisible="True" Caption="Auto Generate Billing Schedule"
        Height="650px" Width="1000px" DesignView="Content"
        AutoCallBack-Command="Refresh" AutoCallBack-Target="grdDistTaxes" Key="AutoGenDist"
        AutoReload="True" LoadOnDemand="True" AutoRepaint="True"
        TabIndex="13700">
        <px:PXFormView ID="frmAutoGenHeader" runat="server" DataSourceID="ds" Style="z-index: 100"
            Width="100%" DataMember="AutoGenHeader" TabIndex="-8636">
            <Template>
                <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="XM" ControlSize="XXL">
                </px:PXLayoutRule>
                <px:PXDropDown CommitChanges="True" ID="edAction" runat="server" DataField="Action" >
                </px:PXDropDown>
                <px:PXDropDown ID="edScheduleType" runat="server" DataField="ScheduleType"
                    Size="S" CommitChanges="True">
                </px:PXDropDown>
                <px:PXLayoutRule runat="server" ControlSize="XS">
                </px:PXLayoutRule>
                <px:PXSelector ID="edAutoGenId" runat="server" DataField="AutoGenId"
                    DataSourceID="ds" CommitChanges="True">
                </px:PXSelector>
                <px:PXLayoutRule runat="server" ControlSize="XM">
                </px:PXLayoutRule>
                <px:PXTextEdit ID="edAutoGenDescr" runat="server" DataField="AutoGenDescr">
                </px:PXTextEdit>
                <px:PXDropDown CommitChanges="True" ID="edAutoGenDescrOption" runat="server" DataField="AutoGenDescrOption" Size="SM">
                </px:PXDropDown>
                <px:PXDateTimeEdit ID="edGenBegDate" runat="server" DataField="GenBegDate">
                </px:PXDateTimeEdit>
                <px:PXLayoutRule runat="server" ControlSize="S">
                </px:PXLayoutRule>
                <px:PXDropDown ID="edAllocateBy" runat="server" DataField="AllocateBy" CommitChanges="True">
                </px:PXDropDown>
                <px:PXDropDown ID="edTotPeriods" runat="server" DataField="TotPeriods" CommitChanges="True">
                </px:PXDropDown>
                <px:PXNumberEdit ID="edNbrBills" runat="server" DataField="NbrBills">
                </px:PXNumberEdit>
                <px:PXCheckBox ID="edUpdateHdrFreq" runat="server" DataField="UpdateHdrFreq" />
                <px:PXCheckBox ID="edSingleMeter" runat="server" DataField="SingleMeter" />
            </Template>
        </px:PXFormView>
        <px:PXGrid ID="grdAutoGen" runat="server" DataSourceID="ds" Style="z-index: 100;"
            Width="100%" Height="150px" SkinID="Details" SyncPosition="True" TabIndex="4500">
            <Levels>
                <px:PXGridLevel DataKeyNames="LineNbr" DataMember="AutoGenDist">
                    <RowTemplate>
                        <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="M"
                            StartColumn="True">
                        </px:PXLayoutRule>
                        <px:PXSelector ID="edInventoryID" runat="server" AutoRefresh="True"
                            DataField="InventoryID">
                        </px:PXSelector>
                        <px:PXTextEdit ID="Descr" runat="server" DataField="Descr">
                        </px:PXTextEdit>
                        <px:PXSegmentMask ID="edSubItemID" runat="server" DataField="SubItemID">
                        </px:PXSegmentMask>
                        <px:PXSelector ID="edAccountID" runat="server" DataField="AccountID"
                            DataMember="_Account_">
                        </px:PXSelector>
                        <px:PXSelector ID="edTaskID" runat="server" DataField="TaskID">
                        </px:PXSelector>
                        <px:PXSelector ID="edSubID" runat="server" DataField="SubID" DataMember="_Sub_">
                        </px:PXSelector>
                        <px:PXSelector ID="edPriceClassID3" runat="server" AutoRefresh="True" CommitChanges="True"
                            DataField="PriceClassID">
                        </px:PXSelector>
                        <px:PXNumberEdit ID="edQty" runat="server" CommitChanges="True" DataField="Qty">
                        </px:PXNumberEdit>
                        <px:PXSelector ID="edUOM" runat="server" DataField="UOM"
                            DataMember="_INUnit_AutoGen.inventoryID_AutoGen.inventoryID_">
                        </px:PXSelector>
                        <px:PXNumberEdit ID="edCuryUnitPrice" runat="server" CommitChanges="True"
                            DataField="CuryUnitPrice">
                        </px:PXNumberEdit>
                        <px:PXNumberEdit ID="Pct" runat="server" CommitChanges="True" DataField="Pct">
                        </px:PXNumberEdit>
                        <px:PXNumberEdit ID="edAutoGenDiscPct" runat="server" CommitChanges="True" DataField="DiscPct">
                        </px:PXNumberEdit>
                        <px:PXNumberEdit ID="edAutoGenCuryDiscAmt" runat="server" CommitChanges="True" DataField="CuryDiscAmt">
                        </px:PXNumberEdit>
                        <px:PXCheckBox ID="edAutogenManualDisc" runat="server" DataField="ManualDisc" Text="ManualDisc" AlreadyLocalized="False" CommitChanges="True">
                        </px:PXCheckBox>
                        <px:PXNumberEdit ID="edCuryEndUserPrc" runat="server"
                            DataField="CuryEndUserPrc">
                        </px:PXNumberEdit>
                        <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="M"
                            StartColumn="True">
                        </px:PXLayoutRule>
                        <px:PXNumberEdit ID="edCuryAmount" runat="server" DataField="CuryAmount">
                        </px:PXNumberEdit>
                        <px:PXSelector ID="edSalesPersonID" runat="server" DataField="SalesPersonID"
                            DataMember="_SalesPerson_">
                        </px:PXSelector>
                        <px:PXSelector ID="edDeferredCode" runat="server" DataField="DeferredCode">
                        </px:PXSelector>
                        <px:PXSelector ID="edTaxCategoryID" runat="server" DataField="TaxCategoryID"
                            DataMember="_TaxCategory_">
                        </px:PXSelector>
                        <px:PXCheckBox ID="edCreateMeter" runat="server" DataField="CreateMeter"
                            Text="Create New Meter">
                        </px:PXCheckBox>
                        <px:PXSelector ID="edAutoGenMeterID" runat="server" DataField="MeterID">
                        </px:PXSelector>
                    </RowTemplate>
                    <Columns>
                        <px:PXGridColumn CommitChanges="True" DataField="InventoryID" Width="108px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="Descr" Width="200px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="SubItemID">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="AccountID" Width="108px" CommitChanges="True">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="TaskID" Width="108px">
                        </px:PXGridColumn>
                        <px:PXGridColumn CommitChanges="True" DataField="SubID" Width="108px">
                        </px:PXGridColumn>
                        <px:PXGridColumn CommitChanges="True" DataField="PriceClassID" Width="108px">
                        </px:PXGridColumn>
                        <px:PXGridColumn AllowNull="False" CommitChanges="True" DataField="Qty"
                            TextAlign="Right">
                        </px:PXGridColumn>
                        <px:PXGridColumn CommitChanges="True" DataField="UOM">
                        </px:PXGridColumn>
                        <px:PXGridColumn AllowNull="False" CommitChanges="True"
                            DataField="CuryUnitPrice" TextAlign="Right" Width="90px">
                        </px:PXGridColumn>
                        <px:PXGridColumn AllowNull="False" CommitChanges="True"
                            DataField="CuryExtPrice" TextAlign="Right" Width="90px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="Pct" TextAlign="Right" Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="ManualDisc" TextAlign="Center" Width="100px" Type="CheckBox" CommitChanges="True">
                        </px:PXGridColumn>
                        <px:PXGridColumn AllowNull="False" CommitChanges="True"
                            DataField="DiscPct" TextAlign="Right" Width="90px">
                        </px:PXGridColumn>
                        <px:PXGridColumn AllowNull="False" CommitChanges="True"
                            DataField="CuryDiscAmt" TextAlign="Right" Width="90px">
                        </px:PXGridColumn>


                        <px:PXGridColumn AllowNull="False" DataField="CuryEndUserPrc" TextAlign="Right"
                            Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn AllowNull="False" DataField="CuryAmount" TextAlign="Right">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="SalesPersonID" Width="108px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="DeferredCode">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="TaxCategoryID" Width="108px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="SiteID">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="CreateMeter" TextAlign="Center" Type="CheckBox"
                            Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="MeterID" Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="ExplodeBundle" TextAlign="Center" Type="CheckBox"
                            Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn CommitChanges="True" DataField="RenewalPriceMthd"
                            Type="DropDownList" Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn AllowNull="False" DataField="CuryRenewalPrice"
                            TextAlign="Right" Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn AllowNull="False" DataField="RenewalPricePct"
                            TextAlign="Right" Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="RenewalPriceClassID" Width="108px">
                        </px:PXGridColumn>
                    </Columns>
                </px:PXGridLevel>
            </Levels>
            <ActionBar>
                <CustomItems>
                    <px:PXToolBarButton Text="Load Template">
                        <AutoCallBack Command="AutoGenTemplateLoad" Target="ds">
                        </AutoCallBack>
                    </px:PXToolBarButton>
                </CustomItems>
            </ActionBar>
        </px:PXGrid>
        <px:PXPanel ID="PXPanel2" runat="server" SkinID="Buttons">
            <px:PXButton ID="AutoGenOK" runat="server" DialogResult="OK" Text="OK">
                <AutoCallBack Command="Commit" Target="ds">
                    <Behavior CommitChanges="True" />
                </AutoCallBack>
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
    <px:PXSmartPanel ID="popupAutoGenTaxes" runat="server" CaptionVisible="true" Caption="Taxes"
        Height="325px" Width="700px" DesignView="Content" Key="AutoGenDistTaxes" AutoReload="True"
        AutoRepaint="True" LoadOnDemand="True">
        <px:PXGrid ID="grdAutoGenTaxes" runat="server" DataSourceID="ds" Style="z-index: 100;"
            Width="100%" Height="150px" SkinID="Details">
            <Levels>
                <px:PXGridLevel DataKeyNames="AutoGenLineNbr,LineNbr" DataMember="AutoGenDistTaxes">
                    <RowTemplate>
                        <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                        </px:PXLayoutRule>
                        <px:PXLayoutRule runat="server" Merge="True">
                        </px:PXLayoutRule>
                        <px:PXLabel Size="xs" ID="lblAutoGenTaxID" runat="server">Tax ID:</px:PXLabel>
                        <px:PXSelector Size="s" ID="edAutoGenTaxID" runat="server" AllowNull="False" DataField="TaxID"
                            DataMember="_Tax_">
                        </px:PXSelector>
                        <px:PXLayoutRule runat="server" Merge="False">
                        </px:PXLayoutRule>
                    </RowTemplate>
                    <Columns>
                        <px:PXGridColumn AllowNull="False" DataField="TaxID">
                        </px:PXGridColumn>
                    </Columns>
                </px:PXGridLevel>
            </Levels>
        </px:PXGrid>
        <px:PXPanel ID="PXPanel3" runat="server" SkinID="Buttons">
            <px:PXButton ID="cmdAutoGenTaxesOK" runat="server" DialogResult="OK" Text="OK">
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
    <px:PXSmartPanel ID="popupTemplate" runat="server" CaptionVisible="true" Caption="Load Distributions from Template"
        Height="90px" Width="350px" DesignView="Content" Key="Tmplt">
        <px:PXFormView ID="frmTemplate" runat="server" DataSourceID="ds" Style="z-index: 100"
            Width="100%" DataMember="Tmplt" TabIndex="16364">
            <Template>
                <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="SM">
                </px:PXLayoutRule>
                <px:PXSelector CommitChanges="True" ID="edTemplateID" runat="server" DataField="TemplateID"
                    DataMember="_XRBTempltHdr_XRBContrHdr.curyID_" DataSourceID="ds">
                </px:PXSelector>
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="PXPanel4" runat="server" SkinID="Buttons">
            <px:PXButton ID="cmdATemplateOK" runat="server" DialogResult="OK" Text="OK">
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
    <px:PXSmartPanel ID="popupRenew" runat="server" CaptionVisible="true" Caption="Contract Renewal"
        Height="90px" Width="350px" DesignView="Content" Key="Tmplt">
        <px:PXFormView ID="PXFormView1" runat="server" DataSourceID="ds" Style="z-index: 100"
            Width="100%" DataMember="Tmplt" TabIndex="16364">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="S"
                    ControlSize="SM">
                </px:PXLayoutRule>
                <px:PXSelector CommitChanges="True" ID="edTemplateID" runat="server" DataField="TemplateID"
                    DataMember="_XRBTempltHdr_XRBContrHdr.curyID_" DataSourceID="ds">
                </px:PXSelector>
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="PXPanel5" runat="server" SkinID="Buttons">
            <px:PXButton ID="PXButton1" runat="server" DialogResult="OK" Text="OK">
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
    <px:PXSmartPanel ID="popupAddContact" runat="server" CaptionVisible="true" Caption="Add Contact"
        Height="89px" Width="713px" DesignView="Content" AutoCallBack-Command="Refresh"
        AutoCallBack-Enabled="True" AutoCallBack-Target="grdContacts" Key="AddContactID"
        AutoReload="True" AutoRepaint="True">
        <px:PXFormView ID="frmContactID" runat="server" DataSourceID="ds" Style="z-index: 100"
            Width="100%" DataMember="AddContactID" TabIndex="16364">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="S"
                    ControlSize="SM">
                </px:PXLayoutRule>
                <px:PXSelector CommitChanges="True" ID="edContactID" runat="server" DataField="ContactID"
                    DataSourceID="ds">
                </px:PXSelector>
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="pnlAddContactOK" runat="server" SkinID="Buttons">
            <px:PXButton ID="btnAddConteactOK" runat="server" DialogResult="OK" Text="OK">
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
    <px:PXSmartPanel ID="popupVoidBill" runat="server" CaptionVisible="True" Caption="Void the Currently Selected Bill?"
        Height="114px" Width="713px" DesignView="Content"
        Key="VoidLineReplacement" AutoReload="True"
        AutoRepaint="True" TabIndex="7900">
        <px:PXFormView ID="frmVoidBill" runat="server" DataSourceID="ds" Style="z-index: 100"
            Width="100%" DataMember="VoidLineReplacement" TabIndex="16364">
            <Template>
                <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S"
                    ControlSize="SM">
                </px:PXLayoutRule>
                <px:PXCheckBox ID="edVoidLineReplace" runat="server" DataField="VoidLineReplace"
                    Text="Replace the Voided Line With a New Scheduled Bill" CommitChanges="True">
                </px:PXCheckBox>
                <px:PXDateTimeEdit ID="edCreditMemoDate" runat="server"
                    DataField="CreditMemoDate">
                </px:PXDateTimeEdit>
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="pnlVoidConfirmBtn" runat="server" SkinID="Buttons">
            <px:PXButton ID="btnVoidConfirmOK" runat="server" DialogResult="OK" Text="OK">
            </px:PXButton>
            <px:PXButton ID="btnVoidConfirmCancel" runat="server" DialogResult="Cancel" Text="Cancel">
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
    <px:PXSmartPanel ID="popupBundle" runat="server" CaptionVisible="True" Caption="Add Bundle"
        Height="100px" Width="713px" DesignView="Content" Key="BundleItem" AutoReload="True"
        AutoRepaint="True" TabIndex="7500">
        <px:PXFormView ID="frmAddBundle" runat="server" DataSourceID="ds" Style="z-index: 100"
            Width="100%" DataMember="BundleItem" TabIndex="16364">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="S"
                    ControlSize="SM">
                </px:PXLayoutRule>
                <px:PXSelector ID="edBundleID" runat="server" DataField="BundleID" AutoRefresh="True">
                </px:PXSelector>
                <px:PXNumberEdit ID="edBundleQty" runat="server" DataField="Qty" CommitChanges="True">
                </px:PXNumberEdit>
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="plnAddBundleBtn" runat="server" SkinID="Buttons">
            <px:PXButton ID="btnAddBundleOK" runat="server" DialogResult="OK" Text="OK">
            </px:PXButton>
            <px:PXButton ID="btnAddBundleCancel" runat="server" DialogResult="Cancel" Text="Cancel">
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
</asp:Content>
