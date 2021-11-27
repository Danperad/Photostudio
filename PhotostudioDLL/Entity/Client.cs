using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhotostudioDLL.Entity
{
    public class Client
    {
        public uint ID { get; set; }
        [Required] [MaxLength(50)] public string LastName { get; set; }
        [Required] [MaxLength(50)] public string FirstName { get; set; }
        [MaxLength(50)] public string MiddleName { get; set; }
        [Required] [MaxLength(15)] public string PhoneNumber { get; set; }
        [MaxLength(50)] public string EMail { get; set; }

        public static void Add(Client client)
        {
            using (var db = new ApplicationContext())
            {
                db.Client.Add(client);
                db.SaveChanges();
            }
        }

        public static void AddAsync(Client client)
        {
            using (var db = new ApplicationContext())
            {
                db.Client.AddAsync(client);
                db.SaveChanges();
            }
        }

        public static void AddRange(params Client[] clients)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var client in clients) db.Client.Add(client);
                db.SaveChanges();
            }
        }

        public static void AddRangeAsync(params Client[] clients)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var client in clients) db.Client.AddAsync(client);
                db.SaveChanges();
            }
        }

        public static List<Client> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.Client.ToList();
            }
        }
    }
}