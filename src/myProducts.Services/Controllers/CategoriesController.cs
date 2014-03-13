using AutoMapper.QueryableExtensions;
using MyProducts.Model;
using MyProducts.Services.DTOs;
using System.Linq;
using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    /// <summary>
    /// Web API to provide categories lookup data.
    /// </summary>
    public class CategoriesController : ApiController
    {
        private readonly ProductsContext productsContext;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CategoriesController()
        {
            productsContext = new ProductsContext();
        }

        /// <summary>
        /// List categories ordered by name.
        /// </summary>
        /// <returns></returns>
        public IQueryable<CategoryDto> Get()
        {
            var categories = productsContext.Categories.AsNoTracking().Project().To<CategoryDto>().Distinct().OrderBy(a => a.Name);

            return categories;
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