namespace PhotostudioDLL.Entities;

public class Role
{
    #region Methods

    /// <summary>
    ///     Добавление новой роли
    /// </summary>
    /// <param name="role"></param>
    public static void Add(Role role)
    {
        ContextDb.Add(role);
    }

    /// <summary>
    ///     Получение списка ролей
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Role> Get()
    {
        return ContextDb.GetRoles();
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Constructors

    public Role()
    {
        Employees = new List<Employee>();
    }

    public Role(string title, string rights, string responsibilities) : this()
    {
        Title = title;
        Rights = rights;
        Responsibilities = responsibilities;
    }

    #endregion

    #region Properties

    public int ID { get; set; }

    public string Title { get; set; }
    public string Rights { get; set; }
    public string Responsibilities { get; set; }
    public virtual List<Employee> Employees { get; set; }

    #endregion
}