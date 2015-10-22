using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketGenerator.Domain;
using TicketGenerator.Domain.Entities;
using TicketGenerator.Models;

namespace TicketGenerator.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
		public ActionResult Index()
		{
			using (var ctx = new TicketDbContext())
			{
				TicketInfo ticket = new TicketInfo()
				{
					EventDate = new DateTime(2015, 10, 20, 21, 45, 0),
					EventName = "Динамо - Челсі",

					Owner = new Person()
					{
						FirstName = "Соломія",
						LastName = "Демків",
						MiddleName = "Тарасівна"
					},

					Seat = new Seat()
					{
						Stadium = "Арена Львів",
						Sector = "A6",
						Row = 15,
						Number = 7
					}
				};

				ctx.TicketInfos.Add(ticket);
				ctx.SaveChanges();
			}

			return View();
		}

		public ActionResult TicketInfo()
		{
			using (var ctx = new TicketDbContext())
			{
				TicketInfo ticket = new TicketInfo()
				{
					EventDate = new DateTime(2015, 10, 20, 21, 45, 0),
					EventName = "Динамо - Челсі",

					Owner = new Person()
					{
						FirstName = "Соломія",
						LastName = "Демків",
						MiddleName = "Тарасівна"
					},

					Seat = new Seat()
					{
						Stadium = "Арена Львів",
						Sector = "A6",
						Row = 15,
						Number = 7
					}
				};

				ctx.TicketInfos.Add(ticket);
				ctx.SaveChanges();
			}
			return View();
		}

		[HttpPost]
		public ActionResult TicketInfo(Ticket ticket)
		{
			if (!ModelState.IsValid)
				return View();

			using (var ctx = new TicketDbContext())
			{
				var newticket = new TicketInfo()
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
				//ctx.SaveChanges();
			}

			return RedirectToAction("DownloadTicket");
		}

		public ActionResult DownloadTicket()
		{
			return View();
		}

	    public ActionResult TicketReport()
	    {
		    return Redirect("../Reports/TicketForm.aspx");
	    }

		public ActionResult Report(string id)
		{
			//LocalReport lr = new LocalReport();
			//string path = Path.Combine(Server.MapPath("~/Report"), "Report.rdlc");
			//if (System.IO.File.Exists(path))
			//{
			//	lr.ReportPath = path;
			//}
			//else
			//{
			//	return View("DownloadTicket");
			//}

			//List<Person> list = new List<Person>();
			//TicketInfo ti = new TicketInfo();
			//using (var ctx = new TicketDbContext())
			//{
			//	ti = ctx.TicketInfos.First<TicketInfo>();
			//	list.Add(ti.Owner);
			//}
			
			//ReportDataSource rd = new ReportDataSource("MyDataSet", list);
			//lr.DataSources.Add(rd);

			//string reportType = id;
			//string mimeType;
			//string encoding;
			//string fileNameExtension;



			//string deviceInfo =

			//"<DeviceInfo>" +
			//"  <OutputFormat>" + id + "</OutputFormat>" +
			//"  <PageWidth>8.5in</PageWidth>" +
			//"  <PageHeight>11in</PageHeight>" +
			//"  <MarginTop>0.5in</MarginTop>" +
			//"  <MarginLeft>1in</MarginLeft>" +
			//"  <MarginRight>1in</MarginRight>" +
			//"  <MarginBottom>0.5in</MarginBottom>" +
			//"</DeviceInfo>";

			//Warning[] warnings;
			//string[] streams;
			//byte[] renderedBytes;

			//renderedBytes = lr.Render(
			//	reportType,
			//	deviceInfo,
			//	out mimeType,
			//	out encoding,
			//	out fileNameExtension,
			//	out streams,
			//	out warnings);


			//return File(renderedBytes, mimeType);
			return new EmptyResult();
		}
	}
}