using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

[Obsolete("Will be removed in Acumatica 8.0")]
public partial class Page_CA206000 : PX.Web.UI.PXPage
{
	protected void Page_Init(object sender, EventArgs e)
	{
		this.Master.PopupHeight = 400;
		this.Master.PopupWidth = 720;		
	}
}
