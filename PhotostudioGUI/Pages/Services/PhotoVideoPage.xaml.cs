using Castle.Core.Internal;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class PhotoVideoPage
{
    private readonly OrderService _orderService;
    private readonly ProvidedServiceWindow _window;

    public PhotoVideoPage(OrderService orderService, ProvidedServiceWindow window)
    {
        _orderService = orderService;
        _window = window;
        InitializeComponent();
        FillElements.FillElement(_orderService, this);
        CheckEndFill();
    }

    private void DatePicker_OnInitialized(object? sender, EventArgs e)
    {
        var datePicker = sender as DatePicker;
        datePicker!.DisplayDateStart = _window.StartDate;
        datePicker.DisplayDateEnd = _window.EndDate;
    }

    private void StartDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        EndDatePicker.DisplayDateStart = StartDatePicker.SelectedDate;
        CheckEndFill();
    }

    private void EndDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        CheckEndFill();
    }

    private void TimePicker_OnSelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
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
        // Проверка времени и заполнение им выбранной услуги
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
        // Выбор списка сотрудников на основе услуги
        switch (_orderService.Service.ID)
        {
            case 1:
            case 9:
            case 11:
                EmployeeComboBox.ItemsSource =
                    Employee.GetPhotoWithTime(_orderService.StartTime!.Value, _orderService.EndTime!.Value);
                break;
            default:
                EmployeeComboBox.ItemsSource =
                    Employee.GetVideoWithTime(_orderService.StartTime!.Value, _orderService.EndTime!.Value);
                break;
        }

        // Проверка на количество доступных сотрудников
        if (EmployeeComboBox.Items.Count != 0)
        {
            if (_orderService.Employee != null) EmployeeComboBox.SelectedValue = _orderService.Employee;
            if (_orderService.PhotoLocation != null) LocationTextBox.Text = _orderService.PhotoLocation;
            EmployeeComboBox.IsEnabled = true;
            MessageTextBlock.Text = "";
            PriceTotal.Text = (_orderService.Service.Cost!.Value * (decimal) hours).ToString("F");
        }
        else
        {
            MessageTextBlock.Text = "Доступных сотрудников нет";
        }
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((sender as ComboBox)!.SelectedItem is Employee employee) _orderService.Employee = employee;
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        _window.Remove(_orderService);
    }

    private void LocationTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var location = sender as TextBox;
        if (location!.Text.IsNullOrEmpty()) return;
        _orderService.PhotoLocation = location.Text;
    }
}