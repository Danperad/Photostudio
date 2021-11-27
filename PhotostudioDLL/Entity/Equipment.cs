using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhotostudioDLL.Entity
{
    public class Equipment
    {
        public uint ID { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }

        public static void Add(Equipment equipment)
        {
            using (var db = new ApplicationContext())
            {
                db.Equipment.Add(equipment);
                db.SaveChanges();
            }
        }

        public static void AddAsync(Equipment equipment)
        {
            using (var db = new ApplicationContext())
            {
                db.Equipment.AddAsync(equipment);
                db.SaveChanges();
            }
        }

        public static void AddRange(params Equipment[] equipments)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var equipment in equipments) db.Equipment.Add(equipment);
                db.SaveChanges();
            }
        }

        public static void AddRangeAsync(params Equipment[] equipments)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var equipment in equipments) db.Equipment.AddAsync(equipment);
                db.SaveChanges();
            }
        }

        public static List<Equipment> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.Equipment.ToList();
            }
        }
    }
}