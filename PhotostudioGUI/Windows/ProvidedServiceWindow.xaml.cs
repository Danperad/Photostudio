using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Pages.Services;

namespace PhotostudioGUI.Windows;

public partial class ProvidedServiceWindow
{
    private readonly List<Service> _allServices;
    private readonly List<ServiceProvided> _services;
    internal DateTime StartDate { get; private set; }
    internal DateTime EndDate { get; private set; }
    internal Employee Employee { get; private set; }

    public ProvidedServiceWindow(List<ServiceProvided> services, DateTime startDate, DateTime endDate,
        Employee employee)
    {
        StartDate = startDate;
        EndDate = endDate;
        Employee = employee;
        _services = services;
        _allServices = Service.Get();
        InitializeComponent();
    }

    private void OpenServiceProvided(object sender, MouseButtonEventArgs e)
    {
        var service = ((ListViewItem)sender).Tag as ServiceProvided;
        Navigate(service!);
    }

    private void Navigate(ServiceProvided service)
    {
        int[] s = { 1, 2, 9, 11, 12 };
        if (s.Contains(service.Service.ID))
        {
            ServiceFrame.Navigate(new PhotoVideoPage(service, this));
            return;
        }

        s = new[] { 5, 6, 10 };
        if (s.Contains(service.Service.ID))
        {
            service.Employee = Employee;
            ServiceFrame.Navigate(new ItemRentPage(service, this));
            return;
        }

        s = new[] { 7 };
        if (s.Contains(service.Service.ID))
        {
            service.Employee = Employee;
            ServiceFrame.Navigate(new HallRentPage(service, this));
        }
        else
        {
            ServiceFrame.Navigate(new VoidPage(service, this));
        }
    }

    internal void Remove(ServiceProvided serviceProvided)
    {
        ServiceFrame.Navigate(new Page());
        _services.Remove(serviceProvided);
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
        var sProvided = new ServiceProvided { Service = service! };
        _services.Add(sProvided);
        ProvidedListView.Items.Add(sProvided);
        Navigate(sProvided);
        Height += 20;
    }

    private void ServiceComboBoxInit(object? sender, EventArgs e)
    {
        (sender as ComboBox)!.ItemsSource = Service.Get();
    }

    private void ServiceComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        (sender as ComboBox)!.SelectedIndex = -1;
    }
}