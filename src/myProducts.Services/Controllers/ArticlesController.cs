using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.SignalR;
using MyProducts.Model;
using MyProducts.Services.DTOs;
using MyProducts.Services.Hubs;
using Newtonsoft.Json;
using PerfIt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Thinktecture.Applications.Framework.Entities;
using Thinktecture.Applications.Framework.WebApi;
using Thinktecture.IdentityModel.WebApi;
using WebAPI.OutputCache;

namespace MyProducts.Services.Controllers
{
    /// <summary>
    /// Web API to manage product articles data. Offers Hub functionality to call into clients via SignalR.
    /// </summary>
    [ApiExceptionFilter]
    public class ArticlesController : HubApiController<ClientNotificationHub>
    {
        private readonly ProductsContext productsContext;
        private static readonly HashSet<Type> ProductChildTypes = new HashSet<Type> { typeof(Category) };

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ArticlesController()
        {
            productsContext = new ProductsContext();
        }

        /// <summary>
        /// List articles based on OData query syntax filter.
        /// </summary>
        /// <param name="options">OData query options</param>
        /// <returns>A paged result list of article data</returns>
        [CacheOutput(ServerTimeSpan = 3600)]
        [PerfItFilter(Name = "Articles.GetAll", Description = "Gets all items", Counters = new[] { CounterTypes.TotalNoOfOperations, CounterTypes.AverageTimeTaken })]
        public PageResult<ArticleDto> Get(ODataQueryOptions<ArticleDto> options)
        {
            var settings = new ODataQuerySettings { PageSize = 10, EnsureStableOrdering = false };

            var artikelQuery = productsContext.Articles.OrderBy(a => a.Id).AsNoTracking().Project().To<ArticleDto>();
            var results = options.ApplyTo(artikelQuery, settings);

            return new PageResult<ArticleDto>(
                    results as IEnumerable<ArticleDto>,
                    Request.GetNextPageLink(),
                    Request.GetInlineCount());
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
            //throw new HttpResponseException(HttpStatusCode.BadRequest);

            Guid guid;

            if (!Guid.TryParse(id, out guid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var artikelQuery = productsContext.Articles.Where(a => a.Id == guid)
                .Include(a => a.Category).AsNoTracking()
                .Project().To<ArticleDetailDto>();

            var artikelDetails = artikelQuery.SingleOrDefault();

            if (artikelDetails == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return artikelDetails;
        }

        /// <summary>
        /// Save a new or existing item article - with or without image data.
        /// </summary>
        /// <returns>Nothing, only HTTP status codes</returns>
        [InvalidateCacheOutput("Get")]
        [InvalidateCacheOutput("GetById")]
        [ResourceActionAuthorize("Save", "Article")]
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //var hasAccess = ClaimsAuthorization.CheckAccess("Save", "Article");

            var uploadImagesFolder = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Constants.ImagesFolder);
            var provider = new MultipartFormDataStreamProvider(uploadImagesFolder);
            var postResult = await Request.Content.ReadAsMultipartAsync(provider);

            if (postResult.FormData["model"] == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "No valid model" });
            }

            var articleDto = JsonConvert.DeserializeObject<ArticleDetailUpdateDto>(postResult.FormData["model"]);

            if (articleDto == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "No valid model" });
            }

            this.ValidateModel(articleDto);

            var entity = articleDto.Map();
            productsContext.AttachByIdValue(entity, ProductChildTypes, "ImageUrl");

            var imageUrl = entity.Id.ToString() + ".jpg";
            entity.ImageUrl = imageUrl;

            productsContext.SaveChanges();

            if (provider.FileData.Count > 0)
            {
                var fileData = provider.FileData[0];
                File.Move(fileData.LocalFileName, Path.Combine(uploadImagesFolder, imageUrl));
            }

            Hub.Clients.All.articleChange();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete a given article by ID.
        /// </summary>
        /// <param name="id">Article ID</param>
        [InvalidateCacheOutput("Get")]
        [InvalidateCacheOutput("GetById")]
        public void Delete(string id)
        {
            productsContext.SetDeleted<Article>(Guid.Parse(id));
            productsContext.SaveChanges();

            var hub = GlobalHost.ConnectionManager.GetHubContext<ClientNotificationHub>();
            hub.Clients.All.articleChange();
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