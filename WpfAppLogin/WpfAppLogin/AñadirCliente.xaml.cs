using System.Windows;
using System.Windows.Input;

namespace WpfAppLogin
{
    public partial class AñadirCliente : Window
    {
        string urlCliente;
        public AñadirCliente(string url)
        {
            InitializeComponent();
            urlCliente = url;
        }

        private void CloseButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void AñadirCliente_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string nombre = NombreTextBox.Text;
            string apellido = ApellidoTextBox.Text;
            string rol = RolComboBox.Text;
            string password = PasswordTextBox.Text;

            string jsonBody = $"{{\"password\": {password}," +
                  $"\"username\": \"{username}\"," +
                  $"\"nombre\": \"{nombre}\"," +
                  $"\"apellidos\": \"{apellido}\"," +
                  $"\"role\": [\"{rol}\"]" +
                  "}}";

            TokenManager.SignUpItems(jsonBody);

            Close();
        }
    }
}
