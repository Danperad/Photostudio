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
        var titles = new List<string>(new[] { "Сотрудники", "Должности", "Услуги", "Заявки" });
        pages = new Dictionary<EPages, Page>();
        pages.Add(EPages.EMPLOYEE, new EmployeePage(window.mainFrame));
        pages.Add(EPages.ROLE, new RolePage());
        pages.Add(EPages.SERVICE, new ServicePage());
        pages.Add(EPages.ORDER, new OrderPage(window.Employee));
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
        var titles = new List<string>(new[] { "Сотрудники", "Должности", "Услуги" });
        pages = new Dictionary<EPages, Page>();
        pages.Add(EPages.EMPLOYEE, new EmployeePage(window.mainFrame));
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

    public static void ManagerBuild(MainWindow window, out Dictionary<EPages, Page> pages)
    {
        var titles = new List<string>(new[] { "Клиенты", "Услуги", "Заявки" });
        pages = new Dictionary<EPages, Page>();
        pages.Add(EPages.CLIENT, new ClientPage(window.mainFrame));
        pages.Add(EPages.SERVICE, new ServicePage());
        pages.Add(EPages.ORDER, new OrderPage(window.Employee));
        foreach (var title in titles)
        {
            PackIconKind icon;
            switch (title)
            {
                case "Клиенты":
                    icon = PackIconKind.PersonCardDetails;
                    window.mainListView.Items.Add(AddItem(title, window.mainFrame, icon, pages[EPages.CLIENT]));
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
        var listViewItem = new ListViewItem();
        listViewItem.PreviewMouseLeftButtonDown += (sender, args) => { frame.Navigate(page); };
        var tempIcon = new PackIcon { Margin = new Thickness(0, 2, 7, 0), Kind = icon };
        var tempStack = new StackPanel { Orientation = Orientation.Horizontal };
        var textBlock = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Text = title };
        tempStack.Children.Add(tempIcon);
        tempStack.Children.Add(textBlock);
        listViewItem.Content = tempStack;
        return listViewItem;
    }
}