using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using PhotostudioGUI.Pages;
using PhotostudioGUI6.Pages;

namespace PhotostudioGUI;

public static class MainWindowBuilder
{
    public static void AdminBuild(MainWindow window, out Page Employee, out Page Role, out Page Service)
    {
        List<string> titles = new List<string>(new[] {"Сотрудники", "Должности", "Услуги"});
        Employee = new EmployeePage();
        Role = new RolePage();
        Service = new ServicePage();
        foreach (var title in titles)
        {
            PackIconKind icon;
            switch (title)
            {
                case "Сотрудники":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(App.AddItem(title, window.mainFrame, icon, Employee));
                    break;
                case "Должности":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(App.AddItem(title, window.mainFrame, icon, Role));
                    break;
                case "Услуги":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(App.AddItem(title, window.mainFrame, icon, Service));
                    break;
            }
        }
    }
}