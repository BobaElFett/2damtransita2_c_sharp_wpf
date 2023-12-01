/*using System;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace WpfAppLogin
{
    public partial class Mapa : Window
    {
        public Mapa()
        {
            InitializeComponent();
            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);

            ChromiumWebBrowser browser = new ChromiumWebBrowser();
            browser.Address = "https://www.openstreetmap.org";

            Grid.Children.Add(browser);
        }

        protected override void OnClosed(EventArgs e)
        {
            Cef.Shutdown();
            base.OnClosed(e);
        }
    }
}*/
