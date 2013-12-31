using System;
using System.Windows.Input;
using CefSharp;
using CefSharp.Wpf;
using System.Windows;

namespace myProducts.WindowsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WebView webView;
        private bool loaded;

        public MainWindow()
        {
            InitializeComponent();

            CEF.Initialize(new Settings { CachePath = @".\cachepath" });

            var browserSettings = new BrowserSettings
            {
                UniversalAccessFromFileUrlsAllowed = true
            };

            var urlToNavigate = AppDomain.CurrentDomain.BaseDirectory + @"client\index.html";

            webView = new WebView(urlToNavigate, browserSettings);
            webView.LoadCompleted += webView_LoadCompleted;

            cefSharpContainer.Children.Add(webView);
        }

        void webView_LoadCompleted(object sender, CefSharp.LoadCompletedEventArgs url)
        {
            loaded = true;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (loaded)
            {
                if (e.Key == Key.I && (Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
                {
                    webView.ShowDevTools();
                }
            }
        }
    }
}
