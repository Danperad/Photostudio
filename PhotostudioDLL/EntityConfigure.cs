using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotostudioDLL.Entities;

namespace PhotostudioDLL;

public static class EntityConfigure
{
    #region Configure

    public static void EmployeeConfigure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasIndex(e => e.PhoneNumber).IsUnique();
        builder.HasIndex(e => e.PassData).IsUnique();
        builder.HasIndex(e => e.EMail).IsUnique();
        builder.Ignore(e => e.FullName);
        builder.HasOne(e => e.Profile).WithOne(p => p.Employee)
            .HasForeignKey<EmployeeProfile>(p => p.ID);
    }

    public static void ClientConfigure(EntityTypeBuilder<Client> builder)
    {
        builder.Ignore(c => c.FullName);
        builder.HasIndex(e => e.PhoneNumber).IsUnique();
        builder.Property(c => c.IsActive).HasDefaultValue(true);
    }

    public static void OrderConfigure(EntityTypeBuilder<Order> builder)
    {
        builder.Ignore(o => o.ListServices);
        builder.Ignore(o => o.TextStatus);
        builder.HasOne(e => e.Contract).WithOne(p => p.Order)
            .HasForeignKey<Contract>(p => p.ID);
    }

    public static void EmployeeProfileConfigure(EntityTypeBuilder<EmployeeProfile> builder)
    {
        builder.HasIndex(e => e.Login).IsUnique();
    }

    #endregion

    #region DataConfigure

    public static void ClientDataConfigure(EntityTypeBuilder<Client> builder)
    {
        var clients = new List<Client>();
        clients.Add(new Client("Берёзов", "Анатолий", "+78652198674") { ID = 1 });
        clients.Add(new Client("Зубьянинко", "Василий", "+75352109785", "Егорович") { ID = 2 });
        clients.Add(new Client("Кислицин", "Иван", "+79621475203", "Ильич", "email@mail.ru") { ID = 3 });
        builder.HasData(clients);
    }

    public static void EmployeeDataConfigure(EntityTypeBuilder<Employee> builder)
    {
        var employees = new List<Employee>();
        employees.Add(new Employee(1, "Берёзов", "Вячеслав",
            "+78005553535", "6024978234", DateOnly.FromDateTime(DateTime.Today)) { ID = 1 });
        employees.Add(new Employee(2, "Власов", "Иван",
                "+76985324710", "6852432107", new DateOnly(2021, 11, 21))
            { MiddleName = "Валентинович", ID = 2 });
        employees.Add(new Employee(7, "Кириллов", "Кирилл",
                "+72036874512", "6521452089", new DateOnly(2021, 11, 21))
            {ID = 3 });
        builder.HasData(employees);
    }

    public static void RoleDataConfigure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role("Администратор", "Доступ ко всем данным",
                "Добавлять новые услуги, новые должности и новых сотрудники (по мере необходимости)") { ID = 1 },
            new Role("Фотограф", "Доступ к предоставляемым им услугам, и инвентарю услуги",
                "Фотографировать согласно услуге") { ID = 2 },
            new Role("Ретушер", "Доступ к предоставляемым им услугам, и данным фотографиям",
                "Обрабатывать фотогафии согласно услуге") { ID = 3 },
            new Role("Оператор", "Доступ к предоставляемым им услугам, и инвентарю услуги",
                "Снимать видеоматериалы согласно услуге") { ID = 4 },
            new Role("Монтажер", "Доступ к предоставляемым им услугам, и данным видеоматериалами",
                "Обрабатывать видеоматериалы согласно услуге") { ID = 5 },
            new Role("Стилист", "Доступ к предоставляемым им услугам, и инвентарю услуги",
                "Видоизменять клиента, согласно заявке") { ID = 6 },
            new Role("Менеджер", "Доступ к клиентам, услугам и арендуемым вещам",
                    "Создавать новые заявки и новых клиентов")
                { ID = 7 });
    }

    public static void EmployeeProfileDataConfigure(EntityTypeBuilder<EmployeeProfile> builder)
    {
        builder.HasData(new EmployeeProfile("admin",
            "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918") { ID = 1 }); // pass: admin
        builder.HasData(new EmployeeProfile("photo",
            "55c64d0fcd6f9d5f7c828093857e3fdfda68478bb4e9bd24d481ef391c7804e8") { ID = 2 }); // pass: photo
        builder.HasData(new EmployeeProfile("manager",
            "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4") { ID = 3 }); // pass: 1234
    }

    #endregion
}