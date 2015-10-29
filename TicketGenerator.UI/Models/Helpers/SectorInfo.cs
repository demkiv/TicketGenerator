using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGenerator.UI.Models.Helpers
{
	public class SectorInfo
	{
        public int EventId { get; set; }
        public string EventDate { get; set; }
        public double Price { get; set; }

        public int SectorId { get; set; }
        public int RowNumber { get; set; }
		public int SeatNumber { get; set; }
	}
}