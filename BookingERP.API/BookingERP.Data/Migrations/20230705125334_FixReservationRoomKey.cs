using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingERP.Data.Migrations
{
    public partial class FixReservationRoomKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationRooms",
                table: "ReservationRooms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationRooms",
                table: "ReservationRooms",
                columns: new[] { "ReservationId", "RoomId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationRooms",
                table: "ReservationRooms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationRooms",
                table: "ReservationRooms",
                column: "ReservationId");
        }
    }
}
