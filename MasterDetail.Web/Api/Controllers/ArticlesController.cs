using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using MasterDetail.DataAccess;
using Microsoft.AspNet.SignalR;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebAPI.OutputCache;

namespace MasterDetail.Web.Api
{
    [ApiExceptionFilter]
    [ValidationResponseFilter]
    public class ArticlesController : ApiController
    {
        private readonly ProductsContext productsContext;

        public ArticlesController()
        {
            productsContext = new ProductsContext();
        }

        [CacheOutput(ServerTimeSpan = 3600)]
        public PageResult<ArticleDto> Get(ODataQueryOptions<ArticleDto> options)
        {
            var settings = new ODataQuerySettings { PageSize = 10, EnsureStableOrdering = false };

            var artikelQuery =
                from a in productsContext.Articles.AsNoTracking()
                orderby a.Code
                select new ArticleDto()
                {
                    Id = a.Id,
                    Code = a.Code,
                    Name = a.Name
                };
            var results = options.ApplyTo(artikelQuery, settings);

            return new PageResult<ArticleDto>(
                    results as IEnumerable<ArticleDto>,
                    Request.GetNextPageLink(),
                    Request.GetInlineCount());
        }

        [CacheOutput(ServerTimeSpan = 3600)]
        [ActionName("GetById")]
        public ArticleDetailDto Get(string id)
        {
            Guid guid;
            if (!Guid.TryParse(id, out guid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var query =
                from artikel in productsContext.Articles.AsNoTracking()
                where artikel.Id == guid
                select new ArticleDetailDto
                    {
                        Id = artikel.Id,
                        Code = artikel.Code,
                        Name = artikel.Name,
                        Description = artikel.Description,
                        ImageUrl = artikel.ImageUrl
                    };

            var artikelDetails = query.FirstOrDefault();

            if (artikelDetails == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return artikelDetails;
        }

        [InvalidateCacheOutput("Get")]
        [InvalidateCacheOutput("GetById")]
        public void Put(string id, ArticleDetailUpdateDto value)
        {
            var entity = new Article
                {
                    Id = value.Id,
                    Code = value.Code,
                    Name = value.Name,
                    Description = value.Description
                };

            productsContext.Entry(entity).State = EntityState.Modified;
            productsContext.Entry(entity).Property(e => e.ImageUrl).IsModified = false;
            productsContext.SaveChanges();

            var hub = GlobalHost.ConnectionManager.GetHubContext<ClientNotificationHub>();
            hub.Clients.All.articleChanged();
        }

        protected override void Dispose(bool disposing)
        {
            productsContext.Dispose();

            base.Dispose(disposing);
        }
    }
}