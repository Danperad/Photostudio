using System.Windows;
using PhotostudioDLL;

namespace PhotostudioGUI;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        try
        {
            ApplicationContext.LoadDb();
        }
        catch
        {
            MessageBox.Show("Заполните файл конфигурации");
            Shutdown();
        }
    }
}