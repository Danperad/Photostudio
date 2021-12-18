using System.Text;
using PhotostudioDLL.Entity;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL;

public static class ContextDB
{
    public static ApplicationContext db { get; private set; }

    internal static void AddDB(ApplicationContext dbb)
    {
        db = dbb;
    }

    #region Client

    public static void Add(Client client)
    {
        db.Client.Add(client);
        db.SaveChanges();
    }

    public static void AddAsync(Client client)
    {
        db.Client.AddAsync(client);
        db.SaveChanges();
    }

    public static void AddRange(params Client[] clients)
    {
        foreach (var client in clients) db.Client.Add(client);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Client[] clients)
    {
        foreach (var client in clients) db.Client.AddAsync(client);
        db.SaveChanges();
    }

    public static List<Client> GetClients()
    {
        return db.Client.ToList();
    }

    #endregion

    #region Contract

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

    public static List<Contract> GetContracts()
    {
        return db.Contract.ToList();
    }

    private static bool CheckContract(Contract contract)
    {
        if (contract.StartDate > contract.EndDate)
            throw new ContractDateException("Не соответствие дат в контракте", contract);
        return true;
    }

    #endregion

    #region Employee

    public static void Add(Employee employee)
    {
        db.Employee.Add(employee);
        db.SaveChanges();
    }

    public static void AddAsync(Employee employee)
    {
        db.Employee.AddAsync(employee);
        db.SaveChanges();
    }

    public static void AddRange(params Employee[] employees)
    {
        foreach (var employee in employees) db.Employee.Add(employee);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Employee[] employees)
    {
        foreach (var employee in employees) db.Employee.AddAsync(employee);
        db.SaveChanges();
    }

    public static List<Employee> GetEmployees()
    {
        return db.Employee.ToList();
    }

    public static Employee GetAuth(string login, string pass)
    {
        return db.Employee.Where(d => d.Profile.Login == login && d.Profile.Password == pass).FirstOrDefault();
    }

    #endregion

    #region Equipment

    public static void Add(Equipment equipment)
    {
        db.Equipment.Add(equipment);
        db.SaveChanges();
    }

    public static void AddAsync(Equipment equipment)
    {
        db.Equipment.AddAsync(equipment);
        db.SaveChanges();
    }

    public static void AddRange(params Equipment[] equipments)
    {
        foreach (var equipment in equipments) db.Equipment.Add(equipment);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Equipment[] equipments)
    {
        foreach (var equipment in equipments) db.Equipment.AddAsync(equipment);
        db.SaveChanges();
    }

    public static List<Equipment> GetEquipments()
    {
        return db.Equipment.ToList();
    }

    #endregion

    #region Hall

    public static void Add(Hall hall)
    {
        db.Hall.Add(hall);
        db.SaveChanges();
    }

    public static void AddAsync(Hall hall)
    {
        db.Hall.AddAsync(hall);
        db.SaveChanges();
    }

    public static void AddRange(params Hall[] halls)
    {
        foreach (var hall in halls) db.Hall.Add(hall);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Hall[] halls)
    {
        foreach (var hall in halls) db.Hall.AddAsync(hall);
        db.SaveChanges();
    }

    public static List<Hall> GetHalls()
    {
        return db.Hall.ToList();
    }

    private static bool CheckHall(Hall hall)
    {
        if (hall.PricePerHour < 0) throw new MoneyException("Цена не может быть меньше нуля", hall);
        return true;
    }

    #endregion

    #region Inventory

    public static void Add(Inventory inventory)
    {
        db.Inventory.Add(inventory);
        db.SaveChanges();
    }

    public static void AddAsync(Inventory inventory)
    {
        db.Inventory.AddAsync(inventory);
        db.SaveChanges();
    }

    public static void AddRange(params Inventory[] inventories)
    {
        foreach (var inventory in inventories) db.Inventory.Add(inventory);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Inventory[] inventories)
    {
        foreach (var inventory in inventories) db.Inventory.AddAsync(inventory);
        db.SaveChanges();
    }

    public static List<Inventory> GetInventories()
    {
        return db.Inventory.ToList();
    }

    #endregion

    #region Order

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

    public static List<FullOrder> GetOrders()
    {
        return db.Order.Select(e => new FullOrder
        {
            ID = e.ID,
            Contract = e.Contract.ID,
            Client = e.Client.GetName(),
            DateTime = e.DateTime,
            Status = e.Status,
            Services = GetServiceName(e.Services)
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
        public string Services { get; set; }
    }

    public static string GetServiceName(List<ServiceProvided> services)
    {
        StringBuilder temp = new StringBuilder();
        foreach (var provided in services)
        {
            temp.Append($"{provided.Service.Title}, ");
        }

        temp.Remove(temp.Length - 2, 2);
        return temp.ToString();
    }

    #endregion

    #region RentedItem

    public static void Add(RentedItem rentedItem)
    {
        db.RentedItem.Add(rentedItem);
        db.SaveChanges();
    }

    public static void AddAsync(RentedItem rentedItem)
    {
        db.RentedItem.AddAsync(rentedItem);
        db.SaveChanges();
    }

    public static void AddRange(params RentedItem[] rentedItems)
    {
        foreach (var rentedItem in rentedItems) db.RentedItem.Add(rentedItem);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params RentedItem[] rentedItems)
    {
        foreach (var rentedItem in rentedItems) db.RentedItem.AddAsync(rentedItem);
        db.SaveChanges();
    }

    public static List<RentedItem> GetRentedItems()
    {
        return db.RentedItem.ToList();
    }

    #endregion

    #region Role

    public static void Add(Role role)
    {
        db.Role.Add(role);
        db.SaveChanges();
    }

    public static void AddAsync(Role role)
    {
        db.Role.AddAsync(role);
        db.SaveChanges();
    }

    public static void AddRange(params Role[] roles)
    {
        foreach (var role in roles) db.Role.Add(role);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Role[] roles)
    {
        foreach (var role in roles) db.Role.AddAsync(role);
        db.SaveChanges();
    }

    public static List<Role> GetRoles()
    {
        return db.Role.ToList();
    }

    public static Role GetRoleByID(int id)
    {
        return db.Role.Where(d => d.ID == id).FirstOrDefault();
    }

    #endregion

    #region Services

    public static void Add(Service service)
    {
        db.Service.Add(service);
        db.SaveChanges();
    }

    public static void AddAsync(Service service)
    {
        db.Service.AddAsync(service);
        db.SaveChanges();
    }

    public static void AddRange(params Service[] services)
    {
        foreach (var service in services) db.Service.Add(service);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Service[] services)
    {
        foreach (var service in services) db.Service.AddAsync(service);
        db.SaveChanges();
    }

    public static List<Service> GetServices()
    {
        return db.Service.ToList();
    }

    #endregion
}