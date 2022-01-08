using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhotostudioDLL.Entities;

namespace PhotostudioGUI.Windows;

public partial class ProvidedServiceWindow
{
    private readonly List<ServiceProvided> _services;
    public ProvidedServiceWindow(List<ServiceProvided> services)
    {
        _services = services;
        InitializeComponent();
    }

    private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var service = sender as ServiceProvided;
    }

    private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
    {
        _services.Remove(sender as ServiceProvided);
        
    }

    private void FrameworkElement_OnInitialized(object? sender, EventArgs e)
    {
        (sender as ListView)!.ItemsSource = _services;
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        
    }
}