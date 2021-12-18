using System;
using System.Windows;
using System.Windows.Controls;
using PhotostudioDLL;
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
        ServiceData.ItemsSource = ContextDB.GetServices();
    }

    private void AddServiceClick(object sender, RoutedEventArgs e)
    {
        Service service = new Service
        {
            Title = TitleBox.Text,
            Description = DescriptionBox.Text,
            Price = Convert.ToDecimal(PriceBox.Text)
        };
        ContextDB.Add(service);
        ServiceData.ItemsSource = ContextDB.GetServices();
    }
}