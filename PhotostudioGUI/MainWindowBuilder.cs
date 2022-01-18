using MaterialDesignThemes.Wpf;
using PhotostudioGUI.Pages;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI;

/// <summary>
///     Класс для конфигурации главного окна, в зависимости от должности
/// </summary>
public static class MainWindowBuilder
{
    /// <summary>
    ///     Конфигурация для администратора
    /// </summary>
    /// <param name="window"></param>
    public static void AdminBuild(MainWindow window)
    {
        var titles = new List<string>(new[] {"Клиенты","Услуги", "Заявки", "Услуги заявок"});
        var mainFrame = window.MainFrame;
        foreach (var title in titles)
        {
            PackIconKind icon;
            switch (title)
            {
                case "Клиенты":
                    icon = PackIconKind.PersonCardDetails;
                    window.MainListView.Items.Add(AddItem(title, icon,
                        () => { mainFrame.Navigate(new ClientPage(mainFrame, window.Employee)); }));
                    mainFrame.Navigate(new ClientPage(mainFrame, window.Employee));
                    break;
                case "Услуги":
                    icon = PackIconKind.PersonCardDetails;
                    window.MainListView.Items.Add(AddItem(title, icon,
                        () => { mainFrame.Navigate(new ServicePage()); }));
                    break;
                case "Заявки":
                    icon = PackIconKind.PersonCardDetails;
                    window.MainListView.Items.Add(AddItem(title, icon,
                        () => { mainFrame.Navigate(new OrderPage(window.Employee)); }));
                    break;
                case "Услуги заявок":
                    icon = PackIconKind.PersonCardDetails;
                    window.MainListView.Items.Add(AddItem(title, icon,
                        () => { mainFrame.Navigate(new OrderServicePage(window.Employee)); }));
                    break;
            }
        }
    }

    /// <summary>
    ///     Конфигурация для менеджера
    /// </summary>
    /// <param name="window"></param>
    public static void ManagerBuild(MainWindow window)
    {
        var titles = new List<string>(new[] {"Клиенты", "Услуги", "Заявки", "Услуги заявок"});
        var mainFrame = window.MainFrame;
        foreach (var title in titles)
        {
            PackIconKind icon;
            switch (title)
            {
                case "Клиенты":
                    icon = PackIconKind.PersonCardDetails;
                    window.MainListView.Items.Add(AddItem(title, icon,
                        () => { mainFrame.Navigate(new ClientPage(mainFrame, window.Employee)); }));
                    mainFrame.Navigate(new ClientPage(mainFrame, window.Employee));
                    break;
                case "Услуги":
                    icon = PackIconKind.PersonCardDetails;
                    window.MainListView.Items.Add(AddItem(title, icon,
                        () => { mainFrame.Navigate(new ServicePage()); }));
                    break;
                case "Заявки":
                    icon = PackIconKind.PersonCardDetails;
                    window.MainListView.Items.Add(AddItem(title, icon,
                        () => { mainFrame.Navigate(new OrderPage(window.Employee)); }));
                    break;
                case "Услуги заявок":
                    icon = PackIconKind.PersonCardDetails;
                    window.MainListView.Items.Add(AddItem(title, icon,
                        () => { mainFrame.Navigate(new OrderServicePage(window.Employee)); }));
                    break;
            }
        }
    }

    /// <summary>
    ///     Конфигурация для сотрудников
    /// </summary>
    /// <param name="window"></param>
    public static void WorkerBuild(MainWindow window)
    {
        var mainFrame = window.MainFrame;
        var mainGrid = window.Content as Grid;
        window.TabGrid.Children.Clear();
        window.TabGrid = null;
        mainGrid!.ColumnDefinitions.Remove(mainGrid.ColumnDefinitions[0]);
        mainFrame.Navigate(new OrderServicePage(window.Employee, window.LogOut));
    }

    /// <summary>
    ///     Создание элементов боковой панели
    /// </summary>
    /// <param name="title"></param>
    /// <param name="icon"></param>
    /// <param name="openPage"></param>
    /// <returns></returns>
    private static ListViewItem AddItem(string title, PackIconKind icon, Action openPage)
    {
        var listViewItem = new ListViewItem();
        listViewItem.PreviewMouseLeftButtonDown += (sender, args) => { openPage.Invoke(); };
        var tempIcon = new PackIcon {Margin = new Thickness(0, 2, 7, 0), Kind = icon};
        var tempStack = new StackPanel {Orientation = Orientation.Horizontal};
        var textBlock = new TextBlock {VerticalAlignment = VerticalAlignment.Center, Text = title};
        tempStack.Children.Add(tempIcon);
        tempStack.Children.Add(textBlock);
        listViewItem.Content = tempStack;
        return listViewItem;
    }
}