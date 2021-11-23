using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotostudioDLL.Entity;

namespace PhotostudioDLL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<RentedItem> RentedItems { get; set; }
        public DbSet<ServiceProvided> ServicesProvided { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public static DbContextOptions<ApplicationContext> getConnet(string path)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(path);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            return optionsBuilder.UseNpgsql(connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasIndex(e => e.PhoneNumber).IsUnique(true);
            modelBuilder.Entity<Employee>().HasIndex(e => e.PhoneNumber).IsUnique(true);
            modelBuilder.Entity<Employee>().HasIndex(e => e.PassData).IsUnique(true);
            modelBuilder.Entity<Employee>().HasIndex(e => e.EMail).IsUnique(true);
        }
    }
}