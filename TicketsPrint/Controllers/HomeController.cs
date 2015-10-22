using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketGenerator.Domain;
using TicketGenerator.Domain.Entities;
using TicketsPrint.Models;
using System.Web.Services.Protocols;
using TicketsPrint.ReportingServicesReference;

namespace TicketsPrint.Controllers
{
	public class HomeController : Controller
	{
		
		public ActionResult TicketInfo()
		{
			return View();
		}

		[HttpPost]
		public ActionResult TicketInfo(Ticket ticket)
		{
			if (!ModelState.IsValid)
				return View();

			TicketInfo newticket;
			using (var ctx = new TicketDbContext())
			{
				newticket = new TicketInfo()
				{
					EventDate = ticket.EventDate,
					EventName = ticket.EventName,

					Owner = new Person()
					{
						FirstName = ticket.FirstName,
						LastName = ticket.LastName,
						MiddleName = ticket.MiddleName
					},

					Seat = new Seat()
					{
						Stadium = ticket.Stadium,
						Sector = ticket.Sector,
						Row = ticket.Row,
						Number = ticket.Number
					}
				};

				ctx.TicketInfos.Add(newticket);
				ctx.SaveChanges();
			}

			return File(CreatePDF(newticket.Id), "application/pdf");
		}

		private byte[] CreatePDF(int ticketId)
		{
			ReportExecutionService rs = new ReportExecutionService();

			string format = "PDF";
			string reportPath = "/Reports/TicketReport";
			string mimeType = "application/pdf";

			var parameters = new [] { 
				new ParameterValue
				{
					Name = "TicketNumber",
					Value = ticketId.ToString()
				},
				new ParameterValue
				{
					Name = "Price",
					Value = "100"
				}
			};

			//var parameters = new ParameterValue[1];
			//parameters[0] = new ParameterValue
			//{
			//	Name = "TicketNumber",
			//	Value = ticketId.ToString()
			//};

			return RenderReport(rs, format, mimeType, reportPath, parameters);
		}

		private static byte[] RenderReport(ReportExecutionService rs, string format, string mimeType, string reportPath, ParameterValue[] parameters)
		{
			rs.Credentials = CredentialCache.DefaultCredentials;

			string deviceInfo = String.Empty;
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