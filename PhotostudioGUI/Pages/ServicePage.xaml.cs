namespace PhotostudioGUI.Pages;

/// <summary>
///     Страница со списком услуг
/// </summary>
public partial class ServicePage
{
    private readonly IEnumerable<Service> _services;

    public ServicePage()
    {
        _services = Service.Get();
        InitializeComponent();
    }

    /// <summary>
    ///     Заполнение списка услуг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ServiceData_OnInitialized(object? sender, EventArgs e)
    {
        (sender as ListView)!.ItemsSource = _services;
    }
}