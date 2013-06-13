using LinqToQuerystring.WebApi;
using MasterDetail.DataAccess;
using Microsoft.AspNet.SignalR;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebAPI.OutputCache;

namespace MasterDetail.Web
{
    [AllowAnonymous]
    public class ArticlesController : ApiController
    {
        private readonly ProductsContext productsContext;

        public ArticlesController()
        {
            productsContext = new ProductsContext();
        }

        [CacheOutput(ServerTimeSpan = 3600)]
        [LinqToQueryable(maxPageSize: 10)]
        public IQueryable<ArticleDto> Get()
        {
            var results =
                from a in productsContext.Articles
                orderby a.Code
                select new ArticleDto()
                {
                    Id = a.Id,
                    Code = a.Code,
                    Name = a.Name
                };

            return results;
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
                from artikel in productsContext.Articles
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
        public void Put(string id, ArticleDetailDto value)
        {
            if (ModelState.IsValid)
            {
                var entity = new Article
                    {
                        Id = value.Id,
                        Code = value.Code,
                        Name = value.Name,
                        Description = value.Description,
                        ImageUrl = value.ImageUrl
                    };

                productsContext.Entry(entity).State = EntityState.Modified;
                productsContext.Entry(entity).Property(e => e.ImageUrl).IsModified = false;
                productsContext.SaveChanges();

                var hub = GlobalHost.ConnectionManager.GetHubContext<ClientNotificationHub>();
                hub.Clients.All.articleChanged();
            }
        }

        protected override void Dispose(bool disposing)
        {
            productsContext.Dispose();

            base.Dispose(disposing);
        }
    }
}