namespace TicketGenerator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newdb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TicketInfos", "Seat_Id", "dbo.Seats");
            DropForeignKey("dbo.TicketInfos", "Owner_Id", "dbo.Persons");
            DropIndex("dbo.TicketInfos", new[] { "Seat_Id" });
            DropIndex("dbo.TicketInfos", new[] { "Owner_Id" });
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Stadium_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stadiums", t => t.Stadium_Id)
                .Index(t => t.Stadium_Id);
            
            CreateTable(
                "dbo.Stadiums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RowNumber = c.Int(nullable: false),
                        SeatNumber = c.Int(nullable: false),
                        Stadium_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stadiums", t => t.Stadium_Id)
                .Index(t => t.Stadium_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Owner_Id = c.Int(nullable: false),
                        Seat_Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.Owner_Id)
                .ForeignKey("dbo.Seats", t => t.Seat_Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Seat_Id)
                .Index(t => t.Event_Id);
            
            AddColumn("dbo.Seats", "Sector_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Seats", "Sector_Id");
            AddForeignKey("dbo.Seats", "Sector_Id", "dbo.Sectors", "Id");
            DropColumn("dbo.Seats", "Stadium");
            DropColumn("dbo.Seats", "Sector");
            DropTable("dbo.TicketInfos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TicketInfos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventDate = c.DateTime(nullable: false),
                        EventName = c.String(),
                        Price = c.Double(nullable: false),
                        Seat_Id = c.Int(nullable: false),
                        Owner_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Seats", "Sector", c => c.String());
            AddColumn("dbo.Seats", "Stadium", c => c.String());
            DropForeignKey("dbo.Tickets", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Sectors", "Stadium_Id", "dbo.Stadiums");
            DropForeignKey("dbo.Seats", "Sector_Id", "dbo.Sectors");
            DropForeignKey("dbo.Tickets", "Seat_Id", "dbo.Seats");
            DropForeignKey("dbo.Tickets", "Owner_Id", "dbo.Persons");
            DropForeignKey("dbo.Events", "Stadium_Id", "dbo.Stadiums");
            DropIndex("dbo.Tickets", new[] { "Event_Id" });
            DropIndex("dbo.Tickets", new[] { "Seat_Id" });
            DropIndex("dbo.Tickets", new[] { "Owner_Id" });
            DropIndex("dbo.Seats", new[] { "Sector_Id" });
            DropIndex("dbo.Sectors", new[] { "Stadium_Id" });
            DropIndex("dbo.Events", new[] { "Stadium_Id" });
            DropColumn("dbo.Seats", "Sector_Id");
            DropTable("dbo.Tickets");
            DropTable("dbo.Sectors");
            DropTable("dbo.Stadiums");
            DropTable("dbo.Events");
            CreateIndex("dbo.TicketInfos", "Owner_Id");
            CreateIndex("dbo.TicketInfos", "Seat_Id");
            AddForeignKey("dbo.TicketInfos", "Owner_Id", "dbo.Persons", "Id");
            AddForeignKey("dbo.TicketInfos", "Seat_Id", "dbo.Seats", "Id");
        }
    }
}
