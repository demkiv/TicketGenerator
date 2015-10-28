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

		public virtual ICollection<Sector> Sectors { get; set; }

		public Stadium()
		{
			Sectors = new List<Sector>();
		}
	}
}
