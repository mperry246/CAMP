<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XMQ90000.aspx.cs" Inherits="Page_XMQ90000"
    Title="Untitled Page" Debug="true" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
		TypeName="MaxQ.Products.Registration.XMQRegMaintenance" PrimaryView="setupRegistration">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="true" />
<px:PXDSCallbackCommand Name="DownloadPackage" Visible="False" />
<px:PXDSCallbackCommand Name="RenewProduct" Visible="False" />
		</CallbackCommands>		
	</px:PXDataSource>
</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">

        <px:PXFormView ID="fvSetup" runat="server" DataSourceID="ds" DataMember="setupRegistration" Width="100%" >
            <Template>
                <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
            	<px:PXMaskEdit ID="edActivationKey" runat="server" DataField="ActivationKey" />
            	<px:PXTextEdit ID="edClientName" runat="server" DataField="ClientName" />
            	<px:PXTextEdit ID="edRegDate" runat="server" DataField="RegistrationDate" />
            </Template>
        </px:PXFormView>

    <px:pxsmartpanel id="popupTemplate" runat="server" captionvisible="true" caption="MaxQ End-User License Agreement" height="470px" width="600px" designview="Content" key="EULA">
        <px:PXFormView ID="fvEULA" runat="server" DataSourceID="ds" DataMember="EULA" Width="100%" >
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" />
                <px:PXLabel ID="lblAutoGenTaxID" runat="server" text="Please read the following license agreement carefully." />

                <pxa:PXRichTextEdit ID="eulaEULA" runat="server" DataField="Body" Height="325" Width="540" />
                        
                <px:PXCheckBox ID="eulaAgreed" runat="server" DataField="Agreed" CommitChanges="True" />

            </Template>
        </px:PXFormView>
        <px:PXPanel ID="PXPanel4" runat="server" SkinID="Buttons">
            <px:PXButton ID="cmdEULAOK" runat="server" DialogResult="OK" Text="OK" />
            <px:PXButton ID="cmdEULACancel" runat="server" DialogResult="Cancel" Text="Cancel" />
        </px:PXPanel>
    </px:pxsmartpanel>


</asp:Content>

<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">

    <px:PXGrid ID="grid" runat="server" Height="100%" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Inquire" TabIndex="100" SyncPosition="true">
		<Levels>
			<px:PXGridLevel DataKeyNames="Code" DataMember="Products">

                <Columns>
                    <px:PXGridColumn DataField="Status" Width="100px" />
                    <px:PXGridColumn DataField="Description" Width="300px" />
                    <px:PXGridColumn DataField="DisplayVersion" Width="120px" />
                    <px:PXGridColumn DataField="ExpirationDate" Width="100px" />
                    <px:PXGridColumn DataField="ServerVersion" Width="120px" />
                    <px:PXGridColumn DataField="ReleaseDate" Width="100px" />
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
		<ActionBar DefaultAction="View">
      <Actions>
        <Save Enabled="False" />
        <EditRecord Enabled="False" />
        <NoteShow Enabled="False" />
        <ExportExcel Enabled="False" />
        <PageNext Enabled="False" />
        <PagePrev Enabled="False" />
        <PageFirst Enabled="False" />
        <PageLast Enabled="False" />
      </Actions>
      <CustomItems>
                 <px:PXToolBarButton Text="Renew" Tooltip="Renew" CommandName="renewProduct" CommandSourceID="ds" />
                 <px:PXToolBarButton Text="Download Update" Tooltip="Download Update" CommandName="downloadPackage" CommandSourceID="ds" />
      </CustomItems>
		</ActionBar>
    <Mode AllowAddNew="False" AllowFormEdit="False" AllowDelete="False" />
	</px:PXGrid>

</asp:Content>
