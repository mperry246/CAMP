<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FS100300.aspx.cs" Inherits="Page_FS100300" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="SetupRecord" SuspendUnloading="False" 
        TypeName="PX.Objects.FS.EquipmentSetupMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" ></px:PXDSCallbackCommand>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="edEquipmentSetup" runat="server" DataMember="SetupRecord" DataSourceID="ds" Width="100%" DefaultControlID="edEquipmentNumberingID" Style="z-index: 100">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True" LabelsWidth="M">
			</px:PXLayoutRule>
			<px:PXLayoutRule runat="server" GroupCaption="Numbering Settings">
			</px:PXLayoutRule>   
			<px:PXSelector ID="edEquipmentNumberingID" runat="server" AllowEdit = "True"
				DataField="EquipmentNumberingID">
			</px:PXSelector>
			<px:PXLayoutRule runat="server" GroupCaption="Contract Settings">
			</px:PXLayoutRule>    
			<px:PXCheckBox ID="edEnableSeasonScheduleContract" runat="server" AlignLeft="True" DataField="EnableSeasonScheduleContract">
			</px:PXCheckBox>
			<px:PXLayoutRule runat="server" GroupCaption="Equipment Settings">
			</px:PXLayoutRule>  
			<px:PXGroupBox ID="edEquipmentCalculateWarrantyFrom" runat="server" 
			Caption="Calculate Warranty From" CommitChanges="True" 
			DataField="EquipmentCalculateWarrantyFrom" 
			Width="100%">
				<Template>
					<px:PXRadioButton ID="edEquipmentCalculateWarrantyFrom_op0" runat="server" 
						GroupName="edEquipmentCalculateWarrantyFrom" Text="Sales Order Date" Value="SD" />
					<px:PXRadioButton ID="edEquipmentCalculateWarrantyFrom_op1" runat="server" 
						GroupName="edEquipmentCalculateWarrantyFrom" Text="Installation Date" Value="AD" />
					<px:PXRadioButton ID="edEquipmentCalculateWarrantyFrom_op2" runat="server" 
						GroupName="edEquipmentCalculateWarrantyFrom" Text="The Earliest of Both Dates" Value="ED" />
					<px:PXRadioButton ID="edEquipmentCalculateWarrantyFrom_op3" runat="server" 
						GroupName="edEquipmentCalculateWarrantyFrom" Text="The Latest of Both Dates" Value="LD" />
					<px:PXLayoutRule runat="server" EndGroup="True">
					</px:PXLayoutRule>
				</Template>
				<ContentLayout layout="Stack" />
			</px:PXGroupBox>
		</Template>
	  <AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXFormView>
</asp:Content>
