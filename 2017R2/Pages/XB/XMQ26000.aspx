<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XMQ26000.aspx.cs" Inherits="Page_XMQ26000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" Width="100%" runat="server" PrimaryView="Frequencies"  Visible="True"
        TypeName="MaxQ.Products.Billing.BillActFreqMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Primary" TabIndex="800">
		<Levels>
			<px:PXGridLevel DataKeyNames="Freq" DataMember="Frequencies">
			    <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXMaskEdit ID="edFreq" runat="server" DataField="Freq">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edDays" runat="server" DataField="Days">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Freq" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Days" Width="200px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXGrid>
</asp:Content>
