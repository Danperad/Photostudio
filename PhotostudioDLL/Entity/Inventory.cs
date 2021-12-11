namespace PhotostudioDLL.Entity;

public class Inventory
{
    private static ApplicationContext db = Context.db;

    public int ID { get; set; }

    public List<Equipment> Equipment { get; set; } = new();
    [Required] public Service Service { get; set; }
    [Required] public string Appointment { get; set; }

    public static void Add(Inventory inventory)
    {
        db.Inventory.Add(inventory);
        db.SaveChanges();
    }

    public static void AddAsync(Inventory inventory)
    {
        db.Inventory.AddAsync(inventory);
        db.SaveChanges();
    }

    public static void AddRange(params Inventory[] inventories)
    {
        foreach (var inventory in inventories) db.Inventory.Add(inventory);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Inventory[] inventories)
    {
        foreach (var inventory in inventories) db.Inventory.AddAsync(inventory);
        db.SaveChanges();
    }

    public static List<Inventory> Get()
    {
        return db.Inventory.ToList();
    }
}