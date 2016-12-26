namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdsBanners",
                c => new
                    {
                        AdsId = c.Int(nullable: false, identity: true),
                        FilmId = c.Int(nullable: false),
                        AdsDescription = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.AdsId)
                .ForeignKey("dbo.Films", t => t.FilmId)
                .Index(t => t.FilmId);
            
            CreateTable(
                "dbo.EventTopics",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventTitle = c.String(),
                        EventContent = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.EventId);
            
            AlterColumn("dbo.CinemaFeatures", "FeatureContent", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdsBanners", "FilmId", "dbo.Films");
            DropIndex("dbo.AdsBanners", new[] { "FilmId" });
            AlterColumn("dbo.CinemaFeatures", "FeatureContent", c => c.String(nullable: false, maxLength: 100));
            DropTable("dbo.EventTopics");
            DropTable("dbo.AdsBanners");
        }
    }
}
