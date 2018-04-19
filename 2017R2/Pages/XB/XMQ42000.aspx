<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XMQ42000.aspx.cs" Inherits="Page_XMQ42000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="Filter" TypeName="MaxQ.Products.Billing.BillingActivityRating">
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
		Width="100%" DataMember="Filter" TabIndex="500">
    <Template>
        <px:PXLayoutRule runat="server" StartColumn="True">
        </px:PXLayoutRule>
        <px:PXSelector runat="server" DataField="CustomerID" DataSourceID="ds" ID="CustomerID">
        </px:PXSelector>
    </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" Height="150px" SkinID="Details" TabIndex="11100">
		<Levels>
			<px:PXGridLevel DataKeyNames="BillActivityCD" DataMember="Activities">
			    <RowTemplate>
                    <px:PXSelector ID="BillActivityCD" runat="server" DataField="BillActivityCD">
                    </px:PXSelector>
                    <px:PXDateTimeEdit ID="ActivityDate" runat="server" DataField="ActivityDate">
                    </px:PXDateTimeEdit>
                    <px:PXTextEdit ID="ARRefNbr" runat="server" DataField="ARRefNbr">
                    </px:PXTextEdit>
                    <px:PXSelector ID="ContractID" runat="server" DataField="ContractID">
                    </px:PXSelector>
                    <px:PXSelector ID="CustomerID" runat="server" DataField="CustomerID">
                    </px:PXSelector>
                    <px:PXDropDown ID="EntityType" runat="server" DataField="EntityType">
                    </px:PXDropDown>
                    <px:PXTextEdit ID="EntityId" runat="server" DataField="EntityId">
                    </px:PXTextEdit>
                    <px:PXNumberEdit ID="FlatFee" runat="server" DataField="FlatFee">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="FlatQty" runat="server" DataField="FlatQty">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="OrigTranAmt" runat="server" DataField="OrigTranAmt">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="OrigTranQty" runat="server" DataField="OrigTranQty">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="Pct" runat="server" DataField="Pct">
                    </px:PXNumberEdit>
                    <px:PXCheckBox ID="Selected" runat="server" DataField="Selected" 
                        Text="Selected">
                    </px:PXCheckBox>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" 
                        Width="70px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="BillActivityCD" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ActivityDate" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ARRefNbr" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ContractID" Width ="180px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CustomerID" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="EntityType" Width ="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="EntityId" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="FlatFee" TextAlign="Right" Width="80px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="FlatQty" TextAlign="Right" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OrigTranAmt" TextAlign="Right" Width="160px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="OrigTranQty" TextAlign="Right" Width="160px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Pct" TextAlign="Right" Width="70px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
