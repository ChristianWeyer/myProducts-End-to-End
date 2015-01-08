using System;
using System.Web;
using System.Web.Optimization;

namespace MyProducts.Web
{
    public class CacheManifestHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();
            context.Response.Cache.SetExpires(DateTime.MinValue);

            context.Response.ContentType = "text/cache-manifest";

            context.Response.Write("CACHE MANIFEST" + Environment.NewLine);
            context.Response.Write("#V0.0.0.3" + Environment.NewLine);

            context.Response.Write("CACHE:" + Environment.NewLine);

            foreach (var bundle in BundleTable.Bundles)
            {
                WriteBundle(context, bundle.Path);
            }

            context.Response.Write(Scripts.Url("~/cordova.js") + Environment.NewLine);
            context.Response.Write(Scripts.Url("~/libs/fonts/glyphicons-halflings-regular.woff") + Environment.NewLine);
            context.Response.Write(Scripts.Url("~/libs/fonts/fontawesome-webfont.woff?v=4.2.0") + Environment.NewLine);
            context.Response.Write(Scripts.Url("~/assets/images/logo.png") + Environment.NewLine);
            context.Response.Write(Scripts.Url("~/assets/images/lang_de_t.png") + Environment.NewLine);
            context.Response.Write(Scripts.Url("~/assets/images/lang_en_t.png") + Environment.NewLine);

            context.Response.Write("NETWORK:" + Environment.NewLine);
            context.Response.Write("*" + Environment.NewLine);
        }

        private void WriteBundle(HttpContext context, string virtualPath)
        {
            if (IsDebug)
            {
                foreach (var contentVirtualPath in BundleResolver.Current.GetBundleContents(virtualPath))
                {
                    context.Response.Write(Scripts.Url(contentVirtualPath) + Environment.NewLine);
                }
            }
            else
            {
                context.Response.Write(Scripts.Url(virtualPath) + Environment.NewLine);
            }
        }

        private bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
            return false;
#endif
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
