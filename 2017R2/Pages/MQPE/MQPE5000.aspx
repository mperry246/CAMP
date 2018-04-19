<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="MQPE5000.aspx.cs" Inherits="Page_MQPE5000"
    Title="Untitled Page" Debug="true" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
		TypeName="MaxQ.Products.ProcessEngine.MQPE5000" PrimaryView="Filter">
		<CallbackCommands>
		</CallbackCommands>		
	</px:PXDataSource>
</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
  <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Filter" Caption="Selection" DefaultControlID="" >  
    <Template>
      <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
	  <px:PXDropDown ID="edStatus" runat="server" DataField="Status"  CommitChanges="True"/>
    </Template>
  </px:PXFormView>
</asp:Content>

<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="100%" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Inquire" TabIndex="100" SyncPosition="true">
		<Levels>
			<px:PXGridLevel DataKeyNames="Code" DataMember="TransactionList">
<RowTemplate>
<px:PXSelector ID="edTransactionID" runat="server" DataField="TransactionID" 
                        AllowEdit="True" edit="1">
                    </px:PXSelector>
</RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" Width="20px" TextAlign="Center" Type="CheckBox" AllowCheckAll="True" AllowSort="False" AllowMove="False" />
                    <px:PXGridColumn DataField="TransactionID" Width="75px" TextAlign="Right" />
                    <px:PXGridColumn DataField="SubmitDate" Width="130px"  TextAlign="Right"/>
                    <px:PXGridColumn DataField="Icon" Width="25px" />
                    <px:PXGridColumn DataField="Status" Width="100px" />
                    <px:PXGridColumn DataField="Description" Width="300px" />
                    <px:PXGridColumn DataField="ErrorDescription" Width="500px" />
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="300" />
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

 

</asp:Content>

