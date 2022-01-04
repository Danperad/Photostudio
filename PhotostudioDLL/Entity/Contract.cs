using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity;

public class Contract
{
    public int ID { get; set; }
    [Required] public virtual Client Client { get; set; }
    [Required] public virtual Employee Employee { get; set; }
    [Required] public DateOnly StartDate { get; set; }
    [Required] public DateOnly EndDate { get; set; }

    public Contract() { }

    public Contract(Client Client, Employee Employee, DateOnly StartDate, DateOnly EndDate)
    {
        this.Client = Client;
        this.Employee = Employee;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
    }
}