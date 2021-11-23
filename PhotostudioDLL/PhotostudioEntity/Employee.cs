
using System;
using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity
{
    public class Employee
    {
        public int ID { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string EMail { get; set; }
        [Required]
        [MaxLength(10)]
        public string PassData { get; set; }
        [Required]
        public DateTime EmploymentDate { get; set; }
    }
}