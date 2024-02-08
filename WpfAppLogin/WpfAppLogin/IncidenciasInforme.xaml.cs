using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using static WpfAppLogin.Incidencias;

namespace WpfAppLogin
{
    public partial class IncidenciasInforme : Window
    {
        string urlIncidencias;
        public IncidenciasInforme(string url)
        {
            InitializeComponent();
            urlIncidencias = url;

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
            List<Incidencia> incidencias = JsonConvert.DeserializeObject<List<Incidencia>>(TokenManager.GetItems(urlIncidencias));

            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream("InformeIncidencias.pdf", FileMode.Create));
            doc.Open();

            int numeroColumnas = ContarCheckBoxesMarcados();
            PdfPTable table = new PdfPTable(numeroColumnas);
            AddTableHeader(table);

            foreach (Incidencia incidencia in incidencias)
            {
                AddRow(table, incidencia, numeroColumnas);
            }

            doc.Add(table);
            doc.Close();
            MessageBox.Show("Informe generado exitosamente.");
        }

        private int ContarCheckBoxesMarcados()
        {
            return gridCheckBoxes.Children.OfType<CheckBox>().Count(cb => cb.IsChecked == true);
        }

        private void AddTableHeader(PdfPTable table)
        {
            foreach (CheckBox cb in gridCheckBoxes.Children.OfType<CheckBox>().Where(cb => cb.IsChecked == true))
            {
                table.AddCell(cb.Content.ToString());
            }
        }

        private void AddRow(PdfPTable table, Incidencia incidencia, int numeroColumnas)
        {
            foreach (CheckBox cb in gridCheckBoxes.Children.OfType<CheckBox>().Where(cb => cb.IsChecked == true))
            {
                switch (cb.Name)
                {
                    case "CheckBoxId":
                        table.AddCell(incidencia.id.ToString());
                        break;
                    case "CheckBoxTitulo":
                        table.AddCell(incidencia.titulo);
                        break;
                    case "CheckBoxDes":
                        table.AddCell(incidencia.descripcion);
                        break;
                    case "CheckBoxFecha":
                        table.AddCell(incidencia.fechahora.ToString());
                        break;
                    case "CheckBoxAcces":
                        table.AddCell(incidencia.accesibilidad.ToString());
                        break;
                    case "CheckBoxEstado":
                        table.AddCell(incidencia.estadoIncidencia.ToString());
                        break;
                    case "CheckBoxIdCliente":
                        table.AddCell(incidencia.id_cliente.ToString());
                        break;
                    case "CheckBoxLon":
                        table.AddCell(incidencia.longitud.ToString());
                        break;
                    case "CheckBoxLat":
                        table.AddCell(incidencia.latitud.ToString());
                        break;
                }
            }

            while (table.NumberOfColumns < numeroColumnas)
            {
                table.AddCell("");
            }
        }
    }
}
