using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using PhotostudioDLL.Entities;
using fw = SourceChord.FluentWPF;

namespace PhotostudioGUI.Pages;

public partial class CurrentEmployeePage : Page
{
    private readonly Frame _frame;
    private Employee _employee;

    public CurrentEmployeePage(Employee employee, Frame frame)
    {
        _employee = employee;
        _frame = frame;
        InitializeComponent();
        AddButtonBack();
    }

    public void AddButtonBack()
    {
        var button = new Button
        {
            HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top,
            Height = Width = 35
        };
        button.Click += (sender, args) => _frame.GoBack();
        button.Content = new PackIcon { Kind = PackIconKind.ArrowLeft };
        button.Style = new Style(typeof(fw.RevealElement));
        MainGrid.Children.Add(button);
    }
}