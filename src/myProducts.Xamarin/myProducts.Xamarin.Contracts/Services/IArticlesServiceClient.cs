using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;

namespace myProducts.Xamarin.Contracts.Services
{
	public interface IArticlesServiceClient
	{
		Task<IEnumerable<ArticleDto>> GetPaged(int pageSize, int page, string searchText);

		Task<ArticleDetailDto> GetArticleBy(Guid id);
	}
}