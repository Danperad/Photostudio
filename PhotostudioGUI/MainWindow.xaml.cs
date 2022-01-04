using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhotostudioDLL.Entity;
using PhotostudioGUI.Pages;

namespace PhotostudioGUI;

public partial class MainWindow : Window
{
    private Employee _employee;

    private Page Client;
    private Page Employee;
    private Page Order;
    private Page Service;
    private Page Role;

    public MainWindow(Employee employee)
    {
        _employee = employee;
        InitializeComponent();
        BuildWindow();
        mainFrame.Navigate(Client);
    }

    private void BuildWindow()
    {
        switch (_employee.RoleID)
        {
            case 1:
                MainWindowBuilder.AdminBuild(this,out Employee,out Role,out Service);
                break;
        }
    }
}