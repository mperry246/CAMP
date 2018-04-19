<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB340000.aspx.cs" Inherits="Page_XB340000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter" TypeName="MaxQ.Products.RBRR.RevenueMilestoneCompletion">
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" DataMember="Filter" TabIndex="1700">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True" LabelsWidth="M"/>
            <px:PXSelector ID="edCustomerID" runat="server" CommitChanges="True" DataField="CustomerID">
            </px:PXSelector>
            <px:PXSelector ID="edMilestoneID" runat="server" CommitChanges="True" DataField="MilestoneID">
            </px:PXSelector>
            <px:PXDateTimeEdit ID="edBegDate" runat="server" CommitChanges="True" DataField="BegDate">
            </px:PXDateTimeEdit>
            <px:PXDateTimeEdit ID="edEndDate" runat="server" CommitChanges="True" DataField="EndDate">
            </px:PXDateTimeEdit>
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" Height="150px" SkinID="Details" TabIndex="1300" TemporaryFilterCaption="Filter Applied">
		<Levels>
			<px:PXGridLevel DataMember="FilteredProcessing">
			    <RowTemplate>
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected" Text="Selected" AlreadyLocalized="False">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edScheduleID" runat="server" DataField="ScheduleID">
                    </px:PXSelector>
                    <px:PXSelector ID="edComponentID" runat="server" DataField="ComponentID">
                    </px:PXSelector>
                    <px:PXDateTimeEdit ID="edDRSchedule__DocDate" runat="server" AlreadyLocalized="False" DataField="DRSchedule__DocDate" DefaultLocale="">
                    </px:PXDateTimeEdit>
                    <px:PXTextEdit ID="edDRSchedule__BAccountID_BAccountR_acctName" runat="server" AlreadyLocalized="False" DataField="DRSchedule__BAccountID_BAccountR_acctName" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXSelector ID="edMilestoneID" runat="server" DataField="MilestoneID">
                    </px:PXSelector>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ScheduleID" Width="100px" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ComponentID" Width="150px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DRSchedule__DocDate" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DRSchedule__BAccountID_BAccountR_acctName" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="MilestoneID" Width="100px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
