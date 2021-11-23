using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotostudioDLL.Entity;

namespace PhotostudioDLL
{
    public sealed class ApplicationContext : DbContext
    {
        private static string _config;

        public ApplicationContext()
        {
            // TODO: Добавить миграцию (?), указвыать путь к файлу с данными БД
            Database.EnsureCreated();
        }

        // TODO: Добавление объектов в их таблицы, удаление и обновдение данных

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

        public static void Login(string login, string password)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var configJson = builder.Build();
            var host = configJson.GetConnectionString("Host");
            var port = configJson.GetConnectionString("Port");
            var database = configJson.GetConnectionString("Database");

            _config =
                $"Host={host};Port={port};Database={database};Username={login};Password={password}";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasIndex(e => e.PhoneNumber).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.PhoneNumber).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.PassData).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.EMail).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config);
        }
    }
}