using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using FontAwesome5;

namespace PhotostudioGUI.Windows;

/// <summary>
///     Авторизация в приложение
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
        _bw = (BackgroundWorker) FindResource("BackgroundWorker");
    }

    /// <summary>
    ///     Начало авторизации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    ///     Авторизация в отдельном потоке
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="doWorkEventArgs"></param>
    private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs doWorkEventArgs)
    {
        _profile = Employee.GetAuth(_login, _pass);
        _login = string.Empty;
        _pass = string.Empty;
    }

    /// <summary>
    ///     Открытие основного окна
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (_profile != null)
        {
            var window = new MainWindow(_profile!);
            window.Show();
            Close();
        }
        else
        {
            ButtonText.Inlines.Clear();
            ButtonText.Text = "Войти";
            LoginButton.IsEnabled = true;
            ErroTextBlock.Text = "Incorrect Login or Password";
        }
    }

    /// <summary>
    ///     Получение Хэш-кода
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static string GetHash(string input)
    {
        var data = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();
        foreach (var t in data) sBuilder.Append(t.ToString("x2"));
        return sBuilder.ToString();
    }
}