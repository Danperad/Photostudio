namespace PhotostudioDLL.Entities;

public class Contract
{
    #region Methods

    internal static bool Check(Contract contract)
    {
        return contract.StartDate <= contract.EndDate ;
    }

    public static List<Contract> Get()
    {
        return ContextDb.GetContracts();
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual Client Client { get; set; }
    public virtual Employee Employee { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public virtual Order Order { get; set; }

    #endregion

    #region Constructors

    public Contract()
    {
    }

    public Contract(Client client, Employee employee, DateOnly startDate, DateOnly endDate)
    {
        Client = client;
        Employee = employee;
        StartDate = startDate;
        EndDate = endDate;
    }

    #endregion
}