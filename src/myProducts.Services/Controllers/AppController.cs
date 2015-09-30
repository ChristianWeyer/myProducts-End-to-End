using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    [AllowAnonymous]
    public class AppController : ApiController
    {
        private static readonly string _version;

        static AppController()
        {
            _version = typeof(AppController).Assembly.GetName().Version.ToString();
        }

        [HttpGet]
        public string GetVersion()
        {
            return _version;
        }
    }
}
