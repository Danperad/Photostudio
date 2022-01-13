using System.ComponentModel;
using System.Windows.Input;
using PhotostudioGUI.Pages;

namespace PhotostudioGUI.Windows;

/// <summary>
///     Окно с информацией о заявке
/// </summary>
public partial class SelectedOrderWindow
{
    private readonly Order _selectedOrder;

    public SelectedOrderWindow(Order order, Action closeEvent)
    {
        _selectedOrder = order;
        CloseEvent = closeEvent;
        InitializeComponent();
        FillOrderInfo();
        Title = $"{order.Client.FullName} {order.DateTime:dd MMMM yyyy}";
    }

    private event Action CloseEvent;

    private void ListView_Initialized(object sender, EventArgs e)
    {
        (sender as ListView)!.ItemsSource = _selectedOrder.Services;
    }

    /// <summary>
    ///     Заполнение основной информации о заявке
    /// </summary>
    private void FillOrderInfo()
    {
        var client = _selectedOrder.Client;
        var contract = _selectedOrder.Contract;
        var employee = _selectedOrder.Contract.Employee;
        ClientNameBlock.Text = $"{client.LastName} {client.FirstName} {client.MiddleName ?? ""}";
        ClientPhoneBlock.Text = client.PhoneNumber;
        ClientEMailBlock.Text = client.EMail ?? "Отсутствует";
        EmployeeBlock.Text = $"{employee.LastName} {employee.FirstName} {employee.MiddleName ?? ""}";
        ClientDateBlock.Text = DateOnly.FromDateTime(_selectedOrder.DateTime).ToLongDateString();
        ClientContractStartBlock.Text = contract.StartDate.ToLongDateString();
        ClientContractEndBlock.Text = contract.EndDate.ToLongDateString();
    }

    /// <summary>
    ///     Выбор услуги для отображение информации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OpenExecuteableService(object sender, MouseButtonEventArgs e)
    {
        var service = ((ListViewItem) sender).Tag as OrderService;
        if (service is null) return;
        ServiceFrame.Navigate(new CurrentService(service));
    }

    /// <summary>
    ///     Закрытие окна
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SelectedOrderWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        CloseEvent.Invoke();
    }
}