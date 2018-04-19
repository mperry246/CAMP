using System;
using PX.Data;
using System.Collections.Generic;
using System.Collections;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using System.Linq;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.SO;
using PX.Objects.GL;
using PX.Objects.PM;

namespace PX.Objects.CR
{
	#region CampaignMaint
    public class CampaignMaint : PXGraph<CampaignMaint, CRCampaign>, PXImportAttribute.IPXPrepareItems
	{
		[PXHidden]
		public PXSetup<CRSetup>
			crSetup;        

        [PXHidden]
		public PXSelect<Contact>
			BaseContacts;

		[PXHidden]
		public PXSelect<APInvoice>
			APInvoicies;

		[PXHidden]
		public PXSelect<ARInvoice>
			ARInvoicies;

		public PXSelect<Contact, Where<Contact.contactID, Equal<Required<Contact.contactID>>>>
			ContactByContactId;

		[PXViewName(Messages.Campaign)]
		public PXSelect<CRCampaign>
			Campaign;

		[PXViewName(Messages.Opportunities)]
		public PXSelectJoin<CROpportunity,
            LeftJoin<CROpportunityProbability, On<CROpportunity.stageID, Equal<CROpportunityProbability.stageCode>>>,
			Where<CROpportunity.campaignSourceID, Equal<Current<CRCampaign.campaignID>>>>
			Opportunities;

		[PXHidden]
		public PXSelect<CRCampaign,
			Where<CRCampaign.campaignID, Equal<Current<CRCampaign.campaignID>>>>
			CampaignCurrent;

        [PXHidden]
        public PXSelect<DAC.Standalone.CRCampaign,
            Where<DAC.Standalone.CRCampaign.campaignID, Equal<Current<CRCampaign.campaignID>>>>
            CalcCampaignCurrent;


        public PXFilter<OperationParam> Operations;

		[PXViewName(Messages.CampaignMembers)]
        [PXImport(typeof(CRCampaign))]
		[PXFilterable]
		public CRCampaignMembersList CampaignMembers;

		[PXHidden]
		public PXSelect<CRCampaignMembers> CampaignMembersHidden;

		[PXViewName(Messages.Answers)]
		public CRAttributeList<CRCampaign>
			Answers;
	

		[PXViewName(Messages.Leads)]
		[PXFilterable]
		[PXCopyPasteHiddenView]
		public PXSelectJoin<Lead,
			LeftJoin<BAccount, 
				On<BAccount.bAccountID, Equal<Lead.bAccountID>>,
			LeftJoin<Address, 
				On<Address.addressID, Equal<Lead.defAddressID>>>>,
			Where<Lead.campaignID, Equal<Current<CRCampaign.campaignID>>>,
			OrderBy<
				Asc<Lead.displayName, Asc<Lead.contactID>>>>
			Leads;
		
		[PXViewName(Messages.Activities)]
		[PXFilterable]
		public CRCampaignMembersActivityList<CRCampaign>
			Activities;

        [PXHidden]
        public PXSelect<CRPMTimeActivity, Where<True, Equal<False>>> _hiddenActivities;

