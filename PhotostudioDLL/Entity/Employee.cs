﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotostudioDLL.Entity;

public class Employee
{
    public int ID { get; set; }

    [Required] public virtual EmployeeProfile Profile { get; set; }

    [Required] public int RoleID { get; set; }
    [Required] public virtual Role Role { get; set; }

    [MaxLength(50)] public string? MiddleName { get; set; }
    [MaxLength(50)] public string? EMail { get; set; }
    [Required] [MaxLength(50)] public string LastName { get; set; }
    [Required] [MaxLength(50)] public string FirstName { get; set; }
    [Required] [MaxLength(13)] public string PhoneNumber { get; set; }
    [Required] [MaxLength(10)] public string PassData { get; set; }
    [Column(TypeName = "date")] [Required] public DateOnly EmploymentDate { get; set; }

    public Employee()
    {
    }

    public Employee(int RoleID,string LastName, string FirstName, string PhoneNumber,
        string PassData, DateOnly EmploymentDate)
    {
        ( this.RoleID ,this.LastName, this.FirstName, this.PhoneNumber, this.PassData, this.EmploymentDate) =
            (RoleID ,LastName, FirstName, PhoneNumber, PassData, EmploymentDate);
    }
}