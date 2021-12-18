using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Castle.Core.Internal;
using PhotostudioDLL;
using PhotostudioDLL.Entity;

namespace PhotostudioGUI.Pages;

/// <summary>
///     Логика взаимодействия для Client.xaml
/// </summary>
public partial class ClientPage : Page
{
    public ClientPage()
    {
        InitializeComponent();
        ClientData.ItemsSource = ContextDB.GetClients();
    }

    private void AddClientClick(object sender, RoutedEventArgs e)
    {
        Client client = new Client
        {
            PhoneNumber = "+7" + PhoneBox.Text,
            FirstName = FirstNameBox.Text,
            LastName = LastNameBox.Text,
        };
        if (EMailBox.Text != String.Empty) client.EMail = EMailBox.Text;
        if (MiddleNameBox.Text != String.Empty) client.MiddleName = MiddleNameBox.Text;
        try
        {
            ContextDB.Add(client);
        }
        catch
        {
            MessageBox.Show("Номер телефона уже используется у другого клиента");
        }

        ClientData.ItemsSource = ContextDB.GetClients();
    }

    private char[] phonesymb = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    private void PhoneBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        int index = PhoneBox.CaretIndex;
        string text = "+";
        foreach (char c in PhoneBox.Text)
        {
            if (phonesymb.Contains(c))
            {
                text += c;
            }
        }

        PhoneBox.Text = text;
        PhoneBox.CaretIndex = index;
    }

    private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (SearchBox.Text.IsNullOrEmpty()) ClientData.ItemsSource = ContextDB.GetClients();
        else
        {
            var list = ContextDB.GetClients();
            var search = SearchBox.Text.ToLower();
            ClientData.ItemsSource = list.Where(d =>
                d.EMail.ToLower().Contains(search) || d.FirstName.ToLower().Contains(search) ||
                d.LastName.ToLower().Contains(search) || d.MiddleName.ToLower().Contains(search) ||
                d.PhoneNumber.Contains(search)).ToList();
        }
    }
}