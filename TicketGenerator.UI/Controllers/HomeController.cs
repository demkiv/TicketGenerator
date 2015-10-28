using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketGenerator.Domain;
using TicketGenerator.Domain.Entities;
using TicketGenerator.UI.Models;
using TicketGenerator.UI.Models.Helpers;
using TicketGenerator.UI.ReportingServiceReference;
using Newtonsoft.Json;

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
					EventName = ctx.Events.First().Name,
					EventDate = ctx.Events.First().Date.ToString(),
					Stadium = ctx.Stadiums.First().Name,
					Events = ctx.Events.Select(e => new DropDownListItem() { Id = e.Id, Value = e.Name }).ToList(),
					Sectors = ctx.Sectors.Select(e => new DropDownListItem() { Id = e.Id, Value = e.Name }).ToList(),
					Price = ctx.Events.First().Price
				};
			}

			return View(ticketInfo);
		}

		[HttpGet]
		public ActionResult GetEventInfo(int eventId)
		{
			using (var ctx = new TicketDbContext())
			{
				Event e = ctx.Events.Find(eventId);
				EventInfo eventInfo = new EventInfo()
				{
					EventDate = e.Date.ToString(),
					Price = e.Price
				};

				return Json(eventInfo, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public ActionResult GetSectorInfo(int sectorId)
		{
			using (var ctx = new TicketDbContext())
			{
				Sector sector = ctx.Sectors.Find(sectorId);
				SectorInfo sectorInfo = new SectorInfo()
				{
					RowNumber = sector.RowNumber,
					SeatNumber = sector.SeatNumber
				};

				return Json(sectorInfo, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public ActionResult TicketInfo(TicketInfo ticket)
		{
			ticket.SeatId = 1;

			Ticket newTicket;
			using (var ctx = new TicketDbContext())
			{
				newTicket = new Ticket()
				{
					Owner = new Person()
					{
						FirstName = ticket.FirstName,
						LastName = ticket.LastName,
						MiddleName = ticket.MiddleName
					},
					Event = ctx.Events.Find(ticket.EventId),
					Seat = ctx.Seats.Find(ticket.SeatId)
				};

				ctx.Tickets.Add(newTicket);
				ctx.SaveChanges();
			}

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