using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotostudioDLL.Entity;

namespace PhotostudioDLL;

public static class EntityConfigure
{
    public static void EmployeeConfigure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasIndex(e => e.PhoneNumber).IsUnique();
        builder.HasIndex(e => e.PassData).IsUnique();
        builder.HasIndex(e => e.EMail).IsUnique();
        builder.Ignore(e => e.FullName);
        builder.HasOne(e => e.Profile).WithOne(p => p.Employee)
            .HasForeignKey<EmployeeProfile>(p => p.ID);
        var employee0 = new Employee(1, "Берёзов", "Вячеслав",
            "+78005553535", "6024978234", DateOnly.FromDateTime(DateTime.Today)) { ID = 1 };
        var employee1 = new Employee(2, "Власов", "Иван",
                "+76985324710", "6852432107", DateOnly.FromDayNumber(15))
            { MiddleName = "Валентинович", ID = 2};
        builder.HasData(employee0, employee1);
    }

    public static void RoleConfigure(EntityTypeBuilder<Role> builder)
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
                "Видоизменять клиента, согласно заявке") { ID = 6 });
    }
    
    public static void ClientConfigure(EntityTypeBuilder<Client> builder)
    {
        builder.Ignore(c => c.FullName);
        builder.HasIndex(e => e.PhoneNumber).IsUnique();
        builder.Property(c => c.IsActive).HasDefaultValue(true);
    }
}