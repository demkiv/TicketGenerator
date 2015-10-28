﻿using System;
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
        public JsonResult CreateSvgItems()
        {
            int sectorId = 2;

            using (var ctx = new TicketDbContext())
            {
                var seatsOfSector = (from s in ctx.Seats where s.Sector.Id == sectorId select s).ToList();
                             
               
                int svgX = 60;
                int svgY = 60;
                int spaceX = 10;
                int spaceY = 10;

                int maxRow = seatsOfSector.Max(e => e.Number);
                int maxCol = seatsOfSector.Max(e => e.Row);
                SVG_model[,] k = new SVG_model[maxRow, maxCol];


                for (int i = 0; i < maxRow; i++)
                {
                    for (int j = 0; j < maxCol; j++)
                    {
                        SVG_model test = new SVG_model();
                        test.svgId = "svg" + (i + 1).ToString() + (j + 1).ToString();
                        test.svgRow = i + 1;
                        test.svgCol = j + 1;
                        test.svgX = (svgX + spaceX) * i;
                        test.svgY = (svgY + spaceY) * j;
                        k[i, j] = test;

                    }
                }

                return Json(k, JsonRequestBehavior.AllowGet);
            }
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