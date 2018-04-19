<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CR203000.aspx.cs" Inherits="Page_CR203000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" AutoCallBack="True" Visible="True" Width="100%" TypeName="PX.Objects.CR.CampaignDocuments"
		PrimaryView="Campaign" PageLoadBehavior="SearchSavedKeys">
		<CallbackCommands>
			<px:PXDSCallbackCommand Visible="false" DependOnGrid="grdSalesOrders" Name="SalesOrders_ViewDetails" />
			<px:PXDSCallbackCommand Visible="false" DependOnGrid="grdARInvoice" Name="ARInvoices_ViewDetails" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView  ID="form" runat="server" DataSourceID="ds" Width="100%" DataMember="Campaign" Caption="Selection">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
			<px:PXSelector ID="edCampaignID" runat="server" DataField="CampaignID"
				DataSourceID="ds" Size="SM" CommitChanges="true" />
		    <px:PXFormView ID="Form1" runat="server" DataMember="FilterView" DataSourceID="ds" Caption="Hidden Form needed for VisibleExp of TabItems. Tabs are Hidden based on the values of Combo" Visible="False" TabIndex="300">
		        <Template>
		            <px:PXCheckBox SuppressLabel="True" ID="chkDist" runat="server" DataField="DistFeatureInstalled" Visible="False"/>
		        </Template>
		    </px:PXFormView>
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXTab ID="PXTabSalesOrders" runat="server" Width="100%" DataSourceID="ds" DataMember="ARInvoices">
		<Items>
			<px:PXTabItem Text="Invoices and Memos">
				<Template>
					<px:PXGrid ID="grdARInvoice" runat="server" ActionsPosition="Top" Style="z-index: 100" AllowPaging="true"
						AdjustPageSize="auto" SkinID="Details" Width="100%" SyncPosition="True">
						<Levels>
							<px:PXGridLevel DataMember="ARInvoices">
								<RowTemplate>
									<px:PXDropDown ID="edDocType" runat="server" AllowNull="False" DataField="DocType" />
									<px:PXSelector CommitChanges="True" ID="edRefNbr" runat="server" DataField="RefNbr" AutoRefresh="true">
										<Parameters>											
											<px:PXControlParam ControlID="grdARInvoice" Name="ARInvoiceAlias.docType" PropertyName="DataValues[&quot;DocType&quot;]" />
										</Parameters>
									</px:PXSelector>
								</RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="DocType" CommitChanges="True"/>
									<px:PXGridColumn DataField="RefNbr" CommitChanges="True" LinkCommand="ARInvoices_ViewDetails" Width="100px"/>									
									<px:PXGridColumn DataField="ARInvoice__Status"/>
									<px:PXGridColumn DataField="ARInvoice__DocDesc" Width="200px"/>
									<px:PXGridColumn DataField="ARInvoice__CuryOrigDocAmt" Width="150px"/>
									<px:PXGridColumn DataField="ARInvoice__CuryID" Width="100px"/>
									<px:PXGridColumn DataField="ARInvoice__CustomerID" Width="150px"/>

									<px:PXGridColumn DataField="ARInvoice__CustomerLocationID" Visible="false" SyncVisible="false"/>

									<px:PXGridColumn DataField="ARInvoice__DocDate"/>
									<px:PXGridColumn DataField="ARInvoice__InvoiceNbr" Width="200px"/>

								    <px:PXGridColumn DataField="ARInvoice__ProjectID" Visible="false" SyncVisible="false"/>
								</Columns>
							</px:PXGridLevel>
						</Levels>

						<ActionBar DefaultAction="cmdItemDetails" PagerVisible="False">
							<PagerSettings Mode="NextPrevFirstLast" />
                            <Actions>
				                <Delete Tooltip="Remove Link"/> 
                                <AddNew Tooltip="Link Document"/>
			                </Actions>
						</ActionBar>
						<AutoSize Container="Window" Enabled="True" MinHeight="150" />
						<Mode AutoInsert="false" AllowUpdate="False" InitNewRow="true"/>
						<CallbackCommands>
							<Refresh CommitChanges="True" PostData="Page" />
						</CallbackCommands>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
            <px:PXTabItem Text="Sales Orders" BindingContext="Form1" VisibleExp="DataControls[&quot;chkDist&quot;].Value == true">
				<Template>
					<px:PXGrid ID="grdSalesOrders" runat="server" ActionsPosition="Top" Style="z-index: 100" AllowPaging="true"
						AdjustPageSize="auto" SkinID="Details" Width="100%" SyncPosition="True">
						<Levels>
							<px:PXGridLevel DataMember="SalesOrders">
								<RowTemplate>
									<px:PXSelector ID="edOrderType" runat="server" AllowNull="False" DataField="OrderType" />
									<px:PXSelector CommitChanges="True" ID="edOrderNbr" runat="server" DataField="OrderNbr" AutoRefresh="true">
										<Parameters>											
											<px:PXControlParam ControlID="grdSalesOrders" Name="SOOrderAlias.OrderType" PropertyName="DataValues[&quot;OrderType&quot;]" />
										</Parameters>
									</px:PXSelector>
								</RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="OrderType" CommitChanges="True"/>
									<px:PXGridColumn DataField="OrderNbr" CommitChanges="True" LinkCommand="SalesOrders_ViewDetails" Width="100px"/>
									<px:PXGridColumn DataField="SOOrder__Status" Width="100px"/>
									<px:PXGridColumn DataField="SOOrder__OrderDesc" Width="200px"/>
									<px:PXGridColumn DataField="SOOrder__CuryOrderTotal" />
									<px:PXGridColumn DataField="SOOrder__CuryID" Width="100px"/>
									<px:PXGridColumn DataField="SOOrder__CustomerID" Width="150px"/>

									<px:PXGridColumn DataField="SOOrder__CustomerLocationID" Visible="false" SyncVisible="false"/>

									<px:PXGridColumn DataField="SOOrder__OrderDate"/>
									<px:PXGridColumn DataField="SOOrder__CustomerOrderNbr" Width="200px"/>

									<px:PXGridColumn DataField="SOOrder__ProjectID" Visible="false" SyncVisible="false"/>
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<ActionBar DefaultAction="cmdItemDetails" PagerVisible="False"  >
							<PagerSettings Mode="NextPrevFirstLast" />
                            <Actions>
				                <Delete Tooltip="Remove Link"/> 
                                <AddNew Tooltip="Link Document"/>
			                </Actions>							
						</ActionBar>
						<AutoSize Container="Window" Enabled="True" MinHeight="150" />
						<Mode AutoInsert="false" AllowUpdate="False" InitNewRow="true"/>
						<CallbackCommands>
							<Refresh CommitChanges="True" PostData="Page" />
						</CallbackCommands>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>			
		</Items>
	</px:PXTab>
</asp:Content>
