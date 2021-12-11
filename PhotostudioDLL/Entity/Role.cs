global using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity;

public class Role
{
    private static ApplicationContext db = Context.db;

    public int ID { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Rights { get; set; }

    [Required] public string Responsibilities { get; set; }
    public ICollection<Employee> Employees { get; set; }

    public Role()
    {
        Employees = new List<Employee>();
    }

    public static void Add(Role role)
    {
        db.Role.Add(role);
        db.SaveChanges();
    }

    public static void AddAsync(Role role)
    {
        db.Role.AddAsync(role);
        db.SaveChanges();
    }

    public static void AddRange(params Role[] roles)
    {
        foreach (var role in roles) db.Role.Add(role);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Role[] roles)
    {
        foreach (var role in roles) db.Role.AddAsync(role);
        db.SaveChanges();
    }

    public static List<Role> Get()
    {
        return db.Role.ToList();
    }

    public static Role GetByID(int id)
    {
        return db.Role.Where(d => d.ID == id).FirstOrDefault();
    }
}