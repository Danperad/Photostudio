using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace PhotostudioGUI;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
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