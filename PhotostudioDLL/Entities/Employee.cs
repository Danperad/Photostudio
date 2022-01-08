using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Castle.Core.Internal;

namespace PhotostudioDLL.Entities;

public class Employee : People
{
    public static void Add(Employee employee)
    {
        Check(employee);
        ContextDB.Add(employee);
    }

    private static void Check(Employee employee)
    {
        People.Check(employee);
        if (employee.PassData.IsNullOrEmpty())
            throw new Exception("PassDataError");
        if (employee.EmploymentDate > DateOnly.FromDateTime(DateTime.Today))
            throw new Exception("EmploymentDateError");
        if (employee.Role == null)
            throw new Exception("RoleError");
    }

    public static List<Employee> Get()
    {
        return ContextDB.GetEmployees();
    }

    public static Employee? GetAuth(string login, string pass)
    {
        return ContextDB.GetAuth(login, pass);
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    #region Properties

    public int ID { get; set; }
    [Required] public virtual EmployeeProfile Profile { get; set; }
    [Required] [MaxLength(10)] public string PassData { get; set; }
    [Column(TypeName = "date")] [Required] public DateOnly EmploymentDate { get; set; }
    [Required] public int RoleID { get; set; }
    [Required] public virtual Role Role { get; set; }

    public virtual List<Contract> Contracts { get; set; }
    public virtual List<ServiceProvided> Services { get; set; }

    #endregion

    #region Constructors

    public Employee()
    {
        Contracts = new List<Contract>();
        Services = new List<ServiceProvided>();
    }

    public Employee(int RoleID, string LastName, string FirstName, string PhoneNumber,
        string PassData, DateOnly EmploymentDate) : base(LastName, FirstName, PhoneNumber)
    {
        (this.RoleID, this.PassData, this.EmploymentDate) =
            (RoleID, PassData, EmploymentDate);
        Contracts = new List<Contract>();
        Services = new List<ServiceProvided>();
    }

    #endregion
}