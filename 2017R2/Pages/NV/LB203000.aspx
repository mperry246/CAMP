<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="LB203000.aspx.cs" Inherits="Page_LB203000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Payments" TypeName="NV.Lockbox.NVLockboxPaymentMaint">
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" DataMember="Payments" TabIndex="100">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True" ControlSize="SM" LabelsWidth="S" StartColumn="True"/>
		    <px:PXSelector ID="edLockboxTranCD" runat="server" DataField="LockboxTranCD" DisplayMode="Value">
            </px:PXSelector>
            <px:PXDropDown ID="edTransactionType" runat="server" DataField="TransactionType" Enabled="False">
            </px:PXDropDown>
            <px:PXDropDown ID="edStatus" runat="server" DataField="Status" Enabled="False">
            </px:PXDropDown>
            <px:PXLayoutRule runat="server" ControlSize="SM" LabelsWidth="S" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID" Size="L" Enabled="False" />
            <px:PXTextEdit ID="edTransactionID" runat="server" DataField="TransactionID" Enabled="False" />
            <px:PXTextEdit ID="edGatewayMessage" runat="server" DataField="GatewayMessage" Size="L" Enabled="False" />
            <px:PXLayoutRule runat="server" ControlSize="SM" LabelsWidth="S" StartColumn="True" />
            <px:PXNumberEdit ID="edCheckAmt" runat="server" DataField="CheckAmt"  Enabled="False" />
            <px:PXDateTimeEdit ID="edCreatedDateTime" runat="server" DataField="CreatedDateTime" Enabled="False" />
            <px:PXTextEdit ID="edCheckNumber" runat="server" DataField="CheckNumber" Enabled="False" />
            <px:PXLayoutRule runat="server" ControlSize="SM" LabelsWidth="SM" StartColumn="True" />
            <px:PXTextEdit ID="edAcumaticaPaymentRefNbr" runat="server" DataField="AcumaticaPaymentRefNbr" Enabled="False" />
            <px:PXSelector ID="edCreatedByID" runat="server" DataField="CreatedByID" Enabled="false" DisplayMode="Text">
            </px:PXSelector>
        </Template>
	</px:PXFormView>
    <px:PXSmartPanel ID="PanelECheck" runat="server" AcceptButtonID="PXButtonOK" AutoReload="true" CancelButtonID="PXButtonCancel"
        Caption="eCheck Processing" CaptionVisible="True" DesignView="Content" HideAfterAction="false" Key="ECheckRecord"
        LoadOnDemand="true" Height="315px" Width="425px">
        <px:PXFormView ID="PXFormView5" runat="server" CaptionVisible="False" DataMember="ECheckRecord" Width="100%"
            DefaultControlID="edECheckRecord" DataSourceID="ds">
            <ContentStyle BackColor="Transparent" BorderStyle="None">
            </ContentStyle>
            <Template>
                <px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="SM" StartColumn="True" StartRow="True" />
                <px:PXDropDown CommitChanges="True" ID="edBankAccountType" runat="server" AllowNull="False"
                        DataField="BankAccountType" Size="SM" />
                <px:PXTextEdit ID="edBankABACode" runat="server" CommitChanges="True" DataField="BankABACode" />
                <px:PXTextEdit ID="edBankAccountNumber" runat="server" CommitChanges="True" DataField="BankAccountNumber" />
                <px:PXTextEdit ID="edBankName" runat="server" CommitChanges="True" DataField="BankName" />
                <px:PXTextEdit ID="edAccountName" runat="server" CommitChanges="True" DataField="AccountName" />
                <px:PXTextEdit ID="edBankCheckNumber" runat="server" Size="S" CommitChanges="True" DataField="BankCheckNumber" />
                <px:PXNumberEdit ID="edCheckAmt" runat="server" DataField="CheckAmt" DataSourceID="ds" Enabled="false" />
                <px:PXTextEdit ID="edInvoiceNbr" runat="server" DataField="InvoiceNbr" DataSourceID="ds" Enabled="false"/>
                <px:PXLayoutRule runat="server" StartRow="True" />
                <px:PXPanel ID="PXPanel6" runat="server" SkinID="Buttons">
                    <px:PXButton ID="OK" runat="server" DialogResult="OK" Text="OK" >
                    </px:PXButton>
                    <px:PXButton ID="Cancel" runat="server" DialogResult="Cancel" Text="Cancel">
                    </px:PXButton>
                </px:PXPanel>
            </Template>
        </px:PXFormView>
    </px:PXSmartPanel>
    <px:PXSmartPanel ID="PanelCreditCard" runat="server" AcceptButtonID="PXButtonOK" AutoReload="true" CancelButtonID="PXButtonCancel"
        Caption="Credit Card Processing" CaptionVisible="True" DesignView="Content" HideAfterAction="false" Key="CreditCardRecord"
        LoadOnDemand="true" Height="260px" Width="425px">
        <px:PXFormView ID="PXFormViewcc" runat="server" CaptionVisible="False" DataMember="CreditCardRecord" Width="100%"
            DefaultControlID="edECheckRecord" DataSourceID="ds">
            <ContentStyle BackColor="Transparent" BorderStyle="None">
            </ContentStyle>
            <Template>
                <px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="SM" StartColumn="True" StartRow="True" />
                <px:PXTextEdit ID="edCardNumber" runat="server" CommitChanges="True" DataField="CardNumber" />
                <px:PXTextEdit ID="edExpirationMonthandYear" runat="server" Size="XS" CommitChanges="True" DataField="ExpirationMonthandYear" />
                <px:PXTextEdit ID="edCardCode" runat="server" CommitChanges="True" Size="XS" DataField="CardCode" />
                <px:PXTextEdit ID="edDescription" runat="server" CommitChanges="True" DataField="Description" />
                <px:PXNumberEdit ID="edAmount" runat="server" DataField="Amount" DataSourceID="ds" Enabled="false" />
                <px:PXTextEdit ID="edInvoiceNbr" runat="server" DataField="InvoiceNbr" DataSourceID="ds" Enabled="false"/>
                <px:PXLayoutRule runat="server" StartRow="True" />
                <px:PXPanel ID="PXPanel6" runat="server" SkinID="Buttons">
                    <px:PXButton ID="OK" runat="server" DialogResult="OK" Text="OK" >
                    </px:PXButton>
                    <px:PXButton ID="Cancel" runat="server" DialogResult="Cancel" Text="Cancel">
                    </px:PXButton>
                </px:PXPanel>
            </Template>
        </px:PXFormView>
    </px:PXSmartPanel>
    
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" Height="150px" SkinID="Inquire" TabIndex="300">
		<Levels>
			<px:PXGridLevel DataKeyNames="LockboxTranCD,LockboxTranDetailID" DataMember="PaymentDetail">
			    <RowTemplate>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="InvoiceNbr" Width="100px" />
                    <px:PXGridColumn DataField="InvoiceAmt" TextAlign="Right" Width="100px" />
                    <px:PXGridColumn DataField="ApplyAmt" TextAlign="Right" Width="100px" CommitChanges="true" />
                </Columns>
			</px:PXGridLevel>
		</Levels>
        <Mode AllowAddNew="false" />
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
