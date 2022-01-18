using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotostudioDLL.Entities;

namespace PhotostudioDLL;

public sealed class ApplicationContext : DbContext
{
    internal ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.Migrate();
        ContextDb.AddDb(this);
    }

    /// <summary>
    ///     Получение данных для подключения к БД. Если файл не найден, создается новый, не заполненный
    /// </summary>
    /// <returns></returns>
    internal static DbContextOptions<ApplicationContext> GetDb()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
        if (!File.Exists("appsettings.json"))
        {
            using var sw = File.Open("appsettings.json", FileMode.Create, FileAccess.Write);
            sw.Write(JsonSerializer.SerializeToUtf8Bytes(new AppConfig()));
        }

        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
        return optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connectionString).Options;
    }

    /// <summary>
    ///     Применение новых ограничений целостности или заполнение данными
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<People>();
        modelBuilder.Entity<Employee>(EntityConfigure.EmployeeConfigure);
        modelBuilder.Entity<Client>(EntityConfigure.ClientConfigure);
        modelBuilder.Entity<Order>(EntityConfigure.OrderConfigure);
        modelBuilder.Entity<EmployeeProfile>(EntityConfigure.EmployeeProfileConfigure);
        modelBuilder.Entity<RentedItem>(EntityConfigure.RentedItemConfigure);
        modelBuilder.Entity<Hall>(EntityConfigure.HallConfigure);
        modelBuilder.Entity<Service>(EntityConfigure.ServiceConfigure);

        modelBuilder.Entity<RentedItem>(EntityConfigure.RentedItemDataConfigure);
        modelBuilder.Entity<Role>(EntityConfigure.RoleDataConfigure);
        modelBuilder.Entity<Employee>(EntityConfigure.EmployeeDataConfigure);
        modelBuilder.Entity<EmployeeProfile>(EntityConfigure.EmployeeProfileDataConfigure);
        modelBuilder.Entity<Client>(EntityConfigure.ClientDataConfigure);
        modelBuilder.Entity<Service>(EntityConfigure.ServiceDataConfigure);
        modelBuilder.Entity<Hall>(EntityConfigure.HallDataConfigure);
    }

    /// <summary>
    ///     Загрузка базы данных
    /// </summary>
    public static bool LoadDb()
    {
        try
        {
            new ApplicationContext(GetDb());
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///     Класс для генерации файла конфигурации
    /// </summary>
    public class AppConfig
    {
        public AppConfig()
        {
            ConnectionStrings = new Conn();
        }

        public Conn ConnectionStrings { get; }

        public class Conn
        {
            public Conn()
            {
                DefaultConnection = "Host=;Port=;Database=;Username=;Password=";
            }

            public string DefaultConnection { get; }
        }
    }

    #region Tables

    public DbSet<Client> Client { get; set; } = null!;
    public DbSet<Contract> Contract { get; set; } = null!;
    public DbSet<Employee> Employee { get; set; } = null!;
    public DbSet<Role> Role { get; set; } = null!;
    public DbSet<Equipment> Equipment { get; set; } = null!;
    public DbSet<Inventory> Inventory { get; set; } = null!;
    public DbSet<Service> Service { get; set; } = null!;
    public DbSet<Hall> Hall { get; set; } = null!;
    public DbSet<RentedItem> RentedItem { get; set; } = null!;
    public DbSet<OrderService> ExecuteableService { get; set; } = null!;
    public DbSet<Order> Order { get; set; } = null!;

    #endregion
}