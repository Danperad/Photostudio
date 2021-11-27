using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PhotostudioDLL.Entity.Interface;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity
{
    public class Service : ICostable
    {
        public uint ID { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Description { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string GetTitle() => Title;

        public static void Add(Service service)
        {
            using (var db = new ApplicationContext())
            {
                db.Service.Add(service);
                db.SaveChanges();
            }
        }

        public static void AddAsync(Service service)
        {
            using (var db = new ApplicationContext())
            {
                db.Service.AddAsync(service);
                db.SaveChanges();
            }
        }

        public static void AddRange(params Service[] services)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var service in services) db.Service.Add(service);
                db.SaveChanges();
            }
        }

        public static void AddRangeAsync(params Service[] services)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var service in services) db.Service.AddAsync(service);
                db.SaveChanges();
            }
        }

        public static List<Service> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.Service.ToList();
            }
        }

        private static bool CheckHall(Service service)
        {
            if (service.Price < 0) throw new MoneyException("Цена не может быть меньше нуля", service);
            return true;
        }
    }
}