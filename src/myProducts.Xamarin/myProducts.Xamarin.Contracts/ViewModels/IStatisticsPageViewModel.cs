using System.Threading.Tasks;
using OxyPlot;

namespace myProducts.Xamarin.Contracts.ViewModels
{
	public interface IStatisticsPageViewModel : IBusyIndicator
	{
		PlotModel DistributionPlotModel { get; set; }
		PlotModel SalesPlotModel { get; set; }

		Task DownloadDistributionData();
		Task DownloadSalesData();
	}
}