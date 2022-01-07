using System.Windows;
using System.Windows.Controls;
using PhotostudioDLL.Entity;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages;

public partial class CurrentEmployeePage : Page
{
    private readonly MainWindow _window;
    private Employee _employee;
    public CurrentEmployeePage(Employee employee,MainWindow window)
    {
        _employee = employee;
        _window = window;
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        _window.BackWindow();
    }
}