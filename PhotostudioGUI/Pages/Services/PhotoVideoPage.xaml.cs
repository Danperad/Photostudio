using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class PhotoVideoPage
{
    private readonly ServiceProvided _service;
    private readonly ProvidedServiceWindow _window;

    public PhotoVideoPage(ServiceProvided service, ProvidedServiceWindow window)
    {
        _service = service;
        _window = window;
        InitializeComponent();
    }
}