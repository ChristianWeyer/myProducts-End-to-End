using System;
using System.Windows.Input;
using CefSharp;
using CefSharp.Wpf;
using System.Windows;
using Thinktecture.Applications.Framework;

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
			webView.RegisterJsObject("cefCallback", new CefBridge());

			CefSharpContainer.Children.Add(webView);
		}

		private void webView_LoadCompleted(object sender, LoadCompletedEventArgs e)
		{
			loaded = true;
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (loaded)
			{
				if (e.Key == Key.I && 
					(Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
				{
					webView.ShowDevTools();
				}
			}
		}

		private void GetSampleDataFromJavaScript(object sender, RoutedEventArgs e)
		{
			webView.ExecuteScript("ttTools.getSampleData()");
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
