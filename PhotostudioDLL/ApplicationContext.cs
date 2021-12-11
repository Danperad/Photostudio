using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using PhotostudioDLL.Entity;

namespace PhotostudioDLL;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        // Database.EnsureCreated();
        Database.Migrate();
        Context.AddDB(this);
    }

    // TODO: Удаление и обновдение данных в таблицах

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
        return optionsBuilder.UseNpgsql(connectionString).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasIndex(e => e.PhoneNumber).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(e => e.PhoneNumber).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(e => e.PassData).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(e => e.EMail).IsUnique();
        modelBuilder.Entity<EmployeeProfile>().HasIndex(e => e.Login).IsUnique();
        modelBuilder.Entity<Employee>().HasOne(e => e.Profile).WithOne(p => p.Employee)
            .HasForeignKey<EmployeeProfile>(p => p.ID);
    }

    public static void LoadDB()
    {
        new ApplicationContext(GetDB());
    }
}