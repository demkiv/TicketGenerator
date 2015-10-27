using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketGenerator.Domain;
using TicketGenerator.Domain.Entities;
using TicketGenerator.UI.Models;
using TicketGenerator.UI.ReportingServiceReference;

namespace TicketGenerator.UI.Controllers
{
	public class HomeController : Controller
	{

		public ActionResult TicketInfo()
		{
			TicketInfo ticketInfo;
			using (var ctx = new TicketDbContext())
			{
				ticketInfo = new TicketInfo()
				{
					EventName = ctx.Events.Find(1).Name,
					EventDate = ctx.Events.Find(1).Date,
					Stadium = ctx.Events.Find(1).Stadium.Name
				};
			}

			return View(ticketInfo);
		}

		[HttpPost]
		public ActionResult TicketInfo(TicketInfo ticket)
		{
			int SeatId = 0;

			//Ticket newticket;
			//using (var ctx = new TicketDbContext())
			//{
			//	ctx.Seats.Find(SeatId).Status = true;
			//	Seat selectedSeat = ctx.Seats.Find(SeatId);

			//	newticket = new Ticket()
			//	{
			//		EventDate = ticket.EventDate,
			//		EventName = ticket.EventName,
			//		Price = 100,

			//		Owner = new Person()
			//		{
			//			FirstName = ticket.FirstName,
			//			LastName = ticket.LastName,
			//			MiddleName = ticket.MiddleName
			//		},

			//		Seat = selectedSeat
			//		//Seat = new Sector()
			//		//{
			//		//	Stadium = ticket.Stadium,
			//		//	SectorNumber = ticket.Sector,
			//		//	Row = ticket.Row,
			//		//	Number = ticket.Number
			//		//}
			//	};

			//	ctx.TicketInfos.Add(newticket);
			//	ctx.SaveChanges();
			//}

			//return File(CreatePDF(newticket.Id), "application/pdf");
			return new EmptyResult();
		}

		private byte[] CreatePDF(int ticketId)
		{
			ReportExecutionService rs = new ReportExecutionService();

			string format = "PDF";
			string reportPath = "/Reports/TicketReport";
			string mimeType = "application/pdf";

			var parameters = new[]
			{
				new ParameterValue
				{
					Name = "TicketNumber",
					Value = ticketId.ToString()
				}
			};
			
			return RenderReport(rs, format, mimeType, reportPath, parameters);
		}

		private static byte[] RenderReport(ReportExecutionService rs, string format, string mimeType, string reportPath,
			ParameterValue[] parameters)
		{
			rs.Credentials = CredentialCache.DefaultCredentials;

			string deviceInfo = string.Empty;
			string extension;
			string encoding;
			Warning[] warnings;
			string[] streamIDs;

			rs.LoadReport(reportPath, null);
			rs.SetExecutionParameters(parameters, "en-us");
			var results = rs.Render(format, deviceInfo, out extension, out mimeType, out encoding, out warnings, out streamIDs);

			return results;
		}
	}
}