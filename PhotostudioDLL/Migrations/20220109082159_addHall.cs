using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotostudioDLL.Migrations
{
    public partial class addHall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 1,
                column: "EmploymentDate",
                value: new DateOnly(2022, 1, 9));

            migrationBuilder.InsertData(
                table: "Hall",
                columns: new[] { "ID", "Description", "PricePerHour", "Title" },
                values: new object[,]
                {
                    { 1, "Зал в белых тонах", 1500m, "Белая комната" },
                    { 2, "Зал стилизованный под морскую тематику", 2000m, "Аквариум" },
                    { 3, "Зал стилизованный под 80-ые", 1200m, "Ретро" },
                    { 4, "Зал с хромокеем", 1000m, "Зелёная комната" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 1,
                column: "EmploymentDate",
                value: new DateOnly(2022, 1, 8));
        }
    }
}
