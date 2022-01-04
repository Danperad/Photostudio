using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotostudioDLL.Entity;

public class Service
{
    public int ID { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    
    public Service(){}

    public Service(string Title, string Description, uint Number, decimal Price)
    {
        this.Title = Title;
        this.Description = Description;
        this.Price = Price;
    }
}