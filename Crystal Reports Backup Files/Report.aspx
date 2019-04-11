﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="WillAssure.Views.ViewDocument.Report" %>

<%@ Register Assembly="CrystalDecisions.Web" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     
        <center><h1>Created Will</h1></center>

        
   
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ReportSourceID="CrystalReportSource1" Visible="true" ToolPanelView="None" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server" Visible="true">
            <Report FileName="D:\Will Assure\WillAssure\WillAssure\CrystalReports\WillTestator.rpt">
            </Report>
        </CR:CrystalReportSource>



        



    </form>
</body>
</html>