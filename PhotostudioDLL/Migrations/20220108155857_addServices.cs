using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotostudioDLL.Migrations
{
    public partial class addServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "ID", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Фотографирование от одного из фотографов", 4000m, "Фотосессия" },
                    { 2, "Видеосъёмка от одного из операторов", 6000m, "Видеосъёмка" },
                    { 3, "Авторская обработка фотографий", 2500m, "Ретуширование" },
                    { 4, "Авторская обработка видеоконтента", 10000m, "Монтаж" },
                    { 5, "Временное изъятие из нашего склада платья для ваших нужд", 5000m, "Аренда платья" },
                    { 6, "Временное изъятие из нашего склада реквизита для ваших нужд", 3000m, "Аренда реквизита" },
                    { 7, "Временное изъятие из нашего склада помещения для ваших нужд", 3000m, "Аренда помещения" },
                    { 8, "Распечатаем ваши фотографии", 100m, "Печать фотографий" },
                    { 9, "Фотографирование детей от одного из фотографов", 3000m, "Детская фотосессия" },
                    { 10, "Временное изъятие из нашего склада платья для ваших нужд", 5000m, "Аренда детского платья" },
                    { 11, "Фотографирование на свадьбе от одного из фотографов", 4500m, "Свадебная фотосессия" },
                    { 12, "Видеосъёмка на свадьбе от одного из операторов", 10000m, "Свадебная видеосъёмка" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ID",
                keyValue: 12);
        }
    }
}
