using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhotostudioDLL.Migrations
{
    public partial class finalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    EMail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Hall",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hall", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RentedItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    IsСlothes = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsKids = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentedItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Rights = table.Column<string>(type: "text", nullable: false),
                    Responsibilities = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientID = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PassData = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    EmploymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RoleID = table.Column<int>(type: "integer", nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    EMail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employee_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceID = table.Column<int>(type: "integer", nullable: false),
                    Appointment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inventory_Service_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Service",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    ClientID = table.Column<int>(type: "integer", nullable: false),
                    EmployeeID = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contract_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Order_ID",
                        column: x => x.ID,
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProfile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeProfile_Employee_ID",
                        column: x => x.ID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderService",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderID = table.Column<int>(type: "integer", nullable: false),
                    ServiceID = table.Column<int>(type: "integer", nullable: false),
                    EmployeeID = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    RentedItemID = table.Column<int>(type: "integer", nullable: true),
                    Number = table.Column<int>(type: "integer", nullable: true),
                    HallID = table.Column<int>(type: "integer", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PhotoLocation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecuteableService", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExecuteableService_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExecuteableService_Hall_HallID",
                        column: x => x.HallID,
                        principalTable: "Hall",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ExecuteableService_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExecuteableService_RentedItem_RentedItemID",
                        column: x => x.RentedItemID,
                        principalTable: "RentedItem",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ExecuteableService_Service_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Service",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentInventory",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "integer", nullable: false),
                    InventoriesID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentInventory", x => new { x.EquipmentID, x.InventoriesID });
                    table.ForeignKey(
                        name: "FK_EquipmentInventory_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentInventory_Inventory_InventoriesID",
                        column: x => x.InventoriesID,
                        principalTable: "Inventory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ID", "EMail", "FirstName", "LastName", "MiddleName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, "Анатолий", "Берёзов", null, "+78652198674" },
                    { 2, null, "Василий", "Зубьянинко", "Егорович", "+75352109785" },
                    { 3, "email@mail.ru", "Иван", "Кислицин", "Ильич", "+79621475203" }
                });

            migrationBuilder.InsertData(
                table: "Hall",
                columns: new[] { "ID", "Cost", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 1500m, "Зал в белых тонах", "Белая комната" },
                    { 2, 2000m, "Зал стилизованный под морскую тематику", "Аквариум" },
                    { 3, 1200m, "Зал стилизованный под 80-ые", "Ретро" },
                    { 4, 1000m, "Зал с хромакеем", "Зеленая комната" }
                });

            migrationBuilder.InsertData(
                table: "RentedItem",
                columns: new[] { "ID", "Cost", "Description", "IsKids", "IsСlothes", "Number", "Title" },
                values: new object[] { 1, 1000m, "55 размер", true, true, 1L, "Платье" });

            migrationBuilder.InsertData(
                table: "RentedItem",
                columns: new[] { "ID", "Cost", "Description", "IsСlothes", "Number", "Title" },
                values: new object[] { 2, 1300m, "45 размер", true, 1L, "Костюм" });

            migrationBuilder.InsertData(
                table: "RentedItem",
                columns: new[] { "ID", "Cost", "Description", "Number", "Title" },
                values: new object[,]
                {
                    { 3, 800m, "Копия известного пистолета", 8L, "Пистолет ММГ" },
                    { 4, 10000m, "Белый, серый, черный", 16L, "Голубь" },
                    { 5, 200m, "В ассортименте", 100L, "Пластмассовые фрукты" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "Responsibilities", "Rights", "Title" },
                values: new object[,]
                {
                    { 1, "Добавлять новые услуги, новые должности и новых сотрудники (по мере необходимости)", "Доступ ко всем данным", "Администратор" },
                    { 2, "Фотографировать согласно услуге", "Доступ к предоставляемым им услугам, и инвентарю услуги", "Фотограф" },
                    { 3, "Обрабатывать фотографии согласно услуге", "Доступ к предоставляемым им услугам, и данным фотографиям", "Ретушер" },
                    { 4, "Снимать видеоматериалы согласно услуге", "Доступ к предоставляемым им услугам, и инвентарю услуги", "Оператор" },
                    { 5, "Обрабатывать видеоматериалы согласно услуге", "Доступ к предоставляемым им услугам, и данным видеоматериалами", "Монтажер" },
                    { 6, "Видоизменять клиента, согласно заявке", "Доступ к предоставляемым им услугам, и инвентарю услуги", "Стилист" },
                    { 7, "Создавать новые заявки и новых клиентов", "Доступ к клиентам, услугам и арендуемым вещам", "Менеджер" }
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "ID", "Cost", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 4000m, "Фотографирование от одного из фотографов", "Фотосессия" },
                    { 2, 6000m, "Видеосъемка от одного из операторов", "Видеосъемка" },
                    { 3, 2500m, "Авторская обработка фотографий", "Ретуширование" },
                    { 4, 10000m, "Авторская обработка видеоконтента", "Монтаж" },
                    { 5, null, "Временное изъятие из нашего склада платья для ваших нужд", "Аренда одежды" },
                    { 6, null, "Временное изъятие из нашего склада реквизита для ваших нужд", "Аренда реквизита" },
                    { 7, 3000m, "Временное изъятие из нашего склада помещения для ваших нужд", "Аренда помещения" },
                    { 8, 100m, "Распечатаем ваши фотографии", "Печать фотографий" },
                    { 9, 3000m, "Фотографирование детей от одного из фотографов", "Детская фотосессия" },
                    { 10, null, "Временное изъятие из нашего склада платья для ваших нужд", "Аренда детской одежды" },
                    { 11, 4500m, "Фотографирование на свадьбе от одного из фотографов", "Свадебная фотосессия" },
                    { 12, 10000m, "Видеосъемка на свадьбе от одного из операторов", "Свадебная видеосъемка" },
                    { 13, 5000m, "Создание образов с помощью средств макияжа", "Макияж" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "ID", "EMail", "EmploymentDate", "FirstName", "LastName", "MiddleName", "PassData", "PhoneNumber", "RoleID" },
                values: new object[,]
                {
                    { 1, null, new DateOnly(2022, 1, 13), "Вячеслав", "Берёзов", null, "6024978234", "+78005553535", 1 },
                    { 2, null, new DateOnly(2021, 11, 21), "Иван", "Власов", "Валентинович", "6852432107", "+76985324710", 2 },
                    { 3, null, new DateOnly(2021, 11, 21), "Кирилл", "Кириллов", null, "6521452089", "+72036874512", 7 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfile",
                columns: new[] { "ID", "Login", "Password" },
                values: new object[,]
                {
                    { 1, "admin", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918" },
                    { 2, "photo", "55c64d0fcd6f9d5f7c828093857e3fdfda68478bb4e9bd24d481ef391c7804e8" },
                    { 3, "manager", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_PhoneNumber",
                table: "Client",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ClientID",
                table: "Contract",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_EmployeeID",
                table: "Contract",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EMail",
                table: "Employee",
                column: "EMail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PassData",
                table: "Employee",
                column: "PassData",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PhoneNumber",
                table: "Employee",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_RoleID",
                table: "Employee",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfile_Login",
                table: "EmployeeProfile",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentInventory_InventoriesID",
                table: "EquipmentInventory",
                column: "InventoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_ExecuteableService_EmployeeID",
                table: "OrderService",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ExecuteableService_HallID",
                table: "OrderService",
                column: "HallID");

            migrationBuilder.CreateIndex(
                name: "IX_ExecuteableService_OrderID",
                table: "OrderService",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ExecuteableService_RentedItemID",
                table: "OrderService",
                column: "RentedItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ExecuteableService_ServiceID",
                table: "OrderService",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ServiceID",
                table: "Inventory",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientID",
                table: "Order",
                column: "ClientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "EmployeeProfile");

            migrationBuilder.DropTable(
                name: "EquipmentInventory");

            migrationBuilder.DropTable(
                name: "OrderService");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Hall");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "RentedItem");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
