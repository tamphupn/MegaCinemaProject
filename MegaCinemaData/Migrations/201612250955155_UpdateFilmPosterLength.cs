namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFilmPosterLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "FilmPoster", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "FilmPoster", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
