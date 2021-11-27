using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PhotostudioDLL.Entity.Interface;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity
{
    public class Hall : ICostable
    {
        public uint ID { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal PricePerHour { get; set; }

        public string GetTitle()
        {
            return Title;
        }

        public static void Add(Hall hall)
        {
            using (var db = new ApplicationContext())
            {
                db.Hall.Add(hall);
                db.SaveChanges();
            }
        }

        public static void AddAsync(Hall hall)
        {
            using (var db = new ApplicationContext())
            {
                db.Hall.AddAsync(hall);
                db.SaveChanges();
            }
        }

        public static void AddRange(params Hall[] halls)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var hall in halls) db.Hall.Add(hall);
                db.SaveChanges();
            }
        }

        public static void AddRangeAsync(params Hall[] halls)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var hall in halls) db.Hall.AddAsync(hall);
                db.SaveChanges();
            }
        }

        public static List<Hall> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.Hall.ToList();
            }
        }

        private static bool CheckHall(Hall hall)
        {
            if (hall.PricePerHour < 0) throw new MoneyException("Цена не может быть меньше нуля", hall);
            return true;
        }
    }
}