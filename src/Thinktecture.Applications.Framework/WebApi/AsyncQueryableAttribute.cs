using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Thinktecture.Applications.Framework.WebApi
{
    public class AsyncQueryableAttribute : ActionFilterAttribute
    {
        private static readonly MethodInfo _bufferAsync = typeof(AsyncQueryableAttribute).GetMethod("BufferAsyncCore", BindingFlags.NonPublic | BindingFlags.Static);
        private readonly QueryableAttribute _innerFilter = new QueryableAttribute();

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            cancellationToken.ThrowIfCancellationRequested();

            HttpResponseMessage response = null;
            Exception exception = null;

            try
            {
                response = await continuation();
            }
            catch (Exception e)
            {
                exception = e;
            }

            try
            {
                HttpActionExecutedContext executedContext = new HttpActionExecutedContext(actionContext, exception) { Response = response };
                _innerFilter.OnActionExecuted(executedContext);

                if (executedContext.Response != null)
                {
                    return await BufferAsyncQueryable(executedContext.Response);
                }
                if (executedContext.Exception != null)
                {
                    throw executedContext.Exception;
                }
            }
            catch
            {
                // Catch is running because OnActionExecuted threw an exception, so we just want to re-throw the exception.
                // We also need to reset the response to forget about it since a filter threw an exception.
                actionContext.Response = null;
                throw;
            }

            throw new NotSupportedException("InnerFilter must set either Response or Exception. Both are null.");
        }

        // Executes the queryable eagerly and asynchronously if it implements IDbAsyncEnumerable.
        // Note that this would call ToListAsync and would load the entire result set into memory.
        private async Task<HttpResponseMessage> BufferAsyncQueryable(HttpResponseMessage response)
        {
            ObjectContent objectContent = response.Content as ObjectContent;

            if (response.IsSuccessStatusCode && objectContent != null)
            {
                IQueryable queryable = objectContent.Value as IQueryable;
                if (queryable != null && queryable is IDbAsyncEnumerable)
                {
                    objectContent.Value = await BufferAsync(queryable);
                }
            }

            return response;
        }

        private static Task<IQueryable> BufferAsync(IQueryable input)
        {
            MethodInfo bufferAsync = _bufferAsync.MakeGenericMethod(input.ElementType);

            return bufferAsync.Invoke(null, new[] { input }) as Task<IQueryable>;
        }

        // called using reflection.
        private static async Task<IQueryable> BufferAsyncCore<T>(IQueryable<T> input)
        {
            var list = await input.ToListAsync();
            return list.AsQueryable();
        }
    }
}
