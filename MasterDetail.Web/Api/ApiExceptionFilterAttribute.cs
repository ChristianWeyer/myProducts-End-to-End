using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace MasterDetail.Web.Api
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var argumentException = actionExecutedContext.Exception as Exception;

            if (argumentException != null)
            {
                var message = string.IsNullOrEmpty(argumentException.Message)
                                  ? "An exception occurred"
                                  : argumentException.Message;

                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                actionExecutedContext.Response.Content.Headers.ContentType =
                    actionExecutedContext.Request.Content.Headers.ContentType;
            }
        }
    }
}