        public CampaignMaint()
		{
			PXDBAttributeAttribute.Activate(this.Caches[typeof(Contact)]);
			PXDBAttributeAttribute.Activate(Opportunities.Cache);
			PXUIFieldAttribute.SetEnabled<DAC.Standalone.CRCampaign.leadsGenerated>(CampaignCurrent.Cache, null, false);
			PXUIFieldAttribute.SetEnabled<DAC.Standalone.CRCampaign.leadsConverted>(CampaignCurrent.Cache, null, false);
			PXUIFieldAttribute.SetEnabled<DAC.Standalone.CRCampaign.contacts>(CampaignCurrent.Cache, null, false);
			PXUIFieldAttribute.SetEnabled<DAC.Standalone.CRCampaign.opportunities>(CampaignCurrent.Cache, null, false);
			PXUIFieldAttribute.SetEnabled<DAC.Standalone.CRCampaign.closedOpportunities>(CampaignCurrent.Cache, null, false);
			PXUIFieldAttribute.SetEnabled<DAC.Standalone.CRCampaign.opportunitiesValue>(CampaignCurrent.Cache, null, false);
			PXUIFieldAttribute.SetEnabled<DAC.Standalone.CRCampaign.closedOpportunitiesValue>(CampaignCurrent.Cache, null, false);

			PXUIFieldAttribute.SetRequired<CRCampaign.startDate>(CampaignCurrent.Cache, true);
			PXUIFieldAttribute.SetRequired<CRCampaign.status>(CampaignCurrent.Cache, true);

            
            var cache = Caches[typeof(Contact)];
			PXDBAttributeAttribute.Activate(cache);            
            PXUIFieldAttribute.SetVisible<Contact.title>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.workgroupID>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.firstName>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.midName>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.lastName>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.phone2>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.phone3>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.fax>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.webSite>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.dateOfBirth>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.createdByID>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.createdDateTime>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.lastModifiedByID>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.lastModifiedDateTime>(cache, null, false);

			PXUIFieldAttribute.SetVisible<Address.addressLine1>(Caches[typeof(Address)], null, false);
			PXUIFieldAttribute.SetVisible<Address.addressLine2>(Caches[typeof(Address)], null, false);

			PXUIFieldAttribute.SetVisible<Contact.classID>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.source>(cache, null, false);
			PXUIFieldAttribute.SetVisible<Contact.status>(cache, null, false);

            PXUIFieldAttribute.SetVisibility<Contact.contactPriority>(cache, null, PXUIVisibility.Invisible);          

            PXUIFieldAttribute.SetDisplayName<BAccount.acctName>(Caches[typeof(BAccount)], Messages.CustomerName);
		    cache = Caches[typeof(Lead)];
		    PXUIFieldAttribute.SetVisible<Lead.title>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.firstName>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.midName>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.lastName>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.phone1>(cache, null, false);
            PXUIFieldAttribute.SetVisible<Lead.phone2>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.phone3>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.fax>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.eMail>(cache, null, false);
            PXUIFieldAttribute.SetVisible<Lead.webSite>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.dateOfBirth>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.createdByID>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.createdDateTime>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.lastModifiedByID>(cache, null, false);
		    PXUIFieldAttribute.SetVisible<Lead.lastModifiedDateTime>(cache, null, false);
		    PXUIFieldAttribute.SetEnabled(this.Caches[typeof(Contact)], null, null, false);
		    PXUIFieldAttribute.SetEnabled<Contact.selected>(this.Caches[typeof(Contact)], null, true);
			
			Inquiry.AddMenuAction(viewCampaignDocuments);
			Inquiry.MenuAutoOpen = true;
		}

        #region CacheAttached

	    [PXMergeAttributes(Method = MergeMethod.Replace)]
        [PXDBString(10, IsUnicode = true, IsKey = true)]
	    [PXDBLiteDefault(typeof(CRCampaign.campaignID))]
	    [PXUIField(DisplayName = Messages.CampaignID)]
	    [PXParent(typeof(Select<CRCampaign, Where<CRCampaign.campaignID, Equal<Current<CRCampaignMembers.campaignID>>>>))]
	    protected virtual void CRCampaignMembers_CampaignID_CacheAttached(PXCache cache)
	    {
		}

		[PXMergeAttributes(Method = MergeMethod.Merge)]
		[PXUIField(DisplayName = "Member Since", Enabled = false)]
		protected virtual void CRCampaignMembers_CreatedDateTime_CacheAttached(PXCache cache)
		{
		}
		
