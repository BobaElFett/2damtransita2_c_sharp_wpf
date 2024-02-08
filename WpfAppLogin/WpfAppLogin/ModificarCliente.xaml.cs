using System;
using System.Windows;
using System.Windows.Input;
using static WpfAppLogin.Clientes;

namespace WpfAppLogin
{
    public partial class ModificarCliente : Window
    {
        string urlClientes = "/clientes/";
        Cliente clienteM;
        public ModificarCliente(String url, Cliente cliente)
        {
            InitializeComponent();

            if (cliente == null)
            {
                MessageBox.Show("Cliente is null!");
            }
            else
            {
                urlClientes = url;

                clienteM = new Cliente
                {
                    id_cliente = cliente.id_cliente,
                    username = cliente.username,
                    nombre = cliente.nombre,
                    apellidos = cliente.apellidos,
                    estado = cliente.estado,
                    password = cliente.password
                };

                UsernameTextBox.Text = clienteM.username;
                NombreTextBox.Text = clienteM.nombre;
                ApellidoTextBox.Text = clienteM.apellidos;
                EstadoTextBox.Text = clienteM.estado;
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

        private void ModificarCliente_Click(object sender, RoutedEventArgs e)
        {

            string jsonBody = $"{{\"username\":\"{clienteM.username}\",\"nombre\":\"{clienteM.nombre}\"," +
                              $"\"apellidos\":\"{clienteM.apellidos}\",\"estado\":\"{clienteM.estado}\"," +
                              $"\"password\":\"{clienteM.password}\"}}";

            TokenManager.PutItems(urlClientes, jsonBody, clienteM.id_cliente);
        }

        private void ValidarCliente_Click(object sender, RoutedEventArgs e)
        {

            string jsonBody = $"{{\"username\":\"{clienteM.username}\",\"nombre\":\"{clienteM.nombre}\"," +
                              $"\"apellidos\":\"{clienteM.apellidos}\",\"estado\":\"{"0"}\"," +
                              $"\"password\":\"{clienteM.password}\"}}";

            TokenManager.PutItems(urlClientes, jsonBody, clienteM.id_cliente);
        }

        private void BanearCliente_Click(object sender, RoutedEventArgs e)
        {
            string jsonBody = $"{{\"username\":\"{clienteM.username}\",\"nombre\":\"{clienteM.nombre}\"," +
                              $"\"apellidos\":\"{clienteM.apellidos}\",\"estado\":\"{"1"}\"," +
                              $"\"password\":\"{clienteM.password}\"}}";

            TokenManager.PutItems(urlClientes, jsonBody, clienteM.id_cliente);
        }
    }
}
