using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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
        ClientData.ItemsSource = Client.Get();
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
            Client.Add(client);
        }
        catch
        {
            MessageBox.Show("Номер телефона уже используется у другого клиента");
        }

        ClientData.ItemsSource = Client.Get();
    }

    private char[] phonesymb = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

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
}