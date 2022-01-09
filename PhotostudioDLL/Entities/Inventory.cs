namespace PhotostudioDLL.Entities;

public class Inventory
{
    #region Methods

    public static void Add(Inventory inventory)
    {
        ContextDB.Add(inventory);
    }

    public static List<Inventory> Get()
    {
        return ContextDB.GetInventories();
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    #endregion

    #region Constructors

    public Inventory()
    {
        Equipment = new List<Equipment>();
    }

    public Inventory(Service Service, string Appointment)
    {
        Equipment = new List<Equipment>();
        this.Service = Service;
        this.Appointment = Appointment;
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