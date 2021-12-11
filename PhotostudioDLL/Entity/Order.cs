using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class Order
{
    private static ApplicationContext db = Context.db;

    public int ID { get; set; }

    [Required] public Contract Contract { get; set; }

    [Required] public Client Client { get; set; }

    [Required] public DateTime DateTime { get; set; }

    [Required] public string Status { get; set; }

    public static void Add(Order order)
    {
        if (CheckOrder(order)) db.Order.Add(order);
        db.SaveChanges();
    }

    public static void AddAsync(Order order)
    {
        if (CheckOrder(order)) db.Order.AddAsync(order);
        db.SaveChanges();
    }

    public static void AddRange(params Order[] orders)
    {
        foreach (var order in orders)
            if (CheckOrder(order))
                db.Order.Add(order);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Order[] orders)
    {
        foreach (var order in orders)
            if (CheckOrder(order))
                db.Order.AddAsync(order);
        db.SaveChanges();
    }

    public static List<Order> Get()
    {
        return db.Order.ToList();
    }

    public static List<FullOrder> Gett()
    {
        return db.Order.Select(e => new FullOrder
        {
            ID = e.ID,
            Contract = e.Contract.ID,
            Client = e.Client.GetName(),
            DateTime = e.DateTime,
            Status = e.Status
        }).ToList();
    }

    private static bool CheckOrder(Order order)
    {
        if (order.Contract.StartDate > order.Contract.EndDate)
            throw new ContractDateException("Не соответствие дат в контракте", order.Contract);
        if (order.Contract.StartDate < order.DateTime)
            throw new OrderDateException("Не соответствие дат в контракте и заявке", order);
        return true;
    }

    public class FullOrder
    {
        public int ID { get; set; }
        public int Contract { get; set; }
        public string Client { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
    }
}