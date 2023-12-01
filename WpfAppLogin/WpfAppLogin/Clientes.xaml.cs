using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void cargarData()
        {
            List<Cliente> newClientes = JsonConvert.DeserializeObject<List<Cliente>>(TokenManager.GetItems(urlClientes));

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

        private async void RecargarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            cargarData();
        }
    }
}
