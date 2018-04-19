<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="MQPE4100.aspx.cs" Inherits="Page_MQPE4100"
    Title="Untitled Page" Debug="true" %>

	<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>
		<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
			<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
		TypeName="MaxQ.Products.ProcessEngine.MQPE4100" PrimaryView="Header">
				<CallbackCommands>
				</CallbackCommands>		
			</px:PXDataSource>
		</asp:Content>

		<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
			<px:PXGrid ID="grid" runat="server" Height="100%" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" 
        SkinID="Inquire" TabIndex="100" SyncPosition="true">
				<Levels>
					<px:PXGridLevel DataKeyNames="LogID" DataMember="Header">
						<RowTemplate>
							<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
							<px:PXTextEdit ID="edTranDate" runat="server" DataField="TransactionDate" />
							<px:PXDropDown  ID="edAction" runat="server" AllowNull="False" DataField="Action" Size="SM" />

							<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" GroupCaption="Transaction" />
<px:PXSelector ID="edTransactionID" runat="server" DataField="TransactionID" 
                        AllowEdit="True" edit="1">
                    </px:PXSelector>
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

							<px:PXGridColumn DataField="TransactionID" Width="75px" TextAlign="Right" />
							<px:PXGridColumn DataField="SubmitDate" Width="130px"  TextAlign="Right"/>
							<px:PXGridColumn DataField="Status" Width="100px" />
							<px:PXGridColumn DataField="Description" Width="200px" />
							<px:PXGridColumn DataField="ErrorDescription" Width="500px" />
						</Columns>
					</px:PXGridLevel>
				</Levels>
				<AutoSize Container="Window" Enabled="True" MinHeight="400" />
				<ActionBar>
					<Actions>
						<Save Enabled="False" />
						<Delete Enabled="False" />
						<EditRecord Enabled="True" />
						<NoteShow Enabled="False" />
						<ExportExcel Enabled="False" />
					</Actions>
					<CustomItems>
					</CustomItems>
				</ActionBar>
				<Mode AllowAddNew="False" AllowFormEdit="True" AllowDelete="False" />
			</px:PXGrid>




		</asp:Content>

		