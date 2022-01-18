using Castle.Core.Internal;
using MaterialDesignThemes.Wpf;
using PhotostudioDLL.Entities.Interfaces;
using PhotostudioDLL.Entities.Services;

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
    private static void FillDateTime(ITimedService service, DatePicker startDate, DatePicker endDate,
        TimePicker startTime, TimePicker endTime)
    {
        startDate.SelectedDate = DateTime.Parse(service.StartTime.ToString("d"));
        startTime.SelectedTime = DateTime.MinValue + service.StartTime.TimeOfDay;
        endDate.SelectedDate = DateTime.Parse(service.EndTime.ToString("d"));
        endTime.SelectedTime = DateTime.MinValue + service.EndTime.TimeOfDay;
    }

    public static void FillElement(HallRentService service, HallRentPage servicePage)
    {
        FillDateTime(service, servicePage.StartDatePicker, servicePage.EndDatePicker, servicePage.StartTimePicker,
            servicePage.EndTimePicker);
        if (service.Hall is not null) servicePage.HallComboBox.SelectedItem = service.Hall;
    }

    public static void FillElement(RentService service, ItemRentPage servicePage)
    {
        FillDateTime(service, servicePage.StartDatePicker, servicePage.EndDatePicker, servicePage.StartTimePicker,
            servicePage.EndTimePicker);

        if (service.RentedItem is not null) servicePage.ItemComboBox.SelectedItem = service.RentedItem;
        if (service.Number != 0) servicePage.Counts.Text = service.Number.ToString();
    }

    public static void FillElement(PhotoVideoService service, PhotoVideoPage servicePage)
    {
        FillDateTime(service, servicePage.StartDatePicker, servicePage.EndDatePicker, servicePage.StartTimePicker,
            servicePage.EndTimePicker);
        if (!service.PhotoLocation.IsNullOrEmpty()) servicePage.LocationTextBox.Text = service.PhotoLocation;
    }

    public static void FillElement(StyleService service, StylePage servicePage)
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