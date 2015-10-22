using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator.Domain.Entities
{
	public class TicketInfo
	{
		public int Id { get; set; }
		
		public DateTime EventDate { get; set; }
		public string EventName { get; set; }
		public string Price { get; set; }

		public virtual Person Owner { get; set; }
		public virtual Seat Seat{ get; set; }
	}
}
