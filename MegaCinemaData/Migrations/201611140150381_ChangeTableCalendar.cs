namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTableCalendar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilmCalendarCreates",
                c => new
                    {
                        FilmCalendarCreateID = c.Int(nullable: false, identity: true),
                        FilmSessionID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                        FilmCalendarContent = c.String(nullable: false),
                        FilmCalendarDescription = c.String(maxLength: 100),
                        StatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FilmCalendarCreateID)
                .ForeignKey("dbo.Statuss", t => t.StatusID)
                .Index(t => t.StatusID);
            
            AddColumn("dbo.FilmSessions", "AssignDescription", c => c.String(maxLength: 100));
            AddColumn("dbo.FilmSessions", "StaffID", c => c.Int(nullable: false));
            CreateIndex("dbo.FilmSessions", "FilmSessionID");
            CreateIndex("dbo.Staffs", "StaffID");
            AddForeignKey("dbo.Staffs", "StaffID", "dbo.FilmCalendarCreates", "FilmCalendarCreateID");
            AddForeignKey("dbo.Staffs", "StaffID", "dbo.FilmSessions", "FilmSessionID");
            AddForeignKey("dbo.FilmSessions", "FilmSessionID", "dbo.FilmCalendarCreates", "FilmCalendarCreateID");
            DropColumn("dbo.FilmSessions", "FilmCalendar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FilmSessions", "FilmCalendar", c => c.String(nullable: false));
            DropForeignKey("dbo.FilmSessions", "FilmSessionID", "dbo.FilmCalendarCreates");
            DropForeignKey("dbo.FilmCalendarCreates", "StatusID", "dbo.Statuss");
            DropForeignKey("dbo.Staffs", "StaffID", "dbo.FilmSessions");
            DropForeignKey("dbo.Staffs", "StaffID", "dbo.FilmCalendarCreates");
            DropIndex("dbo.Staffs", new[] { "StaffID" });
            DropIndex("dbo.FilmCalendarCreates", new[] { "StatusID" });
            DropIndex("dbo.FilmSessions", new[] { "FilmSessionID" });
            DropColumn("dbo.FilmSessions", "StaffID");
            DropColumn("dbo.FilmSessions", "AssignDescription");
            DropTable("dbo.FilmCalendarCreates");
        }
    }
}
