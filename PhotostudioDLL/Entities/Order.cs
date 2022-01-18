﻿using PhotostudioDLL.Attributes;
using PhotostudioDLL.Entities.Interfaces;
using PhotostudioDLL.Entities.Services;

namespace PhotostudioDLL.Entities;

public class Order
{
    public enum OrderStatus
    {
        [StringValue("Завершена")] COMPLETE,
        [StringValue("Подготовка")] PREWORK,
        [StringValue("В работе")] INWORK,
        [StringValue("Отменен")] CLOSED
    }

    #region Methods

    /// <summary>
    ///     Проверка и изменения статуса заявок
    /// </summary>
    public static void CheckStatusTime()
    {
        var orders = Get();
        foreach (var order in orders)
        {
            foreach (var service in order.Services)
            {
                if (service.Service.Type == Service.ServiceType.SIMPLE) continue;

                if (service.Status == OrderService.ServiceStatus.INPROGRESS &&
                    (service as ITimedService)!.EndTime < DateTime.Now)
                    service.Status = OrderService.ServiceStatus.COMPLETE;
            }

            if (order.Services.All(s => s.Status == OrderService.ServiceStatus.COMPLETE))
                order.Status = OrderStatus.COMPLETE;
            if (order.Status != OrderStatus.COMPLETE &&
                order.Contract.EndDate < DateOnly.FromDateTime(DateTime.Today))
            {
                order.Status = OrderStatus.CLOSED;
                foreach (var service in order.Services) service.Status = OrderService.ServiceStatus.CANCЕLED;
            }
        }

        Update();
    }

    /// <summary>
    ///     Добавление новой заявки
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public static bool Add(Order order)
    {
        if (!Check(order)) return false;
        ContextDb.Add(order);
        return true;
    }

    /// <summary>
    ///     Проверка заявки
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private static bool Check(Order order)
    {
        return Contract.Check(order.Contract) && DateOnly.FromDateTime(order.DateTime) <= order.Contract.StartDate;
    }

    /// <summary>
    ///     Получение всех заявок
    /// </summary>
    /// <returns></returns>
    public static List<Order> Get()
    {
        return ContextDb.GetOrders();
    }

    /// <summary>
    ///     Установка новых значение в базе данных
    /// </summary>
    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual Contract Contract { get; set; }
    public virtual Client Client { get; set; }
    public int ClientID { get; set; }
    public DateTime DateTime { get; set; }
    public OrderStatus Status { get; set; }
    public virtual List<OrderService> Services { get; set; }

    /// <summary>
    ///     Своство расчитываемое общую стоимость заявки
    /// </summary>
    public decimal AllGetCost
    {
        get
        {
            decimal cost = 0;
            foreach (var service in Services)
            {
                cost += service.Service.Cost.GetValueOrDefault(0);
                switch (service.Service.Type)
                {
                    case Service.ServiceType.RENT:
                        cost += (service as RentService)!.RentedItem.Cost!.Value * (service as RentService)!.Number;
                        break;
                    case Service.ServiceType.HALLRENT:
                        var tmp = service as HallRentService;
                        cost += tmp!.Hall.Cost!.Value *
                                (decimal) (tmp.EndTime - tmp.StartTime).TotalHours;
                        break;
                    case Service.ServiceType.PHOTOVIDEO:
                        var tmp1 = service as PhotoVideoService;
                        cost += tmp1!.Service.Cost!.Value *
                                (decimal) (tmp1.EndTime - tmp1.StartTime).TotalHours;
                        break;
                }
            }

            return cost;
        }
    }

    /// <summary>
    ///     Свойство находящее описание статуса используя рефлексию
    /// </summary>
    public string? TextStatus
    {
        get
        {
            var type = Status.GetType();
            var fieldInfo = type.GetField(Status.ToString());
            var attribs = fieldInfo!.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs!.Length > 0 ? attribs[0].StringValue : null;
        }
    }

    public string ListServices
    {
        get
        {
            if (Services.Count == 0) return "";
            var temp = Services.Aggregate(string.Empty, (current, service) => current + $"{service.Service.Title}, ");
            return temp.Remove(temp.Length - 2, 2);
        }
    }

    #endregion

    #region Constructors

    public Order()
    {
        Services = new List<OrderService>();
        Status = OrderStatus.PREWORK;
    }

    public Order(Contract contract, Client client, DateTime dateTime, List<OrderService> services)
    {
        Status = OrderStatus.PREWORK;
        Contract = contract;
        Client = client;
        DateTime = dateTime;
        Services = services;
    }

    #endregion
}