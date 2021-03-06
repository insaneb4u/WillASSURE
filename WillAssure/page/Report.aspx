﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="WillAssure.Views.ViewDocument.Report" %>

<%@ Register Assembly="CrystalDecisions.Web" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

     <script src="~/assets/js/jquery.min.js"></script>
     <script src="~/assets/plugins/alerts-boxes/js/sweetalert.min.js"></script>
    <script src="~/assets/plugins/alerts-boxes/js/sweet-alert-script.js"></script>

</head>

<body>
    <form id="form1" runat="server">
     
    


        <center>

               <h1>Created Will</h1>

        
            <br />
        <asp:Button CssClass="btn btn-success" ID="btnverify" Text="Email PDF" runat="server" OnClick="btnverify_Click" />
   <%--     <asp:Button CssClass="btn btn-danger" ID="btncancel" Text="Cancel" runat="server" OnClick="btncancel_Click" />--%>
            <asp:button text="Back To Dashboard" ID="btnback" CssClass="btn btn-info" runat="server" OnClick="btnback_Click"  />
       <%-- <asp:Button CssClass="btn btn-primary" ID="btnChangeTemplate" Text="ChangeTemplate" runat="server" OnClick="btnChangeTemplate_Click"  />--%>
            <br />
            <br />
         
            

            <asp:Label ID="lblmsgquickwill" ForeColor="#cc0000" Visible="false" runat="server"></asp:Label>

             <asp:Label ID="lblsuccessmsg" ForeColor="#33cc33" Visible="false" runat="server"></asp:Label>

            
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource1" Visible="true" ToolPanelView="None" HasCrystalLogo="False" HasDrilldownTabs="False" HasDrillUpButton="False" HasExportButton="False" HasPrintButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" HasZoomFactorList="False" PrintMode="ActiveX" EnableDrillDown="False" ReuseParameterValuesOnRefresh="True" ViewStateMode="Enabled" EnableDatabaseLogonPrompt="False" />
       <CR:CrystalReportSource ID="CrystalReportSource1" runat="server" Visible="true">
            <Report FileName="~/CrystalReports/QuickWill1.rpt">
            </Report>
        </CR:CrystalReportSource>
        </center>
      



        



    </form>


    <script>

        function alertme() {


               swal({

            text: 'DO you Like To Save Information Or Delete If Yes Document Will Get Saved In Completed Document If No Infomation Get Deleted',
            icon: 'warning',
            buttons: true,
            dangerMode: true,
            buttons: ['No', 'Yes']
        })
            .then((willDelete) => {
                if (willDelete) {



                } else {




                    




                }
            });


        }



    </script>


</body>
</html>
