namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSessions", "SeatTableState", c => c.String());
            AddColumn("dbo.TimeSessions", "SeatTableDefault", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeSessions", "SeatTableDefault");
            DropColumn("dbo.TimeSessions", "SeatTableState");
        }
    }
}
