<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB201000.aspx.cs" Inherits="Page_XB201000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" PrimaryView="ReasonCodes" Visible="True"
        TypeName="MaxQ.Products.RBRR.NonRenewalReasonMaint" SuspendUnloading="False">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Primary" TabIndex="100" TemporaryFilterCaption="Filter Applied">
		<Levels>
			<px:PXGridLevel DataKeyNames="Code" DataMember="ReasonCodes">
			    <RowTemplate>
                    <px:PXLayoutRule runat="server" ControlSize="SM">
                    </px:PXLayoutRule>
                    <px:PXMaskEdit ID="edCode" runat="server" DataField="Code" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXMaskEdit>
                    <px:PXLayoutRule runat="server" ControlSize="L">
                    </px:PXLayoutRule>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXSelector ID="edActivityID" runat="server" DataField="ActivityID">
                    </px:PXSelector>
                    <px:PXSelector ID="edResolution" runat="server" DataField="Resolution">
                    </px:PXSelector>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Code" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ActivityID" DisplayMode="Text" Width="300px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Resolution" DisplayMode="Text" Width="160px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
		<ActionBar ActionsText="False">
		</ActionBar>
	    <Mode AllowFormEdit="True" />
	</px:PXGrid>
</asp:Content>
