using System;
using System.Windows.Controls;
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
        FillItems();
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((sender as ComboBox)!.SelectedItem is Employee employee)
        {
            _service.Employee = employee;
        }
    }

    private void FillItems()
    {
        EmployeeComboBox.ItemsSource = _service.Service.ID switch
        {
            3 => Employee.GetByRoleID(3),
            4 => Employee.GetByRoleID(5),
            _ => Employee.GetByRoleID(7)
        };
        if (EmployeeComboBox.Items.Count == 0)
        {
            MessageTextBox.Text = "Сотрудники отсутствуют";
            EmployeeComboBox.IsEnabled = false;
        }
    }
}