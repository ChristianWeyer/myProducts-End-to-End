using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.Services;
using myProducts.Xamarin.Contracts.ViewModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;

namespace myProducts.Xamarin.ViewModels
{
	public class StatisticsPageViewModel : BaseViewModel, IStatisticsPageViewModel
	{
		private readonly IStatisticsServiceClient _statisticsServiceClient;
		private PlotModel _distributionPlotModel;
		private PlotModel _salesPlotModel;

		private readonly OxyColor[] _colorCycle =
		{
			OxyColor.Parse("#1f77b4"),
			OxyColor.Parse("#aec7e8"),
			OxyColor.Parse("#ff7f0e"),
			OxyColor.Parse("#ffbb78"),
			OxyColor.Parse("#2ca02c"),
			OxyColor.Parse("#98df8a")
		};

		public StatisticsPageViewModel(IStatisticsServiceClient statisticsServiceClient)
		{
			_statisticsServiceClient = statisticsServiceClient;
		}

		// TODO: Maybe it is better to bind to the actual data and not to the plotmodel?
		public PlotModel DistributionPlotModel
		{
			get { return _distributionPlotModel; }
			set { Set(ref _distributionPlotModel, value); }
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
			DistributionPlotModel = plotModel;
		}

		public async Task DownloadSalesData()
		{
			var data = await _statisticsServiceClient.GetSales();
			var plotModel = CreateSalesPlotModel(data);
			SalesPlotModel = plotModel;
		}

		private PlotModel CreateDistributionPlotModel(IEnumerable<DistributionDto> data)
		{
			var model = new PlotModel
			{
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.TopCenter,
				LegendOrientation = LegendOrientation.Horizontal,
				IsLegendVisible = true,
				LegendTextColor = GetDefaultTextColor(),
			};

			var chartSerie = new PieSeries
			{
				StartAngle = 0,
				AngleSpan = 360,
				StrokeThickness = 2,
				Title = "Distribution"
			};

			int i = 0;
			foreach (var item in data)
			{
				chartSerie.Slices.Add(new PieSlice(item.Category, item.Value)
				{
					Fill = _colorCycle[i],
				});
				i = ++i % _colorCycle.Length;
			}

			model.Series.Add(chartSerie);

			return model;
		}

		private PlotModel CreateSalesPlotModel(IEnumerable<SalesDto> data)
		{
			var model = new PlotModel
			{
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.TopCenter,
				LegendOrientation = LegendOrientation.Horizontal,
				IsLegendVisible = true,
				LegendTextColor = GetDefaultTextColor(),
			};

			int categoryIndex = 0;
			foreach (var sale in data)
			{
				var columnSeries = new ColumnSeries
				{
					Title = sale.Key,
				};
				model.Series.Add(columnSeries);

				foreach (var value in sale.Values)
				{
					columnSeries.Items.Add(new ColumnItem(Convert.ToInt32(value.Value), categoryIndex));
				}

				categoryIndex++;
			}

			var linearAxis = new LinearAxis
			{
				Position = AxisPosition.Left,
				AxislineColor = GetDefaultTextColor(),
				TextColor = GetDefaultTextColor(),
			};

			model.Axes.Add(linearAxis);

			return model;
		}

		private OxyColor GetDefaultTextColor()
		{
			return Device.OnPlatform(OxyColors.Black, OxyColors.Black, OxyColors.White);
		}
	}
}