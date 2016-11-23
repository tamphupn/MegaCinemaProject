namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAFieldToPromotionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "PromotionDateStart", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Promotions", "PromotionDateStart");
        }
    }
}
