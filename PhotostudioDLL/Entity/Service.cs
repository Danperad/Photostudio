using System.ComponentModel.DataAnnotations.Schema;
using PhotostudioDLL.Entity.Interface;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class Service : ICostable
{
    private static ApplicationContext db = Context.db;

    public int ID { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public string GetTitle()
    {
        return Title;
    }

    public static void Add(Service service)
    {
        db.Service.Add(service);
        db.SaveChanges();
    }

    public static void AddAsync(Service service)
    {
        db.Service.AddAsync(service);
        db.SaveChanges();
    }

    public static void AddRange(params Service[] services)
    {
        foreach (var service in services) db.Service.Add(service);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Service[] services)
    {
        foreach (var service in services) db.Service.AddAsync(service);
        db.SaveChanges();
    }

    public static List<Service> Get()
    {
        return db.Service.ToList();
    }

    private static bool CheckHall(Service service)
    {
        if (service.Price < 0) throw new MoneyException("Цена не может быть меньше нуля", service);
        return true;
    }
}