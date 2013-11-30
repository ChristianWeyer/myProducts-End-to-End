using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Thinktecture.Applications.Framework.WebApi.Debugging
{
    public class ConsoleLoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                string requestContent = await request.Content.ReadAsStringAsync();
                Console.WriteLine("###REQUEST: {0}", requestContent);
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (response.Content != null)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("###RESPONSE: {0}", responseContent);
            }

            return response;
        }
    }
}
