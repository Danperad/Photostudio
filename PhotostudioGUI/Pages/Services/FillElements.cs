using MaterialDesignThemes.Wpf;

namespace PhotostudioGUI.Pages.Services;

/// <summary>
///     Класс для заполнения данными страниц с услугами заявки
/// </summary>
public static class FillElements
{
    /// <summary>
    ///     Заполнение даты и времени существующими значениями
    /// </summary>
    /// <param name="service"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    private static void FillDateTime(OrderService service, DatePicker startDate, DatePicker endDate,
        TimePicker startTime, TimePicker endTime)
    {
        if (service.StartTime is not null)
        {
            startDate.SelectedDate = DateTime.Parse(service.StartTime!.Value.ToString("d"));
            startTime.SelectedTime = DateTime.MinValue + service.StartTime!.Value.TimeOfDay;
        }

        if (service.EndTime is null) return;
        endDate.SelectedDate = DateTime.Parse(service.EndTime!.Value.ToString("d"));
        endTime.SelectedTime = DateTime.MinValue + service.EndTime!.Value.TimeOfDay;
    }

    public static void FillElement(OrderService service, HallRentPage servicePage)
    {
        FillDateTime(service, servicePage.StartDatePicker, servicePage.EndDatePicker, servicePage.StartTimePicker,
            servicePage.EndTimePicker);
        if (service.Hall is not null) servicePage.HallComboBox.SelectedItem = service.Hall;
    }

    public static void FillElement(OrderService service, ItemRentPage servicePage)
    {
        FillDateTime(service, servicePage.StartDatePicker, servicePage.EndDatePicker, servicePage.StartTimePicker,
            servicePage.EndTimePicker);

        if (service.RentedItem is not null) servicePage.ItemComboBox.SelectedItem = service.RentedItem;
        if (service.Number is not null) servicePage.Counts.Text = service.Number.ToString();
    }

    public static void FillElement(OrderService service, PhotoVideoPage servicePage)
    {
        FillDateTime(service, servicePage.StartDatePicker, servicePage.EndDatePicker, servicePage.StartTimePicker,
            servicePage.EndTimePicker);
        if (service.PhotoLocation is not null) servicePage.LocationTextBox.Text = service.PhotoLocation;
    }

    public static void FillElement(OrderService service, StylePage servicePage)
    {
        FillDateTime(service, servicePage.StartDatePicker, servicePage.EndDatePicker, servicePage.StartTimePicker,
            servicePage.EndTimePicker);
    }

    /// <summary>
    ///     Проверка на корректность введённых данных
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static bool CheckDateTime(DatePicker startDate, DatePicker endDate,
        TimePicker startTime, TimePicker endTime)
    {
        if (!(startDate.SelectedDate.HasValue && startTime.SelectedTime.HasValue &&
              endDate.SelectedDate.HasValue && endTime.SelectedTime.HasValue)) return false;
        if (startDate.SelectedDate > endDate.SelectedDate)
        {
            endDate.SelectedDate = null;
            return false;
        }

        if (startDate.SelectedDate != endDate.SelectedDate || (endTime.SelectedTime - startTime.SelectedTime).Value >=
            new TimeOnly(1, 0).ToTimeSpan()) return true;

        endTime.SelectedTime = null;
        return false;
    }
}