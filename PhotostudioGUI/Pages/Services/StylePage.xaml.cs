using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

/// <summary>
///     Страница для заполнения услуги Стилиста/Визажиста
/// </summary>
public partial class StylePage
{
    private readonly OrderService _orderService;
    private readonly ProvidedServiceWindow _window;

    /// <summary>
    ///     После инициализации страницы заполняет имеющими значениями
    /// </summary>
    /// <param name="orderService"></param>
    /// <param name="window"></param>
    public StylePage(OrderService orderService, ProvidedServiceWindow window)
    {
        _orderService = orderService;
        _window = window;
        InitializeComponent();
        FillElements.FillElement(_orderService, this);
        CheckEndFill();
    }

    /// <summary>
    ///     Ограничения на минимальный и максимальный день
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DatePicker_OnInitialized(object? sender, EventArgs e)
    {
        (sender as DatePicker)!.DisplayDateStart = _window.StartDate;
        (sender as DatePicker)!.DisplayDateEnd = _window.EndDate;
    }

    private void StartDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        EndDatePicker.DisplayDateStart = StartDatePicker.SelectedDate;
        CheckEndFill();
    }

    private void TimePicker_OnSelectedTimeChanged(object sender, RoutedEventArgs e)
    {
        CheckEndFill();
    }

    /// <summary>
    ///     Проверка времени и заполнение на основе его списка доступных сотрудников
    /// </summary>
    private void CheckEndFill()
    {
        EmployeeComboBox.IsEnabled = false;
        EmployeeComboBox.SelectedIndex = -1;
        // Заполнение выбранной услуги временем 
        if (!FillElements.CheckDateTime(StartDatePicker, EndDatePicker, StartTimePicker, EndTimePicker)) return;

        _orderService.StartTime =
            StartDatePicker.SelectedDate!.Value + StartTimePicker.SelectedTime!.Value.TimeOfDay;
        _orderService.EndTime =
            EndDatePicker.SelectedDate!.Value + EndTimePicker.SelectedTime!.Value.TimeOfDay;

        var hours = (_orderService.EndTime - _orderService.StartTime)!.Value.TotalHours;
        // Заполнение списка сотрудников
        MessageTextBlock.Text = "";
        FillEmployees(hours);
    }

    /// <summary>
    ///     Заполнение списка сотрудников
    /// </summary>
    private void FillEmployees(double hours)
    {
        EmployeeComboBox.ItemsSource =
            Employee.GetStyleWithTime(_orderService.StartTime!.Value, _orderService.EndTime!.Value);
        // Проверка на количество доступных сотрудников
        if (EmployeeComboBox.Items.Count != 0)
        {
            if (_orderService.Employee != null) EmployeeComboBox.SelectedValue = _orderService.Employee;
            EmployeeComboBox.IsEnabled = true;
            MessageTextBlock.Text = "";
            PriceTotal.Text = (_orderService.Service.Cost!.Value * (decimal) hours).ToString("F");
        }
        else
        {
            MessageTextBlock.Text = "Доступных сотрудников нет";
        }
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
        _window.Remove(_orderService);
    }
}