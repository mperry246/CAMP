<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB260000.aspx.cs" Inherits="Page_XB260000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" Width="100%" runat="server" PrimaryView="ContractStateCodes" Visible="True"
        SuspendUnloading="False" 
        TypeName="MaxQ.Products.RBRR.ContractStateCodeMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Primary" TabIndex="500">
		<Levels>
			<px:PXGridLevel DataMember="ContractStateCodes">
			    <RowTemplate>
                    <px:PXLayoutRule runat="server" ColumnWidth="S" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXMaskEdit ID="edStateCode" runat="server" DataField="StateCode">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" 
                        Size="XM">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="StateCode" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Description" Width="200px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	    <Mode AllowFormEdit="True" />
	</px:PXGrid>
</asp:Content>
