<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FS203600.aspx.cs" Inherits="Page_FS203600" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="EPEquipmentRecords" 
        TypeName="PX.Objects.FS.VehicleMaint" 
        SuspendUnloading="False">
		<CallbackCommands>
            <px:PXDSCallbackCommand Name="CopyPaste" Visible="false" />
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Height="185px" 
        Style="z-index: 100" Width="100%" DataMember="EPEquipmentRecords" FilesIndicator="true"
        TabIndex="800" DefaultControlID="edEquipmentCD" AllowCollapse="True">
		<Template>
            <px:PXLayoutRule runat="server" StartRow="True" StartColumn="True" 
                LabelsWidth="SM" ColumnWidth="L">
            </px:PXLayoutRule>
            <px:PXSelector ID="edEquipmentCD" runat="server" DataField="EquipmentCD" CommitChanges="True" AutoRefresh="True">
            </px:PXSelector>
            <px:PXFormView ID="PXFormView1" runat="server" 
                DataMember="VehicleSelected" DataSourceID="ds" RenderStyle="Simple" 
                TabIndex="10500">
                <Template>
                    <px:PXLayoutRule runat="server" ColumnWidth="L" LabelsWidth="SM" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edVehicleTypeID" AllowEdit="True" runat="server" DataField="VehicleTypeID">
                    </px:PXSelector>
                    <px:PXDropDown ID="edStatus" runat="server" DataField="Status" Size="S">
                    </px:PXDropDown>
                </Template>
            </px:PXFormView>
            <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="SM" 
                LabelsWidth="S">
            </px:PXLayoutRule>
			<px:PXSelector ID="edBranchLocationID" runat="server"  AllowEdit="True"
                DataField="BranchLocationID">
            </px:PXSelector>
            <px:PXSelector ID="edFixedAssetID" runat="server" DataField="FixedAssetID" AllowEdit="True" CommitChanges="True">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" LabelsWidth="SM" StartRow="True">
            </px:PXLayoutRule>
			<px:PXFormView ID="PXFormView2" runat="server" 
                DataMember="VehicleSelected" DataSourceID="ds" RenderStyle="Simple" 
                TabIndex="10700">
                <Template>
                    <px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="SM"
                        StartColumn="True">
                    </px:PXLayoutRule>
					<px:PXTextEdit ID="edRegistrationNbr" runat="server" 
                        DataField="RegistrationNbr">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edSerialNumber" runat="server" DataField="SerialNumber">
                    </px:PXTextEdit>
                </Template>
            </px:PXFormView>
            <px:PXLayoutRule runat="server" ColumnSpan="2"></px:PXLayoutRule>
            <px:PXFormView ID="PXFormView3" runat="server" 
                DataMember="VehicleSelected" DataSourceID="ds" RenderStyle="Simple" 
                TabIndex="9200">
                <Template>
                    <px:PXLayoutRule runat="server" ControlSize="XXL" LabelsWidth="SM"
                        StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
                    </px:PXTextEdit>
                </Template>
            </px:PXFormView>
        </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="100%" DataSourceID="ds" 
        DataMember="VehicleSelected" Style="z-index: 100">
		<Items>
            <px:PXTabItem Text="General Info">
                <Template>
                    <px:PXLayoutRule runat="server" ControlSize="SM" LabelsWidth="SM" StartRow="True" 
                        StartColumn="True">
                    </px:PXLayoutRule>
					<px:PXDateTimeEdit ID="edRegisteredDate" runat="server" 
                        DataField="RegisteredDate">
                    </px:PXDateTimeEdit>
                    <px:PXTextEdit ID="edEngineNo" runat="server" DataField="EngineNo">
                    </px:PXTextEdit>
                    <px:PXNumberEdit ID="edAxles" runat="server" DataField="Axles">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edMaxMiles" runat="server" DataField="MaxMiles">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edTareWeight" runat="server" DataField="TareWeight">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edWeightCapacity" runat="server" 
                        DataField="WeightCapacity">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edGrossVehicleWeight" runat="server" 
                        DataField="GrossVehicleWeight">
                    </px:PXNumberEdit>
					<px:PXSelector ID="edColorID" runat="server" DataField="ColorID">
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" GroupCaption="Manufacturing Info"  ControlSize="SM"
                        StartGroup="True" LabelsWidth="SM" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edManufacturerID" runat="server" AllowEdit="True" 
                        DataField="ManufacturerID">
                    </px:PXSelector>
                    <px:PXSelector ID="edManufacturerModelID" runat="server" AllowEdit="True" 
                        DataField="ManufacturerModelID">
                    </px:PXSelector>
                    <px:PXTextEdit ID="edManufacturingYear" runat="server" 
                        DataField="ManufacturingYear">
                    </px:PXTextEdit>
                    <px:PXLayoutRule runat="server" GroupCaption="Fuel Info"  ControlSize="SM"
                        StartGroup="True">
                    </px:PXLayoutRule>
                    <px:PXDropDown ID="edFuelType" runat="server" DataField="FuelType" Size="SM">
                    </px:PXDropDown>
                    <px:PXNumberEdit ID="edFuelTank1" runat="server" DataField="FuelTank1" Size="SM">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edFuelTank2" runat="server" DataField="FuelTank2" Size="SM">
                    </px:PXNumberEdit>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Purchase Info">
                <Template>
                    <px:PXLayoutRule runat="server" StartRow="True" LabelsWidth="SM">
                    </px:PXLayoutRule>
					<px:PXDropDown ID="edPropertyType" runat="server" DataField="PropertyType">
                    </px:PXDropDown>
                    <px:PXSegmentMask ID="edVendorID" runat="server" DataField="VendorID">
                    </px:PXSegmentMask>
                    <px:PXDateTimeEdit ID="edPurchDate" runat="server" DataField="PurchDate">
                    </px:PXDateTimeEdit>
                    <px:PXTextEdit ID="edPurchPONumber" runat="server" DataField="PurchPONumber">
                    </px:PXTextEdit>
                    <px:PXNumberEdit ID="edPurchAmount" runat="server" DataField="PurchAmount">
                    </px:PXNumberEdit>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" />
    </px:PXTab>
</asp:Content>
