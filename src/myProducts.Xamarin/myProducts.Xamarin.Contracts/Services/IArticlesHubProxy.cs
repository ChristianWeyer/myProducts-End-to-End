using System;
using System.Threading.Tasks;

namespace myProducts.Xamarin.Contracts.Services
{
	public interface IArticlesHubProxy
	{
		event Action OnArticleChanged;
		Task Start();
		void Stop();
	}
}