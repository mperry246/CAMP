<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XMQ10100.aspx.cs" Inherits="Page_XMQ10100" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="BillingSetup" TypeName="MaxQ.Products.Billing.BillingSetupMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" 
        Width="100%" DataMember="BillingSetup" TabIndex="4600">
		<Template>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXMaskEdit ID="edLastActivityCD" runat="server" DataField="LastActivityCD">
            </px:PXMaskEdit>
            <px:PXMaskEdit ID="edLastProfileID" runat="server" DataField="LastProfileID">
            </px:PXMaskEdit>
            <px:PXMaskEdit ID="edLastRevActivityCD" runat="server" 
                DataField="LastRevActivityCD">
            </px:PXMaskEdit>
            <px:PXSelector ID="edActBillARAccountID" runat="server" 
                DataField="ActBillARAccountID">
            </px:PXSelector>
            <px:PXSelector ID="edActBillARSubID" runat="server" DataField="ActBillARSubID">
            </px:PXSelector>
        </Template>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXFormView>
</asp:Content>
