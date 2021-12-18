namespace PhotostudioDLL.Entity;

public class ServiceProvided
{
    public int ID { get; set; }

    [Required] public virtual Employee Employee { get; set; }

    public virtual RentedItem? RentedItem { get; set; }
    public virtual Hall? Hall { get; set; }

    [Required] public virtual Service Service { get; set; }

    public int? Number { get; set; }
    public DateTime? StartRent { get; set; }
    public DateTime? EndRent { get; set; }
    public string? PhotoLocation { get; set; }
    public DateTime? PhotoStartDateTime { get; set; }
    public DateTime? PhotoEndDateTime { get; set; }
    
    [Required] public virtual Order Order { get; set; }
}