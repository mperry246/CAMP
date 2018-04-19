<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FS100400.aspx.cs" Inherits="Page_FS100400" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="RouteSetupRecord" SuspendUnloading="False" 
        TypeName="PX.Objects.FS.RouteSetupMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" ></px:PXDSCallbackCommand>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="edRouteSetup" runat="server" DataMember="RouteSetupRecord" DataSourceID="ds" Width="100%" DefaultControlID="edRouteNumberingID">
                    <Template>
                        <px:PXLayoutRule runat="server" StartRow="True" LabelsWidth="M">
                        </px:PXLayoutRule>
                        <px:PXLayoutRule runat="server" GroupCaption="Numbering Settings" >
                        </px:PXLayoutRule>   
                        <px:PXSelector ID="edRouteNumberingID" runat="server" 
                            DataField="RouteNumberingID" AllowEdit = "True" >
                        </px:PXSelector>  
                        <px:PXLayoutRule runat="server" GroupCaption="Contract Settings" >
                        </px:PXLayoutRule>  
                        <px:PXCheckBox ID="edEnableSeasonScheduleContractRoute" runat="server" AlignLeft="True" DataField="EnableSeasonScheduleContract">
                        </px:PXCheckBox>
                        <px:PXLayoutRule runat="server" GroupCaption="Route Settings">
                        </px:PXLayoutRule>   
                        <px:PXSelector ID="edDfltSrvOrdType" runat="server" 
                            DataField="DfltSrvOrdType" >                        
                        </px:PXSelector>                      
                        <px:PXCheckBox ID="edAutoCalculateRouteStats" runat="server" DataField="AutoCalculateRouteStats" AlignLeft="True">
                        </px:PXCheckBox>
                        <px:PXCheckBox ID="edGroupINDocumentsByPostingProcess" runat="server" DataField="GroupINDocumentsByPostingProcess" Text="Group IN documents by Posting process" AlignLeft="True" CommitChanges="True">
                        </px:PXCheckBox> 
                        <px:PXCheckBox ID="edSetFirstManualAppointment" runat="server" DataField="SetFirstManualAppointment" AlignLeft="True">
                        </px:PXCheckBox> 
                        <px:PXCheckBox ID="edTrackRouteLocation" runat="server" DataField="TrackRouteLocation" AlignLeft="True">
                        </px:PXCheckBox>   
                    </Template>
	  <AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXFormView>
</asp:Content>
