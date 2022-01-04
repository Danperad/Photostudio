using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using PhotostudioDLL.Entity;

namespace PhotostudioDLL;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.Migrate();
        ContextDB.AddDB(this);
    }

    // TODO: Обновдение данных в таблицах

    public DbSet<Client> Client { get; set; }
    public DbSet<Contract> Contract { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<Service> Service { get; set; }
    public DbSet<Hall> Hall { get; set; }
    public DbSet<RentedItem> RentedItem { get; set; }
    public DbSet<ServiceProvided> ServiceProvided { get; set; }
    public DbSet<Order> Order { get; set; }

    public static DbContextOptions<ApplicationContext> GetDB()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        return optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connectionString).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(RoleConfigure);
        modelBuilder.Entity<Employee>(EmployeeConfigure);
        modelBuilder.Entity<Client>().HasIndex(e => e.PhoneNumber).IsUnique();
        modelBuilder.Entity<Client>().Property(c => c.IsActive).HasDefaultValue(true);
        modelBuilder.Entity<EmployeeProfile>().HasIndex(e => e.Login).IsUnique();
        modelBuilder.Entity<EmployeeProfile>().HasData(new EmployeeProfile("admin",
            "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918") {ID = 1});
    }

    public void EmployeeConfigure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasIndex(e => e.PhoneNumber).IsUnique();
        builder.HasIndex(e => e.PassData).IsUnique();
        builder.HasIndex(e => e.EMail).IsUnique();
        builder.HasOne(e => e.Profile).WithOne(p => p.Employee)
            .HasForeignKey<EmployeeProfile>(p => p.ID);
        var employee = new Employee(1, "Берёзов", "Вячеслав",
            "+78005553535", "6024978234", DateOnly.FromDateTime(DateTime.Today)) {ID = 1};
        builder.HasData(employee);
    }

    public void RoleConfigure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role("Администратор", "Доступ ко всем данным",
                "Добавлять новые услуги, новые должности и новых сотрудники (по мере необходимости)") {ID = 1},
            new Role("Фотограф", "Доступ к предоставляемым им услугам, и инвентарю услуги",
                "Фотографировать согласно услуге") {ID = 2},
            new Role("Ретушер", "Доступ к предоставляемым им услугам, и данным фотографиям",
                "Обрабатывать фотогафии согласно услуге") {ID = 3},
            new Role("Оператор", "Доступ к предоставляемым им услугам, и инвентарю услуги",
                "Снимать видеоматериалы согласно услуге") {ID = 4},
            new Role("Монтажер", "Доступ к предоставляемым им услугам, и данным видеоматериалами",
                "Обрабатывать видеоматериалы согласно услуге") {ID = 5},
            new Role("Стилист", "Доступ к предоставляемым им услугам, и инвентарю услуги",
                "Видоизменять клиента, согласно заявке") {ID = 6});
    }

    public static void LoadDB()
    {
        new ApplicationContext(GetDB());
    }
}