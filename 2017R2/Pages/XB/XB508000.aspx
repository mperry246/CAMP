<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB508000.aspx.cs" Inherits="Page_XB508000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        PrimaryView="Filter" TypeName="MaxQ.Products.RBRR.MQProcessContract">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100"
        Width="100%" DataMember="Filter" TabIndex="900">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M">
            </px:PXLayoutRule>
            <px:PXDropDown runat="server" DataField="ContractAction" ID="edContractAction"
                Width="350px" CommitChanges="True">
            </px:PXDropDown>
            <px:PXNumberEdit ID="edRenewNoticeDays" runat="server" AlreadyLocalized="False" DataField="RenewNoticeDays" DefaultLocale="" CommitChanges="True">
            </px:PXNumberEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100"
        Width="100%" Height="150px" SkinID="Details" TabIndex="1100" TemporaryFilterCaption="Filter Applied">
        <Levels>
            <px:PXGridLevel DataKeyNames="ContractCD,RevisionNbr"
                DataMember="FilteredContracts">
                <RowTemplate>
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected"
                        Text="Selected" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edContractCD" runat="server" DataField="ContractCD">
                    </px:PXSelector>
                    <px:PXSelector ID="edRevisionNbr" runat="server" DataField="RevisionNbr">
                    </px:PXSelector>
                    <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID">
                    </px:PXSelector>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" AlreadyLocalized="False">
                    </px:PXTextEdit>
                    <px:PXSelector ID="edARCustomerID" runat="server" DataField="ARCustomerID">
                    </px:PXSelector>
                    <px:PXDateTimeEdit ID="edContractDate" runat="server" DataField="ContractDate" AlreadyLocalized="False">
                    </px:PXDateTimeEdit>
                    <px:PXDateTimeEdit ID="edRenewalDate" runat="server" AlreadyLocalized="False" DataField="RenewalDate" DefaultLocale="">
                    </px:PXDateTimeEdit>
                    <px:PXDropDown ID="edStatus" runat="server" DataField="Status">
                    </px:PXDropDown>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ContractCD" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="RevisionNbr" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CustomerID" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ARCustomerID"
                        Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ContractDate" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="RenewalDate" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Status">
                    </px:PXGridColumn>
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXGrid>
</asp:Content>
