using System.Collections.Generic;

namespace TicketGenerator.Domain.Entities
{
	public class Seat
	{
		public int Id { get; set; }

		public int Row { get; set; }
		public int Number { get; set; }

		public virtual Sector Sector { get; set; }
		public virtual ICollection<Ticket> Tickets { get; set; }

		public Seat()
		{
			Tickets = new List<Ticket>();
		}
	}
}