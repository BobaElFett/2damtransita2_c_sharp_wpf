using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using static WpfAppLogin.Clientes;

namespace WpfAppLogin
{
    public partial class ClientesInforme : Window
    {
        string url;
        public ClientesInforme(string urlC)
        {
            InitializeComponent();
            url = urlC;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void CloseButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void GenerarInforme_Click(object sender, RoutedEventArgs e)
        {
            List<Cliente> newClientes = JsonConvert.DeserializeObject<List<Cliente>>(TokenManager.GetItems(url));

            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream("InformeClientes.pdf", FileMode.Create));
            doc.Open();

            int numeroColumnas = ContarCheckBoxesMarcados();
            PdfPTable table = new PdfPTable(numeroColumnas);
            AddTableHeader(table);

            foreach (Cliente cliente in newClientes)
            {
                AddRow(table, cliente, numeroColumnas);
            }

            doc.Add(table);
            doc.Close();
            MessageBox.Show("Informe generado exitosamente.");
        }

        private int ContarCheckBoxesMarcados()
        {
            int count = 0;
            if (CheckBoxIDCliente.IsChecked == true)
                count++;
            if (CheckBoxUsername.IsChecked == true)
                count++;
            if (CheckBoxNombre.IsChecked == true)
                count++;
            if (CheckBoxApellidos.IsChecked == true)
                count++;
            if (CheckBoxEstado.IsChecked == true)
                count++;
            return count;
        }

        private void AddTableHeader(PdfPTable table)
        {
            if (CheckBoxIDCliente.IsChecked == true)
                table.AddCell("ID Cliente");
            if (CheckBoxUsername.IsChecked == true)
                table.AddCell("Username");
            if (CheckBoxNombre.IsChecked == true)
                table.AddCell("Nombre");
            if (CheckBoxApellidos.IsChecked == true)
                table.AddCell("Apellidos");
            if (CheckBoxEstado.IsChecked == true)
                table.AddCell("Estado");
        }

        private void AddRow(PdfPTable table, Cliente cliente, int numeroColumnas)
        {
            if (CheckBoxIDCliente.IsChecked == true)
                table.AddCell(cliente.id_cliente.ToString());
            if (CheckBoxUsername.IsChecked == true)
                table.AddCell(cliente.username);
            if (CheckBoxNombre.IsChecked == true)
                table.AddCell(cliente.nombre);
            if (CheckBoxApellidos.IsChecked == true)
                table.AddCell(cliente.apellidos);
            if (CheckBoxEstado.IsChecked == true)
                table.AddCell(cliente.estado);

            while (table.NumberOfColumns < numeroColumnas)
            {
                table.AddCell("");
            }
        }
    }
}
