using System.Collections.Generic;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;

namespace myProducts.Xamarin.Contracts.Services
{
	public interface IStatisticsServiceClient
	{
		Task<IEnumerable<DistributionDto>> GetDistribution();
		Task<IEnumerable<SalesDto>> GetSales();
	}
}