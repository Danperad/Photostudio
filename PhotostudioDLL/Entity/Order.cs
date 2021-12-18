using System.Text;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class Order
{
    public int ID { get; set; }

    [Required] public virtual Contract Contract { get; set; }

    [Required] public virtual Client Client { get; set; }

    [Required] public DateTime DateTime { get; set; }

    [Required] public string Status { get; set; }
    [Required] public virtual List<ServiceProvided> Services { get; set; }

    public Order()
    {
        Services = new List<ServiceProvided>();
    }
}