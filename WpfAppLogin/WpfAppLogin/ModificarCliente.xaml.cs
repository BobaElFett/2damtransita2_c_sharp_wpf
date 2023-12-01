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
                PasswordTextBox.Text = clienteM.password;
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
    }
}
