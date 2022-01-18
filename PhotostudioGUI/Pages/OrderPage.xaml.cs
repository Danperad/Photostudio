using System.Windows.Input;
using PhotostudioDLL.Entities.Services;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages;

public partial class OrderPage
{
    private readonly Client? _client;
    private readonly Employee _employee;
    private Order? _order;
    private List<Order> _orders;

    public OrderPage(Employee employee)
    {
        _employee = employee;
        _orders = Order.Get();
        InitializeComponent();
        (MainOrderGrid.Parent as Grid)?.ColumnDefinitions.RemoveAt(1);
        AddPanel.Children.Clear();
        AddPanel.Background = MainOrderGrid.Background;
        ServicesWindow += DisplayServices;
    }

    public OrderPage(Employee employee, Client client)
    {
        _client = client;
        _employee = employee;
        _order = new Order();
        _orders = Order.Get().Where(o => o.Client == client).ToList();
        ServicesWindow += DisplayServices;
        InitializeComponent();
    }

    private event Action ServicesWindow;

    private void OrderData_OnInitialized(object? sender, EventArgs e)
    {
        (sender as ListView)!.ItemsSource = _orders;
    }

    /// <summary>
    ///     Открытие информации про выбранную заявку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (((ListViewItem) sender).Content is not Order order) return;
        var window = new SelectedOrderWindow(order, ServicesWindow);
        window.Show();
        Application.Current.MainWindow!.IsEnabled = false;
    }

    /// <summary>
    ///     Сборка и добавление новой заявки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddOrderClick(object sender, RoutedEventArgs e)
    {
        if (!(StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue))
        {
            ErrorBlock.Text = "Выберите корректные даты";
            return;
        }

        _order!.Contract = new Contract(_client!, _employee,
            DateOnly.FromDateTime(StartDatePicker.SelectedDate!.Value),
            DateOnly.FromDateTime(EndDatePicker.SelectedDate!.Value));
        _order.Client = _client!;
        _order.DateTime = DateTime.Now;
        if (_order.Services.Count == 0)
        {
            ErrorBlock.Text = "Вы не выбрали услуги";
            return;
        }

        var checkServices = OrderService.CheckList(_order.Services);
        if (checkServices is not null)
        {
            ErrorBlock.Text = $"Проблема в услуге {checkServices.Service.Title}";
            return;
        }

        Order.Add(_order);

        OrderCompleted();
    }

    /// <summary>
    ///     Отчистка всех полей добавления, после успешной вставки
    ///     И заполнение таблицы новыми данными
    /// </summary>
    private void OrderCompleted()
    {
        StartDatePicker.SelectedDate = null;
        EndDatePicker.SelectedDate = null;
        _order = new Order();
        ErrorBlock.Text = string.Empty;
        ServicesBlock.Text = string.Empty;
        TotalPriceBlock.Text = string.Empty;
        _orders = Order.Get().Where(o => o.Client == _client).ToList();
        OrderData.ItemsSource = _orders;
    }

    private void DatePicker_OnInitialized(object? sender, EventArgs e)
    {
        (sender as DatePicker)!.DisplayDateStart = DateTime.Today.AddDays(1);
    }

    /// <summary>
    ///     Открытие окна для выбора предоставляемых услуг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!(StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue))
        {
            ErrorBlock.Text = "Выберите корректные даты";
            return;
        }

        var window = new ProvidedServiceWindow(_order!.Services, StartDatePicker.SelectedDate.Value,
            EndDatePicker.SelectedDate.Value, _employee, ServicesWindow);
        window.Show();
        ErrorBlock.Text = string.Empty;
        Application.Current.MainWindow!.IsEnabled = false;
    }

    private void StartDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (StartDatePicker.SelectedDate.HasValue)
            EndDatePicker.DisplayDateStart = StartDatePicker.SelectedDate.Value.AddDays(3);
    }

    private void DisplayServices()
    {
        if (_client != null && _order != null)
        {
            ServicesBlock.Text = $"Предоставляемые услуги: {_order!.ListServices}";
            TotalPriceBlock.Text = $"Итого: {_order.AllGetCost:F} Р.";
        }

        Application.Current.MainWindow!.IsEnabled = true;
    }

    private void TextBlock_Initialized(object sender, EventArgs e)
    {
        (sender as TextBlock)!.Text = _client != null ? _client.FullName : "";
    }

    private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        StartDatePicker.DisplayDateEnd = (sender as DatePicker)!.SelectedDate;
    }
}