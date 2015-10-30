using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Web;
using System.Web.Mvc;
using TicketGenerator.Domain;
using TicketGenerator.Domain.Entities;
using TicketGenerator.UI.Models;
using TicketGenerator.UI.Models.Helpers;
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
                    Events = ctx.Events.Select(e => new DropDownListItem() { Id = e.Id, Value = e.Name }).ToList(),
                    EventDate = ctx.Events.First().Date.ToString(CultureInfo.InvariantCulture),
                    Stadium = ctx.Stadiums.First().Name,
                    Sectors = ctx.Sectors.Select(e => new DropDownListItem() { Id = e.Id, Value = e.Name }).ToList(),
                    Price = ctx.Events.First().Price
                };
            }

            return View(ticketInfo);
        }

        [HttpGet]
        public ActionResult GetEventSectorInfo(int sectorId, int eventId)
        {
            using (var ctx = new TicketDbContext())
            {
                Sector sector = ctx.Sectors.Find(sectorId);
                Event e = ctx.Events.Find(eventId);
                SectorInfo sectorInfo = new SectorInfo()
                {
                    EventId = eventId,
                    EventDate = e.Date.ToString(CultureInfo.InvariantCulture),
                    Price = e.Price,

                    SectorId = sector.Id,
                    RowNumber = sector.RowNumber,
                    SeatNumber = sector.SeatNumber
                };

                return Json(sectorInfo, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult TicketInfo(TicketInfo ticket)
        {
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

			NewTicketInfo newTicketInfo = new NewTicketInfo()
			{
				TicketId = newTicket.Id,
				SeatId = newTicket.Seat.Id
			};

			return Json(newTicketInfo, JsonRequestBehavior.AllowGet);
	        //return File(CreatePDF(newTicket.Id), "application/pdf");
        }

	    public ActionResult OpenPDF(int id)
	    {
			return File(CreatePDF(id), "application/pdf");
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


        public JsonResult CreateSvgItems(SectorInfo sectorInfo)
        {
            int sectorId = 0;
            int maxRow = 0;
            int maxCol = 0;
            int eventId;
            int svgX = 60;
            int svgY = 60;
            int spaceX = 10;
            int spaceY = 10;

            if (sectorInfo.SectorId == 0)
            {
                using (var ctx = new TicketDbContext())
                {
                    sectorId = ctx.Sectors.First().Id;
                    eventId = ctx.Events.First().Id;
                    maxRow = ctx.Seats.Where(r => r.Sector.Id == sectorId).Max(s => s.Row);
                    maxCol = ctx.Seats.Where(r => r.Sector.Id == sectorId).Max(c => c.Number);
                }
            }
            else
            {
                sectorId = sectorInfo.SectorId;
                eventId = sectorInfo.EventId;
                maxRow = sectorInfo.RowNumber;
                maxCol = sectorInfo.SeatNumber;
            }

            SVG_model[,] k = new SVG_model[maxRow, maxCol];

            List<Seat> seats = new List<Seat>();
            List<Ticket> tickets = new List<Ticket>();
            List<int> seatsIdList = new List<int>();

            using (var ctx = new TicketDbContext())
            {
                seats = ctx.Seats.Where(s => s.Sector.Id == sectorId).Cast<Seat>().ToList();
                tickets = ctx.Tickets.Where(t => t.Event.Id == eventId).Cast<Ticket>().ToList();
                seatsIdList = tickets.Select(t => t.Seat.Id).ToList();
            }

            int i = 0;
            int j = 0;
            foreach (var seat in seats)
            {
                var test = new SVG_model();
                test.svgReserved = seatsIdList.Find(s => s == seat.Id) != 0;
                test.svgId = "r" + seat.Id;
                test.svgRow = i + 1;
                test.svgCol = j + 1;
                test.svgX = (svgX + spaceX)*j + 60;
                test.svgY = (svgY + spaceY)*i;
                k[i, j] = test;
                j++;
                if (j == maxCol)
                {
                    j = 0;
                    i++;
                }

            }
            return Json(k, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CreateSvgRows(SectorInfo sectorInfo)
        {
            int sectorId = 0;
            int maxRow = 0;
            int maxCol = 0;
            int svgX = 0;
            int svgY = 25;
            int spaceX = 10;
            int spaceY = 45;

            if (sectorInfo.SectorId == 0)
            {
                using (var ctx = new TicketDbContext())
                {
                    sectorId = ctx.Sectors.First().Id;
                    maxRow = ctx.Seats.Where(r => r.Sector.Id == sectorId).Max(s => s.Row);
                    maxCol = ctx.Seats.Where(r => r.Sector.Id == sectorId).Max(c => c.Number);
                }
            }
            else
            {
                sectorId = sectorInfo.SectorId;
                maxRow = sectorInfo.RowNumber;
                maxCol = sectorInfo.SeatNumber;

            }

            SVG_model[] k = new SVG_model[maxRow];
            int rowNumberX = maxCol*70 + 140;

            for (int i = 0; i < maxRow; i++)
            {
                SVG_model test = new SVG_model();
                test.svgId = "R " + (i + 1) + "";
                test.svgRow = i + 1;
                test.svgX = 25;
                test.svgY = (svgY + spaceY)*i + 25;
                k[i] = test;
            }

            return Json(k, JsonRequestBehavior.AllowGet);

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