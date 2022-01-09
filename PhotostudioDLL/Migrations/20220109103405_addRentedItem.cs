using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotostudioDLL.Migrations
{
    public partial class addRentedItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Client");

            migrationBuilder.AddColumn<bool>(
                name: "IsСlothes",
                table: "RentedItem",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "RentedItem",
                columns: new[] { "ID", "Description", "IsСlothes", "Number", "Title", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "55 размер", true, 1L, "Платье", 1000m },
                    { 2, "45 размер", true, 1L, "Костюм", 1300m }
                });

            migrationBuilder.InsertData(
                table: "RentedItem",
                columns: new[] { "ID", "Description", "Number", "Title", "UnitPrice" },
                values: new object[,]
                {
                    { 3, "Копия изместного пистолета", 8L, "Пистолет ММГ", 800m },
                    { 4, "Белый, серый, чёрный", 16L, "Голубь", 10000m },
                    { 5, "В асортименте", 100L, "Пластмассовые фрукты", 200m }
                });

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 5,
                column: "Title",
                value: "Аренда одежды");

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 10,
                column: "Title",
                value: "Аренда детской одежды");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentedItem",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RentedItem",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RentedItem",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RentedItem",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RentedItem",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "IsСlothes",
                table: "RentedItem");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Client",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 5,
                column: "Title",
                value: "Аренда платья");

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 10,
                column: "Title",
                value: "Аренда детского платья");
        }
    }
}
