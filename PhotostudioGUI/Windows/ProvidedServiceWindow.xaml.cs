using System.ComponentModel;
using System.Windows.Input;
using PhotostudioGUI.Pages.Services;

namespace PhotostudioGUI.Windows;

/// <summary>
///     Страница с предоставляемыми услугами
/// </summary>
public partial class ProvidedServiceWindow
{
    private readonly List<Service> _allServices;
    private readonly List<OrderService> _services;


    public ProvidedServiceWindow(List<OrderService> services, DateTime startDate, DateTime endDate,
        Employee employee, Action closeEvent)
    {
        StartDate = startDate;
        EndDate = endDate;
        Employee = employee;
        _services = services;
        CloseEvent = closeEvent;
        RemoveService += Remove;
        _allServices = Service.Get();
        InitializeComponent();
    }

    internal DateTime StartDate { get; }
    internal DateTime EndDate { get; }
    private Employee Employee { get; }
    private event Action CloseEvent;
    private event Action<OrderService> RemoveService;

    private void FrameworkElement_OnInitialized(object? sender, EventArgs e)
    {
        AddItems();
    }

    /// <summary>
    ///     Заполнение списка существующими услугами
    /// </summary>
    private void AddItems()
    {
        ProvidedListView.Items.Clear();
        foreach (var service in _services) ProvidedListView.Items.Add(service);
    }

    private void OpenExecuteableService(object sender, MouseButtonEventArgs e)
    {
        var service = ((ListViewItem) sender).Tag as OrderService;
        Navigate(service!);
    }

    /// <summary>
    ///     Открытие страницы с услугой
    /// </summary>
    /// <param name="orderService"></param>
    private void Navigate(OrderService orderService)
    {
        switch (orderService.Service.ID)
        {
            case 1:
            case 2:
            case 9:
            case 11:
            case 12:
                ServiceFrame.Navigate(new PhotoVideoPage(orderService, this));
                break;
            case 5:
            case 6:
            case 10:
                orderService.Employee = Employee;
                ServiceFrame.Navigate(new ItemRentPage(orderService, this));
                break;
            case 7:
                orderService.Employee = Employee;
                ServiceFrame.Navigate(new HallRentPage(orderService, this));
                break;
            case 13:
                ServiceFrame.Navigate(new StylePage(orderService, this));
                break;
            default:
                ServiceFrame.Navigate(new VoidPage(orderService, RemoveService));
                break;
        }
    }

    /// <summary>
    ///     Удаление услуги
    /// </summary>
    /// <param name="orderService"></param>
    internal void Remove(OrderService orderService)
    {
        ServiceFrame.Navigate(new Page());
        _services.Remove(orderService);
        AddItems();
    }

    /// <summary>
    ///     Добавление новой услуги
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddService_Click(object sender, RoutedEventArgs e)
    {
        var service = (sender as ComboBoxItem)!.Tag as Service;
        var sProvided = new OrderService {Service = service!};
        _services.Add(sProvided);
        ProvidedListView.Items.Add(sProvided);
        Navigate(sProvided);
    }

    /// <summary>
    ///     Заполнение списка возможных услуг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ServiceComboBoxInit(object? sender, EventArgs e)
    {
        (sender as ComboBox)!.ItemsSource = _allServices;
    }

    private void ServiceComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        (sender as ComboBox)!.SelectedIndex = -1;
    }

    private void CompleteButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    /// <summary>
    ///     Закрытие окна
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Closing(object sender, CancelEventArgs e)
    {
        CloseEvent.Invoke();
    }
}