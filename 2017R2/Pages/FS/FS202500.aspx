<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FS202500.aspx.cs" Inherits="Page_FS202500" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="BranchLocationRecords" 
        TypeName="PX.Objects.FS.BranchLocationMaint" 
        SuspendUnloading="False">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="ViewMainOnMap" Visible="false" ></px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand DependOnGrid="roomGrid" Name="OpenRoom" Visible="false" ></px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="ValidateAddress" Visible="False"/>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Height="470px" 
        Style="z-index: 100" Width="100%" DataMember="BranchLocationRecords"
        TabIndex="1900" DefaultControlID="edBranchLocationCD" FilesIndicator="true">
        <Template>
            <px:PXLayoutRule runat="server" StartRow="True" ColumnSpan="2" 
                StartColumn="True">
            </px:PXLayoutRule>
            <px:PXPanel ID="PXPanel2" runat="server" RenderSimple="True" 
                RenderStyle="Simple">
                <px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="SM" 
                    StartRow="True">
                </px:PXLayoutRule>
                <px:PXSelector ID="edBranchLocationCD" runat="server" DataField="BranchLocationCD">
                </px:PXSelector>
                <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
                </px:PXTextEdit>
            </px:PXPanel>
            <px:PXLayoutRule runat="server" GroupCaption="Main Contact" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXTextEdit ID="edSalutation" runat="server" DataField="Salutation">
            </px:PXTextEdit>
            <px:PXMailEdit ID="edEMail" runat="server" DataField="EMail">
            </px:PXMailEdit>
            <px:PXLinkEdit ID="edWebSite" runat="server" DataField="WebSite">
            </px:PXLinkEdit>
            <px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1">
            </px:PXMaskEdit>
            <px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2">
            </px:PXMaskEdit>
            <px:PXMaskEdit ID="edFax" runat="server" DataField="Fax">
            </px:PXMaskEdit>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" GroupCaption="Main Address" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" Merge="True">
            </px:PXLayoutRule>
            <px:PXCheckBox ID="edIsValidated" runat="server" DataField="IsValidated" 
                Text="Is Validated">
            </px:PXCheckBox>
            <px:PXButton ID="btnValidateAddress" runat="server" Height="21px" Width="120px">
                <AutoCallBack Command="ValidateAddress" Target="ds">
                </AutoCallBack>
            </px:PXButton>
            <px:PXLayoutRule runat="server">
            </px:PXLayoutRule>
            <px:PXTextEdit ID="edAddressLine1" runat="server" CommitChanges="True" 
                DataField="AddressLine1">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edAddressLine2" runat="server" CommitChanges="True" 
                DataField="AddressLine2">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edCity" runat="server" CommitChanges="True" DataField="City">
            </px:PXTextEdit>
            <px:PXSelector ID="edCountryID" runat="server" AllowEdit="True" 
                AutoRefresh="True" CommitChanges="True" DataField="CountryID" DataSourceID="ds">
            </px:PXSelector>
            <px:PXSelector ID="edState" runat="server" AllowEdit="True" AutoRefresh="True" 
                CommitChanges="True" DataField="State" DataSourceID="ds">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" Merge="True">
            </px:PXLayoutRule>
            <px:PXMaskEdit ID="edPostalCode" runat="server" CommitChanges="True" 
                DataField="PostalCode" Size="S">
            </px:PXMaskEdit>
            <px:PXButton ID="btnViewMainOnMap" runat="server" CommandName="ViewMainOnMap" 
                CommandSourceID="ds" Height="23px" Width="146px">
            </px:PXButton>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="M" 
                LabelsWidth="SM">
            </px:PXLayoutRule>

            <px:PXLayoutRule runat="server" GroupCaption="Financial Settings" 
                StartGroup="True">
            </px:PXLayoutRule>

            <px:PXSelector ID="edBranchID" runat="server" DataField="BranchID" 
                DataSourceID="ds" AllowEdit="True" AutoRefresh="True"></px:PXSelector>
            <px:PXSegmentMask ID="edSubID" runat="server" DataField="SubID" DataSourceID="ds" AutoRefresh="True" AllowEdit="True"></px:PXSegmentMask>
            <px:PXLayoutRule runat="server" EndGroup="True"></px:PXLayoutRule>
            <px:PXLayoutRule runat="server" GroupCaption="Inventory Defaults" 
                StartGroup="True">
            </px:PXLayoutRule>
            <px:PXSegmentMask ID="edDfltSiteID" runat="server" DataField="DfltSiteID" 
                DataSourceID="ds">
            </px:PXSegmentMask>
            <px:PXSegmentMask ID="edDfltSubItemID" runat="server" DataField="DfltSubItemID" 
                DataSourceID="ds" Size="S">
            </px:PXSegmentMask>
            <px:PXSelector ID="edDfltUOM" runat="server" DataField="DfltUOM" 
                DataSourceID="ds" Size="S">
            </px:PXSelector>
            <px:PXCheckBox ID="edRoomFeatureEnabled" runat="server"
                DataField="RoomFeatureEnabled" AlignLeft="True" >
            </px:PXCheckBox>
            <px:PXLayoutRule runat="server" EndGroup="True">
            </px:PXLayoutRule>
        </Template>
        <AutoSize Container="Window" Enabled="True" MinHeight="200" />
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" DataSourceID="ds" DataMember="RoomRecords" BindingContext="form">
        <Items>
            <px:PXTabItem Text="Rooms" BindingContext="form" VisibleExp="DataControls[&quot;edRoomFeatureEnabled&quot;].Value == true">
                <Template>
                    <px:PXGrid ID="roomGrid" runat="server" DataSourceID="ds" Style="z-index: 100" 
                        Width="100%" SkinID="Details" TabIndex="2300" 
                        NoteIndicator="False" FilesIndicator="False" AdjustPageSize="Auto" 
                        AllowPaging="True">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="BranchLocationID,RoomID" DataMember="RoomRecords">
                                <RowTemplate>
                                    <px:PXMaskEdit ID="edRoomID" runat="server" DataField="RoomID" CommitChanges="True">
                                    </px:PXMaskEdit>
                                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
                                    </px:PXTextEdit>
                                    <px:PXNumberEdit ID="edFloorNbr" runat="server" DataField="FloorNbr">
                                    </px:PXNumberEdit>
                                    <px:PXCheckBox ID="edSpecificUse" runat="server" DataField="SpecificUse" Text="Exclusive">
                                    </px:PXCheckBox>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="RoomID" Width="100px" LinkCommand="OpenRoom" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="400px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="FloorNbr" TextAlign="Left" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SpecificUse" TextAlign="Center" Type="CheckBox" Width="80px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                        <ActionBar ActionsText="False">
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
    </px:PXTab>
</asp:Content>
