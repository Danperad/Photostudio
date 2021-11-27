using PhotostudioDLL;
using System.Windows;
using System.Windows.Controls;

namespace PhotostudioGUI
{
    public partial class Login : Page
    {
        private MainWindow _mainWindow;
        public Login(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationContext.Login(loginBox.Text, passwordBox.Password))
            {
                // TODO: При авторизации менять страницу, убрать MessageBox при ошибке авторизции
            }
            else
            {
                MessageBox.Show("Wrong login or password.");
            }
        }
    }
}