namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullFilmFinish : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "FilmFinishPremiered", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "FilmFinishPremiered", c => c.DateTime(nullable: false));
        }
    }
}
