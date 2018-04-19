<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB320000.aspx.cs" Inherits="Page_XB320000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Milestones" TypeName="MaxQ.Products.RBRR.MilestoneMaint" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="1500" TemporaryFilterCaption="Filter Applied">
		<Levels>
			<px:PXGridLevel DataMember="Milestones">
			    <RowTemplate>
                    <px:PXMaskEdit ID="edMilestoneID" runat="server" DataField="MilestoneID" DefaultLocale="">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" DefaultLocale="">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="MilestoneID" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXGrid>
</asp:Content>
