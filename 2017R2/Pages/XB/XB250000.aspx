<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB250000.aspx.cs" Inherits="Page_XB250000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="MaxQ.Products.RBRR.TemplateMaintenance"
        PrimaryView="TemplateHeader" BorderStyle="NotSet" SuspendUnloading="False" >
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Last" PostData="Self">
            </px:PXDSCallbackCommand>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="TemplateHeader" TabIndex="6500">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="SM">
            </px:PXLayoutRule>
            <px:PXSelector ID="edTemplateCD" runat="server" DataField="TemplateCD" DataSourceID="ds">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" ControlSize="L">
            </px:PXLayoutRule>
            <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
            </px:PXTextEdit>
            <px:PXLayoutRule runat="server" ControlSize="SM">
            </px:PXLayoutRule>
            <px:PXCheckBox ID="chkActive" runat="server" DataField="Active">
            </px:PXCheckBox>
            <px:PXDropDown CommitChanges="True" ID="edOrdType" runat="server" AllowNull="False"
                DataField="OrdType" SelectedIndex="-1">
            </px:PXDropDown>
            <px:PXCheckBox ID="UseTmpltPricing" runat="server" DataField="UseTmpltPricing" Text="Use Template Pricing">
                <AutoCallBack Command="Save" Target="form">
                </AutoCallBack>
            </px:PXCheckBox>
            <px:PXCheckBox ID="AutoUpdateContracts" runat="server" DataField="AutoUpdateContracts"
                Text="Automatically Update Associated Contracts">
            </px:PXCheckBox>
            <px:PXSelector ID="CPriceClassID" runat="server" DataField="CPriceClassID" DataSourceID="ds">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" ControlSize="XM">
            </px:PXLayoutRule>
            <pxa:PXCurrencyRate ID="edCuryID" runat="server" DataField="CuryID" DataMember="_Currency_"
                RateTypeView="_XRBTempltHdr_CurrencyInfo_" DataSourceID="ds"></pxa:PXCurrencyRate>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        Height="150px" SkinID="Details" TabIndex="500">
        <Levels>
            <px:PXGridLevel DataKeyNames="LineNbr,TemplateID" DataMember="TemplateDetails">
                <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edAccountID" runat="server" DataField="AccountID" DataMember="_Account_">
                    </px:PXSelector>
                    <px:PXSelector ID="edInventoryID" runat="server" DataField="InventoryID" DataMember="_InventoryItem_">
                    </px:PXSelector>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
                    </px:PXTextEdit>
                    <px:PXSegmentMask ID="SubItemID" runat="server" DataField="SubItemID">
                    </px:PXSegmentMask>
                    <px:PXCheckBox ID="chkNotRenewable" runat="server" DataField="NotRenewable">
                    </px:PXCheckBox>
                    <px:PXNumberEdit ID="edQty" runat="server" DataField="Qty">
                    </px:PXNumberEdit>
                    <px:PXSelector ID="edSiteID" runat="server" DataField="SiteID" DataMember="_INSite_">
                    </px:PXSelector>
                    <px:PXSelector ID="edSubID" runat="server" DataField="SubID" DataMember="_Sub_">
                    </px:PXSelector>
                    <px:PXSelector ID="edTaxCategoryID" runat="server" DataField="TaxCategoryID" DataMember="_TaxCategory_">
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="UOM" runat="server" DataField="UOM">
                    </px:PXSelector>
                    <px:PXCheckBox ID="chkCommissionable" runat="server" DataField="Commissionable" >
                    </px:PXCheckBox>
                    <px:PXSelector ID="DeferredCode" runat="server" DataField="DeferredCode" >
                    </px:PXSelector>
                    <px:PXDropDown ID="edPMDeltaOption" runat="server" DataField="PMDeltaOption" />
                    <px:PXNumberEdit ID="edCuryAmount" runat="server" DataField="CuryAmount">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edCuryEndUserPrc" runat="server" DataField="CuryEndUserPrc">
                    </px:PXNumberEdit>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edCuryUnitPrice" runat="server" DataField="CuryUnitPrice">
                    </px:PXNumberEdit></RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="InventoryID" Width="100px" AutoCallBack="True">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="SubItemID">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Qty" TextAlign="Right" AllowNull="False">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="UOM">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="SiteID" Width="70px" >
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CuryUnitPrice" TextAlign="Right" AllowNull="False" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn AllowNull="False" DataField="CuryAmount" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="AccountID" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="SubID" Width="150px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DeferredCode" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="TaxCategoryID" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Commissionable" TextAlign="Center" Type="CheckBox" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="PMDeltaOption" Type="DropDownList" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CuryEndUserPrc" AllowNull="False" TextAlign="Right" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="NotRenewable" AllowNull="False" TextAlign="Center" Type="CheckBox" Width="110px">
                    </px:PXGridColumn>
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150"></AutoSize>
    </px:PXGrid>
</asp:Content>
