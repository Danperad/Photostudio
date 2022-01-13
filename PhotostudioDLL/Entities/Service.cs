namespace PhotostudioDLL.Entities;

public class Service : Costable
{
    #region Methods

    /// <summary>
    ///     Получение списка услуг
    /// </summary>
    /// <returns></returns>
    public static List<Service> Get()
    {
        return ContextDb.GetServices();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual List<Inventory> Inventories { get; set; }
    public virtual List<OrderService> ExecuteableServices { get; set; }

    #endregion

    #region Constructors

    public Service()
    {
        Inventories = new List<Inventory>();
        ExecuteableServices = new List<OrderService>();
    }

    public Service(string title, string description) : base(title, description)
    {
        Inventories = new List<Inventory>();
        ExecuteableServices = new List<OrderService>();
    }

    public Service(string title, string description, decimal cost) : base(title, description, cost)
    {
        Inventories = new List<Inventory>();
        ExecuteableServices = new List<OrderService>();
    }

    #endregion
}