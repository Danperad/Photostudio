using PhotostudioDLL;

namespace PhotostudioGUI;

public partial class App
{
    /// <summary>
    ///     Получение базы данных
    /// </summary>
    /// <param name="e"></param>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        try
        {
            ApplicationContext.LoadDb();
            Order.CheckStatusTime();
        }
        catch
        {
            MessageBox.Show("Заполните файл конфигурации");
            Shutdown();
        }
    }
}