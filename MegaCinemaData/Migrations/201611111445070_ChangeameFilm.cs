namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeameFilm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "FilmFinishPremiered", c => c.DateTime(nullable: false));
            DropColumn("dbo.Films", "FilmLastPremiered");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "FilmLastPremiered", c => c.DateTime(nullable: false));
            DropColumn("dbo.Films", "FilmFinishPremiered");
        }
    }
}
