using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using PX.Web.UI;

public partial class Page_XB200000 : PX.Web.UI.PXPage
{
    protected void Page_LoadCompleted(object sender, EventArgs e)
    {
       
    }

    public string SelectedModuleType
    {
        get
        {
            if (this.form.FindControl("edOrdType") != null && ((PXDropDown)this.form.FindControl("edOrdType")).Value != null)
                return ((PXDropDown)this.form.FindControl("edOrdType")).Value.ToString();
            else
                return string.Empty;
        }
    }

}
