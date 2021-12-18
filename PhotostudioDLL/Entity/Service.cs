using System.ComponentModel.DataAnnotations.Schema;
using PhotostudioDLL.Entity.Interface;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class Service : ICostable
{
    public int ID { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public string GetTitle()
    {
        return Title;
    }
}