using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using FontAwesome5;
using PhotostudioDLL;
using PhotostudioDLL.Entity;

namespace PhotostudioGUI;

public partial class LoginWindow
{
    private readonly BackgroundWorker _bw;

    private Employee Profile;
    private string login = string.Empty;
    private string pass = string.Empty;

    public LoginWindow()
    {
        ApplicationContext.LoadDB();
        InitializeComponent();
        _bw = (BackgroundWorker) FindResource("backgroundWoker");
        /*var role = new Role {Title = "Admin", Responsibilities = "All", Rights = "All"};
        var emp = new Employee
        {
            EMail = "s",
            Profile = new EmployeeProfile
                {Login = "Admin", Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918"},
            EmploymentDate = DateTime.Now, FirstName = "First", Role = role, LastName = "Last", PassData = "6214532089",
            PhoneNumber = "+78005553535"
        };
        Employee.Add(emp);*/
    }

    private void SignInButton_OnClick(object sender, RoutedEventArgs e)
    {
        ButtonText.Text = string.Empty;
        var LoadImage = new ImageAwesome
        {
            Name = "LoadImage", Spin = true, Icon = EFontAwesomeIcon.Solid_Spinner, SpinDuration = 0.75,
            Height = 40, Width = 40
        };
        ButtonText.Inlines.Add(LoadImage);
        LoginButton.IsEnabled = false;
        login = LoginBox.Text.Trim();
        pass = GetHash(PasswordBox.Password.Trim());
        _bw.RunWorkerAsync();
    }

    private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs doWorkEventArgs)
    {
        Profile = Employee.GetEmployee(login, pass);
        if (Profile != null)
        {
            login = string.Empty;
            pass = string.Empty;
        }
    }

    private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (Profile != null)
        {
            OpenMainWindow();
        }
        else
        {
            ButtonText.Inlines.Clear();
            ButtonText.Text = "Войти";
            LoginButton.IsEnabled = true;
            ErroTextBlock.Text = "Incorrect in Login or Password";
        }
    }

    private string GetHash(string input)
    {
        var data = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();
        for (var i = 0; i < data.Length; i++) sBuilder.Append(data[i].ToString("x2"));
        return sBuilder.ToString();
    }

    private void OpenMainWindow()
    {
        var window = new MainWindow();
        window.Show();
        Close();
    }
}