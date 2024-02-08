using MapControl;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static WpfAppLogin.Incidencias;

namespace WpfAppLogin
{
    public partial class Mapa : UserControl
    {
        string urlIncidencias = "/incidencias/";
        List<Incidencia> Incidencias;
        private List<MapControl.Pushpin> pushpins = new List<MapControl.Pushpin>();

        public Mapa()
        {
            InitializeComponent();
            cargarData();
            DataContext = this;

            MapaI.MouseRightButtonDown += MapaI_MouseDoubleClick;
        }

        private void cargarData()
        {
            Incidencias = JsonConvert.DeserializeObject<List<Incidencia>>(TokenManager.GetItems(urlIncidencias));
            DataContext = this;
            AddMarkersForIncidencias();
        }

        private void MapaI_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var location = MapaI.ViewToLocation(e.GetPosition(MapaI));

            double lon = location.Longitude;
            double lat = location.Latitude;

            AñadirIncidencia2 añadirForm = new AñadirIncidencia2(urlIncidencias, lon, lat);
            añadirForm.ShowDialog();

            cargarData();
        }

        private void DobleClickIncidencia(Pushpin pushpin)
        {
            pushpin.MouseDoubleClick += (sender, e) =>
            {
                Incidencia incidencia = (Incidencia)pushpin.Tag;

                if (incidencia != null)
                {
                    ModificarIncidencia modificarI = new ModificarIncidencia(urlIncidencias, incidencia);
                    modificarI.ShowDialog();
                    cargarData();
                }
            };
        }

        private void AddMarkersForIncidencias()
        {
            foreach (var pushpin in pushpins)
            {
                MapaI.Children.Remove(pushpin);
            }

            gridIncidencias.ItemsSource = Incidencias;

            if (MapaI != null && Incidencias != null)
            {
                foreach (var incidencia in Incidencias)
                {
                    var pushpin = new MapControl.Pushpin
                    {
                        Location = new MapControl.Location(incidencia.latitud, incidencia.longitud),
                        Tag = incidencia
                    };

                    if (incidencia.accesibilidad) {
                        pushpin.Background = new SolidColorBrush(Colors.Green);
                    } 
                    else
                    {
                        pushpin.Background = new SolidColorBrush(Colors.Red);
                    }

                    MapaI.Children.Add(pushpin);
                    pushpins.Add(pushpin);

                    DobleClickIncidencia(pushpin);
                }
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Incidencia selectedIncidencia = ((FrameworkElement)sender).DataContext as Incidencia;

            if (selectedIncidencia != null)
            {
                ModificarIncidencia modificarI = new ModificarIncidencia(urlIncidencias, selectedIncidencia);
                MapaI.Center = new Location(selectedIncidencia.latitud, selectedIncidencia.longitud);
                modificarI.ShowDialog();
                cargarData();
            }
        }

        private void filtrar(object sender, RoutedEventArgs e)
        {
            string selectedFilter = ((ComboBoxItem)cmbFiltro.SelectedItem)?.Content.ToString();

            switch (selectedFilter)
            {
                case "Titulo":
                    if (!string.IsNullOrEmpty(FiltroTitulo.Text))
                    {
                        string filtroTitulo = FiltroTitulo.Text;
                        MessageBox.Show(urlIncidencias + "titulo/" + filtroTitulo);
                        Incidencias = JsonConvert.DeserializeObject<List<Incidencia>>(TokenManager.GetItems(urlIncidencias + "titulo/" + filtroTitulo));
                    }
                    break;

                case "Estado":
                    string filtroEstado = ((ComboBoxItem)FiltroEstado.SelectedItem)?.Content.ToString();
                    if (!string.IsNullOrEmpty(filtroEstado))
                    {
                        Incidencias = JsonConvert.DeserializeObject<List<Incidencia>>(TokenManager.GetItems(urlIncidencias + "estado/" + filtroEstado));
                    }
                    break;

                case "Accesibilidad":
                    string acce = ((ComboBoxItem)FiltroAcce.SelectedItem)?.Content.ToString();
                    if (!string.IsNullOrEmpty(acce))
                    {
                        int accesibilidad = acce.Equals("ACCESIBLE") ? 1 : 0;
                        Incidencias = JsonConvert.DeserializeObject<List<Incidencia>>(TokenManager.GetItems(urlIncidencias + "accesibilidad/" + accesibilidad));
                    }
                    break;
            }
            AddMarkersForIncidencias();
        }


        private void cmbFiltro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stackTitulo.Visibility = Visibility.Collapsed;
            stackEstado.Visibility = Visibility.Collapsed;
            stackAcce.Visibility = Visibility.Collapsed;

            switch (((ComboBoxItem)cmbFiltro.SelectedItem)?.Content.ToString())
            {
                case "Titulo":
                    stackTitulo.Visibility = Visibility.Visible;
                    break;
                case "Estado":
                    stackEstado.Visibility = Visibility.Visible;
                    break;
                case "Accesibilidad":
                    stackAcce.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}