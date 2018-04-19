<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="LB502000.aspx.cs" Inherits="Page_LB502000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Records" TypeName="NV.Lockbox.NVLockboxPaymentProcessing" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100" SyncPosition="True"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" SkinID="Inquire" TabIndex="100">
		<Levels>
			<px:PXGridLevel DataKeyNames="LockboxTranCD" DataMember="Records">
			    <RowTemplate>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn AllowCheckAll="true" TextAlign="Center" 
                                        Type="CheckBox"  Width="60px" DataField="Selected">
                        <ValueItems MultiSelect="false" />
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="LockboxTranCD" Width="100px" />
                    <px:PXGridColumn DataField="TransactionType" />
                    <px:PXGridColumn DataField="CheckDate" Width="90px" />
                    <px:PXGridColumn DataField="CheckAmt" Width="100px" TextAlign="Right" />
                    <px:PXGridColumn DataField="CheckNumber" />
                    <px:PXGridColumn DataField="TransactionID" Width="100px" />
                    <px:PXGridColumn DataField="Status" Width="100px" />
                    <px:PXGridColumn DataField="CustomerID" Width="300px" DisplayMode="Hint" />
                    <%--<px:PXGridColumn DataField="GatewayMessage" Width="300px" />--%>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXGrid>
</asp:Content>
