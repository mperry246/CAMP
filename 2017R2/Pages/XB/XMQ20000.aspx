<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XMQ20000.aspx.cs" Inherits="Page_XMQ20000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" Width="100%" runat="server" Visible="True"
        PrimaryView="ActivityCodes" 
        TypeName="MaxQ.Products.Billing.BillingActivityCodeMaint" 
        BorderStyle="NotSet" SuspendUnloading="False">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Primary" TabIndex="3100">
		<Levels>
			<px:PXGridLevel DataKeyNames="Descr" DataMember="ActivityCodes">
			    <RowTemplate>
                    <px:PXLayoutRule runat="server" ControlSize="XL">
                    </px:PXLayoutRule>
                    <px:PXTextEdit ID="Descr" runat="server" DataField="Descr">
                    </px:PXTextEdit>
                    <px:PXSelector ID="InventoryID" runat="server" DataField="InventoryID" 
                        DisplayMode="Text" Size="XM">
                    </px:PXSelector>
                    <px:PXCheckBox ID="Active" runat="server" DataField="Active" Text="Active">
                    </px:PXCheckBox>
                    <px:PXSelector ID="CategoryID" runat="server" DataField="CategoryID" 
                        DisplayMode="Text">
                    </px:PXSelector>
                    <px:PXSelector ID="TypeID" runat="server" DataField="TypeID" DisplayMode="Text">
                    </px:PXSelector>
                    <px:PXCheckBox ID="edManualEntryAllowed" runat="server" 
                        DataField="ManualEntryAllowed" Text="Allow Manual Entry">
                    </px:PXCheckBox>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Descr" Width="300px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="InventoryID" DisplayMode="Hint" Width="300px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Active" TextAlign="Center" Type="CheckBox" 
                        Width="60px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CategoryID" DisplayMode="Hint" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="TypeID" DisplayMode="Hint" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ManualEntryAllowed" TextAlign="Center" 
                        Type="CheckBox" Width="180px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	    <Mode AllowFormEdit="True" />
	</px:PXGrid>
</asp:Content>
