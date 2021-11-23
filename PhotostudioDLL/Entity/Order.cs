using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhotostudioDLL.Entity
{
    public class Order
    {
        public int ID { get; set; }

        [Required] public Contract Contract { get; set; }

        [Required] public Client Client { get; set; }

        [Required] public DateTime DateTime { get; set; }

        [Required] public string Status { get; set; }

        public static void Add(Order order)
        {
            using (var db = new ApplicationContext())
            {
                db.Order.Add(order);
                db.SaveChanges();
            }
        }

        public static void AddRange(params Order[] orders)
        {
            foreach (var order in orders) Add(order);
        }

        public static List<Order> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.Order.ToList();
            }
        }
    }
}