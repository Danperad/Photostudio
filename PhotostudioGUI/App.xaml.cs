using System;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using PhotostudioDLL;

namespace PhotostudioGUI;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        try
        {
            ApplicationContext.LoadDB();
        }
        catch
        {
            MessageBox.Show("Заполните файл конфигурации");
            this.Shutdown();
        }
    }
    
}