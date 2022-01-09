using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Pages;

namespace PhotostudioGUI.Windows;

public partial class MainWindow : Window
{
    private Dictionary<EPages, Page> _pages;

    public MainWindow(Employee employee)
    {
        Employee = employee;
        InitializeComponent();
    }

    internal Employee Employee { get; }

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
        userName.Text = Employee.FullName;
        switch (Employee.RoleID)
        {
            case 1:
                MainWindowBuilder.AdminBuild(this, out _pages);
                mainFrame.Navigate(_pages[EPages.EMPLOYEE]);
                break;
            case 2:
                break;
            case 7:
                MainWindowBuilder.ManagerBuild(this, out _pages);
                mainFrame.Navigate(_pages[EPages.CLIENT]);
                break;
        }
    }

    private void MainWindow_OnClosed(object? sender, EventArgs e)
    {
        Application.Current.Shutdown();
    }
}