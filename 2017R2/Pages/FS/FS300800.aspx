<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="FS300800.aspx.cs" Inherits="Page_FS300800" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="ServiceContractRecords" 
        TypeName="PX.Objects.FS.RouteServiceContractEntry" PageLoadBehavior="InsertRecord"
        SuspendUnloading="False">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True"  />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="AddSchedule" Visible="False" CommitChanges="True"></px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="OpenRouteScheduleScreen" Visible="False"></px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="OpenRouteScheduleScreenByScheduleDetService" Visible="False"></px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="OpenRouteScheduleScreenByScheduleDetPart" Visible="False"></px:PXDSCallbackCommand>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100"
		Width="100%" Height="190px" Caption="Service Contract" NotifyIndicator="True" FilesIndicator="True"
        DataMember="ServiceContractRecords" TabIndex="1300" DefaultControlID="edCustomerID" AllowColapse="True">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" ControlSize = "M" 
                StartRow="True" LabelsWidth="SM">
            </px:PXLayoutRule>
            <px:PXSegmentMask ID="edCustomerID" runat="server" DataField="CustomerID" 
                AllowEdit="True" CommitChanges="True">
            </px:PXSegmentMask>
            <px:PXSelector ID="edRefNbr" runat="server" DataField="RefNbr">
            </px:PXSelector>
            <px:PXSegmentMask ID="edCustomerLocationID" runat="server" 
                DataField="CustomerLocationID" CommitChanges="True">
            </px:PXSegmentMask>
            <px:PXTextEdit ID="edDocDesc" runat="server" DataField="DocDesc">
            </px:PXTextEdit>
            <px:PXDateTimeEdit ID="edStartDate" runat="server" 
                DataField="StartDate" CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXLayoutRule runat="server" Merge="True"></px:PXLayoutRule>
            <px:PXDateTimeEdit ID="edEndDate" runat="server" DataField="EndDate">
            </px:PXDateTimeEdit>
            <px:PXCheckBox ID="edEnableExpirationDate" runat="server" CommitChanges="True" 
                DataField="EnableExpirationDate">
            </px:PXCheckBox>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXDropDown ID="edStatus" runat="server" DataField="Status">
            </px:PXDropDown>
            <px:PXSelector ID="edBranchID" runat="server" DataField="BranchID" CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector ID="edBranchLocationID" runat="server" AllowEdit="True" 
                DataField="BranchLocationID" AutoRefresh="True" CommitChanges="True">
            </px:PXSelector>
            <px:PXDropDown ID="edScheduleGenType" runat="server" DataField="ScheduleGenType">
            </px:PXDropDown>
            <px:PXSelector ID="edMasterContractID" runat="server" AutoRefresh="true"
                DataField="MasterContractID">
            </px:PXSelector>
            <px:PXSegmentMask ID="edVendorID" runat="server" DataField="VendorID" 
                AllowEdit="True">
            </px:PXSegmentMask>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXLayoutRule runat="server" GroupCaption="Commission" StartGroup="True" ControlSize="SM" LabelsWidth="S">
            </px:PXLayoutRule>
            <px:PXSegmentMask ID="edSalesPersonID" runat="server" DataField="SalesPersonID" CommitChanges="True"></px:PXSegmentMask>
            <px:PXCheckBox ID="edCommissionable" runat="server" CommitChanges="True" DataField="Commissionable"></px:PXCheckBox>
            <px:PXLayoutRule runat="server" GroupCaption="Prices Settings" StartGroup="True">
            </px:PXLayoutRule>
            <px:PXGroupBox ID="edSourcePrice" runat="server" Caption="Billing Settings" 
                CommitChanges="True" DataField="SourcePrice">
                <Template>
                    <px:PXRadioButton ID="edSourcePrice_op1" runat="server" 
                        GroupName="edSourcePrice" Text="Price List" Value="P" />
                    <px:PXRadioButton ID="edSourcePrice_op2" runat="server" 
                        GroupName="edSourcePrice" Text="Contract" Value="C" />
                </Template>
                <Contentlayout layout="Stack" outerspacing="Horizontal" />
            </px:PXGroupBox>
        </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="100%" DataSourceID="ds" 
        DataMember="ContractSchedules" Style="z-index: 100">
		<Items>
            <px:PXTabItem Text="Schedules">
                <Template>
                    <px:PXGrid ID="PXGridSchedule" runat="server" AutoAdjustColumns="True" 
                        DataSourceID="ds" Height="100%" SkinID="Inquire" TabIndex="4200" Width="100%" KeepPosition="True"  SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="RefNbr" DataMember="ContractSchedules">
                                <RowTemplate>
                                    <px:PXSelector ID="edRefNbrSchedule" runat="server" AllowEdit="True" DataField="RefNbr" >
                                    </px:PXSelector>
                                    <px:PXSelector ID="edSrvOrdType" runat="server" AllowEdit="True" 
                                        DataField="SrvOrdType" >
                                    </px:PXSelector>
                                    <px:PXSelector ID="edCustomerLocationID" runat="server" AllowEdit="True" 
                                        DataField="CustomerLocationID" >
                                    </px:PXSelector>
                                    <px:PXCheckBox ID="edActiveSchedule" runat="server" DataField="Active" Text="Active">
                                    </px:PXCheckBox>
                                    <px:PXTextEdit ID="edRecurrenceDescription" runat="server" DataField="RecurrenceDescription">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="RefNbr" LinkCommand = "OpenRouteScheduleScreen">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SrvOrdType">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CustomerLocationID">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Active" TextAlign="Center" Type="CheckBox" 
                                        Width="60px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="RecurrenceDescription" Width="130px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <ActionBar ActionsText="False" PagerVisible="False">
                            <CustomItems>
                                <px:PXToolBarButton Text="Add Schedule">
                                    <AutoCallBack Command="AddSchedule" Target="ds" ></AutoCallBack>
                                    <PopupCommand Command="Refresh" Target="PXGridSchedule" ></PopupCommand>                                                                        
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Services">
                <Template>
                    <px:PXGrid ID="PXGridScheduleServices" runat="server" AutoAdjustColumns="True" 
                        DataSourceID="ds" Height="100%" SkinID="Inquire" TabIndex="4200" KeepPosition="True"  SyncPosition="True" 
                        Width="100%" NoteIndicator="False" FilesIndicator="False">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="FSSchedule__RefNbr" 
                                DataMember="ScheduleDetServicesByContract">
                                <RowTemplate>
                                    <px:PXSelector ID="edFSSchedule__RefNbr" runat="server" AllowEdit="True" DataField="FSSchedule__RefNbr" >
                                    </px:PXSelector>
                                    <px:PXDropDown ID="edLineType" runat="server" DataField="LineType">
                                    </px:PXDropDown>
                                    <px:PXSegmentMask ID="edInventoryID" runat="server" AllowEdit="True" DataField="InventoryID">
                                    </px:PXSegmentMask>
                                    <px:PXNumberEdit ID="edQty" runat="server" DataField="Qty">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edSMEquipmentID" runat="server" AllowEdit="True" DataField="SMEquipmentID" >
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edTranDesc" runat="server" DataField="TranDesc">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="FSSchedule__RefNbr" LinkCommand="OpenRouteScheduleScreenByScheduleDetService">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LineType">
                                    </px:PXGridColumn>                                    
                                    <px:PXGridColumn DataField="InventoryID">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SMEquipmentID" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TranDesc" Width="200px"> 
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <Mode AllowAddNew="False" AllowDelete="False"/>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Service Prices">
                <Template>
                    <px:PXGrid ID="PXGridSalesPricesServices" runat="server" DataSourceID="ds" 
                        Height="100%" SkinID="Inquire" TabIndex="4200" AutoAdjustColumns="True"
                        Width="100%" NoteIndicator="False" FilesIndicator="False">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="SalesPriceID,InventoryItem__InventoryCD" DataMember="SalesPricesServices">
                                <RowTemplate>
                                    <px:PXSegmentMask ID="edInventoryItem__InventoryCD" runat="server" DataField="InventoryItem__InventoryCD" AllowEdit="True">
                                    </px:PXSegmentMask>
                                    <px:PXTextEdit ID="edLineType2" runat="server" DataField="LineType">
                                    </px:PXTextEdit>
                                    <px:PXNumberEdit ID="PXNumberEdit1" runat="server" DataField="Mem_UnitPrice" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edUOMSrvContract" runat="server" DataField="UOM">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edCuryID" runat="server" DataField="CuryID">
                                    </px:PXSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="InventoryItem__InventoryCD" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LineType"  Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_UnitPrice" TextAlign="Right" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UOM" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CuryID" Width="100px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <Mode AllowAddNew="False" AllowDelete="False"/>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Inventory Items">
                <Template>
                    <px:PXGrid ID="PXGridScheduleParts" runat="server" AutoAdjustColumns="True" 
                        DataSourceID="ds" Height="100%" SkinID="Inquire" TabIndex="4200" KeepPosition="True"  SyncPosition="True" 
                        Width="100%" NoteIndicator="False" FilesIndicator="False">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="FSSchedule__RefNbr" 
                                DataMember="ScheduleDetPartsByContract">
                                <RowTemplate>
                                    <px:PXSelector ID="edFSSchedule__RefNbrParts" runat="server" AllowEdit="True" DataField="FSSchedule__RefNbr" >
                                    </px:PXSelector>
                                    <px:PXDropDown ID="edLineTypeParts" runat="server" DataField="LineType">
                                    </px:PXDropDown>
                                    <px:PXSegmentMask ID="edInventoryIDParts" runat="server" AllowEdit="True" DataField="InventoryID">
                                    </px:PXSegmentMask>
                                    <px:PXNumberEdit ID="edQtyParts" runat="server" DataField="Qty">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edSMEquipmentIDParts" runat="server" AllowEdit="True" DataField="SMEquipmentID">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edTranDescParts" runat="server" DataField="TranDesc">
                                    </px:PXTextEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="FSSchedule__RefNbr" LinkCommand="OpenRouteScheduleScreenByScheduleDetPart">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LineType">
                                    </px:PXGridColumn>                                    
                                    <px:PXGridColumn DataField="InventoryID">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SMEquipmentID" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="TranDesc" Width="200px"> 
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <Mode AllowAddNew="False" AllowDelete="False"/>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Inventory Item Prices">
                <Template>
                    <px:PXGrid ID="PXGridSalesPricesInventoryItems" runat="server" DataSourceID="ds" 
                        Height="100%" SkinID="Inquire" TabIndex="4200" AutoAdjustColumns="True"
                        Width="100%" NoteIndicator="False" FilesIndicator="False">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="SalesPriceID,InventoryItem__InventoryCD" DataMember="SalesPricesInventoryItems">
                                <RowTemplate>
                                    <px:PXSegmentMask ID="edInventoryItem__InventoryCD2" runat="server" DataField="InventoryItem__InventoryCD" AllowEdit="True">
                                    </px:PXSegmentMask>
                                    <px:PXTextEdit ID="edLineType3" runat="server" DataField="LineType">
                                    </px:PXTextEdit>
                                    <px:PXNumberEdit ID="edMem_UnitPrice2" runat="server" DataField="Mem_UnitPrice" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edUOMSrvContract2" runat="server" DataField="UOM">
                                    </px:PXSelector>
                                    <px:PXSelector ID="edCuryID2" runat="server" DataField="CuryID">
                                    </px:PXSelector>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="InventoryItem__InventoryCD" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LineType"  Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Mem_UnitPrice" TextAlign="Right" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="UOM" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="CuryID" Width="100px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <Mode AllowAddNew="False" AllowDelete="False"/>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" />
    </px:PXTab>
</asp:Content>
