using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MyProducts.Services.Controllers
{
    public static class ApiControllerExtensions
    {
        public static void ValidateModel<TModel>(this ApiController controller, TModel model)
        {
            var svc = controller.Configuration.Services;
            var validator = svc.GetBodyModelValidator();
            var ad = svc.GetActionSelector().SelectAction(controller.ControllerContext);
            var ac = new HttpActionContext(controller.ControllerContext, ad);
            var mp = svc.GetModelMetadataProvider();

            if (!validator.Validate(model, typeof(TModel), mp, ac, "data"))
            {
                throw new HttpResponseException(controller.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ac.ModelState));
            }
        }
    }
}
