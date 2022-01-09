namespace PhotostudioDLL.Entities;

public class Service : Costable
{
    #region Methods
    public static void Add(Service service)
    {
        ContextDB.Add(service);
    }

    public static List<Service> Get()
    {
        return ContextDB.GetServices();
    }

    public static void Save()
    {
        ContextDB.Save();
    }
    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual List<Inventory> Inventories { get; set; }
    public virtual List<ServiceProvided> ServiceProvideds { get; set; }

    #endregion

    #region Constructors

    public Service()
    {
        Inventories = new List<Inventory>();
        ServiceProvideds = new List<ServiceProvided>();
    }

    public Service(string Title, string Description, decimal Cost) : base(Title, Description, Cost)
    {
        Inventories = new List<Inventory>();
        ServiceProvideds = new List<ServiceProvided>();
    }

    #endregion
}