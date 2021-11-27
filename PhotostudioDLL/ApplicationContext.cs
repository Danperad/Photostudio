using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotostudioDLL.Entity;

namespace PhotostudioDLL
{
    public sealed class ApplicationContext : DbContext
    {
        private static string _dbtext;

        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            // TODO: Добавить миграцию (?)
            Database.EnsureCreated();
        }

        #region LoginRegion

        public static bool Login(string login, string pass)
        {
            Logout();
            GetDB();
            _dbtext += $"Username={login};Password={pass}";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            try
            {
                new ApplicationContext(optionsBuilder.UseNpgsql(_dbtext).Options);
                return true;
            }
            catch (Npgsql.PostgresException)
            {
                return false;
            }
        }

        public static void Logout() => _dbtext = "";

        private static void GetDB()
        {
            var builder = new ConfigurationBuilder();
#if DEBUG
            builder.SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
#else
            builder.SetBasePath(Directory.GetCurrentDirectory());
#endif

            builder.AddJsonFile("appsettings.json");
            var configJson = builder.Build();
            var host = configJson.GetConnectionString("Host");
            var port = configJson.GetConnectionString("Port");
            var database = configJson.GetConnectionString("Database");

            _dbtext =
                $"Host={host};Port={port};Database={database};";
        }

        #endregion

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasIndex(e => e.PhoneNumber).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.PhoneNumber).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.PassData).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.EMail).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dbtext);
        }
    }
}