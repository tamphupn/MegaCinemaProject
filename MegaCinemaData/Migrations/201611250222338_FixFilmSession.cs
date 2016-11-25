namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFilmSession : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilmSessions", "StaffID", "dbo.Staffs");
            DropIndex("dbo.FilmSessions", new[] { "StaffID" });
            DropColumn("dbo.FilmSessions", "StaffID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FilmSessions", "StaffID", c => c.Int(nullable: false));
            CreateIndex("dbo.FilmSessions", "StaffID");
            AddForeignKey("dbo.FilmSessions", "StaffID", "dbo.Staffs", "StaffID");
        }
    }
}
