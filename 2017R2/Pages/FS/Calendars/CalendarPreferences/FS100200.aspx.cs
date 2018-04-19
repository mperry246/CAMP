using System;
using System.IO;
using PX.Objects.FS;

public partial class Page_FS100200 : PX.Web.UI.PXPage
{
    public string baseUrl;
    public string pageUrl;
    public string preferencesTemplate;

    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = SharedFunctions.GetInstanceUrl(Request.Url.Scheme, Request.Url.Authority, Request.ApplicationPath.TrimEnd('/'));
        pageUrl = SharedFunctions.GetWebMethodPath(Request.Path);

        // Load Preference Template
        StreamReader streamReader = new StreamReader(Server.MapPath("../../Shared/templates/PreferencesTemplate.html"));
        preferencesTemplate = streamReader.ReadToEnd();
        streamReader.Close();
    }
}