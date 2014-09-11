using System;
using System.Threading.Tasks;

namespace myProducts.Xamarin.Contracts.Networking
{
	public interface IArticlesHub
	{
		event Action OnArticleChanged;
		Task Start();
		void Stop();
	}
}