using PhotostudioDLL.Entities;

namespace PhotostudioDLL;

/// <summary>
///     Класс для работы с базой данных
/// </summary>
internal static class ContextDb
{
    /// <summary>
    ///     Проверка на вхождение времени услуги в промежуток другой
    /// </summary>
    internal static Func<DateTime, DateTime, DateTime, DateTime, bool> FindDateTime =
        (start, end, serviceStart, serviceEnd) =>
            serviceStart < start && start <= serviceEnd ||
            serviceStart < end && end <= serviceEnd ||
            start <= serviceStart && serviceStart <= end ||
            start <= serviceEnd && serviceEnd <= end;

    private static ApplicationContext Db { get; set; }

    internal static void AddDb(ApplicationContext context)
    {
        Db = context;
    }

    public static void Save()
    {
        Db.SaveChanges();
    }

    #region Contract

    internal static List<Contract> GetContracts()
    {
        return Db.Contract.ToList();
    }

    #endregion

    #region Hall

    internal static List<Hall> GetHalls()
    {
        return Db.Hall.ToList();
    }

    #endregion

    #region Services

    internal static List<Service> GetServices()
    {
        Console.WriteLine(Db.Service.GetType().Name);
        return Db.Service.ToList();
    }

    #endregion

    #region OrderService

    internal static List<OrderService> GetExecuteableService()
    {
        return Db.ExecuteableService.ToList();
    }

    #endregion

    #region Client

    internal static void Add(Client client)
    {
        Db.Client.Add(client);
        Db.SaveChanges();
    }

    internal static List<Client> GetClients()
    {
        return Db.Client.ToList();
    }

    #endregion

    #region Employee

    internal static void Add(Employee employee)
    {
        Db.Employee.Add(employee);
        Db.SaveChanges();
    }

    internal static List<Employee> GetEmployees()
    {
        return Db.Employee.ToList();
    }

    internal static Employee? GetAuth(string login, string pass)
    {
        return Db.Employee.FirstOrDefault(d => d.Profile.Login == login && d.Profile.Password == pass);
    }

    internal static Employee? GetEmployeeById(int id)
    {
        return Db.Employee.FirstOrDefault(employee => employee.ID == id);
    }

    #endregion

    #region Equipment

    internal static void Add(Equipment equipment)
    {
        Db.Equipment.Add(equipment);
        Db.SaveChanges();
    }

    internal static List<Equipment> GetEquipments()
    {
        return Db.Equipment.ToList();
    }

    #endregion

    #region Inventory

    internal static void Add(Inventory inventory)
    {
        Db.Inventory.Add(inventory);
        Db.SaveChanges();
    }

    internal static List<Inventory> GetInventories()
    {
        return Db.Inventory.ToList();
    }

    #endregion

    #region Order

    internal static void Add(Order order)
    {
        Db.Order.Add(order);
        Db.SaveChanges();
    }

    internal static List<Order> GetOrders()
    {
        return Db.Order.ToList();
    }

    #endregion

    #region RentedItem

    internal static void Add(RentedItem rentedItem)
    {
        Db.RentedItem.Add(rentedItem);
        Db.SaveChanges();
    }

    internal static List<RentedItem> GetRentedItems()
    {
        return Db.RentedItem.ToList();
    }

    #endregion

    #region Role

    internal static void Add(Role role)
    {
        Db.Role.Add(role);
        Db.SaveChanges();
    }

    internal static List<Role> GetRoles()
    {
        return Db.Role.ToList();
    }

    internal static Role? GetRoleById(int id)
    {
        return Db.Role.FirstOrDefault(d => d.ID == id);
    }

    #endregion
}