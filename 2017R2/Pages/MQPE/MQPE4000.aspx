<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="MQPE4000.aspx.cs" Inherits="Page_MQPE4000"
    Title="Untitled Page" Debug="true" %>

<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
		TypeName="MaxQ.Products.ProcessEngine.XPE10000" PrimaryView="Header">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="true" />
<px:PXDSCallbackCommand Name="Reprocess" Visible="False" />
		</CallbackCommands>		
	</px:PXDataSource>
</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">



    <px:PXGrid ID="grid" runat="server" Height="300px" Width="100%"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Inquire" TabIndex="100" SyncPosition="true">
		<AutoSize Enabled="True" Container="Window"  />
        <AutoCallBack Target="gHistory" Command="Refresh" />
		<Levels>
			<px:PXGridLevel DataKeyNames="TransactionID" DataMember="Header">
                                            <RowTemplate>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
            <px:PXTextEdit ID="edTransactionID" runat="server" DataField="TransactionID" Width="80px" />
            <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" />
            <px:PXDropDown  ID="edStatus" runat="server" AllowNull="False" DataField="Status" Size="SM" />

            <px:PXLayoutRule runat="server" GroupCaption="Handler" />
            <px:PXTextEdit ID="edHandlerName" runat="server" DataField="HandlerName" Width="500px" CommitChanges="True" />

            <px:PXLayoutRule runat="server" GroupCaption="Error Message" />
            <px:PXTextEdit ID="edErrMessage" runat="server" DataField="ErrorDescription" TextMode="MultiLine" Width="575px" />
            <px:PXTextEdit ID="edErrSource" runat="server" DataField="ErrorSource"  Width="575px" />
            <px:PXTextEdit ID="edErrStack" runat="server" DataField="ErrorStack" TextMode="MultiLine" Width="575px" />
            <px:PXTextEdit ID="edMessage" runat="server" DataField="Message" Width="575px" Height="375px" TextMode="MultiLine" />
          </RowTemplate>

                <Columns>
                    <px:PXGridColumn DataField="TransactionID" Width="75px" TextAlign="Right" />
                    <px:PXGridColumn DataField="SubmitDate" Width="130px"  TextAlign="Right"/>
                    <px:PXGridColumn DataField="Icon" Width="25px" />
                    <px:PXGridColumn DataField="Status" Width="100px" />
                    <px:PXGridColumn DataField="Description" Width="200px" />
                    <px:PXGridColumn DataField="HandlerName" Width="150px" />
                    <px:PXGridColumn DataField="ErrorDescription" Width="500px" />
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<ActionBar>
      <Actions>
        <Save Enabled="False" />
        <Delete Enabled="True" />
        <EditRecord Enabled="True" />
        <NoteShow Enabled="False" />
        <ExportExcel Enabled="False" />
      </Actions>
      <CustomItems>
          <px:PXToolBarButton CommandName="Reprocess" Tooltip="Reprocess" ImageKey="Process" CommandSourceID="ds" DisplayStyle="Image" />
      </CustomItems>
		</ActionBar>
    <Mode AllowAddNew="False" AllowFormEdit="True" AllowDelete="True" />
	</px:PXGrid>




  <px:PXTab ID="tabDetails" runat="server" Height="40%" Width="100%" BorderStyle="None">
    <Items>

      <px:PXTabItem Text="History">
        <Template>

    <px:PXGrid ID="gHistory" runat="server"  Width="100%" 
		AllowPaging="False" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Inquire" TabIndex="100"  SyncPosition="true" >
		<Levels>
			<px:PXGridLevel DataKeyNames="Code2" DataMember="History">

                <Columns>
                    <px:PXGridColumn DataField="TransactionDate" Width="130px"  TextAlign="Right"/>
                    <px:PXGridColumn DataField="Icon" Width="25px" />
                    <px:PXGridColumn DataField="Action" Width="130px" />
                    <px:PXGridColumn DataField="ErrorDescription" Width="400px" />
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True"  />
		<ActionBar>
      <Actions>
        <Save Enabled="False" />
        <EditRecord Enabled="False" />
        <NoteShow Enabled="False" />
        <ExportExcel Enabled="False" />
      </Actions>
      <CustomItems>
      </CustomItems>
		</ActionBar>
    <Mode AllowAddNew="False" AllowFormEdit="False" AllowDelete="False" />
	</px:PXGrid>


        </Template>
      </px:PXTabItem>


    </Items>
  </px:PXTab>


</asp:Content>

