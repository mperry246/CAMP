<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB270000.aspx.cs" Inherits="Page_XB270000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Meter"
        TypeName="MaxQ.Products.RBRR.MeterMaint" SuspendUnloading="False">
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
        DataMember="Meter" TabIndex="-17836">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="SM">
            </px:PXLayoutRule>
            <px:PXSelector ID="edMeterID" runat="server" CommitChanges="True" DataField="MeterID">
            </px:PXSelector>
            <px:PXSelector runat="server" DataField="CustomerID" ID="edCustomerID" CommitChanges="True"
                Size="XM">
            </px:PXSelector>
            <px:PXTextEdit runat="server" DataField="ExternalID" ID="edExternalID">
            </px:PXTextEdit>
            <px:PXSelector ID="edInventoryID" runat="server" CommitChanges="True" DataField="InventoryID"
                Size="XM">
            </px:PXSelector>
            <px:PXTextEdit runat="server" DataField="Description" ID="edDescription" Size="L">
            </px:PXTextEdit>
            <px:PXDropDown ID="edMeterType" runat="server" DataField="MeterType">
            </px:PXDropDown>
            <px:PXSelector ID="edUOM" runat="server" DataField="UOM" Size="S">
            </px:PXSelector>
            <px:PXNumberEdit runat="server" DataField="LastBilledQty" ID="edLastBilledQty">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edInitialQty" runat="server" DataField="InitialQty" CommitChanges="True">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edBillPeriodQty" runat="server" DataField="BillPeriodQty">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edRollOverQty" runat="server" DataField="RollOverQty">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edContractMax" runat="server" DataField="ContractMax">
            </px:PXNumberEdit>
            <px:PXNumberEdit ID="edContractTotal" runat="server" DataField="ContractTotal">
            </px:PXNumberEdit>
            <px:PXCheckBox ID="edAutoRenewMax" runat="server" DataField="AutoRenewMax">
            </px:PXCheckBox>
            <px:PXDateTimeEdit ID="edInitializedDate" runat="server" DataField="InitializedDate">
            </px:PXDateTimeEdit>
            <px:PXLayoutRule runat="server" GroupCaption="Reading" StartGroup="True" LabelsWidth="SM"
                StartColumn="True">
            </px:PXLayoutRule>
            <px:PXNumberEdit ID="edReadingQty" runat="server" DataField="ReadingQty"
                CommitChanges="True">
            </px:PXNumberEdit>
            <px:PXDateTimeEdit CommitChanges="True" ID="edReadingDate" runat="server" DataField="ReadingDate">
            </px:PXDateTimeEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="300px" Style="z-index: 100" Width="100%">
        <Items>
            <px:PXTabItem Text="Meter Reading History">
                <Template>
                    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
                        Height="300px" SkinID="Details" TabIndex="800">
                        <Levels>
                            <px:PXGridLevel DataMember="Readings">
                                <RowTemplate>
                                    <px:PXDateTimeEdit ID="edReadingDate" runat="server" DataField="ReadingDate">
                                    </px:PXDateTimeEdit>
                                    <px:PXNumberEdit ID="edMeterQty" runat="server" DataField="MeterQty">
                                    </px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="ReadingDate" Width="110px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="MeterQty" TextAlign="Right" Width="100px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                        <ActionBar ActionsText="False">
                        </ActionBar>
                        <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Associated Contracts">
                <Template>
                    <px:PXGrid ID="grdContracts" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
                        Height="300px" SkinID="Details" TabIndex="800">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="ContractCD,RevisionNbr" DataMember="Contracts">
                                <RowTemplate>
                                    <px:PXSelector ID="edContractCD" runat="server" DataField="ContractCD">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edRevisionNbr" runat="server" DataField="RevisionNbr">
                                    </px:PXSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="ContractCD" Width="100px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="RevisionNbr" Width="120px">
                                        <ValueItems MultiSelect="False">
                                        </ValueItems>
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
    </px:PXTab>
</asp:Content>
