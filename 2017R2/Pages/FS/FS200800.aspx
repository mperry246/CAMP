<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FS200800.aspx.cs" Inherits="Page_FS200800" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" Width="100%" runat="server" 
        PrimaryView="EquipmentTypeRecords" 
        TypeName="PX.Objects.FS.EquipmentTypeMaint" Visible="True"
        BorderStyle="NotSet" SuspendUnloading="False">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
		    <px:PXDSCallbackCommand Name="Insert" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="CopyPaste" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Delete" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="First" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Previous" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Next" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Last" PopupCommand="" PopupCommandTarget="" 
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Primary" TabIndex="1300" NoteIndicator="True">
		<Levels>
			<px:PXGridLevel DataMember="EquipmentTypeRecords">
			    <RowTemplate>
                    <px:PXMaskEdit ID="edEquipmentTypeCD" runat="server" DataField="EquipmentTypeCD" Size="S">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" Size="S">
                    </px:PXTextEdit>
                    <px:PXCheckBox ID="edRequireBranchLocation" runat="server" 
                        DataField="RequireBranchLocation" Text="Require Branch Location" Size="S">
                    </px:PXCheckBox>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="EquipmentTypeCD" Width="150px" CommitChanges="True">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Descr" Width="400px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="RequireBranchLocation" TextAlign="Center" 
                        Type="CheckBox" Width="122px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
		<ActionBar ActionsText="False">
		</ActionBar>
	    <Mode AllowUpload="True"/>
	</px:PXGrid>
</asp:Content>
