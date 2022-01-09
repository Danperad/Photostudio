using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotostudioDLL.Entities;

namespace PhotostudioDLL;

public static class EntityConfigure
{
    #region Configure

    public static void EmployeeConfigure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
        builder.Property(e => e.MiddleName).HasMaxLength(50);
        builder.Property(e => e.EMail).HasMaxLength(50);
        builder.HasIndex(e => e.PhoneNumber).IsUnique();
        builder.HasIndex(e => e.PassData).IsUnique();
        builder.HasIndex(e => e.EMail).IsUnique();
        builder.Property(e => e.PassData).HasMaxLength(10);
        builder.Ignore(e => e.FullName);
        builder.HasOne(e => e.Profile).WithOne(p => p.Employee)
            .HasForeignKey<EmployeeProfile>(p => p.ID);
    }

    public static void ClientConfigure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
        builder.Property(e => e.MiddleName).HasMaxLength(50);
        builder.Property(e => e.EMail).HasMaxLength(50);
        builder.Ignore(c => c.FullName);
        builder.HasIndex(e => e.PhoneNumber).IsUnique();
    }

    public static void OrderConfigure(EntityTypeBuilder<Order> builder)
    {
        builder.Ignore(o => o.ListServices);
        builder.Ignore(o => o.AllGetCost);
        builder.Ignore(o => o.TextStatus);
        builder.HasOne(e => e.Contract).WithOne(p => p.Order)
            .HasForeignKey<Contract>(p => p.ID);
    }

    public static void EmployeeProfileConfigure(EntityTypeBuilder<EmployeeProfile> builder)
    {
        builder.HasIndex(e => e.Login).IsUnique();
        builder.Property(e => e.Login).HasMaxLength(50);
        builder.Property(e => e.Password).HasMaxLength(64);
    }

    public static void RentedItemConfigure(EntityTypeBuilder<RentedItem> builder)
    {
        builder.Property(r => r.IsСlothes).HasDefaultValue(false);
        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.Cost).IsRequired().HasColumnType("money");
        builder.Property(e => e.Title).HasMaxLength(50);
    }
    
    public static void HallConfigure(EntityTypeBuilder<Hall> builder)
    {
        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.Cost).IsRequired().HasColumnType("money");
        builder.Property(e => e.Title).HasMaxLength(50);
    }
    public static void ServiceConfigure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.Cost).IsRequired().HasColumnType("money");
        builder.Property(e => e.Title).HasMaxLength(50);
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
        var profiles = new List<EmployeeProfile>();
        profiles.Add(new EmployeeProfile("admin",
            "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918") { ID = 1 }); // pass: admin
        profiles.Add(new EmployeeProfile("photo",
            "55c64d0fcd6f9d5f7c828093857e3fdfda68478bb4e9bd24d481ef391c7804e8") { ID = 2 }); // pass: photo
        profiles.Add(new EmployeeProfile("manager",
            "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4") { ID = 3 }); // pass: 1234
        builder.HasData(profiles);
    }
    
    public static void ServiceDataConfigure(EntityTypeBuilder<Service> builder)
    {
        var services = new List<Service>();
        services.Add(new Service("Фотосессия", "Фотографирование от одного из фотографов", 4000){ID = 1});
        services.Add(new Service("Видеосъёмка", "Видеосъёмка от одного из операторов", 6000){ID = 2});
        services.Add(new Service("Ретуширование", "Авторская обработка фотографий", 2500){ID = 3});
        services.Add(new Service("Монтаж", "Авторская обработка видеоконтента", 10000){ID = 4});
        services.Add(new Service("Аренда одежды", "Временное изъятие из нашего склада платья для ваших нужд", 5000){ID = 5});
        services.Add(new Service("Аренда реквизита", "Временное изъятие из нашего склада реквизита для ваших нужд", 3000){ID = 6});
        services.Add(new Service("Аренда помещения", "Временное изъятие из нашего склада помещения для ваших нужд", 3000){ID = 7});
        services.Add(new Service("Печать фотографий", "Распечатаем ваши фотографии", 100){ID = 8});
        services.Add(new Service("Детская фотосессия", "Фотографирование детей от одного из фотографов", 3000){ID = 9});
        services.Add(new Service("Аренда детской одежды", "Временное изъятие из нашего склада платья для ваших нужд", 5000){ID = 10});
        services.Add(new Service("Свадебная фотосессия", "Фотографирование на свадьбе от одного из фотографов", 4500){ID = 11});
        services.Add(new Service("Свадебная видеосъёмка", "Видеосъёмка на свадьбе от одного из операторов", 10000){ID = 12});

        builder.HasData(services);
    }

    public static void HallDataConfigure(EntityTypeBuilder<Hall> builder)
    {
        var halls = new List<Hall>();
        halls.Add(new Hall("Белая комната", "Зал в белых тонах", 1500){ID = 1});
        halls.Add(new Hall("Аквариум", "Зал стилизованный под морскую тематику", 2000){ID = 2});
        halls.Add(new Hall("Ретро", "Зал стилизованный под 80-ые", 1200){ID = 3});
        halls.Add(new Hall("Зелёная комната", "Зал с хромокеем", 1000){ID = 4});

        builder.HasData(halls);
    }
    
    public static void RentedItemDataConfigure(EntityTypeBuilder<RentedItem> builder)
    {
        var items = new List<RentedItem>();
        items.Add(new RentedItem("Платье", "55 размер", 1, 1000, true){ID = 1});
        items.Add(new RentedItem("Костюм", "45 размер", 1, 1300, true){ID = 2});
        items.Add(new RentedItem("Пистолет ММГ", "Копия изместного пистолета", 8, 800, false){ID = 3});
        items.Add(new RentedItem("Голубь", "Белый, серый, чёрный", 16, 10000, false) {ID = 4});
        items.Add(new RentedItem("Пластмассовые фрукты", "В асортименте", 100, 200, false) {ID = 5});
        
        builder.HasData(items);
    }

    #endregion
}