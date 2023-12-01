﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class ModificarIncidencia : Window
    {
        string urlIncidencia;
        Incidencia incidenciaM;
        public ModificarIncidencia(string url, Incidencia incidencia)
        {
            if (incidencia == null)
            {
                MessageBox.Show("Incidencia is null!");
            }
            else
            {
                urlIncidencia = url;

                InitializeComponent();

                incidenciaM = new Incidencia
                {
                    id = incidencia.id,
                    titulo = incidencia.titulo,
                    descripcion = incidencia.descripcion,
                    fechahora = incidencia.fechahora,
                    accesibilidad = incidencia.accesibilidad,
                    estadoIncidencia = incidencia.estadoIncidencia,
                    foto = incidencia.foto,
                    latitud = incidencia.latitud,
                    longitud = incidencia.longitud,
                    id_cliente = incidencia.id_cliente
                };

                if (incidenciaM != null)
                {
                    TituloTextBox.Text = incidenciaM.titulo;
                    DescripcionTextBox.Text = incidenciaM.descripcion;
                    EstadoTextBox.Text = incidenciaM.estadoIncidencia;
                    FechaDatePicker.SelectedDate = DateTime.Parse(incidenciaM.fechahora);
                    AccesibilidadCheckBox.IsChecked = incidenciaM.accesibilidad;
                    AccesibilidadCheckBox.UpdateLayout();
                    LatitudTextBox.Text = incidenciaM.latitud.ToString();
                    LongitudTextBox.Text = incidenciaM.longitud.ToString();
                }

                try
                {
                    if (!string.IsNullOrEmpty(incidenciaM.foto))
                    {
                        byte[] imageBytes = Convert.FromBase64String(incidenciaM.foto);
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new System.IO.MemoryStream(imageBytes);
                        bitmapImage.EndInit();
                        FotoImage.Source = bitmapImage;
                    }
                } catch (System.FormatException ex)
                {
                    Console.Write($"Error loading image: {ex.Message}");
                }

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

        private void BorrarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmResult = MessageBox.Show("Confirme que quiere eliminar la incidencia",
                                                 "Confirma Eliminar",
                                                 MessageBoxButton.YesNo);

            if (confirmResult == MessageBoxResult.Yes)
            {
                TokenManager.DeleteItems(incidenciaM.id, urlIncidencia);
            }
        }

        private void ModificarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            string tituloI = TituloTextBox.Text;
            string descripcionI = DescripcionTextBox.Text;
            string estadoI = EstadoTextBox.Text;
            string fechaI = FechaDatePicker.SelectedDate?.ToString("yyyy-MM-ddTHH:mm:ss");
            bool accesibilidadI = AccesibilidadCheckBox.IsChecked ?? false;
            string accesibilidadValue = accesibilidadI ? "true" : "false";
            double  longitud = double.Parse(LongitudTextBox.Text);
            double latitud = double.Parse(LatitudTextBox.Text);

            string jsonBody = $"{{\"titulo\":\"{tituloI}\",\"descripcion\":\"{descripcionI}\"," +
            $"\"estadoIncidencia\":\"{estadoI}\",\"fechahora\":\"{fechaI}\"," +
            $"\"accesibilidad\":{accesibilidadValue}," +
            $"\"foto\":\"{incidenciaM.foto}\"," +
            $"\"id_cliente\":\"{incidenciaM.id_cliente}\"," +
            $"\"latitud\":{latitud},\"longitud\":{longitud}}}";

            TokenManager.PutItems(urlIncidencia, jsonBody, incidenciaM.id);
        }

        private void ValidarIncidencia_Click(object sender, RoutedEventArgs e)
        {
            Validar validarI = new Validar(urlIncidencia, incidenciaM);
            validarI.ShowDialog();
        }
    }
}
