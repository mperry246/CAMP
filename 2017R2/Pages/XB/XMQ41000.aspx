<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XMQ41000.aspx.cs" Inherits="Page_XMQ41000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter"
        TypeName="MaxQ.Products.Billing.ScheduledActivityCreation">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="Filter" TabIndex="8500">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="M"
                LabelsWidth="SM">
            </px:PXLayoutRule>
            <px:PXSelector runat="server" DataField="CustomerID" DataSourceID="ds"
                ID="CustomerID" CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector ID="edSvcParmID" runat="server" DataField="SvcParmID"
                DataSourceID="ds" CommitChanges="True">
            </px:PXSelector>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        Height="150px" SkinID="Details" TabIndex="8900">
        <Levels>
            <px:PXGridLevel DataKeyNames="AcctCD" DataMember="Customers">
                <RowTemplate>
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected"
                        Text="Selected">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edBAccountID" runat="server" DataField="BAccountID">
                    </px:PXSelector>
                    <px:PXSelector ID="edSvcParmCD" runat="server" DataField="SvcParmCD">
                    </px:PXSelector>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="BAccountID" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="SvcParmCD" Width="150px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                    </px:PXGridColumn>
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXGrid>
</asp:Content>
