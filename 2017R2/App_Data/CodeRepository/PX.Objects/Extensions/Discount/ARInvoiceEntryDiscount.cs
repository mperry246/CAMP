namespace PX.Objects.Extensions.Discount
{
	//public class SOOrderEntryDiscount : DiscountGraph<SO.SOOrderEntry, SO.SOOrder>
	//{
	//	protected override DocumentMapping GetDocumentMapping()
	//	{
	//		return new DocumentMapping<SO.SOOrder> { CuryDiscTot = typeof(SO.SOOrder.approvedCreditAmt) };
	//	}

	//	public override PXSelectBase GetDocumentSelect()
	//	{
	//		return Base.Document;
	//	}
	//}
    /*
	public class ARInvoiceEntryDiscount : DiscountGraph<ARInvoiceEntry, ARInvoice>
	{
		protected override DocumentMapping GetDocumentMapping()
		{
			return new DocumentMapping(typeof(ARInvoice));
		}
		protected override DetailMapping GetDetailMapping()
		{
			return new DetailMapping(typeof(ARTran)) { Quantity = typeof(ARTran.qty), CuryLineAmount = typeof(ARTran.curyTranAmt) };
		}
		protected override DiscountMapping GetDiscountMapping()
		{
			return new DiscountMapping(typeof(ARInvoiceDiscountDetail));
		}
		public override PXSelectBase GetDocumentSelect()
		{
			return Base.Document;
		}
		public override PXSelectBase GetDetailsSelect()
		{
			return Base.Transactions;
		}
		public override PXSelectBase GetDiscountDetailsSelect()
		{
			return Base.ARDiscountDetails;
		}
		[PXSelector(typeof(Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouse>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomer>,
			  And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomerPrice>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventory>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventoryPrice>>>>>>>>))]
		[PXMergeAttributes]
		public override void Discount_DiscountID_CacheAttached(PXCache sender)
		{
		}
		[CurrencyInfo(typeof(ARInvoice.curyInfoID))]
		[PXMergeAttributes]
		public override void Discount_CuryInfoID_CacheAttached(PXCache sender)
		{
		}

		protected override bool AddDocumentDiscount
		{
			get
			{
				return true;
			}
		}
		protected override void DefaultDiscountAccountAndSubAccount(Detail det)
		{
			CR.Location customerloc = Base.location.Current;
			//Location companyloc = (Location)PXSelectJoin<Location, InnerJoin<BAccountR, On<Location.bAccountID, Equal<BAccountR.bAccountID>, And<Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<Branch, On<Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<Branch.branchID, Equal<Current<ARRegister.branchID>>>>.Select(this);

			object customer_LocationAcctID = Base.GetValue<CR.Location.cDiscountAcctID>(customerloc);
			//object company_LocationAcctID = GetValue<Location.cDiscountAcctID>(companyloc);

			ARTran tran = (ARTran)Base.Transactions.Cache.GetMain<Detail>(det);
			if (customer_LocationAcctID != null)
			{
				tran.AccountID = (int?)customer_LocationAcctID;
				Base.Discount_Row.Cache.RaiseFieldUpdated<ARTran.accountID>(tran, null);
			}

			if (tran.AccountID != null)
			{
				object customer_LocationSubID = Base.GetValue<CR.Location.cDiscountSubID>(customerloc);
				if (customer_LocationSubID != null)
				{
					tran.SubID = (int?)customer_LocationSubID;
					Base.Discount_Row.Cache.RaiseFieldUpdated<ARTran.subID>(tran, null);
				}
			}

			tran.LineType = PX.Objects.SO.SOLineType.Discount;
			tran.DrCr = (Base.Document.Current.DrCr == "D") ? "C" : "D";
			using (new PXLocaleScope(Base.customer.Current.LocaleName))
				tran.TranDesc = PXMessages.LocalizeNoPrefix(PX.Objects.AR.Messages.DocDiscDescr);

			if (tran.TaskID == null && !PM.ProjectDefaultAttribute.IsNonProject(Base, tran.ProjectID))
			{
				PM.PMProject project = PXSelect<PM.PMProject, Where<PM.PMProject.contractID, Equal<Required<PM.PMProject.contractID>>>>.Select(Base, tran.ProjectID);
				if (project != null && project.BaseType != "C")
				{
					PM.PMAccountTask task = PXSelect<PM.PMAccountTask, Where<PM.PMAccountTask.projectID, Equal<Required<PM.PMAccountTask.projectID>>, And<PM.PMAccountTask.accountID, Equal<Required<PM.PMAccountTask.accountID>>>>>.Select(Base, tran.ProjectID, tran.AccountID);
					if (task != null)
					{
						tran.TaskID = task.TaskID;
					}
					else
					{
						PX.Objects.GL.Account ac = PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Select(Base, tran.AccountID);
						throw new PXException(PX.Objects.AR.Messages.AccountMappingNotConfigured, project.ContractCD, ac.AccountCD);
					}
				}
			}
		}
	}  
    */  
}
