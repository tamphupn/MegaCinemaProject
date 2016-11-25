namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinishFixStaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilmSessions", "StaffID", c => c.Int(nullable: false));
            CreateIndex("dbo.FilmSessions", "StaffID");
            AddForeignKey("dbo.FilmSessions", "StaffID", "dbo.Staffs", "StaffID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilmSessions", "StaffID", "dbo.Staffs");
            DropIndex("dbo.FilmSessions", new[] { "StaffID" });
            DropColumn("dbo.FilmSessions", "StaffID");
        }
    }
}
