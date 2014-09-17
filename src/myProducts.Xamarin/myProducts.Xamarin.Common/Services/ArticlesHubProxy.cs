using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using myProducts.Xamarin.Contracts.Services;

namespace myProducts.Xamarin.Common.Services
{
	public class ArticlesHubProxy : IArticlesHubProxy
	{
		private const string HubConnectionUrl = "https://demo.christianweyer.net";
		private const string ProxyName = "clientNotificationHub";
		private const string ArticleChangedEventName = "articleChange";

		private readonly HubConnection _hubConnection;
		public event Action OnArticleChanged;

		public ArticlesHubProxy()
		{
			_hubConnection = new HubConnection(HubConnectionUrl);
			InitializeProxy();
		}

		private void InitializeProxy()
		{
			var proxy = _hubConnection.CreateHubProxy(ProxyName);
			proxy.On(ArticleChangedEventName, DoOnArticleChanged);
		}

		private void DoOnArticleChanged()
		{
			var handler = OnArticleChanged;

			if (handler != null)
			{
				handler();
			}
		}

		public async Task Start()
		{
			await _hubConnection.Start();
		}

		public async Task Stop()
		{
			await Task.Run(() => _hubConnection.Stop());
		}
	}
}