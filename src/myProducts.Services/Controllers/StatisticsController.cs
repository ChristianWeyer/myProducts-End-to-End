using System.Collections.Generic;
using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    /// <summary>
    /// Web API for delivering statistics data.
    /// </summary>
    [RoutePrefix("api/statistics")]
    public class StatisticsController : ApiController
    {
        /// <summary>
        /// Get sample distribution statistics data.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get sample sales statistics data.
        /// </summary>
        /// <returns></returns>
        [Route("sales")]
        public dynamic GetSales()
        {
            var data = new List<dynamic>
                {
                    new
                        {
                            key = "Total Sales",
                            values = new[]{new {label = "A", value =56000}, new {label="B", value=63000}, new {label="C", value=74000}}
                        },
                    new
                        {
                            key = "Discounted Sales",
                            values = new[]{new {label = "D", value = 52000}, new {label = "E", value = 34000}, new {label = "F", value = 23000}}
                        }
                };

            return data;
        }
    }
}