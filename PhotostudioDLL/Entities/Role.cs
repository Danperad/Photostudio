namespace PhotostudioDLL.Entities;

public class Role
{
    #region Methods
    public static void Add(Role role)
    {
        ContextDB.Add(role);
    }

    public static List<Role> Get()
    {
        return ContextDB.GetRoles();
    }

    public static void Update()
    {
        ContextDB.Save();
    }
    #endregion

    #region Constructors

    public Role()
    {
        Employees = new List<Employee>();
    }

    public Role(string Title, string Rights, string Responsibilities)
    {
        this.Title = Title;
        this.Rights = Rights;
        this.Responsibilities = Responsibilities;
        Employees = new List<Employee>();
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