using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity;

public class Role
{
    public int ID { get; set; }

    [Required] public string Title { get; set; }
    [Required] public string Rights { get; set; }
    [Required] public string Responsibilities { get; set; }
    public virtual List<Employee> Employees { get; set; }
    
    public Role()
    {
        Employees = new List<Employee>();
    }

    public Role(string Title, string Rights, string Responsibilities) : base()
    {
        this.Title = Title;
        this.Rights = Rights;
        this.Responsibilities = Responsibilities;
    }

    public static void Add(Role role)
    {
        ContextDB.Add(role);
    }

    public static List<Role> Get() => ContextDB.GetRoles();
}