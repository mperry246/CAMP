<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="DR202000.aspx.cs" Inherits="Page_DR202000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="deferredcode" TypeName="PX.Objects.DR.DeferredCodeMaint">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="deferredcode" 
        Caption="Deferral Code" NoteIndicator="True" MarkRequired="Dynamic"
        FilesIndicator="True" ActivityIndicator="True" ActivityField="NoteActivity" TabIndex="-10836">
        <AutoSize Container="Window" Enabled="True" MinHeight="200" ></AutoSize>
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" ></px:PXLayoutRule>
            <px:PXSelector ID="edDeferredCodeID" runat="server" DataField="DeferredCodeID" DataSourceID="ds" ></px:PXSelector>
            <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" ValidateRequestMode="Inherit" ></px:PXTextEdit>
			<px:PXCheckBox ID="edMDA" runat="server" DataField="MultiDeliverableArrangement" CommitChanges="True" ValidateRequestMode="Inherit" ></px:PXCheckBox>
            <px:PXDropDown CommitChanges="True" ID="edMethod" runat="server" AllowNull="False" DataField="Method" ></px:PXDropDown>
            <px:PXCheckBox ID="edRecognizeInPastPeriods" runat="server" DataField="RecognizeInPastPeriods">
            </px:PXCheckBox>
            <px:PXNumberEdit ID="edReconNowPct" runat="server" DataField="ReconNowPct" ValidateRequestMode="Inherit" ></px:PXNumberEdit>
            <px:PXNumberEdit ID="edStartOffset" runat="server" DataField="StartOffset" ValidateRequestMode="Inherit" ></px:PXNumberEdit>
            <px:PXNumberEdit ID="edOccurrences" runat="server" DataField="Occurrences" ValidateRequestMode="Inherit" ></px:PXNumberEdit>
            <px:PXDropDown ID="edAccountType" runat="server" AllowNull="False" DataField="AccountType" CommitChanges="True" ></px:PXDropDown>
			<px:PXDropDown ID="edAccountSource" runat="server" AllowNull="False" DataField="AccountSource" CommitChanges="True" ></px:PXDropDown>
			<px:PXSegmentMask ID="edDeferralSubMaskAR" runat="server" DataField="DeferralSubMaskAR" ></px:PXSegmentMask>
			<px:PXSegmentMask ID="edDeferralSubMaskAP" runat="server" DataField="DeferralSubMaskAP" ></px:PXSegmentMask>
			<px:PXCheckBox ID="edCopySub" runat="server" DataField="CopySubFromSourceTran" CommitChanges="True" ValidateRequestMode="Inherit" ></px:PXCheckBox>
            <px:PXSegmentMask CommitChanges="True" ID="edAccountID" runat="server" DataField="AccountID" 
                              DataSourceID="ds" ></px:PXSegmentMask>
            <px:PXSegmentMask ID="edSubID" runat="server" DataField="SubID" DataSourceID="ds" ></px:PXSegmentMask>
	<px:PXSelector runat="server" ID="CstPXSelector1" DataField="MilestoneID" />
            <px:PXLayoutRule runat="server" LabelsWidth="XS" ControlSize="M" StartColumn="True" ></px:PXLayoutRule>
			<px:PXLabel runat="server" ValidateRequestMode="Inherit" ></px:PXLabel>
			<px:PXLabel runat="server" ValidateRequestMode="Inherit" ></px:PXLabel>
			<px:PXLabel runat="server" ValidateRequestMode="Inherit" ></px:PXLabel>
			<px:PXLayoutRule runat="server" GroupCaption="Schedule Settings" ></px:PXLayoutRule>
            <px:PXLayoutRule runat="server" Merge="True" ></px:PXLayoutRule>
            <px:PXNumberEdit Size="xxs" ID="edFrequency" runat="server" DataField="Frequency" ValidateRequestMode="Inherit" ></px:PXNumberEdit>
			<px:PXTextEdit runat="server" ID="edPeriods" DataField="Periods"  Enabled="False" Width="0px" LabelWidth="100px" LabelPostfix="" ValidateRequestMode="Inherit" ></px:PXTextEdit>
            <px:PXLayoutRule runat="server" ></px:PXLayoutRule>
            <px:PXGroupBox CommitChanges="True" RenderStyle="Fieldset" ID="gbPeriodically" runat="server" Caption="Document Date Selection" DataField="ScheduleOption" ValidateRequestMode="Inherit">
                <Template>
                    <px:PXRadioButton Size="xm" ID="rbStartOfPeriod" runat="server" Value="S" Text="Start of Financial Period" ></px:PXRadioButton>
                    <px:PXRadioButton ID="rbEndOfPeriod" runat="server" Value="E" Text="End of Financial Period" ></px:PXRadioButton>
                    <px:PXLayoutRule runat="server" Merge="True" ></px:PXLayoutRule>
                    <px:PXRadioButton Size="" ID="rbFixedDay" runat="server" Value="D" Text="Fixed Day of the Period" ></px:PXRadioButton>
                    <px:PXNumberEdit SuppressLabel="True" Size="xxs" ID="edFixedDay" runat="server" DataField="FixedDay" ValidateRequestMode="Inherit" ></px:PXNumberEdit>
                    <px:PXLayoutRule runat="server" ></px:PXLayoutRule>
                </Template>
            </px:PXGroupBox></Template>
    </px:PXFormView>
</asp:Content>
