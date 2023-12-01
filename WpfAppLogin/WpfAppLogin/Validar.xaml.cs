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
    public partial class Validar : Window
    {
        string urlIncidencia;
        Incidencia incidenciaM;
        public Validar(string url, Incidencia incidencia)
        {
            if (incidencia == null)
            {
                MessageBox.Show("Incidencia is null!");
            }
            else
            {
                urlIncidencia = url;

                InitializeComponent();

                incidenciaM = new Incidencia
                {
                    id = incidencia.id,
                    titulo = incidencia.titulo,
                    descripcion = incidencia.descripcion,
                    fechahora = incidencia.fechahora,
                    accesibilidad = incidencia.accesibilidad,
                    estadoIncidencia = incidencia.estadoIncidencia,
                    foto = incidencia.foto,
                    latitud = incidencia.latitud,
                    longitud = incidencia.longitud,
                    id_cliente = incidencia.id_cliente
                };
            }
        }

        private void CloseButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void ValidarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            string jsonBody = $"{{\"titulo\":\"{incidenciaM.titulo}\",\"descripcion\":\"{incidenciaM.descripcion}\"," +
            $"\"estado\":\"{"ACEPTADA"}\",\"fechahora\":\"{incidenciaM.fechahora}\"," +
            $"\"id_cliente\":\"{incidenciaM.id_cliente}\"," + $"\"accesibilidad\":{incidenciaM.accesibilidad}," +
            $"\"longitud\":{incidenciaM.longitud},\"latitud\":{incidenciaM.latitud},";

            TokenManager.PutItems(urlIncidencia, jsonBody, incidenciaM.id);
        }

        private void RechazarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            string jsonBody = $"{{\"titulo\":\"{incidenciaM.titulo}\",\"descripcion\":\"{incidenciaM.descripcion}\"," +
            $"\"estado\":\"{"RECHAZADA"}\",\"fechahora\":\"{incidenciaM.fechahora}\"," +
            $"\"id_cliente\":\"{incidenciaM.id_cliente}\"," + $"\"accesibilidad\":{incidenciaM.accesibilidad}," +
            $"\"longitud\":{incidenciaM.longitud},\"latitud\":{incidenciaM.latitud},";

            TokenManager.PutItems(urlIncidencia, jsonBody, incidenciaM.id);
        }
    }
}
