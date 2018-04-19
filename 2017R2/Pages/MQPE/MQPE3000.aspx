<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="MQPE3000.aspx.cs" Inherits="Page_MQPE3000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Transaction"
        TypeName="MaxQ.Products.ProcessEngine.MQPETransactionEntry">
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="Transaction" TabIndex="-4500">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
            <px:PXSelector ID="edTransactionID" runat="server" DataField="TransactionID" Size="SM" CommitChanges="True">
                <AutoCallBack Command="Cancel" Target="ds">
                </AutoCallBack>
            </px:PXSelector>
            <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" />
            <px:PXDropDown ID="edStatus" runat="server" AllowNull="False" DataField="Status" Size="SM" />
            <px:PXTextEdit ID="edHandlerName" runat="server" DataField="HandlerName" Width="500px" />
            <px:PXTab ID="tabMessageInfo" runat="server" Width="100%" Height="150px" DataSourceID="ds">
                <Items>
                    <px:PXTabItem Text="Message Details">
                        <Template>
                            <px:PXFormView ID="frmMessageInstance" runat="server" CaptionVisible="False" DataMember="SelectedTransaction"
                                DataSourceID="ds" RenderStyle="Simple" TabIndex="9900">
                                <Template>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                                    <px:PXTextEdit ID="edMessage" runat="server" AllowNull="False" DataField="Message" />
                                </Template>
                                <ContentStyle BorderStyle="None" />
                            </px:PXFormView>
                        </Template>
                    </px:PXTabItem>
                </Items>
                <AutoSize Container="Window" Enabled="True" MinHeight="150" />
            </px:PXTab>
        </Template>
    </px:PXFormView>
</asp:Content>
