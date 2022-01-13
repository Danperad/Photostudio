namespace PhotostudioGUI.Pages;

/// <summary>
///     Страница с предоставляемыми услугами
/// </summary>
public partial class CurrentService
{
    private readonly OrderService _service;

    public CurrentService(OrderService service)
    {
        _service = service;
        InitializeComponent();
    }

    private void Page_Initialized(object sender, EventArgs e)
    {
        var textBlocks = BuildPageOrderService.BuildPage(_service);
        foreach (var block in textBlocks) MainStackPanel.Children.Add(block);
    }
}