using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entities;

public class Role
{
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

    [Required] public string Title { get; set; }
    [Required] public string Rights { get; set; }
    [Required] public string Responsibilities { get; set; }
    public virtual List<Employee> Employees { get; set; }

    #endregion
}