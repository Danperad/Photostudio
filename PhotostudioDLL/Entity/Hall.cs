using System.ComponentModel.DataAnnotations.Schema;
using PhotostudioDLL.Entity.Interface;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class Hall : ICostable
{
    private static ApplicationContext db = Context.db;


    public int ID { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal PricePerHour { get; set; }

    public string GetTitle()
    {
        return Title;
    }

    public static void Add(Hall hall)
    {
        db.Hall.Add(hall);
        db.SaveChanges();
    }

    public static void AddAsync(Hall hall)
    {
        db.Hall.AddAsync(hall);
        db.SaveChanges();
    }

    public static void AddRange(params Hall[] halls)
    {
        foreach (var hall in halls) db.Hall.Add(hall);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Hall[] halls)
    {
        foreach (var hall in halls) db.Hall.AddAsync(hall);
        db.SaveChanges();
    }

    public static List<Hall> Get()
    {
        return db.Hall.ToList();
    }

    private static bool CheckHall(Hall hall)
    {
        if (hall.PricePerHour < 0) throw new MoneyException("Цена не может быть меньше нуля", hall);
        return true;
    }
}