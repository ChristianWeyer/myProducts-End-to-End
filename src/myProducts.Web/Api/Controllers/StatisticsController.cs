using System;
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
                            key = "Total Sales",
                            values = new[]{new dynamic[]{"A", 56000}, new dynamic[]{"B", 63000}, new dynamic[]{"C", 74000}}
                        },
                    new
                        {
                            key = "Discounted Sales",
                            values = new[]{new dynamic[]{"D", 52000}, new dynamic[]{"E", 34000}, new dynamic[]{"F", 23000}}
                        }
                };

            return data;
        }
    }
}