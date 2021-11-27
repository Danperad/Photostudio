using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhotostudioDLL.Entity
{
    public class Employee
    {
        public uint ID { get; set; }
        [Required] public Role Role { get; set; }
        [Required] [MaxLength(50)] public string LastName { get; set; }
        [Required] [MaxLength(50)] public string FirstName { get; set; }
        [MaxLength(50)] public string MiddleName { get; set; }
        [Required] [MaxLength(13)] public string PhoneNumber { get; set; }
        [MaxLength(50)] public string EMail { get; set; }
        [Required] [MaxLength(10)] public string PassData { get; set; }
        [Required] public DateTime EmploymentDate { get; set; }

        public static void Add(Employee employee)
        {
            using (var db = new ApplicationContext())
            {
                db.Employee.Add(employee);
                db.SaveChanges();
            }
        }

        public static void AddAsync(Employee employee)
        {
            using (var db = new ApplicationContext())
            {
                db.Employee.AddAsync(employee);
                db.SaveChanges();
            }
        }

        public static void AddRange(params Employee[] employees)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var employee in employees) db.Employee.Add(employee);
                db.SaveChanges();
            }
        }

        public static void AddRangeAsync(params Employee[] employees)
        {
            using (var db = new ApplicationContext())
            {
                foreach (var employee in employees) db.Employee.AddAsync(employee);
                db.SaveChanges();
            }
        }

        public static List<Employee> Get()
        {
            using (var db = new ApplicationContext())
            {
                return db.Employee.ToList();
            }
        }
    }
}