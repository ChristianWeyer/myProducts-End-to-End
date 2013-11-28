using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.OData;

namespace Thinktecture.Applications.Framework.WebApi
{
    public class InlineCountQueryableAttribute : QueryableAttribute
    {
        private static MethodInfo _createPageResult =
            typeof(InlineCountQueryableAttribute)
            .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
            .Single(m => m.Name == "CreatePageResult");

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            HttpRequestMessage request = actionExecutedContext.Request;
            HttpResponseMessage response = actionExecutedContext.Response;

            IQueryable result;

            if (response.IsSuccessStatusCode
                && response.TryGetContentValue<IQueryable>(out result))
            {
                long? inlineCount = request.GetInlineCount();
            
                if (inlineCount != null)
                {
                    actionExecutedContext.Response = _createPageResult.MakeGenericMethod(result.ElementType).Invoke(
                        null, new object[] { request, request.GetInlineCount(), request.GetNextPageLink(), result }) as HttpResponseMessage;
                }
            }
        }

        internal static HttpResponseMessage CreatePageResult<T>(HttpRequestMessage request, long? count, Uri nextpageLink, IEnumerable<T> results)
        {
            return request.CreateResponse(HttpStatusCode.OK, new PageResult<T>(results, nextpageLink, count));
        }
    }
}
