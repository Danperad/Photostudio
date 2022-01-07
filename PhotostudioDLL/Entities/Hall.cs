using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotostudioDLL.Entity;

public class Hall
{
    #region Properties

    public int ID { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal PricePerHour { get; set; }

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