using System.ComponentModel.DataAnnotations.Schema;

namespace PhotostudioDLL.Entity;

public class Employee
{
    private static ApplicationContext db = Context.db;

    public int ID { get; set; }

    [Required] public EmployeeProfile Profile { get; set; }

    [Required] public int RoleID { get; set; }
    [Required] public Role Role { get; set; }

    [MaxLength(50)] public string? MiddleName { get; set; }
    [MaxLength(50)] public string? EMail { get; set; }
    [Required] [MaxLength(50)] public string LastName { get; set; }
    [Required] [MaxLength(50)] public string FirstName { get; set; }
    [Required] [MaxLength(13)] public string PhoneNumber { get; set; }
    [Required] [MaxLength(10)] public string PassData { get; set; }
    [Column(TypeName = "date")] [Required] public DateTime EmploymentDate { get; set; }

    public static void Add(Employee employee)
    {
        db.Employee.Add(employee);
        db.SaveChanges();
    }

    public static void AddAsync(Employee employee)
    {
        db.Employee.AddAsync(employee);
        db.SaveChanges();
    }

    public static void AddRange(params Employee[] employees)
    {
        foreach (var employee in employees) db.Employee.Add(employee);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Employee[] employees)
    {
        foreach (var employee in employees) db.Employee.AddAsync(employee);
        db.SaveChanges();
    }

    public static List<Employee> Get()
    {
        return db.Employee.ToList();
    }

    public static Employee GetEmployee(string login, string pass)
    {
        return db.Employee.Where(d => d.Profile.Login == login && d.Profile.Password == pass).FirstOrDefault();
    }
}