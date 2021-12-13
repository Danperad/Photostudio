namespace PhotostudioDLL.Entity;

public class Equipment
{
    private static ApplicationContext db = Context.db;

    public int ID { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }

    public static void Add(Equipment equipment)
    {
        db.Equipment.Add(equipment);
        db.SaveChanges();
    }

    public static void AddAsync(Equipment equipment)
    {
        db.Equipment.AddAsync(equipment);
        db.SaveChanges();
    }

    public static void AddRange(params Equipment[] equipments)
    {
        foreach (var equipment in equipments) db.Equipment.Add(equipment);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Equipment[] equipments)
    {
        foreach (var equipment in equipments) db.Equipment.AddAsync(equipment);
        db.SaveChanges();
    }

    public static List<Equipment> Get()
    {
        return db.Equipment.ToList();
    }
}