<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CP204000.aspx.cs" Inherits="Page_CP204000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="ProfileNumberRecords" TypeName="CAMPCustomization.CAMPProfileNumberMaint" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
        FastFilterFields="ProfileNumberCD,Description" AllowPaging="True" AllowSearch="True"  KeepPosition="true" SyncPosition="true"
        AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="100">
		<Levels>
			<px:PXGridLevel DataKeyNames="ProfileNumberCD" DataMember="ProfileNumberRecords">
			    <RowTemplate>
                    <px:PXMaskEdit ID="edProfileNumberCD" runat="server" DataField="ProfileNumberCD" ></px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" ></px:PXTextEdit>
                    <px:PXSelector ID="edTapeModelID" runat="server" DataField="TapeModelID" CommitChanges="true" ></px:PXSelector>
                    <px:PXTextEdit ID="edSerialNbr" runat="server" DataField="SerialNbr">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edRegistrationNbr" runat="server" DataField="RegistrationNbr">
                    </px:PXTextEdit>
                    <px:PXSelector ID="edManufacturerID" runat="server" DataField="ManufacturerID" ></px:PXSelector>
                    <px:PXTextEdit ID="edOPSNbr" runat="server" DataField="OPSNbr" ></px:PXTextEdit>
                    
                    <px:PXDropDown ID="edOperatingFAR" runat="server" DataField="OperatingFAR" ></px:PXDropDown>
                    <px:PXDropDown ID="edStatus" runat="server" DataField="Status" ></px:PXDropDown>
                    <px:PXTextEdit ID="edAutoDeactivation" runat="server" DataField="AutoDeactivation">
                    </px:PXTextEdit>
                    <px:PXDateTimeEdit ID="edAutoDeactivationDate" runat="server" DataField="AutoDeactivationDate">
                    </px:PXDateTimeEdit>
                    <px:PXTextEdit ID="edReasonforDeactivation" runat="server" DataField="ReasonforDeactivation">
                    </px:PXTextEdit>
                    <px:PXNumberEdit ID="edAnalystAssigned" runat="server" DataField="AnalystAssigned">
                    </px:PXNumberEdit>
                    <px:PXSelector ID="edServiceCenterID" runat="server" DataField="ServiceCenterID">
                    </px:PXSelector>
                    <px:PXNumberEdit ID="edOwnerID" runat="server" DataField="OwnerID">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edOEID" runat="server" DataField="OEID">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edMaintenanceID" runat="server" DataField="MaintenanceID">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edOperatorID" runat="server" DataField="OperatorID">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edBillAddress" runat="server" DataField="BillAddress">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edShipAddress" runat="server" DataField="ShipAddress">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edOwnerAddress" runat="server" DataField="OwnerAddress">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edCAMO" runat="server" DataField="CAMO">
                    </px:PXNumberEdit>
                    <px:PXDropDown ID="edTextFunctionality" runat="server" DataField="TextFunctionality">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edPrintOption" runat="server" DataField="PrintOption">
                    </px:PXDropDown>
                    <px:PXNumberEdit ID="edNumberOfCopies" runat="server" DataField="NumberOfCopies">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edOperator2" runat="server" DataField="Operator2">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edOperator3" runat="server" DataField="Operator3">
                    </px:PXNumberEdit>
                    <px:PXCheckBox ID="edAutoNotificationandReports" runat="server" DataField="AutoNotificationandReports" Text="AutoNotification and Reports">
                    </px:PXCheckBox>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="ProfileNumberCD" Width="200"></px:PXGridColumn>
                    <px:PXGridColumn DataField="Description" Width="300px"></px:PXGridColumn>
                    <px:PXGridColumn DataField="TapeModelID" DisplayMode="Value" Width="200" CommitChanges="true" ></px:PXGridColumn>
                    <px:PXGridColumn DataField="SerialNbr" Width="200" ></px:PXGridColumn>
                    <px:PXGridColumn DataField="RegistrationNbr" Width="200" ></px:PXGridColumn>
                    <px:PXGridColumn DataField="OPSNbr" Width="100px" ></px:PXGridColumn>
                    <px:PXGridColumn DataField="ManufacturerID" DisplayMode="Text" Width="200" ></px:PXGridColumn>
                    <px:PXGridColumn DataField="Status">
                    </px:PXGridColumn>
	<px:PXGridColumn DataField="LastActualsDate" Width="90" ></px:PXGridColumn>
                    <px:PXGridColumn DataField="AutoDeactivationDate" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="AnalystAssigned" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OwnerID" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OEID" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="MaintenanceID" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OperatorID" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CAMO" TextAlign="Right">
                    </px:PXGridColumn></Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
        <Mode AllowUpload="True" />
        </px:PXGrid>
</asp:Content>