using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotostudioDLL.Entities;

namespace PhotostudioDLL;

/// <summary>
///     Класс для настройки базы данных
/// </summary>
public static class EntityConfigure
{
    /// <summary>
    ///     Дополнительные ограничения при создании БД
    /// </summary>
    /// <param name="builder"></param>

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
        builder.Property(r => r.IsKids).HasDefaultValue(false);
        builder.Property(e => e.Cost).IsRequired().HasColumnType("money");
        builder.Property(e => e.Title).HasMaxLength(50);
    }

    public static void HallConfigure(EntityTypeBuilder<Hall> builder)
    {
        builder.Property(e => e.Cost).IsRequired().HasColumnType("money");
        builder.Property(e => e.Title).HasMaxLength(50);
    }

    public static void ServiceConfigure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(e => e.Title).HasMaxLength(50);
        builder.Property(e => e.Cost).HasColumnType("money");
    }

    #endregion

    /// <summary>
    ///     Заполнение данными таблиц
    /// </summary>
    /// <param name="builder"></param>

    #region DataConfigure

    public static void ClientDataConfigure(EntityTypeBuilder<Client> builder)
    {
        var clients = new List<Client>
        {
            new("Берёзов", "Анатолий", "+78652198674") {ID = 1},
            new("Зубьянинко", "Василий", "+75352109785", "Егорович") {ID = 2},
            new("Кислицин", "Иван", "+79621475203", "Ильич", "email@mail.ru") {ID = 3}
        };
        builder.HasData(clients);
    }

    public static void EmployeeDataConfigure(EntityTypeBuilder<Employee> builder)
    {
        var employees = new List<Employee>
        {
            new(1, "Берёзов", "Вячеслав",
                "+78005553535", "6024978234", DateOnly.FromDateTime(DateTime.Today)) {ID = 1},
            new(2, "Власов", "Иван",
                    "+76985324710", "6852432107", new DateOnly(2021, 11, 21))
                {MiddleName = "Валентинович", ID = 2},
            new(7, "Кириллов", "Кирилл",
                    "+72036874512", "6521452089", new DateOnly(2021, 11, 21))
                {ID = 3},
            new(3, "Иллюмейтов", "Яе",
                    "+76520764852", "3201459874", new DateOnly(2021, 11, 21))
                {ID = 4},
            new(6, "Михалкова", "Владислава",
                    "+76458213078", "8964203678", new DateOnly(2021, 11, 21))
                {ID = 5}
        };
        builder.HasData(employees);
    }

    public static void RoleDataConfigure(EntityTypeBuilder<Role> builder)
    {
        var roles = new List<Role>
        {
            new("Администратор", "Доступ ко всем данным",
                "Добавлять новые услуги, новые должности и новых сотрудники (по мере необходимости)") {ID = 1},
            new("Фотограф", "Доступ к предоставляемым им услугам, и инвентарю услуги",
                "Фотографировать согласно услуге") {ID = 2},
            new("Ретушер", "Доступ к предоставляемым им услугам, и данным фотографиям",
                "Обрабатывать фотографии согласно услуге") {ID = 3},
            new("Оператор", "Доступ к предоставляемым им услугам, и инвентарю услуги",
                "Снимать видеоматериалы согласно услуге") {ID = 4},
            new("Монтажер", "Доступ к предоставляемым им услугам, и данным видеоматериалами",
                "Обрабатывать видеоматериалы согласно услуге") {ID = 5},
            new("Стилист", "Доступ к предоставляемым им услугам, и инвентарю услуги",
                "Видоизменять клиента, согласно заявке") {ID = 6},
            new("Менеджер", "Доступ к клиентам, услугам и арендуемым вещам",
                    "Создавать новые заявки и новых клиентов")
                {ID = 7}
        };
        builder.HasData(roles);
    }

    public static void EmployeeProfileDataConfigure(EntityTypeBuilder<EmployeeProfile> builder)
    {
        var profiles = new List<EmployeeProfile>
        {
            new("admin",
                "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918") {ID = 1}, // pass: admin
            new("photo",
                "55c64d0fcd6f9d5f7c828093857e3fdfda68478bb4e9bd24d481ef391c7804e8") {ID = 2}, // pass: photo
            new("manager",
                "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4") {ID = 3}, // pass: 1234
            new("retush",
                "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4") {ID = 4}, // pass: 1234
            new("style",
                "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4") {ID = 5} // pass: 1234
        };
        builder.HasData(profiles);
    }

    public static void ServiceDataConfigure(EntityTypeBuilder<Service> builder)
    {
        var services = new List<Service>
        {
            new("Фотосессия", "Фотографирование от одного из фотографов", Service.ServiceType.PHOTOVIDEO, 4000)
                {ID = 1},
            new("Видеосъемка", "Видеосъемка от одного из операторов", Service.ServiceType.PHOTOVIDEO, 6000) {ID = 2},
            new("Ретуширование", "Авторская обработка фотографий", Service.ServiceType.SIMPLE,2500) {ID = 3},
            new("Монтаж", "Авторская обработка видеоконтента", Service.ServiceType.SIMPLE, 10000) {ID = 4},
            new("Аренда одежды", "Временное изъятие из нашего склада платья для ваших нужд", Service.ServiceType.RENT)
                {ID = 5},
            new("Аренда реквизита", "Временное изъятие из нашего склада реквизита для ваших нужд", Service.ServiceType.RENT) {ID = 6},
            new("Аренда помещения", "Временное изъятие из нашего склада помещения для ваших нужд",Service.ServiceType.HALLRENT,
                3000) {ID = 7},
            new("Печать фотографий", "Распечатаем ваши фотографии", Service.ServiceType.SIMPLE, 100) {ID = 8},
            new("Детская фотосессия", "Фотографирование детей от одного из фотографов", Service.ServiceType.PHOTOVIDEO,
                    3000)
                {ID = 9},
            new("Аренда детской одежды", "Временное изъятие из нашего склада платья для ваших нужд", Service.ServiceType.RENT) {ID = 10},
            new("Свадебная фотосессия", "Фотографирование на свадьбе от одного из фотографов",
                    Service.ServiceType.PHOTOVIDEO, 4500)
                {ID = 11},
            new("Свадебная видеосъемка", "Видеосъемка на свадьбе от одного из операторов",
                    Service.ServiceType.PHOTOVIDEO, 10000)
                {ID = 12},
            new("Макияж", "Создание образов с помощью средств макияжа",Service.ServiceType.STYLE, 5000)
                {ID = 13}
        };

        builder.HasData(services);
    }

    public static void HallDataConfigure(EntityTypeBuilder<Hall> builder)
    {
        var halls = new List<Hall>
        {
            new("Белая комната", "Зал в белых тонах", 1500) {ID = 1},
            new("Аквариум", "Зал стилизованный под морскую тематику", 2000) {ID = 2},
            new("Ретро", "Зал стилизованный под 80-ые", 1200) {ID = 3},
            new("Зеленая комната", "Зал с хромакеем", 1000) {ID = 4}
        };

        builder.HasData(halls);
    }

    public static void RentedItemDataConfigure(EntityTypeBuilder<RentedItem> builder)
    {
        var items = new List<RentedItem>
        {
            new("Платье", "55 размер", 1, 1000, true, true) {ID = 1},
            new("Костюм", "45 размер", 1, 1300, true, false) {ID = 2},
            new("Пистолет ММГ", "Копия известного пистолета", 8, 800, false, false) {ID = 3},
            new("Голубь", "Белый, серый, черный", 16, 10000, false, false) {ID = 4},
            new("Пластмассовые фрукты", "В ассортименте", 100, 200, false, false) {ID = 5}
        };

        builder.HasData(items);
    }

    #endregion
}