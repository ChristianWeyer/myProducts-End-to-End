using MyProducts.Model;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    public class ImagesController : ApiController
    {
        private readonly ProductsContext productsContext;
        private const string imagesFolder = "images";

        public ImagesController()
        {
            productsContext = new ProductsContext();
        }

        [Queryable]
        public IQueryable<string> Get()
        {
            var todos = productsContext.Articles.Select(a => imagesFolder + "/" + a.ImageUrl).Distinct();

            return todos;
        }

        public HttpResponseMessage Get(string id)
        {
            //Guid parsedGuid;

            //if (!Guid.TryParse(id, out parsedGuid))
            //{
            //    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "No valid ID" });
            //}

            var imagesFolder = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Constants.ImagesFolder);
            var stream = new FileStream(Path.Combine(imagesFolder, String.Format("{0}.jpg", id)), FileMode.Open);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            return response;
        }

        protected override void Dispose(bool disposing)
        {
            productsContext.Dispose();

            base.Dispose(disposing);
        }
    }
}