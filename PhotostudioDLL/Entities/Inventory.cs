namespace PhotostudioDLL.Entities;

public class Inventory
{
    #region Methods

    public static void Add(Inventory inventory)
    {
        ContextDb.Add(inventory);
    }

    public static List<Inventory> Get()
    {
        return ContextDb.GetInventories();
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Constructors

    public Inventory()
    {
        Equipment = new List<Equipment>();
    }

    public Inventory(Service service, string appointment) : this()
    {
        Service = service;
        Appointment = appointment;
    }

    #endregion

    #region Properties

    public int ID { get; set; }

    public virtual List<Equipment> Equipment { get; set; }
    public virtual Service Service { get; set; }
    public int ServiceID { get; set; }
    public string Appointment { get; set; }

    #endregion
}