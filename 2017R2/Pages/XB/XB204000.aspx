<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB204000.aspx.cs" Inherits="Page_XB204000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" Width="100%" runat="server" Visible="True"
        PrimaryView="CreationCodes" 
        TypeName="MaxQ.Products.RBRR.ContractCreationCodeMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Primary" TabIndex="1300" TemporaryFilterCaption="Filter Applied">
		<Levels>
			<px:PXGridLevel DataKeyNames="Code" DataMember="CreationCodes">
			    <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXMaskEdit ID="edCode" runat="server" DataField="Code" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXSelector ID="edDfltClassID" runat="server" DataField="DfltClassID">
                    </px:PXSelector>
                    <px:PXNumberEdit ID="edDfltContrLen" runat="server" DataField="DfltContrLen" AlreadyLocalized="False" DefaultLocale="">
                    </px:PXNumberEdit>
                    <px:PXDropDown ID="edDfltContrInt" runat="server" DataField="DfltContrInt">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edInvoiceUsage" runat="server" DataField="InvoiceUsage">
                    </px:PXDropDown>
                    <px:PXCheckBox ID="edNotRenewable" runat="server" DataField="NotRenewable" 
                        Text="NotRenewable" AlreadyLocalized="False">
                    </px:PXCheckBox>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Code" Width="80px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DfltClassID" Width="170px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DfltContrLen" TextAlign="Right" Width="110px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DfltContrInt" Width="100px" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="InvoiceUsage" 
                        Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="NotRenewable" TextAlign="Center" Type="CheckBox" Width="125px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	    <Mode AllowFormEdit="True" />
	</px:PXGrid>
</asp:Content>
