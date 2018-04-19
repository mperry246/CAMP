using PX.Objects.CR;
using System;

public partial class Page_CR304000 : PX.Web.UI.PXPage
{
	protected void Page_Init(object sender, EventArgs e)
	{
		this.Master.PopupHeight = 700;
		this.Master.PopupWidth = 900;
	}    

    protected void edContactID_EditRecord(object sender, PX.Web.UI.PXNavigateEventArgs e)
    {
        OpportunityMaint oppmaint = this.ds.DataGraph as OpportunityMaint;
        if (oppmaint != null)
        {
            CROpportunity currentopportunity = this.ds.DataGraph.Views[this.ds.DataGraph.PrimaryView].Cache.Current as CROpportunity;
            if (currentopportunity.ContactID == null && currentopportunity.BAccountID != null)
            {
                {
                    try
                    {
                        oppmaint.addNewContact.Press();
                    }
                    catch (PX.Data.PXRedirectRequiredException e1)
                    {
                        PX.Web.UI.PXBaseDataSource ds = this.ds as PX.Web.UI.PXBaseDataSource;
                        PX.Web.UI.PXBaseDataSource.RedirectHelper helper = new PX.Web.UI.PXBaseDataSource.RedirectHelper(ds);
                        helper.TryRedirect(e1);
                    } 
                }
            }
        }
    }
}
