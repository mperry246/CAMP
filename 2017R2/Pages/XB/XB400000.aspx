<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB400000.aspx.cs" Inherits="Page_XB400000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter"
        TypeName="MaxQ.Products.RBRR.GenerateRecurringOrders" BorderStyle="NotSet" SuspendUnloading="False">
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        Height="123px" DataMember="Filter" Caption="Selection Criteria" TabIndex="1700">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
            </px:PXLayoutRule>
            <px:PXSelector runat="server" DataField="ContractID" DataSourceID="ds" ID="ContractID"
                CommitChanges="True" DisplayMode="Text">
            </px:PXSelector>
            <px:PXSelector runat="server" DataField="ClassID" DataSourceID="ds" ID="ClassID"
                CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector runat="server" DataField="CustomerID" DataSourceID="ds" ID="CustomerID"
                CommitChanges="True">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="SM">
            </px:PXLayoutRule>
            <px:PXDateTimeEdit runat="server" DataField="BegDate" ID="BegDate" CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit runat="server" DataField="EndDate" ID="EndDate" CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXDropDown runat="server" DataField="OrdType" ID="OrdType" CommitChanges="True">
            </px:PXDropDown>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="XS" ControlSize="M">
            </px:PXLayoutRule>
            <px:PXSelector runat="server" DataField="TermsID" DataSourceID="ds" ID="TermsID"
                CommitChanges="True">
            </px:PXSelector>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        Height="150px" SkinID="Details" TabIndex="2700">
        <Levels>
            <px:PXGridLevel DataMember="FilteredProcessing">
                <Layout FormViewHeight=""></Layout>

                <RowTemplate>
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected" 
                        Text="Selected">
                    </px:PXCheckBox>
                    <px:PXMaskEdit ID="edContractCD" runat="server" DataField="ContractCD">
                    </px:PXMaskEdit>
                    <px:PXMaskEdit ID="edRevisionNbr" runat="server" DataField="RevisionNbr">
                    </px:PXMaskEdit>
                    <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID">
                    </px:PXSelector>
                    <px:PXDateTimeEdit ID="edGenDate" runat="server" DataField="GenDate">
                    </px:PXDateTimeEdit>
                    <px:PXSelector ID="edPerPost" runat="server" DataField="PerPost">
                    </px:PXSelector>
                    <px:PXNumberEdit ID="edAmount" runat="server" DataField="Amount">
                    </px:PXNumberEdit>
                    <px:PXTextEdit ID="edComments" runat="server" DataField="Comments">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" 
                        Width="70px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ContractCD" Width="115px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="RevisionNbr" Width="115px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CustomerID" Width="300px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="GenDate" Width="115px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="PerPost" Width="115px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Amount" TextAlign="Right" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Comments" Width="200px">
                    </px:PXGridColumn>
                </Columns>

            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXGrid>
</asp:Content>
