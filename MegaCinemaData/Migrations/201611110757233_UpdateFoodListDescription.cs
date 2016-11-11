namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFoodListDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodLists", "FoodDescription", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodLists", "FoodDescription");
        }
    }
}
