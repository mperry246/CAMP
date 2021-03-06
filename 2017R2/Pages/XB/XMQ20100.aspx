<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XMQ20100.aspx.cs" Inherits="Page_XMQ20100" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" Width="100%" runat="server" Visible="True"
        PrimaryView="ActivityCodes" 
        TypeName="MaxQ.Products.Billing.ActivityCategoryMaint" 
    BorderStyle="NotSet" SuspendUnloading="False">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" 
    DataSourceID="ds" SkinID="Primary" TabIndex="500">
		<Levels>
			<px:PXGridLevel DataMember="ActivityCodes">
			    <RowTemplate>
                    <px:PXMaskEdit ID="CategoryCD" runat="server" DataField="CategoryCD">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="Descr" runat="server" DataField="Descr">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="CategoryCD" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXGrid>
</asp:Content>
