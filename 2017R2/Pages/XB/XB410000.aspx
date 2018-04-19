<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="XB410000.aspx.cs" Inherits="Page_XB410000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="Filter" TypeName="MaxQ.Products.RBRR.LateAmountUpdate" BorderStyle="NotSet" SuspendUnloading="False">
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" Height="150px" DataMember="Filter" TabIndex="700">
        <Template>
            <px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="M" 
                StartColumn="True">
            </px:PXLayoutRule>
            <px:PXSelector runat="server" DataField="ContractID" DataSourceID="ds" 
                ID="ContractID" DisplayMode="Text" CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector runat="server" DataField="ClassID" DataSourceID="ds" ID="ClassID" CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector runat="server" DataField="CustomerID" DataSourceID="ds" ID="CustomerID" CommitChanges="True">
            </px:PXSelector>
            <px:PXDropDown runat="server" DataField="OrdType" ID="OrdType" CommitChanges="True">
            </px:PXDropDown>
        </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" Height="150px" SkinID="Details" TabIndex="900">
		<Levels>
			<px:PXGridLevel DataMember="Contracts">
			    <RowTemplate>
                    <px:PXCheckBox ID="Selected" runat="server" DataField="Selected" 
                        Text="Selected">
                    </px:PXCheckBox>
                    <px:PXSelector ID="ContractCD" runat="server" DataField="ContractCD">
                    </px:PXSelector>
                    <px:PXSelector ID="RevisionNbr" runat="server" DataField="RevisionNbr">
                    </px:PXSelector>
                    <px:PXSelector ID="CustomerID" runat="server" DataField="CustomerID">
                    </px:PXSelector>
                    <px:PXDateTimeEdit ID="BegDate" runat="server" DataField="BegDate">
                    </px:PXDateTimeEdit>
                    <px:PXDateTimeEdit ID="EndDate" runat="server" DataField="EndDate">
                    </px:PXDateTimeEdit>
                    <px:PXDropDown ID="Status" runat="server" DataField="Status">
                    </px:PXDropDown>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" 
                        Width="70px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ContractCD" Width="115px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="RevisionNbr" Width="115px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CustomerID" DisplayMode="Hint" Width="300px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="BegDate" Width="115px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="EndDate" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Status" Width="100px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
