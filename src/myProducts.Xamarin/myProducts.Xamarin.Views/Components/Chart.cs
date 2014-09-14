using OxyPlot;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Components
{
	// Xamarin Custom Renderer Concept
	public class Chart : View
	{
		public static readonly BindableProperty PlotModelProperty =
			BindableProperty.Create<Chart, PlotModel>(chart => chart.PlotModel, null);

		public PlotModel PlotModel
		{
			get { return (PlotModel)GetValue(PlotModelProperty); }
			set { SetValue(PlotModelProperty, value); }
		}
	}
}