using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace MasterDetail.Web.Api.Controllers
{
    public class ImageUploadController : ApiController
    {
        private static readonly string mediaPath = HttpContext.Current.Server.MapPath("~/Images/");
        private static readonly string applicationPath = HostingEnvironment.MapPath("~/");

        public async Task<IEnumerable<string>> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var multipartStreamProvider = new MultipartFormDataStreamProvider(mediaPath);
            await Request.Content.ReadAsMultipartAsync(multipartStreamProvider);

            return multipartStreamProvider.FileData.Select(fd => ResolveVirtual(fd.LocalFileName));
        }


        private static string ResolveVirtual(string physicalPath)
        {
            var url = physicalPath.Substring(applicationPath.Length).Replace('\\', '/');

            return (url);
        }
    }
}