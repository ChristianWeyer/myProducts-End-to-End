using System.Windows.Data;
using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.WindowsPhone.Rendering;
using OxyPlot.WP8;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Binding = System.Windows.Data.Binding;

[assembly:ExportRenderer(typeof(Chart), typeof(ChartRenderer))]
namespace myProducts.Xamarin.WindowsPhone.Rendering
{
	public class ChartRenderer : ViewRenderer<Chart, PlotView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Chart> e)
		{
			base.OnElementChanged(e);

			var plotView = new PlotView();
			plotView.DataContext = e.NewElement.BindingContext;
			plotView.SetBinding(PlotView.ModelProperty, new Binding("PlotModel"));

			SetNativeControl(plotView);
		}
	}
}