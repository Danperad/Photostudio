using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhotostudioDLL.Entity;
using PhotostudioGUI.Pages;

namespace PhotostudioGUI.Windows;

public partial class MainWindow : Window
{
    private Employee _employee;
    private Dictionary<EPages, Page> _pages;

    public MainWindow(Employee employee)
    {
        _employee = employee;
        InitializeComponent();
    }

    public void NavigateWindow(EPages page)
    {
        mainFrame.Navigate(_pages[page]);
    }
    
    public void NavigateWindow(Page page)
    {
        mainFrame.Navigate(page);
    }

    public void BackWindow()
    {
        mainFrame.GoBack();
    }

    private void MainWindow_OnInitialized(object? sender, EventArgs e)
    {
        userName.Text = _employee.FullName;
        switch (_employee.RoleID)
        {
            case 1:
                MainWindowBuilder.AdminBuild(this, out _pages);
                mainFrame.Navigate(_pages[EPages.EMPLOYEE]);
                break;
            case 2:
                break;
        }
    }
}