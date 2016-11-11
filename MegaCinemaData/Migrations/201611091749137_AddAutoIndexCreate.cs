namespace MegaCinemaData.Migrations
{
    using MegaCinemaCommon.SqlScriptCodeFirst;
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddAutoIndexCreate : DbMigration
    {
        public override void Up()
        {
            ScriptGenerate scriptGenerate = new ScriptGenerate();
            Sql(scriptGenerate.AlterAutoIndexTable("Films", "FilmCode", "FilmPrefix", "FilmID"));
            Sql(scriptGenerate.AlterAutoIndexTable("Customers", "CustomerCode", "CustomerPrefix", "CustomerID"));
            Sql(scriptGenerate.AlterAutoIndexTable("Staffs", "StaffCode", "StaffPrefix", "StaffID"));
            Sql(scriptGenerate.AlterAutoIndexTable("Cinemas", "CinemaCode", "CinemaPrefix", "CinemaID"));
            Sql(scriptGenerate.AlterAutoIndexTable("FoodLists", "FoodCode", "FoodPrefix", "FoodID"));
            Sql(scriptGenerate.AlterAutoIndexTable("RoomFilms", "RoomCode", "RoomPrefix", "RoomID"));
            Sql(scriptGenerate.AlterAutoIndexTable("SeatLists", "SeatCode", "SeatPrefix", "SeatID"));
            Sql(scriptGenerate.AlterAutoIndexTable("BookingTickets", "BookingTicketCode", "BookingTicketPrefix", "BookingTicketID"));
        }

        public override void Down()
        {
        }
    }
}
