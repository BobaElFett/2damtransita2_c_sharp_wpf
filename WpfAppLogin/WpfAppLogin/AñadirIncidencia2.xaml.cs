using System.Windows;
using System.Windows.Input;
using static WpfAppLogin.Incidencias;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System;
using System.IO;

namespace WpfAppLogin
{
    public partial class AñadirIncidencia2 : Window
    {
        string urlIncidencia;
        Incidencia incidenciaM;
        private string selectedImagePath;
        private BitmapImage bitmap;

        public AñadirIncidencia2(string url)
        {
            InitializeComponent();
            urlIncidencia = url;
        }

        public AñadirIncidencia2(string url, double longitud, double latitud)
        {
            InitializeComponent();
            incidenciaM = new Incidencia();
            string lat = latitud.ToString();
            string lont = longitud.ToString();
            LatitudTextBox.Text = lat;
            LongitudTextBox.Text = lont;

            urlIncidencia = url;
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

        private void AñadirIncidencia_Click(object sender, RoutedEventArgs e)
        {
            string tituloI = TituloTextBox.Text;
            string descripcionI = DescripcionTextBox.Text;
            string estadoI = EstadoComboBox.Text;
            string fechaI = FechaDatePicker.SelectedDate?.ToString("yyyy-MM-ddTHH:mm:ss");
            bool accesibilidadI = AccesibilidadCheckBox.IsChecked ?? false;
            string accesibilidadValue = accesibilidadI ? "true" : "false";

            string image = ConvertImageToBase64(selectedImagePath);

            string jsonBody = $"{{\"titulo\":\"{tituloI}\",\"descripcion\":\"{descripcionI}\"," +
            $"\"estadoIncidencia\":\"{estadoI}\",\"fechahora\":\"{fechaI}\"," +
            $"\"accesibilidad\":{accesibilidadValue}," +
            $"\"foto\":\"{image}\"," +
            $"\"id_cliente\":\"{TokenManager.IdClient}\"," +
            $"\"latitud\":{LatitudTextBox.Text.Replace(',', '.')},\"longitud\":{LongitudTextBox.Text.Replace(',', '.')}}}";

            TokenManager.PostItems(urlIncidencia, jsonBody);

            Close();
        }

        private string ConvertImageToBase64(string imagePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = fileStream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder(); // Puedes ajustar el encoder según el formato de la imagen
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));
                        encoder.Save(memoryStream);

                        byte[] bytes = memoryStream.ToArray();
                        return Convert.ToBase64String(bytes);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al convertir la imagen a base64: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void BrowseImageButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png;)|*.jpg; *.jpeg; *.png;";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;

                bitmap = new BitmapImage(new Uri(selectedImagePath));
                FotoImage.Source = bitmap;
            }
        }
    }
}
