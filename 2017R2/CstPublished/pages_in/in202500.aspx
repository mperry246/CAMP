<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="IN202500.aspx.cs" Inherits="Page_IN202500" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.IN.InventoryItemMaint" PrimaryView="Item">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand StartNewGroup="True" Name="Action" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="Inquiry" />
            <px:PXDSCallbackCommand Name="AddWarehouseDetail" Visible="false" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="UpdateReplenishment" Visible="false" CommitChanges="true" DependOnGrid="repGrid" />
            <px:PXDSCallbackCommand Name="GenerateSubitems" Visible="false" CommitChanges="true" DependOnGrid="repGrid" />
            <px:PXDSCallbackCommand Name="ViewGroupDetails" Visible="False" DependOnGrid="grid3" />
            <px:PXDSCallbackCommand Name="syncSalesforce" Visible="false" />
        </CallbackCommands>
        <DataTrees>
            <px:PXTreeDataMember TreeView="_EPCompanyTree_Tree_" TreeKeys="WorkgroupID" />
            <px:PXTreeDataMember TreeView="EntityItems" TreeKeys="Key" />
            <px:PXTreeDataMember TreeKeys="CategoryID" TreeView="Categories" />
        </DataTrees>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXSmartPanel ID="pnlChangeID" runat="server" Caption="Specify New ID"
        CaptionVisible="true" DesignView="Hidden" LoadOnDemand="true" Key="ChangeIDDialog" CreateOnDemand="false" AutoCallBack-Enabled="true"
        AutoCallBack-Target="formChangeID" AutoCallBack-Command="Refresh" CallBackMode-CommitChanges="True" CallBackMode-PostData="Page"
        AcceptButtonID="btnOK">
        <px:PXFormView ID="formChangeID" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" CaptionVisible="False"
            DataMember="ChangeIDDialog">
            <ContentStyle BackColor="Transparent" BorderStyle="None" />
            <Template>
                <px:PXLayoutRule ID="rlAcctCD" runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                <px:PXSegmentMask ID="edAcctCD" runat="server" DataField="CD" />
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="pnlChangeIDButton" runat="server" SkinID="Buttons">
            <px:PXButton ID="btnOK" runat="server" DialogResult="OK" Text="OK">
                <AutoCallBack Target="formChangeID" Command="Save" />
            </px:PXButton>
			<px:PXButton ID="btnCancel" runat="server" DialogResult="Cancel" Text="Cancel" />
        </px:PXPanel>
    </px:PXSmartPanel>
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Item" Caption="Stock Item Summary" NoteIndicator="True" FilesIndicator="True" ActivityIndicator="True"
        ActivityField="NoteActivity" DefaultControlID="edInventoryCD">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
            <px:PXSegmentMask ID="edInventoryCD" runat="server" DataField="InventoryCD" DataSourceID="ds" AutoRefresh="true" >
                <GridProperties FastFilterFields="InventoryCD,Descr" />
		</px:PXSegmentMask>
            <px:PXDropDown ID="edItemStatus" runat="server" DataField="ItemStatus" Size="S" />
            <px:PXLayoutRule runat="server" ColumnSpan="2" />
            <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
            <px:PXSelector CommitChanges="True" ID="edProductWorkgroupID" runat="server" DataField="ProductWorkgroupID" />
            <px:PXSelector ID="edProductManagerID" runat="server" DataField="ProductManagerID" AutoRefresh="True" />
            <px:PXCheckBox ID="chkEquipmentManagement" runat="server" DataField="ChkEquipmentManagement"/>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="606px" DataSourceID="ds" DataMember="ItemSettings" FilesIndicator="False" NoteIndicator="False">
        <AutoSize Enabled="True" Container="Window" MinHeight="150" ></AutoSize>
        <Items>
            <px:PXTabItem Text="General Settings">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" ></px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Item Defaults" ></px:PXLayoutRule>
                    <px:PXSegmentMask CommitChanges="True" ID="edItemClassID" runat="server" DataField="ItemClassID" AllowEdit="True" ></px:PXSegmentMask>
                    <px:PXDropDown ID="edItemType" runat="server" DataField="ItemType" ></px:PXDropDown>
                    <px:PXCheckBox SuppressLabel="True" ID="chkKitItem" runat="server" DataField="KitItem" ></px:PXCheckBox>
                    <px:PXDropDown CommitChanges="True" ID="edValMethod" runat="server" DataField="ValMethod" ></px:PXDropDown>
                    <px:PXSelector ID="edTaxCategoryID" runat="server" DataField="TaxCategoryID" AllowEdit="True" CommitChanges="True" AutoRefresh="True" ></px:PXSelector>
                    <px:PXSelector CommitChanges="True" ID="edPostClassID" runat="server" DataField="PostClassID" AllowEdit="True" ></px:PXSelector>
                    <px:PXSelector CommitChanges="True" ID="edLotSerClassID" runat="server" DataField="LotSerClassID" AllowEdit="True" ></px:PXSelector>
                    <px:PXMaskEdit ID="edLotSerNumVal" runat="server" DataField="LotSerNumVal" ></px:PXMaskEdit>
	<px:PXTextEdit runat="server" ID="CustEdMQXPMNIGPCode1" DataField="MQXPMNIGPCode" />
	<px:PXLayoutRule runat="server" ID="Rule8" StartGroup="True" GroupCaption="Advanced Billing Defaults" />
	<px:PXSelector runat="server" ID="CustEdContrCreationCode3" DataField="ContrCreationCode" />
	<px:PXCheckBox runat="server" ID="CustEdNoRenew6" DataField="NoRenew" Text="NoRenew" />
	<px:PXCheckBox runat="server" ID="CustEdNoCopy7" DataField="NoCopy" Text="NoCopy" />
	<px:PXSegmentMask runat="server" ID="CstPXSegmentMask14" DataField="ContractItemID" />
	<px:PXSelector runat="server" ID="CstPXSelector15" DataField="DfltRenewalInventoryID" />
	<px:PXLayoutRule runat="server" ID="Rule21" Merge="True" ControlSize="S" />
	<px:PXNumberEdit runat="server" ID="CustEdDfltContrLen19" DataField="DfltContrLen" />
	<px:PXDropDown runat="server" ID="CustEdDfltContrInt20" DataField="DfltContrInt" SuppressLabel="True" />
	<px:PXLayoutRule runat="server" ID="Rule22" Merge="False" />
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Warehouse Defaults" ></px:PXLayoutRule>
                    <px:PXSegmentMask CommitChanges="True" ID="edDfltSiteID" runat="server" DataField="DfltSiteID" AllowEdit="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask CommitChanges="True" ID="edDfltShipLocationID" runat="server" DataField="DfltShipLocationID" AutoRefresh="True" AllowEdit="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask CommitChanges="True" ID="edDfltReceiptLocationID" runat="server" DataField="DfltReceiptLocationID" AutoRefresh="True" AllowEdit="True" ></px:PXSegmentMask>
                    <px:PXLayoutRule runat="server" Merge="True" ></px:PXLayoutRule>
                    <px:PXSegmentMask Size="s" ID="edDefaultSubItemID" runat="server" DataField="DefaultSubItemID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXCheckBox ID="chkDefaultSubItemOnEntry" runat="server" DataField="DefaultSubItemOnEntry" ></px:PXCheckBox>
                    <px:PXLayoutRule runat="server" ></px:PXLayoutRule>
                    <px:PXLayoutRule ID="PXLayoutRule2" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" ></px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" GroupCaption="Unit of Measure" StartGroup="True" ></px:PXLayoutRule>
                    <px:PXSelector ID="edBaseUnit" Size="s" runat="server" AllowEdit="True" CommitChanges="True" DataField="BaseUnit" ></px:PXSelector>
                    <px:PXSelector ID="edSalesUnit" Size="s" runat="server" AllowEdit="True" AutoRefresh="True" CommitChanges="True" DataField="SalesUnit" ></px:PXSelector>
                    <px:PXSelector ID="edPurchaseUnit" Size="s" runat="server" AllowEdit="True" AutoRefresh="True" CommitChanges="True" DataField="PurchaseUnit" ></px:PXSelector>
                    <px:PXLayoutRule runat="server" LabelsWidth="SM" ControlSize="S" SuppressLabel="True" ></px:PXLayoutRule>
                    <px:PXGrid ID="gridUnits" runat="server" DataSourceID="ds" SkinID="ShortList" Width="400px" Height="114px">
                        <Mode InitNewRow="True" ></Mode>
                        <Levels>
                            <px:PXGridLevel DataMember="itemunits">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" ></px:PXLayoutRule>
                                    <px:PXNumberEdit ID="edItemClassID2" runat="server" DataField="ItemClassID" ></px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edInventoryID" runat="server" DataField="InventoryID" ></px:PXNumberEdit>
                                    <px:PXMaskEdit ID="edFromUnit" runat="server" DataField="FromUnit" ></px:PXMaskEdit>
                                    <px:PXMaskEdit ID="edSampleToUnit" runat="server" DataField="SampleToUnit" ></px:PXMaskEdit>
                                    <px:PXNumberEdit ID="edUnitRate" runat="server" DataField="UnitRate" ></px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="UnitType" Type="DropDownList" Width="99px" Visible="False" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ItemClassID" Width="36px" Visible="False" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="InventoryID" Visible="False" TextAlign="Right" Width="54px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="FromUnit" Width="72px" CommitChanges="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="UnitMultDiv" Type="DropDownList" Width="90px" CommitChanges="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="UnitRate" TextAlign="Right" Width="108px" CommitChanges="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="SampleToUnit" Width="72px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="PriceAdjustmentMultiplier" TextAlign="Right" Width="108px" CommitChanges="True" ></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Layout ColumnsMenu="False" ></Layout>
                    </px:PXGrid>
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Physical Inventory" ></px:PXLayoutRule>
                    <px:PXSelector CommitChanges="True" ID="edPICycleID" runat="server" DataField="CycleID" AllowEdit="True" ></px:PXSelector>
                    <px:PXSelector CommitChanges="True" ID="edABCCodeID" runat="server" DataField="ABCCodeID" AllowEdit="True" ></px:PXSelector>
                    <px:PXCheckBox SuppressLabel="True" ID="chkABCCodeIsFixed" runat="server" DataField="ABCCodeIsFixed" ></px:PXCheckBox>
                    <px:PXSelector CommitChanges="True" ID="edMovementClassID" runat="server" DataField="MovementClassID" AllowEdit="True" ></px:PXSelector>
                    <px:PXCheckBox SuppressLabel="True" ID="chkMovementClassIsFixed" runat="server" DataField="MovementClassIsFixed" ></px:PXCheckBox>
	<px:PXLayoutRule runat="server" ID="Rule30" StartGroup="True" GroupCaption="Advanced Billing Bundle Options" />
	<px:PXCheckBox runat="server" ID="CustEdIsBundle15" DataField="IsBundle" Text="IsBundle" CommitChanges="True" />
	<px:PXDropDown runat="server" ID="CustEdTrialType23" DataField="TrialType" />
	<px:PXLayoutRule runat="server" ID="Rule26" Merge="True" ControlSize="S" />
	<px:PXNumberEdit runat="server" ID="CustEdDfltTrialLen24" DataField="DfltTrialLen" />
	<px:PXDropDown runat="server" ID="CustEdDfltTrialInt25" DataField="DfltTrialInt" SuppressLabel="True" />
	<px:PXLayoutRule runat="server" ID="Rule27" Merge="False" />
	<px:PXNumberEdit runat="server" ID="CustEdTrialPrice18" DataField="TrialPrice" />
	<px:PXLayoutRule runat="server" ID="Rule9" GroupCaption="Advanced Billing Options" StartGroup="True" />
	<px:PXSelector runat="server" ID="CstPXSelector13" DataField="ForecastClassID" />
	<px:PXSelector runat="server" ID="CustEdActivityID2" DataField="ActivityID" DisplayMode="Text" />
	<px:PXCheckBox runat="server" ID="CustEdDepositItem9" DataField="DepositItem" Text="DepositItem" /></Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Subitems" Key="Subitems" RepaintOnDemand="false">
                <Template>
                    <px:PXGrid ID="gridSegmentValues" runat="server" DataSourceID="ds" Height="100%" Width="100%" SkinID="DetailsInTab">
                        <Mode InitNewRow="true" ></Mode>
                        <Levels>
                            <px:PXGridLevel DataMember="SegmentValues">
                                <Columns>
                                    <px:PXGridColumn DataField="SegmentID" Width="90px" CommitChanges="true" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Value" DisplayFormat="&gt;aaaaaa" Width="90px" CommitChanges="true" ></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" ></AutoSize>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Price/Cost Info">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" StartGroup="True" ControlSize="XM" GroupCaption="Price Management" ></px:PXLayoutRule>
                    <px:PXSelector ID="edPriceClassID" runat="server" DataField="PriceClassID" AllowEdit="True" ></px:PXSelector>
                    <px:PXSelector CommitChanges="True" ID="edPriceWorkgroupID" runat="server" DataField="PriceWorkgroupID" ShowRootNode="False" ></px:PXSelector>
                    <px:PXSelector ID="edPriceManagerID" runat="server" DataField="PriceManagerID" AutoRefresh="True" CommitChanges="True"></px:PXSelector>
                    <px:PXCheckBox SuppressLabel="True" ID="chkCommisionable" runat="server" DataField="Commisionable" ></px:PXCheckBox>
                    <px:PXNumberEdit ID="edMinGrossProfitPct" runat="server" DataField="MinGrossProfitPct" ></px:PXNumberEdit>
                    <px:PXNumberEdit ID="edMarkupPct" runat="server" DataField="MarkupPct" ></px:PXNumberEdit>
                    <px:PXNumberEdit ID="edRecPrice" runat="server" DataField="RecPrice" ></px:PXNumberEdit>
                    <px:PXNumberEdit ID="edBasePrice" runat="server" DataField="BasePrice" Enabled="True" ></px:PXNumberEdit>
                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="XM" StartGroup="True" GroupCaption="Standard Cost" ></px:PXLayoutRule>
                    <px:PXNumberEdit ID="edPendingStdCost" runat="server" DataField="PendingStdCost" CommitChanges="True" ></px:PXNumberEdit>
                    <px:PXDateTimeEdit ID="edPendingStdCostDate" runat="server" DataField="PendingStdCostDate" ></px:PXDateTimeEdit>
                    <px:PXNumberEdit ID="edStdCost" runat="server" DataField="StdCost" Enabled="False" ></px:PXNumberEdit>
                    <px:PXDateTimeEdit ID="edStdCostDate" runat="server" DataField="StdCostDate" Enabled="False" ></px:PXDateTimeEdit>
                    <px:PXNumberEdit ID="edLastStdCost" runat="server" DataField="LastStdCost" Enabled="False" ></px:PXNumberEdit>
                    <px:PXLayoutRule runat="server" GroupCaption="Cost Statistics" ControlSize="XM" StartGroup="True" ></px:PXLayoutRule>
                    <px:PXFormView ID="formCostStats" runat="server" Width="100%" DataMember="itemcosts" DataSourceID="ds" SkinID="Transparent">
                        <Template>
                             <px:PXNumberEdit ID="edLastCost" runat="server" DataField="LastCost" ></px:PXNumberEdit>
                            <px:PXNumberEdit ID="edAvgCost" runat="server" DataField="AvgCost" Enabled="False" ></px:PXNumberEdit>
                            <px:PXNumberEdit ID="edMinCost" runat="server" DataField="MinCost" Enabled="False" ></px:PXNumberEdit>
                            <px:PXNumberEdit ID="edMaxCost" runat="server" DataField="MaxCost" Enabled="False" ></px:PXNumberEdit>
                        </Template>
                    </px:PXFormView>
                    <px:PXFormView ID="formRUTROTItemSettings" runat="server" Width="100%" DataMember="RUTROTItemSettings" DataSourceID="ds" Caption="Rules" SkinID="Transparent">
                        <Template>
                            <px:PXLayoutRule runat="server" ID="PXLayoutRuleC1" StartGroup="true" GroupCaption="RUT and RUT Settings" ControlSize="XM" ></px:PXLayoutRule>
                            <px:PXGroupBox runat="server" DataField="RUTROTType" CommitChanges="True" RenderStyle="Simple" ID="gbRRType">
				            <ContentLayout Layout="Stack" Orientation="Horizontal" ></ContentLayout>
				            <Template>
					            <px:PXRadioButton runat="server" Value="O" ID="gbRRType_opO" GroupName="gbRRType" Text="ROT"  ></px:PXRadioButton>
					            <px:PXRadioButton runat="server" Value="U" ID="gbRRType_opU" GroupName="gbRRType" Text="RUT" ></px:PXRadioButton>
				            </Template>
                            </px:PXGroupBox>
                            <px:PXDropDown runat="server" DataField="RUTROTItemType" ID="cmbRUTROTItemType" CommitChanges="true" ></px:PXDropDown>
                            <px:PXSelector runat="server" DataField="RUTROTWorkTypeID" ID="cmbRUTROTWorkType" CommitChanges="true" AutoRefresh="true" ></px:PXSelector>
                        </Template>
                    </px:PXFormView>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Warehouse Details">
                <Template>
                    <px:PXGrid ID="grid2" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100" Width="100%" ActionsPosition="Top" EditPageUrl="~/Pages/IN/IN204500.aspx" BorderWidth="0px" SkinID="Details">
                        <EditPageParams>
                            <px:PXControlParam ControlID="grid2" Direction="Output" Name="InventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" ></px:PXControlParam>
                            <px:PXControlParam ControlID="grid2" Direction="Output" Name="SiteID" PropertyName="DataValues[&quot;SiteID&quot;]" Type="String" ></px:PXControlParam>
                        </EditPageParams>
                        <ActionBar>
                            <CustomItems>
                                <px:PXToolBarButton>
                                    <AutoCallBack Command="AddWarehouseDetail" Target="ds" ></AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <Levels>
                            <px:PXGridLevel DataMember="itemsiterecords" DataKeyNames="InventoryID,SiteID">
                                <RowTemplate>
                                    <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" ></px:PXLayoutRule>
                                    <px:PXSegmentMask ID="edPreferredVendorID" runat="server" DataField="PreferredVendorID" Enabled="False" AllowEdit="True" ></px:PXSegmentMask>
                                    <px:PXSegmentMask SuppressLabel="True" Size="s" ID="edSiteID2" runat="server" DataField="SiteID" AllowEdit="True" TextField="INSite__SiteCD" ></px:PXSegmentMask>
                                    <px:PXSegmentMask SuppressLabel="True" Size="s" ID="edInvtAcctID2" runat="server" DataField="InvtAcctID" ></px:PXSegmentMask>
                                    <px:PXSegmentMask SuppressLabel="True" Size="s" ID="edDfltShipLocationID2" runat="server" DataField="DfltShipLocationID" AutoRefresh="True" ></px:PXSegmentMask>
                                    <px:PXSegmentMask SuppressLabel="True" Size="xm" ID="edInvtSubID2" runat="server" DataField="InvtSubID" ></px:PXSegmentMask>
                                    <px:PXSegmentMask SuppressLabel="True" ID="edDfltReceiptLocationID2" runat="server" DataField="DfltReceiptLocationID" AutoRefresh="True" ></px:PXSegmentMask>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="IsDefault" TextAlign="Center" Type="CheckBox" Width="60px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="SiteID">
                                        <NavigateParams>
                                            <px:PXControlParam ControlID="grid2" Direction="Output" Name="SiteID" PropertyName="DataValues[&quot;SiteID&quot;]" Type="String" ></px:PXControlParam>
                                            <px:PXControlParam ControlID="grid2" Direction="Output" Name="InventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" ></px:PXControlParam>
                                        </NavigateParams>
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="DfltReceiptLocationID" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="DfltShipLocationID" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="SiteStatus" Type="DropDownList" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="InvtAcctID" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="InvtSubID" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ProductWorkgroupID" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ProductManagerID" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="StdCostOverride" TextAlign="Center" Type="CheckBox" Width="60px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="BasePriceOverride" TextAlign="Center" Type="CheckBox" Width="60px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="INSiteStatusSummary__QtyOnHand" TextAlign="Right" Width="100px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="PreferredVendorOverride" Label="Preferred Vendor Override" TextAlign="Center" Type="CheckBox" Width="60px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="PreferredVendorID" Width="81px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ReplenishmentPolicyOverride" Label="Replenishment Policy Override" TextAlign="Center" Type="CheckBox" Width="90px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ReplenishmentPolicyID" Label="Seasonality" Width="90px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ReplenishmentSource" Label="Replenishment Source" RenderEditorText="True" Width="90px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ReplenishmentSourceSiteID" Label="Replenishment Warehouse" Width="90px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ServiceLevelOverride" Label="Service Level Override" TextAlign="Center" Type="CheckBox" Width="60px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ServiceLevelPct" Label="Service Level" Width="90px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="LastForecastDate" Label="LastForecastDate" Width="140px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="DemandPerDayAverage" Label="Demand Per Day Forecast" Width="60px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="DemandPerDaySTDEV" Label="Demand Per Day Error (STDEV)" Width="80px" ></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" ></AutoSize>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Vendor Details" LoadOnDemand="true">
                <Template>
                    <px:PXGrid ID="PXGridVendorItems" runat="server" DataSourceID="ds" Height="100%" Width="100%" SkinID="DetailsInTab" SyncPosition="true">
                        <Mode InitNewRow="True" ></Mode>
                        <Levels>
                            <px:PXGridLevel DataMember="VendorItems" DataKeyNames="RecordID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" ></px:PXLayoutRule>
                                    <px:PXSegmentMask ID="edVendorID" runat="server" DataField="VendorID" AllowEdit="True" ></px:PXSegmentMask>
                                    <px:PXSegmentMask Size="xxs" ID="vp_edSubItemID" runat="server" DataField="SubItemID" AutoRefresh="True" ></px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edLocation__VSiteID" runat="server" DataField="Location__VSiteID" AllowEdit="true" ></px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edVendorLocationID" runat="server" DataField="VendorLocationID" AutoRefresh="True" AllowEdit="True" ></px:PXSegmentMask>
                                    <px:PXMaskEdit ID="edVendorInventoryID" runat="server" DataField="VendorInventoryID" ></px:PXMaskEdit>
                                    <px:PXNumberEdit ID="edAddLeadTimeDays" runat="server" DataField="AddLeadTimeDays" ></px:PXNumberEdit>
                                    <px:PXCheckBox ID="vp_chkActive" runat="server" Checked="True" DataField="Active" ></px:PXCheckBox>
                                    <px:PXNumberEdit ID="edMinOrdFreq" runat="server" DataField="MinOrdFreq" ></px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edMinOrdQty" runat="server" DataField="MinOrdQty" ></px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edMaxOrdQty" runat="server" DataField="MaxOrdQty" ></px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edLotSize" runat="server" DataField="LotSize" ></px:PXNumberEdit>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" ></px:PXLayoutRule>
                                    <px:PXNumberEdit ID="edERQ" runat="server" DataField="ERQ" ></px:PXNumberEdit>
                                    <px:PXSelector ID="edCuryID" runat="server" DataField="CuryID" ></px:PXSelector>
                                    <px:PXNumberEdit ID="edLastPrice" runat="server" DataField="LastPrice" Enabled="False" ></px:PXNumberEdit>
                                    <px:PXCheckBox ID="chkIsDefault" runat="server" DataField="IsDefault" ></px:PXCheckBox>
                                    <px:PXTextEdit ID="edVendor__AcctName" runat="server" DataField="Vendor__AcctName" ></px:PXTextEdit>
                                    <px:PXNumberEdit ID="edLocation__VLeadTime" runat="server" DataField="Location__VLeadTime" ></px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="Active" TextAlign="Center" Type="CheckBox" Width="45px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="IsDefault" TextAlign="Center" Type="CheckBox" Width="45px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="VendorID" Width="81px" AutoCallBack="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Vendor__AcctName" Width="210px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="VendorLocationID" Width="54px" AutoCallBack="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Location__VSiteID" Width="81px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="SubItemID" AutoCallBack="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="PurchaseUnit" Width="63px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="VendorInventoryID" Width="90px" AutoCallBack="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Location__VLeadTime" Width="90px" TextAlign="Right" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="OverrideSettings" TextAlign="Center" Type="CheckBox" Width="60px" AutoCallBack="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="AddLeadTimeDays" TextAlign="Right" Width="90px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="MinOrdFreq" TextAlign="Right" Width="84px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="MinOrdQty" TextAlign="Right" Width="81px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="MaxOrdQty" TextAlign="Right" Width="81px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="LotSize" TextAlign="Right" Width="81px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ERQ" TextAlign="Right" Width="81px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="CuryID" Width="54px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="LastPrice" TextAlign="Right" Width="99px" ></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" ></AutoSize>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Attributes">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" ></px:PXLayoutRule>
                    <px:PXGrid ID="PXGridAnswers" runat="server" Caption="Attributes" DataSourceID="ds" Height="150px" MatrixMode="True" Width="420px" SkinID="Attributes">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="AttributeID,EntityType,EntityID" DataMember="Answers">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="M" StartColumn="True" ></px:PXLayoutRule>
                                    <px:PXTextEdit ID="edParameterID" runat="server" DataField="AttributeID" Enabled="False" ></px:PXTextEdit>
                                    <px:PXTextEdit ID="edAnswerValue" runat="server" DataField="Value" ></px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn AllowShowHide="False" DataField="AttributeID" TextField="AttributeID_description" TextAlign="Left" Width="135px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="isRequired" TextAlign="Center" Type="CheckBox" Width="80px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Value" Width="185px" ></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                    <px:PXGrid ID="PXGridCategory" runat="server" Caption="Sales Categories" DataSourceID="ds" Height="220px" Width="250px"
                        SkinID="ShortList" MatrixMode="False">
                        <Levels>
                            <px:PXGridLevel DataMember="Category">
                                <RowTemplate>
                                    <px:PXLayoutRule ID="PXLayoutRule2" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" ></px:PXLayoutRule>
                                    <px:PXTreeSelector ID="edParent" runat="server" DataField="CategoryID" PopulateOnDemand="True"
                                        ShowRootNode="False" TreeDataSourceID="ds" TreeDataMember="Categories" CommitChanges="true">
                                        <DataBindings>
                                            <px:PXTreeItemBinding TextField="Description" ValueField="CategoryID" ></px:PXTreeItemBinding>
                                        </DataBindings>
                                    </px:PXTreeSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="CategoryID" Width="220px" TextField="INCategory__Description" AllowResize="False"></px:PXGridColumn>
                                </Columns>
                                <Layout FormViewHeight="" ></Layout>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" ></px:PXLayoutRule>
                    <px:PXImageUploader Height="320px" Width="430px" ID="imgUploader" runat="server" DataField="ImageUrl" AllowUpload="true" ShowComment="true" DataMember="ItemSettings"
						 ></px:PXImageUploader>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Packaging">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" ></px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Dimensions" ></px:PXLayoutRule>
                    <px:PXNumberEdit ID="edBaseItemWeight" runat="server" DataField="BaseItemWeight" ></px:PXNumberEdit>
                    <px:PXSelector ID="edWeightUOM" runat="server" DataField="WeightUOM" Size="S" AutoRefresh="true" ></px:PXSelector>
                    <px:PXNumberEdit ID="edBaseItemVolume" runat="server" DataField="BaseItemVolume" ></px:PXNumberEdit>
                    <px:PXSelector ID="edVolumeUOM" runat="server" DataField="VolumeUOM" Size="S" AutoRefresh="true" ></px:PXSelector>
                    <px:PXLayoutRule runat="server" StartColumn="True" ></px:PXLayoutRule>
                    <px:PXLayoutRule ID="PXLayoutRule5" runat="server" StartGroup="True" GroupCaption="Automatic Packaging" ></px:PXLayoutRule>
                    <px:PXLayoutRule ID="PXLayoutRule6" runat="server" Merge="True" ></px:PXLayoutRule>
                    <px:PXDropDown ID="edPackageOption" runat="server" DataField="PackageOption" CommitChanges="true" AllowNull="False" ></px:PXDropDown>
                    <px:PXCheckBox ID="edPackSeparately" DataField="PackSeparately" runat="server" ></px:PXCheckBox>
                    <px:PXLayoutRule ID="PXLayoutRule7" runat="server" Merge="False" ></px:PXLayoutRule>
                    <px:PXGrid ID="PXGridBoxes" runat="server" Caption="Boxes" DataSourceID="ds" Height="130px" Width="420px" SkinID="ShortList" FilesIndicator="False" NoteIndicator="false">
                        <Levels>
                            <px:PXGridLevel DataMember="Boxes">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" ></px:PXLayoutRule>
                                    <px:PXSelector ID="edBoxID" runat="server" DataField="BoxID" ></px:PXSelector>
                                    <px:PXSelector ID="edUOM_box" runat="server" DataField="UOM" ></px:PXSelector>
                                    <px:PXNumberEdit ID="edQty_box" runat="server" DataField="Qty" ></px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="BoxID" Width="91px" CommitChanges="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Description" Width="91px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="UOM" Width="54px" CommitChanges="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" TextAlign="Right" Width="54px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="MaxWeight" Width="54px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="MaxVolume" Width="54px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="MaxQty" TextAlign="Right" Width="54px" ></px:PXGridColumn>
                                </Columns>
                                <Layout FormViewHeight="" ></Layout>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Cross-Reference">
                <Template>
                    <px:PXGrid ID="crossgrid" runat="server" DataSourceID="ds" Height="150px" Width="100%" ActionsPosition="Top" SkinID="DetailsInTab" SyncPosition="true">
                        <Levels>
                            <px:PXGridLevel DataMember="itemxrefrecords" DataKeyNames="InventoryID,SubItemID,AlternateType,BAccountID,AlternateID">
                                <Columns>
                                    <px:PXGridColumn DataField="SubItemID" Width="135px" CommitChanges="true" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="AlternateType" Type="DropDownList" Width="135px" CommitChanges="true"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="BAccountID" Width="135px" CommitChanges="true"  ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="AlternateID" Width="180px" CommitChanges="true" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="UOM" Width="70px" CommitChanges="true" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="351px" ></px:PXGridColumn>
                                </Columns>
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" ></px:PXLayoutRule>
                                    <px:PXSegmentMask ID="edBAccountID" runat="server" DataField="BAccountID" AutoRefresh="True" AllowEdit="True" >
                                        <Parameters>
                                            <px:PXControlParam ControlID="crossgrid" Name="INItemXRef.alternateType" PropertyName="DataValues[&quot;AlternateType&quot;]" ></px:PXControlParam>
                                        </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXSelector ID="edxUOM" runat="server" Size="s" DataField="UOM" AllowEdit="True" AutoRefresh="True" ></px:PXSelector>
                                    <px:PXSegmentMask SuppressLabel="True" ID="edSubItemID" runat="server" DataField="SubItemID" AutoRefresh="True" ></px:PXSegmentMask>
                                    <px:PXTextEdit ID="edAlternateID" runat="server" DataField="AlternateID" ></px:PXTextEdit>
                                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" ></px:PXTextEdit>
                                </RowTemplate>
                                <Layout FormViewHeight="" ></Layout>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode InitNewRow="true" ></Mode>
                        <AutoSize Enabled="True" MinHeight="150" ></AutoSize>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Replenishment Info" RepaintOnDemand="False">
                <Template>
                    <px:PXSplitContainer runat="server" ID="sp1" SplitterPosition="300" SkinID="Horizontal" Height="500px">
                        <AutoSize Enabled="true" ></AutoSize>
                        <Template1>
                            <px:PXGrid ID="repGrid" runat="server" Height="250px" Width="100%" DataSourceID="ds" SkinID="DetailsInTab" Caption="Replenishment Parameters"
                                TabIndex="100">
                                <AutoCallBack Command="Refresh" Target="repSubGrid" ></AutoCallBack>
                                <Mode InitNewRow="True" ></Mode>
                                <Levels>
                                    <px:PXGridLevel DataMember="replenishment" DataKeyNames="InventoryID,ReplenishmentClassID">
                                        <RowTemplate>
                                            <px:PXLayoutRule ID="PXLayoutRule3" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" ></px:PXLayoutRule>
                                            <px:PXSelector ID="edReplenishmentClassID" runat="server" DataField="ReplenishmentClassID" AllowEdit="True" CommitChanges="true" ></px:PXSelector>
                                            <px:PXSelector ID="edReplenishmentPolicyID" runat="server" DataField="ReplenishmentPolicyID" AllowEdit="True" ></px:PXSelector>
                                            <px:PXDropDown ID="edReplenishmentSource" runat="server" DataField="ReplenishmentSource" ></px:PXDropDown>
                                            <px:PXDropDown ID="edReplenishmentMethod" runat="server" DataField="ReplenishmentMethod" ></px:PXDropDown>
                                            <px:PXSegmentMask ID="edReplenishmentSourceSiteID" runat="server" DataField="ReplenishmentSourceSiteID" CommitChanges="true" ></px:PXSegmentMask>
                                            <px:PXNumberEdit ID="edMaxShelfLife" runat="server" DataField="MaxShelfLife" ></px:PXNumberEdit>
                                            <px:PXDateTimeEdit ID="edLaunchDate" runat="server" DataField="LaunchDate" ></px:PXDateTimeEdit>
                                            <px:PXDateTimeEdit ID="edTerminationDate" runat="server" DataField="TerminationDate" ></px:PXDateTimeEdit>
                                            <px:PXNumberEdit ID="edServiceLevelPct" runat="server" DataField="ServiceLevelPct" ></px:PXNumberEdit>
                                            <px:PXNumberEdit ID="edSafetyStock" runat="server" DataField="SafetyStock" ></px:PXNumberEdit>
                                            <px:PXNumberEdit ID="edMinQty" runat="server" DataField="MinQty" ></px:PXNumberEdit>
                                            <px:PXNumberEdit ID="edMaxQty" runat="server" DataField="MaxQty" ></px:PXNumberEdit>
                                            <px:PXNumberEdit ID="edTransferERQ" runat="server" DataField="TransferERQ" ></px:PXNumberEdit>
                                            <px:PXNumberEdit ID="edHistoryDepth" runat="server" AllowNull="true" Size="xxs" DataField="HistoryDepth" ></px:PXNumberEdit>
                                        </RowTemplate>
                                        <Columns>
                                            <px:PXGridColumn DataField="ReplenishmentClassID" AutoCallBack="True" Width="90px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="ReplenishmentPolicyID" Width="100px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="ReplenishmentSource" Type="DropDownList" AutoCallBack="True" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="ReplenishmentMethod" Type="DropDownList" CommitChanges="true" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="ReplenishmentSourceSiteID" Width="140px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="MaxShelfLife" TextAlign="Right" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="LaunchDate" Width="90px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="TerminationDate" Width="90px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="ServiceLevelPct" Width="90px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="SafetyStock" TextAlign="Right" Width="80px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="MinQty" TextAlign="Right" Width="80px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="MaxQty" TextAlign="Right" Width="80px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="TransferERQ" TextAlign="Right" Width="80px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="ForecastModelType" Label="Forecast Model Type" Width="140px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="ForecastPeriodType" Label="Forecast Period Type" Width="50px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="HistoryDepth" Label="History Scan Depth" Width="50px" ></px:PXGridColumn>
                                        </Columns>
                                        <Layout FormViewHeight="" ></Layout>
                                    </px:PXGridLevel>
                                </Levels>
                                <AutoSize Enabled="True" ></AutoSize>
                            </px:PXGrid>
                        </Template1>
                        <Template2>
                            <px:PXGrid ID="repSubGrid" runat="server" Height="200px" Width="100%" DataSourceID="ds" SkinID="DetailsInTab" Caption="Subitem Replenishment Parameters"
                                Style="left: 0px; top: 0px" TabIndex="150">
                                <Mode InitNewRow="True" ></Mode>
                                <CallbackCommands>
                                    <Save CommitChangesIDs="repSubGrid" RepaintControls="None" ></Save>
                                    <FetchRow RepaintControls="None" ></FetchRow>
                                </CallbackCommands>
                                <Parameters>
                                    <px:PXSyncGridParam ControlID="repGrid" ></px:PXSyncGridParam>
                                </Parameters>
                                <ActionBar>
                                    <CustomItems>
                                        <px:PXToolBarButton>
                                            <AutoCallBack Command="GenerateSubitems" Target="ds" ></AutoCallBack>
                                        </px:PXToolBarButton>
                                        <px:PXToolBarButton>
                                            <AutoCallBack Command="UpdateReplenishment" Target="ds" ></AutoCallBack>
                                        </px:PXToolBarButton>
                                    </CustomItems>
                                </ActionBar>
                                <Levels>
                                    <px:PXGridLevel DataMember="subreplenishment" DataKeyNames="InventoryID,ReplenishmentClassID,SubItemID">
                                        <RowTemplate>
                                            <px:PXSegmentMask ID="edRPSubItemID" runat="server" DataField="SubItemID" Width="27px" AutoRefresh="True" ></px:PXSegmentMask>
                                        </RowTemplate>
                                        <Columns>
                                            <px:PXGridColumn DataField="InventoryID" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="ReplenishmentClassID" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="SubItemID" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="SafetyStock" TextAlign="Right" Width="100px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="MinQty" TextAlign="Right" Width="100px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="MaxQty" TextAlign="Right" Width="100px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="TransferERQ" TextAlign="Right" Width="80px" ></px:PXGridColumn>
                                            <px:PXGridColumn DataField="ItemStatus" Type="DropDownList" ></px:PXGridColumn>
                                        </Columns>
                                        <Layout FormViewHeight="" ></Layout>
                                    </px:PXGridLevel>
                                </Levels>
                                <AutoSize Enabled="True" MinHeight="150" ></AutoSize>
                            </px:PXGrid>
                        </Template2>
                    </px:PXSplitContainer>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Deferral Settings">
                <Template>
                    <px:PXFormView ID="formDR" runat="server" Width="100%" DataMember="ItemSettings" DataSourceID="ds" Caption="Rules" SkinID="Transparent">
                        <Activity HighlightColor="" SelectedColor="" ></Activity>
                        <Template>
                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" ></px:PXLayoutRule>
                            <px:PXSelector CommitChanges="True" ID="edDeferredCode1" runat="server" DataField="DeferredCode" AllowEdit="True" DataSourceID="ds" ></px:PXSelector>
							<px:PXLayoutRule runat="server" Merge="true" ></px:PXLayoutRule>
							<px:PXNumberEdit runat="server" ID="edDefaultTerm" DataField="DefaultTerm" CommitChanges="true" ></px:PXNumberEdit>
							<px:PXDropDown runat="server" ID="edDefaultTermUOM" DataField="DefaultTermUOM" CommitChanges="true" Width="134px" SuppressLabel="true" ></px:PXDropDown>
							<px:PXLayoutRule runat="server" ></px:PXLayoutRule>
                            <px:PXCheckBox ID="chkUseParentSubID" runat="server" DataField="UseParentSubID" ></px:PXCheckBox>
                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" ></px:PXLayoutRule>
                            <px:PXNumberEdit ID="edTotalPercentage" runat="server" DataField="TotalPercentage" Enabled="false" ></px:PXNumberEdit>
                        </Template>
                    </px:PXFormView>
                    <px:PXGrid ID="PXGridComponents" runat="server" DataSourceID="ds" AllowFilter="False" Height="200px" Width="100%" Caption="Revenue Components" SkinID="DetailsWithFilter" SyncPosition="true">
                        <Mode InitNewRow="True" ></Mode>
                        <Levels>
                            <px:PXGridLevel DataMember="Components" DataKeyNames="InventoryID,ComponentID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" ></px:PXLayoutRule>
                                    <px:PXDropDown ID="edPriceOption" runat="server" DataField="AmtOption" CommitChanges="true" ></px:PXDropDown>
                                    <px:PXSegmentMask ID="edComponentID" runat="server" DataField="ComponentID" AllowEdit="True" ></px:PXSegmentMask>
                                    <px:PXNumberEdit Size="xs" ID="edFixedAmt" runat="server" DataField="FixedAmt" ></px:PXNumberEdit>
                                    <px:PXSelector ID="edDeferredCode" runat="server" DataField="DeferredCode" CommitChanges="true" AllowEdit="True" ></px:PXSelector>
									<px:PXNumberEdit runat="server" ID="edDefaultTerm" DataField="DefaultTerm" CommitChanges="true" ></px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edPercentage" runat="server" DataField="Percentage" ></px:PXNumberEdit>
                                    <px:PXSegmentMask ID="edSalesAcctID" runat="server" DataField="SalesAcctID" AllowEdit="True" ></px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edSalesSubID" runat="server" DataField="SalesSubID" ></px:PXSegmentMask>
                                    <px:PXSelector ID="edUOM" runat="server" DataField="UOM" AllowEdit="True" AutoRefresh="true" ></px:PXSelector>
                                    <px:PXNumberEdit ID="edQty" runat="server" DataField="Qty" ></px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn AutoCallBack="True" DataField="ComponentID" Width="99px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="SalesAcctID" Width="99px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="SalesSubID" Width="99px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="UOM" Width="99px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" TextAlign="Right" Width="99px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="DeferredCode" Width="99px" CommitChanges="true" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="DefaultTerm" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="DefaultTermUOM" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="AmtOption" RenderEditorText="True" Width="81px" CommitChanges="true" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="FixedAmt" TextAlign="Right" Width="81px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Percentage" TextAlign="Right" Width="99px" ></px:PXGridColumn>
                                </Columns>
                                <Layout FormViewHeight="" ></Layout>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" ></AutoSize>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="GL Accounts">
                <Template>
                    <px:PXLayoutRule ID="PXLayoutRule4" runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" ></px:PXLayoutRule>
                    <px:PXSegmentMask ID="edInvtAcctID" runat="server" DataField="InvtAcctID" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edInvtSubID" runat="server" DataField="InvtSubID" AutoRefresh="True" CommitChanges="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edReasonCodeSubID" runat="server" DataField="ReasonCodeSubID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edSalesAcctID" runat="server" DataField="SalesAcctID" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edSalesSubID" runat="server" DataField="SalesSubID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edCOGSAcctID" runat="server" DataField="COGSAcctID" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edCOGSSubID" runat="server" DataField="COGSSubID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edStdCstVarAcctID" runat="server" DataField="StdCstVarAcctID" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edStdCstVarSubID" runat="server" DataField="StdCstVarSubID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edStdCstRevAcctID" runat="server" DataField="StdCstRevAcctID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edStdCstRevSubID" runat="server" DataField="StdCstRevSubID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edPOAccrualAcctID" runat="server" DataField="POAccrualAcctID" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edPOAccrualSubID" runat="server" DataField="POAccrualSubID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edPPVAcctID" runat="server" DataField="PPVAcctID" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edPPVSubID" runat="server" DataField="PPVSubID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edLCVarianceAcctID" runat="server" DataField="LCVarianceAcctID" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edLCVarianceSubID" runat="server" DataField="LCVarianceSubID" AutoRefresh="True" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edDiscAcctID" runat="server" DataField="DiscAcctID" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edDiscSubID" runat="server" DataField="DiscSubID" AutoRefresh="True" ></px:PXSegmentMask>
					<px:PXSegmentMask ID="edDeferralAcctID" runat="server" DataField="DeferralAcctID" CommitChanges="true" ></px:PXSegmentMask>
                    <px:PXSegmentMask ID="edDeferralSubID" runat="server" DataField="DeferralSubID" AutoRefresh="True" ></px:PXSegmentMask>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Restriction Groups">
                <Template>
                    <px:PXGrid ID="grid3" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100" Width="100%" AdjustPageSize="Auto" AllowSearch="True" SkinID="Details" BorderWidth="0px">
                        <ActionBar>
                            <Actions>
                                <NoteShow Enabled="False" ></NoteShow>
                            </Actions>
                            <CustomItems>
                                <px:PXToolBarButton Text="Group Details" CommandSourceID="ds" CommandName="ViewGroupDetails" ></px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <Levels>
                            <px:PXGridLevel DataMember="Groups" DataKeyNames="GroupName">
                                <Mode AllowAddNew="False" AllowDelete="False" ></Mode>
                                <Columns>
                                    <px:PXGridColumn DataField="Included" TextAlign="Center" Type="CheckBox" Width="40px" AllowCheckAll="True" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="GroupName" Width="150px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="SpecificType" Width="150px" Type="DropDownList" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Description" Width="200px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Active" TextAlign="Center" Type="CheckBox" Width="60px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="GroupType" Label="Visible To Entities" Width="171px" Type="DropDownList" ></px:PXGridColumn>
                                </Columns>
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" ></px:PXLayoutRule>
                                    <px:PXCheckBox SuppressLabel="True" ID="chkSelected" runat="server" DataField="Included" ></px:PXCheckBox>
                                    <px:PXSelector ID="edGroupName" runat="server" DataField="GroupName" ></px:PXSelector>
                                    <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" ></px:PXTextEdit>
                                    <px:PXCheckBox SuppressLabel="True" ID="chkActive" runat="server" Checked="True" DataField="Active" ></px:PXCheckBox>
                                    <px:PXDropDown ID="edGroupType" runat="server" DataField="GroupType" Enabled="False" ></px:PXDropDown>
                                </RowTemplate>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" ></AutoSize>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>


            <px:PXTabItem Text="Description" LoadOnDemand="true" >
                <Template>
                    <px:PXRichTextEdit ID="edBody" runat="server" DataField="Body" Style="border-width: 0px; border-top-width: 1px; width: 100%;"
                        AllowAttached="true" AllowSearch="true" AllowLoadTemplate="false" AllowSourceMode="true">
                        <AutoSize Enabled="True" MinHeight="216" ></AutoSize>
                        <LoadTemplate TypeName="PX.SM.SMNotificationMaint" DataMember="Notifications" ViewName="NotificationTemplate" ValueField="notificationID" TextField="Name" DataSourceID="ds" Size="M"></LoadTemplate>
                    </px:PXRichTextEdit>
                </Template>
            </px:PXTabItem>
  			<px:PXTabItem Text="Service Management" VisibleExp="DataControls[&quot;chkEquipmentManagement&quot;].Value == 1" BindingContext="form">
				<Template>
					<px:PXFormView runat="server" ID="formSM" DataMember="ItemSettings" Style="z-index: 100" Width="100%">
                        <ContentStyle BackColor="Transparent" BorderStyle="None" >
                        </ContentStyle>
						<Template>
							<px:PXLayoutRule runat="server" StartGroup="True" ></px:PXLayoutRule>
							<px:PXGroupBox runat="server" ID="edEquipmentItemClass" Caption="Equipment Item Class" DataField="EquipmentItemClass" CommitChanges="True">
								<Template>
									<px:PXRadioButton runat="server" ID="edEquipmentItemClass_op0" Value="OI" Text="Part or Other Inventory" ></px:PXRadioButton>
									<px:PXRadioButton runat="server" ID="edEquipmentItemClass_op1" Value="ME" Text="Model Equipment" ></px:PXRadioButton>
									<px:PXRadioButton runat="server" ID="edEquipmentItemClass_op2" Text="Component" Value="CT" ></px:PXRadioButton>
									<px:PXRadioButton runat="server" ID="edEquipmentItemClass_op3" Text="Consumable" Value="CE" ></px:PXRadioButton>
								</Template>
							</px:PXGroupBox>
							<px:PXSelector runat="server" ID="edManufacturerID" DataField="ManufacturerID" CommitChanges="True" AllowEdit="True" ></px:PXSelector>
							<px:PXSelector runat="server" ID="edManufacturerModelID" DataField="ManufacturerModelID" AllowEdit="True" CommitChanges="True" AutoRefresh="True" ></px:PXSelector>
							<px:PXCheckBox runat="server" ID="edMem_ShowComponent" DataField="Mem_ShowComponent" ></px:PXCheckBox>
							<px:PXLayoutRule runat="server" StartColumn="True" ></px:PXLayoutRule>
							<px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="EQUIPMENT GENERAL WARRANTY" ></px:PXLayoutRule>
							<px:PXLayoutRule runat="server" Merge="True" ></px:PXLayoutRule>
							<px:PXNumberEdit runat="server" ID="edCpnyWarrantyValue" DataField="CpnyWarrantyValue" Width="30px" ></px:PXNumberEdit>
							<px:PXDropDown runat="server" ID="edCpnyWarrantyType" DataField="CpnyWarrantyType" SuppressLabel="True" Size="S" ></px:PXDropDown>
							<px:PXLayoutRule runat="server" Merge="True" ></px:PXLayoutRule>
							<px:PXNumberEdit runat="server" ID="edVendorWarrantyValue" DataField="VendorWarrantyValue" Width="30px" ></px:PXNumberEdit>
							<px:PXDropDown runat="server" ID="edVendorWarrantyType" DataField="VendorWarrantyType" SuppressLabel="True" Size="S" ></px:PXDropDown>
						</Template>
					</px:PXFormView>
					<px:PXGrid runat="server" ID="gridComponents" SkinID="DetailsInTab" AdjustPageSize="Auto" Width="100%" AutoGenerateColumns="None" SyncPosition="True">
						<Levels>
							<px:PXGridLevel DataMember="ModelComponents">
								<Columns>
									<px:PXGridColumn DataField="ComponentID" Width="150px" CommitChanges="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Active" Width="70px" Type="CheckBox" TextAlign="Center" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Optional" Width="70" Type="CheckBox" TextAlign="Center" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Qty" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Descr" Width="250px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ClassID" Width="120px" CommitChanges="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="InventoryID" Width="120px" CommitChanges="True"></px:PXGridColumn>
									<px:PXGridColumn DataField="RequireSerial" Width="70px" Type="CheckBox" TextAlign="Center" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CpnyWarrantyValue" Width="80px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CpnyWarrantyType" Width="80px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="VendorWarrantyValue" Width="140px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="VendorWarrantyType" Width="70px" ></px:PXGridColumn>
									<px:PXGridColumn DataField="VendorID" Width="120px" ></px:PXGridColumn>
								</Columns>
                                <RowTemplate>
                                    <px:PXSelector ID="edEQComponentID" runat="server" DataField="ComponentID" CommitChanges="True" AutoRefresh="True"></px:PXSelector>
                                    <px:PXCheckBox ID="edEQActive" runat="server" DataField="Active"></px:PXCheckBox>
                                    <px:PXCheckBox ID="edEQOptional" runat="server" DataField="Optional"></px:PXCheckBox>
                                    <px:PXNumberEdit ID="edEQQty" runat="server" DataField="Qty" ></px:PXNumberEdit>
                                    <px:PXTextEdit ID="edEQDescr" runat="server" DataField="Descr" ></px:PXTextEdit>
                                    <px:PXSelector ID="edEQClassID" runat="server" DataField="ClassID" CommitChanges="True" AutoRefresh="True" AllowEdit="True"></px:PXSelector>
                                    <px:PXSelector ID="edEQInventoryID" runat="server" DataField="InventoryID" CommitChanges="True" AutoRefresh="True" AllowEdit="True"></px:PXSelector>
                                    <px:PXCheckBox ID="edEQRequireSerial" runat="server" DataField="RequireSerial"></px:PXCheckBox>
                                    <px:PXNumberEdit ID="edEQCpnyWarrantyValue" runat="server" DataField="CpnyWarrantyValue" ></px:PXNumberEdit>
                                    <px:PXDropDown ID="edEQCpnyWarrantyType" runat="server" DataField="CpnyWarrantyType"></px:PXDropDown>
                                    <px:PXNumberEdit ID="edEQVendorWarrantyValue" runat="server" DataField="VendorWarrantyValue" ></px:PXNumberEdit>
                                    <px:PXDropDown ID="edEQVendorWarrantyType" runat="server" DataField="VendorWarrantyType"></px:PXDropDown>
                                    <px:PXSelector ID="edEQVendorID" runat="server" DataField="VendorID" CommitChanges="True" AutoRefresh="True"></px:PXSelector>
                                </RowTemplate>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" ></AutoSize>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
            <px:PXTabItem Text="Sync Status">
                <Template>
                    <px:PXGrid ID="syncGrid" runat="server" DataSourceID="ds" Height="150px" Width="100%" ActionsPosition="Top" SkinID="Inquire" SyncPosition="true">
                        <Levels>
                            <px:PXGridLevel DataMember="SyncRecs" DataKeyNames="SyncRecordID">
                                <Columns>
                                    <px:PXGridColumn DataField="SYProvider__Name" Width="200px" ></px:PXGridColumn>                                    
                                    <px:PXGridColumn DataField="RemoteID" Width="200px" CommitChanges="True" LinkCommand="GoToSalesforce" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status" Width="120px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Operation" Width="100px" ></px:PXGridColumn>      
                                    <px:PXGridColumn DataField="LastErrorMessage" Width="230" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="LastAttemptTS" Width="120px" DisplayFormat="g" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="AttemptCount" Width="120px" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="SFEntitySetup__ImportScenario" Width="150" ></px:PXGridColumn>
                                    <px:PXGridColumn DataField="SFEntitySetup__ExportScenario" Width="150" ></px:PXGridColumn>
                                </Columns>                               
                                <Layout FormViewHeight="" ></Layout>
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar>                        
                            <CustomItems>
                                <px:PXToolBarButton Key="SyncSalesforce">
                                    <AutoCallBack Command="SyncSalesforce" Target="ds"></AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <Mode InitNewRow="true" ></Mode>
                        <AutoSize Enabled="True" MinHeight="150" ></AutoSize>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
	<px:PXTabItem Text="Advanced Billing Bundle">
		<Template>
			<px:PXGrid runat="server" ID="VirtualContainer14" SkinID="DetailsInTab" Width="100%" Style='height:120px;'>
				<Levels>
					<px:PXGridLevel DataMember="BundleComponents">
						<Columns>
							<px:PXGridColumn DataField="InventoryID" Width="150px" CommitChanges="True" />
							<px:PXGridColumn DataField="Qty" Width="100px" />
							<px:PXGridColumn DataField="PriceOption" Width="150px" CommitChanges="True" />
							<px:PXGridColumn DataField="UnitPrice" Width="100px" />
							<px:PXGridColumn DataField="PricePct" Width="200px" />
							<px:PXGridColumn DataField="CompType" Width="200px" /></Columns>
						<Mode AllowUpdate="True" AllowDelete="True" AllowAddNew="True" /></px:PXGridLevel></Levels>
				<AutoSize Enabled="True" /></px:PXGrid></Template></px:PXTabItem></Items>
        <AutoSize Enabled="True" MinHeight="150" ></AutoSize>
    </px:PXTab>
    <px:PXSmartPanel ID="pnlUpdatePrice" runat="server" Key="VendorItems" CaptionVisible="true" DesignView="Content" Caption="Update Effective Vendor Prices" AllowResize="false">
        <px:PXFormView ID="formEffectiveDate" runat="server" DataSourceID="ds" CaptionVisible="false" DataMember="VendorInventory$UpdatePrice" Width="280px" Height="50px" SkinID="Transparent">
            <Template>
                <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
                <px:PXDateTimeEdit ID="edPendingDate" runat="server" DataField="PendingDate" />
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="PXPanelBtn" runat="server" SkinID="Buttons">
            <px:PXButton ID="PXButton3" runat="server" DialogResult="OK" Text="Update" />
            <px:PXButton ID="PXButton4" runat="server" DialogResult="No" Text="Cancel" />
        </px:PXPanel>
    </px:PXSmartPanel>
</asp:Content>
