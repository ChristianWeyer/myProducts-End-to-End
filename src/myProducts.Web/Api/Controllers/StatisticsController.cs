using System.Collections.Generic;
using System.Web.Http;

namespace MyProducts.Web.Api.Controllers
{
    [RoutePrefix("api/statistics")]
    public class StatisticsController : ApiController
    {
        [Route("distribution")]
        public dynamic GetDistribution()
        {
            var data = new List<dynamic>
                {
                    new
                        {
                            category = "Asia",
                            value = 33.8
                        },
                    new
                        {
                            category = "Europe",
                            value = 36.1
                        },
                    new
                        {
                            category = "Latin America",
                            value = 11.3
                        },
                    new
                        {
                            category = "Africa",
                            value = 9.6
                        },
                    new
                        {
                            category = "Middle East",
                            value = 5.2
                        },
                    new
                        {
                            category = "North America",
                            value = 3.6
                        }
                };

            return data;
        }


        [Route("sales")]
        public dynamic GetSales()
        {
            var data = new List<dynamic>
                {
                    new
                        {
                            name = "Total Sales",
                            data = new[]{56000, 63000, 74000}
                        },
                    new
                        {
                            name = "Discounted Sales",
                            data = new[]{52000, 34000, 23000}
                        }
                };

            return data;
        }
    }
}