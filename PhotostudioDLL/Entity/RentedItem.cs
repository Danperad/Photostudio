using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotostudioDLL.Entity;

public class RentedItem
{
    public int ID { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [Required] public uint Number { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }
    
    public RentedItem(){}

    public RentedItem(string Title, string Description, uint Number, decimal UnitPrice)
    {
        this.Title = Title;
        this.Description = Description;
        this.Number = Number;
        this.UnitPrice = UnitPrice;
    }
}