		[PXMergeAttributes(Method = MergeMethod.Merge)]
		[PXUIField(DisplayName = "Contact", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		protected void Contact_DisplayName_CacheAttached(PXCache sender)
		{
		}

		[PXMergeAttributes(Method = MergeMethod.Merge)]
		[PXDefault("")]
		protected void Contact_ContactType_CacheAttached(PXCache sender)
		{
		}

		[PXMergeAttributes(Method = MergeMethod.Merge)]
		[PXUIField(DisplayName = "Account ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        [PXSelector(typeof(BAccount.bAccountID), SubstituteKey = typeof(BAccount.acctCD), DescriptionField = typeof(BAccount.acctCD), DirtyRead = true)]
        protected void Contact_BAccountID_CacheAttached(PXCache sender)
		{
		}

		[PXMergeAttributes(Method = MergeMethod.Merge)]
		[PXUIField(DisplayName = Messages.ContactStatus)]
		protected virtual void Contact_Status_CacheAttached(PXCache sender)
		{
		}

		[PXMergeAttributes(Method = MergeMethod.Merge)]
	    [PXSelector(typeof(BAccountR.bAccountID), SubstituteKey = typeof(BAccountR.acctCD), DescriptionField = typeof(BAccountR.acctCD))]
        protected void CRPMTimeActivity_BAccountID_CacheAttached(PXCache sender)
	    {
	        
	    }
        [PXMergeAttributes(Method = MergeMethod.Merge)]
	    [PXSelector(typeof(Contact.contactID), DescriptionField = typeof(Contact.displayName))]
	    protected void CRPMTimeActivity_ContactID_CacheAttached(PXCache sender)
	    {

	    }       

		#endregion

		#region Actions

		public PXMenuInquiry<CRCampaign> Inquiry;

		public PXAction<CRCampaign> viewCampaignDocuments;
		[PXUIField(DisplayName = Messages.ViewCampaignDocuments, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.Inquiry)]
		public virtual IEnumerable ViewCampaignDocuments(PXAdapter adapter)
		{
			CampaignDocuments target = PXGraph.CreateInstance<CampaignDocuments>();
			target.Campaign.Current = target.Campaign.Search<CRCampaign.campaignID>(this.CampaignCurrent.Current.CampaignID);
			throw new PXRedirectRequiredException(target, Messages.ViewCampaignDocuments);
		}

		public PXAction<CRCampaign> addOpportunityForContact;
		[PXUIField(DisplayName = Messages.AddNewOpportunity, FieldClass = FeaturesSet.customerModule.FieldClass)]
        [PXButton(OnClosingPopup = PXSpecialButtonType.Cancel)]
        public virtual void AddOpportunityForContact()
		{
			var row = CampaignCurrent.Current;
			if (row == null || row.CampaignID == null) return;

			var member = CampaignMembers.Current;
			if (member == null || member.ContactID == null) return;

			var contact = (Contact)ContactByContactId.Select(member.ContactID);

			var graph = PXGraph.CreateInstance<OpportunityMaint>();
			var newOpportunity = graph.Opportunity.Insert();
			newOpportunity.CampaignSourceID = row.CampaignID;                        
			newOpportunity.BAccountID = contact?.BAccountID;
		    if (contact?.ContactType != ContactTypesAttribute.BAccountProperty)
		        newOpportunity.ContactID = member.ContactID;

            CRContactClass cls = PXSelect<CRContactClass, Where<CRContactClass.classID, Equal<Current<Contact.classID>>>>
                .SelectSingleBound(this, new object[] { contact });
		    if (cls?.OwnerToOpportunity == true)
		    {
		        newOpportunity.WorkgroupID = contact?.WorkgroupID;
		        newOpportunity.OwnerID = contact?.OwnerID;
		    }
		    if (cls?.TargetOpportunityClassID != null)
		    {
		        newOpportunity.CROpportunityClassID = cls.TargetOpportunityClassID;
		    }

            newOpportunity = graph.Opportunity.Update(newOpportunity);
		    newOpportunity.ContactID = member.ContactID;
			PXRedirectHelper.TryRedirect(graph, PXRedirectHelper.WindowMode.NewWindow);
		}

		public PXDBAction<CRCampaign> addOpportunity;
		[PXUIField(DisplayName = Messages.AddNewOpportunity, FieldClass = FeaturesSet.customerModule.FieldClass)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.AddNew, OnClosingPopup = PXSpecialButtonType.Refresh)]
		public virtual void AddOpportunity()
		{
			var row = CampaignCurrent.Current;
			if (row == null || row.CampaignID == null) return;
			
			var graph = PXGraph.CreateInstance<OpportunityMaint>();
			var newOpportunity = graph.Opportunity.Insert();
			newOpportunity.CampaignSourceID = row.CampaignID;
			graph.Opportunity.Update(newOpportunity);
			PXRedirectHelper.TryRedirect(graph, PXRedirectHelper.WindowMode.NewWindow);
		}

		public PXDBAction<CRCampaign> addContact;
		[PXUIField(DisplayName = Messages.AddNewContact, FieldClass = FeaturesSet.customerModule.FieldClass)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.AddNew, OnClosingPopup = PXSpecialButtonType.Refresh)]
		public virtual void AddContact()
		{
			var row = CampaignCurrent.Current;
			if (row?.CampaignID == null) return;

			var graph = PXGraph.CreateInstance<LeadMaint>();

			var newContact = graph.Lead.Insert();
			newContact.CampaignID = row.CampaignID;
			graph.Lead.Update(newContact);
            
            PXRedirectHelper.TryRedirect(graph, PXRedirectHelper.WindowMode.NewWindow);
		}
		
		#endregion

		#region Events

		protected virtual void OperationParam_ContactGI_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			this.Operations.Cache.SetValue<OperationParam.sharedGIFilter>(this.Operations.Current, null);
		}

		protected virtual void CRCampaign_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
		{
			CRCampaign row = e.Row as CRCampaign;
			DAC.Standalone.CRCampaign dacRow = CalcCampaignCurrent.Select();
			if (row != null)
			{
				if (CanBeDeleted(row, dacRow) == false)
				{
					e.Cancel = true;
					throw new PXException(Messages.CampaignIsReferenced);
				}
			}
		}

		protected virtual void CRCampaign_Status_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			CRCampaign campaign = (CRCampaign)e.Row;
			var state = (PXStringState)this.Caches<CRCampaign>().GetStateExt<CRCampaign.status>(campaign);
			campaign.Status = state.AllowedValues.FirstOrDefault();
		}        

