<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XMQ43000.aspx.cs" Inherits="Page_XMQ43000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        TypeName="MaxQ.Products.Billing.GenerateBillActInvc" PrimaryView="Filter">
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
        DataMember="Filter" TabIndex="100">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXSelector runat="server" DataField="CustomerID" DataSourceID="ds" 
                ID="CustomerID" CommitChanges="True">
            </px:PXSelector>
            <px:PXDateTimeEdit ID="edProcessDate" runat="server" DataField="ProcessDate" 
                CommitChanges="True">
            </px:PXDateTimeEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        Height="150px" SkinID="Details" TabIndex="300">
        <Levels>
            <px:PXGridLevel DataKeyNames="BAccountID" DataMember="Customers">
                <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXDateTimeEdit ID="edLastBillActInvGenDate" runat="server" 
                        DataField="LastBillActInvGenDate">
                    </px:PXDateTimeEdit>
                    <px:PXDateTimeEdit ID="edNextBillActInvGenDate" runat="server" 
                        DataField="NextBillActInvGenDate">
                    </px:PXDateTimeEdit>
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected" 
                        Text="Selected">
                    </px:PXCheckBox>
                    <px:PXSegmentMask ID="edCustomer__AcctCD" runat="server" 
                        DataField="Customer__AcctCD">
                    </px:PXSegmentMask>
                    <px:PXTextEdit ID="edCustomer__AcctName" runat="server" 
                        DataField="Customer__AcctName">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" 
                        Width="70px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="AcctCD" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="AcctName" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="LastBillActInvGenDate" Width="190px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="NextBillActInvGenDate" Width="190px">
                    </px:PXGridColumn>
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
        <Mode AllowFormEdit="True" />
    </px:PXGrid>
</asp:Content>
