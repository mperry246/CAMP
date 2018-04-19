<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XMQ25000.aspx.cs" Inherits="Page_XMQ25000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Cust"
        TypeName="MaxQ.Products.Billing.CustomerTrustStatus">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="Cust" TabIndex="6100">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" StartRow="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M">
            </px:PXLayoutRule>
            <px:PXSegmentMask runat="server" DataField="AcctCD" ID="edAcctCD" CommitChanges="True"
                Size="M">
            </px:PXSegmentMask>
            <px:PXLayoutRule runat="server" ControlSize="L" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXTextEdit runat="server" DataField="AcctName" ID="edAcctName" Enabled="False">
            </px:PXTextEdit>
            <px:PXLayoutRule runat="server" StartRow="True" LabelsWidth="M">
            </px:PXLayoutRule>
            <px:PXFormView ID="PXFormView1" runat="server" DataMember="TotalFee" DataSourceID="ds"
                TabIndex="7500" RenderStyle="Simple">
                <Template>
                    <px:PXLayoutRule runat="server" LabelsWidth="M">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edTotalFee" runat="server" DataField="TotalFee" Enabled="False">
                    </px:PXNumberEdit>
                </Template>
            </px:PXFormView>
            <px:PXFormView ID="PXFormView2" runat="server" DataSourceID="ds" DataMember="TotalRevenue"
                RenderStyle="Simple" TabIndex="10300">
                <Template>
                    <px:PXLayoutRule runat="server" LabelsWidth="M">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edAmount" runat="server" DataField="Amount" Enabled="False">
                    </px:PXNumberEdit>
                </Template>
            </px:PXFormView>

            <px:PXFormView ID="PXFormView6" runat="server" DataSourceID="ds" DataMember="ExcludedRevenue"
                RenderStyle="Simple" TabIndex="10300">
                <Template>
                    <px:PXLayoutRule runat="server" LabelsWidth="M">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edAmount" runat="server" DataField="Amount" Enabled="False">
                    </px:PXNumberEdit>
                </Template>
            </px:PXFormView>

            <px:PXFormView ID="PXFormView3" runat="server" DataSourceID="ds" DataMember="ARBal"
                RenderStyle="Simple" TabIndex="10300">
                <Template>
                    <px:PXLayoutRule runat="server" LabelsWidth="M">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edCurrentBal" runat="server" DataField="CurrentBal" Enabled="False">
                    </px:PXNumberEdit>
                </Template>
            </px:PXFormView>
            <px:PXFormView ID="PXFormView4" runat="server" DataSourceID="ds" DataMember="VendorBalance"
                RenderStyle="Simple" TabIndex="10300">
                <Template>
                    <px:PXLayoutRule runat="server" LabelsWidth="M">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edBalance" runat="server" DataField="Balance" Enabled="False">
                    </px:PXNumberEdit>
                </Template>
            </px:PXFormView>
            <px:PXNumberEdit ID="edReserve" runat="server" DataField="Reserve" 
                Enabled="False">
            </px:PXNumberEdit>
            <px:PXFormView ID="PXFormView5" runat="server" DataSourceID="ds" DataMember="AmountAvail"
                RenderStyle="Simple" TabIndex="10300">
                <Template>
                    <px:PXLayoutRule runat="server" LabelsWidth="M">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edAmount3" runat="server" DataField="Amount" Enabled="False">
                    </px:PXNumberEdit>
                </Template>
            </px:PXFormView>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="350px">
        <Items>
            <px:PXTabItem Text="Billing Activities">
                <Template>
                    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="300px"
                        SkinID="DetailsInTab" SyncPosition="True" AdjustPageSize="Auto" MinHeight="150px"
                        AllowPaging="True" TabIndex="25764">
                        <Levels>
                            <px:PXGridLevel DataMember="BillingActivities">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="SM">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="edBillActivityCD" runat="server" DataField="BillActivityCD">
                                    </px:PXSelector>
                                    <px:PXDateTimeEdit ID="edActivityDate" runat="server" DataField="ActivityDate">
                                    </px:PXDateTimeEdit>
                                    <px:PXLayoutRule runat="server" ControlSize="L">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="edActivityID" runat="server" DataField="ActivityID">
                                    </px:PXSelector>
                                    <px:PXLayoutRule runat="server">
                                    </px:PXLayoutRule>
                                    <px:PXNumberEdit ID="edFlatFee" runat="server" DataField="FlatFee">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edFlatQty" runat="server" DataField="FlatQty">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edFlatExt" runat="server" DataField="FlatExt">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edPct" runat="server" DataField="Pct">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edPctExt" runat="server" DataField="PctExt">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edTotalFee" runat="server" DataField="TotalFee">
                                    </px:PXNumberEdit>
                                    <px:PXLayoutRule runat="server" LabelsWidth="M" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXCheckBox ID="edRated" runat="server" DataField="Rated" Text="Rated">
                                    </px:PXCheckBox>
                                    <px:PXSelector ID="edContractID" runat="server" DataField="ContractID">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edOrigTranAmt" runat="server" DataField="OrigTranAmt">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edOrigTranQty" runat="server" DataField="OrigTranQty">
                                    </px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="BillActivityCD" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActivityDate" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActivityID" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FlatFee" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FlatQty" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FlatExt" TextAlign="Right" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Pct" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PctExt" TextAlign="Right" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TotalFee" TextAlign="Right" Width="150px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Rated" TextAlign="Center" Width="60px" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ContractID" TextAlign="Right" Width="175px" DisplayMode="Text">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="OrigTranAmt" TextAlign="Right" Width="175px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="OrigTranQty" TextAlign="Right" Width="175px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowAddNew="False" AllowDelete="False" AllowFormEdit="True" AllowUpdate="False" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Revenue Activities">
                <Template>
                    <px:PXGrid ID="grid2" runat="server" DataSourceID="ds" Width="100%" Height="300px"
                        SkinID="DetailsInTab" SyncPosition="True" AdjustPageSize="Auto" MinHeight="150px"
                        AllowPaging="True" TabIndex="25764">
                        <Levels>
                            <px:PXGridLevel DataMember="RevenueActivities">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="edRevActivityCD" runat="server" DataField="RevActivityCD">
                                    </px:PXSelector>
                                    <px:PXDateTimeEdit ID="edActivityDate2" runat="server" DataField="ActivityDate">
                                    </px:PXDateTimeEdit>
                                    <px:PXNumberEdit ID="edAmount" runat="server" DataField="Amount">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edContractID2" runat="server" DataField="ContractID">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edInvcRefNbr" runat="server" DataField="InvcRefNbr">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edPaymentMethodID" runat="server" DataField="PaymentMethodID">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edPayRefNbr" runat="server" DataField="PayRefNbr">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="RevActivityCD" Width="135px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActivityDate" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Amount" TextAlign="Right" Width="140px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ContractID" TextAlign="Right" DisplayMode="Text" Width="175px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="InvcRefNbr" Width="160px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PaymentMethodID" Width="140px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="PayRefNbr" Width="175px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowAddNew="False" AllowDelete="False" AllowFormEdit="True" AllowUpdate="False" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXTab>
</asp:Content>
