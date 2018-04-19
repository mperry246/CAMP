<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB470000.aspx.cs" Inherits="Page_XB470000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="Filter" TypeName="MaxQ.Products.RBRR.RepriceContracts">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" DataMember="Filter" TabIndex="100">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M">
            </px:PXLayoutRule>
            <px:PXSelector ID="edClassID" runat="server" CommitChanges="True" 
                DataField="ClassID">
            </px:PXSelector>
            <px:PXDropDown ID="edOrdType" runat="server" CommitChanges="True" 
                DataField="OrdType">
            </px:PXDropDown>
            <px:PXDropDown ID="edEditableStatus" runat="server" CommitChanges="True" 
                DataField="EditableStatus">
            </px:PXDropDown>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edCustomerID" runat="server" CommitChanges="True" 
                DataField="CustomerID">
            </px:PXSelector>
            <px:PXSelector runat="server" DataField="ContractID" ID="edContractID" 
                CommitChanges="True" DisplayMode="Text">
            </px:PXSelector>
        </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" Height="150px" SkinID="Details" TabIndex="300">
		<Levels>
			<px:PXGridLevel DataKeyNames="ContractCD,RevisionNbr" 
                DataMember="ContractsToReprice">
			    <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected" 
                        Text="Selected">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edContractCD" runat="server" DataField="ContractCD">
                    </px:PXSelector>
                    <px:PXSelector ID="edRevisionNbr" runat="server" DataField="RevisionNbr">
                    </px:PXSelector>
                    <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID">
                    </px:PXSelector>
                    <px:PXSelector ID="edClassID" runat="server" DataField="ClassID">
                    </px:PXSelector>
                    <px:PXDateTimeEdit ID="edBegDate" runat="server" DataField="BegDate">
                    </px:PXDateTimeEdit>
                    <px:PXDateTimeEdit ID="edEndDate" runat="server" DataField="EndDate">
                    </px:PXDateTimeEdit>
                    <px:PXDropDown ID="edOrdType" runat="server" DataField="OrdType">
                    </px:PXDropDown>
                    <px:PXDropDown ID="edStatus" runat="server" DataField="Status">
                    </px:PXDropDown>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" 
                        Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ContractCD" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="RevisionNbr" Width="125px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CustomerID" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ClassID" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="BegDate" Width="125px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="EndDate" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OrdType" Width="175px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Status" Width="120px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
