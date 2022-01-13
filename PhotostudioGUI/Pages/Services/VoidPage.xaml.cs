namespace PhotostudioGUI.Pages.Services;

/// <summary>
///     Страница для заполнения услуг требующих только сотрудника
/// </summary>
public partial class VoidPage
{
    private readonly OrderService _orderService;

    public VoidPage(OrderService orderService, Action<OrderService> removeService)
    {
        _orderService = orderService;
        RemoveService = removeService;
        InitializeComponent();
        FillItems();
    }

    private event Action<OrderService> RemoveService;

    /// <summary>
    ///     Заполнение сотрудников на основе услуги
    /// </summary>
    private void FillItems()
    {
        PriceTotal.Text = _orderService.Service.Cost!.Value.ToString("F");
        EmployeeComboBox.ItemsSource = _orderService.Service.ID switch
        {
            3 => Employee.GetByRoleId(3),
            4 => Employee.GetByRoleId(5),
            _ => Employee.GetByRoleId(7)
        };
        if (EmployeeComboBox.Items.Count != 0)
        {
            if (_orderService.Employee != null) EmployeeComboBox.SelectedValue = _orderService.Employee;
            return;
        }

        MessageTextBox.Text = "Сотрудники отсутствуют";
        EmployeeComboBox.IsEnabled = false;
    }

    /// <summary>
    ///     Выбор сотрудника
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((sender as ComboBox)!.SelectedItem is Employee employee) _orderService.Employee = employee;
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        RemoveService.Invoke(_orderService);
    }
}