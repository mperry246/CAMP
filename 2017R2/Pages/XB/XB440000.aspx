<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB440000.aspx.cs" Inherits="Page_XB440000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter"
        TypeName="MaxQ.Products.RBRR.ContractRenewal">
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
        DataMember="Filter" TabIndex="22500">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXCheckBox ID="edAutoRenewOnly" runat="server" 
                DataField="AutoRenewOnly" Text="Auto Renewing Contracts Only" 
                CommitChanges="True">
            </px:PXCheckBox>
            <px:PXSelector runat="server" DataField="CustomerID" ID="edCustomerID" 
                CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector runat="server" DataField="ARCustomerID" ID="edARCustomerID" 
                CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector runat="server" DataField="BillProfCustomerID" 
                ID="edBillProfCustomerID" CommitChanges="True">
            </px:PXSelector>
            <px:PXDropDown runat="server" DataField="OrdType" ID="edOrdType" 
                CommitChanges="True">
            </px:PXDropDown>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXTextEdit runat="server" DataField="Descr" ID="edDescr" 
                CommitChanges="True">
            </px:PXTextEdit>
            <px:PXSelector runat="server" DataField="ClassID" ID="edClassID" 
                CommitChanges="True">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" Merge="True">
            </px:PXLayoutRule>
            <px:PXDateTimeEdit runat="server" DataField="EndDateFrom" ID="edEndDateFrom" 
                CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit runat="server" DataField="EndDateTo" ID="edEndDateTo" 
                CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXLayoutRule runat="server" Merge="True">
            </px:PXLayoutRule>
            <px:PXDateTimeEdit runat="server" DataField="RenewDateFrom" 
                ID="edRenewDateFrom" CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit runat="server" DataField="RenewDateTo" ID="edRenewDateTo" 
                CommitChanges="True">
            </px:PXDateTimeEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        Height="150px" SkinID="Details" TabIndex="800" SyncPosition="True">
        <Levels>
            <px:PXGridLevel DataKeyNames="ContractCD,RevisionNbr" DataMember="Contracts">
                <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected" Text="Selected">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edContractCD" runat="server" DataField="ContractCD">
                    </px:PXSelector>
                    <px:PXSelector ID="edRevisionNbr" runat="server" DataField="RevisionNbr">
                    </px:PXSelector>
                    <px:PXDropDown ID="edStatus" runat="server" DataField="Status">
                    </px:PXDropDown>
                    <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID">
                    </px:PXSelector>
                    <px:PXSelector ID="edARCustomerID" runat="server" DataField="ARCustomerID">
                    </px:PXSelector>
                    <px:PXSelector ID="edBillProfCustomerID" runat="server" DataField="BillProfCustomerID">
                    </px:PXSelector>
                    <px:PXNumberEdit ID="edTotal" runat="server" DataField="Total">
                    </px:PXNumberEdit>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
                    </px:PXTextEdit>
                    <px:PXDateTimeEdit ID="edBegDate" runat="server" DataField="BegDate">
                    </px:PXDateTimeEdit>
                    <px:PXDateTimeEdit ID="edEndDate" runat="server" DataField="EndDate">
                    </px:PXDateTimeEdit>
                    <px:PXDateTimeEdit ID="edRenewalDate" runat="server" DataField="RenewalDate">
                    </px:PXDateTimeEdit>
                    <px:PXDropDown ID="edOrdType" runat="server" DataField="OrdType">
                    </px:PXDropDown>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" Width="70px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ContractCD" Width="115px" LinkCommand="ViewContract">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="RevisionNbr" Width="115px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CustomerID" Width="300px" DisplayMode="Hint">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ARCustomerID" DisplayMode="Hint" Width="300px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="BillProfCustomerID" DisplayMode="Hint" 
                        Width="300px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Total" Width="100px" TextAlign="Right">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="BegDate" Width="115px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="EndDate" Width="100px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="RenewalDate" Width="100px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OrdType" Width="175px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXGrid>
</asp:Content>
