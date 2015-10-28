namespace TicketGenerator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStadiumEventrelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Stadium_Id", "dbo.Stadiums");
            DropIndex("dbo.Events", new[] { "Stadium_Id" });
            DropColumn("dbo.Events", "Stadium_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Stadium_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "Stadium_Id");
            AddForeignKey("dbo.Events", "Stadium_Id", "dbo.Stadiums", "Id");
        }
    }
}
