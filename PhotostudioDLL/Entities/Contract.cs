using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity;

public class Contract
{
    #region Properties

    public int ID { get; set; }
    [Required] public virtual Client Client { get; set; }
    [Required] public virtual Employee Employee { get; set; }
    [Required] public DateOnly StartDate { get; set; }
    [Required] public DateOnly EndDate { get; set; }

    #endregion


    #region Constructors

    public Contract()
    {
    }

    public Contract(Client Client, Employee Employee, DateOnly StartDate, DateOnly EndDate)
    {
        this.Client = Client;
        this.Employee = Employee;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
    }

    #endregion

    public static void Add(Contract contract)
    {
        Check(contract);
        ContextDB.Add(contract);
    }

    internal static void Check(Contract contract)
    {
        if (contract.StartDate > contract.EndDate)
            throw new System.Exception("DateError");
    }

    public static List<Contract> Get() => ContextDB.GetContracts();
}