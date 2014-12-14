using System.Collections.Generic;
using System.Dynamic;
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
            dynamic data = new ExpandoObject();

            data.Labels = new[] { "Download Sales", "In-Store Sales", "Mail-Order Sales", "A", "B" };
            data.Data = new[] { 300, 500, 100, 80, 420 };

            return data;
        }

        /// <summary>
        /// Get sample sales statistics data.
        /// </summary>
        /// <returns></returns>
        [Route("sales")]
        public dynamic GetSales()
        {
            dynamic data = new ExpandoObject();

            data.Labels = new[] { "2006", "2007", "2008", "2009", "2010", "2011", "2012" };
            data.Series = new[] { "Series A", "Series B" };
            data.Data = new[] { new[] { 65, 59, 80, 81, 56, 55, 40 }, new[] { 28, 48, 40, 19, 86, 27, 90 } };

            return data;
        }
    }
}