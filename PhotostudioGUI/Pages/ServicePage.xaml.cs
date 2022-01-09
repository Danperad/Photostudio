using System;
using System.Collections.Generic;
using System.Windows.Controls;
using PhotostudioDLL.Entities;

namespace PhotostudioGUI.Pages;

public partial class ServicePage
{
    private readonly List<Service> _services;

    public ServicePage()
    {
        _services = Service.Get();
        InitializeComponent();
    }

    private void ServiceData_OnInitialized(object? sender, EventArgs e)
    {
        (sender as ListView)!.ItemsSource = _services;
    }
}