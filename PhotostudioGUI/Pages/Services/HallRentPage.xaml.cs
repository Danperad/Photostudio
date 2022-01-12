using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class HallRentPage
{
    private readonly ExecuteableService _executeableService;
    private readonly ProvidedServiceWindow _window;

    public HallRentPage(ExecuteableService executeableService, ProvidedServiceWindow window)
    {
        _executeableService = executeableService;
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
            PriceTotal.Text = (hall.Cost * (decimal)(_executeableService.EndRent!.Value - _executeableService.StartRent!.Value).TotalHours)
                .ToString("F");
            _executeableService.Hall = hall;
            return;
        }

        _executeableService.Hall = hall;
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
        if (_executeableService.RentDate is not null)
            DatePicker.SelectedDate = DateTime.Parse(_executeableService.RentDate!.Value.ToString());

        if (_executeableService.StartRent is not null)
            StartTimePicker.SelectedTime = DateTime.MinValue + _executeableService.StartRent.Value.ToTimeSpan();

        if (_executeableService.EndRent is not null)
            EndTimePicker.SelectedTime = DateTime.MinValue + _executeableService.EndRent.Value.ToTimeSpan();

        if (_executeableService.Hall is not null) HallComboBox.SelectedItem = _executeableService.Hall;
        CheckEndFill();
    }

    private void CheckEndFill()
    {
        if (DatePicker.SelectedDate is not null)
            _executeableService.RentDate = DateOnly.FromDateTime(DatePicker.SelectedDate.Value);

        if (StartTimePicker.SelectedTime is not null)
            _executeableService.StartRent = TimeOnly.FromDateTime(StartTimePicker.SelectedTime.Value);

        if (EndTimePicker.SelectedTime is not null)
            _executeableService.EndRent = TimeOnly.FromDateTime(EndTimePicker.SelectedTime.Value);

        HallComboBox.SelectedIndex = -1;
        if (_executeableService.StartRent is null) return;
        if (_executeableService.EndRent is null) return;
        HallComboBox.IsEnabled = true;
        HallComboBox.ItemsSource = Hall.Get().Where(h =>
            !h.Services.Any(s =>
                s.StartRent <= _executeableService.StartRent ||
                s.EndRent >= _executeableService.EndRent ||
                s.StartRent >= _executeableService.StartRent && s.EndRent <= _executeableService.EndRent
            )).ToList();
    }
}