<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FS300700.aspx.cs" Inherits="Page_FS300700" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="DB" >
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">

     <title>Room Calendar Board</title>
     <link rel="stylesheet" href="../../Shared/css/sch-all.css">
     <link rel="stylesheet" href="../../Shared/css/style.css">
     <link rel="stylesheet" href="../../Shared/css/scheduler-main.css">
     <link rel="stylesheet" href="../../Shared/css/boxselect.css">
     <link rel="stylesheet" href="../../Shared/css/style-tree.css">
</head>
<body>   

     <!-- Main Container -->
    <div class="container-fluid main-container">

        <!-- Title -->
        <form id="form1" runat="server">
			<px_pt:PageTitle ID="pageTitle" runat="server" CustomizationAvailable="false" HelpAvailable="false"/>
		</form>
        <!-- End Title -->
        
        <!-- Scheduler -->
        <div class="row-fluid container">
            <div id="scheduler-container" class="span12">
	        </div>
        </div>
        <!-- End Scheduler -->

    </div>
   <!-- End Main Container -->

   <!-- Teemplates ToolTip -->
   <%= toolTipTemplateAppointment %>
   <%= toolTipTemplateServiceOrder %>
   <%= appointmentBodyTemplate %>

   <!-- Variables Globales -->
    <script type="text/javascript">
        var pageUrl= "<%= pageUrl %>";
        var baseUrl= "<%= baseUrl %>";
        var startDate = "<%= startDate %>";
        var RefNbr= "<%= RefNbr %>";
        var ExternalCustomerID = "<%= CustomerID %>";
    </script>

    <script src="../../Shared/definition/Calendars/ID.js" type="text/javascript"></script>
    <script src="../../Shared/definition/Calendars/TX.js"></script>
    <script src="../../Shared/definition/Calendars/FieldsLabel.js" type="text/javascript"></script>
    <script src="../../Shared/definition/Calendars/FieldsName.js" type="text/javascript"></script>
    <script src="../../Shared/definition/Calendars/Messages.js" type="text/javascript"></script>
    <script src="../../Shared/js/jquery-1.10.1.js" type="text/javascript"></script>

<link rel="stylesheet" href="resources/MultiRoomDispatch-all.css"/>
<script type="text/javascript" src="app.js"></script>
    
	
</body>
</html>
