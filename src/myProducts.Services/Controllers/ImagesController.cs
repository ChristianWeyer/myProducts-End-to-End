using System.Web.Http.OData;
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
    /// <summary>
    /// Web API for product images.
    /// </summary>
    public class ImagesController : ApiController
    {
        private readonly ProductsContext productsContext;
        private const string imagesFolder = "images";

        /// <summary>
        ///  Default constructor
        /// </summary>
        public ImagesController()
        {
            productsContext = new ProductsContext();
        }

        /// <summary>
        /// List product images URLs.
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        public IQueryable<string> Get()
        {
            var imageUrls = productsContext.Articles.Select(a => imagesFolder + "/" + a.ImageUrl).Distinct();

            return imageUrls;
        }

        /// <summary>
        /// Get an image (JPG) by ID.
        /// </summary>
        /// <param name="id">Image ID</param>
        /// <returns>Raw JPG image data</returns>
        public HttpResponseMessage Get(string id)
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Constants.ImagesFolder);
            var stream = new FileStream(Path.Combine(folder, String.Format("{0}.jpg", id)), FileMode.Open);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream, 65000)
                // Default buffer size may result in very slow downloads.
                // However, buffer size should not go over 85KB due to LOH.
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            return response;
        }

        /// <summary>
        /// Dispose DB context.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            productsContext.Dispose();

            base.Dispose(disposing);
        }
    }
}