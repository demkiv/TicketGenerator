using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketsPrint.Models
{
	public class Ticket
	{
		[DisplayName("Event Name")]
		[Required(ErrorMessage = "Name is required")]
		public string EventName { get; set; }

		[DisplayName("Event Date")]
		[Required(ErrorMessage = "Date is required")]
		public DateTime EventDate { get; set; }


		[DisplayName("First Name")]
		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		[DisplayName("Middle Name")]
		[Required(ErrorMessage = "Middle Name is required")]
		public string MiddleName { get; set; }


		[DisplayName("Stadium")]
		[Required(ErrorMessage = "Stadium is required")]
		public string Stadium { get; set; }

		[DisplayName("Sector")]
		[Required(ErrorMessage = "Sector is required")]
		public string Sector { get; set; }

		[DisplayName("Row Number")]
		[Required(ErrorMessage = "Row Number is required")]
		public int Row { get; set; }

		[DisplayName("Seat Number")]
		[Required(ErrorMessage = "Seat Number is required")]
		public int Number { get; set; }
	}
}
