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
    private readonly Client? _client;
    private readonly Employee _employee;
    private Order _order;
    private List<Order> _orders;
    public delegate void ServicesWindowHandler();
    private event ServicesWindowHandler? ServicesWindow;

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
        _order = new Order();
        _orders = Order.Get().Where(o => o.Client == client).ToList();
        InitializeComponent();
        ServicesWindow += DisplayServices;
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
        _order.Contract = new Contract(_client!, _employee,
            DateOnly.FromDateTime(StartDatePicker.SelectedDate!.Value),
            DateOnly.FromDateTime(EndDatePicker.SelectedDate!.Value));
        _order.Client = _client!;
        _order.DateTime = DateTime.Now;
        if (_order.Services.Count == 0) return;
        Order.Add(_order);
        _orders = Order.Get();
    }

    private void DatePicker_OnInitialized(object? sender, EventArgs e)
    {
        (sender as DatePicker)!.DisplayDateStart = DateTime.Today;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!StartDatePicker.SelectedDate.HasValue || !EndDatePicker.SelectedDate.HasValue ||
            StartDatePicker.SelectedDate.Value > EndDatePicker.SelectedDate.Value)
        {
            ErrorBlock.Text = "Выберите корректные даты";
            return;
        }

        var window = new ProvidedServiceWindow(_order.Services, StartDatePicker.SelectedDate.Value,
            EndDatePicker.SelectedDate.Value, _employee, ServicesWindow!);
        window.Show();
        ErrorBlock.Text = string.Empty;
    }

    private void StartDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        EndDatePicker.DisplayDateStart = StartDatePicker.SelectedDate;
    }

    private void DisplayServices()
    {
        ServicesBlock.Text = $"Предоставляеме услуги: {_order.ListServices}";
        TotalPriceBlock.Text = $"Итого: {_order.AllGetCost.ToString("F")} Р.";
    }
}