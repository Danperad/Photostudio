using System;
using System.Windows;
using System.Windows.Controls;
using PhotostudioDLL.Entity;


namespace PhotostudioGUI.Pages;

/// <summary>
/// Логика взаимодействия для ServicePage.xaml
/// </summary>
public partial class ServicePage : Page
{
    public ServicePage()
    {
        InitializeComponent();
        ServiceData.ItemsSource = Service.Get();
    }

    private void AddServiceClick(object sender, RoutedEventArgs e)
    {
        Service service = new Service
        {
            Title = TitleBox.Text,
            Description = DescriptionBox.Text,
            Price = Convert.ToDecimal(PriceBox.Text)
        };
        Service.Add(service);
        ServiceData.ItemsSource = Service.Get();
    }
}