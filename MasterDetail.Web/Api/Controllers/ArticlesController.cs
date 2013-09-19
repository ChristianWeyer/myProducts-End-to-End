using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using MasterDetail.DataAccess;
using MasterDetail.Web.Api.DTOs;
using MasterDetail.Web.Api.Hubs;
using MasterDetail.Web.Api.Validation;
using Microsoft.AspNet.SignalR;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebAPI.OutputCache;

namespace MasterDetail.Web.Api.Controllers
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
                orderby a.Id
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
            //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            //    {
            //        Content = new StringContent("Some big shit happened...!") // TODO: get language-specific error message
            //    });

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
        public void Post(string id, ArticleDetailUpdateDto value)
        {
            var entity = new Article
                {
                    Id = value.Id,
                    Code = value.Code,
                    Name = value.Name,
                    Description = value.Description
                };

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                productsContext.Entry(entity).State = EntityState.Added;
            }
            else
            {
                productsContext.Entry(entity).State = EntityState.Modified;
                productsContext.Entry(entity).Property(e => e.ImageUrl).IsModified = false;
            }

            productsContext.SaveChanges();

            var hub = GlobalHost.ConnectionManager.GetHubContext<ClientNotificationHub>();
            hub.Clients.All.articleChange();
        }

        [InvalidateCacheOutput("Get")]
        [InvalidateCacheOutput("GetById")]
        public void Delete(string id)
        {
            productsContext.Entry(new Article { Id = Guid.Parse(id) }).State = EntityState.Deleted;
            productsContext.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            productsContext.Dispose();

            base.Dispose(disposing);
        }
    }
}