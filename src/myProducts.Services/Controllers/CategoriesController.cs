using AutoMapper.QueryableExtensions;
using MyProducts.Model;
using MyProducts.Services.DTOs;
using System.Linq;
using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ProductsContext productsContext;

        public CategoriesController()
        {
            productsContext = new ProductsContext();
        }

        public IQueryable<CategoryDto> Get()
        {
            var categories = productsContext.Categories.AsNoTracking().Project().To<CategoryDto>().Distinct().OrderBy(a => a.Name);

            return categories;
        }

        protected override void Dispose(bool disposing)
        {
            productsContext.Dispose();

            base.Dispose(disposing);
        }
    }
}