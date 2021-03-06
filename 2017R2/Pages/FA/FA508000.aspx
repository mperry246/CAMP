<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FA508000.aspx.cs"
    Inherits="Page_FA508000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" Width="100%" runat="server" PrimaryView="Docs" TypeName="PX.Objects.FA.DeleteDocsProcess" Visible="True" />
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100" AllowPaging="True" AllowSearch="true"
        AdjustPageSize="Auto" DataSourceID="ds" SkinID="PrimaryInquire" Caption="Unreleased FA Documents" FastFilterFields="RefNbr, Description" SyncPosition="true">
        <Levels>
            <px:PXGridLevel DataKeyNames="RefNbr" DataMember="Docs">
                <Columns>
                    <px:PXGridColumn AllowCheckAll="True" AllowNull="False" DataField="Selected" Label="Selected" TextAlign="Center" Type="CheckBox"
                        Width="30px" />
                    <px:PXGridColumn DataField="RefNbr" />
                    <px:PXGridColumn DataField="DocDate" Width="90px" />
                    <px:PXGridColumn AllowNull="False" DataField="Origin" RenderEditorText="True" />
                    <px:PXGridColumn DataField="DocDesc" Width="200px" />
                    <px:PXGridColumn AllowNull="False" DataField="Hold" TextAlign="Center" Type="CheckBox" Width="60px" />
                    <px:PXGridColumn AllowNull="False" DataField="IsEmpty" TextAlign="Center" Type="CheckBox" Width="60px" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="400" />
    </px:PXGrid>
</asp:Content>
