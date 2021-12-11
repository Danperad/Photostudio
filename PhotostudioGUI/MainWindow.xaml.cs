using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhotostudioGUI.Pages;

namespace PhotostudioGUI;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Page Client = new ClientPage();
    private readonly Page Employee = new EmployeePage();
    private readonly Page Order = new OrderPage();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void videoBox_Click(object sender, MouseButtonEventArgs e)
    {
        mainFrame.Navigate(Client);
    }

    private void audioBox_Click(object sender, MouseButtonEventArgs e)
    {
        mainFrame.Navigate(Employee);
    }

    private void orderBox_Click(object sender, MouseButtonEventArgs e)
    {
        mainFrame.Navigate(Order);
    }

    private void HideBar(object sender, MouseButtonEventArgs e)
    {
    }
}