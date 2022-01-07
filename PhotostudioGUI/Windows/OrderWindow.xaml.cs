using System;
using System.Windows;
using PhotostudioDLL.Entity;

namespace PhotostudioGUI.Windows;

public partial class OrderWindow : Window
{
    private readonly Order _order;
    public OrderWindow(Order order)
    {
        _order = order;
        InitializeComponent();
    }

    private void OrderWindow_OnInitialized(object? sender, EventArgs e)
    {
        this.Title = $"{_order.Client.FullName} {_order.DateTime.ToString("d")}";
    }
}