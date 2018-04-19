<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CP203000.aspx.cs" Inherits="Page_CP203000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="TapeModelRecords" TypeName="CAMPCustomization.CAMPTapeModelMaint" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
        FastFilterFields="TapeModelCD,Description" AllowPaging="True" AllowSearch="True" 
        AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="100">
		<Levels>
			<px:PXGridLevel DataKeyNames="TapeModelCD" DataMember="TapeModelRecords">
			    <RowTemplate>
                    <px:PXMaskEdit ID="edTapeModelCD" runat="server" DataField="TapeModelCD">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescription" runat="server" DataField="Description">
                    </px:PXTextEdit>
                    <px:PXSelector ID="edManufacturerID" runat="server" DataField="ManufacturerID">
                    </px:PXSelector>
                    <px:PXTextEdit ID="edOPSModelID" runat="server" DataField="OPSModelID">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="TapeModelCD" Width="150"/>
                    <px:PXGridColumn DataField="Description" Width="300px"/>
                    <px:PXGridColumn DataField="ManufacturerID"  Width="200" />
                    <px:PXGridColumn DataField="OPSModelID"  Width="100" />
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
        <Mode AllowUpload="True" />
	</px:PXGrid>
</asp:Content>