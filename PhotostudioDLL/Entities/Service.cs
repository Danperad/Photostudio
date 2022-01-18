using PhotostudioDLL.Entities.Interfaces;
using PhotostudioDLL.Entities.Services;

namespace PhotostudioDLL.Entities;

public class Service : ICostable
{
    public enum ServiceType
    {
        PHOTOVIDEO,
        STYLE,
        RENT,
        HALLRENT,
        SIMPLE
    }

    #region Methods

    /// <summary>
    ///     Получение списка услуг
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Service> Get()
    {
        return ContextDb.GetServices();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public string Title { get; set; }
    public decimal? Cost { get; set; }
    public string Description { get; set; }
    public ServiceType Type { get; set; }
    public virtual List<Inventory> Inventories { get; set; }
    public virtual List<OrderService> ExecuteableServices { get; set; }

    #endregion

    #region Constructors

    public Service()
    {
        Inventories = new List<Inventory>();
        ExecuteableServices = new List<OrderService>();
    }

    public Service(string title, string description, ServiceType type) : this()
    {
        Title = title;
        Description = description;
        Type = type;
    }

    public Service(string title, string description, ServiceType type, decimal cost) : this(title, description, type)
    {
        Cost = cost;
    }

    #endregion
}