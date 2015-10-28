using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketGenerator.UI.Models.Helpers;

namespace TicketGenerator.UI.Models
{
	public class TicketInfo
	{
		[DisplayName("Event Name")]
		public string EventName { get; set; }

		[DisplayName("Event Date")]
		public string EventDate { get; set; }


		public string Stadium { get; set; }

		public string Sector { get; set; }

		[DisplayName("Row Number")]
		public string Row { get; set; }

		[DisplayName("Seat Number")]
		public string Number { get; set; }


		[DisplayName("First Name")]
		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		[DisplayName("Middle Name")]
		[Required(ErrorMessage = "Middle Name is required")]
		public string MiddleName { get; set; }

		public double Price { get; set; }


		public List<DropDownListItem> Events { get; set; }
		public int EventId { get; set; }

		public List<DropDownListItem> Sectors { get; set; }
		public int SectorId { get; set; }

		public int SeatId { get; set; }
	}
}