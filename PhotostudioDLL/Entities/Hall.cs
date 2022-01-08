using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotostudioDLL.Entities.Interfaces;

namespace PhotostudioDLL.Entities;

public class Hall : ICostable
{
    public string GetTitle()
    {
        return Title;
    }

    public decimal GetCost()
    {
        return PricePerHour;
    }

    public static void Add(Hall hall)
    {
        ContextDB.Add(hall);
    }

    public static List<Hall> Get()
    {
        return ContextDB.GetHalls();
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    #region Properties

    public int ID { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal PricePerHour { get; set; }

    public virtual List<ServiceProvided> Services { get; set; }

    #endregion

    #region Constructors

    public Hall()
    {
    }

    public Hall(string Title, string Description, decimal PricePerHour)
    {
        this.Title = Title;
        this.Description = Description;
        this.PricePerHour = PricePerHour;
    }

    #endregion
}