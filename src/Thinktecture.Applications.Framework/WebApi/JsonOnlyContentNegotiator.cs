using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Thinktecture.Applications.Framework.WebApi
{
    public class JsonOnlyContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _formatter;

        public JsonOnlyContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _formatter = formatter;
        }

        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            var result = new ContentNegotiationResult(_formatter, new MediaTypeHeaderValue("application/json"));

            return result;
        }
    }
}
