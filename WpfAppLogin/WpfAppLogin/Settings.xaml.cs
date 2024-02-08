using System.Windows;
using System.Windows.Input;

namespace WpfAppLogin
{
    /// <summary>
    /// Lógica de interacción para Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();

            UrlTextBox.Text = TokenManager.UrlHost;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Confirma que desea cambiar la URL a: " + UrlTextBox.Text,
                                          "Confirmar Cambio",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Config.WriteConfiguration(UrlTextBox.Text);
            }

        }
    }
}
