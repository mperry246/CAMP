<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XMQ28000.aspx.cs" Inherits="Page_XMQ28000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"  PrimaryView="BAccount" TypeName="MaxQ.Products.Billing.CustomerCollectionMaint" >
        
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand StartNewGroup="True" Name="First" />
			<px:PXDSCallbackCommand DependOnGrid="grdContacts" Name="ViewContact" Visible="False" />
			<px:PXDSCallbackCommand Name="NewContact" Visible="False" CommitChanges="true" />
			<px:PXDSCallbackCommand DependOnGrid="grdLocations" Name="ViewLocation" Visible="False" CommitChanges="true" />
			<px:PXDSCallbackCommand Name="NewLocation" Visible="False" CommitChanges="true" />
			<px:PXDSCallbackCommand DependOnGrid="grdLocations" Name="SetDefault" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewVendor" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewBusnessAccount" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewMainOnMap" Visible="false" />
			<px:PXDSCallbackCommand Name="ViewDefLocationOnMap" Visible="false" />
			<px:PXDSCallbackCommand Name="ViewRestrictionGroups" Visible="False" />
			<px:PXDSCallbackCommand Name="ExtendToVendor" Visible="false" />
			<px:PXDSCallbackCommand Name="CustomerDocuments" Visible="False" />
			<px:PXDSCallbackCommand Name="StatementForCustomer" Visible="False" />
			<px:PXDSCallbackCommand DependOnGrid="grdPaymentMethods" Name="ViewPaymentMethod" Visible="false" StartNewGroup="True" CommitChanges="true" />
			<px:PXDSCallbackCommand Name="AddPaymentMethod" Visible="False" CommitChanges="true" />
			<px:PXDSCallbackCommand Name="NewInvoiceMemo" Visible="False" />
			<px:PXDSCallbackCommand Name="NewSalesOrder" Visible="False" />
			<px:PXDSCallbackCommand Name="NewPayment" Visible="False" />
			<px:PXDSCallbackCommand Name="WriteOffBalance" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewBillAddressOnMap" Visible="false" />
			<px:PXDSCallbackCommand Name="Action" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="Inquiry" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="Report" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ARBalanceByCustomer" Visible="False" />
			<px:PXDSCallbackCommand Name="CustomerHistory" Visible="False" />
			<px:PXDSCallbackCommand Name="ARAgedPastDue" Visible="False" />
			<px:PXDSCallbackCommand Name="ARAgedOutstanding" Visible="False" />
			<px:PXDSCallbackCommand Name="ARRegister" Visible="False" />
			<px:PXDSCallbackCommand Name="CustomerDetails" Visible="False" />
			<px:PXDSCallbackCommand Name="CustomerStatement" Visible="False" />
			<px:PXDSCallbackCommand Name="SalesPrice" Visible="False" />
			<px:PXDSCallbackCommand StartNewGroup="True" Name="ValidateAddresses" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand StartNewGroup="True" Name="RegenerateLastStatement" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="NewTask" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="NewEvent" Visible="False" CommitChanges="True" />
            <px:PXDSCallbackCommand Name="RemoveContact" PopupCommand="" PopupCommandTarget=""
                PopupPanel="" Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AddContact" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="AddNewContact" PopupCommand="" PopupCommandTarget="" PopupPanel=""
                Text="" Visible="False">
            </px:PXDSCallbackCommand>
			<px:PXDSCallbackCommand Name="NewActivity" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="NewMailActivity" DependOnGrid="grdContacts" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ViewActivity" DependOnGrid="gridActivities" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="OpenActivityOwner" Visible="False" CommitChanges="True" DependOnGrid="gridActivities" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="BAccount" TabIndex="1600">
       
 <Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
