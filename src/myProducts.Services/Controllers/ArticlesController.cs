using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.SignalR;
using MyProducts.Model;
using MyProducts.Services.DTOs;
using MyProducts.Services.Hubs;
using Newtonsoft.Json;
using PerfIt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using WebAPI.OutputCache;

namespace MyProducts.Services.Controllers
{
    [ApiExceptionFilter]
    public class ArticlesController : ApiController
    {
        private readonly ProductsContext productsContext;
        private static readonly HashSet<Type> ProductChildTypes = new HashSet<Type>() { typeof(Category) };

        public ArticlesController()
        {
            productsContext = new ProductsContext();
        }

        [CacheOutput(ServerTimeSpan = 3600)]
        [PerfItFilter(Name = "Articles.GetAll", Description = "Gets all items", Counters = new[] { CounterTypes.TotalNoOfOperations, CounterTypes.AverageTimeTaken })]
        public PageResult<ArticleDto> Get(ODataQueryOptions<ArticleDto> options)
        {
            var settings = new ODataQuerySettings { PageSize = 10, EnsureStableOrdering = false };

            var artikelQuery = productsContext.Articles.AsNoTracking().Project().To<ArticleDto>().OrderBy(a => a.Id);
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
            // For demos:
            //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Ooops?!?!?!" });

            Guid guid;

            if (!Guid.TryParse(id, out guid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var artikelQuery = productsContext.Articles.Include("Categories").AsNoTracking()
                .Project().To<ArticleDetailDto>()
                .Where(a => a.Id == guid);

            var artikelDetails = artikelQuery.SingleOrDefault();

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
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var uploadImagesFolder = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Constants.ImagesFolder);
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

            var entity = value.Map();
            productsContext.AttachByIdValue(entity, ProductChildTypes, "ImageUrl");

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
            productsContext.SetDeleted<Article>(Guid.Parse(id));
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