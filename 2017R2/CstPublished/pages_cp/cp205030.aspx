<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="CP205030.aspx.cs" Inherits="Page_CP205030" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="LineofBusinessRecords" 
        TypeName="CAMPCustomization.CAMPLineofBusinessMaint">
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView SkinID="" ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" 
        DataMember="LineofBusinessRecords" TabIndex="600" FilesIndicator="True">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True"></px:PXLayoutRule>
            <px:PXSelector ID="edLineofBusinessCD" DisplayMode="Value" runat="server" DataField="LineofBusinessCD" CommitChanges="true" AutoRefresh="true" FilterByAllFields="True"></px:PXSelector>
            <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" ></px:PXTextEdit></Template>
	</px:PXFormView>
	
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXTab ID="tab" runat="server"  Height="522px" Width="100%" DataMember="CurrentLOB" DataSourceID="ds">
		<Items>
			<px:PXTabItem Text="Details">
                <Template>
                    <px:PXLayoutRule runat="server" GroupCaption="Contact Information" StartGroup="True" />
                    <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
                    <px:PXMailEdit ID="edEMail" runat="server" DataField="EMail" CommitChanges="True"/>
                    <px:PXLinkEdit ID="edWeb" runat="server" DataField="Web" CommitChanges="True"/>
                    <px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1" />
                    <px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2" />
                    <px:PXMaskEdit ID="edFax" runat="server" DataField="Fax" />
                    <px:PXCheckBox ID="edEngine" runat="server" DataField="Engine" />
                    <px:PXSelector ID="edDefaultSubaccount" runat="server" DataField="DefaultSubaccount" />
                    <px:PXSelector ID="edPriceClass" runat="server" DataField="PriceClass" />
            
                    <px:PXLayoutRule runat="server" GroupCaption=" Overnight LB/Wire Information" StartGroup="True" />
                    <px:PXTextEdit ID="edOvernightLBWireName" runat="server" DataField="OvernightLBWireName" />
                    <px:PXTextEdit ID="edOvernightLBWireAddressLine" runat="server" DataField="OvernightLBWireAddressLine" />
                    <px:PXTextEdit ID="edOvernightLBWireCityStateZipcode" runat="server" DataField="OvernightLBWireCityStateZipcode" />

                    <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
                    <px:PXLayoutRule runat="server" GroupCaption="Wire Information" StartGroup="True" />
                    <px:PXTextEdit ID="edWireBank" runat="server" DataField="WireBank" />
                    <px:PXTextEdit ID="edWireAddress" runat="server" DataField="WireAddress" />
                    <px:PXTextEdit ID="edWireABA" runat="server" DataField="WireABA" />
                    <px:PXTextEdit ID="edWireAccount" runat="server" DataField="WireAccount" />
                    <px:PXTextEdit ID="edWireName" runat="server" DataField="WireName" />
                    <px:PXTextEdit ID="edWireSwiftCode" runat="server" DataField="WireSwiftCode" />
                    <px:PXTextEdit ID="edWireIBAN" runat="server" DataField="WireIBAN" />
                    <px:PXTextEdit ID="edWireBeneficiary" runat="server" DataField="WireBeneficiary" />
                    <px:PXLayoutRule runat="server" GroupCaption="LB/Wire Information" StartGroup="True" />
                    <px:PXTextEdit ID="edLBWireName" runat="server" DataField="LBWireName" />
                    <px:PXTextEdit ID="edLBWireAddressLine" runat="server" DataField="LBWireAddressLine" />
                    <px:PXTextEdit ID="edLBWireCityStateZipcode" runat="server" DataField="LBWireCityStateZipcode" />
            </Template>
			</px:PXTabItem>
			
		</Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXTab>
</asp:Content>