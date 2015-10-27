using System.Collections.Generic;
using TicketGenerator.Domain.Entities;

namespace TicketGenerator.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TicketGenerator.Domain.TicketDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TicketGenerator.Domain.TicketDbContext context)
        {
	        var sectors = new List<Sector>
	        {
		        new Sector() {Name = "A1", RowNumber = 5, SeatNumber = 7},
		        new Sector() {Name = "A2", RowNumber = 9, SeatNumber = 7}
	        };

	        foreach (Sector sector in sectors)
	        {
		        for (int i = 0; i < sector.RowNumber; i++)
		        {
			        for (int j = 0; j < sector.SeatNumber; j++)
			        {
						Seat seat = new Seat()
						{
							Row = i + 1,
							Number = j + 1,
						};

						sector.Seats.Add(seat);
			        }
		        }
	        }

			Stadium stadium = new Stadium()
			{
				Name = "Arena Lviv",
				Sectors = sectors
			};

			var events = new List<Event>()
	        {
				new Event()
				{
					Name = "Liverpool - Chelsea",
					Date = new DateTime(2015, 11, 25, 21, 30, 0),
					Price = 100,
					Stadium = stadium
				},
				new Event()
				{
					Name = "Juventus - Milan",
					Date = new DateTime(2015, 11, 18, 20, 30, 0),
					Price = 150,
					Stadium = stadium
				},
				new Event()
				{
					Name = "Arsenal - Barcelona",
					Date = new DateTime(2015, 12, 6, 20, 0, 0),
					Price = 200,
					Stadium = stadium
				}
	        };

	        foreach (var e in events)
	        {
				context.Events.Add(e);
	        }

			base.Seed(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
