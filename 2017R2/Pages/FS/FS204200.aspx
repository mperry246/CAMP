<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FS204200.aspx.cs" Inherits="Page_FS204200" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="VehicleTypeRecords" SuspendUnloading="False" 
        TypeName="PX.Objects.FS.VehicleTypeMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Height="76px" 
        Style="z-index: 100" Width="100%" DataMember="VehicleTypeRecords" 
        TabIndex="500" DefaultControlID="edVehicleTypeCD" FilesIndicator="true">
		<Template>
            <px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="M" StartRow="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edVehicleTypeCD" runat="server"  AutoRefresh="True"
                DataField="VehicleTypeCD">
            </px:PXSelector>
            <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
            </px:PXTextEdit>
        </Template>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXFormView>
</asp:Content>

