<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" 
    ValidateRequest="false" CodeFile="FS501000.aspx.cs" Title="Untitled Page" Inherits="Page_FS501000" %>
    <%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

        <asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
            <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter" TypeName="PX.Objects.FS.CreatePurchaseOrderProcess">
            </px:PXDataSource>
        </asp:Content>
        <asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
            <px:PXFormView ID="form" runat="server" DataSourceID="ds" 
                Style="z-index: 100" Width="100%" DataMember="Filter" TabIndex="700" 
                DefaultControlID="">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXDateTimeEdit runat="server" DataField="UpToDate" ID="edUpToDate" CommitChanges="True">
                    </px:PXDateTimeEdit>
                    <px:PXSelector ID="edItemClassID" runat="server" DataField="ItemClassID" CommitChanges="True">
                    </px:PXSelector>
                    <px:PXSelector ID="edInventoryID" runat="server" DataField="InventoryID" CommitChanges="True" AutoRefresh="True">
                    </px:PXSelector>
                    <px:PXSelector ID="edSiteID" runat="server" DataField="SiteID" CommitChanges="True">
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXSegmentMask ID="edPOVendorID" runat="server" DataField="POVendorID" CommitChanges="True">
                    </px:PXSegmentMask>
                    <px:PXSegmentMask ID="edCustomerID" runat="server" DataField="CustomerID" CommitChanges="True">
                    </px:PXSegmentMask>
                    <px:PXSelector ID="edSrvOrdType" runat="server" DataField="SrvOrdType" CommitChanges="True">
                    </px:PXSelector>
                    <px:PXSelector ID="edSORefNbr" runat="server" DataField="SORefNbr" CommitChanges="True" AutoRefresh="True">
                    </px:PXSelector>
                </Template>
            </px:PXFormView>
        </asp:Content>
        <asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
            <px:PXGrid ID="gridInventoryLines" runat="server" AllowPaging="True" DataSourceID="ds" 
                Style="z-index: 100" Width="100%"
                SkinID="PrimaryInquire" TabIndex="500" SyncPosition="True" BatchUpdate="True">
                <Levels>
                    <px:PXGridLevel DataMember="LinesToPO">
                        <RowTemplate>
                            <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected" 
                                Text="Selected">
                            </px:PXCheckBox>
                            <px:PXSelector ID="edBranchID" runat="server" AllowEdit="True" 
                                DataField="BranchID">
                            </px:PXSelector>
                            <px:PXSelector ID="edSrvBranchLocationID" runat="server" AllowEdit="True" DataField="SrvBranchLocationID" DataSourceID="ds">
                            </px:PXSelector>
                            <px:PXTextEdit ID="edLineType" runat="server" DataField="LineType">
                            </px:PXTextEdit>
                            <px:PXSegmentMask ID="ServiceID" runat="server" DataField="ServiceID" AllowEdit="True">
                            </px:PXSegmentMask>
                            <px:PXSelector ID="edInventoryItemClassID" runat="server" AllowEdit="True" DataField="InventoryItemClassID">
                            </px:PXSelector>
                            <px:PXSegmentMask ID="InventoryID2" runat="server" DataField="InventoryID" AllowEdit="True">
                            </px:PXSegmentMask>
                            <px:PXSegmentMask ID="edSiteID2" runat="server" DataField="SiteID" AllowEdit="True">
                                <Parameters>
                                    <px:PXControlParam ControlID="PXGrid2" Name="FSSODet.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" ></px:PXControlParam>
                                    <px:PXControlParam ControlID="PXGrid2" Name="FSSODet.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" ></px:PXControlParam>
                                    </Parameters>
                            </px:PXSegmentMask>
                            <px:PXNumberEdit ID="Qty2" runat="server" DataField="Qty">
                            </px:PXNumberEdit>
                            <px:PXDateTimeEdit runat="server" DataField="OrderDate" ID="edOrderDate" DataSourceID="ds">
                            </px:PXDateTimeEdit>
                            <px:PXSegmentMask ID="edVendorID" runat="server" DataField="POVendorID" Enabled="False" AutoRefresh="true">
                            </px:PXSegmentMask>
                            <px:PXSegmentMask ID="edPOVendorLocationID" runat="server" DataField="POVendorLocationID" AutoRefresh="true">
                            </px:PXSegmentMask>
                            <px:PXSegmentMask ID="edSrvCustomerID" runat="server" AllowEdit="True"
                                DataField="SrvCustomerID" DataSourceID="ds">
                            </px:PXSegmentMask>
                            <px:PXSegmentMask ID="edSrvLocationID" runat="server" AllowEdit="True"
                            DataField="SrvLocationID" DataSourceID="ds">
                            </px:PXSegmentMask>
                            <px:PXSelector ID="edSrvOrdType" runat="server" AllowEdit="True"
                                DataField="SrvOrdType" DataSourceID="ds">
                            </px:PXSelector>
                            <px:PXSelector ID="edSORefNbr" runat="server" DataField="SORefNbr" AllowEdit="True">
                            </px:PXSelector>
                            <px:PXSelector ID="edPONbrCreated" runat="server" DataField="PONbrCreated" AllowEdit="True" AutoRefresh="True">
                            </px:PXSelector>
                        </RowTemplate>
                        <Columns>
                            <px:PXGridColumn AllowCheckAll="True" DataField="Selected" 
                                TextAlign="Center" Type="CheckBox" Width="40px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="BranchID">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="SrvBranchLocationID">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="LineType">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="ServiceID" Width="150px" DisplayMode="Hint">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="InventoryItemClassID" Width="100px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="InventoryID" Width="150px" DisplayMode="Hint">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="SiteID" Width="150px" DisplayMode="Hint">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="Qty" TextAlign="Right" Width="75px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="OrderDate" Width="100px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="POVendorID" Width="100px" DisplayMode="Hint" CommitChanges="True">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="POVendorLocationID" Width="100px" CommitChanges="True">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="SrvCustomerID" Width="200px" DisplayMode="Hint">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="SrvLocationID" Width="100px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="SrvOrdType" Width="100px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="SORefNbr" Width="100px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="PONbrCreated" Width="100px">
                            </px:PXGridColumn>
                        </Columns>
                    </px:PXGridLevel>
                </Levels>
                <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                <ActionBar DefaultAction="viewDocument"></ActionBar>
            </px:PXGrid>
        </asp:Content>