using System.Windows.Input;
using Castle.Core.Internal;

namespace PhotostudioGUI.Pages;

/// <summary>
///     Логика взаимодействия для Client.xaml
/// </summary>
public partial class ClientPage
{
    private readonly List<string> _country = new(new[] {"+7", "+1", "+38"});
    private readonly Employee _employee;
    private readonly Frame _frame;

    private readonly char[] _phonesymb = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
    private IEnumerable<Client> _clients;

    public ClientPage(Frame frame, Employee employee)
    {
        _employee = employee;
        _frame = frame;
        _clients = Client.Get();
        InitializeComponent();
    }

    /// <summary>
    ///     Добавление клиента
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddClientClick(object sender, RoutedEventArgs e)
    {
        if (PhoneBox.Text.Length != 10)
        {
            ErrorBlock.Text = "Введен некорректный номер телефона";
            return;
        }

        var client = new Client
        (
            LastNameBox.Text,
            FirstNameBox.Text,
            CountryBox.Text + PhoneBox.Text
        );
        if (!EMailBox.Text.IsNullOrEmpty()) client.EMail = EMailBox.Text;
        if (!MiddleNameBox.Text.IsNullOrEmpty()) client.MiddleName = MiddleNameBox.Text;
        try
        {
            Client.Add(client);
            ErrorBlock.Text = "";
        }
        catch
        {
            ErrorBlock.Text = "Номер телефона уже используется у другого клиента";
        }

        _clients = Client.Get();
        ClientData.ItemsSource = _clients;
    }

    /// <summary>
    ///     Заполнение номера телефона только числами
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PhoneBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var index = PhoneBox.CaretIndex;
        var text = PhoneBox.Text.Where(c => _phonesymb.Contains(c)).Aggregate("", (current, c) => current + c);

        PhoneBox.Text = text;
        PhoneBox.CaretIndex = index;
    }

    /// <summary>
    ///     Поиск клиентов (только по одному столбцу)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (SearchBox.Text.IsNullOrEmpty())
        {
            ClientData.ItemsSource = _clients;
        }
        else
        {
            var search = SearchBox.Text.ToLower();
            ClientData.ItemsSource = _clients.Where(d =>
                d.EMail != null && d.EMail.ToLower().Contains(search) ||
                d.FirstName.ToLower().Contains(search) ||
                d.LastName.ToLower().Contains(search) ||
                d.MiddleName != null && d.MiddleName.ToLower().Contains(search) ||
                d.PhoneNumber.Contains(search)).ToList();
        }
    }

    /// <summary>
    ///     Добавление телефонных кодов страны
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CountryBox_OnInitialized(object? sender, EventArgs e)
    {
        var temp = sender as ComboBox;
        temp!.ItemsSource = _country;
        temp.SelectedItem = temp.Items[0];
    }

    /// <summary>
    ///     Загрузка клиентов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ClientData_OnInitialized(object? sender, EventArgs e)
    {
        ClientData.ItemsSource = _clients;
    }

    /// <summary>
    ///     Открытие заявок клиента по двойному клику
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OpenOrderClient(object sender, MouseButtonEventArgs e)
    {
        var client = ((ListViewItem) sender).Content as Client;
        _frame.Navigate(new OrderPage(_employee, client!));
    }
}