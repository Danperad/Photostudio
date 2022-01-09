namespace PhotostudioDLL.Entities;

public class Contract
{
    #region Methods
    public static void Add(Contract contract)
    {
        Check(contract);
        ContextDB.Add(contract);
    }

    internal static void Check(Contract contract)
    {
        if (contract.StartDate > contract.EndDate)
            throw new Exception("DateError");
    }

    public static List<Contract> Get()
    {
        return ContextDB.GetContracts();
    }

    public static void Update()
    {
        ContextDB.Save();
    }
    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual Client Client { get; set; }
    public int ClientID { get; set; }
    public virtual Employee Employee { get; set; }
    public int EmployeeID { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public virtual Order Order { get; set; }
    public int OrderID { get; set; }

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
}