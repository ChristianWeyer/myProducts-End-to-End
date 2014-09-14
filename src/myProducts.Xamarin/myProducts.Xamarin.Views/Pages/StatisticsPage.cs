using System.ServiceModel.Channels;
using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class StatisticsPage : BasePage
	{
		private readonly IStatisticsPageViewModel _viewModel;

		public StatisticsPage(IStatisticsPageViewModel viewModel)
		{
			_viewModel = viewModel;
			BindingContext = _viewModel;
			CreateUI();
		}

		private void CreateUI()
		{
			this.SetDefaultPadding();

			var stackLayout = CreateStackLayout();
			var distributionChart = CreateDistributionChart();
			var salesChart = CreateSalesChart();

			distributionChart.SetBinding<IStatisticsPageViewModel>(Chart.PlotModelProperty, m => m.DistributonPlotModel);
	//		salesChart.SetBinding<IStatisticsPageViewModel>(Chart.PlotModelProperty, m => m.SalesPlotModel);

			stackLayout.Children.AddRange(distributionChart, salesChart);

			SetScrollViewContent(stackLayout);
		}

		private Chart CreateSalesChart()
		{
			return new Chart()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};
		}

		private Chart CreateDistributionChart()
		{
			return new Chart()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};
		}

		private StackLayout CreateStackLayout()
		{
			return new StackLayout();
		}

		protected async override void OnAppearing()
		{
			await _viewModel.DownloadDistributionData();
			await _viewModel.DownloadSalesData();
		}
	}
}