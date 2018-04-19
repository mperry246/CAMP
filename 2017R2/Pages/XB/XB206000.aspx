<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XB206000.aspx.cs" Inherits="Page_XB206000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" BorderStyle="NotSet"
        PrimaryView="Class" SuspendUnloading="False"
        TypeName="MaxQ.Products.RBRR.QuickClassMaint">
        <CallbackCommands>
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="CopyDefaults" PopupCommand=""
                PopupCommandTarget="" PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="Class" NoteIndicator="True"
        TabIndex="1300">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="S"
                LabelsWidth="S">
            </px:PXLayoutRule>
            <px:PXSelector ID="edClassID" runat="server" DataField="ClassID" 
                CommitChanges="True" DataSourceID="ds" DisplayMode="Value">
            </px:PXSelector>
            <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" Size="L" AlreadyLocalized="False" DefaultLocale="">
            </px:PXTextEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="400px" Style="z-index: 100"
        DataSourceID="ds" DataMember="CurrentClass" Width="100%">
        <Items>
            <px:PXTabItem Text="Attributes">
                                <Template>
                    <px:PXGrid runat="server" ID="AttributeGrid" Width="100%" DataSourceID="ds" Height="100%" SkinID="DetailsInTab" BorderWidth="0px" MatrixMode="True">
                        <AutoSize Enabled="True" />
                        <Levels>
                            <px:PXGridLevel DataMember="AttributeGroup">
                                <RowTemplate>
                                    <px:PXSelector ID="edCRAttributeID" runat="server" DataField="AttributeID" AutoRefresh="true" AllowEdit="true" FilterByAllFields="True" />
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="AttributeID" Width="108px" AutoCallBack="True" />
                                    <px:PXGridColumn  DataField="Description" Width="351px" />
                                    <px:PXGridColumn DataField="SortOrder" TextAlign="Right" Width="108px" />
                                    <px:PXGridColumn  DataField="Required" TextAlign="Center" Type="CheckBox" />
                                    <px:PXGridColumn AllowNull="True" DataField="CSAttribute__IsInternal" TextAlign="Center" Type="CheckBox" />
					                <px:PXGridColumn AllowNull="False" DataField="ControlType" Type="DropDownList" Width="90px" />
                                    <px:PXGridColumn AllowNull="True" DataField="DefaultValue" Width="100px" RenderEditorText="True" />
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar></ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
    </px:PXTab>
</asp:Content>
