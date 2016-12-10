namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFilmCalendarCreate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Staffs", new[] { "StaffID" });
            CreateIndex("dbo.FilmSessions", "StaffID");
            CreateIndex("dbo.FilmCalendarCreates", "StaffID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FilmCalendarCreates", new[] { "StaffID" });
            DropIndex("dbo.FilmSessions", new[] { "StaffID" });
            CreateIndex("dbo.Staffs", "StaffID");
        }
    }
}
