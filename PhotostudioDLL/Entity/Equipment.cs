using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity;

public class Equipment
{
    public int ID { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    public Equipment(){}

    public Equipment(string Title, string Description)
    {
        this.Title = Title;
        this.Description = Description;
    }
}