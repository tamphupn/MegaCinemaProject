namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilmCategoriesTableFIlm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "FilmCategories", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Films", "FilmCategories");
        }
    }
}
