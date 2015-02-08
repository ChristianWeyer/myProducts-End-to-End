using System.Net.Http.Formatting;
using System.Web.Http;

namespace Thinktecture.Applications.Framework.WebApi
{
    public static class HttpConfigurationExtensions
    {
        public static void UseJsonOnly(this HttpConfiguration config)
        {
            var jsonFormatter = new JsonMediaTypeFormatter();
            config.Services.Replace(typeof(IContentNegotiator), new JsonOnlyContentNegotiator(jsonFormatter));
            config.Formatters.Clear();
            config.Formatters.Add(jsonFormatter);
        }
    }
}