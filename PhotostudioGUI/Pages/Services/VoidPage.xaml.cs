using System;
using System.Windows.Controls;
using PhotostudioDLL.Entities;
using PhotostudioGUI.Windows;

namespace PhotostudioGUI.Pages.Services;

public partial class VoidPage
{
    private readonly ExecuteableService _executeableService;
    private readonly ProvidedServiceWindow _window;

    public VoidPage(ExecuteableService executeableService, ProvidedServiceWindow window)
    {
        _executeableService = executeableService;
        _window = window;
        InitializeComponent();
        FillItems();
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((sender as ComboBox)!.SelectedItem is Employee employee)
        {
            _executeableService.Employee = employee;
        }
    }

    private void FillItems()
    {
        EmployeeComboBox.ItemsSource = _executeableService.Service.ID switch
        {
            3 => Employee.GetByRoleId(3),
            4 => Employee.GetByRoleId(5),
            _ => Employee.GetByRoleId(7)
        };
        if (EmployeeComboBox.Items.Count != 0) return;
        MessageTextBox.Text = "Сотрудники отсутствуют";
        EmployeeComboBox.IsEnabled = false;
    }
}