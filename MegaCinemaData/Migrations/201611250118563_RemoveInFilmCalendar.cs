namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveInFilmCalendar : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FilmCalendarCreates", new[] { "StaffID" });
            RenameColumn(table: "dbo.FilmCalendarCreates", name: "StaffID", newName: "Staff_StaffID");
            AlterColumn("dbo.FilmCalendarCreates", "Staff_StaffID", c => c.Int());
            CreateIndex("dbo.FilmCalendarCreates", "Staff_StaffID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FilmCalendarCreates", new[] { "Staff_StaffID" });
            AlterColumn("dbo.FilmCalendarCreates", "Staff_StaffID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.FilmCalendarCreates", name: "Staff_StaffID", newName: "StaffID");
            CreateIndex("dbo.FilmCalendarCreates", "StaffID");
        }
    }
}
