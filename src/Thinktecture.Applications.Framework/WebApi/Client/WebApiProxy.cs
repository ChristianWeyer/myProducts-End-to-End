using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using ImpromptuInterface;
using Newtonsoft.Json;
using RestSharp;

namespace Thinktecture.Applications.Framework.WebApi.Client
{
    public class WebApiProxyFactory : DynamicObject
    {
        private readonly RestClient _restClient;
        private readonly Type _webApiType;

        protected WebApiProxyFactory(RestClient restClient, Type webApiType)
        {
            _restClient = restClient;
            _webApiType = webApiType;
        }

        public static TWebApiInterface CreateProxyFor<TWebApiInterface>(string baseAddress) where TWebApiInterface : class
        {
            var restClient = new RestClient(baseAddress);
            var proxy = new WebApiProxyFactory(restClient, typeof(TWebApiInterface)).ActLike<TWebApiInterface>();

            return proxy;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var methodName = binder.Name;
            var methodInfo = _webApiType.GetMethod(methodName);

            if (methodInfo == null)
            {
                throw new Exception("No such method");
            }

            var actionHttpMethodProviderAttributes = methodInfo.GetCustomAttributes(typeof(IActionHttpMethodProvider), true);

            if (actionHttpMethodProviderAttributes.Length != 1)
            {
                throw new Exception("Unable to determine the HTTP method");
            }

            var actionHttpMethodProviderAttribute = actionHttpMethodProviderAttributes.OfType<IActionHttpMethodProvider>().Single();

            if (actionHttpMethodProviderAttribute.HttpMethods.Count != 1)
            {
                throw new Exception("Unable to determine the HTTP method");
            }

            var httpMethod = actionHttpMethodProviderAttribute.HttpMethods.Single();
            Method method;

            if (httpMethod == HttpMethod.Get)
            {
                method = Method.GET;
            }
            else if (httpMethod == HttpMethod.Post)
            {
                method = Method.POST;
            }
            else if (httpMethod == HttpMethod.Put)
            {
                method = Method.PUT;
            }
            else if (httpMethod == HttpMethod.Delete)
            {
                method = Method.DELETE;
            }
            else
            {
                throw new Exception("Unsupported HTTP method");
            }

            var actionNameAttributes = methodInfo.GetCustomAttributes(typeof(ActionNameAttribute), true);

            if (actionNameAttributes.Length != 1)
            {
                throw new Exception("Unable to determine the HTTP action");
            }

            var actionNameAttribute = actionNameAttributes.OfType<ActionNameAttribute>().First();

            if (actionNameAttributes.Length != 1)
            {
                throw new Exception("Unable to determine the HTTP action");
            }

            var action = actionNameAttribute.Name;

            var restRequest = new RestRequest(action, method)
            {
                RequestFormat = DataFormat.Json
            };
            var methodParameters = methodInfo.GetParameters();

            for (var parameterIndex = 0; parameterIndex < methodParameters.Length; ++parameterIndex)
            {
                var methodParameter = methodParameters[parameterIndex];
                var argument = args[parameterIndex];

                if (argument.GetType().IsPrimitive)
                {
                    restRequest.AddParameter(methodParameter.Name, argument);
                }
                else
                {
                    restRequest.AddBody(argument);
                }
            }

            var content = _restClient.Execute(restRequest).Content;

            result = JsonConvert.DeserializeObject(content, methodInfo.ReturnType);

            return true;
        }
    }
}
