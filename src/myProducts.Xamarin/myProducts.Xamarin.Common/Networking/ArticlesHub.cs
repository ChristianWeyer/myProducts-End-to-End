using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using myProducts.Xamarin.Contracts.Networking;

namespace myProducts.Xamarin.Common.Networking
{
	public class ArticlesHub : IArticlesHub
	{
		private const string HubConnectionUrl = "https://demo.christianweyer.net";
		private const string ProxyName = "clientNotificationHub";
		private const string ArticleChangedEventName = "articleChange";

		private readonly HubConnection _hubConnection;
		public event Action OnArticleChanged;

		public ArticlesHub()
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

		public void Stop()
		{
			_hubConnection.Stop();
		}
	}
}