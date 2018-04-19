<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XMQ27000.aspx.cs" Inherits="Page_XMQ27000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="PromiseHdr"
        TypeName="MaxQ.Products.Billing.PromiseToPayMaint">
        <CallbackCommands>
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" PopupVisible="true" />
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="AddARDocs" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AddDoc" Visible="False">
            </px:PXDSCallbackCommand>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="PromiseHdr" TabIndex="600" ActivityIndicator="True">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" ColumnWidth="M" LabelsWidth="SM">
            </px:PXLayoutRule>
            <px:PXSelector ID="edPromiseID" runat="server" DataField="PromiseID" Size="M">
            </px:PXSelector>
            <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID" Size="L" CommitChanges="True">
            </px:PXSelector>
            <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" Size="L">
            </px:PXTextEdit>
            <px:PXDateTimeEdit ID="edPromiseDate" runat="server" DataField="PromiseDate">
            </px:PXDateTimeEdit>
            <px:PXLayoutRule runat="server" ControlSize="XM">
            </px:PXLayoutRule>
            <pxa:PXCurrencyRate ID="edCuryID" runat="server" DataField="CuryID" DataMember="_Currency_"
                DataSourceID="ds" RateTypeView="_XMQPromisePayHdr_CurrencyInfo_"></pxa:PXCurrencyRate>
            <px:PXLayoutRule runat="server" ControlSize="XS">
            </px:PXLayoutRule>
            <px:PXNumberEdit ID="edCuryTotPaid" runat="server" DataField="CuryTotPaid">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edCuryTotPromised" runat="server" DataField="CuryTotPromised">
            </px:PXNumberEdit>
            <px:PXDropDown ID="edStatus" runat="server" DataField="Status" CommitChanges="True">
            </px:PXDropDown>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="540px" Style="z-index: 100;" DataSourceID="ds">
        <Items>
            <px:PXTabItem Text=" AR Docs">
                <Template>
                    <table cellpadding="0" cellspacing="0" style="height: 700px; width: 100%">
                        <tr>
                            <td style="height: 49%; vertical-align: top">
                                <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%"
                                    Height="473px" SkinID="DetailsInTab" TabIndex="100" NoteIndicator="True" TemporaryFilterCaption="Filter Applied" SyncPosition="True">
                                    <Levels>
                                        <px:PXGridLevel DataKeyNames="LineNbr,PromiseID" DataMember="PromiseDet">
                                            <RowTemplate>
                                                <px:PXLayoutRule runat="server" StartColumn="True">
                                                </px:PXLayoutRule>
                                                <px:PXDropDown ID="edDocType" runat="server" DataField="DocType" CommitChanges="True">
                                                </px:PXDropDown>
                                                <px:PXSelector ID="edRefNbr" runat="server" DataField="RefNbr"
                                                    CommitChanges="True" AutoRefresh="True">
                                                </px:PXSelector>
                                                <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" Size="L">
                                                </px:PXTextEdit>
                                                <px:PXNumberEdit ID="edCuryAmtPromised" runat="server" DataField="CuryAmtPromised">
                                                </px:PXNumberEdit>
                                                <px:PXNumberEdit ID="edCuryAmtPaid" runat="server" DataField="CuryAmtPaid" Enabled="False">
                                                </px:PXNumberEdit>
                                                <px:PXNumberEdit ID="edCuryDisputeAmt" runat="server" DataField="CuryDisputeAmt">
                                                </px:PXNumberEdit>
                                                <px:PXCheckBox ID="edAgingExclude" runat="server" DataField="AgingExclude" Text="Exclude from Aging">
                                                </px:PXCheckBox>
                                                <px:PXDropDown ID="edStatus" runat="server" DataField="Status" CommitChanges="True">
                                                </px:PXDropDown>
                                            </RowTemplate>
                                            <Columns>
                                                <px:PXGridColumn DataField="DocType" Width="125px" CommitChanges="True">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="RefNbr" Width="150px" CommitChanges="True" LinkCommand="ViewInvoice">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="Descr" Width="200px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CuryAmtPromised" Width="125px" TextAlign="Right">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CuryAmtPaid" TextAlign="Right" Width="100px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="CuryDisputeAmt" Width="125px" TextAlign="Right">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn DataField="AgingExclude" TextAlign="Center" Type="CheckBox" Width="125px">
                                                </px:PXGridColumn>
                                                <px:PXGridColumn CommitChanges="True" DataField="Status" Width="100px">
                                                </px:PXGridColumn>
                                            </Columns>
                                        </px:PXGridLevel>
                                    </Levels>
                                    <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                                    <ActionBar ActionsText="False">
                                        <CustomItems>
                                            <px:PXToolBarButton SuppressHtmlEncoding="False" Text="ADD AR DOCS">
                                                <AutoCallBack Command="AddARDocs" Target="ds">
                                                </AutoCallBack>
                                            </px:PXToolBarButton>
                                        </CustomItems>
                                    </ActionBar>
                                    <Mode AllowFormEdit="True" />
                                </px:PXGrid>
                            </td>
                        </tr>
                    </table>
                </Template>
                <ContentLayout ColumnsWidth="540" />
            </px:PXTabItem>
            <px:PXTabItem Text="Activities">
                <Template>
                    <px:PXGrid ID="PXGrid1" runat="server" DataSourceID="ds" TemporaryFilterCaption="Filter Applied" Height="473px" SkinID="DetailsInTab" Width="100%" TabIndex="17200" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="TaskID" DataMember="Activities">
                                <RowTemplate>
                                    <px:PXSelector ID="edSubject" runat="server" DataField="Subject">
                                    </px:PXSelector>
                                    <px:PXDropDown ID="edUIStatus" runat="server" DataField="UIStatus">
                                    </px:PXDropDown>
                                    <px:PXDateTimeEdit ID="edStartDate_Date" runat="server" DataField="StartDate_Date">
                                    </px:PXDateTimeEdit>
                                    <px:PXDateTimeEdit ID="edEndDate_Date" runat="server" DataField="EndDate_Date">
                                    </px:PXDateTimeEdit>
                                    <px:PXTextEdit ID="edSource" runat="server" DataField="Source">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="Subject" Width="200px" LinkCommand="ViewDetails">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UIStatus">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="StartDate_Date" Width="90px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EndDate_Date" Width="90px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Source" LinkCommand="ViewEntity" Width="200px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150"></AutoSize>
    </px:PXTab>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="phDialogs">
    <px:PXSmartPanel ID="ARDocsPanel" runat="server" CaptionVisible="True" Caption="ADD AR Docs"
        Height="594px" Width="1000px" DesignView="Content" Key="AddARDocsFilter" AutoReload="True"
        AutoRepaint="True" TabIndex="7500" CreateOnDemand="True">
        <px:PXFormView ID="frmAddARDocs" runat="server" DataSourceID="ds" Style="z-index: 100"
            Width="100%" DataMember="AddARDocsFilter" TabIndex="16364">
            <Template>
                <px:PXDropDown ID="edDocType" runat="server" DataField="DocType" Size="SM" CommitChanges="True">
                </px:PXDropDown>
                <px:PXCheckBox ID="edOnlyAvailable" runat="server" DataField="OnlyAvailable" Text="Past Due" CommitChanges="True">
                </px:PXCheckBox>
            </Template>
        </px:PXFormView>

        <px:PXGrid ID="ARDOCS" runat="server" DataSourceID="ds" TabIndex="10400" TemporaryFilterCaption="Filter Applied" Height="300px" SkinID="Details" SyncPosition="True" MatrixMode="True" RepaintColumns="True">
            <Levels>
                <px:PXGridLevel DataKeyNames="DocType,RefNbr" DataMember="ARDocs">
                    <RowTemplate>
                        <px:PXCheckBox ID="edSelect" runat="server" DataField="Select" Text="Select">
                        </px:PXCheckBox>
                        <px:PXDropDown ID="edDocTyp" runat="server" DataField="DocType">
                        </px:PXDropDown>
                        <px:PXSelector ID="edRefNbre" runat="server" DataField="RefNbr">
                        </px:PXSelector>
                        <px:PXSegmentMask ID="edCustomerID" runat="server" DataField="CustomerID">
                        </px:PXSegmentMask>
                        <px:PXDropDown ID="edOrigModule" runat="server" DataField="OrigModule">
                        </px:PXDropDown>
                        <px:PXDateTimeEdit ID="edDocDate" runat="server" DataField="DocDate">
                        </px:PXDateTimeEdit>
                        <px:PXDateTimeEdit ID="edDueDate" runat="server" DataField="DueDate">
                        </px:PXDateTimeEdit>
                        <px:PXSelector ID="edFinPeriodID" runat="server" DataField="FinPeriodID">
                        </px:PXSelector>
                        <px:PXSelector ID="edCuryID" runat="server" DataField="CuryID">
                        </px:PXSelector>
                        <px:PXNumberEdit ID="edCuryOrigDocAmt" runat="server" DataField="CuryOrigDocAmt">
                        </px:PXNumberEdit>
                        <px:PXNumberEdit ID="edCuryDocBal" runat="server" DataField="CuryDocBal">
                        </px:PXNumberEdit>
                        <px:PXNumberEdit ID="edCuryOrigDiscAmt" runat="server" DataField="CuryOrigDiscAmt">
                        </px:PXNumberEdit>
                        <px:PXNumberEdit ID="edCuryDiscBal" runat="server" DataField="CuryDiscBal">
                        </px:PXNumberEdit>
                        <px:PXTextEdit ID="edDocDesc" runat="server" DataField="DocDesc">
                        </px:PXTextEdit>
                    </RowTemplate>
                    <Columns>
                        <px:PXGridColumn DataField="Select" TextAlign="Center" Type="CheckBox" Width="60px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="DocType">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="RefNbr">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="CustomerID" Width="120px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="OrigModule">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="DocDate" Width="90px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="DueDate" Width="90px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="FinPeriodID">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="CuryID">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="CuryOrigDocAmt" TextAlign="Right" Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="CuryDocBal" TextAlign="Right" Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="CuryOrigDiscAmt" TextAlign="Right" Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="CuryDiscBal" TextAlign="Right" Width="100px">
                        </px:PXGridColumn>
                        <px:PXGridColumn DataField="DocDesc" Width="200px">
                        </px:PXGridColumn>
                    </Columns>
                </px:PXGridLevel>
            </Levels>
            <Mode AllowAddNew="False" AllowDelete="False" />
        </px:PXGrid>

        <px:PXPanel ID="PXPanel1" runat="server" SkinID="Buttons">
            <px:PXButton ID="Add" runat="server" Text="Add" CommandName="AddDoc" CommandSourceID="ds" SyncVisible="False" Height="22px">
            </px:PXButton>
            <px:PXButton ID="AddClose" runat="server" Text="ADD &amp; CLOSE" DialogResult="OK">
            </px:PXButton>
            <px:PXButton ID="Cancel" runat="server" Text="Cancel" DialogResult="Cancel" Height="22px">
            </px:PXButton>
        </px:PXPanel>

    </px:PXSmartPanel>
</asp:Content>

