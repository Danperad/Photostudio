using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhotostudioDLL.Entity
{
    public class Inventory
    {
        public uint ID { get; set; }

        public List<Equipment> Equipment { get; set; } = new List<Equipment>();
        [Required] public Service Service { get; set; }
        [Required] public string Appointment { get; set; }

        public static void Add(Inventory inventory)
        {
            using (var db = new ApplicationContext())
            {
                db.Inventory.Add(inventory);
                db.SaveChanges();
            }
        }

        public static void AddAsync(Inventory inventory)
        {
            using (var db = new ApplicationContext())
            {
                db.Inventory.AddAsync(inventory);
                db.SaveChanges();
            }
        }

        public static void AddRange(params Inventory[] inventories)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var inventory in inventories) db.Inventory.Add(inventory);
                db.SaveChanges();
            }
        }

        public static void AddRangeAsync(params Inventory[] inventories)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var inventory in inventories) db.Inventory.AddAsync(inventory);
                db.SaveChanges();
            }
        }

        public static List<Inventory> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.Inventory.ToList();
            }
        }
    }
}