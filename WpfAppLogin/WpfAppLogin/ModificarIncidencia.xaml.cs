using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static WpfAppLogin.Incidencias;

namespace WpfAppLogin
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class ModificarIncidencia : Window
    {
        string urlIncidencia;
        Incidencia incidenciaM;
        public ModificarIncidencia(string url, Incidencia incidencia)
        {
            if (incidencia == null)
            {
                MessageBox.Show("Incidencia is null!");
            } else
            {
                urlIncidencia = url;

                InitializeComponent();

                incidenciaM = new Incidencia
                {
                    id_incidencia = incidencia.id_incidencia,
                    titulo = incidencia.titulo,
                    descripcion = incidencia.descripcion,
                    estadoIncidencia = incidencia.estadoIncidencia,
                    duracion = incidencia.duracion,
                    fechahora = incidencia.fechahora,
                    id_punto = incidencia.id_punto
                };

                if (incidenciaM != null)
                {
                    TituloTextBox.Text = incidenciaM.titulo.ToString();
                    DescripcionTextBox.Text = incidenciaM.descripcion.ToString();
                    EstadoTextBox.Text = incidenciaM.estadoIncidencia.ToString();
                    DuracionTextBox.Text = incidenciaM.duracion.ToString();
                    FechaDatePicker.Text = incidenciaM.fechahora.ToString();
                    PuntoTextBox.Text = incidenciaM.id_punto.ToString();
                }

                InitializeComponent();
            }
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

        private void BorrarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmResult = MessageBox.Show("Confirme que quiere eliminar la incidencia",
                                                 "Confirma Eliminar",
                                                 MessageBoxButton.YesNo);

            if (confirmResult == MessageBoxResult.Yes)
            {
                TokenManager.DeleteItems(incidenciaM.id_incidencia, urlIncidencia);
            }
        }

        private void ModificarIncidencia_Click(object sender, RoutedEventArgs e)
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

            TokenManager.PutItems(urlIncidencia, jsonBody, incidenciaM.id_incidencia);
        }
    }
}
