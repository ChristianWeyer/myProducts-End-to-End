using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProducts.Services.DTOs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.OData;

namespace MyProducts.Tests
{
    [TestClass]
    public class ArticlesClientTests
    {
        private HttpClient client;

        [TestInitialize]
        public void Init()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://windows8vm/ngmd/api/");
        }

        [TestMethod]
        public async Task Test_GetArticles_Without_Credentials()
        {
            var result = await client.GetAsync("articles");

            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }

        [TestMethod]
        public async Task Test_GetArticles_With_Invalid_Credentials()
        {
            client.SetBasicAuthentication("cw", "foo");
            var result = await client.GetAsync("articles");

            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }

        [TestMethod]
        public async Task Test_GetArticles_With_Valid_Credentials()
        {
            client.SetBasicAuthentication("cw", "cw");
            var result = await client.GetAsync("articles");
            var response = result.EnsureSuccessStatusCode();

            var articles = await result.Content.ReadAsAsync<PageResult<ArticleDto>>();

            Assert.IsNotNull(articles);
        }

        [TestMethod]
        public async Task Test_AddNewArticle_WithoutImage()
        {
            client.SetBasicAuthentication("cw", "cw");

            var article = new ArticleDetailUpdateDto
            {
                Name = "From Test",
                Description = "Foo",
                Code = "X",
                Category = new CategoryDto { Id = Guid.Parse("5040ab6b-34f3-4121-a126-5cade8959beb") }
            };

            var content = new MultipartFormDataContent();
            content.Add(new ObjectContent<ArticleDetailUpdateDto>(article, new JsonMediaTypeFormatter()), "model");

            var response = await client.PostAsync("articles", content);
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task Test_AddNewArticle_WitImage()
        {
            client.SetBasicAuthentication("cw", "cw");

            var article = new ArticleDetailUpdateDto
            {
                Name = "From Test With Image",
                Description = "Bar",
                Code = "Y",
                Category = new CategoryDto { Id = Guid.Parse("5040ab6b-34f3-4121-a126-5cade8959beb") }
            };

            var content = new MultipartFormDataContent();
            content.Add(new ObjectContent<ArticleDetailUpdateDto>(article, new JsonMediaTypeFormatter()), "model");
            content.Add(CreateFileContent(File.OpenRead("pic.jpg"), "image.jpg", "image/jpeg"));

            var response = await client.PostAsync("articles", content);
            response.EnsureSuccessStatusCode();
        }

        private StreamContent CreateFileContent(Stream stream, string fileName, string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"files\"",
                FileName = "\"" + fileName + "\""
            };
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return fileContent;
        }
    }
}
