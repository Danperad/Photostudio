using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Pages;
using PhotostudioGUI.Pages.Services;

namespace PhotostudioGUI.Windows;

public partial class ProvidedServiceWindow
{
    private readonly List<Service> _allServices;
    private readonly List<ExecuteableService> _services;
    private event OrderPage.ServicesWindowHandler _closeEvent;

    public ProvidedServiceWindow(List<ExecuteableService> services, DateTime startDate, DateTime endDate,
        Employee employee, OrderPage.ServicesWindowHandler closeEvent)
    {
        StartDate = startDate;
        EndDate = endDate;
        Employee = employee;
        _services = services;
        _closeEvent = closeEvent;
        _allServices = Service.Get();
        InitializeComponent();
    }

    internal DateTime StartDate { get; }
    internal DateTime EndDate { get; }
    internal Employee Employee { get; }

    private void OpenExecuteableService(object sender, MouseButtonEventArgs e)
    {
        var service = ((ListViewItem)sender).Tag as ExecuteableService;
        Navigate(service!);
    }

    private void Navigate(ExecuteableService executeableService)
    {
        switch (executeableService.Service.ID)
        {
            case 1:
            case 2:
            case 9:
            case 11:
            case 12:
                ServiceFrame.Navigate(new PhotoVideoPage(executeableService, this));
                break;
            case 5:
            case 6:
            case 10:
                executeableService.Employee = Employee;
                ServiceFrame.Navigate(new ItemRentPage(executeableService, this));
                break;
            case 7:
                executeableService.Employee = Employee;
                ServiceFrame.Navigate(new HallRentPage(executeableService, this));
                break;
            default:
                ServiceFrame.Navigate(new VoidPage(executeableService, this));
                break;
        }
    }

    internal void Remove(ExecuteableService executeableService)
    {
        ServiceFrame.Navigate(new Page());
        _services.Remove(executeableService);
        AddItems();
    }

    private void FrameworkElement_OnInitialized(object? sender, EventArgs e)
    {
        AddItems();
    }

    private void AddItems()
    {
        ProvidedListView.Items.Clear();
        foreach (var service in _services) ProvidedListView.Items.Add(service);
    }

    private void EventSetter_OnHandler(object sender, RoutedEventArgs e)
    {
        var service = (sender as ComboBoxItem)!.Tag as Service;
        var sProvided = new ExecuteableService { Service = service! };
        _services.Add(sProvided);
        ProvidedListView.Items.Add(sProvided);
        Navigate(sProvided);
    }

    private void ServiceComboBoxInit(object? sender, EventArgs e)
    {
        (sender as ComboBox)!.ItemsSource = Service.Get();
    }

    private void ServiceComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        (sender as ComboBox)!.SelectedIndex = -1;
    }

    private void CompleteButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _closeEvent.Invoke();
    }
}