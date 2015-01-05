using CefSharp;
using CefSharp.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using Thinktecture.Applications.Framework;

namespace myProducts.WindowsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChromiumWebBrowser webBrowser;

        public MainWindow()
        {
            InitializeComponent();

            Cef.Initialize(new CefSettings());
            //CEF.Initialize(new Settings { CachePath = @".\cachepath" });

            var urlToNavigate = AppDomain.CurrentDomain.BaseDirectory + @"client\app\index.html";
            var browserSettings = new BrowserSettings
            {
                UniversalAccessFromFileUrlsAllowed = true
            };

            webBrowser = new ChromiumWebBrowser();
            webBrowser.Address = urlToNavigate;
            webBrowser.BrowserSettings = browserSettings;
            webBrowser.RegisterJsObject("cefCallback", new CefBridge());

            CefSharpContainer.Children.Add(webBrowser);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.I &&
                (Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                webBrowser.ShowDevTools();
            }
        }

        private void GetSampleDataFromJavaScript(object sender, RoutedEventArgs e)
        {
            webBrowser.ExecuteScriptAsync("ttTools.getSampleData()");
        }
    }

    public class CefBridge
    {
        public void SampleDataResult(object result)
        {
            dynamic data = result.ToDynamic();
            MessageBox.Show("Total articles: " + data.Count, "From JavaScript in Chromium");
        }
    }
}
