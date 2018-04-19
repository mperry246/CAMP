using PX.Objects.FS;
using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.EP;
using System;
using System.Globalization;
using System.IO;

public partial class Page_FS300400 : PX.Web.UI.PXPage
{
    public String baseUrl;
    public String pageUrl;
    public String RefNbr;
    public String CustomerID;
    public String appointmentBridgeUrl;
    public String appointmentBodyTemplate;
    public String toolTipTemplateServiceOrder;
    public String toolTipTemplateAppointment;
    public String startDate;
    public String DefaultEmployee = "";
    public String ExternalEmployee;
    public String SMEquipmentID;

    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = SharedFunctions.GetInstanceUrl(Request.Url.Scheme, Request.Url.Authority, Request.ApplicationPath.TrimEnd('/'));
        pageUrl = SharedFunctions.GetWebMethodPath(Request.Path);

		DateTime? startDateBridge;
        var date = PXContext.GetBusinessDate();

        startDateBridge = (date != null) ? date : PXTimeZoneInfo.Now;

        // Filter By RefNbr
        RefNbr = Request.QueryString["RefNbr"];

        // External CustomerID
        CustomerID = Request.QueryString["CustomerID"];

        // Employee
        ExternalEmployee = Request.QueryString["EmployeeID"];

        // External SMEquipmentID
        SMEquipmentID = Request.QueryString["SMEquipmentID"];

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

        var graphExternalControls = PXGraph.CreateInstance<ExternalControls>();
        var results = graphExternalControls.EmployeeSelected.Select();

        startDate = ((DateTime)startDateBridge).ToString("MM/dd/yyyy h:mm:ss tt", new CultureInfo("en-US"));

        PXResult<EPEmployee, Contact> result = (PXResult<EPEmployee, Contact>)results;

        EPEmployee epEmployeeRow = result;
        if (epEmployeeRow != null)
        {
            DefaultEmployee = epEmployeeRow.BAccountID.ToString();
        }

        if (string.IsNullOrEmpty(ExternalEmployee) && epEmployeeRow != null)
        {
        ExternalEmployee = DefaultEmployee;
        }

        // Load Appointment's Body to be used in index.aspx
        StreamReader streamReader = new StreamReader(Server.MapPath("../../Shared/templates/EventBodyTemplate.html"));
        appointmentBodyTemplate = streamReader.ReadToEnd();
        streamReader.Close();

        // Load Service Order's ToolTip to be used in index.aspx
        streamReader = new StreamReader(Server.MapPath("../../Shared/templates/TooltipServiceOrder.html"));
        toolTipTemplateServiceOrder = streamReader.ReadToEnd();
        streamReader.Close();

        // Load Appointment's ToolTip to be used in index.aspx
        streamReader = new StreamReader(Server.MapPath("../../Shared/templates/TooltipAppointment.html"));
        toolTipTemplateAppointment = streamReader.ReadToEnd();
        streamReader.Close();
    }

}