<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CP208000.aspx.cs" Inherits="Page_CP208000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="DiscountReasonCodeRecords" TypeName="CAMPCustomization.CAMPDiscountReasonCodeMaint" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
        FastFilterFields="DiscountReasonCD,Description" AllowPaging="True" AllowSearch="True" 
        AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="100">
		<Levels>
			<px:PXGridLevel DataKeyNames="DiscountReasonCD" DataMember="DiscountReasonCodeRecords">
			    <RowTemplate>
                    <px:PXMaskEdit ID="edDiscountReasonCD" runat="server" DataField="DiscountReasonCD">
                    </px:PXMaskEdit>
                    <px:PXTextEdit ID="edDescription" runat="server" DataField="Description">
                    </px:PXTextEdit>
                    <px:PXNumberEdit ID="edDefaultPct" runat="server" DataField="DefaultPct">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edMaxPct" runat="server" DataField="MaxPct">
                    </px:PXNumberEdit>
                    <px:PXCheckBox ID="edAllowChanges" runat="server" DataField="AllowChanges" Text="Allow Changes">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edAllowOverMax" runat="server" DataField="AllowOverMax" Text="Allow Over Max">
                    </px:PXCheckBox>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="DiscountReasonCD" Width="150"/>
                    <px:PXGridColumn DataField="Description" Width="300px"/>
                    <px:PXGridColumn DataField="DefaultPct" TextAlign="Right" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="MaxPct" TextAlign="Right" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="AllowChanges" TextAlign="Center" Type="CheckBox" Width="60px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="AllowOverMax" TextAlign="Center" Type="CheckBox" Width="60px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
        <Mode AllowUpload="True" />
	</px:PXGrid>
</asp:Content>