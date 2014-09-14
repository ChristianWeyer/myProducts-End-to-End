using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.Services;
using myProducts.Xamarin.Contracts.ViewModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace myProducts.Xamarin.ViewModels
{
	public class StatisticsPageViewModel : BaseViewModel, IStatisticsPageViewModel
	{
		private readonly IStatisticsServiceClient _statisticsServiceClient;
		private PlotModel _distributonPlotModel;
		private PlotModel _salesPlotModel;

		public StatisticsPageViewModel(IStatisticsServiceClient statisticsServiceClient)
		{
			_statisticsServiceClient = statisticsServiceClient;
		}

		// TODO: Maybe it is better to bind to the actual data and not to the plotmodel?
		public PlotModel DistributonPlotModel
		{
			get { return _distributonPlotModel; }
			set { Set(ref _distributonPlotModel, value); }
		}

		public PlotModel SalesPlotModel
		{
			get { return _salesPlotModel; }
			set { Set(ref _salesPlotModel, value); }
		}

		public async Task DownloadDistributionData()
		{
			var data = await _statisticsServiceClient.GetDistribution();
			var plotModel = CreateDistributionPlotModel(data);
			DistributonPlotModel = plotModel;
		}

		private PlotModel CreateDistributionPlotModel(IEnumerable<DistributionDto> data)
		{
			var model = new PlotModel();
			var chartSerie = new PieSeries()
			{
				StartAngle = 0,
				AngleSpan = 360,
			};

			foreach (var distribution in data)
			{
				chartSerie.Slices.Add(new PieSlice(distribution.Category, distribution.Value));
			}

			model.Series.Add(chartSerie);

			return model;
		}

		public async Task DownloadSalesData()
		{
			var data = await _statisticsServiceClient.GetSales();
			//var plotModel = CreateSalesPlotModel(data);
			//SalesPlotModel = plotModel;
		}

		private PlotModel CreateSalesPlotModel(IEnumerable<SalesDto> data)
		{
			var model = new PlotModel();
			var barSerie = new BarSeries();
			var categoryAxis = new CategoryAxis();

			foreach (var sale in data)
			{
				categoryAxis.Labels.Add(sale.Key);

				foreach (var value in sale.Values)
				{
					barSerie.BarWidth = value.Value;
				}
			}

			model.Series.Add(barSerie);
			model.Axes.Add(categoryAxis);

			return model;
		}
	}
}