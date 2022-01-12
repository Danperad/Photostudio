using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Castle.Core.Internal;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class ItemRentPage
{
    private readonly ExecuteableService _executeableService;
    private readonly ProvidedServiceWindow _window;
    private readonly char[] numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

    public ItemRentPage(ExecuteableService executeableService, ProvidedServiceWindow window)
    {
        _executeableService = executeableService;
        _window = window;
        InitializeComponent();
        FillElements();
    }

    private void FillElements()
    {
        if (_executeableService.RentDate is not null)
            DatePicker.SelectedDate = DateTime.Parse(_executeableService.RentDate!.Value.ToString());

        if (_executeableService.StartRent is not null)
            StartTimePicker.SelectedTime = DateTime.MinValue + _executeableService.StartRent.Value.ToTimeSpan();

        if (_executeableService.EndRent is not null)
            EndTimePicker.SelectedTime = DateTime.MinValue + _executeableService.EndRent.Value.ToTimeSpan();

        if (_executeableService.RentedItem is not null) ItemComboBox.SelectedItem = _executeableService.RentedItem;

        if (_executeableService.Number is not null) Counts.Text = _executeableService.Number.ToString();
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
        if (DatePicker.SelectedDate.HasValue)
            _executeableService.RentDate = DateOnly.FromDateTime(DatePicker.SelectedDate.Value);

        if (StartTimePicker.SelectedTime.HasValue)
            _executeableService.StartRent = TimeOnly.FromDateTime(StartTimePicker.SelectedTime.Value);

        if (EndTimePicker.SelectedTime.HasValue)
        {
            if (StartTimePicker.SelectedTime.HasValue &&
                EndTimePicker.SelectedTime.Value < StartTimePicker.SelectedTime.Value)
                return;
            _executeableService.EndRent = TimeOnly.FromDateTime(EndTimePicker.SelectedTime.Value);
        }

        if (_executeableService.StartRent is null) return;
        if (_executeableService.EndRent is null) return;
        ItemComboBox.IsEnabled = true;
        ItemComboBox.ItemsSource = _executeableService.Service.ID switch
        {
            5 => RentedItem.GetСlothes(_executeableService.RentDate!.Value, _executeableService.StartRent!.Value, _executeableService.EndRent!.Value)
                .Where(r => !r.IsKids)
                .ToList(),
            6 => RentedItem.GetNoDress(_executeableService.RentDate!.Value, _executeableService.StartRent!.Value, _executeableService.EndRent!.Value),
            10 => RentedItem.GetKidsСlothes(_executeableService.RentDate!.Value, _executeableService.StartRent!.Value,
                _executeableService.EndRent!.Value),
            _ => ItemComboBox.ItemsSource
        };
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = (sender as ComboBox)!.SelectedItem as RentedItem;
        if (item is not null)
        {
            DescriptionBlock.Text = item.Description;
            PricePerUnit.Text = item.Cost.ToString("F");
            Counts.IsEnabled = true;
            _executeableService.RentedItem = item;
            TotalUnits.Text = (item.Number - RentedItem.GetByID(item.ID)!.Services.Where(s =>
                    s.StartRent <= _executeableService.StartRent ||
                    s.EndRent >= _executeableService.EndRent ||
                    s.StartRent >= _executeableService.StartRent && s.EndRent <= _executeableService.EndRent).Sum(s => s.Number)!.Value)
                .ToString();
            Counts.Text = "1";
            return;
        }

        Counts.IsEnabled = false;
        Counts.Text = "";
        _executeableService.RentedItem = item;
        _executeableService.Number = null;
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
        if (int.Parse(text) > int.Parse(TotalUnits.Text))
            text = TotalUnits.Text;
        else if (int.Parse(text) < 1) text = "1";

        countTextBox.Text = text;
        countTextBox.CaretIndex = index;
        PriceTotal.Text = (Convert.ToInt32(text) * Convert.ToDecimal(PricePerUnit.Text)).ToString("F");
    }
}