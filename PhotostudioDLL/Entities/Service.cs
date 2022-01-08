using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotostudioDLL.Entities.Interfaces;

namespace PhotostudioDLL.Entities;

public class Service : ICostable
{
    public string GetTitle()
    {
        return Title;
    }

    public decimal GetCost()
    {
        return Price;
    }


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

    #region Properties

    public int ID { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public virtual List<Inventory> Inventories { get; set; }
    public virtual List<ServiceProvided> ServiceProvideds { get; set; }

    #endregion

    #region Constructors

    public Service()
    {
        Inventories = new List<Inventory>();
        ServiceProvideds = new List<ServiceProvided>();
    }

    public Service(string Title, string Description, decimal Price)
    {
        this.Title = Title;
        this.Description = Description;
        this.Price = Price;
        Inventories = new List<Inventory>();
        ServiceProvideds = new List<ServiceProvided>();
    }

    #endregion
}