using System;
using System.Windows;
using System.Windows.Controls;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class PhotoVideoPage
{
    private readonly ExecuteableService _executeableService;
    private readonly ProvidedServiceWindow _window;

    public PhotoVideoPage(ExecuteableService executeableService, ProvidedServiceWindow window)
    {
        _executeableService = executeableService;
        _window = window;
        InitializeComponent();
        FillElements();
    }

    private void FillElements()
    {
        if (_executeableService.PhotoStartDateTime is not null)
        {
            StartDatePicker.SelectedDate = DateTime.Parse(_executeableService.PhotoStartDateTime!.Value.ToString("d"));
            StartTimePicker.SelectedTime = DateTime.MinValue + _executeableService.PhotoStartDateTime!.Value.TimeOfDay;
        }

        if (_executeableService.PhotoEndDateTime is not null)
        {
            EndDatePicker.SelectedDate = DateTime.Parse(_executeableService.PhotoEndDateTime!.Value.ToString("d"));
            EndTimePicker.SelectedTime = DateTime.MinValue + _executeableService.PhotoEndDateTime!.Value.TimeOfDay;
        }

        if (_executeableService.PhotoLocation is not null) LocationTextBox.Text = _executeableService.PhotoLocation;

        if (_executeableService.Employee != null) EmployeeComboBox.SelectedValue = _executeableService.Employee;
        

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

    private void CheckEndFill()
    {
        EmployeeComboBox.IsEnabled = false;
        if (StartDatePicker.SelectedDate is not null && StartTimePicker.SelectedTime is not null)
            _executeableService.PhotoStartDateTime = StartDatePicker.SelectedDate.Value + StartTimePicker.SelectedTime.Value.TimeOfDay;

        if (EndDatePicker.SelectedDate is not null && EndTimePicker.SelectedTime is not null)
            _executeableService.PhotoEndDateTime = EndDatePicker.SelectedDate.Value + EndTimePicker.SelectedTime.Value.TimeOfDay;
        if (!_executeableService.PhotoStartDateTime.HasValue) return;
        if (!_executeableService.PhotoEndDateTime.HasValue) return;
        switch (_executeableService.Service.ID)
        {
            case 1:
            case 9:
            case 11:
                EmployeeComboBox.ItemsSource =
                    Employee.GetPhotoWithTime(_executeableService.PhotoStartDateTime!.Value, _executeableService.PhotoEndDateTime!.Value);
                break;
            default:
                EmployeeComboBox.ItemsSource =
                    Employee.GetVideoWithTime(_executeableService.PhotoStartDateTime!.Value, _executeableService.PhotoEndDateTime!.Value);
                break;
        }

        if (EmployeeComboBox.Items.Count != 0)
        {
            EmployeeComboBox.IsEnabled = true;
            MessageTextBlock.Text = "";
        }
        else MessageTextBlock.Text = "Доступных сотрудников нет";
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((sender as ComboBox)!.SelectedItem is Employee employee)
        {
            _executeableService.Employee = employee;
        }
    }
}