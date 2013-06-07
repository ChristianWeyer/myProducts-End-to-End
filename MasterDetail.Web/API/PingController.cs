using System.Web.Http;

namespace MasterDetail.Web
{
    public class PingController : ApiController
    {
        public bool Get()
        {
            return true;
        }
    }
}