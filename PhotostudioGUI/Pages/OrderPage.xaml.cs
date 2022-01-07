using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using PhotostudioDLL;
using PhotostudioDLL.Entity;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages;

public partial class OrderPage : Page
{
    private List<Order> _orders = Order.Get();

    public OrderPage()
    {
        InitializeComponent();
    }

    private void OrderData_OnInitialized(object? sender, EventArgs e)
    {
        (sender as ListView).ItemsSource = _orders;
    }

    private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var order = ((ListViewItem) sender).Content as Order;
        var window = new OrderWindow(order);
        window.Show();
    }
}