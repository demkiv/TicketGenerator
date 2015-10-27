using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator.Domain.Entities
{
	public class Sector
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public int RowNumber { get; set; }
		public int SeatNumber { get; set; }

		public virtual Stadium Stadium { get; set; }
		public virtual ICollection<Seat> Seats { get; set; }

		public Sector()
		{
			Seats = new List<Seat>();
		}
	}
}
