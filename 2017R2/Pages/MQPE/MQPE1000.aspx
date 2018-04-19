<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="MQPE1000.aspx.cs" Inherits="Page_MQPE1000"
    Title="Untitled Page" Debug="true" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
		TypeName="MaxQ.Products.ProcessEngine.MQPESetupMaint" PrimaryView="Setup">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="true" />
		</CallbackCommands>		
	</px:PXDataSource>
</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
        <px:PXFormView ID="fvSetup" runat="server" DataSourceID="ds" DataMember="Setup" Width="100%" >
            <Template>
                <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="L" ControlSize="M"  />
                  <px:PXLayoutRule runat="server" Merge="True" StartGroup="True" GroupCaption="Numbering Settings"  />
                    <px:PXSelector ID="edTransactionNumberingID" runat="server" 
                        DataField="TransactionNumberingID"
                        AllowEdit="True"  
                        CommitChanges="True">
                    </px:PXSelector>
					  <px:PXLayoutRule runat="server" EndGroup="True" />
			<px:PXNumberEdit ID="edTraceLogLength" runat="server" DataField="TraceLogLength" />
		          		<px:PXTextEdit ID="edVersionInfo" runat="server" DataField="VersionInfo" Width="300" />
          </Template>
        </px:PXFormView>
</asp:Content>
