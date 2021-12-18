using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class Contract
{
    public int ID { get; set; }
    [Required] public virtual Client Client { get; set; }
    [Required] public virtual Employee Employee { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
}