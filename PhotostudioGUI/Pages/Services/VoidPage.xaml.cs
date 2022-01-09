using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class VoidPage
{
    private readonly ServiceProvided _service;
    private readonly ProvidedServiceWindow _window;

    public VoidPage(ServiceProvided service, ProvidedServiceWindow window)
    {
        _service = service;
        _window = window;
        InitializeComponent();
    }
}