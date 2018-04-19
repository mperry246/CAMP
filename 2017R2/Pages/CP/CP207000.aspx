<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CP207000.aspx.cs" Inherits="Page_CP207000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="ContractTypeRecords" TypeName="CAMPCustomization.CAMPContractTypeMaint" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
        FastFilterFields="ContractTypeCD,Description" AllowPaging="True" AllowSearch="True" 
        AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="100">
		<Levels>
			<px:PXGridLevel DataKeyNames="ContractTypeCD" DataMember="ContractTypeRecords">
			    <RowTemplate>
                    <px:PXMaskEdit ID="edContractTypeCD" runat="server" DataField="ContractTypeCD">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescription" runat="server" DataField="Description">
                    </px:PXTextEdit>
                    <px:PXCheckBox ID="edUpdateContractType" runat="server" DataField="UpdateContractType" />
                    <px:PXSelector ID="edNextContractTypeID" runat="server" DataSourceID="ds" DataField="NextContractTypeID" CommitChanges="true" AutoRefresh="true" DisplayMode="Text" />
            
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="ContractTypeCD" Width="150"/>
                    <px:PXGridColumn DataField="Description" Width="300px"/>
                    <px:PXGridColumn DataField="UpdateContractType" Width="75px" Type="CheckBox" TextAlign="Center"/>
                    <px:PXGridColumn DataField="NextContractTypeID" Width="250px" DisplayMode="Text"/>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
        <Mode AllowUpload="True" />
	</px:PXGrid>
</asp:Content>