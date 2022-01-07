using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
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
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
        if (!File.Exists("appsettings.json"))
        {
            using (var sw = File.Open("appsettings.json", FileMode.Create, FileAccess.Write))
            {
                sw.Write(JsonSerializer.SerializeToUtf8Bytes(new AppConfig()));
            }
        }

        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        return optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connectionString).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<People>();
        modelBuilder.Entity<Role>(EntityConfigure.RoleConfigure);
        modelBuilder.Entity<Employee>(EntityConfigure.EmployeeConfigure);
        modelBuilder.Entity<Client>(EntityConfigure.ClientConfigure);
        modelBuilder.Entity<Order>().Ignore(o => o.ListServices);
        modelBuilder.Entity<Order>().Ignore(o => o.TextStatus);
        modelBuilder.Entity<EmployeeProfile>().HasIndex(e => e.Login).IsUnique();
        modelBuilder.Entity<EmployeeProfile>().HasData(new EmployeeProfile("admin",
            "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918") { ID = 1 });
        modelBuilder.Entity<EmployeeProfile>().HasData(new EmployeeProfile("photo",
            "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918") { ID = 2 });
    }

    public static void LoadDB()
    {
        new ApplicationContext(GetDB());
    }

    private class AppConfig
    {
        internal class conn
        {
            public string DefaultConnection { get; set; }

            public conn()
            {
                DefaultConnection = "Host=;Port=;Database=;Username=;Password=";
            }
        }

        public conn ConnectionStrings { get; set; }

        public AppConfig()
        {
            ConnectionStrings = new conn();
        }
    }
}