using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Castle.Core.Internal;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class ItemRentPage
{
    private readonly ServiceProvided _service;
    private readonly ProvidedServiceWindow _window;
    private readonly char[] numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

    public ItemRentPage(ServiceProvided service, ProvidedServiceWindow window)
    {
        _service = service;
        _window = window;
        InitializeComponent();
        FillElements();
    }

    private void FillElements()
    {
        if (_service.RentDate is not null)
        {
            DatePicker.SelectedDate = DateTime.Parse(_service.RentDate!.Value.ToString());
        }

        if (_service.StartRent is not null)
        {
            StartTimePicker.SelectedTime = DateTime.MinValue + _service.StartRent.Value.ToTimeSpan();
        }

        if (_service.EndRent is not null)
        {
            EndTimePicker.SelectedTime = DateTime.MinValue + _service.EndRent.Value.ToTimeSpan();
        }

        if (_service.RentedItem is not null)
        {
            ItemComboBox.SelectedItem = _service.RentedItem;
        }

        if (_service.Number is not null)
        {
            Counts.Text = _service.Number.ToString();
        }
        CheckEndFill();
    }

    private void DatePicker_OnInitialized(object? sender, EventArgs e)
    {
        var datePicker = sender as DatePicker;
        datePicker!.DisplayDateStart = _window.StartDate;
        datePicker.DisplayDateEnd = _window.EndDate;
    }

    private void DatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        CheckEndFill();
    }

    private void TimePicker_OnSelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
    {
        CheckEndFill();
    }

    private void CheckEndFill()
    {
        if (DatePicker.SelectedDate is not null)
        {
            _service.RentDate = DateOnly.FromDateTime(DatePicker.SelectedDate.Value);
        }

        if (StartTimePicker.SelectedTime is not null)
        {
            _service.StartRent = TimeOnly.FromDateTime(StartTimePicker.SelectedTime.Value);
        }

        if (EndTimePicker.SelectedTime is not null)
        {
            _service.EndRent = TimeOnly.FromDateTime(EndTimePicker.SelectedTime.Value);
        }
        if (_service.StartRent is null) return;
        if (_service.EndRent is null) return;
        ItemComboBox.IsEnabled = true;
        ItemComboBox.ItemsSource = RentedItem.Get().Where(h =>
                h.Number - h.Services.Where(s =>
                    (s.StartRent <= _service.StartRent) ||
                    (s.EndRent >= _service.EndRent) ||
                    (s.StartRent >= _service.StartRent && s.EndRent <= _service.EndRent)).Sum(s => s.Number)!.Value > 0)
            .ToList();
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = (sender as ComboBox)!.SelectedItem as RentedItem;
        if (item is not null)
        {
            DescriptionBlock.Text = item.Description;
            PricePerUnit.Text = item.Cost.ToString("F");
            Counts.IsEnabled = true;
            _service.RentedItem = item;
            TotalUnits.Text = (item.Number - RentedItem.GetByID(item.ID)!.Services.Where(s =>
                    (s.StartRent <= _service.StartRent) ||
                    (s.EndRent >= _service.EndRent) ||
                    (s.StartRent >= _service.StartRent && s.EndRent <= _service.EndRent)).Sum(s => s.Number)!.Value)
                .ToString();
            Counts.Text = "1";
            return;
        }

        Counts.IsEnabled = false;
        Counts.Text = "";
        _service.RentedItem = item;
        _service.Number = null;
        DescriptionBlock.Text = string.Empty;
        PricePerUnit.Text = string.Empty;
        TotalUnits.Text = string.Empty;
        PriceTotal.Text = string.Empty;
    }

    private void Counts_OnTextInput(object sender, TextChangedEventArgs e)
    {
        var countTextBox = sender as TextBox;
        var index = countTextBox!.CaretIndex;
        var text = countTextBox.Text.Where(c => numbers.Contains(c)).Aggregate("", (current, c) => current + c);
        if (text.IsNullOrEmpty()) text = "1";
        if (Int32.Parse(text) > int.Parse(TotalUnits.Text))
        {
            text = TotalUnits.Text;
        }
        else if (int.Parse(text) < 1)
        {
            text = "1";
        }

        countTextBox.Text = text;
        countTextBox.CaretIndex = index;
        PriceTotal.Text = (Convert.ToInt32(text) * Convert.ToDecimal(PricePerUnit.Text)).ToString("F");
    }
}