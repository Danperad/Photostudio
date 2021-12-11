using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class Contract
{
    private static ApplicationContext db = Context.db;

    
    public int ID { get; set; }
    [Required] public Client Client { get; set; }
    [Required] public Employee Employee { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }

    public static void Add(Contract contract)
    {
        if (CheckContract(contract)) db.Contract.Add(contract);
        db.SaveChanges();
    }

    public static void AddAsync(Contract contract)
    {
        if (CheckContract(contract)) db.Contract.AddAsync(contract);
        db.SaveChanges();
    }

    public static void AddRange(params Contract[] contracts)
    {
        foreach (var contract in contracts)
            if (CheckContract(contract))
                db.Contract.Add(contract);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Contract[] contracts)
    {
        foreach (var contract in contracts)
            if (CheckContract(contract))
                db.Contract.AddAsync(contract);
        db.SaveChanges();
    }

    public static List<Contract> Get()
    {
        return db.Contract.ToList();
    }

    private static bool CheckContract(Contract contract)
    {
        if (contract.StartDate > contract.EndDate)
            throw new ContractDateException("Не соответствие дат в контракте", contract);
        return true;
    }
}