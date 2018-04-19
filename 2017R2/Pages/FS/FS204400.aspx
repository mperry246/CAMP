<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FS204400.aspx.cs" Inherits="Page_FS204400" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        BorderStyle="NotSet" PrimaryView="ManufacturerRecords" SuspendUnloading="False" 
        TypeName="PX.Objects.FS.ManufacturerMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="ViewMainOnMap" Visible="false" ></px:PXDSCallbackCommand>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Height="200px" 
        Style="z-index: 100" Width="100%" DataMember="ManufacturerRecords" TabIndex="500" DefaultControlID="edManufacturerCD">
		<Template>
            <px:PXLayoutRule runat="server" StartRow="True" 
                StartColumn="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edManufacturerCD" runat="server" DataField="ManufacturerCD">
            </px:PXSelector>
            <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
            </px:PXTextEdit>
            <px:PXLayoutRule runat="server" GroupCaption="Main Contact" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edContactID" runat="server" AllowEdit="True" 
                CommitChanges="True" DataField="ContactID" DisplayMode="Text">
            </px:PXSelector>
            <px:PXTextEdit ID="edSalutation" runat="server" DataField="Salutation">
            </px:PXTextEdit>
            <px:PXMailEdit ID="edEMail" runat="server" DataField="EMail">
            </px:PXMailEdit>
            <px:PXLinkEdit ID="edWebSite" runat="server" DataField="WebSite">
            </px:PXLinkEdit>
            <px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1">
            </px:PXMaskEdit>
            <px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2">
            </px:PXMaskEdit>
            <px:PXMaskEdit ID="edFax" runat="server" DataField="Fax">
            </px:PXMaskEdit>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" GroupCaption="Main Address" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXCheckBox ID="edIsValidated" runat="server" DataField="IsValidated" 
                Text="Is Validated">
            </px:PXCheckBox>
            <px:PXTextEdit ID="edAddressLine1" runat="server" CommitChanges="True" 
                DataField="AddressLine1">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edAddressLine2" runat="server" CommitChanges="True" 
                DataField="AddressLine2">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edCity" runat="server" CommitChanges="True" DataField="City">
            </px:PXTextEdit>
            <px:PXSelector ID="edCountryID" runat="server" AllowEdit="True" 
                AutoRefresh="True" CommitChanges="True" DataField="CountryID" 
                DataSourceID="ds">
            </px:PXSelector>
            <px:PXSelector ID="edState" runat="server" AllowEdit="True" AutoRefresh="True" 
                CommitChanges="True" DataField="State" DataSourceID="ds">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" Merge="True">
            </px:PXLayoutRule>
            <px:PXMaskEdit ID="edPostalCode" runat="server" CommitChanges="True" 
                DataField="PostalCode" Size="S">
            </px:PXMaskEdit>
            <px:PXButton ID="btnViewMainOnMap" runat="server" CommandName="ViewMainOnMap" 
                CommandSourceID="ds" Height="21px" Text="View on Map" Width="94px">
            </px:PXButton>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
        </Template>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXFormView>
</asp:Content>
