using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator.Domain.Entities
{
	public class Seat
	{
		public int Id { get; set; }

		public string Stadium { get; set; }
		public string Sector { get; set; }
		public int Row { get; set; }
		public int Number { get; set; }

		public virtual ICollection<TicketInfo> TicketInfos { get; set; }

		public Seat()
		{
			TicketInfos = new List<TicketInfo>();
		}
	}
}
