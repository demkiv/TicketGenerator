using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGenerator.Domain.Configurations;
using TicketGenerator.Domain.Entities;

namespace TicketGenerator.Domain
{
	public class TicketDbContext : DbContext
	{
		public DbSet<Ticket> Tickets { get; set; }

		public DbSet<Person> Persons { get; set; }
		public DbSet<Event> Events { get; set; }

		public DbSet<Stadium> Stadiums { get; set; }
		public DbSet<Sector> Sectors { get; set; }
		public DbSet<Seat> Seats { get; set; }

		public TicketDbContext()
			: base("DefaultConnection")
        {
        }

		public TicketDbContext(string connString)
            : base(connString)
        {
        }

		public static TicketDbContext Create()
        {
			return new TicketDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new TicketConfigurations());
            modelBuilder.Configurations.Add(new PersonConfigurations());
			modelBuilder.Configurations.Add(new EventConfigurations());
			modelBuilder.Configurations.Add(new StadiumConfigurations());
			modelBuilder.Configurations.Add(new SectorConfigurations());
            modelBuilder.Configurations.Add(new SeatConfigurations());
        }
	}
}
