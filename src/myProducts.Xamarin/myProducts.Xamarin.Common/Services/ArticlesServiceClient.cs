using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.Services;

namespace myProducts.Xamarin.Common.Services
{
	public class ArticlesServiceClient : BaseServiceClient, IArticlesServiceClient
	{
		private const string PagedArticlesUrlTemplate = "articles?$inlinecount=allpages&$top={0}&$skip={1}&ts={2}";
		private const string SearchUrlTemplate = "&$filter=substringof('{0}',tolower(Name))";
		private const string SingleArticleUrlTemplate = "articles/{0}";

		public ArticlesServiceClient(ITokenManager tokenManager) 
			: base(tokenManager) {}


		public async Task<IEnumerable<ArticleDto>> GetPaged(int pageSize, int page, string searchText)
		{
			// TODO: Temporary workaround for always getting actual data and not cached
			var requestUrl = String.Format(PagedArticlesUrlTemplate, pageSize, (page - 1) * pageSize, DateTime.Now.Ticks);

			if (!String.IsNullOrWhiteSpace(searchText))
			{
				requestUrl += String.Format(SearchUrlTemplate, searchText.ToLower());
			}

			var response = await Get<PageResult<ArticleDto>>(requestUrl);
			return response.Items;
		}

		public async Task<ArticleDetailDto> GetArticleBy(Guid id)
		{
			var requestUrl = String.Format(SingleArticleUrlTemplate, id);

			var response = await Get<ArticleDetailDto>(requestUrl);
			return response;
		}
	}
}