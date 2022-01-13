namespace PhotostudioDLL.Entities;

public class Employee : People
{
    // Время между двумя новыми заявками
    private const int HoursWait = 3;

    #region Methods

    /// <summary>
    ///     Получение всех сотрудников
    /// </summary>
    /// <returns></returns>
    public static List<Employee> Get()
    {
        return ContextDb.GetEmployees();
    }

    /// <summary>
    ///     Получение всех сотрудников с определенной ролью
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static List<Employee> GetByRoleId(int id)
    {
        return ContextDb.GetEmployees().Where(e => e.Role.ID == id).ToList();
    }

    /// <summary>
    ///     Получение всех доступных фотографов
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static IEnumerable<Employee> GetPhotoWithTime(DateTime start, DateTime end)
    {
        return GetByRoleId(2)
            .Where(e => !e.Services.Any(s => ContextDb.FindDateTime(start, end.AddHours(HoursWait),
                s.StartTime!.Value, s.EndTime!.Value.AddHours(HoursWait))));
    }

    /// <summary>
    ///     Получение всех доступных операторов
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static IEnumerable<Employee> GetVideoWithTime(DateTime start, DateTime end)
    {
        return GetByRoleId(4).Where(e => !e.Services.Any(s => ContextDb.FindDateTime(start,
            end.AddHours(HoursWait),
            s.StartTime!.Value, s.EndTime!.Value.AddHours(HoursWait))));
    }

    /// <summary>
    ///     Получение всех доступных стилистов
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static IEnumerable<Employee> GetStyleWithTime(DateTime start, DateTime end)
    {
        return GetByRoleId(6).Where(e => !e.Services.Any(s => ContextDb.FindDateTime(start,
            end.AddHours(1),
            s.StartTime!.Value, s.EndTime!.Value.AddHours(1))));
    }

    /// <summary>
    ///     Получение пользователя для авторизации
    /// </summary>
    /// <param name="login"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public static Employee? GetAuth(string login, string pass)
    {
        return ContextDb.GetAuth(login, pass);
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
    public virtual List<OrderService> Services { get; set; }

    #endregion

    #region Constructors

    public Employee()
    {
        Contracts = new List<Contract>();
        Services = new List<OrderService>();
    }

    public Employee(int roleID, string lastName, string firstName, string phoneNumber,
        string passData, DateOnly employmentDate) : base(lastName, firstName, phoneNumber)
    {
        (RoleID, PassData, EmploymentDate) =
            (roleID, passData, employmentDate);
        Contracts = new List<Contract>();
        Services = new List<OrderService>();
    }

    #endregion
}