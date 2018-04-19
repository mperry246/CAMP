<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB430000.aspx.cs" Inherits="Page_XB430000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        PrimaryView="Filter" TypeName="MaxQ.Products.RBRR.MarketingListBuilder">
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
        Width="100%" DataMember="Filter" TabIndex="15700" >
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM">
            </px:PXLayoutRule>


            <px:PXSelector ID="edCustomerID" runat="server" CommitChanges="True"
                DataField="CustomerID" Size="XM">
            </px:PXSelector>
            <px:PXSelector ID="edARCustomerID" runat="server" CommitChanges="True"
                DataField="ARCustomerID">
            </px:PXSelector>

            <px:PXSelector ID="edClassID" runat="server" CommitChanges="True"
                DataField="ClassID" Size="XM">
            </px:PXSelector>
            <px:PXSelector ID="edCustomerClassID" runat="server" CommitChanges="True"
                DataField="CustomerClassID" Size="XM">
            </px:PXSelector>
            <px:PXDropDown ID="edStatus" runat="server" CommitChanges="True"
                DataField="Status" Size="SM">
            </px:PXDropDown>



            <px:PXLayoutRule runat="server" LabelsWidth="SM" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" LabelsWidth="SM" Merge="True">
            </px:PXLayoutRule>
            <px:PXDateTimeEdit ID="edEndDateFrom" runat="server" CommitChanges="True"
                DataField="EndDateFrom">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit ID="edEndDateTo" runat="server" CommitChanges="True"
                DataField="EndDateTo" LabelWidth="35px">
            </px:PXDateTimeEdit>
            <px:PXLayoutRule runat="server" LabelsWidth="SM" Merge="True">
            </px:PXLayoutRule>
            <px:PXDateTimeEdit ID="edStartDateFrom" runat="server"
                DataField="StartDateFrom" CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit ID="edStartDateTo" runat="server" CommitChanges="True"
                DataField="StartDateTo" LabelWidth="35px">
            </px:PXDateTimeEdit>

            <px:PXLayoutRule runat="server" LabelsWidth="SM" Merge="False">
            </px:PXLayoutRule>

            <px:PXSelector ID="edIncInventoryID" runat="server" CommitChanges="True"
                DataField="IncInventoryID" Size="XM">
            </px:PXSelector>
            <px:PXSelector ID="edExcInventoryID" runat="server" CommitChanges="True"
                DataField="ExcInventoryID" Size="XM">
            </px:PXSelector>


            <px:PXSelector ID="edMarketingListID" runat="server" CommitChanges="True"
                DataField="MarketingListID" Size="XM">
            </px:PXSelector>
            <px:PXDropDown ID="edMarketingListAction" runat="server" CommitChanges="True"
                DataField="MarketingListAction" Size="SM">
            </px:PXDropDown>

        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100"
        Width="100%" Height="150px" SkinID="Details" TabIndex="15900"
        SyncPosition="True">
        <Levels>
            <px:PXGridLevel DataKeyNames="ContractCD,RevisionNbr" DataMember="Contracts">
                <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected"
                        Text="Selected">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edContractCD" runat="server" DataField="ContractCD">
                    </px:PXSelector>
                    <px:PXSelector ID="edRevisionNbr" runat="server" DataField="RevisionNbr">
                    </px:PXSelector>
                    <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID">
                    </px:PXSelector>
                    <px:PXSelector ID="edARCustomerID" runat="server" DataField="ARCustomerID">
                    </px:PXSelector>
                    <px:PXDropDown ID="edStatus" runat="server" DataField="Status">
                    </px:PXDropDown>
                    <px:PXDateTimeEdit ID="edBegDate" runat="server" DataField="BegDate">
                    </px:PXDateTimeEdit>
                    <px:PXDateTimeEdit ID="edEndDate" runat="server" DataField="EndDate">
                    </px:PXDateTimeEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox"
                        Width="80px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ContractCD" Width="115px"
                        LinkCommand="ViewContract">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="RevisionNbr" Width="115px">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CustomerID" DisplayMode="Hint" Width="300px" LinkCommand="ViewCustomer">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ARCustomerID" DisplayMode="Hint" Width="300px" LinkCommand="ViewARCustomer">
                        <ValueItems MultiSelect="False">
                        </ValueItems>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Status" Width="120px">
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
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />

        <ActionBar DefaultAction="cmdViewContract">
            <CustomItems>
                <px:PXToolBarButton Text="Contract Details" Key="cmdViewContract" Visible="False">
                    <AutoCallBack Command="ViewContract" Target="ds">
                    </AutoCallBack>
                </px:PXToolBarButton>
            </CustomItems>
        </ActionBar>

    </px:PXGrid>
</asp:Content>
