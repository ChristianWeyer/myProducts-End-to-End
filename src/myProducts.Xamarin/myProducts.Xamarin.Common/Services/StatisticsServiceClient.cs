using System.Collections.Generic;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.Services;

namespace myProducts.Xamarin.Common.Services
{
	public class StatisticsServiceClient : BaseServiceClient, IStatisticsServiceClient
	{
		private const string DistributionUrlTemplate = "statistics/distributin";
		private const string SalesUrlTemplate = "statistics/sales";

		public StatisticsServiceClient(ITokenManager tokenManager) : base(tokenManager) {}
		
		public async Task<IEnumerable<DistributionDto>> GetDistribution()
		{
			var response = await Get<IEnumerable<DistributionDto>>(DistributionUrlTemplate);
			return response;
		}

		public async Task<IEnumerable<SalesDto>> GetSales()
		{
			var response = await Get<IEnumerable<SalesDto>>(SalesUrlTemplate);
			return response;
		}
	}
}