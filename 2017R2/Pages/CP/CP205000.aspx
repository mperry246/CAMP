<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CP205000.aspx.cs" Inherits="Page_CP205000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="LineofBusinessRecords" TypeName="CAMPCustomization.CAMPLineofBusinessMaint" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
        FastFilterFields="LineofBusinessCD,Description" AllowPaging="True" AllowSearch="True" 
        AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="100">
		<Levels>
			<px:PXGridLevel DataKeyNames="LineofBusinessCD" DataMember="LineofBusinessRecords">
			    <RowTemplate>
                    <px:PXSelector ID="edBranchID" runat="server" DataField="BranchID" />
                    <px:PXMaskEdit ID="edLineofBusinessCD" runat="server" DataField="LineofBusinessCD" />
                    <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" />
                    <px:PXSelector ID="edPriceClass" runat="server" DataField="PriceClass" />
                ]   <px:PXSelector ID="edDefaultSubaccount" runat="server" DataField="DefaultSubaccount" />
                    <px:PXTextEdit ID="edLogo" runat="server" DataField="Logo">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edPhone1" runat="server" DataField="Phone1">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edPhone2" runat="server" DataField="Phone2">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edFax" runat="server" DataField="Fax">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edWeb" runat="server" DataField="Web">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edEmail" runat="server" DataField="Email">
                    </px:PXTextEdit>
                    <px:PXCheckBox ID="edEngine" runat="server" DataField="Engine" Text="Engine">
                    </px:PXCheckBox>
                    <px:PXTextEdit ID="edWireBank" runat="server" DataField="WireBank">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edWireAddress" runat="server" DataField="WireAddress">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edWireABA" runat="server" DataField="WireABA">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edWireAccount" runat="server" DataField="WireAccount">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edWireName" runat="server" DataField="WireName">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edWireSwiftCode" runat="server" DataField="WireSwiftCode">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edWireIBAN" runat="server" DataField="WireIBAN">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edWireBeneficiary" runat="server" DataField="WireBeneficiary">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edLBWireName" runat="server" DataField="LBWireName">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edLBWireAddressLine" runat="server" DataField="LBWireAddressLine">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edLBWireCityStateZipcode" runat="server" DataField="LBWireCityStateZipcode">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edOvernightLBWireName" runat="server" DataField="OvernightLBWireName">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edOvernightLBWireAddressLine" runat="server" DataField="OvernightLBWireAddressLine">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edOvernightLBWireCityStateZipcode" runat="server" DataField="OvernightLBWireCityStateZipcode">
                    </px:PXTextEdit>
                 </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="BranchID" Width="75"/>
                    <px:PXGridColumn DataField="LineofBusinessCD" Width="150"/>
                    <px:PXGridColumn DataField="Description" Width="300px"/>
                    <px:PXGridColumn DataField="PriceClass" DisplayMode="Text" Width="200" />
                    <px:PXGridColumn DataField="DefaultSubaccount" DisplayMode="Text" Width="200" />
                    <px:PXGridColumn DataField="Logo" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Phone1" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Phone2" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Fax" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Web" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Email" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Engine" TextAlign="Center" Type="CheckBox" Width="60px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="WireBank" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="WireAddress" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="WireABA" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="WireAccount" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="WireName" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="WireSwiftCode" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="WireIBAN" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="WireBeneficiary" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="LBWireName" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="LBWireAddressLine" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="LBWireCityStateZipcode" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OvernightLBWireName" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OvernightLBWireAddressLine" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OvernightLBWireCityStateZipcode" Width="200px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
        <Mode AllowUpload="True" />
	</px:PXGrid>
</asp:Content>