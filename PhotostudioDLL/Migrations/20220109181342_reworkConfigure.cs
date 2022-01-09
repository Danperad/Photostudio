using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotostudioDLL.Migrations
{
    public partial class reworkConfigure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Service",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "RentedItem",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "PricePerHour",
                table: "Hall",
                newName: "Cost");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartRent",
                table: "ServiceProvided",
                type: "time without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndRent",
                table: "ServiceProvided",
                type: "time without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "RentDate",
                table: "ServiceProvided",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Service",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "RentedItem",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Hall",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Client",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentDate",
                table: "ServiceProvided");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Service",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "RentedItem",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Hall",
                newName: "PricePerHour");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartRent",
                table: "ServiceProvided",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndRent",
                table: "ServiceProvided",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Service",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "RentedItem",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Hall",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
