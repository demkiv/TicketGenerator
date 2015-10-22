namespace TicketGenerator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketInfos", "Price", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketInfos", "Price");
        }
    }
}
