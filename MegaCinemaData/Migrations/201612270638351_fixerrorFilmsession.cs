namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixerrorFilmsession : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FilmSessions", new[] { "FilmSessionID" });
            CreateIndex("dbo.FilmCalendarCreates", "FilmSessionID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FilmCalendarCreates", new[] { "FilmSessionID" });
            CreateIndex("dbo.FilmSessions", "FilmSessionID");
        }
    }
}
