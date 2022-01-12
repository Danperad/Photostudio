namespace PhotostudioDLL.Entities;

public class Role
{
    #region Methods

    public static void Add(Role role)
    {
        ContextDb.Add(role);
    }

    public static List<Role> Get()
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