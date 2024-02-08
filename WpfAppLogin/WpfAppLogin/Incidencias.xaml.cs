using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }

        public Incidencias()
        {
            InitializeComponent();
            cargarData();

            this.PreviewKeyDown += Incidencias_PreviewKeyDown;

            this.Focusable = true;
            this.KeyDown += HomeWindow_KeyDown;
        }

        private void cargarData()
        {
            //MessageBox.Show(TokenManager.GetItems(urlIncidencias));

            List<Incidencia> newIncidencias = JsonConvert.DeserializeObject<List<Incidencia>>(TokenManager.GetItems(urlIncidencias));

            dataGrid.ItemsSource = newIncidencias;

            if (dataGrid != null && dataGrid.ItemsSource != null)
            {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
                dataView.SortDescriptions.Add(new SortDescription("estadoIncidencia", ListSortDirection.Ascending));
            }
        }

        private void filtrar(object sender, RoutedEventArgs e)
        {
            string selectedFilter = ((ComboBoxItem)cmbFiltro.SelectedItem)?.Content.ToString();

            switch (selectedFilter)
            {
                case "Título":
                    string filtroTitulo = FiltroTitulo.Text;
                    MessageBox.Show(urlIncidencias + "titulo/" + filtroTitulo);
                    List<Incidencia> newIncidencias = JsonConvert.DeserializeObject<List<Incidencia>>(TokenManager.GetItems(urlIncidencias + "titulo/" + filtroTitulo));

                    dataGrid.ItemsSource = newIncidencias;

                    if (dataGrid != null && dataGrid.ItemsSource != null)
                    {
                        ICollectionView dataView = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
                    }
                    break;
                case "Estado":
                    string filtroEstado = ((ComboBoxItem)FiltroEstado.SelectedItem)?.Content.ToString();
                    List<Incidencia> newIncidencias3 = JsonConvert.DeserializeObject<List<Incidencia>>(TokenManager.GetItems(urlIncidencias + "estado/" + filtroEstado));

                    dataGrid.ItemsSource = newIncidencias3;

                    if (dataGrid != null && dataGrid.ItemsSource != null)
                    {
                        ICollectionView dataView = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
                    }
                    break;
                case "Accesibilidad":
                    string acce = ((ComboBoxItem)FiltroAcce.SelectedItem)?.Content.ToString();
                    if (acce.Equals("ACCESIBLE"))
                    {
                        List<Incidencia> newIncidencias4 = JsonConvert.DeserializeObject<List<Incidencia>>(
                            TokenManager.GetItems(urlIncidencias + "accesibilidad/1"));
                        dataGrid.ItemsSource = newIncidencias4;
                    } else
                    {
                        List<Incidencia> newIncidencias4 = JsonConvert.DeserializeObject<List<Incidencia>>(
                            TokenManager.GetItems(urlIncidencias + "accesibilidad/0"));
                        dataGrid.ItemsSource = newIncidencias4;
                    }

                    if (dataGrid != null && dataGrid.ItemsSource != null)
                    {
                        ICollectionView dataView = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
                    }
                    break;
            }
        }

        private void AñadirIncidencia_Click(object sender, RoutedEventArgs e)
        {
            AñadirIncidencia2 añadirI = new AñadirIncidencia2(urlIncidencias);
            añadirI.ShowDialog();
            cargarData();
        }

        private void ModificarIncidencia_Click(object sender, MouseButtonEventArgs e)
        {
            Incidencia currentSelection = (Incidencia)(sender as DataGrid).CurrentItem;

            if (currentSelection != null)
            {
                ModificarIncidencia modificarI = new ModificarIncidencia(urlIncidencias, currentSelection);
                modificarI.ShowDialog();
                cargarData();
            }
        }

        private void InformeIncidencia_Click(object sender, RoutedEventArgs e)
        {
            IncidenciasInforme informe = new IncidenciasInforme(urlIncidencias);
            informe.ShowDialog();
        }

        private async void RecargarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            cargarData();
        }

        private void Incidencias_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.N)
                {
                    AñadirIncidencia_Click(sender, e);
                }
            }
        }

        private void HomeWindow_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbFiltro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stackTitulo.Visibility = Visibility.Collapsed;
            stackEstado.Visibility = Visibility.Collapsed;
            stackAcce.Visibility = Visibility.Collapsed;

            // Show the selected filter option stack
            switch (((ComboBoxItem)cmbFiltro.SelectedItem)?.Content.ToString())
            {
                case "Título":
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
