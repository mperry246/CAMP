<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CP202000.aspx.cs" Inherits="Page_CP202000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="ManufacturerRecords" TypeName="CAMPCustomization.CAMPManufacturerMaint" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
        FastFilterFields="ManufacturerCD,Description" AllowPaging="True" AllowSearch="True" 
        AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="100">
		<Levels>
			<px:PXGridLevel DataKeyNames="ManufacturerCD" DataMember="ManufacturerRecords">
			    <RowTemplate>
                    <px:PXMaskEdit ID="edManufacturerCD" runat="server" DataField="ManufacturerCD">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescription" runat="server" DataField="Description">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="ManufacturerCD" Width="150"/>
                    <px:PXGridColumn DataField="Description" Width="300px"/>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
        <Mode AllowUpload="True" />
	</px:PXGrid>
</asp:Content>