using System;
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
        private WebView _webView;

        public MainWindow()
        {
            InitializeComponent();

            var browserSettings = new BrowserSettings
            {
                FileAccessFromFileUrlsAllowed = true,
                UniversalAccessFromFileUrlsAllowed = true
            };

           var urlToNavigate = AppDomain.CurrentDomain.BaseDirectory + @"client\index.html";

            _webView = new WebView(urlToNavigate, browserSettings);
            _webView.LoadCompleted += _webView_LoadCompleted;

            cefSharpContainer.Children.Add(_webView);
        }

        void _webView_LoadCompleted(object sender, CefSharp.LoadCompletedEventArgs url)
        {
            _webView.ShowDevTools();
        }
    }
}
