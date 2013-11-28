using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Thinktecture.Applications.Framework.WebApi
{
    /// <summary>
    /// Simple Web API & SignalR integration.
    /// No access to full hub, e.g. HubCallerContext.
    /// </summary>
    /// <typeparam name="THub"></typeparam>
    public abstract class HubApiController<THub> : ApiController
        where THub : IHub
    {
        private Lazy<IHubContext> hub = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<THub>()
        );

        protected string ConnectionId
        {
            get
            {
                var connectionId = new FormDataCollection(Request.RequestUri).Get("connectionId");
                return connectionId;
            }
        }

        protected IHubContext Hub
        {
            get { return hub.Value; }
        }
    }
}
