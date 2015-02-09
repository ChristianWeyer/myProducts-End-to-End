using System.Web.Http.OData.Extensions;
using AutoMapper.QueryableExtensions;
using MyProducts.Model;
using MyProducts.Services.DTOs;
using PerfIt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using WebAPI.OutputCache;

namespace MyProducts.Services.Controllers
{
    [AllowAnonymous]
    public class ArticlesDemoController : ApiController
    {
        private readonly ProductsContext productsContext;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ArticlesDemoController()
        {
            productsContext = new ProductsContext();
        }

        public PageResult<ArticleDto> Get(ODataQueryOptions<ArticleDto> options)
        {
            var settings = new ODataQuerySettings { PageSize = 10, EnsureStableOrdering = false };

            var artikelQuery = productsContext.Articles
                .OrderBy(a => a.Id).AsNoTracking().Project().To<ArticleDto>(null,
                dest => dest.Id, dest => dest.Name, dest => dest.Code);
            var results = options.ApplyTo(artikelQuery, settings);

            return new PageResult<ArticleDto>(
                    results as IEnumerable<ArticleDto>,
                    Request.ODataProperties().NextLink,
                    Request.ODataProperties().TotalCount);
        }

        /// <summary>
        /// Get a single article by ID.
        /// </summary>
        /// <param name="id">Article ID</param>
        /// <returns>Article details</returns>
        [CacheOutput(ServerTimeSpan = 3600)]
        [PerfItFilter(Name = "Articles.GetById", Description = "Gets one item", Counters = new[] { CounterTypes.TotalNoOfOperations, CounterTypes.AverageTimeTaken })]
        public ArticleDetailDto Get(string id)
        {
            Guid guid;

            if (!Guid.TryParse(id, out guid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var artikelQuery = productsContext.Articles.Where(a => a.Id == guid)
                .Include(a => a.Category).AsNoTracking()
				.Project().To<ArticleDetailDto>(null);

            var artikelDetails = artikelQuery.SingleOrDefault();

            if (artikelDetails == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return artikelDetails;
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