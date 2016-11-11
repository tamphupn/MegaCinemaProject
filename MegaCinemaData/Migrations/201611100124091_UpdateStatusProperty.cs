namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStatusProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statuss", "StatusDescription", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Statuss", "StatusDescription");
        }
    }
}
