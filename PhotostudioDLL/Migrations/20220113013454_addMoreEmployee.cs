using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotostudioDLL.Migrations
{
    public partial class addMoreEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "ID", "EMail", "EmploymentDate", "FirstName", "LastName", "MiddleName", "PassData", "PhoneNumber", "RoleID" },
                values: new object[,]
                {
                    { 4, null, new DateOnly(2021, 11, 21), "Яе", "Иллюмейтов", null, "3201459874", "+76520764852", 3 },
                    { 5, null, new DateOnly(2021, 11, 21), "Владислава", "Михалкова", null, "8964203678", "+76458213078", 6 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfile",
                columns: new[] { "ID", "Login", "Password" },
                values: new object[,]
                {
                    { 4, "retush", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" },
                    { 5, "style", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeProfile",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmployeeProfile",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 5);
        }
    }
}
