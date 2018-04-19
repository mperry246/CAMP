<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB205000.aspx.cs" Inherits="Page_XB205000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="MaxQ.Products.RBRR.MSAMaint" PrimaryView="MSAHeader">
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="MSAHeader" TabIndex="16100" ActivityIndicator="True">
        <Template>
            <px:PXLayoutRule runat="server" StartRow="True" StartColumn="True" />
            <px:PXSelector ID="edMSAID" runat="server" DataField="MSAID" Size="SM" CommitChanges="True">
            </px:PXSelector>
            <px:PXTextEdit ID="edDescr" runat="server" AlreadyLocalized="False" DataField="Descr" Size="L">
            </px:PXTextEdit>
            <px:PXDateTimeEdit ID="edMSADate" runat="server" AlreadyLocalized="False" DataField="MSADate">
            </px:PXDateTimeEdit>
            <px:PXSegmentMask ID="edBranchID" runat="server" DataField="BranchID" Size="XM">
            </px:PXSegmentMask>
            <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID" Size="XM" CommitChanges="True">
            </px:PXSelector>
            <px:PXCheckBox ID="edOnlyOneCustomer" runat="server" AlreadyLocalized="False" DataField="OnlyOneCustomer" Text="Only Valid for this Customer">
            </px:PXCheckBox>
            <px:PXSelector ID="edCustomerLocationID" runat="server" DataField="CustomerLocationID" Size="XM">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXDropDown ID="edStatus" runat="server" DataField="Status" Size="S" CommitChanges="True">
            </px:PXDropDown>
            <px:PXCheckBox ID="edHold" runat="server" AlreadyLocalized="False" DataField="Hold" Text="Hold" CommitChanges="True">
            </px:PXCheckBox>
            <px:PXSelector ID="edClassID" runat="server" DataField="ClassID" Size="XM" CommitChanges="True">
            </px:PXSelector>
            <px:PXDateTimeEdit ID="edBegDate" runat="server" AlreadyLocalized="False" DataField="BegDate" CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit ID="edEndDate" runat="server" AlreadyLocalized="False" DataField="EndDate" CommitChanges="True">
            </px:PXDateTimeEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" DataMember="CurrentMSA">
        <Items>
            <px:PXTabItem Text="Related Contracts">
                <Template>
                    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" 
                        SkinID="DetailsInTab" SyncPosition="True" AdjustPageSize="Auto"
                        AllowPaging="True" TabIndex="25764" KeepPosition="True" TemporaryFilterCaption="Filter Applied">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="ContractCD,RevisionNbr" DataMember="RelatedContracts">
                                <RowTemplate>
                                    <px:PXSelector ID="edContractCD" runat="server" DataField="ContractCD">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edRevisionNbr" runat="server" DataField="RevisionNbr">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edDescr" runat="server" AlreadyLocalized="False" DataField="Descr">
                                    </px:PXTextEdit>
                                    <px:PXDateTimeEdit ID="edBegDate" runat="server" AlreadyLocalized="False" DataField="BegDate">
                                    </px:PXDateTimeEdit>
                                    <px:PXDateTimeEdit ID="edEndDate" runat="server" AlreadyLocalized="False" DataField="EndDate">
                                    </px:PXDateTimeEdit>
                                    <px:PXDropDown ID="edStatus" runat="server" DataField="Status">
                                    </px:PXDropDown>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="ContractCD" Width="100px" LinkCommand="ViewContract">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="RevisionNbr" Width="150px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CustomerID" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="BegDate" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EndDate" Width="90px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status" Width="100px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar>
                            <Actions>
                                <AddNew Enabled="False" />
                                <Delete Enabled="False" />
                                <EditRecord Enabled="False" />
                            </Actions>
                        </ActionBar>
                        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Attributes">
                <Template>
                    <px:PXGrid runat="server" ID="AttrbGrd" TemporaryFilterCaption="Filter Applied" DataSourceID="ds" TabIndex="15000"
                        Width="100%" SkinID="Inquire" MatrixMode="True">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="RefNoteID,AttributeID" DataMember="Answers">
                                <Columns>
                                    <px:PXGridColumn DataField="AttributeID" TextAlign="Left" Width="220px" AllowShowHide="False" TextField="AttributeID_description"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="isRequired" TextAlign="Center" Type="CheckBox" Width="75px"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Value" Width="148px"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXTab>
</asp:Content>
