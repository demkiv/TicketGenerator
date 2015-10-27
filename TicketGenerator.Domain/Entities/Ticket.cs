using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator.Domain.Entities
{
	public class Ticket
	{
		public int Id { get; set; }

		public virtual Event Event { get; set; }
		public virtual Person Owner { get; set; }
		public virtual Seat Seat { get; set; }
	}
}
