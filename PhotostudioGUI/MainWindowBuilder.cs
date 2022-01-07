using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using PhotostudioGUI.Pages;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI;

public static class MainWindowBuilder
{
    public static void AdminBuild(MainWindow window, out Dictionary<EPages, Page> pages)
    {
        List<string> titles = new List<string>(new[] {"Сотрудники", "Должности", "Услуги", "Заявки"});
        pages = new Dictionary<EPages, Page>();
        pages.Add(EPages.EMPLOYEE, new EmployeePage(window));
        pages.Add(EPages.ROLE, new RolePage());
        pages.Add(EPages.SERVICE, new ServicePage());
        pages.Add(EPages.ORDER, new OrderPage());
        foreach (var title in titles)
        {
            PackIconKind icon;
            switch (title)
            {
                case "Сотрудники":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(AddItem(title, window.mainFrame, icon, pages[EPages.EMPLOYEE]));
                    break;
                case "Должности":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(AddItem(title, window.mainFrame, icon, pages[EPages.ROLE]));
                    break;
                case "Услуги":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(AddItem(title, window.mainFrame, icon, pages[EPages.SERVICE]));
                    break;
                case "Заявки":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(AddItem(title, window.mainFrame, icon, pages[EPages.ORDER]));
                    break;
            }
        }
    }

    public static void PhotographBuild(MainWindow window, out Dictionary<EPages, Page> pages)
    {
        List<string> titles = new List<string>(new[] {"Сотрудники", "Должности", "Услуги"});
        pages = new Dictionary<EPages, Page>();
        pages.Add(EPages.EMPLOYEE, new EmployeePage(window));
        pages.Add(EPages.ROLE, new RolePage());
        pages.Add(EPages.SERVICE, new ServicePage());
        foreach (var title in titles)
        {
            PackIconKind icon;
            switch (title)
            {
                case "Сотрудники":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(AddItem(title, window.mainFrame, icon, pages[EPages.EMPLOYEE]));
                    break;
                case "Должности":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(AddItem(title, window.mainFrame, icon, pages[EPages.ROLE]));
                    break;
                case "Услуги":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(AddItem(title, window.mainFrame, icon, pages[EPages.SERVICE]));
                    break;
            }
        }
    }
    
    /// <summary>
    ///     Создание эллементов боковой панели
    /// </summary>
    /// <param name="title"></param>
    /// <param name="frame"></param>
    /// <param name="icon"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public static ListViewItem AddItem(string title, Frame frame, PackIconKind icon, Page page)
    {
        ListViewItem listViewItem = new ListViewItem();
        listViewItem.PreviewMouseLeftButtonDown += (sender, args) => { frame.Navigate(page); };
        PackIcon tempIcon = new PackIcon {Margin = new Thickness(0, 2, 7, 0), Kind = icon};
        StackPanel tempStack = new StackPanel {Orientation = Orientation.Horizontal};
        TextBlock textBlock = new TextBlock {VerticalAlignment = VerticalAlignment.Center, Text = title};
        tempStack.Children.Add(tempIcon);
        tempStack.Children.Add(textBlock);
        listViewItem.Content = tempStack;
        return listViewItem;
    }
}