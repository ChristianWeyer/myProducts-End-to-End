using System.Linq;
using System.Web.Http;
using MasterDetail.DataAccess;

namespace MasterDetail.Web.Api.Controllers
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

        protected override void Dispose(bool disposing)
        {
            productsContext.Dispose();

            base.Dispose(disposing);
        }
    }
}