using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class HallRentPage
{
    private readonly ServiceProvided _service;
    private readonly ProvidedServiceWindow _window;

    public HallRentPage(ServiceProvided service, ProvidedServiceWindow window)
    {
        _service = service;
        _window = window;
        InitializeComponent();
        FillElements();
    }

    private void DatePicker_OnInitialized(object? sender, EventArgs e)
    {
        (sender as DatePicker)!.DisplayDateStart = _window.StartDate;
        (sender as DatePicker)!.DisplayDateEnd = _window.EndDate;
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var hall = (sender as ComboBox)!.SelectedItem as Hall;
        if (hall is not null)
        {
            DescriptionBlock.Text = hall.Description;
            PricePerHour.Text = hall.Cost.ToString("F");
            PriceTotal.Text = (hall.Cost * (decimal)(_service.EndRent!.Value - _service.StartRent!.Value).TotalHours)
                .ToString("F");
            _service.Hall = hall;
            return;
        }

        _service.Hall = hall;
        DescriptionBlock.Text = string.Empty;
        PricePerHour.Text = string.Empty;
        PriceTotal.Text = string.Empty;
    }

    private void TimePicker_OnSelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
    {
        CheckEndFill();
    }

    private void DatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        CheckEndFill();
    }

    private void FillElements()
    {
        if (_service.RentDate is not null)
            DatePicker.SelectedDate = DateTime.Parse(_service.RentDate!.Value.ToString());

        if (_service.StartRent is not null)
            StartTimePicker.SelectedTime = DateTime.MinValue + _service.StartRent.Value.ToTimeSpan();

        if (_service.EndRent is not null)
            EndTimePicker.SelectedTime = DateTime.MinValue + _service.EndRent.Value.ToTimeSpan();

        if (_service.Hall is not null) HallComboBox.SelectedItem = _service.Hall;
        CheckEndFill();
    }

    private void CheckEndFill()
    {
        if (DatePicker.SelectedDate is not null)
            _service.RentDate = DateOnly.FromDateTime(DatePicker.SelectedDate.Value);

        if (StartTimePicker.SelectedTime is not null)
            _service.StartRent = TimeOnly.FromDateTime(StartTimePicker.SelectedTime.Value);

        if (EndTimePicker.SelectedTime is not null)
            _service.EndRent = TimeOnly.FromDateTime(EndTimePicker.SelectedTime.Value);

        HallComboBox.SelectedIndex = -1;
        if (_service.StartRent is null) return;
        if (_service.EndRent is null) return;
        HallComboBox.IsEnabled = true;
        HallComboBox.ItemsSource = Hall.Get().Where(h =>
            !h.Services.Any(s =>
                s.StartRent <= _service.StartRent ||
                s.EndRent >= _service.EndRent ||
                s.StartRent >= _service.StartRent && s.EndRent <= _service.EndRent
            )).ToList();
    }
}