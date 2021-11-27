using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PhotostudioDLL.Entity.Interface;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity
{
    public class RentedItem : ICostable
    {
        public uint ID { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Description { get; set; }

        [Required] public uint Number { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public string GetTitle()
        {
            return Title;
        }

        public static void Add(RentedItem rentedItem)
        {
            using (var db = new ApplicationContext())
            {
                db.RentedItem.Add(rentedItem);
                db.SaveChanges();
            }
        }

        public static void AddAsync(RentedItem rentedItem)
        {
            using (var db = new ApplicationContext())
            {
                db.RentedItem.AddAsync(rentedItem);
                db.SaveChanges();
            }
        }

        public static void AddRange(params RentedItem[] rentedItems)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var rentedItem in rentedItems) db.RentedItem.Add(rentedItem);
                db.SaveChanges();
            }
        }

        public static void AddRangeAsync(params RentedItem[] rentedItems)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var rentedItem in rentedItems) db.RentedItem.AddAsync(rentedItem);
                db.SaveChanges();
            }
        }

        public static List<RentedItem> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.RentedItem.ToList();
            }
        }

        private static bool CheckHall(RentedItem hall)
        {
            if (hall.UnitPrice < 0) throw new MoneyException("Цена не может быть меньше нуля", hall);
            return true;
        }
    }
}