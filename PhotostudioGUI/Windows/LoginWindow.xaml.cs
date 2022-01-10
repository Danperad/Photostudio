using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using FontAwesome5;
using PhotostudioDLL.Entities;

namespace PhotostudioGUI.Windows;

/// <summary>
/// Авторизация в приложение
/// </summary>
public partial class LoginWindow
{
    private readonly BackgroundWorker _bw;
    private string _login = string.Empty;
    private string _pass = string.Empty;

    private Employee? _profile;

    public LoginWindow()
    {
        InitializeComponent();
        _bw = (BackgroundWorker)FindResource("BackgroundWoker");
    }

    private void SignInButton_OnClick(object sender, RoutedEventArgs e)
    {
        ButtonText.Text = string.Empty;
        var loadImage = new ImageAwesome
        {
            Name = "LoadImage", Spin = true, Icon = EFontAwesomeIcon.Solid_Spinner, SpinDuration = 0.75,
            Height = 40, Width = 40
        };
        ButtonText.Inlines.Add(loadImage);
        LoginButton.IsEnabled = false;
        _login = LoginBox.Text.Trim();
        _pass = GetHash(PasswordBox.Password.Trim());
        _bw.RunWorkerAsync();
    }

    private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs doWorkEventArgs)
    {
        _profile = Employee.GetAuth(_login, _pass);
        if (_profile == null) return;
        _login = string.Empty;
        _pass = string.Empty;
    }

    private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (_profile != null)
        {
            OpenMainWindow();
        }
        else
        {
            ButtonText.Inlines.Clear();
            ButtonText.Text = "Войти";
            LoginButton.IsEnabled = true;
            ErroTextBlock.Text = "Incorrect Login or Password";
        }
    }

    private static string GetHash(string input)
    {
        var data = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();
        foreach (var t in data) sBuilder.Append(t.ToString("x2"));
        return sBuilder.ToString();
    }

    private void OpenMainWindow()
    {
        var window = new MainWindow(_profile!);
        window.Show();
        Close();
    }
}