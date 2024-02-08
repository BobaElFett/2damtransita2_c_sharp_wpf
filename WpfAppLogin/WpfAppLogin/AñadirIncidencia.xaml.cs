using System.Windows;
using System.Windows.Input;
using static WpfAppLogin.Incidencias;

namespace WpfAppLogin
{
    public partial class AñadirIncidencia : Window
    {
        string urlIncidencia;
        Incidencia incidenciaM;
        public AñadirIncidencia(string url)
        {
            InitializeComponent();
            urlIncidencia = url;

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
            string estadoI = EstadoComboBox.Text;
            string fechaI = FechaDatePicker.SelectedDate?.ToString("yyyy-MM-ddTHH:mm:ss");
            bool accesibilidadI = AccesibilidadCheckBox.IsChecked ?? false;
            string accesibilidadValue = accesibilidadI ? "true" : "false";
            double longitud = double.Parse(LongitudTextBox.Text);
            double latitud = double.Parse(LatitudTextBox.Text);

            string jsonBody = $"{{\"titulo\":\"{tituloI}\",\"descripcion\":\"{descripcionI}\"," +
            $"\"estadoIncidencia\":\"{estadoI}\",\"fechahora\":\"{fechaI}\"," +
            $"\"accesibilidad\":{accesibilidadValue}," +
            $"\"foto\":\"{""}\"," +
            $"\"id_cliente\":\"{TokenManager.IdClient}\"," +
            $"\"latitud\":{latitud},\"longitud\":{longitud}}}";

            TokenManager.PostItems(urlIncidencia, jsonBody);

            Close();
        }
    }
}
