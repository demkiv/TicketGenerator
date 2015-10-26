namespace TicketGenerator.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePricetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketInfos", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketInfos", "Price", c => c.String());
        }
    }
}
