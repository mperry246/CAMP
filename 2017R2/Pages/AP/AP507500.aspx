<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AP507500.aspx.cs" Inherits="Page_AP507500" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter" TypeName="PX.Objects.AP.MISC1099EFileProcessing" PageLoadBehavior="PopulateSavedValues" />
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100"
		Width="100%" DataMember="Filter" TabIndex="100">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True" ControlSize="M" LabelsWidth="S" StartColumn="True" />
			<px:PXSelector ID="edBranch" runat="server" DataField="MasterBranchID" CommitChanges="True" />
			<px:PXSelector ID="edFinYear" runat="server" DataField="FinYear" CommitChanges="True" AutoRefresh="True" />
			<px:PXDropDown ID="edInclude" runat="server" DataField="Include" CommitChanges="True"/>
			<px:PXLayoutRule runat="server" StartColumn="True" SuppressLabel="True" />
			<px:PXCheckBox ID="edIsPriorYear" runat="server" DataField="IsPriorYear" />
			<px:PXCheckBox ID="edIsCorrectionReturn" runat="server" DataField="IsCorrectionReturn" />
			<px:PXCheckBox ID="edIsLastFiling" runat="server" DataField="IsLastFiling" />
			<px:PXCheckBox ID="edReportingDirectSalesOnly" runat="server" DataField="ReportingDirectSalesOnly" />
			<px:PXCheckBox ID="edIsTestMode" runat="server" DataField="IsTestMode" />
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100"
		Width="100%" Height="150px" AllowPaging="true" AdjustPageSize="Auto" SkinID="PrimaryInquire" SyncPosition="true" FastFilterFields="VAcctCD, VAcctName">
		<Levels>
			<px:PXGridLevel DataMember="Records">
				<Columns>
					<px:PXGridColumn DataField="Selected" Width="30px" TextAlign="Center" Type="CheckBox" AllowCheckAll="True" AllowSort="False" AllowMove="False"></px:PXGridColumn>
					<px:PXGridColumn DataField="VAcctCD" Width="120px" />
					<px:PXGridColumn DataField="VAcctName" Width="200px" />
					<px:PXGridColumn DataField="HistAmt" TextAlign="Right" Width="100px" />
					<px:PXGridColumn DataField="LTaxRegistrationID" Width="200px" />
					<px:PXGridColumn DataField="PayerBranchID" Width="100px" />
				</Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="400"></AutoSize>
	</px:PXGrid>
</asp:Content>
