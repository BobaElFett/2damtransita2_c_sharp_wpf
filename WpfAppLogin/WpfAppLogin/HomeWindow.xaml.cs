using System.Windows;
using System.Windows.Input;


namespace WpfAppLogin
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            ContentHome.Content = new Incidencias();

            this.PreviewKeyDown += HomeWindow_PreviewKeyDown;

            this.Focusable = true;

            WindowState = WindowState.Maximized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainForm = new MainWindow();
            this.Hide();
            mainForm.Show();
        }

        private void MinimizeButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void MaximizedButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            /*if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }*/
        }

        private void CloseButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            ContentHome.Content = new Clientes();
        }

        private void Incidencias_Click(object sender, RoutedEventArgs e)
        {
            ContentHome.Content = new Incidencias();
        }

        private void Mapa_Click(object sender, RoutedEventArgs e)
        {
            ContentHome.Content = new Mapa();
        }

        private void HomeWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.D1)
                {
                    Clientes_Click(sender, e);
                }
                else if (e.Key == Key.D2)
                {
                    Incidencias_Click(sender, e);
                }
                else if (e.Key == Key.D3)
                {
                    Mapa_Click(sender, e);
                }
            }
        }
    }
}
