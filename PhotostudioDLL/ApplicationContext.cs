using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PhotostudioDLL
{
    public class ApplicationContext : DbContext
    {
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
            modelBuilder.Entity<ServiceProvided>();
            // modelBuilder.Entity<Inventory>();
            modelBuilder.Entity<Client>().HasAlternateKey(c => c.PhoneNumber);
            modelBuilder.Entity<Employee>().HasAlternateKey(e => e.PhoneNumber);
            modelBuilder.Entity<Employee>().HasAlternateKey(e => e.PassData);
            modelBuilder.Entity<Employee>().HasAlternateKey(e => e.EMail);
        }
    }
}
