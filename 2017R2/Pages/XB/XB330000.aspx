<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB330000.aspx.cs" Inherits="Page_XB330000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter" TypeName="MaxQ.Products.RBRR.MilestoneCompletion">
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" DataMember="Filter" TabIndex="1700">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True" LabelsWidth="M"/>
		    <px:PXSelector ID="edContractID" runat="server" CommitChanges="True" DataField="ContractID">
            </px:PXSelector>
            <px:PXSelector ID="edClassID" runat="server" CommitChanges="True" DataField="ClassID">
            </px:PXSelector>
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
                    <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected" Text="Selected">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edXRBContrHdr__ContractCD" runat="server" DataField="XRBContrHdr__ContractCD">
                    </px:PXSelector>
                    <px:PXSelector ID="edXRBContrHdr__RevisionNbr" runat="server" DataField="XRBContrHdr__RevisionNbr">
                    </px:PXSelector>
                    <px:PXTextEdit ID="edXRBContrHdr__Descr" runat="server" DataField="XRBContrHdr__Descr" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXDateTimeEdit ID="edGenDate" runat="server" DataField="GenDate" DefaultLocale="">
                    </px:PXDateTimeEdit>
                    <px:PXSelector ID="edXRBContrHdr__CustomerID" runat="server" DataField="XRBContrHdr__CustomerID">
                    </px:PXSelector>
                    <px:PXSelector ID="edMilestoneID" runat="server" DataField="MilestoneID">
                    </px:PXSelector>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="XRBContrHdr__ContractCD" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="XRBContrHdr__RevisionNbr" Width="125px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="XRBContrHdr__Descr" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="GenDate" Width="110px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="XRBContrHdr__CustomerID" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="MilestoneID" Width="100px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
