namespace TicketGenerator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketInfos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventDate = c.DateTime(nullable: false),
                        EventName = c.String(),
                        Seat_Id = c.Int(nullable: false),
                        Owner_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seats", t => t.Seat_Id)
                .ForeignKey("dbo.Persons", t => t.Owner_Id)
                .Index(t => t.Seat_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Stadium = c.String(),
                        Sector = c.String(),
                        Row = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketInfos", "Owner_Id", "dbo.Persons");
            DropForeignKey("dbo.TicketInfos", "Seat_Id", "dbo.Seats");
            DropIndex("dbo.TicketInfos", new[] { "Owner_Id" });
            DropIndex("dbo.TicketInfos", new[] { "Seat_Id" });
            DropTable("dbo.Seats");
            DropTable("dbo.TicketInfos");
            DropTable("dbo.Persons");
        }
    }
}
