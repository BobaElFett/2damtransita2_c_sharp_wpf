using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppLogin
{
    /// <summary>
    /// Lógica de interacción para Incidencias.xaml
    /// </summary>
    public partial class Incidencias : UserControl
    {
        string urlIncidencias = "/incidencias/";

        List<Incidencia> incidencias = new List<Incidencia>();

        public class Incidencia
        {
            public int id_incidencia { get; set; }
            public string titulo { get; set; }
            public string descripcion { get; set; }
            public string estadoIncidencia { get; set; }
            public string fechahora { get; set; }
            public string duracion { get; set; }
            public string id_cliente { get; set; }
            public string id_punto { get; set; }

            /*public List<Incidencia> GetIncidencias() {

                String incidencias = TokenManager.GetItems("/incidencias");

                List<Incidencia> l =

                    return l;
            }*/

        }

        public Incidencias()
        {
            InitializeComponent();
            cargarData();
        }


        private void cargarData()
        {
            List<Incidencia> newIncidencias = JsonConvert.DeserializeObject<List<Incidencia>>(TokenManager.GetItems(urlIncidencias));

            dataGrid.Items.Clear();

            foreach (var incidencia in newIncidencias)
            {
                dataGrid.Items.Add(new Incidencia
                {
                    id_incidencia = incidencia.id_incidencia,
                    titulo = incidencia.titulo,
                    descripcion = incidencia.descripcion,
                    estadoIncidencia = incidencia.estadoIncidencia,
                    fechahora = incidencia.fechahora,
                    duracion = incidencia.duracion,
                    id_cliente = incidencia.id_cliente,
                    id_punto = incidencia.id_punto
                });

                incidencias.Add(incidencia);
            }
        }

        private void AñadirIncidencia_Click(object sender, RoutedEventArgs e)
        {
            AñadirIncidencia añadirI = new AñadirIncidencia(urlIncidencias);
            añadirI.ShowDialog();
            cargarData();
        }

        private void ModificarIncidencia_Click(object sender, MouseButtonEventArgs e)
        {
            Incidencia currentSelection = (Incidencia)(sender as DataGrid).CurrentItem;

            if (currentSelection != null) {
                ModificarIncidencia modificarI = new ModificarIncidencia(urlIncidencias, currentSelection);
                modificarI.ShowDialog();
                cargarData();
            }
        }

        private async void RecargarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            cargarData();
        }
    }
}
