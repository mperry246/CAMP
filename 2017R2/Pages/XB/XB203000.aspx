<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB203000.aspx.cs" Inherits="Page_XB203000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" Width="100%" runat="server" 
        PrimaryView="ReasonCodes" 
        TypeName="MaxQ.Products.RBRR.CancelReasonMaint" Visible="true">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
             <px:PXDSCallbackCommand Name="Insert" PostData="Self" Visible="False" />
            <px:PXDSCallbackCommand Name="CopyPaste" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False"></px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Delete" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
			<px:PXDSCallbackCommand Name="First" PostData="Self" Visible="False" />
			<px:PXDSCallbackCommand Name="Previous" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Next" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
			<px:PXDSCallbackCommand Name="Last" PostData="Self" Visible="False" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="true" AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary">
		<Levels>
			<px:PXGridLevel DataKeyNames="Code" DataMember="ReasonCodes">
			    <RowTemplate>
                    <px:PXLabel ID="lblCode" runat="server" 
                        style="z-index:100;position:absolute;left:9px;top:9px;">Code:</px:PXLabel>
                    <px:PXMaskEdit ID="edCode" runat="server" DataField="Code" LabelID="lblCode" 
                        style="z-index:101;position:absolute;left:126px;top:9px;" TabIndex="10" 
                        Width="108px">
                    </px:PXMaskEdit>
                    <px:PXLabel ID="lblDescr" runat="server" 
                        style="z-index:102;position:absolute;left:9px;top:36px;">Description:</px:PXLabel>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" LabelID="lblDescr" 
                        style="z-index:103;position:absolute;left:126px;top:36px;" TabIndex="15" 
                        Width="108px">
                    </px:PXTextEdit>
                    <px:PXLabel ID="lblActivityID" runat="server" 
                        style="z-index:104;position:absolute;left:9px;top:63px;">Activity ID:</px:PXLabel>
                    <px:PXSelector ID="edActivityID" runat="server" DataField="ActivityID" 
                        DataMember="_XMQBillActivityCode_" LabelID="lblActivityID" 
                        style="z-index:105;position:absolute;left:126px;top:63px;" TabIndex="20" 
                        Width="216px">
                    </px:PXSelector>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Code" Width="108px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ActivityID" Width="216px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
        <Mode AllowFormEdit="True"></Mode>
	</px:PXGrid>
</asp:Content>
