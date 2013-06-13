using System.Web.Http;

namespace MasterDetail.Web.Api
{
    public class PingController : ApiController
    {
        public bool Get()
        {
            return true;
        }
    }
}