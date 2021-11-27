using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhotostudioDLL.Entity
{
    public class Role
    {
        public uint ID { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Rights { get; set; }

        [Required] public string Responsibilities { get; set; }

        public static void Add(Role role)
        {
            using (var db = new ApplicationContext())
            {
                db.Role.Add(role);
                db.SaveChanges();
            }
        }

        public static void AddAsync(Role role)
        {
            using (var db = new ApplicationContext())
            {
                db.Role.AddAsync(role);
                db.SaveChanges();
            }
        }

        public static void AddRange(params Role[] roles)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var role in roles) db.Role.Add(role);
                db.SaveChanges();
            }
        }

        public static void AddRangeAsync(params Role[] roles)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var role in roles) db.Role.AddAsync(role);
                db.SaveChanges();
            }
        }

        public static List<Role> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.Role.ToList();
            }
        }
    }
}