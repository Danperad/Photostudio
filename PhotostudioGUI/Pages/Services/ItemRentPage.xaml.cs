using Castle.Core.Internal;
using PhotostudioDLL.Entities.Services;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

/// <summary>
///     Страница для заполнения услуги Аренды вещей
/// </summary>
public partial class ItemRentPage
{
    private readonly RentService _orderService;
    private readonly ProvidedServiceWindow _window;
    private readonly char[] numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

    /// <summary>
    ///     После инициализации страницы заполняет имеющими значениями
    /// </summary>
    /// <param name="orderService"></param>
    /// <param name="window"></param>
    public ItemRentPage(RentService orderService, ProvidedServiceWindow window)
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
    ///     Проверка на верность введенного времени и заполнение списка вещей на их основе
    /// </summary>
    private void CheckEndFill()
    {
        ItemComboBox.IsEnabled = false;
        ItemComboBox.SelectedIndex = -1;

        if (!FillElements.CheckDateTime(StartDatePicker, EndDatePicker, StartTimePicker, EndTimePicker)) return;

        _orderService.StartTime =
            StartDatePicker.SelectedDate!.Value + StartTimePicker.SelectedTime!.Value.TimeOfDay;
        _orderService.EndTime =
            EndDatePicker.SelectedDate!.Value + EndTimePicker.SelectedTime!.Value.TimeOfDay;

        ItemComboBox.IsEnabled = true;
        ItemComboBox.ItemsSource = _orderService.Service.ID switch
        {
            5 => RentedItem.GetСlothes(_orderService.StartTime, _orderService.EndTime),
            6 => RentedItem.GetNoDress(_orderService.StartTime, _orderService.EndTime),
            10 => RentedItem.GetKidsСlothes(_orderService.StartTime, _orderService.EndTime),
            _ => ItemComboBox.ItemsSource
        };
    }

    /// <summary>
    ///     Заполнение данных о выбранном помещении
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = (sender as ComboBox)!.SelectedItem as RentedItem;
        if (item is not null)
        {
            DescriptionBlock.Text = item.Description;
            PricePerUnit.Text = item.Cost!.Value.ToString("F");
            Counts.IsEnabled = true;
            _orderService.RentedItem = item;
            TotalUnits.Text =
                item.GetAvailable(_orderService.StartTime, _orderService.EndTime)
                    .ToString();
            _orderService.Number = 1;
            Counts.Text = "1";
            return;
        }

        Counts.IsEnabled = false;
        Counts.Text = "";
        _orderService.RentedItem = item;
        _orderService.Number = 0;
        DescriptionBlock.Text = string.Empty;
        PricePerUnit.Text = string.Empty;
        TotalUnits.Text = string.Empty;
        PriceTotal.Text = string.Empty;
    }

    /// <summary>
    ///     Преобразование введенного количества в число, с проверкой на максимальное значение
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Counts_OnTextInput(object sender, TextChangedEventArgs e)
    {
        var countTextBox = sender as TextBox;
        var index = countTextBox!.CaretIndex;
        var text = countTextBox.Text.Where(c => numbers.Contains(c)).Aggregate("", (current, c) => current + c);
        if (text.IsNullOrEmpty()) text = "1";
        if (int.Parse(text) > int.Parse(TotalUnits.Text))
            text = TotalUnits.Text;
        else if (int.Parse(text) < 1) text = "1";

        countTextBox.Text = int.Parse(text).ToString();
        countTextBox.CaretIndex = index;
        _orderService.Number = int.Parse(text);
        PriceTotal.Text = (Convert.ToInt32(text) * Convert.ToDecimal(PricePerUnit.Text)).ToString("F");
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        _window.Remove(_orderService);
    }
}