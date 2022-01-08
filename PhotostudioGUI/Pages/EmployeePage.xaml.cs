using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Castle.Core.Internal;
using PhotostudioDLL.Entities;

namespace PhotostudioGUI.Pages;

/// <summary>
///     Логика взаимодействия для Employee.xaml
/// </summary>
public partial class EmployeePage : Page
{
    private List<String> country = new List<string>(new[] {"+7", "+1", "+381"});
    private List<Employee> _employees = Employee.Get();
    private readonly Frame _frame;

    public EmployeePage(Frame frame)
    {
        _frame = frame;
        InitializeComponent();
    }

    private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (SearchBox.Text.IsNullOrEmpty()) EmployeeData.ItemsSource = _employees;
        else
        {
            var search = SearchBox.Text.ToLower();
            EmployeeData.ItemsSource = _employees.Where(d =>
                (d.EMail != null ? (d.EMail.ToLower().Contains(search)) : false) ||
                d.FirstName.ToLower().Contains(search) ||
                d.LastName.ToLower().Contains(search) ||
                (d.MiddleName != null ? (d.MiddleName.ToLower().Contains(search)) : false) ||
                d.PhoneNumber.Contains(search)).ToList();
        }
    }

    private void EmployeeData_OnInitialized(object? sender, EventArgs e)
    {
        (sender as ListView)!.ItemsSource = _employees;
    }

    private void CountryBox_OnInitialized(object? sender, EventArgs e)
    {
        ComboBox temp = (ComboBox) sender!;
        temp.ItemsSource = country;
        temp.SelectedItem = temp.Items[0];    
    }

    private static char[] phonesymb = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

    private void PhoneBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        int index = PhoneBox.CaretIndex;
        string text = "";
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

    private void AddClientClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var employee = ((ListViewItem) sender).Content as Employee;
        _frame.Navigate(new CurrentEmployeePage(employee, _frame));
    }

    private void RoleBox_OnInitialized(object? sender, EventArgs e)
    {
        RoleBox.ItemsSource = Role.Get();
    }

    private void AddEmployee()
    {
        if (LastNameBox.Text.IsNullOrEmpty())
        {
            LastNameBox.BorderBrush = new SolidColorBrush(Colors.Red);
            return;
        }
        if (FirstNameBox.Text.IsNullOrEmpty())
        {
            FirstNameBox.BorderBrush = new SolidColorBrush(Colors.Red);
            return;
        }
    }
}