<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="XMQ23000.aspx.cs" Inherits="Page_XMQ23000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="MaxQ.Products.Billing.SvcParmsMaint"
        PrimaryView="ClientParameters" SuspendUnloading="False">
        <CallbackCommands>
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True">
            </px:PXDSCallbackCommand>
            <px:PXDSCallbackCommand Name="Last" PostData="Self">
            </px:PXDSCallbackCommand>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
        DataMember="ClientParameters" TabIndex="14864">
        <Template>
            <px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="SM" StartColumn="True"
                StartRow="True">
            </px:PXLayoutRule>
            <px:PXSelector ID="edBAccountID" runat="server" CommitChanges="True" DataField="BAccountID">
            </px:PXSelector>
            <px:PXSelector ID="edSvcParmCD" runat="server" CommitChanges="True" DataField="SvcParmCD">
            </px:PXSelector>
            <px:PXLayoutRule runat="server" ControlSize="XL">
            </px:PXLayoutRule>
            <px:PXTextEdit runat="server" DataField="Descr" ID="edDescr">
            </px:PXTextEdit>
            <px:PXDropDown ID="edStatus" runat="server" DataField="Status" Size="S">
            </px:PXDropDown>
            <px:PXLayoutRule runat="server" ControlSize="M" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXTextEdit runat="server" DataField="UniverseID" ID="edUniverseID">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edDropDescr" runat="server" DataField="DropDescr">
            </px:PXTextEdit>
            <px:PXTextEdit ID="edShortDropDescr" runat="server" DataField="ShortDropDescr">
            </px:PXTextEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="412px" DataSourceID="ds" DataMember="CurrentClientParms">
        <Items>
            <px:PXTabItem Text="Parameters">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXCheckBox ID="chkAllowRenewals" runat="server" DataField="AllowRenewals">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkAutoCancel" runat="server" DataField="AutoCancel">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkRenewalLetters" runat="server" DataField="RenewalLetters">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkSpcDrftLastPmt" runat="server" DataField="SpcDrftLastPmt">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkWelcomeLetters" runat="server" DataField="WelcomeLetters">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkReclearLastPmt" runat="server" DataField="ReclearLastPmt">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkFDCPAOnWelc" runat="server" DataField="FDCPAOnWelc">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkExitFeeBnkRpt" runat="server" DataField="ExitFeeBnkRpt">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkLateLetters" runat="server" DataField="LateLetters">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkEFTDraft" runat="server" DataField="EFTDraft">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkLoadNewAccts" runat="server" DataField="LoadNewAccts">
                    </px:PXCheckBox>
                    <px:PXDropDown ID="edBureauRpt" runat="server" AllowNull="False" DataField="BureauRpt"
                        SelectedIndex="-1">
                    </px:PXDropDown>
                    <px:PXCheckBox ID="chkEOMInterest" runat="server" DataField="EOMInterest">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkPhoneCollect" runat="server" DataField="PhoneCollect">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkPICollect" runat="server" DataField="PICollect">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkMemberCards" runat="server" DataField="MemberCards">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkExtCollect" runat="server" DataField="ExtCollect">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkActivateAccts" runat="server" DataField="ActivateAccts">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkMailBillType" runat="server" DataField="MailBillType">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkAlwaysRepurge" runat="server" DataField="AlwaysRepurge">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edNotRenewableDflt" runat="server" DataField="NotRenewableDflt"
                        Text="Cash/Check">
                    </px:PXCheckBox>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edPhase3BalAddon" runat="server" DataField="Phase3BalAddon">
                    </px:PXNumberEdit>
                    <px:PXCheckBox ID="chkPIFWelcome" runat="server" DataField="PIFWelcome">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkSendCIR" runat="server" DataField="SendCIR">
                    </px:PXCheckBox>
                    <px:PXSelector ID="edDfltInventoryID" runat="server" 
                        DataField="DfltInventoryID">
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Client Ranges">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edHighNote" runat="server" DataField="HighNote">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edLowNote" runat="server" DataField="LowNote">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edHighPmt" runat="server" DataField="HighPmt">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edLowPmt" runat="server" DataField="LowPmt">
                    </px:PXNumberEdit>
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Customer Phase Change Timing">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edExtDaysLate" runat="server" DataField="ExtDaysLate">
                    </px:PXNumberEdit>
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Trust Information"
                        LabelsWidth="XM">
                    </px:PXLayoutRule>
                    <px:PXTextEdit ID="edACHCode" runat="server" DataField="ACHCode">
                    </px:PXTextEdit>
                    <px:PXNumberEdit ID="edMinTrust" runat="server" DataField="MinTrust">
                    </px:PXNumberEdit>
                    <px:PXDropDown ID="edTrustType" runat="server" DataField="TrustType">
                    </px:PXDropDown>
                    <px:PXNumberEdit ID="edMaxExitFee" runat="server" DataField="MaxExitFee">
                    </px:PXNumberEdit>
                    <px:PXLayoutRule runat="server" GroupCaption="Allowed Contract Payment Methods" StartGroup="True">
                    </px:PXLayoutRule>
                    <px:PXCheckBox ID="edAllowCashCheck" runat="server" DataField="AllowCashCheck" Text="Allow Cash/Check Payments">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edAllowCreditCard" runat="server" DataField="AllowCreditCard"
                        Text="Allow Credit Card Payments">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edAllowDirectDeposit" runat="server" DataField="AllowDirectDeposit"
                        Text="Allow Direct Deposit Payments">
                    </px:PXCheckBox>
                    <px:PXLayoutRule runat="server" GroupCaption="Allowed Contract Types" StartColumn="True"
                        StartGroup="True">
                    </px:PXLayoutRule>
                    <px:PXCheckBox ID="edAllowTerm" runat="server" DataField="AllowTerm" Text="Term">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edAllowTermToPerpet" runat="server" DataField="AllowTermToPerpet"
                        Text="Term to Perpetual">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="edAllowPerpetual" runat="server" DataField="AllowPerpetual" Text="Perpetual">
                    </px:PXCheckBox>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Client Switches">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXTextEdit ID="edBussinesType" runat="server" DataField="BussinesType">
                    </px:PXTextEdit>
                    <px:PXDateTimeEdit ID="edPaidDate" runat="server" DataField="PaidDate">
                    </px:PXDateTimeEdit>
                    <px:PXCheckBox ID="chkClientManSvcs" runat="server" DataField="ClientManSvcs">
                    </px:PXCheckBox>
                    <px:PXDateTimeEdit ID="edLateEffDate" runat="server" DataField="LateEffDate">
                    </px:PXDateTimeEdit>
                    <px:PXDateTimeEdit Size="s" ID="edLastPaperDate" runat="server" DataField="LastPaperDate">
                    </px:PXDateTimeEdit>
                    <px:PXCheckBox ID="chkDownloadClient" runat="server" DataField="DownloadClient">
                    </px:PXCheckBox>
                    <px:PXTextEdit ID="edSoftware" runat="server" DataField="Software">
                    </px:PXTextEdit>
                    <px:PXCheckBox ID="chkSpecialSvcs" runat="server" DataField="SpecialSvcs">
                    </px:PXCheckBox>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM">
                    </px:PXLayoutRule>
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Other Fees/Services">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edCCDiscPct" runat="server" DataField="CCDiscPct">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edAMXDiscPct" runat="server" DataField="AMXDiscPct">
                    </px:PXNumberEdit>
                    <px:PXNumberEdit ID="edMaxSlide" runat="server" DataField="MaxSlide">
                    </px:PXNumberEdit>
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Other Fees/Services">
                    </px:PXLayoutRule>
                    <px:PXNumberEdit ID="edRenewGraceDays" runat="server" DataField="RenewGraceDays">
                    </px:PXNumberEdit>
                    <px:PXTextEdit ID="edExitType" runat="server" DataField="ExitType">
                    </px:PXTextEdit>
                    <px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Reports/Letters">
                    </px:PXLayoutRule>
                    <px:PXCheckBox ID="chkCustomWelcLet" runat="server" DataField="CustomWelcLet">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkPaymentDet" runat="server" DataField="PaymentDet">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkReturnLetters" runat="server" DataField="ReturnLetters">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkAutoChrgbkCIR" runat="server" DataField="AutoChrgbkCIR">
                    </px:PXCheckBox>
                    <px:PXCheckBox ID="chkEOMReports" runat="server" DataField="EOMReports">
                    </px:PXCheckBox>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Billing Profile">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="SM">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="MasterProfileID" runat="server" DataField="MasterProfileID" DataSourceID="ds"
                        CommitChanges="True" AllowEdit="True" Size="XL" edit="1">
                    </px:PXSelector>
                    <px:PXCheckBox ID="LinkedToMaster" runat="server" DataField="LinkedToMaster" CommitChanges="True">
                    </px:PXCheckBox>
                    <px:PXGrid ID="grdBillingProfile" runat="server" DataSourceID="ds" Style="z-index: 100;"
                        Width="100%" Height="300px" SkinID="DetailsInTab" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataMember="BillingProfile" DataKeyNames="SvcParmID,BillProfileLineID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXSelector ID="ActivityID" runat="server" DataField="ActivityID" CommitChanges="True">
                                    </px:PXSelector>
                                    <px:PXTextEdit ID="Descr" runat="server" DataField="Descr">
                                    </px:PXTextEdit>
                                    <px:PXCheckBox ID="Active" runat="server" DataField="Active">
                                    </px:PXCheckBox>
                                    <px:PXTextEdit ID="ChargeType" runat="server" DataField="ChargeType" CommitChanges="True">
                                    </px:PXTextEdit>
                                    <px:PXNumberEdit ID="Fee" runat="server" DataField="Fee" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="Pct" runat="server" DataField="Pct">
                                    </px:PXNumberEdit>
                                    <px:PXDateTimeEdit ID="edBegDate" runat="server" DataField="BegDate" CommitChanges="True">
                                    </px:PXDateTimeEdit>
                                    <px:PXLayoutRule runat="server" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXDateTimeEdit ID="edEndDate" runat="server" DataField="EndDate" CommitChanges="True">
                                    </px:PXDateTimeEdit>
                                    <px:PXDropDown ID="edActivityType" runat="server" DataField="ActivityType" CommitChanges="True">
                                    </px:PXDropDown>
                                    <px:PXDropDown ID="edSchedule" runat="server" DataField="Schedule" CommitChanges="True">
                                    </px:PXDropDown>
                                    <px:PXNumberEdit ID="edDayNumber" runat="server" DataField="DayNumber" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edMinCharge" runat="server" DataField="MinCharge" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXNumberEdit ID="edMaxCharge" runat="server" DataField="MaxCharge" CommitChanges="True">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edLateID" runat="server" DataField="LateID">
                                    </px:PXSelector>
                                    <px:PXLayoutRule runat="server" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXNumberEdit ID="edLimit" runat="server" DataField="Limit">
                                    </px:PXNumberEdit>
                                    <px:PXDropDown ID="edInvcMethod" runat="server" DataField="InvcMethod">
                                    </px:PXDropDown>
                                    <px:PXDateTimeEdit ID="edDateLastProcessed" runat="server" DataField="DateLastProcessed">
                                    </px:PXDateTimeEdit>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="ActivityID" Width="200px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Descr" Width="200px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Active" Width="60px" TextAlign="Center" Type="CheckBox">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ChargeType" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Fee" TextAlign="Right" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Pct" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="BegDate" Width="90px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="EndDate" Width="90px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="ActivityType" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Schedule" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="DayNumber" TextAlign="Right" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="MinCharge" TextAlign="Right" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="MaxCharge" TextAlign="Right" Width="100px" CommitChanges="True">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="LateID">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Limit" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="InvcMethod">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="DateLastProcessed" Width="90px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <Mode AllowFormEdit="True"></Mode>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Trust Split">
                <Template>
                    <px:PXGrid ID="grdTrustSplit" runat="server" DataSourceID="ds" Style="z-index: 100;"
                        Width="100%" Height="300px" SkinID="DetailsInTab" SyncPosition="True">
                        <Levels>
                            <px:PXGridLevel DataMember="TrustSplit" DataKeyNames="SvcParmID,VendorID">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True">
                                    </px:PXLayoutRule>
                                    <px:PXNumberEdit ID="edSequence" runat="server" DataField="Sequence">
                                    </px:PXNumberEdit>
                                    <px:PXSelector ID="edVendorID" runat="server" DataField="VendorID">
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edPct" runat="server" DataField="Pct">
                                    </px:PXNumberEdit>
                                    <px:PXDropDown ID="edSplitType" runat="server" DataField="SplitType">
                                    </px:PXDropDown>
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="Sequence" TextAlign="Right" Width="90px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="VendorID" Width="120px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="Pct" TextAlign="Right" Width="100px">
                                    </px:PXGridColumn>
                                    <px:PXGridColumn DataField="SplitType" Width="90px">
                                    </px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150"></AutoSize>
    </px:PXTab>
</asp:Content>
