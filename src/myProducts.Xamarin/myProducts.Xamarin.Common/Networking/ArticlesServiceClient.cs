using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.Networking;

namespace myProducts.Xamarin.Common.Networking
{
	public class ArticlesServiceClient : BaseServiceClient, IArticlesServiceClient
	{
		private const string PagedArticlesUrlTemplate = "articles?$inlinecount=allpages&$top={0}&$skip={1}";
		private const string SearchUrlTemplate = "&$filter=substringof('{0}',tolower(Name))";

		public ArticlesServiceClient(ITokenManager tokenManager) 
			: base(tokenManager) {}


		public async Task<IEnumerable<ArticleDto>> GetPaged(int pageSize, int page, string searchText)
		{
			var requestUrl = String.Format(PagedArticlesUrlTemplate, pageSize, (page - 1) * pageSize);

			if (!String.IsNullOrWhiteSpace(searchText))
			{
				requestUrl += String.Format(SearchUrlTemplate, searchText.ToLower());
			}

			var response = await Get<PageResult<ArticleDto>>(requestUrl);
			return response.Items;
		}
	}
}