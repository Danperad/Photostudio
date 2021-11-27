using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity
{
    public class Order
    {
        public uint ID { get; set; }

        [Required] public Contract Contract { get; set; }

        [Required] public Client Client { get; set; }

        [Required] public DateTime DateTime { get; set; }

        [Required] public string Status { get; set; }

        public static void Add(Order order)
        {
            using (var db = new ApplicationContext())
            {
                if (CheckOrder(order)) db.Order.Add(order);
                db.SaveChanges();
            }
        }

        public static void AddAsync(Order order)
        {
            using (var db = new ApplicationContext())
            {
                if (CheckOrder(order)) db.Order.AddAsync(order);
                db.SaveChanges();
            }
        }

        public static void AddRange(params Order[] orders)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var order in orders)
                    if (CheckOrder(order))
                        db.Order.Add(order);
                db.SaveChanges();
            }
        }

        public static void AddRangeAsync(params Order[] orders)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var order in orders)
                    if (CheckOrder(order))
                        db.Order.AddAsync(order);
                db.SaveChanges();
            }
        }

        public static List<Order> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.Order.ToList();
            }
        }

        private static bool CheckOrder(Order order)
        {
            if (order.Contract.StartDate > order.Contract.EndDate)
                throw new ContractDateException("Не соответствие дат в контракте", order.Contract);
            if (order.Contract.StartDate < order.DateTime)
                throw new OrderDateException("Не соответствие дат в контракте и заявке", order);
            return true;
        }
    }
}