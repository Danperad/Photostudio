using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Castle.Core.Internal;
using PhotostudioDLL.Entities;

namespace PhotostudioGUI.Pages;

/// <summary>
///     Логика взаимодействия для Client.xaml
/// </summary>
public partial class ClientPage : Page
{
    private readonly Employee _employee;
    private readonly Frame _frame;
    private List<Client> _clients;
    private readonly List<string> country = new(new[] { "+7", "+1", "+38" });

    private readonly char[] phonesymb = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public ClientPage(Frame frame)
    {
        _frame = frame;
        _clients = Client.Get();
        InitializeComponent();
    }

    private void AddClientClick(object sender, RoutedEventArgs e)
    {
        if (PhoneBox.Text.Length != 10)
        {
            ErrorBlock.Text = "Ведён не коректный номер телефона";
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

    private void PhoneBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var index = PhoneBox.CaretIndex;
        var text = "";
        foreach (var c in PhoneBox.Text)
            if (phonesymb.Contains(c))
                text += c;

        PhoneBox.Text = text;
        PhoneBox.CaretIndex = index;
    }

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

    private void CountryBox_OnInitialized(object? sender, EventArgs e)
    {
        var temp = (ComboBox)sender;
        temp.ItemsSource = country;
        temp.SelectedItem = temp.Items[0];
    }

    private void ClientData_OnInitialized(object? sender, EventArgs e)
    {
        ClientData.ItemsSource = _clients;
    }

    private void OpenOrderClient(object sender, MouseButtonEventArgs e)
    {
        var client = ((ListViewItem)sender).Content as Client;
        _frame.Navigate(new OrderPage(_employee, client));
    }
}