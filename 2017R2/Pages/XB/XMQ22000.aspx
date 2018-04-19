<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XMQ22000.aspx.cs" Inherits="Page_XMQ22000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Activities"
        TypeName="MaxQ.Products.Billing.BillActivityEntry">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" PopupVisible = "true" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Height="200px" Style="z-index: 100"
        Width="100%" DataMember="Activities" TabIndex="500">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" GroupCaption="Activity Details" LabelsWidth="M" 
                StartGroup="True" ControlSize="L">
            </px:PXLayoutRule>
            <px:PXSelector ID="BillActivityCD" runat="server" DataField="BillActivityCD" 
                DataSourceID="ds">
            </px:PXSelector>
            <px:PXSelector ID="CustomerID" runat="server" DataField="CustomerID" DataSourceID="ds">
            </px:PXSelector>
            <px:PXSelector ID="edSvcParmCustID" runat="server" DataField="SvcParmCustID" 
                DataSourceID="ds">
            </px:PXSelector>
            <px:PXSelector ID="edSvcParmID" runat="server" DataField="SvcParmID" 
                DataSourceID="ds">
            </px:PXSelector>
            <px:PXSelector ID="ActivityID" runat="server" DataField="ActivityID" DataSourceID="ds">
            </px:PXSelector>
            <px:PXDateTimeEdit ID="ActivityDate" runat="server" DataField="ActivityDate">
            </px:PXDateTimeEdit>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" GroupCaption="Related Documents" 
                LabelsWidth="M" StartGroup="True" ControlSize="M">
            </px:PXLayoutRule>
            <px:PXDropDown CommitChanges="True" ID="edEntityType" runat="server"
                DataField="EntityType" SelectedIndex="-1">
            </px:PXDropDown>
            <px:PXTextEdit ID="EntityId" runat="server" DataField="EntityId">
            </px:PXTextEdit>
            <px:PXSelector ID="ContractID" runat="server" DataField="ContractID" 
                DataSourceID="ds" DisplayMode="Text">
            </px:PXSelector>
            <px:PXNumberEdit ID="OrigTranQty" runat="server" DataField="OrigTranQty">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="OrigTranAmt" runat="server" DataField="OrigTranAmt" 
                CommitChanges="True">
            </px:PXNumberEdit>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" GroupCaption="Fee" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXNumberEdit ID="FlatFee" runat="server" DataField="FlatFee" 
                CommitChanges="True">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="FlatQty" runat="server" DataField="FlatQty" 
                CommitChanges="True">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="FlatExt" runat="server" DataField="FlatExt">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="Pct" runat="server" DataField="Pct" CommitChanges="True">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="PctExt" runat="server" DataField="PctExt" 
                CommitChanges="True">
            </px:PXNumberEdit>
            <px:PXLayoutRule runat="server" GroupCaption="Total" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXNumberEdit ID="TotalFee" runat="server" DataField="TotalFee">
            </px:PXNumberEdit>
            <px:PXLayoutRule runat="server" GroupCaption="Status" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXCheckBox ID="Rated" runat="server" DataField="Rated" Text="Rated">
            </px:PXCheckBox>
            <px:PXCheckBox ID="Processed" runat="server" DataField="Processed" Text="Processed">
            </px:PXCheckBox>
            <px:PXTextEdit ID="ARRefNbr" runat="server" DataField="ARRefNbr">
            </px:PXTextEdit>
        </Template>
        <AutoSize Container="Window" Enabled="True" MinHeight="200" />
    </px:PXFormView>
</asp:Content>
