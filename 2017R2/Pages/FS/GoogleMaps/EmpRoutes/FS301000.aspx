<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FS301000.aspx.cs" Inherits="Page_FS301000" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="DB" >
    <head id="Head1" runat="server">
        <meta http-equiv="content-type" content="text/html; charset=UTF-8">

        <title>Routes on Map</title>
        <link rel="stylesheet" href="../../Shared/css/sch-all.css">
        <link rel="stylesheet" href="../../Shared/css/style.css">
        <link rel="stylesheet" href="../../Shared/css/routes-main-new.css">

    </head>
    <body>   
		<!-- Main Container -->
		<div id="main-container">
			<!-- Title -->
			<form id="form1" runat="server">
				<px_pt:PageTitle ID="pageTitle" runat="server" CustomizationAvailable="false" HelpAvailable="false"/>
			</form>
			<!-- Routes -->
			<div class="container">
				<div id="routes-container">
				</div>
			</div>
			<!-- End Routes -->
		</div>
	   <!-- End Main Container -->

   <!-- Teemplates ToolTip -->
   <%= infoRoute %>

    <!-- Global Variables -->
    <script type="text/javascript">
        var pageUrl= "<%= pageUrl %>";
        var baseUrl= "<%= baseUrl %>";
        var startDate = "<%= startDate %>";
        var MapApiKey = "<%= apiKey %>";
        var isIE11 = !!(navigator.userAgent.match(/Trident/) && !navigator.userAgent.match(/MSIE/));
        if (isIE11) {
            if (typeof window.attachEvent == "undefined" || !window.attachEvent) {
                window.attachEvent = window.addEventListener;
            }
        }
        var mapCallBack = function () {};
        var mapClass;
    </script>

    <!-- Configuration files -->
    <script src="../../Shared/definition/GoogleMaps/ID.js" type="text/javascript"></script>
    <script src="../../Shared/definition/GoogleMaps/TX.js" type="text/javascript"></script>
    <script src="../../Shared/definition/GoogleMaps/Cfg.js" type="text/javascript"></script>
    <script src="../../Shared/definition/Calendars/Messages.js" type="text/javascript"></script>
    <script src="../../Shared/js/jquery-1.10.1.js" type="text/javascript"></script>

    <!-- Bing Maps api-->
    <script type='text/javascript' src='https://www.bing.com/api/maps/mapcontrol?callback=mapCallBack' async defer />

    <script>
        window.onbeforeunload = function(e) {
            return TX.messages.windowCloseWarning;
        };
    </script>

<link rel="stylesheet" href="resources/EmpRoutes-all.css"/>
<script type="text/javascript" src="app.js"></script>

    </body>
</html>

