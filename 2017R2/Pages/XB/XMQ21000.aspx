<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XMQ21000.aspx.cs" Inherits="Page_XMQ21000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" BorderStyle="NotSet"
        PrimaryView="ProfileHeader" SuspendUnloading="False" TypeName="MaxQ.Products.Billing.BillingProfileMaint">
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
        Height="95px" DataMember="ProfileHeader" TabIndex="7900">
        <Template>
            <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXSelector runat="server" DataField="ProfileID" ID="ProfileID" DataSourceID="ds">
            </px:PXSelector>
            <px:PXTextEdit runat="server" DataField="Descr" ID="Descr" Size="L">
            </px:PXTextEdit>
            <px:PXCheckBox runat="server" Text="Active" DataField="Active" ID="Active">
            </px:PXCheckBox>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="570px" Style="z-index: 100" Width="100%">
        <Items>
            <px:PXTabItem Text="Details">
                <Template>
                    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
                        Height="100%" SkinID="DetailsInTab" TabIndex="900" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataMember="ProfileDetails" DataKeyNames="ProfileID,LineNbr">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="S" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="ActivityID" runat="server" DataField="ActivityID" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="Descr2" runat="server" DataField="Descr">
                                    </px:PXTextEdit>
                                    <px:PXCheckBox ID="Active2" runat="server" DataField="Active" Text="Active">
                                    </px:PXCheckBox>
                                    <px:PXDropDown ID="ChargeType" runat="server" DataField="ChargeType" Size="S" CommitChanges="True"> 
                                    </px:PXDropDown>
                                    <px:PXNumberEdit ID="Fee" runat="server" DataField="Fee" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="Pct" runat="server" DataField="Pct">
                                    </px:PXNumberEdit>
                                    <px:PXDateTimeEdit ID="BegDate" runat="server" DataField="BegDate" CommitChanges="True">
                                    </px:PXDateTimeEdit>
                                    <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXDateTimeEdit ID="EndDate" runat="server" DataField="EndDate" CommitChanges="True">
                                    </px:PXDateTimeEdit>
                                    <px:PXDropDown ID="ActivityType" runat="server" DataField="ActivityType" Size="S"
                                        CommitChanges="True">
                                    </px:PXDropDown>
                                    <px:PXDropDown ID="Schedule" runat="server" DataField="Schedule" CommitChanges="True">
                                    </px:PXDropDown>
                                    <px:PXNumberEdit ID="DayNumber" runat="server" DataField="DayNumber" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="MinCharge" runat="server" DataField="MinCharge" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="MaxCharge" runat="server" DataField="MaxCharge" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="LateID" runat="server" DataField="LateID">
                                    </px:PXSelector>
                                    <px:PXLayoutRule runat="server" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXNumberEdit ID="Limit" runat="server" DataField="Limit">
                                    </px:PXNumberEdit>
                                    <px:PXDropDown ID="edInvcMethod" runat="server" DataField="InvcMethod">
                                    </px:PXDropDown>
                                    <px:PXSelector ID="edCategoryID" runat="server" DataField="CategoryID">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edTypeID" runat="server" DataField="TypeID">
                                    </px:PXSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="ActivityID" Width="200px" AutoCallBack="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Active" Width="60px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ChargeType" AutoCallBack="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Fee" TextAlign="Right" Width="100px" AutoCallBack="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Pct" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="BegDate" Width="90px" AutoCallBack="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EndDate" Width="90px" AutoCallBack="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn AutoCallBack="True" DataField="ActivityType">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Schedule" Width="120px" AutoCallBack="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="DayNumber" TextAlign="Right" AutoCallBack="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="MinCharge" TextAlign="Right" Width="100px" AutoCallBack="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn AutoCallBack="True" DataField="MaxCharge" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LateID">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Limit" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="InvcMethod">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CategoryID">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TypeID">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <Mode AllowFormEdit="True" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Linked Service Parameters">
                <Template>
                    <px:PXGrid ID="grdSvcParms" runat="server" DataSourceID="ds" Style="z-index: 100"
                        Width="100%" Height="100%" SkinID="DetailsInTab" TabIndex="900" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="BAccountID,SvcParmCD" DataMember="LinkedSvcParms">
                                <RowTemplate>
                                    <px:PXSelector ID="edBAccountID" runat="server" DataField="BAccountID">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edSvcParmCD" runat="server" DataField="SvcParmCD">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edDescr3" runat="server" DataField="Descr">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="BAccountID" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SvcParmCD" Width="145px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="300px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowFormEdit="True" />
                        <AutoSize Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
    </px:PXTab>
</asp:Content>
