using System.Net;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Globalization;
using System.Threading;

namespace MyProducts.Services.Controllers
{
    public static class ApiControllerExtensions
    {
        public static void ValidateModel<TModel>(this ApiController controller, TModel model)
        {
            var services = controller.Configuration.Services;
            var validator = services.GetBodyModelValidator();
            var selectAction = services.GetActionSelector().SelectAction(controller.ControllerContext);
            var actionContext = new HttpActionContext(controller.ControllerContext, selectAction);
            var metadataProvider = services.GetModelMetadataProvider();

            var culture = controller.Request.Headers.AcceptLanguage.SingleOrDefault().Value;

            if (!String.IsNullOrWhiteSpace(culture))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture);
            }

            if (!validator.Validate(model, typeof(TModel), metadataProvider, actionContext, "data"))
            {
                throw new HttpResponseException(controller.Request.CreateErrorResponse((HttpStatusCode)422, actionContext.ModelState));
            }
        }
    }
}
