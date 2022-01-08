using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotostudioDLL.Entities.Interfaces;

namespace PhotostudioDLL.Entities;

public class RentedItem : ICostable
{
    public string GetTitle()
    {
        return Title;
    }

    public decimal GetCost()
    {
        return UnitPrice;
    }

    public static void Add(RentedItem rentedItem)
    {
        ContextDB.Add(rentedItem);
    }

    public static List<RentedItem> Get()
    {
        return ContextDB.GetRentedItems();
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    #region Properties

    public int ID { get; set; }

    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public uint Number { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }

    #endregion

    #region Constructors

    public RentedItem()
    {
    }

    public RentedItem(string Title, string Description, uint Number, decimal UnitPrice)
    {
        this.Title = Title;
        this.Description = Description;
        this.Number = Number;
        this.UnitPrice = UnitPrice;
    }

    #endregion
}