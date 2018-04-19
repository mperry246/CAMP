<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB310000.aspx.cs" Inherits="Page_XB310000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" PrimaryView="ForecastClass" TypeName="MaxQ.Products.RBRR.ForecastClassMaint" Visible="True" SuspendUnloading="False">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="1500">
		<Levels>
			<px:PXGridLevel DataKeyNames="ClassID" DataMember="ForecastClass">
			    <RowTemplate>
                    <px:PXMaskEdit ID="edClassID" runat="server" DataField="ClassID">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
                    </px:PXTextEdit>
                    <px:PXCheckBox ID="edOneTime" runat="server" DataField="OneTime" Text="OneTime">
                    </px:PXCheckBox>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="ClassID" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OneTime" TextAlign="Center" Type="CheckBox" Width="120px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXGrid>
</asp:Content>
