using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator.Domain.Entities
{
	public class Stadium
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Event> Events { get; set; }
		public virtual ICollection<Sector> Sectors { get; set; }

		public Stadium()
		{
			Events = new List<Event>();
			Sectors = new List<Sector>();
		}
	}
}
