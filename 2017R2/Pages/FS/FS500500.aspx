<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FS500500.aspx.cs" Inherits="Page_FS500500" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="Filter" 
        TypeName="PX.Objects.FS.UpdateInventoryPost" 
        SuspendUnloading="False" PageLoadBehavior="InsertRecord">
		<CallbackCommands>
            <px:PXDSCallbackCommand DependOnGrid="gridAppointments" Name="Appointments_ViewDetails" Visible="False" RepaintControls="All"/>
            <px:PXDSCallbackCommand Name="ViewPostBatch" PopupCommand="" PopupCommandTarget="" PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" Height="125px" DataMember="Filter" TabIndex="700" DefaultControlID="edRouteDocumentID">
        <Template>
            <px:PXLayoutRule runat="server" StartRow="True" GroupCaption="Filters Options" ControlSize="SM" LabelsWidth="SM">
            </px:PXLayoutRule>
            <px:PXDateTimeEdit ID="edCutOffDate" runat="server" DataField="CutOffDate" CommitChanges="True" AutoRefresh="True">
            </px:PXDateTimeEdit>
            <px:PXSelector ID="edRouteDocumentID" runat="server" CommitChanges="True"  DataField="RouteDocumentID" AutoRefresh="True">
            </px:PXSelector>
            <px:PXSelector ID="edAppointmentID" runat="server" CommitChanges="True" DataField="AppointmentID" AutoRefresh="True">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" StartColumn="True" GroupCaption="Generation Options">
            </px:PXLayoutRule>
            <px:PXDateTimeEdit ID="edocumentDate" runat="server" DataField="DocumentDate" CommitChanges="True">
            </px:PXDateTimeEdit>
            <px:PXSelector ID="edFinPeriodID" runat="server" CommitChanges="True" 
                DataField="FinPeriodID" Width="100px">
            </px:PXSelector>
        </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
    <px:PXGrid ID="gridAppointments" runat="server" AllowPaging="True" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" Height="150px" SkinID="Inquire" TabIndex="500" SyncPosition="True" BatchUpdate="True">
		<Levels>
			<px:PXGridLevel DataMember="Appointments">
			    
			    <RowTemplate>
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected" 
                        Text="Selected">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edFSAppointment__SrvOrdType" runat="server" DataField="FSAppointment__SrvOrdType" 
                        AllowEdit="True">
                    </px:PXSelector>
                    <px:PXSelector ID="edFSAppointment__RefNbr" runat="server" DataField="FSAppointment__RefNbr" AllowEdit="True">
                    </px:PXSelector>
                    <px:PXSegmentMask ID="edFSServiceOrder__BillCustomerID" runat="server" 
                        DataField="FSServiceOrder__BillCustomerID" AllowEdit="True" 
                        DisplayMode="Text">
                    </px:PXSegmentMask>
                    <px:PXTextEdit ID="edCustomer__AcctName" runat="server" DataField="Customer__AcctName">
                    </px:PXTextEdit>
                    <px:PXSelector ID="edFSAppointment__SORefNbr" runat="server" DataField="FSAppointment__SORefNbr" 
                        AllowEdit="True">
                    </px:PXSelector>                    
                    <px:PXSelector ID="edSODetID" runat="server" DataField="SODetID" AllowEdit="True">
                    </px:PXSelector>
                    <px:PXSegmentMask ID="edFSAppointmentDet__ServiceID" runat="server" DataField="FSAppointmentDet__ServiceID" AllowEdit="True">
                    </px:PXSegmentMask>
                    <px:PXSegmentMask ID="edInventoryID" runat="server" DataField="InventoryID" AllowEdit="True">
                    </px:PXSegmentMask>                    
                    <px:PXTextEdit ID="edMem_BatchNbr" runat="server" 
                        DataField="Mem_BatchNbr" AllowEdit="True">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" 
                        Width="50px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="FSAppointment__SrvOrdType" Width="50px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="FSAppointment__RefNbr" Width="80px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="FSServiceOrder__BillCustomerID" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Customer__AcctName" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="FSAppointment__SORefNbr">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="SODetID">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="FSAppointmentDet__ServiceID">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="InventoryID">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Mem_BatchNbr" LinkCommand="ViewPostBatch">
                    </px:PXGridColumn>
                </Columns>
			    
			</px:PXGridLevel>
		</Levels>
        <ActionBar DefaultAction="viewDetail" PagerActionsText="true" PagerVisible="False">
            <CustomItems>
				<px:PXToolBarButton Text="View Details" Key="viewDetail">
					<ActionBar GroupIndex="0" />
					<AutoCallBack Target="ds" Command="Appointments_ViewDetails" />
					<PopupCommand Target="gridAppointments" Command="Refresh" />
					<Images Normal="main@Inquiry" />
				</px:PXToolBarButton>
            </CustomItems>
		</ActionBar>
	    <Mode AllowAddNew="False" AllowDelete="False"/>
		<AutoSize Container="Window" Enabled="True" MinHeight="150"/>
	</px:PXGrid>
</asp:Content>
