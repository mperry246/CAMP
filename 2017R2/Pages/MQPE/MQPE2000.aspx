<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
ValidateRequest="false" CodeFile="MQPE2000.aspx.cs" Inherits="Page_MQPE2000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
TypeName="MaxQ.Products.ProcessEngine.MQPETransactionMaint" PrimaryView="Transaction" >
		<CallbackCommands>
		</CallbackCommands>		
	</px:PXDataSource>
</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Transaction" TabIndex="-23836">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
			<px:PXSelector ID="edTransactionID" runat="server" DataField="TransactionID" DataSourceID="ds" Size="SM" CommitChanges="True" >
			</px:PXSelector>
			<px:PXTextEdit ID="edDescription" runat="server" DataField="Description" />
			<px:PXDropDown ID="edStatus" runat="server" DataField="Status" />
			<px:PXTextEdit ID="edSubmitDate" runat="server" DataField="SubmitDate" />
			<px:PXTextEdit ID="edProcessDate" runat="server" DataField="ProcessDate" />
			<px:PXNumberEdit ID="edPriority" runat="server" DataField="Priority" />
			<px:PXNumberEdit ID="edAttempt" runat="server" DataField="Attempt" />
			<px:PXDropDown ID="edHandlerType" runat="server" DataField="HandlerType" />
			<px:PXTextEdit ID="edHandlerName" runat="server" DataField="HandlerName"  Width="100%"  />
			<px:PXLayoutRule runat="server" EndGroup="True" />
			<px:PXLayoutRule runat="server" StartRow="True" />
			<px:PXTab ID="tabDetailInfo" runat="server" Width="100%" Height="150px" DataSourceID="ds" DataMember="CurrentTransaction">
				<Items>
					<px:PXTabItem Text="Message Details">
						<Template>
							<px:PXDropDown ID="edMessageType" runat="server" DataField="MessageType" />
							<px:PXRichTextEdit ID="edMessage" runat="server" DataField="Message" TextMode="MultiLine" AllowAttached="false" AllowImageEditor="false" AllowInsertParameter="false" AllowLinkEditor="false" AllowMacros="true" AllowPlaceholders="false" AllowSearch="true" AllowLoadTemplate="false">
								<AutoSize Enabled="True"/>
							</px:PXRichTextEdit>
						</Template>
					</px:PXTabItem>
					<px:PXTabItem Text="Error Details">
						<Template>
							<px:PXTextEdit ID="edErrorDescription" runat="server" DataField="ErrorDescription" Width="100%" />
							<px:PXTextEdit ID="edErrorSource" runat="server" DataField="ErrorSource"  Width="100%" />
							<px:PXRichTextEdit ID="edErrorStack" runat="server" DataField="ErrorStack" TextMode="MultiLine" AllowAttached="false" AllowImageEditor="false" AllowInsertParameter="false" AllowLinkEditor="false" AllowMacros="true" AllowPlaceholders="false" AllowSearch="true" AllowLoadTemplate="false">
								<AutoSize Enabled="True" />
							</px:PXRichTextEdit>
						</Template>
					</px:PXTabItem>
					<px:PXTabItem Text="Logs">
						<Template>
							<px:PXGrid ID="grid" runat="server" Height="100%" Width="100%" 
AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds"  AutoRefresh="True"
SkinID="Inquire" SyncPosition="True">
								<Levels>
									<px:PXGridLevel DataKeyNames="TransactionID,LogID" DataMember="Logs">
										<RowTemplate>
											<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
											<px:PXTextEdit ID="edTranDate" runat="server" DataField="TransactionDate" />
											<px:PXDropDown  ID="edAction" runat="server" AllowNull="False" DataField="Action" Size="SM" />
											<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" GroupCaption="Transaction" />
											<px:PXTextEdit ID="edDescription" runat="server" DataField="Description" />
											<px:PXDropDown  ID="edStatus" runat="server" AllowNull="False" DataField="Status" Size="SM" />
											<px:PXLayoutRule runat="server" GroupCaption="Handler" />
											<px:PXTextEdit ID="edHandlerName" runat="server" DataField="HandlerName" Width="500px" />
											<px:PXLayoutRule runat="server" GroupCaption="Error Message" />
											<px:PXTextEdit ID="edErrMessage" runat="server" DataField="ErrorDescription" TextMode="MultiLine" Width="575px" />
											<px:PXTextEdit ID="edErrSource" runat="server" DataField="ErrorSource"  Width="575px" />
											<px:PXTextEdit ID="edErrStack" runat="server" DataField="ErrorStack" TextMode="MultiLine" Width="575px" >
												<AutoSize Enabled="True" MinHeight="100" />
											</px:PXTextEdit>
											<px:PXTextEdit ID="edMessage" runat="server" DataField="Message" Width="575px" Height="375px" TextMode="MultiLine" />
										</RowTemplate>
										<Columns>
											<px:PXGridColumn DataField="TransactionDate" Width="130px"  TextAlign="Right"/>
											<px:PXGridColumn DataField="Icon" Width="25px" />
											<px:PXGridColumn DataField="Action" Width="100px" />
											<px:PXGridColumn DataField="Status" Width="100px" />
											<px:PXGridColumn DataField="Description" Width="200px" />
											<px:PXGridColumn DataField="ErrorDescription" Width="500px" />
										</Columns>
									</px:PXGridLevel>
								</Levels>
								<AutoSize Container="Window" Enabled="True" MinHeight="400" />
							<Mode AllowAddNew="False" AllowFormEdit="True" AllowDelete="False" />
							</px:PXGrid>
						</Template>
					</px:PXTabItem>
				</Items>
				<AutoSize Container="Window" Enabled="True" MinHeight="150" />
			</px:PXTab>
		</Template>
	</px:PXFormView>
</asp:Content>

