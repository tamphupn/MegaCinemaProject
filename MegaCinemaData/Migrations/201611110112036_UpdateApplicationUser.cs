namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicationUsers", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "Phone", c => c.String(nullable: false, maxLength: 12));
        }
    }
}
