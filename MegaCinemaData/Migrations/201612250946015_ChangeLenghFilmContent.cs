namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLenghFilmContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "FilmContent", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "FilmContent", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
