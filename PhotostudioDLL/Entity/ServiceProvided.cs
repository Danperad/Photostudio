namespace PhotostudioDLL.Entity;

public class ServiceProvided
{
    public int ID { get; set; }

    [Required] public Employee Employee { get; set; }

    public RentedItem RentedItem { get; set; }
    public Hall Hall { get; set; }

    [Required] public Service Service { get; set; }

    public int Number { get; set; }
    public DateTime StartRent { get; set; }
    public DateTime EndRent { get; set; }
    public string PhotoLocation { get; set; }
    public DateTime PhotoStartDateTime { get; set; }
    public DateTime PhotoEndDateTime { get; set; }


    [Required] public Order Order { get; set; }
}