		private bool CanBeDeleted(CRCampaign campaign, DAC.Standalone.CRCampaign dacCampaign)
		{
			foreach (var f in new string[]
			{
				nameof(CRCampaign.mailsSent)
			})
			{
				var state = CampaignCurrent.Cache.GetStateExt(campaign, f);
				if (((PXIntState) state).Value != null && (int) ((PXIntState) state).Value > 0)
					return false;
			}
			
			foreach (var f in new string[]
			{
				nameof(DAC.Standalone.CRCampaign.closedOpportunities),
				nameof(DAC.Standalone.CRCampaign.contacts),
				nameof(DAC.Standalone.CRCampaign.leadsConverted),
				nameof(DAC.Standalone.CRCampaign.leadsGenerated),
				nameof(DAC.Standalone.CRCampaign.opportunities),
			})
			{
				var state = CalcCampaignCurrent.Cache.GetStateExt(dacCampaign, f);
				if (((PXIntState) state).Value != null && (int) ((PXIntState) state).Value > 0)
					return false;
			}
			
			if (PXSelectGroupBy<SOOrder,
				Where<SOOrder.campaignID, Equal<Current<CRCampaign.campaignID>>>,
				Aggregate<Count>>.Select(this).RowCount > 0)
				return false;

			if (PXSelectGroupBy<ARInvoice,
				Where<ARInvoice.campaignID, Equal<Current<CRCampaign.campaignID>>>,
				Aggregate<Count>>.Select(this).RowCount > 0)
				return false;

			if (PXSelectGroupBy<PMProject,
				Where<PMProject.campaignID, Equal<Current<CRCampaign.campaignID>>>,
				Aggregate<Count>>.Select(this).RowCount > 0)
				return false;

			return true;
		}

		protected virtual void CRCampaign_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			CRCampaign row = e.Row as CRCampaign;
			if (row == null) return;

			var isNotInserted = cache.GetStatus(row) != PXEntryStatus.Inserted;
			addOpportunity.SetEnabled(isNotInserted);
            this.Actions[CRCampaignMembersList.addAction].SetEnabled(isNotInserted);            
        }

        protected virtual void CRCampaign_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
		{
			CRCampaign row = (CRCampaign)e.Row;
			if (row != null)
			{
				if (row.StartDate.HasValue == false)
				{
					if (cache.RaiseExceptionHandling<CRCampaign.startDate>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(CRCampaign.startDate).Name)))
					{
						throw new PXRowPersistingException(typeof(CRCampaign.startDate).Name, null, ErrorMessages.FieldIsEmpty, typeof(CRCampaign.startDate).Name);
					}
				}
			}
		}

		#endregion

		#region Implementation of IPXPrepareItems

		public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
        {
            if (string.Compare(viewName, "CampaignMembers", true) == 0)
            {
                if (values.Contains("ContactID"))
                {
                    
                    Contact contact;
                    int contactID;
                    if (int.TryParse(values["ContactID"].ToString(), out contactID))
                    {
                        contact = PXSelect<Contact, Where<Contact.contactID, Equal<Required<Contact.contactID>>, And<
                                    Where<Contact.contactType, Equal<ContactTypesAttribute.lead>,
                                    Or<Contact.contactType, Equal<ContactTypesAttribute.person>>>>>>.Select(this, contactID);
                    }
                    else
                    {
                        string contactDisplayName = values["ContactID"].ToString();
                        contact = PXSelect <Contact, Where<Contact.memberName, Equal<Required<Contact.memberName>>, And<
                                    Where<Contact.contactType, Equal<ContactTypesAttribute.lead>,
                                    Or<Contact.contactType, Equal<ContactTypesAttribute.person>,
                                    Or<Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>>>,
                                    OrderBy<Asc<Contact.contactPriority>>>.Select(this, contactDisplayName);
                    }

                    if (contact != null)
                    {
                        if (values.Contains("CampaignID"))
                            values["CampaignID"]= Campaign.Current.CampaignID;
                        else
                            values.Add("CampaignID", Campaign.Current.CampaignID);
                        keys["CampaignID"] = Campaign.Current.CampaignID;
                        
                        if (values.Contains("ContactID"))
                            values["ContactID"] = contact.ContactID;
                        else
                            values.Add("ContactID", contact.ContactID);
                        keys["ContactID"] = contact.ContactID;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool RowImporting(string viewName, object row)
        {
            return row == null;
        }

        public bool RowImported(string viewName, object row, object oldRow)
        {
            return oldRow == null;
        }

        public virtual void PrepareItems(string viewName, IEnumerable items)
        {
        }

        #endregion
	}
	#endregion	
}
