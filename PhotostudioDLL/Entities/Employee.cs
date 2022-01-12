using Castle.Core.Internal;

namespace PhotostudioDLL.Entities;

public class Employee : People
{
    #region Methods

    public static bool Add(Employee employee)
    {
        if (!Check(employee)) return false;
        ContextDb.Add(employee);
        return true;
    }

    private static bool Check(Employee employee)
    {
        return People.Check(employee) && !employee.PassData.IsNullOrEmpty() &&
               employee.EmploymentDate > DateOnly.FromDateTime(DateTime.Today);
    }

    public static List<Employee> Get()
    {
        return ContextDb.GetEmployees();
    }

    public static List<Employee> GetByRoleId(int id)
    {
        return ContextDb.GetEmployees().Where(e => e.Role.ID == id).ToList();
    }

    public static IEnumerable<Employee> GetPhotoWithTime(DateTime start, DateTime end)
    {
        return ContextDb.GetEmployees().Where(e => e.RoleID == 2 && !e.Services
            .Any(s => s.PhotoStartDateTime <= start ||
                       s.PhotoEndDateTime >= end ||
                       s.PhotoStartDateTime >= start && s.PhotoEndDateTime <= end));
    }

    public static IEnumerable<Employee> GetVideoWithTime(DateTime start, DateTime end)
    {
        return ContextDb.GetEmployees().Where(e => e.RoleID == 4 && !e.Services
            .Any(s => s.PhotoStartDateTime <= start ||
                       s.PhotoEndDateTime >= end ||
                       s.PhotoStartDateTime >= start && s.PhotoEndDateTime <= end));
    }

    public static Employee? GetAuth(string login, string pass)
    {
        return ContextDb.GetAuth(login, pass);
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual EmployeeProfile Profile { get; set; }
    public string PassData { get; set; }
    public DateOnly EmploymentDate { get; set; }
    public int RoleID { get; set; }
    public virtual Role Role { get; set; }

    public virtual List<Contract> Contracts { get; set; }
    public virtual List<ExecuteableService> Services { get; set; }

    #endregion

    #region Constructors

    public Employee()
    {
        Contracts = new List<Contract>();
        Services = new List<ExecuteableService>();
    }

    public Employee(int roleID, string lastName, string firstName, string phoneNumber,
        string passData, DateOnly employmentDate) : base(lastName, firstName, phoneNumber)
    {
        (RoleID, PassData, EmploymentDate) =
            (roleID, passData, employmentDate);
        Contracts = new List<Contract>();
        Services = new List<ExecuteableService>();
    }

    #endregion
}