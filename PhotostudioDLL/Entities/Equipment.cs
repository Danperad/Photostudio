namespace PhotostudioDLL.Entities;

public class Equipment
{
    #region Methods

    public static void Add(Equipment equipment)
    {
        ContextDb.Add(equipment);
    }

    public static List<Equipment> Get()
    {
        return ContextDb.GetEquipments();
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public virtual List<Inventory> Inventories { get; set; }

    #endregion

    #region Constructors

    public Equipment()
    {
        Inventories = new List<Inventory>();
    }

    public Equipment(string title, string description) : this()
    {
        Title = title;
        Description = description;
    }

    #endregion
}