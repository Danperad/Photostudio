using PhotostudioDLL.Entities.Services;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

/// <summary>
///     Страница для заполнения услуги Аренды помещения
/// </summary>
public partial class HallRentPage
{
    private readonly HallRentService _orderService;
    private readonly ProvidedServiceWindow _window;

    /// <summary>
    ///     После инициализации страницы заполняет имеющими значениями
    /// </summary>
    /// <param name="orderService"></param>
    /// <param name="window"></param>
    public HallRentPage(HallRentService orderService, ProvidedServiceWindow window)
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

    private void EndDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        CheckEndFill();
    }

    private void TimePicker_OnSelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
    {
        CheckEndFill();
    }

    /// <summary>
    ///     Проверка времени и заполнение на основе его списка доступных помещений
    /// </summary>
    private void CheckEndFill()
    {
        HallComboBox.IsEnabled = false;
        HallComboBox.SelectedIndex = -1;
        if (!FillElements.CheckDateTime(StartDatePicker, EndDatePicker, StartTimePicker, EndTimePicker)) return;

        _orderService.StartTime =
            StartDatePicker.SelectedDate!.Value + StartTimePicker.SelectedTime!.Value.TimeOfDay;
        _orderService.EndTime =
            EndDatePicker.SelectedDate!.Value + EndTimePicker.SelectedTime!.Value.TimeOfDay;
        HallComboBox.IsEnabled = true;
        HallComboBox.ItemsSource =
            Hall.GetWithTime(_orderService.StartTime, _orderService.EndTime);
    }

    /// <summary>
    ///     Заполнение данных о выбранном помещении
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var hall = (sender as ComboBox)!.SelectedItem as Hall;
        if (hall is not null)
        {
            DescriptionBlock.Text = hall.Description;
            var price = hall.Cost!.Value;
            PricePerHour.Text = price.ToString("F");
            PriceTotal.Text = (price *
                               (decimal) (_orderService.EndTime - _orderService.StartTime).TotalHours)
                .ToString("F");
            _orderService.Hall = hall;
            return;
        }

        _orderService.Hall = hall;
        DescriptionBlock.Text = string.Empty;
        PricePerHour.Text = string.Empty;
        PriceTotal.Text = string.Empty;
    }

    /// <summary>
    ///     Удаление Услуги
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        _window.Remove(_orderService);
    }
}