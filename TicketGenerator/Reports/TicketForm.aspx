<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketForm.aspx.cs" Inherits="TicketGenerator.Reports.TicketForm" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<style type="text/css">
		#form1 {
			width: 966px;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    	<rsweb:ReportViewer ID="ReportViewer" runat="server" Height="458px" ProcessingMode="Remote" Width="961px">
			<ServerReport DisplayName="ticketform" ReportPath="/ASPMVCReports/ticketform" />
		</rsweb:ReportViewer>
    </form>
</body>
</html>
