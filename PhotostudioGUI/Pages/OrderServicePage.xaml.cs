using System.Windows.Input;

namespace PhotostudioGUI.Pages;

/// <summary>
///     Страница с предоставляемыми услугами
/// </summary>
public partial class OrderServicePage
{
    private readonly Employee _employee;

    public OrderServicePage(Employee employee)
    {
        _employee = employee;
        InitializeComponent();
    }

    public OrderServicePage(Employee employee, Action LogOut) : this(employee)
    {
        LogOutEvent = LogOut;
        AddLogOutBtn();
        EmployeeBlock.Text = _employee.FullName;
    }

    private event Action? LogOutEvent;

    /// <summary>
    ///     Добавление кнопки Выхода из аккаунта
    /// </summary>
    private void AddLogOutBtn()
    {
        var btn = new Button
            {Content = "Выйти", FontSize = 14, Width = 75, HorizontalAlignment = HorizontalAlignment.Right};
        btn.Click += LogOut_OnClick;
        EmployeePanel.Children.Add(btn);
    }

    private void LogOut_OnClick(object sender, RoutedEventArgs e)
    {
        LogOutEvent?.Invoke();
    }

    /// <summary>
    ///     Заполнение предоставляемых услуг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ServicesListView_OnInitialized(object? sender, EventArgs e)
    {
        var services = new List<OrderService>();
        switch (_employee.RoleID)
        {
            case 1:
                services.AddRange(OrderService.Get());
                break;
            case 2:
                services.AddRange(OrderService.GetWithServiceId(1));
                services.AddRange(OrderService.GetWithServiceId(9));
                services.AddRange(OrderService.GetWithServiceId(11));
                break;
            case 3:
                services.AddRange(OrderService.GetWithServiceId(3));
                break;
            case 4:
                services.AddRange(OrderService.GetWithServiceId(2));
                services.AddRange(OrderService.GetWithServiceId(12));
                break;
            case 5:
                services.AddRange(OrderService.GetWithServiceId(4));
                break;
            case 6:
                services.AddRange(OrderService.GetWithServiceId(13));
                break;
            case 7:
                services.AddRange(OrderService.GetWithServiceId(5));
                services.AddRange(OrderService.GetWithServiceId(6));
                services.AddRange(OrderService.GetWithServiceId(7));
                services.AddRange(OrderService.GetWithServiceId(8));
                services.AddRange(OrderService.GetWithServiceId(10));
                break;
        }

        if (_employee.RoleID == 1) (sender as ListView)!.ItemsSource = services;
        else
            (sender as ListView)!.ItemsSource = services.Where(s =>
                s.Status != OrderService.ServiceStatus.COMPLETE && s.Status != OrderService.ServiceStatus.CANCЕLED &&
                s.Employee == _employee);
    }

    /// <summary>
    ///     Открытие информации про услугу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (((ListViewItem) sender).Tag is OrderService service) AddServiceInfo(service);
    }

    private void AddServiceInfo(OrderService service)
    {
        ServiceInfoPanel.Children.Clear();
        var textBlocks = BuildPageOrderService.BuildPage(service);
        foreach (var block in textBlocks) ServiceInfoPanel.Children.Add(block);

        var btn = new Button
        {
            Content = "Изменить Статус", FontSize = 15, FontWeight = FontWeights.Normal, Tag = service,
            HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(10)
        };
        btn.Click += ChangeStatusButton_OnClick;

        if (service.Status == OrderService.ServiceStatus.INPROGRESS && service.StartTime == null)
            ServiceInfoPanel.Children.Add(btn);
        else if (service.Status == OrderService.ServiceStatus.WAITING)
            ServiceInfoPanel.Children.Add(btn);
    }

    /// <summary>
    ///     Обработка кнопки изменения статуса
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ChangeStatusButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (((Button) sender).Tag is not OrderService service) return;
        if (service.Status == OrderService.ServiceStatus.WAITING)
        {
            service.Status = OrderService.ServiceStatus.INPROGRESS;
            if (service.Order.Status == Order.OrderStatus.PREWORK)
                service.Order.Status = Order.OrderStatus.INWORK;
        }
        else if (service.Status == OrderService.ServiceStatus.INPROGRESS && service.StartTime == null)
        {
            service.Status = OrderService.ServiceStatus.COMPLETE;
            if (service.Order.Status == Order.OrderStatus.INWORK &&
                service.Order.Services.All(s => s.Status == OrderService.ServiceStatus.COMPLETE))
                service.Order.Status = Order.OrderStatus.COMPLETE;
        }

        OrderService.Update();
        AddServiceInfo(service);
    }
}