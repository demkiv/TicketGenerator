using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGenerator.UI.Models.Helpers
{
	public class NewTicketInfo
	{
		public bool IsSuccessfulOperation { get; set; }
		public int TicketId { get; set; }
		public int SeatId { get; set; }
	}
}