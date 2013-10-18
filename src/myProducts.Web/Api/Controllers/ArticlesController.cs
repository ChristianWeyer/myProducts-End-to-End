using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using MyProducts.DataAccess;
using MyProducts.Web.Api.DTOs;
using MyProducts.Web.Api.Hubs;
using MyProducts.Web.Api.Validation;
using Newtonsoft.Json;
using PerfIt;
using WebAPI.OutputCache;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace MyProducts.Web.Api.Controllers
{
    [ApiExceptionFilter]
    [ValidationResponseFilter]
    public class ArticlesController : ApiController
    {
        private readonly ProductsContext productsContext;
        private const string imagesFolder = "images";

        public ArticlesController()
        {
            productsContext = new ProductsContext();
        }

        [CacheOutput(ServerTimeSpan = 3600)]
        [PerfItFilter(Name = "Articles.GetAll", Description = "Gets all items", Counters = new[] { CounterTypes.TotalNoOfOperations, CounterTypes.AverageTimeTaken })]
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
        [PerfItFilter(Name = "Articles.GetById", Description = "Gets one item", Counters = new[] { CounterTypes.TotalNoOfOperations, CounterTypes.AverageTimeTaken })]
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
                        ImageUrl = imagesFolder + "/" + artikel.ImageUrl
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
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var uploadImagesFolder = HttpContext.Current.Server.MapPath("../" + imagesFolder);
            var provider = new MultipartFormDataStreamProvider(uploadImagesFolder);
            var postResult = await Request.Content.ReadAsMultipartAsync(provider);

            if (postResult.FormData["model"] == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "No valid model" });
            }

            var value = JsonConvert.DeserializeObject<ArticleDetailUpdateDto>(postResult.FormData["model"]);

            if (value == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "No valid model" });
            }

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

            var imageUrl = entity.Id.ToString() + ".jpg";
            entity.ImageUrl = imageUrl;

            productsContext.SaveChanges();

            if (provider.FileData.Count > 0)
            {
                var fileData = provider.FileData[0];
                File.Move(fileData.LocalFileName, Path.Combine(uploadImagesFolder, imageUrl));
            }

            var hub = GlobalHost.ConnectionManager.GetHubContext<ClientNotificationHub>();
            hub.Clients.All.articleChange();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [InvalidateCacheOutput("Get")]
        [InvalidateCacheOutput("GetById")]
        public void Delete(string id)
        {
            productsContext.Entry(new Article { Id = Guid.Parse(id) }).State = EntityState.Deleted;
            productsContext.SaveChanges();

            var hub = GlobalHost.ConnectionManager.GetHubContext<ClientNotificationHub>();
            hub.Clients.All.articleChange();
        }

        protected override void Dispose(bool disposing)
        {
            productsContext.Dispose();

            base.Dispose(disposing);
        }
    }
}