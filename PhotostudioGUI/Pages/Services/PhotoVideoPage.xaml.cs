using System;
using System.Windows;
using System.Windows.Controls;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class PhotoVideoPage
{
    private readonly ServiceProvided _service;
    private readonly ProvidedServiceWindow _window;

    public PhotoVideoPage(ServiceProvided service, ProvidedServiceWindow window)
    {
        _service = service;
        _window = window;
        InitializeComponent();
    }

    private void FillElements()
    {
        if (_service.PhotoStartDateTime is not null)
        {
            StartDatePicker.SelectedDate = DateTime.Parse(_service.PhotoStartDateTime!.Value.ToString("d"));
            StartTimePicker.SelectedTime = DateTime.MinValue + _service.PhotoStartDateTime!.Value.TimeOfDay;
        }

        if (_service.PhotoEndDateTime is not null)
        {
            EndDatePicker.SelectedDate = DateTime.Parse(_service.PhotoEndDateTime!.Value.ToString("d"));
            EndTimePicker.SelectedTime = DateTime.MinValue + _service.PhotoEndDateTime!.Value.TimeOfDay;
        }

        if (_service.PhotoLocation is not null) LocationTextBox.Text = _service.PhotoLocation;

        if (_service.Employee != null) EmployeeComboBox.SelectedValue = _service.Employee;
        

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
            _service.PhotoStartDateTime = StartDatePicker.SelectedDate.Value + StartTimePicker.SelectedTime.Value.TimeOfDay;

        if (EndDatePicker.SelectedDate is not null && EndTimePicker.SelectedTime is not null)
            _service.PhotoEndDateTime = EndDatePicker.SelectedDate.Value + EndTimePicker.SelectedTime.Value.TimeOfDay;
        if (!_service.PhotoStartDateTime.HasValue) return;
        if (!_service.PhotoEndDateTime.HasValue) return;
        switch (_service.Service.ID)
        {
            case 1:
            case 9:
            case 11:
                EmployeeComboBox.ItemsSource =
                    Employee.GetPhotoWithTime(_service.PhotoStartDateTime!.Value, _service.PhotoEndDateTime!.Value);
                break;
            default:
                EmployeeComboBox.ItemsSource =
                    Employee.GetVideoWithTime(_service.PhotoStartDateTime!.Value, _service.PhotoEndDateTime!.Value);
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
            _service.Employee = employee;
        }
    }
}