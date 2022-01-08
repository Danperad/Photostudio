using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages;

public partial class OrderPage
{
    private List<Order> _orders;
    private readonly Employee _employee;
    private readonly Client _client;
    private readonly List<ServiceProvided> _serviceProvideds;

    public OrderPage(Employee employee)
    {
        _employee = employee;
        _orders = Order.Get();
        InitializeComponent();
        (MainOrderGrid.Parent as Grid)?.ColumnDefinitions.RemoveAt(1);
        AddPanel.Children.Clear();
    }

    public OrderPage(Employee employee, Client client)
    {
        _client = client;
        _employee = employee;
        _serviceProvideds = new List<ServiceProvided>();
        _orders = Order.Get().Where(o => o.Client == client).ToList();
        InitializeComponent();
    }

    private void OrderData_OnInitialized(object? sender, EventArgs e)
    {
        (sender as ListView)!.ItemsSource = _orders;
    }

    private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var order = ((ListViewItem)sender).Content as Order;
    }

    private void AddOrderClick(object sender, RoutedEventArgs e)
    {
        if (_serviceProvideds.Count == 0)
        {
            return;
        }
        var order = new Order(new Contract(_client, _employee, DateOnly.FromDateTime(StartDatePicker.SelectedDate.Value),
            DateOnly.FromDateTime(EndDatePicker.SelectedDate.Value)), _client, DateTime.Now, _serviceProvideds);
        Order.Add(order);
        _orders = Order.Get();
    }

    private void FrameworkElement_OnInitialized(object? sender, EventArgs e)
    {
        (sender as ComboBox)!.ItemsSource = Employee.Get();
    }

    private void DatePicker_OnInitialized(object? sender, EventArgs e)
    {
        (sender as DatePicker)!.DisplayDateStart = DateTime.Today;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new ProvidedServiceWindow(_serviceProvideds);
        window.Show();
    }
}