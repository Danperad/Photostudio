namespace PhotostudioDLL.Entities;

public class Service : Costable
{
    #region Methods

    public static List<Service> Get()
    {
        return ContextDb.GetServices();
    }

    public static void Save()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual List<Inventory> Inventories { get; set; }
    public virtual List<ExecuteableService> ExecuteableServices { get; set; }

    #endregion

    #region Constructors

    public Service()
    {
        Inventories = new List<Inventory>();
        ExecuteableServices = new List<ExecuteableService>();
    }

    public Service(string title, string description, decimal cost) : base(title, description, cost)
    {
        Inventories = new List<Inventory>();
        ExecuteableServices = new List<ExecuteableService>();
    }

    #endregion
}