using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfAppLogin
{
    public partial class Clientes : UserControl
    {
        string urlClientes = "/clientes/";
        public class Cliente
        {
            public int id_cliente { get; set; }
            public string username { get; set; }
            public string nombre { get; set; }
            public string apellidos { get; set; }
            public string estado { get; set; }
            public string password { get; set; }
        }

        public Clientes()
        {
            InitializeComponent();
            cargarData();
        }

        private void AñadirClientes_Click(object sender, RoutedEventArgs e)
        {
            AñadirCliente añadirI = new AñadirCliente(urlClientes);
            añadirI.ShowDialog();
            cargarData();
        }

        private void cargarData()
        {
            List<Cliente> newClientes = JsonConvert.
                DeserializeObject<List<Cliente>>(TokenManager.GetItems(urlClientes));
            dataGrid.Items.Clear();

            foreach (var cliente in newClientes)
            {
                dataGrid.Items.Add(new Cliente
                {
                    id_cliente = cliente.id_cliente,
                    username = cliente.username,
                    nombre = cliente.nombre,
                    apellidos = cliente.apellidos,
                    estado = cliente.estado,
                    password = cliente.password
                });
            }
        }

        private void ModificarCliente_Click(object sender, MouseButtonEventArgs e)
        {
            Cliente currentSelection = (Cliente)(sender as DataGrid).CurrentItem;

            if (currentSelection != null)
            {
                ModificarCliente modificarI = new ModificarCliente(urlClientes, currentSelection);
                modificarI.ShowDialog();
                cargarData();
            }
        }

        private void InformeCliente_Click(object sender, RoutedEventArgs e)
        {
            ClientesInforme informe = new ClientesInforme(urlClientes);
            informe.ShowDialog();
        }

        private async void RecargarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            cargarData();
        }

        private void filtrar(object sender, RoutedEventArgs e)
        {
            string selectedFilter = ((ComboBoxItem)cmbFiltro.SelectedItem)?.Content.ToString();

            if (selectedFilter.Equals("ACTIVO"))
            {
                selectedFilter = "0";
            }
            else
            {
                selectedFilter = "1";
            }

            string filtroEstado = ((ComboBoxItem)cmbFiltro.SelectedItem)?.Content.ToString();
            List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(TokenManager.GetItems(urlClientes + "estado/" + filtroEstado));

            // Clear the existing items in the DataGrid
            dataGrid.Items.Clear();

            // Set the new items
            foreach (var cliente in clientes)
            {
                dataGrid.Items.Add(new Cliente
                {
                    id_cliente = cliente.id_cliente,
                    username = cliente.username,
                    nombre = cliente.nombre,
                    apellidos = cliente.apellidos,
                    estado = cliente.estado,
                    password = cliente.password
                });
            }

            if (dataGrid != null && dataGrid.Items != null)
            {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(dataGrid.Items);
                // dataView.SortDescriptions.Add(new SortDescription("estado", ListSortDirection.Ascending));
            }
        }
    }
}
