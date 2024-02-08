using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppLogin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Config.ReadConfiguration();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            string accessToken = TokenManager.GetAccessToken(email, password);

            if (!string.IsNullOrEmpty(accessToken))
            {
                TokenManager.AccessToken = accessToken;

                HomeWindow homeForm = new HomeWindow();
                this.Hide();
                homeForm.Show();
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void MinimizeButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void CloseButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}