<px:PXSegmentMask ID="edAcctCD" runat="server" DataField="AcctCD" FilterByAllFields="True" CommitChanges="True" />
 <px:PXLayoutRule runat="server" ColumnSpan="2" />
 <px:PXTextEdit CommitChanges="True" ID="edAcctName" runat="server" DataField="AcctName" />
 <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="XS" ControlSize="S" />
 <px:PXDropDown ID="edStatus" runat="server" DataField="Status" CommitChanges="True" />
 <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
 <px:PXFormView ID="CustomerBalance" runat="server" DataMember="CustomerBalance" RenderStyle="Simple" DataSourceID="ds" TabIndex="5300">
				<Template>
					<px:PXLayoutRule runat="server" ControlSize="SM" LabelsWidth="SM" StartColumn="True" />
					<px:PXNumberEdit ID="edBalance" runat="server" DataField="Balance" Enabled="False" />
					<px:PXNumberEdit ID="edConsolidatedBalance" runat="server" DataField="ConsolidatedBalance" Enabled="False" />
					<px:PXNumberEdit ID="edSignedDepositsBalance" runat="server" DataField="SignedDepositsBalance" Enabled="False" />
				</Template>
			</px:PXFormView>
			<px:PXLayoutRule runat="server" />
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="900px" Style="z-index: 100" Width="100%" DataSourceID="ds">
        
 <Items>
 <px:PXTabItem Text="General Info">
                <Template>

                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM"></px:PXLayoutRule>

                    <px:PXFormView runat="server" TabIndex="1300" Caption="Main Contact" ID="DefContact" DataSourceID="ds" DataMember="DefContact" RenderStyle="Fieldset">
                        <Template>
                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM"></px:PXLayoutRule>

                            <px:PXTextEdit runat="server" DataField="FullName" ID="edFullName" DefaultLocale=""></px:PXTextEdit>

                            <px:PXTextEdit runat="server" DataField="Salutation" ID="edSalutation" DefaultLocale=""></px:PXTextEdit>

                            <px:PXMailEdit runat="server" CommitChanges="True" DataField="EMail" ID="edEMail" DefaultLocale=""></px:PXMailEdit>

                            <px:PXLinkEdit runat="server" CommitChanges="True" DataField="WebSite" ID="edWebSite" DefaultLocale=""></px:PXLinkEdit>

                            <px:PXMaskEdit runat="server" DataField="Phone1" ID="edPhone1" DefaultLocale=""></px:PXMaskEdit>

                            <px:PXMaskEdit runat="server" DataField="Phone2" ID="edPhone2" DefaultLocale=""></px:PXMaskEdit>

                            <px:PXMaskEdit runat="server" DataField="Fax" ID="edFax" DefaultLocale=""></px:PXMaskEdit>


                        </Template>
                    </px:PXFormView>

                    <px:PXTextEdit runat="server" DataField="AcctReferenceNbr" ID="edAcctReferenceNbr" DefaultLocale=""></px:PXTextEdit>

                    <px:PXFormView runat="server" TabIndex="1350" Caption="Main Address" RenderStyle="Fieldset" ID="DefAddress" SyncPosition="True" DataSourceID="ds" DataMember="DefAddress">
                        <Template>
                            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM"></px:PXLayoutRule>

                            <px:PXCheckBox runat="server" Enabled="False" DataField="IsValidated" ID="edIsValidated"></px:PXCheckBox>

                            <px:PXTextEdit runat="server" DataField="AddressLine1" ID="edAddressLine1" DefaultLocale=""></px:PXTextEdit>

                            <px:PXTextEdit runat="server" DataField="AddressLine2" ID="edAddressLine2" DefaultLocale=""></px:PXTextEdit>

                            <px:PXTextEdit runat="server" DataField="City" ID="edCity" DefaultLocale=""></px:PXTextEdit>

                            <px:PXSelector runat="server" DataField="CountryID" CommitChanges="True" ID="edCountryID"></px:PXSelector>

                            <px:PXSelector runat="server" DataField="State" AutoRefresh="True" AllowAddNew="True" ID="edState"></px:PXSelector>

                            <px:PXLayoutRule runat="server" Merge="True"></px:PXLayoutRule>

                            <px:PXMaskEdit runat="server" Size="s" CommitChanges="True" DataField="PostalCode" ID="edPostalCode" DefaultLocale=""></px:PXMaskEdit>

                            <px:PXButton runat="server" Text="View on Map" CommandSourceID="ds" CommandName="ViewMainOnMap" ID="btnViewMainOnMap"></px:PXButton>

                            <px:PXLayoutRule runat="server"></px:PXLayoutRule>


                        </Template>
                    </px:PXFormView>

                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="XM" LabelsWidth="SM"></px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" GroupCaption="Financial Settings" StartGroup="True"></px:PXLayoutRule>


                    <px:PXSelector runat="server" DataField="CustomerClassID" AllowEdit="True" CommitChanges="True" ID="edCustomerClassID" edit="1"></px:PXSelector>

                    <px:PXSelector runat="server" DataField="TermsID" AllowEdit="True" ID="edTermsID" edit="1"></px:PXSelector>

                    <px:PXSelector runat="server" DataField="StatementCycleId" AllowEdit="True" ID="edStatementCycleId" edit="1" CommitChanges="True"></px:PXSelector>

                    <px:PXCheckBox runat="server" DataField="AutoApplyPayments" ID="chkAutoApplyPayments" SuppressLabel="True"></px:PXCheckBox>

                    <px:PXCheckBox runat="server" CommitChanges="True" DataField="FinChargeApply" ID="chkFinChargeApply"></px:PXCheckBox>

                    <px:PXCheckBox runat="server" CommitChanges="True" DataField="SmallBalanceAllow" ID="chkSmallBalanceAllow"></px:PXCheckBox>

                    <px:PXNumberEdit runat="server" DataField="SmallBalanceLimit" ID="edSmallBalanceLimit" DefaultLocale=""></px:PXNumberEdit>

                    <px:PXLayoutRule runat="server" Merge="True"></px:PXLayoutRule>

                    <px:PXSelector runat="server" DataField="CuryID" Size="S" ID="edCuryID"></px:PXSelector>

                    <px:PXCheckBox runat="server" DataField="AllowOverrideCury" ID="chkAllowOverrideCury"></px:PXCheckBox>

                    <px:PXLayoutRule runat="server"></px:PXLayoutRule>

                    <px:PXLayoutRule runat="server" Merge="True"></px:PXLayoutRule>

                    <px:PXSelector runat="server" DataField="CuryRateTypeID" ID="edCuryRateTypeID" Size="S"></px:PXSelector>

                    <px:PXCheckBox runat="server" DataField="AllowOverrideRate" ID="chkAllowOverrideRate"></px:PXCheckBox>

                    <px:PXLayoutRule runat="server"></px:PXLayoutRule>

                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Credit Verification Rules"></px:PXLayoutRule>

                    <px:PXDropDown runat="server" CommitChanges="True" DataField="CreditRule" ID="edCreditRule"></px:PXDropDown>

                    <px:PXNumberEdit runat="server" CommitChanges="True" DataField="CreditLimit" ID="edCreditLimit" DefaultLocale=""></px:PXNumberEdit>

                    <px:PXNumberEdit runat="server" DataField="CreditDaysPastDue" ID="edCreditDaysPastDue" DefaultLocale=""></px:PXNumberEdit>

                    <px:PXFormView runat="server" RenderStyle="Simple" ID="CustomerBalance" DataSourceID="ds" DataMember="CustomerBalance" TabIndex="500">
                        <Template>
                            <px:PXLayoutRule runat="server" ControlSize="SM" LabelsWidth="SM"></px:PXLayoutRule>
                            <px:PXNumberEdit runat="server" Enabled="False" DataField="UnreleasedBalance" ID="edUnreleasedBalance" DefaultLocale=""></px:PXNumberEdit>

                            <px:PXNumberEdit runat="server" Enabled="False" DataField="OpenOrdersBalance" ID="edOpenOrdersBalance" DefaultLocale=""></px:PXNumberEdit>

                            <px:PXNumberEdit runat="server" Enabled="False" DataField="RemainingCreditLimit" ID="edRemainingCreditLimit" DefaultLocale=""></px:PXNumberEdit>

                            <px:PXDateTimeEdit runat="server" Enabled="False" DataField="OldInvoiceDate" ID="edOldInvoiceDate" DefaultLocale=""></px:PXDateTimeEdit>


                        </Template>
                    </px:PXFormView>

                    <px:PXLayoutRule runat="server" StartColumn="True"></px:PXLayoutRule>


                   
                    <px:PXFormView ID="PXFormView1" runat="server" DataMember="Aging" DataSourceID="ds" TabIndex="11200" Caption="Aging" RenderStyle="Fieldset">
                        <Template>
                            <px:PXNumberEdit ID="edCurrent" runat="server" DataField="Current" LabelWidth="180px" DefaultLocale="">
                            </px:PXNumberEdit>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXTextEdit ID="edCDays1" runat="server" DataField="CDays1" SuppressLabel="True" TextAlign="Left" DefaultLocale="">
                            </px:PXTextEdit>
                            <px:PXNumberEdit ID="edDays1" runat="server" DataField="Days1" SuppressLabel="True" DefaultLocale="">
                            </px:PXNumberEdit>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXTextEdit ID="edCDays2" runat="server" DataField="CDays2" SuppressLabel="True" TextAlign="Left" DefaultLocale="">
                            </px:PXTextEdit>
                            <px:PXNumberEdit ID="edDays2" runat="server" DataField="Days2" SuppressLabel="True" DefaultLocale="">
                            </px:PXNumberEdit>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXTextEdit ID="edCDays3" runat="server" DataField="CDays3" SuppressLabel="True" TextAlign="Left" DefaultLocale="">
                            </px:PXTextEdit>
                            <px:PXNumberEdit ID="edDays3" runat="server" DataField="Days3" SuppressLabel="True" DefaultLocale="">
                            </px:PXNumberEdit>
                            <px:PXLayoutRule runat="server" Merge="True">
                            </px:PXLayoutRule>
                            <px:PXTextEdit ID="edCOver" runat="server" DataField="COver" SuppressLabel="True" TextAlign="Left" DefaultLocale="">
                            </px:PXTextEdit>
                            <px:PXNumberEdit ID="edOver" runat="server" DataField="Over" LabelWidth="180px" SuppressLabel="True" DefaultLocale="">
                            </px:PXNumberEdit>
                            <px:PXLayoutRule runat="server" StartRow="True">
                            </px:PXLayoutRule>
                            <px:PXNumberEdit ID="edTotal" runat="server" DataField="Total" LabelWidth="180px" DefaultLocale="">
                            </px:PXNumberEdit>
                        </Template>
                    </px:PXFormView>


                   
                    <px:PXGrid runat="server" Height="200px" TabIndex="1200" Width="605px" ID="PXGrid1" Caption="Recent Payments" TemporaryFilterCaption="Filter Applied" DataSourceID="ds" SkinID="DetailsInTab" RepaintColumns="True" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataMember="Payments">
                                <RowTemplate>

                                    <px:PXDropDown runat="server" DataField="Status" ID="edStatus"></px:PXDropDown>
                                    <px:PXDateTimeEdit runat="server" DataField="DocDate" ID="edDocDate" DefaultLocale=""></px:PXDateTimeEdit>
                                    <px:PXDateTimeEdit runat="server" DataField="DueDate" ID="edDueDate" DefaultLocale=""></px:PXDateTimeEdit>
                                    <px:PXNumberEdit runat="server" DataField="CuryOrigDocAmt" ID="edCuryOrigDocAmt" DefaultLocale=""></px:PXNumberEdit>
                                    <px:PXNumberEdit runat="server" DataField="CuryDocBal" ID="edCuryDocBal" DefaultLocale=""></px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="RefNbr" LinkCommand="ViewPayment"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="DocDate" Width="90px"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="DueDate" Width="90px"></px:PXGridColumn>
                                    <px:PXGridColumn TextAlign="Right" DataField="CuryDocBal" Width="100px"></px:PXGridColumn>
                                    <px:PXGridColumn TextAlign="Right" DataField="CuryOrigDocAmt" Width="100px"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
                    </px:PXGrid>



                    <px:PXLayoutRule runat="server" StartRow="True"></px:PXLayoutRule>



                    <px:PXGrid runat="server" SyncPosition="True" SkinID="DetailsInTab" TabIndex="25964" Width="100%" ID="grid0" Caption="Promises to pay" TemporaryFilterCaption="Filter Applied" DataSourceID="ds" AllowPaging="True" AdjustPageSize="Auto" Style="left: 0px; top: 0px; height: 100px" Height="100px">
                        <Levels>
                            <px:PXGridLevel DataMember="Promises">
                                <RowTemplate>
                                    <px:PXSelector runat="server" DataField="PromiseID" ID="edPromiseID"></px:PXSelector>

                                    <px:PXTextEdit runat="server" DataField="Descr" ID="edDescr" DefaultLocale=""></px:PXTextEdit>
                                    <px:PXDateTimeEdit runat="server" DataField="PromiseDate" ID="edPromiseDate" DefaultLocale=""></px:PXDateTimeEdit>
                                    <px:PXDropDown runat="server" DataField="Status" ID="eddStatus"></px:PXDropDown>
                                    <px:PXNumberEdit runat="server" DataField="TotPaid" ID="edTotPaid" DefaultLocale=""></px:PXNumberEdit>
                                    <px:PXNumberEdit runat="server" DataField="TotPromised" ID="edTotPromised" DefaultLocale=""></px:PXNumberEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="CustomerID" Width="120px" TextAlign="Center"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="PromiseID" TextAlign="Center" LinkCommand="ViewPromise"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px" TextAlign="Center"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="PromiseDate" Width="110px" TextAlign="Center"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status" TextAlign="Center"></px:PXGridColumn>
                                    <px:PXGridColumn TextAlign="Center" DataField="TotPromised" Width="110px"></px:PXGridColumn>
                                    <px:PXGridColumn TextAlign="Center" DataField="TotPaid" Width="110px"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
                    </px:PXGrid>

                    <px:PXGrid runat="server" TabIndex="4500" ID="PXGrid2" TemporaryFilterCaption="Filter Applied" DataSourceID="ds" Height="300px" Caption="Aging" SkinID="DetailsInTab" Width="100%" RepaintColumns="True">
                        <Levels>
                            <px:PXGridLevel DataMember="Aging">
                                <RowTemplate>
                                    <px:PXSelector runat="server" DataField="ARInvoice__RefNbr" ID="edARInvoice__RefNbr"></px:PXSelector>
                                    <px:PXDropDown runat="server" DataField="ARInvoice__DocType" ID="edARInvoice__DocType"></px:PXDropDown>
                                    <px:PXDateTimeEdit runat="server" DataField="ARInvoice__DocDate" ID="edARInvoice__DocDate" DefaultLocale=""></px:PXDateTimeEdit>
                                    <px:PXNumberEdit runat="server" DataField="ARInvoice__CuryOrigDocAmt" ID="edARInvoice__CuryOrigDocAmt" DefaultLocale=""></px:PXNumberEdit><px:PXNumberEdit runat="server" DataField="ARInvoice__CuryDocBal" ID="edARInvoice__CuryDocBal" DefaultLocale=""></px:PXNumberEdit>

                                    
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="ARInvoice__RefNbr"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ARInvoice__DocType"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ARInvoice__DocDate" Width="90px"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ARInvoice__CuryOrigDocAmt" TextAlign="Right" Width="100px"></px:PXGridColumn>
                                    <px:PXGridColumn TextAlign="Right" DataField="ARInvoice__CuryDocBal" Width="100px"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="ARInvoice__DueDate" Width="90px"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>


                </Template>
            </px:PXTabItem>
 <px:PXTabItem Text="Contacts">
                <Template>
                    <px:PXLayoutRule runat="server" StartRow="True"></px:PXLayoutRule>
                    <px:PXGrid runat="server" TabIndex="700" ID="grdContacts" TemporaryFilterCaption="Filter Applied" DataSourceID="ds" SkinID="DetailsInTab" Width="100%" Height="400px" Caption="Contacts" ActionsPosition="Top" AllowSearch="True" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="ContactID" DataMember="ConnectedContacts">
                               <RowTemplate>
                                    <px:PXLayoutRule runat="server">
                                    </px:PXLayoutRule>
                                    <px:PXTextEdit ID="edFirstName" runat="server" DataField="FirstName" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edLastName" runat="server" DataField="LastName" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edAddress__AddressLine1" runat="server" DataField="Address__AddressLine1" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edAddress__AddressLine2" runat="server" DataField="Address__AddressLine2" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edAddress__City" runat="server" DataField="Address__City" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXSelector ID="edAddress__State" runat="server" DataField="Address__State">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="edAddress__PostalCode" runat="server" DataField="Address__PostalCode" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edEMail" runat="server" DataField="EMail" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXTextEdit ID="edPhone1" runat="server" DataField="Phone1" DefaultLocale="">
                                    </px:PXTextEdit>
                                    <px:PXDropDown ID="edPhone1Type" runat="server" DataField="Phone1Type">
                                    </px:PXDropDown>
                                    <px:PXDropDown ID="edGender" runat="server" DataField="Gender">
                                    </px:PXDropDown>
                                    <px:PXDateTimeEdit ID="edBirthday" runat="server" DataField="Birthday" DefaultLocale="">
                                    </px:PXDateTimeEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="FirstName" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LastName" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__AddressLine1" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__AddressLine2" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__City" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__State" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address__PostalCode" Width="80px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EMail" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Phone1" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Phone1Type">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Gender">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Birthday" Width="90px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinWidth="100"></AutoSize>

                        <ActionBar DefaultAction="cmdViewContact">
                            <Actions>
                                <Save Enabled="False"></Save>

                                <AddNew Enabled="False"></AddNew>

                                <Delete Enabled="False"></Delete>

                                <EditRecord Enabled="False"></EditRecord>
                            </Actions>
                            <CustomItems>
                                <px:PXToolBarButton Text="Remove Contact" SuppressHtmlEncoding="False">
                                    <AutoCallBack Target="ds" Command="RemoveContact"></AutoCallBack>

                                    <PopupCommand Target="grdContacts" Command="Refresh"></PopupCommand>
                                </px:PXToolBarButton>
                            </CustomItems>
                            <CustomItems>
                                <px:PXToolBarButton Text="Add Contact" SuppressHtmlEncoding="False">
                                    <AutoCallBack Target="ds" Command="AddContact"></AutoCallBack>

                                    <PopupCommand Target="grdContacts" Command="Refresh"></PopupCommand>
                                </px:PXToolBarButton>
                            </CustomItems>
                            
                            <CustomItems>
                                <px:PXToolBarButton Text="Add New Contact" SuppressHtmlEncoding="False">
                                    <AutoCallBack Target="ds" Command="AddNewContact"></AutoCallBack>

                                    <PopupCommand Target="grdContacts" Command="Refresh"></PopupCommand>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>

                        <Mode AllowAddNew="False" AllowUpdate="False" AllowDelete="False" AllowColMoving="False"></Mode>

                        <LevelStyles>
                            <RowForm Height="500px" Width="920px"></RowForm>
                        </LevelStyles>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
 </Items>

 <AutoSize Enabled="True" Container="Window" MinWidth="300" MinHeight="250"></AutoSize>
		<Activity HighlightColor="" SelectedColor="" Width="" Height=""></Activity>
		
 </px:PXTab>
    <px:PXSmartPanel ID="popupAddContact" runat="server" CaptionVisible="true" Caption="Add Contact"
        Height="89px" Width="713px" DesignView="Content" AutoCallBack-Command="Refresh"
        AutoCallBack-Enabled="True" AutoCallBack-Target="grdContacts" Key="AddContactID"
        AutoReload="True" AutoRepaint="True">
        <px:PXFormView ID="frmContactID" runat="server" DataSourceID="ds" Style="z-index: 100"
            Width="100%" DataMember="AddContactID" TabIndex="16364">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="S"
                    ControlSize="SM">
                </px:PXLayoutRule>
                <px:PXSelector CommitChanges="True" ID="edContactID" runat="server" DataField="ContactID"
                    DataSourceID="ds">
                </px:PXSelector>
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="pnlAddContactOK" runat="server" SkinID="Buttons">
            <px:PXButton ID="btnAddConteactOK" runat="server" DialogResult="OK" Text="OK">
            </px:PXButton>
        </px:PXPanel>
    </px:PXSmartPanel>
</asp:Content>
