using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfAppLogin
{
    public partial class Incidencias : UserControl
    {
        string urlIncidencias = "/incidencias/";

        public class Incidencia
        {
            public int id { get; set; }
            public string titulo { get; set; }
            public string descripcion { get; set; }
            public string fechahora { get; set; }
            public bool accesibilidad { get; set; }
            public string estadoIncidencia { get; set; }
            public string foto { get; set; }
            public int id_cliente { get; set; }
            public double longitud { get; set; }
            public double latitud { get; set; }


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

            dataGrid.ItemsSource = newIncidencias;

            if (dataGrid != null && dataGrid.ItemsSource != null)
            {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
                dataView.SortDescriptions.Add(new SortDescription("estadoIncidencia", ListSortDirection.Ascending));
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
