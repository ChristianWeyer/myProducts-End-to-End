using System.ComponentModel;
using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.iOS.Rendering;
using myProducts.Xamarin.Views.Components;
using OxyPlot.XamarinIOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(Chart), typeof(ChartRenderer))]

namespace myProducts.Xamarin.iOS.Rendering
{
	public class ChartRenderer : ViewRenderer<Chart, PlotView>
	{
		private Chart _chart;
		private PlotView _plotView;
		private IStatisticsPageViewModel _viewModel;

		protected override void OnElementChanged(ElementChangedEventArgs<Chart> e)
		{
			base.OnElementChanged(e);

			_plotView = new PlotView();
			_chart = e.NewElement;
			_viewModel = _chart.BindingContext as IStatisticsPageViewModel;
			_viewModel.PropertyChanged += ViewModelPropertyChanged;

			SetNativeControl(_plotView);
		}

		private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals(_chart.BindingName)
				&& (_chart.BindingName.Equals("DistributionPlotModel")))
			{
				_plotView.Model = _viewModel.DistributionPlotModel;
			}

			if (e.PropertyName.Equals(_chart.BindingName)
				&& (_chart.BindingName.Equals("SalesPlotModel")))
			{
				_plotView.Model = _viewModel.SalesPlotModel;
			}
		}
	}
}