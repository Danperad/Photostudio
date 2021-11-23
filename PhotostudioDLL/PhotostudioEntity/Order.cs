using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PhotostudioDLL;
using PhotostudioDLL.Exceptions;

namespace PhotostudioDLL.Entity
{
    public class Order
    {
        public int ID { get; set; }
        [Required]
        public Contract Contract { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Status { get; set; }
        
        public static void AddOrder(Order order, string path)
        {
            using (ApplicationContext db = new ApplicationContext(ApplicationContext.getConnet(path)))
            {
                if(order.Contract.StartDate < order.Contract.EndDate && order.Contract.StartDate > order.DateTime)
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
                else
                {
                    if (order.Contract.StartDate < order.Contract.EndDate)
                    {
                        throw new ContractDateException("Contract Error", order);
                    }
                    throw new OrderDateException("OrderDate Error", order);
                }
            }
        }

        public static List<Order> GetOrders(string path)
        {
            using (ApplicationContext db = new ApplicationContext(ApplicationContext.getConnet(path)))
            {
                var services = db.Orders.ToList();
                return services;
            }
        }
    }
}