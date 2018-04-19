using PX.Objects.FS;
using PX.Common;
using System;
using System.Globalization;
using System.IO;

public partial class Page_FS300300 : PX.Web.UI.PXPage
{
    public String baseUrl;
    public String pageUrl;
    public String RefNbr;
    public String CustomerID;
    public String appointmentBodyTemplate;
    public String toolTipTemplateAppointment;
    public String toolTipTemplateServiceOrder;
    public String startDate;
    public String SMEquipmentID;

    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = SharedFunctions.GetInstanceUrl(Request.Url.Scheme, Request.Url.Authority, Request.ApplicationPath.TrimEnd('/'));
        pageUrl = SharedFunctions.GetWebMethodPath(Request.Path);

		DateTime? startDateBridge;
        var date = PXContext.GetBusinessDate();
        
        startDateBridge = (date != null) ? date : PXTimeZoneInfo.Now;

        // Date
        try
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Date"]))
            {
                startDateBridge = Convert.ToDateTime(Request.QueryString["Date"]);
            }
        }
        catch (Exception)
        {
        }

        startDate = ((DateTime)startDateBridge).ToString("MM/dd/yyyy h:mm:ss tt", new CultureInfo("en-US"));
        
        // Filter By RefNbr
        RefNbr = Request.QueryString["RefNbr"];

        // External CustomerID
        CustomerID = Request.QueryString["CustomerID"];

        // External SMEquipmentID
        SMEquipmentID = Request.QueryString["SMEquipmentID"];

        // Load Appointment's ToolTip to be used in index.aspx
        StreamReader streamReader = new StreamReader(Server.MapPath("../../Shared/templates/TooltipAppointment.html"));
        toolTipTemplateAppointment = streamReader.ReadToEnd();
        streamReader.Close();

        // Load Service Order's ToolTip to be used in index.aspx
        streamReader = new StreamReader(Server.MapPath("../../Shared/templates/TooltipServiceOrder.html"));
        toolTipTemplateServiceOrder = streamReader.ReadToEnd();
        streamReader.Close();

        // Load Appointment's Body to be used in index.aspx
        streamReader = new StreamReader(Server.MapPath("../../Shared/templates/EventBodyTemplate.html"));
        appointmentBodyTemplate = streamReader.ReadToEnd();
        streamReader.Close();
    }

}