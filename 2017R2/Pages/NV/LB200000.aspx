<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="LB200000.aspx.cs" Inherits="Page_LB200000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="NVLockboxRecords" TypeName="NV.Lockbox.NVLockboxMaint" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="2000">
		<Levels>
			<px:PXGridLevel DataMember="NVLockboxRecords" DataKeyNames="LockboxCD">
			    <Columns>
                    <px:PXGridColumn DataField="LockboxCD" Width="100px" CommitChanges="true" />
                    <px:PXGridColumn DataField="Description" Width="175px" />
                    <px:PXGridColumn DataField="CashAccountID" Width="175px" TextAlign="Left" DisplayMode="Text" CommitChanges="true" />
                    <px:PXGridColumn DataField="InTestMode" TextAlign="Center" Type="CheckBox" Width="60px" />
                    <px:PXGridColumn DataField="PaymentMethodID" Width="125px" />
                    <px:PXGridColumn DataField="LineofBusiness" Width="175px" DisplayMode="Hint" />
                    <px:PXGridColumn DataField="GatewayAPILogin" Width="175px" />
                    <px:PXGridColumn DataField="GatewayTransactionKey" Width="175px" />
                    <px:PXGridColumn DataField="DefaultGateway" TextAlign="Center" Type="CheckBox" Width="60px" />
                    </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXGrid>
</asp:Content>
