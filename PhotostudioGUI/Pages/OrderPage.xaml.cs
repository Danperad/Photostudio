using System.Windows.Controls;
using PhotostudioDLL;
using PhotostudioDLL.Entity;

namespace PhotostudioGUI.Pages;

public partial class OrderPage : Page
{
    public OrderPage()
    {
        InitializeComponent();
        OrderData.ItemsSource = ContextDB.GetOrders();
    }
}