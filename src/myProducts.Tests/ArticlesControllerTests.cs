using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProducts.Services.DTOs;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.OData;

namespace MyProducts.Tests
{
    [TestClass]
    public class ArticlesControllerTests
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
        public async Task Test_GetArticles_With_Valid_Credentials()
        {
            client.SetBasicAuthentication("cw", "cw");
            var result = await client.GetAsync("articles");
            var response = result.EnsureSuccessStatusCode();

            var articles = await result.Content.ReadAsAsync<PageResult<ArticleDto>>();

            Assert.IsNotNull(articles);
        }
    }
}
