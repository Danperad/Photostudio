namespace PhotostudioGUI.Windows;

/// <summary>
///     Основное окно
/// </summary>
public partial class MainWindow
{
    public MainWindow(Employee employee)
    {
        Application.Current.MainWindow = this;
        Employee = employee;
        InitializeComponent();
    }

    internal Employee Employee { get; }

    /// <summary>
    ///     Конфигурация окна на основе роли
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainWindow_OnInitialized(object? sender, EventArgs e)
    {
        UserNameBlock.Text = Employee.FullName;
        switch (Employee.RoleID)
        {
            case 1:
                MainWindowBuilder.AdminBuild(this);
                break;
            case 7:
                MainWindowBuilder.ManagerBuild(this);
                break;
            default:
                MainWindowBuilder.WorkerBuild(this);
                break;
        }
    }

    private void LogOut_OnClick(object sender, RoutedEventArgs e)
    {
        LogOut();
    }

    /// <summary>
    /// Выход из аккаунта
    /// </summary>
    internal void LogOut()
    {
        var window = new LoginWindow();
        Application.Current.MainWindow = window;
        window.Show();
        Close();
    }
}