using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfAppLogin
{
    /// <summary>
    /// Lógica de interacción para AñadirIncidencia.xaml
    /// </summary>
    public partial class AñadirIncidencia : Window
    {
        string urlIncidencias;
        public AñadirIncidencia(string url)
        {
            InitializeComponent();
            urlIncidencias = url;
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

        private void AñadirIncidencia_Click(object sender, RoutedEventArgs e)
        {
            string tituloI = TituloTextBox.Text;
            string descripcionI = DescripcionTextBox.Text;
            string estadoI = EstadoTextBox.Text;
            string duracionI = DuracionTextBox.Text;
            string fechaI = FechaDatePicker.SelectedDate?.ToString("yyyy-MM-ddTHH:mm:ss");
            string puntoI = PuntoTextBox.Text;

            string jsonBody = $"{{\"titulo\":\"{tituloI}\",\"descripcion\":\"{descripcionI}\"," +
                $"\"estadoIncidencia\":" + $"\"{estadoI}\",\"fechahora\":\"{fechaI}\"," +
                $"\"duracion\":\"{duracionI}\"," + $"\"id_cliente\":\"{TokenManager.IdClient}\"," +
                $" \"id_punto\":\"{puntoI}\"}}";

            //MessageBox.Show(jsonBody);

            TokenManager.PostItems(urlIncidencias, jsonBody);
        }
    }
}
