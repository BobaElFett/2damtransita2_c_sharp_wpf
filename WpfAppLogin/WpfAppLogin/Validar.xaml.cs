using System;
using System.Windows;
using System.Windows.Input;
using static WpfAppLogin.Incidencias;

namespace WpfAppLogin
{
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
            string accesibilidadValue = incidenciaM.accesibilidad ? "true" : "false";

            string jsonBody = $"{{\"titulo\":\"{incidenciaM.titulo}\",\"descripcion\":\"{incidenciaM.descripcion}\"," +
                $"\"estadoIncidencia\":\"{"ACEPTADA"}\",\"fechahora\":\"{incidenciaM.fechahora}\"," +
                $"\"accesibilidad\":{accesibilidadValue}," +
                $"\"foto\":\"{incidenciaM.foto}\"," +
                $"\"id_cliente\":\"{incidenciaM.id_cliente}\"," +
                $"\"id\":\"{incidenciaM.id}\"," +
                $"\"longitud\":{incidenciaM.longitud},\"latitud\":{incidenciaM.latitud}}}";

            Console.WriteLine(urlIncidencia + " " + jsonBody + " " + incidenciaM.id);

            TokenManager.PutItems(urlIncidencia, jsonBody, incidenciaM.id);

            Close();
        }

        private void RechazarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            string accesibilidadValue = incidenciaM.accesibilidad ? "true" : "false";

            string jsonBody = $"{{\"titulo\":\"{incidenciaM.titulo}\",\"descripcion\":\"{incidenciaM.descripcion}\"," +
                $"\"estadoIncidencia\":\"{"RECHAZADA"}\",\"fechahora\":\"{incidenciaM.fechahora}\"," +
                $"\"accesibilidad\":{accesibilidadValue}," +
                $"\"foto\":\"{incidenciaM.foto}\"," +
                $"\"id_cliente\":\"{incidenciaM.id_cliente}\"," +
                $"\"id\":\"{incidenciaM.id}\"," +
                $"\"longitud\":{incidenciaM.longitud},\"latitud\":{incidenciaM.latitud}}}";

            TokenManager.PutItems(urlIncidencia, jsonBody, incidenciaM.id);

            Close();
        }
    }
}
