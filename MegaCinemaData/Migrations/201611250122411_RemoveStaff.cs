namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStaff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilmCalendarCreates", "Staff_StaffID", "dbo.Staffs");
            DropIndex("dbo.FilmCalendarCreates", new[] { "Staff_StaffID" });
            DropColumn("dbo.FilmCalendarCreates", "Staff_StaffID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FilmCalendarCreates", "Staff_StaffID", c => c.Int());
            CreateIndex("dbo.FilmCalendarCreates", "Staff_StaffID");
            AddForeignKey("dbo.FilmCalendarCreates", "Staff_StaffID", "dbo.Staffs", "StaffID");
        }
    }
}
