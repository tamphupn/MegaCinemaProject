namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignkeyFilmCalendar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilmCalendarCreates", "StaffID", c => c.Int(nullable: false));
            CreateIndex("dbo.FilmCalendarCreates", "StaffID");
            AddForeignKey("dbo.FilmCalendarCreates", "StaffID", "dbo.Staffs", "StaffID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilmCalendarCreates", "StaffID", "dbo.Staffs");
            DropIndex("dbo.FilmCalendarCreates", new[] { "StaffID" });
            DropColumn("dbo.FilmCalendarCreates", "StaffID");
        }
    }
}
