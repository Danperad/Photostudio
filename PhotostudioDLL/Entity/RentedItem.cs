using System.ComponentModel.DataAnnotations.Schema;
using PhotostudioDLL.Entity.Interface;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class RentedItem : ICostable
{
    private static ApplicationContext db = Context.db;

    public int ID { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [Required] public uint Number { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }

    public string GetTitle()
    {
        return Title;
    }

    public static void Add(RentedItem rentedItem)
    {
        db.RentedItem.Add(rentedItem);
        db.SaveChanges();
    }

    public static void AddAsync(RentedItem rentedItem)
    {
        db.RentedItem.AddAsync(rentedItem);
        db.SaveChanges();
    }

    public static void AddRange(params RentedItem[] rentedItems)
    {
        foreach (var rentedItem in rentedItems) db.RentedItem.Add(rentedItem);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params RentedItem[] rentedItems)
    {
        foreach (var rentedItem in rentedItems) db.RentedItem.AddAsync(rentedItem);
        db.SaveChanges();
    }

    public static List<RentedItem> Get()
    {
        return db.RentedItem.ToList();
    }

    private static bool CheckHall(RentedItem hall)
    {
        if (hall.UnitPrice < 0) throw new MoneyException("Цена не может быть меньше нуля", hall);
        return true;
    }
}